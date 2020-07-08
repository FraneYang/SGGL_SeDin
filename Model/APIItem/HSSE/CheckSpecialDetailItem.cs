using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 专项检查详细信息项
    /// </summary>
    public class CheckSpecialDetailItem
    {
        /// <summary>
        /// ID
        /// </summary>
        public string CheckSpecialDetailId
        {
            get;
            set;
        }
        /// <summary>
        /// 检查ID
        /// </summary>
        public string CheckSpecialId
        {
            get;
            set;
        }
        /// <summary>
        /// 检查类型ID【CheckItem】
        /// </summary>
        public string CheckItemSetId
        {
            get;
            set;
        }

        /// <summary>
        /// 检查类型名称
        /// </summary>
        public string CheckItemSetName
        {
            get;
            set;
        }
        /// <summary>
        /// 序号
        /// </summary>
        public int? SortIndex
        {
            get;
            set;
        }
        /// <summary>
        /// 检查内容项
        /// </summary>
        public string CheckContent
        {
            get;
            set;
        }
        /// <summary>
        /// 不符合项
        /// </summary>
        public string Unqualified
        {
            get;
            set;
        }
        /// <summary>
        /// 整改要求
        /// </summary>
        public string Suggestions
        {
            get;
            set;
        }
        /// <summary>
        /// 检查区域
        /// </summary>
        public string WorkAreaId
        {
            get;
            set;
        }
        /// <summary>
        /// 检查区域
        /// </summary>
        public string WorkArea
        {
            get;
            set;
        }
        /// <summary>
        /// 责任单位ID
        /// </summary>
        public string UnitId
        {
            get;
            set;
        }
        /// <summary>
        /// 责任单位名称
        /// </summary>
        public string UnitName
        {
            get;
            set;
        }
        /// <summary>
        /// 处理措施Id
        /// </summary>
        public string HandleStep
        {
            get;
            set;
        }
        /// <summary>
        /// 处理措施名称
        /// </summary>
        public string HandleStepName
        {
            get;
            set;
        }
        /// <summary>
        /// 限制时间
        /// </summary>
        public string LimitedDate
        {
            get;
            set;
        }
        /// <summary>
        /// 整改情况
        /// </summary>
        public bool? CompleteStatus
        {
            get;
            set;
        }
        /// <summary>
        /// 整改情况
        /// </summary>
        public string CompleteStatusName
        {
            get;
            set;
        }
        /// <summary>
        /// 完成时间
        /// </summary>
        public string CompletedDate
        {
            get;
            set;
        }
        /// <summary>
        /// 整改通知单ID
        /// </summary>
        public string RectifyNoticeId
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
    }
}
