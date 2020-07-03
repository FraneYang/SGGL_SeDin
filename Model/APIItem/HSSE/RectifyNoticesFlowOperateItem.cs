using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 隐患整改通知单审核记录项
    /// </summary>
    public class RectifyNoticesFlowOperateItem
    {
        /// <summary>
        /// ID
        /// </summary>
        public string FlowOperateId
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
        /// 操作步骤名称
        /// </summary>
        public string OperateName
        {
            get;
            set;
        }
        /// <summary>
        /// 操作人ID
        /// </summary>
        public string OperateManId
        {
            get;
            set;
        }
        /// <summary>
        /// 操作人姓名
        /// </summary>
        public string OperateManName
        {
            get;
            set;
        }
        /// <summary>
        /// 操作时间
        /// </summary>
        public string OperateTime
        {
            get;
            set;
        }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime? OperateTimeD
        {
            get;
            set;
        }
        /// <summary>
        /// 是否同意
        /// </summary>
        public bool? IsAgree
        {
            get;
            set;
        }
        /// <summary>
        /// 意见
        /// </summary>
        public string Opinion
        {
            get;
            set;
        }
        /// <summary>
        /// 签名地址
        /// </summary>
        public string SignatureUrl
        {
            get;
            set;
        }
    }
}
