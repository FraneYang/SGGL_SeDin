using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class SysTestRuleItem
    {
        /// <summary>
        /// ID
        /// </summary>
        public string TestRuleId
        {
            get;
            set;
        }
        /// <summary>
        /// 时长
        /// </summary>
        public int Duration
        {
            get;
            set;
        }

        /// <summary>
        /// 单选题分值
        /// </summary>
        public string SValue
        {
            get;
            set;
        }
        /// <summary>
        /// 多选题分值
        /// </summary>
        public string MValue
        {
            get;
            set;
        }
        /// <summary>
        /// 判断题分值
        /// </summary>
        public string JValue
        {
            get;
            set;
        }

        /// <summary>
        /// 单选题数量
        /// </summary>
        public string SCount
        {
            get;
            set;
        }
        /// <summary>
        /// 多选题数量
        /// </summary>
        public string MCount
        {
            get;
            set;
        }
        /// <summary>
        /// 判断题数量
        /// </summary>
        public string JCount
        {
            get;
            set;
        }
    }
}
