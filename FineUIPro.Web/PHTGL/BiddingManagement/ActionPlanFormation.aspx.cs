using Aspose.Words;
using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;

namespace FineUIPro.Web.PHTGL.BiddingManagement
{
    public partial class ActionPlanFormation : PageBase
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
                this.DropState.DataValueField = "Value";
                DropState.DataTextField = "Text";
                DropState.DataSource = BLL.DropListService.GetState();
                DropState.DataBind();
                Funs.FineUIPleaseSelect(DropState);


                btnNew.OnClientClick = Window1.GetShowReference("ActionPlanFormationEdit.aspx", "实施计划编制") + "return false;";
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
            string strSql = @"SELECT  Act.ActionPlanID
                                  ,Act.ActionPlanCode
                                  ,Act.ProjectShortName as Name
                                  ,Act.EPCCode
                                  ,(CASE Act.State  
                                    WHEN  @ContractCreating     THEN '编制中'
                                    WHEN  @ContractCreat_Complete     THEN '编制完成'
                                    WHEN  @Contract_countersign     THEN '会签中'
                                    WHEN  @ContractReviewing        THEN '审批中'
                                    WHEN  @ContractReview_Complete  THEN '审批成功'
                                    WHEN  @ContractReview_Refuse    THEN '审批被拒'   END) AS State 
                                  ,U.UserName as CreatUser
                                  ,Act.CreateTime
                                  ,Act.ProjectID
                                  ,Act.ProjectName
                                  ,Act.Unit
                                  ,Act.BidProject
                                  ,Act.BidType
                                  ,Act.PriceType
                                  ,Act.BidPrice
                                  ,Act.ConstructionSite "
                            + @" FROM PHTGL_ActionPlanFormation  AS Act "
                            + @" LEFT JOIN Sys_User AS U ON U.UserId = Act.CreatUser  "
                            + @" LEFT JOIN Base_Project AS Pro ON Pro.ProjectId = Act.ProjectID  WHERE 1=1";

            List<SqlParameter> listStr = new List<SqlParameter>();
            listStr.Add(new SqlParameter("@ContractCreating", Const.ContractCreating.ToString()));
            listStr.Add(new SqlParameter("@ContractCreat_Complete", Const.ContractCreat_Complete.ToString()));
            listStr.Add(new SqlParameter("@Contract_countersign", Const.Contract_countersign));
            listStr.Add(new SqlParameter("@ContractReviewing", Const.ContractReviewing));
            listStr.Add(new SqlParameter("@ContractReview_Complete", Const.ContractReview_Complete));
            listStr.Add(new SqlParameter("@ContractReview_Refuse", Const.ContractReview_Refuse));

            if (!(this.CurrUser.UserId == Const.sysglyId))
            {
                strSql += " and  Act.ProjectID =@ProjectId";

                listStr.Add(new SqlParameter("@ProjectId", this.CurrUser.LoginProjectId));
            }
            if (!string .IsNullOrEmpty(txtActionPlanCode.Text))
            {
                strSql += " and Act.ActionPlanCode like @ActionPlanCode ";
                listStr.Add(new SqlParameter("@ActionPlanCode", "%"+ txtActionPlanCode.Text + "%"));

            }
          
            if (DropState.SelectedValue != Const._Null)
            {
                strSql += " and Act.State  =@State  ";
                listStr.Add(new SqlParameter("@State", DropState.SelectedValue));
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

        protected void btnRset_Click(object sender, EventArgs e)
        {
            txtActionPlanCode.Text = "";
             DropState.SelectedValue = "null";
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
        /// 行事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("ActionPlanFormationEdit.aspx?ActionPlanID={0}", id, "编辑 - ")));
                return;
            }

            if (e.CommandName == "export")
            {
                btnPrinter_Click(null, null);
            }

        }
        /// <summary>
        /// 表格单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_RowClick(object sender, GridRowClickEventArgs e)
        {
            string id = Grid1.SelectedRowID;
             var actReview = PHTGL_ActionPlanReviewService.GetPHTGL_ActionPlanReviewByActionPlanID(id);
            if (actReview!=null)
            {
                if (actReview.State == Const.ContractReview_Refuse)
                {
                    btnEditAgain.Hidden = false;

                }
                else
                {
                    btnEditAgain.Hidden = true;

                }

                if (actReview.State>= Const.ContractCreat_Complete)
                {
                    btnDelete.Hidden = true;
                }
                else
                {
                    var buttonList = CommonService.GetAllButtonList(CurrUser.LoginProjectId, CurrUser.UserId, Const.ActionPlanFormation);
                    if (buttonList.Count() > 0)
                    {
                        if (buttonList.Contains(Const.BtnDelete))
                        {
                            btnDelete.Hidden = false;
                        }
                    }

                }
               

            }
            
        }
        /// <summary>
        /// 编辑事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuEdit_Click(object sender, EventArgs e)
        {
            this.EditData();
        }
        /// <summary>
        /// 重新编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnEditAgain_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            string id = Grid1.SelectedRowID;

            var model = PHTGL_ActionPlanFormationService.GetPHTGL_ActionPlanFormationById(id);
            model.State = Const.ContractCreating;
            PHTGL_ActionPlanFormationService.UpdatePHTGL_ActionPlanFormation(model);
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("ActionPlanFormationEdit.aspx?ActionPlanID={0}", id, "编辑 - ")));
    
  
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
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("ActionPlanFormationEdit.aspx?ActionPlanID={0}", id, "编辑 - ")));
       
        }
        #endregion

        #region 删除
        /// <summary>
        /// 删除事件
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
                        var p = BLL.PHTGL_ActionPlanFormationService.GetPHTGL_ActionPlanFormationById(rowID);
                        if (p != null)
                        {
                            BLL.LogService.AddSys_Log(this.CurrUser, p.ProjectName, p.ActionPlanID, BLL.Const.ActionPlanFormation, BLL.Const.BtnDelete);
                            BLL.PHTGL_ActionPlanFormationService.DeletePHTGL_ActionPlanFormationById(rowID);
                            BLL.PHTGL_ActionPlanFormation_Sch1Service.DeletePHTGL_ActionPlanFormation_Sch1ById(rowID);
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
            var buttonList = CommonService.GetAllButtonList(CurrUser.LoginProjectId, CurrUser.UserId, Const.ActionPlanFormation);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(Const.BtnAdd))
                {
                    btnNew.Hidden = false;
                }
                if (buttonList.Contains(Const.BtnModify))
                {
                    btnEdit.Hidden = false;
                }
                if (buttonList.Contains(Const.BtnDelete))
                {
                    btnDelete.Hidden = false;
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

            Print(Id);
        }

        /// <summary>
        /// ActionPlanID
        /// </summary>
        /// <param name="Id"></param>
        public void Print(string Id)
        {
            string rootPath = Server.MapPath("~/");
            string initTemplatePath = string.Empty;
            string uploadfilepath = string.Empty;
            string newUrl = string.Empty;
            string filePath = string.Empty;
            initTemplatePath = "File\\Word\\PHTGL\\施工招标实施计划审批表.docx";
            uploadfilepath = rootPath + initTemplatePath;
            newUrl = uploadfilepath.Replace(".docx", string.Format("{0:yyyy-MM}", DateTime.Now) + ".docx");
            filePath = initTemplatePath.Replace(".docx", string.Format("{0:yyyy-MM}", DateTime.Now) + ".pdf");
            File.Copy(uploadfilepath, newUrl);
            ///更新书签
            var getFireWork = PHTGL_ActionPlanFormationService.GetPHTGL_ActionPlanFormationById(Id);
            var Act = PHTGL_ActionPlanReviewService.GetPHTGL_ActionPlanReviewByActionPlanID(Id);
            var list = PHTGL_ActionPlanFormation_Sch1Service.GetListPHTGL_ActionPlanFormation_Sch1ById(Id);
           
            Document doc = new Aspose.Words.Document(newUrl);
            Bookmark txtActionPlanCode=doc.Range.Bookmarks["ActionPlanCode"];
            Bookmark txtBidProject = doc.Range.Bookmarks["txtBidProject"];
            Bookmark txtName = doc.Range.Bookmarks["txtName"];
            Bookmark txtCreateTime = doc.Range.Bookmarks["CreateTime"];
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
            #region 附件表
             Dictionary<string, object> Dic_File = new Dictionary<string, object>();
            for (int i = 1; i < 14; i++)
            {
                string txtPlanningContent = "txtPlanningContent" + Convert.ToString(i);
                string txtRemarks = "txtRemarks" + Convert.ToString(i);
                string value_ActionPlan = "";
                string value_Remarks = "";
                 var model = list.Find(e => e.SortIndex == Convert.ToString(i));
                if (model != null)
                {
                    value_ActionPlan = model.ActionPlan;
                    value_Remarks = model.Remarks;
                }
                string[] key = { txtPlanningContent, txtRemarks };
                object[] value = { value_ActionPlan, value_Remarks };
                doc.MailMerge.Execute(key, value);

            }
           
             #endregion
            if (txtActionPlanCode != null)
            {
                if (getFireWork != null)
                {
                    txtActionPlanCode.Text = getFireWork.ActionPlanCode;
                }
            }
            if (txtName!=null)
            {
                if (getFireWork != null)
                {
                    txtName.Text = getFireWork.ProjectShortName;
                }

            }
            if (txtBidProject != null)
            {
                if (getFireWork != null)
                {

                    txtBidProject.Text = getFireWork.BidProject;
                }
            }
            if (txtCreateTime != null)
            {
                if (getFireWork != null)
                {
                    
                    txtCreateTime.Text = string.Format("{0:D}", getFireWork.CreateTime);
                }
            }
            if (Act!=null)
            {
                if (Act.State == Const.ContractReview_Complete)
                {

                    AsposeWordHelper.InsertImg(doc, rootPath, "Approval_Construction", Act.Approval_Construction, "");
                    AsposeWordHelper.InsertImg(doc, rootPath, "ConstructionManager", Act.ConstructionManager, "");
                    AsposeWordHelper.InsertImg(doc, rootPath, "DeputyGeneralManager", Act.DeputyGeneralManager, "");
                    AsposeWordHelper.InsertImg(doc, rootPath, "ProjectManager", Act.ProjectManager, "");
                }
            }
            

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
            if (!string .IsNullOrEmpty( getFireWork.AttachUrlContent))
            {
                var Path = newUrl.Replace(".docx", "编辑栏.docx"); //word文件保存路径
                AsposeWordHelper.HtmlIntoWord(getFireWork.AttachUrlContent, Path);
                Document doc2 = new Document(Path);

                doc2.FirstSection.PageSetup.SectionStart = SectionStart.OddPage;

                doc.AppendDocument(doc2, ImportFormatMode.UseDestinationStyles);
                File.Delete(Path);
            }
 
            doc.Save(newUrl);
            //生成PDF文件
            string pdfUrl = newUrl.Replace(".doc", ".pdf");
            Document doc1 = new Aspose.Words.Document(newUrl);
            //验证参数
            if (doc1 == null) { throw new Exception("Word文件无效"); }
            doc1.Save(pdfUrl, Aspose.Words.SaveFormat.Pdf);//还可以改成其它格式
            string fileName = Path.GetFileName(filePath).Replace("施工招标实施计划审批表", getFireWork.ActionPlanCode+ "施工分包实施计划");
            FileInfo info = new FileInfo(pdfUrl);
            long fileSize = info.Length;
            System.Web.HttpContext.Current.Response.Clear();
            System.Web.HttpContext.Current.Response.ContentType = "application/x-zip-compressed";
            System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8));
            System.Web.HttpContext.Current.Response.AddHeader("Content-Length", fileSize.ToString());
            System.Web.HttpContext.Current.Response.TransmitFile(pdfUrl, 0, fileSize);
            System.Web.HttpContext.Current.Response.Flush();
            System.Web.HttpContext.Current.Response.Close();
            File.Delete(newUrl);
            File.Delete(pdfUrl);
        }


         #endregion

    }
}
