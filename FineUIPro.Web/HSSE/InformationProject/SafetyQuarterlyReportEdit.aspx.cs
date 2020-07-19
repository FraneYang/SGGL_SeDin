using BLL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FineUIPro.Web.HSSE.InformationProject
{
    public partial class SafetyQuarterlyReportEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        private string SafetyQuarterlyReportId
        {
            get
            {
                return (string)ViewState["SafetyQuarterlyReportId"];
            }
            set
            {
                ViewState["SafetyQuarterlyReportId"] = value;
            }
        }

        /// <summary>
        /// 安全专职人员名单附件
        /// </summary>
        private string FullTimeManAttachUrl
        {
            get
            {
                return (string)ViewState["FullTimeManAttachUrl"];
            }
            set
            {
                ViewState["FullTimeManAttachUrl"] = value;
            }
        }

        /// <summary>
        /// 项目经理人员名单附件
        /// </summary>
        private string PMManAttachUrl
        {
            get
            {
                return (string)ViewState["PMManAttachUrl"];
            }
            set
            {
                ViewState["PMManAttachUrl"] = value;
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
        #endregion        

        #region 加载
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
                this.ProjectId = this.CurrUser.LoginProjectId;
                if (!string.IsNullOrEmpty(Request.Params["projectId"]) && Request.Params["projectId"] != this.CurrUser.LoginProjectId)
                {
                    this.ProjectId = Request.Params["projectId"];
                }
                BLL.ConstValue.InitConstValueDropDownList(this.ddlYearId, BLL.ConstValue.Group_0008, true);
                BLL.ConstValue.InitConstValueDropDownList(this.ddlQuarter, BLL.ConstValue.Group_0011, true);
               
                this.SafetyQuarterlyReportId = Request.Params["SafetyQuarterlyReportId"];
                if (!string.IsNullOrEmpty(this.SafetyQuarterlyReportId))
                {
                    var safetyQuarterlyReport = BLL.ProjectSafetyQuarterlyReportService.GetSafetyQuarterlyReportById(this.SafetyQuarterlyReportId);
                    if (safetyQuarterlyReport != null)
                    {
                        this.ProjectId = safetyQuarterlyReport.ProjectId;
                        #region 赋值
                        if (safetyQuarterlyReport.YearId != null)
                        {
                            this.ddlYearId.SelectedValue = safetyQuarterlyReport.YearId.ToString();
                        }
                        if (safetyQuarterlyReport.Quarters != null)
                        {
                            this.ddlQuarter.SelectedValue = safetyQuarterlyReport.Quarters.ToString();
                        }
                        if (safetyQuarterlyReport.TotalInWorkHours != null)
                        {
                            this.txtTotalInWorkHours.Text = Convert.ToString(safetyQuarterlyReport.TotalInWorkHours);
                        }
                        this.txtTotalInWorkHoursRemark.Text = safetyQuarterlyReport.TotalInWorkHoursRemark;
                        if (safetyQuarterlyReport.TotalOutWorkHours != null)
                        {
                            this.txtTotalOutWorkHours.Text = Convert.ToString(safetyQuarterlyReport.TotalOutWorkHours);
                        }
                        this.txtTotalOutWorkHoursRemark.Text = safetyQuarterlyReport.TotalOutWorkHoursRemark;
                        if (safetyQuarterlyReport.WorkHoursLossRate != null)
                        {
                            this.txtWorkHoursLossRate.Text = Convert.ToString(safetyQuarterlyReport.WorkHoursLossRate);
                        }
                        this.txtWorkHoursLossRateRemark.Text = safetyQuarterlyReport.WorkHoursLossRateRemark;
                        if (safetyQuarterlyReport.WorkHoursAccuracy != null)
                        {
                            this.txtWorkHoursAccuracy.Text = Convert.ToString(safetyQuarterlyReport.WorkHoursAccuracy);
                        }
                        this.txtWorkHoursAccuracyRemark.Text = safetyQuarterlyReport.WorkHoursAccuracyRemark;
                        if (safetyQuarterlyReport.MainBusinessIncome != null)
                        {
                            this.txtMainBusinessIncome.Text = Convert.ToString(safetyQuarterlyReport.MainBusinessIncome);
                        }
                        this.txtMainBusinessIncomeRemark.Text = safetyQuarterlyReport.MainBusinessIncomeRemark;
                        if (safetyQuarterlyReport.ConstructionRevenue != null)
                        {
                            this.txtConstructionRevenue.Text = Convert.ToString(safetyQuarterlyReport.ConstructionRevenue);
                        }
                        this.txtConstructionRevenueRemark.Text = safetyQuarterlyReport.ConstructionRevenueRemark;
                        if (safetyQuarterlyReport.UnitTimeIncome != null)
                        {
                            this.txtUnitTimeIncome.Text = Convert.ToString(safetyQuarterlyReport.UnitTimeIncome);
                        }
                        this.txtUnitTimeIncomeRemark.Text = safetyQuarterlyReport.UnitTimeIncomeRemark;
                        if (safetyQuarterlyReport.BillionsOutputMortality != null)
                        {
                            this.txtBillionsOutputMortality.Text = Convert.ToString(safetyQuarterlyReport.BillionsOutputMortality);
                        }
                        this.txtBillionsOutputMortalityRemark.Text = safetyQuarterlyReport.BillionsOutputMortalityRemark;
                        if (safetyQuarterlyReport.MajorFireAccident != null)
                        {
                            this.txtMajorFireAccident.Text = Convert.ToString(safetyQuarterlyReport.MajorFireAccident);
                        }
                        this.txtMajorFireAccidentRemark.Text = safetyQuarterlyReport.MajorFireAccidentRemark;
                        if (safetyQuarterlyReport.MajorEquipAccident != null)
                        {
                            this.txtMajorEquipAccident.Text = Convert.ToString(safetyQuarterlyReport.MajorEquipAccident);
                        }
                        this.txtMajorEquipAccidentRemark.Text = safetyQuarterlyReport.MajorEquipAccidentRemark;
                        if (safetyQuarterlyReport.AccidentFrequency != null)
                        {
                            this.txtAccidentFrequency.Text = Convert.ToString(safetyQuarterlyReport.AccidentFrequency);
                        }
                        this.txtAccidentFrequencyRemark.Text = safetyQuarterlyReport.AccidentFrequencyRemark;
                        if (safetyQuarterlyReport.SeriousInjuryAccident != null)
                        {
                            this.txtSeriousInjuryAccident.Text = Convert.ToString(safetyQuarterlyReport.SeriousInjuryAccident);
                        }
                        this.txtSeriousInjuryAccidentRemark.Text = safetyQuarterlyReport.SeriousInjuryAccidentRemark;
                        if (safetyQuarterlyReport.FireAccident != null)
                        {
                            this.txtFireAccident.Text = Convert.ToString(safetyQuarterlyReport.FireAccident);
                        }
                        this.txtFireAccidentRemark.Text = safetyQuarterlyReport.FireAccidentRemark;
                        if (safetyQuarterlyReport.EquipmentAccident != null)
                        {
                            this.txtEquipmentAccident.Text = Convert.ToString(safetyQuarterlyReport.EquipmentAccident);
                        }
                        this.txtEquipmentAccidentRemark.Text = safetyQuarterlyReport.EquipmentAccidentRemark;
                        if (safetyQuarterlyReport.PoisoningAndInjuries != null)
                        {
                            this.txtPoisoningAndInjuries.Text = Convert.ToString(safetyQuarterlyReport.PoisoningAndInjuries);
                        }
                        this.txtPoisoningAndInjuriesRemark.Text = safetyQuarterlyReport.PoisoningAndInjuriesRemark;
                        if (safetyQuarterlyReport.ProductionSafetyInTotal != null)
                        {
                            this.txtProductionSafetyInTotal.Text = Convert.ToString(safetyQuarterlyReport.ProductionSafetyInTotal);
                        }
                        this.txtProductionSafetyInTotalRemark.Text = safetyQuarterlyReport.ProductionSafetyInTotalRemark;
                        if (safetyQuarterlyReport.ProtectionInput != null)
                        {
                            this.txtProtectionInput.Text = Convert.ToString(safetyQuarterlyReport.ProtectionInput);
                        }
                        this.txtProtectionInputRemark.Text = safetyQuarterlyReport.ProtectionInputRemark;
                        if (safetyQuarterlyReport.LaboAndHealthIn != null)
                        {
                            this.txtLaboAndHealthIn.Text = Convert.ToString(safetyQuarterlyReport.LaboAndHealthIn);
                        }
                        this.txtLaboAndHealthInRemark.Text = safetyQuarterlyReport.LaborAndHealthInRemark;
                        if (safetyQuarterlyReport.TechnologyProgressIn != null)
                        {
                            this.txtTechnologyProgressIn.Text = Convert.ToString(safetyQuarterlyReport.TechnologyProgressIn);
                        }
                        this.txtTechnologyProgressInRemark.Text = safetyQuarterlyReport.TechnologyProgressInRemark;
                        if (safetyQuarterlyReport.EducationTrainIn != null)
                        {
                            this.txtEducationTrainIn.Text = Convert.ToString(safetyQuarterlyReport.EducationTrainIn);
                        }
                        this.txtEducationTrainInRemark.Text = safetyQuarterlyReport.EducationTrainInRemark;
                        if (safetyQuarterlyReport.ProjectCostRate != null)
                        {
                            this.txtProjectCostRate.Text = Convert.ToString(safetyQuarterlyReport.ProjectCostRate);
                        }
                        this.txtProjectCostRateRemark.Text = safetyQuarterlyReport.ProjectCostRateRemark;
                        if (safetyQuarterlyReport.ProductionInput != null)
                        {
                            this.txtProductionInput.Text = Convert.ToString(safetyQuarterlyReport.ProductionInput);
                        }
                        this.txtProductionInputRemark.Text = safetyQuarterlyReport.ProductionInputRemark;
                        if (safetyQuarterlyReport.Revenue != null)
                        {
                            this.txtRevenue.Text = Convert.ToString(safetyQuarterlyReport.Revenue);
                        }
                        this.txtRevenueRemark.Text = safetyQuarterlyReport.RevenueRemark;
                        if (safetyQuarterlyReport.FullTimeMan != null)
                        {
                            this.txtFullTimeMan.Text = Convert.ToString(safetyQuarterlyReport.FullTimeMan);
                        }
                        this.txtFullTimeManRemark.Text = safetyQuarterlyReport.FullTimeManRemark;

                        if (safetyQuarterlyReport.PMMan != null)
                        {
                            this.txtPMMan.Text = Convert.ToString(safetyQuarterlyReport.PMMan);
                        }
                        this.txtPMManRemark.Text = safetyQuarterlyReport.PMManRemark;

                        if (safetyQuarterlyReport.CorporateDirectorEdu != null)
                        {
                            this.txtCorporateDirectorEdu.Text = Convert.ToString(safetyQuarterlyReport.CorporateDirectorEdu);
                        }
                        this.txtCorporateDirectorEduRemark.Text = safetyQuarterlyReport.CorporateDirectorEduRemark;
                        if (safetyQuarterlyReport.ProjectLeaderEdu != null)
                        {
                            this.txtProjectLeaderEdu.Text = Convert.ToString(safetyQuarterlyReport.ProjectLeaderEdu);
                        }
                        this.txtProjectLeaderEduRemark.Text = safetyQuarterlyReport.ProjectLeaderEduRemark;
                        if (safetyQuarterlyReport.FullTimeEdu != null)
                        {
                            this.txtFullTimeEdu.Text = Convert.ToString(safetyQuarterlyReport.FullTimeEdu);
                        }
                        this.txtFullTimeEduRemark.Text = safetyQuarterlyReport.FullTimeEduRemark;
                        if (safetyQuarterlyReport.ThreeKidsEduRate != null)
                        {
                            this.txtThreeKidsEduRate.Text = Convert.ToString(safetyQuarterlyReport.ThreeKidsEduRate);
                        }
                        this.txtThreeKidsEduRateRemark.Text = safetyQuarterlyReport.ThreeKidsEduRateRemark;
                        if (safetyQuarterlyReport.UplinReportRate != null)
                        {
                            this.txtUplinReportRate.Text = Convert.ToString(safetyQuarterlyReport.UplinReportRate);
                        }
                        this.txtUplinReportRateRemark.Text = safetyQuarterlyReport.UplinReportRateRemark;
                        this.txtRemark.Text = safetyQuarterlyReport.Remarks;
                        if (safetyQuarterlyReport.KeyEquipmentTotal != null)
                        {
                            this.txtKeyEquipmentTotal.Text = Convert.ToString(safetyQuarterlyReport.KeyEquipmentTotal);
                        }
                        this.txtKeyEquipmentTotalRemark.Text = safetyQuarterlyReport.KeyEquipmentTotalRemark;
                        if (safetyQuarterlyReport.KeyEquipmentReportCount != null)
                        {
                            this.txtKeyEquipmentReportCount.Text = Convert.ToString(safetyQuarterlyReport.KeyEquipmentReportCount);
                        }
                        this.txtKeyEquipmentReportCountRemark.Text = safetyQuarterlyReport.KeyEquipmentReportCountRemark;
                        if (safetyQuarterlyReport.ChemicalAreaProjectCount != null)
                        {
                            this.txtChemicalAreaProjectCount.Text = Convert.ToString(safetyQuarterlyReport.ChemicalAreaProjectCount);
                        }
                        this.txtChemicalAreaProjectCountRemark.Text = safetyQuarterlyReport.ChemicalAreaProjectCountRemark;
                        if (safetyQuarterlyReport.HarmfulMediumCoverCount != null)
                        {
                            this.txtHarmfulMediumCoverCount.Text = Convert.ToString(safetyQuarterlyReport.HarmfulMediumCoverCount);
                        }
                        this.txtHarmfulMediumCoverCountRemark.Text = safetyQuarterlyReport.HarmfulMediumCoverCountRemark;
                        if (safetyQuarterlyReport.HarmfulMediumCoverRate != null)
                        {
                            this.txtHarmfulMediumCoverRate.Text = Convert.ToString(safetyQuarterlyReport.HarmfulMediumCoverRate);
                        }
                        this.txtHarmfulMediumCoverRateRemark.Text = safetyQuarterlyReport.HarmfulMediumCoverRateRemark;
                        this.FullTimeManAttachUrl = safetyQuarterlyReport.FullTimeManAttachUrl;
                        this.divFullTimeManAttachUrl.InnerHtml = BLL.UploadAttachmentService.ShowAttachment("../../", this.FullTimeManAttachUrl);
                        this.PMManAttachUrl = safetyQuarterlyReport.PMManAttachUrl;
                        this.divPMManAttachUrl.InnerHtml = BLL.UploadAttachmentService.ShowAttachment("../../", this.PMManAttachUrl);
                        #endregion
                    }
                }
                else
                {
                    DateTime showDate = DateTime.Now.AddMonths(-3);
                    this.ddlQuarter.SelectedValue = Funs.GetNowQuarterlyByTime(showDate).ToString();
                    this.ddlYearId.SelectedValue = showDate.Year.ToString();
                    DateTime startTime = Funs.GetQuarterlyMonths(this.ddlYearId.SelectedValue, this.ddlQuarter.SelectedValue);
                    DateTime endTime = startTime.AddMonths(3);
                    GetData(startTime, endTime);
                }
                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.ProjectSafetyQuarterlyReportMenuId;
                this.ctlAuditFlow.DataId = this.SafetyQuarterlyReportId;
                this.ctlAuditFlow.ProjectId = this.ProjectId;
            }
        }
        #endregion

        #region 附件上传
        /// <summary>
        /// 安全专职人员名单附件上传
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnFullTimeManAttachUrl_Click(object sender, EventArgs e)
        {
            if (btnFullTimeManAttachUrl.HasFile)
            {
                this.FullTimeManAttachUrl = BLL.UploadFileService.UploadAttachment(BLL.Funs.RootPath, this.btnFullTimeManAttachUrl, this.FullTimeManAttachUrl, UploadFileService.FullTimeManFilePath);
                this.divFullTimeManAttachUrl.InnerHtml = BLL.UploadAttachmentService.ShowAttachment("../../", this.FullTimeManAttachUrl);
            }
        }

        /// <summary>
        /// 项目经理人员名单附件上传
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnPMManAttachUrl_Click(object sender, EventArgs e)
        {
            if (btnPMManAttachUrl.HasFile)
            {
                this.PMManAttachUrl = BLL.UploadFileService.UploadAttachment(BLL.Funs.RootPath, this.btnPMManAttachUrl, this.PMManAttachUrl, UploadFileService.PMManFilePath);
                this.divPMManAttachUrl.InnerHtml = BLL.UploadAttachmentService.ShowAttachment("../../", this.PMManAttachUrl);
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
            if (this.ddlYearId.SelectedValue == BLL.Const._Null)
            {
                Alert.ShowInTop("请选择年度", MessageBoxIcon.Warning);
                return;
            }
            if (this.ddlQuarter.SelectedValue == BLL.Const._Null)
            {
                Alert.ShowInTop("请选择季度", MessageBoxIcon.Warning);
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
            if (this.ddlYearId.SelectedValue == BLL.Const._Null)
            {
                Alert.ShowInTop("请选择年度", MessageBoxIcon.Warning);
                return;
            }
            if (this.ddlQuarter.SelectedValue == BLL.Const._Null)
            {
                Alert.ShowInTop("请选择季度", MessageBoxIcon.Warning);
                return;
            }
            this.SaveData(BLL.Const.BtnSubmit);
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
        }

        /// <summary>
        /// 提交数据
        /// </summary>
        /// <param name="p"></param>
        private void SaveData(string type)
        {
            Model.InformationProject_SafetyQuarterlyReport safetyQuarterlyReport = new Model.InformationProject_SafetyQuarterlyReport
            {
                ProjectId = this.ProjectId,
                UnitId = string.IsNullOrEmpty(this.CurrUser.UnitId) ? Const.UnitId_SEDIN : this.CurrUser.UnitId,
            };
            if (this.ddlYearId.SelectedValue != BLL.Const._Null)
            {
                safetyQuarterlyReport.YearId = Funs.GetNewIntOrZero(this.ddlYearId.SelectedValue);
            }
            if (this.ddlQuarter.SelectedValue != BLL.Const._Null)
            {
                safetyQuarterlyReport.Quarters = Funs.GetNewIntOrZero(this.ddlQuarter.SelectedValue);
            }
            safetyQuarterlyReport.TotalInWorkHours = Funs.GetNewIntOrZero(this.txtTotalInWorkHours.Text.Trim());
            safetyQuarterlyReport.TotalInWorkHoursRemark = this.txtTotalInWorkHoursRemark.Text.Trim();
            safetyQuarterlyReport.TotalOutWorkHours = Funs.GetNewIntOrZero(this.txtTotalOutWorkHours.Text.Trim());
            safetyQuarterlyReport.TotalOutWorkHoursRemark = this.txtTotalOutWorkHoursRemark.Text.Trim();
            safetyQuarterlyReport.WorkHoursLossRate = Funs.GetNewDecimalOrZero(this.txtWorkHoursLossRate.Text.Trim());
            safetyQuarterlyReport.WorkHoursLossRateRemark = this.txtWorkHoursLossRateRemark.Text.Trim();
            safetyQuarterlyReport.WorkHoursAccuracy = Funs.GetNewDecimalOrZero(this.txtWorkHoursAccuracy.Text.Trim());
            safetyQuarterlyReport.WorkHoursAccuracyRemark = this.txtWorkHoursAccuracyRemark.Text.Trim();
            safetyQuarterlyReport.MainBusinessIncome = Funs.GetNewDecimalOrZero(this.txtMainBusinessIncome.Text.Trim());
            safetyQuarterlyReport.MainBusinessIncomeRemark = this.txtMainBusinessIncomeRemark.Text.Trim();
            safetyQuarterlyReport.ConstructionRevenue = Funs.GetNewDecimalOrZero(this.txtConstructionRevenue.Text.Trim());
            safetyQuarterlyReport.ConstructionRevenueRemark = this.txtConstructionRevenueRemark.Text.Trim();
            safetyQuarterlyReport.UnitTimeIncome = Funs.GetNewDecimalOrZero(this.txtUnitTimeIncome.Text.Trim());
            safetyQuarterlyReport.UnitTimeIncomeRemark = this.txtUnitTimeIncomeRemark.Text.Trim();
            safetyQuarterlyReport.BillionsOutputMortality = Funs.GetNewDecimalOrZero(this.txtBillionsOutputMortality.Text.Trim());
            safetyQuarterlyReport.BillionsOutputMortalityRemark = this.txtBillionsOutputMortalityRemark.Text.Trim();
            safetyQuarterlyReport.MajorFireAccident = Funs.GetNewIntOrZero(this.txtMajorFireAccident.Text.Trim());
            safetyQuarterlyReport.MajorFireAccidentRemark = this.txtMajorFireAccidentRemark.Text.Trim();
            safetyQuarterlyReport.MajorEquipAccident = Funs.GetNewIntOrZero(this.txtMajorEquipAccident.Text.Trim());
            safetyQuarterlyReport.MajorEquipAccidentRemark = this.txtMajorEquipAccidentRemark.Text.Trim();
            safetyQuarterlyReport.AccidentFrequency = Funs.GetNewDecimalOrZero(this.txtAccidentFrequency.Text.Trim());
            safetyQuarterlyReport.AccidentFrequencyRemark = this.txtAccidentFrequencyRemark.Text.Trim();
            safetyQuarterlyReport.SeriousInjuryAccident = Funs.GetNewIntOrZero(this.txtSeriousInjuryAccident.Text.Trim());
            safetyQuarterlyReport.SeriousInjuryAccidentRemark = this.txtSeriousInjuryAccidentRemark.Text.Trim();
            safetyQuarterlyReport.FireAccident = Funs.GetNewIntOrZero(this.txtFireAccident.Text.Trim());
            safetyQuarterlyReport.FireAccidentRemark = this.txtFireAccidentRemark.Text.Trim();
            safetyQuarterlyReport.EquipmentAccident = Funs.GetNewIntOrZero(this.txtEquipmentAccident.Text.Trim());
            safetyQuarterlyReport.EquipmentAccidentRemark = this.txtEquipmentAccidentRemark.Text.Trim();
            safetyQuarterlyReport.PoisoningAndInjuries = Funs.GetNewIntOrZero(this.txtPoisoningAndInjuries.Text);
            safetyQuarterlyReport.PoisoningAndInjuriesRemark = this.txtPoisoningAndInjuriesRemark.Text.Trim();
            safetyQuarterlyReport.ProductionSafetyInTotal = Funs.GetNewIntOrZero(this.txtProductionSafetyInTotal.Text.Trim());
            safetyQuarterlyReport.ProductionSafetyInTotalRemark = this.txtProductionSafetyInTotalRemark.Text.Trim();
            safetyQuarterlyReport.ProtectionInput = Funs.GetNewDecimalOrZero(this.txtProtectionInput.Text.Trim());
            safetyQuarterlyReport.ProtectionInputRemark = this.txtProtectionInputRemark.Text.Trim();
            safetyQuarterlyReport.LaboAndHealthIn = Funs.GetNewDecimalOrZero(this.txtLaboAndHealthIn.Text.Trim());
            safetyQuarterlyReport.LaborAndHealthInRemark = this.txtLaboAndHealthInRemark.Text.Trim();
            safetyQuarterlyReport.TechnologyProgressIn = Funs.GetNewDecimalOrZero(this.txtTechnologyProgressIn.Text.Trim());
            safetyQuarterlyReport.TechnologyProgressInRemark = this.txtTechnologyProgressInRemark.Text.Trim();
            safetyQuarterlyReport.EducationTrainIn = Funs.GetNewDecimalOrZero(this.txtEducationTrainIn.Text.Trim());
            safetyQuarterlyReport.EducationTrainInRemark = this.txtEducationTrainInRemark.Text.Trim();
            safetyQuarterlyReport.ProjectCostRate = Funs.GetNewDecimalOrZero(this.txtProjectCostRate.Text.Trim());
            safetyQuarterlyReport.ProjectCostRateRemark = this.txtProjectCostRateRemark.Text.Trim();
            safetyQuarterlyReport.ProductionInput = Funs.GetNewDecimalOrZero(this.txtProductionInput.Text.Trim());
            safetyQuarterlyReport.ProductionInputRemark = this.txtProductionInputRemark.Text.Trim();
            safetyQuarterlyReport.Revenue = Funs.GetNewDecimalOrZero(this.txtRevenue.Text.Trim());
            safetyQuarterlyReport.RevenueRemark = this.txtRevenueRemark.Text.Trim();
            safetyQuarterlyReport.FullTimeMan = Funs.GetNewIntOrZero(this.txtFullTimeMan.Text.Trim());
            safetyQuarterlyReport.FullTimeManRemark = this.txtFullTimeManRemark.Text;
            safetyQuarterlyReport.FullTimeManAttachUrl = this.FullTimeManAttachUrl;
            safetyQuarterlyReport.PMMan = Funs.GetNewIntOrZero(this.txtPMMan.Text.Trim());
            safetyQuarterlyReport.PMManRemark = this.txtPMManRemark.Text.Trim();
            safetyQuarterlyReport.PMManAttachUrl = this.PMManAttachUrl;
            safetyQuarterlyReport.CorporateDirectorEdu = Funs.GetNewIntOrZero(this.txtCorporateDirectorEdu.Text.Trim());
            safetyQuarterlyReport.CorporateDirectorEduRemark = this.txtCorporateDirectorEduRemark.Text.Trim();
            safetyQuarterlyReport.ProjectLeaderEdu = Funs.GetNewIntOrZero(this.txtProjectLeaderEdu.Text.Trim());
            safetyQuarterlyReport.ProjectLeaderEduRemark = this.txtProjectLeaderEduRemark.Text.Trim();
            safetyQuarterlyReport.FullTimeEdu = Funs.GetNewIntOrZero(this.txtFullTimeEdu.Text.Trim());
            safetyQuarterlyReport.FullTimeEduRemark = this.txtFullTimeEduRemark.Text.Trim();
            safetyQuarterlyReport.ThreeKidsEduRate = Funs.GetNewDecimalOrZero(this.txtThreeKidsEduRate.Text.Trim());
            safetyQuarterlyReport.ThreeKidsEduRateRemark = this.txtThreeKidsEduRateRemark.Text.Trim();
            safetyQuarterlyReport.UplinReportRate = Funs.GetNewDecimalOrZero(this.txtUplinReportRate.Text.Trim());
            safetyQuarterlyReport.UplinReportRateRemark = this.txtUplinReportRateRemark.Text.Trim();
            safetyQuarterlyReport.Remarks = this.txtRemark.Text.Trim();
            safetyQuarterlyReport.KeyEquipmentTotal = Funs.GetNewIntOrZero(this.txtKeyEquipmentTotal.Text.Trim());
            safetyQuarterlyReport.KeyEquipmentTotalRemark = this.txtKeyEquipmentTotalRemark.Text.Trim();
            safetyQuarterlyReport.KeyEquipmentReportCount = Funs.GetNewIntOrZero(this.txtKeyEquipmentReportCount.Text.Trim());
            safetyQuarterlyReport.KeyEquipmentReportCountRemark = this.txtKeyEquipmentReportCountRemark.Text.Trim();
            safetyQuarterlyReport.ChemicalAreaProjectCount = Funs.GetNewIntOrZero(this.txtChemicalAreaProjectCount.Text.Trim());
            safetyQuarterlyReport.ChemicalAreaProjectCountRemark = this.txtChemicalAreaProjectCountRemark.Text.Trim();
            safetyQuarterlyReport.HarmfulMediumCoverCount = Funs.GetNewIntOrZero(this.txtHarmfulMediumCoverCount.Text.Trim());
            safetyQuarterlyReport.HarmfulMediumCoverCountRemark = this.txtHarmfulMediumCoverCountRemark.Text.Trim();
            safetyQuarterlyReport.HarmfulMediumCoverRate = Funs.GetNewDecimalOrZero(this.txtHarmfulMediumCoverRate.Text.Trim());
            safetyQuarterlyReport.HarmfulMediumCoverRateRemark = this.txtHarmfulMediumCoverRateRemark.Text.Trim();
            safetyQuarterlyReport.CompileMan = this.CurrUser.UserId;
            safetyQuarterlyReport.CompileDate = DateTime.Now;
            safetyQuarterlyReport.States = BLL.Const.State_0;
            if (type == BLL.Const.BtnSubmit)
            {
                safetyQuarterlyReport.States = this.ctlAuditFlow.NextStep;
            }
            if (!string.IsNullOrEmpty(this.SafetyQuarterlyReportId))
            {
                safetyQuarterlyReport.SafetyQuarterlyReportId = this.SafetyQuarterlyReportId;
                BLL.ProjectSafetyQuarterlyReportService.UpdateSafetyQuarterlyReport(safetyQuarterlyReport);
                BLL.LogService.AddSys_Log(this.CurrUser, safetyQuarterlyReport.YearId.ToString() + "-" + safetyQuarterlyReport.Quarters.ToString(), safetyQuarterlyReport.SafetyQuarterlyReportId, BLL.Const.ProjectSafetyQuarterlyReportMenuId, BLL.Const.BtnModify);
            }
            else
            {
                Model.InformationProject_SafetyQuarterlyReport oldSafetyQuarterlyReport = (from x in Funs.DB.InformationProject_SafetyQuarterlyReport
                                                                                           where x.ProjectId == safetyQuarterlyReport.ProjectId && x.YearId == safetyQuarterlyReport.YearId && x.Quarters == safetyQuarterlyReport.Quarters
                                                                                           select x).FirstOrDefault();
                if (oldSafetyQuarterlyReport == null)
                {
                    this.SafetyQuarterlyReportId = SQLHelper.GetNewID(typeof(Model.InformationProject_SafetyQuarterlyReport));
                    safetyQuarterlyReport.SafetyQuarterlyReportId = this.SafetyQuarterlyReportId;
                    BLL.ProjectSafetyQuarterlyReportService.AddSafetyQuarterlyReport(safetyQuarterlyReport);
                    BLL.LogService.AddSys_Log(this.CurrUser, safetyQuarterlyReport.YearId.ToString() + "-" + safetyQuarterlyReport.Quarters.ToString(), safetyQuarterlyReport.SafetyQuarterlyReportId, BLL.Const.ProjectSafetyQuarterlyReportMenuId, BLL.Const.BtnAdd);
                    ////删除未上报月报信息
                    //Model.ManagementReport_ReportRemind reportRemind = (from x in Funs.DB.ManagementReport_ReportRemind
                    //                                                    where x.ProjectId == this.ProjectId && x.Year == safetyQuarterlyReport.YearId && x.Quarterly == safetyQuarterlyReport.Quarters && x.ReportName == "安全生产数据季报"
                    //                                                    select x).FirstOrDefault();
                    //if (reportRemind != null)
                    //{
                    //    BLL.ReportRemindService.DeleteReportRemindByReportRemind(reportRemind);
                    //}
                }
                else
                {
                    Alert.ShowInTop("该季度记录已存在", MessageBoxIcon.Warning);
                    return;
                }
            }
            ////保存流程审核数据         
            this.ctlAuditFlow.btnSaveData(this.ProjectId, BLL.Const.ProjectSafetyQuarterlyReportMenuId, this.SafetyQuarterlyReportId, (type == BLL.Const.BtnSubmit ? true : false), safetyQuarterlyReport.YearId + "-" + safetyQuarterlyReport.Quarters, "../InformationProject/SafetyQuarterlyReportView.aspx?SafetyQuarterlyReportId={0}");
        }
        #endregion

        #region 年季度变化事件
        /// <summary>
        /// 年季度变化事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.ddlYearId.SelectedValue != BLL.Const._Null && this.ddlQuarter.SelectedValue != BLL.Const._Null)
            {
                DateTime startTime = Funs.GetQuarterlyMonths(this.ddlYearId.SelectedValue, this.ddlQuarter.SelectedValue);
                DateTime endTime = startTime.AddMonths(3);
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
                decimal sumPersonWorkTimeTotal = 0;
                //获取当期人工时日报
                List<Model.SitePerson_DayReport> dayReports = BLL.SitePerson_DayReportService.GetDayReportsByCompileDate(startTime, endTime, this.ProjectId);
                //完成人工时（当期）
                sumPersonWorkTimeTotal = (from x in dayReports
                                          join y in db.SitePerson_DayReportDetail
                                       on x.DayReportId equals y.DayReportId
                                          select y.PersonWorkTime ?? 0).Sum();
                //总投入工时数
                this.txtTotalInWorkHours.Text = sumPersonWorkTimeTotal.ToString();
                //总损失工时数
                var accidentReports = BLL.AccidentReportService.GetAccidentReportsByAccidentTime(startTime, endTime, this.ProjectId);
                this.txtTotalOutWorkHours.Text = accidentReports.Sum(x => x.WorkingHoursLoss ?? 0).ToString();
                //百万工时损失率
                if (sumPersonWorkTimeTotal != 0)
                {
                    decimal totalOutWorkHours = Funs.GetNewDecimalOrZero(this.txtTotalOutWorkHours.Text.Trim());
                    this.txtWorkHoursLossRate.Text = decimal.Round((totalOutWorkHours * 1000000 / sumPersonWorkTimeTotal), 2).ToString();
                }
                else
                {
                    this.txtWorkHoursLossRate.Text = "0";
                }
            }
        }
        #endregion
    }
}