using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using System.Data;

namespace FineUIPro.Web
{
    public partial class mainMenu_JDGL : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
            }
        }

        #region 赢得值曲线
        protected string Two
        {
            get
            {
                DataTable tb = new DataTable();
                DateTime startDate, endDate, startMonth, endMonth;
                List<DateTime> months = new List<DateTime>();
                List<DateTime> weeks = new List<DateTime>();
                List<DateTime> days = new List<DateTime>();
                string isDataOK = string.Empty;
                Model.Base_Project project = BLL.ProjectService.GetProjectByProjectId(this.CurrUser.LoginProjectId);
                //默认开始结束日期为项目开始结束日期
                startDate = Convert.ToDateTime(project.StartDate);
                endDate = DateTime.Now.Date.AddDays(1).AddSeconds(-1);
                if (project.EndDate != null && DateTime.Now > project.EndDate)
                {
                    endDate = project.EndDate.Value.AddDays(1).AddSeconds(-1);
                }
                List<Model.WBS_UnitWork> unitWorks = new List<Model.WBS_UnitWork>();
                List<Model.WBS_WorkPackage> workPackages = new List<Model.WBS_WorkPackage>();
                List<Model.WBS_ControlItemAndCycle> controlItemAndCycles = new List<Model.WBS_ControlItemAndCycle>();
                List<Model.View_Check_SoptCheckDetail> soptCheckDetails = new List<Model.View_Check_SoptCheckDetail>();
                unitWorks = BLL.UnitWorkService.GetUnitWorkLists(this.CurrUser.LoginProjectId);
                decimal totalUnitWorkWeights = 0;
                bool noWeights = false;
                bool noProjectWeights = false;

                workPackages = BLL.WorkPackageService.GetAllWorkPackagesByProjectId(this.CurrUser.LoginProjectId);
                controlItemAndCycles = BLL.ControlItemAndCycleService.GetControlItemAndCyclesByProjectIdAndDate(this.CurrUser.LoginProjectId, endDate);
                soptCheckDetails = BLL.SpotCheckDetailService.GetViewSpotCheckDetailsByProjectIdAndDate(this.CurrUser.LoginProjectId, endDate, isDataOK);
                var unitWork1 = Funs.DB.WBS_UnitWork.FirstOrDefault(x => x.ProjectId == this.CurrUser.LoginProjectId && x.Weights == null);
                if (unitWork1 != null)   //存在没有权重值的单位工程
                {
                    noProjectWeights = true;
                }

                //if (noWeights && unitWorkIds.Length > 1)
                //{
                //    Alert.ShowInTop("请先设置所选单位工程的权重值！", MessageBoxIcon.Warning);
                //    return;
                //}
                //if (noProjectWeights)
                //{
                //    Alert.ShowInTop("请先设置所有单位工程的权重值！", MessageBoxIcon.Warning);
                //    return;
                //}
                //if (this.drpUnitWork.SelectedValue != BLL.Const._Null)
                //{
                //    workPackages = BLL.WorkPackageService.GetAllWorkPackagesByUnitWorkId(this.drpUnitWork.SelectedValue);
                //    controlItemAndCycles = BLL.ControlItemAndCycleService.GetControlItemAndCyclesByUnitWorkIdAndDate(this.drpUnitWork.SelectedValue, endDate);
                //    soptCheckDetails = BLL.SpotCheckDetailService.GetViewSpotCheckDetailsByUnitWorkIdAndDate(this.drpUnitWork.SelectedValue, endDate, isDataOK);
                //}
                //else
                //{
                //    workPackages = BLL.WorkPackageService.GetAllWorkPackagesByProjectId(this.CurrUser.LoginProjectId);
                //    controlItemAndCycles = BLL.ControlItemAndCycleService.GetControlItemAndCyclesByProjectIdAndDate(this.CurrUser.LoginProjectId, endDate);
                //    soptCheckDetails = BLL.SpotCheckDetailService.GetViewSpotCheckDetailsByProjectIdAndDate(this.CurrUser.LoginProjectId, endDate, isDataOK);
                //}
                decimal lastDPlanTotal = 0;
                decimal lastDCompleteTotal = 0;
                tb.Columns.Add("月");
                startMonth = Convert.ToDateTime(startDate.Year + "-" + startDate.Month + "-01");
                endMonth = Convert.ToDateTime(endDate.Year + "-" + endDate.Month + "-01");
                do
                {
                    months.Add(startMonth);
                    startMonth = startMonth.AddMonths(1);
                } while (startMonth <= endMonth);
                tb.Columns.Add("计划值");
                tb.Columns.Add("累计计划值");
                tb.Columns.Add("实际值");
                tb.Columns.Add("累计实际值");
                for (int i = 0; i < months.Count; i++)
                {
                    DataRow row = tb.NewRow();
                    row[0] = string.Format("{0:yyyy-MM}", months[i]);
                    //对应月份的记录
                    decimal dPlan = 0, dPlan1 = 0, dPlanTotal = 0, dComplete = 0, dComplete1 = 0, dCompleteMonth = 0, dCompleteTotal = 0;
                    //当月及之前所有工作包内容
                    var totalPlanCompleteControlItemAndCycles = controlItemAndCycles.Where(x => x.PlanCompleteDate < months[i].AddMonths(1));
                    //当月及之前所有验收合格记录
                    var totalSoptCheckDetails = soptCheckDetails.Where(x => x.SpotCheckDate < months[i].AddMonths(1));
                    foreach (var item in totalPlanCompleteControlItemAndCycles)
                    {
                        {
                            var workPackage1 = workPackages.FirstOrDefault(x => x.WorkPackageId == item.WorkPackageId);
                            if (workPackage1 != null)
                            {
                                //逐级递推计算权重计划值
                                dPlan1 = Convert.ToDecimal((workPackage1.Weights ?? 0) / 100) * Convert.ToDecimal(item.Weights / 100);
                                var workPackage2 = workPackages.FirstOrDefault(x => x.WorkPackageId == workPackage1.SuperWorkPackageId);
                                if (workPackage2 != null)
                                {
                                    dPlan1 = Convert.ToDecimal((workPackage2.Weights ?? 0) / 100) * dPlan1;
                                    var workPackage3 = workPackages.FirstOrDefault(x => x.WorkPackageId == workPackage2.SuperWorkPackageId);
                                    if (workPackage3 != null)
                                    {
                                        dPlan1 = Convert.ToDecimal((workPackage3.Weights ?? 0) / 100) * dPlan1;
                                    }
                                }
                            }
                            var unitWork = unitWorks.FirstOrDefault(x => x.UnitWorkId == workPackage1.UnitWorkId);
                            if (unitWork != null)
                            {
                                dPlan1 = Convert.ToDecimal((unitWork.Weights ?? 0) / 100) * dPlan1;
                            }
                        }
                        if (item.PlanCompleteDate >= months[i])   //当月计划完成记录
                        {
                            dPlan += dPlan1;   //累加当月值
                        }
                        dPlanTotal += dPlan1;   //累加累计值

                    }
                    foreach (var item in controlItemAndCycles)
                    {
                        //实际值
                        var itemSoptCheckDetails = soptCheckDetails.Where(x => x.ControlItemAndCycleId == item.ControlItemAndCycleId && x.SpotCheckDate < months[i].AddMonths(1));
                        var itemMonthSoptCheckDetails = soptCheckDetails.Where(x => x.ControlItemAndCycleId == item.ControlItemAndCycleId && x.SpotCheckDate >= months[i] && x.SpotCheckDate < months[i].AddMonths(1));
                        if (itemSoptCheckDetails.Count() > 0)  //存在验收合格的记录
                        {
                            //工作包实际值
                            dComplete1 = Convert.ToDecimal(itemSoptCheckDetails.Count()) / Convert.ToDecimal(item.CheckNum) * Convert.ToDecimal(item.Weights);
                            var workPackage1 = workPackages.FirstOrDefault(x => x.WorkPackageId == item.WorkPackageId);
                            if (workPackage1 != null)
                            {
                                //逐级递推计算权重计划值
                                dComplete1 = Convert.ToDecimal((workPackage1.Weights ?? 0) / 100) * Convert.ToDecimal(dComplete1 / 100);
                                var workPackage2 = workPackages.FirstOrDefault(x => x.WorkPackageId == workPackage1.SuperWorkPackageId);
                                if (workPackage2 != null)
                                {
                                    dComplete1 = Convert.ToDecimal((workPackage2.Weights ?? 0) / 100) * dComplete1;
                                    var workPackage3 = workPackages.FirstOrDefault(x => x.WorkPackageId == workPackage2.SuperWorkPackageId);
                                    if (workPackage3 != null)
                                    {
                                        dComplete1 = Convert.ToDecimal((workPackage3.Weights ?? 0) / 100) * dComplete1;
                                    }
                                }
                            }
                            var unitWork = unitWorks.FirstOrDefault(x => x.UnitWorkId == workPackage1.UnitWorkId);
                            if (unitWork != null)
                            {
                                dComplete1 = Convert.ToDecimal((unitWork.Weights ?? 0) / 100) * dComplete1;
                            }
                            dCompleteTotal += dComplete1;
                        }
                        if (itemMonthSoptCheckDetails.Count() > 0)  //当月存在验收合格的记录
                        {
                            //工作包实际值
                            dCompleteMonth = Convert.ToDecimal(itemMonthSoptCheckDetails.Count()) / Convert.ToDecimal(item.CheckNum) * Convert.ToDecimal(item.Weights);
                            var workPackage1 = workPackages.FirstOrDefault(x => x.WorkPackageId == item.WorkPackageId);
                            if (workPackage1 != null)
                            {
                                //逐级递推计算权重计划值
                                dCompleteMonth = Convert.ToDecimal((workPackage1.Weights ?? 0) / 100) * Convert.ToDecimal(dCompleteMonth / 100);
                                var workPackage2 = workPackages.FirstOrDefault(x => x.WorkPackageId == workPackage1.SuperWorkPackageId);
                                if (workPackage2 != null)
                                {
                                    dCompleteMonth = Convert.ToDecimal((workPackage2.Weights ?? 0) / 100) * dCompleteMonth;
                                    var workPackage3 = workPackages.FirstOrDefault(x => x.WorkPackageId == workPackage2.SuperWorkPackageId);
                                    if (workPackage3 != null)
                                    {
                                        dCompleteMonth = Convert.ToDecimal((workPackage3.Weights ?? 0) / 100) * dCompleteMonth;
                                    }
                                }
                            }
                            var unitWork = unitWorks.FirstOrDefault(x => x.UnitWorkId == workPackage1.UnitWorkId);
                            if (unitWork != null)
                            {
                                dCompleteMonth = Convert.ToDecimal((unitWork.Weights ?? 0) / 100) * dCompleteMonth;
                            }
                            dComplete += dCompleteMonth;
                        }
                    }
                    row[1] = dPlan.ToString();   //计划值
                    if (dPlanTotal != lastDPlanTotal) //当期累计计划值不等于上月累计计划值时，再保存累计计划值
                    {
                        row[2] = dPlanTotal.ToString();   //累计计划值
                        for (int j = 0; j < i; j++)
                        {
                            if (string.IsNullOrEmpty(tb.Rows[j][2].ToString()))
                            {
                                tb.Rows[j][2] = lastDPlanTotal.ToString();
                            }
                        }
                    }
                    else
                    {
                        if (dPlanTotal == 0)
                        {
                            row[2] = "0";   //累计计划值
                        }
                        else
                        {
                            row[2] = dPlanTotal;
                        }
                    }
                    lastDPlanTotal = dPlanTotal;
                    row[3] = dComplete.ToString();   //实际值
                    if (dCompleteTotal != lastDCompleteTotal) //当期累计实际值不等于上月累计实际值时，再保存累计实际值
                    {
                        row[4] = dCompleteTotal.ToString();   //累计实际值
                        for (int j = 0; j < i; j++)
                        {
                            if (string.IsNullOrEmpty(tb.Rows[j][4].ToString()))
                            {
                                tb.Rows[j][4] = lastDCompleteTotal.ToString();
                            }
                        }
                    }
                    else
                    {
                        if (dCompleteTotal == 0)
                        {
                            row[4] = "0";   //累计实际值
                        }
                        else
                        {
                            row[4] = dCompleteTotal;
                        }
                    }
                    lastDCompleteTotal = dCompleteTotal;
                    tb.Rows.Add(row);
                }

                List<Model.SingleSerie> series = new List<Model.SingleSerie>();
                Model.BusinessColumn businessColumn = new Model.BusinessColumn();
                List<string> listCategories = new List<string>();
                businessColumn.title = "赢得值曲线";
                var units = BLL.ProjectUnitService.GetProjectUnitListByProjectIdUnitType(CurrUser.LoginProjectId, BLL.Const.ProjectUnitType_2);
                Model.SingleSerie s = new Model.SingleSerie();
                Model.SingleSerie s2 = new Model.SingleSerie();
                Model.SingleSerie s3 = new Model.SingleSerie();
                Model.SingleSerie s4 = new Model.SingleSerie();
                List<double> listdata = new List<double>();
                List<double> listdata2 = new List<double>();
                List<double> listdata3 = new List<double>();
                List<double> listdata4 = new List<double>();
                for (int i = 0; i < tb.Rows.Count; i++)
                {
                    listCategories.Add(tb.Rows[i][0].ToString());
                    listdata.Add(Convert.ToDouble(tb.Rows[i][1].ToString()) * 100);
                    listdata2.Add(Convert.ToDouble(tb.Rows[i][2].ToString()) * 100);
                    listdata3.Add(Convert.ToDouble(tb.Rows[i][3].ToString()) * 100);
                    listdata4.Add(Convert.ToDouble(tb.Rows[i][4].ToString()) * 100);
                }
                s.data = listdata;
                s2.data = listdata2;
                s3.data = listdata3;
                s4.data = listdata4;
                series.Add(s);
                series.Add(s2);
                series.Add(s3);
                series.Add(s4);
                businessColumn.categories = listCategories;
                businessColumn.series = series;
                return JsonConvert.SerializeObject(businessColumn);
            }
        }
        #endregion

        #region 施工进度统计
        protected string Three
        {
            get
            {
                List<Model.SingleSerie> series = new List<Model.SingleSerie>();
                Model.BusinessColumn businessColumn = new Model.BusinessColumn();
                List<string> listCategories = new List<string>();
                businessColumn.title = "施工进度统计";
                var unitWorks = BLL.UnitWorkService.GetUnitWorkLists(CurrUser.LoginProjectId);
                unitWorks = unitWorks.Where(x => x.ProjectType == "2").ToList();
                var spotCheckDetails = from x in Funs.DB.View_Check_SoptCheckDetail
                                       where x.ProjectId == this.CurrUser.LoginProjectId && x.IsOK == true
                                       select x;
                Model.SingleSerie s = new Model.SingleSerie();
                List<double> listdata = new List<double>();
                foreach (var unitWork in unitWorks)
                {
                    listCategories.Add(unitWork.UnitWorkName);
                    var unitWorkSpotCheckDetails = spotCheckDetails.Where(x => x.UnitWorkId == unitWork.UnitWorkId);
                    listdata.Add(unitWorkSpotCheckDetails.Count());
                }
                s.data = listdata;
                series.Add(s);
                businessColumn.categories = listCategories;
                businessColumn.series = series;
                return JsonConvert.SerializeObject(businessColumn);
            }
        }

        protected string Three2
        {
            get
            {
                List<Model.SingleSerie> series = new List<Model.SingleSerie>();
                Model.BusinessColumn businessColumn = new Model.BusinessColumn();
                List<string> listCategories = new List<string>();
                businessColumn.title = "施工进度统计";
                var unitWorks = BLL.UnitWorkService.GetUnitWorkLists(CurrUser.LoginProjectId);
                unitWorks = unitWorks.Where(x => x.ProjectType == "1").ToList();
                var spotCheckDetails = from x in Funs.DB.View_Check_SoptCheckDetail
                                       where x.ProjectId == this.CurrUser.LoginProjectId && x.IsOK == true
                                       select x;
                Model.SingleSerie s = new Model.SingleSerie();
                List<double> listdata = new List<double>();
                foreach (var unitWork in unitWorks)
                {
                    listCategories.Add(unitWork.UnitWorkName);
                    var unitWorkSpotCheckDetails = spotCheckDetails.Where(x => x.UnitWorkId == unitWork.UnitWorkId);
                    listdata.Add(unitWorkSpotCheckDetails.Count());
                }
                s.data = listdata;
                series.Add(s);
                businessColumn.categories = listCategories;
                businessColumn.series = series;
                return JsonConvert.SerializeObject(businessColumn);
            }
        }
        #endregion
    }
}