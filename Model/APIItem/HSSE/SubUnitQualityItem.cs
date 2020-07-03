using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class SubUnitQualityItem
    {
        /// <summary>
        /// 单位ID
        /// </summary>
        public string UnitId { get; set; }
        public string UnitName { get; set; }
        public string ProjectId { get; set; }
        public string SubUnitQualityId { get; set; }
        public string SubUnitQualityName { get; set; }
        public string BusinessLicense { get; set; }
        public string BL_EnableDate { get; set; }
        public string BL_ScanUrl { get; set; }
        public string Certificate { get; set; }
        public string C_EnableDate { get; set; }
        public string C_ScanUrl { get; set; }
        public string QualityLicense { get; set; }
        public string QL_EnableDate { get; set; }
        public string QL_ScanUrl { get; set; }
        public string HSELicense { get; set; }
        public string H_EnableDate { get; set; }
        public string H_ScanUrl { get; set; }
        public string HSELicense2 { get; set; }
        public string H_EnableDate2 { get; set; }
        public string H_ScanUrl2 { get; set; }
        public string SecurityLicense { get; set; }
        public string SL_EnableDate { get; set; }
        public string SL_ScanUrl { get; set; }
    }
}
