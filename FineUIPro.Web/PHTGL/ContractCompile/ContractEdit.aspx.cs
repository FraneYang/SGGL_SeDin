using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FineUIPro.Web.PHTGL.ContractCompile
{
    public partial class ContractEdit : PageBase
    {
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
                //总承包合同编号
                BLL.ProjectService.InitAllProjectCodeDropDownList(this.drpProjectId, true);
                //币种
                this.drpCurrency.DataTextField = "Text";
                this.drpCurrency.DataValueField = "Value";
                this.drpCurrency.DataSource = BLL.DropListService.GetCurrency();
                this.drpCurrency.DataBind();
                Funs.FineUIPleaseSelect(this.drpCurrency);
                //主办部门
                BLL.DepartService.InitDepartDropDownList(this.drpDepartId, true);
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
                            this.txtProjectName.Text = BLL.ProjectService.GetProjectNameByProjectId(contract.ProjectId);
                        }
                        this.txtContractName.Text = contract.ContractName;
                        this.txtContractNum.Text = contract.ContractNum;
                        this.txtParties.Text = contract.Parties;
                        if (!string.IsNullOrEmpty(contract.Currency))
                        {
                            this.drpCurrency.SelectedValue = contract.Currency;
                        }
                        this.txtContractAmount.Text =  contract.ContractAmount.ToString();
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
                        this.txtRemark.Text = contract.Remarks;
                    }
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string contractId = Request.Params["ContractId"];
            Model.PHTGL_Contract newContract = new Model.PHTGL_Contract();
            if (this.drpProjectId.SelectedValue!=BLL.Const._Null)
            {
                newContract.ProjectId = this.drpProjectId.SelectedValue;
            }
            newContract.ContractName = this.txtContractName.Text.Trim();
            newContract.ContractNum = this.txtContractNum.Text.Trim();
            newContract.Parties = this.txtParties.Text.Trim();
            if (this.drpCurrency.SelectedValue!=BLL.Const._Null)
            {
                newContract.Currency = this.drpCurrency.SelectedValue;
            }
            newContract.ContractAmount = Funs.GetNewDecimal(this.txtContractAmount.Text.Trim());
            if (this.drpDepartId.SelectedValue != BLL.Const._Null)
            {
                newContract.DepartId = this.drpDepartId.SelectedValue;
            }
            if (this.drpAgent.SelectedValue!=BLL.Const._Null)
            {
                newContract.Agent = this.drpAgent.SelectedValue;
            }
            if (this.drpContractType.SelectedValue!=BLL.Const._Null)
            {
                newContract.ContractType = this.drpContractType.SelectedValue;
            }
            newContract.Remarks = this.txtRemark.Text.Trim();
            if (!string.IsNullOrEmpty(contractId))
            {
                newContract.ContractId = contractId;
                BLL.ContractService.UpdateContract(newContract);
                ShowNotify("修改成功！", MessageBoxIcon.Success);
            }
            else
            {
                newContract.ContractId = SQLHelper.GetNewID(typeof(Model.PHTGL_Contract));
                BLL.ContractService.AddContract(newContract);
                ShowNotify("保存成功！", MessageBoxIcon.Success);
            }
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
        }

        #region DropDownList下拉选择事件
        /// <summary>
        /// 选择项目Id获取项目名称
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpProjectId_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.drpProjectId.SelectedValue != BLL.Const._Null)
            {
                this.txtProjectName.Text = BLL.ProjectService.GetProjectNameByProjectId(this.drpProjectId.SelectedValue);
            }
            else
            {
                this.txtProjectName.Text = string.Empty;
            }
        }
        #endregion
    }
}