using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class CheckItem
    {
        /// <summary>
        /// 部门
        /// </summary>
        public string Depart
        {
            get;
            set;
        }

        /// <summary>
        /// 本月发出整改项
        /// </summary>
        public string ThisRectifyNum
        {
            get;
            set;
        }

        /// <summary>
        /// 本月关闭整改项
        /// </summary>
        public string ThisOKRectifyNum
        {
            get;
            set;
        }

        /// <summary>
        /// 累计发出整改项
        /// </summary>
        public string TotalRectifyNum
        {
            get;
            set;
        }

        /// <summary>
        /// 累计关闭整改项
        /// </summary>
        public string TotalOKRectifyNum
        {
            get;
            set;
        }
    }
}
