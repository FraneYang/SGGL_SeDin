using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 赛鼎月报信息项 --1、项目信息
    /// </summary>
    public class SeDinMonthReport1Item
    { 
        /// <summary>
        /// ID
        /// </summary>
        public string MonthReport1Id
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
        /// 项目编号
        /// </summary>
        public string ProjectCode
        {
            get;
            set;
        }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName
        {
            get;
            set;
        }
        /// <summary>
        /// 项目类型
        /// </summary>
        public string ProjectType
        {
            get;
            set;
        }
        /// <summary>
        ///  合同开始日期
        /// </summary>
        public string StartDate
        {
            get;
            set;
        }
        /// <summary>
        /// 合同结束日期
        /// </summary>
        public string EndDate
        {
            get;
            set;
        }
        /// <summary>
        ///  项目经理及联系方式
        /// </summary>
        public string ProjectManager
        {
            get;
            set;
        }
        /// <summary>
        /// 安全经理及联系方式
        /// </summary>
        public string HsseManager
        {
            get;
            set;
        }
        /// <summary>
        /// 合同号
        /// </summary>
        public string ContractNo
        {
            get;
            set;
        }
        /// <summary>
        /// 合同额
        /// </summary>
        public string ContractAmount
        {
            get;
            set;
        }
        /// <summary>
        /// 所处的施工阶段
        /// </summary>
        public string ConstructionStage
        {
            get;
            set;
        }
        /// <summary>
        /// 项目所在地
        /// </summary>
        public string ProjectAddress
        {
            get;
            set;
        }
    }
}
