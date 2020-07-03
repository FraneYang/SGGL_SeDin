using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Model
{
    public class SpotCheckItem
    {
        /// <summary>
        /// 
        /// </summary>
        public Check_SpotCheckDetail SpotCheckDetail { get; set; }
        /// <summary>
        /// 工作包
        /// </summary>
        public WBS_ControlItemAndCycle controlItemAndCycle { get; set; }
        public List<WBS_ControlItemAndCycle> controlItemAndCycleList { get; set; }
        /// <summary>
        /// 分部工程
        /// </summary>
        public Base_CNProfessional CNProfessional { get; set; }
        /// <summary>
        /// 分项工程
        /// </summary>

        public WBS_WorkPackage workPackage { get; set; }
        /// <summary>
        /// 单位工程
        /// </summary>

        public WBS_UnitWork unitWork { get; set; }
    }
}