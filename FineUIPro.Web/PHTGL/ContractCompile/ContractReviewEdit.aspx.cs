using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FineUIPro.Web.PHTGL.ContractCompile
{
    public partial class ContractReviewEdit : PageBase
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                #region 会签下拉框
                //BLL.UserService.InitUserProjectIdRoleIdDropDownList(this.dropCountersign_Construction, this.CurrUser.LoginProjectId, Const.SGContractManageEngineer, true);
                //BLL.UserService.InitUserProjectIdRoleIdDropDownList(this.dropCountersign_Law, this.CurrUser.LoginProjectId, Const.Countersign_Law, true);
                BLL.UserService.InitUserRoleIdUnitIdDropDownList(this.dropCountersign_Construction, this.CurrUser.UnitId, Const.SGContractManageEngineer, true);  ///施工管理部合同评审人员
                BLL.UserService.InitUserRoleIdUnitIdDropDownList(this.dropCountersign_Law, this.CurrUser.UnitId, Const.Countersign_Law, true);    ///法律合规部合同评审人员

                #endregion
                #region 签订评审下拉框
                ///绑定施工管理部正副主任
                dropApproval_Construction.DataValueField = "UserId";
                dropApproval_Construction.DataTextField = "UserName";
                var model1 = BLL.UserService.GetUserListByRoleIDAndUnitId(CurrUser.UnitId, Const.ConstructionMinister);
                var model2 = BLL.UserService.GetUserListByRoleIDAndUnitId(CurrUser.UnitId, Const.ConstructionViceMinister);
                var model3 = model1.Concat(model2).ToList();
                dropApproval_Construction.DataSource = model3;
                dropApproval_Construction.DataBind();
                Funs.FineUIPleaseSelect(dropApproval_Construction);   
                ///法律合规部主任
                BLL.UserService.InitUserRoleIdUnitIdDropDownList(this.dropApproval_Law, this.CurrUser.UnitId, Const.dropApproval_Law, true);
                //  BLL.UserService.InitUserProjectIdRoleIdDropDownList(this.dropApproval_Law, this.CurrUser.LoginProjectId, Const.dropApproval_Law, true);

                #endregion
                BindGrid();

            }



        }

        /// <summary>
        /// 数据绑定
        /// </summary>
        private void BindGrid()
        {
            string strSql = @"SELECT Con.ContractId, 
                                    Con.ProjectId, 
                                    Con.ContractName, 
                                    Con.ContractNum, 
                                    Con.Parties, 
                                    Con.Currency, 
                                    Con.ContractAmount, 
                                    Con.DepartId, 
                                    Con.Agent, 
                                    (CASE Con.ContractType WHEN '1' THEN '施工总承包分包合同'
                                     WHEN '2' THEN '施工专业分包合同'
                                     WHEN '3' THEN '上官红劳务分包合同'
                                     WHEN '4' THEN '试车服务合同'
                                     WHEN '5' THEN '租赁合同' END) AS ContractType,
                                    Con.Remarks,
                                    Pro.ProjectCode,
                                    Pro.ProjectName,
                                    Dep.DepartName,
                                    U.UserName AS AgentName"
                           + @" FROM PHTGL_Contract AS Con"
                           + @" LEFT JOIN Base_Project AS Pro ON Pro.ProjectId = Con.ProjectId"
                           + @" LEFT JOIN Base_Depart AS Dep ON Dep.DepartId = Con.DepartId"
                           + @" LEFT JOIN Sys_User AS U ON U.UserId = Con.Agent WHERE 1=1  AND Con.ApproveState = 1 ";
            List<SqlParameter> listStr = new List<SqlParameter>();

            if (!(this.CurrUser.UserId == Const.sysglyId))
            {
                strSql += " and Con.ProjectId =@ProjectId";

                listStr.Add(new SqlParameter("@ProjectId", this.CurrUser.LoginProjectId));
            }
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            Grid1.RecordCount = tb.Rows.Count;
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();
        }


        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (drpProjectId.Value ==null)
            {
                ShowNotify("请选择要审批的合同", MessageBoxIcon.Warning);
                return;

            }
            if ( dropCountersign_Construction.SelectedValue==Const._Null)
            {
                ShowNotify("请选择会签评审中施工管理部人员", MessageBoxIcon.Warning);
                return;

            }
            if ( dropCountersign_Law.SelectedValue == Const._Null)
            {
                ShowNotify("请选择会签评审中法律合规部人员", MessageBoxIcon.Warning);
                return;
            }
            if (dropApproval_Construction.SelectedValue == Const._Null)
            {
                ShowNotify("请选择签订评审中施工管理部人员", MessageBoxIcon.Warning);
                return;
            }
            if ( dropApproval_Law.SelectedValue == Const._Null)
            {
                ShowNotify("请选择签订评审中法律合规部人员", MessageBoxIcon.Warning);
                return;
            }
            //1 是会签2 是审批 3是成功 4是失败
            Model.PHTGL_ContractReview newmodel = new Model.PHTGL_ContractReview();
            newmodel.ContractReviewId = SQLHelper.GetNewID(typeof(Model.PHTGL_ContractReview));
            newmodel.ContractId = drpProjectId.Value;
            newmodel.DocumentNumber = txtContractNum.Text;
            newmodel.State = 1;
            newmodel.Countersign_Construction = dropCountersign_Construction.SelectedValue;
            newmodel.Countersign_Law = dropCountersign_Law.SelectedValue;
            newmodel.Approval_Construction = dropApproval_Construction.SelectedValue;
            newmodel.Approval_Law = dropApproval_Law.SelectedValue;
            BLL.PHTGL_ContractReviewService.AddPHTGL_ContractReview(newmodel);

            Model.PHTGL_Contract table = BLL.ContractService.GetContractById(this.drpProjectId.Value);

            //创建会签人员信息
            foreach (KeyValuePair<int, string> kvp in PHTGL_ContractReviewService.Countersigner)
            {
                Model.PHTGL_Approve _Approve = new Model.PHTGL_Approve();
                _Approve.ApproveId = SQLHelper.GetNewID(typeof(Model.PHTGL_Approve));
                _Approve.ContractId = drpProjectId.Value;
                _Approve.ApproveMan = BLL.ProjectService.GetRoleID(table.ProjectId, kvp.Value);
                _Approve.ApproveDate = "";
                _Approve.State = 0;
                _Approve.IsAgree = 0;
                _Approve.ApproveIdea = "";
                _Approve.ApproveType = kvp.Key.ToString();

                BLL.PHTGL_ApproveService.AddPHTGL_Approve(_Approve);
            }
            var _Att = BLL.ContractService.GetContractById(drpProjectId.Value);
            if (_Att != null)
            {
                _Att.ApproveState = 2;
                ContractService.UpdateContract(_Att);
            }

            ShowNotify("保存成功！", MessageBoxIcon.Success);
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());


        }
        #region DropDownList下拉选择事件
        /// <summary>
        /// 选择项目Id获取会签评审人员信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DropDownBox1_TextChanged(object sender, EventArgs e)
        {
            if (this.drpProjectId.Value != BLL.Const._Null)
            {
                Model.PHTGL_Contract table = BLL.ContractService.GetContractById(this.drpProjectId.Value);
                txtContractNum.Text = BLL.ContractService.GetContractByProjectId(table.ProjectId).ContractNum;
                string UnitId = ProjectService.GetProjectByProjectId(table.ProjectId).UnitId;
                #region 项目角色
                //施工经理
                this.txtConstructionManager.Text = BLL.ProjectService.GetRoleName(table.ProjectId, BLL.Const.ConstructionManager);
                //安全经理（HSE）
                this.txtHSSEManager.Text = BLL.ProjectService.GetRoleName(table.ProjectId, BLL.Const.HSSEManager);
                //质量经理
                this.txtQAManager.Text = BLL.ProjectService.GetRoleName(table.ProjectId, BLL.Const.QAManager);
                //采购经理
                this.txtPurchasingManager.Text = BLL.ProjectService.GetRoleName(table.ProjectId, BLL.Const.PurchasingManager);
                //控制经理
                this.txtControlManager.Text = BLL.ProjectService.GetRoleName(table.ProjectId, BLL.Const.ControlManager);
                //财务经理
                this.txtFinancialManager.Text = BLL.ProjectService.GetRoleName(table.ProjectId, BLL.Const.FinancialManager);
                //项目经理
                this.txtProjectManager.Text = BLL.ProjectService.GetRoleName(table.ProjectId, BLL.Const.ProjectManager);

                #endregion
                #region 本部角色
                //总会计师
                this.txtGeneralAccountant.Text = BLL.ProjectService.GetOfficeRoleName(UnitId, BLL.Const.GeneralAccountant);
                //董事长
                this.txtChairman.Text = BLL.ProjectService.GetOfficeRoleName(UnitId, BLL.Const.Chairman);
                //总经理
                this.txtGeneralManager.Text = BLL.ProjectService.GetOfficeRoleName(UnitId, BLL.Const.GeneralManager);
                //分管副总经理
                this.txtDeputyGeneralManager.Text = BLL.ProjectService.GetOfficeRoleName(UnitId, BLL.Const.DeputyGeneralManager);
                #endregion




            }
            else
            {
                this.txtConstructionManager.Text = string.Empty;
                this.txtHSSEManager.Text = string.Empty;
                this.txtQAManager.Text = string.Empty;
                this.txtPurchasingManager.Text = string.Empty;
                this.txtControlManager.Text = string.Empty;
                this.txtFinancialManager.Text = string.Empty;
                this.txtProjectManager.Text = string.Empty;
                this.txtGeneralAccountant.Text = string.Empty;
                this.txtChairman.Text = string.Empty;
                this.txtGeneralManager.Text = string.Empty;
                this.txtDeputyGeneralManager.Text = string.Empty;

                //  this.txtProjectName.Text = string.Empty;
            }
        }
        #endregion
    }
}