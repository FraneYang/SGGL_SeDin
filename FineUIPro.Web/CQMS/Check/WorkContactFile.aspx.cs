using Aspose.Words;
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

namespace FineUIPro.Web.CQMS.Check
{
    public partial class WorkContactFile : PageBase
    {
        /// <summary>
        /// 工程联系单主键
        /// </summary>
        public string WorkContactId
        {
            get
            {
                return (string)ViewState["WorkContactId"];
            }
            set
            {
                ViewState["WorkContactId"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                UnitService.GetUnit(drpProposeUnit, CurrUser.LoginProjectId, true);
                Funs.FineUIPleaseSelect(this.drpIsReply);
                ddlPageSize.SelectedValue = Grid1.PageSize.ToString();
                BindGrid();
            }
        }
        protected DataTable ChecklistData()
        {
            string strSql = @"SELECT chec.WorkContactId,chec.ProjectId,chec.ProposedUnitId,chec.MainSendUnitIds,chec.CCUnitIds,"
                          + @" chec.CompileMan,chec.CompileDate,chec.code,chec.state,chec.IsReply,chec.cause,"
                          + @" unit.UnitName,u.userName "
                          + @" FROM Unqualified_WorkContact chec "
                          + @" left join Base_Unit unit on unit.unitId=chec.ProposedUnitId "
                          + @" left join sys_User u on u.userId = chec.CompileMan"
                          + @" where chec.ProjectId=@ProjectId";

            List<SqlParameter> listStr = new List<SqlParameter>();
            listStr.Add(new SqlParameter("@ProjectId", CurrUser.LoginProjectId));
            strSql += " AND (chec.CompileDate>=@startTime or @startTime='') and (chec.CompileDate<=@endTime or @endTime='') ";
            listStr.Add(new SqlParameter("@startTime", !string.IsNullOrEmpty(txtStartTime.Text.Trim()) ? txtStartTime.Text.Trim() + " 00:00:00" : ""));
            listStr.Add(new SqlParameter("@endTime", !string.IsNullOrEmpty(txtEndTime.Text.Trim()) ? txtEndTime.Text.Trim() + " 23:59:59" : ""));
            if (drpProposeUnit.SelectedValue != Const._Null)
            {
                strSql += " AND chec.ProposedUnitId=@unitId";
                listStr.Add(new SqlParameter("@unitId", drpProposeUnit.SelectedValue));
            }
            if (drpIsReply.SelectedValue != Const._Null)
            {
                strSql += " AND chec.IsReply=@IsReply";
                listStr.Add(new SqlParameter("@IsReply", drpIsReply.SelectedValue));
            }
            strSql += " AND chec.State=@State";
            listStr.Add(new SqlParameter("@State", Const.WorkContact_Complete));
            strSql += " order by chec.code desc ";
            //if (drpUnitWork.SelectedValue != Const._Null)
            //{
            //    strSql += " AND chec.unitworkId=@unitworkId";
            //    listStr.Add(new SqlParameter("@unitworkId", drpUnitWork.SelectedValue));
            //}
            //if (drpCNProfessional.SelectedValue != Const._Null)
            //{
            //    strSql += " AND chec.CNProfessionalCode=@CNProfessionalCode";
            //    listStr.Add(new SqlParameter("@CNProfessionalCode", drpCNProfessional.SelectedValue));
            //}
            //if (drpQuestionType.SelectedValue != Const._Null)
            //{
            //    strSql += " AND chec.QuestionType=@QuestionType";
            //    listStr.Add(new SqlParameter("@QuestionType", drpQuestionType.SelectedValue));
            //}
            //if (dpHandelStatus.SelectedValue != Const._Null)
            //{
            //    if (dpHandelStatus.SelectedValue.Equals("1"))
            //    {
            //        strSql += " AND (chec.state='5' or chec.state='6')";
            //    }
            //    else if (dpHandelStatus.SelectedValue.Equals("2"))
            //    {
            //        strSql += " AND chec.state='7'";
            //    }
            //    else if (dpHandelStatus.SelectedValue.Equals("3"))
            //    {
            //        strSql += " AND DATEADD(day,1,chec.LimitDate)< GETDATE() and chec.state<>5 and chec.state<>6 and chec.state<>7";
            //    }
            //    else if (dpHandelStatus.SelectedValue.Equals("4"))
            //    {
            //        strSql += " AND DATEADD(day,1,chec.LimitDate)> GETDATE() and chec.state<>5 and chec.state<>6 and chec.state<>7";
            //    }
            //}
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            return tb;
        }
        private void BindGrid()
        {
            var list = ChecklistData();
            Grid1.RecordCount = list.Rows.Count;
            var unit = UnitService.GetUnitByProjectIdList(CurrUser.LoginProjectId);
            if (list.Rows.Count > 0)
            {
                for (int i = 0; i < list.Rows.Count; i++)
                {
                    if (list.Rows[i]["MainSendUnitIds"] != null)
                    {
                        var unitIds = list.Rows[i]["MainSendUnitIds"].ToString().Split(',');
                        var listf = unit.Where(p => unitIds.Contains(p.UnitId)).Select(p => p.UnitName).ToArray();
                        list.Rows[i]["MainSendUnitIds"] = string.Join(",", listf);
                    }
                    if (list.Rows[i]["CCUnitIds"] != null)
                    {
                        var unitIds = list.Rows[i]["CCUnitIds"].ToString().Split(',');
                        var listf = unit.Where(p => unitIds.Contains(p.UnitId)).Select(p => p.UnitName).ToArray();
                        list.Rows[i]["CCUnitIds"] = string.Join(",", listf);
                    }
                    if (list.Rows[i]["IsReply"] != null)
                    {
                        list.Rows[i]["IsReply"] = list.Rows[i]["IsReply"].ToString() == "1" ? "需要回复" : "不需回复";
                    }
                }
            }
            list = GetFilteredTable(Grid1.FilteredData, list);
            var table = GetPagedDataTable(Grid1, list);
            Grid1.DataSource = table;
            Grid1.DataBind();
        }

        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void btnRset_Click(object sender, EventArgs e)
        {
            drpProposeUnit.SelectedIndex = 0;
            txtStartTime.Text = "";
            txtEndTime.Text = "";
            drpIsReply.SelectedIndex = 0;
            BindGrid();
        }



        protected void window_tt_Close(object sender, WindowCloseEventArgs e)
        {
            BindGrid();
        }



        protected void btnMenuView_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            string workContactId = Grid1.SelectedRowID.Split(',')[0];
            Model.Unqualified_WorkContact workContact = WorkContactService.GetWorkContactByWorkContactId(workContactId);
            if (workContact.IsFinal == true)
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("../Unqualified/WorkContactFinalFileView.aspx?WorkContactId={0}", workContactId), "已定稿文件"));
                return;
            }
            PageContext.RegisterStartupScript(window_tt.GetShowReference(String.Format("../Unqualified/WorkContactview.aspx?WorkContactId={0}", workContactId, "查看 - ")));
        }

        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
            BindGrid();
        }

        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
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
                Model.Unqualified_WorkContact workContact = WorkContactService.GetWorkContactByWorkContactId(fileId);
                if (workContact.IsReply == "1")   //需要回复
                {
                    initTemplatePath = Const.WorkContactTemplateUrl;
                    uploadfilepath = rootPath + initTemplatePath;
                    newUrl = uploadfilepath.Replace(".doc", workContact.Code.Replace("/", "-") + ".doc");
                    filePath = initTemplatePath.Replace(".doc", workContact.Code.Replace("/", "-") + ".pdf");
                }
                else   //不需回复
                {
                    initTemplatePath = Const.WorkContactTemplateUrl2;
                    uploadfilepath = rootPath + initTemplatePath;
                    newUrl = uploadfilepath.Replace("2.doc", workContact.Code.Replace("/", "-") + ".doc");
                    filePath = initTemplatePath.Replace("2.doc", workContact.Code.Replace("/", "-") + ".pdf");
                }
                File.Copy(uploadfilepath, newUrl);
                //更新书签内容
                Document doc = new Aspose.Words.Document(newUrl);
                Bookmark bookmarkProjectName = doc.Range.Bookmarks["ProjectName"];
                if (bookmarkProjectName != null)
                {
                    var project = ProjectService.GetProjectByProjectId(workContact.ProjectId);
                    if (project != null)
                    {
                        bookmarkProjectName.Text = project.ProjectName;
                    }
                }
                Bookmark bookmarkCode = doc.Range.Bookmarks["Code"];
                if (bookmarkCode != null)
                {
                    bookmarkCode.Text = workContact.Code;
                }
                Bookmark bookmarkProposedUnit = doc.Range.Bookmarks["ProposedUnit"];
                if (bookmarkProposedUnit != null)
                {
                    var unit = UnitService.GetUnitByUnitId(workContact.ProposedUnitId);
                    var projectUnit = ProjectUnitService.GetProjectUnitByUnitIdProjectId(this.CurrUser.LoginProjectId, workContact.ProposedUnitId);
                    if (unit != null)
                    {
                        bookmarkProposedUnit.Text = unit.UnitName;
                        unitType = projectUnit.UnitType;
                    }
                }
                Bookmark bookmarkIsReply = doc.Range.Bookmarks["IsReply"];
                if (bookmarkIsReply != null)
                {
                    if (workContact.IsReply == "1")
                    {
                        bookmarkIsReply.Text = "■需要回复   □不需回复";
                    }
                    else
                    {
                        bookmarkIsReply.Text = "□需要回复   ■不需回复";
                    }
                }
                Bookmark bookmarkMainSendUnit = doc.Range.Bookmarks["MainSendUnit"];
                if (bookmarkMainSendUnit != null)
                {
                    bookmarkMainSendUnit.Text = UnitService.getUnitNamesUnitIds(workContact.MainSendUnitIds);
                }
                Bookmark bookmarkCCUnit = doc.Range.Bookmarks["CCUnit"];
                if (bookmarkCCUnit != null)
                {
                    bookmarkCCUnit.Text = UnitService.getUnitNamesUnitIds(workContact.CCUnitIds);
                }
                Bookmark bookmarkCause = doc.Range.Bookmarks["Cause"];
                if (bookmarkCause != null)
                {
                    bookmarkCause.Text = workContact.Cause;
                }
                Model.Unqualified_WorkContactApprove approve1 = new Model.Unqualified_WorkContactApprove();
                Model.Unqualified_WorkContactApprove approve2 = new Model.Unqualified_WorkContactApprove();
                Model.Unqualified_WorkContactApprove approve3 = new Model.Unqualified_WorkContactApprove();
                if (unitType == BLL.Const.ProjectUnitType_2)   //分包商提出
                {
                    approve1 = WorkContactApproveService.GetAudit1(fileId);
                    if (approve1 != null)
                    {
                        Model.Sys_User user = UserService.GetUserByUserId(approve1.ApproveMan);
                        if (user != null)
                        {
                            auditMan1 = user.UserName;
                        }
                        if (approve1.ApproveDate != null)
                        {
                            auditDate1 = string.Format("{0:yyyy-MM-dd}", approve1.ApproveDate);
                        }
                        approveIdea1 = approve1.ApproveIdea;
                    }
                    approve2 = WorkContactApproveService.GetAudit2(fileId);
                    if (approve2 != null)
                    {
                        Model.Sys_User user = UserService.GetUserByUserId(approve2.ApproveMan);
                        if (user != null)
                        {
                            auditMan2 = user.UserName;
                        }
                        if (approve2.ApproveDate != null)
                        {
                            auditDate2 = string.Format("{0:yyyy-MM-dd}", approve2.ApproveDate);
                        }
                        approveIdea2 = approve2.ApproveIdea;
                    }
                    approve3 = WorkContactApproveService.GetAudit3(fileId);
                    if (approve3 != null)
                    {
                        Model.Sys_User user = UserService.GetUserByUserId(approve3.ApproveMan);
                        if (user != null)
                        {
                            auditMan3 = user.UserName;
                        }
                        if (approve3.ApproveDate != null)
                        {
                            auditDate3 = string.Format("{0:yyyy-MM-dd}", approve3.ApproveDate);
                        }
                        approveIdea3 = approve3.ApproveIdea;
                    }
                }
                else  //总包提出
                {
                    approve1 = WorkContactApproveService.GetAudit3(fileId);
                    if (approve1 != null)
                    {
                        Model.Sys_User user = UserService.GetUserByUserId(approve1.ApproveMan);
                        var file = user.SignatureUrl;
                        if (!string.IsNullOrWhiteSpace(file))
                        {
                            string url = rootPath + file;
                            DocumentBuilder builders = new DocumentBuilder(doc);
                            builders.MoveToBookmark("AuditMan1");
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
                                    builders.InsertImage(url, JpgSize.Width / i, JpgSize.Height / i);
                                }
                                else
                                {
                                    auditMan1 = user.UserName;
                                }

                            }
                        }
                        else
                        {
                            auditMan1 = user.UserName;
                        }
                        if (approve1.ApproveDate != null)
                        {
                            auditDate1 = string.Format("{0:yyyy-MM-dd}", approve1.ApproveDate);
                        }
                        approveIdea1 = approve1.ApproveIdea;
                    }
                    approve2 = WorkContactApproveService.GetAudit4(fileId);
                    if (approve2 != null)
                    {
                        Model.Sys_User user = UserService.GetUserByUserId(approve2.ApproveMan);
                        var file = user.SignatureUrl;
                        if (!string.IsNullOrWhiteSpace(file))
                        {
                            string url = rootPath + file;
                            DocumentBuilder builders = new DocumentBuilder(doc);
                            builders.MoveToBookmark("AuditMan2");
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
                                    builders.InsertImage(url, JpgSize.Width / i, JpgSize.Height / i);
                                }
                                else
                                {
                                    auditMan2 = user.UserName;
                                }

                            }
                        }
                        else
                        {
                            auditMan2 = user.UserName;
                        }


                        if (approve2.ApproveDate != null)
                        {
                            auditDate2 = string.Format("{0:yyyy-MM-dd}", approve2.ApproveDate);
                        }
                        approveIdea2 = approve2.ApproveIdea;
                    }
                    approve3 = WorkContactApproveService.GetAudit1(fileId);
                    if (approve3 != null)
                    {
                        Model.Sys_User user = UserService.GetUserByUserId(approve3.ApproveMan);
                        var file = user.SignatureUrl;
                        if (!string.IsNullOrWhiteSpace(file))
                        {
                            string url = rootPath + file;
                            DocumentBuilder builders = new DocumentBuilder(doc);
                            builders.MoveToBookmark("AuditMan3");
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
                                    builders.InsertImage(url, JpgSize.Width / i, JpgSize.Height / i);
                                }
                                else
                                {
                                    auditMan3 = user.UserName;
                                }

                            }
                        }
                        else
                        {
                            auditMan3 = user.UserName;
                        }
                        if (approve3.ApproveDate != null)
                        {
                            auditDate3 = string.Format("{0:yyyy-MM-dd}", approve3.ApproveDate);
                        }
                        approveIdea3 = approve3.ApproveIdea;
                    }
                }
                Bookmark bookmarkContents = doc.Range.Bookmarks["Contents"];
                if (bookmarkContents != null)
                {
                    bookmarkContents.Text = workContact.Contents + "\r\n" + approveIdea1;
                }
                Bookmark bookmarkAttachUrl = doc.Range.Bookmarks["AttachUrl"];
                if (bookmarkAttachUrl != null)
                {
                    if (AttachFileService.Getfile(workContact.WorkContactId, Const.WorkContactMenuId))
                    {
                        bookmarkAttachUrl.Text = "见附页";
                    }
                    else
                    {
                        bookmarkAttachUrl.Text = "无";
                    }
                }
                Bookmark bookmarkCompileMan = doc.Range.Bookmarks["CompileMan"];
                if (bookmarkCompileMan != null)
                {
                    Model.Unqualified_WorkContactApprove approve = WorkContactApproveService.GetComplie(fileId);
                    if (approve != null)
                    {
                        Model.Sys_User user = UserService.GetUserByUserId(approve.ApproveMan);
                        var file = user.SignatureUrl;
                        if (!string.IsNullOrWhiteSpace(file))
                        {
                            string url = rootPath + file;
                            DocumentBuilder builders = new DocumentBuilder(doc);
                            builders.MoveToBookmark("CompileMan");
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
                                    builders.InsertImage(url, JpgSize.Width / i, JpgSize.Height / i);
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
                Bookmark bookmarkAuditMan1 = doc.Range.Bookmarks["AuditMan1"];
                if (bookmarkAuditMan1 != null)
                {
                    bookmarkAuditMan1.Text = auditMan1;
                }
                Bookmark bookmarkAuditDate1 = doc.Range.Bookmarks["AuditDate1"];
                if (bookmarkAuditDate1 != null)
                {
                    bookmarkAuditDate1.Text = auditDate1;
                }
                Bookmark bookmarkApproveIdea = doc.Range.Bookmarks["ApproveIdea"];
                if (bookmarkApproveIdea != null)
                {
                    //if (string.IsNullOrEmpty(approveIdea2) && string.IsNullOrEmpty(approveIdea3))
                    //{
                    //    approveIdea = "同意";
                    //}
                    //else if (string.IsNullOrEmpty(approveIdea2))
                    //{
                    //    approveIdea = approveIdea3;
                    //}
                    //else if (string.IsNullOrEmpty(approveIdea3))
                    //{
                    //    approveIdea = approveIdea2;
                    //}
                    //else
                    //{
                    //    approveIdea = approveIdea2 + "\r\n" + approveIdea3;
                    //}
                    if (!string.IsNullOrWhiteSpace(workContact.ReOpinion))
                    {
                        bookmarkApproveIdea.Text = workContact.ReOpinion;
                    }

                }
                Bookmark bookmarkReAttachUrl = doc.Range.Bookmarks["ReAttachUrl"];
                if (bookmarkReAttachUrl != null)
                {
                    if (AttachFileService.Getfile(workContact.WorkContactId + "r", Const.WorkContactMenuId))
                    {
                        bookmarkReAttachUrl.Text = "见附页";
                    }
                    else
                    {
                        bookmarkReAttachUrl.Text = "无";
                    }

                    //if (!string.IsNullOrEmpty(workContact.AttachUrl))
                    //{
                    //    bookmarkAttachUrl.Text = "见附页";
                    //}
                    //else
                    //{
                    //    bookmarkAttachUrl.Text = "无";
                    //}
                }
                Bookmark bookmarkAuditMan2 = doc.Range.Bookmarks["AuditMan2"];
                if (bookmarkAuditMan2 != null)
                {
                    bookmarkAuditMan2.Text = auditMan2;
                }
                Bookmark bookmarkAuditMan3 = doc.Range.Bookmarks["AuditMan3"];
                if (bookmarkAuditMan3 != null)
                {
                    bookmarkAuditMan3.Text = auditMan3;
                }
                Bookmark bookmarkAuditDate3 = doc.Range.Bookmarks["AuditDate3"];
                if (bookmarkAuditDate3 != null)
                {
                    bookmarkAuditDate3.Text = auditDate3;
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
                string menuId = Const.WorkContactMenuId;
                PageContext.RegisterStartupScript(Windowtt.GetShowReference(
                 String.Format("../../AttachFile/webuploader.aspx?type=-1&source=1&toKeyId={0}&source=1&path=FileUpload/WorkContact&menuId={1}", fileId, menuId)));
            }
        }
    }
}