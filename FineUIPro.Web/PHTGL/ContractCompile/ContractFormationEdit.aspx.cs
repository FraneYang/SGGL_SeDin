using BLL;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FineUIPro.Web.PHTGL.ContractCompile
{
    public partial class ContractFormationEdit : PageBase
    {
        #region 定义属性
        //    public Dictionary<string, string> myDictionary = new Dictionary<string, string>();

        public Dictionary<string, string> myDictionary
        {
            get
            {
                return (Dictionary<string, string>) ViewState["myDictionary"];
            }
            set
            {
                ViewState["myDictionary"] = value;
            }
        }
        /// <summary>
        /// 合同基本信息主键
        /// </summary>
        public string ContractId
        {
            get
            {
                return (string)ViewState["ContractId"];
            }
            set
            {
                ViewState["ContractId"] = value;
            }
        }
        /// <summary>
        /// 合同协议书主键
        /// </summary>
        public string SubcontractAgreementId
        {
            get
            {
                return (string)ViewState["SubcontractAgreementId"];
            }
            set
            {
                ViewState["SubcontractAgreementId"] = value;
            }
        }
        /// <summary>
        /// 专用协议主键
        /// </summary>
        public string SpecialTermsConditionsId
        {
            get
            {
                return (string)ViewState["SpecialTermsConditionsId"];
            }
            set
            {
                ViewState["SpecialTermsConditionsId"] = value;
            }
        }
        
        public bool IsCreate
        {
            get
            {
                return (bool)ViewState["IsCreate"];
            }
            set
            {
                ViewState["IsCreate"] = value;
            }
        }
        #endregion

        #region 加载
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.btnClose.OnClientClick = ActiveWindow.GetHideRefreshReference();
                myDictionary= new Dictionary<string, string>();
                ContractId = Request.Params["ContractId"];
                IsCreate = true; 
                 if (!string .IsNullOrEmpty(Request.Params["ContractId"]))
                {
                   IsCreate = false;
                 }
                
                BindingTab1();
                BindingTab2();
                BindingTab4();

                if (!string.IsNullOrEmpty(Request.Params["ContractId"]))
                {
                    Model.PHTGL_Contract _Contract = BLL.ContractService.GetContractById(ContractId);
                    if (_Contract.ApproveState >=Const.ContractCreat_Complete)
                    {
                        btnSave_Tab1.Hidden = true;
                        btnSave.Hidden = true;
                        btnSave_Tab4.Hidden = true;
                    }
 
                }
            

            }
        }

        #endregion


        #region 基本信息

        void BindingTab1()
        {
            //  this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
            //总承包合同编号
            BLL.ProjectService.InitAllProjectCodeDropDownList(this.drpProjectId, true);
            this.drpProjectId.SelectedValue = this.CurrUser.LoginProjectId;
         //   this.drpProjectId.Enabled = false;
             drpProjectId_SelectedIndexChanged(null, null);
            //币种
            this.drpCurrency.DataTextField = "Text";
            this.drpCurrency.DataValueField = "Value";
            this.drpCurrency.DataSource = BLL.DropListService.GetCurrency();
            this.drpCurrency.DataBind();
            Funs.FineUIPleaseSelect(this.drpCurrency);
            //主办部门
            BLL.DepartService.InitDepartDropDownList(this.drpDepartId, true);
            this.drpDepartId.SelectedValue = Const.Depart_constructionId;  //默认为施工管理部id
            //经办人
            BLL.UserService.InitUserDropDownList(this.drpAgent, this.CurrUser.LoginProjectId, true);
            //合同类型
            this.drpContractType.DataTextField = "Text";
            this.drpContractType.DataValueField = "Value";
            this.drpContractType.DataSource = BLL.DropListService.GetContractType();
            this.drpContractType.DataBind();
            Funs.FineUIPleaseSelect(this.drpContractType);

            string contractId = Request.Params["ContractId"];
            if (!string.IsNullOrEmpty(contractId))
            {
                Model.PHTGL_Contract contract = BLL.ContractService.GetContractById(contractId);
                if (contract != null)
                {
                    if (!string.IsNullOrEmpty(contract.ProjectId))
                    {
                        this.drpProjectId.SelectedValue = contract.ProjectId;
                        this.tab1_txtProjectName.Text = BLL.ProjectService.GetProjectNameByProjectId(contract.ProjectId);
                    }
                    this.tab1_txtContractName.Text = contract.ContractName;
                    this.tab1_txtContractNum.Text = contract.ContractNum;
                    this.tab1_txtParties.Text = contract.Parties;
                    if (!string.IsNullOrEmpty(contract.Currency))
                    {
                        this.drpCurrency.SelectedValue = contract.Currency;
                    }
                    this.tab1_txtContractAmount.Text = contract.ContractAmount.HasValue ? contract.ContractAmount.ToString() : "";
                    if (!string.IsNullOrEmpty(contract.DepartId))
                    {
                        this.drpDepartId.SelectedValue = contract.DepartId;
                    }
                    if (!string.IsNullOrEmpty(contract.Agent))
                    {
                        this.drpAgent.SelectedValue = contract.Agent;
                    }
                    if (!string.IsNullOrEmpty(contract.ContractType))
                    {
                        this.drpContractType.SelectedValue = contract.ContractType;
                    }
                    this.tab1_txtRemark.Text = contract.Remarks;
                }
            }

        }
        

            protected void btnSave_Tab1_Click(object sender, EventArgs e)
            {
             Model.PHTGL_Contract newContract = new Model.PHTGL_Contract();
            if (this.drpProjectId.SelectedValue != BLL.Const._Null)
            {
                newContract.ProjectId = this.drpProjectId.SelectedValue;
            }
            newContract.ContractName = this.tab1_txtContractName.Text.Trim();
            newContract.ContractNum = this.tab1_txtContractNum.Text.Trim();
            newContract.Parties = this.tab1_txtParties.Text.Trim();
            if (this.drpCurrency.SelectedValue != BLL.Const._Null)
            {
                newContract.Currency = this.drpCurrency.SelectedValue;
            }
            newContract.ContractAmount = Funs.GetNewDecimal(this.tab1_txtContractAmount.Text.Trim());
            if (this.drpDepartId.SelectedValue != BLL.Const._Null)
            {
                newContract.DepartId = this.drpDepartId.SelectedValue;
            }
            if (this.drpAgent.SelectedValue != BLL.Const._Null)
            {
                newContract.Agent = this.drpAgent.SelectedValue;
            }
            if (this.drpContractType.SelectedValue != BLL.Const._Null)
            {
                newContract.ContractType = this.drpContractType.SelectedValue;
            }
            newContract.Remarks = this.tab1_txtRemark.Text.Trim();
            if (!string.IsNullOrEmpty(ContractId))
            {
                newContract.ContractId = ContractId;
                BLL.ContractService.UpdateContract(newContract);
                ShowNotify("修改成功！", MessageBoxIcon.Success);
            }
            else
            {
                newContract.ContractId = SQLHelper.GetNewID(typeof(Model.PHTGL_Contract));
                newContract.ApproveState = Const.ContractCreating;
                newContract.CreatUser = this.CurrUser.UserId;
                ContractId = newContract.ContractId;
                BLL.ContractService.AddContract(newContract);
                ShowNotify("保存成功！", MessageBoxIcon.Success);
            }
           // PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
        }

 

        protected void drpProjectId_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.drpProjectId.SelectedValue != BLL.Const._Null)
            {
                this.tab1_txtProjectName.Text = BLL.ProjectService.GetProjectNameByProjectId(this.drpProjectId.SelectedValue);
            }
            else
            {
                this.tab1_txtProjectName.Text = string.Empty;
            }
        }

        #endregion


        #region 合同协议书
        void BindingTab2()
        {
            if (!string.IsNullOrEmpty(ContractId))
            {
                Model.PHTGL_SubcontractAgreement sub = BLL.SubcontractAgreementService.GetSubcontractAgreementByContractId(ContractId);
                if (sub != null)
                {
                    this.tab2_txtGeneralContractor.Text = sub.GeneralContractor;
                    this.tab2_txtSubConstruction.Text = sub.SubConstruction;
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
                else
                {
                    sub = BLL.SubcontractAgreementService.GetSubcontractAgreementById("合同协议书模板");
                    if (sub != null)
                    {
                        this.tab2_txtGeneralContractor.Text = sub.GeneralContractor;
                        this.tab2_txtSubConstruction.Text = sub.SubConstruction;
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
            else
            {
                 Model.PHTGL_SubcontractAgreement sub = BLL.SubcontractAgreementService.GetSubcontractAgreementById("合同协议书模板");
                if (sub != null)
                {
                    this.tab2_txtGeneralContractor.Text = sub.GeneralContractor;
                    this.tab2_txtSubConstruction.Text = sub.SubConstruction;
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

        protected void btnSave_Tab_2_Click(object sender, EventArgs e)
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
            if (!IsCreate )  //编辑进来
            {
                var IsExit_sub = BLL.SubcontractAgreementService.GetSubcontractAgreementByContractId(ContractId);
                if (IsExit_sub!=null)
                {
                    newSub.SubcontractAgreementId = IsExit_sub.SubcontractAgreementId;
                    newSub.ContractId = IsExit_sub.ContractId;
                    BLL.SubcontractAgreementService.UpdateSubcontractAgreement(newSub);
                     ShowNotify("保存成功！", MessageBoxIcon.Success);
                }
                else
                {
                    newSub.ContractId = ContractId;
                    newSub.SubcontractAgreementId = SQLHelper.GetNewID(typeof(Model.PHTGL_SubcontractAgreement));
                    BLL.SubcontractAgreementService.AddSubcontractAgreement(newSub);

                }

            }
            else //新建
            {
                if (!string.IsNullOrEmpty(ContractId))  //判断有没有保存基本信息
                {
                    var IsExit_sub = BLL.SubcontractAgreementService.GetSubcontractAgreementByContractId(ContractId);

                    if (IsExit_sub != null)
                    {
                        newSub.SubcontractAgreementId = IsExit_sub.SubcontractAgreementId;
                        newSub.ContractId = IsExit_sub.ContractId;
                         BLL.SubcontractAgreementService.UpdateSubcontractAgreement(newSub);
                    }
                    else
                    {
                        newSub.ContractId = ContractId;
                        newSub.SubcontractAgreementId = SQLHelper.GetNewID(typeof(Model.PHTGL_SubcontractAgreement));
                        BLL.SubcontractAgreementService.AddSubcontractAgreement(newSub);
                    }

                    ShowNotify("保存成功！", MessageBoxIcon.Success);
                 //   PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
                }
                else
                {
                    ShowNotify("请先编制基本信息页，并保存！", MessageBoxIcon.Warning);
                }
            }
 
        }

        #endregion


        #region 通用条款
        #endregion


        #region 专用条款
        void BindingTab4()
        {
             deleteLog();
 
            if (!IsCreate )
            {    
                var model = BLL.PHTGL_SpecialTermsConditionsService.GetSpecialTermsConditionsByContractId(ContractId);
                if (model!=null)
                {
                    SpecialTermsConditionsId = model.SpecialTermsConditionsId;
                    DataGridAttachUrl(SpecialTermsConditionsId);
                    RederDatabase(Form_Tab4, SpecialTermsConditionsId); //从数据库读取数据填充

                }
                else
                {
                    SpecialTermsConditionsId = SQLHelper.GetNewID(typeof(Model.PHTGL_SpecialTermsConditions));
                    saveAtturl(SpecialTermsConditionsId);
                    DataGridAttachUrl(SpecialTermsConditionsId);
                    RederDatabase(Form_Tab4, "专用条款模板"); //从数据库读取数据填充

                }

            }
            else
            {
                 SpecialTermsConditionsId = SQLHelper.GetNewID(typeof(Model.PHTGL_SpecialTermsConditions));
                 saveAtturl(SpecialTermsConditionsId);
                 DataGridAttachUrl(SpecialTermsConditionsId);
                 RederDatabase(Form_Tab4, "专用条款模板"); //从数据库读取数据填充

            }
 
        }
        protected void btnSave_Tab4__Click(object sender, EventArgs e)
        {
            Save(true );
           

        }

        void deleteLog()
        {
            string strsql = @"  delete from  PHTGL_AttachUrl where AttachUrlId in (
                                                                  SELECT Att.AttachUrlId
                                                                  FROM dbo.PHTGL_AttachUrl as Att
                                                                  left join dbo.PHTGL_SpecialTermsConditions as Sp on att.SpecialTermsConditionsId = sp.SpecialTermsConditionsId
                                                                  where sp.SpecialTermsConditionsId is null)"  ;
            DataTable tb = SQLHelper.RunSqlGetTable(strsql);
        }

        void saveAtturl(string SpecialTermsConditionsId)
        {
            var list = BLL.AttachUrlService.GetAttachUrlBySpecialTermsConditionsId("专用条款模板");
          
                for (int i = 0; i < list.Count; i++)
                {
                    Model.PHTGL_AttachUrl _AttachUrl = new PHTGL_AttachUrl();
                    _AttachUrl.AttachUrlId = SQLHelper.GetNewID(typeof(Model.PHTGL_AttachUrl));
                    _AttachUrl.SpecialTermsConditionsId = SpecialTermsConditionsId;
                    _AttachUrl.AttachUrlCode = list[i].AttachUrlCode;
                    _AttachUrl.AttachUrlName = list[i].AttachUrlName;
                    _AttachUrl.IsBuild = list[i].IsBuild;
                    _AttachUrl.IsSelected = list[i].IsSelected;
                    _AttachUrl.SortIndex = list[i].SortIndex;
                    BLL.AttachUrlService.AddPHTGL_AttachUrl(_AttachUrl);
                }
          
        }

        void Save(bool IsEnd)
        {
             if (!IsCreate )
            {
                string contractId = Request.Params["ContractId"];
                var isExit = BLL.PHTGL_SpecialTermsConditionsService.GetSpecialTermsConditionsByContractId(contractId);
                if (isExit != null)
                {
                    SpecialTermsConditionsId = isExit.SpecialTermsConditionsId;
                }

            }
            else
            {
                 if (string .IsNullOrEmpty(ContractId))
                {
                    ShowNotify("请先编制基本信息页，并保存！", MessageBoxIcon.Warning);
                    return;
                }

            }
 
            myDictionary.Clear();
            myDictionary.Add("SpecialTermsConditionsId", SpecialTermsConditionsId);
            myDictionary.Add("ContractId", ContractId);

            SaveTextEmpty(Form_Tab4);  //得到键值对

            DataTable table = GetDataTable(myDictionary);//键值对转DATatable;

            List<PHTGL_SpecialTermsConditions> List_pHTGL_SpecialTermsConditions = TableToEntity<PHTGL_SpecialTermsConditions>(table);

            Model.PHTGL_SpecialTermsConditions pHTGL_SpecialTermsConditions = List_pHTGL_SpecialTermsConditions[0];
            Model.PHTGL_SpecialTermsConditions model = BLL.PHTGL_SpecialTermsConditionsService.GetSpecialTermsConditionsById(SpecialTermsConditionsId);
            if (model != null)
            {
                BLL.PHTGL_SpecialTermsConditionsService.UpdateSpecialTermsConditions(pHTGL_SpecialTermsConditions);
            }
            else
            {
                BLL.PHTGL_SpecialTermsConditionsService.AddSpecialTermsConditions(pHTGL_SpecialTermsConditions);
            }
            if (IsEnd)
            {
                ShowNotify("保存成功！", MessageBoxIcon.Success);
            }
        }

        public static string Gettype(string name)
        {
            if (name.Contains("System.String"))
            {
                return "String";
            }
            if (name.Contains("System.Int32"))
            {
                return "Int32";
            }
            if (name.Contains("System.Decimal"))
            {
                return "Decimal";
            }
            if (name.Contains("System.DateTime"))
            {
                return "DateTime";
            }
            return "";
        }

        private void SaveTextEmpty(Control c)
        {
            //遍历控件
            //myDictionary.Clear();
            foreach (Control childControl in c.Controls)
            {
                if (childControl is TextBox)
                {
                    TextBox tb = (TextBox)childControl;
                    if (!tb.ID.StartsWith("TextBox"))
                    {
                        myDictionary.Add(tb.ID, tb.Text.ToString());
                    }

                    //  tb.Text = "";
                    if (tb.Text.Length > 7)
                    {
                        tb.Width = tb.Text.Length * 16;
                    }
                }
                else if (childControl is TextArea)
                {
                    TextArea textArea = (TextArea)childControl;
                    if (!textArea.ID.StartsWith("TextArea"))
                    {
                        myDictionary.Add(textArea.ID, textArea.Text.ToString());
                    }

                }
                else
                {
                    SaveTextEmpty(childControl);
                }
            }
        }

        private void RederDatabase(Control c,string SpecialTermsConditionsId)
        {
            //遍历控件给控件赋值
            myDictionary.Clear();
            foreach (Control childControl in c.Controls)
            {
                if (childControl is TextBox)
                {
                    TextBox tb = (TextBox)childControl;
                    if (!tb.ID.StartsWith("TextBox"))
                    {
                        tb.Text = getvalue(SpecialTermsConditionsId, tb.ID);
                    }
                    if (tb.Text.Length > 7)
                    {
                        tb.Width = tb.Text.Length * 16;
                    }
                }
                else if (childControl is TextArea)
                {
                    TextArea textArea = (TextArea)childControl;
                    if (!textArea.ID.StartsWith("TextArea"))
                    {
                        textArea.Text = getvalue(SpecialTermsConditionsId, textArea.ID);
                    }
                }
                else
                {
                    RederDatabase(childControl, SpecialTermsConditionsId);
                }
            }
        }

        protected void TextBoxChanged(object sender, EventArgs e)
        {

            TextBox textBox = (TextBox)sender;
            if (textBox.Text.Length > 7)
            {
                textBox.Width = textBox.Text.Length * 16;

            }

            Save(false);


        }


        /// <summary>
        ///  根据主键获取要查询字段的值
        /// </summary>
        /// <param name="SpecialTermsConditionsId">主键</param>
        /// <param name="field"></param>
        /// <returns></returns>
        public string getvalue(string SpecialTermsConditionsId, string field)
        {
            string values = "";

            string sql = "select " + field + " from PHTGL_SpecialTermsConditions where SpecialTermsConditionsId='" + SpecialTermsConditionsId + "'";

            DataTable tb = SQLHelper.RunSqlGetTable(sql);
            if (tb != null && tb.Rows.Count > 0)
            {
                values = tb.Rows[0][field].ToString();

            }
            return values;
        }

        /// <summary>
        /// 集合转DataTable
        /// </summary>
        /// <param name="Dictionary"></param>
        /// <returns></returns>
        private DataTable GetDataTable(Dictionary<string, string> Dictionary)
        {

            DataTable dt = new DataTable();
            foreach (KeyValuePair<string, string> kvp in Dictionary)
            {
                //dt.Rows.Add(kvp.Key, kvp.Value);
                DataColumn dc = new DataColumn(kvp.Key.ToString());
                dt.Columns.Add(dc);

            }
            DataRow dr = dt.NewRow();
            dt.Rows.Add(dr);
            foreach (KeyValuePair<string, string> kvp in Dictionary)
            {
                dt.Rows[0][kvp.Key.ToString()] = kvp.Value;
            }

            return dt;
        }

        /// <summary>
        /// data转实体类
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt"></param>
        /// <returns></returns>
        private static List<T> TableToEntity<T>(DataTable dt) where T : class, new()
        {
            Type type = typeof(T);
            List<T> list = new List<T>();

            foreach (DataRow row in dt.Rows)
            {
                PropertyInfo[] pArray = type.GetProperties();
                T entity = new T();
                foreach (PropertyInfo p in pArray)
                {

                    if (row[p.Name].ToString() == "")
                    {
                        p.SetValue(entity, null, null);
                        continue;
                    }
                    switch (Gettype(p.PropertyType.FullName))
                    {
                        case "String":
                            p.SetValue(entity, row[p.Name].ToString(), null);
                            break;
                        case "Int32":
                            p.SetValue(entity, Int32.Parse(row[p.Name].ToString()), null);
                            break;
                        case "Decimal":
                            p.SetValue(entity, Decimal.Parse(row[p.Name].ToString()), null);
                            break;
                        case "DateTime":
                            p.SetValue(entity, DateTime.Parse(row[p.Name].ToString()), null);
                            break;
                        default:
                            p.SetValue(entity, row[p.Name], null);
                            break;
                    }


                }
                list.Add(entity);
            }
            return list;
        }

        #endregion

        #region 附件
        /// <summary>
        /// Grid绑定
        /// </summary>
        private void DataGridAttachUrl(string SpecialTermsConditionsId)
        {
            string strSql = @"SELECT Att.AttachUrlId, 
                                    Att.AttachUrlCode, 
                                    Att.AttachUrlName, 
                                    Att.IsBuild,
                                    Att.IsSelected, 
                                    Att.SortIndex"
                            + @" FROM PHTGL_AttachUrl AS Att"
                            + @" WHERE 1=1  "
                            + @" and  Att.SpecialTermsConditionsId=@SpecialTermsConditionsId  ORDER BY Att.SortIndex ";
            List<SqlParameter> listStr = new List<SqlParameter>();
            listStr.Add(new SqlParameter("@SpecialTermsConditionsId", SpecialTermsConditionsId));

            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            //Grid1.RecordCount = tb.Rows.Count;
            //var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = tb;
            Grid1.DataBind();
        }

        /// <summary>
        /// grid行绑定前事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_PreRowDataBound(object sender, GridPreRowEventArgs e)
        {
            DataRowView row = e.DataItem as DataRowView;
            CheckBoxField cbIsSelected = Grid1.FindColumn("cbIsSelected") as CheckBoxField;
            bool isSelected = Convert.ToBoolean(row["IsBuild"]);
            if (isSelected == true)
            {
                cbIsSelected.Enabled = false;
            }
            else
            {
                cbIsSelected.Enabled = true;
            }
        }


        /// <summary>
        /// Grid行点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_RowCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "edit")
            {
                CheckBoxField cbIsSelected = Grid1.FindColumn("cbIsSelected") as CheckBoxField;
                if (cbIsSelected.GetCheckedState(e.RowIndex))
                {
                    string id = Grid1.SelectedRowID;
                    var att = BLL.AttachUrlService.GetAttachUrlById(id);
                    if (att != null)
                    {
                        att.IsSelected = true;
                        BLL.AttachUrlService.UpdateAttachUrl(att);
                    }
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("AttachUrl{0}.aspx?AttachUrlId={1}", att.SortIndex,id, "编辑 - ")));
                    //if (id == BLL.Const.AttachUrlId1)
                    //{
                    //    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("AttachUrl1.aspx?AttachUrlId={0}", id, "编辑 - ")));
                    //}
                    //else if (id == BLL.Const.AttachUrlId2)
                    //{
                    //    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("AttachUrl2.aspx?AttachUrlId={0}", id, "编辑 - ")));
                    //}
                    //else if (id == BLL.Const.AttachUrlId3)
                    //{
                    //    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("AttachUrl3.aspx?AttachUrlId={0}", id, "编辑 - ")));
                    //}
                    //else if (id == BLL.Const.AttachUrlId4)
                    //{
                    //    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("AttachUrl4.aspx?AttachUrlId={0}", id, "编辑 - ")));
                    //}
                    //else if (id == BLL.Const.AttachUrlId5)
                    //{
                    //    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("AttachUrl5.aspx?AttachUrlId={0}", id, "编辑 - ")));
                    //}
                    //else if (id == BLL.Const.AttachUrlId6)
                    //{
                    //    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("AttachUrl6.aspx?AttachUrlId={0}", id, "编辑 - ")));
                    //}
                    //else if (id == BLL.Const.AttachUrlId7)
                    //{
                    //    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("AttachUrl7.aspx?AttachUrlId={0}", id, "编辑 - ")));
                    //}
                    //else if (id == BLL.Const.AttachUrlId8)
                    //{
                    //    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("AttachUrl8.aspx?AttachUrlId={0}", id, "编辑 - ")));
                    //}
                    //else if (id == BLL.Const.AttachUrlId9)
                    //{
                    //    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("AttachUrl9.aspx?AttachUrlId={0}", id, "编辑 - ")));
                    //}
                    //else if (id == BLL.Const.AttachUrlId10)
                    //{
                    //    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("AttachUrl10.aspx?AttachUrlId={0}", id, "编辑 - ")));
                    //}
                    //else if (id == BLL.Const.AttachUrlId11)
                    //{
                    //    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("AttachUrl11.aspx?AttachUrlId={0}", id, "编辑 - ")));
                    //}
                    //else if (id == BLL.Const.AttachUrlId12)
                    //{
                    //    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("AttachUrl12.aspx?AttachUrlId={0}", id, "编辑 - ")));
                    //}
                    //else if (id == BLL.Const.AttachUrlId13)
                    //{
                    //    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("AttachUrl13.aspx?AttachUrlId={0}", id, "编辑 - ")));
                    //}
                    //else if (id == BLL.Const.AttachUrlId14)
                    //{
                    //    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("AttachUrl14.aspx?AttachUrlId={0}", id, "编辑 - ")));
                    //}
                    //else if (id == BLL.Const.AttachUrlId15)
                    //{
                    //    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("AttachUrl15.aspx?AttachUrlId={0}", id, "编辑 - ")));
                    //}
                    //else if (id == BLL.Const.AttachUrlId16)
                    //{
                    //    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("AttachUrl16.aspx?AttachUrlId={0}", id, "编辑 - ")));
                    //}
                    //else if (id == BLL.Const.AttachUrlId17)
                    //{
                    //    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("AttachUrl17.aspx?AttachUrlId={0}", id, "编辑 - ")));
                    //}
                    //else if (id == BLL.Const.AttachUrlId18)
                    //{
                    //    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("AttachUrl18.aspx?AttachUrlId={0}", id, "编辑 - ")));
                    //}
                    //else if (id == BLL.Const.AttachUrlId19)
                    //{
                    //    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("AttachUrl19.aspx?AttachUrlId={0}", id, "编辑 - ")));
                    //}
                }
                else
                {
                    Alert.ShowInTop("未选中的项！", MessageBoxIcon.Warning);
                    return;
                }
            }
        }

        #endregion
        protected void btnSubmitForm1_Click(object sender, EventArgs e)
        {
            Model.PHTGL_Contract _Contract = BLL.ContractService.GetContractById(ContractId);
            _Contract.ApproveState = Const.ContractCreat_Complete;
            ContractService.UpdateContract(_Contract);
            ShowNotify("提交成功！", MessageBoxIcon.Success);
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
        }


        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
          //  ShowNotify("窗体被关闭了。参数：" + (String.IsNullOrEmpty(e.CloseArgument) ? "无" : e.CloseArgument));
        }
    }
}