﻿using BLL;
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
    public partial class SetSubReviewEdit : PageBase
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
                defaultObj.Add("Price_ReviewResults", "");
                defaultObj.Add("Skill_ReviewResults", "");
                defaultObj.Add("Business_ReviewResults", "");
                defaultObj.Add("Synthesize_ReviewResults", "");

                // 在第一行新增一条数据
                btnNew.OnClientClick = Grid1.GetAddNewRecordReference(defaultObj, false);
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
                var _SetSubReview = BLL.PHTGL_SetSubReviewService.GetPHTGL_SetSubReviewById (SetSubReviewID);
                var  BidUser= BLL.PHTGL_BidApproveUserReviewService.GetPHTGL_BidApproveUserReviewById(_SetSubReview.ApproveUserReviewID);
                var BidDocument = BLL.PHTGL_BidDocumentsReviewService.GetPHTGL_BidDocumentsReviewById(BidUser.BidDocumentsReviewId);
                if (_SetSubReview != null)
                {
                    txtSetSubReviewCode.Text = _SetSubReview.SetSubReviewCode;
                    DropBidCode.SelectedValue = _SetSubReview.ApproveUserReviewID;
                    txtProjectName.Text =ProjectService.GetProjectNameByProjectId(BidUser.ProjectId);
                    txtBidContent.Text = BidDocument.BidContent;
                    StartTime.SelectedDate = BidDocument.Bidding_StartTime;
                    this.DropConstructionManager.SelectedValue = _SetSubReview.ConstructionManager;
                    this.DropProjectManager.SelectedValue = _SetSubReview.ProjectManager;
                    this.DropApproval_Construction.SelectedValue = _SetSubReview.Approval_Construction;
                    this.DropDeputyGeneralManager.SelectedValue = _SetSubReview.DeputyGeneralManager;
                }   

            }
        }
        private void BindGrid()
        {
            string strSql = @"        SELECT ID
                                      ,SetSubReviewID
                                      ,Company
                                      ,Price_ReviewResults
                                      ,Skill_ReviewResults
                                      ,Business_ReviewResults
                                      ,Synthesize_ReviewResults
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

            Grid1.RecordCount = tb.Rows.Count;
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();
        }
        #endregion

        protected void Grid1_Sort(object sender, GridSortEventArgs e)
        {
            //Grid1.SortDirection = e.SortDirection;
            //Grid1.SortField = e.SortField;
 
        }
        protected void drpProjectId_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DropBidCode.SelectedValue!=Const._Null)
            {
                var BidUser = BLL.PHTGL_BidApproveUserReviewService.GetPHTGL_BidApproveUserReviewById(DropBidCode.SelectedValue);
                var BidDocument = BLL.PHTGL_BidDocumentsReviewService.GetPHTGL_BidDocumentsReviewById(BidUser.BidDocumentsReviewId);

               string projectcode = BLL.ProjectService.GetProjectCodeByProjectId(BidUser.ProjectId);
                if (string.IsNullOrEmpty(SetSubReviewID))
                {
                    this.txtSetSubReviewCode.Text = projectcode + ".000.C01.93-";

                }
                txtProjectName.Text = ProjectService.GetProjectNameByProjectId(BidUser.ProjectId);
                txtBidContent.Text = BidDocument.BidContent;
                StartTime.SelectedDate = BidDocument.Bidding_StartTime;
            }
           

        }
        private bool Save(int state)
        {
            if (DropBidCode.SelectedValue==Const._Null)
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
            _SetSubReview.Type = BLL.PHTGL_SetSubReviewService.Type_ConEvaluation;
            _SetSubReview.ConstructionManager = DropConstructionManager.SelectedValue;
            _SetSubReview.ProjectManager = DropProjectManager.SelectedValue;
            _SetSubReview.Approval_Construction = DropApproval_Construction.SelectedValue;
            _SetSubReview.DeputyGeneralManager =DropDeputyGeneralManager.SelectedValue;
            if (string .IsNullOrEmpty(SetSubReviewID))
            {
                _SetSubReview.SetSubReviewID = SQLHelper.GetNewID(typeof(Model.PHTGL_SetSubReview));
                SetSubReviewID = _SetSubReview.SetSubReviewID;
                PHTGL_SetSubReviewService.AddPHTGL_SetSubReview(_SetSubReview);

            }
            else
            {
                _SetSubReview.SetSubReviewID = SetSubReviewID;
                PHTGL_SetSubReviewService.UpdatePHTGL_SetSubReview(_SetSubReview);
            }

            BLL.PHTGL_SetSubReview_Sch2Service.DeletePHTGL_SetSubReview_Sch2BySetSubReviewID(SetSubReviewID);
             JArray EditorArr = Grid1.GetMergedData();
            if (EditorArr.Count > 0)
            {
                Model.PHTGL_SetSubReview_Sch2 model = null;
                for (int i = 0; i < EditorArr.Count; i++)
                {
                    JObject objects = (JObject)EditorArr[i];
                    model = new Model.PHTGL_SetSubReview_Sch2();
                    model.ID = SQLHelper.GetNewID(typeof(Model.PHTGL_SetSubReview_Sch2));
                    model.SetSubReviewID = SetSubReviewID;
                    model.Company = objects["values"]["Company"].ToString();
                    model.Price_ReviewResults = objects["values"]["Price_ReviewResults"].ToString();
                    model.Skill_ReviewResults = objects["values"]["Skill_ReviewResults"].ToString();
                    model.Business_ReviewResults = objects["values"]["Business_ReviewResults"].ToString();
                    model.Synthesize_ReviewResults = objects["values"]["Synthesize_ReviewResults"].ToString();
                    BLL.PHTGL_SetSubReview_Sch2Service.AddPHTGL_SetSubReview_Sch2(model);
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
                //创建第一节点审批信息
                Model.PHTGL_Approve _Approve = new Model.PHTGL_Approve();
                _Approve.ApproveId = SQLHelper.GetNewID(typeof(Model.PHTGL_Approve));
                _Approve.ContractId = SetSubReviewID;
                _Approve.ApproveMan = Dic_ApproveMan[1];
                _Approve.ApproveDate = "";
                _Approve.State = 0;
                _Approve.IsAgree = 0;
                _Approve.ApproveIdea = "";
                _Approve.ApproveType = "1";
                _Approve.ApproveForm = Request.Path;

                BLL.PHTGL_ApproveService.AddPHTGL_Approve(_Approve);
                ShowNotify("提交成功！", MessageBoxIcon.Success);
                PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());

            }
        }
        protected void btnAttachUrl_Click(object sender, EventArgs e)
        {
        }
        
    }
}