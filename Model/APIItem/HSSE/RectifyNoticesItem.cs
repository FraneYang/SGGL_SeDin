using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 隐患整改通知单项
    /// </summary>
    public class RectifyNoticesItem
    {
        /// <summary>
        /// ID
        /// </summary>
        public string RectifyNoticesId
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
        /// 项目名称
        /// </summary>
        public string ProjectName
        {
            get;
            set;
        }
        /// <summary>
        /// 编号
        /// </summary>
        public string RectifyNoticesCode
        {
            get;
            set;
        }
        /// <summary>
        /// 受检单位ID
        /// </summary>
        public string UnitId
        {
            get;
            set;
        }
        /// <summary>
        /// 受检单位名称
        /// </summary>
        public string UnitName
        {
            get;
            set;
        }
        /// <summary>
        /// 区域ID
        /// </summary>
        public string WorkAreaId
        {
            get;
            set;
        }
        /// <summary>
        /// 区域名称
        /// </summary>
        public string WorkAreaName
        {
            get;
            set;
        }
        /// <summary>
        /// 检查人员姓名(输入)
        /// </summary>
        public string CheckManNames
        {
            get;
            set;
        }
        /// <summary>
        /// 检查人员Id(选择)
        /// </summary>
        public string CheckManIds
        {
            get;
            set;
        }
        /// <summary>
        /// 检查人员Id(选择)
        /// </summary>
        public string CheckManIdNames
        {
            get;
            set;
        }
        /// <summary>
        /// 检查人员姓名(所有)
        /// </summary>
        public string CheckManAllNames
        {
            get;
            set;
        }
        /// <summary>
        /// 检查日期
        /// </summary>
        public string CheckedDate
        {
            get;
            set;
        }
        /// <summary>
        /// 检查日期
        /// </summary>
        public DateTime? CheckedDateD
        {
            get;
            set;
        }
        /// <summary>
        /// 隐患类别(1-一般;2-较大;3-重大)
        /// </summary>
        public string HiddenHazardType
        {
            get;
            set;
        }
        /// <summary>
        /// 编制人ID
        /// </summary>
        public string CompleteManId
        {
            get;
            set;
        }
        /// <summary>
        /// 编制人姓名
        /// </summary>
        public string CompleteManName
        {
            get;
            set;
        }
        /// <summary>
        /// 隐患类别
        /// </summary>
        public string HiddenHazardTypeName
        {
            get;
            set;
        }
        /// <summary>
        /// 安全隐患内容及整改意见
        /// </summary>
        public string WrongContent
        {
            get;
            set;
        }
        /// <summary>
        /// 签发人(总包单位项目安全经理)ID
        /// </summary>
        public string SignPersonId
        {
            get;
            set;
        }
        /// <summary>
        /// 签发人(总包单位项目安全经理)姓名
        /// </summary>
        public string SignPersonName
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
        /// 专业工程师（整改前）抄送日期1
        /// </summary>
        public string ProfessionalEngineerTime1
        {
            get;
            set;
        }
        /// <summary>
        /// 专业工程师（整改后）抄送日期2
        /// </summary>
        public string ProfessionalEngineerTime2
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
        /// 施工经理（整改前）抄送日期1
        /// </summary>
        public string ConstructionManagerTime1
        {
            get;
            set;
        }
        /// <summary>
        /// 施工经理（整改后）抄送日期2
        /// </summary>
        public string ConstructionManagerTime2
        {
            get;
            set;
        }
        /// <summary>
        /// 项目经理ID
        /// </summary>
        public string ProjectManagerId
        {
            get;
            set;
        }
        /// <summary>
        /// 项目经理姓名
        /// </summary>
        public string ProjectManagerName
        {
            get;
            set;
        }
        /// <summary>
        /// 项目经理（整改前）抄送日期1
        /// </summary>
        public string ProjectManagerTime1
        {
            get;
            set;
        }
        /// <summary>
        /// 项目经理（整改后）抄送日期2
        /// </summary>
        public string ProjectManagerTime2
        {
            get;
            set;
        }
        /// <summary>
        /// 整改措施和完成情况
        /// </summary>
        public string CompleteStatus
        {
            get;
            set;
        }
        /// <summary>
        /// 接收人（施工单位项目安全经理）ID
        /// </summary>
        public string DutyPersonId
        {
            get;
            set;
        }
        /// <summary>
        /// 接收人（施工单位项目安全经理）姓名
        /// </summary>
        public string DutyPersonName
        {
            get;
            set;
        }
        /// <summary>
        /// 接收人（施工单位项目安全经理） 接收日期
        /// </summary>
        public string DutyPersonTime
        {
            get;
            set;
        }
        /// <summary>
        /// 整改日期
        /// </summary>
        public string CompleteDate
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
        /// 施工单位项目负责人审核日期
        /// </summary>
        public string UnitHeadManDate
        {
            get;
            set;
        }
        /// <summary>
        /// 整改人姓名
        /// </summary>
        public string RectificationName
        {
            get;
            set;
        }
      
        /// <summary>
        /// 是否完成
        /// </summary>
        public bool? IsRectify
        {
            get;
            set;
        }
        /// <summary>
        /// 总包单位复查意见
        /// </summary>
        public string ReCheckOpinion
        {
            get;
            set;
        }
        /// <summary>
        /// 检查人ID
        /// </summary>
        public string CheckPersonId
        {
            get;
            set;
        }
        /// <summary>
        /// 检查人姓名
        /// </summary>
        public string CheckPersonName
        {
            get;
            set;
        }
        /// <summary>
        /// 复查日期
        /// </summary>
        public string ReCheckDate
        {
            get;
            set;
        }
        /// <summary>
        /// 图片/附件
        /// </summary>
        public string AttachUrl
        {
            get;
            set;
        }
        /// <summary>
        ///  整改前图片/附件
        /// </summary>
        public string BeAttachUrl
        {
            get;
            set;
        }

        /// <summary>
        /// 整改后图片/附件
        /// </summary>
        public string AfAttachUrl
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
        /// 状态（0：待提交；1：待签发；2：待整改；3：待复查，4:已完成；）
        /// </summary>
        public string States
        {
            get;
            set;
        }
        /// <summary>
        /// 巡检ID
        /// </summary>
        public string HazardRegisterId
        {
            get;
            set;
        }
        /// <summary>
        /// 专项检查明细项ID
        /// </summary>
        public string CheckSpecialDetailId
        {
            get;
            set;
        }
        /// <summary>
        ///  隐患整改明细表
        /// </summary>
        public List<RectifyNoticesItemItem> RectifyNoticesItemItem
        {
            get;
            set;
        }
        /// <summary>
        ///  隐患整改审核记录
        /// </summary>
        public List<RectifyNoticesFlowOperateItem> RectifyNoticesFlowOperateItem
        {
            get;
            set;
        }
    }
}
