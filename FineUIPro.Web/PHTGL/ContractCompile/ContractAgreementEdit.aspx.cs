using BLL;
using System;

namespace FineUIPro.Web.PHTGL.ContractCompile
{
    public partial class ContractAgreementEdit : PageBase
    {
        public string subcontractAgreementId
        {
            get
            {
                return (string)ViewState["subcontractAgreementId"];
            }
            set
            {
                ViewState["subcontractAgreementId"] = value;
            }
        }
        public string contractId
        {
            get
            {
                return (string)ViewState["contractId"];
            }
            set
            {
                ViewState["contractId"] = value;
            }
        }

        #region 加载
        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                   contractId = "合同协议书模板";
                   subcontractAgreementId = "合同协议书模板";
                if (!string.IsNullOrEmpty(subcontractAgreementId))
                {
                    Model.PHTGL_SubcontractAgreement sub = BLL.SubcontractAgreementService.GetSubcontractAgreementById(subcontractAgreementId);
                    if (sub != null)
                    {
                        this.tab2_txtGeneralContractor.Text = sub.GeneralContractor;
                        this.tab2_txtSubConstruction.Text = sub.SubConstruction;
                        tab2_txtSub.Text = sub.SubConstruction;
                        this.tab2_txtContents.Text = sub.Contents;
                        this.tab2_txtContractProject.Text = sub.ContractProject;
                        this.tab2_txtContractProjectOwner.Text = sub.ContractProjectOwner;
                        this.tab2_txtSubProject.Text = sub.SubProject;
                        this.tab2_txtSubProjectAddress.Text = sub.SubProjectAddress;
                        this.tab2_txtFundingSources.Text = sub.FundingSources;
                        this.tab2_txtSubProjectContractScope.Text = sub.SubProjectContractScope;
                        this.tab2_txtSubProjectContent.Text = sub.SubProjectContent;
                        this.tab2_txtPlanStartYear.Text = sub.PlanStartYear.HasValue ? sub.PlanStartYear.ToString() : "";
                        this.tab2_txtPlanStartMonth.Text = sub.PlanStartMonth.HasValue ? sub.PlanStartMonth.ToString() : "";
                        this.tab2_txtPlanStartDay.Text = sub.PlanStartDay.HasValue ? sub.PlanStartDay.ToString() : "";
                        this.tab2_txtPlanEndYear.Text = sub.PlanEndYear.HasValue ? sub.PlanEndYear.ToString() : "";
                        this.tab2_txtPlanEndMonth.Text = sub.PlanEndMonth.HasValue ? sub.PlanEndMonth.ToString() : "";
                        this.tab2_txtPlanEndDay.Text = sub.PlanEndDay.HasValue ? sub.PlanEndDay.ToString() : "";
                        this.tab2_txtLimit.Text = sub.Limit.HasValue ? sub.Limit.ToString() : "";
                        this.tab2_txtQualityStandards.Text = sub.QualityStandards;
                        this.tab2_txtHSEManageStandards.Text = sub.HSEManageStandards;
                        this.tab2_txtSubcontractPriceForm.Text = sub.SubcontractPriceForm;
                        this.tab2_txtContractPriceCapital.Text = sub.ContractPriceCapital;
                        this.tab2_txtContractPriceCNY.Text = sub.ContractPriceCNY.HasValue ? sub.ContractPriceCNY.ToString() : "";
                        this.tab2_txtContractPriceDesc.Text = sub.ContractPriceDesc;
                        this.tab2_txtInvoice.Text = sub.Invoice;
                        this.tab2_txtLaw.Text = sub.Law;
                        this.tab2_txtSignedYear.Text = sub.SignedYear.HasValue ? sub.SignedYear.ToString() : "";
                        this.tab2_txtSignedMonth.Text = sub.SignedMonth.HasValue ? sub.SignedMonth.ToString() : "";
                        this.tab2_txtSignedAddress.Text = sub.SignedAddress;
                        this.tab2_txtAgreementNum.Text = sub.AgreementNum.HasValue ? sub.AgreementNum.ToString() : "";
                        this.tab2_txtGeneralContractorNum.Text = sub.GeneralContractorNum.HasValue ? sub.GeneralContractorNum.ToString() : "";
                        this.tab2_txtSubContractorNum.Text = sub.SubContractorNum.HasValue ? sub.SubContractorNum.ToString() : "";
                        this.tab2_txtSocialCreditCode1.Text = sub.SocialCreditCode1;
                        this.tab2_txtSocialCreditCode2.Text = sub.SocialCreditCode2;
                        this.tab2_txtAddress1.Text = sub.Address1;
                        this.tab2_txtAddress2.Text = sub.Address2;
                        this.tab2_txtZipCode1.Text = sub.ZipCode1;
                        this.tab2_txtZipCode2.Text = sub.ZipCode2;
                        this.tab2_txtLegalRepresentative1.Text = sub.LegalRepresentative1;
                        this.tab2_txtLegalRepresentative2.Text = sub.LegalRepresentative2;
                        this.tab2_txtEntrustedAgent1.Text = sub.EntrustedAgent1;
                        this.tab2_txtEntrustedAgent2.Text = sub.EntrustedAgent2;
                        this.tab2_txtTelephone1.Text = sub.Telephone1;
                        this.tab2_txtTelephone2.Text = sub.Telephone2;
                        this.tab2_txtFax1.Text = sub.Fax1;
                        this.tab2_txtFax2.Text = sub.Fax2;
                        this.tab2_txtEmail1.Text = sub.Email1;
                        this.tab2_txtEmail2.Text = sub.Email2;
                        this.tab2_txtBank1.Text = sub.Bank1;
                        this.tab2_txtBank2.Text = sub.Bank2;
                        this.tab2_txtAccount1.Text = sub.Account1;
                        this.tab2_txtAccount2.Text = sub.Account2;
                    }
                }
            }
        }
        #endregion

        #region 保存
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            Model.PHTGL_SubcontractAgreement newSub = new Model.PHTGL_SubcontractAgreement();
            newSub.GeneralContractor = tab2_txtGeneralContractor.Text;
            newSub.SubConstruction = tab2_txtSubConstruction.Text;
            newSub.Contents = tab2_txtContents.Text;
            newSub.ContractProject = tab2_txtContractProject.Text;
            newSub.ContractProjectOwner = tab2_txtContractProjectOwner.Text;
            newSub.SubProject = tab2_txtSubProject.Text;
            newSub.SubProjectAddress = tab2_txtSubProjectAddress.Text;
            newSub.FundingSources = tab2_txtFundingSources.Text;
            newSub.SubProjectContractScope = tab2_txtSubProjectContractScope.Text;
            newSub.SubProjectContent = tab2_txtSubProjectContent.Text;
            newSub.PlanStartYear = Funs.GetNewInt(tab2_txtPlanStartYear.Text);
            newSub.PlanStartMonth = Funs.GetNewInt(tab2_txtPlanStartMonth.Text);
            newSub.PlanStartDay = Funs.GetNewInt(tab2_txtPlanStartDay.Text);
            newSub.PlanEndYear = Funs.GetNewInt(tab2_txtPlanEndYear.Text);
            newSub.PlanEndMonth = Funs.GetNewInt(tab2_txtPlanEndMonth.Text);
            newSub.PlanEndDay = Funs.GetNewInt(tab2_txtPlanEndDay.Text);
            newSub.Limit = Funs.GetNewInt(tab2_txtLimit.Text);
            newSub.QualityStandards = tab2_txtQualityStandards.Text;
            newSub.HSEManageStandards = tab2_txtHSEManageStandards.Text;
            newSub.SubcontractPriceForm = tab2_txtSubcontractPriceForm.Text;
            newSub.ContractPriceCapital = tab2_txtContractPriceCapital.Text;
            newSub.ContractPriceCNY = Funs.GetNewDecimal(tab2_txtContractPriceCNY.Text);
            newSub.ContractPriceDesc = tab2_txtContractPriceDesc.Text;
            newSub.Invoice = tab2_txtInvoice.Text;
            newSub.Law = tab2_txtLaw.Text;
            newSub.SignedYear = Funs.GetNewInt(tab2_txtSignedYear.Text);
            newSub.SignedMonth = Funs.GetNewInt(tab2_txtSignedMonth.Text);
            newSub.SignedAddress = tab2_txtSignedAddress.Text;
            newSub.AgreementNum = Funs.GetNewInt(tab2_txtAgreementNum.Text);
            newSub.GeneralContractorNum = Funs.GetNewInt(tab2_txtGeneralContractorNum.Text);
            newSub.SubContractorNum = Funs.GetNewInt(tab2_txtSubContractorNum.Text);
            newSub.SocialCreditCode1 = tab2_txtSocialCreditCode1.Text;
            newSub.SocialCreditCode2 = tab2_txtSocialCreditCode2.Text;
            newSub.Address1 = tab2_txtAddress1.Text;
            newSub.Address2 = tab2_txtAddress2.Text;
            newSub.ZipCode1 = tab2_txtZipCode1.Text;
            newSub.ZipCode2 = tab2_txtZipCode2.Text;
            newSub.LegalRepresentative1 = tab2_txtLegalRepresentative1.Text;
            newSub.LegalRepresentative2 = tab2_txtLegalRepresentative2.Text;
            newSub.EntrustedAgent1 = tab2_txtEntrustedAgent1.Text;
            newSub.EntrustedAgent2 = tab2_txtEntrustedAgent2.Text;
            newSub.Telephone1 = tab2_txtTelephone1.Text;
            newSub.Telephone2 = tab2_txtTelephone2.Text;
            newSub.Fax1 = tab2_txtFax1.Text;
            newSub.Fax2 = tab2_txtFax2.Text;
            newSub.Email1 = tab2_txtEmail1.Text;
            newSub.Email2 = tab2_txtEmail2.Text;
            newSub.Bank1 = tab2_txtBank1.Text;
            newSub.Bank2 = tab2_txtBank2.Text;
            newSub.Account1 = tab2_txtAccount1.Text;
            newSub.Account2 = tab2_txtAccount2.Text;

            var sub = BLL.SubcontractAgreementService.GetSubcontractAgreementById(subcontractAgreementId);
            if (sub!=null)
            {
                newSub.SubcontractAgreementId = subcontractAgreementId;
                 BLL.SubcontractAgreementService.UpdateSubcontractAgreement(newSub);
            }       
            else
            {
              
                     newSub.SubcontractAgreementId = subcontractAgreementId;
                    BLL.SubcontractAgreementService.AddSubcontractAgreement(newSub);
               
            }
            ShowNotify("保存成功！", MessageBoxIcon.Success);
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
        }
        #endregion
    }
}