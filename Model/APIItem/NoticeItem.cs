using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class NoticeItem
    {
        /// <summary>
        /// ID
        /// </summary>
        public string NoticeId
        {
            get;
            set;
        }
        /// <summary>
        /// 编号
        /// </summary>
        public string NoticeCode
        {
            get;
            set;
        }

        /// <summary>
        /// 标题
        /// </summary>
        public string NoticeTitle
        {
            get;
            set;
        }
        /// <summary>
        /// 发布日期
        /// </summary>
        public string ReleaseDate
        {
            get;
            set;
        }
        /// <summary>
        /// 主要内容
        /// </summary>
        public string MainContent
        {
            get;
            set;
        }

        /// <summary>
        /// 附件路径
        /// </summary>
        public string AttachUrl
        {
            get;
            set;
        }
        /// <summary>
        /// 是否阅读
        /// </summary>
        public bool IsRead
        {
            get;
            set;
        }
    }
}
