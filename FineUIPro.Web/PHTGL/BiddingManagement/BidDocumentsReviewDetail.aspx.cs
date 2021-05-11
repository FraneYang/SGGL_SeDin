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
                EndApproveType = 4;
                this.btnClose.OnClientClick = ActiveWindow.GetHideRefreshReference();

                //获取招标实施计划基本信息
                var _Bid = PHTGL_BidDocumentsReviewService.GetPHTGL_BidDocumentsReviewById(BidDocumentsReviewId);

                //获取审批人字典
                Dic_ApproveMan = PHTGL_BidDocumentsReviewService.Get_DicApproveman(_Bid.ProjectId, _Bid.BidDocumentsReviewId);
                //获取当前登录人审批信息
                pHTGL_Approve = BLL.PHTGL_ApproveService.GetPHTGL_ApproveByUserId(BidDocumentsReviewId, this.CurrUser.UserId);

                this.txtProjectCode.Text = BLL.ProjectService.GetProjectCodeByProjectId(_Bid.ProjectId);
                this.txtProjectName.Text = BLL.ProjectService.GetProjectNameByProjectId(_Bid.ProjectId);
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
            string strsql = "select  count(*) from PHTGL_Approve where  ApproveType<=2  and IsAgree='2' and ContractId='" + pHTGL_Approve.ContractId + "'";
            DataTable tb = SQLHelper.RunSqlGetTable(strsql);
            if (tb != null && tb.Rows.Count > 0)
            {
                if (tb.Rows[0][0].ToString() == "2")
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


        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(pHTGL_Approve.ApproveType) <= 2)
            {
                CountersignerSave();
            }
            else
            {
                ApprovemanSave();
            }

        }

        /// <summary>
        /// 会签人员保存
        /// </summary>
        void CountersignerSave()
        {
            pHTGL_Approve.ApproveDate = Funs.GetNewDateTimeOrNow("").ToString();
            if (CBIsAgree.SelectedValueArray.Length > 0)
            {
                pHTGL_Approve.State = 1;
                pHTGL_Approve.IsAgree = Convert.ToInt32(CBIsAgree.SelectedValueArray[0]);
                pHTGL_Approve.ApproveIdea = txtApproveIdea.Text;
                BLL.PHTGL_ApproveService.UpdatePHTGL_Approve(pHTGL_Approve);
                ChangeState(1);
                if (Convert.ToInt32(CBIsAgree.SelectedValueArray[0]) == 2)  //同意时
                {
                    if (IsCountersignerAllAgree())
                    {
                        Model.PHTGL_Approve _Approve = new Model.PHTGL_Approve();
                        //_Approve.ApproveId = SQLHelper.GetNewID(typeof(Model.PHTGL_Approve));
                        _Approve.ContractId = pHTGL_Approve.ContractId;
                        _Approve.ApproveMan = Dic_ApproveMan[3];
                        _Approve.ApproveDate = "";
                        _Approve.State = 0;
                        _Approve.IsAgree = 0;
                        _Approve.ApproveIdea = "";
                        _Approve.ApproveType = "3";

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

                        ChangeState(1);
                    }
                }
                else
                {
                    ChangeState(3);
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
                   //     _Approve.ApproveId = SQLHelper.GetNewID(typeof(Model.PHTGL_Approve));
                        _Approve.ContractId = pHTGL_Approve.ContractId;
                        _Approve.ApproveMan = Dic_ApproveMan[nextApproveType];
                        _Approve.ApproveDate = "";
                        _Approve.State = 0;
                        _Approve.IsAgree = 0;
                        _Approve.ApproveIdea = "";
                        _Approve.ApproveType = nextApproveType.ToString();

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
                        ChangeState(1);
                    }
                    else
                    {
                        ChangeState(3);
                    }

                }
                else
                {
                    ChangeState(2);
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