using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace IE310.Core.JsonService
{
    /// <summary>
    /// JsonService服务异常
    /// </summary>
    [Serializable]
    public class JsonServiceException : Exception
    {
        /// <summary>
        /// 初始化 JsonServiceException 类的新实例。
        /// </summary>
        public JsonServiceException()
            : base()
        {
        }
        /// <summary>
        /// 使用指定错误信息初始化 JsonServiceException 类的新实例。
        /// </summary>
        /// <param name="message">描述错误的消息。</param>
        public JsonServiceException(string message)
            : base(message)
        {
        }
        /// <summary>
        /// 使用指定错误信息和对作为此异常原因的内部异常的引用来初始化 System.FieldAccessException 类的新实例。
        /// </summary>
        /// <param name="message">解释异常原因的错误信息。</param>
        /// <param name="inner">导致当前异常的异常。</param>
        public JsonServiceException(string message, Exception inner)
            : base(message, inner)
        {
        }

        /// <summary>
        /// 用序列化数据初始化 JsonServiceException 类的新实例。
        /// </summary>
        /// <param name="info">保存序列化对象数据的对象。</param>
        /// <param name="context">有关源或目标的上下文信息。</param>
        protected JsonServiceException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
