using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class JointCompreInfoItem
    {
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
        public string Medium
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
        /// 焊缝类型
        /// </summary>
        public string WeldType
        {
            get;
            set;
        }

        /// <summary>
        /// 材质2
        /// </summary>
        public string Material
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
        /// 焊工代号
        /// </summary>
        public string WelderCode
        {
            get;
            set;
        }

        /// <summary>
        /// 焊接日期
        /// </summary>
        public string WeldingDate
        {
            get;
            set;
        }

        /// <summary>
        /// 日报编号
        /// </summary>
        public string WeldingDailyCode
        {
            get;
            set;
        }

        /// <summary>
        /// 所在批次
        /// </summary>
        public string PointBatchCode
        {
            get;
            set;
        }

        /// <summary>
        /// 点口/扩透
        /// </summary>
        public string IsPoint
        {
            get;
            set;
        }
    }
}
