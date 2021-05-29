using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class NDETrustItem
    {
        /// <summary>
        /// 点口批ID
        /// </summary>
        public string PointBatchId
        {
            get;
            set;
        }

        /// <summary>
        /// 点口批号
        /// </summary>
        public string PointBatchCode
        {
            get;
            set;
        }

        /// <summary>
        /// 点口批明细ID
        /// </summary>
        public string PointBatchItemId
        {
            get;
            set;
        }

        /// <summary>
        /// 点口状态
        /// </summary>
        public string PointState
        {
            get;
            set;
        }

        /// <summary>
        /// 批开始日期
        /// </summary>
        public DateTime? StartDate
        {
            get;
            set;
        }

        /// <summary>
        /// 焊口ID
        /// </summary>
        public string WeldJointId
        {
            get;
            set;
        }

        /// <summary>
        /// 焊口号
        /// </summary>
        public string WeldJointCode
        {
            get;
            set;
        }

        /// <summary>
        /// 管线编号
        /// </summary>
        public string PipelineCode
        {
            get;
            set;
        }

        /// <summary>
        /// 焊接区域
        /// </summary>
        public string JointArea
        {
            get;
            set;
        }

        /// <summary>
        /// 检测结果
        /// </summary>
        public string CheckResult
        {
            get;
            set;
        }


        /// <summary>
        /// 附件路径
        /// </summary>
        public string AttachUrl
        {
            get;
            set;
        }

        /// <summary>
        /// 焊工号
        /// </summary>
        public string WelderCode
        {
            get;
            set;
        }

        /// <summary>
        /// 检测日期
        /// </summary>
        public DateTime? FilmDate
        {
            get;
            set;
        }

        /// <summary>
        /// 报告日期
        /// </summary>
        public DateTime? ReportDate
        {
            get;
            set;
        }

        /// <summary>
        /// 检测总数
        /// </summary>
        public int? TotalFilm
        {
            get;
            set;
        }

        /// <summary>
        /// 合格数
        /// </summary>
        public int? PassFilm
        {
            get;
            set;
        }

        /// <summary>
        /// 评定级别
        /// </summary>
        public string JudgeGrade
        {
            get;
            set;
        }

        /// <summary>
        /// 缺陷
        /// </summary>
        public string CheckDefects
        {
            get;
            set;
        }

        /// <summary>
        /// 返修位置
        /// </summary>
        public string RepairLocation
        {
            get;
            set;
        }
    }
}
