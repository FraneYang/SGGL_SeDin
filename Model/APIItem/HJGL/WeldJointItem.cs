using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
   public  class WeldJointItem
    {
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
        /// 管线ID
        /// </summary>
        public string PipelineId
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
        /// 单位工程
        /// </summary>
        public string UnitWorkId
        {
            get;
            set;
        }

        /// <summary>
        /// 打底焊工ID
        /// </summary>
        public string BackingWelderId
        {
            get;
            set;
        }

        /// <summary>
        /// 打底焊工代号
        /// </summary>
        public string BackingWelderCode
        {
            get;
            set;
        }

        /// <summary>
        /// 盖面焊工ID
        /// </summary>
        public string CoverWelderId
        {
            get;
            set;
        }

        /// <summary>
        /// 盖面焊工代号
        /// </summary>
        public string CoverWelderCode
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
        /// 焊口属性
        /// </summary>
        public string JointAttribute
        {
            get;
            set;
        }

        /// <summary>
        /// 焊口机动化程度（手工、机动/自动）
        /// </summary>
        public string WeldingMode
        {
            get;
            set;
        }

        
        /// <summary>
        /// 寸径
        /// </summary>
        public decimal? Size
        {
            get;
            set;
        }

        /// <summary>
        /// 外径
        /// </summary>
        public decimal? Dia
        {
            get;
            set;
        }

        /// <summary>
        /// 壁厚
        /// </summary>
        public decimal? Thickness
        {
            get;
            set;
        }

        /// <summary>
        /// 焊接方法
        /// </summary>
        public string WeldingMethodCode
        {
            get;
            set;
        }

        /// <summary>
        /// 是否热处理
        /// </summary>
        public string IsHotProess
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
