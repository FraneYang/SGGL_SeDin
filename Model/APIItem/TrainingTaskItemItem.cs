using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class TrainingTaskItemItem
    {
        /// <summary>
        /// 培训任务教材明细ID
        /// </summary>
        public string TaskItemId
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
        /// 培训任务ID
        /// </summary>
        public string TaskId
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
        /// 培训计划名称
        /// </summary>
        public string PlanName
        {
            get;
            set;
        }
        /// <summary>
        /// 培训计划编号
        /// </summary>
        public string PlanCode
        {
            get;
            set;
        }
        /// <summary>
        /// 培训人员ID
        /// </summary>
        public string PersonId
        {
            get;
            set;
        }
        /// <summary>
        /// 教材编号
        /// </summary>
        public string TrainingItemCode
        {
            get;
            set;
        }
        /// <summary>
        /// 教材名称
        /// </summary>
        public string TrainingItemName
        {
            get;
            set;
        }
        /// <summary>
        /// 教材附件
        /// </summary>
        public string AttachUrl
        {
            get;
            set;
        }
    }
}
