using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using EbookReader.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public async ValueTask<MessageModel<byte[]>> ReadFile(string path)
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
    }
}