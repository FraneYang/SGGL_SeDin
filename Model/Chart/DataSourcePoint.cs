using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 数据集合
    /// </summary>
    public class DataSourcePoint
    {
        private string pointText;
        /// <summary>
        /// 数据点x值
        /// </summary>
        public string PointText
        {
            get { return pointText; }
            set { pointText = value; }
        }

        private string pointValue;
        /// <summary>
        /// 数据点y值
        /// </summary>
        public string PointValue
        {
            get { return pointValue; }
            set { pointValue = value; }
        }
    }
}
