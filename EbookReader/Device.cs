using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Util;
using Android.Views;
using Android.Webkit;
using Android.Widget;
using Java.Interop;
using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Threading.Tasks;
using Wuyu.Epub;
using Uri = Android.Net.Uri;

namespace EbookReader
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public class Device : Java.Lang.Object
    {
        private readonly MainActivity _activity;

        public Device(MainActivity activity)
        {
            _activity = activity;
        }

        [JavascriptInterface]
        [Export("toast")]
        public void Toast(string msg)
        {
            Android.Widget.Toast.MakeText(_activity, msg, ToastLength.Short).Show();
        }

        [JavascriptInterface]
        [Export("getStatusBarHeight")]
        public int GetStatusBarHeight()
        {
            var height = 0;
            if (Build.VERSION.SdkInt is >= BuildVersionCodes.M and <= BuildVersionCodes.Q)
            {
                var resourceId = _activity.ApplicationContext.Resources.GetIdentifier(
                    "status_bar_height",
                    "dimen",
                    "android"
                );
                if (resourceId > 0)
                {
                    height = _activity.ApplicationContext.Resources.GetDimensionPixelSize(resourceId);
                }
            }

            return height;
        }

        [JavascriptInterface]
        [Export("readFile")]
        public string ReadFile(string path)
        {
            Log.Info("Path", path);
            var fileName = path.Md5() + ".epub";
            var fullPath = $"{GetExternalFilesDir("Document")}/{fileName}";

            if (path.StartsWith("content://"))
            {
                using (var stream = _activity.ContentResolver.OpenInputStream(_activity.Intent.Data))
                {
                    using var fileStream = new FileStream(fullPath, FileMode.Create);
                    stream.CopyTo(fileStream);
                    fileStream.Dispose();
                }
                return fullPath;
            }
            else
            {
                if (path.StartsWith("file://")) path = path[7..];
                return path;
            }
        }

        private string ReadFileFromUri(Uri uri)
        {
            var stream = _activity.ContentResolver.OpenInputStream(uri);
            var bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            stream.Dispose();
            var result = Base64.EncodeToString(bytes, Base64Flags.NoWrap);
            return result;
        }

        [JavascriptInterface]
        [Export("readFileAsync")]
        public void ReadFileAsync(int resultCode, string name, string path)
        {
            Task.Run(() =>
            {
                try
                {
                    var data = ReadFile(path);
                    _activity.WebView.Post(() =>
                    {
                        _activity.WebView.EvaluateJavascript($"readFileResult({resultCode},'{name}','{data}')",
                            null);
                    });
                }
                catch (Exception e)
                {
                    Log.Warn("ReadFileAsync", e.Message);
                }
            });
        }

        [JavascriptInterface]
        [Export("saveFile")]
        public void SaveFile(string hash, string type, string base64Data)
        {
            var data = Convert.FromBase64String(base64Data.Split(',')[1]);
            var dir = _activity.GetExternalFilesDir(type);
            File.WriteAllBytes($"{dir}/{hash}", data);
        }

        [JavascriptInterface]
        [Export("loadCover")]
        public void LoadCover(int resultCode, string name, string path, string uriString)
        {
            Task.Run(() =>
            {
                var uri = Uri.Parse(uriString);
                if (uri == null) return;
                var stream = _activity.ContentResolver.OpenInputStream(uri);
                var memoryStream = new MemoryStream();
                var epub = EpubBook.ReadEpub(stream, memoryStream);
                var coverStream = epub.GetItemByID(epub.Cover);
                var image = BitmapFactory.DecodeStream(coverStream);
                var fileStream = new FileStream(path, FileMode.Create);
                image.CompressBitmap(fileStream, 80, 0.3);

                fileStream.Dispose();
                image.Dispose();
                coverStream.Dispose();
                memoryStream.Dispose();
                stream.Dispose();

                var bytes = File.ReadAllBytes(path);
                var result = Base64.EncodeToString(bytes, Base64Flags.NoWrap);
                _activity.WebView.Post(() =>
                {
                    _activity.WebView.EvaluateJavascript($"readFileResult({resultCode},'{name}','{result}')",
                        null);
                });
            });
        }

        [JavascriptInterface]
        [Export("setResultOK")]
        public void SetResultOK()
        {
            _activity.SetResult(Result.Ok, _activity.Intent);
            _activity.DeleteCache();
        }

        [JavascriptInterface]
        [Export("setProgress")]
        public void SetProgress(int num)
        {
            Log.Debug("SetProgress", num.ToString());
            _activity.Intent.PutExtra("progress", num / 10.0);
        }
        
        [JavascriptInterface]
        [Export("setLight")]
        public void SetLight()
        {
            _activity.Window.DecorView.SystemUiVisibility =
                (StatusBarVisibility) SystemUiFlags.LightStatusBar;
        }

        [JavascriptInterface]
        [Export("setDark")]
        public void SetDark()
        {
            _activity.Window.DecorView.SystemUiVisibility = 0;
        }

        [JavascriptInterface]
        [Export("fileExits")]
        public bool FileExits(string path)
        {
            return new Java.IO.File(path).Exists();
        }

        [JavascriptInterface]
        [Export("getExternalFilesDir")]
        public string GetExternalFilesDir(string type)
        {
            return _activity.GetExternalFilesDir(type).Path;
        }

        [JavascriptInterface]
        [Export("push")]
        public void Push(string str)
        {
            _activity.Back.Add(str);
        }

        [JavascriptInterface]
        [Export("pop")]
        public void Pop()
        {
            _activity.Back.RemoveAt(_activity.Back.Count - 1);
        }

        [JavascriptInterface]
        [Export("openBook")]
        public void OpenBook(string name, string path)
        {
            var newIntent = new Intent(_activity, typeof(MainActivity));
            newIntent.SetData(Uri.Parse(path));
            _activity.StartActivityForResult(newIntent, 4);
        }

        /// <summary>
        /// 选择书籍打开
        /// </summary>
        [JavascriptInterface]
        [Export("choiceBook")]
        public void ChoiceBook()
        {
            var intent = new Intent(Intent.ActionOpenDocument);
            intent.AddCategory(Intent.CategoryOpenable);
            intent.SetType("application/epub+zip");
            intent.PutExtra(Intent.ExtraLocalOnly, true);
            intent.AddFlags(ActivityFlags.GrantPersistableUriPermission);
            intent.AddFlags(ActivityFlags.GrantReadUriPermission);
            _activity.StartActivityForResult(intent, 1);
        }

        [JavascriptInterface]
        [Export("choiceDir")]
        public void ChoiceDir()
        {
            var intent = new Intent(Intent.ActionOpenDocumentTree);
            intent.PutExtra(Intent.ExtraLocalOnly, true);
            intent.AddFlags(ActivityFlags.GrantPersistableUriPermission);
            intent.AddFlags(ActivityFlags.GrantReadUriPermission);
            _activity.StartActivityForResult(intent, 3);
        }

        [JavascriptInterface]
        [Export("readBook")]
        public void ReadBook()
        {
            Task.Run(() =>
            {
                if (_activity.Intent?.Data != null)
                {
                    var path = _activity.Intent.GetPath();
                    var fullPath = ReadFile(path);

                    _activity.WebView.Post(() =>
                    {
                        _activity.WebView.EvaluateJavascript($"loadBook('{path}','{fullPath}')", null);
                    });
                }

            });
        }
    }
}