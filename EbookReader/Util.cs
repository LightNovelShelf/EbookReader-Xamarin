using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Android.Graphics;
using Java.IO;
using File = System.IO.File;

namespace EbookReader
{
    public static class Util
    {
        public static string Md5(this string str)
        {
            var md5Hash = MD5.Create();
            var data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(str));
            var sBuilder = new StringBuilder();
            foreach (var t in data)
            {
                sBuilder.Append(t.ToString("x2"));
            }

            return sBuilder.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bmp"></param>
        /// <param name="outStream"></param>
        /// <param name="quality">0-100，越小质量越差</param>
        /// <param name="ratio"></param>
        public static void CompressBitmap(this Bitmap bmp, Stream outStream, int quality = 90, double ratio = 0.5)
        {
            var height = (int) (bmp.Height * ratio);
            var width = (int) (bmp.Width * ratio);
            var result = Bitmap.CreateBitmap(width, height, Bitmap.Config.Argb8888);
            var canvas = new Canvas(result);
            var rect = new Rect(0, 0, width, height);
            canvas.DrawBitmap(bmp, null, rect, null);
            result.Compress(Bitmap.CompressFormat.Jpeg, quality, outStream);
        }
    }
}