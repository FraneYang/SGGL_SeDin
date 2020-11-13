using BLL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace FineUIPro.Web.common
{
    public partial class mainProject : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ///项目概况
                #region 项目概况
                var project = ProjectService.GetProjectByProjectId(this.CurrUser.LoginProjectId);
                if (project != null)
                {
                    this.divProjectName.InnerHtml = project.ProjectName;
                    this.divProjectAddress.InnerHtml = project.ProjectAddress;
                    this.divProjectType.InnerHtml = ProjectTypeService.GetProjectTypeNameById(project.ProjectType);
                    if (project.ProjectState == Const.ProjectState_2)
                    {
                        this.divProjectstate.InnerHtml = "停工";
                    }
                    else if (project.ProjectState == BLL.Const.ProjectState_3)
                    {
                        this.divProjectstate.InnerHtml = "竣工";
                    }
                    else
                    {
                        this.divProjectstate.InnerHtml = "在建";
                    }
                    this.divStartDate.InnerHtml = string.Format("{0:yyyy-MM-dd}", project.StartDate);
                    this.divEndDate.InnerHtml = string.Format("{0:yyyy-MM-dd}", project.EndDate);
                    this.divDuration.InnerHtml = project.Duration.ToString();
                    this.divOwnUnit.InnerHtml = ProjectService.getProjectUnitNameByUnitType(project.ProjectId, Const.ProjectUnitType_4);
                    this.divJLUnit.InnerHtml = ProjectService.getProjectUnitNameByUnitType(project.ProjectId, Const.ProjectUnitType_3);
                    var getName = ProjectService.getProjectUnitNameByUnitType(project.ProjectId, Const.ProjectUnitType_2);
                    if (!string.IsNullOrEmpty(getName))
                    {
                        this.divSGUnit.InnerHtml = getName.Replace(",", "</br>");
                    }

                    this.divProjectManager.InnerHtml = ProjectService.GetProjectManagerName(project.ProjectId);
                    this.divConstructionManager.InnerHtml = ProjectService.GetConstructionManagerName(project.ProjectId);
                    this.divHSSEManager.InnerHtml = ProjectService.GetHSSEManagerName(project.ProjectId);
                    this.divProjectMoney.InnerHtml = project.ProjectMoney.ToString();
                    this.divConstructionMoney.InnerHtml = project.ConstructionMoney.ToString();
                }
                #endregion

                /// 获取安全人工时
                getPersonWorkTime();
                ///劳务统计
                getSitePerson();
            }
        }

        #region 安全人工时
        /// <summary>
        /// 获取安全人工时
        /// </summary>
        private void getPersonWorkTime()
        {
            int wHours = Funs.DB.SitePerson_PersonInOutNumber.Where(x => x.ProjectId == this.CurrUser.LoginProjectId).Max(x => x.WorkHours) ?? 0;
            if (wHours > 0)
            {
                this.divPNum1.InnerHtml = (wHours % 10).ToString();
                this.divPNum2.InnerHtml = ((wHours % 100) / 10).ToString();
                this.divPNum3.InnerHtml = ((wHours % 1000) / 100).ToString();
                this.divPNum4.InnerHtml = ((wHours % 10000) / 1000).ToString();
                this.divPNum5.InnerHtml = ((wHours % 100000) / 10000).ToString();
                this.divPNum6.InnerHtml = ((wHours % 1000000) / 100000).ToString();
                this.divPNum7.InnerHtml = ((wHours % 10000000) / 1000000).ToString();
                this.divPNum8.InnerHtml = ((wHours % 100000000) / 10000000).ToString();
            }
            ///整改单          
            var getRectify = Funs.DB.Check_RectifyNotices.Where(x => x.ProjectId == this.CurrUser.LoginProjectId);
            int allcout = getRectify.Count();
            if (allcout > 0)
            {
                this.divAllRectify.InnerHtml = allcout.ToString();
                int ccount = getRectify.Where(x => x.States == "5").Count();
                this.divCRectify.InnerHtml = ccount.ToString();
                this.divUCRectify.InnerHtml = (allcout - ccount).ToString();
            }
        }
        #endregion

        #region 劳务统计
        /// <summary>
        ///  劳务统计
        /// </summary>
        private void getSitePerson()
        {
            int AllCount = 0;
            int MCount = 0;
            var getallin = APIPageDataService.getPersonNum(this.CurrUser.LoginProjectId, DateTime.Now);
            AllCount = getallin.Count();
            if (AllCount > 0)
            {
                MCount = getallin.Where(x => x.PostType == Const.PostType_1).Count();
            }

            if (AllCount > 0)
            {
                ////总人数
                this.person00.InnerHtml = ((AllCount % 1000) / 100).ToString();
                this.person01.InnerHtml = ((AllCount % 100) / 10).ToString();
                this.person02.InnerHtml = (AllCount % 10).ToString();

                if (MCount > 0)
                {
                    /////管理人数
                    this.person20.InnerHtml = ((MCount % 1000) / 100).ToString();
                    this.person21.InnerHtml = ((MCount % 100) / 10).ToString();
                    this.person22.InnerHtml = (MCount % 10).ToString();
                }

                /////作业人数
                int WCount = AllCount - MCount;
                if (WCount > 0)
                {
                    this.person10.InnerHtml = ((WCount % 1000) / 100).ToString();
                    this.person11.InnerHtml = ((WCount % 100) / 10).ToString();
                    this.person12.InnerHtml = (WCount % 10).ToString();
                }
            }
        }
        #endregion

        #region  质量一次验收合格率
        protected string Two
        {
            get
            {
                List<Model.SingleSerie> series = new List<Model.SingleSerie>();
                Model.BusinessColumn businessColumn = new Model.BusinessColumn();
                List<string> listCategories = new List<string>();
                businessColumn.title = "质量一次验收合格率";
                var unitWorks = BLL.UnitWorkService.GetUnitWorkLists(CurrUser.LoginProjectId);
                unitWorks = unitWorks.Where(x => x.ProjectType == "1").ToList();
                List<Model.View_Check_SoptCheckDetail> TotalCheckDetailOKLists = SpotCheckDetailService.GetTotalOKSpotCheckDetailListByTime1(CurrUser.LoginProjectId, DateTime.Now);
                List<Model.View_Check_SoptCheckDetail> TotalCheckDetailLists = SpotCheckDetailService.GetTotalAllSpotCheckDetailListByTime(CurrUser.LoginProjectId, DateTime.Now);
                Model.SingleSerie s = new Model.SingleSerie();
                //Model.SingleSerie s2 = new Model.SingleSerie();
                List<double> listdata = new List<double>();
                //List<double> listdata2 = new List<double>();
                double result = 0, result2 = 0;
                foreach (var unitWork in unitWorks)
                {
                    listCategories.Add(unitWork.UnitWorkName);
                    var okChecks = TotalCheckDetailOKLists.Where(x => x.UnitWorkId == unitWork.UnitWorkId).ToList();
                    var totalChecks = TotalCheckDetailLists.Where(x => x.UnitWorkId == unitWork.UnitWorkId).ToList();
                    if (okChecks.Count > 0 && totalChecks.Count > 0)
                    {
                        var a = Convert.ToDouble(okChecks.Count);
                        var b = Convert.ToDouble(totalChecks.Count);
                        result = Convert.ToDouble(decimal.Round(decimal.Parse((a / b * 100).ToString()), 1));
                    }
                    //var dataOkChecks = totalCheckDetailDataOKLists.Where(x => x.UnitWorkId == unitWork.UnitWorkId).ToList();
                    //if (dataOkChecks.Count > 0 && okChecks.Count > 0)
                    //{
                    //    var a = Convert.ToDouble(dataOkChecks.Count);
                    //    var b = Convert.ToDouble(okChecks.Count);
                    //    result2 = Convert.ToDouble(decimal.Round(decimal.Parse((a / b * 100).ToString()), 1));
                    //}
                    listdata.Add(result);
                    //listdata2.Add(result2);
                    result = 0;
                    //result2 = 0;
                }
                s.data = listdata;
                //s2.data = listdata2;
                series.Add(s);
                //series.Add(s2);
                businessColumn.categories = listCategories;
                businessColumn.series = series;
                return JsonConvert.SerializeObject(businessColumn);
            }
        }

        protected string Two2
        {
            get
            {
                List<Model.SingleSerie> series = new List<Model.SingleSerie>();
                Model.BusinessColumn businessColumn = new Model.BusinessColumn();
                List<string> listCategories = new List<string>();
                businessColumn.title = "质量一次验收合格率";
                var unitWorks = BLL.UnitWorkService.GetUnitWorkLists(CurrUser.LoginProjectId);
                unitWorks = unitWorks.Where(x => x.ProjectType == "2").ToList();
                List<Model.View_Check_SoptCheckDetail> TotalCheckDetailOKLists = SpotCheckDetailService.GetTotalOKSpotCheckDetailListByTime1(CurrUser.LoginProjectId, DateTime.Now);
                List<Model.View_Check_SoptCheckDetail> TotalCheckDetailLists = SpotCheckDetailService.GetTotalAllSpotCheckDetailListByTime(CurrUser.LoginProjectId, DateTime.Now);
                Model.SingleSerie s = new Model.SingleSerie();
                //Model.SingleSerie s2 = new Model.SingleSerie();
                List<double> listdata = new List<double>();
                //List<double> listdata2 = new List<double>();
                double result = 0, result2 = 0;
                foreach (var unitWork in unitWorks)
                {
                    listCategories.Add(unitWork.UnitWorkName);
                    var okChecks = TotalCheckDetailOKLists.Where(x => x.UnitWorkId == unitWork.UnitWorkId).ToList();
                    var totalChecks = TotalCheckDetailLists.Where(x => x.UnitWorkId == unitWork.UnitWorkId).ToList();
                    if (okChecks.Count > 0 && totalChecks.Count > 0)
                    {
                        var a = Convert.ToDouble(okChecks.Count);
                        var b = Convert.ToDouble(totalChecks.Count);
                        result = Convert.ToDouble(decimal.Round(decimal.Parse((a / b * 100).ToString()), 1));
                    }
                    //var dataOkChecks = totalCheckDetailDataOKLists.Where(x => x.UnitWorkId == unitWork.UnitWorkId).ToList();
                    //if (dataOkChecks.Count > 0 && okChecks.Count > 0)
                    //{
                    //    var a = Convert.ToDouble(dataOkChecks.Count);
                    //    var b = Convert.ToDouble(okChecks.Count);
                    //    result2 = Convert.ToDouble(decimal.Round(decimal.Parse((a / b * 100).ToString()), 1));
                    //}
                    listdata.Add(result);
                    //listdata2.Add(result2);
                    result = 0;
                    //result2 = 0;
                }
                s.data = listdata;
                //s2.data = listdata2;
                series.Add(s);
                //series.Add(s2);
                businessColumn.categories = listCategories;
                businessColumn.series = series;
                return JsonConvert.SerializeObject(businessColumn);
            }
        }
        #endregion

        #region 焊接一次合格率
        protected string Three1
        {
            get
            {
                Model.SingleSerie series = new Model.SingleSerie();
                Model.BusinessColumn businessColumn = new Model.BusinessColumn();
                List<double> listdata = new List<double>();
                double result = 0;
                Model.SGGLDB db = Funs.DB;
                //一次检测合格焊口数
                int oneCheckJotNum = (from x in db.HJGL_Batch_NDEItem
                                      join y in db.HJGL_Batch_BatchTrustItem on x.TrustBatchItemId equals y.TrustBatchItemId
                                      join z in db.HJGL_Batch_PointBatchItem on y.PointBatchItemId equals z.PointBatchItemId
                                      join a in db.HJGL_Batch_NDE on x.NDEID equals a.NDEID
                                      where z.PointDate != null && z.PointState == "1" && y.RepairRecordId == null && a.ProjectId == this.CurrUser.LoginProjectId
                                      select x.NDEItemID).Count();
                //一次检测返修焊口数
                int oneCheckRepairJotNum = (from x in db.HJGL_Batch_NDEItem
                                            join y in db.HJGL_Batch_BatchTrustItem on x.TrustBatchItemId equals y.TrustBatchItemId
                                            join z in db.HJGL_Batch_PointBatchItem on y.PointBatchItemId equals z.PointBatchItemId
                                            join a in db.HJGL_Batch_NDE on x.NDEID equals a.NDEID
                                            where z.PointDate != null && z.PointState == "1" && y.RepairRecordId == null && x.CheckResult == "2" && a.ProjectId == this.CurrUser.LoginProjectId
                                            select x.NDEItemID).Count();
                if (oneCheckJotNum > 0)
                {
                    var a = Convert.ToDouble(oneCheckJotNum - oneCheckRepairJotNum);
                    var b = Convert.ToDouble(oneCheckJotNum);
                    result = Convert.ToDouble(decimal.Round(decimal.Parse((a / b * 100).ToString()), 0));
                }
                listdata.Add(result);
                series.name = result + "%";
                series.data = listdata;
                return JsonConvert.SerializeObject(series);
            }
        }
        #endregion

        #region 焊接进度完成率
        protected string Three2
        {
            get
            {
                Model.SingleSerie series = new Model.SingleSerie();
                Model.BusinessColumn businessColumn = new Model.BusinessColumn();
                List<double> listdata = new List<double>();
                double result = 0;
                Model.SGGLDB db = Funs.DB;
                //项目焊口数
                int allJotNum = (from x in db.HJGL_WeldJoint
                                 where x.ProjectId == this.CurrUser.LoginProjectId
                                 select x).Count();
                //项目已焊焊口数
                int weldJotNum = (from x in db.HJGL_WeldJoint
                                  where x.ProjectId == this.CurrUser.LoginProjectId && x.WeldingDailyId != null
                                  select x).Count();
                if (allJotNum > 0)
                {
                    var a = Convert.ToDouble(weldJotNum);
                    var b = Convert.ToDouble(allJotNum);
                    result = Convert.ToDouble(decimal.Round(decimal.Parse((a / b * 100).ToString()), 0));
                }
                listdata.Add(result);
                series.name = result + "%";
                series.data = listdata;
                return JsonConvert.SerializeObject(series);
            }
        }
        #endregion

        #region 进度统计
        protected string Four
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
                endDate = DateTime.Now.Date.AddDays(1).AddSeconds(-1);
                if (project.EndDate != null && DateTime.Now > project.EndDate)
                {
                    endDate = project.EndDate.Value.AddDays(1).AddSeconds(-1);
                }
                startDate = endDate.AddMonths(-6);
                List<Model.WBS_UnitWork> unitWorks = new List<Model.WBS_UnitWork>();
                List<Model.WBS_WorkPackage> workPackages = new List<Model.WBS_WorkPackage>();
                List<Model.WBS_ControlItemAndCycle> controlItemAndCycles = new List<Model.WBS_ControlItemAndCycle>();
                List<Model.View_Check_SoptCheckDetail> soptCheckDetails = new List<Model.View_Check_SoptCheckDetail>();
                unitWorks = BLL.UnitWorkService.GetUnitWorkLists(this.CurrUser.LoginProjectId);
                workPackages = BLL.WorkPackageService.GetAllWorkPackagesByProjectId(this.CurrUser.LoginProjectId);
                controlItemAndCycles = BLL.ControlItemAndCycleService.GetControlItemAndCyclesByProjectIdAndDate(this.CurrUser.LoginProjectId, endDate);
                soptCheckDetails = BLL.SpotCheckDetailService.GetViewSpotCheckDetailsByProjectIdAndDate(this.CurrUser.LoginProjectId, endDate, isDataOK);

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

        /// <summary>
        /// 获取项目照片
        /// </summary>
        protected string projectSitePhoto
        {
            get
            {
                string photo = "../images/sedinsite.jpg";
                var getMap = (from x in Funs.DB.InformationProject_ProjectMap
                              where x.MapType == "1" && x.ProjectId == this.CurrUser.LoginProjectId
                              orderby x.UploadDate descending
                              select x).FirstOrDefault();
                if (getMap != null)
                {
                    var geturl = Funs.DB.AttachFile.FirstOrDefault(x => x.ToKeyId == getMap.ProjectMapId);
                    if (geturl != null && !string.IsNullOrEmpty(geturl.AttachUrl))
                    {
                        photo = "../" + geturl.AttachUrl;
                    }
                }
                return "\"" + photo + "\"";
            }
        }

        protected int TodoNum;

        protected string swiper_One
        {
            get
            {
                //安全
                var getDataList = Funs.DB.Sp_APP_GetToDoItems(this.CurrUser.LoginProjectId, this.CurrUser.UserId).ToList(); ;
                string strNoticeHtml = string.Empty;
                //质量
                SqlParameter[] val = new SqlParameter[]
                {
                    new SqlParameter("@UserId", this.CurrUser.UserId),
                    new SqlParameter("@ProjectId", this.CurrUser.LoginProjectId)
                 };
                var dt = BLL.SQLHelper.GetDataTableRunProc("SpAuditingManageByProjectId", val).AsEnumerable().ToArray();
                TodoNum = getDataList.Count + dt.Count();
                if (TodoNum >= 8)
                {
                    foreach (var item in getDataList)
                    {
                        strNoticeHtml += "<li data-id=\"" + item.PCUrl + "\" class=\"c-item swiper-slide\"><div class=\"tit\" title=\"" + item.MenuName + "\">" + item.Content + "</div></li>";
                    }
                    foreach (var item in dt)
                    {
                        strNoticeHtml += "<li data-id=\"" + item.ItemArray[2].ToString() + "\" class=\"c-item swiper-slide\"><div class=\"tit\" title=\"" + item.ItemArray[1].ToString() + "\">" + item.ItemArray[1].ToString() + "</div></li>";
                    }
                }
                else
                {
                    if (TodoNum > 0)
                    {
                        foreach (var item in getDataList)
                        {
                            strNoticeHtml += "<li data-id=\"" + item.PCUrl + "\" class=\"c-item swiper-slide\"><div class=\"tit\" title=\"" + item.MenuName + "\">" + item.Content + "</div></li>";
                        }
                        foreach (var item in dt)
                        {
                            strNoticeHtml += "<li data-id=\"" + item.ItemArray[2].ToString() + "\" class=\"c-item swiper-slide\"><div class=\"tit\" title=\"" + item.ItemArray[1].ToString() + "\">" + item.ItemArray[1].ToString() + "</div></li>";
                        }
                        int addRowNum = 8 - TodoNum;
                        for (int i = 0; i < addRowNum; i++)
                        {
                            strNoticeHtml += "<li data-id=\"\" class=\"c-item disabled swiper-slide\"><div class=\"tit\" title=\"\"></div></li>";
                        }
                    }
                }
                return "<ul class=\"content-ul swiper-wrapper\">" + strNoticeHtml + "</ul>";
            }
        }

        protected int WarnNum;

        protected string swiper_Two
        {
            get
            {
                var getperson0 = APIPersonService.getPersonQualityByProjectIdUnitId(this.CurrUser.LoginProjectId, this.CurrUser.UnitId, "0");
                var getperson1 = APIPersonService.getPersonQualityByProjectIdUnitId(this.CurrUser.LoginProjectId, this.CurrUser.UnitId, "1");
                SqlParameter[] val = new SqlParameter[]
                {
                    new SqlParameter("@ProjectId", this.CurrUser.LoginProjectId)
                };
                var dt = BLL.SQLHelper.GetDataTableRunProc("SpEnableCueQualityByPrject", val).AsEnumerable().ToArray();
                WarnNum = getperson0.Count + getperson1.Count + dt.Count();
                string strNoticeHtml = string.Empty;
                string url = "../HSSE/QualityAudit/PersonQualityEdit.aspx?PersonId=";
                string cqmsUrl = "../CQMS/Check/EditCheckEquipment.aspx?see=see&CheckEquipmentId=";
                if (WarnNum >= 8)
                {
                    foreach (var item in getperson0)
                    {
                        string pur = url + item.PersonId;
                        string strT = item.UnitName + "[" + item.PersonName + "]：" + item.CertificateName + "。证书已过期";
                        strNoticeHtml += "<li data-id=\"" + pur + "\" class=\"c-item swiper-slide\"><div class=\"tit\" title=\"" + strT + "\">" + item.PersonName + "：" + item.CertificateName + "。证书已过期" + "</div></li>";
                    }
                    foreach (var item in getperson1)
                    {
                        string pur = url + item.PersonId;
                        string strT = item.UnitName + "[" + item.PersonName + "]：" + item.CertificateName + "。证书待过期";
                        strNoticeHtml += "<li data-id=\"" + url + "\" class=\"c-item swiper-slide\"><div class=\"tit\" title=\"" + strT + "\">" + item.PersonName + "：" + item.CertificateName + "。证书待过期" + "</div></li>";
                    }
                    foreach (var item in dt)
                    {
                        string pur = cqmsUrl + item.ItemArray[0].ToString();
                        strNoticeHtml += "<li data-id=\"" + pur + "\" class=\"c-item swiper-slide\"><div class=\"tit\" title=\"" + item.ItemArray[1].ToString() + "\">" + item.ItemArray[1].ToString() + "</div></li>";
                    }
                }
                else
                {
                    if (WarnNum > 0)
                    {
                        foreach (var item in getperson0)
                        {
                            string pur = url + item.PersonId;
                            string strT = item.UnitName + "[" + item.PersonName + "]：" + item.CertificateName + "。证书已过期";
                            strNoticeHtml += "<li data-id=\"" + pur + "\" class=\"c-item swiper-slide\"><div class=\"tit\" title=\"" + strT + "\">" + item.PersonName + "：" + item.CertificateName + "。证书已过期" + "</div></li>";
                        }
                        foreach (var item in getperson1)
                        {
                            string pur = url + item.PersonId;
                            string strT = item.UnitName + "[" + item.PersonName + "]：" + item.CertificateName + "。证书待过期";
                            strNoticeHtml += "<li data-id=\"" + url + "\" class=\"c-item swiper-slide\"><div class=\"tit\" title=\"" + strT + "\">" + item.PersonName + "：" + item.CertificateName + "。证书待过期" + "</div></li>";
                        }
                        foreach (var item in dt)
                        {
                            string pur = cqmsUrl + item.ItemArray[0].ToString();
                            strNoticeHtml += "<li data-id=\"" + pur + "\" class=\"c-item swiper-slide\"><div class=\"tit\" title=\"" + item.ItemArray[1].ToString() + "\">" + item.ItemArray[1].ToString() + "</div></li>";
                        }
                        int addRowNum = 8 - WarnNum;
                        for (int i = 0; i < addRowNum; i++)
                        {
                            strNoticeHtml += "<li data-id=\"\" class=\"c-item disabled swiper-slide\"><div class=\"tit\" title=\"\"></div></li>";
                        }
                    }
                }

                return "<ul class=\"content-ul swiper-wrapper\">" + strNoticeHtml + "</ul>";
            }
        }

        protected string swiper_Three
        {
            get
            {
                var getNotice = (from x in Funs.DB.InformationProject_Notice
                                 where x.IsRelease == true && x.AccessProjectId.Contains(this.CurrUser.LoginProjectId)
                                 orderby x.ReleaseDate
                                 select x).Distinct().Take(20);
                var readIds = from x in Funs.DB.Sys_UserRead where x.UserId == this.CurrUser.UserId select x.DataId;
                string strNoticeHtml = string.Empty;
                foreach (var item in getNotice)
                {
                    string url = "../Notice/NoticeView2.aspx?NoticeId=" + item.NoticeId;
                    var attachFile = BLL.AttachFileService.GetAttachFile(item.NoticeId, BLL.Const.ServerNoticeMenuId);
                    if (attachFile != null && !string.IsNullOrEmpty(attachFile.AttachUrl))
                    {
                        url = "../" + attachFile.AttachUrl.Split(',')[0];
                    }
                    if (!readIds.Contains(item.NoticeId))
                    {
                        strNoticeHtml += "<li data-id=\"" + url + "\" class=\"c-item swiper-slide\"><div class=\"tit\" title=\"" + item.NoticeTitle + "\"><div class=\"flex\" ><div class=\"tit-t flex1\">" + item.NoticeTitle + "</div><div class=\"tit-v\">" + string.Format("{0:yyyy-MM-dd}", item.CompileDate) + "</div></div></div></li>";
                    }
                    else
                    {
                        strNoticeHtml += "<li data-id=\"" + url + "\" class=\"c-item disabled swiper-slide\"><div class=\"tit tit-read\" title=\"" + item.NoticeTitle + "\"><div class=\"flex\" ><div class=\"tit-t flex1\">" + item.NoticeTitle + "</div><div class=\"tit-v\">" + string.Format("{0:yyyy-MM-dd}", item.CompileDate) + "</div></div></div></li>";
                    }
                }
                return "<ul class=\"content-ul swiper-wrapper\">" + strNoticeHtml + "</ul>";
            }
        }
    }
}
