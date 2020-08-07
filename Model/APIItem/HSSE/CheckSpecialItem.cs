using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 专项检查信息项
    /// </summary>
    public class CheckSpecialItem
    {
        /// <summary>
        /// ID
        /// </summary>
        public string CheckSpecialId
        {
            get;
            set;
        }
        /// <summary>
        /// 编号
        /// </summary>
        public string CheckSpecialCode
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
        /// 检查类型0-周检；1-月检；2-其他
        /// </summary>
        public string CheckType
        {
            get;
            set;
        }
        /// <summary>
        /// 检查类型0-周检；1-月检；2-其他
        /// </summary>
        public string CheckTypeName
        {
            get;
            set;
        }
        /// <summary>
        /// 检查项目
        /// </summary>
        public string CheckItemSetId
        {
            get;
            set;
        }
        /// <summary>
        /// 检查项目名称
        /// </summary>
        public string CheckItemSetName
        {
            get;
            set;
        }
        /// <summary>
        /// 检查组长Id
        /// </summary>
        public string CheckPersonId
        {
            get;
            set;
        }
        /// <summary>
        /// 检查组长姓名
        /// </summary>
        public string CheckPersonName
        {
            get;
            set;
        }
        /// <summary>
        /// 检查日期
        /// </summary>
        public string CheckTime
        {
            get;
            set;
        }
        /// <summary>
        /// 其他情况日小结
        /// </summary>
        public string DaySummary
        {
            get;
            set;
        }
        /// <summary>
        /// 参与单位IDS
        /// </summary>
        public string PartInUnitIds
        {
            get;
            set;
        }
        /// <summary>
        /// 参与单位名称S
        /// </summary>
        public string PartInUnitNames
        {
            get;
            set;
        }
        /// <summary>
        /// 组成员ID
        /// </summary>
        public string PartInPersonIds
        {
            get;
            set;
        }
        /// <summary>
        /// 组成员姓名【PartInPersons】
        /// </summary>
        public string PartInPersonNames
        {
            get;
            set;
        }

        /// <summary>
        /// 参与人员姓名【PartInPersonNames】
        /// </summary>
        public string PartInPersonNames2
        {
            get;
            set;
        }
        /// <summary>
        /// 整理人ID
        /// </summary>
        public string CompileManId
        {
            get;
            set;
        }
        /// <summary>
        /// 整理人姓名
        /// </summary>
        public string CompileManName
        {
            get;
            set;
        }
        /// <summary>
        /// 附件
        /// </summary>
        public string AttachUrl1
        {
            get;
            set;
        }
        /// <summary>
        ///  状态
        /// </summary>
        public string States
        {
            get;
            set;
        }
        /// <summary>
        ///  专项检查明细项
        /// </summary>
        public List<CheckSpecialDetailItem> CheckSpecialDetailItems
        {
            get;
            set;
        }
    }
}
