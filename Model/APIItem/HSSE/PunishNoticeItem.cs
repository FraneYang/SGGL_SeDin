using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 处罚通知单数据项
    /// </summary>
    public class PunishNoticeItem
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public string PunishNoticeId
        {
            get;
            set;
        }
        /// <summary>
        /// 编号
        /// </summary>
        public string PunishNoticeCode
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
        /// 受罚单位ID
        /// </summary>
        public string UnitId
        {
            get;
            set;
        }
        /// <summary>
        /// 受罚单位名称
        /// </summary>
        public string UnitName
        {
            get;
            set;
        }
        /// <summary>
        /// 受罚人ID
        /// </summary>
        public string PunishPersonId
        {
            get;
            set;
        }
        /// <summary>
        /// 受罚人名称
        /// </summary>
        public string PunishPersonName
        {
            get;
            set;
        }
        /// <summary>
        /// 合同号
        /// </summary>
        public string ContractNum
        {
            get;
            set;
        }
        /// <summary>
        /// 处罚时间
        /// </summary>
        public string PunishNoticeDate
        {
            get;
            set;
        }
        /// <summary>
        /// 处罚根据
        /// </summary>
        public string BasicItem
        {
            get;
            set;
        }
        /// <summary>
        /// 处罚原因
        /// </summary>
        public string IncentiveReason
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
        /// 币种
        /// </summary>
        public string Currency
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
        /// 是否同意
        /// </summary>
        public bool? IsAgree
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
        /// 编制日期
        /// </summary>
        public string CompileDate
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
        /// 签发日期
        /// </summary>
        public string SignDate
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
        /// 批准日期
        /// </summary>
        public string ApproveDate
        {
            get;
            set;
        }
        /// <summary>
        ///责任人ID
        /// </summary>
        public string DutyPersonId
        {
            get;
            set;
        }
        /// <summary>
        /// 责任人姓名
        /// </summary>
        public string DutyPersonName
        {
            get;
            set;
        }
        /// <summary>
        /// 责任人回执日期
        /// </summary>
        public string DutyPersonDate
        {
            get;
            set;
        }

        /// <summary>
        /// 专业工程师ID
        /// </summary>
        public string ProfessionalEngineerId
        {
            get;
            set;
        }
        /// <summary>
        /// 专业工程师姓名
        /// </summary>
        public string ProfessionalEngineerName
        {
            get;
            set;
        }
        /// <summary>
        /// 专业工程师抄送日期
        /// </summary>
        public string ProfessionalEngineerTime
        {
            get;
            set;
        }
        /// <summary>
        /// 施工经理ID
        /// </summary>
        public string ConstructionManagerId
        {
            get;
            set;
        }
        /// <summary>
        /// 施工经理姓名
        /// </summary>
        public string ConstructionManagerName
        {
            get;
            set;
        }
        /// <summary>
        /// 施工经理抄送日期
        /// </summary>
        public string ConstructionManagerTime
        {
            get;
            set;
        }
        /// <summary>
        /// 施工单位项目负责人ID
        /// </summary>
        public string UnitHeadManId
        {
            get;
            set;
        }
        /// <summary>
        /// 施工单位项目负责人姓名
        /// </summary>
        public string UnitHeadManName
        {
            get;
            set;
        }
        /// <summary>
        /// 施工单位项目负责人 接收日期
        /// </summary>
        public string UnitHeadManTime
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
        /// <summary>
        /// 处罚单状态（0待提交；1待签发；2待批准；3待回执；4已闭环）
        /// </summary>
        public string PunishStates
        {
            get;
            set;
        }
        /// <summary>
        /// 通知单附件
        /// </summary>
        public string PunishUrl
        {
            get;
            set;
        }
        /// <summary>
        /// 回执单附件
        /// </summary>
        public string ReceiptUrl
        {
            get;
            set;
        }
        public string HazardRegisterId
        {
            get;
            set;
        }

        /// <summary>
        /// 专项检查主键
        /// </summary>
        public string CheckSpecialDetailId
        {
            get;
            set;
        }
        /// <summary>
        ///   审核记录
        /// </summary>
        public List<FlowOperateItem> FlowOperateItem
        {
            get;
            set;
        }
        /// <summary>
        ///  处罚明细表
        /// </summary>
        public List<PunishNoticeItemItem> PunishNoticeItemItem
        {
            get;
            set;
        }
    }
}
