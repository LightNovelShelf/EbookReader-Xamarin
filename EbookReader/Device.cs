using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using Android.Webkit;
using Java.Interop;

namespace EbookReader
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "UnusedMember.Local")]
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
        public int GetStatusBarHeight() {
            var height = 0;
            if (Build.VERSION.SdkInt >= BuildVersionCodes.M && Build.VERSION.SdkInt<=BuildVersionCodes.Q)
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
        [Export("setResultOK")]
        public void SetResultOK()
        {
            _activity.SetResult(Result.Ok, _activity.Intent);
        }

        [JavascriptInterface]
        [Export("setLight")]
        public void SetLight()
        {
            _activity.Window.DecorView.SystemUiVisibility = (StatusBarVisibility)Android.Views.SystemUiFlags.LightStatusBar;
        }
        
        [JavascriptInterface]
        [Export("setDark")]
        public void SetDark()
        {
            _activity.Window.DecorView.SystemUiVisibility = 0;
        }
        
        [JavascriptInterface]
        [Export("getExternalFilesDir")]
        public string GetExternalFilesDir(string type)
        {
            return _activity.GetExternalFilesDir(type).Path;
        }
    }
}