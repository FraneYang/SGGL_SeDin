using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 赛鼎月报信息项 --9、项目HSE检查统计
    /// </summary>
    public class SeDinMonthReport9Item
    { 
        /// <summary>
        /// ID
        /// </summary>
        public string MonthReport9Id
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
        /// 日常巡检-月次
        /// </summary>
        public int? DailyMonth
        {
            get;
            set;
        }
        /// <summary>
        /// 日常巡检-年次
        /// </summary>
        public int? DailyYear
        {
            get;
            set;
        }
        /// <summary>
        /// 日常巡检-总累计
        /// </summary>
        public int? DailyTotal
        {
            get;
            set;
        }
        /// <summary>
        /// 周联合检查-月次
        /// </summary>
        public int? WeekMonth
        {
            get;
            set;
        }
        /// <summary>
        /// 周联合检查-年次
        /// </summary>
        public int? WeekYear
        {
            get;
            set;
        }
        /// <summary>
        /// 周联合检查-总累计
        /// </summary>
        public int? WeekTotal
        {
            get;
            set;
        }
        /// <summary>
        /// 专项检查-月次
        /// </summary>
        public int? SpecialMonth
        {
            get;
            set;
        }
        /// <summary>
        /// 专项检查-年次
        /// </summary>
        public int? SpecialYear
        {
            get;
            set;
        }
        /// <summary>
        /// 专项检查-总累计
        /// </summary>
        public int? SpecialTotal
        {
            get;
            set;
        }
        /// <summary>
        /// 月综合HSE检查-月次
        /// </summary>
        public int? MonthlyMonth
        {
            get;
            set;
        }
        /// <summary>
        /// 月综合HSE检查-年次
        /// </summary>
        public int? MonthlyYear
        {
            get;
            set;
        }
        /// <summary>
        /// 月综合HSE检查-总累计
        /// </summary>
        public int? MonthlyTotal
        {
            get;
            set;
        }
        /// <summary>
        /// 隐患整改单
        /// </summary>
        public List<Model.SeDinMonthReport9ItemRectification> SeDinMonthReport9ItemRectification
        {
            get;
            set;
        }
        /// <summary>
        /// 专项检查
        /// </summary>
        public List<Model.SeDinMonthReport9ItemSpecial> SeDinMonthReport9ItemSpecial
        {
            get;
            set;
        }
        /// <summary>
        /// 停工令
        /// </summary>
        public List<Model.SeDinMonthReport9ItemStoppage> SeDinMonthReport9ItemStoppage
        {
            get;
            set;
        }
    }
}
