using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 工程暂停令数据项
    /// </summary>
    public class PauseNoticeItem
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public string PauseNoticeId
        {
            get;
            set;
        }
        /// <summary>
        /// 编号
        /// </summary>
        public string PauseNoticeCode
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
        /// 工程部位
        /// </summary>
        public string ProjectPlace
        {
            get;
            set;
        }
        /// <summary>
        /// 安全和质量隐患内容
        /// </summary>
        public string WrongContent
        {
            get;
            set;
        }
        /// <summary>
        /// 暂停时间
        /// </summary>
        public string PauseTime
        {
            get;
            set;
        }
        /// <summary>
        /// 暂停作业项
        /// </summary>
        public string PauseContent
        {
            get;
            set;
        }
        /// <summary>
        /// 第一条内容
        /// </summary>
        public string OneContent
        {
            get;
            set;
        }
        /// <summary>
        /// 第二条内容
        /// </summary>
        public string SecondContent
        {
            get;
            set;
        }
        /// <summary>
        /// 第三条内容
        /// </summary>
        public string ThirdContent
        {
            get;
            set;
        }       
        /// <summary>
        /// 是否确认
        /// </summary>
        public bool? IsConfirm
        {
            get;
            set;
        }
        /// <summary>
        /// 是否确认
        /// </summary>
        public string IsConfirmName
        {
            get;
            set;
        }
        /// <summary>
        /// 确认日期
        /// </summary>
        public string ConfirmDate
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
        /// 编制时间
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
        /// 签发时间
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
        /// 批准时间
        /// </summary>
        public string ApproveDate
        {
            get;
            set;
        }
        /// <summary>
        /// 接收人ID
        /// </summary>
        public string DutyPersonId
        {
            get;
            set;
        }
        /// <summary>
        /// 接收人姓名
        /// </summary>
        public string DutyPersonName
        {
            get;
            set;
        }
        /// <summary>
        /// 接收时间
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
        /// 监理ID
        /// </summary>
        public string SupervisorManId
        {
            get;
            set;
        }
        /// <summary>
        /// 监理姓名
        /// </summary>
        public string SupervisorManName
        {
            get;
            set;
        }
        /// <summary>
        /// 监理 接收日期
        /// </summary>
        public string SupervisorManTime
        {
            get;
            set;
        }
        /// <summary>
        /// 业主ID
        /// </summary>
        public string OwnerId
        {
            get;
            set;
        }
        /// <summary>
        /// 业主姓名
        /// </summary>
        public string OwnerName
        {
            get;
            set;
        }
        /// <summary>
        /// 业主 接收日期
        /// </summary>
        public string OwnerTime
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
        /// 暂停令状态（0待提交；1待签发；2待批准；3待接收；4已闭环）
        /// </summary>
        public string PauseStates
        {
            get;
            set;
        }
        public string HazardRegisterId
        {
            get;
            set;
        }
        public string CheckSpecialDetailId
        {
            get;
            set;
        }
        /// <summary>
        /// 附件
        /// </summary>
        public string PauseNoticeAttachUrl
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
    }
}
