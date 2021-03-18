using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 安全合规类
    /// </summary>
    public class SafeLawItem
    {
        /// <summary>
        /// ID
        /// </summary>
        public string ID
        {
            get;
            set;
        }
        /// <summary>
        /// DataType 类型
        /// </summary>
        public string DataType
        {
            get;
            set;
        }
        /// <summary>
        /// DataType 类型
        /// </summary>
        public string DataTypeName
        {
            get;
            set;
        }
        /// <summary>
        /// 发布状态ID
        /// </summary>
        public string ReleaseStates
        {
            get;
            set;
        }
   /// <summary>
        /// 发布状态名称
        /// </summary>
        public string ReleaseStatesName
        {
            get;
            set;
        }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// 编号
        /// </summary>
        public string Code
        {
            get;
            set;
        }
        /// <summary>
        /// 类型Id
        /// </summary>
        public string TypeId
        {
            get;
            set;
        }
        /// <summary>
        /// 类型名称
        /// </summary>
        public string TypeName
        {
            get;
            set;
        }
        /// <summary>
        /// 发布机构
        /// </summary>
        public string ReleaseUnit
        {
            get;
            set;
        }
        /// <summary>
        /// 发布日期
        /// </summary>
        public string ApprovalDateStr
        {
            get;
            set;
        }
        /// <summary>
        /// 发布日期
        /// </summary>
        public DateTime? ApprovalDate
        {
            get;
            set;
        }
        /// <summary>
        /// 生效日期
        /// </summary>
        public string EffectiveDateStr
        {
            get;
            set;
        }
        /// <summary>
        /// 生效日期
        /// </summary>
        public DateTime? EffectiveDate
        {
            get;
            set;
        }
        /// <summary>
        /// 废止日期
        /// </summary>
        public string AbolitionDateStr
        {
            get;
            set;
        }
        /// <summary>
        /// 废止日期
        /// </summary>
        public DateTime? AbolitionDate
        {
            get;
            set;
        }
        /// <summary>
        /// 替换内容
        /// </summary>
        public string ReplaceInfo
        {
            get;
            set;
        }
        /// <summary>
        /// 内容简介及重点关注条款
        /// </summary>
        public string Description
        {
            get;
            set;
        }
        /// <summary>
        /// 索引ID
        /// </summary>
        public string IndexesIds
        {
            get;
            set;
        }
        /// <summary>
        /// 索引名称
        /// </summary>
        public string IndexesNames
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
        /// 上传人
        /// </summary>
        public string CompileManName
        {
            get;
            set;
        }
        /// <summary>
        /// 上传时间
        /// </summary>
        public string CompileDateStr
        {
            get;
            set;
        }
        /// <summary>
        /// 上传时间
        /// </summary>
        public DateTime? CompileDate
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
    }
}
