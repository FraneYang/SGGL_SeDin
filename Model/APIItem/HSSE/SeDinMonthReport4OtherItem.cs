using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 赛鼎月报信息项 --4、赛鼎公司人员信息统计表
    /// </summary>
    public class SeDinMonthReport4OtherItem
    { 
        /// <summary>
        /// ID
        /// </summary>
        public string MonthReport4OtherId
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
        /// 项目现场正式员工总数
        /// </summary>
        public int? FormalNum
        {
            get;
            set;
        }
        /// <summary>
        /// 项目现场外籍人员总数
        /// </summary>
        public int? ForeignNum
        {
            get;
            set;
        }
        /// <summary>
        /// 项目现场外聘人员总数
        /// </summary>
        public int? OutsideNum
        {
            get;
            set;
        }
        /// <summary>
        /// 项目现场HSE管理人员总数
        /// </summary>
        public int? ManagerNum
        {
            get;
            set;
        }
        /// <summary>
        /// 项目现场员工总数（含外聘）
        /// </summary>
        public int? TotalNum
        {
            get;
            set;
        }
    }
}
