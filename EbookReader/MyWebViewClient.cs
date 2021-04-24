#nullable enable
using System;
using Android.Util;
using Android.Webkit;

namespace EbookReader
{
    public class MyWebViewClient : WebViewClient
    {
        [Obsolete("deprecated")]
        public override bool ShouldOverrideUrlLoading(WebView? view, string? url)
        {
            Log.Debug("MainActivity", $"Url: {url}");
            if (url != null) view?.LoadUrl(url);
            return true;
        }
    }
}