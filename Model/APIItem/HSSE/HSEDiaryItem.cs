using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// HSE日志信息项
    /// </summary>
    public class HSEDiaryItem
    {
        /// <summary>
        /// ID
        /// </summary>
        public string HSEDiaryId
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
        /// 日期
        /// </summary>
        public string DiaryDate
        {
            get;
            set;
        }
        /// <summary>
        /// 用户ID
        /// </summary>
        public string UserId
        {
            get;
            set;
        }
        /// <summary>
        /// 用户姓名
        /// </summary>
        public string UserName
        {
            get;
            set;
        }
        /// <summary>
        /// 1HSE检查情况及检查次数
        /// </summary>
        public string Value1
        {
            get;
            set;
        }
        /// <summary>
        /// 2隐患整改情况及隐患整改数量
        /// </summary>
        public string Value2
        {
            get;
            set;
        }
        /// <summary>
        /// 3作业许可情况及作业票数量
        /// </summary>
        public string Value3
        {
            get;
            set;
        }
        /// <summary>
        /// 4施工机具、安全设施检查、验收情况及检查验收数量
        /// </summary>
        public string Value4
        {
            get;
            set;
        }
        /// <summary>
        /// 5危险源辨识工作情况及次数
        /// </summary>
        public string Value5
        {
            get;
            set;
        }
        /// <summary>
        /// 6应急计划修编、演练及物资准备情况及次数
        /// </summary>
        public string Value6
        {
            get;
            set;
        }
        /// <summary>
        /// 7教育培训情况及人次
        /// </summary>
        public string Value7
        {
            get;
            set;
        }
        /// <summary>
        /// 8 HSE会议情况及次数
        /// </summary>
        public string Value8
        {
            get;
            set;
        }
        /// <summary>
        /// 9 HSE宣传工作情况
        /// </summary>
        public string Value9
        {
            get;
            set;
        }
        /// <summary>
        /// 10 HSE奖惩工作情况、HSE奖励次数、HSE处罚次数
        /// </summary>
        public string Value10
        {
            get;
            set;
        }

        /// <summary>
        /// 当日小结
        /// </summary>
        public string DailySummary
        {
            get;
            set;
        }

        /// <summary>
        /// 明日计划
        /// </summary>
        public string TomorrowPlan
        {
            get;
            set;
        }
    }
}
