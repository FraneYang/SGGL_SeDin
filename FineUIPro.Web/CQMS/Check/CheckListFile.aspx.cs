using Aspose.Words;
using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FineUIPro.Web.CQMS.Check
{
    public partial class CheckListFile : PageBase
    {
        /// <summary>
        /// 项目id
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
        #region 加载页面
        /// <summary>
        /// 加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {

            // 表头过滤
            //FilterDataRowItem = FilterDataRowItemImplement;
            if (!IsPostBack)
            {
                this.ProjectId = this.CurrUser.LoginProjectId;
                //if (!string.IsNullOrEmpty(Request.Params["projectId"]) && Request.Params["projectId"] != this.ProjectId)
                //{
                //    this.ProjectId = Request.Params["projectId"];
                //}
                //权限按钮方法
                UnitService.InitUnitByProjectIdUnitTypeDropDownList(drpSponsorUnit, this.CurrUser.LoginProjectId, BLL.Const.ProjectUnitType_2, true);
                QualityQuestionTypeService.InitQualityQuestionTypeDownList(drpQuestionType, true);
                UnitWorkService.InitUnitWorkDownList(drpUnitWork, this.CurrUser.LoginProjectId, true);
                CNProfessionalService.InitCNProfessionalDownList(drpCNProfessional, true);
                ddlPageSize.SelectedValue = Grid1.PageSize.ToString();

                // 绑定表格
                BindGrid();



            }


        }
        #endregion
        //public System.Web.UI.WebControls.ListItem[] GetHandelStatus()
        //{
        //    var list = Handelstatus();
        //    System.Web.UI.WebControls.ListItem[]  litem = new System.Web.UI.WebControls.ListItem[list.Count];

        //    for (int i = 0; i < list.Count; i++)
        //    {

        //        litem[i]= new System.Web.UI.WebControls.ListItem(list.Keys.ToString(),list.Values.ToString());

        //     }      
        //    return litem;
        //}
        /// <summary>
        /// 整改状态
        /// </summary>
        /// <returns></returns>
        protected IDictionary<int, string> Handelstatus()
        {
            Dictionary<int, string> dic = new Dictionary<int, string>();
            dic.Add(1, "未确认");
            dic.Add(2, "已闭环");
            dic.Add(3, "超期未整改");
            dic.Add(4, "未整改");
            return dic;
        }
        public Task<DataTable> data()
        {
            Task<DataTable> task = new Task<DataTable>(() =>
            {
                return ChecklistData();
            });
            task.Start();
            return task;
        }
        protected DataTable ChecklistData()
        {
            string strSql = @"SELECT chec.CheckControlCode,chec.CheckSite,chec.ProjectId,chec.unitId,cNProfessional.ProfessionalName,"
                          + @" QualityQuestionType.QualityQuestionType as QuestionType,"
                          + @" chec.checkman,chec.CheckDate,chec.DocCode,chec.submitman,chec.state,chec.CNProfessionalCode,"
                          + @" unit.UnitName,unitWork.UnitWorkName,u.userName "
                          + @" FROM Check_CheckControl chec"
                          + @" left join Base_Unit unit on unit.unitId=chec.unitId"
                          + @" left join Base_CNProfessional cNProfessional on cNProfessional.CNProfessionalId=chec.CNProfessionalCode"
                          + @" left join WBS_UnitWork unitWork on unitWork.UnitWorkId = chec.UnitWorkId"
                          + @" left join Base_QualityQuestionType QualityQuestionType on QualityQuestionType.QualityQuestionTypeId = chec.QuestionType"
                          + @" left join sys_User u on u.userId = chec.CheckMan"
                          + @" where chec.ProjectId=@ProjectId";

            List<SqlParameter> listStr = new List<SqlParameter>();
            listStr.Add(new SqlParameter("@ProjectId", this.CurrUser.LoginProjectId));
            strSql += " AND (chec.CheckDate>=@startTime or @startTime='') and (chec.CheckDate<=@endTime or @endTime='') ";
            listStr.Add(new SqlParameter("@startTime", !string.IsNullOrEmpty(txtStartTime.Text.Trim()) ? txtStartTime.Text.Trim() + " 00:00:00" : ""));
            listStr.Add(new SqlParameter("@endTime", !string.IsNullOrEmpty(txtEndTime.Text.Trim()) ? txtEndTime.Text.Trim() + "  23:59:59" : ""));
            if (drpSponsorUnit.SelectedValue != BLL.Const._Null)
            {
                strSql += " AND chec.unitId=@unitId";
                listStr.Add(new SqlParameter("@unitId", drpSponsorUnit.SelectedValue));
            }
            if (drpUnitWork.SelectedValue != Const._Null)
            {
                strSql += " AND chec.unitworkId=@unitworkId";
                listStr.Add(new SqlParameter("@unitworkId", drpUnitWork.SelectedValue));
            }
            if (drpCNProfessional.SelectedValue != Const._Null)
            {
                strSql += " AND chec.CNProfessionalCode=@CNProfessionalCode";
                listStr.Add(new SqlParameter("@CNProfessionalCode", drpCNProfessional.SelectedValue));
            }
            if (drpQuestionType.SelectedValue != Const._Null)
            {
                strSql += " AND chec.QuestionType=@QuestionType";
                listStr.Add(new SqlParameter("@QuestionType", drpQuestionType.SelectedValue));
            }
            strSql += " AND chec.State=" + Const.CheckControl_Complete;
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            return tb;
        }
        public string Convertstatus(Object code)
        {
            Model.Check_CheckControl checkControl = BLL.CheckControlService.GetCheckControl(code.ToString());
            if (checkControl.State.Equals("5") || checkControl.State.Equals("6"))
            {
                return "未确认";
            }
            else if (checkControl.State == Const.CheckControl_Complete)
            { //闭环
                return "已闭环";
            }
            //else if( checkControl.LimitDate> )
            else if (Convert.ToDateTime(checkControl.LimitDate).AddDays(1) < DateTime.Now)  //延期未整改
            {
                return "超期未整改";

            }
            else  //期内未整改
            {
                return "未整改";

            }
        }
        /// <summary>
        /// 绑定数据
        /// </summary>

        public void BindGrid()
        {
            DataTable tb = ChecklistData();

            // 2.获取当前分页数据
            //var table = this.GetPagedDataTable(Grid1, tb1);

            Grid1.RecordCount = tb.Rows.Count;
            tb = GetFilteredTable(Grid1.FilteredData, tb);
            var table = this.GetPagedDataTable(Grid1, tb);

            Grid1.DataSource = table;
            Grid1.DataBind();

            for (int i = 0; i < Grid1.Rows.Count; i++)
            {
                string rowID = Grid1.Rows[i].DataKeys[0].ToString();
                if (rowID.Count() > 0)
                {
                    Model.Check_CheckControl checkControl = BLL.CheckControlService.GetCheckControl(rowID);
                    if (checkControl.State.Equals("5") || checkControl.State.Equals("6"))
                    {
                        Grid1.Rows[i].RowCssClass = "LightGreen";//未确认      
                    }
                    else if (checkControl.State == Const.CheckControl_Complete)
                    { //闭环
                        Grid1.Rows[i].RowCssClass = "Green";

                    }
                    else if (Convert.ToDateTime(checkControl.LimitDate).AddDays(1) < DateTime.Now)  //延期未整改
                    {
                        Grid1.Rows[i].RowCssClass = "HotPink";
                    }
                    else  //期内未整改
                    {
                        Grid1.Rows[i].RowCssClass = " Yellow ";
                    }
                }
            }
        }


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

        #region 过滤表头、排序、分页、关闭窗口
        /// <summary>
        /// 过滤表头
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_FilterChange(object sender, EventArgs e)
        {
            BindGrid();
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            //Grid1.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_Sort(object sender, GridSortEventArgs e)
        {
            //Grid1.SortDirection = e.SortDirection;
            //Grid1.SortField = e.SortField;
            BindGrid();
        }

        /// <summary>
        /// 分页显示条数下拉框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Grid1.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
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
        protected void btnMenuView_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            string codes = Grid1.SelectedRowID.Split(',')[0];
            var checks = BLL.CheckControlService.GetCheckControl(codes);

            if (checks != null)
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("CheckListView.aspx?CheckControlCode={0}", codes, "查看 - ")));
            }
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void btnRset_Click(object sender, EventArgs e)
        {
            drpSponsorUnit.SelectedIndex = 0;
            drpCNProfessional.SelectedIndex = 0;
            drpQuestionType.SelectedIndex = 0;
            drpUnitWork.SelectedIndex = 0;
            txtStartTime.Text = "";
            txtEndTime.Text = "";
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
            if (e.CommandName == "export")
            {
                string rootPath = Server.MapPath("~/");
                string initTemplatePath = string.Empty;
                string uploadfilepath = string.Empty;
                string newUrl = string.Empty;
                string unitType = string.Empty;
                string auditDate = string.Empty;
                string filePath = string.Empty;
                string auditMan1 = string.Empty;
                string auditMan2 = string.Empty;
                string auditDate1 = string.Empty;
                string approveIdea1 = string.Empty;
                string approveIdea2 = string.Empty;
                string approveIdea3 = string.Empty;
                string auditDate2 = string.Empty;
                string auditDate3 = string.Empty;
                string auditMan3 = string.Empty;
                string auditMan4 = string.Empty;
                string approveIdea = string.Empty;
                Model.Check_CheckControl checkControl = CheckControlService.GetCheckControl(fileId);
                var checkAtach = AttachFileService.Getfile(checkControl.CheckControlCode, Const.CheckListMenuId);
                if (checkAtach)
                {
                    initTemplatePath = Const.CheckListTemplateUrl;
                    uploadfilepath = rootPath + initTemplatePath;
                    newUrl = uploadfilepath.Replace(".doc", checkControl.DocCode.Replace("/", "-") + ".doc");
                    filePath = initTemplatePath.Replace(".doc", checkControl.DocCode.Replace("/", "-") + ".pdf");
                }
                else
                {
                    initTemplatePath = Const.CheckListTemplateUrl2;
                    uploadfilepath = rootPath + initTemplatePath;
                    newUrl = uploadfilepath.Replace("2.doc", checkControl.DocCode.Replace("/", "-") + ".doc");
                    filePath = initTemplatePath.Replace("2.doc", checkControl.DocCode.Replace("/", "-") + ".pdf");
                }
                File.Copy(uploadfilepath, newUrl);
                Document doc = new Aspose.Words.Document(newUrl);
                Bookmark bookmarkProjectName = doc.Range.Bookmarks["ProjectName"];
                if (bookmarkProjectName != null)
                {
                    var project = ProjectService.GetProjectByProjectId(checkControl.ProjectId);
                    if (project != null)
                    {
                        bookmarkProjectName.Text = project.ProjectName;
                    }
                }
                Bookmark bookmarkDocCode = doc.Range.Bookmarks["DocCode"];
                if (bookmarkDocCode != null)
                {
                    bookmarkDocCode.Text = checkControl.DocCode;
                }
                Bookmark bookmarkUnit = doc.Range.Bookmarks["Unit"];
                if (bookmarkUnit != null)
                {
                    var unit = UnitService.GetUnitByUnitId(checkControl.UnitId);
                    if (unit != null)
                    {
                        bookmarkUnit.Text = unit.UnitName;
                    }
                }
                Bookmark bookmarkUnitWork = doc.Range.Bookmarks["UnitWork"];
                if (bookmarkUnitWork != null)
                {
                    var unitWork = UnitWorkService.GetUnitWorkByUnitWorkId(checkControl.UnitWorkId.ToString());
                    if (unitWork != null)
                    {
                        bookmarkUnitWork.Text = unitWork.UnitWorkName;
                    }
                }
                Bookmark bookmarkCNProfessional = doc.Range.Bookmarks["CNProfessional"];
                if (bookmarkCNProfessional != null)
                {
                    var cNProfessional = CNProfessionalService.GetCNProfessional(checkControl.CNProfessionalCode);
                    if (cNProfessional != null)
                    {
                        bookmarkCNProfessional.Text = cNProfessional.ProfessionalName;
                    }
                }
                Bookmark bookmarkQuestionType = doc.Range.Bookmarks["QuestionType"];
                if (bookmarkQuestionType != null)
                {
                    string questionType = string.Empty;
                    var lists = BLL.QualityQuestionTypeService.GetList();
                    if (lists.Count > 0)
                    {
                        for (int i = 0; i < lists.Count; i++)
                        {
                            if (checkControl.QuestionType == lists[i].QualityQuestionTypeId)
                            {
                                questionType += "■" + lists[i].QualityQuestionType + "   ";
                            }
                            else
                            {
                                questionType += "□" + lists[i].QualityQuestionType + "   ";
                            }

                        }
                    }
                    bookmarkQuestionType.Text = questionType;
                }
                Bookmark bookmarkCheckSite = doc.Range.Bookmarks["CheckSite"];
                if (bookmarkCheckSite != null)
                {
                    bookmarkCheckSite.Text = checkControl.CheckSite;
                }
                Bookmark bookmarkQuestionDef = doc.Range.Bookmarks["QuestionDef"];
                if (bookmarkQuestionDef != null)
                {
                    bookmarkQuestionDef.Text = checkControl.QuestionDef;
                }
                Bookmark bookmarkAttachUrl = doc.Range.Bookmarks["AttachUrl"];
                if (bookmarkAttachUrl != null)
                {
                    if (AttachFileService.Getfile(checkControl.CheckControlCode, Const.CheckListMenuId))
                    {
                        bookmarkAttachUrl.Text = "见附页";
                    }
                    else
                    {
                        bookmarkAttachUrl.Text = "无";
                    }
                }
                Bookmark bookmarkYear = doc.Range.Bookmarks["Year"];
                if (bookmarkYear != null)
                {
                    bookmarkYear.Text = checkControl.LimitDate.Value.Year.ToString();
                }
                Bookmark bookmarkMonth = doc.Range.Bookmarks["Month"];
                if (bookmarkMonth != null)
                {
                    bookmarkMonth.Text = checkControl.LimitDate.Value.Month.ToString();
                }
                Bookmark bookmarkDay = doc.Range.Bookmarks["Day"];
                if (bookmarkDay != null)
                {
                    bookmarkDay.Text = checkControl.LimitDate.Value.Day.ToString();
                }
                Bookmark bookmarkCompileMan = doc.Range.Bookmarks["CompileMan"];
                if (bookmarkCompileMan != null)
                {
                    Model.Check_CheckControlApprove approve = CheckControlApproveService.GetComplie(fileId);
                    if (approve != null)
                    {
                        Model.Sys_User user = UserService.GetUserByUserId(approve.ApproveMan);
                        if (user != null)
                        {
                            var file = user.SignatureUrl;
                            if (!string.IsNullOrWhiteSpace(file))
                            {
                                string url = rootPath + file;
                                DocumentBuilder builder = new DocumentBuilder(doc);
                                builder.MoveToBookmark("CompileMan");
                                if (!string.IsNullOrEmpty(url))
                                {
                                    System.Drawing.Size JpgSize;
                                    float Wpx;
                                    float Hpx;
                                    UploadAttachmentService.getJpgSize(rootPath + file, out JpgSize, out Wpx, out Hpx);
                                    double i = 1;
                                    i = JpgSize.Width / 50.0;
                                    if (File.Exists(url))
                                    {
                                        builder.InsertImage(url, JpgSize.Width / i, JpgSize.Height / i);
                                    }
                                    else
                                    {
                                        bookmarkCompileMan.Text = user.UserName;
                                    }


                                }
                            }
                            else
                            {
                                bookmarkCompileMan.Text = user.UserName;
                            }
                        }


                    }
                }
                Bookmark bookmarkAuditMan1 = doc.Range.Bookmarks["AuditMan1"];
                if (bookmarkAuditMan1 != null)
                {
                    Model.Check_CheckControlApprove approve = CheckControlApproveService.GetAudit1(fileId);
                    if (approve != null)
                    {
                        Model.Sys_User user = UserService.GetUserByUserId(approve.ApproveMan);
                        if (user != null)
                        {
                            var file = user.SignatureUrl;
                            if (!string.IsNullOrWhiteSpace(file))
                            {
                                string url = rootPath + file;
                                DocumentBuilder builder = new DocumentBuilder(doc);
                                builder.MoveToBookmark("AuditMan1");
                                if (!string.IsNullOrEmpty(url))
                                {
                                    System.Drawing.Size JpgSize;
                                    float Wpx;
                                    float Hpx;
                                    UploadAttachmentService.getJpgSize(url, out JpgSize, out Wpx, out Hpx);
                                    double i = 1;
                                    i = JpgSize.Width / 50.0;
                                    if (File.Exists(url))
                                    {
                                        builder.InsertImage(url, JpgSize.Width / i, JpgSize.Height / i);
                                    }
                                    else
                                    {
                                        bookmarkAuditMan1.Text = user.UserName;
                                    }


                                }
                            }
                            else
                            {
                                bookmarkAuditMan1.Text = user.UserName;
                            }
                        }
                        approveIdea1 = approve.ApproveIdea;


                    }
                }
                Bookmark bookmarkAuditDate1 = doc.Range.Bookmarks["AuditDate1"];
                if (bookmarkAuditDate1 != null)
                {
                    Model.Check_CheckControlApprove approve1 = CheckControlApproveService.GetComplie(fileId);
                    if (approve1 != null)
                    {
                        if (approve1.ApproveDate != null)
                        {
                            auditDate = string.Format("{0:yyyy-MM-dd}", approve1.ApproveDate);
                        }
                    }
                    Model.Check_CheckControlApprove approve2 = CheckControlApproveService.GetAudit1(fileId);
                    if (approve2 != null)
                    {
                        if (approve2.ApproveDate != null)
                        {
                            auditDate = string.Format("{0:yyyy-MM-dd}", approve2.ApproveDate);
                        }
                    }
                    bookmarkAuditDate1.Text = auditDate;

                }
                Bookmark bookmarkHandleWay = doc.Range.Bookmarks["HandleWay"];
                if (bookmarkHandleWay != null)
                {
                    bookmarkHandleWay.Text = checkControl.HandleWay ?? "";
                    Model.Check_CheckControlApprove approve = CheckControlApproveService.GetAudit2(fileId);
                    if (approve != null)
                    {
                        if (!string.IsNullOrEmpty(approve.ApproveIdea))
                        {
                            bookmarkHandleWay.Text += "\r\n" + approve.ApproveIdea;
                        }
                    }
                    Model.Check_CheckControlApprove approve2 = CheckControlApproveService.GetAudit3(fileId);
                    if (approve2 != null)
                    {
                        if (!string.IsNullOrEmpty(approve2.ApproveIdea))
                        {
                            bookmarkHandleWay.Text += "\r\n" + approve2.ApproveIdea;
                        }
                    }
                }
                Bookmark bookmarkReAttachUrl = doc.Range.Bookmarks["ReAttachUrl"];
                if (bookmarkReAttachUrl != null)
                {
                    if (AttachFileService.Getfile(checkControl.CheckControlCode + "r", Const.CheckListMenuId))
                    {
                        bookmarkReAttachUrl.Text = "见附页";
                    }
                    else
                    {
                        bookmarkReAttachUrl.Text = "无";
                    }
                }
                Bookmark bookmarkAuditMan2 = doc.Range.Bookmarks["AuditMan2"];
                if (bookmarkAuditMan2 != null)
                {
                    Model.Check_CheckControlApprove approve = CheckControlApproveService.GetAudit3(fileId);
                    if (approve != null)
                    {
                        Model.Sys_User user = UserService.GetUserByUserId(approve.ApproveMan);
                        if (user != null)
                        {
                            var file = user.SignatureUrl;
                            if (!string.IsNullOrWhiteSpace(file))
                            {
                                string url = rootPath + file;
                                DocumentBuilder builder = new DocumentBuilder(doc);
                                builder.MoveToBookmark("AuditMan2");
                                if (!string.IsNullOrEmpty(url))
                                {
                                    System.Drawing.Size JpgSize;
                                    float Wpx;
                                    float Hpx;
                                    UploadAttachmentService.getJpgSize(url, out JpgSize, out Wpx, out Hpx);
                                    double i = 1;
                                    i = JpgSize.Width / 50.0;
                                    if (File.Exists(url))
                                    {
                                        builder.InsertImage(url, JpgSize.Width / i, JpgSize.Height / i);
                                    }
                                    else
                                    {
                                        bookmarkAuditMan2.Text = user.UserName;
                                    }

                                }
                            }
                            else
                            {
                                bookmarkAuditMan2.Text = user.UserName;
                            }
                        }
                    }
                }
                Bookmark bookmarkAuditDate2 = doc.Range.Bookmarks["AuditDate2"];
                if (bookmarkAuditDate2 != null)
                {
                    Model.Check_CheckControlApprove approve1 = CheckControlApproveService.GetAudit2(fileId);
                    if (approve1 != null)
                    {
                        if (approve1.ApproveDate != null)
                        {
                            auditDate2 = string.Format("{0:yyyy-MM-dd}", approve1.ApproveDate);
                        }
                    }
                    Model.Check_CheckControlApprove approve2 = CheckControlApproveService.GetAudit3(fileId);
                    if (approve2 != null)
                    {
                        if (approve2.ApproveDate != null)
                        {
                            auditDate2 = string.Format("{0:yyyy-MM-dd}", approve2.ApproveDate);
                        }
                    }
                    bookmarkAuditDate2.Text = auditDate2;
                }
                Bookmark bookmarkIdea = doc.Range.Bookmarks["Idea"];
                if (bookmarkIdea != null)
                {
                    if (!string.IsNullOrWhiteSpace(approveIdea1))
                    {
                        bookmarkIdea.Text = approveIdea1;
                    }

                }
                Bookmark bookmarkApproveIdea = doc.Range.Bookmarks["ApproveIdea"];
                if (bookmarkApproveIdea != null)
                {
                    Model.Check_CheckControlApprove approve = CheckControlApproveService.GetAudit4(fileId);
                    Model.Check_CheckControlApprove approve2 = CheckControlApproveService.GetAudit5(fileId);
                    if (approve != null)
                    {
                        Model.Sys_User user = UserService.GetUserByUserId(approve.ApproveMan);

                        if (user != null)
                        {
                            auditMan3 = user.UserName;
                        }
                        //if (user != null)
                        //{

                        //    auditMan3 = user.UserName;
                        //}
                        //approveIdea = approve.ApproveIdea + "\r\n";
                        approveIdea = approve.ApproveIdea + "\r\n";
                        if (approve.ApproveDate != null)
                        {
                            auditDate3 = string.Format("{0:yyyy-MM-dd}", approve.ApproveDate);
                        }
                    }
                    if (approve2 != null)
                    {
                        Model.Sys_User user = UserService.GetUserByUserId(approve2.ApproveMan);
                        if (user != null)
                        {
                            var file = user.SignatureUrl;
                            if (!string.IsNullOrWhiteSpace(file))
                            {
                                string url = rootPath + file;
                                DocumentBuilder builder = new DocumentBuilder(doc);
                                builder.MoveToBookmark("AuditMan4");
                                if (!string.IsNullOrEmpty(url))
                                {
                                    System.Drawing.Size JpgSize;
                                    float Wpx;
                                    float Hpx;
                                    UploadAttachmentService.getJpgSize(url, out JpgSize, out Wpx, out Hpx);
                                    double i = 1;
                                    i = JpgSize.Width / 50.0;
                                    if (File.Exists(url))
                                    {
                                        builder.InsertImage(url, JpgSize.Width / i, JpgSize.Height / i);
                                    }
                                    else
                                    {
                                        auditMan4 = user.UserName;
                                    }
                                }
                            }
                            else
                            {
                                auditMan4 = user.UserName;
                            }
                        }

                        approveIdea += approve2.ApproveIdea;
                        if (approve2.ApproveDate != null)
                        {
                            auditDate3 = string.Format("{0:yyyy-MM-dd}", approve2.ApproveDate);
                        }
                    }
                    if (string.IsNullOrEmpty(approveIdea) || approveIdea == "\r\n")
                    {
                        approveIdea = "同意";
                    }
                    bookmarkApproveIdea.Text = approveIdea;
                }
                Bookmark bookmarkAuditMan3 = doc.Range.Bookmarks["AuditMan3"];
                if (bookmarkAuditMan3 != null)
                {
                    Model.Check_CheckControlApprove approve = CheckControlApproveService.GetAudit4(fileId);
                    if (approve != null)
                    {
                        Model.Sys_User user = UserService.GetUserByUserId(approve.ApproveMan);
                        if (user != null)
                        {
                            var file = user.SignatureUrl;
                            if (!string.IsNullOrWhiteSpace(file))
                            {
                                string url = rootPath + file;
                                DocumentBuilder builder = new DocumentBuilder(doc);
                                builder.MoveToBookmark("AuditMan3");
                                if (!string.IsNullOrEmpty(url))
                                {
                                    System.Drawing.Size JpgSize;
                                    float Wpx;
                                    float Hpx;
                                    UploadAttachmentService.getJpgSize(url, out JpgSize, out Wpx, out Hpx);
                                    double i = 1;
                                    i = JpgSize.Width / 50.0;
                                    if (File.Exists(url))
                                    {
                                        builder.InsertImage(url, JpgSize.Width / i, JpgSize.Height / i);
                                    }
                                    else
                                    {
                                        bookmarkAuditMan3.Text = user.UserName;
                                    }

                                }
                            }
                            else
                            {
                                bookmarkAuditMan3.Text = user.UserName;
                            }
                        }

                    }
                }
                Bookmark bookmarkAuditMan4 = doc.Range.Bookmarks["AuditMan4"];
                if (bookmarkAuditMan4 != null)
                {
                    bookmarkAuditMan4.Text = auditMan4;
                }
                Bookmark bookmarkAuditMan5 = doc.Range.Bookmarks["AuditMan5"];
                if (bookmarkAuditMan5 != null)
                {
                    Model.Check_CheckControlApprove approve = CheckControlApproveService.GetAudit2(fileId);
                    if (approve != null)
                    {
                        Model.Sys_User user = UserService.GetUserByUserId(approve.ApproveMan);
                        if (user != null)
                        {
                            var file = user.SignatureUrl;
                            if (!string.IsNullOrWhiteSpace(file))
                            {
                                string url = rootPath + file;
                                DocumentBuilder builder = new DocumentBuilder(doc);
                                builder.MoveToBookmark("AuditMan5");
                                if (!string.IsNullOrEmpty(url))
                                {
                                    System.Drawing.Size JpgSize;
                                    float Wpx;
                                    float Hpx;
                                    UploadAttachmentService.getJpgSize(url, out JpgSize, out Wpx, out Hpx);
                                    double i = 1;
                                    i = JpgSize.Width / 50.0;
                                    if (File.Exists(url))
                                    {
                                        builder.InsertImage(url, JpgSize.Width / i, JpgSize.Height / i);
                                    }
                                    else
                                    {
                                        bookmarkAuditMan5.Text = user.UserName;
                                    }

                                }
                            }
                            else
                            {
                                bookmarkAuditMan5.Text = user.UserName;
                            }
                        }

                    }
                }
                Bookmark bookmarkAuditDate3 = doc.Range.Bookmarks["AuditDate3"];
                if (bookmarkAuditDate3 != null)
                {
                    bookmarkAuditDate3.Text = auditDate3;
                }
                Bookmark bookmarkFile = doc.Range.Bookmarks["File"];
                if (bookmarkFile != null)
                {
                    DocumentBuilder builder = new DocumentBuilder(doc);
                    builder.MoveToBookmark("File");
                    var checkFile = AttachFileService.Getfiles(checkControl.CheckControlCode, Const.CheckListMenuId);
                    if (!string.IsNullOrEmpty(checkFile.AttachUrl))
                    {
                        string[] urls = checkFile.AttachUrl.Split(',');
                        if (urls.Length > 0)
                        {
                            foreach (var url in urls)
                            {
                                if (!string.IsNullOrWhiteSpace(url))
                                {
                                    System.Drawing.Size JpgSize;
                                    float Wpx;
                                    float Hpx;
                                    string spliurl = url;
                                    //if (index == urls.Length - 1) {
                                    //    spliurl = url.Substring(0, url.Length - 1);
                                    //}
                                    UploadAttachmentService.getJpgSize(rootPath + spliurl, out JpgSize, out Wpx, out Hpx);
                                    float i = 1;
                                    if (JpgSize.Width > 0 && JpgSize.Height > 0)
                                    {
                                        if (JpgSize.Width >= JpgSize.Height)
                                        {
                                            if (JpgSize.Width > 320)
                                            {
                                                i = (float)JpgSize.Width / 320;
                                            }
                                        }
                                        else
                                        {
                                            if (JpgSize.Height > 320)
                                            {
                                                i = (float)JpgSize.Height / 320;
                                            }

                                        }
                                        if (File.Exists(rootPath + spliurl))
                                        {
                                            builder.InsertImage(rootPath + spliurl, Convert.ToDouble(JpgSize.Width / i), Convert.ToDouble(JpgSize.Height / i));
                                        }
                                    }
                                }


                            }
                        }
                        //string url = item.AttachUrl.Substring(0, item.AttachUrl.Length - 1);   
                    }
                }
                Bookmark bookmarkReFile = doc.Range.Bookmarks["ReFile"];
                if (bookmarkReFile != null)
                {
                    DocumentBuilder builder = new DocumentBuilder(doc);
                    builder.MoveToBookmark("ReFile");
                    var checkFile = AttachFileService.Getfiles(checkControl.CheckControlCode + "r", Const.CheckListMenuId);
                    if (!string.IsNullOrEmpty(checkFile.AttachUrl))
                    {
                        string[] urls = checkFile.AttachUrl.Split(',');
                        if (urls.Length > 0)
                        {
                            foreach (var url in urls)
                            {
                                if (!string.IsNullOrWhiteSpace(url))
                                {
                                    System.Drawing.Size JpgSize;
                                    float Wpx;
                                    float Hpx;
                                    string spliurl = url;
                                    //if (index == urls.Length - 1) {
                                    //    spliurl = url.Substring(0, url.Length - 1);
                                    //}
                                    UploadAttachmentService.getJpgSize(rootPath + spliurl, out JpgSize, out Wpx, out Hpx);
                                    float i = 1;
                                    if (JpgSize.Width > 0 && JpgSize.Height > 0)
                                    {
                                        if (JpgSize.Width >= JpgSize.Height)
                                        {
                                            if (JpgSize.Width > 320)
                                            {
                                                i = (float)JpgSize.Width / 320;
                                            }
                                        }
                                        else
                                        {
                                            if (JpgSize.Height > 320)
                                            {
                                                i = (float)JpgSize.Height / 320;
                                            }

                                        }
                                        if (File.Exists(rootPath + spliurl))
                                        {
                                            builder.InsertImage(rootPath + spliurl, JpgSize.Width / i, JpgSize.Height / i);
                                        }
                                    }
                                }


                            }
                        }
                        //string url = item.AttachUrl.Substring(0, item.AttachUrl.Length - 1);
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
            if (e.CommandName.Equals("download"))
            {
                string menuId = Const.CheckListMenuId;
                PageContext.RegisterStartupScript(Windowtt.GetShowReference(
                 String.Format("../../AttachFile/webuploader.aspx?type=-1&source=1&toKeyId={0}&path=FileUpload/CheckControl&menuId={1}", fileId, menuId)));
            }
        }
    }
}