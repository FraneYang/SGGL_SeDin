using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 焊接日报-预焊接
    /// </summary>
    public class HJGL_PreWeldingDailyItem
    {
        /// <summary>
        /// ID
        /// </summary>
        public string PreWeldingDailyId
        {
            get;
            set;
        }
        /// <summary>
        /// 编号
        /// </summary>
        public string ProjectId
        {
            get;
            set;
        }

        /// <summary>
        /// 区域ID
        /// </summary>
        public string UnitWorkId
        {
            get;
            set;
        }
    
        /// <summary>
        /// 区域名称
        /// </summary>
        public string UnitWorkName
        {
            get;
            set;
        }
        /// <summary>
        /// 单位ID
        /// </summary>
        public string UnitId
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
        /// 焊口ID
        /// </summary>
        public string WeldJointId
        {
            get;
            set;
        }
        /// <summary>
        /// 焊口编码
        /// </summary>
        public string WeldJointCode
        {
            get;
            set;
        }

    //    public List<Model.BaseInfoItem> BaseInfoItem
    //    {
    //        get; set;
    //    }
   }
}
