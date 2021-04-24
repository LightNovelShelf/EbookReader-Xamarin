using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Graphics;
using Android.Net;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Android.Views;
using Android.Webkit;
using AndroidX.AppCompat.App;
using AndroidX.Core.App;
using AndroidX.Core.Content;
using Environment = Android.OS.Environment;
#pragma warning disable 618

namespace EbookReader
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private readonly string[] _permissionsStorage = { Manifest.Permission.ReadExternalStorage };
        private const int RequestPermissionCode = 1;
        public WebView _webView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            if (Build.VERSION.SdkInt >= BuildVersionCodes.M && Build.VERSION.SdkInt <= BuildVersionCodes.Q)
            {
                Window.SetStatusBarColor(Color.Argb(255, 255, 255, 255));
                Window.DecorView.SystemUiVisibility = (StatusBarVisibility)Android.Views.SystemUiFlags.LightStatusBar;
            }
            else
            {
                //Todo 兼容Android 11
            }
            // 申请权限
            RequestAllPower();

            _webView = FindViewById<WebView>(Resource.Id.webView1);
            WebView.SetWebContentsDebuggingEnabled(true);
            var webSetting = _webView.Settings;
            webSetting.JavaScriptEnabled = true;
            webSetting.DomStorageEnabled = true;
            webSetting.AllowFileAccess = true;
            webSetting.AllowFileAccessFromFileURLs = true;
            _webView.AddJavascriptInterface(new Device(this), "device");
            _webView.SetWebViewClient(new MyWebViewClient());

            const string path = "/storage/emulated/0/轻小说/东京暗鸦/东京暗鸦 01.epub";
            _webView.LoadUrl(
                $"http://172.18.20.250:8080/#/read/{Uri.Encode(path)}/{System.IO.Path.GetFileName(path)}");
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
    }
}