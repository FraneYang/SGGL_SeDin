using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ItemEndCheck
    {
        /// <summary>
        /// ID
        /// </summary>
        public string ItemCheckId
        {
            get;
            set;
        }
        /// <summary>
        /// 管线Id
        /// </summary>
        public string PipelineId
        {
            get;
            set;
        }
        /// <summary>
        /// 尾项内容
        /// </summary>
        public string Content
        {
            get;
            set;
        }
        /// <summary>
        /// 检查类别
        /// </summary>
        public string ItemType
        {
            get;
            set;
        }
        /// <summary>
        /// 消项结果确认
        /// </summary>
        public string Result
        {
            get;
            set;
        }
        /// <summary>
        /// 主表Id
        /// </summary>
        public string ItemEndCheckListId
        {
            get;
            set;
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            get;
            set;
        }
    }
}
