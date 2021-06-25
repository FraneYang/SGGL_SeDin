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
        #region  定义属性
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
        public List<ApproveManModel> ApproveManModels
        {
            get
            {
                return (List<ApproveManModel>)Session["ApproveManModels"];
            }
            set
            {
                Session["ApproveManModels"] = value;
            }
        }
        public string ActionPlanReviewId
        {
            get
            {
                return (string)ViewState["ActionPlanReviewId"];
            }
            set
            {
                ViewState["ActionPlanReviewId"] = value;
            }
        }
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                ActionPlanReviewId = Request.Params["ActionPlanReviewId"];
                #region 绑定下拉列表
                /// 绑定施工管理部正副主任
                Approval_Construction.DataValueField = "UserId";
                Approval_Construction.DataTextField = "UserName";
                var model1 = BLL.UserService.GetUserListByRoleIDAndUnitId(CurrUser.UnitId, Const.ConstructionMinister);
                var model2 = BLL.UserService.GetUserListByRoleIDAndUnitId(CurrUser.UnitId, Const.ConstructionViceMinister);
                var model3 = model1.Concat(model2).ToList();
                Approval_Construction.DataSource = model3;
                Approval_Construction.DataBind();
                Funs.FineUIPleaseSelect(Approval_Construction);

                UserService.InitUserUnitIdDropDownList(DropConstructionManager, Const.UnitId_SEDIN, true);
                UserService.InitUserUnitIdDropDownList(DropPreliminaryMan, Const.UnitId_SEDIN, true);
                UserService.InitUserUnitIdDropDownList(DropProjectManager, Const.UnitId_SEDIN, true);
                UserService.InitUserUnitIdDropDownList(DropDeputyGeneralManager, Const.UnitId_SEDIN, true);
                #endregion
                BindGrid();
                BindForm();
                var newmodel = PHTGL_ActionPlanReviewService.GetPHTGL_ActionPlanReviewById(ActionPlanReviewId);
                if (newmodel != null)
                {
                    if (newmodel.State > Const.ContractCreat_Complete)
                    {
                        this.btnSave.Hidden = true;
                        this.btnSubmit.Hidden = true;
                    }

                }


            }
 
        }

        private void BindForm()
        {
            var act = PHTGL_ActionPlanReviewService.GetPHTGL_ActionPlanReviewById(ActionPlanReviewId);
            drpProjectId.Value = Convert.ToString(act.ActionPlanID);
            Approval_Construction.SelectedValue = Convert.ToString(act.Approval_Construction);
            DropConstructionManager.SelectedValue = Convert.ToString(act.ConstructionManager);
            DropPreliminaryMan.SelectedValue = Convert.ToString(act.PreliminaryMan);
            DropProjectManager.SelectedValue = Convert.ToString(act.ProjectManager);
            DropDeputyGeneralManager.SelectedValue = Convert.ToString(act.DeputyGeneralManager);
        }
            /// <summary>
            /// 数据绑定
            /// </summary>
            private void BindGrid()
        {
            string strSql = @"SELECT  Act.ActionPlanID
                                  ,Act.ActionPlanCode
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
                          + @" LEFT JOIN Base_Project AS Pro ON Pro.ProjectId = Act.ProjectID  WHERE 1=1 and Act.State=@ContractCreat_Complete";
            List<SqlParameter> listStr = new List<SqlParameter>();
            listStr.Add(new SqlParameter("@ContractCreat_Complete", Const.ContractCreat_Complete));

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

        private bool Save()
        {
            bool isOk = false ;

            if (string.IsNullOrEmpty(drpProjectId.Value))
            {
                ShowNotify("请选择合同", MessageBoxIcon.Warning);
                return isOk;
            }
            if (Approval_Construction.SelectedValue == Const._Null)
            {
                ShowNotify("请选择施工管理部人员", MessageBoxIcon.Warning);
                return isOk;

            }

            //0 是审批中 1是成功 2是失败
            Model.PHTGL_ActionPlanReview newmodel = new Model.PHTGL_ActionPlanReview();
            newmodel.ActionPlanReviewId = ActionPlanReviewId;
            newmodel.ActionPlanID = drpProjectId.Value;
            newmodel.Approval_Construction = Approval_Construction.SelectedValue;
            newmodel.State = Const.ContractCreat_Complete;
            newmodel.CreateUser = this.CurrUser.UserId;
            newmodel.ConstructionManager = DropConstructionManager.SelectedValue;
            newmodel.PreliminaryMan = DropPreliminaryMan.SelectedValue;
            newmodel.ProjectManager = DropProjectManager.SelectedValue;
            newmodel.DeputyGeneralManager = DropDeputyGeneralManager.SelectedValue;
            BLL.PHTGL_ActionPlanReviewService.UpdatePHTGL_ActionPlanReview(newmodel);
            isOk = true;
            return isOk;
          
        }
        private void CreateApprove()
        {
            var newmodel = PHTGL_ActionPlanReviewService.GetPHTGL_ActionPlanReviewById(ActionPlanReviewId);
            var _Act = BLL.PHTGL_ActionPlanFormationService.GetPHTGL_ActionPlanFormationById(newmodel.ActionPlanID);
            Dic_ApproveMan = PHTGL_ActionPlanReviewService.Get_DicApproveman(_Act.ProjectID, newmodel.ActionPlanReviewId);
            ApproveManModels = PHTGL_ActionPlanReviewService.GetApproveManModels(_Act.ProjectID, newmodel.ActionPlanReviewId);
            //创建第一节点审批信息
            Model.PHTGL_Approve _Approve = new Model.PHTGL_Approve();
            _Approve.ApproveId = SQLHelper.GetNewID(typeof(Model.PHTGL_Approve));
            _Approve.ContractId = newmodel.ActionPlanReviewId;
            _Approve.ApproveMan = ApproveManModels.Find(e=>e.Number==1).userid;
            _Approve.ApproveDate = "";
            _Approve.State = 0;
            _Approve.IsAgree = 0;
            _Approve.ApproveIdea = "";
            _Approve.ApproveType = ApproveManModels.Find(e => e.Number == 1).Rolename;
            _Approve.IsPushOa = 0;
            _Approve.ApproveForm = PHTGL_ApproveService.ActionPlanReview;

            BLL.PHTGL_ApproveService.AddPHTGL_Approve(_Approve);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Save())
            {
                ShowNotify("保存成功！", MessageBoxIcon.Success);
                PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
            }
            
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (Save())
            {
                CreateApprove();
                var model1 = PHTGL_ActionPlanReviewService.GetPHTGL_ActionPlanReviewById(ActionPlanReviewId);
                model1.State = Const.ContractReviewing;
                PHTGL_ActionPlanReviewService.UpdatePHTGL_ActionPlanReview(model1);

                 //model2.State= Const.ContractReviewing;
                //PHTGL_ActionPlanFormationService.UpdatePHTGL_ActionPlanFormation(model2);
                OAWebSevice.Pushoa();
                PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());


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
                Model.PHTGL_ActionPlanFormation table = BLL.PHTGL_ActionPlanFormationService.GetPHTGL_ActionPlanFormationById(this.drpProjectId.Value);
                string UnitId = ProjectService.GetProjectByProjectId(table.ProjectID).UnitId;

                //施工经理
                this.DropConstructionManager.SelectedValue = BLL.ProjectService.GetRoleID(table.ProjectID, BLL.Const.ConstructionManager);
   
                //项目经理
                this.DropProjectManager.SelectedValue = BLL.ProjectService.GetRoleID(table.ProjectID, BLL.Const.ProjectManager);
 
                //分管副总经理
                this.DropDeputyGeneralManager.SelectedValue = BLL.ProjectService.GetRoleID(UnitId, BLL.Const.DeputyGeneralManager);
 
            }
           
        }
        #endregion
    }
}