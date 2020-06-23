using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class HazardRegisterItem
    {
        /// <summary>
        /// 巡检ID
        /// </summary>
        public string HazardRegisterId
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
        /// 状态
        /// </summary>
        public string States
        {
            get;
            set;
        }
        /// <summary>
        /// 状态名称
        /// </summary>
        public string StatesStr
        {
            get;
            set;
        }
        /// <summary>
        /// 巡检类型（D-日 W-周 M-月）
        /// </summary>
        public string CheckCycle
        {
            get;
            set;
        }
        /// <summary>
        /// 编号
        /// </summary>
        public string HazardCode
        {
            get;
            set;
        }      
        /// <summary>
        /// 问题描述
        /// </summary>
        public string RegisterDef
        {
            get;
            set;
        }
        /// <summary>
        /// 问题类型id
        /// </summary>
        public string RegisterTypesId
        {
            get;
            set;
        }
        /// <summary>
        /// 问题类型名称
        /// </summary>
        public string RegisterTypesName
        {
            get;
            set;
        }
        /// <summary>
        /// 整改措施 -反馈
        /// </summary>
        public string Rectification
        {
            get;
            set;
        }
        /// <summary>
        /// 责任单位ID
        /// </summary>
        public string ResponsibleUnit
        {
            get;
            set;
        }
        /// <summary>
        /// 责任单位-名称
        /// </summary>
        public string ResponsibilityUnitName
        {
            get;
            set;
        }
        /// <summary>
        /// 责任人-ID
        /// </summary>
        public string ResponsibleMan
        {
            get;
            set;
        }
        /// <summary>
        ///责任人 -名称
        /// </summary>
        public string ResponsibilityManName
        {
            get;
            set;
        }
        /// <summary>
        ///责任人 -电话
        /// </summary>
        public string ResponsibilityManTel
        {
            get;
            set;
        }
        /// <summary>
        /// 区域id
        /// </summary>
        public string Place
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
        /// 检查人id
        /// </summary>
        public string CheckManId
        {
            get;
            set;
        }
        /// <summary>
        /// 检查人姓名
        /// </summary>
        public string CheckManName
        {
            get;
            set;
        }
        /// <summary>
        /// 检查人电话
        /// </summary>
        public string CheckManTel
        {
            get;
            set;
        }
        /// <summary>
        /// 检查时间
        /// </summary>
        public DateTime? CheckTime
        {
            get;
            set;
        }
        /// <summary>
        /// 整改期限
        /// </summary>
        public DateTime? RectificationPeriod
        {
            get;
            set;
        }
        /// <summary>
        /// 整改前照片
        /// </summary>
        public string ImageUrl
        {
            get;
            set;
        }
        /// <summary>
        /// 整改后照片
        /// </summary>
        public string RectificationImageUrl
        {
            get;
            set;
        }
        /// <summary>
        /// 整改时间
        /// </summary>
        public DateTime? RectificationTime
        {
            get;
            set;
        }
        /// <summary>
        /// 处罚金额
        /// </summary>
        public int CutPayment
        {
            get;
            set;
        }
        /// <summary>
        /// 问题类型 安全 默认值 1
        /// </summary>
        public string ProblemTypes
        {
            get;
            set;
        }
        /// <summary>
        /// 复检问题描述
        /// </summary>
        public string HandleIdea
        {
            get;
            set;
        }
        /// <summary>
        /// 是否合格 
        /// </summary>
        public bool? SafeSupervisionIsOK
        {
            get;
            set;
        }
        /// <summary>
        /// 确认人 id
        /// </summary>
       public string ConfirmMan
        {
            get;
            set;
        }
      /// <summary>
      /// 确认人名称
      /// </summary>
        public string ConfirmManName
        {
            get;
            set;
        }
        /// <summary>
        /// 确认人电话
        /// </summary>
        public string ConfirmManTel
        {
            get;
            set;
        }
        /// <summary>
        /// 确认时间
        /// </summary>
        public DateTime? ConfirmDate
        {
            get;
            set;
        }
        /// <summary>
        /// 抄送人员IDs
        /// </summary>
        public string CCManIds
        {
            get;
            set;
        }
        /// <summary>
        /// 抄送姓名IDs
        /// </summary>
        public string CCManNames
        {
            get;
            set;
        }
        /// <summary>
        /// 整改要求
        /// </summary>
        public string Requirements
        {
            get;
            set;
        }
        /// <summary>
        /// 相关单据类型（1-整改单；2-惩罚单；3-停工令）
        /// </summary>
        public string ResultType
        {
            get;
            set;
        }
        /// <summary>
        /// 相关单据ID
        /// </summary>
        public string ResultId
        {
            get;
            set;
        }
    }
}
