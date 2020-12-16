using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class PersonItem
    {
        /// <summary>
        /// 人员主键ID
        /// </summary>
        public string PersonId { get; set; }
        /// <summary>
        /// 卡号
        /// </summary>
        public string CardNo { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string PersonName { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public string SexName { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public string Sex { get; set; }
        /// <summary>
        /// 身份证号码
        /// </summary>
        public string IdentityCard { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 项目id
        /// </summary>
        public string ProjectId { get; set; }
        /// <summary>
        /// 项目号
        /// </summary>
        public string ProjectCode { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName { get; set; }
        /// <summary>
        /// 单位ID
        /// </summary>
        public string UnitId { get; set; }
        /// <summary>
        /// 单位代号
        /// </summary>
        public string UnitCode { get; set; }
        /// <summary>
        /// 单位名称
        /// </summary>
        public string UnitName { get; set; }
        /// <summary>
        /// 班组ID
        /// </summary>
        public string TeamGroupId { get; set; }
        /// <summary>
        /// 班组名称
        /// </summary>
        public string TeamGroupName { get; set; }    
        /// <summary>
        /// 岗位ID
        /// </summary>
        public string WorkPostId { get; set; }
        /// <summary>
        /// 岗位名称
        /// </summary>
        public string WorkPostName { get; set; }
        /// <summary>
        /// 入场时间
        /// </summary>
        public string InTime { get; set; }
        /// <summary>
        /// 出场时间
        /// </summary>
        public string OutTime { get; set; }
        /// <summary>
        /// 出场原因
        /// </summary>
        public string OutResult { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string Telephone { get; set; }        
        /// <summary>
        /// 照片路径
        /// </summary>
        public string PhotoUrl { get; set; }            
        /// <summary>
        /// 部门名称
        /// </summary>
        public string DepartName { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsUsed { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public string IsUsedName { get; set; }
        /// <summary>
        /// 作业区域ID
        /// </summary>
        public string WorkAreaId { get; set; }
        /// <summary>
        /// 作业区域名称
        /// </summary>
        public string WorkAreaName { get; set; }
        /// <summary>
        /// 审核人ID
        /// </summary>
        public string AuditorId { get; set; }
        /// <summary>
        /// 审核人姓名
        /// </summary>
        public string AuditorName { get; set; }
        /// <summary>
        /// 审核时间
        /// </summary>
        public string AuditorDate { get; set; }

        /// <summary>
        /// 身份证 附件
        /// </summary>
        public string AttachUrl1
        {
            get;
            set;
        }
        /// <summary>
        /// 保险附件
        /// </summary>
        public string AttachUrl2
        {
            get;
            set;
        }
        /// <summary>
        /// 体检证明
        /// </summary>
        public string AttachUrl3
        {
            get;
            set;
        }
        /// <summary>
        /// 安全生产考核合格证书/特种作业操作证
        /// </summary>
        public string AttachUrl4
        {
            get;
            set;
        }
        /// <summary>
        /// 身份证背面照
        /// </summary>
        public string AttachUrl5
        {
            get;
            set;
        }
        /// <summary>
        /// 岗位类型
        /// </summary>
        public string PostType
        {
            get;
            set;
        }
        /// <summary>
        /// 岗位类型名称
        /// </summary>
        public string PostTypeName
        {
            get;
            set;
        }
        /// <summary>
        /// 是否外籍人员
        /// </summary>
        public bool? IsForeign
        {
            get;
            set;
        }
        /// <summary>
        /// 是否外聘人员
        /// </summary>
        public bool? IsOutside
        {
            get;
            set;
        }
        /// <summary>
        /// 文化程度
        /// </summary>
        public string EduLevel
        {
            get;
            set;
        }
        /// <summary>
        /// 文化程度
        /// </summary>
        public string EduLevelName
        {
            get;
            set;
        }
        /// <summary>
        /// 婚姻状况
        /// </summary>
        public string MaritalStatus
        {
            get;
            set;
        }
        /// <summary>
        /// 婚姻状况
        /// </summary>
        public string MaritalStatusName
        {
            get;
            set;
        }
        /// <summary>
        /// 证件类型
        /// </summary>
        public string IdcardType
        {
            get;
            set;
        }
        /// <summary>
        /// 证件类型
        /// </summary>
        public string IdcardTypeName
        {
            get;
            set;
        }
        /// <summary>
        /// 证件开始时间
        /// </summary>
        public string IdcardStartDate
        {
            get;
            set;
        }
        /// <summary>
        /// 证件结束时间
        /// </summary>
        public string IdcardEndDate
        {
            get;
            set;
        }
        /// <summary>
        /// 证件是否永久有效
        /// </summary>
        public string IdcardForever
        {
            get;
            set;
        }
        /// <summary>
        /// 证件是否永久有效
        /// </summary>
        public string IdcardForeverStr
        {
            get;
            set;
        }
        /// <summary>
        /// 政治面貌
        /// </summary>
        public string PoliticsStatus
        {
            get;
            set;
        }
        /// <summary>
        /// 政治面貌
        /// </summary>
        public string PoliticsStatusName
        {
            get;
            set;
        }
        /// <summary>
        /// 发证机关
        /// </summary>
        public string IdcardAddress
        {
            get;
            set;
        }
        /// <summary>
        /// 民族
        /// </summary>
        public string Nation
        {
            get;
            set;
        }
        /// <summary>
        /// 民族
        /// </summary>
        public string NationName
        {
            get;
            set;
        }
        /// <summary>
        /// 国家
        /// </summary>
        public string CountryCode
        {
            get;
            set;
        }
        /// <summary>
        /// 国家
        /// </summary>
        public string CountryName
        {
            get;
            set;
        }
        /// <summary>
        /// 省份
        /// </summary>
        public string ProvinceCode
        {
            get;
            set;
        }
        /// <summary>
        /// 省份
        /// </summary>
        public string ProvinceName
        {
            get;
            set;
        }
        /// <summary>
        /// 主专业
        /// </summary>
        public string MainCNProfessionalId
        {
            get;
            set;
        }
        /// <summary>
        /// 主专业
        /// </summary>
        public string MainCNProfessionalName
        {
            get;
            set;
        }
        /// <summary>
        /// 副专业
        /// </summary>
        public string ViceCNProfessionalId
        {
            get;
            set;
        }
        /// <summary>
        /// 副专业
        /// </summary>
        public string ViceCNProfessionalName
        {
            get;
            set;
        }
    }
}
