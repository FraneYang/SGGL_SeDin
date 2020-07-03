using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.DataVisualization.Charting;
using System.Drawing;

namespace Web.Controls
{
    public partial class ChartControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
            }
        }

        /// <summary>
        /// 创建Chart图形
        /// </summary>
        /// <param name="dataSourceChart">Chart类</param>
        public void CreateChart(Model.DataSourceChart dataSourceChart)
        {
            Chart chart1 = new Chart
            {
                ID = "chart1",
                BackColor = Color.WhiteSmoke,
                ImageLocation = "~/Images/ChartPic_#SEQ(300,3)",
                BorderlineDashStyle = ChartDashStyle.Solid,
                Palette = ChartColorPalette.BrightPastel,
                BackSecondaryColor = Color.White,
                BackGradientStyle = GradientStyle.TopBottom,
                BorderWidth = 2,
                BorderColor = Color.FromArgb(26, 59, 105),
                ImageType = ChartImageType.Png,

                Width = dataSourceChart.Width,
                Height = dataSourceChart.Height
            };

            Title title = new Title
            {
                Text = dataSourceChart.Title,
                ShadowColor = Color.FromArgb(32, 0, 0, 0),
                Font = new Font("Trebuchet MS", 10F, FontStyle.Bold),
                ShadowOffset = 3,
                ForeColor = Color.FromArgb(26, 59, 105)
            };
            chart1.Titles.Add(title);

            Legend legend = new Legend
            {
                Name = dataSourceChart.Title,
                TextWrapThreshold = 1,
                Docking = Docking.Top,
                Alignment = StringAlignment.Center,
                BackColor = Color.Transparent,
                Font = new Font(new FontFamily("Trebuchet MS"), 8),
                LegendStyle = LegendStyle.Row,
                IsEquallySpacedItems = true,
                IsTextAutoFit = false
            };
            chart1.Legends.Add(legend);

            ChartArea chartArea = new ChartArea
            {
                Name = dataSourceChart.Title,
                BackColor = Color.Transparent
            };
            chartArea.AxisX.IsLabelAutoFit = false;
            chartArea.AxisY.IsLabelAutoFit = false;
            chartArea.AxisX.LabelStyle.Font = new Font("Verdana,Arial,Helvetica,sans-serif", 8F, FontStyle.Regular);
            chartArea.AxisY.LabelStyle.Font = new Font("Verdana,Arial,Helvetica,sans-serif", 8F, FontStyle.Regular);
            chartArea.AxisY.LineColor = Color.FromArgb(64, 64, 64, 64);
            chartArea.AxisX.LineColor = Color.FromArgb(64, 64, 64, 64);
            chartArea.AxisY.MajorGrid.LineColor = Color.FromArgb(64, 64, 64, 64);
            chartArea.AxisX.MajorGrid.LineColor = Color.FromArgb(64, 64, 64, 64);
            chartArea.AxisX.Interval = 1;
            chartArea.Area3DStyle.Enable3D = dataSourceChart.IsNotEnable3D;
            chart1.ChartAreas.Add(chartArea);

            if (dataSourceChart.ChartType == SeriesChartType.Pie)
            {
                foreach (Model.DataSourceTeam dataSourceTeam in dataSourceChart.DataSourceTeams)
                {
                    this.lblTotal.Text = "累计值为：";
                    if (dataSourceTeam.DataPointName == "累计")
                    {
                        foreach (Model.DataSourcePoint dataSourcePoint in dataSourceTeam.DataSourcePoints)
                        {
                            this.lblTotal.Text += (dataSourcePoint.PointText + "：" + dataSourcePoint.PointValue + ",");
                        }
                        if (this.lblTotal.Text != "累计值为：")
                        {
                            this.lblTotal.Text = this.lblTotal.Text.Substring(0, this.lblTotal.Text.LastIndexOf(","));
                        }
                    }
                    else
                    {
                        this.lblTotal.Visible = false;
                        chart1.Series.Add(dataSourceTeam.DataPointName);
                        chart1.Series[dataSourceTeam.DataPointName].ChartType = dataSourceChart.ChartType;
                        chart1.Series[dataSourceTeam.DataPointName].Name = dataSourceTeam.DataPointName;
                        chart1.Series[dataSourceTeam.DataPointName].IsValueShownAsLabel = true;
                        chart1.Series[dataSourceTeam.DataPointName].BorderWidth = 2;
                        chart1.Series[dataSourceTeam.DataPointName].Label = "#PERCENT{P1}";
                        chart1.Series[dataSourceTeam.DataPointName]["DrawingStyle"] = "Cylinder";
                        int m = 0;
                        foreach (Model.DataSourcePoint dataSourcePoint in dataSourceTeam.DataSourcePoints)
                        {
                            chart1.Series[dataSourceTeam.DataPointName].Points.AddXY(dataSourcePoint.PointText, dataSourcePoint.PointValue);
                            chart1.Series[dataSourceTeam.DataPointName].Points[m].LegendText = dataSourcePoint.PointText + "#PERCENT{P1}";
                            m++;
                        }
                    }
                }
            }
            else
            {
                foreach (Model.DataSourceTeam dataSourceTeam in dataSourceChart.DataSourceTeams)
                {
                    this.lblTotal.Text = "累计值为：";
                    if (dataSourceTeam.DataPointName == "累计")
                    {
                        foreach (Model.DataSourcePoint dataSourcePoint in dataSourceTeam.DataSourcePoints)
                        {
                            this.lblTotal.Text += (dataSourcePoint.PointText + "：" + dataSourcePoint.PointValue + ",");
                        }
                        if (this.lblTotal.Text != "累计值为：")
                        {
                            this.lblTotal.Text = this.lblTotal.Text.Substring(0, this.lblTotal.Text.LastIndexOf(","));
                        }
                    }
                    else
                    {
                        this.lblTotal.Visible = false;
                        chart1.Series.Add(dataSourceTeam.DataPointName);
                        chart1.Series[dataSourceTeam.DataPointName].ChartType = dataSourceChart.ChartType;
                        chart1.Series[dataSourceTeam.DataPointName].Name = dataSourceTeam.DataPointName;
                        chart1.Series[dataSourceTeam.DataPointName].IsValueShownAsLabel = true;
                        chart1.Series[dataSourceTeam.DataPointName].BorderWidth = 2;
                        chart1.Series[dataSourceTeam.DataPointName]["DrawingStyle"] = "Cylinder";
                        foreach (Model.DataSourcePoint dataSourcePoint in dataSourceTeam.DataSourcePoints)
                        {
                            chart1.Series[dataSourceTeam.DataPointName].Points.AddXY(dataSourcePoint.PointText, dataSourcePoint.PointValue);
                        }
                    }
                }
            }
            Controls.Add(chart1);
        }

        /// <summary>
        /// 创建自定义Chart图形
        /// </summary>
        /// <param name="dt">数据源</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        public void CreateMaryChart(DataTable dt, int width, int height, string projectShortName)
        {
            Chart chart1 = new Chart();
            chart1.ID = "chart1";
            chart1.BackColor = Color.WhiteSmoke;
            chart1.ImageLocation = "~/Images/ChartPic_#SEQ(300,3)";
            chart1.BorderlineDashStyle = ChartDashStyle.Solid;
            chart1.Palette = ChartColorPalette.BrightPastel;
            chart1.BackSecondaryColor = Color.White;
            chart1.BackGradientStyle = GradientStyle.TopBottom;
            chart1.BorderWidth = 2;
            chart1.BorderColor = Color.FromArgb(26, 59, 105);
            chart1.ImageType = ChartImageType.Png;

            chart1.Width = width;
            chart1.Height = height;

            Title title = new Title();
            title.Text = "赢得值曲线";
            if (!string.IsNullOrEmpty(projectShortName))
            {
                title.Text = projectShortName + "赢得值曲线";
            }
            title.Text = BLL.Funs.GetSubStr(title.Text, 30);
            title.ToolTip = title.Text;
            title.ShadowColor = Color.FromArgb(32, 0, 0, 0);
            title.Font = new Font("Trebuchet MS", 10F, FontStyle.Bold);
            title.ShadowOffset = 3;
            title.ForeColor = Color.FromArgb(26, 59, 105);
            chart1.Titles.Add(title);

            Legend legend1 = new Legend();
            legend1.TextWrapThreshold = 1;
            legend1.Docking = Docking.Right;
            legend1.Alignment = StringAlignment.Center;
            legend1.BackColor = Color.Transparent;
            legend1.Font = new Font(new FontFamily("Trebuchet MS"), 8);
            legend1.LegendStyle = LegendStyle.Column;
            legend1.IsEquallySpacedItems = true;
            legend1.IsTextAutoFit = false;
            chart1.Legends.Add(legend1);

            ChartArea chartArea = new ChartArea();
            chartArea.BackColor = Color.Transparent;
            chartArea.AxisX.IsLabelAutoFit = false;
            chartArea.AxisY.Maximum = 1;//设置最小值，为了让第一个柱紧挨坐标轴
            chartArea.AxisY.LabelStyle.Format = "0%";//格式化，为了显示百分号
            chartArea.AxisY.IsLabelAutoFit = false;
            chartArea.AxisX.LabelStyle.Font = new Font("Verdana,Arial,Helvetica,sans-serif", 8F, FontStyle.Regular);
            chartArea.AxisY.LabelStyle.Font = new Font("Verdana,Arial,Helvetica,sans-serif", 8F, FontStyle.Regular);
            chartArea.AxisY.LineColor = Color.FromArgb(64, 64, 64, 64);
            chartArea.AxisX.LineColor = Color.FromArgb(64, 64, 64, 64);
            chartArea.AxisY.MajorGrid.LineColor = Color.FromArgb(64, 64, 64, 64);
            chartArea.AxisX.MajorGrid.LineColor = Color.FromArgb(64, 64, 64, 64);
            chartArea.AxisX.Interval = 1;
            chartArea.Area3DStyle.Enable3D = false;
            chart1.ChartAreas.Add(chartArea);

            chart1.Series.Add("计划值");
            chart1.Series.Add("实际值");
            chart1.Series.Add("累计计划值");
            chart1.Series.Add("累计实际值");

            DataView dv = dt.DefaultView;

            chart1.Series["计划值"].Points.DataBindXY(dv, dt.Columns[0].ColumnName, dv, "计划值");
            chart1.Series["计划值"].ChartType = SeriesChartType.Column;

            for (int i = 0; i < chart1.Series["计划值"].Points.Count; i++)
            {
                chart1.Series["计划值"].Points[i].ToolTip = "#VALX,#VALY";
            }

            chart1.Series["实际值"].Points.DataBindXY(dv, dt.Columns[0].ColumnName, dv, "实际值");
            chart1.Series["实际值"].ChartType = SeriesChartType.Column;

            for (int i = 0; i < chart1.Series["实际值"].Points.Count; i++)
            {
                chart1.Series["实际值"].Points[i].ToolTip = "#VALX,#VALY";
            }

            chart1.Series["累计计划值"].Points.DataBindXY(dv, dt.Columns[0].ColumnName, dv, "累计计划值");
            chart1.Series["累计计划值"].ChartType = SeriesChartType.Spline;
            chart1.Series["累计计划值"].Color = Color.Blue;
            chart1.Series["累计计划值"].BorderWidth = 2;

            for (int i = 0; i < chart1.Series["累计计划值"].Points.Count; i++)
            {
                chart1.Series["累计计划值"].Points[i].ToolTip = "#VALX,#VALY";
            }

            chart1.Series["累计实际值"].Points.DataBindXY(dv, dt.Columns[0].ColumnName, dv, "累计实际值");
            chart1.Series["累计实际值"].ChartType = SeriesChartType.Spline;
            chart1.Series["累计实际值"].Color = Color.MediumSeaGreen;
            chart1.Series["累计实际值"].BorderWidth = 2;

            for (int i = 0; i < chart1.Series["累计实际值"].Points.Count; i++)
            {
                chart1.Series["累计实际值"].Points[i].ToolTip = "#VALX,#VALY";
            }

            Controls.Add(chart1);
        }
    }
}