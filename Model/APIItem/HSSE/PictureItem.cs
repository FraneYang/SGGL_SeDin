using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 项目图片项
    /// </summary>
    public class PictureItem
    {
        /// <summary>
        /// ID
        /// </summary>
        public string PictureId
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
        /// 标题
        /// </summary>
        public string Title
        {
            get;
            set;
        }
        /// <summary>
         /// 内容
         /// </summary>
        public string ContentDef
        {
            get;
            set;
        }

        /// <summary>
        /// 图片类型ID
        /// </summary>
        public string PictureTypeId
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
        /// <summary>
        /// 编制人ID
        /// </summary>
        public string CompileManId
        {
            get;
            set;
        }
        /// <summary>
        /// 附件url
        /// </summary>
        public string AttachUrl
        {
            get;
            set;
        }
    }
}
