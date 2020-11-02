using BLL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FineUIPro.Web.common
{
    public partial class main : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                /// 获取安全人工时
                getPersonWorkTime();

                #region 项目数据
                var getAllProject = ProjectService.GetAllProjectDropDownList();
                int acount = getAllProject.Count();
                int pcount1 = 0;
                int pcount2 = 0;
                int pcount3 = 0;
                if (acount > 0)
                {
                    pcount1 = getAllProject.Where(x => x.ProjectState == "1" || x.ProjectState == null).Count();
                    pcount2 = getAllProject.Where(x => x.ProjectState == "2").Count();
                    pcount3 = getAllProject.Where(x => x.ProjectState == "3").Count();
                }
                this.numProjetcA.InnerHtml = acount.ToString();
                ///在建
                this.numProjetc1.InnerHtml = pcount1.ToString();
                this.numProjetc2.InnerHtml = pcount2.ToString();
                this.numProjetc3.InnerHtml = pcount3.ToString();

                string projectHtml = string.Empty;
                var workProjects = BLL.ProjectService.GetProjectWorkList();
                foreach (var item in workProjects)
                {
                    projectHtml += "<div>" + item.ProjectName + " </div>";
                }
                this.divProjectList.InnerHtml = projectHtml;
                #endregion
                #region 进度统计
                this.divJD.InnerHtml = "<div class='flex tab-h'><div class='txt'>工地名称</div><div class='txt'>状态</div><div class='flex1' style='text-align: center'>进度</div></div>";
                decimal dComplete1 = 0, dCompleteTotal = 0;
                foreach (var p in workProjects)
                {
                    dComplete1 = 0;
                    dCompleteTotal = 0;
                    var unitWorks = BLL.UnitWorkService.GetUnitWorkLists(p.ProjectId);
                    var workPackages = BLL.WorkPackageService.GetAllWorkPackagesByProjectId(p.ProjectId);
                    var controlItemAndCycles = BLL.ControlItemAndCycleService.GetControlItemAndCyclesByProjectIdAndDate(p.ProjectId, DateTime.Now);
                    var soptCheckDetails = BLL.SpotCheckDetailService.GetViewSpotCheckDetailsByProjectIdAndDate(p.ProjectId, DateTime.Now, string.Empty);
                    foreach (var item in controlItemAndCycles)
                    {
                        //实际值
                        var itemSoptCheckDetails = soptCheckDetails.Where(x => x.ControlItemAndCycleId == item.ControlItemAndCycleId && x.SpotCheckDate < DateTime.Now);
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
                    }
                    dCompleteTotal = dCompleteTotal * 100;
                    this.divJD.InnerHtml += "<div class='flex tab-i'><div class='txt'>" + p.ShortName + "</div><div class='txt'>在建</div><div class='flex1 flex line-wrap'><div class='line-item'><div class='normal' style='width: " + dCompleteTotal + "%'></div></div></div></div>";
                }
                #endregion
            }
        }

        #region  项目数量
        protected string ProjectNum
        {
            get
            {
                Model.SingleSerie series = new Model.SingleSerie();
                var getAllProject = ProjectService.GetAllProjectDropDownList();
                List<double> listdata = new List<double>();
                listdata.Add(getAllProject.Count());
                listdata.Add(getAllProject.Where(x => x.Province == "上海").Count());
                listdata.Add(getAllProject.Where(x => x.Province == "河北").Count());
                listdata.Add(getAllProject.Where(x => x.Province == "山西").Count());
                listdata.Add(getAllProject.Where(x => x.Province == "内蒙古").Count());
                listdata.Add(getAllProject.Where(x => x.Province == "辽宁").Count());
                listdata.Add(getAllProject.Where(x => x.Province == "吉林").Count());
                listdata.Add(getAllProject.Where(x => x.Province == "黑龙江").Count());
                listdata.Add(getAllProject.Where(x => x.Province == "江苏").Count());
                listdata.Add(getAllProject.Where(x => x.Province == "浙江").Count());
                listdata.Add(getAllProject.Where(x => x.Province == "安徽").Count());
                listdata.Add(getAllProject.Where(x => x.Province == "福建").Count());
                listdata.Add(getAllProject.Where(x => x.Province == "江西").Count());
                listdata.Add(getAllProject.Where(x => x.Province == "山东").Count());
                listdata.Add(getAllProject.Where(x => x.Province == "河南").Count());
                listdata.Add(getAllProject.Where(x => x.Province == "湖北").Count());
                listdata.Add(getAllProject.Where(x => x.Province == "湖南").Count());
                listdata.Add(getAllProject.Where(x => x.Province == "广东").Count());
                listdata.Add(getAllProject.Where(x => x.Province == "广西").Count());
                listdata.Add(getAllProject.Where(x => x.Province == "海南").Count());
                listdata.Add(getAllProject.Where(x => x.Province == "四川").Count());
                listdata.Add(getAllProject.Where(x => x.Province == "贵州").Count());
                listdata.Add(getAllProject.Where(x => x.Province == "云南").Count());
                listdata.Add(getAllProject.Where(x => x.Province == "西藏").Count());
                listdata.Add(getAllProject.Where(x => x.Province == "陕西").Count());
                listdata.Add(getAllProject.Where(x => x.Province == "甘肃").Count());
                listdata.Add(getAllProject.Where(x => x.Province == "青海").Count());
                listdata.Add(getAllProject.Where(x => x.Province == "宁夏").Count());
                listdata.Add(getAllProject.Where(x => x.Province == "新疆").Count());
                listdata.Add(getAllProject.Where(x => x.Province == "北京").Count());
                listdata.Add(getAllProject.Where(x => x.Province == "天津").Count());
                listdata.Add(getAllProject.Where(x => x.Province == "重庆").Count());
                listdata.Add(getAllProject.Where(x => x.Province == "香港").Count());
                listdata.Add(getAllProject.Where(x => x.Province == "澳门").Count());
                series.data = listdata;
                return JsonConvert.SerializeObject(series);
            }
        }
        #endregion

        #region 安全人工时
        /// <summary>
        /// 获取安全人工时
        /// </summary>
        private void getPersonWorkTime()
        {
            var getMax = from x in Funs.DB.SitePerson_PersonInOutNumber
                         group x by x.ProjectId into g
                         select new { g.First().ProjectId, WorkHours = g.Max(x => x.WorkHours) };
            int wHours = getMax.Sum(x => x.WorkHours) ?? 0;
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
            var getRectify = Funs.DB.Check_RectifyNotices;
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

        protected string swiper_One
        {
            get
            {
                var getNotice = (from x in Funs.DB.InformationProject_Notice
                                 where x.IsRelease == true
                                 orderby x.ReleaseDate
                                 select x.NoticeTitle).Distinct().Take(20);
                string strNoticeHtml = string.Empty;
                foreach (var item in getNotice)
                {
                    strNoticeHtml += "<li data-id=\"http://www.baidu.com\" class=\"c-item swiper-slide\"><div class=\"tit\" title=\"" + item + "\">" + item + "</div></li>";
                }
                return "<ul class=\"content-ul swiper-wrapper\">" + strNoticeHtml + "</ul>";
            }
        }

        protected string swiper_Two
        {
            get
            {
                var getNotice = (from x in Funs.DB.InformationProject_ReceiveFileManager
                                 orderby x.GetFileDate
                                 select x.ReceiveFileName).Distinct().Take(20);
                string strNoticeHtml = string.Empty;
                foreach (var item in getNotice)
                {
                    strNoticeHtml += "<li class=\"c-item swiper-slide\"><div class=\"tit\" title=\"" + item + "\">" + item + "</div></li>";
                }
                return "<ul class=\"content-ul swiper-wrapper\">" + strNoticeHtml + "</ul>";
            }
        }
        #region  质量一次验收合格率
        protected string Two
        {
            get
            {
                List<Model.SingleSerie> series = new List<Model.SingleSerie>();
                Model.BusinessColumn businessColumn = new Model.BusinessColumn();
                List<string> listCategories = new List<string>();
                businessColumn.title = "质量一次验收合格率";
                var projects = BLL.ProjectService.GetProjectWorkList();
                Model.SingleSerie s = new Model.SingleSerie();
                //Model.SingleSerie s2 = new Model.SingleSerie();
                List<double> listdata = new List<double>();
                //List<double> listdata2 = new List<double>();
                double result = 0, result2 = 0;
                foreach (var project in projects)
                {
                    listCategories.Add(project.ShortName);
                    List<Model.View_Check_SoptCheckDetail> TotalCheckDetailOKLists = SpotCheckDetailService.GetTotalOKSpotCheckDetailListByTime1(project.ProjectId, DateTime.Now);
                    List<Model.View_Check_SoptCheckDetail> TotalCheckDetailLists = SpotCheckDetailService.GetTotalAllSpotCheckDetailListByTime(project.ProjectId, DateTime.Now);
                    //List<Model.View_Check_SoptCheckDetail> totalCheckDetailDataOKLists = SpotCheckDetailService.GetAllDataOkSpotCheckDetailListByTime(project.ProjectId, DateTime.Now);
                    if (TotalCheckDetailOKLists.Count > 0 && TotalCheckDetailLists.Count > 0)
                    {
                        var a = Convert.ToDouble(TotalCheckDetailOKLists.Count);
                        var b = Convert.ToDouble(TotalCheckDetailLists.Count);
                        result = Convert.ToDouble(decimal.Round(decimal.Parse((a / b * 100).ToString()), 1));
                    }
                    //if (totalCheckDetailDataOKLists.Count > 0 && TotalCheckDetailOKLists.Count > 0)
                    //{
                    //    var a = Convert.ToDouble(totalCheckDetailDataOKLists.Count);
                    //    var b = Convert.ToDouble(TotalCheckDetailOKLists.Count);
                    //    result2 = Convert.ToDouble(decimal.Round(decimal.Parse((a / b * 100).ToString()), 1));
                    //}
                    listdata.Add(result);
                    //listdata2.Add(result2);
                    result = 0;
                    result2 = 0;
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

        #region  焊接一次合格率统计
        protected string Three
        {
            get
            {
                List<Model.SingleSerie> series = new List<Model.SingleSerie>();
                Model.BusinessColumn businessColumn = new Model.BusinessColumn();
                List<string> listCategories = new List<string>();
                businessColumn.title = "焊接一次合格率统计";
                var projects = BLL.ProjectService.GetProjectWorkList();
                Model.SingleSerie s = new Model.SingleSerie();
                //Model.SingleSerie s2 = new Model.SingleSerie();
                List<double> listdata = new List<double>();
                //List<double> listdata2 = new List<double>();
                double result = 0;
                Model.SGGLDB db = Funs.DB;
                foreach (var project in projects)
                {
                    listCategories.Add(project.ShortName);
                    //一次检测合格焊口数
                    int oneCheckJotNum = (from x in db.HJGL_Batch_NDEItem
                                          join y in db.HJGL_Batch_BatchTrustItem on x.TrustBatchItemId equals y.TrustBatchItemId
                                          join z in db.HJGL_Batch_PointBatchItem on y.PointBatchItemId equals z.PointBatchItemId
                                          join a in db.HJGL_Batch_NDE on x.NDEID equals a.NDEID
                                          where z.PointDate != null && z.PointState == "1" && y.RepairRecordId == null && a.ProjectId == project.ProjectId
                                          select x.NDEItemID).Count();
                    //一次检测返修焊口数
                    int oneCheckRepairJotNum = (from x in db.HJGL_Batch_NDEItem
                                                join y in db.HJGL_Batch_BatchTrustItem on x.TrustBatchItemId equals y.TrustBatchItemId
                                                join z in db.HJGL_Batch_PointBatchItem on y.PointBatchItemId equals z.PointBatchItemId
                                                join a in db.HJGL_Batch_NDE on x.NDEID equals a.NDEID
                                                where z.PointDate != null && z.PointState == "1" && y.RepairRecordId == null && x.CheckResult == "2" && a.ProjectId == project.ProjectId
                                                select x.NDEItemID).Count();
                    if (oneCheckJotNum > 0)
                    {
                        var a = Convert.ToDouble(oneCheckJotNum - oneCheckRepairJotNum);
                        var b = Convert.ToDouble(oneCheckJotNum);
                        result = Convert.ToDouble(decimal.Round(decimal.Parse((a / b * 100).ToString()), 1));
                    }
                    listdata.Add(result);
                    //listdata2.Add(result2);
                    result = 0;
                }
                s.data = listdata;
                series.Add(s);
                businessColumn.categories = listCategories;
                businessColumn.series = series;
                return JsonConvert.SerializeObject(businessColumn);
            }
        }
        #endregion

        protected string CQMSData
        {
            get
            {
                List<Model.SingleSerie> series = new List<Model.SingleSerie>();
                Model.BusinessColumn businessColumn = new Model.BusinessColumn();
                List<string> listCategories = new List<string>();
                List<string> listLegend = new List<string>();
                businessColumn.title = "质量验收一次合格率";
                var projects = BLL.ProjectService.GetAllProjectDropDownList();
                Model.SingleSerie s = new Model.SingleSerie();
                List<double> listdata = new List<double>();
                double i = 0.4;
                foreach (var project in projects)
                {
                    listCategories.Add(project.ShortName);
                    listdata.Add(i * 100);
                    i = i + 0.1;
                }
                s.name = "质量验收一次合格率";
                s.type = "bar";
                s.data = listdata;
                series.Add(s);
                //listLegend.Add("分包一");
                //listLegend.Add("分包二");
                //listCategories.Add("分包一");
                //listCategories.Add("分包二");
                //Model.SingleSerie s1 = new Model.SingleSerie();
                //List<double> listdata1 = new List<double>();
                //listdata1.Add(0.85);
                //listdata1.Add(0.82);
                //s1.name = "质量验收一次合格率";
                //s1.type = "bar";
                //s1.data = listdata1;
                //Model.SingleSerie s2 = new Model.SingleSerie();
                //List<double> listdata2 = new List<double>();
                //listdata2.Add(0.69);
                //listdata2.Add(0.65);
                //s2.name = "施工资料同步率";
                //s2.type = "bar";
                //s2.data = listdata2;
                //series.Add(s1);
                //series.Add(s2);
                businessColumn.legend = listLegend;
                businessColumn.categories = listCategories;
                businessColumn.series = series;
                return JsonConvert.SerializeObject(businessColumn);
            }
        }

        protected string JDGLData
        {
            get
            {
                List<Model.SingleSerie> series = new List<Model.SingleSerie>();
                Model.BusinessColumn businessColumn = new Model.BusinessColumn();
                List<string> listCategories = new List<string>();
                List<string> listLegend = new List<string>();
                businessColumn.title = "施工进度";
                var projects = BLL.ProjectService.GetAllProjectDropDownList();
                Model.SingleSerie s = new Model.SingleSerie();
                List<double> listdata = new List<double>();
                double i = 0.5;
                foreach (var project in projects)
                {
                    listCategories.Add(project.ShortName);
                    listdata.Add(i);
                    i = i + 0.1;
                }
                s.name = "施工进度";
                s.type = "bar";
                s.data = listdata;
                series.Add(s);
                //listLegend.Add("分包一");
                //listLegend.Add("分包二");
                //listCategories.Add("分包一");
                //listCategories.Add("分包二");
                //Model.SingleSerie s1 = new Model.SingleSerie();
                //List<double> listdata1 = new List<double>();
                //listdata1.Add(0.85);
                //listdata1.Add(0.82);
                //s1.name = "质量验收一次合格率";
                //s1.type = "bar";
                //s1.data = listdata1;
                //Model.SingleSerie s2 = new Model.SingleSerie();
                //List<double> listdata2 = new List<double>();
                //listdata2.Add(0.69);
                //listdata2.Add(0.65);
                //s2.name = "施工资料同步率";
                //s2.type = "bar";
                //s2.data = listdata2;
                //series.Add(s1);
                //series.Add(s2);
                businessColumn.legend = listLegend;
                businessColumn.categories = listCategories;
                businessColumn.series = series;
                return JsonConvert.SerializeObject(businessColumn);
            }
        }
    }
}
