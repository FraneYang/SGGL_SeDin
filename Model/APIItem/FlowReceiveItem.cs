using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 待办信息接收(不审核)项
    /// </summary>
    public class FlowReceiveItem
    {
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
        /// 接收人ID
        /// </summary>
        public string OperaterId
        {
            get;
            set;
        }
        /// <summary>
        /// 接收人姓名
        /// </summary>
        public string OperaterName
        {
            get;
            set;
        }
        /// <summary>
        /// 接收时间
        /// </summary>
        public string OperaterTime
        {
            get;
            set;
        }
    }
}
