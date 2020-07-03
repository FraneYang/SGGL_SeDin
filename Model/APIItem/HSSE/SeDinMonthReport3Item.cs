using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 赛鼎月报信息项 --3、项目HSE事故、事件统计
    /// </summary>
    public class SeDinMonthReport3Item
    { 
        /// <summary>
        /// ID
        /// </summary>
        public string MonthReport3Id
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
        /// 排序
        /// </summary>
        public int? SortIndex
        {
            get;
            set;
        }
        /// <summary>
        /// 类型
        /// </summary>
        public string BigType
        {
            get;
            set;
        }
        /// <summary>
        /// 事故类型
        /// </summary>
        public string AccidentType
        {
            get;
            set;
        }
        /// <summary>
        /// 本月次数
        /// </summary>
        public int? MonthTimes
        {
            get;
            set;
        }
        /// <summary>
        /// 累计次数
        /// </summary>
        public int? TotalTimes
        {
            get;
            set;
        }
        /// <summary>
        /// 本月损失工时
        /// </summary>
        public decimal? MonthLossTime
        {
            get;
            set;
        }
        /// <summary>
        /// 累计损失工时
        /// </summary>
        public decimal? TotalLossTime
        {
            get;
            set;
        }        
        /// <summary>
        /// 本月经济损失
        /// </summary>
        public decimal? MonthMoney
        {
            get;
            set;
        }
        /// <summary>
        /// 累计经济损失
        /// </summary>
        public decimal? TotalMoney
        {
            get;
            set;
        }
        /// <summary>
        /// 本月人数
        /// </summary>
        public int? MonthPersons
        {
            get;
            set;
        }
        /// <summary>
        /// 累计人数
        /// </summary>
        public int? TotalPersons
        {
            get;
            set;
        }
    }
}
