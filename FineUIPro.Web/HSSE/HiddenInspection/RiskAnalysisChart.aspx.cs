using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using BLL;

namespace FineUIPro.Web.HSSE.HiddenInspection
{
    public partial class RiskAnalysisChart : PageBase
    {
        #region 加载页面
        /// <summary>
        /// 加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
            }
        }
        #endregion

        #region 统计
        /// <summary>
        /// 统计分析
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnAnalyse_Click(object sender, EventArgs e)
        {
            this.AnalyseData();
        }

        /// <summary>
        /// 统计方法
        /// </summary>
        private void AnalyseData()
        {
            var hazardRegisters = (from x in Funs.DB.HSSE_Hazard_HazardRegister
                                   where x.ProjectId == this.CurrUser.LoginProjectId
                                   && x.States != "4" && x.ProblemTypes == "1"
                                   select x);
            if (!string.IsNullOrEmpty(this.txtStartRectificationTime.Text.Trim()))
            {
                hazardRegisters = hazardRegisters.Where(x => x.CheckTime >= Funs.GetNewDateTime(this.txtStartRectificationTime.Text.Trim()));
            }
            if (!string.IsNullOrEmpty(this.txtEndRectificationTime.Text.Trim()))
            {
                hazardRegisters = hazardRegisters.Where(x => x.CheckTime <= Funs.GetNewDateTime(this.txtEndRectificationTime.Text.Trim()));
            }         
            if (this.rblState.SelectedValue == "0")
            {
                #region 按单位统计
                if (this.drpChartType.SelectedValue != "Pie")  //非饼形图
                {
                    ///按单位统计
                    DataTable dtTime = new DataTable();
                    dtTime.Columns.Add("单位", typeof(string));
                    dtTime.Columns.Add("总数量", typeof(string));
                    dtTime.Columns.Add("待整改", typeof(string));
                    dtTime.Columns.Add("已整改", typeof(string));

                    var units = BLL.UnitService.GetUnitByProjectIdUnitTypeList(this.CurrUser.LoginProjectId, BLL.Const.ProjectUnitType_2);
                    foreach (var item in units)
                    {
                        DataRow rowTime = dtTime.NewRow();
                        Model.SpTDesktopItem newspItem = new Model.SpTDesktopItem();
                        rowTime["单位"] = item.UnitName;
                        var unitHazad = hazardRegisters.Where(x => x.ResponsibleUnit == item.UnitId);
                        rowTime["总数量"] = unitHazad.Count();
                        rowTime["待整改"] = unitHazad.Where(x => x.States == "1" || x.States == null).Count();
                        rowTime["已整改"] = unitHazad.Where(x => x.States == "3" || x.States == "2").Count();
                        dtTime.Rows.Add(rowTime);
                    }
                    this.ChartAccidentTime.CreateChart(BLL.ChartControlService.GetDataSourceChart(dtTime, "单位巡检分析", this.drpChartType.SelectedValue, 1130, 450, this.ckbShow.Checked));
                }
                else   //饼形图
                {
                    DataTable dtTime = new DataTable();
                    dtTime.Columns.Add("单位", typeof(string));
                    dtTime.Columns.Add("总数量", typeof(string));

                    var units = BLL.UnitService.GetUnitByProjectIdUnitTypeList(this.CurrUser.LoginProjectId, BLL.Const.ProjectUnitType_2);
                    foreach (var item in units)
                    {
                        DataRow rowTime = dtTime.NewRow();
                        Model.SpTDesktopItem newspItem = new Model.SpTDesktopItem();
                        rowTime["单位"] = item.UnitName;
                        var unitHazad = hazardRegisters.Where(x => x.ResponsibleUnit == item.UnitId);
                        rowTime["总数量"] = unitHazad.Count();
                        dtTime.Rows.Add(rowTime);
                    }
                    this.ChartAccidentTime.CreateChart(BLL.ChartControlService.GetDataSourceChart(dtTime, "单位巡检分析", this.drpChartType.SelectedValue, 1130, 450, this.ckbShow.Checked));
                }
                #endregion
            }
            else
            {
                #region 按检查项
                if (this.drpChartType.SelectedValue != "Pie")  //非饼形图
                {
                    ///按检查项
                    DataTable dtTime = new DataTable();
                    dtTime.Columns.Add("检查项", typeof(string));
                    dtTime.Columns.Add("总数量", typeof(string));
                    dtTime.Columns.Add("待整改", typeof(string));
                    dtTime.Columns.Add("已整改", typeof(string));

                    var types = from x in Funs.DB.HSSE_Hazard_HazardRegisterTypes where x.HazardRegisterType == "1" orderby x.TypeCode select x;
                    foreach (var item in types)
                    {
                        DataRow rowTime = dtTime.NewRow();
                        Model.SpTDesktopItem newspItem = new Model.SpTDesktopItem();
                        rowTime["检查项"] = item.RegisterTypesName;
                        var typeHazad = hazardRegisters.Where(x => x.RegisterTypesId == item.RegisterTypesId);
                        rowTime["总数量"] = typeHazad.Count();
                        rowTime["待整改"] = typeHazad.Where(x => x.States == "1" || x.States == null).Count();
                        rowTime["已整改"] = typeHazad.Where(x => x.States == "3" || x.States == "2").Count();
                        dtTime.Rows.Add(rowTime);
                    }

                    this.ChartAccidentTime.CreateChart(BLL.ChartControlService.GetDataSourceChart(dtTime, "巡检问题分析", this.drpChartType.SelectedValue, 1100, 330, this.ckbShow.Checked));
                }
                else   //饼形图
                {
                    ///按问题类型
                    DataTable dtTime = new DataTable();
                    dtTime.Columns.Add("检查项", typeof(string));
                    dtTime.Columns.Add("总数量", typeof(string));

                    var types = from x in Funs.DB.HSSE_Hazard_HazardRegisterTypes where x.HazardRegisterType == "1" orderby x.TypeCode select x;
                    foreach (var item in types)
                    {
                        DataRow rowTime = dtTime.NewRow();
                        Model.SpTDesktopItem newspItem = new Model.SpTDesktopItem();
                        rowTime["检查项"] = item.RegisterTypesName;
                        var typeHazad = hazardRegisters.Where(x => x.RegisterTypesId == item.RegisterTypesId);
                        rowTime["总数量"] = typeHazad.Count();
                        dtTime.Rows.Add(rowTime);
                    }

                    this.ChartAccidentTime.CreateChart(BLL.ChartControlService.GetDataSourceChart(dtTime, "巡检问题分析", this.drpChartType.SelectedValue, 1100, 330, this.ckbShow.Checked));
                }
                #endregion
            }
        }
        #endregion

        #region 图形
        /// <summary>
        /// 图形变换 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpChartType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.AnalyseData();
        }

        protected void ckbShow_CheckedChanged(object sender, CheckedEventArgs e)
        {
            this.AnalyseData();
        }
        #endregion
    }
}