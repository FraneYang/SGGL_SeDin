using BLL;
using Newtonsoft.Json.Linq;
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
    public partial class BidDocumentsReviewEdit : PageBase
    {
        #region 初始化属性
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
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BidDocumentsReviewId = Request.Params["BidDocumentsReviewId"];
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                //总承包合同编号
                BLL.ProjectService.InitAllProjectCodeDropDownList(this.DropProjectId, true);
                //招标方式
                BLL.PHTGL_BidDocumentsReviewService.InitGetBidTypeDropDownList(this.txtBidType,true);
                this.DropProjectId.SelectedValue = this.CurrUser.LoginProjectId;
                drpProjectId_SelectedIndexChanged(null, null);
                BLL.UserService.InitUserRoleIdUnitIdDropDownList(Approval_Construction, CurrUser.UnitId, Const.ConstructionMinister, Const.ConstructionViceMinister, true);
                Bind();

            }
        }


        #region 数据绑定
        /// <summary>
        /// 数据绑定
        /// </summary>
        private void Bind()
        {
            if (!string.IsNullOrEmpty(BidDocumentsReviewId))
            {
                var Bid = BLL.PHTGL_BidDocumentsReviewService.GetPHTGL_BidDocumentsReviewById(BidDocumentsReviewId);
                if (Bid !=null)
                {
                    txtBidContent.Text = Bid.BidContent.ToString();
                    this.DropProjectId.SelectedValue = Bid.ProjectId.ToString();
                    drpProjectId_SelectedIndexChanged(null, null);
                    txtBidType.SelectedValue = Bid.BidType.ToString();
                    txtBidDocumentsName.Text = Bid.BidDocumentsName.ToString();
                    txtBidDocumentsCode.Text = Bid.BidDocumentsCode.ToString();
                    Bidding_SendTime.SelectedDate = Bid.Bidding_SendTime;
                    Bidding_StartTime.SelectedDate = Bid.Bidding_StartTime;
                    Approval_Construction.SelectedValue = Bid.Approval_Construction;
                 }
             
            }

        }
        #endregion



        #region DropDownList下拉选择事件
        /// <summary>
        /// 选择项目Id获取项目名称
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpProjectId_SelectedIndexChanged(object sender, EventArgs e)
        {
          
            if (this.DropProjectId.SelectedValue != BLL.Const._Null)
            {
                //项目名称
                this.txtProjectName.Text = BLL.ProjectService.GetProjectNameByProjectId(this.DropProjectId.SelectedValue);

                 //施工经理
                this.txtConstructionManager.Text = BLL.ProjectService.GetRoleName(this.DropProjectId.SelectedValue, BLL.Const.ConstructionManager);

                //项目经理
                this.txtProjectManager.Text = BLL.ProjectService.GetRoleName(this.DropProjectId.SelectedValue, BLL.Const.ProjectManager);

                //控制经理
                this.txtControlManager.Text = BLL.ProjectService.GetRoleName(this.DropProjectId.SelectedValue, BLL.Const.ControlManager);

            }
            else
            {
                this.txtProjectName.Text = string.Empty;
                this.txtConstructionManager.Text = string.Empty;
                this.txtProjectManager.Text = string.Empty;
                this.txtControlManager.Text = string.Empty;
            }
        }
        #endregion

         private  bool  Save()
        {
            if (Approval_Construction.SelectedValue==Const._Null)
            {
                ShowNotify("请选择施工管理部审批人员！", MessageBoxIcon.Warning);
                return false ;

            }
            if (txtBidType.SelectedValue==Const._Null)
            {
                ShowNotify("请选择招标方式！", MessageBoxIcon.Warning);
                return false;

            }
            Model.PHTGL_BidDocumentsReview pHTGL_Bid = new Model.PHTGL_BidDocumentsReview();
            pHTGL_Bid.ProjectId = DropProjectId.SelectedValue;
            pHTGL_Bid.State = 0;
            pHTGL_Bid.BidContent = txtBidContent.Text;
            pHTGL_Bid.BidType = txtBidType.SelectedValue;
            pHTGL_Bid.BidDocumentsName = txtBidDocumentsName.Text;
            pHTGL_Bid.BidDocumentsCode = txtBidDocumentsCode.Text;
            pHTGL_Bid.Bidding_SendTime = Bidding_SendTime.SelectedDate;
            pHTGL_Bid.Bidding_StartTime = Bidding_StartTime.SelectedDate;
            pHTGL_Bid.Url = "";
            pHTGL_Bid.CreateUser = this.CurrUser.UserId;
            pHTGL_Bid.Approval_Construction = Approval_Construction.SelectedValue;
            pHTGL_Bid.CreatTime = DateTime.Now;
            if (string.IsNullOrEmpty(BidDocumentsReviewId))
            {
                BidDocumentsReviewId = SQLHelper.GetNewID(typeof(Model.PHTGL_BidDocumentsReview));
                pHTGL_Bid.BidDocumentsReviewId = BidDocumentsReviewId;
                PHTGL_BidDocumentsReviewService.AddPHTGL_BidDocumentsReview(pHTGL_Bid);
 
                ShowNotify("保存成功！", MessageBoxIcon.Success);
            }
            else
            {
                pHTGL_Bid.BidDocumentsReviewId = BidDocumentsReviewId;
                PHTGL_BidDocumentsReviewService.UpdatePHTGL_BidDocumentsReview(pHTGL_Bid);
                ShowNotify("修改成功", MessageBoxIcon.Success);
            }

            return true ;


        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Save())
            {
                PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());

            }
 
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (Save())
            {
                var Bid = PHTGL_BidDocumentsReviewService.GetPHTGL_BidDocumentsReviewById(BidDocumentsReviewId);
                Bid.State = 1;
                PHTGL_BidDocumentsReviewService.UpdatePHTGL_BidDocumentsReview(Bid);
                #region 创建审批信息
                Dic_ApproveMan = PHTGL_BidDocumentsReviewService.Get_DicApproveman(DropProjectId.SelectedValue, BidDocumentsReviewId);
                //创建第一节点审批信息
                Model.PHTGL_Approve _Approve = new Model.PHTGL_Approve();
                _Approve.ApproveId = SQLHelper.GetNewID(typeof(Model.PHTGL_Approve));
                _Approve.ContractId = BidDocumentsReviewId;
                _Approve.ApproveMan = Dic_ApproveMan[1];
                _Approve.ApproveDate = "";
                _Approve.State = 0;
                _Approve.IsAgree = 0;
                _Approve.ApproveIdea = "";
                _Approve.ApproveType = "1";
                BLL.PHTGL_ApproveService.AddPHTGL_Approve(_Approve);
                //创建第2节点审批信息
                Model.PHTGL_Approve _Approve2 = new Model.PHTGL_Approve();
                _Approve2.ApproveId = SQLHelper.GetNewID(typeof(Model.PHTGL_Approve));
                _Approve2.ContractId = BidDocumentsReviewId;
                _Approve2.ApproveMan = Dic_ApproveMan[2];
                _Approve2.ApproveDate = "";
                _Approve2.State = 0;
                _Approve2.IsAgree = 0;
                _Approve2.ApproveIdea = "";
                _Approve2.ApproveType = "2";
                BLL.PHTGL_ApproveService.AddPHTGL_Approve(_Approve2);
                #endregion
                ShowNotify("提交成功！", MessageBoxIcon.Success);
                PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());

            }
          
        }
        #region 附件上传
        /// <summary>
        /// 上传附件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAttachUrl_Click(object sender, EventArgs e)
        {
            Save();
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/BidDocumentsAttachUrl&menuId={1}", this.BidDocumentsReviewId, BLL.Const.BidDocumentsReviewIdMenuid)));
        }
        #endregion

    }
}