using System;
using System.IO;
using Android.Content;
using Android.Database;
using Android.Provider;
using Android.Util;
using Android.Widget;

namespace EbookReader
{
    public static class UriHelper
    {
        public static string GetFileName(this Context context, Intent intent)
        {
            ICursor cursor = null;
            const string column = MediaStore.IMediaColumns.DisplayName;
            var projection =  new []{MediaStore.IMediaColumns.DisplayName};
            try
            {
                cursor = context.ContentResolver.Query(intent.Data, projection, null, null, null);
                if (cursor != null && cursor.MoveToFirst())
                {
                    return Path.GetFileNameWithoutExtension(cursor.GetString(cursor.GetColumnIndexOrThrow(column)));
                }
            }
            catch (Exception e)
            {
                Toast.MakeText(context, e.Message, ToastLength.Short).Show();
                Log.Warn("GetFileName", e.Message);
            }
            finally
            {
                cursor?.Dispose();
            }

            return null;
        }

        public static string GetPath(this Intent intent)
        {
            var path = intent.DataString;
            if (path.StartsWith("content://"))
            {
                return path;
            }
            else
            {
                if (path.StartsWith("file://")) path = path[7..];
                return path;
            }
        }
    }
}