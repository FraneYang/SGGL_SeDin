using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 考试试卷项
    /// </summary>
    public class TestRecordItemItem
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public string TestRecordItemId
        {
            get;
            set;
        }
        /// <summary>
        /// 考生记录ID
        /// </summary>
        public string TestRecordId
        {
            get;
            set;
        }
        /// <summary>
        /// 题目名称(题号)
        /// </summary>
        public string TrainingItemName
        {
            get;
            set;
        }
        /// <summary>
        /// 题目内容
        /// </summary>
        public string Abstracts
        {
            get;
            set;
        }

        /// <summary>
        /// 附件(图片)
        /// </summary>
        public string AttachUrl
        {
            get;
            set;
        }
        /// <summary>
        /// 题型（1-单选；2-多选；3-判断题）
        /// </summary>
        public string TestType
        {
            get;
            set;
        }
        /// <summary>
        /// 题型（1-单选；2-多选；3-判断题）
        /// </summary>
        public string TestTypeName
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
        /// 正确答案项
        /// </summary>
        public string AnswerItems
        {
            get;
            set;
        }
        /// <summary>
        /// 题目分值
        /// </summary>
        public int Score
        {
            get;
            set;
        }
        /// <summary>
        /// 题目得分
        /// </summary>
        public decimal SubjectScore
        {
            get;
            set;
        }
        /// <summary>
        /// 回答项
        /// </summary>
        public string SelectedItem
        {
            get;
            set;
        }
        /// <summary>
        /// 题目编号
        /// </summary>
        public string TrainingItemCode
        {
            get;
            set;
        }
    }
}
