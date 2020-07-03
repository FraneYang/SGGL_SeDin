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
    }
}
