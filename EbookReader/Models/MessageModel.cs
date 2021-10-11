using System;
using System.Collections.Generic;
using System.Text;

namespace EbookReader.Models
{
    /// <summary>
    /// 通用返回信息类
    /// </summary>
    public class MessageModel<T>
    {
        /// <summary>
        /// 状态码
        /// </summary>
        public int Status { get; set; } = 200;
        /// <summary>
        /// 操作是否成功
        /// </summary>
        public bool Success { get; set; } = true;
        /// <summary>
        /// 返回信息
        /// </summary>
        public string Msg { get; set; } = string.Empty;
        /// <summary>
        /// 返回数据集合
        /// </summary>
        public T Response { get; set; }
    }

    /// <summary>
    /// 通用返回信息类
    /// </summary>
    public class MessageModel
    {
        /// <summary>
        /// 状态码
        /// </summary>
        public int Status { get; set; } = 200;
        /// <summary>
        /// 操作是否成功
        /// </summary>
        public bool Success { get; set; } = true;
        /// <summary>
        /// 返回信息
        /// </summary>
        public string Msg { get; set; } = string.Empty;
    }

    public static class MessageHelp
    {
        public static MessageModel Success()
        {
            return new MessageModel() { Success = true };
        }

        public static MessageModel<T> Success<T>(T response)
        {
            return new MessageModel<T>() { Success = true, Response = response };
        }

        public static MessageModel Error(string msg, int code = 200)
        {
            return new MessageModel()
            {
                Success = false,
                Msg = msg,
                Status = code,
            };
        }

        public static MessageModel<T> Error<T>(string msg, int code = 200)
        {
            return new MessageModel<T>()
            {
                Success = false,
                Msg = msg,
                Status = code,
            };
        }
    }
}