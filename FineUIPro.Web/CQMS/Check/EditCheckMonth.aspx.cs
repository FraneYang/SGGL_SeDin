using BLL;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FineUIPro.Web.CQMS.Check
{
    public partial class EditCheckMonth : PageBase
    {
        /// <summary>
        /// 检查月报主键
        /// </summary>
        public string CheckMonthId
        {
            get
            {
                return (string)ViewState["CheckMonthId"];
            }
            set
            {
                ViewState["CheckMonthId"] = value;
            }
        }
        /// <summary>
        /// 明细集合
        /// </summary>
        private static List<Model.Check_MonthSpotCheckDetail> monthSpotCheckDetails = new List<Model.Check_MonthSpotCheckDetail>();
        private static DateTime startTime;

        private static DateTime endTime;

        protected void Page_Load(object sender, EventArgs e)
        {

            CheckMonthId = Request.Params["CheckMonthId"];
            BLL.MainItemService.InitGridMainItemDownList(drpMainItemId, this.CurrUser.LoginProjectId, false);//主项
            DesignProfessionalService.InitDesignProfessional(drpDesignProfessionalId, false);//专业
            if (!IsPostBack)
            {
                monthSpotCheckDetails.Clear();
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                if (!string.IsNullOrEmpty(Request.Params["see"]))
                {
                    btnSave.Visible = false;
                    txtManagementOverview.Readonly = true;
                    txtAccidentSituation.Readonly = true;
                    txtConstructionData.Readonly = true;
                    txtNextMonthPlan.Readonly = true;
                    txtNeedSolved.Readonly = true;
                    txtMonthOk.Readonly = true;
                    txtMonthDataOk.Readonly = true;
                    txtAllOk.Readonly = true;
                    txtAllDataOk.Readonly = true;
                    this.Toolbar3.Hidden = true;
                    this.GridDesign.Columns[6].Hidden = true;
                }
                if (!string.IsNullOrEmpty(Request.Params["months"]))
                {
                    Model.Project_Sys_Set CheckMonthStartDay = BLL.Project_SysSetService.GetSysSetBySetName("月报开始日期", this.CurrUser.LoginProjectId);
                    Model.Project_Sys_Set CheckMonthEndDay = BLL.Project_SysSetService.GetSysSetBySetName("月报结束日期", this.CurrUser.LoginProjectId);
                    DateTime fromPageMonths = Convert.ToDateTime(Request.Params["months"]).AddMonths(-1);
                    startTime = Convert.ToDateTime(fromPageMonths.Year.ToString() + "-" + fromPageMonths.Month.ToString() + "-25");
                    endTime = startTime.AddMonths(1);
                    if (CheckMonthStartDay != null)
                    {
                        if (CheckMonthStartDay.SetValue != "")
                        {

                            if (CheckMonthEndDay != null)
                            {
                                if (CheckMonthEndDay.SetValue != "")
                                {
                                    startTime = Convert.ToDateTime(fromPageMonths.Year.ToString() + "-" + fromPageMonths.Month.ToString() + "-" + CheckMonthStartDay.SetValue);
                                    endTime = Convert.ToDateTime(fromPageMonths.Year.ToString() + "-" + fromPageMonths.Month.ToString() + "-" + CheckMonthEndDay.SetValue).AddMonths(1);
                                }
                                else
                                {
                                    startTime = Convert.ToDateTime(fromPageMonths.Year.ToString() + "-" + fromPageMonths.Month.ToString() + "-" + CheckMonthStartDay.SetValue);
                                    endTime = startTime.AddMonths(1);
                                }

                            }
                            else
                            {
                                startTime = Convert.ToDateTime(fromPageMonths.Year.ToString() + "-" + fromPageMonths.Month.ToString() + "-" + CheckMonthStartDay.SetValue);
                                endTime = startTime.AddMonths(1);
                            }
                        }
                        else
                        {
                            if (CheckMonthEndDay != null)
                            {
                                if (CheckMonthEndDay.SetValue != "")
                                {
                                    startTime = Convert.ToDateTime(fromPageMonths.Year.ToString() + "-" + fromPageMonths.Month.ToString() + "-" + CheckMonthEndDay.SetValue);
                                    endTime = startTime.AddMonths(1);
                                }

                            }
                        }
                    }
                    else
                    {
                        if (CheckMonthEndDay != null)
                        {
                            if (CheckMonthEndDay.SetValue != "")
                            {
                                startTime = Convert.ToDateTime(fromPageMonths.Year.ToString() + "-" + fromPageMonths.Month.ToString() + "-" + CheckMonthEndDay.SetValue);
                                endTime = startTime.AddMonths(1);
                            }

                        }
                    }

                }
                Model.Check_CheckMonth checkMonth = CheckMonthService.GetCheckMonth(CheckMonthId);
                if (checkMonth!=null)
                {
                    startTime = Convert.ToDateTime(checkMonth.Months.Value.AddMonths(-1).Year.ToString() + "-" + checkMonth.Months.Value.AddMonths(-1).Month.ToString() + "-25");
                    endTime = startTime.AddMonths(1);
                    txtManagementOverview.Text = checkMonth.ManagementOverview;
                    txtAccidentSituation.Text = checkMonth.AccidentSituation;
                    txtConstructionData.Text = checkMonth.ConstructionData;
                    txtNextMonthPlan.Text = checkMonth.NextMonthPlan;
                    txtNeedSolved.Text = checkMonth.NeedSolved;
                }
                lbMonths.Text = endTime.Year.ToString() + "年" + endTime.Month.ToString() + "月";
                BindRectify();
                BindNDTCheck();
                BindWelder();
                OutputSummaryData();
                BindSpotCheck();
                BindSpecialEquipmentDetail();
                BindDesign();
                //质量验收本月合格
                List<Model.View_Check_SoptCheckDetail> spotCheckDetailOKLists = SpotCheckDetailService.GetOKSpotCheckDetailListByTime1(CurrUser.LoginProjectId, startTime, endTime);
                if (checkMonth == null)
                {
                    List<Model.View_Check_SoptCheckDetail> spotCheckDetailLists = SpotCheckDetailService.GetAllSpotCheckDetailListByTime(CurrUser.LoginProjectId, startTime, endTime);
                    if (spotCheckDetailOKLists.Count > 0 && spotCheckDetailLists.Count > 0)
                    {
                        var a = Convert.ToDouble(spotCheckDetailOKLists.Count);
                        var b = Convert.ToDouble(spotCheckDetailLists.Count);
                        decimal result = decimal.Round(decimal.Parse((a / b * 100).ToString()), 2);
                        this.txtMonthOk.Text = result.ToString() + "%";
                    }
                }
                else
                {
                    this.txtMonthOk.Text = checkMonth.MonthOk;
                }
                //质量验收累计合格
                List<Model.View_Check_SoptCheckDetail> TotalCheckDetailOKLists = SpotCheckDetailService.GetTotalOKSpotCheckDetailListByTime1(CurrUser.LoginProjectId, endTime);
                if (checkMonth == null)
                {
                    List<Model.View_Check_SoptCheckDetail> TotalCheckDetailLists = SpotCheckDetailService.GetTotalAllSpotCheckDetailListByTime(CurrUser.LoginProjectId, endTime);
                    if (TotalCheckDetailOKLists.Count > 0 && TotalCheckDetailLists.Count > 0)
                    {
                        var a = Convert.ToDouble(TotalCheckDetailOKLists.Count);
                        var b = Convert.ToDouble(TotalCheckDetailLists.Count);
                        decimal result = decimal.Round(decimal.Parse((a / b * 100).ToString()), 2);
                        this.txtAllOk.Text = result.ToString() + "%";
                    }
                }
                else
                {
                    this.txtAllOk.Text = checkMonth.AllOk;
                }
                //质量记录本月同步率
                if (checkMonth == null)
                {
                    List<Model.View_Check_SoptCheckDetail> spotCheckDetailDataOKLists = SpotCheckDetailService.GetMonthDataOkSpotCheckDetailListByTime(CurrUser.LoginProjectId, startTime, endTime);
                    if (spotCheckDetailOKLists.Count > 0 && spotCheckDetailDataOKLists.Count > 0)
                    {
                        var a = Convert.ToDouble(spotCheckDetailDataOKLists.Count);
                        var b = Convert.ToDouble(spotCheckDetailOKLists.Where(x => x.IsDataOK != "2").ToList().Count); //当期内，资料验收合格数÷实体合格数且需要上传资料的项数 * 100 %
                        decimal result = decimal.Round(decimal.Parse((a / b * 100).ToString()), 2);
                        this.txtMonthDataOk.Text = result.ToString() + "%";
                    }
                    else
                    {
                        this.txtMonthDataOk.Text = "0%";
                    }
                }
                else
                {
                    this.txtMonthDataOk.Text = checkMonth.MonthDataOk;
                }
                //质量记录累计同步率
                if (checkMonth == null)
                {
                    List<Model.View_Check_SoptCheckDetail> TotalCheckDetailDataOKLists = SpotCheckDetailService.GetAllDataOkSpotCheckDetailListByTime(CurrUser.LoginProjectId, endTime);
                    if (TotalCheckDetailDataOKLists.Count > 0 && TotalCheckDetailOKLists.Count > 0)
                    {
                        var a = Convert.ToDouble(TotalCheckDetailDataOKLists.Count);
                        var b = Convert.ToDouble(TotalCheckDetailOKLists.Where(x => x.IsDataOK != "2").ToList().Count);
                        decimal result = decimal.Round(decimal.Parse((a / b * 100).ToString()), 2);
                        this.txtAllDataOk.Text = result.ToString() + "%";
                    }
                    else
                    {
                        this.txtAllDataOk.Text = "0%";
                    }
                }
                else
                {
                    this.txtAllDataOk.Text = checkMonth.AllDataOk;
                }
            }
            else
            {
                if (GetRequestEventArgument() == "UPDATE_SUMMARY")
                {
                    // 页面要求重新计算合计行的值
                    OutputSummaryData();
                }
            }

        }
        private void BindRectify()
        {
            var list = MonthRectifyService.getListData(CheckMonthId);
            if (list.Count == 0)
            {
                List<Model.View_Check_JointCheckDetail> checkLists = JointCheckDetailService.GetJointCheckDetailListByTime(CurrUser.LoginProjectId, startTime, endTime);
                List<Model.View_Check_JointCheckDetail> totalCheckLists = JointCheckDetailService.GetTotalJointCheckDetailListByTime(CurrUser.LoginProjectId, endTime);
                List<Model.CheckItem> checkItems = new List<Model.CheckItem>();
                Model.CheckItem checkItem1 = new Model.CheckItem();
                checkItem1.Depart = "质量监督站";
                checkItem1.ThisRectifyNum = checkLists.Where(x => x.ProposeUnitType == "11").Count().ToString();
                checkItem1.ThisOKRectifyNum = checkLists.Where(x => x.ProposeUnitType == "11" && x.OK == 1).Count().ToString();
                checkItem1.TotalRectifyNum = totalCheckLists.Where(x => x.ProposeUnitType == "11").Count().ToString();
                checkItem1.TotalOKRectifyNum = totalCheckLists.Where(x => x.ProposeUnitType == "11" && x.OK == 1).Count().ToString();
                checkItems.Add(checkItem1);
                Model.CheckItem checkItem2 = new Model.CheckItem();
                checkItem2.Depart = "集团公司";
                checkItem2.ThisRectifyNum = checkLists.Where(x => x.ProposeUnitType == "8").Count().ToString();
                checkItem2.ThisOKRectifyNum = checkLists.Where(x => x.ProposeUnitType == "8" && x.OK == 1).Count().ToString();
                checkItem2.TotalRectifyNum = totalCheckLists.Where(x => x.ProposeUnitType == "8").Count().ToString();
                checkItem2.TotalOKRectifyNum = totalCheckLists.Where(x => x.ProposeUnitType == "8" && x.OK == 1).Count().ToString();
                checkItems.Add(checkItem2);
                Model.CheckItem checkItem3 = new Model.CheckItem();
                checkItem3.Depart = "公司本部";
                checkItem3.ThisRectifyNum = checkLists.Where(x => x.ProposeUnitType == "10").Count().ToString();
                checkItem3.ThisOKRectifyNum = checkLists.Where(x => x.ProposeUnitType == "10" && x.OK == 1).Count().ToString();
                checkItem3.TotalRectifyNum = totalCheckLists.Where(x => x.ProposeUnitType == "10").Count().ToString();
                checkItem3.TotalOKRectifyNum = totalCheckLists.Where(x => x.ProposeUnitType == "10" && x.OK == 1).Count().ToString();
                checkItems.Add(checkItem3);
                Model.CheckItem checkItem4 = new Model.CheckItem();
                checkItem4.Depart = "建设单位";
                checkItem4.ThisRectifyNum = checkLists.Where(x => x.ProposeUnitType == BLL.Const.ProjectUnitType_4).Count().ToString();
                checkItem4.ThisOKRectifyNum = checkLists.Where(x => x.ProposeUnitType == BLL.Const.ProjectUnitType_4 && x.OK == 1).Count().ToString();
                checkItem4.TotalRectifyNum = totalCheckLists.Where(x => x.ProposeUnitType == BLL.Const.ProjectUnitType_4).Count().ToString();
                checkItem4.TotalOKRectifyNum = totalCheckLists.Where(x => x.ProposeUnitType == BLL.Const.ProjectUnitType_4 && x.OK == 1).Count().ToString();
                checkItems.Add(checkItem4);
                Model.CheckItem checkItem5 = new Model.CheckItem();
                checkItem5.Depart = "监理单位";
                checkItem5.ThisRectifyNum = checkLists.Where(x => x.ProposeUnitType == BLL.Const.ProjectUnitType_3).Count().ToString();
                checkItem5.ThisOKRectifyNum = checkLists.Where(x => x.ProposeUnitType == BLL.Const.ProjectUnitType_3 && x.OK == 1).Count().ToString();
                checkItem5.TotalRectifyNum = totalCheckLists.Where(x => x.ProposeUnitType == BLL.Const.ProjectUnitType_3).Count().ToString();
                checkItem5.TotalOKRectifyNum = totalCheckLists.Where(x => x.ProposeUnitType == BLL.Const.ProjectUnitType_3 && x.OK == 1).Count().ToString();
                checkItems.Add(checkItem5);
                Model.CheckItem checkItem6 = new Model.CheckItem();
                checkItem6.Depart = "总承包商项目部";
                checkItem6.ThisRectifyNum = checkLists.Where(x => x.ProposeUnitType == BLL.Const.ProjectUnitType_1).Count().ToString();
                checkItem6.ThisOKRectifyNum = checkLists.Where(x => x.ProposeUnitType == BLL.Const.ProjectUnitType_1 && x.OK == 1).Count().ToString();
                checkItem6.TotalRectifyNum = totalCheckLists.Where(x => x.ProposeUnitType == BLL.Const.ProjectUnitType_1).Count().ToString();
                checkItem6.TotalOKRectifyNum = totalCheckLists.Where(x => x.ProposeUnitType == BLL.Const.ProjectUnitType_1 && x.OK == 1).Count().ToString();
                checkItems.Add(checkItem6);
                GridRectify.DataSource = checkItems;
                GridRectify.DataBind();
            }
            else
            {
                GridRectify.DataSource = list;
                GridRectify.DataBind();
            }
        }
        private void BindNDTCheck()
        {
            var list = BLL.MonthNDTCheckService.getListData(CheckMonthId);
            if (list.Count == 0)
            {
                List<Model.Check_MonthNDTCheck> nDTCheckItems = new List<Model.Check_MonthNDTCheck>();
                var totalNDEItems = from x in Funs.DB.HJGL_Batch_NDEItem
                                    join y in Funs.DB.HJGL_Batch_NDE
                                    on x.NDEID equals y.NDEID
                                    where y.NDEDate <= endTime && y.ProjectId == CurrUser.LoginProjectId
                                    select new
                                    {
                                        NDEItemID = x.NDEItemID,
                                        UnitId = y.UnitId,
                                        TotalFilm = x.TotalFilm,
                                        PassFilm = x.PassFilm,
                                    };
                var monthNDEItems = from x in Funs.DB.HJGL_Batch_NDEItem
                                    join y in Funs.DB.HJGL_Batch_NDE
                                    on x.NDEID equals y.NDEID
                                    where y.NDEDate >= startTime && y.NDEDate <= endTime && y.ProjectId == CurrUser.LoginProjectId
                                    select new
                                    {
                                        NDEItemID = x.NDEItemID,
                                        UnitId = y.UnitId,
                                        TotalFilm = x.TotalFilm,
                                        PassFilm = x.PassFilm,
                                    };
                var monthRepairRecords = from x in Funs.DB.HJGL_RepairRecord
                                         where x.RepairDate >= startTime && x.RepairDate <= endTime && x.ProjectId == this.CurrUser.LoginProjectId
                                         select x;
                List<Model.Base_Unit> units = UnitService.GetUnitByProjectIdUnitTypeList(CurrUser.LoginProjectId, BLL.Const.ProjectUnitType_2);
                foreach (var unit in units)
                {
                    var unitTotalNDEItems = totalNDEItems.Where(x => x.UnitId == unit.UnitId);
                    var unitNDEItems = monthNDEItems.Where(x => x.UnitId == unit.UnitId);
                    Model.Check_MonthNDTCheck nDTCheckItem = new Model.Check_MonthNDTCheck();
                    nDTCheckItem.UnitId = unit.UnitId;
                    int filmNum = 0, passFilm = 0;
                    if (unitNDEItems.Count() > 0)
                    {
                        filmNum = unitNDEItems.Sum(x => x.TotalFilm ?? 0);
                        passFilm = unitNDEItems.Sum(x => x.PassFilm ?? 0);
                    }
                    int totalFilmNum = 0, totalPassFilm = 0;
                    if (unitTotalNDEItems.Count() > 0)
                    {
                        totalFilmNum = unitTotalNDEItems.Sum(x => x.TotalFilm ?? 0);
                        totalPassFilm = unitTotalNDEItems.Sum(x => x.PassFilm ?? 0);
                    }
                    nDTCheckItem.FilmNum = filmNum;
                    nDTCheckItem.NotOKFileNum = filmNum - passFilm;
                    nDTCheckItem.RepairFileNum = monthRepairRecords.Where(x => x.UnitId == unit.UnitId).Count();
                    if (filmNum > 0)
                    {
                        nDTCheckItem.OneOKRate = decimal.Round(decimal.Parse((Convert.ToDouble(passFilm) / Convert.ToDouble(filmNum) * 100).ToString()), 2).ToString() + "%";
                    }
                    else
                    {
                        nDTCheckItem.OneOKRate = "0%";
                    }
                    nDTCheckItem.TotalFilmNum = totalFilmNum;
                    nDTCheckItem.TotalNotOKFileNum = totalFilmNum - totalPassFilm;
                    if (totalFilmNum > 0)
                    {
                        nDTCheckItem.TotalOneOKRate = decimal.Round(decimal.Parse((Convert.ToDouble(totalPassFilm) / Convert.ToDouble(totalFilmNum) * 100).ToString()), 2).ToString() + "%";
                    }
                    else
                    {
                        nDTCheckItem.TotalOneOKRate = "0%";
                    }
                    nDTCheckItems.Add(nDTCheckItem);
                }
                GridNDTCheck.DataSource = nDTCheckItems;
                GridNDTCheck.DataBind();
            }
            else
            {
                GridNDTCheck.DataSource = list;
                GridNDTCheck.DataBind();
            }
        }
        private void BindWelder()
        {
            var list = BLL.MonthWelderService.getListData(CheckMonthId);
            if (list.Count == 0)
            {
                List<Model.Check_MonthWelder> welderItems = new List<Model.Check_MonthWelder>();
                List<Model.Base_Unit> units = UnitService.GetUnitByProjectIdUnitTypeList(CurrUser.LoginProjectId, BLL.Const.ProjectUnitType_2);
                foreach (var unit in units)
                {
                    Model.Check_MonthWelder welderItem = new Model.Check_MonthWelder();
                    welderItem.UnitId = unit.UnitId;
                    welderItem.ThisPersonNum = 0;
                    welderItem.ThisOKPersonNum = 0;
                    welderItem.ThisOKRate = "0%";
                    welderItem.TotalPersonNum = 0;
                    welderItem.TotalOKPersonNum = 0;
                    welderItem.TotalOKRate = "0%";
                    welderItems.Add(welderItem);
                }
                GridWelder.DataSource = welderItems;
                GridWelder.DataBind();
            }
            else
            {
                GridWelder.DataSource = list;
                GridWelder.DataBind();
            }
        }

        private void BindSpotCheck()
        {
            var monthSpotCheckDetails = BLL.MonthSpotCheckDetailService.getListData(CheckMonthId);
            if (monthSpotCheckDetails.Count == 0)
            {
                List<Model.Check_SpotCheckDetail> spotCheckDetailOKLists = SpotCheckDetailService.GetOKSpotCheckDetailListByTime(CurrUser.LoginProjectId, startTime, endTime);
                List<Model.Check_SpotCheckDetail> totalSpotCheckDetailOKLists = SpotCheckDetailService.GetTotalOKSpotCheckDetailListByTime(CurrUser.LoginProjectId, endTime);
                List<Model.WBS_ControlItemAndCycle> totalControlItemAndCycles = ControlItemAndCycleService.GetTotalControlItemAndCycles(CurrUser.LoginProjectId);
                string modelId = SQLHelper.GetNewID(typeof(Model.Check_MonthSpotCheckDetail));
                Model.Check_MonthSpotCheckDetail monthSpotCheckDetail1 = new Model.Check_MonthSpotCheckDetail();
                monthSpotCheckDetail1.MonthSpotCheckDetailId = modelId.Substring(0, modelId.Length - 1) + "1";
                monthSpotCheckDetail1.ControlPoint = "AR";
                monthSpotCheckDetail1.TotalNum = ControlItemAndCycleService.GetControlItemAndCyclesByControlPoint("AR").ToString();
                monthSpotCheckDetail1.ThisOKNum = (from x in spotCheckDetailOKLists
                                                   join y in totalControlItemAndCycles
                                                   on x.ControlItemAndCycleId equals y.ControlItemAndCycleId
                                                   where y.ControlPoint == "AR"
                                                   select x).Count().ToString();
                monthSpotCheckDetail1.TotalOKNum = (from x in totalSpotCheckDetailOKLists
                                                    join y in totalControlItemAndCycles
                                                    on x.ControlItemAndCycleId equals y.ControlItemAndCycleId
                                                    where y.ControlPoint == "AR"
                                                    select x).Count().ToString();
                if (Convert.ToInt32(monthSpotCheckDetail1.TotalNum) > 0)
                {
                    monthSpotCheckDetail1.TotalOKRate = decimal.Round(Convert.ToDecimal(Convert.ToDecimal(monthSpotCheckDetail1.TotalOKNum) * 100 / Convert.ToDecimal(monthSpotCheckDetail1.TotalNum)), 2) + "%";
                }
                else
                {
                    monthSpotCheckDetail1.TotalOKRate = "0";
                }
                monthSpotCheckDetails.Add(monthSpotCheckDetail1);
                Model.Check_MonthSpotCheckDetail monthSpotCheckDetail2 = new Model.Check_MonthSpotCheckDetail();
                monthSpotCheckDetail2.MonthSpotCheckDetailId = modelId.Substring(0, modelId.Length - 1) + "2";
                monthSpotCheckDetail2.ControlPoint = "A";
                monthSpotCheckDetail2.TotalNum = ControlItemAndCycleService.GetControlItemAndCyclesByControlPoint("A").ToString();
                monthSpotCheckDetail2.ThisOKNum = (from x in spotCheckDetailOKLists
                                                   join y in totalControlItemAndCycles
                                                   on x.ControlItemAndCycleId equals y.ControlItemAndCycleId
                                                   where y.ControlPoint == "A"
                                                   select x).Count().ToString();
                monthSpotCheckDetail2.TotalOKNum = (from x in totalSpotCheckDetailOKLists
                                                    join y in totalControlItemAndCycles
                                                    on x.ControlItemAndCycleId equals y.ControlItemAndCycleId
                                                    where y.ControlPoint == "A"
                                                    select x).Count().ToString();
                if (Convert.ToInt32(monthSpotCheckDetail2.TotalNum) > 0)
                {
                    monthSpotCheckDetail2.TotalOKRate = decimal.Round(Convert.ToDecimal(Convert.ToDecimal(monthSpotCheckDetail2.TotalOKNum) * 100 / Convert.ToDecimal(monthSpotCheckDetail2.TotalNum)), 2) + "%";
                }
                else
                {
                    monthSpotCheckDetail2.TotalOKRate = "0";
                }
                monthSpotCheckDetails.Add(monthSpotCheckDetail2);
                Model.Check_MonthSpotCheckDetail monthSpotCheckDetail3 = new Model.Check_MonthSpotCheckDetail();
                monthSpotCheckDetail3.MonthSpotCheckDetailId = modelId.Substring(0, modelId.Length - 1) + "3";
                monthSpotCheckDetail3.ControlPoint = "BR";
                monthSpotCheckDetail3.TotalNum = ControlItemAndCycleService.GetControlItemAndCyclesByControlPoint("BR").ToString();
                monthSpotCheckDetail3.ThisOKNum = (from x in spotCheckDetailOKLists
                                                   join y in totalControlItemAndCycles
                                                   on x.ControlItemAndCycleId equals y.ControlItemAndCycleId
                                                   where y.ControlPoint == "BR"
                                                   select x).Count().ToString();
                monthSpotCheckDetail3.TotalOKNum = (from x in totalSpotCheckDetailOKLists
                                                    join y in totalControlItemAndCycles
                                                    on x.ControlItemAndCycleId equals y.ControlItemAndCycleId
                                                    where y.ControlPoint == "BR"
                                                    select x).Count().ToString();
                if (Convert.ToInt32(monthSpotCheckDetail3.TotalNum) > 0)
                {
                    monthSpotCheckDetail3.TotalOKRate = decimal.Round(Convert.ToDecimal(Convert.ToDecimal(monthSpotCheckDetail3.TotalOKNum) * 100 / Convert.ToDecimal(monthSpotCheckDetail3.TotalNum)), 2) + "%";
                }
                else
                {
                    monthSpotCheckDetail3.TotalOKRate = "0";
                }
                monthSpotCheckDetails.Add(monthSpotCheckDetail3);
                Model.Check_MonthSpotCheckDetail monthSpotCheckDetail4 = new Model.Check_MonthSpotCheckDetail();
                monthSpotCheckDetail4.MonthSpotCheckDetailId = modelId.Substring(0, modelId.Length - 1) + "4";
                monthSpotCheckDetail4.ControlPoint = "B";
                monthSpotCheckDetail4.TotalNum = ControlItemAndCycleService.GetControlItemAndCyclesByControlPoint("B").ToString();
                monthSpotCheckDetail4.ThisOKNum = (from x in spotCheckDetailOKLists
                                                   join y in totalControlItemAndCycles
                                                   on x.ControlItemAndCycleId equals y.ControlItemAndCycleId
                                                   where y.ControlPoint == "B"
                                                   select x).Count().ToString();
                monthSpotCheckDetail4.TotalOKNum = (from x in totalSpotCheckDetailOKLists
                                                    join y in totalControlItemAndCycles
                                                    on x.ControlItemAndCycleId equals y.ControlItemAndCycleId
                                                    where y.ControlPoint == "B"
                                                    select x).Count().ToString();
                if (Convert.ToInt32(monthSpotCheckDetail4.TotalNum) > 0)
                {
                    monthSpotCheckDetail4.TotalOKRate = decimal.Round(Convert.ToDecimal(Convert.ToDecimal(monthSpotCheckDetail4.TotalOKNum) * 100 / Convert.ToDecimal(monthSpotCheckDetail4.TotalNum)), 2) + "%";
                }
                else
                {
                    monthSpotCheckDetail4.TotalOKRate = "0";
                }
                monthSpotCheckDetails.Add(monthSpotCheckDetail4);
                GridSpotCheckDetail.DataSource = monthSpotCheckDetails;
                GridSpotCheckDetail.DataBind();
                //if (!string.IsNullOrEmpty(CheckMonthId))
                //{
                //    foreach (var monthSpotCheckDetail in monthSpotCheckDetails)
                //    {
                //        monthSpotCheckDetail.CheckMonthId = CheckMonthId;
                //        MonthSpotCheckDetailService.AddMonthSpotCheckDetail(monthSpotCheckDetail);
                //    }
                //}
            }
            else
            {
                GridSpotCheckDetail.DataSource = monthSpotCheckDetails;
                GridSpotCheckDetail.DataBind();
            }
        }

        #region 月报数据集合
        /// <summary>
        ///月报数据集合
        /// </summary>
        public static List<Model.Check_SpecialEquipmentDetail> specialEquipmentDetails = new List<Model.Check_SpecialEquipmentDetail>();
        #endregion


        //<summary>
        //获取施工完成情况
        //</summary>
        //<param name="state"></param>
        //<returns></returns>
        protected string ConvertState(object DesignId)
        {
            if (DesignId != null)
            {
                Model.Check_Design design = BLL.DesignService.GetDesignByDesignId(DesignId.ToString());
                if (design != null)
                {
                    if (design.State == Const.Design_Complete)
                    {
                        return "已完成";
                    }
                    else
                    {
                        return "未完成";
                    }
                }
                else
                {
                    return "";
                }
            }
            return "";
        }

        //<summary>
        //获取单位名称
        //</summary>
        //<param name="state"></param>
        //<returns></returns>
        protected string ConvertUnitName(object UnitId)
        {
            string unitName = string.Empty;
            if (UnitId != null)
            {
                Model.Base_Unit unit = BLL.UnitService.GetUnitByUnitId(UnitId.ToString());
                if (unit != null)
                {
                    unitName = unit.UnitName;
                }
            }
            return unitName;
        }

        private void BindSpecialEquipmentDetail()
        {

            specialEquipmentDetails = SpecialEquipmentDetailService.GetList(CheckMonthId);
            if (specialEquipmentDetails.Count == 0)  //新增月报
            {
                string Id = SQLHelper.GetNewID(typeof(Model.Check_SpecialEquipmentDetail));
                Model.Check_SpecialEquipmentDetail specialEquipmentDetail1 = new Model.Check_SpecialEquipmentDetail();
                specialEquipmentDetail1.SpecialEquipmentName = "压力容器(个)";
                specialEquipmentDetail1.SpecialEquipmentDetailId = Id.Substring(0, Id.Length - 1) + "1";
                specialEquipmentDetails.Add(specialEquipmentDetail1);
                Model.Check_SpecialEquipmentDetail specialEquipmentDetail2 = new Model.Check_SpecialEquipmentDetail();
                specialEquipmentDetail2.SpecialEquipmentName = "压力管道(条)";
                specialEquipmentDetail2.SpecialEquipmentDetailId = Id.Substring(0, Id.Length - 1) + "2";
                specialEquipmentDetails.Add(specialEquipmentDetail2);
                Model.Check_SpecialEquipmentDetail specialEquipmentDetail3 = new Model.Check_SpecialEquipmentDetail();
                specialEquipmentDetail3.SpecialEquipmentName = "起重机械";
                specialEquipmentDetail3.SpecialEquipmentDetailId = Id.Substring(0, Id.Length - 1) + "3";
                specialEquipmentDetails.Add(specialEquipmentDetail3);
                Model.Check_SpecialEquipmentDetail specialEquipmentDetail4 = new Model.Check_SpecialEquipmentDetail();
                specialEquipmentDetail4.SpecialEquipmentName = "锅炉";
                specialEquipmentDetail4.SpecialEquipmentDetailId = Id.Substring(0, Id.Length - 1) + "4";
                specialEquipmentDetails.Add(specialEquipmentDetail4);
                Model.Check_SpecialEquipmentDetail specialEquipmentDetail5 = new Model.Check_SpecialEquipmentDetail();
                specialEquipmentDetail5.SpecialEquipmentName = "电梯";
                specialEquipmentDetail5.SpecialEquipmentDetailId = Id.Substring(0, Id.Length - 1) + "5";
                specialEquipmentDetails.Add(specialEquipmentDetail5);
            }
            GridSpecialEquipmentDetail.DataSource = specialEquipmentDetails;
            GridSpecialEquipmentDetail.DataBind();
        }
        private void BindDesign()
        {
            var list = MonthDesignService.getListData(CheckMonthId);
            var mainItems = BLL.MainItemService.GetMainItemList(this.CurrUser.LoginProjectId);
            var designProfessionals = BLL.DesignProfessionalService.GetDesignProfessionalItem();
            if (list.Count == 0)
            {
                var designList = DesignService.GetDesignListsByTime(CurrUser.LoginProjectId, startTime, endTime);
                List<Model.Check_MonthDesign> monthDesigns = new List<Model.Check_MonthDesign>();
                string monthDesignId = SQLHelper.GetNewID();
                monthDesignId = monthDesignId.Substring(0, monthDesignId.Length - 1);
                int i = 0;
                foreach (var item in designList)
                {
                    Model.Check_MonthDesign monthDesign = new Model.Check_MonthDesign();
                    monthDesign.MonthDesignId = monthDesignId + i;
                    monthDesign.DesignCode = item.DesignCode;
                    var mainItem = mainItems.FirstOrDefault(x => x.Value == item.MainItemId);
                    if (mainItem != null)
                    {
                        monthDesign.MainItemId = mainItem.Text;
                    }
                    var designProfessional = designProfessionals.FirstOrDefault(x => x.Value == item.CNProfessionalCode);
                    if (designProfessional != null)
                    {
                        monthDesign.DesignProfessionalId = designProfessional.Text;
                    }
                    monthDesign.State = item.State == Const.Design_Complete ? "已完成" : "未完成";
                    monthDesigns.Add(monthDesign);
                    i++;
                }
                GridDesign.DataSource = monthDesigns;
                GridDesign.DataBind();
            }
            else
            {
                foreach (var item in list)
                {
                    var mainItem = mainItems.FirstOrDefault(x => x.Value == item.MainItemId);
                    if (mainItem != null)
                    {
                        item.MainItemId = mainItem.Text;
                    }
                    var designProfessional = designProfessionals.FirstOrDefault(x => x.Value == item.DesignProfessionalId);
                    if (designProfessional != null)
                    {
                        item.DesignProfessionalId = designProfessional.Text;
                    }
                }
                GridDesign.DataSource = list;
                GridDesign.DataBind();
            }
        }
        #region 增加按钮事件
        /// <summary>
        /// 增加按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNew_Click(object sender, EventArgs e)
        {
            var list = GetDetails();
            Model.Check_MonthDesign monthDesign = new Model.Check_MonthDesign();
            monthDesign.MonthDesignId = SQLHelper.GetNewID();
            list.Add(monthDesign);
            GridDesign.DataSource = list;
            GridDesign.DataBind();
        }
        private List<Model.Check_MonthDesign> GetDetails()
        {
            List<Model.Check_MonthDesign> monthDesigns = new List<Model.Check_MonthDesign>();
            foreach (JObject mergedRow in GridDesign.GetMergedData())
            {
                JObject values = mergedRow.Value<JObject>("values");
                int i = mergedRow.Value<int>("index");
                Model.Check_MonthDesign monthDesign = new Model.Check_MonthDesign();
                monthDesign.MonthDesignId = GridDesign.Rows[i].RowID;
                monthDesign.DesignCode = values.Value<string>("DesignCode");
                monthDesign.MainItemId = values.Value<string>("MainItemId");
                monthDesign.DesignProfessionalId = values.Value<string>("DesignProfessionalId");
                monthDesign.State = values.Value<string>("State");
                monthDesign.Remark = values.Value<string>("Remark");
                monthDesigns.Add(monthDesign);
            }
            return monthDesigns;
        }
        #endregion
        #region 行点击事件
        /// <summary>
        /// Grid行点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridDesign_RowCommand(object sender, GridCommandEventArgs e)
        {
            string monthDesignId = GridDesign.DataKeys[e.RowIndex][0].ToString();
            var list = GetDetails();
            if (e.CommandName == "del")//删除
            {
                var Report = list.FirstOrDefault(x => x.MonthDesignId == monthDesignId);
                if (Report != null)
                {
                    list.Remove(Report);
                }
                this.GridDesign.DataSource = list;
                this.GridDesign.DataBind();
            }
        }
        #endregion
        private void OutputSummaryData()
        {
            JObject summary = new JObject();
            int filmNum = 0, notOKFileNum = 0, repairFileNum = 0, totalFilmNum = 0, totalNotOKFileNum = 0;
            string oneOKRate = string.Empty, totalOneOKRate = string.Empty;
            foreach (JObject mergedRow in GridNDTCheck.GetMergedData())
            {
                JObject values = mergedRow.Value<JObject>("values");
                if (values["FilmNum"].ToString() != "" && values["NotOKFileNum"].ToString() != "")
                {
                    filmNum += values.Value<int>("FilmNum");
                    notOKFileNum += values.Value<int>("NotOKFileNum");
                }
                if (values["RepairFileNum"].ToString() != "")
                {
                    repairFileNum += values.Value<int>("RepairFileNum");
                }
                if (values["TotalFilmNum"].ToString() != "" && values["TotalNotOKFileNum"].ToString() != "")
                {
                    totalFilmNum += values.Value<int>("TotalFilmNum");
                    totalNotOKFileNum += values.Value<int>("TotalNotOKFileNum");
                }
            }
            if (filmNum > 0)
            {
                var a = Convert.ToDouble(filmNum - notOKFileNum);
                var b = Convert.ToDouble(filmNum);
                decimal result = decimal.Round(decimal.Parse((a / b * 100).ToString()), 2);
                oneOKRate = result.ToString() + "%";
            }
            if (totalFilmNum > 0)
            {
                var a = Convert.ToDouble(totalFilmNum - totalNotOKFileNum);
                var b = Convert.ToDouble(totalFilmNum);
                decimal result = decimal.Round(decimal.Parse((a / b * 100).ToString()), 2);
                totalOneOKRate = result.ToString() + "%";
            }
            JObject summary1 = new JObject();
            summary1.Add("UnitName", "合计");
            summary1.Add("FilmNum", filmNum);
            summary1.Add("NotOKFileNum", notOKFileNum);
            summary1.Add("RepairFileNum", repairFileNum);
            summary1.Add("OneOKRate", oneOKRate);
            summary1.Add("TotalFilmNum", totalFilmNum);
            summary1.Add("TotalNotOKFileNum", totalNotOKFileNum);
            summary1.Add("TotalOneOKRate", totalOneOKRate);
            GridNDTCheck.SummaryData = summary1;
            int thisPersonNum = 0, thisOKPersonNum = 0, totalPersonNum = 0, totalOKPersonNum = 0;
            string thisOKRate = string.Empty, totalOKRate = string.Empty;
            foreach (JObject mergedRow in GridWelder.GetMergedData())
            {
                JObject values = mergedRow.Value<JObject>("values");
                if (values["ThisPersonNum"].ToString() != "" && values["ThisOKPersonNum"].ToString() != "")
                {
                    thisPersonNum += values.Value<int>("ThisPersonNum");
                    thisOKPersonNum += values.Value<int>("ThisOKPersonNum");
                }
                if (values["TotalPersonNum"].ToString() != "" && values["TotalOKPersonNum"].ToString() != "")
                {
                    totalPersonNum += values.Value<int>("TotalPersonNum");
                    totalOKPersonNum += values.Value<int>("TotalOKPersonNum");
                }
            }
            if (thisPersonNum > 0 && thisOKPersonNum > 0)
            {
                var a = Convert.ToDouble(thisOKPersonNum);
                var b = Convert.ToDouble(thisPersonNum);
                decimal result = decimal.Round(decimal.Parse((a / b * 100).ToString()), 2);
                thisOKRate = result.ToString() + "%";
            }
            if (totalPersonNum > 0 && totalOKPersonNum > 0)
            {
                var a = Convert.ToDouble(totalOKPersonNum);
                var b = Convert.ToDouble(totalPersonNum);
                decimal result = decimal.Round(decimal.Parse((a / b * 100).ToString()), 2);
                totalOKRate = result.ToString() + "%";
            }
            summary.Add("UnitName", "合计");
            summary.Add("ThisPersonNum", thisPersonNum);
            summary.Add("ThisOKPersonNum", thisOKPersonNum);
            summary.Add("ThisOKRate", thisOKRate);
            summary.Add("TotalPersonNum", totalPersonNum);
            summary.Add("TotalOKPersonNum", totalOKPersonNum);
            summary.Add("TotalOKRate", totalOKRate);
            GridWelder.SummaryData = summary;

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (CommonService.GetAllButtonPowerList(CurrUser.LoginProjectId, CurrUser.UserId, Const.CheckMonthMenuId, Const.BtnModify))
            {
                Model.Check_CheckMonth CheckMonth = new Model.Check_CheckMonth();
                CheckMonth.ProjectId = CurrUser.LoginProjectId;
                CheckMonth.ManagementOverview = txtManagementOverview.Text.Trim();
                CheckMonth.AccidentSituation = txtAccidentSituation.Text.Trim();
                CheckMonth.ConstructionData = txtConstructionData.Text.Trim();
                CheckMonth.NextMonthPlan = txtNextMonthPlan.Text.Trim();
                CheckMonth.NeedSolved = txtNeedSolved.Text.Trim();
                CheckMonth.MonthOk = txtMonthOk.Text.Trim();
                CheckMonth.MonthDataOk = txtMonthDataOk.Text.Trim();
                CheckMonth.AllOk = txtAllOk.Text.Trim();
                CheckMonth.AllDataOk = txtAllDataOk.Text.Trim();
                if (!string.IsNullOrEmpty(CheckMonthId))
                {
                    CheckMonth.CheckMonthId = CheckMonthId;
                    CheckMonthService.UpdateCheckMonth(CheckMonth);
                    //LogService.AddLog(CurrUser.UserId, "修改质量月报", CurrUser.LoginProjectId);
                }
                else
                {
                    CheckMonth.CheckMonthId = SQLHelper.GetNewID(typeof(Model.Check_CheckMonth));
                    CheckMonth.Months = Convert.ToDateTime(Request.Params["months"]);
                    CheckMonth.CompileMan = CurrUser.UserId;
                    CheckMonth.CompileDate = DateTime.Now;
                    CheckMonthService.AddCheckMonth(CheckMonth);
                    //LogService.AddLog(CurrUser.UserId, "增加质量月报", CurrUser.LoginProjectId);
                }
                SpecialEquipmentDetailService.DeleteSpecialEquipmentDetailsByCheckMonthId(CheckMonth.CheckMonthId);
                BLL.MonthRectifyService.DeleteMonthRectifysByCheckMonthId(CheckMonth.CheckMonthId);
                BLL.MonthNDTCheckService.DeleteMonthNDTChecksByCheckMonthId(CheckMonth.CheckMonthId);
                BLL.MonthWelderService.DeleteMonthWeldersByCheckMonthId(CheckMonth.CheckMonthId);
                BLL.MonthSpotCheckDetailService.DeleteMonthSpotCheckDetailsByCheckMonthId(CheckMonthId);
                BLL.MonthDesignService.DeleteMonthDesignsByCheckMonthId(CheckMonthId);
                int count = GridSpecialEquipmentDetail.Rows.Count;
                string monthRectifyId = SQLHelper.GetNewID();
                monthRectifyId = monthRectifyId.Substring(0, monthRectifyId.Length - 1);
                foreach (JObject mergedRow in GridRectify.GetMergedData())  //质量缺陷/不合格项整改关闭情况
                {
                    Model.Check_MonthRectify monthRectify = new Model.Check_MonthRectify();
                    int i = mergedRow.Value<int>("index");
                    JObject values = mergedRow.Value<JObject>("values");
                    //string id = GridSpecialEquipmentDetail.Rows[i].RowID;
                    monthRectify.MonthRectifyId = monthRectifyId + i.ToString();
                    monthRectify.CheckMonthId = CheckMonth.CheckMonthId;
                    monthRectify.Depart = values.Value<string>("Depart");
                    monthRectify.ThisRectifyNum = Funs.GetNewIntOrZero(values.Value<string>("ThisRectifyNum"));
                    monthRectify.ThisOKRectifyNum = Funs.GetNewIntOrZero(values.Value<string>("ThisOKRectifyNum"));
                    monthRectify.TotalRectifyNum = Funs.GetNewIntOrZero(values.Value<string>("TotalRectifyNum"));
                    monthRectify.TotalOKRectifyNum = Funs.GetNewIntOrZero(values.Value<string>("TotalOKRectifyNum"));
                    MonthRectifyService.AddMonthRectify(monthRectify);
                }
                string monthNDTCheckId = SQLHelper.GetNewID();
                monthNDTCheckId = monthNDTCheckId.Substring(0, monthNDTCheckId.Length - 1);
                foreach (JObject mergedRow in GridNDTCheck.GetMergedData())  //无损检测情况
                {
                    Model.Check_MonthNDTCheck monthNDTCheck = new Model.Check_MonthNDTCheck();
                    int i = mergedRow.Value<int>("index");
                    JObject values = mergedRow.Value<JObject>("values");
                    //string id = GridSpecialEquipmentDetail.Rows[i].RowID;
                    monthNDTCheck.MonthNDTCheckId = monthNDTCheckId + i.ToString();
                    monthNDTCheck.CheckMonthId = CheckMonth.CheckMonthId;
                    monthNDTCheck.UnitId = values.Value<string>("UnitId");
                    monthNDTCheck.FilmNum = Funs.GetNewIntOrZero(values.Value<string>("FilmNum"));
                    monthNDTCheck.NotOKFileNum = Funs.GetNewIntOrZero(values.Value<string>("NotOKFileNum"));
                    monthNDTCheck.RepairFileNum = Funs.GetNewIntOrZero(values.Value<string>("RepairFileNum"));
                    monthNDTCheck.OneOKRate = values.Value<string>("OneOKRate");
                    monthNDTCheck.TotalFilmNum = Funs.GetNewIntOrZero(values.Value<string>("TotalFilmNum"));
                    monthNDTCheck.TotalNotOKFileNum = Funs.GetNewIntOrZero(values.Value<string>("TotalNotOKFileNum"));
                    monthNDTCheck.TotalOneOKRate = values.Value<string>("TotalOneOKRate");
                    MonthNDTCheckService.AddMonthNDTCheck(monthNDTCheck);
                }
                string monthWelderId = SQLHelper.GetNewID();
                monthWelderId = monthWelderId.Substring(0, monthWelderId.Length - 1);
                foreach (JObject mergedRow in GridWelder.GetMergedData())  //焊工资格评定情况
                {
                    Model.Check_MonthWelder monthWelder = new Model.Check_MonthWelder();
                    int i = mergedRow.Value<int>("index");
                    JObject values = mergedRow.Value<JObject>("values");
                    //string id = GridSpecialEquipmentDetail.Rows[i].RowID;
                    monthWelder.MonthWelderId = monthNDTCheckId + i.ToString();
                    monthWelder.CheckMonthId = CheckMonth.CheckMonthId;
                    monthWelder.UnitId = values.Value<string>("UnitId");
                    monthWelder.ThisPersonNum = Funs.GetNewIntOrZero(values.Value<string>("ThisPersonNum"));
                    monthWelder.ThisOKPersonNum = Funs.GetNewIntOrZero(values.Value<string>("ThisOKPersonNum"));
                    monthWelder.ThisOKRate = values.Value<string>("ThisOKRate");
                    monthWelder.TotalPersonNum = Funs.GetNewIntOrZero(values.Value<string>("TotalPersonNum"));
                    monthWelder.TotalOKPersonNum = Funs.GetNewIntOrZero(values.Value<string>("TotalOKPersonNum"));
                    monthWelder.TotalOKRate = values.Value<string>("TotalOKRate");
                    MonthWelderService.AddMonthWelder(monthWelder);
                }
                string monthSpotCheckDetailId = SQLHelper.GetNewID();
                monthSpotCheckDetailId = monthSpotCheckDetailId.Substring(0, monthSpotCheckDetailId.Length - 1);
                foreach (JObject mergedRow in GridSpotCheckDetail.GetMergedData())  //焊工资格评定情况
                {
                    Model.Check_MonthSpotCheckDetail monthSpotCheckDetail = new Model.Check_MonthSpotCheckDetail();
                    int i = mergedRow.Value<int>("index");
                    JObject values = mergedRow.Value<JObject>("values");
                    //string id = GridSpecialEquipmentDetail.Rows[i].RowID;
                    monthSpotCheckDetail.MonthSpotCheckDetailId = monthSpotCheckDetailId + i.ToString();
                    monthSpotCheckDetail.CheckMonthId = CheckMonth.CheckMonthId;
                    monthSpotCheckDetail.ControlPoint = values.Value<string>("ControlPoint");
                    monthSpotCheckDetail.TotalNum = values.Value<string>("TotalSpotNum");
                    monthSpotCheckDetail.ThisOKNum = values.Value<string>("ThisSpotOKNum");
                    monthSpotCheckDetail.TotalOKNum = values.Value<string>("TotalSpotOKNum");
                    monthSpotCheckDetail.TotalOKRate = values.Value<string>("TotalSpotOKRate");
                    MonthSpotCheckDetailService.AddMonthSpotCheckDetail(monthSpotCheckDetail);
                }
                if (GridSpecialEquipmentDetail.GetMergedData().Count > 0)//判断用户编辑数据是都大于0
                {
                    //var datasGridSpecialEquipmentDetail = GridSpecialEquipmentDetail.GetMergedData();
                    //if (datasGridSpecialEquipmentDetail.Count > 0)//把list中
                    //{
                    foreach (JObject mergedRow in GridSpecialEquipmentDetail.GetMergedData())
                    {
                        Model.Check_SpecialEquipmentDetail specialEquipmentCheckDetail = new Model.Check_SpecialEquipmentDetail();
                        int i = mergedRow.Value<int>("index");
                        string id = GridSpecialEquipmentDetail.Rows[i].RowID;
                        //specialEquipmentDetails.Remove(specialEquipmentDetails.FirstOrDefault(p => p.SpecialEquipmentDetailId.Equals(id)));//先移除指定行数据
                        JObject values = mergedRow.Value<JObject>("values");
                        //string id = GridSpecialEquipmentDetail.Rows[i].RowID;
                        specialEquipmentCheckDetail.SpecialEquipmentDetailId = id;
                        specialEquipmentCheckDetail.CheckMonthId = CheckMonth.CheckMonthId;
                        specialEquipmentCheckDetail.SpecialEquipmentName = values.Value<string>("SpecialEquipmentName");
                        specialEquipmentCheckDetail.TotalNum = values.Value<string>("TotalNum");
                        specialEquipmentCheckDetail.ThisCompleteNum1 = values.Value<string>("ThisCompleteNum1");
                        specialEquipmentCheckDetail.TotalCompleteNum1 = values.Value<string>("TotalCompleteNum1");
                        specialEquipmentCheckDetail.TotalRate1 = values.Value<string>("TotalRate1");
                        specialEquipmentCheckDetail.ThisCompleteNum2 = values.Value<string>("ThisCompleteNum2");
                        specialEquipmentCheckDetail.TotalCompleteNum2 = values.Value<string>("TotalCompleteNum2");
                        specialEquipmentCheckDetail.TotalRate2 = values.Value<string>("TotalRate2");
                        //specialEquipmentDetails.Add(specialEquipmentCheckDetail);
                        SpecialEquipmentDetailService.AddSpecialEquipmentDetail(specialEquipmentCheckDetail);
                    }
                }
                //}
                string monthDesignId = SQLHelper.GetNewID();
                monthDesignId = monthDesignId.Substring(0, monthDesignId.Length - 1);
                var mainItems = BLL.MainItemService.GetMainItemList(this.CurrUser.LoginProjectId);
                var designProfessionals = BLL.DesignProfessionalService.GetDesignProfessionalItem();
                foreach (JObject mergedRow in GridDesign.GetMergedData())  //焊工资格评定情况
                {
                    Model.Check_MonthDesign monthDesign = new Model.Check_MonthDesign();
                    int i = mergedRow.Value<int>("index");
                    JObject values = mergedRow.Value<JObject>("values");
                    //string id = GridSpecialEquipmentDetail.Rows[i].RowID;
                    monthDesign.MonthDesignId = monthDesignId + i.ToString();
                    monthDesign.CheckMonthId = CheckMonth.CheckMonthId;
                    monthDesign.DesignCode = values.Value<string>("DesignCode");
                    var mainItem = mainItems.FirstOrDefault(x => x.Text == values.Value<string>("MainItemId"));
                    if (mainItem != null)
                    {
                        monthDesign.MainItemId = mainItem.Value;
                    }
                    var designProfessional = designProfessionals.FirstOrDefault(x => x.Text == values.Value<string>("DesignProfessionalId"));
                    if (designProfessional != null)
                    {
                        monthDesign.DesignProfessionalId = designProfessional.Value;
                    }
                    monthDesign.State = values.Value<string>("State");
                    monthDesign.Remark = values.Value<string>("Remark");
                    MonthDesignService.AddMonthDesign(monthDesign);
                }
                //saveSpecialEquipmentDetails();//保存内存数据到数据库
                ShowNotify("提交成功！", MessageBoxIcon.Success);
                PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
            }
            else
            {
                Alert.ShowInTop("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
            }
        }
    }
}