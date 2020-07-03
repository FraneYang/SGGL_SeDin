using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 赛鼎月报信息项 --9、项目HSE检查统计-隐患整改单
    /// </summary>
    public class SeDinMonthReport9ItemRectification
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
        ///  单位名称
        /// </summary>
        public string UnitName
        {
            get;
            set;
        }
        /// <summary>
        /// 下发数量（本月）
        /// </summary>
        public int? IssuedMonth
        {
            get;
            set;
        }
        /// <summary>
        /// 整改完成数量（本月）
        /// </summary>
        public int? RectificationMoth
        {
            get;
            set;
        }
        /// <summary>
        /// 下发数量（累计）
        /// </summary>
        public int? IssuedTotal
        {
            get;
            set;
        }
        /// <summary>
        /// 整改完成数量（累计）
        /// </summary>
        public int? RectificationTotal
        {
            get;
            set;
        }
    }
}
