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
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BidDocumentsReviewId = Request.Params["BidDocumentsReviewId"];
                if (string.IsNullOrEmpty(BidDocumentsReviewId))
                {
                    ContentPanel2.Hidden = true;
                    btnSubmit.Hidden = true;

                }
                else
                {
                    ContentPanel1.Hidden = true;

                }

    
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                //总承包合同编号
                // BLL.ProjectService.InitAllProjectCodeDropDownList(this.DropProjectId, true);
                //招标方式
                BLL.PHTGL_BidDocumentsReviewService.InitGetBidTypeDropDownList(this.txtBidType, false);

                UserService.InitUserUnitIdDropDownList(DropConstructionManager, Const.UnitId_SEDIN, false);
                UserService.InitUserUnitIdDropDownList(DropControlManager, Const.UnitId_SEDIN, false);
                UserService.InitUserUnitIdDropDownList(DropProjectManager, Const.UnitId_SEDIN, false);
                UserService.InitUserUnitIdDropDownList(DropPreliminaryMan, Const.UnitId_SEDIN, false);

                //   this.DropProjectId.SelectedValue = this.CurrUser.LoginProjectId;
                BLL.UserService.InitUserRoleIdUnitIdDropDownList(Approval_Construction, CurrUser.UnitId, Const.ConstructionMinister, Const.ConstructionViceMinister, false);
                Bind();
                BindGrid();
                DropDownBox1_TextChanged(null, null);


                if (Request.Params["State"] == "Again")
                {
                    this.btnSave.Hidden = false;
                    this.btnSubmit.Hidden = false;
                     ContentPanel1.Hidden = false;

                }
                else if (Request.Params["State"] == "Edit")
                {
                     this.btnSave.Hidden = false;
                     btnSubmit.Hidden = true;
                     ContentPanel1.Hidden = false;
                     ContentPanel2.Hidden = true;
                   
                }
                var newmodel = BLL.PHTGL_BidDocumentsReviewService.GetPHTGL_BidDocumentsReviewById(BidDocumentsReviewId);
                if (newmodel != null)
                {
                    if (newmodel.State >= Const.ContractCreat_Complete)
                    {
                        this.btnSave.Hidden = true;
                        this.btnSubmit.Hidden = true;
                    }

                }
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
                    txtBidContent.Text = Convert.ToString(Bid.BidContent);
                    drpProjectId.Value = Convert.ToString(Bid.ActionPlanReviewId);
                    //this.DropProjectId.SelectedValue = Convert.ToString( Bid.ActionPlanReviewId);
                    // drpProjectId_SelectedIndexChanged(null, null);
                    txtBidType.SelectedValue = Convert.ToString(Bid.BidType);
                    txtBidDocumentsName.Text = Convert.ToString(Bid.BidDocumentsName);
                    txtBidDocumentsCode.Text = Convert.ToString(Bid.BidDocumentsCode);
                    Bidding_SendTime.SelectedDate = Bid.Bidding_SendTime;
                    Bidding_StartTime.SelectedDate = Bid.Bidding_StartTime;
                    Approval_Construction.SelectedValue = Bid.Approval_Construction;
                    DropConstructionManager.SelectedValue = Bid.ConstructionManager;
                    DropControlManager.SelectedValue = Bid.ControlManager;
                    DropProjectManager.SelectedValue = Bid.ProjectManager;
                    DropPreliminaryMan.SelectedValue = Bid.PreliminaryMan;
                }
             
            }

        }

        private void BindGrid()
        {
            string strSql = @"SELECT Acp.ActionPlanReviewId
                                  ,Act.ActionPlanID
                                  ,Act.ActionPlanCode
                                   ,Pro.ProjectCode
                                  ,U.UserName as CreatUser
                                  ,Act.ProjectShortName as Name
                                  ,Act.EPCCode
                                  ,Act.CreateTime
                                  ,Act.ProjectID
                                  ,Act.ProjectName
                                  ,Act.Unit
                                  ,Act.ConstructionSite "
                          + @" FROM PHTGL_ActionPlanReview  AS Acp "
                          + @" LEFT JOIN PHTGL_ActionPlanFormation AS Act ON Act.ActionPlanID = Acp.ActionPlanID  "
                          + @" LEFT JOIN Sys_User AS U ON U.UserId = Act.CreatUser  "
                          + @" LEFT JOIN Base_Project AS Pro ON Pro.ProjectId = Act.ProjectID  WHERE 1=1 and Acp.State=@ContractReview_Complete";
            List<SqlParameter> listStr = new List<SqlParameter>();
            listStr.Add(new SqlParameter("@ContractReview_Complete", Const.ContractReview_Complete));

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
        #endregion



        #region DropDownList下拉选择事件
       
        protected void DropDownBox1_TextChanged(object sender, EventArgs e)
        {
            if (this.drpProjectId.Value != null)
            {
                var Acp = BLL.PHTGL_ActionPlanReviewService.GetPHTGL_ActionPlanReviewById(drpProjectId.Value);
                var _ActionPlanFormation = BLL.PHTGL_ActionPlanFormationService.GetPHTGL_ActionPlanFormationById(Acp.ActionPlanID);

                Model.PHTGL_ActionPlanFormation table = BLL.PHTGL_ActionPlanFormationService.GetPHTGL_ActionPlanFormationById(Acp.ActionPlanID);
                string UnitId = ProjectService.GetProjectByProjectId(table.ProjectID).UnitId;

                //项目名称
                this.txtProjectName.Text = _ActionPlanFormation.ProjectShortName;

                this.txtEPCCode.Text = _ActionPlanFormation.EPCCode;



            }
            else
            {
                this.txtProjectName.Text = string.Empty;
                this.txtEPCCode.Text = string.Empty;


            }

        }
        #endregion

        private  bool  Save()
        {
            //if (Approval_Construction.SelectedValue==Const._Null)
            //{
            //    ShowNotify("请选择施工管理部审批人员！", MessageBoxIcon.Warning);
            //    return false ;

            //}
            if (txtBidType.SelectedValue==Const._Null)
            {
                ShowNotify("请选择招标方式！", MessageBoxIcon.Warning);
                return false;

            }
            Model.PHTGL_BidDocumentsReview pHTGL_Bid = new Model.PHTGL_BidDocumentsReview();
            var acp = BLL.PHTGL_ActionPlanReviewService.GetPHTGL_ActionPlanReviewById(drpProjectId.Value);
            var act = BLL.PHTGL_ActionPlanFormationService.GetPHTGL_ActionPlanFormationById(acp.ActionPlanID);
            pHTGL_Bid.ActionPlanReviewId = acp.ActionPlanReviewId;
            pHTGL_Bid.ActionPlanID = acp.ActionPlanID;
            pHTGL_Bid.ProjectId = act.ProjectID;
            pHTGL_Bid.State = Const.ContractCreating;
            pHTGL_Bid.BidContent = txtBidContent.Text;
            pHTGL_Bid.BidType = txtBidType.SelectedValue;
            pHTGL_Bid.BidDocumentsName = txtBidDocumentsName.Text;
            pHTGL_Bid.BidDocumentsCode = txtBidDocumentsCode.Text;
            pHTGL_Bid.Bidding_SendTime = Bidding_SendTime.SelectedDate;
            pHTGL_Bid.Bidding_StartTime = Bidding_StartTime.SelectedDate;
            pHTGL_Bid.Url = "";
            pHTGL_Bid.CreateUser = this.CurrUser.UserId;
            pHTGL_Bid.CreatTime = DateTime.Now;
            if (!string.IsNullOrEmpty(BidDocumentsReviewId))
            {
                pHTGL_Bid.Approval_Construction = Approval_Construction.SelectedValue;
                pHTGL_Bid.ConstructionManager = DropConstructionManager.SelectedValue;
                pHTGL_Bid.ControlManager = DropControlManager.SelectedValue;
                pHTGL_Bid.PreliminaryMan = DropPreliminaryMan.SelectedValue;
                pHTGL_Bid.ProjectManager = DropProjectManager.SelectedValue;
            }
           
            if (string.IsNullOrEmpty(BidDocumentsReviewId))
            {
                BidDocumentsReviewId = SQLHelper.GetNewID(typeof(Model.PHTGL_BidDocumentsReview));
                pHTGL_Bid.BidDocumentsReviewId = BidDocumentsReviewId;
                
                PHTGL_BidDocumentsReviewService.AddPHTGL_BidDocumentsReview(pHTGL_Bid);
                 ShowNotify("保存成功！", MessageBoxIcon.Success);
            }
            else
            {
                var Bid = PHTGL_BidDocumentsReviewService.GetPHTGL_BidDocumentsReviewById(BidDocumentsReviewId);
                if (Bid==null)
                {
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
                
            }

            return true ;


        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Save())
            {
                if (!BLL.AttachFileService.Getfile(BidDocumentsReviewId, BLL.Const.BidDocumentsReviewIdMenuid))
                {
                    ShowNotify("未上传附件，无法提交！", MessageBoxIcon.Warning);
                    BLL.PHTGL_BidDocumentsReviewService.DeletePHTGL_BidDocumentsReviewById(BidDocumentsReviewId);
                     return;
                }
                PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());

            }
 
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (Save())
            {
                if (!BLL.AttachFileService.Getfile(BidDocumentsReviewId, BLL.Const.BidDocumentsReviewIdMenuid))
                {
                    ShowNotify("未上传附件，无法提交！", MessageBoxIcon.Warning);

                    return  ;
                }
                var Bid = PHTGL_BidDocumentsReviewService.GetPHTGL_BidDocumentsReviewById(BidDocumentsReviewId);
                Bid.State = Const.ContractReviewing;
                PHTGL_BidDocumentsReviewService.UpdatePHTGL_BidDocumentsReview(Bid);
                #region 创建审批信息
                Dic_ApproveMan = PHTGL_BidDocumentsReviewService.Get_DicApproveman(Bid.ProjectId, BidDocumentsReviewId);
                ApproveManModels = PHTGL_BidDocumentsReviewService.GetApproveManModels(Bid.ProjectId, BidDocumentsReviewId);
                //创建第一节点审批信息
                Model.PHTGL_Approve _Approve = new Model.PHTGL_Approve();
                _Approve.ApproveId = SQLHelper.GetNewID(typeof(Model.PHTGL_Approve));
                _Approve.ContractId = BidDocumentsReviewId;
                _Approve.ApproveMan = ApproveManModels.Find(BidDoc => BidDoc.Number == 1).userid;
                _Approve.ApproveDate = "";
                _Approve.State = 0;
                _Approve.IsAgree = 0;
                _Approve.ApproveIdea = "";
                _Approve.ApproveType = ApproveManModels.Find(BidDoc => BidDoc.Number == 1).Rolename;
                _Approve.IsPushOa = 0;

                _Approve.ApproveForm = PHTGL_ApproveService.BidDocumentsReview;

                BLL.PHTGL_ApproveService.AddPHTGL_Approve(_Approve);
                //创建第2节点审批信息
                Model.PHTGL_Approve _Approve2 = new Model.PHTGL_Approve();
                _Approve2.ApproveId = SQLHelper.GetNewID(typeof(Model.PHTGL_Approve));
                _Approve2.ContractId = BidDocumentsReviewId;
                _Approve2.ApproveMan = ApproveManModels.Find(BidDoc => BidDoc.Number == 2).userid;
                _Approve2.ApproveDate = "";
                _Approve2.State = 0;
                _Approve2.IsAgree = 0;
                _Approve2.ApproveIdea = "";
                _Approve2.ApproveType = ApproveManModels.Find(BidDoc => BidDoc.Number == 2).Rolename;
                _Approve2.IsPushOa = 0;

                _Approve2.ApproveForm = PHTGL_ApproveService.BidDocumentsReview;

                BLL.PHTGL_ApproveService.AddPHTGL_Approve(_Approve2);
                #endregion
                OAWebSevice.Pushoa();
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