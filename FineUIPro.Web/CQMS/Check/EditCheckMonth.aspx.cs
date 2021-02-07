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
                if (!string.IsNullOrEmpty(CheckMonthId))
                {
                    Model.Check_CheckMonth checkMonth = CheckMonthService.GetCheckMonth(CheckMonthId);
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
                BindWelder();
                OutputSummaryData();
                BindNDTCheck();
                BindSpotCheck();
                BindSpecialEquipmentDetail();
                BindDesign();
                //质量验收本月合格
                List<Model.View_Check_SoptCheckDetail> spotCheckDetailOKLists = SpotCheckDetailService.GetOKSpotCheckDetailListByTime1(CurrUser.LoginProjectId, startTime, endTime);
                List<Model.View_Check_SoptCheckDetail> spotCheckDetailLists = SpotCheckDetailService.GetAllSpotCheckDetailListByTime(CurrUser.LoginProjectId, startTime, endTime);
                if (spotCheckDetailOKLists.Count > 0 && spotCheckDetailLists.Count > 0)
                {
                    var a = Convert.ToDouble(spotCheckDetailOKLists.Count);
                    var b = Convert.ToDouble(spotCheckDetailLists.Count);
                    decimal result = decimal.Round(decimal.Parse((a / b * 100).ToString()), 2);
                    this.MonthOk.Text = result.ToString() + "%";
                }


                //质量验收累计合格
                List<Model.View_Check_SoptCheckDetail> TotalCheckDetailOKLists = SpotCheckDetailService.GetTotalOKSpotCheckDetailListByTime1(CurrUser.LoginProjectId, endTime);
                List<Model.View_Check_SoptCheckDetail> TotalCheckDetailLists = SpotCheckDetailService.GetTotalAllSpotCheckDetailListByTime(CurrUser.LoginProjectId, endTime);

                if (TotalCheckDetailOKLists.Count > 0 && TotalCheckDetailLists.Count > 0)
                {
                    var a = Convert.ToDouble(TotalCheckDetailOKLists.Count);
                    var b = Convert.ToDouble(TotalCheckDetailLists.Count);
                    decimal result = decimal.Round(decimal.Parse((a / b * 100).ToString()), 2);
                    this.AllOk.Text = result.ToString() + "%";
                }
                //质量记录本月同步率
                List<Model.View_Check_SoptCheckDetail> spotCheckDetailDataOKLists = SpotCheckDetailService.GetMonthDataOkSpotCheckDetailListByTime(CurrUser.LoginProjectId, startTime, endTime);

                if (spotCheckDetailOKLists.Count > 0 && spotCheckDetailDataOKLists.Count > 0)
                {
                    var a = Convert.ToDouble(spotCheckDetailDataOKLists.Count);
                    var b = Convert.ToDouble(spotCheckDetailOKLists.Where(x => x.IsDataOK != "2").ToList().Count); //当期内，资料验收合格数÷实体合格数且需要上传资料的项数 * 100 %
                    decimal result = decimal.Round(decimal.Parse((a / b * 100).ToString()), 2);
                    this.MonthDataOk.Text = result.ToString() + "%";
                }
                else
                {
                    this.MonthDataOk.Text = "0%";
                }
                //质量记录累计同步率
                List<Model.View_Check_SoptCheckDetail> TotalCheckDetailDataOKLists = SpotCheckDetailService.GetAllDataOkSpotCheckDetailListByTime(CurrUser.LoginProjectId, endTime);
                if (TotalCheckDetailDataOKLists.Count > 0 && TotalCheckDetailOKLists.Count > 0)
                {
                    var a = Convert.ToDouble(TotalCheckDetailDataOKLists.Count);
                    var b = Convert.ToDouble(TotalCheckDetailOKLists.Where(x => x.IsDataOK != "2").ToList().Count);
                    decimal result = decimal.Round(decimal.Parse((a / b * 100).ToString()), 2);
                    this.AllDataOk.Text = result.ToString() + "%";
                }
                else
                {
                    this.AllDataOk.Text = "0%";
                }
            }

        }
        private void BindRectify()
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
        private void BindNDTCheck()
        {
            List<Model.NDTCheckItem> nDTCheckItems = new List<Model.NDTCheckItem>();
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
            var totalRepairRecords = from x in Funs.DB.HJGL_RepairRecord
                                     where x.ProjectId == this.CurrUser.LoginProjectId
                                     select x;
            List<Model.Base_Unit> units = UnitService.GetUnitByProjectIdUnitTypeList(CurrUser.LoginProjectId, BLL.Const.ProjectUnitType_2);
            foreach (var unit in units)
            {
                var unitTotalNDEItems = totalNDEItems.Where(x => x.UnitId == unit.UnitId);
                var unitNDEItems = monthNDEItems.Where(x => x.UnitId == unit.UnitId);
                Model.NDTCheckItem nDTCheckItem = new Model.NDTCheckItem();
                nDTCheckItem.UnitName = unit.UnitName;
                int filmNum = unitNDEItems.Sum(x => x.TotalFilm ?? 0);
                int totalFilmNum = unitTotalNDEItems.Sum(x => x.TotalFilm ?? 0);
                nDTCheckItem.FilmNum = filmNum.ToString();
                int passFilm = unitNDEItems.Sum(x => x.PassFilm ?? 0);
                int totalPassFilm = unitTotalNDEItems.Sum(x => x.PassFilm ?? 0);
                nDTCheckItem.NotOKFileNum = (filmNum - passFilm).ToString();

                nDTCheckItem.RepairFileNum = "0";
                nDTCheckItem.OneOKRate = "0";
                nDTCheckItem.TotalFilmNum = totalFilmNum.ToString();
                nDTCheckItem.TotalNotOKFileNum = (totalFilmNum - totalPassFilm).ToString();
                nDTCheckItem.TotalOneOKRate = "0";
                nDTCheckItems.Add(nDTCheckItem);
            }
            GridNDTCheck.DataSource = nDTCheckItems;
            GridNDTCheck.DataBind();
        }
        private void BindWelder()
        {
            List<Model.WelderItem> welderItems = new List<Model.WelderItem>();
            List<Model.Base_Unit> units = UnitService.GetUnitByProjectIdUnitTypeList(CurrUser.LoginProjectId, BLL.Const.ProjectUnitType_2);
            foreach (var unit in units)
            {
                Model.WelderItem welderItem = new Model.WelderItem();
                welderItem.UnitName = unit.UnitName;
                welderItem.ThisPersonNum = "0";
                welderItem.ThisOKPersonNum = "0";
                welderItem.ThisOKRate = "0";
                welderItem.TotalPersonNum = "0";
                welderItem.TotalOKPersonNum = "0";
                welderItem.TotalOKRate = "0";
                welderItems.Add(welderItem);
            }
            GridWelder.DataSource = welderItems;
            GridWelder.DataBind();
        }

        private void BindSpotCheck()
        {
            MonthSpotCheckDetailService.DeleteMonthSpotCheckDetailsByCheckMonthId(CheckMonthId);
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
            if (!string.IsNullOrEmpty(CheckMonthId))
            {
                foreach (var monthSpotCheckDetail in monthSpotCheckDetails)
                {
                    monthSpotCheckDetail.CheckMonthId = CheckMonthId;
                    MonthSpotCheckDetailService.AddMonthSpotCheckDetail(monthSpotCheckDetail);
                }
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
            GridDesign.DataSource = DesignService.GetDesignListByTime(CurrUser.LoginProjectId, startTime, endTime);
            GridDesign.DataBind();
        }
        private void OutputSummaryData()
        {
            JObject summary = new JObject();
            summary.Add("UnitName", "合计");
            summary.Add("ThisPersonNum", "0");
            summary.Add("hisPersonNum", "0");
            summary.Add("ThisOKPersonNum", "0");
            summary.Add("ThisOKRate", "0");
            summary.Add("TotalPersonNum", "0");
            summary.Add("TotalOKPersonNum", "0");
            summary.Add("TotalOKRate", "0");
            GridWelder.SummaryData = summary;
            JObject summary1 = new JObject();
            summary1.Add("UnitName", "合计");
            summary1.Add("FilmNum", "0");
            summary1.Add("NotOKFileNum", "0");
            summary1.Add("RepairFileNum", "0");
            summary1.Add("OneOKRate", "0");
            summary1.Add("TotalFilmNum", "0");
            summary1.Add("TotalNotOKFileNum", "0");
            summary1.Add("TotalOneOKRate", "0");
            GridNDTCheck.SummaryData = summary1;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (CommonService.GetAllButtonPowerList(CurrUser.LoginProjectId, CurrUser.UserId, Const.CheckMonthMenuId, Const.BtnModify))
            {
                MonthSpotCheckDetailService.DeleteMonthSpotCheckDetailsByCheckMonthId(CheckMonthId);
                Model.Check_CheckMonth CheckMonth = new Model.Check_CheckMonth();
                CheckMonth.ProjectId = CurrUser.LoginProjectId;
                CheckMonth.ManagementOverview = txtManagementOverview.Text.Trim();
                CheckMonth.AccidentSituation = txtAccidentSituation.Text.Trim();
                CheckMonth.ConstructionData = txtConstructionData.Text.Trim();
                CheckMonth.NextMonthPlan = txtNextMonthPlan.Text.Trim();
                CheckMonth.NeedSolved = txtNeedSolved.Text.Trim();
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
                foreach (var monthSpotCheckDetail in monthSpotCheckDetails)
                {
                    monthSpotCheckDetail.CheckMonthId = CheckMonth.CheckMonthId;
                    MonthSpotCheckDetailService.AddMonthSpotCheckDetail(monthSpotCheckDetail);
                }
                SpecialEquipmentDetailService.DeleteSpecialEquipmentDetailsByCheckMonthId(CheckMonth.CheckMonthId);
                Model.Check_SpecialEquipmentDetail specialEquipmentCheckDetail = new Model.Check_SpecialEquipmentDetail();
                string modelId = SQLHelper.GetNewID(typeof(Model.Check_SpecialEquipmentDetail));
                int count = GridSpecialEquipmentDetail.Rows.Count;
                if (GridSpecialEquipmentDetail.GetMergedData().Count > 0)//判断用户编辑数据是都大于0
                {
                    //var datasGridSpecialEquipmentDetail = GridSpecialEquipmentDetail.GetMergedData();
                    //if (datasGridSpecialEquipmentDetail.Count > 0)//把list中
                    //{
                    foreach (JObject mergedRow in GridSpecialEquipmentDetail.GetMergedData())
                    {
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