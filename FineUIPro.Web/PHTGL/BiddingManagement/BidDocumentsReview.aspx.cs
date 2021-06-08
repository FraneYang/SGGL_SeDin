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
    public partial class BidDocumentsReview : PageBase
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

                btnNew.OnClientClick = Window1.GetShowReference("BidDocumentsReviewEdit.aspx", "施工计划审批创建") + "return false;";
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
            string strSql = @"SELECT   Bid.BidDocumentsReviewId
                                      ,Bid.ProjectId
                                      ,Pro.ProjectName
                                      ,Pro.ProjectCode
                                      ,Acp.ActionPlanCode
                                      ,(CASE Bid.State 
                                        WHEN @ContractCreating THEN '编制中'
                                        WHEN @ContractCreat_Complete THEN '编制完成'
                                        WHEN @ContractReviewing THEN '审批中'
                                        WHEN @ContractReview_Complete THEN '审批成功'
                                        WHEN @ContractReview_Refuse THEN '审批被拒'END) AS State
                                      ,Bid.BidContent
                                      ,Bid.BidType
                                      ,Bid.BidDocumentsName
                                      ,Bid.BidDocumentsCode
                                      ,Bid.Bidding_SendTime
                                      ,Bid.Bidding_StartTime
                                      ,Bid.url
                                      ,U.UserName AS CreateUser
                                      ,Bid.CreatTime"
                            + @"  FROM  PHTGL_BidDocumentsReview AS Bid "
                            + @" LEFT JOIN Sys_User AS U ON U.UserId =Bid.CreateUser  "
                            + @" LEFT JOIN PHTGL_ActionPlanFormation AS Acp ON Acp.ActionPlanID =Bid.ActionPlanID  "
                            + @" LEFT JOIN Base_Project AS Pro ON Pro.ProjectId = Bid.ProjectId WHERE 1=1";

            List<SqlParameter> listStr = new List<SqlParameter>();

            listStr.Add(new SqlParameter("@ContractCreating", Const.ContractCreating.ToString ()));
            listStr.Add(new SqlParameter("@ContractCreat_Complete", Const.ContractCreat_Complete.ToString ()));
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
                strSql += " and Bid.BidDocumentsCode like @BidDocumentsCode ";
                listStr.Add(new SqlParameter("@BidDocumentsCode", "%" + txtBidDocumentsCode.Text + "%"));

            }
           
            if (DropState.SelectedValue != Const._Null)
            {
                strSql += " and Bid.State  =@State  ";
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
                var Bid = BLL.PHTGL_BidDocumentsReviewService.GetPHTGL_BidDocumentsReviewById(id);
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("BidDocumentsReviewEdit.aspx?BidDocumentsReviewId={0}", Bid.BidDocumentsReviewId, "审批 - ")));
                return;
            }
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
             txtBidDocumentsCode.Text = "";
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
            var Bid = BLL.PHTGL_BidDocumentsReviewService.GetPHTGL_BidDocumentsReviewById(id);
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("BidDocumentsReviewEdit.aspx?BidDocumentsReviewId={0}", Bid.BidDocumentsReviewId, "审批 - ")));
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
            var Bid = BLL.PHTGL_BidDocumentsReviewService.GetPHTGL_BidDocumentsReviewById(id);
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("BidDocumentsReviewDetail.aspx?BidDocumentsReviewId={0}", Bid.BidDocumentsReviewId, "审批 - ")));

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
                        var p = BLL.PHTGL_BidDocumentsReviewService.GetPHTGL_BidDocumentsReviewById(rowID);
                        if (p != null)
                        {
                            BLL.LogService.AddSys_Log(this.CurrUser, p.BidDocumentsReviewId, p.ProjectId, BLL.Const.BidDocumentsReviewIdMenuid, BLL.Const.BtnDelete);
                            PHTGL_ApproveService.DeletePHTGL_ApproveBycontractId(rowID);
                            PHTGL_BidDocumentsReviewService.DeletePHTGL_BidDocumentsReviewById(rowID);
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
            var buttonList = CommonService.GetAllButtonList(CurrUser.LoginProjectId, CurrUser.UserId, Const.BidDocumentsReviewIdMenuid);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(Const.BtnAdd))
                {
                  //  btnNew.Hidden = false;
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
        /// BidDocumentsReviewId
        /// </summary>
        /// <param name="Id"></param>
        public void Print(string Id)
        {
            string rootPath = Server.MapPath("~/");
            string initTemplatePath = string.Empty;
            string uploadfilepath = string.Empty;
            string newUrl = string.Empty;
            string filePath = string.Empty;
            initTemplatePath = "File\\Word\\PHTGL\\招标文件审批表.docx";
            uploadfilepath = rootPath + initTemplatePath;
            newUrl = uploadfilepath.Replace(".docx", string.Format("{0:yyyy-MM}", DateTime.Now) + ".docx");
            filePath = initTemplatePath.Replace(".docx", string.Format("{0:yyyy-MM}", DateTime.Now) + ".pdf");
            File.Copy(uploadfilepath, newUrl);
            Document doc = new Aspose.Words.Document(newUrl);

            ///更新书签
            var getFireWork = PHTGL_BidDocumentsReviewService.GetPHTGL_BidDocumentsReviewById(Id);

            var model_ConstructionManager = PHTGL_ApproveService.GetPHTGL_ApproveByContractIdandUserId(Id, getFireWork.ConstructionManager);
            var model_ControlManager = PHTGL_ApproveService.GetPHTGL_ApproveByContractIdandUserId(Id, getFireWork.ControlManager);
            var model_Approval_Construction = PHTGL_ApproveService.GetPHTGL_ApproveByContractIdandUserId(Id, getFireWork.Approval_Construction);
            var model_ProjectManager = PHTGL_ApproveService.GetPHTGL_ApproveByContractIdandUserId(Id, getFireWork.ProjectManager);

            Dictionary<string, object> Dic_File = new Dictionary<string, object>();
            Dic_File.Add("txtBidDocumentCode", getFireWork.BidDocumentsCode);
            Dic_File.Add("txtProjectName", ProjectService.GetProjectNameByProjectId(getFireWork.ProjectId));
            Dic_File.Add("txtProjectCode", ProjectService.GetProjectCodeByProjectId(getFireWork.ProjectId));
            Dic_File.Add("txtBidContent", getFireWork.BidContent);
            Dic_File.Add("txtBidType", getFireWork.BidType);
            Dic_File.Add("txtBidDocumentsName", getFireWork.BidDocumentsName);
            Dic_File.Add("Bidding_SendTime", string.Format("{0:D}", getFireWork.Bidding_SendTime));
            Dic_File.Add("Bidding_StartTime", string.Format("{0:D}", getFireWork.Bidding_StartTime));

            if (getFireWork.State == Const.ContractReview_Complete)
            {
                Dic_File.Add("ConstructionManagerTime", string.Format("{0:D}", DateTime.Parse(model_ConstructionManager.ApproveDate)));
                Dic_File.Add("ControlManagerTime", string.Format("{0:D}", DateTime.Parse(model_ControlManager.ApproveDate)));
                Dic_File.Add("Approval_ConstructionTime", string.Format("{0:D}", DateTime.Parse(model_Approval_Construction.ApproveDate)));
                Dic_File.Add("ProjectManagerTime", string.Format("{0:D}", DateTime.Parse(model_ProjectManager.ApproveDate)));

                AsposeWordHelper.InsertImg(doc, rootPath, "Approval_Construction", getFireWork.Approval_Construction, "");
                AsposeWordHelper.InsertImg(doc, rootPath, "ConstructionManager", getFireWork.ConstructionManager, "");
                AsposeWordHelper.InsertImg(doc, rootPath, "ControlManager", getFireWork.ControlManager, "");
                AsposeWordHelper.InsertImg(doc, rootPath, "ProjectManager", getFireWork.ProjectManager, "");
            }
            else
            {
                Dic_File.Add("ConstructionManagerTime", "  年  月  日");
                Dic_File.Add("ControlManagerTime", "  年  月  日");
                Dic_File.Add("Approval_ConstructionTime", "  年  月  日");
                Dic_File.Add("ProjectManagerTime", "  年  月  日");
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