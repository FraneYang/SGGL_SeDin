using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI.DataVisualization.Charting;
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
                this.Analyse();
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
            this.Analyse();
        }
        /// <summary>
        /// 
        /// </summary>
        private void Analyse()
        {
            gethazardRegisters = (from x in Funs.DB.HSSE_Hazard_HazardRegister
                                  where x.ProjectId == this.CurrUser.LoginProjectId
                                  && x.States != "4"
                                  select x).ToList();
            gethazardRegisterTypes = (from x in Funs.DB.HSSE_Hazard_HazardRegisterTypes
                                      select x).ToList();
            this.AnalyseData();
        }
        /// <summary>
        /// 安全巡检
        /// </summary>
        private static List<Model.HSSE_Hazard_HazardRegister> gethazardRegisters;
        /// <summary>
        /// 安全巡检类型
        /// </summary>
        private static List<Model.HSSE_Hazard_HazardRegisterTypes> gethazardRegisterTypes;
        /// <summary>
        /// 统计方法
        /// </summary>
        private void AnalyseData()
        {
            var hazardRegisters = gethazardRegisters;
            if (!string.IsNullOrEmpty(this.txtStartRectificationTime.Text.Trim()))
            {
                hazardRegisters = hazardRegisters.Where(x => x.CheckTime >= Funs.GetNewDateTime(this.txtStartRectificationTime.Text.Trim())).ToList();
            }
            if (!string.IsNullOrEmpty(this.txtEndRectificationTime.Text.Trim()))
            {
                hazardRegisters = hazardRegisters.Where(x => x.CheckTime <= Funs.GetNewDateTime(this.txtEndRectificationTime.Text.Trim())).ToList();
            }
            ///按问题类型
            DataTable dtTime = new DataTable();
            if (this.drpType.SelectedValue == "0")
            {
                ///按单位统计
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
            }
            else if (this.drpType.SelectedValue == "5")
            {
                ///按单位统计
                dtTime.Columns.Add("单位工程", typeof(string));
                dtTime.Columns.Add("总数量", typeof(string));
                dtTime.Columns.Add("待整改", typeof(string));
                dtTime.Columns.Add("已整改", typeof(string));
                var workArea = (from x in hazardRegisters
                               join y in Funs.DB.WBS_UnitWork on x.Place equals y.UnitWorkId
                               where y.ProjectId == this.CurrUser.LoginProjectId && y.SuperUnitWork == null
                               select y).Distinct();
                foreach (var item in workArea)
                {
                    DataRow rowTime = dtTime.NewRow();
                    Model.SpTDesktopItem newspItem = new Model.SpTDesktopItem();
                    rowTime["单位工程"] = item.UnitWorkName;
                    var unitHazad = hazardRegisters.Where(x => x.Place == item.UnitWorkId);
                    rowTime["总数量"] = unitHazad.Count();
                    rowTime["待整改"] = unitHazad.Where(x => x.States == "1" || x.States == null).Count();
                    rowTime["已整改"] = unitHazad.Where(x => x.States == "3" || x.States == "2").Count();
                    dtTime.Rows.Add(rowTime);
                }
            }
            else
            {
                ///按检查项
                dtTime.Columns.Add("检查项", typeof(string));
                dtTime.Columns.Add("总数量", typeof(string));
                dtTime.Columns.Add("待整改", typeof(string));
                dtTime.Columns.Add("已整改", typeof(string));
                var types = gethazardRegisterTypes.Where(x=> x.HazardRegisterType == this.drpType.SelectedValue).OrderBy(x=>x.TypeCode);
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
            }

            this.ChartAccidentTime.CreateChart(BLL.ChartControlService.GetDataSourceChart(dtTime, "巡检分析", "Column", 1100, 400, false));
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