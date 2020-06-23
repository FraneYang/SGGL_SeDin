using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.DataVisualization.Charting;

namespace Model
{
    /// <summary>
    /// chart类
    /// </summary>
    public class DataSourceChart
    {
        private int width;
        /// <summary>
        /// 宽度
        /// </summary>
        public int Width
        {
            get { return width; }
            set { width = value; }
        }

        private int height;
        /// <summary>
        /// 高度
        /// </summary>
        public int Height
        {
            get { return height; }
            set { height = value; }
        }

        private string title;
        /// <summary>
        /// 标题名
        /// </summary>
        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        private bool isNotEnable3D;
        /// <summary>
        /// 是否显示三维效果
        /// </summary>
        public bool IsNotEnable3D
        {
            get { return isNotEnable3D; }
            set { isNotEnable3D = value; }
        }

        private SeriesChartType chartType;
        /// <summary>
        /// 图形类型
        /// </summary>
        public SeriesChartType ChartType
        {
            get { return chartType; }
            set { chartType = value; }
        }

        private List<DataSourceTeam> dataSourceTeams;
        /// <summary>
        /// 数据点集合
        /// </summary>
        public List<DataSourceTeam> DataSourceTeams
        {
            get { return dataSourceTeams; }
            set { dataSourceTeams = value; }
        }
    }
}
