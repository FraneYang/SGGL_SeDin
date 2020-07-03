using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 公共资源类型类
    /// </summary>
    public class ResourcesItem
    {
        /// <summary>
        /// ID
        /// </summary>
        public string ResourcesId
        {
            get;
            set;
        }
        /// <summary>
        /// 编号
        /// </summary>
        public string ResourcesCode
        {
            get;
            set;
        }

        /// <summary>
        /// 名称
        /// </summary>
        public string ResourcesName
        {
            get;
            set;
        }

        /// <summary>
        /// 上级ID
        /// </summary>
        public string SupResourcesId
        {
            get;
            set;
        }
        /// <summary>
        /// 是否末级
        /// </summary>
        public bool? IsEndLever
        {
            get;
            set;
        }
    }
}
