using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class ConstructSolutionItem
    {
       /// <summary>
       /// 方案ID
       /// </summary>
       public string ConstructSolutionId
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
        public string ConstructSolutionCode
        {
            get;
            set;
        }
        /// <summary>
        /// 标题
        /// </summary>
        public string ConstructSolutionName
        {
            get;
            set;
        }
        /// <summary>
        /// 版本号
        /// </summary>
        public string VersionNo
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
        /// 审查类型
        /// </summary>
        public string InvestigateType
        {
            get;
            set;
        }
        /// <summary>
        /// 审查类型名称
        /// </summary>
        public string InvestigateTypeName
        {
            get;
            set;
        }
        /// <summary>
        /// 方案类型ID
        /// </summary>
        public string SolutinTypeId
        {
           get;
           set;
       }
        /// <summary>
        /// 方案类型
        /// </summary>
        public string SolutinTypeName
        {
           get;
           set;
       }
        /// <summary>
        /// 内容
        /// </summary>
        public string FileContents
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
        /// 编制人
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
        /// 状态
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
    }
}
