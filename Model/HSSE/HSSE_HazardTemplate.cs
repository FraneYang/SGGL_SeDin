using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class HSSE_HazardTemplate
    {
        public string HazardId
        {
            get;
            set;
        }

        public string HazardListTypeId
        {
            get;
            set;
        }

        /// <summary>
        /// 危险因素
        /// </summary>
        public string HazardItems
        {
            get;
            set;
        }

        /// <summary>
        /// 导致原因
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

        public string Remark
        {
            get;
            set;
        }

        public string State
        {
            get;
            set;
        }

        public string WorkStage
        {
            get;
            set;
        }
    }
}