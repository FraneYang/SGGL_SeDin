using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{ 
    public class Pro_PersonDataItem
    {
        /// <summary>
        /// 项目代号
        /// </summary>
        public string ProjectCode { get; set; }
        /// <summary>
        /// 工号
        /// </summary>
        public string UserCode { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        public string IdentityCard { get; set; }
    }

    public class Pro_Person
    {
        /// <summary>
        /// 
        /// </summary>
        public List<Pro_PersonDataItem> data { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Message { get; set; }
    }

}
