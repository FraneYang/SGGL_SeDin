﻿using BLL;
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
        public string ContractReviewId
        {
            get
            {
                return (string)ViewState["ContractReviewId"];
            }
            set
            {
                ViewState["ContractReviewId"] = value;
            }
        }
 

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ContractReviewId = Request.Params["ContractReviewId"];
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                #region 会签下拉框
                UserService.InitUserUnitIdDropDownList(DropConstructionManager, Const.UnitId_SEDIN, false);//施工经理
                UserService.InitUserUnitIdDropDownList(DropPurchasingManager, Const.UnitId_SEDIN, false);//采购经理
                UserService.InitUserUnitIdDropDownList(DropHSSEManager, Const.UnitId_SEDIN, false);//HSE经理
                UserService.InitUserUnitIdDropDownList(DropControlManager, Const.UnitId_SEDIN, false);  //控制经理
                UserService.InitUserUnitIdDropDownList(DropQAManager, Const.UnitId_SEDIN, false);  //质量经理
                UserService.InitUserUnitIdDropDownList(DropFinancialManager, Const.UnitId_SEDIN, false);  //财务经理
                UserService.InitUserUnitIdDropDownList(DropProjectManager, Const.UnitId_SEDIN, false);  //项目经理
                UserService.InitUserUnitIdDropDownList(DropProjectManager2, Const.UnitId_SEDIN, false);  //项目经理


                BLL.UserService.InitUserRoleIdUnitIdDropDownList(this.dropCountersign_Construction, this.CurrUser.UnitId, Const.SGContractManageEngineer, false);  ///施工管理部合同评审人员
                BLL.UserService.InitUserRoleIdUnitIdDropDownList(DropCountersign_Law, this.CurrUser.UnitId, Const.Countersign_Law, false);    ///法律合规部合同评审人员
                BLL.UserService.InitUserRoleIdUnitIdDropDownList(DropCountersign_Law2, this.CurrUser.UnitId, Const.Countersign_Law, false);    ///法律合规部合同评审人员

                #endregion
                #region 签订评审下拉框
                ///绑定施工管理部正副主任
                BLL.UserService.InitUserRoleIdUnitIdDropDownList(dropApproval_Construction, CurrUser.UnitId, Const.ConstructionMinister, Const.ConstructionViceMinister, false);
                BLL.UserService.InitUserRoleIdUnitIdDropDownList(dropApproval_Law, this.CurrUser.UnitId, Const.dropApproval_Law, false);   ///法律合规部主任
                BLL.UserService.InitUserRoleIdUnitIdDropDownList(DropGeneralAccountant, this.CurrUser.UnitId, Const.GeneralAccountant, false); //总会计师
                BLL.UserService.InitUserRoleIdUnitIdDropDownList(DropGeneralManager, this.CurrUser.UnitId, Const.GeneralManager, false);  //总经理
                BLL.UserService.InitUserRoleIdUnitIdDropDownList(DropDeputyGeneralManager, this.CurrUser.UnitId, Const.DeputyGeneralManager, false); //分管副总经理
                BLL.UserService.InitUserRoleIdUnitIdDropDownList(DropChairman, this.CurrUser.UnitId, Const.Chairman, false);//董事长
                #endregion
                BindGrid();
                BindFrom();
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
                                     WHEN '3' THEN '施工劳务分包合同'
                                     WHEN '4' THEN '试车服务合同'
                                     WHEN '5' THEN '租赁合同' END) AS ContractType,
                                    Con.Remarks,
                                    Con.EPCCode,
                                    Act.ProjectShortName,
                                    Pro.ProjectCode,
                                    Pro.ProjectName,
                                    Dep.DepartName,
                                    U.UserName AS AgentName"
                           + @" FROM PHTGL_Contract AS Con"
                           + @"  left join PHTGL_ActionPlanFormation as Act on Act.EPCCode=Con.EPCCode"
                           + @" LEFT JOIN Base_Project AS Pro ON Pro.ProjectId = Con.ProjectId"
                           + @" LEFT JOIN Base_Depart AS Dep ON Dep.DepartId = Con.DepartId"
                           + @" LEFT JOIN Sys_User AS U ON U.UserId = Con.Agent WHERE 1=1  AND Con.ApproveState = @ContractCreat_Complete ";
            List<SqlParameter> listStr = new List<SqlParameter>();
            listStr.Add(new SqlParameter("@ContractCreat_Complete", Const.ContractCreat_Complete));

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
        private void BindFrom()
        {
            var newmodel = PHTGL_ContractReviewService.GetPHTGL_ContractReviewById(ContractReviewId);
            var _Contract = ContractService.GetContractById(newmodel.ContractId);
            if (_Contract.ContractAmount >= 50000000)
            {
                DropGeneralAccountant.Hidden = false;
                DropGeneralManager.Hidden = false;
                DropChairman.Hidden = false;
            }
            if (_Contract.ContractAmount >= 20000000)
            {
                DropGeneralAccountant.Hidden = false;
                DropGeneralManager.Hidden = false;
            }

            drpProjectId.Value = Convert.ToString(newmodel.ContractId);
            dropCountersign_Construction.SelectedValue = Convert.ToString(newmodel.Countersign_Construction);
            DropCountersign_Law.SelectedValue = Convert.ToString(newmodel.Countersign_Law);
            DropCountersign_Law2.SelectedValue = Convert.ToString(newmodel.Countersign_Law2);
            dropApproval_Construction.SelectedValue = Convert.ToString(newmodel.Approval_Construction);
            dropApproval_Law.SelectedValue = Convert.ToString(newmodel.Approval_Law);
            DropConstructionManager.SelectedValue = Convert.ToString(newmodel.Countersign_ConstructionManager);
            DropHSSEManager.SelectedValue = Convert.ToString(newmodel.Countersign_HSSEManager);
            DropQAManager.SelectedValue = Convert.ToString(newmodel.Countersign_QAManager);
            DropPurchasingManager.SelectedValue = Convert.ToString(newmodel.Countersign_PurchasingManager);
            DropControlManager.SelectedValue = Convert.ToString(newmodel.Countersign_ControlManager);
            DropFinancialManager.SelectedValue = Convert.ToString(newmodel.Countersign_FinancialManager);
            DropProjectManager.SelectedValue = Convert.ToString(newmodel.Approval_SubProjectManager);
            DropProjectManager.SelectedValue = Convert.ToString(newmodel.Approval_ProjectManager);
            DropDeputyGeneralManager.SelectedValue = Convert.ToString(newmodel.Approval_DeputyGeneralManager);
            DropGeneralAccountant.SelectedValue = Convert.ToString(newmodel.Approval_GeneralAccountant);
            DropGeneralManager.SelectedValue = Convert.ToString(newmodel.Approval_GeneralManager);
            DropChairman.SelectedValue = Convert.ToString(newmodel.Approval_Chairman);
            DropProjectManager_SelectedIndexChanged(null, null);
            Model.PHTGL_Contract table = BLL.ContractService.GetContractById(this.drpProjectId.Value);
            txtContractNum.Text = BLL.ContractService.GetContractByProjectId(table.ProjectId).ContractNum;
        }

        private bool DropIsNull(Control c)
        {
            bool IsOk = true;
            //遍历控件
            //myDictionary.Clear();
            foreach (Control childControl in c.Controls)
            {
                if (childControl is DropDownList)
                {
                    DropDownList tb = (DropDownList)childControl;
                    if (tb.SelectedValue==Const._Null)
                    {
                        IsOk = false;
                        ShowNotify("请选择要审批的"+tb.Label, MessageBoxIcon.Warning);
                        return IsOk;
                    }
                }
 
            }
            return IsOk;

        }

        private void save()
        {
           
            Model.PHTGL_ContractReview newmodel = new Model.PHTGL_ContractReview();
            newmodel.ContractId = drpProjectId.Value;
            //newmodel.SetSubReviewId = DropBidCode.SelectedValue;
            newmodel.DocumentNumber = txtContractNum.Text;
            newmodel.State = Const.ContractCreating;
            newmodel.Countersign_Construction = dropCountersign_Construction.SelectedValue;
            newmodel.Countersign_Law = DropCountersign_Law.SelectedValue;
            newmodel.Countersign_Law2 = DropCountersign_Law2.SelectedValue;
            newmodel.Approval_Construction = dropApproval_Construction.SelectedValue;
            newmodel.Approval_Law = dropApproval_Law.SelectedValue;
            newmodel.Countersign_ConstructionManager = DropConstructionManager.SelectedValue;
            newmodel.Countersign_HSSEManager = DropHSSEManager.SelectedValue;
            newmodel.Countersign_QAManager = DropQAManager.SelectedValue;
            newmodel.Countersign_PurchasingManager = DropPurchasingManager.SelectedValue;
            newmodel.Countersign_ControlManager = DropControlManager.SelectedValue;
            newmodel.Countersign_FinancialManager = DropFinancialManager.SelectedValue;
            newmodel.Approval_SubProjectManager = DropProjectManager.SelectedValue;
            newmodel.Approval_ProjectManager = DropProjectManager.SelectedValue;
            newmodel.Approval_DeputyGeneralManager = DropDeputyGeneralManager.SelectedValue;
            newmodel.Approval_GeneralAccountant = DropGeneralAccountant.SelectedValue;
            newmodel.Approval_GeneralManager = DropGeneralManager.SelectedValue;
            newmodel.Approval_Chairman = DropChairman.SelectedValue;
            newmodel.CreateUser = this.CurrUser.UserId;
            if (ContractReviewId==null)
            {
                newmodel.ContractReviewId = SQLHelper.GetNewID(typeof(Model.PHTGL_ContractReview));
                ContractReviewId = newmodel.ContractReviewId;
                BLL.PHTGL_ContractReviewService.AddPHTGL_ContractReview(newmodel);

            }
            else
            {
                newmodel.ContractReviewId = ContractReviewId;
                BLL.PHTGL_ContractReviewService.UpdatePHTGL_ContractReview(newmodel);

            }

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (drpProjectId.Value == null)
            {
                ShowNotify("请选择要审批的合同", MessageBoxIcon.Warning);
                return;

            }
            if (!DropIsNull(SimpleForm1))
            {
                return;
            }
            save();

            ShowNotify("保存成功！", MessageBoxIcon.Success);

        }


        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (drpProjectId.Value == null)
            {
                ShowNotify("请选择要审批的合同", MessageBoxIcon.Warning);
                return;

            }
            if (!DropIsNull(SimpleForm1))
            {
                return;
            }
            save();

             Model.PHTGL_Contract table = BLL.ContractService.GetContractById(this.drpProjectId.Value);

            var Countersignermodel = PHTGL_ContractReviewService.GetApproveManModels__Countersigner(ContractReviewId);
            for (int i = 0; i < Countersignermodel.Count; i++)
            {
                Model.PHTGL_Approve _Approve = new Model.PHTGL_Approve();
                _Approve.ApproveId = SQLHelper.GetNewID(typeof(Model.PHTGL_Approve));
                _Approve.ContractId = ContractReviewId;
                _Approve.ApproveMan = Countersignermodel[i].userid;
                _Approve.ApproveDate = "";
                _Approve.State = 0;
                _Approve.IsAgree = 0;
                _Approve.ApproveIdea = "";
                _Approve.ApproveType = Countersignermodel[i].Rolename;
                _Approve.IsPushOa = 0;
                _Approve.ApproveForm = PHTGL_ApproveService.ContractReview;

                BLL.PHTGL_ApproveService.AddPHTGL_Approve(_Approve);
            }

             ChangeState(Const.Contract_countersign);

            OAWebSevice.Pushoa();
            ShowNotify("提交成功！", MessageBoxIcon.Success);
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());

        }

        /// <summary>
        /// 改变审批流状态 
        /// </summary>
        /// <param name="state"></param>
        private void ChangeState(int state)
        {
            var _Att = BLL.ContractService.GetContractById(drpProjectId.Value);
            if (_Att != null)
            {
                _Att.ApproveState = state;
                ContractService.UpdateContract(_Att);
            }
            var table = BLL.PHTGL_ContractReviewService.GetPHTGL_ContractReviewById(ContractReviewId);
            if (table != null)
            {
                table.State = state;
                PHTGL_ContractReviewService.UpdatePHTGL_ContractReview(table);
            }
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
                this.DropConstructionManager.SelectedValue = BLL.ProjectService.GetRoleID(table.ProjectId, BLL.Const.ConstructionManager);
                //安全经理（HSE）
                this.DropHSSEManager.SelectedValue = BLL.ProjectService.GetRoleID(table.ProjectId, BLL.Const.HSSEManager);
                //质量经理
                this.DropQAManager.SelectedValue = BLL.ProjectService.GetRoleID(table.ProjectId, BLL.Const.QAManager);
                //采购经理
                this.DropPurchasingManager.SelectedValue = BLL.ProjectService.GetRoleID(table.ProjectId, BLL.Const.PurchasingManager);
                //控制经理
                this.DropControlManager.SelectedValue = BLL.ProjectService.GetRoleID(table.ProjectId, BLL.Const.ControlManager);
                //财务经理
                this.DropFinancialManager.SelectedValue = BLL.ProjectService.GetRoleID(table.ProjectId, BLL.Const.FinancialManager);
                //项目经理
                this.DropProjectManager.SelectedValue = BLL.ProjectService.GetRoleID(table.ProjectId, BLL.Const.ProjectManager);
                 
                #endregion
                #region 本部角色
                //总会计师
                this.DropGeneralAccountant.SelectedValue = BLL.ProjectService.GetOfficeRoleID(UnitId, BLL.Const.GeneralAccountant);
                //董事长
                this.DropChairman.SelectedValue = BLL.ProjectService.GetOfficeRoleID(UnitId, BLL.Const.Chairman);
                //总经理
                this.DropGeneralManager.SelectedValue = BLL.ProjectService.GetOfficeRoleID(UnitId, BLL.Const.GeneralManager);
                //分管副总经理
                this.DropDeputyGeneralManager.SelectedValue = BLL.ProjectService.GetOfficeRoleID(UnitId, BLL.Const.DeputyGeneralManager);
                #endregion
 
            }
            
        }
        #endregion

        protected void DropProjectManager_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropProjectManager2.SelectedValue = DropProjectManager.SelectedValue;
        }
    }
}