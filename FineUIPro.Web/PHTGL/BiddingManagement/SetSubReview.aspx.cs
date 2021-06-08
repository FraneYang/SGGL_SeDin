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
            if (!string.IsNullOrEmpty(txtSetSubReviewCode.Text))
            {
                strSql += " and  Sub.SetSubReviewCode like @SetSubReviewCode";
                listStr.Add(new SqlParameter("@SetSubReviewCode", "%" + txtSetSubReviewCode.Text + "%"));

            }
            if (DropState.SelectedValue != Const._Null)
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
                    case 0:
                        Alert.ShowInTop("请先编制审批类型！", MessageBoxIcon.Warning);
                        break;
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
        protected void btnQueryApprove_Click(object sender, EventArgs e)
        {
            this.EditData();
        }
        protected void btnRset_Click(object sender, EventArgs e)
        {
            txtSetSubReviewCode.Text = "";
            DropState.SelectedValue = "null";
            DropType.SelectedValue = "null";
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
                case 0:
                    Alert.ShowInTop("请先编制审批类型！", MessageBoxIcon.Warning);

                    break;
                case PHTGL_SetSubReviewService.Type_MinPrice:
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("SetSubReviewEdit2.aspx?SetSubReviewID={0}", Sub.SetSubReviewID, "审批 - ")));

                    break;
                case PHTGL_SetSubReviewService.@Type_ConEvaluation:
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("SetSubReviewEdit.aspx?SetSubReviewID={0}", Sub.SetSubReviewID, "审批 - ")));

                    break;
            }
        }
        protected void btnNew_Click(object sender, EventArgs e)
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
                case 0:
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("SetSubReviewEdit.aspx?SetSubReviewID={0}", Sub.SetSubReviewID, "审批 - ")));

                    break;
                case PHTGL_SetSubReviewService.Type_MinPrice:
                    Alert.ShowInTop("审批类型不是综合评估法无法编辑！", MessageBoxIcon.Warning);

                    break;
                case PHTGL_SetSubReviewService.@Type_ConEvaluation:
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("SetSubReviewEdit.aspx?SetSubReviewID={0}", Sub.SetSubReviewID, "审批 - ")));

                    break;
            }

        }
        protected void btnNew2_Click(object sender, EventArgs e)
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
                case 0:
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("SetSubReviewEdit2.aspx?SetSubReviewID={0}", Sub.SetSubReviewID, "审批 - ")));

                    break;
                case PHTGL_SetSubReviewService.Type_MinPrice:
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("SetSubReviewEdit2.aspx?SetSubReviewID={0}", Sub.SetSubReviewID, "审批 - ")));
                    break;
                case PHTGL_SetSubReviewService.@Type_ConEvaluation:
                    Alert.ShowInTop("审批类型不是经评审的最低投标报价法无法编辑！", MessageBoxIcon.Warning);
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
                    //btnMenuEdit.Hidden = false;
                }
                if (buttonList.Contains(Const.BtnDelete))
                {
                    btnMenuDelete.Hidden = false;
                }
            }
        }
        #endregion

        #region 打印
        /// <summary>
        /// SetSubReviewID
        /// </summary>
        /// <param name="Id"></param>
        public void Print(string Id)
        {
            string rootPath = Server.MapPath("~/");
            string initTemplatePath = string.Empty;
            string uploadfilepath = string.Empty;
            string newUrl = string.Empty;
            string filePath = string.Empty;
            string strSql = "";
            var getFireWork = PHTGL_SetSubReviewService.GetPHTGL_SetSubReviewById(Id);
            var BidUser = PHTGL_BidApproveUserReviewService.GetPHTGL_BidApproveUserReviewById(getFireWork.ApproveUserReviewID);
            var BidDoc = PHTGL_BidDocumentsReviewService.GetPHTGL_BidDocumentsReviewById(BidUser.BidDocumentsReviewId);

            switch (getFireWork.Type)
            {
                case PHTGL_SetSubReviewService.Type_ConEvaluation:
                    initTemplatePath = "File\\Word\\PHTGL\\确定分包商审批表（用于综合评估法）.docx";
                    strSql = @"  SELECT     
                                           Sch2.Company as Company
                                           ,Sch2.Price_ReviewResults as Price_ReviewResults
                                           ,Sch2.Skill_ReviewResults as Skill_ReviewResults
                                           ,Sch2.Business_ReviewResults as Business_ReviewResults
                                           ,Sch2.Synthesize_ReviewResults as Synthesize_ReviewResults "
                         + @" FROM PHTGL_SetSubReview_Sch2 AS Sch2 "
                          + @"where 1=1 AND SetSubReviewID = @SetSubReviewID ";
                    break;
                case PHTGL_SetSubReviewService.Type_MinPrice:
                    initTemplatePath = "File\\Word\\PHTGL\\确定分包商审批表（用于经评审的最低投标报价法）.docx";
                    strSql = @"  SELECT     
                                            Sch1.Company as Company
                                           ,Sch1.ReviewResults as ReviewResults "
                          + @" FROM PHTGL_SetSubReview_Sch1 AS Sch1 "
                          + @"where 1=1 AND SetSubReviewID = @SetSubReviewID ";
                    break;
                default: /* 可选的 */
                    Alert.ShowInTop("请先编制审批类型！", MessageBoxIcon.Warning);
                    return;
            }
            uploadfilepath = rootPath + initTemplatePath;
            newUrl = uploadfilepath.Replace(".docx", string.Format("{0:yyyy-MM}", DateTime.Now) + ".docx");
            filePath = initTemplatePath.Replace(".docx", string.Format("{0:yyyy-MM}", DateTime.Now) + ".pdf");
            File.Copy(uploadfilepath, newUrl);

            List<SqlParameter> listStr = new List<SqlParameter>();
            listStr.Add(new SqlParameter("@SetSubReviewID", Id));
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            tb.TableName = "Table";
            Document doc = new Aspose.Words.Document(newUrl);
            doc.MailMerge.ExecuteWithRegions(tb);

            var model_ConstructionManager = PHTGL_ApproveService.GetPHTGL_ApproveByContractIdandUserId(Id, getFireWork.ConstructionManager);
            var model_ProjectManager = PHTGL_ApproveService.GetPHTGL_ApproveByContractIdandUserId(Id, getFireWork.ProjectManager);
            var model_Approval_Construction = PHTGL_ApproveService.GetPHTGL_ApproveByContractIdandUserId(Id, getFireWork.Approval_Construction);
            var model_DeputyGeneralManager = PHTGL_ApproveService.GetPHTGL_ApproveByContractIdandUserId(Id, getFireWork.DeputyGeneralManager);

            Dictionary<string, object> Dic_File = new Dictionary<string, object>();
            Dic_File.Add("txtSetSubReviewCode", getFireWork.SetSubReviewCode);
            Dic_File.Add("txtBidDocumentsCode", BidDoc.BidDocumentsCode);
            Dic_File.Add("txtProjectName", ProjectService.GetProjectNameByProjectId(BidDoc.ProjectId));
            Dic_File.Add("txtBidContent", BidDoc.BidContent);
            Dic_File.Add("txtBidding_StartTime", string.Format("{0:D}", BidDoc.Bidding_StartTime));

            if (getFireWork.State == Const.ContractReview_Complete)
            {

                Dic_File.Add("txtConstructionManagerIdea", model_ConstructionManager.ApproveIdea);
                Dic_File.Add("ConstructionManagerTime", string.Format("{0:D}", DateTime.Parse(model_ConstructionManager.ApproveDate)));

                Dic_File.Add("txtApproval_ConstructionIdea", model_Approval_Construction.ApproveIdea);
                Dic_File.Add("Approval_ConstructionTime", string.Format("{0:D}", DateTime.Parse(model_Approval_Construction.ApproveDate)));

                Dic_File.Add("ProjectManagerIdea", model_ProjectManager.ApproveIdea);
                Dic_File.Add("ProjectManagerTime", string.Format("{0:D}", DateTime.Parse(model_ProjectManager.ApproveDate)));

                Dic_File.Add("txtDeputyGeneralManagerIdea", model_DeputyGeneralManager.ApproveIdea);
                Dic_File.Add("DeputyGeneralManagerTime", string.Format("{0:D}", DateTime.Parse(model_DeputyGeneralManager.ApproveDate)));

                AsposeWordHelper.InsertImg(doc, rootPath, "Approval_Construction", getFireWork.Approval_Construction, "");
                AsposeWordHelper.InsertImg(doc, rootPath, "ConstructionManager", getFireWork.ConstructionManager, "");
                AsposeWordHelper.InsertImg(doc, rootPath, "DeputyGeneralManager", getFireWork.DeputyGeneralManager, "");
                AsposeWordHelper.InsertImg(doc, rootPath, "ProjectManager", getFireWork.ProjectManager, "");

            }
            else
            {
                Dic_File.Add("txtConstructionManagerIdea", "");
                Dic_File.Add("ConstructionManagerTime", "  年  月  日");

                Dic_File.Add("txtApproval_ConstructionIdea", "");
                Dic_File.Add("Approval_ConstructionTime", "  年  月  日");

                Dic_File.Add("ProjectManagerIdea", "");
                Dic_File.Add("ProjectManagerTime", "  年  月  日");

                Dic_File.Add("txtDeputyGeneralManagerIdea", "");
                Dic_File.Add("DeputyGeneralManagerTime", "  年  月  日");

            }
            foreach (var item in Dic_File)
            {
                string[] key = { item.Key };
                object[] value = { item.Value };
                doc.MailMerge.Execute(key, value);
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
        #endregion



    }
}