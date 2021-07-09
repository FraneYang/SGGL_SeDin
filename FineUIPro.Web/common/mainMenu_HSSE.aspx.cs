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
                ///当前现场总人数
                getSitePerson();
                var project = BLL.ProjectService.GetProjectByProjectId(this.CurrUser.LoginProjectId);
                if (project != null && project.Telephone != null)
                {
                    this.xmb.InnerHtml = project.Telephone;
                }
            }
        }

        #region 当前现场总人数
        /// <summary>
        ///  当前现场总人数
        /// </summary>
        private void getSitePerson()
        {
           
            var getallin = APIPageDataService.getPersonNum(this.CurrUser.LoginProjectId, DateTime.Now);
            int AllCount = getallin.Count();
            if (AllCount > 0)
            {
                ////总人数
                this.divperson.InnerHtml= ((AllCount % 10000) / 1000).ToString();
                this.person00.InnerHtml = ((AllCount % 1000) / 100).ToString();
                this.person01.InnerHtml = ((AllCount % 100) / 10).ToString();
                this.person02.InnerHtml = (AllCount % 10).ToString();
            }
        }
        #endregion
        
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

                Model.SingleSerie s2 = new Model.SingleSerie();
                List<double> listdata2= new List<double>();
                var getMonts= SitePerson_MonthReportService.getMonthReports(CurrUser.LoginProjectId,null);            
                foreach (var month in getMonts.OrderBy(x=>x.CompileDate))
                {
                    listCategories.Add(string.Format("{0:yyyy-MM}", month.CompileDate));                    
                    listdata.Add(double.Parse((month.TotalPersonWorkTime ?? 0).ToString()));
                    listdata2.Add(double.Parse((month.DayWorkTime ?? 0).ToString()));
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

        #region 作业许可数量统计
        /// <summary>
        ///  作业许可数量统计
        /// </summary>
        protected string Three
        {
            get
            {
                List<Model.SingleSerie> series = new List<Model.SingleSerie>();
                Model.BusinessColumn businessColumn = new Model.BusinessColumn();
                List<string> listCategories = new List<string>();
                businessColumn.title = "作业许可数量统计";
                Model.SingleSerie s = new Model.SingleSerie();
                List<double> listdata = new List<double>();
                var getStates = LicensePublicService.drpStatesItem().Where(x=>x.Value != Const._Null);
                var getLicense = APILicenseDataService.getLicenseDataListByStates(this.CurrUser.LoginProjectId, Const.UnitId_SEDIN, null,0);
                if (getLicense != null)
                {
                    foreach (var itemStates in getStates)
                    {
                        listCategories.Add(itemStates.Text);
                        listdata.Add(getLicense.Where(x => x.States == itemStates.Value).Count());
                    }
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
                    listCategories.Add(unit.ShortUnitName);
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

        #region 入场安全培训
        /// <summary>
        ///  入场安全培训
        /// </summary>
        protected string Five
        {
            get
            {
                List<Model.SingleSerie> series = new List<Model.SingleSerie>();
                Model.BusinessColumn businessColumn = new Model.BusinessColumn();
                List<string> listCategories = new List<string>();
                businessColumn.title = "入场培训";
                Model.SingleSerie s = new Model.SingleSerie();
                Model.SingleSerie s2 = new Model.SingleSerie();
                //// 每月培训数量
                List<double> listdata = new List<double>();
                List<double> listdata2 = new List<double>();
                var getTrainRecord = from x in Funs.DB.EduTrain_TrainRecord
                                     where x.ProjectId == this.CurrUser.LoginProjectId && x.TrainTypeId == Const.EntryTrainTypeId
                                     select x;
                var getTrainRecordDetail = from x in Funs.DB.EduTrain_TrainRecordDetail
                                           join y in getTrainRecord on x.TrainingId equals y.TrainingId
                                           select x;
                DateTime startTime = DateTime.Now;
                DateTime endTime = DateTime.Now;
                var getProject = ProjectService.GetProjectByProjectId(this.CurrUser.LoginProjectId);
                if (getProject != null && getProject.StartDate.HasValue)
                {
                    startTime = getProject.StartDate.Value;
                    if (getProject.EndDate.HasValue && getProject.EndDate < DateTime.Now)
                    {
                        endTime = getProject.EndDate.Value;
                    }
                }
                int totalCout = 0;
                for (int i = 0; startTime.AddMonths(i) <= endTime; i++)
                {
                    listCategories.Add(string.Format("{0:yyyy-MM}", startTime.AddMonths(i)));
                    
                    var getMontDetail = from x in getTrainRecordDetail
                                        join y in getTrainRecord on x.TrainingId equals y.TrainingId
                                        where y.TrainStartDate.Value.Year == startTime.AddMonths(i).Year && y.TrainStartDate.Value.Month == startTime.AddMonths(i).Month
                                        select x;
                    listdata.Add(getMontDetail.Count());
                    totalCout = totalCout + getMontDetail.Count();
                    listdata2.Add(totalCout);
                }

                s.data = listdata;
                s2.data = listdata2;
                series.Add(s);
                series.Add(s2);
                businessColumn.categories = listCategories;
                businessColumn.series = series;
                return JsonConvert.SerializeObject(businessColumn);
            }
        }
        #endregion

        # region 事故统计
        /// <summary>
        ///  事故统计
        /// </summary>
        protected string Six
        {
            get
            {
                List<Model.SingleSerie> series = new List<Model.SingleSerie>();
                Model.BusinessColumn businessColumn = new Model.BusinessColumn();
             
                Model.SingleSerie s = new Model.SingleSerie();
                List<double> listdata = new List<double>();

                businessColumn.title = "事故统计";
                var getAccident = from x in Funs.DB.Accident_AccidentReport
                                  where x.ProjectId == this.CurrUser.LoginProjectId
                                  select x;
           
                listdata.Add(getAccident.Where(x => x.AccidentTypeId == "1" || x.AccidentTypeId == "2" || x.AccidentTypeId == "3" || x.AccidentTypeId == "4").Count());
                listdata.Add(getAccident.Where(x => x.AccidentTypeId == "5").Count());
                listdata.Add(getAccident.Where(x => x.AccidentTypeId == "6" || x.AccidentTypeId == "7").Count());
                listdata.Add(getAccident.Where(x => x.AccidentTypeId == "8" || x.AccidentTypeId == "9").Count());
                listdata.Add(getAccident.Where(x => x.AccidentTypeId == "10").Count());
                listdata.Add(getAccident.Where(x => x.AccidentTypeId == "11").Count());

                s.data = listdata;
                series.Add(s);
                businessColumn.series = series;
                return JsonConvert.SerializeObject(businessColumn);
            }
        }
        #endregion
    }
}