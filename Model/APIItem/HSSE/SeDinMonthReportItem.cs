using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 赛鼎月报信息项
    /// </summary>
    public class SeDinMonthReportItem
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
        public string ReporMonth
        {
            get;
            set;
        }
        /// <summary>
        /// 截止日期
        /// </summary>
        public string DueDate
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
        /// 编制人ID
        /// </summary>
        public string CompileManId
        {
            get;
            set;
        }
        /// <summary>
        /// 编制人姓名
        /// </summary>
        public string CompileManName
        {
            get;
            set;
        }
        /// <summary>
        /// 审核人ID
        /// </summary>
        public string AuditManId
        {
            get;
            set;
        }
        /// <summary>
        /// 审核人姓名
        /// </summary>
        public string AuditManName
        {
            get;
            set;
        }
        /// <summary>
        /// 批准人ID
        /// </summary>
        public string ApprovalManId
        {
            get;
            set;
        }
        /// <summary>
        /// 批准人姓名
        /// </summary>
        public string ApprovalManName
        {
            get;
            set;
        }
        
        /// <summary>
        /// 本月HSE活动综述
        /// </summary>
        public string ThisSummary
        {
            get;
            set;
        }
        /// <summary>
        /// 下月HSE工作计划
        /// </summary>
        public string NextPlan
        {
            get;
            set;
        }
        /// <summary>
        ///  状态（0-待提交；1-已提交）
        /// </summary>
        public string States
        {
            get;
            set;
        }

        /// <summary>
        ///  事故综述
        /// </summary>
        public string AccidentsSummary
        {
            get;
            set;
        }
        /// <summary>
        ///  3、项目HSE事故、事件统计
        /// </summary>
        public List<Model.SeDinMonthReport3Item> SeDinMonthReport3Item
        {
            get;
            set;
        }
        /// <summary>
        ///  4、本月人员投入情况
        /// </summary>
        public List<Model.SeDinMonthReport4Item> SeDinMonthReport4Item
        {
            get;
            set;
        }
        /// <summary>
        ///  5、本月大型、特种设备投入情况
        /// </summary>
        public List<Model.SeDinMonthReport5Item> SeDinMonthReport5Item
        {
            get;
            set;
        }
    }
}
