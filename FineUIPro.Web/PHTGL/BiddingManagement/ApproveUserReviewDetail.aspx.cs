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
    public partial class ApproveUserReviewDetail : PageBase
    {
        #region  定义属性
        /// <summary>
        /// 合同ID
        /// </summary>
        public string ApproveUserReviewID
        {
            get
            {
                return (string)ViewState["ApproveUserReviewID"];
            }
            set
            {
                ViewState["ApproveUserReviewID"] = value;
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
            //定义变量列表
            ApproveUserReviewID = Request.Params["ApproveUserReviewID"];
 
            //获取招标实施计划基本信息
            var _Bid = BLL.PHTGL_BidApproveUserReviewService.GetPHTGL_BidApproveUserReviewById(ApproveUserReviewID);
            var BidDoc = BLL.PHTGL_BidDocumentsReviewService.GetPHTGL_BidDocumentsReviewById(_Bid.BidDocumentsReviewId);
            //获取审批人字典
             ApproveManModels = PHTGL_BidApproveUserReviewService.GetApproveManModels(ApproveUserReviewID);
             EndApproveType = ApproveManModels.Count;

             //获取当前登录人审批信息
             pHTGL_Approve = BLL.PHTGL_ApproveService.GetPHTGL_ApproveByUserId(ApproveUserReviewID, this.CurrUser.UserId);
             //文本框赋值
             txtBidProject.Text = _Bid.BidProject;
             txtProjectName.Text = PHTGL_ActionPlanFormationService.GetPHTGL_ActionPlanFormationById(BidDoc.ActionPlanID).ProjectShortName;
              txtBidDocumentsCode.Text = BidDoc.BidDocumentsCode; ;
             this.txtCreateUser.Text = BLL.UserService.GetUserNameByUserId(_Bid.CreateUser);

            if (PHTGL_ApproveService.IsApproveMan(ApproveUserReviewID, this.CurrUser.UserId))
            {
                BindGrid();

            }
            else if (this.CurrUser.UserId == Const.sysglyId || this.CurrUser.UserId == Const.hfnbdId)
            {
                BindGrid();

            }
            else if (_Bid.CreateUser == this.CurrUser.UserId)
            {
                BindGrid();
            }

            if (pHTGL_Approve != null)
            {
                btnAgree.Enabled = true;
                btnDisgree.Enabled = true;
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
                             + @"   left join Sys_User AS U ON U.UserId = App.ApproveMan WHERE 1=1   and App.IsAgree <>0 and app.ContractId= @ContractId order by convert(datetime ,App.ApproveDate)  ";
            List<SqlParameter> listStr = new List<SqlParameter>();
            listStr.Add(new SqlParameter("@ContractId", ApproveUserReviewID));

            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            Grid1.RecordCount = tb.Rows.Count;
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();
            BindGrid2();
        }
        private void BindGrid2()
        {
            string strSql = @"   SELECT    APP.ID
                                          ,APP.ApproveUserReviewID
                                          ,APP.ApproveUserName
                                          ,APP.ApproveUserSpecial
                                          ,APP.ApproveUserUnit
                                          ,APP.Remarks"
                          + @" FROM PHTGL_BidApproveUserReview_Sch1 AS APP "
                          + @" LEFT JOIN Sys_User AS U ON U.UserId =APP.ApproveUserName  "
                          + @"where 1=1 AND ApproveUserReviewID = @ApproveUserReviewID ";
            List<SqlParameter> listStr = new List<SqlParameter>();
             if (string.IsNullOrEmpty(ApproveUserReviewID))
            {
                listStr.Add(new SqlParameter("@ApproveUserReviewID", ""));

            }
            else
            {
                listStr.Add(new SqlParameter("@ApproveUserReviewID", ApproveUserReviewID));

            }
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            Grid2.RecordCount = tb.Rows.Count;
            var table = this.GetPagedDataTable(Grid2, tb);
            Grid2.DataSource = table;
            Grid2.DataBind();
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


        protected void btnAgree_Click(object sender, EventArgs e)
        {
            ApprovemanSave(true);
            OAWebSevice.Pushoa();
            OAWebSevice.DoneRequest(pHTGL_Approve.ApproveId);
        }

        protected void btnDisgree_Click(object sender, EventArgs e)
        {
            ApprovemanSave(false);
            OAWebSevice.DoneRequest(pHTGL_Approve.ApproveId);
            OAWebSevice.Pushoa_Creater(pHTGL_Approve.ApproveId);

        }


        void ApprovemanSave(bool IsAgree)
        {
            pHTGL_Approve.ApproveDate = Funs.GetNewDateTimeOrNow("").ToString();
            pHTGL_Approve.State = 1;
            pHTGL_Approve.IsAgree = IsAgree ? 2 : 1;
            pHTGL_Approve.ApproveIdea = txtApproveIdea.Text;
            BLL.PHTGL_ApproveService.UpdatePHTGL_Approve(pHTGL_Approve);

            int thisApproveTypeNumber = ApproveManModels.Find(e => e.Rolename == pHTGL_Approve.ApproveType).Number;
            int nextApproveType = thisApproveTypeNumber + 1;

            if (IsAgree)
            {
                if (thisApproveTypeNumber < EndApproveType)
                {

                    Model.PHTGL_Approve _Approve = new Model.PHTGL_Approve();
                    _Approve.ContractId = pHTGL_Approve.ContractId;
                    _Approve.ApproveMan = ApproveManModels.Find(e => e.Number == nextApproveType).userid;
                    _Approve.ApproveDate = "";
                    _Approve.State = 0;
                    _Approve.IsAgree = 0;
                    _Approve.ApproveIdea = "";
                    _Approve.ApproveType = ApproveManModels.Find(e => e.Number == nextApproveType).Rolename;
                    _Approve.IsPushOa = 0;
                    _Approve.ApproveForm = PHTGL_ApproveService.ApproveUserReview;

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
                    Model.PHTGL_SetSubReview _SetSubReview = new Model.PHTGL_SetSubReview();
                    _SetSubReview.SetSubReviewID = SQLHelper.GetNewID(typeof(Model.PHTGL_SetSubReview));
                    var BidUser = BLL.PHTGL_BidApproveUserReviewService.GetPHTGL_BidApproveUserReviewById(ApproveUserReviewID);
                    _SetSubReview.ApproveUserReviewID = ApproveUserReviewID;
                    _SetSubReview.ActionPlanID = BidUser.ActionPlanID;
                    _SetSubReview.State = Const.ContractCreating;
                    _SetSubReview.Type = 0;
                    PHTGL_SetSubReviewService.AddPHTGL_SetSubReview(_SetSubReview);

                    ChangeState(Const.ContractReview_Complete);
                }
            }
            else
            {
                ChangeState(Const.ContractReview_Refuse);
            }


            ShowNotify("保存成功！", MessageBoxIcon.Success);
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());


        }


        /// <summary>
        /// 改变审批流状态 
        /// </summary>
        /// <param name="state"></param>
        private void ChangeState(int state)
        {
            var _Bid = PHTGL_BidApproveUserReviewService.GetPHTGL_BidApproveUserReviewById(ApproveUserReviewID);
            _Bid.State = state;
            PHTGL_BidApproveUserReviewService.UpdatePHTGL_BidApproveUserReview(_Bid);
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