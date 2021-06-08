using Aspose.Words;
using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FineUIPro.Web.PHTGL.BiddingManagement
{
    public partial class ActionPlanReviewDetail : PageBase
    {
        #region  定义属性
        /// <summary>
        /// 合同ID
        /// </summary>
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
        /// <summary>
        /// 最末的审批节点
        /// </summary>
        public int EndApproveType
        {
            get
            {
                return (int)ViewState["EndApproveType"];
            }
            set
            {
                ViewState["EndApproveType"] = value;
            }
        }
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
        public Model.PHTGL_Approve pHTGL_Approve
        {
            get
            {
                return (Model.PHTGL_Approve)Session["pHTGL_Approve"];
            }
            set
            {
                Session["pHTGL_Approve"] = value;
            }
        }
        #endregion
 
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ActionPlanReviewId = Request.Params["ActionPlanReviewId"];
                EndApproveType = 5;
                this.btnClose.OnClientClick = ActiveWindow.GetHideRefreshReference();

                //获取招标实施计划基本信息
                var _ActionPlanReview = BLL.PHTGL_ActionPlanReviewService.GetPHTGL_ActionPlanReviewById(ActionPlanReviewId);
                var _ActionPlanFormation = BLL.PHTGL_ActionPlanFormationService.GetPHTGL_ActionPlanFormationById(_ActionPlanReview.ActionPlanID);


                //获取审批人字典
                Dic_ApproveMan = PHTGL_ActionPlanReviewService.Get_DicApproveman(_ActionPlanFormation.ProjectID, ActionPlanReviewId);
                //获取当前登录人审批信息
                pHTGL_Approve = BLL.PHTGL_ApproveService.GetPHTGL_ApproveByUserId(ActionPlanReviewId, this.CurrUser.UserId);

                this.txtProjectCode.Text = BLL.ProjectService.GetProjectCodeByProjectId(_ActionPlanFormation.ProjectID);
                this.txtProjectName.Text = BLL.ProjectService.GetProjectNameByProjectId(_ActionPlanFormation.ProjectID);
                this.txtCreateUser.Text = BLL.UserService.GetUserNameByUserId(_ActionPlanFormation.CreatUser);
                if (pHTGL_Approve != null)
                {
                    txtApproveType.Text = pHTGL_Approve.ApproveType;
                    BindGrid();
                    if (pHTGL_Approve.State == 1)
                    {
                        btnSave.Hidden = true;
                    }
                }
                else if (this.CurrUser.UserId == Const.sysglyId || this.CurrUser.UserId ==Const.hfnbdId)
                {
                    txtApproveType.Text = "管理员";
                    BindGrid();
                    btnSave.Hidden = true;
                }
                else if (_ActionPlanFormation.CreatUser == this.CurrUser.UserId)
                {
                    txtApproveType.Text = "创建者";
                    BindGrid();
                    btnSave.Hidden = true;
                }
                else
                {
                    btnSave.Hidden = true;
                }

            }

        }

        /// <summary>
        /// 数据绑定
        /// </summary>
        private void BindGrid()
        {
            string strSql = @"  select u.UserName as  ApproveMan,
                                       App.ApproveDate,
                                      (CASE App.IsAgree WHEN '1' THEN '不同意'
                                        WHEN '2' THEN '同意' END) AS IsAgree,
                                        App.ApproveIdea,
                                        App.ApproveId,
                                        App.ApproveType
                                       from PHTGL_Approve as App"
                             + @"   left join Sys_User AS U ON U.UserId = App.ApproveMan WHERE 1=1   and App.IsAgree <>0 and app.ContractId= @ContractId";
            List<SqlParameter> listStr = new List<SqlParameter>();
            listStr.Add(new SqlParameter("@ContractId", ActionPlanReviewId));

            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            Grid1.RecordCount = tb.Rows.Count;
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();
        }

        protected void btnAttachUrl_Click(object sender, EventArgs e)
        {
            ActionPlanFormation actionPlanFormation = new ActionPlanFormation();
            actionPlanFormation.Print(ActionPlanReviewId);
         }
        protected void btnLooK_Click(object sender, EventArgs e)
        {
             
            var Act = BLL.PHTGL_ActionPlanReviewService.GetPHTGL_ActionPlanReviewById(ActionPlanReviewId);
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("ActionPlanFormationEdit.aspx?ActionPlanID={0}", Act.ActionPlanID, "编辑 - ")));
        }

        #region 分页 排序
        /// <summary>
        /// 改变索引事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            BindGrid();
        }


        protected void btnSave_Click(object sender, EventArgs e)
        {

            ApprovemanSave();

        }


        /// <summary>
        /// 审批人员保存
        /// </summary>
        void ApprovemanSave()
        {
            pHTGL_Approve.ApproveDate = Funs.GetNewDateTimeOrNow("").ToString();
            if (CBIsAgree.SelectedValueArray.Length > 0)
            {
                pHTGL_Approve.State = 1;
                pHTGL_Approve.IsAgree = Convert.ToInt32(CBIsAgree.SelectedValueArray[0]);
                pHTGL_Approve.ApproveIdea = txtApproveIdea.Text;
                BLL.PHTGL_ApproveService.UpdatePHTGL_Approve(pHTGL_Approve);

                int nextApproveType = Convert.ToInt32(pHTGL_Approve.ApproveType) + 1;

                if (Convert.ToInt32(pHTGL_Approve.ApproveType) < EndApproveType) //判断当前审批节点是否结束
                {
                    if (Convert.ToInt32(CBIsAgree.SelectedValueArray[0]) == 2)  //同意时
                    {
                        Model.PHTGL_Approve _Approve = new Model.PHTGL_Approve();
                      //  _Approve.ApproveId = SQLHelper.GetNewID(typeof(Model.PHTGL_Approve));
                        _Approve.ContractId = pHTGL_Approve.ContractId;
                        _Approve.ApproveMan = Dic_ApproveMan[nextApproveType];
                        _Approve.ApproveDate = "";
                        _Approve.State = 0;
                        _Approve.IsAgree = 0;
                        _Approve.ApproveIdea = "";
                        _Approve.ApproveType = nextApproveType.ToString();
                        _Approve.ApproveForm = Request.Path;

                        var IsExitmodel = PHTGL_ApproveService.GetPHTGL_ApproveByContractIdAndType(_Approve.ContractId, _Approve.ApproveType);
                        if (IsExitmodel == null)
                        {
                            _Approve.ApproveId = SQLHelper.GetNewID(typeof(Model.PHTGL_Approve));
                            BLL.PHTGL_ApproveService.AddPHTGL_Approve(_Approve);

                        }
                        else
                        {
                            _Approve.ApproveId = IsExitmodel.ApproveId;
                            BLL.PHTGL_ApproveService.UpdatePHTGL_Approve(_Approve);

                        }
                        ChangeState(Const.ContractReviewing);
                    }
                    else
                    {
                        ChangeState(Const.ContractReview_Refuse);
                    }

                }
                else
                {
                    var acp = BLL.PHTGL_ActionPlanReviewService.GetPHTGL_ActionPlanReviewById(ActionPlanReviewId);
                    var act = BLL.PHTGL_ActionPlanFormationService.GetPHTGL_ActionPlanFormationById(acp.ActionPlanID);
                    Model.PHTGL_BidDocumentsReview model = new Model.PHTGL_BidDocumentsReview();
                    model.BidDocumentsReviewId = SQLHelper.GetNewID(typeof(Model.PHTGL_BidDocumentsReview));
                    model.ActionPlanReviewId = ActionPlanReviewId;
                    model.ActionPlanID = acp.ActionPlanID;
                    model.ProjectId = act.ProjectID;
                    model.State = Const.ContractCreating;
                    PHTGL_BidDocumentsReviewService.AddPHTGL_BidDocumentsReview(model);
                    ChangeState(Const.ContractReview_Complete);
                }

                ShowNotify("保存成功！", MessageBoxIcon.Success);
                PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());

            }
            else
            {
                ShowNotify("请确认是否同意！", MessageBoxIcon.Warning);

            }

   
        }

        /// <summary>
        /// 改变审批流状态 
        /// </summary>
        /// <param name="state"></param>
        private void ChangeState(int state)
        {
            var _ActionPlanReview = BLL.PHTGL_ActionPlanReviewService.GetPHTGL_ActionPlanReviewById(ActionPlanReviewId);
            _ActionPlanReview.State = state;
            PHTGL_ActionPlanReviewService.UpdatePHTGL_ActionPlanReview(_ActionPlanReview);

            //var _ActionPlanFormation= BLL.PHTGL_ActionPlanFormationService.GetPHTGL_ActionPlanFormationById(_ActionPlanReview.ActionPlanID);
            //_ActionPlanFormation.State = state;
            //PHTGL_ActionPlanFormationService.UpdatePHTGL_ActionPlanFormation(_ActionPlanFormation);
        }



        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_Sort(object sender, FineUIPro.GridSortEventArgs e)
        {
            BindGrid();
        }
        #endregion
    }
}