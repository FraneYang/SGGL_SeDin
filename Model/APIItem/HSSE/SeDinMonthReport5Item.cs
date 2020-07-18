using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 赛鼎月报信息项 --5、本月大型、特种设备投入情况
    /// </summary>
    public class SeDinMonthReport5Item
    {
        /// <summary>
        /// ID
        /// </summary>
        public string MonthReport5Id
        {
            get;
            set;
        }
        /// <summary>
        /// 月报ID
        /// </summary>
        public string MonthReportId
        {
            get;
            set;
        }
        /// <summary>
        /// 单位名称
        /// </summary>
        public string UnitName
        {
            get;
            set;
        }
        /// <summary>
        /// 特种设备-汽车吊
        /// </summary>
        public int? T01
        {
            get;
            set;
        }
        /// <summary>
        /// 特种设备-履带吊
        /// </summary>
        public int? T02
        {
            get;
            set;
        }
        /// <summary>
        /// 特种设备-塔吊
        /// </summary>
        public int? T03
        {
            get;
            set;
        }
        /// <summary>
        /// 特种设备-门式起重机
        /// </summary>
        public int? T04
        {
            get;
            set;
        }
        /// <summary>
        /// 特种设备-升降机
        /// </summary>
        public int? T05
        {
            get;
            set;
        }
        /// <summary>
        /// 特种设备-叉车
        /// </summary>
        public int? T06
        {
            get;
            set;
        }
        /// <summary>
        /// 大型机具设备-挖掘机
        /// </summary>
        public int? D01
        {
            get;
            set;
        }
        /// <summary>
        /// 大型机具设备-装载机
        /// </summary>
        public int? D02
        {
            get;
            set;
        }
        /// <summary>
        /// 大型机具设备-拖板车
        /// </summary>
        public int? D03
        {
            get;
            set;
        }
        /// <summary>
        /// 大型机具设备-桩机
        /// </summary>
        public int? D04
        {
            get;
            set;
        }
        /// <summary>
        /// 特殊机具设备-吊篮
        /// </summary>
        public int? S01
        {
            get;
            set;
        }

        /// <summary>
        ///  合计
        /// </summary>
        public int? TotalNum
        {
            get;
            set;
        }
    }
}
