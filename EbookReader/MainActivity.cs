using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Graphics;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Webkit;
using Android.Widget;
using AndroidX.AppCompat.App;
using AndroidX.Core.App;
using AndroidX.Core.Content;
using AndroidX.DocumentFile.Provider;
using Org.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Environment = Android.OS.Environment;
using Uri = Android.Net.Uri;

#pragma warning disable 618

namespace EbookReader
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private readonly string[] _permissionsStorage = {Manifest.Permission.ReadExternalStorage};
        private const int RequestPermissionCode = 1;
        public WebView WebView;
        public List<string> Back = new List<string>();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            if (Build.VERSION.SdkInt is >= BuildVersionCodes.M and <= BuildVersionCodes.Q)
            {
                Window.SetStatusBarColor(Color.Argb(255, 255, 255, 255));
                Window.DecorView.SystemUiVisibility = (StatusBarVisibility) Android.Views.SystemUiFlags.LightStatusBar;
            }
            else
            {
                //Todo 兼容Android 11
            }

            // 申请权限
            RequestAllPower();

            WebView = FindViewById<WebView>(Resource.Id.webView1);
            WebView.SetWebContentsDebuggingEnabled(true);
            var webSetting = WebView.Settings;
            webSetting.JavaScriptEnabled = true;
            webSetting.DomStorageEnabled = true;
            webSetting.AllowFileAccess = true;
            webSetting.AllowFileAccessFromFileURLs = true;
            WebView.AddJavascriptInterface(new Device(this), "device");
            WebView.SetWebViewClient(new MyWebViewClient());
            WebView.SetWebChromeClient(new MyWebChromeClient(this));

            // var path = Intent?.GetStringExtra("path");
            string name = null;
            if (Intent?.Data != null)
            {
                name = this.GetFileName(Intent);
            }

            // const string path = "/storage/emulated/0/轻小说/东京暗鸦/东京暗鸦 01.epub";
            if (string.IsNullOrWhiteSpace(name))
            {
                // WebView.LoadUrl("http://192.168.10.103:8080");
                WebView.LoadUrl("file:///android_asset/dist/index.html");
            }
            else
            {
                if (Build.VERSION.SdkInt is >= BuildVersionCodes.M and <= BuildVersionCodes.Q)
                {
                    Window.DecorView.SystemUiVisibility =
                        (StatusBarVisibility) (SystemUiFlags.Fullscreen | SystemUiFlags.LayoutStable |
                                               SystemUiFlags.LightStatusBar | SystemUiFlags.Visible);
                    Window.SetStatusBarColor(Color.Transparent);
                    Window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);
                }

                // WebView.LoadUrl($"http://192.168.10.103:8080/#/read/{Uri.Encode(name)}");
                WebView.LoadUrl($"file:///android_asset/dist/index.html#/read/{Uri.Encode(name)}");
            }

            // var cursor = ContentResolver.Query(MediaStore.Files.GetContentUri("external"), null, 
            //     MediaStore.Files.IFileColumns.MimeType + "='application/epub+zip'", null,
            //     null);
        }

        public override bool OnKeyUp(Keycode keyCode, KeyEvent e)
        {
            if (keyCode == Keycode.Back && e.Action == KeyEventActions.Up)
            {
                if (Back.Any())
                {
                    WebView.EvaluateJavascript($"back('{Back.Last()}')",null);
                    return true;
                }

                if (WebView.CanGoBack())
                {
                    WebView.GoBack();
                    return true;
                }
            }
            return base.OnKeyUp(keyCode, e);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (resultCode == Result.Ok)
            {
                switch (requestCode)
                {
                    case 1:
                    {
                        // 打开一本书
                        if (data?.Data != null)
                        {
                            TakePersistableUriPermission(data);
                            LoadData(data);
                        }

                        break;
                    }
                    case 2:
                    {
                        var path = data?.GetPath();
                        var name = this.GetFileName(data);
                        WebView.EvaluateJavascript($"addToBook('{path}','{name}')", null);
                        break;
                    }
                    case 3:
                    {
                        // 导入一个文件夹
                        if (data?.Data != null)
                        {
                            TakePersistableUriPermission(data);
                            var treeUri = data.Data;
                            Log.Info("Uri", data.DataString ?? string.Empty);
                            // var basePath = FileUtil.getFullPathFromTreeUri(treeUri, this);
                            var documentFile = DocumentFile.FromTreeUri(this, treeUri);
                            Task.Run(() => ScanEpub(documentFile));
                        }

                        break;
                    }
                    case 4:
                    {
                        WebView.EvaluateJavascript("moveToFirst(0)", null);
                        break;
                    }
                }
            }
        }

        private void ScanEpub(DocumentFile documentFile)
        {
            var dirs = new List<DocumentFile>();
            var addFile = new List<DocumentFile>();
            foreach (var file in documentFile.ListFiles())
            {
                if (file.IsVirtual) continue;
                if (file.IsDirectory && !file.Name.StartsWith(".")) dirs.Add(file);
                if (file.IsFile && file.Name.EndsWith("epub", StringComparison.InvariantCultureIgnoreCase))
                    addFile.Add(file);
            }

            if (addFile.Any())
            {
                var baseName = documentFile.Name;
                var result = new JSONArray();
                var currentTime = DateTime.Now.ToString("yyyy-MM-dd");
                foreach (var file in addFile)
                {
                    var jsonObj = new JSONObject();
                    var fileName = file.Name[0..^5];
                    jsonObj.Put("book_title", fileName);
                    var path = file.Uri.ToString();
                    jsonObj.Put("book_path", path);
                    jsonObj.Put("book_cover", path.Md5());
                    jsonObj.Put("add_time", currentTime);
                    result.Put(jsonObj);
                }

                WebView.Post(() => { WebView.EvaluateJavascript($"addToBooks('{baseName}','{result}')", null); });
            }

            foreach (var d in dirs)
            {
                ScanEpub(d);
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions,
            [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        private void RequestAllPower()
        {
            if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.ReadExternalStorage) != Permission.Granted)
            {
                if (ActivityCompat.ShouldShowRequestPermissionRationale(this, Manifest.Permission.ReadExternalStorage))
                {
                }
                else
                {
                    ActivityCompat.RequestPermissions(this, _permissionsStorage, RequestPermissionCode);
                }
            }

            if (Build.VERSION.SdkInt > BuildVersionCodes.Q)
            {
                if (!Environment.IsExternalStorageManager)
                {
                    var callIntent = new Android.Content.Intent(Settings.ActionManageAllFilesAccessPermission);
                    StartActivity(callIntent);
                }
            }
        }

        private void GetPath(Intent intent)
        {
            var uri = intent.Data;
        }

        private void LoadData(Intent intent)
        {
            var name = this.GetFileName(intent);
            var newIntent = new Intent(this, typeof(MainActivity));
            newIntent.SetData(intent.Data);
            StartActivityForResult(newIntent, 2);
        }

        private void TakePersistableUriPermission(Intent intent)
        {
            if (intent.Data != null)
            {
                var flags = intent.Flags &
                            (ActivityFlags.GrantReadUriPermission | ActivityFlags.GrantWriteUriPermission);
                try
                {
                    ContentResolver?.TakePersistableUriPermission(intent.Data, flags);
                }
                catch (Exception e)
                {
                    Toast.MakeText(this, e.Message, ToastLength.Short).Show();
                    Log.Warn("TakePersistableUriPermission", e.Message);
                }
            }
        }

        protected override void OnDestroy()
        {
            WebView?.Destroy();
            base.OnDestroy();
        }
    }
}