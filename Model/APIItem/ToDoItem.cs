using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class ToDoItem
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public string DataId
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
        /// 菜单名称
        /// </summary>
        public string MenuName
        {
            get;
            set;
        }
        /// <summary>
        /// 内容
        /// </summary>
        public string Content
        {
            get;
            set;
        }
        /// <summary>
        /// 办理人ID
        /// </summary>
        public string UserId
        {
            get;
            set;
        }
        /// <summary>
        /// 办理人姓名
        /// </summary>
        public string UserName
        {
            get;
            set;
        }
        /// <summary>
        /// 时间
        /// </summary>
        public DateTime? DataTime
        {
            get;
            set;
        }
        /// <summary>
        /// 时间字符串
        /// </summary>
        public string DataTimeStr
        {
            get;
            set;
        }
        /// <summary>
        /// 路径
        /// </summary>
        public string UrlStr
        {
            get;
            set;
        }

        /// <summary>
        /// IsInsert 1:插入；2：更新
        /// </summary>
        public string IsInsert
        {
            get;
            set;
        }
    }
}
