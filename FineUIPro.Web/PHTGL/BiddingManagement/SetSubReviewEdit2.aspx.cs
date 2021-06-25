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
    public partial class SetSubReviewEdit2 : PageBase
    {
        #region 定义属性
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
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                SetSubReviewID = Request.Params["SetSubReviewID"];

                PHTGL_BidApproveUserReviewService.InitGetBidCompleteDropDownList(DropBidCode, true);
                UserService.InitUserUnitIdDropDownList(this.DropConstructionManager, Const.UnitId_SEDIN, true);
                UserService.InitUserUnitIdDropDownList(this.DropProjectManager, Const.UnitId_SEDIN, true);
                UserService.InitUserUnitIdDropDownList(this.DropApproval_Construction, Const.UnitId_SEDIN, true);
                UserService.InitUserUnitIdDropDownList(this.DropDeputyGeneralManager, Const.UnitId_SEDIN, true);

                Bind();
                BindGrid();
                #region Grid1
                // 删除选中单元格的客户端脚本
                string deleteScript = GetDeleteScript();

                JObject defaultObj = new JObject();
                defaultObj.Add("Company", "");
                defaultObj.Add("ReviewResults", "");
                defaultObj.Add("Remarks", "");

                // 在第一行新增一条数据
                btnNew.OnClientClick = Grid1.GetAddNewRecordReference(defaultObj, true);
                // 删除选中行按钮
                btnDelete.OnClientClick = Grid1.GetNoSelectionAlertReference("请选择一条记录!") + deleteScript;
                #endregion
                var newmodel = BLL.PHTGL_SetSubReviewService.GetPHTGL_SetSubReviewById(SetSubReviewID);
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
                    ContentPanel1.Hidden = false;

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
            if (!string.IsNullOrEmpty(SetSubReviewID))
            {
                var _SetSubReview = BLL.PHTGL_SetSubReviewService.GetPHTGL_SetSubReviewById(SetSubReviewID);
                var BidUser = BLL.PHTGL_BidApproveUserReviewService.GetPHTGL_BidApproveUserReviewById(_SetSubReview.ApproveUserReviewID);
                var BidDocument = BLL.PHTGL_BidDocumentsReviewService.GetPHTGL_BidDocumentsReviewById(BidUser.BidDocumentsReviewId);
                var Act = BLL.PHTGL_ActionPlanFormationService.GetPHTGL_ActionPlanFormationById(BidDocument.ActionPlanID);

                if (_SetSubReview != null)
                {
                    txtSetSubReviewCode.Text = _SetSubReview.SetSubReviewCode;
                    if (string.IsNullOrEmpty(txtSetSubReviewCode.Text))
                    {
                        txtSetSubReviewCode.Text = Act.ProjectCode + ".000.C01.93-";
                    }
                    DropBidCode.SelectedValue = _SetSubReview.ApproveUserReviewID;
                    txtProjectName.Text = PHTGL_ActionPlanFormationService.GetPHTGL_ActionPlanFormationById(BidDocument.ActionPlanID).ProjectShortName;
                    txtBidContent.Text = BidDocument.BidContent;
                    StartTime.SelectedDate = BidDocument.Bidding_StartTime;
                    string[] a = { _SetSubReview.IsOwenerApprove.ToString() };
                    CBIsOwenerApprove.SelectedValueArray = a;
                    this.DropConstructionManager.SelectedValue = _SetSubReview.ConstructionManager;
                    this.DropProjectManager.SelectedValue = _SetSubReview.ProjectManager;
                    this.DropApproval_Construction.SelectedValue = _SetSubReview.Approval_Construction;
                    this.DropDeputyGeneralManager.SelectedValue = _SetSubReview.DeputyGeneralManager;
                }

            }
        }
        private void BindGrid()
        {
            string strSql = @"       SELECT ID
                                          ,SetSubReviewID
                                          ,Company
                                          ,ReviewResults
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

            Grid1.RecordCount = tb.Rows.Count;
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();
        }
        #endregion

        #region 下拉列表选择事件
        protected void drpProjectId_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DropBidCode.SelectedValue != Const._Null)
            {
                var BidUser = BLL.PHTGL_BidApproveUserReviewService.GetPHTGL_BidApproveUserReviewById(DropBidCode.SelectedValue);
                var BidDocument = BLL.PHTGL_BidDocumentsReviewService.GetPHTGL_BidDocumentsReviewById(BidUser.BidDocumentsReviewId);
                string projectcode = BLL.ProjectService.GetProjectCodeByProjectId(BidUser.ProjectId);
                if (string.IsNullOrEmpty(SetSubReviewID))
                {
                    this.txtSetSubReviewCode.Text = projectcode + ".000.C01.93-";

                }
                txtProjectName.Text = PHTGL_ActionPlanFormationService.GetPHTGL_ActionPlanFormationById(BidDocument.ActionPlanID).ProjectShortName;
                txtBidContent.Text = BidDocument.BidContent;
                StartTime.SelectedDate = BidDocument.Bidding_StartTime;
            }
               

        }
        #endregion

        #region 保存提交
        private bool Save(int state)
        {
            if (DropBidCode.SelectedValue == Const._Null)
            {
                ShowNotify("请选择招标编号", MessageBoxIcon.Warning);
                return false;

            }
            if (DropConstructionManager.SelectedValue == Const._Null)
            {
                ShowNotify("请选择施工经理！", MessageBoxIcon.Warning);
                return false;

            }
            if (DropProjectManager.SelectedValue == Const._Null)
            {
                ShowNotify("请选择项目经理！", MessageBoxIcon.Warning);
                return false;

            }
            if (DropApproval_Construction.SelectedValue == Const._Null)
            {
                ShowNotify("请选择施工管理部人员！", MessageBoxIcon.Warning);
                return false;

            }
            if (DropDeputyGeneralManager.SelectedValue == Const._Null)
            {
                ShowNotify("请选择分管副总经理！", MessageBoxIcon.Warning);
                return false;

            }
            var IsExitCodemodel = PHTGL_SetSubReviewService.GetPHTGL_SetSubReviewBySetSubReviewCode(this.txtSetSubReviewCode.Text.Trim().ToString());

            if (string.IsNullOrEmpty(SetSubReviewID))
            {
                if (IsExitCodemodel != null)
                {
                    ShowNotify("编号已经重复,请修改！", MessageBoxIcon.Warning);

                    return false;
                }

            }
            else
            {
                if (IsExitCodemodel != null && IsExitCodemodel.SetSubReviewID != SetSubReviewID)
                {
                    ShowNotify("编号已经重复,请修改！", MessageBoxIcon.Warning);

                    return false;
                }

            }

            Model.PHTGL_SetSubReview _SetSubReview = new Model.PHTGL_SetSubReview();
            var BidUser = BLL.PHTGL_BidApproveUserReviewService.GetPHTGL_BidApproveUserReviewById(DropBidCode.SelectedValue);
            _SetSubReview.ApproveUserReviewID = DropBidCode.SelectedValue;
            _SetSubReview.ActionPlanID = BidUser.ActionPlanID;
            _SetSubReview.SetSubReviewCode = txtSetSubReviewCode.Text.Trim().ToString();
            _SetSubReview.CreateUser = this.CurrUser.UserId;
            _SetSubReview.State = state;
            _SetSubReview.Type = BLL.PHTGL_SetSubReviewService.Type_MinPrice;
            _SetSubReview.ConstructionManager = DropConstructionManager.SelectedValue;
            _SetSubReview.ProjectManager = DropProjectManager.SelectedValue;
            _SetSubReview.Approval_Construction = DropApproval_Construction.SelectedValue;
            _SetSubReview.DeputyGeneralManager = DropDeputyGeneralManager.SelectedValue;
            _SetSubReview.IsOwenerApprove = Convert.ToInt32(CBIsOwenerApprove.SelectedValueArray[0]);

            if (string.IsNullOrEmpty(SetSubReviewID))
            {
                _SetSubReview.SetSubReviewID = SQLHelper.GetNewID(typeof(Model.PHTGL_SetSubReview));
                SetSubReviewID = _SetSubReview.SetSubReviewID;
                if (CBIsOwenerApprove.SelectedValueArray[0] == "1")
                {
                    if (!BLL.AttachFileService.Getfile(SetSubReviewID, BLL.Const.SetSubReview))
                    {
                        ShowNotify("未上传业主审批结果，无法保存！", MessageBoxIcon.Warning);

                        return false;
                    }
                }
                if (!BLL.AttachFileService.Getfile(SetSubReviewID+ "report", BLL.Const.SetSubReview))
                {
                    ShowNotify("未上评标报告，无法保存！", MessageBoxIcon.Warning);

                    return false;
                }
                PHTGL_SetSubReviewService.AddPHTGL_SetSubReview(_SetSubReview);

            }
            else
            {
                _SetSubReview.SetSubReviewID = SetSubReviewID;
                if (CBIsOwenerApprove.SelectedValueArray[0] == "1")
                {
                    if (!BLL.AttachFileService.Getfile(SetSubReviewID, BLL.Const.SetSubReview))
                    {
                        ShowNotify("未上传业主审批结果，无法保存！", MessageBoxIcon.Warning);

                        return false;
                    }
                }
                if (!BLL.AttachFileService.Getfile(SetSubReviewID + "report", BLL.Const.SetSubReview))
                {
                    ShowNotify("未上评标报告，无法保存！", MessageBoxIcon.Warning);

                    return false;
                }
                PHTGL_SetSubReviewService.UpdatePHTGL_SetSubReview(_SetSubReview);
            }

            BLL.PHTGL_SetSubReview_Sch1Service.DeletePHTGL_SetSubReview_Sch1BySetSubReviewID(SetSubReviewID);
            JArray EditorArr = Grid1.GetMergedData();
            if (EditorArr.Count > 0)
            {
                Model.PHTGL_SetSubReview_Sch1 model = null;
                for (int i = 0; i < EditorArr.Count; i++)
                {
                    JObject objects = (JObject)EditorArr[i];
                    model = new Model.PHTGL_SetSubReview_Sch1();
                    model.ID = SQLHelper.GetNewID(typeof(Model.PHTGL_SetSubReview_Sch1));
                    model.SetSubReviewID = SetSubReviewID;
                    model.Company = objects["values"]["Company"].ToString();
                    model.ReviewResults = objects["values"]["ReviewResults"].ToString();
                    model.Remarks = objects["values"]["Remarks"].ToString();
                    BLL.PHTGL_SetSubReview_Sch1Service.AddPHTGL_SetSubReview_Sch1(model);
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
                var Bid = PHTGL_SetSubReviewService.GetPHTGL_SetSubReviewById(SetSubReviewID);
                Bid.State = Const.ContractReviewing;
                PHTGL_SetSubReviewService.UpdatePHTGL_SetSubReview(Bid);
                Dic_ApproveMan = PHTGL_SetSubReviewService.Get_DicApproveman(SetSubReviewID);
                var ApproveManModels = PHTGL_SetSubReviewService.GetApproveManModels(SetSubReviewID);
                //创建第一节点审批信息
                Model.PHTGL_Approve _Approve = new Model.PHTGL_Approve();
                _Approve.ApproveId = SQLHelper.GetNewID(typeof(Model.PHTGL_Approve));
                _Approve.ContractId = SetSubReviewID;
                _Approve.ApproveMan = ApproveManModels.Find(x => x.Number == 1).userid;
                _Approve.ApproveDate = "";
                _Approve.State = 0;
                _Approve.IsAgree = 0;
                _Approve.ApproveIdea = "";
                _Approve.ApproveType = ApproveManModels.Find(x => x.Number == 1).Rolename;
                _Approve.IsPushOa = 0;
                _Approve.ApproveForm = PHTGL_ApproveService.SetSubReview;

                BLL.PHTGL_ApproveService.AddPHTGL_Approve(_Approve);
                OAWebSevice.Pushoa();
                ShowNotify("提交成功！", MessageBoxIcon.Success);
                PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());

            }
        }
        #endregion

        #region 附件上传
        /// <summary>
        /// 上传附件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAttachUrl_Click(object sender, EventArgs e)
        {
             PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/SetSubReviewAttachUrl&menuId={1}", this.SetSubReviewID, BLL.Const.SetSubReview)));
        }

        protected void btnAttachUrl2_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/SetSubReviewAttachUrl&menuId={1}", this.SetSubReviewID + "report", BLL.Const.SetSubReview)));
        }
        #endregion

    }
}