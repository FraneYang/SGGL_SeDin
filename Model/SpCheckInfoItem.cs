using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class SpCheckInfoItem
    {
        /// <summary>
        /// 对象类型
        /// </summary>
        public string ID
        {
            get;
            set;
        }

        /// <summary>
        /// 明细ID
        /// </summary>
        public string CheckItemId
        {
            get;
            set;
        }

        /// <summary>
        /// 排列序号
        /// </summary>
        public int? SortIndex
        {
            get;
            set;
        }

        /// <summary>
        /// 检查对象名称
        /// </summary>
        public string CheckItemName
        {
            get;
            set;
        }

        /// <summary>
        /// 检查对象
        /// </summary>
        public string CheckObject
        {
            get;
            set;
        }

        /// <summary>
        /// 检查时间
        /// </summary>
        public DateTime? CheckDate
        {
            get;
            set;
        }

        /// <summary>
        /// 检查结果
        /// </summary>
        public string CheckResult
        {
            get;
            set;
        }
    }
}
