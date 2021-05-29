﻿using Aspose.Words;
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
                this.btnClose.OnClientClick = ActiveWindow.GetHideRefreshReference();

                 //获取审批人字典
                Dic_ApproveMan = PHTGL_SetSubReviewService.Get_DicApproveman(SetSubReviewID);
                //获取当前登录人审批信息
                pHTGL_Approve = BLL.PHTGL_ApproveService.GetPHTGL_ApproveByUserId(SetSubReviewID, this.CurrUser.UserId);
                //数据加载
                Bind();
                var _SetSubReview = BLL.PHTGL_SetSubReviewService.GetPHTGL_SetSubReviewById(SetSubReviewID);

                if (pHTGL_Approve != null)
                {
                    txtApproveType.Text = pHTGL_Approve.ApproveType;
                    BindGrid();
                    BindSchGrid();
                    if (pHTGL_Approve.State == 1)
                    {
                        btnSave.Hidden = true;
                    }
                }
                else if (this.CurrUser.UserId == Const.sysglyId || this.CurrUser.UserId == Const.hfnbdId)
                {
                    txtApproveType.Text = "管理员";
                    BindGrid();
                    BindSchGrid();
                    btnSave.Hidden = true;
                }
                else if (_SetSubReview.CreateUser == this.CurrUser.UserId)
                {
                    txtApproveType.Text = "创建者";
                    BindGrid();
                    BindSchGrid();
                    btnSave.Hidden = true;
                }
                else
                {
                    btnSave.Hidden = true;
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
                    DropBidCode.SelectedValue = _SetSubReview.ApproveUserReviewID;
                    txtProjectName.Text = ProjectService.GetProjectNameByProjectId(BidUser.ProjectId);
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
                             + @"   left join Sys_User AS U ON U.UserId = App.ApproveMan WHERE 1=1   and App.IsAgree <>0 and app.ContractId= @ContractId";
            List<SqlParameter> listStr = new List<SqlParameter>();
            listStr.Add(new SqlParameter("@ContractId", SetSubReviewID));

            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            Grid1.RecordCount = tb.Rows.Count;
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();
        }
        #endregion

        protected void btnAttachUrl_Click(object sender, EventArgs e)
        {
            Print(SetSubReviewID);
        }
        public void Print(string Id)
        {
            string rootPath = Server.MapPath("~/");
            string initTemplatePath = string.Empty;
            string uploadfilepath = string.Empty;
            string newUrl = string.Empty;
            string filePath = string.Empty;
            initTemplatePath = "File\\Word\\PHTGL\\施工招标实施计划及招标文件审批表.docx";
            uploadfilepath = rootPath + initTemplatePath;
            newUrl = uploadfilepath.Replace(".docx", string.Format("{0:yyyy-MM}", DateTime.Now) + ".docx");
            filePath = initTemplatePath.Replace(".docx", string.Format("{0:yyyy-MM}", DateTime.Now) + ".pdf");
            File.Copy(uploadfilepath, newUrl);
            ///更新书签
            var getFireWork = PHTGL_ActionPlanFormationService.GetPHTGL_ActionPlanFormationById(Id);
            Document doc = new Aspose.Words.Document(newUrl);

            Bookmark txtProjectName = doc.Range.Bookmarks["txtProjectName"];
            Bookmark txtUnit = doc.Range.Bookmarks["txtUnit"];
            Bookmark txtConstructionSite = doc.Range.Bookmarks["txtConstructionSite"];
            Bookmark txtBiddingProjectScope = doc.Range.Bookmarks["txtBiddingProjectScope"];
            Bookmark txtBiddingProjectContent = doc.Range.Bookmarks["txtBiddingProjectContent"];
            Bookmark txtTimeRequirements = doc.Range.Bookmarks["txtTimeRequirements"];
            Bookmark txtQualityRequirement = doc.Range.Bookmarks["txtQualityRequirement"];
            Bookmark txtHSERequirement = doc.Range.Bookmarks["txtHSERequirement"];
            Bookmark txtTechnicalRequirement = doc.Range.Bookmarks["txtTechnicalRequirement"];
            Bookmark txtCurrentRequirement = doc.Range.Bookmarks["txtCurrentRequirement"];
            Bookmark txtSub_Selection = doc.Range.Bookmarks["txtSub_Selection"];
            Bookmark txtBid_Selection = doc.Range.Bookmarks["txtBid_Selection"];
            Bookmark txtContractingMode_Select = doc.Range.Bookmarks["txtContractingMode_Select"];
            Bookmark txtPriceMode_Select = doc.Range.Bookmarks["txtPriceMode_Select"];
            Bookmark txtMaterialsDifferentiate = doc.Range.Bookmarks["txtMaterialsDifferentiate"];
            Bookmark txtImportExplain = doc.Range.Bookmarks["txtImportExplain"];
            Bookmark txtShortNameList = doc.Range.Bookmarks["txtShortNameList"];
            Bookmark txtEvaluationMethods = doc.Range.Bookmarks["txtEvaluationMethods"];
            Bookmark txtEvaluationPlan = doc.Range.Bookmarks["txtEvaluationPlan"];
            Bookmark txtBiddingMethods_Select = doc.Range.Bookmarks["txtBiddingMethods_Select"];
            Bookmark txtSchedulePlan = doc.Range.Bookmarks["txtSchedulePlan"];

            if (txtProjectName != null)
            {
                if (getFireWork != null)
                {
                    txtProjectName.Text = getFireWork.ProjectName;
                }
            }
            if (txtUnit != null)
            {
                if (getFireWork != null)
                {
                    txtUnit.Text = getFireWork.Unit;

                }
            }
            if (txtConstructionSite != null)
            {
                if (getFireWork != null)
                {
                    txtConstructionSite.Text = getFireWork.ConstructionSite;

                }
            }
            if (txtBiddingProjectScope != null)
            {
                if (getFireWork != null)
                {
                    txtBiddingProjectScope.Text = getFireWork.BiddingProjectScope;

                }
            }
            if (txtBiddingProjectContent != null)
            {
                if (getFireWork != null)
                {
                    txtBiddingProjectContent.Text = getFireWork.BiddingProjectContent;

                }
            }
            if (txtTimeRequirements != null)
            {
                if (getFireWork != null)
                {
                    txtTimeRequirements.Text = getFireWork.TimeRequirements;

                }

            }
            if (txtQualityRequirement != null)
            {
                if (getFireWork != null)
                {
                    txtQualityRequirement.Text = getFireWork.QualityRequirement;

                }

            }
            if (txtHSERequirement != null)
            {
                if (getFireWork != null)
                {
                    txtHSERequirement.Text = getFireWork.HSERequirement;

                }

            }
            if (txtTechnicalRequirement != null)
            {
                if (getFireWork != null)
                {
                    txtTechnicalRequirement.Text = getFireWork.TechnicalRequirement;

                }

            }
            if (txtCurrentRequirement != null)
            {
                if (getFireWork != null)
                {
                    txtCurrentRequirement.Text = getFireWork.CurrentRequirement;

                }

            }
            if (txtSub_Selection != null)
            {
                if (getFireWork != null)
                {
                    txtSub_Selection.Text = getFireWork.Sub_Selection;

                }

            }
            if (txtBid_Selection != null)
            {
                if (getFireWork != null)
                {
                    txtBid_Selection.Text = getFireWork.Bid_Selection;

                }

            }
            if (txtContractingMode_Select != null)
            {
                if (getFireWork != null)
                {
                    txtContractingMode_Select.Text = getFireWork.ContractingMode_Select;

                }

            }
            if (txtPriceMode_Select != null)
            {
                if (getFireWork != null)
                {
                    txtPriceMode_Select.Text = getFireWork.PriceMode_Select;

                }

            }
            if (txtMaterialsDifferentiate != null)
            {
                if (getFireWork != null)
                {
                    txtMaterialsDifferentiate.Text = getFireWork.MaterialsDifferentiate;

                }

            }
            if (txtImportExplain != null)
            {
                if (getFireWork != null)
                {
                    txtImportExplain.Text = getFireWork.ImportExplain;

                }

            }
            if (txtShortNameList != null)
            {
                if (getFireWork != null)
                {
                    txtShortNameList.Text = getFireWork.ShortNameList;

                }

            }
            if (txtEvaluationMethods != null)
            {
                if (getFireWork != null)
                {
                    txtEvaluationMethods.Text = getFireWork.EvaluationMethods;

                }

            }
            if (txtEvaluationPlan != null)
            {
                if (getFireWork != null)
                {
                    txtEvaluationPlan.Text = getFireWork.EvaluationPlan;

                }

            }
            if (txtBiddingMethods_Select != null)
            {
                if (getFireWork != null)
                {
                    txtBiddingMethods_Select.Text = getFireWork.BiddingMethods_Select;

                }

            }
            if (txtSchedulePlan != null)
            {
                if (getFireWork != null)
                {
                    txtSchedulePlan.Text = getFireWork.SchedulePlan;

                }

            }





            doc.Save(newUrl);
            //生成PDF文件
            string pdfUrl = newUrl.Replace(".doc", ".pdf");
            Document doc1 = new Aspose.Words.Document(newUrl);
            //验证参数
            if (doc1 == null) { throw new Exception("Word文件无效"); }
            doc1.Save(pdfUrl, Aspose.Words.SaveFormat.Pdf);//还可以改成其它格式
            string fileName = Path.GetFileName(filePath);
            FileInfo info = new FileInfo(pdfUrl);
            long fileSize = info.Length;
            Response.Clear();
            Response.ContentType = "application/x-zip-compressed";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8));
            Response.AddHeader("Content-Length", fileSize.ToString());
            Response.TransmitFile(pdfUrl, 0, fileSize);
            Response.Flush();
            Response.Close();
            File.Delete(newUrl);
            File.Delete(pdfUrl);
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
                        //  _Approve.ApproveId = SQLHelper.GetNewID(typeof(Model.PHTGL_Approve));
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
            var _SetSubReview = BLL.PHTGL_SetSubReviewService.GetPHTGL_SetSubReviewById(SetSubReviewID);
            _SetSubReview.State = state;
            PHTGL_SetSubReviewService.UpdatePHTGL_SetSubReview(_SetSubReview);
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