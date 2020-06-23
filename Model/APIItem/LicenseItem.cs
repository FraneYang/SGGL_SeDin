using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{ 
    /// <summary>
    /// 作业票明细-安全措施项
    /// </summary>
    public class LicenseItem
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public string LicenseItemId
        {
            get;
            set;
        }
        /// <summary>
        /// 单据ID
        /// </summary>
        public string DataId
        {
            get;
            set;
        }
        /// <summary>
        /// 序号
        /// </summary>
        public int SortIndex
        {
            get;
            set;
        }
        /// <summary>
        /// 安全措施
        /// </summary>
        public string SafetyMeasures
        {
            get;
            set;
        }
        /// <summary>
        /// 适用
        /// </summary>
        public bool IsUsed
        {
            get;
            set;
        }
        /// <summary>
        /// 确认人ID
        /// </summary>
        public string ConfirmManId
        {
            get;
            set;
        }
        /// <summary>
        /// 确认人姓名
        /// </summary>
        public string ConfirmManName
        {
            get;
            set;
        }
    }
}
