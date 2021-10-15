using Android.Graphics;
using Android.Widget;
using EbookReader.Models;
using EbookReader.Models.Result;
using Microsoft.AspNetCore.SignalR;
using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Wuyu.Epub;
using static EbookReader.Util.Util;
using Path = System.IO.Path;

namespace EbookReader.Hubs
{
    class ApiHub : Hub
    {
        // 弹出提示
        public MessageModel ShowToast(string str)
        {
            try
            {
                MainActivity.Activity.RunOnUiThread(() =>
                {
                    Toast.MakeText(MainActivity.Activity.ApplicationContext,
                            str,
                            ToastLength.Long)
                        ?.Show();
                });
            }
            catch (Exception e)
            {
                return MessageHelp.Error(e.Message);
            }

            return MessageHelp.Success();
        }

        // 取根目录
        public MessageModel<string> GetExternalStorageDirectory()
        {
            // TODO 弃用又不是不能用
            return MessageHelp.Success(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath);
        }

        // 取目录下文件
        public MessageModel<string[]> GetFiles(string path)
        {
            try
            {
                var files = Directory.GetFiles(path);
                return MessageHelp.Success(files);
            }
            catch (Exception e)
            {
                return MessageHelp.Error<string[]>(e.Message);
            }
        }

        // 取目录下文件夹
        public MessageModel<string[]> GetDirectories(string path)
        {
            try
            {
                var files = Directory.GetDirectories(path);
                return MessageHelp.Success(files);
            }
            catch (Exception e)
            {
                return MessageHelp.Error<string[]>(e.Message);
            }
        }

        // 读取文件, 测试下来大文件会报错断开连接
        public async Task<MessageModel<byte[]>> ReadFile(string path)
        {
            try
            {
                return MessageHelp.Success(await File.ReadAllBytesAsync(path));
            }
            catch (Exception e)
            {
                return MessageHelp.Error<byte[]>(e.Message);
            }
        }

        public MessageModel DeleteFile(string path)
        {
            try
            {
                File.Delete(path);
                return MessageHelp.Success();
            }
            catch (Exception e)
            {
                return MessageHelp.Error(e.Message);
            }
        }

        public async Task<MessageModel<EpubInfo>> GetEpubInfo(string path, bool isDev)
        {
            try
            {
                var md5 = GetMD5HashFromFile(path);
                //var md5 = path.Md5();
                using var ms = new MemoryStream();
                using var epub = EpubBook.ReadEpub(path, ms);
                using var coverStream = epub.GetItemStreamByID(epub.Cover);
                using var image = BitmapFactory.DecodeStream(coverStream);
                var coverPath = MainActivity.Activity.GetExternalFilesDir("image").AbsolutePath + "/" + md5;
                using var fileStream = new FileStream(coverPath, FileMode.Create);
                double ratio;
                if (image.Height > 1200) ratio = 0.3;
                else if (image.Height > 800) ratio = 0.5;
                else if (image.Height > 600) ratio = 0.7;
                else ratio = 1;
                await image.CompressBitmapAsync(fileStream, 100, ratio);

                var coverUrl = coverPath;
                if (isDev) coverUrl = $"http://127.0.0.1:{KestrelWebHost.Server.Port}/{Path.GetRelativePath(KestrelWebHost.Server.Root, coverPath)}";

                return MessageHelp.Success(new EpubInfo { id = md5, title = epub.Title, cover = coverUrl });
            }
            catch (Exception e)
            {
                return MessageHelp.Error<EpubInfo>(e.Message);
            }
        }

        // 取文件目录
        public async Task<MessageModel<string[]>> GetEpubPath(string path, bool isDev)
        {
            // path 是真实目录，这里解压后传递opf的目录
            try
            {
                var cacheDir = MainActivity.Activity.ExternalCacheDir.AbsolutePath;
                var md5 = GetMD5HashFromFile(path);
                //var md5 = path.Md5();
                ZipFile.ExtractToDirectory(path, $"{cacheDir}/{md5}", true);

                var text = await File.ReadAllTextAsync($"{cacheDir}/{md5}/META-INF/container.xml");
                var opfPath = Regex.Match(text, "full-path=\"(.*?)\"").Groups[1].Value;
                var relaPath = Path.GetRelativePath(KestrelWebHost.Server.Root, cacheDir);

                if (isDev) return MessageHelp.Success(new[] { $"http://127.0.0.1:{KestrelWebHost.Server.Port}/{relaPath}/{md5}/{opfPath}", md5 });
                return MessageHelp.Success(new[] { $"{cacheDir}/{md5}/{opfPath}", md5 });
            }
            catch (Exception e)
            {
                return MessageHelp.Error<string[]>(e.Message);
            }
        }

        // 扫描Epub并调用Api添加到书架中
        public async Task<MessageModel> ScanEpub(string path)
        {
            try
            {
                var files = Directory.GetFiles(path, "*.epub").Select(x => new { id = Guid.NewGuid(), path = x });
                if (files.Any())
                {
                    await Clients.Caller.SendAsync("AddBookGroup", new { id = Guid.NewGuid(), data = files });
                }

                foreach (var item in Directory.GetDirectories(path))
                {
                    await ScanEpub(item);
                }

                return MessageHelp.Success();
            }
            catch (Exception e)
            {
                return MessageHelp.Error(e.Message);
            }
        }
    }
}