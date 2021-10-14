using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EbookReader.Util
{
    static class Util
    {
        /// <summary>
        /// 获取文件的MD5码
        /// </summary>
        /// <param name="fileName">传入的文件名（含路径及后缀名）</param>
        /// <returns></returns>
        public static string GetMD5HashFromFile(string fileName)
        {
            try
            {
                FileStream file = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
                MD5 md5 = new MD5CryptoServiceProvider();
                byte[] retVal = md5.ComputeHash(file);
                file.Close();
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < retVal.Length; i++)
                {
                    sb.Append(retVal[i].ToString("x2"));
                }
                return sb.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception("GetMD5HashFromFile() fail,error:" + ex.Message);
            }
        }

        /// <summary>
        /// 取字符串md5
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
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
        /// 压缩图片
        /// </summary>
        /// <param name="bmp"></param>
        /// <param name="outStream"></param>
        /// <param name="quality">0-100，越小质量越差</param>
        /// <param name="ratio"></param>
        public static async Task CompressBitmapAsync(this Bitmap bmp, Stream outStream, int quality = 90, double ratio = 0.5)
        {
            var height = (int)(bmp.Height * ratio);
            var width = (int)(bmp.Width * ratio);
            var result = Bitmap.CreateBitmap(width, height, Bitmap.Config.Argb8888);
            var canvas = new Canvas(result);
            var rect = new Rect(0, 0, width, height);
            canvas.DrawBitmap(bmp, null, rect, null);
            await result.CompressAsync(Bitmap.CompressFormat.WebpLossy, quality, outStream);
        }
    }
}