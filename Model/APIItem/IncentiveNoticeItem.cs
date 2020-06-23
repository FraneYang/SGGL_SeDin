using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 奖励通知单数据项
    /// </summary>
    public class IncentiveNoticeItem
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public string IncentiveNoticeId
        {
            get;
            set;
        }

        /// <summary>
        /// 编号
        /// </summary>
        public string IncentiveNoticeCode
        {
            get;
            set;
        }

        /// <summary>
        /// 项目ID
        /// </summary>
        public string ProjectId
        {
            get;
            set;
        }
        /// <summary>
        /// 奖励时间
        /// </summary>
        public string IncentiveDate
        {
            get;
            set;
        }
        /// <summary>
        /// 奖励类型ID
        /// </summary>
        public string RewardTypeId
        {
            get;
            set;
        }
        /// <summary>
        /// 奖励类型名称
        /// </summary>
        public string RewardTypeName
        {
            get;
            set;
        }
        /// <summary>
        /// 受奖单位ID
        /// </summary>
        public string UnitId
        {
            get;
            set;
        }
        /// <summary>
        /// 受奖单位名称
        /// </summary>
        public string UnitName
        {
            get;
            set;
        }
        /// <summary>
        /// 受奖班组ID
        /// </summary>
        public string TeamGroupId
        {
            get;
            set;
        }
        /// <summary>
        /// 受奖班组名称
        /// </summary>
        public string TeamGroupName
        {
            get;
            set;
        }
        /// <summary>
        /// 受奖个人ID
        /// </summary>
        public string PersonId
        {
            get;
            set;
        }
        /// <summary>
        /// 受奖个人名称
        /// </summary>
        public string PersonName
        {
            get;
            set;
        }
        /// <summary>
        /// 奖罚根据
        /// </summary>
        public string BasicItem
        {
            get;
            set;
        }
        /// <summary>
        /// 奖励金额
        /// </summary>
        public decimal? IncentiveMoney
        {
            get;
            set;
        }

        /// <summary>
        /// 币种
        /// </summary>
        public string Currency
        {
            get;
            set;
        }
        /// <summary>
        /// 奖励称号
        /// </summary>
        public string TitleReward
        {
            get;
            set;
        }
        /// <summary>
        /// 物质奖励
        /// </summary>
        public string MattleReward
        {
            get;
            set;
        }
        /// <summary>
        /// 有关证据（被奖罚人姓名、岗位、证件编号、有关文字描述或照片等）：
        /// </summary>
        public string FileContents
        {
            get;
            set;
        }
        /// <summary>
        /// 附件
        /// </summary>
        public string AttachUrl
        {
            get;
            set;
        }
        /// <summary>
        /// 编制人ID
        /// </summary>
        public string CompileManId
        {
            get;
            set;
        }
        /// <summary>
        /// 编制人姓名
        /// </summary>
        public string CompileManName
        {
            get;
            set;
        }
        /// <summary>
        /// 签发人ID
        /// </summary>
        public string SignManId
        {
            get;
            set;
        }
        /// <summary>
        /// 签发人姓名
        /// </summary>
        public string SignManName
        {
            get;
            set;
        }
        /// <summary>
        /// 批准人ID
        /// </summary>
        public string ApproveManId
        {
            get;
            set;
        }
        /// <summary>
        /// 批准人姓名
        /// </summary>
        public string ApproveManName
        {
            get;
            set;
        }
        /// <summary>
        /// 编制日期
        /// </summary>
        public string CompileDate
        {
            get;
            set;
        }
        /// <summary>
        /// 状态
        /// </summary>
        public string States
        {
            get;
            set;
        }
    }
}
