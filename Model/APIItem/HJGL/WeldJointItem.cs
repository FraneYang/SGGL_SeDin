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
        /// 焊口标识
        /// </summary>
        public string WeldJointIdentify
        {
            get;
            set;
        }

        /// <summary>
        /// 焊口绝对位置
        /// </summary>
        public string Position
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
        /// 单位工程
        /// </summary>
        public string UnitWorkId
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
        /// 单线图号
        /// </summary>
        public string SingleNumber
        {
            get;
            set;
        }

        /// <summary>
        /// 管道等级
        /// </summary>
        public string PipingClass
        {
            get;
            set;
        }

        /// <summary>
        /// 介质代号
        /// </summary>
        public string  Medium
        {
            get;
            set;
        }

        /// <summary>
        /// 探伤比例
        /// </summary>
        public string DetectionRate
        {
            get;
            set;
        }

        /// <summary>
        /// 探伤类型
        /// </summary>
        public string DetectionType
        {
            get;
            set;
        }

        /// <summary>
        /// 试验压力
        /// </summary>
        public decimal? TestPressure
        {
            get;
            set;
        }

        public string TestMedium
        {
            get;
            set;
        }

        /// <summary>
        /// 焊缝类型
        /// </summary>
        public string WeldType
        {
            get;
            set;
        }

        /// <summary>
        /// 材质1
        /// </summary>
        public string Material1
        {
            get;
            set;
        }

        /// <summary>
        /// 材质2
        /// </summary>
        public string Material2
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
        /// 焊接日期
        /// </summary>
        public DateTime? WeldingDate
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
        /// 焊口属性(活动口，固定口)
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
        /// 焊口规格
        /// </summary>
        public string Specification
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
        /// 坡口类型
        /// </summary>
        public string GrooveType
        {
            get;
            set;
        }

        /// <summary>
        /// 焊接位置
        /// </summary>
        public string WeldingLocation
        {
            get;
            set;
        }

        /// <summary>
        /// 焊丝牌号
        /// </summary>
        public string WeldingWire
        {
            get;
            set;
        }

        /// <summary>
        /// 焊条牌号
        /// </summary>
        public string WeldingRod
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
