using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class TestPackageApprove
    {
        /// <summary>
        /// ID
        /// </summary>
        public string ApproveId
        {
            get;
            set;
        }
        /// <summary>
        /// 办理时间
        /// </summary>
        public DateTime? ApproveDate
        {
            get;
            set;
        }
        /// <summary>
        /// 办理意见
        /// </summary>
        public string Opinion
        {
            get;
            set;
        }
        /// <summary>
        /// 办理人Id
        /// </summary>
        public string ApproveMan
        {
            get;
            set;
        }
        /// <summary>
        /// 办理类型
        /// </summary>
        public string ApproveType
        {
            get;
            set;
        }
        /// <summary>
        /// 主表Id
        /// </summary>
        public string ItemEndCheckListId
        {
            get;
            set;
        }
    }
}
