using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// 处罚通知单处罚项
    /// </summary>
    public class PunishNoticeItemItem
    {
        /// <summary>
        /// ID
        /// </summary>
        public string PunishNoticeItemId
        {
            get;
            set;
        }
        /// <summary>
        /// 处罚单ID
        /// </summary>
        public string PunishNoticeId
        {
            get;
            set;
        }
        /// <summary>
        /// 处罚内容
        /// </summary>
        public string PunishContent
        {
            get;
            set;
        }
        /// <summary>
        /// 处罚金额
        /// </summary>
        public decimal? PunishMoney
        {
            get;
            set;
        }
        /// <summary>
        /// 排序
        /// </summary>
        public int? SortIndex
        {
            get;
            set;
        }
    }
}
