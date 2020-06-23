using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 赛鼎月报信息项 --11、项目危大工程施工情况
    /// </summary>
    public class SeDinMonthReport11Item
    { 
        /// <summary>
        /// ID
        /// </summary>
        public string MonthReport11Id
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
        /// 危险性较大分部分项工程本月正在施工
        /// </summary>
        public int? RiskWorkNum
        {
            get;
            set;
        }
        /// <summary>
        /// 危险性较大分部分项工程已完工
        /// </summary>
        public int? RiskFinishedNum
        {
            get;
            set;
        }
        /// <summary>
        /// 危险性较大分部分项工程下月施工计划
        /// </summary>
        public string RiskWorkNext
        {
            get;
            set;
        }
        /// <summary>
        /// 超过一定规模危大工程本月正在施工
        /// </summary>
        public int? LargeWorkNum
        {
            get;
            set;
        }
        /// <summary>
        /// 超过一定规模危大工程已完工
        /// </summary>
        public int? LargeFinishedNum
        {
            get;
            set;
        }
        /// <summary>
        /// 超过一定规模危大工程下月施工计划
        /// </summary>
        public string LargeWorkNext
        {
            get;
            set;
        }
    }
}
