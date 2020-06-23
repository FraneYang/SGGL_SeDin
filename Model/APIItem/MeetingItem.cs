using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 会议数据项
    /// </summary>
    public class MeetingItem
    {
        /// <summary>
        /// 会议ID
        /// </summary>
        public string MeetingId
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
        /// 班组ID
        /// </summary>
        public string TeamGroupId
        {
            get;
            set;
        }
        /// <summary>
        /// 班组名称
        /// </summary>
        public string TeamGroupName
        {
            get;
            set;
        }
        /// <summary>
        /// 编号
        /// </summary>
        public string MeetingCode
        {
            get;
            set;
        }
        /// <summary>
        /// 会议名称
        /// </summary>
        public string MeetingName
        {
            get;
            set;
        }
        /// <summary>
        /// 会议日期
        /// </summary>
        public string MeetingDate
        {
            get;
            set;
        }       
        /// <summary>
        /// 主要内容
        /// </summary>
        public string MeetingContents
        {
            get;
            set;
        }
        /// <summary>
        /// 会议地点
        /// </summary>
        public string MeetingPlace
        {
            get;
            set;
        }
        /// <summary>
        /// 会议类型(C-班前会；W-周例会；M-例会；S-专题例会；A-其他会议)
        /// </summary>
        public string MeetingType
        {
            get;
            set;
        }
        /// <summary>
        /// 会议时长
        /// </summary>
        public decimal MeetingHours
        {
            get;
            set;
        }
        /// <summary>
        /// 会议主持人
        /// </summary>
        public string MeetingHostMan
        {
            get;
            set;
        }        
        /// <summary>
        /// 参会人员
        /// </summary>
        public string AttentPerson
        {
            get;
            set;
        }
        /// <summary>
        /// 会议主持人id
        /// </summary>
        public string MeetingHostManId
        {
            get;
            set;
        }
        /// <summary>
        /// 会议主持人姓名
        /// </summary>
        public string MeetingHostManName
        {
            get;
            set;
        }
        /// <summary>
        /// 参会人员IDs
        /// </summary>
        public string AttentPersonIds
        {
            get;
            set;
        }
        /// <summary>
        /// 参会人员NAMEs
        /// </summary>
        public string AttentPersonNames
        {
            get;
            set;
        }
        /// <summary>
        /// 参会人数
        /// </summary>
        public int AttentPersonNum
        {
            get;
            set;
        }
        /// <summary>
        /// 编制时间
        /// </summary>
        public string CompileDate
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
        /// 编制姓名
        /// </summary>
        public string CompileManName
        {
            get;
            set;
        }
        /// <summary>
        /// 附件
        /// </summary>
        public string AttachUrl
        {
            get;
            set;
        }
        /// <summary>
        /// 签到附件
        /// </summary>
        public string AttachUrl1
        {
            get;
            set;
        }
        /// <summary>
        /// 过程附件
        /// </summary>
        public string AttachUrl2
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
