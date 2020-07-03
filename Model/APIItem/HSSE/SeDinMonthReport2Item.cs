using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 赛鼎月报信息项 --2、项目安全工时统计
    /// </summary>
    public class SeDinMonthReport2Item
    { 
        /// <summary>
        /// ID
        /// </summary>
        public string MonthReport2Id
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
        /// 当月安全人工时
        /// </summary>
        public decimal?  MonthWorkTime
        {
            get;
            set;
        }
        /// <summary>
        /// 年度累计安全人工时
        /// </summary>
        public decimal? YearWorkTime
        {
            get;
            set;
        }
        /// <summary>
        /// 项目累计安全人工时
        /// </summary>
        public decimal? ProjectWorkTime
        {
            get;
            set;
        }
        /// <summary>
        /// 总损失工时
        /// </summary>
        public decimal? TotalLostTime
        {
            get;
            set;
        }
        /// <summary>
        /// 百万工时损失率
        /// </summary>
        public string MillionLossRate
        {
            get;
            set;
        }
        /// <summary>
        ///  工时统计准确率
        /// </summary>
        public string TimeAccuracyRate
        {
            get;
            set;
        }
        /// <summary>
        /// 开始日期
        /// </summary>
        public string StartDate
        {
            get;
            set;
        }
        /// <summary>
        /// 结束日期
        /// </summary>
        public string EndDate
        {
            get;
            set;
        }
        /// <summary>
        /// 安全生产 人工时
        /// </summary>
        public decimal? SafeWorkTime
        {
            get;
            set;
        }
    }
}
