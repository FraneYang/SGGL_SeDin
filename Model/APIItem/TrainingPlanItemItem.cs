using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class TrainingPlanItemItem
    {
        /// <summary>
        /// 培训计划明细ID
        /// </summary>
        public string PlanItemId
        {
            get;
            set;
        }
        /// <summary>
        /// 培训计划ID
        /// </summary>
        public string PlanId
        {
            get;
            set;
        }
        /// <summary>
        /// 培训教材ID
        /// </summary>
        public string CompanyTrainingId
        {
            get;
            set;
        }
        /// <summary>
        /// 培训教材名称
        /// </summary>
        public string CompanyTrainingName
        {
            get;
            set;
        }
        /// <summary>
        /// 培训教材编号
        /// </summary>
        public string CompanyTrainingCode
        {
            get;
            set;
        }

        /// <summary>
        /// 培训教材明细ID
        /// </summary>
        public string CompanyTrainingItemId
        {
            get;
            set;
        }
        /// <summary>
        /// 培训教材明细名称
        /// </summary>
        public string CompanyTrainingItemName
        {
            get;
            set;
        }
        /// <summary>
        /// 培训教材明细编号
        /// </summary>
        public string CompanyTrainingItemCode
        {
            get;
            set;
        }
    }
}
