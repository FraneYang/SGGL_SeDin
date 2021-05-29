﻿using Aspose.Words;
using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;

namespace FineUIPro.Web.PHTGL.BiddingManagement
{
	public partial class SetSubReview : PageBase
    {
        #region 加载
        /// <summary>
        /// 加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlPageSize.SelectedValue = Grid1.PageSize.ToString();
                btnNew.OnClientClick = Window1.GetShowReference("SetSubReviewEdit.aspx", "确定分包商审批创建") + "return false;";
                btnNew2.OnClientClick = Window1.GetShowReference("SetSubReviewEdit2.aspx", "确定分包商审批创建") + "return false;";

                this.DropState.DataValueField = "Value";
                DropState.DataTextField = "Text";
                DropState.DataSource = BLL.DropListService.GetState();
                DropState.DataBind();
                Funs.FineUIPleaseSelect(DropState);

                this.DropType.DataValueField = "Value";
                DropType.DataTextField = "Text";
                DropType.DataSource = BLL.PHTGL_SetSubReviewService.GetCreateType();
                DropType.DataBind();
                Funs.FineUIPleaseSelect(DropType);



                GetButtonPower();
                BindGrid();
            }
        }

        #endregion

        #region 数据绑定
        /// <summary>
        /// 数据绑定
        /// </summary>
        private void BindGrid()
        {
            string strSql = @"  select Sub.SetSubReviewID
                                      ,Sub.SetSubReviewCode
                                      ,BidDoc.BidDocumentsCode
                                      ,BidDoc.BidContent
                                      ,BidDoc.Bidding_StartTime
                                      ,(CASE Sub.State 
                                        WHEN @ContractCreating THEN '编制中'
                                        WHEN @ContractReviewing THEN '审批中'
                                        WHEN @ContractReview_Complete THEN '审批成功'
                                        WHEN @ContractReview_Refuse THEN '审批被拒'END) AS State
                                     ,(Case Sub.Type  
				                          WHEN @Type_MinPrice THEN '用于经评审的最低投标报价法'
                                          WHEN @Type_ConEvaluation THEN '综合评估法' END) AS Type
                                       ,U.UserName AS CreateUser
                                       ,Pro.ProjectName
                                       ,Pro.ProjectCode"
                               + @"  from  PHTGL_SetSubReview as  Sub "
                               + @" LEFT JOIN  PHTGL_BidApproveUserReview as BidUser on BidUser.ApproveUserReviewID = Sub.ApproveUserReviewID   "
                               + @" LEFT JOIN PHTGL_BidDocumentsReview as BidDoc  on BidDoc.BidDocumentsReviewId = BidUser.BidDocumentsReviewId "
                               + @" LEFT JOIN Sys_User AS U ON U.UserId = Sub.CreateUser    "
                               + @" LEFT JOIN Base_Project AS Pro ON Pro.ProjectId = BidUser.ProjectId WHERE 1 = 1  ";

            List<SqlParameter> listStr = new List<SqlParameter>();

            listStr.Add(new SqlParameter("@ContractCreating", Const.ContractCreating.ToString()));
            listStr.Add(new SqlParameter("@ContractReviewing", Const.ContractReviewing));
            listStr.Add(new SqlParameter("@ContractReview_Complete", Const.ContractReview_Complete));
            listStr.Add(new SqlParameter("@ContractReview_Refuse", Const.ContractReview_Refuse));
            listStr.Add(new SqlParameter("@Type_MinPrice", BLL.PHTGL_SetSubReviewService.Type_MinPrice));
            listStr.Add(new SqlParameter("@Type_ConEvaluation", PHTGL_SetSubReviewService.Type_ConEvaluation));

            if (!(this.CurrUser.UserId == Const.sysglyId))
            {
                strSql += " and  BidUser.ProjectId  =@ProjectId";

                listStr.Add(new SqlParameter("@ProjectId", this.CurrUser.LoginProjectId));
            }
            if (!string .IsNullOrEmpty(txtSetSubReviewCode.Text))
            {
                strSql += " and  Sub.SetSubReviewCode like @SetSubReviewCode";
                listStr.Add(new SqlParameter("@SetSubReviewCode","%"+txtSetSubReviewCode.Text+"%"));

            }
            if (!string .IsNullOrEmpty(txtBidDocumentsCode.Text))
            {
                strSql += " and  BidDoc.BidDocumentsCode like @BidDocumentsCode";
                listStr.Add(new SqlParameter("@BidDocumentsCode", "%" + txtBidDocumentsCode.Text + "%"));
            }
            if (DropState.SelectedValue!=Const._Null)
            {
                strSql += " and Sub.State =@State";
                listStr.Add(new SqlParameter("@State", DropState.SelectedValue));
            }
            if (DropType.SelectedValue != Const._Null)
            {
                strSql += " and  Sub.Type =@Type";
                listStr.Add(new SqlParameter("@Type", DropType.SelectedValue));
            }
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            Grid1.RecordCount = tb.Rows.Count;
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();
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
        /// 分页下拉选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid1.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
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
        protected void Grid1_RowCommand(object sender, GridCommandEventArgs e)
        {
            object[] keys = Grid1.DataKeys[e.RowIndex];
            string fileId = string.Empty;
            if (keys == null)
            {
                return;
            }
            else
            {
                fileId = keys[0].ToString();
            }
            if (e.CommandName == "LooK")
            {
                string id = fileId;
                var Sub = BLL.PHTGL_SetSubReviewService.GetPHTGL_SetSubReviewById(id);
                switch (Sub.Type)
                {
                    case PHTGL_SetSubReviewService.Type_MinPrice:
                        PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("SetSubReviewEdit2.aspx?SetSubReviewID={0}", Sub.SetSubReviewID, "审批 - ")));

                        break;
                    case PHTGL_SetSubReviewService.@Type_ConEvaluation:
                        PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("SetSubReviewEdit.aspx?SetSubReviewID={0}", Sub.SetSubReviewID, "审批 - ")));

                        break;
                }
                return;
            }

            if (e.CommandName == "export")
            { }

        }
        #endregion

        #region 关闭弹出窗体
        /// <summary>
        /// 关闭弹出窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
            BindGrid();
        }
        #endregion

        #region 查询
        /// <summary>
        /// 查询按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindGrid();
        }
        #endregion

        #region 编辑
        /// <summary>
        /// 双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            this.EditData();
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuEdit_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            string id = Grid1.SelectedRowID;
            var Sub = BLL.PHTGL_SetSubReviewService.GetPHTGL_SetSubReviewById(id);
            switch (Sub.Type)
            {
                case PHTGL_SetSubReviewService.Type_MinPrice:
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("SetSubReviewEdit2.aspx?SetSubReviewID={0}",Sub.SetSubReviewID, "审批 - ")));

                    break;
                case PHTGL_SetSubReviewService.@Type_ConEvaluation:
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("SetSubReviewEdit.aspx?SetSubReviewID={0}", Sub.SetSubReviewID, "审批 - ")));

                    break;
            }
        }

        /// <summary>
        /// 编辑数据方法
        /// </summary>
        private void EditData()
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            string id = Grid1.SelectedRowID;
            var Sub = BLL.PHTGL_SetSubReviewService.GetPHTGL_SetSubReviewById(id);

            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("SetSubReviewDetail.aspx?SetSubReviewID={0}", Sub.SetSubReviewID, "审批 - ")));

        }
        #endregion

        #region 删除
        /// <summary>
        /// 右键删除事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuDelete_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length > 0)
            {
                bool isShow = false;
                if (Grid1.SelectedRowIndexArray.Length == 1)
                {
                    isShow = true;
                }
                foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                {
                    string rowID = Grid1.DataKeys[rowIndex][0].ToString();
                    if (this.judgementDelete(rowID, isShow))
                    {
                        var p = BLL.PHTGL_SetSubReviewService.GetPHTGL_SetSubReviewById(rowID);
                        if (p != null)
                        {
                            BLL.LogService.AddSys_Log(this.CurrUser, p.SetSubReviewID, p.ApproveUserReviewID, BLL.Const.SetSubReview, BLL.Const.BtnDelete);
                            PHTGL_ApproveService.DeletePHTGL_ApproveBycontractId(rowID);
                            PHTGL_SetSubReviewService.DeletePHTGL_SetSubReviewById(rowID);
                        }
                    }
                }
                BindGrid();
                ShowNotify("删除数据成功!", MessageBoxIcon.Success);
            }
        }

        /// <summary>
        /// 判断是否可以删除
        /// </summary>
        /// <returns></returns>
        private bool judgementDelete(string id, bool isShow)
        {
            string content = string.Empty;

            if (string.IsNullOrEmpty(content))
            {
                return true;
            }
            else
            {
                if (isShow)
                {
                    Alert.ShowInTop(content);
                }
                return false;
            }
        }
        #endregion

        #region 获取按钮权限
        /// <summary>
        /// 获取按钮权限
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        private void GetButtonPower()
        {
            if (Request.Params["value"] == "0")
            {
                return;
            }
            var buttonList = CommonService.GetAllButtonList(CurrUser.LoginProjectId, CurrUser.UserId, Const.SetSubReview);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(Const.BtnAdd))
                {
                    btnNew.Hidden = false;
                    btnNew2.Hidden = false;

                }
                if (buttonList.Contains(Const.BtnModify))
                {
                    btnMenuEdit.Hidden = false;
                }
                if (buttonList.Contains(Const.BtnDelete))
                {
                    btnMenuDelete.Hidden = false;
                }
            }
        }
        #endregion

        #region 打印
        protected void btnPrinter_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            string Id = Grid1.SelectedRowID;

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
        #endregion
    }
}