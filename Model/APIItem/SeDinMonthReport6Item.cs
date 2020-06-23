using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 赛鼎月报信息项 --6、安全生产费用投入情况
    /// </summary>
    public class SeDinMonthReport6Item
    { 
        /// <summary>
        /// ID
        /// </summary>
        public string MonthReport6Id
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
        /// 安全防护投入-月
        /// </summary>
        public decimal? SafetyMonth
        {
            get;
            set;
        }
        /// <summary>
        /// 安全防护投入-年
        /// </summary>
        public decimal? SafetyYear
        {
            get;
            set;
        }
        /// <summary>
        /// 安全防护投入-总
        /// </summary>
        public decimal? SafetyTotal
        {
            get;
            set;
        }
        /// <summary>
        /// 劳动保护及职业健康投入-月
        /// </summary>
        public decimal? LaborMonth
        {
            get;
            set;
        }
        /// <summary>
        /// 劳动保护及职业健康投入-年
        /// </summary>
        public decimal? LaborYear
        {
            get;
            set;
        }
        /// <summary>
        /// 劳动保护及职业健康投入-总
        /// </summary>
        public decimal? LaborTotal
        {
            get;
            set;
        }
        /// <summary>
        /// 安全技术进步投入-月
        /// </summary>
        public decimal? ProgressMonth
        {
            get;
            set;
        }
        /// <summary>
        /// 安全技术进步投入-年
        /// </summary>
        public decimal? ProgressYear
        {
            get;
            set;
        }
        /// <summary>
        /// 安全技术进步投入-总
        /// </summary>
        public decimal? ProgressTotal
        {
            get;
            set;
        }
        /// <summary>
        /// 安全教育培训投入-月
        /// </summary>
        public decimal? EducationMonth
        {
            get;
            set;
        }
        /// <summary>
        /// 安全教育培训投入-年
        /// </summary>
        public decimal? EducationYear
        {
            get;
            set;
        }
        /// <summary>
        /// 安全教育培训投入-总
        /// </summary>
        public decimal? EducationTotal
        {
            get;
            set;
        }
        /// <summary>
        /// 合计-月
        /// </summary>
        public decimal? SumMonth
        {
            get;
            set;
        }
        /// <summary>
        /// 合计-年
        /// </summary>
        public decimal? SumYear
        {
            get;
            set;
        }
        /// <summary>
        /// 合计-总
        /// </summary>
        public decimal? SumTotal
        {
            get;
            set;
        }
        /// <summary>
        /// 完成合同额-月
        /// </summary>
        public decimal? ContractMonth
        {
            get;
            set;
        }
        /// <summary>
        /// 完成合同额-年度
        /// </summary>
        public decimal? ContractYear
        {
            get;
            set;
        }
        /// <summary>
        /// 完成合同额-累计
        /// </summary>
        public decimal? ContractTotal
        {
            get;
            set;
        }
        /// <summary>
        /// 工程造价占比
        /// </summary>
        public decimal? ConstructionCost
        {
            get;
            set;
        }
    }
}
