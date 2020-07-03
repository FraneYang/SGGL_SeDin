using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 赛鼎月报信息项 --8、项目HSE会议统计(班前会)
    /// </summary>
    public class SeDinMonthReport8ItemItem
    { 
        /// <summary>
        /// ID
        /// </summary>
        public string MonthReport8ItemId
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
        /// 单位名称
        /// </summary>
        public string UnitName
        {
            get;
            set;
        }
        /// <summary>
        /// 班组名称
        /// </summary>
        public string TeamName
        {
            get;
            set;
        }
        /// <summary>
        /// 班前会-月次
        /// </summary>
        public int? ClassNum
        {
            get;
            set;
        }
        /// <summary>
        /// 班前会-人次
        /// </summary>
        public int? ClassPersonNum
        {
            get;
            set;
        }
    }
}
