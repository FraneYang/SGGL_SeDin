using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 赛鼎月报信息项 --8、项目HSE会议统计
    /// </summary>
    public class SeDinMonthReport8Item
    { 
        /// <summary>
        /// ID
        /// </summary>
        public string MonthReport8Id
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
        /// 周例会-月次
        /// </summary>
        public int? WeekMontNum
        {
            get;
            set;
        }
        /// <summary>
        /// 周例会-项目累计次
        /// </summary>
        public int? WeekTotalNum
        {
            get;
            set;
        }
        /// <summary>
        /// 周例会-月人
        /// </summary>
        public int? WeekMontPerson
        {
            get;
            set;
        }
        /// <summary>
        /// 月例会-月次
        /// </summary>
        public int? MonthMontNum
        {
            get;
            set;
        }
        /// <summary>
        /// 月例会-项目累计次
        /// </summary>
        public int? MonthTotalNum
        {
            get;
            set;
        }
        /// <summary>
        /// 月例会-月人
        /// </summary>
        public int? MonthMontPerson
        {
            get;
            set;
        }
        /// <summary>
        /// 专题例会-月次
        /// </summary>
        public int? SpecialMontNum
        {
            get;
            set;
        }
        /// <summary>
        /// 专题例会-项目累计次
        /// </summary>
        public int? SpecialTotalNum
        {
            get;
            set;
        }
        /// <summary>
        /// 专题例会-月人
        /// </summary>
        public int? SpecialMontPerson
        {
            get;
            set;
        }
        /// <summary>
        /// 班组会议-明细表
        /// </summary>
        public List<Model.SeDinMonthReport8ItemItem> SeDinMonthReport8ItemItem
        {
            get;
            set;
        }
    }
}
