using Aspose.Words;
using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using AspNet = System.Web.UI.WebControls;

namespace FineUIPro.Web.HSSE.Check
{
    public partial class PauseNotice : PageBase
    {
        #region 项目主键
        /// <summary>
        /// 项目主键
        /// </summary>
        public string ProjectId
        {
            get
            {
                return (string)ViewState["ProjectId"];
            }
            set
            {
                ViewState["ProjectId"] = value;
            }
        }
        #endregion

        #region 加载页面
        /// <summary>
        /// 加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Funs.DropDownPageSize(this.ddlPageSize);
                this.ProjectId = this.CurrUser.LoginProjectId;
                if (!string.IsNullOrEmpty(Request.Params["projectId"]) && Request.Params["projectId"] != this.CurrUser.LoginProjectId)
                {
                    this.ProjectId = Request.Params["projectId"];
                }
                ////权限按钮方法
                this.GetButtonPower();
                btnNew.OnClientClick = Window1.GetShowReference("PauseNoticeAdd.aspx") + "return false;";

                ddlPageSize.SelectedValue = Grid1.PageSize.ToString();
                // 绑定表格
                BindGrid();
            }
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            string strSql = @"SELECT PauseNotice.PauseNoticeId,PauseNotice.ProjectId,CodeRecords.Code AS PauseNoticeCode,Unit.UnitName,PauseNotice.ProjectPlace,PauseNotice.UnitId,PauseNotice.PauseTime,case PauseNotice.IsConfirm when 1 then '已确认' else '未确认' end as IsConfirmStr ,
                (CASE WHEN PauseNotice.PauseStates = '0' OR PauseNotice.PauseStates IS NULL THEN '待['+CompileMan.UserName+']提交' WHEN PauseNotice.PauseStates = '1' THEN '待['+SignMan.UserName+']签发'  WHEN PauseNotice.PauseStates = '2' THEN '待['+ApproveMan.UserName+']批准' WHEN PauseNotice.PauseStates = '3' THEN '待['+DutyPerson.UserName+']接收' WHEN PauseNotice.PauseStates = '4' THEN '审批完成' END) AS  FlowOperateName 
             FROM Check_PauseNotice AS PauseNotice 
             LEFT JOIN Sys_User AS CompileMan ON CompileMan.UserId=PauseNotice.CompileManId 
             LEFT JOIN Sys_User AS SignMan ON SignMan.UserId=PauseNotice.SignManId 
             LEFT JOIN Sys_User AS ApproveMan ON ApproveMan.UserId=PauseNotice.ApproveManId 
             LEFT JOIN Sys_User AS DutyPerson ON DutyPerson.UserId=PauseNotice.DutyPersonId
             LEFT JOIN Sys_CodeRecords AS CodeRecords ON PauseNotice.PauseNoticeId=CodeRecords.DataId 
             LEFT JOIN Base_Unit AS Unit ON Unit.UnitId=PauseNotice.UnitId WHERE 1=1 ";
            List<SqlParameter> listStr = new List<SqlParameter>();
            strSql += " AND PauseNotice.ProjectId = @ProjectId";
            if (!string.IsNullOrEmpty(Request.Params["projectId"]))  ///是否文件柜查看页面传项目值
            {
                listStr.Add(new SqlParameter("@ProjectId", Request.Params["projectId"]));
                strSql += " AND PauseNotice.States = @States";  ///状态为已完成
                listStr.Add(new SqlParameter("@States", BLL.Const.State_2));
            }
            else
            {
                listStr.Add(new SqlParameter("@ProjectId", this.CurrUser.LoginProjectId));
            }
            if (this.rbStates.SelectedValue != "-1")
            {
                strSql += " AND PauseNotice.PauseStates =@PauseStates";
                listStr.Add(new SqlParameter("@PauseStates", this.rbStates.SelectedValue));
            }
            if (BLL.ProjectUnitService.GetProjectUnitTypeByProjectIdUnitId(this.ProjectId, this.CurrUser.UnitId))
            {
                strSql += " AND PauseNotice.UnitId = @UnitId";  ///状态为已完成
                listStr.Add(new SqlParameter("@UnitId", this.CurrUser.UnitId));

                //strSql += " AND PauseNotice.States = @States";  ///状态为已完成
                //listStr.Add(new SqlParameter("@States", BLL.Const.State_2));
            }
            if (!string.IsNullOrEmpty(this.txtPauseNoticeCode.Text.Trim()))
            {
                strSql += " AND PauseNoticeCode LIKE @PauseNoticeCode";
                listStr.Add(new SqlParameter("@PauseNoticeCode", "%" + this.txtPauseNoticeCode.Text.Trim() + "%"));
            }
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            Grid1.RecordCount = tb.Rows.Count;
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();
           
        }
        #endregion

        #region 查询
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            this.BindGrid();
        }
        #endregion

        #region 表排序、分页、关闭窗口
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            Grid1.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_Sort(object sender, GridSortEventArgs e)
        {
            BindGrid();
        }

        /// <summary>
        /// 分页显示条数下拉框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid1.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            BindGrid();
        }

        /// <summary>
        /// 关闭弹出窗
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
            BindGrid();
        }
        #endregion

        #region Grid双击事件
        /// <summary>
        /// Grid行双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            EditData(Grid1.SelectedRowID);
        }
        #endregion

        #region 编辑
        /// <summary>
        /// 编辑按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuModify_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            EditData(Grid1.SelectedRowID);
            
        }
        protected void btnMenuView_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            string id = Grid1.SelectedRowID;
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("PauseNoticeView.aspx?PauseNoticeId={0}", id, "操作 - ")));
        }
        /// <summary>
        /// 编辑数据方法
        /// </summary>
        private void EditData(string PauseNoticeId)
        {
            string url = "PauseNoticeView.aspx?PauseNoticeId={0}";
            var pauseNotice = BLL.Check_PauseNoticeService.GetPauseNoticeByPauseNoticeId(PauseNoticeId);
            if (pauseNotice.PauseStates == BLL.Const.State_0 && pauseNotice.CompileManId == this.CurrUser.UserId)
            {
                url = "PauseNoticeAdd.aspx?PauseNoticeId={0}";
            }
            else if (pauseNotice.PauseStates == BLL.Const.State_1 && pauseNotice.SignManId == this.CurrUser.UserId)
            {
                url = "PauseNoticeEdit.aspx?PauseNoticeId={0}";
            }
            else if (pauseNotice.PauseStates == BLL.Const.State_2 && pauseNotice.ApproveManId == this.CurrUser.UserId)
            {
                url = "PauseNoticeEdit.aspx?PauseNoticeId={0}";
            }
            else if (pauseNotice.PauseStates == BLL.Const.State_3 && pauseNotice.DutyPersonId == this.CurrUser.UserId)
            {
                url = "PauseNoticeEdit.aspx?PauseNoticeId={0}";
            }
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format(url, PauseNoticeId, "操作 - ")));
            
        }
        #endregion

        #region 删除
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuDel_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length > 0)
            {
                foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                {
                    string rowID = Grid1.DataKeys[rowIndex][0].ToString();
                    Model.Check_PunishNoticeFlowOperate Operate = (from x in Funs.DB.Check_PunishNoticeFlowOperate
                                                                   where x.PunishNoticeId == rowID
                                                            select x).FirstOrDefault();
                    var getV = BLL.Check_PauseNoticeService.GetPauseNoticeByPauseNoticeId(rowID);
                    if (getV != null)
                    {
                        BLL.LogService.AddSys_Log(this.CurrUser, getV.PauseNoticeCode, getV.PauseNoticeId, BLL.Const.ProjectPauseNoticeMenuId, BLL.Const.BtnDelete);                    
                        BLL.Check_PauseNoticeService.DeletePauseNotice(rowID);
                    }
                }

                BindGrid();
                ShowNotify("删除数据成功!（表格数据已重新绑定）");
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
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.ProjectPauseNoticeMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnAdd))
                {
                    this.btnNew.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnModify))
                {
                    this.btnMenuModify.Hidden = false;
                }
                //if (buttonList.Contains(BLL.Const.BtnConfirm))
                //{
                //    this.btnMenuConfirm.Hidden = false;
                //}
                if (buttonList.Contains(BLL.Const.BtnDelete))
                {
                    this.btnMenuDel.Hidden = false;
                }
            }
        }
        #endregion

        #region 导出按钮
        /// 导出按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnOut_Click(object sender, EventArgs e)
        {
            Response.ClearContent();
            string filename = Funs.GetNewFileName();
            Response.AddHeader("content-disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode("工程暂停令" + filename, System.Text.Encoding.UTF8) + ".xls");
            Response.ContentType = "application/excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            this.Grid1.PageSize = this.Grid1.RecordCount;
            this.BindGrid();
            Response.Write(GetGridTableHtml(Grid1));
            Response.End();
        }
        #endregion

        protected void rbStates_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.BindGrid();
        }

        #region 导出
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
            initTemplatePath = "File\\Word\\HSSE\\工程停工令.doc";
            uploadfilepath = rootPath + initTemplatePath;
            newUrl = uploadfilepath.Replace(".doc", string.Format("{0:yyyy-MM}", DateTime.Now) + ".doc");
            filePath = initTemplatePath.Replace(".doc", string.Format("{0:yyyy-MM}", DateTime.Now) + ".pdf");
            File.Copy(uploadfilepath, newUrl);
            ///更新书签内容
            var getPauseNotice = BLL.Check_PauseNoticeService.GetPauseNoticeByPauseNoticeId(Id);
            Document doc = new Aspose.Words.Document(newUrl);
            Bookmark bookmarkProjectName = doc.Range.Bookmarks["ProjectName"];
            if (bookmarkProjectName != null)
            {
                if (getPauseNotice != null)
                {
                    bookmarkProjectName.Text = BLL.ProjectService.GetProjectNameByProjectId(getPauseNotice.ProjectId);
                }
            }
            Bookmark bookmarkPunishNoticeCode = doc.Range.Bookmarks["PauseNoticeCode"];
            if (bookmarkPunishNoticeCode != null)
            {
                if (getPauseNotice != null)
                {
                    bookmarkPunishNoticeCode.Text = getPauseNotice.PauseNoticeCode;
                }
            }
            Bookmark bookmarkWrongContent = doc.Range.Bookmarks["WrongContent"];
            if (bookmarkWrongContent != null)
            {
                if (getPauseNotice != null)
                {
                    if (!string.IsNullOrEmpty(getPauseNotice.WrongContent))
                    {
                        bookmarkWrongContent.Text = getPauseNotice.WrongContent;
                    }

                }
            }
            Bookmark bookmarkDateTime = doc.Range.Bookmarks["DateTime"];
            if (bookmarkDateTime != null)
            {
                if (getPauseNotice != null)
                {
                    if (getPauseNotice.PauseTime.HasValue)
                    {
                        bookmarkDateTime.Text = getPauseNotice.PauseTime.Value.Year+"年"+ getPauseNotice.PauseTime.Value.Month+"月"+ getPauseNotice.PauseTime.Value.Day+"日";

                    }

                }
            }
                Bookmark bookmarkSignPerson = doc.Range.Bookmarks["SignPerson"];
                if (bookmarkSignPerson != null)
                {
                    if (!string.IsNullOrEmpty(getPauseNotice.SignManId))
                    {

                        var getUser = UserService.GetUserByUserId(getPauseNotice.SignManId);
                        if (getUser != null)
                        {
                            if (!string.IsNullOrEmpty(getUser.SignatureUrl) && File.Exists(rootPath + getUser.SignatureUrl))
                            {
                                var file = rootPath + getUser.SignatureUrl;
                                DocumentBuilder builders = new DocumentBuilder(doc);
                                builders.MoveToBookmark("SignPerson");
                                builders.InsertImage(file, 80, 20);
                            }
                            else
                            {
                                bookmarkSignPerson.Text = getUser.UserName;
                            }
                        }
                    }
                }


            Bookmark bookmarkSignDate = doc.Range.Bookmarks["SignTime"];
            if (bookmarkSignDate != null)
            {
                if (getPauseNotice != null)
                {
                    bookmarkSignDate.Text = string.Format("{0:yyyy-MM-dd}", getPauseNotice.SignDate);
                }
            }
                Bookmark bookmarkDutyPerson = doc.Range.Bookmarks["DutyPerson"];
                if (bookmarkDutyPerson != null)
                {
                    var getUser = UserService.GetUserByUserId(getPauseNotice.DutyPersonId);
                    if (getUser != null)
                    {
                        if (!string.IsNullOrEmpty(getUser.SignatureUrl) && File.Exists(rootPath + getUser.SignatureUrl))
                        {
                            var file = rootPath + getUser.SignatureUrl;
                            DocumentBuilder builders = new DocumentBuilder(doc);
                            builders.MoveToBookmark("DutyPerson");
                            builders.InsertImage(file, 80, 20);
                        }
                        else
                        {
                            bookmarkDutyPerson.Text = getUser.UserName;
                        }
                    }
                }


            Bookmark bookmarkDutyTime = doc.Range.Bookmarks["DutyTime"];
            if (bookmarkDutyTime != null)
            {
                if (getPauseNotice != null)
                {
                    bookmarkDutyTime.Text = string.Format("{0:yyyy-MM-dd}", getPauseNotice.DutyPersonDate);
                }
            }
            Bookmark bookmarkSupervisorMan = doc.Range.Bookmarks["SupervisorMan"];
            if (bookmarkSupervisorMan != null)
            {
                var getUser = UserService.GetUserByUserId(getPauseNotice.SupervisorManId);
                if (getUser != null)
                {
                    if (!string.IsNullOrEmpty(getUser.SignatureUrl) && File.Exists(rootPath + getUser.SignatureUrl))
                    {
                        var file = rootPath + getUser.SignatureUrl;
                        DocumentBuilder builders = new DocumentBuilder(doc);
                        builders.MoveToBookmark("SupervisorMan");
                        builders.InsertImage(file, 80, 20);
                    }
                    else
                    {
                        bookmarkSupervisorMan.Text = getUser.UserName;
                    }
                }
            }


            Bookmark bookmarkSupervisorTime = doc.Range.Bookmarks["SupervisorTime"];
            if (bookmarkSupervisorTime != null)
            {
                if (getPauseNotice != null)
                {
                    bookmarkSupervisorTime.Text = string.Format("{0:yyyy-MM-dd}", getPauseNotice.SupervisorManTime);
                }
            }
            Bookmark bookmarkOwner = doc.Range.Bookmarks["Owner"];
            if (bookmarkOwner != null)
            {
                var getUser = UserService.GetUserByUserId(getPauseNotice.OwnerId);
                if (getUser != null)
                {
                    if (!string.IsNullOrEmpty(getUser.SignatureUrl) && File.Exists(rootPath + getUser.SignatureUrl))
                    {
                        var file = rootPath + getUser.SignatureUrl;
                        DocumentBuilder builders = new DocumentBuilder(doc);
                        builders.MoveToBookmark("Owner");
                        builders.InsertImage(file, 80, 20);
                    }
                    else
                    {
                        bookmarkOwner.Text = getUser.UserName;
                    }
                }
            }


            Bookmark bookmarkOwnerTime = doc.Range.Bookmarks["OwnerTime"];
            if (bookmarkOwnerTime != null)
            {
                if (getPauseNotice != null)
                {
                    bookmarkOwnerTime.Text = string.Format("{0:yyyy-MM-dd}", getPauseNotice.OwnerTime);
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
            //PrinterDocService.PrinterDocMethod(Const.ProjectRectifyNoticesMenuId + "#1", Grid1.SelectedRowID, "隐患整改通知单");          
        }
        #endregion
    }
}