using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 危险源辨识评价明细项
    /// </summary>
    public class HazardListSelectedItem
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string HazardId
        {
            get;
            set;
        }

        /// <summary>
        /// 工作阶段
        /// </summary>
        public string WorkStageName
        {
            get;
            set;
        }
        /// <summary>
        /// 危险类别
        /// </summary>
        public string SupHazardListTypeName
        {
            get;
            set;
        }
        /// <summary>
        /// 危险源项
        /// </summary>
        public string HazardListTypeName
        {
            get;
            set;
        }
        /// <summary>
        /// 危险源代码
        /// </summary>
        public string HazardCode
        {
            get;
            set;
        }
        /// <summary>
        /// 危险因素明细
        /// </summary>
        public string HazardItems
        {
            get;
            set;
        }

        /// <summary>
        /// 缺陷类型
        /// </summary>
        public string DefectsType
        {
            get;
            set;
        }

        /// <summary>
        /// 可能导致的事故
        /// </summary>
        public string MayLeadAccidents
        {
            get;
            set;
        }

        /// <summary>
        /// 辅助方法
        /// </summary>
        public string HelperMethod
        {
            get;
            set;
        }

        /// <summary>
        /// 作业条件危险性评价(L)
        /// </summary>
        public decimal? HazardJudge_L
        {
            get;
            set;
        }

        /// <summary>
        /// 作业条件危险性评价(E)
        /// </summary>
        public decimal? HazardJudge_E
        {
            get;
            set;
        }

        /// <summary>
        /// 作业条件危险性评价(C)
        /// </summary>
        public decimal? HazardJudge_C
        {
            get;
            set;
        }

        /// <summary>
        /// 作业条件危险性评价(D)
        /// </summary>
        public decimal? HazardJudge_D
        {
            get;
            set;
        }

        /// <summary>
        /// 危险等级
        /// </summary>
        public string HazardLevel
        {
            get;
            set;
        }

        /// <summary>
        /// 控制措施
        /// </summary>
        public string ControlMeasures
        {
            get;
            set;
        }
    }
}
