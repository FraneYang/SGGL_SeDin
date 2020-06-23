using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class PersonInOutItem
    {
        /// <summary>
        /// 人员进出场主键ID
        /// </summary>
        public string PersonInOutId
        {
            get;
            set;
        }
        /// <summary>
        /// 人员主键ID
        /// </summary>
        public string PersonId
        {
            get;
            set;
        }
        /// <summary>
        /// 项目ID
        /// </summary>
        public string ProjectId
        { get; set; }
        /// <summary>
        /// 单位ID
        /// </summary>
        public string UnitId
        { get; set; }
        /// <summary>
        /// 单位名称
        /// </summary>
        public string UnitName
        { get; set; }
        /// <summary>
        /// 人员姓名
        /// </summary>
        public string PersonName
        { get; set; }
        /// <summary>
        /// 进出
        /// </summary>
        public bool? IsIn
        { get; set; }
        /// <summary>
        /// 进出
        /// </summary>
        public string IsInName
        { get; set; }
        /// <summary>
        ///  进出时间
        /// </summary>
        public string ChangeTime
        { get; set; }
        /// <summary>
        ///  进出时间
        /// </summary>
        public DateTime? ChangeTimeD
        { get; set; }
        /// <summary>
        /// 班组ID
        /// </summary>
        public string TeamGroupId
        { get; set; }
        /// <summary>
        /// 班组名称
        /// </summary>
        public string TeamGroupName
        { get; set; }
        /// <summary>
        /// 岗位ID
        /// </summary>
        public string WorkPostId
        { get; set; }
        /// <summary>
        /// 岗位名称
        /// </summary>
        public string WorkPostName
        { get; set; }
    }
}
