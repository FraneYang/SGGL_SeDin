using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 考试题目题型及数量项
    /// </summary>
    public class TestPlanTrainingItem
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public string TestPlanTrainingId
        {
            get;
            set;
        }
        /// <summary>
        /// 考试计划ID
        /// </summary>
        public string TestPlanId
        {
            get;
            set;
        }
        /// <summary>
        /// 考题类型ID
        /// </summary>
        public string TrainingTypeId
        {
            get;
            set;
        }

        /// <summary>
        /// 考题类型名称
        /// </summary>
        public string TrainingTypeName
        {
            get;
            set;
        }
        /// <summary>
        /// 考生类型ID
        /// </summary>
        public string UserTypeId
        {
            get;
            set;
        }

        /// <summary>
        /// 考生类型名称
        /// </summary>
        public string UserTypeName
        {
            get;
            set;
        }
        /// <summary>
        /// 单选题数量
        /// </summary>
        public int TestType1Count
        {
            get;
            set;
        }/// <summary>
         /// 多选题数量
         /// </summary>
        public int TestType2Count
        {
            get;
            set;
        }
        /// <summary>
        /// 判断题数量
        /// </summary>
        public int TestType3Count
        {
            get;
            set;
        }
    }
}
