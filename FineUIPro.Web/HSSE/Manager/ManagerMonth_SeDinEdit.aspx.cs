using BLL;
using Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI.HtmlControls;

namespace FineUIPro.Web.HSSE.Manager
{
    public partial class ManagerMonth_SeDinEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 月报告查主键
        /// </summary>
        public string MonthReportId
        {
            get
            {
                return (string)ViewState["MonthReportId"];
            }
            set
            {
                ViewState["MonthReportId"] = value;
            }
        }

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
        /// <summary>
        /// 状态
        /// </summary>
        public string States
        {
            get
            {
                return (string)ViewState["States"];
            }
            set
            {
                ViewState["States"] = value;
            }
        }
        #endregion

        /// <summary>
        /// 页面加载时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                btnClose.OnClientClick = ActiveWindow.GetHideReference();               
                this.ProjectId = this.CurrUser.LoginProjectId;
                if (!string.IsNullOrEmpty(Request.Params["projectId"]) && Request.Params["projectId"] != this.ProjectId)
                {
                    this.ProjectId = Request.Params["projectId"];
                }
                this.States = Const.State_0;
                ////权限按钮方法
                this.GetButtonPower();
                if (!string.IsNullOrEmpty(Request.Params["MonthReportId"]))
                {
                    MonthReportId = Request.Params["MonthReportId"];
                }            
                BLL.UserService.InitFlowOperateControlUserDropDownList(this.CompileManId, this.CurrUser.LoginProjectId, Const.UnitId_SEDIN, false);
                BLL.UserService.InitFlowOperateControlUserDropDownList(this.AuditManId, this.CurrUser.LoginProjectId, Const.UnitId_SEDIN, false);
                BLL.UserService.InitFlowOperateControlUserDropDownList(this.ApprovalManId, this.CurrUser.LoginProjectId, Const.UnitId_SEDIN, false);
                string montvalues = ReporMonth.Text;
                var getMont = Funs.DB.SeDin_MonthReport.FirstOrDefault(x => x.MonthReportId == this.MonthReportId);
                if (getMont != null)
                {
                    this.CompileManId.SelectedValue = getMont.CompileManId;
                    this.AuditManId.SelectedValue = getMont.AuditManId;
                    this.ApprovalManId.SelectedValue = getMont.ApprovalManId;
                    montvalues = string.Format("{0:yyyy-MM-dd}", getMont.ReporMonth);
                    this.States = getMont.States;
                    if (getMont.States == Const.State_1)
                    {
                        this.btnSave.Hidden = true;
                        this.btnSysSubmit.Hidden = true;
                    }
                }
                else
                {
                    CompileManId.SelectedValue = CurrUser.UserId;
                    AuditManId.SelectedIndex = 0;
                    ApprovalManId.SelectedIndex = 0;
                    montvalues = Request.Params["Month"];
                }
              
                for (int i = 0; i < 14; i++)
                {
                    getInfo(ProjectId, montvalues, StartDate.Text, EndDate.Text, i.ToString());
                }
                //BLL.UnitService.InitUnitDropDownList(this.drpUnit, ProjectId, false);
            }
        }
               
        /// <summary>
        /// 
        /// </summary>
        /// <param name="j"></param>
        /// <param name="bigType"></param>
        /// <param name="i"></param>
        private void display(int j, List<Model.SeDinMonthReport3Item> bigType, int i)
        {
            HtmlGenericControl myLabel = (HtmlGenericControl)ContentPanel2.FindControl("AccidentType" + (j + 1));
            HtmlInputText monthTimes = (HtmlInputText)ContentPanel2.FindControl("MonthTimes" + (j + 1));
            HtmlInputText totalTimes = (HtmlInputText)ContentPanel2.FindControl("TotalTimes" + (j + 1));
            HtmlInputText monthLossTime = (HtmlInputText)ContentPanel2.FindControl("MonthLossTime" + (j + 1));
            HtmlInputText totalLossTime = (HtmlInputText)ContentPanel2.FindControl("TotalLossTime" + (j + 1));
            HtmlInputText MonthMoney = (HtmlInputText)ContentPanel2.FindControl("MonthMoney" + (j + 1));
            HtmlInputText totalMoney = (HtmlInputText)ContentPanel2.FindControl("TotalMoney" + (j + 1));
            HtmlInputText monthPersons = (HtmlInputText)ContentPanel2.FindControl("MonthPersons" + (j + 1));
            HtmlInputText totalPersons = (HtmlInputText)ContentPanel2.FindControl("TotalPersons" + (j + 1));
            if (myLabel != null)
            {
                myLabel.InnerText = bigType[i].AccidentType;
            }
            if (monthTimes != null)
            {
                monthTimes.Value = bigType[i].MonthTimes.ToString();
            }
            if (totalTimes != null)
            {
                totalTimes.Value = bigType[i].TotalTimes.ToString();
            }
            if (monthLossTime != null)
            {
                monthLossTime.Value = bigType[i].MonthLossTime.ToString();
            }
            if (totalLossTime != null)
            {
                totalLossTime.Value = bigType[i].TotalLossTime.ToString();
            }
            if (MonthMoney != null)
            {
                MonthMoney.Value = bigType[i].MonthMoney.ToString();
            }
            if (totalMoney != null)
            {
                totalMoney.Value = bigType[i].TotalMoney.ToString();
            }
            if (monthPersons != null)
            {
                monthPersons.Value = bigType[i].MonthPersons.ToString();
            }
            if (totalPersons != null)
            {
                totalPersons.Value = bigType[i].TotalPersons.ToString();
            }
        }

        #region 获取页面信息
        /// <summary>
        ///  获取页面信息
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="month"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="pageNum"></param>
        protected void getInfo(string projectId, string month, string startDate, string endDate, string pageNum)
        {            
            if (pageNum == "0") ////封面
            {
                var getReport = APISeDinMonthReportService.getSeDinMonthReport0ById(projectId, month);
                if (getReport == null || string.IsNullOrEmpty(getReport.MonthReportId))
                {
                    getReport = APISeDinMonthReportService.getSeDinMonthReportNullPage0(projectId, Funs.GetNewDateTimeOrNow(month));
                }
                ReporMonth.Text = getReport.ReporMonth;
                DueDate.Text = getReport.DueDate;
                StartDate.Text = getReport.StartDate;
                EndDate.Text = getReport.EndDate;
                CompileManId.SelectedValue = getReport.CompileManId;
                AuditManId.SelectedValue = getReport.AuditManId;
                ApprovalManId.SelectedValue = getReport.ApprovalManId;
            }
            else if (pageNum == "1") ////1、项目信息
            {
                var getInfo = APISeDinMonthReportService.getSeDinMonthReport1ById(projectId, month);
                if (this.States ==Const.State_0 && ( getInfo == null || string.IsNullOrEmpty(getInfo.MonthReportId)))
                {
                    getInfo = APISeDinMonthReportService.getSeDinMonthReportNullPage1(projectId);
                }
                projectName.Text = getInfo.ProjectName;
                projectCode.Text = getInfo.ProjectCode;
                projectType.Text = getInfo.ProjectType;
                string[] str1 = getInfo.ProjectManager.Split('；');
                ProjectManager.Text = str1[0];
                if (str1.Length > 1)
                {
                    ProjectManagerPhone.Text = str1[1];
                }
                string[] str2 = getInfo.HsseManager.Split('；');
                HsseManager.Text = str2[0];
                if (str2.Length > 1)
                {
                    HsseManagerPhone.Text = str2[1];
                }
                ConstructionStage.Text = getInfo.ConstructionStage;
                ContractAmount.Text = getInfo.ContractAmount;
                ProjectAddress.Text = getInfo.ProjectAddress;
                pStartDate.Text = getInfo.StartDate;
                pEndDate.Text = getInfo.EndDate;
                ProjectAddress.Text = getInfo.ProjectAddress;
            }
            else if (pageNum == "2") ////2、项目安全工时统计
            {
                var getInfo = APISeDinMonthReportService.getSeDinMonthReport2ById(projectId, month);
                if (this.States == Const.State_0 && (getInfo == null || string.IsNullOrEmpty(getInfo.MonthReportId)))
                {
                    getInfo = APISeDinMonthReportService.getSeDinMonthReportNullPage2(projectId, month, startDate, endDate);
                }
                MillionLossRate.Text = getInfo.MillionLossRate;
                if (getInfo.MonthWorkTime != null)
                {
                    MonthWorkTime.Text = getInfo.MonthWorkTime.ToString();
                }
                if (getInfo.ProjectWorkTime != null)
                {
                    ProjectWorkTime.Text = getInfo.ProjectWorkTime.ToString();
                }
                if (getInfo.SafeWorkTime != null)
                {
                    SafeWorkTime.Text = getInfo.SafeWorkTime.ToString();
                }
                PsafeStartDate.Text = getInfo.StartDate;
                PsafeEndDate.Text = getInfo.EndDate;
                TimeAccuracyRate.Text = getInfo.TimeAccuracyRate;
                if (getInfo.TotalLostTime != null)
                {
                    TotalLostTime.Text = getInfo.TotalLostTime.ToString();
                }
                if (getInfo.YearWorkTime != null)
                {
                    YearWorkTime.Text = getInfo.YearWorkTime.ToString();
                }
            }
            else if (pageNum == "3") ////3、项目HSE事故、事件统计
            {
                var getInfo = APISeDinMonthReportService.getSeDinMonthReport3ById(projectId, month);
                if (this.States == Const.State_0 &&  (getInfo == null || getInfo.SeDinMonthReport3Item == null || getInfo.SeDinMonthReport3Item.Count() == 0))
                {
                    getInfo = APISeDinMonthReportService.getSeDinMonthReportNullPage3(projectId, month, startDate, endDate);
                }

                if (getInfo.SeDinMonthReport3Item.Count > 0)
                {
                    var count = getInfo.SeDinMonthReport3Item.Count;
                    var bigType = getInfo.SeDinMonthReport3Item.Where(p => p.BigType != null).ToList();
                    var bType = getInfo.SeDinMonthReport3Item.Where(p => p.BigType == null).ToList();
                    BigType.InnerText = bigType[0].BigType;
                    for (int i = 0; i < bigType.Count; i++)
                    {
                        int j = i;
                        display(j, bigType, i);
                    }
                    int jc = 3;
                    for (int i = 0; i < bType.Count; i++)
                    {
                        jc++;
                        display(jc, bType, i);
                    }
                }
            }
            else if (pageNum == "4") ////4、人员
            {
                var getLists = APISeDinMonthReportService.getSeDinMonthReport4ById(projectId, month);
                if (this.States == Const.State_0 &&  getLists.Count() == 0)
                {
                    getLists = APISeDinMonthReportService.getSeDinMonthReportNullPage4(projectId, month, startDate, endDate);
                }
                GvSeDinMonthReport4Item.DataSource = getLists;
                GvSeDinMonthReport4Item.DataBind();
                int sumSafeManangerNum = getLists.Sum(x => x.SafeManangerNum) ?? 0;
                int sumOtherManangerNum = getLists.Sum(x => x.OtherManangerNum) ?? 0;
                int sumSpecialWorkerNum = getLists.Sum(x => x.SpecialWorkerNum) ?? 0;
                int sumGeneralWorkerNum = getLists.Sum(x => x.GeneralWorkerNum) ?? 0;
                int sumALL = sumSafeManangerNum + sumOtherManangerNum + sumSpecialWorkerNum + sumGeneralWorkerNum;
                if (this.GvSeDinMonthReport4Item.Rows.Count > 0)
                {
                    JObject summary = new JObject
                    {
                        { "UnitName", "合计" },
                        { "SafeManangerNum", sumSafeManangerNum },
                        { "OtherManangerNum", sumOtherManangerNum },
                        { "SpecialWorkerNum", sumSpecialWorkerNum },
                        { "GeneralWorkerNum", sumGeneralWorkerNum },
                        { "TotalNum", sumALL }
                    };
                    GvSeDinMonthReport4Item.SummaryData = summary;
                }
                else
                {
                    GvSeDinMonthReport4Item.SummaryData = null;
                }

                var getOtherLists = APISeDinMonthReportService.getSeDinMonthReport4OtherById(projectId, month);
                if (this.States == Const.State_0 && getOtherLists == null)
                {
                    getOtherLists = APISeDinMonthReportService.getSeDinMonthReportNullPage4Other(projectId, month, startDate, endDate);
                }
                if (getOtherLists != null)
                {
                    this.txtFormalNum.Value = getOtherLists.FormalNum.ToString();
                    this.txtForeignNum.Value = getOtherLists.ForeignNum.ToString();
                    this.txtOutsideNum.Value = getOtherLists.OutsideNum.ToString();
                    this.txtManagerNum.Value = getOtherLists.ManagerNum.ToString();
                    this.txtTotalNum.Value = getOtherLists.TotalNum.ToString();
                }
            }
            else if (pageNum == "5") ////5、本月大型、特种设备投入情况
            {
                var getLists = APISeDinMonthReportService.getSeDinMonthReport5ById(projectId, month);
                if (this.States == Const.State_0 && getLists.Count == 0)
                {
                    getLists = APISeDinMonthReportService.getSeDinMonthReportNullPage5(projectId, month, startDate, endDate);
                }
                GvSeDinMonthReport5Item.DataSource = getLists;
                GvSeDinMonthReport5Item.DataBind();

                int sumT01 = getLists.Sum(x => x.T01) ?? 0;
                int sumT02 = getLists.Sum(x => x.T02) ?? 0;
                int sumT03 = getLists.Sum(x => x.T03) ?? 0;
                int sumT04 = getLists.Sum(x => x.T04) ?? 0;
                int sumT05 = getLists.Sum(x => x.T05) ?? 0;
                int sumT06 = getLists.Sum(x => x.T06) ?? 0;
                int sumD01 = getLists.Sum(x => x.D01) ?? 0;
                int sumD02 = getLists.Sum(x => x.D02) ?? 0;
                int sumD03 = getLists.Sum(x => x.D03) ?? 0;
                int sumD04 = getLists.Sum(x => x.D04) ?? 0;
                int sumS01 = getLists.Sum(x => x.S01) ?? 0;
                if (this.GvSeDinMonthReport4Item.Rows.Count > 0)
                {
                    JObject summary = new JObject
                    {
                        { "UnitName", "合计" },
                        { "T01", sumT01 },
                        { "T02", sumT02 },
                        { "T03", sumT03 },
                        { "T04", sumT04 },
                        { "T05", sumT05 },
                        { "T06", sumT06 },
                        { "D01", sumD01 },
                        { "D02", sumD02 },
                        { "D03", sumD03 },
                        { "D04", sumD04 },
                        { "S01", sumS01 },
                        { "TotalNum", sumS01+sumT02+sumT03+sumT04+sumT05+sumT06+sumD01+sumD02+sumD03+sumD04+sumS01 }
                    };
                    GvSeDinMonthReport5Item.SummaryData = summary;
                }
                else
                {
                    GvSeDinMonthReport5Item.SummaryData = null;
                }

            }
            else if (pageNum == "6") ////6、安全生产费用投入情况
            {
                var getInfo = APISeDinMonthReportService.getSeDinMonthReport6ById(projectId, month);
                if (this.States == Const.State_0 && (getInfo == null || string.IsNullOrEmpty(getInfo.MonthReportId)))
                {
                    getInfo = APISeDinMonthReportService.getSeDinMonthReportNullPage6(projectId, month, startDate, endDate);
                }
                SafetyMonth.Value = getInfo.SafetyMonth.ToString();
                SafetyYear.Value = getInfo.SafetyYear.ToString();
                SafetyTotal.Value = getInfo.SafetyTotal.ToString();
                LaborMonth.Value = getInfo.LaborMonth.ToString();
                LaborYear.Value = getInfo.LaborYear.ToString();
                LaborTotal.Value = getInfo.LaborTotal.ToString();
                ProgressMonth.Value = getInfo.ProgressMonth.ToString();
                ProgressYear.Value = getInfo.ProgressYear.ToString();
                ProgressTotal.Value = getInfo.ProgressTotal.ToString();
                EducationMonth.Value = getInfo.EducationMonth.ToString();
                EducationYear.Value = getInfo.EducationYear.ToString();
                EducationTotal.Value = getInfo.EducationTotal.ToString();
                SumMonth.Value = getInfo.SumMonth.ToString();
                SumYear.Value = getInfo.SumYear.ToString();
                SumTotal.Value = getInfo.SumTotal.ToString();
                ContractMonth.Value = getInfo.ContractMonth.ToString();
                ContractYear.Value = getInfo.ContractYear.ToString();
                ContractTotal.Value = getInfo.ContractTotal.ToString();
                ConstructionCost.Value = getInfo.ConstructionCost.ToString();

            }
            else if (pageNum == "7") ////7、项目HSE培训统计
            {
                var getInfo = APISeDinMonthReportService.getSeDinMonthReport7ById(projectId, month);
                if (this.States == Const.State_0 && (getInfo == null || string.IsNullOrEmpty(getInfo.MonthReportId)))
                {
                    getInfo = APISeDinMonthReportService.getSeDinMonthReportNullPage7(projectId, month, startDate, endDate);
                }

                EmployeeMontNum.Value = getInfo.EmployeeMontNum.ToString();
                EmployeeYearNum.Value = getInfo.EmployeeYearNum.ToString();
                EmployeeTotalNum.Value = getInfo.EmployeeTotalNum.ToString();
                EmployeeMontPerson.Value = getInfo.EmployeeMontPerson.ToString();
                EmployeeYearPerson.Value = getInfo.EmployeeYearPerson.ToString();
                EmployeeTotalPerson.Value = getInfo.EmployeeTotalPerson.ToString();
                SpecialMontNum.Value = getInfo.SpecialMontNum.ToString();
                SpecialYearNum.Value = getInfo.SpecialYearNum.ToString();
                SpecialTotalNum.Value = getInfo.SpecialTotalNum.ToString();
                SpecialMontPerson.Value = getInfo.SpecialMontPerson.ToString();
                SpecialYearPerson.Value = getInfo.SpecialYearPerson.ToString();
                SpecialTotalPerson.Value = getInfo.SpecialTotalPerson.ToString();
            }
            else if (pageNum == "8") ////8、项目HSE会议统计
            {
                var getInfo = APISeDinMonthReportService.getSeDinMonthReport8ById(projectId, month);
                if (this.States == Const.State_0 && (getInfo == null || string.IsNullOrEmpty(getInfo.MonthReportId)))
                {
                    getInfo = APISeDinMonthReportService.getSeDinMonthReportNullPage8(projectId, month, startDate, endDate);
                }
                Report8WeekMontNum.Value = getInfo.WeekMontNum.ToString();
                Report8WeekTotalNum.Value = getInfo.WeekTotalNum.ToString();
                Report8WeekMontPerson.Value = getInfo.WeekMontPerson.ToString();
                Report8MonthMontNum.Value = getInfo.MonthMontNum.ToString();
                Report8MonthTotalNum.Value = getInfo.MonthTotalNum.ToString();
                Report8MonthMontPerson.Value = getInfo.MonthMontPerson.ToString();
                Report8SpecialMontNum.Value = getInfo.SpecialMontNum.ToString();
                Report8SpecialTotalNum.Value = getInfo.SpecialTotalNum.ToString();
                Report8SpecialMontPerson.Value = getInfo.SpecialMontPerson.ToString();
                GvSeDinMonthReport8Item.DataSource = getInfo.SeDinMonthReport8ItemItem;
                GvSeDinMonthReport8Item.DataBind();
            }
            else if (pageNum == "9") ////9、项目HSE检查统计
            {
                var getInfo = APISeDinMonthReportService.getSeDinMonthReport9ById(projectId, month);
                if (this.States == Const.State_0 && (getInfo == null || string.IsNullOrEmpty(getInfo.MonthReportId)))
                {
                    getInfo = APISeDinMonthReportService.getSeDinMonthReportNullPage9(projectId, month, startDate, endDate);
                }
                DailyMonth.Value = getInfo.DailyMonth.ToString();
                DailyYear.Value = getInfo.DailyYear.ToString();
                DailyTotal.Value = getInfo.DailyTotal.ToString();
                WeekMonth.Value = getInfo.WeekMonth.ToString();
                WeekYear.Value = getInfo.WeekYear.ToString();
                WeekTotal.Value = getInfo.WeekTotal.ToString();
                SpecialMonth.Value = getInfo.SpecialMonth.ToString();
                SpecialYear.Value = getInfo.SpecialYear.ToString();
                SpecialTotal.Value = getInfo.SpecialTotal.ToString();
                MonthlyMonth.Value = getInfo.MonthlyMonth.ToString();
                MonthlyYear.Value = getInfo.MonthlyYear.ToString();
                MonthlyTotal.Value = getInfo.MonthlyTotal.ToString();
                GvSeDinMonthReport9ItemRect.DataSource = getInfo.SeDinMonthReport9ItemRectification;
                GvSeDinMonthReport9ItemRect.DataBind();
                GvSeDinMonthReport9ItemSpecial.DataSource = getInfo.SeDinMonthReport9ItemSpecial;
                GvSeDinMonthReport9ItemSpecial.DataBind();
                GvSeDinMonthReport9ItemStoppage.DataSource = getInfo.SeDinMonthReport9ItemStoppage;
                GvSeDinMonthReport9ItemStoppage.DataBind();
            }
            else if (pageNum == "10") ////10、项目奖惩情况统计
            {
                var getInfo = APISeDinMonthReportService.getSeDinMonthReport10ById(projectId, month);
                if (this.States == Const.State_0 && (getInfo == null || string.IsNullOrEmpty(getInfo.MonthReportId)))
                {
                    getInfo = APISeDinMonthReportService.getSeDinMonthReportNullPage10(projectId, month, startDate, endDate);
                }
                SafeMonthNum.Value = getInfo.SafeMonthNum.ToString();
                SafeTotalNum.Value = getInfo.SafeTotalNum.ToString();
                SafeMonthMoney.Value = getInfo.SafeMonthMoney.ToString();
                SafeTotalMoney.Value = getInfo.SafeTotalMoney.ToString();
                HseMonthNum.Value = getInfo.HseMonthNum.ToString();
                HseTotalNum.Value = getInfo.HseTotalNum.ToString();
                HseMonthMoney.Value = getInfo.HseMonthMoney.ToString();
                HseTotalMoney.Value = getInfo.HseTotalMoney.ToString();
                ProduceMonthNum.Value = getInfo.ProduceMonthNum.ToString();
                ProduceTotalNum.Value = getInfo.ProduceTotalNum.ToString();
                ProduceMonthMoney.Value = getInfo.ProduceMonthMoney.ToString();
                ProduceTotalMoney.Value = getInfo.ProduceTotalMoney.ToString();
                AccidentMonthNum.Value = getInfo.AccidentMonthNum.ToString();
                AccidentTotalNum.Value = getInfo.AccidentTotalNum.ToString();
                AccidentMonthMoney.Value = getInfo.AccidentMonthMoney.ToString();
                AccidentTotalMoney.Value = getInfo.AccidentTotalMoney.ToString();
                ViolationMonthNum.Value = getInfo.ViolationMonthNum.ToString();
                ViolationTotalNum.Value = getInfo.ViolationTotalNum.ToString();
                ViolationMonthMoney.Value = getInfo.ViolationMonthMoney.ToString();
                ViolationTotalMoney.Value = getInfo.ViolationTotalMoney.ToString();
                ManageMonthNum.Value = getInfo.ManageMonthNum.ToString();
                ManageTotalNum.Value = getInfo.ManageTotalNum.ToString();
                ManageMonthMoney.Value = getInfo.ManageMonthMoney.ToString();
                ManageTotalMoney.Value = getInfo.ManageTotalMoney.ToString();
            }
            else if (pageNum == "11") ////11、项目危大工程施工情况
            {
                var getInfo = APISeDinMonthReportService.getSeDinMonthReport11ById(projectId, month);
                if (this.States == Const.State_0 && (getInfo == null || string.IsNullOrEmpty(getInfo.MonthReportId)))
                {
                    getInfo = APISeDinMonthReportService.getSeDinMonthReportNullPage11(projectId, month, startDate, endDate);
                }
                RiskWorkNum.Value = getInfo.RiskWorkNum.ToString();
                RiskFinishedNum.Value = getInfo.RiskFinishedNum.ToString();
                RiskWorkNext.Value = getInfo.RiskWorkNext.ToString();
                LargeWorkNum.Value = getInfo.LargeWorkNum.ToString();
                LargeFinishedNum.Value = getInfo.LargeFinishedNum.ToString();
                LargeWorkNext.Value = getInfo.LargeWorkNext.ToString();

            }
            else if (pageNum == "12") ////12、项目应急演练情况
            {
                var getInfo = APISeDinMonthReportService.getSeDinMonthReport12ById(projectId, month);
                if (this.States == Const.State_0 && (getInfo == null || string.IsNullOrEmpty(getInfo.MonthReportId)))
                {
                    getInfo = APISeDinMonthReportService.getSeDinMonthReportNullPage12(projectId, month, startDate, endDate);
                }

                MultipleSiteInput.Value = getInfo.MultipleSiteInput.ToString();
                MultipleSitePerson.Value = getInfo.MultipleSitePerson.ToString();
                MultipleSiteNum.Value = getInfo.MultipleSiteNum.ToString();
                MultipleSiteTotalNum.Value = getInfo.MultipleSiteTotalNum.ToString();
                if (!string.IsNullOrWhiteSpace(getInfo.MultipleSiteNext))
                {
                    MultipleSiteNext.Value = getInfo.MultipleSiteNext.ToString();
                }
                MultipleDesktopInput.Value = getInfo.MultipleDesktopInput.ToString();
                MultipleDesktopPerson.Value = getInfo.MultipleDesktopPerson.ToString();
                MultipleDesktopNum.Value = getInfo.MultipleDesktopNum.ToString();
                MultipleDesktopTotalNum.Value = getInfo.MultipleDesktopTotalNum.ToString();
                if (!string.IsNullOrWhiteSpace(getInfo.MultipleDesktopNext))
                {
                    MultipleDesktopNext.Value = getInfo.MultipleDesktopNext.ToString();
                }
                SingleSiteInput.Value = getInfo.SingleSiteInput.ToString();
                SingleSitePerson.Value = getInfo.SingleSitePerson.ToString();
                SingleSiteNum.Value = getInfo.SingleSiteNum.ToString();
                SingleSiteTotalNum.Value = getInfo.SingleSiteTotalNum.ToString();
                if (!string.IsNullOrWhiteSpace(getInfo.SingleSiteNext))
                {
                    SingleSiteNext.Value = getInfo.SingleSiteNext.ToString();
                }
                SingleDesktopInput.Value = getInfo.SingleDesktopInput.ToString();
                SingleDesktopPerson.Value = getInfo.SingleDesktopPerson.ToString();
                SingleDesktopNum.Value = getInfo.SingleDesktopNum.ToString();
                SingleDesktopTotalNum.Value = getInfo.SingleDesktopTotalNum.ToString();
                if (!string.IsNullOrWhiteSpace(getInfo.SingleDesktopNext))
                {
                    SingleDesktopNext.Value = getInfo.SingleDesktopNext.ToString();
                }

            }
            else ////13、14、本月HSE活动综述、下月HSE工作计划
            {
                var getReport = APISeDinMonthReportService.getSeDinMonthReport0ById(projectId, month);               
                if (getReport != null)
                {
                    ThisSummary.Text = getReport.ThisSummary;
                    NextPlan.Text = getReport.NextPlan;
                    AccidentsSummary.Value = getReport.AccidentsSummary;
                }
            }
        }
        #endregion

        #region 保存页面信息方法
        #region 保存 MonthReport0 封面
        /// <summary>
        /// 保存赛鼎月报
        /// </summary>
        /// <param name="newItem">赛鼎月报</param>
        /// <returns></returns>
        public string SaveSeDinMonthReport0(string type)
        {
            SeDinMonthReportItem newItem = new SeDinMonthReportItem
            {
                ProjectId = ProjectId,
                DueDate = DueDate.Text,
                StartDate = StartDate.Text,
                EndDate = EndDate.Text,
                ReporMonth = ReporMonth.Text,
                CompileManId = CompileManId.SelectedValue,
                AuditManId = AuditManId.SelectedValue,
                ApprovalManId = ApprovalManId.SelectedValue,
            };
            if (!string.IsNullOrWhiteSpace(MonthReportId))
            {
                newItem.MonthReportId = MonthReportId;
            }
            ////提交
            if (type ==Const.BtnSubmit)
            { 
                newItem.States = Const.State_1;
            }
            else
            {
                newItem.States = Const.State_0;
            }
            MonthReportId = APISeDinMonthReportService.SaveSeDinMonthReport0(newItem);
            return MonthReportId;
        }
        #endregion
        #region 保存 MonthReport1、项目信息
        /// <summary>
        /// 保存赛鼎月报
        /// </summary>
        /// <param name="newItem">赛鼎月报</param>
        /// <returns></returns>
        public void SaveSeDinMonthReport1()
        {
            SeDinMonthReport1Item newItem = new SeDinMonthReport1Item
            {
                MonthReportId = MonthReportId,
                ProjectCode = projectCode.Text,
                ProjectName = projectName.Text,
                ProjectType = projectType.Text,
                StartDate = pStartDate.Text,
                EndDate = pEndDate.Text,
                ProjectManager = ProjectManager.Text + "；" + ProjectManagerPhone.Text,
                HsseManager = HsseManager.Text + "；" + HsseManagerPhone.Text,
                ContractAmount = ContractAmount.Text,
                ConstructionStage = ConstructionStage.Text,
                ProjectAddress = ProjectAddress.Text
            };
            APISeDinMonthReportService.SaveSeDinMonthReport1(newItem);
        }
        #endregion
        #region 保存 MonthReport2、项目安全工时统计
        /// <summary>
        /// 保存赛鼎月报
        /// </summary>
        /// <param name="newItem">赛鼎月报</param>
        /// <returns></returns>

        public void SaveSeDinMonthReport2()
        {
            SeDinMonthReport2Item newItem = new SeDinMonthReport2Item();
            newItem.MonthReportId = MonthReportId;
            newItem.MonthWorkTime = Funs.GetNewDecimalOrZero(MonthWorkTime.Text);
            newItem.YearWorkTime = Funs.GetNewDecimalOrZero(YearWorkTime.Text);
            newItem.YearWorkTime = Funs.GetNewDecimalOrZero(YearWorkTime.Text);
            newItem.TotalLostTime = Funs.GetNewDecimalOrZero(TotalLostTime.Text);
            newItem.MillionLossRate = MillionLossRate.Text;
            newItem.ProjectWorkTime = Funs.GetNewDecimalOrZero(ProjectWorkTime.Text);
            newItem.TimeAccuracyRate = TimeAccuracyRate.Text;
            newItem.StartDate = StartDate.Text;
            newItem.EndDate = EndDate.Text;
            newItem.SafeWorkTime = Funs.GetNewDecimalOrZero(SafeWorkTime.Text);
            APISeDinMonthReportService.SaveSeDinMonthReport2(newItem);
        }
        #endregion
        #region 保存 MonthReport3、项目HSE事故、事件统计
        /// <summary>
        /// 保存赛鼎月报
        /// </summary>
        /// <param name="newItem">赛鼎月报</param>
        /// <returns></returns>
        public void SaveSeDinMonthReport3()
        {
            List<SeDinMonthReport3Item> listMonthReport3 = new List<SeDinMonthReport3Item>();
            for (int i = 1; i < 12; i++)
            {
                HtmlGenericControl myLabel = (HtmlGenericControl)ContentPanel2.FindControl("AccidentType" + (i));
                HtmlInputText monthTimes = (HtmlInputText)ContentPanel2.FindControl("MonthTimes" + (i));
                HtmlInputText totalTimes = (HtmlInputText)ContentPanel2.FindControl("TotalTimes" + (i));
                HtmlInputText monthLossTime = (HtmlInputText)ContentPanel2.FindControl("MonthLossTime" + (i));
                HtmlInputText totalLossTime = (HtmlInputText)ContentPanel2.FindControl("TotalLossTime" + (i));
                HtmlInputText MonthMoney = (HtmlInputText)ContentPanel2.FindControl("MonthMoney" + (i));
                HtmlInputText totalMoney = (HtmlInputText)ContentPanel2.FindControl("TotalMoney" + (i));
                HtmlInputText monthPersons = (HtmlInputText)ContentPanel2.FindControl("MonthPersons" + (i));
                HtmlInputText totalPersons = (HtmlInputText)ContentPanel2.FindControl("TotalPersons" + (i));
                SeDinMonthReport3Item mo = new SeDinMonthReport3Item();
                mo.MonthReportId = MonthReportId;
                if (i < 5)
                {
                    mo.BigType = BigType.InnerText;
                }
                mo.AccidentType = myLabel.InnerText;
                mo.SortIndex = i;
                if (!string.IsNullOrWhiteSpace(monthTimes.Value))
                {
                    mo.MonthTimes = Funs.GetNewInt(monthTimes.Value);
                }
                if (!string.IsNullOrWhiteSpace(totalTimes.Value))
                {
                    mo.TotalTimes = Funs.GetNewInt(totalTimes.Value);
                }
                if (!string.IsNullOrWhiteSpace(monthLossTime.Value))
                {
                    mo.MonthLossTime = Funs.GetNewDecimalOrZero(monthLossTime.Value);
                }
                if (!string.IsNullOrWhiteSpace(totalLossTime.Value))
                {
                    mo.TotalLossTime = Funs.GetNewDecimalOrZero(totalLossTime.Value);
                }
                if (!string.IsNullOrWhiteSpace(MonthMoney.Value))
                {
                    mo.MonthMoney = Funs.GetNewDecimalOrZero(MonthMoney.Value);
                }
                if (!string.IsNullOrWhiteSpace(totalMoney.Value))
                {
                    mo.TotalMoney = Funs.GetNewDecimalOrZero(totalMoney.Value);
                }
                if (!string.IsNullOrWhiteSpace(monthPersons.Value))
                {
                    mo.MonthPersons = Funs.GetNewInt(monthPersons.Value);
                }
                if (!string.IsNullOrWhiteSpace(totalPersons.Value))
                {
                    mo.TotalPersons = Funs.GetNewInt(totalPersons.Value);
                }
                listMonthReport3.Add(mo);
            }
            var newItem = new SeDinMonthReportItem
            {
                SeDinMonthReport3Item = listMonthReport3,
                MonthReportId = MonthReportId,
                AccidentsSummary = AccidentsSummary.Value
            };
            APISeDinMonthReportService.SaveSeDinMonthReport3(newItem);
        }
        #endregion
        #region 保存 MonthReport4、本月人员投入情况
        /// <summary>
        /// 保存赛鼎月报
        /// </summary>
        /// <param name="newItem">赛鼎月报</param>
        /// <returns></returns>

        public void SaveSeDinMonthReport4()
        {
            Model.SeDinMonthReportItem newItem = new SeDinMonthReportItem
            {
                MonthReportId = MonthReportId
            };
            List<SeDinMonthReport4Item> listSeDinMonthReport4Item = new List<SeDinMonthReport4Item>();
            foreach (JObject mergedRow in GvSeDinMonthReport4Item.GetMergedData())
            {
                int i = mergedRow.Value<int>("index");
                JObject values = mergedRow.Value<JObject>("values");
                var safeManangerNum = values.Value<string>("SafeManangerNum");
                var unitName = GvSeDinMonthReport4Item.Rows[i].Values[0].ToString();
                var OoherManangerNum = values.Value<string>("OtherManangerNum");
                var specialWorkerNum = values.Value<string>("SpecialWorkerNum");
                var generalWorkerNum = values.Value<string>("GeneralWorkerNum");
                Model.SeDinMonthReport4Item newReport4Item = new Model.SeDinMonthReport4Item
                {
                    MonthReportId = MonthReportId,
                    UnitName = unitName,
                    SafeManangerNum = Funs.GetNewIntOrZero(safeManangerNum),
                    OtherManangerNum = Funs.GetNewIntOrZero(OoherManangerNum),
                    SpecialWorkerNum = Funs.GetNewIntOrZero(specialWorkerNum),
                    GeneralWorkerNum = Funs.GetNewIntOrZero(generalWorkerNum)
                };
                newReport4Item.TotalNum = newReport4Item.SafeManangerNum + newReport4Item.OtherManangerNum
                    + newReport4Item.SpecialWorkerNum + newReport4Item.GeneralWorkerNum;
                listSeDinMonthReport4Item.Add(newReport4Item);
            }
            newItem.SeDinMonthReport4Item = listSeDinMonthReport4Item;
            APISeDinMonthReportService.SaveSeDinMonthReport4(newItem);

            Model.SeDinMonthReport4OtherItem newItemOher = new SeDinMonthReport4OtherItem()
            {
                MonthReportId = MonthReportId,
                FormalNum =Funs.GetNewIntOrZero(this.txtFormalNum.Value),
                ForeignNum = Funs.GetNewIntOrZero(this.txtForeignNum.Value),
                OutsideNum = Funs.GetNewIntOrZero(this.txtOutsideNum.Value),
                ManagerNum = Funs.GetNewIntOrZero(this.txtManagerNum.Value),
                TotalNum = Funs.GetNewIntOrZero(this.txtTotalNum.Value),
            };
            APISeDinMonthReportService.SaveSeDinMonthReport4Other(newItemOher);
        }
        #endregion
        #region 保存 MonthReport5、本月大型、特种设备投入情况
        /// <summary>
        /// 保存赛鼎月报
        /// </summary>
        /// <param name="newItem">赛鼎月报</param>
        /// <returns></returns>
        public void SaveSeDinMonthReport5()
        {
            Model.SeDinMonthReportItem newItem = new SeDinMonthReportItem
            {
                MonthReportId = MonthReportId
            };
            List<SeDinMonthReport5Item> listSeDinMonthReport5Item = new List<SeDinMonthReport5Item>();
            foreach (JObject mergedRow in GvSeDinMonthReport5Item.GetMergedData())
            {
                int i = mergedRow.Value<int>("index");
                JObject values = mergedRow.Value<JObject>("values");
                var unitName = GvSeDinMonthReport5Item.Rows[i].Values[0].ToString();
                var t01 = values.Value<string>("T01");
                var t02 = values.Value<string>("T02");
                var t03 = values.Value<string>("T03");
                var t04 = values.Value<string>("T04");
                var t05 = values.Value<string>("T05");
                var t06 = values.Value<string>("T06");
                var d01 = values.Value<string>("D01");
                var d02 = values.Value<string>("D02");
                var d03 = values.Value<string>("D03");
                var d04 = values.Value<string>("D04");
                var s01 = values.Value<string>("S01");
                Model.SeDinMonthReport5Item newReport5Item = new Model.SeDinMonthReport5Item();
                newReport5Item.UnitName = unitName;
                newReport5Item.T01 = Funs.GetNewInt(t01);
                newReport5Item.T02 = Funs.GetNewInt(t02);
                newReport5Item.T03 = Funs.GetNewInt(t03);
                newReport5Item.T04 = Funs.GetNewInt(t04);
                newReport5Item.T05 = Funs.GetNewInt(t05);
                newReport5Item.T06 = Funs.GetNewInt(t06);
                newReport5Item.D01 = Funs.GetNewInt(d01);
                newReport5Item.D02 = Funs.GetNewInt(d02);
                newReport5Item.D03 = Funs.GetNewInt(d03);
                newReport5Item.D04 = Funs.GetNewInt(d04);
                newReport5Item.S01 = Funs.GetNewInt(s01);
                listSeDinMonthReport5Item.Add(newReport5Item);
            }
            newItem.SeDinMonthReport5Item = listSeDinMonthReport5Item;
            APISeDinMonthReportService.SaveSeDinMonthReport5(newItem);
        }
        #endregion
        #region 保存 MonthReport6、安全生产费用投入情况
        /// <summary>
        /// 保存赛鼎月报
        /// </summary>
        /// <param name="newItem">赛鼎月报</param>
        /// <returns></returns>
        public void SaveSeDinMonthReport6()
        {
            SeDinMonthReport6Item monthReport6Item = new SeDinMonthReport6Item
            {
                MonthReportId = MonthReportId,
                SafetyMonth = Funs.GetNewDecimalOrZero(SafetyMonth.Value),
                SafetyYear = Funs.GetNewDecimalOrZero(SafetyYear.Value),
                SafetyTotal = Funs.GetNewDecimalOrZero(SafetyTotal.Value),
                LaborMonth = Funs.GetNewDecimalOrZero(LaborMonth.Value),
                LaborYear = Funs.GetNewDecimalOrZero(LaborYear.Value),
                LaborTotal = Funs.GetNewDecimalOrZero(LaborTotal.Value),
                ProgressMonth = Funs.GetNewDecimalOrZero(ProgressMonth.Value),
                ProgressYear = Funs.GetNewDecimalOrZero(ProgressYear.Value),
                ProgressTotal = Funs.GetNewDecimalOrZero(ProgressTotal.Value),
                EducationMonth = Funs.GetNewDecimalOrZero(EducationMonth.Value),
                EducationYear = Funs.GetNewDecimalOrZero(EducationYear.Value),
                EducationTotal = Funs.GetNewDecimalOrZero(EducationTotal.Value),
                SumMonth = Funs.GetNewDecimalOrZero(SumMonth.Value),
                SumYear = Funs.GetNewDecimalOrZero(SumYear.Value),
                SumTotal = Funs.GetNewDecimalOrZero(SumTotal.Value),
                ContractMonth = Funs.GetNewDecimalOrZero(ContractMonth.Value),
                ContractYear = Funs.GetNewDecimalOrZero(ContractYear.Value),
                ContractTotal = Funs.GetNewDecimalOrZero(ContractTotal.Value),
                ConstructionCost = Funs.GetNewDecimalOrZero(ConstructionCost.Value)
            };
            APISeDinMonthReportService.SaveSeDinMonthReport6(monthReport6Item);
        }
        #endregion
        #region 保存 MonthReport7、项目HSE培训统计
        /// <summary>
        /// 保存赛鼎月报
        /// </summary>
        /// <param name="newItem">赛鼎月报</param>
        /// <returns></returns>
        public void SaveSeDinMonthReport7()
        {
            var newItem = new SeDinMonthReport7Item
            {
                MonthReportId = MonthReportId,
                SpecialMontNum = Funs.GetNewInt(SpecialMontNum.Value),
                SpecialYearNum = Funs.GetNewInt(SpecialYearNum.Value),
                SpecialTotalNum = Funs.GetNewInt(SpecialTotalNum.Value),
                SpecialMontPerson = Funs.GetNewInt(SpecialMontPerson.Value),
                SpecialYearPerson = Funs.GetNewInt(SpecialYearPerson.Value),
                SpecialTotalPerson = Funs.GetNewInt(SpecialTotalPerson.Value),
                EmployeeMontNum = Funs.GetNewInt(EmployeeMontNum.Value),
                EmployeeYearNum = Funs.GetNewInt(EmployeeYearNum.Value),
                EmployeeTotalNum = Funs.GetNewInt(EmployeeTotalNum.Value),
                EmployeeMontPerson = Funs.GetNewInt(EmployeeMontPerson.Value),
                EmployeeYearPerson = Funs.GetNewInt(EmployeeYearPerson.Value),
                EmployeeTotalPerson = Funs.GetNewInt(EmployeeTotalPerson.Value)
            };
            APISeDinMonthReportService.SaveSeDinMonthReport7(newItem);
        }
        #endregion
        #region 保存 MonthReport8、项目HSE会议统计
        /// <summary>
        /// 保存赛鼎月报
        /// </summary>
        /// <param name="newItem">赛鼎月报</param>
        /// <returns></returns>
        public void SaveSeDinMonthReport8()
        {
            Model.SeDinMonthReport8Item newItem = new SeDinMonthReport8Item
            {
                MonthReportId = MonthReportId,
                WeekMontNum = Funs.GetNewInt(Report8WeekMontNum.Value),
                WeekTotalNum = Funs.GetNewInt(Report8WeekTotalNum.Value),
                WeekMontPerson = Funs.GetNewInt(Report8WeekMontPerson.Value),
                MonthMontNum = Funs.GetNewInt(Report8MonthMontNum.Value),
                MonthTotalNum = Funs.GetNewInt(Report8MonthTotalNum.Value),
                MonthMontPerson = Funs.GetNewInt(Report8MonthMontPerson.Value),
                SpecialMontNum = Funs.GetNewInt(Report8SpecialMontNum.Value),
                SpecialTotalNum = Funs.GetNewInt(Report8SpecialTotalNum.Value),
                SpecialMontPerson = Funs.GetNewInt(Report8SpecialMontPerson.Value)
            };
            List<SeDinMonthReport8ItemItem> listSeDin_MonthReport8Item = new List<SeDinMonthReport8ItemItem>();
            foreach (JObject mergedRow in GvSeDinMonthReport8Item.GetMergedData())
            {
                int i = mergedRow.Value<int>("index");
                JObject values = mergedRow.Value<JObject>("values");
                var unitName = GvSeDinMonthReport8Item.Rows[i].Values[0].ToString();
                var teamName = GvSeDinMonthReport8Item.Rows[i].Values[1].ToString();
                var classNum = GvSeDinMonthReport8Item.Rows[i].Values[2].ToString();
                var classPersonNum = values.Value<string>("ClassPersonNum");
                Model.SeDinMonthReport8ItemItem report8Item = new Model.SeDinMonthReport8ItemItem
                {
                    MonthReportId = MonthReportId,
                    UnitName = unitName,
                    TeamName = teamName,
                    ClassNum = Funs.GetNewInt(classNum),
                    ClassPersonNum = Funs.GetNewInt(classPersonNum)
                };
                listSeDin_MonthReport8Item.Add(report8Item);
            }
            newItem.SeDinMonthReport8ItemItem = listSeDin_MonthReport8Item;
            APISeDinMonthReportService.SaveSeDinMonthReport8(newItem);
        }
        #endregion
        #region 保存 MonthReport9、项目HSE检查统计
        /// <summary>
        /// 保存赛鼎月报
        /// </summary>
        /// <param name="newItem">赛鼎月报</param>
        /// <returns></returns>

        public void SaveSeDinMonthReport9()
        {
            SeDinMonthReport9Item newItem = new SeDinMonthReport9Item
            {
                MonthReportId = MonthReportId,
                DailyMonth = Funs.GetNewInt(DailyMonth.Value),
                DailyYear = Funs.GetNewInt(DailyYear.Value),
                DailyTotal = Funs.GetNewInt(DailyTotal.Value),
                WeekMonth = Funs.GetNewInt(WeekMonth.Value),
                WeekYear = Funs.GetNewInt(WeekYear.Value),
                WeekTotal = Funs.GetNewInt(WeekTotal.Value),
                SpecialMonth = Funs.GetNewInt(SpecialMonth.Value),
                SpecialYear = Funs.GetNewInt(SpecialYear.Value),
                SpecialTotal = Funs.GetNewInt(SpecialTotal.Value),
                MonthlyMonth = Funs.GetNewInt(MonthlyMonth.Value),
                MonthlyYear = Funs.GetNewInt(MonthlyYear.Value),
                MonthlyTotal = Funs.GetNewInt(MonthlyTotal.Value)
            };
            #region 整改单
            List<SeDinMonthReport9ItemRectification> listReport9ItemRectification = new List<SeDinMonthReport9ItemRectification>();
            var Report9ItemRects = GvSeDinMonthReport9ItemRect.GetMergedData();
            if (Report9ItemRects != null)
            {
                foreach (JObject mergedRow in GvSeDinMonthReport9ItemRect.GetMergedData())
                {
                    int i = mergedRow.Value<int>("index");
                    JObject values = mergedRow.Value<JObject>("values");
                    var unitName = GvSeDinMonthReport9ItemRect.Rows[i].Values[0].ToString();
                    SeDinMonthReport9ItemRectification report9ItemRectification = new SeDinMonthReport9ItemRectification
                    {
                        MonthReportId = MonthReportId,
                        UnitName = unitName,
                        IssuedMonth = Funs.GetNewInt(values.Value<string>("IssuedMonth")),
                        IssuedMonthLarge = Funs.GetNewInt(values.Value<string>("IssuedMonthLarge")),
                        IssuedMonthSerious = Funs.GetNewInt(values.Value<string>("IssuedMonthSerious")),
                        RectificationMoth = Funs.GetNewInt(values.Value<string>("RectificationMoth")),
                        RectificationMothLarge = Funs.GetNewInt(values.Value<string>("RectificationMothLarge")),
                        RectificationMothSerious = Funs.GetNewInt(values.Value<string>("RectificationMothSerious")),
                        IssuedTotal = Funs.GetNewInt(values.Value<string>("IssuedTotal")),
                        IssuedTotalLarge = Funs.GetNewInt(values.Value<string>("IssuedTotalLarge")),
                        IssuedTotalSerious = Funs.GetNewInt(values.Value<string>("IssuedTotalSerious")),
                        RectificationTotal = Funs.GetNewInt(values.Value<string>("RectificationTotal")),
                        RectificationTotalLarge = Funs.GetNewInt(values.Value<string>("RectificationTotalLarge")),
                        RectificationTotalSerious = Funs.GetNewInt(values.Value<string>("RectificationTotalSerious")),
                    };

                    listReport9ItemRectification.Add(report9ItemRectification);
                }
            }
            newItem.SeDinMonthReport9ItemRectification = listReport9ItemRectification;
            #endregion
            #region 专项检查
            List<SeDinMonthReport9ItemSpecial> listReport9ItemSpecial = new List<SeDinMonthReport9ItemSpecial>();
            var special = GvSeDinMonthReport9ItemSpecial.GetMergedData();
            if (special != null)
            {
                foreach (JObject mergedRow in GvSeDinMonthReport9ItemSpecial.GetMergedData())
                {
                    int i = mergedRow.Value<int>("index");
                    JObject values = mergedRow.Value<JObject>("values");
                    var typeName = GvSeDinMonthReport9ItemSpecial.Rows[i].Values[0].ToString();
                    var checkMonth = values.Value<string>("CheckMonth");
                    var checkYear = values.Value<string>("CheckYear");
                    var checkTotal = values.Value<string>("CheckTotal");
                    SeDinMonthReport9ItemSpecial report9ItemSpecial = new SeDinMonthReport9ItemSpecial();
                    report9ItemSpecial.MonthReportId = MonthReportId;
                    report9ItemSpecial.TypeName = typeName;
                    report9ItemSpecial.CheckMonth = Funs.GetNewInt(checkMonth);
                    report9ItemSpecial.CheckYear = Funs.GetNewInt(checkYear);
                    report9ItemSpecial.CheckTotal = Funs.GetNewInt(checkTotal);
                    listReport9ItemSpecial.Add(report9ItemSpecial);
                }
            }
            newItem.SeDinMonthReport9ItemSpecial = listReport9ItemSpecial;
            #endregion
            #region 暂停令
            List<SeDinMonthReport9ItemStoppage> listReport9ItemStoppage = new List<SeDinMonthReport9ItemStoppage>();
            var GetMergedData = GvSeDinMonthReport9ItemStoppage.GetMergedData();
            if (GetMergedData != null)
            {
                foreach (JObject mergedRow in GvSeDinMonthReport9ItemStoppage.GetMergedData())
                {
                    int i = mergedRow.Value<int>("index");
                    JObject values = mergedRow.Value<JObject>("values");
                    var unitName = GvSeDinMonthReport9ItemStoppage.Rows[i].Values[0].ToString();
                    var issuedMonth = values.Value<string>("IssuedMonth");
                    var stoppageMonth = values.Value<string>("StoppageMonth");
                    var issuedTotal = values.Value<string>("IssuedTotal");
                    var stoppageTotal = values.Value<string>("StoppageTotal");
                    SeDinMonthReport9ItemStoppage report9ItemStoppage = new SeDinMonthReport9ItemStoppage();
                    report9ItemStoppage.MonthReportId = MonthReportId;
                    report9ItemStoppage.UnitName = unitName;
                    report9ItemStoppage.IssuedMonth = Funs.GetNewInt(issuedMonth);
                    report9ItemStoppage.StoppageMonth = Funs.GetNewInt(stoppageMonth);
                    report9ItemStoppage.IssuedTotal = Funs.GetNewInt(issuedTotal);
                    report9ItemStoppage.StoppageTotal = Funs.GetNewInt(stoppageTotal);
                    listReport9ItemStoppage.Add(report9ItemStoppage);
                }
            }
            newItem.SeDinMonthReport9ItemStoppage = listReport9ItemStoppage;
            #endregion
            APISeDinMonthReportService.SaveSeDinMonthReport9(newItem);
        }
        #endregion
        #region 保存 MonthReport10、项目奖惩情况统计
        /// <summary>
        /// 保存赛鼎月报
        /// </summary>
        /// <param name="newItem">赛鼎月报</param>
        /// <returns></returns>
        public void SaveSeDinMonthReport10()
        {
            SeDinMonthReport10Item newItem = new SeDinMonthReport10Item
            {
                MonthReportId = MonthReportId,
                SafeMonthNum = Funs.GetNewInt(SafeMonthNum.Value),
                SafeTotalNum = Funs.GetNewInt(SafeTotalNum.Value),
                SafeMonthMoney = Funs.GetNewDecimalOrZero(SafeMonthMoney.Value),
                SafeTotalMoney = Funs.GetNewDecimalOrZero(SafeTotalMoney.Value),
                HseMonthNum = Funs.GetNewInt(HseMonthNum.Value),
                HseTotalNum = Funs.GetNewInt(HseTotalNum.Value),
                HseMonthMoney = Funs.GetNewDecimalOrZero(HseMonthMoney.Value),
                HseTotalMoney = Funs.GetNewDecimalOrZero(HseTotalMoney.Value),
                ProduceMonthNum = Funs.GetNewInt(ProduceMonthNum.Value),
                ProduceTotalNum = Funs.GetNewInt(ProduceTotalNum.Value),
                ProduceMonthMoney = Funs.GetNewDecimalOrZero(ProduceMonthMoney.Value),
                ProduceTotalMoney = Funs.GetNewDecimalOrZero(ProduceTotalMoney.Value),
                AccidentMonthNum = Funs.GetNewInt(AccidentMonthNum.Value),
                AccidentTotalNum = Funs.GetNewInt(AccidentTotalNum.Value),
                AccidentMonthMoney = Funs.GetNewDecimalOrZero(AccidentMonthMoney.Value),
                AccidentTotalMoney = Funs.GetNewDecimalOrZero(AccidentTotalMoney.Value),
                ViolationMonthNum = Funs.GetNewInt(ViolationMonthNum.Value),
                ViolationTotalNum = Funs.GetNewInt(ViolationTotalNum.Value),
                ViolationMonthMoney = Funs.GetNewDecimalOrZero(ViolationMonthMoney.Value),
                ViolationTotalMoney = Funs.GetNewDecimalOrZero(ViolationTotalMoney.Value),
                ManageMonthNum = Funs.GetNewInt(ManageMonthNum.Value),
                ManageTotalNum = Funs.GetNewInt(ManageTotalNum.Value),
                ManageMonthMoney = Funs.GetNewDecimalOrZero(ManageMonthMoney.Value),
                ManageTotalMoney = Funs.GetNewDecimalOrZero(ManageTotalMoney.Value)
            };
            APISeDinMonthReportService.SaveSeDinMonthReport10(newItem);
        }
        #endregion
        #region 保存 MonthReport11、项目危大工程施工情况
        /// <summary>
        /// 保存赛鼎月报
        /// </summary>
        /// <param name="newItem">赛鼎月报</param>
        /// <returns></returns>
        public void SaveSeDinMonthReport11()
        {
            SeDinMonthReport11Item newItem = new SeDinMonthReport11Item
            {
                MonthReportId = MonthReportId,
                RiskWorkNum = Funs.GetNewInt(RiskWorkNum.Value),
                RiskFinishedNum = Funs.GetNewInt(RiskFinishedNum.Value),
                RiskWorkNext = RiskWorkNext.Value,
                LargeWorkNum = Funs.GetNewInt(LargeWorkNum.Value),
                LargeFinishedNum = Funs.GetNewInt(LargeFinishedNum.Value),
                LargeWorkNext = LargeWorkNext.Value
            };
            APISeDinMonthReportService.SaveSeDinMonthReport11(newItem);
        }
        #endregion
        #region 保存 MonthReport12、项目应急演练情况
        /// <summary>
        /// 保存赛鼎月报
        /// </summary>
        /// <param name="newItem">赛鼎月报</param>
        /// <returns></returns>

        public void SaveSeDinMonthReport12()
        {
            SeDinMonthReport12Item newItem = new SeDinMonthReport12Item
            {
                MonthReportId = MonthReportId,
                MultipleSiteInput = Funs.GetNewDecimalOrZero(MultipleSiteInput.Value),
                MultipleSitePerson = Funs.GetNewInt(MultipleSitePerson.Value),
                MultipleSiteNum = Funs.GetNewInt(MultipleSiteNum.Value),
                MultipleSiteTotalNum = Funs.GetNewInt(MultipleSiteTotalNum.Value)
            };
            if (!string.IsNullOrWhiteSpace(MultipleSiteNext.Value))
            {
                newItem.MultipleSiteNext = MultipleSiteNext.Value;
            }
            newItem.MultipleDesktopInput = Funs.GetNewDecimalOrZero(MultipleDesktopInput.Value);
            newItem.MultipleDesktopPerson = Funs.GetNewInt(MultipleDesktopPerson.Value);
            newItem.MultipleDesktopNum = Funs.GetNewInt(MultipleDesktopNum.Value);
            newItem.MultipleDesktopTotalNum = Funs.GetNewInt(MultipleDesktopTotalNum.Value);

            if (!string.IsNullOrWhiteSpace(MultipleDesktopNext.Value))
            {
                newItem.MultipleDesktopNext = MultipleDesktopNext.Value;
            }
            newItem.SingleSiteInput = Funs.GetNewDecimalOrZero(SingleSiteInput.Value);
            newItem.SingleSitePerson = Funs.GetNewInt(SingleSitePerson.Value);
            newItem.SingleSiteNum = Funs.GetNewInt(SingleSiteNum.Value);
            newItem.SingleSiteTotalNum = Funs.GetNewInt(SingleSiteTotalNum.Value);
            newItem.SingleSiteNext = SingleSiteNext.Value;
            newItem.SingleDesktopInput = Funs.GetNewDecimalOrZero(SingleDesktopInput.Value);
            newItem.SingleDesktopPerson = Funs.GetNewInt(SingleDesktopPerson.Value);
            newItem.SingleDesktopNum = Funs.GetNewInt(SingleDesktopNum.Value);
            newItem.SingleDesktopTotalNum = Funs.GetNewInt(SingleDesktopTotalNum.Value);
            if (!string.IsNullOrWhiteSpace(SingleDesktopNext.Value))
            {
                newItem.SingleDesktopNext = SingleDesktopNext.Value;
            }
            APISeDinMonthReportService.SaveSeDinMonthReport12(newItem);
        }
        #endregion
        #region 保存 MonthReport13、14、本月HSE活动综述、下月HSE工作计划
        /// <summary>
        /// 保存赛鼎月报
        /// </summary>
        /// <param name="newItem">赛鼎月报</param>
        /// <returns></returns>    
        public void SaveSeDinMonthReport13()
        {
            SeDinMonthReportItem newItem = new SeDinMonthReportItem
            {
                MonthReportId = MonthReportId,
                ThisSummary = ThisSummary.Text.Trim(),
                NextPlan = NextPlan.Text.Trim(),
                AccidentsSummary = AccidentsSummary.Value
            };
            APISeDinMonthReportService.SaveSeDinMonthReport13(newItem);
        }
        #endregion
        #region 保存按钮
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(SaveSeDinMonthReport0(Const.BtnSave)))
            {
                SaveData();
            }
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }
        #endregion

        #region 提交按钮
        /// <summary>
        /// 提交
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSysSubmit_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(SaveSeDinMonthReport0(Const.BtnSubmit)))
            {
                SaveData();
            }
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }
        #endregion

        #region 保存数据
        /// <summary>
        ///  保存数据
        /// </summary>
        protected void SaveData()
        {
            if (BLL.ManagerMonth_SeDinService.GetMonthReportByMonthReportId(MonthReportId) == null)
            {
                Alert.ShowInTop("请先保存月报主表信息！", MessageBoxIcon.Warning);
                return;
            }
            else
            {
                SaveSeDinMonthReport1();
                SaveSeDinMonthReport2();
                SaveSeDinMonthReport3();
                SaveSeDinMonthReport4();
                SaveSeDinMonthReport5();
                SaveSeDinMonthReport6();
                SaveSeDinMonthReport7();
                SaveSeDinMonthReport8();
                SaveSeDinMonthReport9();
                SaveSeDinMonthReport10();
                SaveSeDinMonthReport11();
                SaveSeDinMonthReport12();
                SaveSeDinMonthReport13();
            }
        }
        #endregion
        #endregion

        #region 获取按钮权限
        /// <summary>
        /// 获取按钮权限
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        private void GetButtonPower()
        {
            if (Request.Params["value"] == "0")
            {
                return;
            }
            var buttonList = CommonService.GetAllButtonList(this.ProjectId, this.CurrUser.UserId, BLL.Const.ProjectManagerMonth_SeDinMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(Const.BtnSave))
                {
                    this.btnSave.Hidden = false;
                    this.btnSysSubmit.Hidden = false;
                }
            }
        }
        #endregion
    }
}