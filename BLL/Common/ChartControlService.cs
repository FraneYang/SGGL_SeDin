namespace BLL
{
    using System.Collections.Generic;
    using System.Data;
    using System.Web.UI.DataVisualization.Charting;

    /// <summary>
    /// 自定义图形通用类
    /// </summary>
    public static class ChartControlService
    {
        #region 给chart类赋值
        /// <summary>
        /// 给chart类赋值
        /// </summary>
        /// <param name="dt">数据源表值</param>
        /// <param name="title">图标题</param>
        /// <param name="type">图类型</param>
        /// <param name="width">图显示宽度</param>
        /// <param name="height">图显示高度</param>
        /// <param name="isNotEnable3D">是否显示3D效果</param>
        /// <returns>返回图</returns>
        public static Model.DataSourceChart GetDataSourceChart(DataTable dt, string title, string type, int width, int height, bool isNotEnable3D)
        {
            Model.DataSourceChart dataSourceChart = new Model.DataSourceChart
            {
                Width = width,
                Height = height,
                Title = title,
                IsNotEnable3D = isNotEnable3D,
                ChartType = GetChartType(type)
            };
            List<Model.DataSourceTeam> dataSourceTeams = new List<Model.DataSourceTeam>();
            for (int i = 1; i < dt.Columns.Count; i++)
            {
                Model.DataSourceTeam dataSourceTeam = new Model.DataSourceTeam
                {
                    DataPointName = dt.Columns[i].ToString()
                };
                List<Model.DataSourcePoint> dataSourcePoints = new List<Model.DataSourcePoint>();
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    Model.DataSourcePoint dataSourcePoint = new Model.DataSourcePoint
                    {
                        PointText = dt.Rows[j][0].ToString(),
                        PointValue = dt.Rows[j][i].ToString()
                    };
                    dataSourcePoints.Add(dataSourcePoint);
                }
                dataSourceTeam.DataSourcePoints = dataSourcePoints;
                dataSourceTeams.Add(dataSourceTeam);
            }
            dataSourceChart.DataSourceTeams = dataSourceTeams;
            return dataSourceChart;
        }

        /// <summary>
        /// 显示类型
        /// </summary>
        /// <param name="chartType"></param>
        /// <returns></returns>
        public static SeriesChartType GetChartType(string chartType)
        {
            SeriesChartType chart = SeriesChartType.Column;
            if (chartType == "Column")
            {
                chart = SeriesChartType.Column;
            }
            else if (chartType == "Line")
            {
                chart = SeriesChartType.Line;
            }
            else if (chartType == "StackedArea")
            {
                chart = SeriesChartType.StackedArea;
            }
            else if (chartType == "StackedArea100")
            {
                chart = SeriesChartType.StackedArea100;
            }
            else if (chartType == "StepLine")
            {
                chart = SeriesChartType.StepLine;
            }
            else if (chartType == "Spline")
            {
                chart = SeriesChartType.Spline;
            }
            else if (chartType == "Stock")
            {
                chart = SeriesChartType.Stock;
            }
            else if (chartType == "Candlestick")
            {
                chart = SeriesChartType.Candlestick;
            }
            else if (chartType == "Pie")
            {
                chart = SeriesChartType.Pie;
            }
            else if (chartType == "Polar")
            {
                chart = SeriesChartType.Polar;
            }
            else if (chartType == "ErrorBar")
            {
                chart = SeriesChartType.ErrorBar;
            }
            return chart;
        }
        #endregion
    }
}
