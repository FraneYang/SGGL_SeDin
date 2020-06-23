using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class UnitItem
    {
        /// <summary>
        /// 单位ID
        /// </summary>
        public string UnitId { get; set; }
        public string UnitCode { get; set; }
        public string UnitName { get; set; }
        public string UnitTypeId { get; set; }
        public string ProjectRange { get; set; }
        public string Corporate { get; set; }
        public string Address { get; set; }
        public string Telephone { get; set; }
        public string Fax { get; set; }
        public string IsThisUnit { get; set; }
        public string IsBuild { get; set; }
        public string EMail { get; set; }
        public string IsHide { get; set; }
        public string IsBranch { get; set; }
        public string ShortUnitName { get; set; }
        public string DataSources { get; set; }
        public string FromUnitId { get; set; }
    }
}
