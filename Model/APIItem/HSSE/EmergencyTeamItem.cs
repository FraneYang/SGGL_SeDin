using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 应急队伍信息
    /// </summary>
    public class EmergencyTeamItem
    {
        /// <summary>
        /// ID
        /// </summary>
        public string EmergencyTeamItemId
        {
            get;
            set;
        }
        /// <summary>
        /// 应急主表ID
        /// </summary>
        public string FileId
        {
            get;
            set;
        }

        /// <summary>
        /// 队员ID
        /// </summary>
        public string PersonId
        {
            get;
            set;
        }
        /// <summary>
        /// 队员姓名
        /// </summary>
        public string PersonName
        {
            get;
            set;
        }

        /// <summary>
        /// 职务
        /// </summary>
        public string Job
        {
            get;
            set;
        }
        /// <summary>
        /// 电话
        /// </summary>
        public string Tel
        {
            get;
            set;
        }
    }
}
