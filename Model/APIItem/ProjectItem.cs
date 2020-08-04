using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class ProjectItem
    {
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
        public string ProjectCode
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
        /// 开工时间
        /// </summary>
        public DateTime? StartDate
        {
            get;
            set;
        }
        /// <summary>
        /// 完工时间
        /// </summary>
        public DateTime? EndDate
        {
            get;
            set;
        }
        /// <summary>
        /// 地址
        /// </summary>
        public string ProjectAddress
        {
            get;
            set;
        }
        /// <summary>
        /// 项目简称
        /// </summary>
        public string ShortName
        {
            get;
            set;
        }
        /// <summary>
        /// 项目类型
        /// </summary>
        public string ProjectType
        {
            get;
            set;
        }
        /// <summary>
        /// 项目状态
        /// </summary>
        public string ProjectState
        {
            get;
            set;
        }
        /// <summary>
        /// 项目所属单位ID
        /// </summary>
        public string UnitId
        {
            get;
            set;
        }
        /// <summary>
        /// 项目坐标
        /// </summary>
        public string MapCoordinates
        {
            get;
            set;
        }
    }
}
