using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 基础信息项
    /// </summary>
    public class BaseInfoItem
    {
        /// <summary>
        /// ID
        /// </summary>
        public string BaseInfoId
        {
            get;
            set;
        }
        /// <summary>
        /// 编号
        /// </summary>
        public string BaseInfoCode
        {
            get;
            set;
        }

        /// <summary>
        /// 名称
        /// </summary>
        public string BaseInfoName
        {
            get;
            set;
        }
    
        /// <summary>
        /// 图片/附件
        /// </summary>
        public string ImageUrl
        {
            get;
            set;
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            get;
            set;
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string RemarkOther
        {
            get;
            set;
        }
    }
}
