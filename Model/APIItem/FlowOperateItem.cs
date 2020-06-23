using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 审核信息项
    /// </summary>
    public class FlowOperateItem
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public string FlowOperateId
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
        /// 项目ID
        /// </summary>
        public string ProjectId
        {
            get;
            set;
        }
        /// <summary>
        /// 单据ID
        /// </summary>
        public string DataId
        {
            get;
            set;
        }
        /// <summary>
        /// 步骤名称
        /// </summary>
        public string AuditFlowName
        {
            get;
            set;
        }
        /// <summary>
        /// 审核步骤序号
        /// </summary>
        public int SortIndex
        {
            get;
            set;
        }
        /// <summary>
        /// 组号
        /// </summary>
        public int GroupNum
        {
            get;
            set;
        }
        /// <summary>
        /// 组内序号
        /// </summary>
        public int OrderNum
        {
            get;
            set;
        }
        /// <summary>
        /// 审核角色IDs
        /// </summary>
        public string RoleIds
        {
            get;
            set;
        }
        /// <summary>
        /// 审核角色名称s
        /// </summary>
        public string RoleNames
        {
            get;
            set;
        }
        /// <summary>
        /// 审核人ID
        /// </summary>
        public string OperaterId
        {
            get;
            set;
        }
        /// <summary>
        /// 审核人姓名
        /// </summary>
        public string OperaterName
        {
            get;
            set;
        }
        /// <summary>
        /// 审核时间
        /// </summary>
        public string OperaterTime
        {
            get;
            set;
        }
        /// <summary>
        /// 审核时间
        /// </summary>
        public DateTime? OperaterTimeD
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
        /// 审核意见
        /// </summary>
        public string Opinion
        {
            get;
            set;
        }
        /// <summary>
        /// 流程是否结束
        /// </summary>
        public bool IsFlowEnd
        {
            get;
            set;
        }
        /// <summary>
        /// 审核是否关闭
        /// </summary>
        public bool IsClosed
        {
            get;
            set;
        }
        /// <summary>
        /// 下一步审核人ID
        /// </summary>
        public string NextOperaterId
        {
            get;
            set;
        }
        /// <summary>
        /// 签名
        /// </summary>
        public string SignatureUrl
        {
            get;
            set;
        }
    }
}
