using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// 作业票项
    /// </summary>
    public class LicenseDataItem
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public string LicenseId
        {
            get;
            set;
        }
        /// <summary>
        /// 菜单ID
        /// </summary>
        public string MenuId
        {
            get;
            set;
        }
        /// <summary>
        /// 菜单名称
        /// </summary>
        public string MenuName
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
        /// 编号
        /// </summary>
        public string LicenseCode
        {
            get;
            set;
        }
        /// <summary>
        /// 申请单位ID
        /// </summary>
        public string ApplyUnitId
        {
            get;
            set;
        }
        /// <summary>
        /// 申请单位名称
        /// </summary>
        public string ApplyUnitName
        {
            get;
            set;
        }
        /// <summary>
        /// 申请人ID
        /// </summary>
        public string ApplyManId
        {
            get;
            set;
        }
        /// <summary>
        /// 申请人名称
        /// </summary>
        public string ApplyManName
        {
            get;
            set;
        }
        /// <summary>
        /// 申请日期
        /// </summary>
        public string ApplyDate
        {
            get;
            set;
        }
        /// <summary>
        /// 作业地点
        /// </summary>
        public string WorkPalce
        {
            get;
            set;
        }
        /// <summary>
        /// 监火人ID（动火、受限、射线）
        /// </summary>
        public string FireWatchManId
        {
            get;
            set;
        }
        /// <summary>
        /// 监火人名称（动火、受限射线）
        /// </summary>
        public string FireWatchManName
        {
            get;
            set;
        }
        /// <summary>
        /// 有效期开始时间
        /// </summary>
        public string ValidityStartTime
        {
            get;
            set;
        }
        /// <summary>
        /// 有效期结束时间
        /// </summary>
        public string ValidityEndTime
        {
            get;
            set;
        }
        /// <summary>
        /// 【动火-作业内容、措施；受限-作业内容；射线：安全距离及受影响区域/单位；】
        /// 【断路：作业内容；动土：作业内容、机具及安全措施】
        /// </summary>
        public string WorkMeasures
        {
            get;
            set;
        }
        /// <summary>
        /// 取消人ID
        /// </summary>
        public string CancelManId
        {
            get;
            set;
        }
        /// <summary>
        /// 取消人姓名
        /// </summary>
        public string CancelManName
        {
            get;
            set;
        }
        /// <summary>
        /// 取消原因
        /// </summary>
        public string CancelReasons
        {
            get;
            set;
        }
        /// <summary>
        /// 取消时间
        /// </summary>
        public string CancelTime
        {
            get;
            set;
        }
        /// <summary>
        /// 关闭人ID
        /// </summary>
        public string CloseManId
        {
            get;
            set;
        }
        /// <summary>
        /// 关闭人姓名
        /// </summary>
        public string CloseManName
        {
            get;
            set;
        }
        /// <summary>
        /// 关闭原因
        /// </summary>
        public string CloseReasons
        {
            get;
            set;
        }
        /// <summary>
        /// 关闭时间
        /// </summary>
        public string CloseTime
        {
            get;
            set;
        }
        /// <summary>
        /// 下步办理人ID
        /// </summary>
        public string NextManId
        {
            get;
            set;
        }
        /// <summary>
        /// 下步办理人姓名
        /// </summary>
        public string NextManName
        {
            get;
            set;
        }
        /// <summary>
        /// 状态(0-待提交；1-审核中；2-作业中；3-已关闭；-1已取消)
        /// </summary>
        public string States
        {
            get;
            set;
        }
        /// <summary>
        /// 附件路径
        /// </summary>
        public string AttachUrl
        {
            get;
            set;
        }
        /// <summary>
        /// 安全措施明细
        /// </summary>
        public List<LicenseItem> LicenseItems
        {
            get;
            set;
        }
        /// <summary>
        /// 作业类别（高处）
        /// </summary>
        public string WorkType
        {
            get;
            set;
        }
        /// <summary>
        /// 设备及工具（高处）
        /// </summary>
        public string EquipmentTools
        {
            get;
            set;
        }
        /// <summary>
        /// 射线类型（χ射线/γ射线）（射线）
        /// </summary>
        public string RadialType
        {
            get;
            set;
        }
        /// <summary>
        /// 作业负责人ID（射线、夜间）
        /// </summary>
        public string WorkLeaderId
        {
            get;
            set;
        }
        /// <summary>
        /// 作业负责人名字（射线、夜间）
        /// </summary>
        public string WorkLeaderName
        {
            get;
            set;
        }
        /// <summary>
        /// 作业负责人电话（射线、夜间）
        /// </summary>
        public string WorkLeaderTel
        {
            get;
            set;
        }
        /// <summary>
        /// 安全作业负责人ID（夜间）
        /// </summary>
        public string SafeLeaderId
        {
            get;
            set;
        }
        /// <summary>
        /// 安全作业负责人名字（夜间）
        /// </summary>
        public string SafeLeaderName
        {
            get;
            set;
        }
        /// <summary>
        /// 安全作业负责人电话（夜间）
        /// </summary>
        public string SafeLeaderTel
        {
            get;
            set;
        }
        /// <summary>
        /// 监护人联系方式（射线）
        /// </summary>
        public string WatchManContact
        {
            get;
            set;
        }
        /// <summary>
        /// 占用道路名称（断路）
        /// </summary>
        public string RoadName
        {
            get;
            set;
        }
        /// <summary>
        /// 安全措施及使用的工机具说明（断路）
        /// </summary>
        public string SafeMeasures
        {
            get;
            set;
        }
        /// <summary>
        /// 开挖深度（动土）
        /// </summary>
        public string WorkDepth
        {
            get;
            set;
        }
        /// <summary>
        /// 级别（吊装）
        /// </summary>
        public string WorkLevel
        {
            get;
            set;
        }
        /// <summary>
        /// 起重机能力及索具规格（吊装）
        /// </summary>
        public string CraneCapacity
        {
            get;
            set;
        }

        /// <summary>
        /// 区域（定稿）
        /// </summary>
        public string WorkAreaIds
        {
            get;
            set;
        }
        /// <summary>
        /// 类型ID（定稿）
        /// </summary>
        public string LicenseTypeId
        {
            get;
            set;
        }
        /// <summary>
        /// 原作业票ID
        /// </summary>
        public string OldLicenseId
        {
            get;
            set;
        }
    }
}
