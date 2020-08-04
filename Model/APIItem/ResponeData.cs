using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class ResponeData
    {
        /// <summary>
        /// 代码 1-正常；0-异常；
        /// </summary>
        public int code
        {
            get;
            set;
        } = 1;

       /// <summary>
       /// 消息
       /// </summary>
       public string message
        {
           get;
           set;
       }

       /// <summary>
       /// 数据
       /// </summary>
       public object data
        {
           get;
           set;
       }
    }
}
