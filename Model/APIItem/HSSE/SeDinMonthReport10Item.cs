using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 赛鼎月报信息项 --10、项目奖惩情况统计
    /// </summary>
    public class SeDinMonthReport10Item
    { 
        /// <summary>
        /// ID
        /// </summary>
        public string MonthReport10Id
        {
            get;
            set;
        }
        /// <summary>
        ///  月报ID
        /// </summary>
        public string MonthReportId
        {
            get;
            set;
        }
        /// <summary>
        /// 安全工时奖次数（本月）
        /// </summary>
        public int? SafeMonthNum
        {
            get;
            set;
        }
        /// <summary>
        /// 安全工时奖次数（累计）
        /// </summary>
        public int? SafeTotalNum
        {
            get;
            set;
        }
        /// <summary>
        /// 安全工时奖金额（本月）
        /// </summary>
        public decimal? SafeMonthMoney
        {
            get;
            set;
        }
        /// <summary>
        /// 安全工时奖金额（累计）
        /// </summary>
        public decimal? SafeTotalMoney
        {
            get;
            set;
        }
        /// <summary>
        /// HSE绩效考核奖励次数（本月）
        /// </summary>
        public int? HseMonthNum
        {
            get;
            set;
        }
        /// <summary>
        /// HSE绩效考核奖励次数（累计）
        /// </summary>
        public int? HseTotalNum
        {
            get;
            set;
        }
        /// <summary>
        /// HSE绩效考核奖励金额（本月）
        /// </summary>
        public decimal? HseMonthMoney
        {
            get;
            set;
        }
        /// <summary>
        /// HSE绩效考核奖励金额（累计）
        /// </summary>
        public decimal? HseTotalMoney
        {
            get;
            set;
        }
        /// <summary>
        /// 安全生产先进个人奖次数（本月）
        /// </summary>
        public int? ProduceMonthNum
        {
            get;
            set;
        }
        /// <summary>
        /// 安全生产先进个人奖次数（累计）
        /// </summary>
        public int? ProduceTotalNum
        {
            get;
            set;
        }
        /// <summary>
        /// 安全生产先进个人奖金额（本月）
        /// </summary>
        public decimal? ProduceMonthMoney
        {
            get;
            set;
        }
        /// <summary>
        /// 安全生产先进个人奖金额（累计）
        /// </summary>
        public decimal? ProduceTotalMoney
        {
            get;
            set;
        }
        /// <summary>
        /// 事故责任处罚次数（本月）
        /// </summary>
        public int? AccidentMonthNum
        {
            get;
            set;
        }
        /// <summary>
        /// 事故责任处罚次数（累计）
        /// </summary>
        public int? AccidentTotalNum
        {
            get;
            set;
        }
        /// <summary>
        /// 事故责任处罚金额（本月）
        /// </summary>
        public decimal? AccidentMonthMoney
        {
            get;
            set;
        }
        /// <summary>
        /// 事故责任处罚金额（累计）
        /// </summary>
        public decimal? AccidentTotalMoney
        {
            get;
            set;
        }
        /// <summary>
        /// 违章处罚次数（本月）
        /// </summary>
        public int? ViolationMonthNum
        {
            get;
            set;
        }
        /// <summary>
        /// 违章处罚次数（累计）
        /// </summary>
        public int? ViolationTotalNum
        {
            get;
            set;
        }
        /// <summary>
        /// 违章处罚金额（本月）
        /// </summary>
        public decimal? ViolationMonthMoney
        {
            get;
            set;
        }
        /// <summary>
        /// 违章处罚金额（累计）
        /// </summary>
        public decimal? ViolationTotalMoney
        {
            get;
            set;
        }
        /// <summary>
        /// 安全管理处罚次数（本月）
        /// </summary>
        public int? ManageMonthNum
        {
            get;
            set;
        }
        /// <summary>
        /// 安全管理处罚次数（累计）
        /// </summary>
        public int? ManageTotalNum
        {
            get;
            set;
        }
        /// <summary>
        /// 安全管理处罚金额（本月）
        /// </summary>
        public decimal? ManageMonthMoney
        {
            get;
            set;
        }
        /// <summary>
        /// 安全管理处罚金额（累计）
        /// </summary>
        public decimal? ManageTotalMoney
        {
            get;
            set;
        }
    }
}
