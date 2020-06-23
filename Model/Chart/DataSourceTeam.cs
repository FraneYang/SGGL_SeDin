using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 数据点类
    /// </summary>
    public class DataSourceTeam
    {
        private string dataPointName;
        /// <summary>
        /// 数据点名称
        /// </summary>
        public string DataPointName
        {
            get { return dataPointName; }
            set { dataPointName = value; }
        }

        private List<DataSourcePoint> dataSourcePoints;
        /// <summary>
        /// 数据集合
        /// </summary>
        public List<DataSourcePoint> DataSourcePoints
        {
            get { return dataSourcePoints; }
            set { dataSourcePoints = value; }
        }
    }
}
