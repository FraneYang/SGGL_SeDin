using BLL;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FineUIPro.Web.HSSE.InformationProject
{
    public partial class AccidentCauseReportEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        private string AccidentCauseReportId
        {
            get
            {
                return (string)ViewState["AccidentCauseReportId"];
            }
            set
            {
                ViewState["AccidentCauseReportId"] = value;
            }
        }

        #region 项目主键
        /// <summary>
        /// 项目主键
        /// </summary>
        public string ProjectId
        {
            get
            {
                return (string)ViewState["ProjectId"];
            }
            set
            {
                ViewState["ProjectId"] = value;
            }
        }
        #endregion

        /// <summary>
        /// 定义集合
        /// </summary>
        private static List<Model.InformationProject_AccidentCauseReportItem> items = new List<Model.InformationProject_AccidentCauseReportItem>();
        #endregion

        #region 加载页面
        /// <summary>
        /// 加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                items.Clear();
                this.ProjectId = this.CurrUser.LoginProjectId;
                if (!string.IsNullOrEmpty(Request.Params["projectId"]) && Request.Params["projectId"] != this.CurrUser.LoginProjectId)
                {
                    this.ProjectId = Request.Params["projectId"];
                }
                BLL.ConstValue.InitConstValueDropDownList(this.drpYear, BLL.ConstValue.Group_0008, true);
                BLL.ConstValue.InitConstValueDropDownList(this.drpMonth, BLL.ConstValue.Group_0009, true);
                BLL.UserService.InitUserDropDownList(this.drpCompileMan, this.ProjectId, true);
                this.AccidentCauseReportId = Request.Params["AccidentCauseReportId"];
                if (!string.IsNullOrEmpty(this.AccidentCauseReportId))
                {
                    var q = BLL.ProjectAccidentCauseReportService.GetAccidentCauseReportById(AccidentCauseReportId);
                    if (q != null)
                    {
                        this.ProjectId = q.ProjectId;
                        BLL.UserService.InitUserDropDownList(this.drpCompileMan, this.ProjectId, true);
                        drpMonth.SelectedValue = q.Month.ToString();
                        drpYear.SelectedValue = q.Year.ToString();
                        lbMonth1.Text = "(" + drpMonth.SelectedText + ")";
                        lbMonth2.Text = "(" + drpMonth.SelectedText + ")";
                        lbMonth3.Text = "(" + drpMonth.SelectedText + ")";
                        if (DateTime.Now.Month == 1)
                        {
                            lbLastMonth.Text = "(十二月)";
                        }
                        else
                        {
                            int month = Funs.GetNewIntOrZero(drpMonth.SelectedValue);
                            string lastMonth = (from x in Funs.DB.Sys_Const where x.GroupId == BLL.ConstValue.Group_0009 && Convert.ToInt32(x.ConstValue) == (month - 1) select x.ConstText).FirstOrDefault();
                            lbLastMonth.Text = "(" + lastMonth + ")";
                        }
                        txtAccidentCauseReportCode.Text = q.AccidentCauseReportCode;
                        if (q.DeathAccident != null)
                        {
                            txtDeathAccident.Text = q.DeathAccident.ToString();
                        }
                        if (q.DeathToll != null)
                        {
                            txtDeathToll.Text = q.DeathToll.ToString();
                        }
                        if (q.InjuredAccident != null)
                        {
                            txtInjuredAccident.Text = q.InjuredAccident.ToString();
                        }
                        if (q.InjuredToll != null)
                        {
                            txtInjuredToll.Text = q.InjuredToll.ToString();
                        }
                        if (q.MinorWoundAccident != null)
                        {
                            txtMinorWoundAccident.Text = q.MinorWoundAccident.ToString();
                        }
                        if (q.MinorWoundToll != null)
                        {
                            txtMinorWoundToll.Text = q.MinorWoundToll.ToString();
                        }
                        if (q.AverageTotalHours != null)
                        {
                            txtAverageTotalHours.Text = q.AverageTotalHours.ToString();
                        }
                        if (q.AverageManHours != null)
                        {
                            txtAverageManHours.Text = q.AverageManHours.ToString();
                        }
                        if (q.TotalLossMan != null)
                        {
                            txtTotalLossMan.Text = q.TotalLossMan.ToString();
                        }
                        if (q.LastMonthLossHoursTotal != null)
                        {
                            txtLastMonthLossHoursTotal.Text = q.LastMonthLossHoursTotal.ToString();
                        }
                        if (q.KnockOffTotal != null)
                        {
                            txtKnockOffTotal.Text = q.KnockOffTotal.ToString();
                        }
                        if (q.DirectLoss != null)
                        {
                            txtDirectLoss.Text = q.DirectLoss.ToString();
                        }
                        if (q.IndirectLosses != null)
                        {
                            txtIndirectLosses.Text = q.IndirectLosses.ToString();
                        }
                        if (q.TotalLoss != null)
                        {
                            txtTotalLoss.Text = q.TotalLoss.ToString();
                        }
                        if (q.TotalLossTime != null)
                        {
                            txtTotalLossTime.Text = q.TotalLossTime.ToString();
                        }
                        if (!string.IsNullOrEmpty(q.CompileMan))
                        {
                            this.drpCompileMan.SelectedValue = q.CompileMan;
                        }
                        if (q.CompileDate != null)
                        {
                            this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", q.CompileDate);
                        }
                        items = BLL.ProjectAccidentCauseReportItemService.GetItemsNoSum(AccidentCauseReportId);
                        this.Grid1.DataSource = items;
                        this.Grid1.DataBind();

                    }
                }
                else
                {
                    this.drpYear.SelectedValue = DateTime.Now.Year.ToString();
                    this.drpMonth.SelectedValue = DateTime.Now.Month.ToString();
                    DateTime startTime = Convert.ToDateTime(this.drpYear.SelectedValue + "-" + this.drpMonth.SelectedValue + "-01");
                    DateTime endTime = startTime.AddMonths(1);
                    GetData(startTime, endTime);
                    lbMonth1.Text = "(" + drpMonth.SelectedText + ")";
                    lbMonth2.Text = "(" + drpMonth.SelectedText + ")";
                    lbMonth3.Text = "(" + drpMonth.SelectedText + ")";
                    if (DateTime.Now.Month == 1)
                    {
                        lbLastMonth.Text = "(十二月)";
                    }
                    else
                    {
                        string lastMonth = (from x in Funs.DB.Sys_Const where x.GroupId == BLL.ConstValue.Group_0009 && Convert.ToInt32(x.ConstValue) == (Convert.ToInt32(this.drpMonth.SelectedValue) - 1) select x.ConstText).FirstOrDefault();
                        lbLastMonth.Text = "(" + lastMonth + ")"; ;
                    }
                    var accidentTypes = BLL.ConstValue.drpConstItemList(BLL.ConstValue.Group_0012);
                    foreach (var a in accidentTypes)
                    {
                        if (a.ConstText != "总计")
                        {
                            Model.InformationProject_AccidentCauseReportItem item = new Model.InformationProject_AccidentCauseReportItem
                            {
                                AccidentCauseReportItemId = SQLHelper.GetNewID(typeof(Model.InformationProject_AccidentCauseReportItem)),
                                AccidentType = a.ConstText,
                                TotalDeath = 0,
                                TotalInjuries = 0,
                                TotalMinorInjuries = 0,
                                Death1 = 0,
                                Injuries1 = 0,
                                MinorInjuries1 = 0,
                                Death2 = 0,
                                Injuries2 = 0,
                                MinorInjuries2 = 0,
                                Death3 = 0,
                                Injuries3 = 0,
                                MinorInjuries3 = 0,
                                Death4 = 0,
                                Injuries4 = 0,
                                MinorInjuries4 = 0,
                                Death5 = 0,
                                Injuries5 = 0,
                                MinorInjuries5 = 0,
                                Death6 = 0,
                                Injuries6 = 0,
                                MinorInjuries6 = 0,
                                Death7 = 0,
                                Injuries7 = 0,
                                MinorInjuries7 = 0,
                                Death8 = 0,
                                Injuries8 = 0,
                                MinorInjuries8 = 0,
                                Death9 = 0,
                                Injuries9 = 0,
                                MinorInjuries9 = 0,
                                Death10 = 0,
                                Injuries10 = 0,
                                MinorInjuries10 = 0,
                                Death11 = 0,
                                Injuries11 = 0,
                                MinorInjuries11 = 0
                            };
                            items.Add(item);
                        }
                    }
                    this.Grid1.DataSource = items;
                    this.Grid1.DataBind();
                    this.drpCompileMan.SelectedValue = this.CurrUser.UserId;
                    this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);

                    ////自动生成编码
                    this.txtAccidentCauseReportCode.Text = BLL.CodeRecordsService.ReturnCodeByMenuIdProjectId(BLL.Const.ProjectAccidentCauseReportMenuId, this.ProjectId, this.CurrUser.UnitId);
                }
                var unit = BLL.UnitService.GetUnitByUnitId(this.CurrUser.UnitId);
                if (unit != null)
                {
                    this.lblUnitName.Text = unit.UnitName;
                }

                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.ProjectAccidentCauseReportMenuId;
                this.ctlAuditFlow.DataId = this.AccidentCauseReportId;
                this.ctlAuditFlow.ProjectId = this.ProjectId;
            }
        }
        #endregion

        #region 月份下拉事件
        /// <summary>
        /// 月份下拉事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbMonth1.Text = "(" + drpMonth.SelectedText + ")";
            lbMonth2.Text = "(" + drpMonth.SelectedText + ")";
            lbMonth3.Text = "(" + drpMonth.SelectedText + ")";
            if (drpMonth.SelectedValue == "1")
            {
                lbLastMonth.Text = "(十二月)";
            }
            else
            {
                int month = Funs.GetNewIntOrZero(drpMonth.SelectedValue);
                string lastMonth = (from x in Funs.DB.Sys_Const where x.GroupId == BLL.ConstValue.Group_0009 && Convert.ToInt32(x.ConstValue) == (month - 1) select x.ConstText).FirstOrDefault();
                lbLastMonth.Text = "(" + lastMonth + ")";
            }
            if (this.drpYear.SelectedValue != BLL.Const._Null && this.drpMonth.SelectedValue != BLL.Const._Null)
            {
                DateTime startTime = Convert.ToDateTime(this.drpYear.SelectedValue + "-" + this.drpMonth.SelectedValue + "-01");
                DateTime endTime = startTime.AddMonths(1);
                GetData(startTime, endTime);
            }
        }
        #endregion

        #region 保存、提交
        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (this.drpYear.SelectedValue == BLL.Const._Null)
            {
                Alert.ShowInTop("请选择年度", MessageBoxIcon.Warning);
                return;
            }
            if (this.drpMonth.SelectedValue == BLL.Const._Null)
            {
                Alert.ShowInTop("请选择月份", MessageBoxIcon.Warning);
                return;
            }
            this.SaveData(BLL.Const.BtnSave);
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
        }

        /// <summary>
        /// 提交按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (this.drpYear.SelectedValue == BLL.Const._Null)
            {
                Alert.ShowInTop("请选择年度", MessageBoxIcon.Warning);
                return;
            }
            if (this.drpMonth.SelectedValue == BLL.Const._Null)
            {
                Alert.ShowInTop("请选择月份", MessageBoxIcon.Warning);
                return;
            }
            this.SaveData(BLL.Const.BtnSubmit);
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="p"></param>
        private void SaveData(string type)
        {
            Model.InformationProject_AccidentCauseReport accidentCauseReport = new Model.InformationProject_AccidentCauseReport
            {
                ProjectId = this.ProjectId,
                UnitId = string.IsNullOrEmpty(this.CurrUser.UnitId) ? Const.UnitId_SEDIN : this.CurrUser.UnitId,
                AccidentCauseReportCode = txtAccidentCauseReportCode.Text.Trim()
            };
            if (this.drpYear.SelectedValue != BLL.Const._Null)
            {
                accidentCauseReport.Year = Funs.GetNewIntOrZero(drpYear.SelectedValue);
            }
            if (this.drpMonth.SelectedValue != BLL.Const._Null)
            {
                accidentCauseReport.Month = Funs.GetNewIntOrZero(drpMonth.SelectedValue);
            }
            accidentCauseReport.DeathAccident = Funs.GetNewIntOrZero(txtDeathAccident.Text.Trim());
            accidentCauseReport.DeathToll = Funs.GetNewIntOrZero(txtDeathToll.Text.Trim());
            accidentCauseReport.InjuredAccident = Funs.GetNewIntOrZero(txtInjuredAccident.Text.Trim());
            accidentCauseReport.InjuredToll = Funs.GetNewIntOrZero(txtInjuredToll.Text.Trim());
            accidentCauseReport.MinorWoundAccident = Funs.GetNewIntOrZero(txtMinorWoundAccident.Text.Trim());
            accidentCauseReport.MinorWoundToll = Funs.GetNewIntOrZero(txtMinorWoundToll.Text.Trim());
            accidentCauseReport.AverageTotalHours = Funs.GetNewDecimalOrZero(txtAverageTotalHours.Text.Trim());
            accidentCauseReport.AverageManHours = Funs.GetNewIntOrZero(txtAverageManHours.Text.Trim());
            accidentCauseReport.TotalLossMan = Funs.GetNewIntOrZero(txtTotalLossMan.Text.Trim());
            accidentCauseReport.LastMonthLossHoursTotal = Funs.GetNewIntOrZero(txtLastMonthLossHoursTotal.Text.Trim());
            accidentCauseReport.KnockOffTotal = Funs.GetNewIntOrZero(txtKnockOffTotal.Text.Trim());
            accidentCauseReport.DirectLoss = Funs.GetNewIntOrZero(txtDirectLoss.Text.Trim());
            accidentCauseReport.IndirectLosses = Funs.GetNewIntOrZero(txtIndirectLosses.Text.Trim());
            accidentCauseReport.TotalLoss = Funs.GetNewIntOrZero(txtTotalLoss.Text.Trim());
            accidentCauseReport.TotalLossTime = Funs.GetNewIntOrZero(txtTotalLossTime.Text.Trim());
            if (this.drpCompileMan.SelectedValue != BLL.Const._Null)
            {
                accidentCauseReport.CompileMan = this.drpCompileMan.SelectedValue;
            }
            accidentCauseReport.CompileDate = Funs.GetNewDateTime(this.txtCompileDate.Text.Trim());
            accidentCauseReport.States = BLL.Const.State_0;
            if (type == BLL.Const.BtnSubmit)
            {
                accidentCauseReport.States = this.ctlAuditFlow.NextStep;
            }
            if (!string.IsNullOrEmpty(this.AccidentCauseReportId))
            {
                accidentCauseReport.AccidentCauseReportId = this.AccidentCauseReportId;
                BLL.ProjectAccidentCauseReportService.UpdateAccidentCauseReport(accidentCauseReport);
                BLL.LogService.AddSys_Log(this.CurrUser, accidentCauseReport.AccidentCauseReportCode, accidentCauseReport.AccidentCauseReportId, BLL.Const.ProjectAccidentCauseReportMenuId, BLL.Const.BtnModify);
                BLL.ProjectAccidentCauseReportItemService.DeleteAccidentCauseReportItemByAccidentCauseReportId(this.AccidentCauseReportId);
            }
            else
            {
                Model.InformationProject_AccidentCauseReport oldAccidentCauseReport = (from x in Funs.DB.InformationProject_AccidentCauseReport
                                                                                       where x.ProjectId == accidentCauseReport.ProjectId && x.Year == accidentCauseReport.Year && x.Month == accidentCauseReport.Month
                                                                                       select x).FirstOrDefault();
                if (oldAccidentCauseReport == null)
                {
                    this.AccidentCauseReportId = SQLHelper.GetNewID(typeof(Model.InformationProject_AccidentCauseReport));
                    accidentCauseReport.AccidentCauseReportId = this.AccidentCauseReportId;
                    BLL.ProjectAccidentCauseReportService.AddAccidentCauseReport(accidentCauseReport);
                    BLL.LogService.AddSys_Log(this.CurrUser, accidentCauseReport.AccidentCauseReportCode, accidentCauseReport.AccidentCauseReportId, BLL.Const.ProjectAccidentCauseReportMenuId, BLL.Const.BtnAdd);
                    ////删除未上报月报信息
                    //Model.ManagementReport_ReportRemind reportRemind = (from x in Funs.DB.ManagementReport_ReportRemind
                    //                                                    where x.ProjectId == this.ProjectId && x.Year == accidentCauseReport.Year && x.Month == accidentCauseReport.Month && x.ReportName == "职工伤亡事故原因分析报"
                    //                                                    select x).FirstOrDefault();
                    //if (reportRemind != null)
                    //{
                    //    BLL.ReportRemindService.DeleteReportRemindByReportRemind(reportRemind);
                    //}
                }
                else
                {
                    Alert.ShowInTop("该月份记录已存在", MessageBoxIcon.Warning);
                    return;
                }
            }
            AddItems(accidentCauseReport.AccidentCauseReportId);//增加明细
            ////保存流程审核数据         
            this.ctlAuditFlow.btnSaveData(this.ProjectId, BLL.Const.ProjectAccidentCauseReportMenuId, this.AccidentCauseReportId, (type == BLL.Const.BtnSubmit ? true : false), accidentCauseReport.Year + "-" + accidentCauseReport.Month, "../InformationProject/AccidentCauseReportView.aspx?AccidentCauseReportId={0}");
        }

        /// <summary>
        /// 增加明细
        /// </summary>
        /// <param name="accidentCauseReportId"></param>
        private void AddItems(string accidentCauseReportId)
        {
            int Death1 = 0, Injuries1 = 0, MinorInjuries1 = 0, Death2 = 0, Injuries2 = 0, MinorInjuries2 = 0, Death3 = 0, Injuries3 = 0, MinorInjuries3 = 0,
                           Death4 = 0, Injuries4 = 0, MinorInjuries4 = 0, Death5 = 0, Injuries5 = 0, MinorInjuries5 = 0, Death6 = 0, Injuries6 = 0, MinorInjuries6 = 0,
                           Death7 = 0, Injuries7 = 0, MinorInjuries7 = 0, Death8 = 0, Injuries8 = 0, MinorInjuries8 = 0, Death9 = 0, Injuries9 = 0, MinorInjuries9 = 0,
                           Death10 = 0, Injuries10 = 0, MinorInjuries10 = 0, Death11 = 0, Injuries11 = 0, MinorInjuries11 = 0;
            foreach (JObject mergedRow in Grid1.GetMergedData())
            {
                JObject values = mergedRow.Value<JObject>("values");
                if (values["AccidentType"].ToString() != "")
                {
                    int sumD = 0, sumI = 0, sumM = 0;
                    Model.InformationProject_AccidentCauseReportItem item = items.FirstOrDefault(x => x.AccidentType == values["AccidentType"].ToString());
                    if (values["Death1"].ToString() != "")
                    {
                        item.Death1 = values.Value<int>("Death1");
                        Death1 += values.Value<int>("Death1");
                        sumD += values.Value<int>("Death1");
                    }
                    if (values["Injuries1"].ToString() != "")
                    {
                        item.Injuries1 = values.Value<int>("Injuries1");
                        Injuries1 += values.Value<int>("Injuries1");
                        sumI += values.Value<int>("Injuries1");
                    }
                    if (values["MinorInjuries1"].ToString() != "")
                    {
                        item.MinorInjuries1 = values.Value<int>("MinorInjuries1");
                        MinorInjuries1 += values.Value<int>("MinorInjuries1");
                        sumM += values.Value<int>("MinorInjuries1");
                    }
                    if (values["Death2"].ToString() != "")
                    {
                        item.Death2 = values.Value<int>("Death2");
                        Death2 += values.Value<int>("Death2");
                        sumD += values.Value<int>("Death2");
                    }
                    if (values["Injuries2"].ToString() != "")
                    {
                        item.Injuries2 = values.Value<int>("Injuries2");
                        Injuries2 += values.Value<int>("Injuries2");
                        sumI += values.Value<int>("Injuries2");
                    }
                    if (values["MinorInjuries2"].ToString() != "")
                    {
                        item.MinorInjuries2 = values.Value<int>("MinorInjuries2");
                        MinorInjuries2 += values.Value<int>("MinorInjuries2");
                        sumM += values.Value<int>("MinorInjuries2");
                    }
                    if (values["Death3"].ToString() != "")
                    {
                        item.Death3 = values.Value<int>("Death3");
                        Death3 += values.Value<int>("Death3");
                        sumD += values.Value<int>("Death3");
                    }
                    if (values["Injuries3"].ToString() != "")
                    {
                        item.Injuries3 = values.Value<int>("Injuries3");
                        Injuries3 += values.Value<int>("Injuries3");
                        sumI += values.Value<int>("Injuries3");
                    }
                    if (values["MinorInjuries3"].ToString() != "")
                    {
                        item.MinorInjuries3 = values.Value<int>("MinorInjuries3");
                        MinorInjuries3 += values.Value<int>("MinorInjuries3");
                        sumM += values.Value<int>("MinorInjuries3");
                    }
                    if (values["Death4"].ToString() != "")
                    {
                        item.Death4 = values.Value<int>("Death4");
                        Death4 += values.Value<int>("Death4");
                        sumD += values.Value<int>("Death4");
                    }
                    if (values["Injuries4"].ToString() != "")
                    {
                        item.Injuries4 = values.Value<int>("Injuries4");
                        Injuries4 += values.Value<int>("Injuries4");
                        sumI += values.Value<int>("Injuries4");
                    }
                    if (values["MinorInjuries4"].ToString() != "")
                    {
                        item.MinorInjuries4 = values.Value<int>("MinorInjuries4");
                        MinorInjuries4 += values.Value<int>("MinorInjuries4");
                        sumM += values.Value<int>("MinorInjuries4");
                    }
                    if (values["Death5"].ToString() != "")
                    {
                        item.Death5 = values.Value<int>("Death5");
                        Death5 += values.Value<int>("Death5");
                        sumD += values.Value<int>("Death5");
                    }
                    if (values["Injuries5"].ToString() != "")
                    {
                        item.Injuries5 = values.Value<int>("Injuries5");
                        Injuries5 += values.Value<int>("Injuries5");
                        sumI += values.Value<int>("Injuries5");
                    }
                    if (values["MinorInjuries5"].ToString() != "")
                    {
                        item.MinorInjuries5 = values.Value<int>("MinorInjuries5");
                        MinorInjuries5 += values.Value<int>("MinorInjuries5");
                        sumM += values.Value<int>("MinorInjuries5");
                    }
                    if (values["Death6"].ToString() != "")
                    {
                        item.Death6 = values.Value<int>("Death6");
                        Death6 += values.Value<int>("Death6");
                        sumD += values.Value<int>("Death6");
                    }
                    if (values["Injuries6"].ToString() != "")
                    {
                        item.Injuries6 = values.Value<int>("Injuries6");
                        Injuries6 += values.Value<int>("Injuries6");
                        sumI += values.Value<int>("Injuries6");
                    }
                    if (values["MinorInjuries6"].ToString() != "")
                    {
                        item.MinorInjuries6 = values.Value<int>("MinorInjuries6");
                        MinorInjuries6 += values.Value<int>("MinorInjuries6");
                        sumM += values.Value<int>("MinorInjuries6");
                    }
                    if (values["Death7"].ToString() != "")
                    {
                        item.Death7 = values.Value<int>("Death7");
                        Death7 += values.Value<int>("Death7");
                        sumD += values.Value<int>("Death7");
                    }
                    if (values["Injuries7"].ToString() != "")
                    {
                        item.Injuries7 = values.Value<int>("Injuries7");
                        Injuries7 += values.Value<int>("Injuries7");
                        sumI += values.Value<int>("Injuries7");
                    }
                    if (values["MinorInjuries7"].ToString() != "")
                    {
                        item.MinorInjuries7 = values.Value<int>("MinorInjuries7");
                        MinorInjuries7 += values.Value<int>("MinorInjuries7");
                        sumM += values.Value<int>("MinorInjuries7");
                    }
                    if (values["Death8"].ToString() != "")
                    {
                        item.Death8 = values.Value<int>("Death8");
                        Death8 += values.Value<int>("Death8");
                        sumD += values.Value<int>("Death8");
                    }
                    if (values["Injuries8"].ToString() != "")
                    {
                        item.Injuries8 = values.Value<int>("Injuries8");
                        Injuries8 += values.Value<int>("Injuries8");
                        sumI += values.Value<int>("Injuries8");
                    }
                    if (values["MinorInjuries8"].ToString() != "")
                    {
                        item.MinorInjuries8 = values.Value<int>("MinorInjuries8");
                        MinorInjuries8 += values.Value<int>("MinorInjuries8");
                        sumM += values.Value<int>("MinorInjuries8");
                    }
                    if (values["Death9"].ToString() != "")
                    {
                        item.Death9 = values.Value<int>("Death9");
                        Death9 += values.Value<int>("Death9");
                        sumD += values.Value<int>("Death9");
                    }
                    if (values["Injuries9"].ToString() != "")
                    {
                        item.Injuries9 = values.Value<int>("Injuries9");
                        Injuries9 += values.Value<int>("Injuries9");
                        sumI += values.Value<int>("Injuries9");
                    }
                    if (values["MinorInjuries9"].ToString() != "")
                    {
                        item.MinorInjuries9 = values.Value<int>("MinorInjuries9");
                        MinorInjuries9 += values.Value<int>("MinorInjuries9");
                        sumM += values.Value<int>("MinorInjuries9");
                    }
                    if (values["Death10"].ToString() != "")
                    {
                        item.Death10 = values.Value<int>("Death10");
                        Death10 += values.Value<int>("Death10");
                        sumD += values.Value<int>("Death10");
                    }
                    if (values["Injuries10"].ToString() != "")
                    {
                        item.Injuries10 = values.Value<int>("Injuries10");
                        Injuries10 += values.Value<int>("Injuries10");
                        sumI += values.Value<int>("Injuries10");
                    }
                    if (values["MinorInjuries10"].ToString() != "")
                    {
                        item.MinorInjuries10 = values.Value<int>("MinorInjuries10");
                        MinorInjuries10 += values.Value<int>("MinorInjuries10");
                        sumM += values.Value<int>("MinorInjuries10");
                    }
                    if (values["Death11"].ToString() != "")
                    {
                        item.Death11 = values.Value<int>("Death11");
                        Death11 += values.Value<int>("Death11");
                        sumD += values.Value<int>("Death11");
                    }
                    if (values["Injuries11"].ToString() != "")
                    {
                        item.Injuries11 = values.Value<int>("Injuries11");
                        Injuries11 += values.Value<int>("Injuries11");
                        sumI += values.Value<int>("Injuries11");
                    }
                    if (values["MinorInjuries11"].ToString() != "")
                    {
                        item.MinorInjuries11 = values.Value<int>("MinorInjuries11");
                        MinorInjuries11 += values.Value<int>("MinorInjuries11");
                        sumM += values.Value<int>("MinorInjuries11");
                    }
                    item.TotalDeath = sumD;
                    item.TotalInjuries = sumI;
                    item.TotalMinorInjuries = sumM;
                }
            }
            Model.InformationProject_AccidentCauseReportItem totalItem = new Model.InformationProject_AccidentCauseReportItem
            {
                AccidentCauseReportItemId = SQLHelper.GetNewID(typeof(Model.InformationProject_AccidentCauseReportItem)),
                AccidentType = "总计",
                TotalDeath = Death1 + Death2 + Death3 + Death4 + Death5 + Death6 + Death7 + Death8 + Death9 + Death10 + Death11,
                TotalInjuries = Injuries1 + Injuries2 + Injuries3 + Injuries4 + Injuries5 + Injuries6 + Injuries7 + Injuries8 + Injuries9 + Injuries10 + Injuries11,
                TotalMinorInjuries = MinorInjuries1 + MinorInjuries2 + MinorInjuries3 + MinorInjuries4 + MinorInjuries5 + MinorInjuries6 + MinorInjuries7 + MinorInjuries8 + MinorInjuries9 + MinorInjuries10 + MinorInjuries11,
                Death1 = Death1,
                Death2 = Death2,
                Death3 = Death3,
                Death4 = Death4,
                Death5 = Death5,
                Death6 = Death6,
                Death7 = Death7,
                Death8 = Death8,
                Death9 = Death9,
                Death10 = Death10,
                Death11 = Death11,
                Injuries1 = Injuries1,
                Injuries2 = Injuries2,
                Injuries3 = Injuries3,
                Injuries4 = Injuries4,
                Injuries5 = Injuries5,
                Injuries6 = Injuries6,
                Injuries7 = Injuries7,
                Injuries8 = Injuries8,
                Injuries9 = Injuries9,
                Injuries10 = Injuries10,
                Injuries11 = Injuries11,
                MinorInjuries1 = MinorInjuries1,
                MinorInjuries2 = MinorInjuries2,
                MinorInjuries3 = MinorInjuries3,
                MinorInjuries4 = MinorInjuries4,
                MinorInjuries5 = MinorInjuries5,
                MinorInjuries6 = MinorInjuries6,
                MinorInjuries7 = MinorInjuries7,
                MinorInjuries8 = MinorInjuries8,
                MinorInjuries9 = MinorInjuries9,
                MinorInjuries10 = MinorInjuries10,
                MinorInjuries11 = MinorInjuries11
            };
            items.Add(totalItem);
            foreach (var item in items)
            {
                item.AccidentCauseReportId = accidentCauseReportId;
                BLL.ProjectAccidentCauseReportItemService.AddAccidentCauseReportItem(item);
            }
        }
        #endregion

        #region 年月变化事件
        /// <summary>
        /// 年月变化事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.drpYear.SelectedValue != BLL.Const._Null && this.drpMonth.SelectedValue != BLL.Const._Null)
            {
                DateTime startTime = Convert.ToDateTime(this.drpYear.SelectedValue + "-" + this.drpMonth.SelectedValue + "-01");
                DateTime endTime = startTime.AddMonths(1);
                GetData(startTime, endTime);
            }
            else
            {

            }
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        private void GetData(DateTime startTime, DateTime endTime)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                //死亡事故
                var accidentReports1 = BLL.AccidentReportService.GetAccidentReportsByAccidentTypeAndTime(startTime, endTime, this.ProjectId, "1");
                this.txtDeathAccident.Text = accidentReports1.Count().ToString();
                this.txtDeathToll.Text = accidentReports1.Sum(x => x.PeopleNum ?? 0).ToString();
                //重伤事故
                var accidentReports2 = BLL.AccidentReportService.GetAccidentReportsByAccidentTypeAndTime(startTime, endTime, this.ProjectId, "2");
                this.txtInjuredAccident.Text = accidentReports2.Count().ToString();
                this.txtInjuredToll.Text = accidentReports2.Sum(x => x.PeopleNum ?? 0).ToString();
                //轻伤事故
                var accidentReports3 = BLL.AccidentReportService.GetAccidentReportsByAccidentTypeAndTime(startTime, endTime, this.ProjectId, "3");
                this.txtMinorWoundAccident.Text = accidentReports3.Count().ToString();
                this.txtMinorWoundToll.Text = accidentReports3.Sum(x => x.PeopleNum ?? 0).ToString();
                int sumPersonTotal = 0;
                int sumPersonWorkTimeTotal = 0;
                // List<Model.Manager_ManhoursSortB> manhoursSortBs = new List<Model.Manager_ManhoursSortB>();
                //获取当期人工时日报
                List<Model.SitePerson_DayReport> dayReports = BLL.SitePerson_DayReportService.GetDayReportsByCompileDate(startTime, endTime, this.ProjectId);
                //获取单位集合
                var unitIds = (from x in db.Project_ProjectUnit
                               where x.ProjectId == this.ProjectId
                               select x.UnitId).ToList();
                foreach (var unitId in unitIds)
                {
                    //员工总数
                    decimal personNum = (from x in dayReports
                                         join y in db.SitePerson_DayReportDetail
                                      on x.DayReportId equals y.DayReportId
                                         where y.UnitId == unitId
                                         select y.RealPersonNum ?? 0).Sum();
                    int count = BLL.SitePerson_DayReportService.GetDayReportsByCompileDateAndUnitId(startTime, endTime, this.CurrUser.LoginProjectId, unitId).Count();
                    if (count > 0)
                    {
                        decimal persontotal = Convert.ToDecimal(Math.Round(personNum / count, 2));
                        if (persontotal.ToString().IndexOf(".") > 0 && persontotal.ToString().Substring(persontotal.ToString().IndexOf("."), persontotal.ToString().Length - persontotal.ToString().IndexOf(".")) != ".00")
                        {
                            string personint = persontotal.ToString().Substring(0, persontotal.ToString().IndexOf("."));
                            sumPersonTotal += Convert.ToInt32(personint) + 1;
                        }
                        else
                        {
                            sumPersonTotal += Convert.ToInt32(persontotal);
                        }
                    }
                    //完成人工时（当月）
                    decimal personWorkTimeTotal = (from x in dayReports
                                                   join y in db.SitePerson_DayReportDetail
                                                on x.DayReportId equals y.DayReportId
                                                   where y.UnitId == unitId
                                                   select y.PersonWorkTime ?? 0).Sum();
                    sumPersonWorkTimeTotal += Convert.ToInt32(personWorkTimeTotal);
                }
                //平均工时总数
                this.txtAverageTotalHours.Text = sumPersonWorkTimeTotal.ToString();
                //人数
                this.txtAverageManHours.Text = sumPersonTotal.ToString();
                //损失工时总数
                var accidentReports4 = BLL.AccidentReportService.GetAccidentReportsByAccidentTime(startTime, endTime, this.ProjectId);
                this.txtTotalLossMan.Text = accidentReports4.Sum(x => x.WorkingHoursLoss ?? 0).ToString();
                var accidentReports5 = BLL.AccidentReportService.GetAccidentReportsByAccidentTime(startTime.AddMonths(-1), endTime.AddMonths(-1), this.ProjectId);
                this.txtLastMonthLossHoursTotal.Text = accidentReports5.Sum(x => x.WorkingHoursLoss ?? 0).ToString();
                //歇工总日数
                var accidentReports6 = BLL.AccidentReportService.GetRecordAccidentReportsByAccidentTime(startTime, endTime, this.ProjectId);
                decimal totalLoseHours = accidentReports6.Sum(x => x.WorkingHoursLoss ?? 0);
                this.txtKnockOffTotal.Text = decimal.Round(totalLoseHours / 8, 2).ToString().Split('.')[0];
                //经济损失
                List<Model.Accident_AccidentReport> accidentReports = BLL.AccidentReportService.GetAccidentReportsByAccidentTime(startTime, endTime, this.ProjectId);
                List<Model.Accident_AccidentReportOther> accidentReportOthers = BLL.AccidentReportOtherService.GetAccidentReportOthersByAccidentTime(startTime, endTime, this.ProjectId);
                this.txtDirectLoss.Text = (accidentReports.Sum(x => x.EconomicLoss ?? 0) + accidentReportOthers.Sum(x => x.EconomicLoss ?? 0)).ToString();
                this.txtIndirectLosses.Text = (accidentReports.Sum(x => x.EconomicOtherLoss ?? 0) + accidentReportOthers.Sum(x => x.EconomicOtherLoss ?? 0)).ToString();
                this.txtTotalLoss.Text = (Funs.GetNewDecimalOrZero(this.txtDirectLoss.Text.Trim()) + Funs.GetNewDecimalOrZero(this.txtIndirectLosses.Text.Trim())).ToString();
                //无损失工时总数
                int totalSafeHours = 0;
                // 冻结时间
                var sysSet = BLL.ConstValue.drpConstItemList(BLL.ConstValue.Group_MonthReportFreezeDay).FirstOrDefault();
                int freezeDay = !string.IsNullOrEmpty(sysSet.ConstValue) ? Convert.ToInt32(sysSet.ConstValue) : 5;
                //Model.Manager_MonthReportB lastMonthReport = BLL.MonthReportBService.GetLastMonthReportByDate(DateTime.Now, freezeDay, this.ProjectId);
                Model.Accident_AccidentReport maxAccident = BLL.AccidentReportService.GetMaxAccidentTimeReport(endTime, this.ProjectId);
                DateTime? maxAccidentTime = null;
                if (maxAccident != null)
                {
                    maxAccidentTime = maxAccident.AccidentDate;
                }
                if (maxAccidentTime != null)
                {
                    DateTime newTime = Convert.ToDateTime(maxAccidentTime);
                    if (newTime.AddDays(1).Year > newTime.Year || newTime.AddDays(1).Month > newTime.Month)
                    {
                        this.txtTotalLossTime.Text = "0";
                    }
                    else
                    {
                        if (startTime >= newTime)
                        {
                            //if (lastMonthReport != null)
                            //{
                            //    totalSafeHours = (lastMonthReport.TotalHseManhours ?? 0) + sumPersonWorkTimeTotal;
                            //}
                            //else
                            //{
                            totalSafeHours = sumPersonWorkTimeTotal;
                            // }
                        }
                        else
                        {
                            int? sumHseManhours2 = 0;
                            List<Model.SitePerson_DayReport> newDayReports2 = BLL.SitePerson_DayReportService.GetDayReportsByCompileDate(newTime.AddDays(1), endTime, this.ProjectId);
                            if (newDayReports2.Count > 0)
                            {
                                foreach (var dayReport in newDayReports2)
                                {
                                    sumHseManhours2 += Convert.ToInt32((from y in db.SitePerson_DayReportDetail where y.DayReportId == dayReport.DayReportId.ToString() select y.PersonWorkTime ?? 0).Sum());
                                }
                            }
                            else
                            {
                                sumHseManhours2 = 0;
                            }
                            totalSafeHours = sumHseManhours2 ?? 0;
                        }
                    }
                }
                else
                {
                    //if (lastMonthReport != null)
                    //{
                    //    totalSafeHours = (lastMonthReport.TotalHseManhours ?? 0) + sumPersonWorkTimeTotal;
                    //}
                    //else
                    //{
                    totalSafeHours = sumPersonWorkTimeTotal;
                    //}
                }
                this.txtTotalLossTime.Text = totalSafeHours.ToString();
            }
        }
        #endregion
    }
}