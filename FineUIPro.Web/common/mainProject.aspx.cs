using BLL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
                        this.divSGUnit.InnerHtml = getName.Replace(",","</br>");
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
    }
}
