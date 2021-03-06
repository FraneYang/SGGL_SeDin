﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 焊接日报明细
    /// </summary>
    public class SpWeldingDailyItem
    {
        public string WeldTaskId
        {
            get;
            set;
        }
        /// <summary>
        /// 焊口id
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
        /// 规格
        /// </summary>
        public string Specification
        {
            get;
            set;
        }
        /// <summary>
        /// 盖面焊工号
        /// </summary>
        public string CoverWelderCode
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
        /// 打底焊工号
        /// </summary>
        public string BackingWelderCode
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
        /// 可焊焊工号
        /// </summary>
        public string CanWelderCode
        {
            get;
            set;
        }

        /// <summary>
        /// 可焊焊工ID
        /// </summary>
        public string CanWelderId
        {
            get;
            set;
        }

        /// <summary>
        /// 焊缝类型
        /// </summary>
        public string WeldTypeId
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
        /// 焊接位置Id
        /// </summary>
        public string WeldingLocationId
        {
            get;
            set;
        }
        /// <summary>
        /// 焊接位置代号
        /// </summary>
        public string WeldingLocationCode
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
        /// 焊接类型
        /// </summary>
        public string WeldTypeCode
        {
            get;
            set;
        }

        // 是否热处理
        public string IsHotProessStr
        {
            get;
            set;
        }

        /// <summary>
        /// 序号
        /// </summary>
        public int Num
        {
            get;
            set;
        }
        /// <summary>
        /// 机动化程度
        /// </summary>
        public string WeldingMode
        {
            get;
            set;
        }
        /// <summary>
        /// 是否已焊
        /// </summary>
        public string IsWelding {
            get;
            set;
        }
        /// <summary>
        ///任务日期
        /// </summary>
        public DateTime TaskDate
        {
            get;
            set;
        }

        public string WeldingWireCode
        {
            get;
            set;
        }
        public string WeldingRodCode
        {
            get;
            set;
        }
    }
}
