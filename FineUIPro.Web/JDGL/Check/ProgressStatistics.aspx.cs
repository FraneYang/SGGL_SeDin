using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.JDGL.Check
{
    public partial class ProgressStatistics : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Funs.FineUIPleaseSelect(this.drpProjectType);
                Funs.FineUIPleaseSelect(this.drpUnitWork);
                //UnitWorkService.InitUnitWorkDownList(drpUnitWork, this.CurrUser.LoginProjectId, true);
            }
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            BindGrid();
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        public void BindGrid()
        {
            DataTable tb = new DataTable();
            DateTime startDate, endDate, startMonth, endMonth, startDay, endDay, endDayItem;
            List<DateTime> months = new List<DateTime>();
            List<DateTime> weeks = new List<DateTime>();
            List<DateTime> days = new List<DateTime>();
            Model.Base_Project project = BLL.ProjectService.GetProjectByProjectId(this.CurrUser.LoginProjectId);
            //默认开始结束日期为项目开始结束日期
            startDate = Convert.ToDateTime(project.StartDate);
            endDate = DateTime.Now.Date.AddDays(1).AddSeconds(-1);
            if (!string.IsNullOrEmpty(this.txtStartTime.Text.Trim()))
            {
                startDate = Convert.ToDateTime(this.txtStartTime.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.txtEndTime.Text.Trim()))
            {
                endDate = Convert.ToDateTime(this.txtEndTime.Text.Trim()).AddDays(1).AddSeconds(-1);
            }
            string isDataOK = string.Empty;
            if (this.ckbData.Checked)
            {
                isDataOK = "True";
            }
            List<Model.WBS_UnitWork> unitWorks = new List<Model.WBS_UnitWork>();
            List<Model.WBS_WorkPackage> workPackages = new List<Model.WBS_WorkPackage>();
            List<Model.WBS_ControlItemAndCycle> controlItemAndCycles = new List<Model.WBS_ControlItemAndCycle>();
            List<Model.View_Check_SoptCheckDetail> soptCheckDetails = new List<Model.View_Check_SoptCheckDetail>();
            unitWorks = BLL.UnitWorkService.GetUnitWorkLists(this.CurrUser.LoginProjectId);
            decimal totalUnitWorkWeights = 0;
            bool noWeights = false;
            bool noProjectWeights = false;
            string[] unitWorkIds = this.drpUnitWork.SelectedValueArray;
            if (unitWorkIds.Length == 1 && unitWorkIds[0] == BLL.Const._Null)   //统计项目
            {
                workPackages = BLL.WorkPackageService.GetAllWorkPackagesByProjectId(this.CurrUser.LoginProjectId);
                controlItemAndCycles = BLL.ControlItemAndCycleService.GetControlItemAndCyclesByProjectIdAndDate(this.CurrUser.LoginProjectId, endDate);
                soptCheckDetails = BLL.SpotCheckDetailService.GetViewSpotCheckDetailsByProjectIdAndDate(this.CurrUser.LoginProjectId, endDate, isDataOK);
                var unitWork = new Model.SGGLDB(Funs.ConnString).WBS_UnitWork.FirstOrDefault(x => x.ProjectId == this.CurrUser.LoginProjectId && x.Weights == null);
                if (unitWork != null)   //存在没有权重值的单位工程
                {
                    noProjectWeights = true;
                }
            }
            else   //统计单位工程（多选）
            {
                workPackages = BLL.WorkPackageService.GetAllWorkPackagesByUnitWorkIds(unitWorkIds);
                controlItemAndCycles = BLL.ControlItemAndCycleService.GetControlItemAndCyclesByUnitWorkIdsAndDate(unitWorkIds, endDate);
                soptCheckDetails = BLL.SpotCheckDetailService.GetViewSpotCheckDetailsByUnitWorkIdsAndDate(unitWorkIds, endDate, isDataOK);
                foreach (var item in unitWorkIds)
                {
                    var unitWork = BLL.UnitWorkService.GetUnitWorkByUnitWorkId(item);
                    if (unitWork != null)
                    {
                        if (unitWork.Weights != null)
                        {
                            totalUnitWorkWeights += Convert.ToDecimal(unitWork.Weights);
                        }
                        else
                        {
                            noWeights = true;
                            break;
                        }
                    }
                }
            }
            if (noWeights && unitWorkIds.Length > 1)
            {
                Alert.ShowInTop("请先设置所选单位工程的权重值！", MessageBoxIcon.Warning);
                return;
            }
            if (noProjectWeights)
            {
                Alert.ShowInTop("请先设置所有单位工程的权重值！", MessageBoxIcon.Warning);
                return;
            }
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
            #region 按月
            if (this.rblType.SelectedValue == "Month")
            {
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
                            if (unitWorkIds.Length == 1 && unitWorkIds[0] == BLL.Const._Null)   //按项目统计时，再乘以单位工程权重
                            {
                                var unitWork = unitWorks.FirstOrDefault(x => x.UnitWorkId == workPackage1.UnitWorkId);
                                if (unitWork != null)
                                {
                                    dPlan1 = Convert.ToDecimal((unitWork.Weights ?? 0) / 100) * dPlan1;
                                }
                            }
                            else
                            {
                                if (unitWorkIds.Length > 1)
                                {
                                    var unitWork = unitWorks.FirstOrDefault(x => x.UnitWorkId == workPackage1.UnitWorkId);
                                    if (unitWork != null && totalUnitWorkWeights > 0)
                                    {
                                        dPlan1 = Convert.ToDecimal((unitWork.Weights ?? 0) / totalUnitWorkWeights) * dPlan1;
                                    }
                                }
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
                            if (unitWorkIds.Length == 1 && unitWorkIds[0] == BLL.Const._Null)   //按项目统计时，再乘以单位工程权重
                            {
                                var unitWork = unitWorks.FirstOrDefault(x => x.UnitWorkId == workPackage1.UnitWorkId);
                                if (unitWork != null)
                                {
                                    dComplete1 = Convert.ToDecimal((unitWork.Weights ?? 0) / 100) * dComplete1;
                                }
                            }
                            else
                            {
                                if (unitWorkIds.Length > 1)
                                {
                                    var unitWork = unitWorks.FirstOrDefault(x => x.UnitWorkId == workPackage1.UnitWorkId);
                                    if (unitWork != null && totalUnitWorkWeights > 0)
                                    {
                                        dComplete1 = Convert.ToDecimal((unitWork.Weights ?? 0) / totalUnitWorkWeights) * dComplete1;
                                    }
                                }
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
                            if (unitWorkIds.Length == 1 && unitWorkIds[0] == BLL.Const._Null)   //按项目统计时，再乘以单位工程权重
                            {
                                var unitWork = unitWorks.FirstOrDefault(x => x.UnitWorkId == workPackage1.UnitWorkId);
                                if (unitWork != null)
                                {
                                    dCompleteMonth = Convert.ToDecimal((unitWork.Weights ?? 0) / 100) * dCompleteMonth;
                                }
                            }
                            else
                            {
                                if (unitWorkIds.Length > 1)
                                {
                                    var unitWork = unitWorks.FirstOrDefault(x => x.UnitWorkId == workPackage1.UnitWorkId);
                                    if (unitWork != null && totalUnitWorkWeights > 0)
                                    {
                                        dCompleteMonth = Convert.ToDecimal((unitWork.Weights ?? 0) / totalUnitWorkWeights) * dCompleteMonth;
                                    }
                                }
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
                            row[2] = null;
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
                            row[4] = null;
                        }
                    }
                    lastDCompleteTotal = dCompleteTotal;
                    tb.Rows.Add(row);
                }
            }
            #endregion
            #region 按周
            else if (this.rblType.SelectedValue == "Week")
            {
                tb.Columns.Add("周");
                int w = 1;
                tb.Columns.Add("计划值");
                tb.Columns.Add("累计计划值");
                tb.Columns.Add("实际值");
                tb.Columns.Add("累计实际值");
                startDay = startDate;
                endDay = endDate;
                do
                {
                    endDayItem = startDay.AddDays(6);
                    if (endDayItem > endDate)
                    {
                        endDayItem = endDate;
                    }
                    DataRow row = tb.NewRow();
                    row[0] = "第" + w.ToString() + "周";
                    //对应周的记录
                    decimal dPlan = 0, dPlan1 = 0, dPlanTotal = 0, dComplete = 0, dComplete1 = 0, dCompleteWeek = 0, dCompleteTotal = 0;
                    //当周及之前所有工作包内容
                    var totalPlanCompleteControlItemAndCycles = controlItemAndCycles.Where(x => x.PlanCompleteDate < endDayItem);
                    //当周及之前所有验收合格记录
                    var totalSoptCheckDetails = soptCheckDetails.Where(x => x.SpotCheckDate < endDayItem);
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
                            if (unitWorkIds.Length == 1 && unitWorkIds[0] == BLL.Const._Null)   //按项目统计时，再乘以单位工程权重
                            {
                                var unitWork = unitWorks.FirstOrDefault(x => x.UnitWorkId == workPackage1.UnitWorkId);
                                if (unitWork != null)
                                {
                                    dPlan1 = Convert.ToDecimal((unitWork.Weights ?? 0) / 100) * dPlan1;
                                }
                            }
                            else
                            {
                                if (unitWorkIds.Length > 1)
                                {
                                    var unitWork = unitWorks.FirstOrDefault(x => x.UnitWorkId == workPackage1.UnitWorkId);
                                    if (unitWork != null && totalUnitWorkWeights > 0)
                                    {
                                        dPlan1 = Convert.ToDecimal((unitWork.Weights ?? 0) / totalUnitWorkWeights) * dPlan1;
                                    }
                                }
                            }
                        }
                        if (item.PlanCompleteDate >= startDay)   //当周计划完成记录
                        {
                            dPlan += dPlan1;   //累加当周值
                        }
                        dPlanTotal += dPlan1;   //累加累计值

                    }
                    foreach (var item in controlItemAndCycles)
                    {
                        //实际值
                        var itemSoptCheckDetails = soptCheckDetails.Where(x => x.ControlItemAndCycleId == item.ControlItemAndCycleId && x.SpotCheckDate < endDayItem);
                        var itemWeekSoptCheckDetails = soptCheckDetails.Where(x => x.ControlItemAndCycleId == item.ControlItemAndCycleId && x.SpotCheckDate >= startDay && x.SpotCheckDate < endDayItem.AddDays(1));
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
                            if (unitWorkIds.Length == 1 && unitWorkIds[0] == BLL.Const._Null)   //按项目统计时，再乘以单位工程权重
                            {
                                var unitWork = unitWorks.FirstOrDefault(x => x.UnitWorkId == workPackage1.UnitWorkId);
                                if (unitWork != null)
                                {
                                    dComplete1 = Convert.ToDecimal((unitWork.Weights ?? 0) / 100) * dComplete1;
                                }
                            }
                            else
                            {
                                if (unitWorkIds.Length > 1)
                                {
                                    var unitWork = unitWorks.FirstOrDefault(x => x.UnitWorkId == workPackage1.UnitWorkId);
                                    if (unitWork != null && totalUnitWorkWeights > 0)
                                    {
                                        dComplete1 = Convert.ToDecimal((unitWork.Weights ?? 0) / totalUnitWorkWeights) * dComplete1;
                                    }
                                }
                            }
                            dCompleteTotal += dComplete1;
                        }
                        if (itemWeekSoptCheckDetails.Count() > 0)  //当周存在验收合格的记录
                        {
                            //工作包实际值
                            dCompleteWeek = Convert.ToDecimal(itemWeekSoptCheckDetails.Count()) / Convert.ToDecimal(item.CheckNum) * Convert.ToDecimal(item.Weights);
                            var workPackage1 = workPackages.FirstOrDefault(x => x.WorkPackageId == item.WorkPackageId);
                            if (workPackage1 != null)
                            {
                                //逐级递推计算权重计划值
                                dCompleteWeek = Convert.ToDecimal((workPackage1.Weights ?? 0) / 100) * Convert.ToDecimal(dCompleteWeek / 100);
                                var workPackage2 = workPackages.FirstOrDefault(x => x.WorkPackageId == workPackage1.SuperWorkPackageId);
                                if (workPackage2 != null)
                                {
                                    dCompleteWeek = Convert.ToDecimal((workPackage2.Weights ?? 0) / 100) * dCompleteWeek;
                                    var workPackage3 = workPackages.FirstOrDefault(x => x.WorkPackageId == workPackage2.SuperWorkPackageId);
                                    if (workPackage3 != null)
                                    {
                                        dCompleteWeek = Convert.ToDecimal((workPackage3.Weights ?? 0) / 100) * dCompleteWeek;
                                    }
                                }
                            }
                            if (unitWorkIds.Length == 1 && unitWorkIds[0] == BLL.Const._Null)   //按项目统计时，再乘以单位工程权重
                            {
                                var unitWork = unitWorks.FirstOrDefault(x => x.UnitWorkId == workPackage1.UnitWorkId);
                                if (unitWork != null)
                                {
                                    dCompleteWeek = Convert.ToDecimal((unitWork.Weights ?? 0) / 100) * dCompleteWeek;
                                }
                            }
                            else
                            {
                                if (unitWorkIds.Length > 1)
                                {
                                    var unitWork = unitWorks.FirstOrDefault(x => x.UnitWorkId == workPackage1.UnitWorkId);
                                    if (unitWork != null && totalUnitWorkWeights > 0)
                                    {
                                        dCompleteWeek = Convert.ToDecimal((unitWork.Weights ?? 0) / totalUnitWorkWeights) * dCompleteWeek;
                                    }
                                }
                            }
                            dComplete += dCompleteWeek;
                        }
                    }
                    row[1] = dPlan.ToString();   //计划值
                    if (dPlanTotal != lastDPlanTotal) //当期累计计划值不等于上月累计计划值时，再保存累计计划值
                    {
                        row[2] = dPlanTotal.ToString();   //累计计划值
                        for (int j = 0; j < tb.Rows.Count; j++)
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
                            row[2] = null;
                        }
                    }
                    lastDPlanTotal = dPlanTotal;
                    row[3] = dComplete.ToString();   //实际值
                    if (dCompleteTotal != lastDCompleteTotal) //当期累计实际值不等于上月累计实际值时，再保存累计实际值
                    {
                        row[4] = dCompleteTotal.ToString();   //累计实际值
                        for (int j = 0; j < tb.Rows.Count; j++)
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
                            row[4] = null;
                        }
                    }
                    lastDCompleteTotal = dCompleteTotal;
                    tb.Rows.Add(row);
                    startDay = startDay.AddDays(7);
                    w++;
                } while (startDay <= endDay);
            }
            #endregion
            #region 按天
            else
            {
                tb.Columns.Add("天");
                tb.Columns.Add("计划值");
                tb.Columns.Add("累计计划值");
                tb.Columns.Add("实际值");
                tb.Columns.Add("累计实际值");
                startDay = startDate;
                endDay = endDate;
                do
                {
                    endDayItem = startDay.AddDays(1);
                    if (endDayItem > endDate)
                    {
                        endDayItem = endDate;
                    }
                    DataRow row = tb.NewRow();
                    row[0] = string.Format("{0:MM-dd}", startDay);
                    //对应天的记录
                    decimal dPlan = 0, dPlan1 = 0, dPlanTotal = 0, dComplete = 0, dComplete1 = 0, dCompleteDay = 0, dCompleteTotal = 0;
                    //当天及之前所有工作包内容
                    var totalPlanCompleteControlItemAndCycles = controlItemAndCycles.Where(x => x.PlanCompleteDate < endDayItem);
                    //当天及之前所有验收合格记录
                    var totalSoptCheckDetails = soptCheckDetails.Where(x => x.SpotCheckDate < endDayItem);
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
                            if (unitWorkIds.Length == 1 && unitWorkIds[0] == BLL.Const._Null)   //按项目统计时，再乘以单位工程权重
                            {
                                var unitWork = unitWorks.FirstOrDefault(x => x.UnitWorkId == workPackage1.UnitWorkId);
                                if (unitWork != null)
                                {
                                    dPlan1 = Convert.ToDecimal((unitWork.Weights ?? 0) / 100) * dPlan1;
                                }
                            }
                            else
                            {
                                if (unitWorkIds.Length > 1)
                                {
                                    var unitWork = unitWorks.FirstOrDefault(x => x.UnitWorkId == workPackage1.UnitWorkId);
                                    if (unitWork != null && totalUnitWorkWeights > 0)
                                    {
                                        dPlan1 = Convert.ToDecimal((unitWork.Weights ?? 0) / totalUnitWorkWeights) * dPlan1;
                                    }
                                }
                            }
                        }
                        if (item.PlanCompleteDate >= startDay)   //当天计划完成记录
                        {
                            dPlan += dPlan1;   //累加当天值
                        }
                        dPlanTotal += dPlan1;   //累加累计值

                    }
                    foreach (var item in controlItemAndCycles)
                    {
                        //实际值
                        var itemSoptCheckDetails = soptCheckDetails.Where(x => x.ControlItemAndCycleId == item.ControlItemAndCycleId && x.SpotCheckDate < endDayItem);
                        var itemDaySoptCheckDetails = soptCheckDetails.Where(x => x.ControlItemAndCycleId == item.ControlItemAndCycleId && x.SpotCheckDate >= startDay && x.SpotCheckDate < endDayItem);
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
                            if (unitWorkIds.Length == 1 && unitWorkIds[0] == BLL.Const._Null)   //按项目统计时，再乘以单位工程权重
                            {
                                var unitWork = unitWorks.FirstOrDefault(x => x.UnitWorkId == workPackage1.UnitWorkId);
                                if (unitWork != null)
                                {
                                    dComplete1 = Convert.ToDecimal((unitWork.Weights ?? 0) / 100) * dComplete1;
                                }
                            }
                            else
                            {
                                if (unitWorkIds.Length > 1)
                                {
                                    var unitWork = unitWorks.FirstOrDefault(x => x.UnitWorkId == workPackage1.UnitWorkId);
                                    if (unitWork != null && totalUnitWorkWeights > 0)
                                    {
                                        dComplete1 = Convert.ToDecimal((unitWork.Weights ?? 0) / totalUnitWorkWeights) * dComplete1;
                                    }
                                }
                            }
                            dCompleteTotal += dComplete1;
                        }
                        if (itemDaySoptCheckDetails.Count() > 0)  //当天存在验收合格的记录
                        {
                            //工作包实际值
                            dCompleteDay = Convert.ToDecimal(itemDaySoptCheckDetails.Count()) / Convert.ToDecimal(item.CheckNum) * Convert.ToDecimal(item.Weights);
                            var workPackage1 = workPackages.FirstOrDefault(x => x.WorkPackageId == item.WorkPackageId);
                            if (workPackage1 != null)
                            {
                                //逐级递推计算权重计划值
                                dCompleteDay = Convert.ToDecimal((workPackage1.Weights ?? 0) / 100) * Convert.ToDecimal(dCompleteDay / 100);
                                var workPackage2 = workPackages.FirstOrDefault(x => x.WorkPackageId == workPackage1.SuperWorkPackageId);
                                if (workPackage2 != null)
                                {
                                    dCompleteDay = Convert.ToDecimal((workPackage2.Weights ?? 0) / 100) * dCompleteDay;
                                    var workPackage3 = workPackages.FirstOrDefault(x => x.WorkPackageId == workPackage2.SuperWorkPackageId);
                                    if (workPackage3 != null)
                                    {
                                        dCompleteDay = Convert.ToDecimal((workPackage3.Weights ?? 0) / 100) * dCompleteDay;
                                    }
                                }
                            }
                            if (unitWorkIds.Length == 1 && unitWorkIds[0] == BLL.Const._Null)   //按项目统计时，再乘以单位工程权重
                            {
                                var unitWork = unitWorks.FirstOrDefault(x => x.UnitWorkId == workPackage1.UnitWorkId);
                                if (unitWork != null)
                                {
                                    dCompleteDay = Convert.ToDecimal((unitWork.Weights ?? 0) / 100) * dCompleteDay;
                                }
                            }
                            else
                            {
                                if (unitWorkIds.Length > 1)
                                {
                                    var unitWork = unitWorks.FirstOrDefault(x => x.UnitWorkId == workPackage1.UnitWorkId);
                                    if (unitWork != null && totalUnitWorkWeights > 0)
                                    {
                                        dCompleteDay = Convert.ToDecimal((unitWork.Weights ?? 0) / totalUnitWorkWeights) * dCompleteDay;
                                    }
                                }
                            }
                            dComplete += dCompleteDay;
                        }
                    }
                    row[1] = dPlan.ToString();   //计划值
                    if (dPlanTotal != lastDPlanTotal) //当期累计计划值不等于上月累计计划值时，再保存累计计划值
                    {
                        row[2] = dPlanTotal.ToString();   //累计计划值
                        for (int j = 0; j < tb.Rows.Count; j++)
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
                            row[2] = null;
                        }
                    }
                    lastDPlanTotal = dPlanTotal;
                    row[3] = dComplete.ToString();   //实际值
                    if (dCompleteTotal != lastDCompleteTotal) //当期累计实际值不等于上月累计实际值时，再保存累计实际值
                    {
                        row[4] = dCompleteTotal.ToString();   //累计实际值
                        for (int j = 0; j < tb.Rows.Count; j++)
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
                            row[4] = null;
                        }
                    }
                    lastDCompleteTotal = dCompleteTotal;
                    tb.Rows.Add(row);
                    startDay = startDay.AddDays(1);
                } while (startDay <= endDay);
            }

            #endregion
            this.ChartEV.CreateMaryChart(tb, 1300, 580, null);
        }

        protected void drpProjectType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.drpUnitWork.Items.Clear();
            string[] array = this.drpProjectType.SelectedValueArray;
            List<string> str = new List<string>();
            List<Model.WBS_UnitWork> list = new List<Model.WBS_UnitWork>();
            List<Model.WBS_UnitWork> list1 = new List<Model.WBS_UnitWork>();
            if (array.Length == 1 && array[0] == BLL.Const._Null)
            {
                return;
            }
            if (array.Length == 0)
            {
                str.Add(BLL.Const._Null);
            }
            else
            {
                foreach (var item in array)
                {
                    if (item != BLL.Const._Null)
                    {
                        str.Add(item);
                        list = BLL.UnitWorkService.GetUnitWorkDownList(item, this.CurrUser.LoginProjectId);
                        for (int i = 0; i < list.Count; i++)
                        {
                            list1.Add(list[i]);
                        }
                    }
                }
            }

            ListItem[] ListItem = new ListItem[list1.Count()];
            for (int i = 0; i < list1.Count(); i++)
            {
                if (list1[i].ProjectType == "1")
                {
                    ListItem[i] = new ListItem(list1[i].UnitWorkName + "(建筑)", list1[i].UnitWorkId.ToString());
                }
                else
                {
                    ListItem[i] = new ListItem(list1[i].UnitWorkName + "(安装)", list1[i].UnitWorkId.ToString());
                }

            }
            this.drpUnitWork.DataTextField = "Text";
            this.drpUnitWork.DataValueField = "Value";
            this.drpUnitWork.DataSource = ListItem;
            this.drpUnitWork.DataBind();
            Funs.FineUIPleaseSelect(this.drpUnitWork);
            this.drpUnitWork.SelectedIndex = 0;
            this.drpProjectType.SelectedValueArray = str.ToArray();
            //UnitWorkService.InitUnitWorkDownList(drpUnitWork, this.CurrUser.LoginProjectId, true);
        }

        protected void drpUnitWork_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<string> str = new List<string>();
            string[] array = this.drpUnitWork.SelectedValueArray;
            if (array.Length == 1 && array[0] == BLL.Const._Null)
            {
                return;
            }
            if (array.Length == 0)
            {
                str.Add(BLL.Const._Null);
            }
            else
            {
                foreach (var item in array)
                {
                    if (item != BLL.Const._Null)
                    {
                        str.Add(item);
                    }
                }
            }
            this.drpUnitWork.SelectedValueArray = str.ToArray();
        }
    }
}