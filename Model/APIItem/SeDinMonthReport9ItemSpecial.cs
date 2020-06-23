using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 赛鼎月报信息项 --9、项目HSE检查统计-专项检查
    /// </summary>
    public class SeDinMonthReport9ItemSpecial
    { 
        /// <summary>
        /// ID
        /// </summary>
        public string MonthReport9ItemId
        {
            get;
            set;
        }
        /// <summary>
        /// 月报ID
        /// </summary>
        public string MonthReportId
        {
            get;
            set;
        }
        /// <summary>
        /// 类型
        /// </summary>
        public string TypeName
        {
            get;
            set;
        }
        /// <summary>
        /// 检查次数（本月）
        /// </summary>
        public int? CheckMonth
        {
            get;
            set;
        }
        /// <summary>
        /// 次数（本年度累计）
        /// </summary>
        public int? CheckYear
        {
            get;
            set;
        }
        /// <summary>
        /// 次数（项目总累计）
        /// </summary>
        public int? CheckTotal
        {
            get;
            set;
        }
    }
}
