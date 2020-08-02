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
    public partial class PunishNotice : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
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
                Funs.DropDownPageSize(this.ddlPageSize);
                this.ProjectId = this.CurrUser.LoginProjectId;
                if (!string.IsNullOrEmpty(Request.Params["projectId"]) && Request.Params["projectId"] != this.CurrUser.LoginProjectId)
                {
                    this.ProjectId = Request.Params["projectId"];
                }
                BLL.UnitService.InitUnitDropDownList(this.drpUnitId, this.ProjectId, true);
                if (BLL.ProjectUnitService.GetProjectUnitTypeByProjectIdUnitId(this.ProjectId, this.CurrUser.UnitId))
                {
                    this.drpUnitId.SelectedValue = this.CurrUser.UnitId;
                    this.drpUnitId.Enabled = false;
                }
                ////权限按钮方法
                this.GetButtonPower();
                this.btnNew.OnClientClick = Window1.GetShowReference("PunishNoticeAdd.aspx") + "return false;";
                if (this.CurrUser != null && this.CurrUser.PageSize.HasValue)
                {
                    Grid1.PageSize = this.CurrUser.PageSize.Value;
                }
                this.ddlPageSize.SelectedValue = Grid1.PageSize.ToString();
                // 绑定表格
                this.BindGrid();
            }
        }
        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            string strSql = "SELECT PunishNotice.PunishNoticeId,PunishNotice.ProjectId,CodeRecords.Code AS PunishNoticeCode,PunishNotice.UnitId,PunishNotice.PunishNoticeDate,PunishNotice.BasicItem,PunishNotice.PunishMoney,PunishStates,ISNULL(PunishNotice.Currency, '人民币') AS Currency, PunishNotice.FileContents,PunishNotice.AttachUrl,PunishNotice.CompileMan,PunishNotice.CompileDate,Sign.UserName AS SignManName,Approve.UserName AS ApproveManName,PunishNotice.ContractNum,PunishNotice.States,Unit.UnitName,(CASE WHEN PunishNotice.PunishStates = '0' THEN '待[' + Users.UserName + ']提交' WHEN PunishNotice.PunishStates = '1' THEN '待[' + Sign.UserName + ']签发'  WHEN PunishNotice.PunishStates = '2' THEN '待[' + Approve.UserName + ']批准' WHEN PunishNotice.PunishStates = '3' THEN '待[' + Duty.UserName + ']回执'  WHEN(SELECT COUNT(*) FROM AttachFile WHERE ToKeyId = PunishNotice.PunishNoticeId AND MenuId = '" + BLL.Const.ProjectPunishNoticeMenuId + "') > 0 THEN '已闭环' ELSE '未回执' END) AS RetrunSateName FROM Check_PunishNotice AS PunishNotice" +
                " LEFT JOIN Sys_CodeRecords AS CodeRecords ON PunishNotice.PunishNoticeId = CodeRecords.DataId " +
                " LEFT JOIN Sys_FlowOperate AS FlowOperate ON PunishNotice.PunishNoticeId = FlowOperate.DataId AND FlowOperate.IsClosed <> 1" +
                " LEFT JOIN Sys_User AS OperateUser ON FlowOperate.OperaterId = OperateUser.UserId" +
                " LEFT JOIN Base_Unit AS Unit ON Unit.UnitId = PunishNotice.UnitId" +
                " LEFT JOIN Sys_User AS Sign ON Sign.UserId = PunishNotice.SignMan" +
                " LEFT JOIN Sys_User AS Approve ON Approve.UserId = PunishNotice.ApproveMan" +
                " LEFT JOIN Sys_User AS Duty ON Duty.UserId = PunishNotice.DutyPersonId" +
                " LEFT JOIN Sys_User AS Users ON PunishNotice.CompileMan = Users.UserId WHERE 1 = 1 ";
            List<SqlParameter> listStr = new List<SqlParameter>();
            strSql += " AND PunishNotice.ProjectId = @ProjectId";
            if (!string.IsNullOrEmpty(Request.Params["projectId"]))  ///是否文件柜查看页面传项目值
            {
                listStr.Add(new SqlParameter("@ProjectId", Request.Params["projectId"]));
                strSql += " AND PunishNotice.States = @States";  ///状态为已完成
                listStr.Add(new SqlParameter("@States", BLL.Const.State_2));
            }
            else
            {
                listStr.Add(new SqlParameter("@ProjectId", this.CurrUser.LoginProjectId));
            }
            if (this.rbStates.SelectedValue != "-1")
            {
                strSql += " AND PunishNotice.PunishStates =@PunishStates";
                listStr.Add(new SqlParameter("@PunishStates", this.rbStates.SelectedValue));
            }
            /// 施工分包 只看到自己已完成的处罚单
            //if (BLL.ProjectUnitService.GetProjectUnitTypeByProjectIdUnitId(this.ProjectId, this.CurrUser.UnitId))
            //{
            //    strSql += " AND PunishNotice.UnitId = @UnitId";  ///处罚单位
            //    listStr.Add(new SqlParameter("@UnitId", this.CurrUser.UnitId));

            //    strSql += " AND PunishNotice.States = @States";  ///状态为已完成
            //    listStr.Add(new SqlParameter("@States", BLL.Const.State_2));
            //}

            if (!string.IsNullOrEmpty(this.txtPunishNoticeCode.Text.Trim()))
            {
                strSql += " AND PunishNoticeCode LIKE @PunishNoticeCode";
                listStr.Add(new SqlParameter("@PunishNoticeCode", "%" + this.txtPunishNoticeCode.Text.Trim() + "%"));
            }
            if (this.drpUnitId.SelectedValue != BLL.Const._Null)
            {
                strSql += " AND PunishNotice.UnitId = @UnitId2";
                listStr.Add(new SqlParameter("@UnitId2", this.drpUnitId.SelectedValue.Trim()));
            }
            if (!string.IsNullOrEmpty(this.txtStartDate.Text.Trim()))
            {
                strSql += " AND PunishNotice.PunishNoticeDate >= @StartDate";
                listStr.Add(new SqlParameter("@StartDate", this.txtStartDate.Text.Trim()));
            }
            if (!string.IsNullOrEmpty(this.txtEndDate.Text.Trim()))
            {
                strSql += " AND PunishNotice.PunishNoticeDate <= @EndDate";
                listStr.Add(new SqlParameter("@EndDate", this.txtEndDate.Text.Trim()));
            }
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            Grid1.RecordCount = tb.Rows.Count;

            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();
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
            this.Grid1.PageSize = Convert.ToInt32(this.ddlPageSize.SelectedValue);
            BindGrid();
        }

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_Sort(object sender, FineUIPro.GridSortEventArgs e)
        {
            this.BindGrid();
        }
        #endregion
        #endregion

        #region 查询
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.txtStartDate.Text.Trim()) && !string.IsNullOrEmpty(this.txtEndDate.Text.Trim()))
            {
                if (Convert.ToDateTime(this.txtStartDate.Text.Trim()) > Convert.ToDateTime(this.txtEndDate.Text.Trim()))
                {
                    Alert.ShowInTop("开始时间不能大于结束时间", MessageBoxIcon.Warning);
                    return;
                }
            }
            this.BindGrid();
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
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            this.EditData(Grid1.SelectedRowID);
        }

        /// <summary>
        /// 右键编辑事件
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
            this.EditData(Grid1.SelectedRowID);
        }
        protected void btnMenuView_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            string id = Grid1.SelectedRowID;
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("PunishNoticeView.aspx?PunishNoticeId={0}", id, "操作 - ")));
        }
        /// <summary>
        /// 编辑数据方法
        /// </summary>
        private void EditData(string PunishNoticeId)
        {
            string url = "PunishNoticeView.aspx?PunishNoticeId={0}";
            var punishNotice = BLL.PunishNoticeService.GetPunishNoticeById(PunishNoticeId);
            if (punishNotice.PunishStates == BLL.Const.State_0 && punishNotice.CompileMan == this.CurrUser.UserId)
            {
                url = "PunishNoticeAdd.aspx?PunishNoticeId={0}";
            }
            else if (punishNotice.PunishStates == BLL.Const.State_1 && punishNotice.SignMan == this.CurrUser.UserId)
            {
                url = "PunishNoticeEdit.aspx?PunishNoticeId={0}";
            }
            else if (punishNotice.PunishStates == BLL.Const.State_2 && punishNotice.ApproveMan == this.CurrUser.UserId)
            {
                url = "PunishNoticeEdit.aspx?PunishNoticeId={0}";
            }
            else if (punishNotice.PunishStates == BLL.Const.State_3 && punishNotice.DutyPersonId == this.CurrUser.UserId)
            {
                url = "PunishNoticeEdit.aspx?PunishNoticeId={0}";
            }
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format(url, PunishNoticeId, "操作 - ")));
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
                foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                {
                    string rowID = Grid1.DataKeys[rowIndex][0].ToString();
                    var punishNotice = BLL.PunishNoticeService.GetPunishNoticeById(rowID);
                    if (punishNotice != null)
                    {
                        BLL.LogService.AddSys_Log(this.CurrUser, punishNotice.PunishNoticeCode, punishNotice.PunishNoticeId, BLL.Const.ProjectPunishNoticeMenuId, BLL.Const.BtnDelete);

                        BLL.PunishNoticeService.DeletePunishNoticeById(rowID);
                    }
                }

                this.BindGrid();
                ShowNotify("删除数据成功!", MessageBoxIcon.Success);
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
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.ProjectPunishNoticeMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnAdd))
                {
                    this.btnNew.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnModify))
                {
                    this.btnMenuEdit.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnDelete))
                {
                    this.btnMenuDelete.Hidden = false;
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
            Response.AddHeader("content-disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode("处罚通知单" + filename, System.Text.Encoding.UTF8) + ".xls");
            Response.ContentType = "application/excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            this.Grid1.PageSize = this.Grid1.RecordCount;
            this.BindGrid();
            Response.Write(GetGridTableHtml(Grid1));
            Response.End();
        }
        #endregion

        #region 关闭弹出框
        protected void WindowAtt_Close(object sender, WindowCloseEventArgs e)
        {
            BindGrid();
        }
        #endregion

        protected void rbStates_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.BindGrid();
        }

        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
            BindGrid();
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
            initTemplatePath = "File\\Word\\HSSE\\安全处罚通知单.doc";
            uploadfilepath = rootPath + initTemplatePath;
            newUrl = uploadfilepath.Replace(".doc", string.Format("{0:yyyy-MM}", DateTime.Now) + ".doc");
            filePath = initTemplatePath.Replace(".doc", string.Format("{0:yyyy-MM}", DateTime.Now) + ".pdf");
            File.Copy(uploadfilepath, newUrl);
            ///更新书签内容
            var getPunishNotice = BLL.PunishNoticeService.GetPunishNoticeById(Id);
            Document doc = new Aspose.Words.Document(newUrl);
            Bookmark bookmarkProjectName = doc.Range.Bookmarks["ProjectName"];
            if (bookmarkProjectName != null)
            {
                if (getPunishNotice != null)
                {
                    bookmarkProjectName.Text = BLL.ProjectService.GetProjectNameByProjectId(getPunishNotice.ProjectId);
                }
            }
            Bookmark bookmarkPunishNoticeCode = doc.Range.Bookmarks["PunishNoticeCode"];
            if (bookmarkPunishNoticeCode != null)
            {
                if (getPunishNotice != null)
                {
                    bookmarkPunishNoticeCode.Text = getPunishNotice.PunishNoticeCode;
                }
            }
            Bookmark bookmarkUnitAndPersonName = doc.Range.Bookmarks["UnitAndPersonName"];
            if (bookmarkUnitAndPersonName != null)
            {
                if (getPunishNotice != null)
                {
                    if (!string.IsNullOrEmpty(getPunishNotice.UnitId))
                    {
                        bookmarkUnitAndPersonName.Text = BLL.UnitService.GetUnitNameByUnitId(getPunishNotice.UnitId);
                    }
                    if (!string.IsNullOrEmpty(getPunishNotice.PunishPersonId)) {
                        bookmarkUnitAndPersonName.Text +="  " +BLL.UserService.GetUserNameByUserId(getPunishNotice.PunishPersonId);
                    }

                }
            }
            Bookmark bookmarkDateTime = doc.Range.Bookmarks["DateTime"];
            if (bookmarkDateTime != null)
            {
                if (getPunishNotice != null)
                {
                    if (getPunishNotice.PunishNoticeDate.HasValue)
                    {
                        bookmarkDateTime.Text = string.Format("{0:yyyy-MM-dd}", getPunishNotice.PunishNoticeDate);

                    }

                }
            }
            Bookmark bookmarkIncentiveReason = doc.Range.Bookmarks["IncentiveReason"];
            if (bookmarkIncentiveReason != null)
            {
                if (getPunishNotice != null)
                {
                    bookmarkIncentiveReason.Text = getPunishNotice.IncentiveReason;

                }
            }
            Bookmark bookmarkBasicItem = doc.Range.Bookmarks["BasicItem"];
            if (bookmarkBasicItem != null)
            {
                if (getPunishNotice != null)
                {
                    bookmarkBasicItem.Text = getPunishNotice.BasicItem; ;
                }
            }
            Bookmark bookmarkPunishMoney = doc.Range.Bookmarks["PunishMoney"];
            if (bookmarkPunishMoney != null)
            {
                if (getPunishNotice != null)
                {
                    if (!string.IsNullOrEmpty(getPunishNotice.PunishMoney.ToString()))
                    {
                        bookmarkPunishMoney.Text = getPunishNotice.PunishMoney.ToString();
                    }
                }
                
            }
            Bookmark bookmarkSginopinion = doc.Range.Bookmarks["Sginopinion"];
            if (bookmarkSginopinion != null)
            {
                if (getPunishNotice != null) {
                    if (!string.IsNullOrEmpty(getPunishNotice.SginOpinion)) {
                        bookmarkSginopinion.Text = getPunishNotice.SginOpinion;
                    }
                    
                }
            }
            if (getPunishNotice.PunishStates != "1") {//已签发
                Bookmark bookmarkSignPerson = doc.Range.Bookmarks["SignPerson"];
                if (bookmarkSignPerson != null)
                {
                    if (!string.IsNullOrEmpty(getPunishNotice.SignMan))
                    {

                        var getUser = UserService.GetUserByUserId(getPunishNotice.SignMan);
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
            }
            
           
            Bookmark bookmarkSignDate = doc.Range.Bookmarks["SignTime"];
            if (bookmarkSignDate != null)
            {
                if (getPunishNotice != null)
                {
                    bookmarkSignDate.Text = string.Format("{0:yyyy-MM-dd}", getPunishNotice.SignDate);
                }
            }
            Bookmark bookmarkApproveopinion = doc.Range.Bookmarks["Approveopinion"];
            if (bookmarkApproveopinion != null)
            {
                if (getPunishNotice != null)
                {
                    if (!string.IsNullOrEmpty(getPunishNotice.ApproveOpinion)) {
                        bookmarkApproveopinion.Text = getPunishNotice.ApproveOpinion;
                    }
                    
                }
            }
            if (getPunishNotice.PunishStates == "3" || getPunishNotice.PunishStates=="4")
            {//已批准
                Bookmark bookmarkApproveMan = doc.Range.Bookmarks["ApproveMan"];
                if (bookmarkApproveMan != null)
                {
                        var getUser = UserService.GetUserByUserId(getPunishNotice.ApproveMan);
                        if (getUser != null)
                        {
                            if (!string.IsNullOrEmpty(getUser.SignatureUrl) && File.Exists(rootPath + getUser.SignatureUrl))
                            {
                                var file = rootPath + getUser.SignatureUrl;
                                DocumentBuilder builders = new DocumentBuilder(doc);
                                builders.MoveToBookmark("ApproveMan");
                                builders.InsertImage(file, 80, 20);
                            }
                            else
                            {
                            bookmarkApproveMan.Text = getUser.UserName;
                            }
                        }
                }
            }


            Bookmark bookmarkApproveTime = doc.Range.Bookmarks["ApproveTime"];
            if (bookmarkApproveTime != null)
            {
                if (getPunishNotice != null)
                {
                    bookmarkApproveTime.Text = string.Format("{0:yyyy-MM-dd}", getPunishNotice.ApproveDate);
                }
            }

            if (getPunishNotice.States == "4")
            {//已接受
                Bookmark bookmarkDutyPerson = doc.Range.Bookmarks["DutyPerson"];
                if (bookmarkDutyPerson != null)
                {
                    var getUser = UserService.GetUserByUserId(getPunishNotice.DutyPersonId);
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
            }


            Bookmark bookmarkDutyTime = doc.Range.Bookmarks["DutyTime"];
            if (bookmarkDutyTime != null)
            {
                if (getPunishNotice != null)
                {
                    bookmarkDutyTime.Text = string.Format("{0:yyyy-MM-dd}", getPunishNotice.DutyPersonDate);
                }
            }
            //附图
            Bookmark bookmarkPhotoUrl = doc.Range.Bookmarks["PhotoUrl"];
            if (bookmarkPhotoUrl != null) {
                Aspose.Words.DocumentBuilder builder = new Aspose.Words.DocumentBuilder(doc);
                builder.MoveToBookmark("PhotoUrl");
                
                var getItem = BLL.AttachFileService.GetAttachFile(getPunishNotice.PunishNoticeId, BLL.Const.ProjectPunishNoticeStatisticsMenuId);//通知单
                if (getItem != null)
                {
                    string url = rootPath + getItem.AttachUrl;
                    if (File.Exists(url))
                    {
                        builder.StartTable();
                        builder.RowFormat.Alignment = Aspose.Words.Tables.RowAlignment.Center;
                        builder.CellFormat.Borders.LineStyle = LineStyle.Single;
                        builder.CellFormat.Borders.Color = System.Drawing.Color.Black;
                        builder.RowFormat.LeftIndent = 5;
                        builder.Bold = false;
                        builder.RowFormat.Height = 20;
                        builder.Bold = false;
                        builder.InsertCell();
                        builder.CellFormat.VerticalMerge = Aspose.Words.Tables.CellMerge.None;
                        builder.CellFormat.HorizontalMerge = Aspose.Words.Tables.CellMerge.First;
                        builder.CellFormat.VerticalAlignment = Aspose.Words.Tables.CellVerticalAlignment.Center;//垂直居中对齐
                        builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;//水平居中对齐
                        builder.CellFormat.Width = 450;
                        builder.InsertImage(url, 150, 150);
                        builder.Write("通知单   ");
                    }
                }
                var getItem1 = BLL.AttachFileService.GetAttachFile(getPunishNotice.PunishNoticeId, Const.ProjectPunishNoticeMenuId);//回执单
                if (getItem1 != null)
                {
                    string url = rootPath + getItem1.AttachUrl;
                    if (File.Exists(url))
                    {
                        builder.InsertImage(url, 150, 150);
                        builder.Write("回执单   ");
                    }
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