using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 考试试题资源
    /// </summary>
    public class TestTrainingResourcesItem
    {
        /// <summary>
        /// 试题ID
        /// </summary>
        public string TrainingItemId
        {
            get;
            set;
        }
        /// <summary>
        /// 试题类型ID
        /// </summary>
        public string TrainingId
        {
            get;
            set;
        }
        /// <summary>
        /// 编号
        /// </summary>
        public string TrainingItemCode
        {
            get;
            set;
        }
        /// <summary>
        /// 内容
        /// </summary>
        public string Abstracts
        {
            get;
            set;
        }
        /// <summary>
        /// 附件
        /// </summary>
        public string AttachUrl
        {
            get;
            set;
        }
        /// <summary>
        /// 题型（1-单选题；2-多选题；3-判断题）
        /// </summary>
        public string TestType
        {
            get;
            set;
        }
        /// <summary>
        /// 题型名称
        /// </summary>
        public string TestTypeName
        {
            get;
            set;
        }
        /// <summary>
        /// 适合岗位
        /// </summary>
        public string WorkPostIds
        {
            get;
            set;
        }
        /// <summary>
        /// 岗位名称
        /// </summary>
        public string WorkPostNames
        {
            get;
            set;
        }
        /// <summary>
        /// 答案项A
        /// </summary>
        public string AItem
        {
            get;
            set;
        }
        /// <summary>
        /// 答案项B
        /// </summary>
        public string BItem
        {
            get;
            set;
        }
        /// <summary>
        /// 答案项C
        /// </summary>
        public string CItem
        {
            get;
            set;
        }
        /// <summary>
        /// 答案项D
        /// </summary>
        public string DItem
        {
            get;
            set;
        }
        /// <summary>
        /// 答案项E
        /// </summary>
        public string EItem
        {
            get;
            set;
        }
        /// <summary>
        /// 分值
        /// </summary>
        public int Score
        {
            get;
            set;
        }
        /// <summary>
        /// 正确答案
        /// </summary>
        public string AnswerItems
        {
            get;
            set;
        }
    }
}
