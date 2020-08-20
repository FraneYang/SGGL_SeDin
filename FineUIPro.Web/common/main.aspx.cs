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
                    pcount2 = getAllProject.Where(x => x.ProjectState == "2" ).Count();
                    pcount3 = getAllProject.Where(x => x.ProjectState == "3").Count();
                }
                this.numProjetcA.InnerHtml = acount.ToString();
                ///在建
                this.numProjetc1.InnerHtml = pcount1.ToString();
                this.numProjetc2.InnerHtml = pcount2.ToString();
                this.numProjetc3.InnerHtml = pcount3.ToString();

                string projectHtml = string.Empty;
                foreach (var item in getAllProject)
                {
                    projectHtml += "<div>" + item.ProjectName + " </div>";
                }
                this.divProjectList.InnerHtml = projectHtml;
                #endregion
                //string str = CQMSData; 
                #region 通知
                var getNotice = (from x in Funs.DB.InformationProject_Notice
                                where x.IsRelease == true
                                orderby x.ReleaseDate
                                select x.NoticeTitle).Distinct().Take(20);
                string strNoticeHtml = string.Empty;
                foreach (var item in getNotice)
                {
                    strNoticeHtml+= "<li class=\"c-item swiper-slide\"><div class=\"tit\">" + item + "</div></li>";
                }
                this.swiper2.InnerHtml= "<ul class=\"content-ul swiper-wrapper\">" + strNoticeHtml + "</ul>";
                #endregion
            }
        }

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

        #region  质量一次验收合格率
        protected string Two
        {
            get
            {
                List<Model.SingleSerie> series = new List<Model.SingleSerie>();
                Model.BusinessColumn businessColumn = new Model.BusinessColumn();
                List<string> listCategories = new List<string>();
                businessColumn.title = "质量一次验收合格率";
                var projects = BLL.ProjectService.GetAllProjectDropDownList();
                Model.SingleSerie s = new Model.SingleSerie();
                //Model.SingleSerie s2 = new Model.SingleSerie();
                List<double> listdata = new List<double>();
                //List<double> listdata2 = new List<double>();
                double result = 0, result2 = 0;
                foreach (var project in projects)
                {
                    listCategories.Add(project.ProjectCode);
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
                    listdata.Add(i*100);
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
