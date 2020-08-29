using Aspose.Words;
using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;

namespace FineUIPro.Web.HSSE.Check
{
    public partial class RectifyNotices : PageBase
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
        /// <summary>
        /// 行号
        /// </summary>
        public int RowCount
        {
            get
            {
                return (int)ViewState["RowCount"];
            }
            set
            {
                ViewState["RowCount"] = value;
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
                //btnNew.OnClientClick = Window1.GetShowReference("RectifyNoticesEdit.aspx") + "return false;";
                ddlPageSize.SelectedValue = Grid1.PageSize.ToString();
                // 绑定表格
                BindGrid();
                btnNew.OnClientClick = Window1.GetShowReference("RectifyNoticesAdd.aspx") + "return false;";
            }
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            string strSql = @"SELECT R.RectifyNoticesId,R.ProjectId,R.RectifyNoticesCode,R.UnitId ,Unit.UnitName,R.WorkAreaId,R.CheckManNames,R.CheckManIds
                        ,WorkAreaName= STUFF(( SELECT ',' + UnitWorkName FROM dbo.WBS_UnitWork where PATINDEX('%,' + RTRIM(UnitWorkId) + ',%',',' +R.WorkAreaId + ',')>0 FOR XML PATH('')), 1, 1,''),CheckPersonName= (STUFF(( SELECT ',' + UserName FROM dbo.Sys_User where PATINDEX('%,' + RTRIM(UserId) + ',%',',' +R.CheckManIds+ ',')>0 FOR XML PATH('')), 1, 1,'')+ (CASE WHEN CheckManNames IS NOT NULL AND CheckManNames !='' THEN ','+ CheckManNames ELSE '' END)),R.DutyPerson,R.CheckedDate,DutyPerson.UserName AS DutyPersonName,R.DutyPersonTime,R.CompleteDate
                        ,(CASE WHEN States = 0 THEN '待['+CompileMan.UserName+']提交' WHEN States = 1 THEN '待['+SignMan.UserName+']签发' WHEN States = 2 THEN '待['+DutyPerson.UserName+']整改' WHEN States = 3 THEN '待['+UnitHeadMan.UserName+']审核' WHEN States = 4 THEN '待['+CheckPerson.UserName+']复查' WHEN States = 5 THEN '已完成' ELSE '' END) AS StatesName
                        FROM Check_RectifyNotices AS R
                        LEFT JOIN Base_Project AS Project ON Project.ProjectId = R.ProjectId 
                        LEFT JOIN Base_Unit AS Unit ON Unit.UnitId = R.UnitId 
						LEFT JOIN Sys_User AS CompileMan ON CompileMan.UserId=R.CompleteManId 
                        LEFT JOIN Sys_User AS SignMan ON SignMan.UserId=R.SignPerson  
                        LEFT JOIN Sys_User AS DutyPerson ON DutyPerson.UserId = R.DutyPersonId
						LEFT JOIN Sys_User AS UnitHeadMan ON UnitHeadMan.UserId = R.UnitHeadManId
						LEFT JOIN Sys_User AS CheckPerson ON CheckPerson.UserId = R.CheckPerson
                        LEFT JOIN Sys_CodeRecords AS CodeRecords ON R.RectifyNoticesId = CodeRecords.DataId 
                        WHERE States IS NOT NULL  ";
            List<SqlParameter> listStr = new List<SqlParameter>();
            strSql += " AND R.ProjectId = @ProjectId";
            listStr.Add(new SqlParameter("@ProjectId", this.ProjectId));
            if (BLL.ProjectUnitService.GetProjectUnitTypeByProjectIdUnitId(this.ProjectId, this.CurrUser.UnitId))
            {
                strSql += " AND R.UnitId = @UnitId";  ///状态为已完成
                listStr.Add(new SqlParameter("@UnitId", this.CurrUser.UnitId));
            }
            if (!string.IsNullOrEmpty(this.txtRectifyNoticesCode.Text.Trim()))
            {
                strSql += " AND RectifyNoticesCode LIKE @RectifyNoticesCode";
                listStr.Add(new SqlParameter("@RectifyNoticesCode", "%" + this.txtRectifyNoticesCode.Text.Trim() + "%"));
            }
            if (!string.IsNullOrEmpty(this.txtUnitName.Text.Trim()))
            {
                strSql += " AND Unit.UnitName LIKE @UnitName";
                listStr.Add(new SqlParameter("@UnitName", "%" + this.txtUnitName.Text.Trim() + "%"));
            }
            if (!string.IsNullOrEmpty(this.txtWorkAreaName.Text.Trim()))
            {
                strSql += " AND WorkArea.WorkAreaName LIKE @WorkAreaName";
                listStr.Add(new SqlParameter("@WorkAreaName", "%" + this.txtWorkAreaName.Text.Trim() + "%"));
            }
            if (this.rbStates.SelectedValue != "-1")
            {
                strSql += " AND R.States =@States";
                listStr.Add(new SqlParameter("@States", this.rbStates.SelectedValue));
            }
            if (this.rbrbHiddenHazardType.SelectedValue != "-1")
            {
                strSql += " AND R.HiddenHazardType =@HiddenHazardType";
                listStr.Add(new SqlParameter("@HiddenHazardType", this.rbrbHiddenHazardType.SelectedValue));
            }

            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            Grid1.RecordCount = tb.Rows.Count;
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();
            this.RowCount = Grid1.RecordCount;
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

        #region Grid双击事件 编辑
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

        /// <summary>
        /// 
        /// </summary>
        private void EditData(string rectifyNoticeId)
        {
            string url = "RectifyNoticesView.aspx?RectifyNoticesId={0}";
            var RectifyNotices = RectifyNoticesService.GetRectifyNoticesById(Grid1.SelectedRowID);
            if (RectifyNotices.States == "0" && this.CurrUser.UserId == RectifyNotices.CompleteManId)
            {
                url = "RectifyNoticesAdd.aspx?RectifyNoticesId={0}";
            }
            else if (RectifyNotices.States == "1" && this.CurrUser.UserId == RectifyNotices.SignPerson)
            {
                url = "RectifyNoticesAudit.aspx?RectifyNoticesId={0}";
            }
            else if (RectifyNotices.States == "2" && this.CurrUser.UserId == RectifyNotices.DutyPersonId)
            {
                url = "RectifyNoticesRectify.aspx?RectifyNoticesId={0}";
            }
            else if (RectifyNotices.States == "3" && this.CurrUser.UserId == RectifyNotices.UnitHeadManId)
            {
                url = "RectifyNoticesAudit.aspx?RectifyNoticesId={0}";
            }
            else if (RectifyNotices.States == "4" && this.CurrUser.UserId == RectifyNotices.CheckPerson)
            {
                url = "RectifyNoticesRecheck.aspx?RectifyNoticesId={0}";
            }

            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format(url, rectifyNoticeId, "操作 - ")));
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
                    var RectifyNotices = BLL.RectifyNoticesService.GetRectifyNoticesById(rowID);
                    if (RectifyNotices != null)
                    {

                        LogService.AddSys_Log(this.CurrUser, RectifyNotices.RectifyNoticesCode, rowID, BLL.Const.ProjectRectifyNoticeMenuId, BLL.Const.BtnDelete);
                        RectifyNoticesService.DeleteRectifyNoticesById(rowID);
                        Model.Check_CheckSpecialDetail specialDetail = (from x in Funs.DB.Check_CheckSpecialDetail
                                                                        where x.RectifyNoticeId == rowID
                                                                        select x).FirstOrDefault();
                        Model.Check_CheckColligationDetail colligationDetail = (from x in Funs.DB.Check_CheckColligationDetail
                                                                                where x.RectifyNoticeId == rowID
                                                                                select x).FirstOrDefault();
                        if (specialDetail != null)
                        {
                            specialDetail.RectifyNoticeId = null;
                            Funs.DB.SubmitChanges();
                        }
                        else if (colligationDetail != null)
                        {
                            colligationDetail.RectifyNoticeId = null;
                            Funs.DB.SubmitChanges();
                        }
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
            var buttonList = BLL.CommonService.GetAllButtonList(this.ProjectId, this.CurrUser.UserId, BLL.Const.ProjectRectifyNoticesMenuId);
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
            Response.AddHeader("content-disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode("隐患整改通知单" + filename, System.Text.Encoding.UTF8) + ".xls");
            Response.ContentType = "application/excel";
            Response.ContentEncoding = Encoding.UTF8;
            this.Grid1.PageSize = this.RowCount;
            this.BindGrid();
            Response.Write(GetGridTableHtml(Grid1));
            Response.End();
        }
        #endregion

        protected void rbStates_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.BindGrid();
        }
        /// <summary>
        /// 获取检察人员
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        protected string ConvertCheckPerson(object CheckManIds)
        {
            string CheckManName = string.Empty;
            if (CheckManIds != null)
            {
                string[] Ids = CheckManIds.ToString().Split(',');
                foreach (string t in Ids)
                {
                    var Name = BLL.UserService.GetUserNameByUserId(t);
                    if (Name != null)
                    {
                        CheckManName += Name + ",";
                    }
                }
            }
            if (CheckManName != string.Empty)
            {
                return CheckManName.Substring(0, CheckManName.Length - 1);
            }
            else
            {
                return "";
            }
        }

        #region 通知单打印按钮
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnPrinter_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            var getRectify = BLL.RectifyNoticesService.GetRectifyNoticesById(Grid1.SelectedRowID);
            if (getRectify == null)
            {
                Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }

            string rootPath = Server.MapPath("~/");
            string initTemplatePath = string.Empty;
            string uploadfilepath = string.Empty;
            string newUrl = string.Empty;
            string filePath = string.Empty;
            initTemplatePath = "File\\Word\\HSSE\\安全隐患整改通知单.doc";
            uploadfilepath = rootPath + initTemplatePath;
            string filename = Funs.GetNewFileName();
            newUrl = uploadfilepath.Replace(".doc", filename + ".doc");
            filePath = initTemplatePath.Replace(".doc", filename + ".pdf");
            File.Copy(uploadfilepath, newUrl);
            ///更新书签内容           
            Document doc = new Aspose.Words.Document(newUrl);
            Bookmark bookmarkProjectName = doc.Range.Bookmarks["ProjectName"];
            if (bookmarkProjectName != null)
            {
                if (getRectify != null)
                {
                    bookmarkProjectName.Text = BLL.ProjectService.GetProjectByProjectId(getRectify.ProjectId).ProjectName;
                }
            }
            Bookmark bookmarkRectifyNoticesCode = doc.Range.Bookmarks["RectifyNoticesCode"];
            if (bookmarkRectifyNoticesCode != null)
            {
                if (getRectify != null)
                {
                    bookmarkRectifyNoticesCode.Text = getRectify.RectifyNoticesCode;
                }
            }
            Bookmark bookmarkUnitName = doc.Range.Bookmarks["UnitName"];
            if (bookmarkUnitName != null)
            {
                if (!string.IsNullOrEmpty(getRectify.UnitId))
                {
                    bookmarkUnitName.Text = BLL.UnitService.GetUnitNameByUnitId(getRectify.UnitId);
                }
            }
            Bookmark bookmarkUnitWorkName = doc.Range.Bookmarks["UnitWorkName"];
            if (bookmarkUnitWorkName != null)
            {
                if (!string.IsNullOrEmpty(getRectify.WorkAreaId))
                {
                    bookmarkUnitWorkName.Text = UnitWorkService.GetUnitWorkName(getRectify.WorkAreaId);
                }
            }
            Bookmark bookmarkCheckManNames = doc.Range.Bookmarks["CheckManNames"];
            if (bookmarkCheckManNames != null)
            {
                if (!string.IsNullOrEmpty(getRectify.CheckManIds))
                {
                    bookmarkCheckManNames.Text = UserService.getUserNamesUserIds(getRectify.CheckManIds);
                }
            }
            Bookmark bookmarkCheckedDate = doc.Range.Bookmarks["CheckedDate"];
            if (bookmarkCheckedDate != null)
            {
                bookmarkCheckedDate.Text = string.Format("{0:yyyy-MM-dd}", getRectify.CheckedDate);
            }
            Bookmark bookmarkHiddenHazardTypeName = doc.Range.Bookmarks["HiddenHazardTypeName"];
            if (bookmarkHiddenHazardTypeName != null)
            {
                if (getRectify.HiddenHazardType == "2")
                {
                    bookmarkHiddenHazardTypeName.Text = "□一般   √较大   □重大";
                }
                else if (getRectify.HiddenHazardType == "3")
                {
                    bookmarkHiddenHazardTypeName.Text = "□一般    □较大   √重大";
                }
                else
                {
                    bookmarkHiddenHazardTypeName.Text = "√一般   □较大   □重大";
                }
            }

            Bookmark bookmarktab = doc.Range.Bookmarks["tab"];
            if (bookmarktab != null)
            {
                var ItemList = (from x in Funs.DB.Check_RectifyNoticesItem where x.RectifyNoticesId == getRectify.RectifyNoticesId select x).ToList();
                if (ItemList.Count > 0)
                {
                    for (int i = 1; i <= ItemList.Count; i++)
                    {
                        string imgStr = string.Empty;
                        var att = AttachFileService.GetAttachFile(ItemList[i - 1].RectifyNoticesItemId.ToString() + "#1", BLL.Const.ProjectRectifyNoticesMenuId);
                        if (att != null)
                        {
                            List<string> listStr = Funs.GetStrListByStr(att.AttachUrl, ',');
                            if (listStr.Count > 0)
                            {
                                if (File.Exists(rootPath + listStr[0]))
                                {
                                    imgStr = "(详见附图)";
                                }
                            }
                        }
                        bookmarktab.Text += i + "." + ItemList[i - 1].WrongContent + ",整改要求：" + ItemList[i - 1].Requirement + imgStr + ",整改期限：" + string.Format("{0:yyyy-MM-dd}", ItemList[i - 1].LimitTime) + "\r\n";
                    }
                }
            }
            Bookmark bookmarkSignPersonName = doc.Range.Bookmarks["SignPersonName"];
            if (bookmarkSignPersonName != null)
            {
                if (!string.IsNullOrEmpty(getRectify.SignPerson))
                {
                    var getUser = UserService.GetUserByUserId(getRectify.SignPerson);
                    if (getUser != null)
                    {
                        if (!string.IsNullOrEmpty(getUser.SignatureUrl) && File.Exists(rootPath + getUser.SignatureUrl))
                        {
                            var file = rootPath + getUser.SignatureUrl;
                            DocumentBuilder builders = new DocumentBuilder(doc);
                            builders.MoveToBookmark("SignPersonName");
                            builders.InsertImage(file, 80, 20);
                        }
                        else
                        {
                            bookmarkSignPersonName.Text = getUser.UserName;
                        }
                    }
                }
            }
            Bookmark bookmarkSignDate = doc.Range.Bookmarks["SignDate"];
            if (bookmarkSignDate != null)
            {
                bookmarkSignDate.Text = string.Format("{0:yyyy-MM-dd}", getRectify.SignDate);
            }
            Bookmark bookmarkProfessionalEngineerName = doc.Range.Bookmarks["ProfessionalEngineerName"];
            if (bookmarkProfessionalEngineerName != null)
            {
                if (!string.IsNullOrEmpty(getRectify.ProfessionalEngineerId))
                {
                    var getUser = UserService.GetUserByUserId(getRectify.ProfessionalEngineerId);
                    if (getUser != null)
                    {
                        if (!string.IsNullOrEmpty(getUser.SignatureUrl) && File.Exists(rootPath + getUser.SignatureUrl))
                        {
                            var file = rootPath + getUser.SignatureUrl;
                            DocumentBuilder builders = new DocumentBuilder(doc);
                            builders.MoveToBookmark("ProfessionalEngineerName");
                            builders.InsertImage(file, 80, 20);
                        }
                        else
                        {
                            bookmarkProfessionalEngineerName.Text = getUser.UserName;
                        }
                    }
                }
            }
            Bookmark bookmarkProfessionalEngineerTime = doc.Range.Bookmarks["ProfessionalEngineerTime"];
            if (bookmarkProfessionalEngineerTime != null)
            {
                bookmarkProfessionalEngineerTime.Text = string.Format("{0:yyyy-MM-dd}", getRectify.ProfessionalEngineerTime2);
            }
            Bookmark bookmarkConstructionManagerName = doc.Range.Bookmarks["ConstructionManagerName"];
            if (bookmarkConstructionManagerName != null)
            {
                if (!string.IsNullOrEmpty(getRectify.ConstructionManagerId))
                {
                    var getUser = UserService.GetUserByUserId(getRectify.ConstructionManagerId);
                    if (getUser != null)
                    {
                        if (!string.IsNullOrEmpty(getUser.SignatureUrl) && File.Exists(rootPath + getUser.SignatureUrl))
                        {
                            var file = rootPath + getUser.SignatureUrl;
                            DocumentBuilder builders = new DocumentBuilder(doc);
                            builders.MoveToBookmark("ConstructionManagerName");
                            builders.InsertImage(file, 80, 20);
                        }
                        else
                        {
                            bookmarkConstructionManagerName.Text = getUser.UserName;
                        }
                    }
                }
            }
            Bookmark bookmarkConstructionManagerTime = doc.Range.Bookmarks["ConstructionManagerTime"];
            if (bookmarkConstructionManagerTime != null)
            {
                bookmarkConstructionManagerTime.Text = string.Format("{0:yyyy-MM-dd}", getRectify.ConstructionManagerTime2);
            }
            Bookmark bookmarkProjectManagerName = doc.Range.Bookmarks["ProjectManagerName"];
            if (bookmarkProjectManagerName != null)
            {
                if (!string.IsNullOrEmpty(getRectify.ProjectManagerId))
                {
                    var getUser = UserService.GetUserByUserId(getRectify.ProjectManagerId);
                    if (getUser != null)
                    {
                        if (!string.IsNullOrEmpty(getUser.SignatureUrl) && File.Exists(rootPath + getUser.SignatureUrl))
                        {
                            var file = rootPath + getUser.SignatureUrl;
                            DocumentBuilder builders = new DocumentBuilder(doc);
                            builders.MoveToBookmark("ProjectManagerName");
                            builders.InsertImage(file, 80, 20);
                        }
                        else
                        {
                            bookmarkProjectManagerName.Text = getUser.UserName;
                        }
                    }
                }
            }
            Bookmark bookmarkProjectManagerTime = doc.Range.Bookmarks["ProjectManagerTime"];
            if (bookmarkProjectManagerTime != null)
            {
                bookmarkProjectManagerTime.Text = string.Format("{0:yyyy-MM-dd}", getRectify.ProjectManagerTime2);
            }
            Bookmark bookmarkDutyPersonName = doc.Range.Bookmarks["DutyPersonName"];
            if (bookmarkDutyPersonName != null)
            {
                if (!string.IsNullOrEmpty(getRectify.DutyPersonId))
                {
                    var getUser = UserService.GetUserByUserId(getRectify.DutyPersonId);
                    if (getUser != null)
                    {
                        if (!string.IsNullOrEmpty(getUser.SignatureUrl) && File.Exists(rootPath + getUser.SignatureUrl))
                        {

                            var file = rootPath + getUser.SignatureUrl;
                            DocumentBuilder builders = new DocumentBuilder(doc);
                            builders.MoveToBookmark("DutyPersonName");
                            builders.InsertImage(file, 80, 20);

                        }
                        else
                        {
                            bookmarkDutyPersonName.Text = getUser.UserName;
                        }
                    }
                }
            }
            Bookmark bookmarkDutyPersonTime = doc.Range.Bookmarks["DutyPersonTime"];
            if (bookmarkDutyPersonTime != null)
            {
                bookmarkDutyPersonTime.Text = string.Format("{0:yyyy-MM-dd}", getRectify.DutyPersonTime);
            }
            //附图
            var getItem = from x in Funs.DB.Check_RectifyNoticesItem
                          where x.RectifyNoticesId == getRectify.RectifyNoticesId
                          orderby x.RectifyNoticesItemId
                          select x;
            Aspose.Words.DocumentBuilder builder = new Aspose.Words.DocumentBuilder(doc);
            builder.MoveToBookmark("PhotoUrl");
            builder.MoveToBookmark("PhotoUrl");
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
            builder.ParagraphFormat.Alignment = ParagraphAlignment.Left;//水平居中对齐
            builder.CellFormat.Width = 450;
            bool flag = false;
            if (getItem.ToList().Count > 0)
            {
                int j = 1;
                foreach (var item in getItem)
                {
                    var att = AttachFileService.GetAttachFile(item.RectifyNoticesItemId.ToString() + "#1", BLL.Const.ProjectRectifyNoticesMenuId);
                    if (att != null && !string.IsNullOrEmpty(att.AttachUrl))
                    {

                        string imgStr = string.Empty;
                        List<string> listStr = Funs.GetStrListByStr(att.AttachUrl, ',');
                        foreach (var urlItem in listStr)
                        {

                            string url = rootPath + urlItem;
                            if (File.Exists(url))
                            {
                                builder.InsertImage(url, 205, 205);
                                builder.Font.Size = 8;
                                builder.Write("图" + j);
                                builder.Write("   ");
                                j++;
                                flag = true;
                            }
                        }
                    }
                }
            }
            if (!flag)
            {
                builder.Write("无");
                builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;//水平居中对齐
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

        #region 反馈单打印按钮
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnPrinterReturn_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            var getRectify = BLL.RectifyNoticesService.GetRectifyNoticesById(Grid1.SelectedRowID);
            if (getRectify == null)
            {
                Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }

            string rootPath = Server.MapPath("~/");
            string initTemplatePath = string.Empty;
            string uploadfilepath = string.Empty;
            string newUrl = string.Empty;
            string filePath = string.Empty;
            initTemplatePath = "File\\Word\\HSSE\\安全隐患整改反馈单.doc";
            uploadfilepath = rootPath + initTemplatePath;
            newUrl = uploadfilepath.Replace(".doc", string.Format("{0:yyyy-MM}", DateTime.Now) + ".doc");
            filePath = initTemplatePath.Replace(".doc", string.Format("{0:yyyy-MM}", DateTime.Now) + ".pdf");
            File.Copy(uploadfilepath, newUrl);
            ///更新书签内容
            Document doc = new Aspose.Words.Document(newUrl);
            Bookmark bookmarkProjectName = doc.Range.Bookmarks["ProjectName"];
            if (bookmarkProjectName != null)
            {
                bookmarkProjectName.Text = BLL.ProjectService.GetProjectNameByProjectId(getRectify.ProjectId);
            }
            Bookmark IsRectify = doc.Range.Bookmarks["IsRectify"];
            if (IsRectify != null)
            {
                if (getRectify.IsRectify?.ToString() == "True")
                    IsRectify.Text = "合格";
                else
                    IsRectify.Text = "不合格";
            }
            Bookmark bookmarkRectifyNoticesCode = doc.Range.Bookmarks["RectifyNoticesCode"];
            if (bookmarkRectifyNoticesCode != null)
            {
                bookmarkRectifyNoticesCode.Text = getRectify.RectifyNoticesCode;
            }
            Bookmark UnitName = doc.Range.Bookmarks["UnitName"];
            if (UnitName != null)
            {
                UnitName.Text = BLL.UnitService.GetUnitNameByUnitId(getRectify.UnitId);
            }
            Bookmark bookmarkUnitWorkName = doc.Range.Bookmarks["UnitWorkName"];
            if (bookmarkUnitWorkName != null)
            {
                if (!string.IsNullOrEmpty(getRectify.WorkAreaId))
                {
                    bookmarkUnitWorkName.Text = UnitWorkService.GetUnitWorkName(getRectify.WorkAreaId);
                }
            }
            Bookmark bookmarkCheckManNames = doc.Range.Bookmarks["CheckManNames"];
            if (bookmarkCheckManNames != null)
            {
                if (!string.IsNullOrEmpty(getRectify.CheckManIds))
                {
                    bookmarkCheckManNames.Text = BLL.UserService.getUserNamesUserIds(getRectify.CheckManIds);
                }
            }
            Bookmark bookmarkCheckedDate = doc.Range.Bookmarks["CheckedDate"];
            if (bookmarkCheckedDate != null)
            {
                bookmarkCheckedDate.Text = string.Format("{0:yyyy-MM-dd}", getRectify.CheckedDate);
            }
            Bookmark bookmarkHiddenHazardTypeName = doc.Range.Bookmarks["HiddenHazardTypeName"];
            if (bookmarkHiddenHazardTypeName != null)
            {
                if (getRectify.HiddenHazardType == "2")
                {
                    bookmarkHiddenHazardTypeName.Text = "☐一般   √较大   ☐重大";
                }
                else if (getRectify.HiddenHazardType == "3")
                {
                    bookmarkHiddenHazardTypeName.Text = "☐一般    ☐较大   √重大";
                }
                else
                {
                    bookmarkHiddenHazardTypeName.Text = "√一般   ☐较大   ☐重大";
                }
            }
            Bookmark bookmarktab = doc.Range.Bookmarks["tab"];
            if (bookmarktab != null)
            {
                var ItemList = (from x in Funs.DB.Check_RectifyNoticesItem where x.RectifyNoticesId == getRectify.RectifyNoticesId select x).ToList();
                if (ItemList.Count > 0)
                {
                    for (int i = 1; i <= ItemList.Count; i++)
                    {
                        string imgStr = string.Empty;
                        var att = AttachFileService.GetAttachFile(ItemList[i - 1].RectifyNoticesItemId.ToString() + "#2", BLL.Const.ProjectRectifyNoticesMenuId);
                        if (att != null)
                        {
                            List<string> listStr = Funs.GetStrListByStr(att.AttachUrl, ',');
                            if (listStr.Count > 0)
                            {
                                if (File.Exists(rootPath + listStr[0]))
                                {
                                    imgStr = "(详见附图)";
                                }
                            }
                        }
                        if (ItemList[i - 1].IsRectify?.ToString() == "True")
                            bookmarktab.Text += i + "." + ItemList[i - 1].RectifyResults + imgStr + "，整改结果：合格。" + "\r\n";
                        else
                            bookmarktab.Text += i + "." + ItemList[i - 1].RectifyResults + imgStr + "，整改结果：不合格。" + "\r\n";
                    }
                }
            }

            Bookmark bookmarkDutyPersonName = doc.Range.Bookmarks["DutyPersonName"];
            if (bookmarkDutyPersonName != null)
            {
                if (!string.IsNullOrEmpty(getRectify.DutyPersonId))
                {
                    var getUser = UserService.GetUserByUserId(getRectify.DutyPersonId);
                    if (getUser != null)
                    {
                        if (!string.IsNullOrEmpty(getUser.SignatureUrl) && File.Exists(rootPath + getUser.SignatureUrl))
                        {
                            var file = rootPath + getUser.SignatureUrl;
                            DocumentBuilder builders = new DocumentBuilder(doc);
                            builders.MoveToBookmark("DutyPersonName");
                            builders.InsertImage(file, 80, 20);
                        }
                        else
                        {
                            bookmarkDutyPersonName.Text = getUser.UserName;
                        }
                    }
                }
            }
            Bookmark bookmarkDutyPersonTime = doc.Range.Bookmarks["DutyPersonTime"];
            if (bookmarkDutyPersonTime != null)
            {
                bookmarkDutyPersonTime.Text = string.Format("{0:yyyy-MM-dd}", getRectify.DutyPersonTime);
            }
            Bookmark bookmarkUnitHeadManName = doc.Range.Bookmarks["UnitHeadManName"];
            if (bookmarkUnitHeadManName != null)
            {
                if (!string.IsNullOrEmpty(getRectify.UnitHeadManId))
                {
                    var getUser = UserService.GetUserByUserId(getRectify.UnitHeadManId);
                    if (getUser != null)
                    {
                        if (!string.IsNullOrEmpty(getUser.SignatureUrl) && File.Exists(rootPath + getUser.SignatureUrl))
                        {
                            var file = rootPath + getUser.SignatureUrl;
                            DocumentBuilder builders = new DocumentBuilder(doc);
                            builders.MoveToBookmark("UnitHeadManName");
                            builders.InsertImage(file, 80, 20);
                        }
                        else
                        {
                            bookmarkUnitHeadManName.Text = getUser.UserName;
                        }
                    }
                }
            }
            Bookmark bookmarkUnitHeadManDate = doc.Range.Bookmarks["UnitHeadManDate"];
            if (bookmarkUnitHeadManDate != null)
            {
                bookmarkUnitHeadManDate.Text = string.Format("{0:yyyy-MM-dd}", getRectify.UnitHeadManDate);
            }
            Bookmark bookmarkReCheckOpinion = doc.Range.Bookmarks["ReCheckOpinion"];
            if (bookmarkReCheckOpinion != null)
            {
                if (!string.IsNullOrEmpty(getRectify.ReCheckOpinion))
                {
                    bookmarkReCheckOpinion.Text = getRectify.ReCheckOpinion;
                }
            }
            Bookmark bookmarkCheckPersonName = doc.Range.Bookmarks["CheckPersonName"];
            if (bookmarkCheckPersonName != null)
            {
                if (!string.IsNullOrEmpty(getRectify.CheckPerson))
                {
                    var getUser = UserService.GetUserByUserId(getRectify.CheckPerson);
                    if (getUser != null)
                    {
                        if (!string.IsNullOrEmpty(getUser.SignatureUrl) && File.Exists(rootPath + getUser.SignatureUrl))
                        {
                            var file = rootPath + getUser.SignatureUrl;
                            DocumentBuilder builders = new DocumentBuilder(doc);
                            builders.MoveToBookmark("CheckPersonName");
                            builders.InsertImage(file, 80, 20);
                        }
                        else
                        {
                            bookmarkCheckPersonName.Text = getUser.UserName;
                        }
                    }
                }
            }
            Bookmark bookmarkReCheckDate = doc.Range.Bookmarks["ReCheckDate"];
            if (bookmarkReCheckDate != null)
            {
                bookmarkReCheckDate.Text = string.Format("{0:yyyy-MM-dd}", getRectify.ReCheckDate);
            }
            Bookmark bookmarkProfessionalEngineerName = doc.Range.Bookmarks["ProfessionalEngineerName"];
            if (bookmarkProfessionalEngineerName != null)
            {
                var getUser = UserService.GetUserByUserId(getRectify.ProfessionalEngineerId);
                if (getUser != null)
                {
                    if (!string.IsNullOrEmpty(getUser.SignatureUrl) && File.Exists(rootPath + getUser.SignatureUrl))
                    {

                        var file = rootPath + getUser.SignatureUrl;
                        DocumentBuilder builders = new DocumentBuilder(doc);
                        builders.MoveToBookmark("ProfessionalEngineerName");
                        builders.InsertImage(file, 80, 20);
                    }
                    else
                    {
                        bookmarkProfessionalEngineerName.Text = getUser.UserName;
                    }
                }
            }
            Bookmark bookmarkProfessionalEngineerTime = doc.Range.Bookmarks["ProfessionalEngineerTime"];
            if (bookmarkProfessionalEngineerTime != null)
            {
                bookmarkProfessionalEngineerTime.Text = string.Format("{0:yyyy-MM-dd}", getRectify.ProfessionalEngineerTime2);
            }
            Bookmark bookmarkConstructionManagerName = doc.Range.Bookmarks["ConstructionManagerName"];
            if (bookmarkConstructionManagerName != null)
            {
                if (!string.IsNullOrEmpty(getRectify.ConstructionManagerId))
                {

                    var getUser = UserService.GetUserByUserId(getRectify.ConstructionManagerId);
                    if (getUser != null)
                    {
                        if (!string.IsNullOrEmpty(getUser.SignatureUrl) && File.Exists(rootPath + getUser.SignatureUrl))
                        {

                            var file = rootPath + getUser.SignatureUrl;
                            DocumentBuilder builders = new DocumentBuilder(doc);
                            builders.MoveToBookmark("ConstructionManagerName");
                            builders.InsertImage(file, 80, 20);

                        }
                        else
                        {
                            bookmarkConstructionManagerName.Text = getUser.UserName;
                        }
                    }
                }
            }
            Bookmark bookmarkConstructionManagerTime = doc.Range.Bookmarks["ConstructionManagerTime"];
            if (bookmarkConstructionManagerTime != null)
            {
                bookmarkConstructionManagerTime.Text = string.Format("{0:yyyy-MM-dd}", getRectify.ConstructionManagerTime2);
            }
            Bookmark bookmarkProjectManagerName = doc.Range.Bookmarks["ProjectManagerName"];
            if (bookmarkProjectManagerName != null)
            {
                if (getRectify != null)
                {
                    if (!string.IsNullOrEmpty(getRectify.ProjectManagerId))
                    {
                        var getUser = UserService.GetUserByUserId(getRectify.ProjectManagerId);
                        if (getUser != null)
                        {
                            if (!string.IsNullOrEmpty(getUser.SignatureUrl) && File.Exists(rootPath + getUser.SignatureUrl))
                            {
                                var file = rootPath + getUser.SignatureUrl;
                                DocumentBuilder builders = new DocumentBuilder(doc);
                                builders.MoveToBookmark("ProjectManagerName");
                                builders.InsertImage(file, 80, 20);

                            }
                            else
                            {
                                bookmarkProjectManagerName.Text = getUser.UserName;
                            }
                        }
                    }

                }
            }
            Bookmark bookmarkProjectManagerTime = doc.Range.Bookmarks["ProjectManagerTime"];
            if (bookmarkProjectManagerTime != null)
            {
                bookmarkProjectManagerTime.Text = string.Format("{0:yyyy-MM-dd}", getRectify.ProjectManagerTime2);
            }
            //附图
            var getItem = from x in Funs.DB.Check_RectifyNoticesItem
                          where x.RectifyNoticesId == getRectify.RectifyNoticesId
                          orderby x.RectifyNoticesItemId
                          select x;
            Aspose.Words.DocumentBuilder builder = new Aspose.Words.DocumentBuilder(doc);
            builder.MoveToBookmark("PhotoUrl");
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
            builder.ParagraphFormat.Alignment = ParagraphAlignment.Left;//水平居中对齐
            builder.CellFormat.Width = 450;
            bool flag = false;
            if (getItem.ToList().Count > 0)
            {
                int j = 1;
                foreach (var item in getItem)
                {
                    var att = AttachFileService.GetAttachFile(item.RectifyNoticesItemId.ToString() + "#2", BLL.Const.ProjectRectifyNoticesMenuId);
                    if (att != null && !string.IsNullOrEmpty(att.AttachUrl))
                    {

                        string imgStr = string.Empty;
                        List<string> listStr = Funs.GetStrListByStr(att.AttachUrl, ',');
                        foreach (var urlItem in listStr)
                        {

                            string url = rootPath + urlItem;
                            if (File.Exists(url))
                            {
                                builder.InsertImage(url, 205, 205);
                                builder.Font.Size = 8;
                                builder.Write("图" + j);
                                builder.Write("   ");
                                j++;
                                flag = true;
                            }
                        }
                    }
                }
            }
            if (!flag)
            {
                builder.Write("无");
                builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;//水平居中对齐
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
            //PrinterDocService.PrinterDocMethod(Const.ProjectRectifyNoticesMenuId + "#2", Grid1.SelectedRowID, "安全隐患整改反馈单");
        }

        #endregion

        protected void btnView_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }

            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("RectifyNoticesView.aspx?RectifyNoticesId={0}", Grid1.SelectedRowID, "查看 - ")));
        }
    }
}