using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class UserReadItem
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public string UserReadId
        {
            get;
            set;
        }
        /// <summary>
        /// 用户ID
        /// </summary>
        public string UserId
        {
            get;
            set;
        }
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
        /// 数据ID
        /// </summary>
        public string DataId
        {
            get;
            set;
        }
        /// <summary>
        /// 时间
        /// </summary>
        public string ReadTime
        {
            get;
            set;
        }
    }
}
