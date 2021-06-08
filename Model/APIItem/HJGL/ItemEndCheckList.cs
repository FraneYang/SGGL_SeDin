using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ItemEndCheckList
    {
        /// <summary>
        /// ID
        /// </summary>
        public string ItemEndCheckListId
        {
            get;
            set;
        }
        /// <summary>
        /// 试压包Id
        /// </summary>
        public string PTP_ID
        {
            get;
            set;
        }

        /// <summary>
        /// 编制人Id
        /// </summary>
        public string CompileMan
        {
            get;
            set;
        }
        /// <summary>
        /// 编制日期
        /// </summary>
        public DateTime? CompileDate
        {
            get;
            set;
        }
        /// <summary>
        /// 状态
        /// </summary>
        public string State
        {
            get;
            set;
        }
        /// <summary>
        /// A类是否整改完成
        /// </summary>
        public bool? AIsOK
        {
            get;
            set;
        }
        /// <summary>
        /// B类是否整改完成
        /// </summary>
        public bool? BIsOK
        {
            get;
            set;
        }
        /// <summary>
        /// A类整改完成
        /// </summary>
        public bool? AOKState
        {
            get;
            set;
        }
       
        /// <summary>
        ///  尾项明细
        /// </summary>
        public List<ItemEndCheck> ItemEndCheckItems
        {
            get;
            set;
        }

        /// <summary>
        ///  审批明细
        /// </summary>
        public List<TestPackageApprove> TestPackageApproveItems
        {
            get;
            set;
        }
        /// <summary>
        /// 之前状态
        /// </summary>
        public string OldState
        {
            get;
            set;
        }
        /// <summary>
        /// 当前操作人Id
        /// </summary>
        public string CurrUserId
        {
            get;
            set;
        }
        /// <summary>
        /// 保存或提交
        /// </summary>
        public string SaveOrSubmit
        {
            get;
            set;
        }
    }
}
