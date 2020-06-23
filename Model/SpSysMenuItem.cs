using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 菜单项
    /// </summary>
    public class SpSysMenuItem
    {
        /// <summary>
        /// 系统ID
        /// </summary>
        public string MenuId
        {
            get;
            set;
        }
        /// <summary>
        /// 系统名称
        /// </summary>
        public string MenuName
        {
            get;
            set;
        }
        /// <summary>
        /// 路径
        /// </summary>
        public string Url
        {
            get;
            set;
        }
        
        /// <summary>
        /// 菜单图标
        /// </summary>
        public string Icon
        {
            get;
            set;
        }

        /// <summary>
        /// 排列序号
        /// </summary>
        public int? SortIndex
        {
            get;
            set;
        }
        /// <summary>
        /// 上级菜单
        /// </summary>
        public string SuperMenu
        {
            get;
            set;
        }
        /// <summary>
        /// 菜单类别
        /// </summary>
        public string MenuType
        {
            get;
            set;
        }
        /// <summary>
        /// 是否末级
        /// </summary>
        public bool? IsEnd
        {
            get;
            set;
        }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool? IsUsed
        {
            get;
            set;
        }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? ModifyDate
        {
            get;
            set;
        }
    }
}
