using System;

namespace FineUIPro.Web.HSSE.InformationProject
{
    public partial class SafetyQuarterlyReportView : PageBase
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
               
                this.SafetyQuarterlyReportId = Request.Params["SafetyQuarterlyReportId"];
                if (!string.IsNullOrEmpty(this.SafetyQuarterlyReportId))
                {
                    var safetyQuarterlyReport = BLL.ProjectSafetyQuarterlyReportService.GetSafetyQuarterlyReportById(this.SafetyQuarterlyReportId);
                    if (safetyQuarterlyReport != null)
                    {
                        #region 赋值
                        if (safetyQuarterlyReport.YearId != null)
                        {
                            var constValue = BLL.ConstValue.GetConstByConstValueAndGroupId(safetyQuarterlyReport.YearId.ToString(), BLL.ConstValue.Group_0008);
                            if (constValue!=null)
                            {
                                this.txtYear.Text = constValue.ConstText;
                            }
                        }
                        if (safetyQuarterlyReport.Quarters != null)
                        {
                            var constValue = BLL.ConstValue.GetConstByConstValueAndGroupId(safetyQuarterlyReport.Quarters.ToString(), BLL.ConstValue.Group_0011);
                            if (constValue != null)
                            {
                                this.txtQuarter.Text = constValue.ConstText;
                            }
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
                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.ProjectSafetyQuarterlyReportMenuId;
                this.ctlAuditFlow.DataId = this.SafetyQuarterlyReportId;
                this.ctlAuditFlow.ProjectId = this.CurrUser.LoginProjectId;
            }
        }
        #endregion
    }
}