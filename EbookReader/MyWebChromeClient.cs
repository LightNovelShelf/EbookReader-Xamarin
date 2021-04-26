using Android.App;
using Android.Content;
using Android.Webkit;

namespace EbookReader
{
    public class MyWebChromeClient : WebChromeClient
    {
        private readonly Context _mContext;
        private JsResult _res;

        public MyWebChromeClient(Context context)
        {
            _mContext = context;
        }
        
        public override bool OnJsConfirm(WebView view, string url, string message, JsResult result)
        {
            _res = result;
            var builder = new AlertDialog.Builder(_mContext);
            builder.SetTitle("提示");
            builder.SetMessage(message);
            builder.SetPositiveButton(Android.Resource.String.Ok, OkAction);
            builder.SetNegativeButton(Android.Resource.String.Cancel, CancelAction);
            builder.Create();
            builder.Show();
            return true;
        }

        private void CancelAction(object sender, DialogClickEventArgs e)
        {
            _res.Cancel();
        }

        private void OkAction(object sender, DialogClickEventArgs e)
        {
            _res.Confirm();
        }
    }
}