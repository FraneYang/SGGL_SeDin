using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{ 
    public class PersonDataItem
    {
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

        /// <summary>
        /// 
        /// </summary>
        public string Image { get; set; }
    }

    public class Person
    {
        /// <summary>
        /// 
        /// </summary>
        public List<PersonDataItem> data { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Message { get; set; }
    
    }

}
