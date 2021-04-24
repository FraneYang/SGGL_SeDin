using BLL;
using System;

namespace FineUIPro.Web.PHTGL.ContractCompile
{
    public partial class ContractAgreementEdit : PageBase
    {
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
                string contractId = Request.Params["ContractId"];
                string subcontractAgreementId = Request.Params["SubcontractAgreementId"];
                if (!string.IsNullOrEmpty(subcontractAgreementId))
                {
                    Model.PHTGL_SubcontractAgreement sub = BLL.SubcontractAgreementService.GetSubcontractAgreementById(subcontractAgreementId);
                    if (sub != null)
                    {
                        this.txtGeneralContractor.Text = sub.GeneralContractor;
                        this.txtSubConstruction.Text = sub.SubConstruction;
                        this.txtContents.Text = sub.Contents;
                        this.txtContractProject.Text = sub.ContractProject;
                        this.txtContractProjectOwner.Text = sub.ContractProjectOwner;
                        this.txtSubProject.Text = sub.SubProject;
                        this.txtSubProjectAddress.Text = sub.SubProjectAddress;
                        this.txtFundingSources.Text = sub.FundingSources;
                        this.txtSubProjectContractScope.Text = sub.SubProjectContractScope;
                        this.txtSubProjectContent.Text = sub.SubProjectContent;
                        this.txtPlanStartYear.Text = sub.PlanStartYear.HasValue ? sub.PlanStartYear.ToString() : "";
                        this.txtPlanStartMonth.Text = sub.PlanStartMonth.HasValue ? sub.PlanStartMonth.ToString() : "";
                        this.txtPlanStartDay.Text = sub.PlanStartDay.HasValue ? sub.PlanStartDay.ToString() : "";
                        this.txtPlanEndYear.Text = sub.PlanEndYear.HasValue ? sub.PlanEndYear.ToString() : "";
                        this.txtPlanEndMonth.Text = sub.PlanEndMonth.HasValue ? sub.PlanEndMonth.ToString() : "";
                        this.txtPlanEndDay.Text = sub.PlanEndDay.HasValue ? sub.PlanEndDay.ToString() : "";
                        this.txtLimit.Text = sub.Limit.HasValue ? sub.Limit.ToString() : "";
                        this.txtQualityStandards.Text = sub.QualityStandards;
                        this.txtHSEManageStandards.Text = sub.HSEManageStandards;
                        this.txtSubcontractPriceForm.Text = sub.SubcontractPriceForm;
                        this.txtContractPriceCapital.Text = sub.ContractPriceCapital;
                        this.txtContractPriceCNY.Text = sub.ContractPriceCNY.HasValue ? sub.ContractPriceCNY.ToString() : "";
                        this.txtContractPriceDesc.Text = sub.ContractPriceDesc;
                        this.txtInvoice.Text = sub.Invoice;
                        this.txtLaw.Text = sub.Law;
                        this.txtSignedYear.Text = sub.SignedYear.HasValue ? sub.SignedYear.ToString() : "";
                        this.txtSignedMonth.Text = sub.SignedMonth.HasValue ? sub.SignedMonth.ToString() : "";
                        this.txtSignedAddress.Text = sub.SignedAddress;
                        this.txtAgreementNum.Text = sub.AgreementNum.HasValue ? sub.AgreementNum.ToString() : "";
                        this.txtGeneralContractorNum.Text = sub.GeneralContractorNum.HasValue ? sub.GeneralContractorNum.ToString() : "";
                        this.txtSubContractorNum.Text = sub.SubContractorNum.HasValue ? sub.SubContractorNum.ToString() : "";
                        this.txtSocialCreditCode1.Text = sub.SocialCreditCode1;
                        this.txtSocialCreditCode2.Text = sub.SocialCreditCode2;
                        this.txtAddress1.Text = sub.Address1;
                        this.txtAddress2.Text = sub.Address2;
                        this.txtZipCode1.Text = sub.ZipCode1;
                        this.txtZipCode2.Text = sub.ZipCode2;
                        this.txtLegalRepresentative1.Text = sub.LegalRepresentative1;
                        this.txtLegalRepresentative2.Text = sub.LegalRepresentative2;
                        this.txtEntrustedAgent1.Text = sub.EntrustedAgent1;
                        this.txtEntrustedAgent2.Text = sub.EntrustedAgent2;
                        this.txtTelephone1.Text = sub.Telephone1;
                        this.txtTelephone2.Text = sub.Telephone2;
                        this.txtFax1.Text = sub.Fax1;
                        this.txtFax2.Text = sub.Fax2;
                        this.txtEmail1.Text = sub.Email1;
                        this.txtEmail2.Text = sub.Email2;
                        this.txtBank1.Text = sub.Bank1;
                        this.txtBank2.Text = sub.Bank2;
                        this.txtAccount1.Text = sub.Account1;
                        this.txtAccount2.Text = sub.Account2;
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
            newSub.GeneralContractor = txtGeneralContractor.Text;
            newSub.SubConstruction = txtSubConstruction.Text;
            newSub.Contents = txtContents.Text;
            newSub.ContractProject = txtContractProject.Text;
            newSub.ContractProjectOwner = txtContractProjectOwner.Text;
            newSub.SubProject = txtSubProject.Text;
            newSub.SubProjectAddress = txtSubProjectAddress.Text;
            newSub.FundingSources = txtFundingSources.Text;
            newSub.SubProjectContractScope = txtSubProjectContractScope.Text;
            newSub.SubProjectContent = txtSubProjectContent.Text;
            newSub.PlanStartYear = Funs.GetNewInt(txtPlanStartYear.Text);
            newSub.PlanStartMonth = Funs.GetNewInt(txtPlanStartMonth.Text);
            newSub.PlanStartDay = Funs.GetNewInt(txtPlanStartDay.Text);
            newSub.PlanEndYear = Funs.GetNewInt(txtPlanEndYear.Text);
            newSub.PlanEndMonth = Funs.GetNewInt(txtPlanEndMonth.Text);
            newSub.PlanEndDay = Funs.GetNewInt(txtPlanEndDay.Text);
            newSub.Limit = Funs.GetNewInt(txtLimit.Text);
            newSub.QualityStandards = txtQualityStandards.Text;
            newSub.HSEManageStandards = txtHSEManageStandards.Text;
            newSub.SubcontractPriceForm = txtSubcontractPriceForm.Text;
            newSub.ContractPriceCapital = txtContractPriceCapital.Text;
            newSub.ContractPriceCNY = Funs.GetNewDecimal(txtContractPriceCNY.Text);
            newSub.ContractPriceDesc = txtContractPriceDesc.Text;
            newSub.Invoice = txtInvoice.Text;
            newSub.Law = txtLaw.Text;
            newSub.SignedYear = Funs.GetNewInt(txtSignedYear.Text);
            newSub.SignedMonth = Funs.GetNewInt(txtSignedMonth.Text);
            newSub.SignedAddress = txtSignedAddress.Text;
            newSub.AgreementNum = Funs.GetNewInt(txtAgreementNum.Text);
            newSub.GeneralContractorNum = Funs.GetNewInt(txtGeneralContractorNum.Text);
            newSub.SubContractorNum = Funs.GetNewInt(txtSubContractorNum.Text);
            newSub.SocialCreditCode1 = txtSocialCreditCode1.Text;
            newSub.SocialCreditCode2 = txtSocialCreditCode2.Text;
            newSub.Address1 = txtAddress1.Text;
            newSub.Address2 = txtAddress2.Text;
            newSub.ZipCode1 = txtZipCode1.Text;
            newSub.ZipCode2 = txtZipCode2.Text;
            newSub.LegalRepresentative1 = txtLegalRepresentative1.Text;
            newSub.LegalRepresentative2 = txtLegalRepresentative2.Text;
            newSub.EntrustedAgent1 = txtEntrustedAgent1.Text;
            newSub.EntrustedAgent2 = txtEntrustedAgent2.Text;
            newSub.Telephone1 = txtTelephone1.Text;
            newSub.Telephone2 = txtTelephone2.Text;
            newSub.Fax1 = txtFax1.Text;
            newSub.Fax2 = txtFax2.Text;
            newSub.Email1 = txtEmail1.Text;
            newSub.Email2 = txtEmail2.Text;
            newSub.Bank1 = txtBank1.Text;
            newSub.Bank2 = txtBank2.Text;
            newSub.Account1 = txtAccount1.Text;
            newSub.Account2 = txtAccount2.Text;

            string contractId = Request.Params["ContractId"];
            string subcontractAgreementId = Request.Params["SubcontractAgreementId"];
            if (!string.IsNullOrEmpty(subcontractAgreementId))
            {
                newSub.SubcontractAgreementId = subcontractAgreementId;
                BLL.SubcontractAgreementService.UpdateSubcontractAgreement(newSub);
            }
            else
            {
                if (!string.IsNullOrEmpty(contractId))
                {
                    newSub.ContractId = contractId;
                    newSub.SubcontractAgreementId = SQLHelper.GetNewID(typeof(Model.PHTGL_SubcontractAgreement));
                    BLL.SubcontractAgreementService.AddSubcontractAgreement(newSub);
                }
            }
            ShowNotify("保存成功！", MessageBoxIcon.Success);
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
        }
        #endregion
    }
}