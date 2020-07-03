using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class TrainingTaskItem
    {
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
        /// 培训计划编号
        /// </summary>
        public string PlanCode
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
        /// 培训计划状态
        /// </summary>
        public string PlanStatesName
        {
            get;
            set;
        }
        /// <summary>
        /// 培训计划时间
        /// </summary>
        public string TrainStartDate
        {
            get;
            set;
        }
        /// <summary>
        /// 培训计划地点
        /// </summary>
        public string TeachAddress
        {
            get;
            set;
        }
        /// <summary>
        /// 培训类型
        /// </summary>
        public string TrainTypeName
        {
            get;
            set;
        }
        /// <summary>
        /// 培训级别
        /// </summary>
        public string TrainLevelName
        {
            get;
            set;
        }
        /// <summary>
        /// 人员ID
        /// </summary>
        public string PersonId
        {
            get;
            set;
        }
        /// <summary>
        /// 人员姓名
        /// </summary>
        public string PersonName
        {
            get;
            set;
        }
        /// <summary>
        /// 任务制定时间
        /// </summary>
        public string TaskDate
        {
            get;
            set;
        }
        /// <summary>
        /// 状态
        /// </summary>
        public string States
        {
            get;
            set;
        }
    }
}
