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
    public partial class BidDocumentsReviewDetail : PageBase
    {
        #region  定义属性
        /// <summary>
        /// 合同ID
        /// </summary>
        public string BidDocumentsReviewId
        {
            get
            {
                return (string)ViewState["BidDocumentsReviewId"];
            }
            set
            {
                ViewState["BidDocumentsReviewId"] = value;
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
                BidDocumentsReviewId = Request.Params["BidDocumentsReviewId"];
                EndApproveType = 5;
               // this.btnClose.OnClientClick = ActiveWindow.GetHideRefreshReference();

                //获取招标实施计划基本信息
                var _Bid = PHTGL_BidDocumentsReviewService.GetPHTGL_BidDocumentsReviewById(BidDocumentsReviewId);
                var _ActionPlanFormation = BLL.PHTGL_ActionPlanFormationService.GetPHTGL_ActionPlanFormationById(_Bid.ActionPlanID);

                //获取审批人字典
                Dic_ApproveMan = PHTGL_BidDocumentsReviewService.Get_DicApproveman(_Bid.ProjectId, _Bid.BidDocumentsReviewId);
                ApproveManModels = PHTGL_BidDocumentsReviewService.GetApproveManModels(_Bid.ProjectId, _Bid.BidDocumentsReviewId);

                //获取当前登录人审批信息
                pHTGL_Approve = BLL.PHTGL_ApproveService.GetPHTGL_ApproveByUserId(BidDocumentsReviewId, this.CurrUser.UserId);

                this.txtProjectCode.Text = _ActionPlanFormation.EPCCode;
                this.txtProjectName.Text = _ActionPlanFormation.ProjectShortName;
                this.txtCreateUser.Text = BLL.UserService.GetUserNameByUserId(_Bid.CreateUser);
                if (PHTGL_ApproveService.IsApproveMan(BidDocumentsReviewId, this.CurrUser.UserId))
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
                if (_Bid.State ==Const.ContractReview_Refuse)
                {
                    btnAgree.Enabled = false ;
                    btnDisgree.Enabled = false;
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
                             + @"   left join Sys_User AS U ON U.UserId = App.ApproveMan WHERE 1=1   and App.IsAgree <>0 and app.ContractId= @ContractId order by convert(datetime ,App.ApproveDate)  ";
            List<SqlParameter> listStr = new List<SqlParameter>();
            listStr.Add(new SqlParameter("@ContractId", BidDocumentsReviewId));

            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            Grid1.RecordCount = tb.Rows.Count;
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();
        }
        protected void btnAttachUrl_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/BidDocumentsAttachUrl&menuId={1}", this.BidDocumentsReviewId, BLL.Const.BidDocumentsReviewIdMenuid)));

        }


        /// <summary>
        /// 判断是否所以会签人员都已经同意
        /// </summary>
        /// <returns></returns>
        private bool IsCountersignerAllAgree()
        {
            bool IsNext = false;
            string strsql = "select  count(*) from PHTGL_Approve where    State='0'  and ApproveMan !='' and ContractId='" + pHTGL_Approve.ContractId + "'";
            DataTable tb = SQLHelper.RunSqlGetTable(strsql);
            if (tb != null && tb.Rows.Count > 0)
            {
                if (tb.Rows[0][0].ToString() == "0")
                {
                    IsNext = true;
                }

            }

            return IsNext;

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
             
            var number = ApproveManModels.Find(x => x.Rolename == pHTGL_Approve.ApproveType).Number;

            if (number <= 2)
            {
                CountersignerSave(true);
                OAWebSevice.DoneRequest(pHTGL_Approve.ApproveId);

            }
            else
            {
                ApprovemanSave(true);
                OAWebSevice.DoneRequest(pHTGL_Approve.ApproveId);
            }
            OAWebSevice.Pushoa();

        }

        protected void btnDisgree_Click(object sender, EventArgs e)
        {
            var number = ApproveManModels.Find(x => x.Rolename == pHTGL_Approve.ApproveType).Number;

            if (number <= 2)
            {
                CountersignerSave(false);
                OAWebSevice.DoneRequest(pHTGL_Approve.ApproveId);
                OAWebSevice.Pushoa_Creater(pHTGL_Approve.ApproveId);


            }
            else
            {
                ApprovemanSave(false);
                OAWebSevice.DoneRequest(pHTGL_Approve.ApproveId);
                OAWebSevice.Pushoa_Creater(pHTGL_Approve.ApproveId);

            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
           

        }

        /// <summary>
        /// 会签人员保存
        /// </summary>
        void CountersignerSave(bool IsAgree)
        {
            pHTGL_Approve.ApproveDate = Funs.GetNewDateTimeOrNow("").ToString();
            pHTGL_Approve.State = 1;
            pHTGL_Approve.IsAgree = IsAgree ? 2 : 1;
            pHTGL_Approve.ApproveIdea = txtApproveIdea.Text;
            BLL.PHTGL_ApproveService.UpdatePHTGL_Approve(pHTGL_Approve);
            ChangeState(Const.ContractReviewing);

            if (IsAgree)
            {
                if (IsCountersignerAllAgree())
                {
                    Model.PHTGL_Approve _Approve = new Model.PHTGL_Approve();
                    //_Approve.ApproveId = SQLHelper.GetNewID(typeof(Model.PHTGL_Approve));
                    _Approve.ContractId = pHTGL_Approve.ContractId;
                    _Approve.ApproveMan = ApproveManModels.Find(e => e.Number == 3).userid;
                    _Approve.ApproveDate = "";
                    _Approve.State = 0;
                    _Approve.IsAgree = 0;
                    _Approve.ApproveIdea = "";
                    _Approve.ApproveType = ApproveManModels.Find(e => e.Number == 3).Rolename;
                    _Approve.IsPushOa = 0;
                    _Approve.ApproveForm = PHTGL_ApproveService.BidDocumentsReview;

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

            }
            else
            {
                ChangeState(Const.ContractReview_Refuse);
                var model = PHTGL_ApproveService.GetPHTGL_ApproveByContractId(pHTGL_Approve.ContractId);
                if (model!=null)
                {
                    for (int i = 0; i < model.Count ; i++)
                    {
                        Model.PHTGL_Approve _Approve = model[i];
                        _Approve.State = 1;
                        _Approve.IsAgree = 1;
                        _Approve.ApproveIdea = "其他会签人员已经拒绝";
                        PHTGL_ApproveService.UpdatePHTGL_Approve(_Approve);

                        OAWebSevice.DoneRequest(_Approve.ApproveId);

                    }

                }

            }

                ShowNotify("保存成功！", MessageBoxIcon.Success);
                PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
            
           
        }

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
                    _Approve.ApproveForm = PHTGL_ApproveService.BidDocumentsReview;

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
                    var Bid = PHTGL_BidDocumentsReviewService.GetPHTGL_BidDocumentsReviewById(BidDocumentsReviewId);
                    Model.PHTGL_BidApproveUserReview newtable = new Model.PHTGL_BidApproveUserReview();
                    newtable.ApproveUserReviewID = SQLHelper.GetNewID(typeof(Model.PHTGL_BidApproveUserReview));
                    newtable.BidDocumentsReviewId = BidDocumentsReviewId;
                    newtable.ProjectId = Bid.ProjectId;
                    newtable.ActionPlanID = Bid.ActionPlanID;
                    newtable.State = Const.ContractCreating;
                    PHTGL_BidApproveUserReviewService.AddPHTGL_BidApproveUserReview(newtable);
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
            var _Bid = PHTGL_BidDocumentsReviewService.GetPHTGL_BidDocumentsReviewById(BidDocumentsReviewId);
            _Bid.State = state;
            PHTGL_BidDocumentsReviewService.UpdatePHTGL_BidDocumentsReview(_Bid);
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