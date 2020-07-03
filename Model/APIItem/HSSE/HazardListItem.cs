using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 危险源辨识评价项
    /// </summary>
    public class HazardListItem
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public string HazardListId
        {
            get;
            set;
        }

        /// <summary>
        /// 编号
        /// </summary>
        public string HazardListCode
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
        /// 版本号
        /// </summary>
        public string VersionNo
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
        /// 工作阶段id
        /// </summary>
        public string WorkStageIds
        {
            get;
            set;
        }
        /// <summary>
        /// 工作阶段名称
        /// </summary>
        public string WorkStageNames
        {
            get;
            set;
        }
        /// <summary>
        /// 辨识内容
        /// </summary>
        public string Contents
        {
            get;
            set;
        }
        /// <summary>
        /// 风险区域
        /// </summary>
        public string WorkAreaName
        {
            get;
            set;
        }
        /// <summary>
        /// 辨识时间
        /// </summary>
        public string IdentificationDate
        {
            get;
            set;
        }
        /// <summary>
        /// 控制人ID
        /// </summary>
        public string ControllingPersonId
        {
            get;
            set;
        }
        /// <summary>
        /// 控制人名称
        /// </summary>
        public string ControllingPersonName
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
