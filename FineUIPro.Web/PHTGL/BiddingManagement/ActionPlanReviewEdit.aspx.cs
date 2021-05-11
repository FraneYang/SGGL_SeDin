using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FineUIPro.Web.PHTGL.BiddingManagement
{
    public partial class ActionPlanReviewEdit : PageBase
    {
        /// <summary>
        /// 审批人字典
        /// </summary>
        public Dictionary<int, string> Dic_ApproveMan
        {
            get
            {
                return (Dictionary<int, string>)ViewState["Dic_ApproveMan"];
            }
            set
            {
                ViewState["Dic_ApproveMan"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                ///绑定施工管理部正副主任
                Approval_Construction.DataValueField = "UserId";
                Approval_Construction.DataTextField = "UserName";
                //var model1 = BLL.UserService.GetUserListByProjectIdAndRoleId(CurrUser.LoginProjectId, Const.ConstructionMinister);
                //var model2 = BLL.UserService.GetUserListByProjectIdAndRoleId(CurrUser.LoginProjectId, Const.ConstructionViceMinister);
                var model1 = BLL.UserService.GetUserListByRoleIDAndUnitId(CurrUser.UnitId, Const.ConstructionMinister);
                var model2 = BLL.UserService.GetUserListByRoleIDAndUnitId(CurrUser.UnitId, Const.ConstructionViceMinister);
                var model3 = model1.Concat(model2).ToList();
                Approval_Construction.DataSource = model3;
                Approval_Construction.DataBind();
                Funs.FineUIPleaseSelect(Approval_Construction);
 
                BindGrid();

            }



        }

        /// <summary>
        /// 数据绑定
        /// </summary>
        private void BindGrid()
        {
            string strSql = @"SELECT  Act.ActionPlanID
                                  ,Pro.ProjectName as Name
                                  ,Pro.ProjectCode
                                  ,U.UserName as CreatUser
                                  ,Act.CreateTime
                                  ,Act.ProjectID
                                  ,Act.ProjectName
                                  ,Act.Unit
                                  ,Act.ConstructionSite "
                          + @" FROM PHTGL_ActionPlanFormation  AS Act "
                          + @" LEFT JOIN Sys_User AS U ON U.UserId = Act.CreatUser  "
                          + @" LEFT JOIN Base_Project AS Pro ON Pro.ProjectId = Act.ProjectID  WHERE 1=1 and Act.State=1";
            List<SqlParameter> listStr = new List<SqlParameter>();

            if (!(this.CurrUser.UserId == Const.sysglyId))
            {
                strSql += " and  Act.ProjectID =@ProjectId";

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
            if (string.IsNullOrEmpty( drpProjectId.Value))
            {
                ShowNotify("请选择合同", MessageBoxIcon.Warning);
                return;
            }
            if (Approval_Construction.SelectedValue==Const._Null)
            {
                ShowNotify("请选择施工管理部人员", MessageBoxIcon.Warning);
                return;

            }
 
            //0 是审批中 1是成功 2是失败
            Model.PHTGL_ActionPlanReview newmodel = new Model.PHTGL_ActionPlanReview();
            newmodel.ActionPlanReviewId = SQLHelper.GetNewID(typeof(Model.PHTGL_ActionPlanReview));
            newmodel.ActionPlanID = drpProjectId.Value;
            newmodel.Approval_Construction= Approval_Construction.SelectedValue;
            newmodel.State =0;
            newmodel.CreateUser =this.CurrUser.UserId;
            BLL.PHTGL_ActionPlanReviewService.AddPHTGL_ActionPlanReview(newmodel);
            var _Act = BLL.PHTGL_ActionPlanFormationService.GetPHTGL_ActionPlanFormationById(newmodel.ActionPlanID);

            Dic_ApproveMan = PHTGL_ActionPlanReviewService.Get_DicApproveman(_Act.ProjectID, newmodel.ActionPlanReviewId);


            //创建第一节点审批信息
            Model.PHTGL_Approve _Approve = new Model.PHTGL_Approve();
            _Approve.ApproveId = SQLHelper.GetNewID(typeof(Model.PHTGL_Approve));
            _Approve.ContractId = newmodel.ActionPlanID;
            _Approve.ApproveMan = Dic_ApproveMan[1];
            _Approve.ApproveDate = "";
            _Approve.State = 0;
            _Approve.IsAgree = 0;
            _Approve.ApproveIdea = "";
            _Approve.ApproveType = "1";

            BLL.PHTGL_ApproveService.AddPHTGL_Approve(_Approve);
 
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
                Model.PHTGL_ActionPlanFormation table = BLL.PHTGL_ActionPlanFormationService.GetPHTGL_ActionPlanFormationById(this.drpProjectId.Value);
                string UnitId = ProjectService.GetProjectByProjectId(table.ProjectID).UnitId;

                //施工经理
                this.txtConstructionManager.Text = BLL.ProjectService.GetRoleName(table.ProjectID, BLL.Const.ConstructionManager);
   
                //项目经理
                this.txtProjectManager.Text = BLL.ProjectService.GetRoleName(table.ProjectID, BLL.Const.ProjectManager);
 
                //分管副总经理
                this.txtDeputyGeneralManager.Text = BLL.ProjectService.GetOfficeRoleName(UnitId, BLL.Const.DeputyGeneralManager);


            }
            else
            {
                this.txtConstructionManager.Text = string.Empty;
                this.txtProjectManager.Text = string.Empty;
                this.txtDeputyGeneralManager.Text = string.Empty;

                //  this.txtProjectName.Text = string.Empty;
            }
        }
        #endregion
    }
}