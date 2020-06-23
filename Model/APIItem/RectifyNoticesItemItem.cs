using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 隐患整改通知单整改项
    /// </summary>
    public class RectifyNoticesItemItem
    {
        /// <summary>
        /// ID
        /// </summary>
        public string RectifyNoticesItemId
        {
            get;
            set;
        }
        /// <summary>
        /// 整改单ID
        /// </summary>
        public string RectifyNoticesId
        {
            get;
            set;
        }
        /// <summary>
        /// 具体位置及隐患内容
        /// </summary>
        public string WrongContent
        {
            get;
            set;
        }
        /// <summary>
        /// 整改要求
        /// </summary>
        public string Requirement
        {
            get;
            set;
        }
        /// <summary>
        /// 整改前照片
        /// </summary>
        public string PhotoBeforeUrl
        {
            get;
            set;
        }
        /// <summary>
        /// 整改期限
        /// </summary>
        public string LimitTime
        {
            get;
            set;
        }
        /// <summary>
        /// 整改结果
        /// </summary>
        public string RectifyResults
        {
            get;
            set;
        }
        /// <summary>
        /// 整改后照片
        /// </summary>
        public string PhotoAfterUrl
        {
            get;
            set;
        }
        /// <summary>
        /// 整改是否合格
        /// </summary>
        public bool? IsRectify
        {
            get;
            set;
        }
    }
}
