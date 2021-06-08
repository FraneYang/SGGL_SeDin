using Aspose.Words;
using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
using System.Data.SqlClient;
using System.IO;
using System.Linq;

namespace FineUIPro.Web.PHTGL.BiddingManagement
{
    public partial class ApproveUserReview : PageBase
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

                btnNew.OnClientClick = Window1.GetShowReference("ApproveUserReviewEdit.aspx", "评标小组名单审批创建") + "return false;";
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
            string strSql = @"SELECT   Bid.ApproveUserReviewID
                                      ,BidDoc.BidDocumentsCode
                                      ,Acp.ActionPlanCode
                                      ,Bid.ProjectId
	                                  ,Pro.ProjectName
                                      ,Pro.ProjectCode
                                      ,Bid.BidProject
	                                  ,U.UserName AS CreateUser
                                      ,(CASE Bid.State 
                                        WHEN @ContractCreating THEN '编制中'
                                        WHEN @ContractCreat_Complete THEN '编制完成'
                                        WHEN @ContractReviewing THEN '审批中'
                                        WHEN @ContractReview_Complete THEN '审批成功'
                                        WHEN @ContractReview_Refuse THEN '审批被拒'END) AS State
                                      ,Bid.ConstructionManager
                                      ,Bid.ProjectManager
                                      ,Bid.Approval_Construction
                                      ,Bid.DeputyGeneralManager"
                            + @" from  PHTGL_BidApproveUserReview AS Bid "
                            + @" LEFT JOIN PHTGL_BidDocumentsReview AS BidDoc ON BidDoc.BidDocumentsReviewId =Bid.BidDocumentsReviewId"
                            + @" LEFT JOIN PHTGL_ActionPlanFormation AS Acp ON Acp.ActionPlanID =Bid.ActionPlanID"
                            + @" LEFT JOIN Sys_User AS U ON U.UserId =Bid.CreateUser  "
                            + @" LEFT JOIN Base_Project AS Pro ON Pro.ProjectId = Bid.ProjectId WHERE 1=1";

            List<SqlParameter> listStr = new List<SqlParameter>();

            listStr.Add(new SqlParameter("@ContractCreating", Const.ContractCreating.ToString()));
            listStr.Add(new SqlParameter("@ContractCreat_Complete", Const.ContractCreat_Complete));
            listStr.Add(new SqlParameter("@ContractReviewing", Const.ContractReviewing));
            listStr.Add(new SqlParameter("@ContractReview_Complete", Const.ContractReview_Complete));
            listStr.Add(new SqlParameter("@ContractReview_Refuse", Const.ContractReview_Refuse));

            if (!(this.CurrUser.UserId == Const.sysglyId))
            {
                strSql += " and  Bid.ProjectID =@ProjectId";

                listStr.Add(new SqlParameter("@ProjectId", this.CurrUser.LoginProjectId));
            }
            if (!string.IsNullOrEmpty(txtBidDocumentsCode.Text))
            {
                strSql += " and BidDoc.BidDocumentsCode like @BidDocumentsCode ";
                listStr.Add(new SqlParameter("@BidDocumentsCode", "%" + txtBidDocumentsCode.Text + "%"));

            }
            if (DropState.SelectedValue != Const._Null)
            {
                strSql += " and Bid.State   =@State  ";
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
                var Bid = BLL.PHTGL_BidApproveUserReviewService.GetPHTGL_BidApproveUserReviewById(id);
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("ApproveUserReviewEdit.aspx?ApproveUserReviewID={0}", Bid.ApproveUserReviewID, "审批 - ")));
                return;
            }

            if (e.CommandName == "export")
            { }
           
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
            txtBidDocumentsCode.Text = "";
            DropState.SelectedValue = "null";
            BindGrid();
        }
        protected void btnQueryApprove_Click(object sender, EventArgs e)
        {
            this.EditData();
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
            var Bid = BLL.PHTGL_BidApproveUserReviewService.GetPHTGL_BidApproveUserReviewById (id);
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("ApproveUserReviewEdit.aspx?ApproveUserReviewID={0}", Bid.ApproveUserReviewID, "审批 - ")));
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
            var Bid = BLL.PHTGL_BidApproveUserReviewService.GetPHTGL_BidApproveUserReviewById(id);
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("ApproveUserReviewDetail.aspx?ApproveUserReviewID={0}", Bid.ApproveUserReviewID, "审批 - ")));

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
                        var p = BLL.PHTGL_BidApproveUserReviewService.GetPHTGL_BidApproveUserReviewById(rowID);
                        if (p != null)
                        {
                            BLL.LogService.AddSys_Log(this.CurrUser, p.ApproveUserReviewID, p.ProjectId, BLL.Const.ApproveUserReview, BLL.Const.BtnDelete);
                            PHTGL_ApproveService.DeletePHTGL_ApproveBycontractId(rowID);
                            PHTGL_BidApproveUserReviewService.DeletePHTGL_BidApproveUserReviewById(rowID);
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
            var buttonList = CommonService.GetAllButtonList(CurrUser.LoginProjectId, CurrUser.UserId, Const.ApproveUserReview);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(Const.BtnAdd))
                {
                //   btnNew.Hidden = false;
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
        /// <summary>
        /// ApproveUserReviewById
        /// </summary>
        /// <param name="id"></param>
        public void Print(string Id)
        {
            string rootPath = Server.MapPath("~/");
            string initTemplatePath = string.Empty;
            string uploadfilepath = string.Empty;
            string newUrl = string.Empty;
            string filePath = string.Empty;
            initTemplatePath = "File\\Word\\PHTGL\\施工招标评标小组名单审批表.docx";
            uploadfilepath = rootPath + initTemplatePath;
            newUrl = uploadfilepath.Replace(".docx", string.Format("{0:yyyy-MM}", DateTime.Now) + ".docx");
            filePath = initTemplatePath.Replace(".docx", string.Format("{0:yyyy-MM}", DateTime.Now) + ".pdf");
            File.Copy(uploadfilepath, newUrl);
            ///更新书签
            var getFireWork = PHTGL_BidApproveUserReviewService.GetPHTGL_BidApproveUserReviewById(Id);
            var Bid = PHTGL_BidDocumentsReviewService.GetPHTGL_BidDocumentsReviewById(getFireWork.BidDocumentsReviewId);
            #region 评标小组成员名单
            string strSql = @"  SELECT    Number = row_number() over(order by ID)
                                           ,U.UserName as Name
                                          ,APP.ApproveUserSpecial as Special
                                          ,APP.ApproveUserUnit as Unit
                                          ,APP.Remarks as Remarks"
                          + @" FROM PHTGL_BidApproveUserReview_Sch1 AS APP "
                          + @" LEFT JOIN Sys_User AS U ON U.UserId =APP.ApproveUserName  "
                          + @"where 1=1 AND ApproveUserReviewID = @ApproveUserReviewID ";
            List<SqlParameter> listStr = new List<SqlParameter>();
            listStr.Add(new SqlParameter("@ApproveUserReviewID", Id));

            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            tb.TableName = "Table";
            Document doc = new Aspose.Words.Document(newUrl);
            doc.MailMerge.ExecuteWithRegions(tb);
            #endregion
            Dictionary<string, object> Dic_File = new Dictionary<string, object>();
            Dic_File.Add("txtProjectName", BLL.ProjectService.GetProjectNameByProjectId(getFireWork.ProjectId));
            Dic_File.Add("txtBidProject", getFireWork.BidProject);
            Dic_File.Add("txtBidDocumentCode", Bid.BidDocumentsCode);
            foreach (var item in Dic_File)
            {
                string[] key = { item.Key };
                object[] value = { item.Value };
                doc.MailMerge.Execute(key, value);

            }
  
            if (getFireWork.State == Const.ContractReview_Complete)
            {
                AsposeWordHelper.InsertImg(doc, rootPath, "Approval_Construction", getFireWork.Approval_Construction, "");
                AsposeWordHelper.InsertImg(doc, rootPath, "ConstructionManager", getFireWork.ConstructionManager, "");
                AsposeWordHelper.InsertImg(doc, rootPath, "DeputyGeneralManager", getFireWork.DeputyGeneralManager, "");
                AsposeWordHelper.InsertImg(doc, rootPath, "ProjectManager", getFireWork.ProjectManager, "");

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