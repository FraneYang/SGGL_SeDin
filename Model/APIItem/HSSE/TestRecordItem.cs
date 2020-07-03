using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 考试记录项
    /// </summary>
    public class TestRecordItem
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public string TestRecordId
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
        /// 项目名称
        /// </summary>
        public string ProjectName
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
        /// 考试计划名称
        /// </summary>
        public string TestPlanName
        {
            get;
            set;
        }

        /// <summary>
        /// 考生ID
        /// </summary>
        public string TestManId
        {
            get;
            set;
        }
        /// <summary>
        /// 考生姓名
        /// </summary>
        public string TestManName
        {
            get;
            set;
        }
        /// <summary>
        /// 单位Id
        /// </summary>
        public string UnitId
        {
            get;
            set;
        }
        /// <summary>
        /// 是否本单位
        /// </summary>
        public bool IsThiUnit
        {
            get;
            set;
        }
        /// <summary>
        /// 单位名称
        /// </summary>
        public string UnitName
        {
            get;
            set;
        }
        /// <summary>
        /// 考试开始时间
        /// </summary>
        public string TestStartTime
        {
            get;
            set;
        }
        
        /// <summary>
        /// 考试结束时间
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
        /// 考试计划结束时间
        /// </summary>
        public string TestPlanStartTime
        {
            get;
            set;
        }
        /// <summary>
        /// 考试计划结束时间
        /// </summary>
        public string TestPlanEndTime
        {
            get;
            set;
        }
        /// <summary>
        /// 总分
        /// </summary>
        public int TotalScore
        {
            get;
            set;
        }
        /// <summary>
        /// 题目数量
        /// </summary>
        public int QuestionCount
        {
            get;
            set;
        }
        /// <summary>
        /// 考试成绩
        /// </summary>
        public decimal TestScores
        {
            get;
            set;
        }
        
        /// <summary>
        /// 是否合格
        /// </summary>
        public bool CheckResult
        {
            get;
            set;
        }
        /// <summary>
        /// 考试类型名称
        /// </summary>
        public string TestType
        {
            get;
            set;
        }
        /// <summary>
        /// 考试信息（临时）
        /// </summary>
        public string TemporaryUser
        {
            get;
            set;
        }

        /// <summary>
        /// 考生类型（知识竞赛）
        /// </summary>
        public string UserType
        {
            get;
            set;
        }
        /// <summary>
        /// 部门ID（知识竞赛）
        /// </summary>
        public string DepartId
        {
            get;
            set;
        }
        /// <summary>
        /// 部门名称（知识竞赛）
        /// </summary>
        public string DepartName
        {
            get;
            set;
        }
        /// <summary>
        /// 岗位ID（知识竞赛）
        /// </summary>
        public string WorkPostId
        {
            get;
            set;
        }
        /// <summary>
        /// 岗位名称（知识竞赛）
        /// </summary>
        public string WorkPostName
        {
            get;
            set;
        }
        /// <summary>
        /// 身份证号码（知识竞赛）
        /// </summary>
        public string IdentityCard
        {
            get;
            set;
        }
        /// <summary>
        /// 电话号码（知识竞赛）
        /// </summary>
        public string Telephone
        {
            get;
            set;
        }

        /// <summary>
        /// 考试状态
        /// </summary>
        public string TestStates
        {
            get;
            set;
        }
    }
}
