using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// HSE管理月报信息项
    /// </summary>
    public class HSEMonthItem
    {
        /// <summary>
        /// ID
        /// </summary>
        public string MonthReportId
        {
            get;
            set;
        }
        /// <summary>
        /// 项目ID
        /// </summary>
        public string ProjectId
        {
            get;
            set;
        }
        /// <summary>
        /// 月份
        /// </summary>
        public string ReportMonths
        {
            get;
            set;
        }
        /// <summary>
        /// 报告人ID
        /// </summary>
        public string ReportManId
        {
            get;
            set;
        }
        /// <summary>
        /// 报告人
        /// </summary>
        public string ReportManName
        {
            get;
            set;
        }
        /// <summary>
        /// 编号
        /// </summary>
        public string MonthReportCode
        {
            get;
            set;
        }
        /// <summary>
        /// 开始时间
        /// </summary>
        public string ReportStartDate
        {
            get;
            set;
        }
        /// <summary>
        /// 结束时间
        /// </summary>
        public string ReportEndDate
        {
            get;
            set;
        }
        /// <summary>
        /// 安全人工时
        /// </summary>
        public string AllManhoursData
        {
            get;
            set;
       }
    }
}
