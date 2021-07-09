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
    public partial class SetSubReviewDetail : PageBase
    {
        #region  定义属性
        /// <summary>
        /// 合同ID
        /// </summary>
        public string SetSubReviewID
        {
            get
            {
                return (string)ViewState["SetSubReviewID"];
            }
            set
            {
                ViewState["SetSubReviewID"] = value;
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
            if (!IsPostBack)
            {
                SetSubReviewID = Request.Params["SetSubReviewID"];
                EndApproveType = 4;
 
                 //获取审批人字典
                Dic_ApproveMan = PHTGL_SetSubReviewService.Get_DicApproveman(SetSubReviewID);
                ApproveManModels = PHTGL_SetSubReviewService.GetApproveManModels(SetSubReviewID);
                //获取当前登录人审批信息
                pHTGL_Approve = BLL.PHTGL_ApproveService.GetPHTGL_ApproveByUserId(SetSubReviewID, this.CurrUser.UserId);
                //数据加载
                Bind();
                var _SetSubReview = BLL.PHTGL_SetSubReviewService.GetPHTGL_SetSubReviewById(SetSubReviewID);

                if (PHTGL_ApproveService.IsApproveMan(SetSubReviewID, this.CurrUser.UserId))
                {
                    BindGrid();

                }
                else if (this.CurrUser.UserId == Const.sysglyId || this.CurrUser.UserId == Const.hfnbdId)
                {
                    BindGrid();

                }
                else if (_SetSubReview.CreateUser == this.CurrUser.UserId)
                {
                    BindGrid();
                }

                if (pHTGL_Approve != null)
                {
                    btnAgree.Enabled = true;
                    btnDisgree.Enabled = true;
                }
                if (!BLL.AttachFileService.Getfile(SetSubReviewID, BLL.Const.SetSubReview))
                {
                    btnAttachUrl.Hidden = true;
                 }

            }

        }
        #region 数据绑定

        private void Bind()
        {
            if (!string.IsNullOrEmpty(SetSubReviewID))
            {
                var _SetSubReview = BLL.PHTGL_SetSubReviewService.GetPHTGL_SetSubReviewById(SetSubReviewID);
                var BidUser = BLL.PHTGL_BidApproveUserReviewService.GetPHTGL_BidApproveUserReviewById(_SetSubReview.ApproveUserReviewID);
                var BidDocument = BLL.PHTGL_BidDocumentsReviewService.GetPHTGL_BidDocumentsReviewById(BidUser.BidDocumentsReviewId);
                if (_SetSubReview != null)
                {
                    txtSetSubReviewCode.Text = _SetSubReview.SetSubReviewCode;
                    txtProjectName.Text = PHTGL_ActionPlanFormationService.GetPHTGL_ActionPlanFormationById(BidDocument.ActionPlanID).ProjectShortName;
                    txtBidContent.Text = BidDocument.BidContent;
                    this.txtCreateUser.Text = BLL.UserService.GetUserNameByUserId(_SetSubReview.CreateUser);
                }

            }
        }
        private void BindGrid_MinPrice()
        {
            string strSql = @"       SELECT ID
                                          ,SetSubReviewID
                                          ,Company
                                          ,ReviewResults
                                          ,SortIndex
                                          ,Remarks"
                            + @"     FROM PHTGL_SetSubReview_Sch1 where 1=1 AND SetSubReviewID = @SetSubReviewID ";
            List<SqlParameter> listStr = new List<SqlParameter>();
            if (string.IsNullOrEmpty(SetSubReviewID))
            {
                listStr.Add(new SqlParameter("@SetSubReviewID", ""));

            }
            else
            {
                listStr.Add(new SqlParameter("@SetSubReviewID", SetSubReviewID));

            }
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            Grid_MinPrice.RecordCount = tb.Rows.Count;
            var table = this.GetPagedDataTable(Grid_MinPrice, tb);
            Grid_MinPrice.DataSource = table;
            Grid_MinPrice.DataBind();
        }
        private void BindGrid_ConEvaluation()
        {
            string strSql = @"        SELECT ID
                                      ,SetSubReviewID
                                      ,Company
                                      ,Price_ReviewResults
                                      ,Skill_ReviewResults
                                      ,Business_ReviewResults
                                      ,Synthesize_ReviewResults
                                      ,SortIndex
                                      ,Remarks"
                            + @"     FROM PHTGL_SetSubReview_Sch2 where 1=1 AND SetSubReviewID = @SetSubReviewID ";
            List<SqlParameter> listStr = new List<SqlParameter>();
            if (string.IsNullOrEmpty(SetSubReviewID))
            {
                listStr.Add(new SqlParameter("@SetSubReviewID", ""));

            }
            else
            {
                listStr.Add(new SqlParameter("@SetSubReviewID", SetSubReviewID));

            }
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            Grid_ConEvaluation.RecordCount = tb.Rows.Count;
            var table = this.GetPagedDataTable(Grid_ConEvaluation, tb);
            Grid_ConEvaluation.DataSource = table;
            Grid_ConEvaluation.DataBind();
        }
        private void BindSchGrid()
        {
            var _SetSubReview = BLL.PHTGL_SetSubReviewService.GetPHTGL_SetSubReviewById(SetSubReviewID);
            switch  (_SetSubReview.Type)
            {
                case PHTGL_SetSubReviewService.Type_ConEvaluation:
                    BindGrid_ConEvaluation();
                    Grid_MinPrice.Hidden = true;
                    break;
                case PHTGL_SetSubReviewService.Type_MinPrice:
                    BindGrid_MinPrice();
                    Grid_ConEvaluation.Hidden = true;
                    break;
                default:
                    Grid_MinPrice.Hidden = true;
                    Grid_ConEvaluation.Hidden = true;


                    break;
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
            listStr.Add(new SqlParameter("@ContractId", SetSubReviewID));

            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            Grid1.RecordCount = tb.Rows.Count;
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();
            BindSchGrid();

        }
        #endregion


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

        #region 保存提交
        protected void btnAgree_Click(object sender, EventArgs e)
        {
            ApprovemanSave(true);
            OAWebSevice.DoneRequest(pHTGL_Approve.ApproveId);
            OAWebSevice.Pushoa();
        }

        protected void btnDisgree_Click(object sender, EventArgs e)
        {
            ApprovemanSave(false);
            OAWebSevice.DoneRequest(pHTGL_Approve.ApproveId);
            OAWebSevice.Pushoa_Creater(pHTGL_Approve.ApproveId);

        }

        /// <summary>
        /// 审批人员保存
        /// </summary>
        /// <param name="IsAgree"></param>
        void ApprovemanSave(bool IsAgree)
        {
            pHTGL_Approve.ApproveDate = Funs.GetNewDateTimeOrNow("").ToString();
            pHTGL_Approve.State = 1;
            //pHTGL_Approve.IsAgree = Convert.ToInt32(CBIsAgree.SelectedValueArray[0]);
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
                    _Approve.ApproveForm = PHTGL_ApproveService.SetSubReview;

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
            var _SetSubReview = BLL.PHTGL_SetSubReviewService.GetPHTGL_SetSubReviewById(SetSubReviewID);
            _SetSubReview.State = state;
            PHTGL_SetSubReviewService.UpdatePHTGL_SetSubReview(_SetSubReview);
        }
        #endregion

        #region 附件
        protected void btnAttachUrl_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/SetSubReviewAttachUrl&menuId={1}", this.SetSubReviewID, BLL.Const.SetSubReview)));

            //SetSubReview setSubReview = new SetSubReview();
            //setSubReview.Print(SetSubReviewID);
        }
        protected void btnAttachUrl2_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/SetSubReviewAttachUrl&menuId={1}", this.SetSubReviewID + "report", BLL.Const.SetSubReview)));
        }
        #endregion


    }
}