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

                string str = CQMSData;
            }
        }

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
