namespace BLL
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// 自定义异常  系统将捕获此异常，并转到自定义错误页面

    /// </summary>
    [Serializable()]
    public class FriendlyException : Exception
    {
        /// <summary>
        /// 构造异常

        /// </summary>
        public FriendlyException()
            : base()
        {
        }

        /// <summary>
        /// 构造异常

        /// </summary>
        /// <param name="message">显示信息</param>
        public FriendlyException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// 构造异常

        /// </summary>
        /// <param name="message">显示信息</param>
        /// <param name="inner">内部异常类</param>
        public FriendlyException(string message, Exception inner)
            : base(message, inner)
        {
        }

        /// <summary>
        ///  构造异常

        /// </summary>
        /// <param name="info">序列化信息</param>
        /// <param name="context">数据流上下文</param>
        protected FriendlyException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
