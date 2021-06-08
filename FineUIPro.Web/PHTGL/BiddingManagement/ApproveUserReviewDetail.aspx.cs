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
            ApproveUserReviewID = Request.Params["ApproveUserReviewID"];
            EndApproveType = 4;
            this.btnClose.OnClientClick = ActiveWindow.GetHideRefreshReference();

            //获取招标实施计划基本信息
            BLL.ProjectService.InitAllProjectDropDownList(drpProjectId, true);

            var _Bid = BLL.PHTGL_BidApproveUserReviewService.GetPHTGL_BidApproveUserReviewById(ApproveUserReviewID);
            var BidDoc = BLL.PHTGL_BidDocumentsReviewService.GetPHTGL_BidDocumentsReviewById(_Bid.BidDocumentsReviewId);
            //获取审批人字典
            Dic_ApproveMan = PHTGL_BidApproveUserReviewService.Get_DicApproveman(ApproveUserReviewID);
            //获取当前登录人审批信息
            pHTGL_Approve = BLL.PHTGL_ApproveService.GetPHTGL_ApproveByUserId(ApproveUserReviewID, this.CurrUser.UserId);
            txtProjectName.Text = _Bid.BidProject;
            drpProjectId.SelectedValue = _Bid.ProjectId;
            txtBidDocumentsCode.Text = BidDoc.BidDocumentsCode; ;
           
             this.txtCreateUser.Text = BLL.UserService.GetUserNameByUserId(_Bid.CreateUser);
            if (pHTGL_Approve != null)
            {
                txtApproveType.Text = pHTGL_Approve.ApproveType;
                BindGrid();
                if (pHTGL_Approve.State == 1)
                {
                    btnSave.Hidden = true;
                }

            }
            else if (this.CurrUser.UserId == Const.sysglyId || this.CurrUser.UserId == Const.hfnbdId)
            {
                txtApproveType.Text = "管理员";
                BindGrid();
                btnSave.Hidden = true;
            }
            else if (_Bid.CreateUser == this.CurrUser.UserId)
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
                                          ,U.UserName as ApproveUserName
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

        //protected void btnAttachUrl_Click(object sender, EventArgs e)
        //{
        //    PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/BidDocumentsAttachUrl&menuId={1}", this.BidDocumentsReviewId, BLL.Const.BidDocumentsReviewIdMenuid)));

        //}


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