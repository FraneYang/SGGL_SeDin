using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class TestPackageItem
    {
        /// <summary>
        /// 试压包ID
        /// </summary>
        public string PTP_ID
        {
            get;
            set;
        }

        /// <summary>
        /// 试压包号
        /// </summary>
        public string TestPackageNo
        {
            get;
            set;
        }

        /// <summary>
        /// 管线号
        /// </summary>
        public string PipelineCode
        {
            get;
            set;
        }

        /// <summary>
        /// 总焊口数
        /// </summary>
        public int WeldJointCount
        {
            get;
            set;
        }

        /// <summary>
        /// 完成焊口数
        /// </summary>
        public int WeldJointCountT
        {
            get;
            set;
        }

        /// <summary>
        /// 检测合格焊口数
        /// </summary>
        public int? CountS
        {
            get;
            set;
        }

        /// <summary>
        /// 检测不合格焊口数
        /// </summary>
        public int? CountU
        {
            get;
            set;
        }

        /// <summary>
        /// 检测比例
        /// </summary>
        public string NDTR_Name
        {
            get;
            set;
        }

        /// <summary>
        /// 实际检测比例
        /// </summary>
        public string Ratio
        {
            get;
            set;
        }
    }
}
