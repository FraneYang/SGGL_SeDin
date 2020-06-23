using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class ErrorInfo
    {
        /// <summary>
        /// 错误行号
        /// </summary>
        public string Row
        {
            get;
            set;
        }

        /// <summary>
        /// 错误列
        /// </summary>
        public string Column
        {
            get;
            set;
        }

        /// <summary>
        /// 错误类型
        /// </summary>
        public string Reason
        {
            get;
            set;
        }
    }
}
