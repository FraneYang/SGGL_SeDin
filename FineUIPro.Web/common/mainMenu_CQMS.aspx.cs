using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web
{
    public partial class mainMenu_CQMS : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //  ClientScript.RegisterClientScriptBlock(typeof(Page), "", " category_One('one1', '项目质量验收一次合格率', " + new mainMenu_CQMS().One1 + ")", true);
            }
        }

        #region 项目质量验收一次合格率
        protected string One1
        {
            get
            {
                List<Model.View_Check_SoptCheckDetail> TotalCheckDetailOKLists = SpotCheckDetailService.GetTotalOKSpotCheckDetailListByTime1(CurrUser.LoginProjectId, DateTime.Now);
                List<Model.View_Check_SoptCheckDetail> TotalCheckDetailLists = SpotCheckDetailService.GetTotalAllSpotCheckDetailListByTime(CurrUser.LoginProjectId, DateTime.Now);
                double result = 0;
                if (TotalCheckDetailOKLists.Count > 0 && TotalCheckDetailLists.Count > 0)
                {
                    var a = Convert.ToDouble(TotalCheckDetailOKLists.Count);
                    var b = Convert.ToDouble(TotalCheckDetailLists.Count);
                    result = Convert.ToDouble(decimal.Round(decimal.Parse((a / b * 100).ToString()), 1));
                }
                return JsonConvert.SerializeObject(result);
            }
        }
        #endregion

        #region 项目施工资料同步率
        protected string One2
        {
            get
            {
                List<Model.View_Check_SoptCheckDetail> totalCheckDetailDataOKLists = SpotCheckDetailService.GetAllDataOkSpotCheckDetailListByTime(CurrUser.LoginProjectId, DateTime.Now);
                List<Model.View_Check_SoptCheckDetail> totalCheckDetailOKLists = SpotCheckDetailService.GetTotalOKSpotCheckDetailListByTime1(CurrUser.LoginProjectId, DateTime.Now);
                totalCheckDetailOKLists = totalCheckDetailOKLists.Where(x => x.IsShow == true).ToList();  //需要上传资料的IsShow为true
                double result = 0;
                if (totalCheckDetailDataOKLists.Count > 0 && totalCheckDetailOKLists.Count > 0)
                {
                    var a = Convert.ToDouble(totalCheckDetailDataOKLists.Count);
                    var b = Convert.ToDouble(totalCheckDetailOKLists.Count);
                    result = Convert.ToDouble(decimal.Round(decimal.Parse((a / b * 100).ToString()), 1));
                }
                return JsonConvert.SerializeObject(result);
            }
        }
        #endregion

        #region 项目质量问题整改完成率
        protected string One3
        {
            get
            {
                List<Model.View_Check_JointCheckDetail> totalCheckLists = JointCheckDetailService.GetTotalJointCheckDetailListByTime(CurrUser.LoginProjectId, DateTime.Now);
                int a = totalCheckLists.Where(x => x.OK == 1).Count();
                double result = 0;
                if (a > 0 && totalCheckLists.Count > 0)
                {
                    var b = Convert.ToDouble(totalCheckLists.Count);
                    result = Convert.ToDouble(decimal.Round(decimal.Parse((a / b * 100).ToString()), 1));
                }
                return JsonConvert.SerializeObject(result);
            }
        }
        #endregion

        #region 质量控制点通知
        protected string One4
        {
            get
            {
                var db = new Model.SGGLDB(Funs.ConnString);
                Model.Num num = new Model.Num();
                var controlItemAndCycles = (from x in db.WBS_ControlItemAndCycle
                                            where x.ProjectId == CurrUser.LoginProjectId && x.IsApprove == true
                                            orderby x.ControlItemAndCycleCode
                                            select x).ToList();
                var oKSpotCheckDetails = (from x in db.Check_SpotCheckDetail
                                          join y in db.Check_SpotCheck
                                          on x.SpotCheckCode equals y.SpotCheckCode
                                          where x.IsOK == true && y.ProjectId == CurrUser.LoginProjectId
                                          select x).ToList();
                //A类
                var Alist = controlItemAndCycles.Where(x => x.ControlPoint.Contains("A"));
                int a = 0;
                foreach (var item in Alist)
                {
                    if (item.CheckNum != 0)  //检查次数为0表示一直检查
                    {
                        List<Model.Check_SpotCheckDetail> details = oKSpotCheckDetails.Where(x => x.ControlItemAndCycleId == item.ControlItemAndCycleId).ToList();
                        if (details.Count == item.CheckNum)  //检查次数已达到最大值
                        {
                            a++;
                        }
                    }
                }
                num.num2 = a;
                num.num3 = Alist.Count();
                num.num1 = num.num3 - num.num2;
                //B类
                var Blist = controlItemAndCycles.Where(x => x.ControlPoint.Contains("B"));
                int b = 0;
                foreach (var item in Blist)
                {
                    if (item.CheckNum != 0)  //检查次数为0表示一直检查
                    {
                        List<Model.Check_SpotCheckDetail> details = oKSpotCheckDetails.Where(x => x.ControlItemAndCycleId == item.ControlItemAndCycleId).ToList();
                        if (details.Count == item.CheckNum)  //检查次数已达到最大值
                        {
                            b++;
                        }
                    }
                }
                num.num5 = b;
                num.num6 = Blist.Count();
                num.num4 = num.num6 - num.num5;
                //C类
                var Clist = controlItemAndCycles.Where(x => x.ControlPoint.Contains("C"));
                int c = 0;
                foreach (var item in Clist)
                {
                    if (item.CheckNum != 0)  //检查次数为0表示一直检查
                    {
                        List<Model.Check_SpotCheckDetail> details = oKSpotCheckDetails.Where(x => x.ControlItemAndCycleId == item.ControlItemAndCycleId).ToList();
                        if (details.Count == item.CheckNum)  //检查次数已达到最大值
                        {
                            c++;
                        }
                    }
                }
                num.num8 = c;
                num.num9 = Clist.Count();
                num.num7 = num.num9 - num.num8;
                return JsonConvert.SerializeObject(num);
            }
        }
        #endregion

        #region  质量问题统计
        protected string Two
        {
            get
            {
                List<Model.SingleSerie> series = new List<Model.SingleSerie>();
                Model.BusinessColumn businessColumn = new Model.BusinessColumn();
                List<string> listCategories = new List<string>();
                businessColumn.title = "质量问题统计";
                businessColumn.xFontNum = 8;
                var units = BLL.ProjectUnitService.GetProjectUnitListByProjectIdUnitType(CurrUser.LoginProjectId, BLL.Const.ProjectUnitType_2);
                var checks = JointCheckDetailService.GetTotalJointCheckDetailListByTime(CurrUser.LoginProjectId, DateTime.Now);
                Model.SingleSerie s = new Model.SingleSerie();
                Model.SingleSerie s2 = new Model.SingleSerie();
                List<double> listdata = new List<double>();
                List<double> listdata2 = new List<double>();
                int okNum = 0;
                foreach (var unit in units)
                {
                    listCategories.Add(BLL.UnitService.GetShortUnitNameByUnitId(unit.UnitId));
                    var unitChecks = checks.Where(x => x.UnitId == unit.UnitId);
                    okNum = unitChecks.Where(x => x.OK == 1).Count();
                    listdata.Add(unitChecks.Count() - okNum);
                    listdata2.Add(okNum);
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

        protected string Two2
        {
            get
            {
                List<Model.SingleSerie> series = new List<Model.SingleSerie>();
                Model.BusinessColumn businessColumn = new Model.BusinessColumn();
                List<string> listCategories = new List<string>();
                businessColumn.title = "质量问题统计";
                businessColumn.xFontNum = 5;
                var unitWorks = BLL.UnitWorkService.GetUnitWorkLists(CurrUser.LoginProjectId);
                unitWorks = unitWorks.Where(x => x.ProjectType == "2").ToList();
                var checks = JointCheckDetailService.GetTotalJointCheckDetailListByTime(CurrUser.LoginProjectId, DateTime.Now);
                Model.SingleSerie s = new Model.SingleSerie();
                Model.SingleSerie s2 = new Model.SingleSerie();
                List<double> listdata = new List<double>();
                List<double> listdata2 = new List<double>();
                int okNum = 0;
                foreach (var unitWork in unitWorks)
                {
                    listCategories.Add(unitWork.UnitWorkName);
                    var unitChecks = checks.Where(x => x.UnitWorkId == unitWork.UnitWorkId);
                    okNum = unitChecks.Where(x => x.OK == 1).Count();
                    listdata.Add(unitChecks.Count() - okNum);
                    listdata2.Add(okNum);
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

        protected string Two3
        {
            get
            {
                List<Model.SingleSerie> series = new List<Model.SingleSerie>();
                Model.BusinessColumn businessColumn = new Model.BusinessColumn();
                List<string> listCategories = new List<string>();
                businessColumn.title = "质量问题统计";
                businessColumn.xFontNum = 5;
                var cns = BLL.CNProfessionalService.GetList();
                var checks = JointCheckDetailService.GetTotalJointCheckDetailListByTime(CurrUser.LoginProjectId, DateTime.Now);
                Model.SingleSerie s = new Model.SingleSerie();
                Model.SingleSerie s2 = new Model.SingleSerie();
                List<double> listdata = new List<double>();
                List<double> listdata2 = new List<double>();
                int okNum = 0;
                foreach (var cn in cns)
                {
                    listCategories.Add(cn.ProfessionalName);
                    var unitChecks = checks.Where(x => x.CNProfessionalCode == cn.CNProfessionalId);
                    okNum = unitChecks.Where(x => x.OK == 1).Count();
                    listdata.Add(unitChecks.Count() - okNum);
                    listdata2.Add(okNum);
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

        protected string Two4
        {
            get
            {
                List<Model.SingleSerie> series = new List<Model.SingleSerie>();
                Model.BusinessColumn businessColumn = new Model.BusinessColumn();
                List<string> listCategories = new List<string>();
                businessColumn.title = "质量问题统计";
                businessColumn.xFontNum = 5;
                var unitWorks = BLL.UnitWorkService.GetUnitWorkLists(CurrUser.LoginProjectId);
                unitWorks = unitWorks.Where(x => x.ProjectType == "1").ToList();
                var checks = JointCheckDetailService.GetTotalJointCheckDetailListByTime(CurrUser.LoginProjectId, DateTime.Now);
                Model.SingleSerie s = new Model.SingleSerie();
                Model.SingleSerie s2 = new Model.SingleSerie();
                List<double> listdata = new List<double>();
                List<double> listdata2 = new List<double>();
                int okNum = 0;
                foreach (var unitWork in unitWorks)
                {
                    listCategories.Add(unitWork.UnitWorkName);
                    var unitChecks = checks.Where(x => x.UnitWorkId == unitWork.UnitWorkId);
                    okNum = unitChecks.Where(x => x.OK == 1).Count();
                    listdata.Add(unitChecks.Count() - okNum);
                    listdata2.Add(okNum);
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

        #region  质量一次合格率/资料同步率
        protected string Three
        {
            get
            {
                List<Model.SingleSerie> series = new List<Model.SingleSerie>();
                Model.BusinessColumn businessColumn = new Model.BusinessColumn();
                List<string> listCategories = new List<string>();
                businessColumn.title = "质量一次合格率";
                var units = BLL.ProjectUnitService.GetProjectUnitListByProjectIdUnitType(CurrUser.LoginProjectId, BLL.Const.ProjectUnitType_2);
                List<Model.View_Check_SoptCheckDetail> TotalCheckDetailOKLists = SpotCheckDetailService.GetTotalOKSpotCheckDetailListByTime1(CurrUser.LoginProjectId, DateTime.Now);
                List<Model.View_Check_SoptCheckDetail> TotalCheckDetailLists = SpotCheckDetailService.GetTotalAllSpotCheckDetailListByTime(CurrUser.LoginProjectId, DateTime.Now);
                List<Model.View_Check_SoptCheckDetail> totalCheckDetailDataOKLists = SpotCheckDetailService.GetAllDataOkSpotCheckDetailListByTime(CurrUser.LoginProjectId, DateTime.Now);
                Model.SingleSerie s = new Model.SingleSerie();
                Model.SingleSerie s2 = new Model.SingleSerie();
                List<double> listdata = new List<double>();
                List<double> listdata2 = new List<double>();
                double result = 0, result2 = 0;
                foreach (var unit in units)
                {
                    listCategories.Add(BLL.UnitService.GetShortUnitNameByUnitId(unit.UnitId));
                    var okChecks = TotalCheckDetailOKLists.Where(x => x.UnitId == unit.UnitId).ToList();
                    var totalChecks = TotalCheckDetailLists.Where(x => x.UnitId == unit.UnitId).ToList();
                    if (okChecks.Count > 0 && totalChecks.Count > 0)
                    {
                        var a = Convert.ToDouble(okChecks.Count);
                        var b = Convert.ToDouble(totalChecks.Count);
                        result = Convert.ToDouble(decimal.Round(decimal.Parse((a / b * 100).ToString()), 1));
                    }
                    var dataOkChecks = totalCheckDetailDataOKLists.Where(x => x.UnitId == unit.UnitId).ToList();
                    okChecks = okChecks.Where(x => x.IsShow == true).ToList();  //需要上传资料的IsShow为true
                    if (dataOkChecks.Count > 0 && okChecks.Count > 0)
                    {
                        var a = Convert.ToDouble(dataOkChecks.Count);
                        var b = Convert.ToDouble(okChecks.Count);
                        result2 = Convert.ToDouble(decimal.Round(decimal.Parse((a / b * 100).ToString()), 1));
                    }
                    listdata.Add(result);
                    listdata2.Add(result2);
                    result = 0;
                    result2 = 0;
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

        protected string Three2
        {
            get
            {
                List<Model.SingleSerie> series = new List<Model.SingleSerie>();
                Model.BusinessColumn businessColumn = new Model.BusinessColumn();
                List<string> listCategories = new List<string>();
                businessColumn.title = "质量一次合格率";
                var cns = BLL.CNProfessionalService.GetList();
                List<Model.View_Check_SoptCheckDetail> TotalCheckDetailOKLists = SpotCheckDetailService.GetTotalOKSpotCheckDetailListByTime1(CurrUser.LoginProjectId, DateTime.Now);
                List<Model.View_Check_SoptCheckDetail> TotalCheckDetailLists = SpotCheckDetailService.GetTotalAllSpotCheckDetailListByTime(CurrUser.LoginProjectId, DateTime.Now);
                List<Model.View_Check_SoptCheckDetail> totalCheckDetailDataOKLists = SpotCheckDetailService.GetAllDataOkSpotCheckDetailListByTime(CurrUser.LoginProjectId, DateTime.Now);
                Model.SingleSerie s = new Model.SingleSerie();
                Model.SingleSerie s2 = new Model.SingleSerie();
                List<double> listdata = new List<double>();
                List<double> listdata2 = new List<double>();
                double result = 0, result2 = 0;
                foreach (var cn in cns)
                {
                    listCategories.Add(cn.ProfessionalName);
                    var okChecks = TotalCheckDetailOKLists.Where(x => x.CNProfessionalCode == cn.CNProfessionalId).ToList();
                    var totalChecks = TotalCheckDetailLists.Where(x => x.CNProfessionalCode == cn.CNProfessionalId).ToList();
                    if (okChecks.Count > 0 && totalChecks.Count > 0)
                    {
                        var a = Convert.ToDouble(okChecks.Count);
                        var b = Convert.ToDouble(totalChecks.Count);
                        result = Convert.ToDouble(decimal.Round(decimal.Parse((a / b * 100).ToString()), 1));
                    }
                    var dataOkChecks = totalCheckDetailDataOKLists.Where(x => x.CNProfessionalCode == cn.CNProfessionalId).ToList();
                    okChecks = okChecks.Where(x => x.IsShow == true).ToList();  //需要上传资料的IsShow为true
                    if (dataOkChecks.Count > 0 && okChecks.Count > 0)
                    {
                        var a = Convert.ToDouble(dataOkChecks.Count);
                        var b = Convert.ToDouble(okChecks.Count);
                        result2 = Convert.ToDouble(decimal.Round(decimal.Parse((a / b * 100).ToString()), 1));
                    }
                    listdata.Add(result);
                    listdata2.Add(result2);
                    result = 0;
                    result2 = 0;
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

        protected string Four
        {
            get
            {
                List<Model.SingleSerie> series = new List<Model.SingleSerie>();
                Model.BusinessColumn businessColumn = new Model.BusinessColumn();
                List<string> listCategories = new List<string>();
                businessColumn.title = "质量一次合格率";
                var unitWorks = BLL.UnitWorkService.GetUnitWorkLists(CurrUser.LoginProjectId);
                unitWorks = unitWorks.Where(x => x.ProjectType == "2").ToList();
                List<Model.View_Check_SoptCheckDetail> TotalCheckDetailOKLists = SpotCheckDetailService.GetTotalOKSpotCheckDetailListByTime1(CurrUser.LoginProjectId, DateTime.Now);
                List<Model.View_Check_SoptCheckDetail> TotalCheckDetailLists = SpotCheckDetailService.GetTotalAllSpotCheckDetailListByTime(CurrUser.LoginProjectId, DateTime.Now);
                List<Model.View_Check_SoptCheckDetail> totalCheckDetailDataOKLists = SpotCheckDetailService.GetAllDataOkSpotCheckDetailListByTime(CurrUser.LoginProjectId, DateTime.Now);
                Model.SingleSerie s = new Model.SingleSerie();
                Model.SingleSerie s2 = new Model.SingleSerie();
                List<double> listdata = new List<double>();
                List<double> listdata2 = new List<double>();
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
                    var dataOkChecks = totalCheckDetailDataOKLists.Where(x => x.UnitWorkId == unitWork.UnitWorkId).ToList();
                    okChecks = okChecks.Where(x => x.IsShow == true).ToList();  //需要上传资料的IsShow为true
                    if (dataOkChecks.Count > 0 && okChecks.Count > 0)
                    {
                        var a = Convert.ToDouble(dataOkChecks.Count);
                        var b = Convert.ToDouble(okChecks.Count);
                        result2 = Convert.ToDouble(decimal.Round(decimal.Parse((a / b * 100).ToString()), 1));
                    }
                    listdata.Add(result);
                    listdata2.Add(result2);
                    result = 0;
                    result2 = 0;
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

        protected string Four2
        {
            get
            {
                List<Model.SingleSerie> series = new List<Model.SingleSerie>();
                Model.BusinessColumn businessColumn = new Model.BusinessColumn();
                List<string> listCategories = new List<string>();
                businessColumn.title = "质量一次合格率";
                var unitWorks = BLL.UnitWorkService.GetUnitWorkLists(CurrUser.LoginProjectId);
                unitWorks = unitWorks.Where(x => x.ProjectType == "1").ToList();
                List<Model.View_Check_SoptCheckDetail> TotalCheckDetailOKLists = SpotCheckDetailService.GetTotalOKSpotCheckDetailListByTime1(CurrUser.LoginProjectId, DateTime.Now);
                List<Model.View_Check_SoptCheckDetail> TotalCheckDetailLists = SpotCheckDetailService.GetTotalAllSpotCheckDetailListByTime(CurrUser.LoginProjectId, DateTime.Now);
                List<Model.View_Check_SoptCheckDetail> totalCheckDetailDataOKLists = SpotCheckDetailService.GetAllDataOkSpotCheckDetailListByTime(CurrUser.LoginProjectId, DateTime.Now);
                Model.SingleSerie s = new Model.SingleSerie();
                Model.SingleSerie s2 = new Model.SingleSerie();
                List<double> listdata = new List<double>();
                List<double> listdata2 = new List<double>();
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
                    var dataOkChecks = totalCheckDetailDataOKLists.Where(x => x.UnitWorkId == unitWork.UnitWorkId).ToList();
                    okChecks = okChecks.Where(x => x.IsShow == true).ToList();  //需要上传资料的IsShow为true
                    if (dataOkChecks.Count > 0 && okChecks.Count > 0)
                    {
                        var a = Convert.ToDouble(dataOkChecks.Count);
                        var b = Convert.ToDouble(okChecks.Count);
                        result2 = Convert.ToDouble(decimal.Round(decimal.Parse((a / b * 100).ToString()), 1));
                    }
                    listdata.Add(result);
                    listdata2.Add(result2);
                    result = 0;
                    result2 = 0;
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
    }
}