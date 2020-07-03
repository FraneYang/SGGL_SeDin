using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 赛鼎月报信息项 --4、本月人员投入情况
    /// </summary>
    public class SeDinMonthReport4Item
    { 
        /// <summary>
        /// ID
        /// </summary>
        public string MonthReport4Id
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
        /// 安全管理人员
        /// </summary>
        public int? SafeManangerNum
        {
            get;
            set;
        }
        /// <summary>
        /// 其他管理人员
        /// </summary>
        public int? OtherManangerNum
        {
            get;
            set;
        }
        /// <summary>
        /// 特种作业人员
        /// </summary>
        public int? SpecialWorkerNum
        {
            get;
            set;
        }
        /// <summary>
        /// 一般作业人员
        /// </summary>
        public int? GeneralWorkerNum
        {
            get;
            set;
        }
        /// <summary>
        /// 合计人数
        /// </summary>
        public int? TotalNum
        {
            get;
            set;
        }
    }
}
