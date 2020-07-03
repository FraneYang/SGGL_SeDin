using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 考试计划项
    /// </summary>
    public class TestPlanItem
    {
        /// <summary>
        /// 考试计划ID
        /// </summary>
        public string TestPlanId
        {
            get;
            set;
        }
        /// <summary>
        /// 培训计划ID
        /// </summary>
        public string TrainingPlanId
        {
            get;
            set;
        }
        /// <summary>
        /// 考试计划编号
        /// </summary>
        public string TestPlanCode
        {
            get;
            set;
        }
        /// <summary>
        /// 考试计划名称
        /// </summary>
        public string TestPlanName
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
        public string TestPlanManId
        {
            get;
            set;
        }
        /// <summary>
        /// 计划人姓名
        /// </summary>
        public string TestPlanManName
        {
            get;
            set;
        }
        /// <summary>
        /// 制定时间
        /// </summary>
        public string TestPlanDate
        {
            get;
            set;
        }
        /// <summary>
        /// 扫码开始时间
        /// </summary>
        public string TestStartTime
        {
            get;
            set;
        }
        /// <summary>
        /// 扫码结束时间
        /// </summary>
        public string TestEndTime
        {
            get;
            set;
        }
        /// <summary>
        /// 考试时长
        /// </summary>
        public int Duration
        {
            get;
            set;
        }
        /// <summary>
        /// 单选题分值
        /// </summary>
        public int SValue
        {
            get;
            set;
        }
        /// <summary>
        /// 多选题分值
        /// </summary>
        public int MValue
        {
            get;
            set;
        }
        /// <summary>
        /// 判断题分值
        /// </summary>
        public int JValue
        {
            get;
            set;
        }
        /// <summary>
        /// 总分数
        /// </summary>
        public int TotalScore
        {
            get;
            set;
        }
        /// <summary>
        /// 考题数量
        /// </summary>
        public int QuestionCount
        {
            get;
            set;
        }
        /// <summary>
        /// 考试地点
        /// </summary>
        public string TestPalce
        {
            get;
            set;
        }

        /// <summary>
        /// 考试单位ID
        /// </summary>
        public string UnitIds
        {
            get;
            set;
        }
        /// <summary>
        /// 考试单位名称
        /// </summary>
        public string UnitNames
        {
            get;
            set;
        }
        /// <summary>
        /// 考试岗位ID
        /// </summary>
        public string WorkPostIds
        {
            get;
            set;
        }
        /// <summary>
        /// 考试岗位名称
        /// </summary>
        public string WorkPostNames
        {
            get;
            set;
        }
        /// <summary>
        /// 考试二维码
        /// </summary>
        public string QRCodeUrl
        {
            get;
            set;
        }  
        ///// <summary>
        ///// 考试类型
        ///// </summary>
        //public string TestType
        //{
        //    get;
        //    set;
        //}     
        /// <summary>
        /// 状态
        /// </summary>
        public string States
        {
            get;
            set;
        }
        /// <summary>
        /// 实际结束时间
        /// </summary>
        public string ActualTime
        {
            get;
            set;
        }
        /// <summary>
        /// 考卷设置项
        /// </summary>
        public List<TestPlanTrainingItem> TestPlanTrainingItems
        {
            get;
            set;
        }
        /// <summary>
        /// 考生记录
        /// </summary>
        public List<TestRecordItem> TestRecordItems
        {
            get;
            set;
        }
    }
}
