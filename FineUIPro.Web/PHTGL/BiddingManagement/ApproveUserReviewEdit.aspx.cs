using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using Newtonsoft.Json.Linq;

namespace FineUIPro.Web.PHTGL.BiddingManagement
{
    public partial class ApproveUserReviewEdit : PageBase
    {
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
        /// 审批人字典
        /// </summary>
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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                ApproveUserReviewID = Request.Params["ApproveUserReviewID"];
                 BLL.PHTGL_BidDocumentsReviewService.InitGetBidCompleteDropDownList(drpBidDocumentCode,true);
                DropUser.DataValueField = "UserName";
                DropUser.DataTextField = "UserName";
                DropUser.DataSource = BLL.UserService.GetUserListByUnitId(Const.UnitId_SEDIN);
                DropUser.DataBind();
                Funs.FineUIPleaseSelect(DropUser);
               
                //UserService.InitUserUnitIdDropDownList(this.DropUser, Const.UnitId_SEDIN, true);
                UserService.InitUserUnitIdDropDownList(this.DropConstructionManager, Const.UnitId_SEDIN, true);
               // UserService.InitUserUnitIdDropDownList(this.DropProjectManager, Const.UnitId_SEDIN, true);
                UserService.InitUserUnitIdDropDownList(this.DropApproval_Construction, Const.UnitId_SEDIN, true);
               // UserService.InitUserUnitIdDropDownList(this.DropDeputyGeneralManager, Const.UnitId_SEDIN, true);
                Bind();
                BindGrid();
                #region Grid1
                // 删除选中单元格的客户端脚本
                string deleteScript = GetDeleteScript();

                JObject defaultObj = new JObject();
                defaultObj.Add("ApproveUserName", "");
                defaultObj.Add("ApproveUserSpecial", "");
                defaultObj.Add("ApproveUserUnit", "");
                defaultObj.Add("Remarks", "");

                // 在第一行新增一条数据
                btnNew.OnClientClick = Grid1.GetAddNewRecordReference(defaultObj, true);
                // 删除选中行按钮
                btnDelete.OnClientClick = Grid1.GetNoSelectionAlertReference("请选择一条记录!") + deleteScript;
                #endregion
                var newmodel = BLL.PHTGL_BidApproveUserReviewService.GetPHTGL_BidApproveUserReviewById(ApproveUserReviewID);
                if (newmodel != null)
                {
                    if (newmodel.State >= Const.ContractCreat_Complete)
                    {
                        this.btnSave.Hidden = true;
                        this.btnSubmit.Hidden = true;
                    }

                }
                if (Request.Params["State"] == "Again")
                {
                    this.btnSave.Hidden = false;
                    this.btnSubmit.Hidden = false;
 
                }
            }
        }

        private string GetDeleteScript()
        {
            return Confirm.GetShowReference("确定删除当前数据吗？", String.Empty, MessageBoxIcon.Question, Grid1.GetDeleteSelectedRowsReference(), String.Empty);
        }

        #region 数据绑定
        /// <summary>
        /// 数据绑定
        /// </summary>
        private void Bind()
        {
             
            if (!string.IsNullOrEmpty(ApproveUserReviewID))
            {
                var Bid = BLL.PHTGL_BidApproveUserReviewService.GetPHTGL_BidApproveUserReviewById(ApproveUserReviewID);
                 if (Bid != null)
                {
                    txtProjectName.Text = PHTGL_ActionPlanFormationService.GetPHTGL_ActionPlanFormationById(Bid.ActionPlanID).ProjectShortName;
                    drpBidDocumentCode.SelectedValue = Bid.BidDocumentsReviewId;
                    txtBidProject.Text = Bid.BidProject;
                    DropConstructionManager.SelectedValue = Bid.ConstructionManager;
                  //  DropProjectManager.SelectedValue = Bid.ProjectManager;
                    DropApproval_Construction.SelectedValue = Bid.Approval_Construction;
                    //DropDeputyGeneralManager.SelectedValue = Bid.DeputyGeneralManager;

                }

            }

        }
        private void BindGrid()
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

            Grid1.RecordCount = tb.Rows.Count;
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();
        }
        #endregion


        protected void drpProjectId_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private bool   Save(int state)
        {
            if (drpBidDocumentCode.SelectedValue == Const._Null)
            {
                ShowNotify("请选择招标文件！", MessageBoxIcon.Warning);
                return false;

            }
 
            if (DropConstructionManager.SelectedValue == Const._Null)
            {
                ShowNotify("请选择施工经理！", MessageBoxIcon.Warning);
                return false;

            }
            //if (DropProjectManager.SelectedValue == Const._Null)
            //{
            //    ShowNotify("请选择项目经理！", MessageBoxIcon.Warning);
            //    return false;

            //}
            if (DropApproval_Construction.SelectedValue == Const._Null)
            {
                ShowNotify("请选择施工管理部人员！", MessageBoxIcon.Warning);
                return false;

            }
            //if (DropDeputyGeneralManager.SelectedValue == Const._Null)
            //{
            //    ShowNotify("请选择分管副总经理！", MessageBoxIcon.Warning);
            //    return false;

            //}

            Model.PHTGL_BidApproveUserReview newtable = new Model.PHTGL_BidApproveUserReview();
            var Bid = BLL.PHTGL_BidDocumentsReviewService.GetPHTGL_BidDocumentsReviewById(drpBidDocumentCode.SelectedValue);

            newtable.BidDocumentsReviewId = drpBidDocumentCode.SelectedValue;
            newtable.ActionPlanID = Bid.ActionPlanID;
            newtable.BidProject = txtBidProject.Text;
            newtable.State = state;
            newtable.CreateUser = this.CurrUser.UserId;
            newtable.ProjectId = Bid.ProjectId;
            newtable.ConstructionManager = DropConstructionManager.SelectedValue;
           // newtable.ProjectManager = DropProjectManager.SelectedValue;
            newtable.Approval_Construction = DropApproval_Construction.SelectedValue;
           // newtable.DeputyGeneralManager = DropDeputyGeneralManager.SelectedValue;
            if (string.IsNullOrEmpty(ApproveUserReviewID))
            {
                newtable.ApproveUserReviewID = SQLHelper.GetNewID(typeof(Model.PHTGL_BidApproveUserReview));
                ApproveUserReviewID = newtable.ApproveUserReviewID;
                BLL.PHTGL_BidApproveUserReviewService.AddPHTGL_BidApproveUserReview(newtable);
            }
            else
            {
                newtable.ApproveUserReviewID = ApproveUserReviewID;
                BLL.PHTGL_BidApproveUserReviewService.UpdatePHTGL_BidApproveUserReview(newtable);
            }


            BLL.PHTGL_BidApproveUserReview_Sch1Service.DeletePHTGL_BidApproveUserReview_Sch1ByReviewID(newtable.ApproveUserReviewID);
            JArray EditorArr = Grid1.GetMergedData();
            if (EditorArr.Count > 0)
            {
                Model.PHTGL_BidApproveUserReview_Sch1 model = null;
                for (int i = 0; i < EditorArr.Count; i++)
                {
                    JObject objects = (JObject)EditorArr[i];
                    model = new Model.PHTGL_BidApproveUserReview_Sch1();
                    model.ID = SQLHelper.GetNewID(typeof(Model.PHTGL_BidApproveUserReview_Sch1));
                    model.ApproveUserReviewID = ApproveUserReviewID;
                    model.ApproveUserName = objects["values"]["ApproveUserName"].ToString();
                    model.ApproveUserSpecial = objects["values"]["ApproveUserSpecial"].ToString();
                    model.ApproveUserUnit = objects["values"]["ApproveUserUnit"].ToString();
                    model.Remarks = objects["values"]["Remarks"].ToString();
                    BLL.PHTGL_BidApproveUserReview_Sch1Service.AddPHTGL_BidApproveUserReview_Sch1(model);
                }
            }
            return true;

        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Save(Const.ContractCreating))
            {
                ShowNotify("保存成功！", MessageBoxIcon.Success);

                PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());

            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (Save(Const.ContractCreat_Complete))
            {
                var Bid = PHTGL_BidApproveUserReviewService.GetPHTGL_BidApproveUserReviewById(ApproveUserReviewID);
                Bid.State = Const.ContractReviewing;
                PHTGL_BidApproveUserReviewService.UpdatePHTGL_BidApproveUserReview(Bid);
                 ApproveManModels = PHTGL_BidApproveUserReviewService.GetApproveManModels(ApproveUserReviewID);
                //创建第一节点审批信息
                Model.PHTGL_Approve _Approve = new Model.PHTGL_Approve();
                _Approve.ApproveId = SQLHelper.GetNewID(typeof(Model.PHTGL_Approve));
                _Approve.ContractId = ApproveUserReviewID;
                _Approve.ApproveMan = ApproveManModels.Find(x => x.Number == 1).userid;
                _Approve.ApproveDate = "";
                _Approve.State = 0;
                _Approve.IsAgree = 0;
                _Approve.ApproveIdea = "";
                _Approve.ApproveType = ApproveManModels.Find(x => x.Number == 1).Rolename;
                _Approve.IsPushOa = 0;
                _Approve.ApproveForm = PHTGL_ApproveService.ApproveUserReview;

                BLL.PHTGL_ApproveService.AddPHTGL_Approve(_Approve);
                ShowNotify("提交成功！", MessageBoxIcon.Success);
                OAWebSevice.Pushoa();
                PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());

            }
        }
    }
}