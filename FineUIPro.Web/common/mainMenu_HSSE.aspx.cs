using BLL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FineUIPro.Web
{
    public partial class mainMenu_HSSE : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                getHazardRegisterLists= HSSE_Hazard_HazardRegisterService.GetHazardRegisterListByProjectId(this.CurrUser.LoginProjectId);
            }
        }

        #region 项目安全人工时
        /// <summary>
        ///  项目安全人工时
        /// </summary>
        protected string Two
        {
            get
            {
                List<Model.SingleSerie> series = new List<Model.SingleSerie>();
                Model.BusinessColumn businessColumn = new Model.BusinessColumn();
                List<string> listCategories = new List<string>();
                businessColumn.title = "项目安全人工时";
                Model.SingleSerie s = new Model.SingleSerie();
                List<double> listdata = new List<double>();           
                var getMonts= SitePerson_MonthReportService.getMonthReports(CurrUser.LoginProjectId, null);            
                foreach (var month in getMonts.OrderBy(x=>x.CompileDate))
                {
                    listCategories.Add(string.Format("{0:yyyy-MM}", month.CompileDate));                    
                    listdata.Add(double.Parse((month.DayWorkTime ?? 0).ToString()));
                }
                s.data = listdata;
                series.Add(s);
                businessColumn.categories = listCategories;
                businessColumn.series = series;
                return JsonConvert.SerializeObject(businessColumn);
            }
        }
        #endregion

        #region  安全检查问题统计
        /// <summary>
        /// 
        /// </summary>
        public static List<Model.HSSE_Hazard_HazardRegister> getHazardRegisterLists;

        /// <summary>
        /// 按单位统计
        /// </summary>
        protected string Four1
        {
            get
            {
                List<Model.SingleSerie> series = new List<Model.SingleSerie>();
                Model.BusinessColumn businessColumn = new Model.BusinessColumn();
                List<string> listCategories = new List<string>();
                Model.SingleSerie s = new Model.SingleSerie();
                Model.SingleSerie s2 = new Model.SingleSerie();
                List<double> listdata = new List<double>();
                List<double> listdata2 = new List<double>();
                businessColumn.title = "安全检查问题统计";
                var units = UnitService.GetUnitByProjectIdUnitTypeList(this.CurrUser.LoginProjectId, BLL.Const.ProjectUnitType_2);                
                foreach (var unit in units)
                {
                    listCategories.Add(unit.UnitName);
                    var unitHazardRegisters = getHazardRegisterLists.Where(x => x.ResponsibleUnit == unit.UnitId);
                    var noW = unitHazardRegisters.Where(x => x.States !="3");
                    listdata.Add(unitHazardRegisters.Count() - noW.Count());
                    listdata2.Add(unitHazardRegisters.Count());
                }

                s.data = listdata;
                series.Add(s);

                s2.data = listdata2;
                series.Add(s2);
                businessColumn.categories = listCategories;
                businessColumn.series = series;
                return JsonConvert.SerializeObject(businessColumn);
            }
        }

        /// <summary>
        /// 按类型统计
        /// </summary>
        protected string Four2
        {
            get
            {
                List<Model.SingleSerie> series = new List<Model.SingleSerie>();
                Model.BusinessColumn businessColumn = new Model.BusinessColumn();
                List<string> listCategories = new List<string>();
                Model.SingleSerie s = new Model.SingleSerie();
                List<double> listdata = new List<double>();
                Model.SingleSerie s2 = new Model.SingleSerie();
                List<double> listdata2 = new List<double>();
                businessColumn.title = "安全检查问题统计";
                var getTypes = HSSE_Hazard_HazardRegisterTypesService.GetHazardRegisterTypesList("1");
                foreach (var item in getTypes)
                {
                    listCategories.Add(item.RegisterTypesName);
                    var unitHazardRegisters = getHazardRegisterLists.Where(x => x.RegisterTypesId == item.RegisterTypesId);
                    var noW = unitHazardRegisters.Where(x => x.States != "3");
                    listdata.Add(unitHazardRegisters.Count() - noW.Count());
                    listdata2.Add(unitHazardRegisters.Count());
                }

                s.data = listdata;
                series.Add(s);
                s2.data = listdata2;
                series.Add(s2);
                businessColumn.categories = listCategories;
                businessColumn.series = series;
                return JsonConvert.SerializeObject(businessColumn);
            }
        }
        #endregion
    }
}