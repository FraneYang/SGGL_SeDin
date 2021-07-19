using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class BatchItem
    {
        /// <summary>
        /// 批ID
        /// </summary>
        public string PointBatchId
        {
            get;
            set;
        }

        /// <summary>
        /// 检测单位Id
        /// </summary>
        public string NDEUnit
        {
            get;
            set;
        }
    }
}
