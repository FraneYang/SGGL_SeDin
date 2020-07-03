using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class TrainingPlanItem
    {
        /// <summary>
        /// 培训计划ID
        /// </summary>
        public string PlanId
        {
            get;
            set;
        }
        /// <summary>
        /// 编号
        /// </summary>
        public string PlanCode
        {
            get;
            set;
        }
        /// <summary>
        /// 计划名称
        /// </summary>
        public string PlanName
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
        /// 计划人
        /// </summary>
        public string DesignerId
        {
            get;
            set;
        }
        /// <summary>
        /// 计划人姓名
        /// </summary>
        public string DesignerName
        {
            get;
            set;
        }
        /// <summary>
        /// 制定时间
        /// </summary>
        public string DesignerDate
        {
            get;
            set;
        }
        /// <summary>
        /// 培训内容
        /// </summary>
        public string TrainContent
        {
            get;
            set;
        }
        /// <summary>
        /// 培训类型ID
        /// </summary>
        public string TrainTypeId
        {
            get;
            set;
        }
        /// <summary>
        /// 培训类型名称
        /// </summary>
        public string TrainTypeName
        {
            get;
            set;
        }
        /// <summary>
        /// 培训级别ID
        /// </summary>
        public string TrainLevelId
        {
            get;
            set;
        }
        /// <summary>
        /// 培训级别名称
        /// </summary>
        public string TrainLevelName
        {
            get;
            set;
        }

        /// <summary>
        /// 学时
        /// </summary>
        public decimal TeachHour
        {
            get;
            set;
        }
        /// <summary>
        /// 培训地点
        /// </summary>
        public string TeachAddress
        {
            get;
            set;
        }
        /// <summary>
        /// 培训时间
        /// </summary>
        public string TrainStartDate
        {
            get;
            set;
        }
        /// <summary>
        /// 培训时间
        /// </summary>
        public string TrainEndDate
        {
            get;
            set;
        }
        /// <summary>
        /// 授课人
        /// </summary>
        public string TeachMan
        {
            get;
            set;
        }
        /// <summary>
        /// 培训单位ID
        /// </summary>
        public string UnitIds
        {
            get;
            set;
        }
        /// <summary>
        /// 培训单位名称
        /// </summary>
        public string UnitNames
        {
            get;
            set;
        }
        /// <summary>
        /// 培训岗位ID
        /// </summary>
        public string WorkPostId
        {
            get;
            set;
        }
        /// <summary>
        /// 培训岗位名称
        /// </summary>
        public string WorkPostNames
        {
            get;
            set;
        }
        /// <summary>
        /// 二维码路径
        /// </summary>
        public string QRCodeUrl
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

        /// <summary>
        /// 培训计划教材类型
        /// </summary>
        public List<TrainingPlanItemItem> TrainingPlanItems
        {
            get;
            set;
        }
        /// <summary>
        /// 培训计划人员任务
        /// </summary>
        public List<TrainingTaskItem> TrainingTasks
        {
            get;
            set;
        }
    }
}
