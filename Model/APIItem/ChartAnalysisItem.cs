using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 图型分析信息项
    /// </summary>
    public class ChartAnalysisItem
    {
        /// <summary>
        /// ID
        /// </summary>
        public string DataId
        {
            get;
            set;
        }
        /// <summary>
        ///  类型
        /// </summary>
        public string Type
        {
            get;
            set;
        }
        /// <summary>
        /// 名称
        /// </summary>
        public string DataName
        {
            get;
            set;
        }

        /// <summary>
        ///  总数
        /// </summary>
        public int DataSumCount
        {
            get;
            set;
        }

        /// <summary>
        /// 数量1
        /// </summary>
        public int DataCount1
        {
            get;
            set;
        }
    }
}
