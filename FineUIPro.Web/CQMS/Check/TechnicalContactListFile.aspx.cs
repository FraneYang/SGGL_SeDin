using Aspose.Words;
using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;

namespace FineUIPro.Web.CQMS.Check
{
    public partial class TechnicalContactListFile : PageBase
    {
        /// <summary>
        /// 工程联络单主键
        /// </summary>
        public string TechnicalContactListId
        {
            get
            {
                return (string)ViewState["TechnicalContactListId"];
            }
            set
            {
                ViewState["TechnicalContactListId"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                UnitService.GetUnit(drpProposeUnit, CurrUser.LoginProjectId, true);
                Funs.FineUIPleaseSelect(this.drpContactListType);
                Funs.FineUIPleaseSelect(this.drpIsReply);
                UnitWorkService.InitUnitWorkDownList(drpUnitWork, this.CurrUser.LoginProjectId, true);
                CNProfessionalService.InitCNProfessionalDownList(drpCNProfessional, true);
                ddlPageSize.SelectedValue = Grid1.PageSize.ToString();

                BindData();

            }
        }


        protected DataTable ChecklistData()
        {
            string strSql = @"SELECT chec.TechnicalContactListId,chec.ProjectId,chec.ProposedUnitId,chec.UnitWorkId,"
                          + @" chec.CompileMan,chec.CompileDate,chec.code,chec.state,chec.CNProfessionalCode,chec.IsReply,chec.ContactListType,"
                          + @" unit.UnitName,u.userName "
                          + @" FROM Check_TechnicalContactList chec "
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
            if (drpContactListType.SelectedValue != Const._Null)
            {
                strSql += " AND chec.ContactListType=@ContactListType";
                listStr.Add(new SqlParameter("@ContactListType", drpContactListType.SelectedValue));
            }
            if (drpUnitWork.SelectedValue != Const._Null)
            {
                strSql += " AND   CHARINDEX(@unitworkId,chec.unitworkId) > 0 ";
                listStr.Add(new SqlParameter("@unitworkId", drpUnitWork.SelectedValue));
            }
            if (drpCNProfessional.SelectedValue != Const._Null)
            {
                strSql += " AND   CHARINDEX(@CNProfessionalCode,chec.CNProfessionalCode) > 0 ";
                //strSql += " AND chec.CNProfessionalCode=@CNProfessionalCode";
                listStr.Add(new SqlParameter("@CNProfessionalCode", drpCNProfessional.SelectedValue));
            }

            if (drpIsReply.SelectedValue != Const._Null)
            {
                strSql += " AND chec.IsReply=@IsReply";
                listStr.Add(new SqlParameter("@IsReply", drpIsReply.SelectedValue));
            }
            strSql += " AND chec.State=@State";
            listStr.Add(new SqlParameter("@State", Const.TechnicalContactList_Complete));
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


        private void BindData()
        {

            var list = ChecklistData();
            Grid1.RecordCount = list.Rows.Count;
            var CNProfessional = CNProfessionalService.GetCNProfessionalItem();
            var uniWork = UnitWorkService.GetUnitWorkLists(CurrUser.LoginProjectId);
            if (list.Rows.Count > 0)
            {
                for (int i = 0; i < list.Rows.Count; i++)
                {
                    if (list.Rows[i]["CNProfessionalCode"] != null)
                    {
                        var code = list.Rows[i]["CNProfessionalCode"].ToString().Split(',');
                        var listf = CNProfessional.Where(p => code.Contains(p.Value)).Select(p => p.Text).ToArray();
                        list.Rows[i]["CNProfessionalCode"] = string.Join(",", listf);
                    }
                    if (list.Rows[i]["UnitWorkId"] != null)
                    {
                        var code = list.Rows[i]["UnitWorkId"].ToString().Split(',');
                        var workid = uniWork.Where(p => code.Contains(p.UnitWorkId)).Select(p => p.UnitWorkName).ToArray();
                        list.Rows[i]["UnitWorkId"] = string.Join(",", workid);
                    }
                    if (list.Rows[i]["ContactListType"] != null)
                    {
                        list.Rows[i]["ContactListType"] = list.Rows[i]["ContactListType"].ToString() == "1" ? "图纸类" : "非图纸类";
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
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            //Grid1.PageIndex = e.NewPageIndex;
            BindData();
        }

        /// <summary>
        /// 分页显示条数下拉框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid1.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            BindData();
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            BindData();
        }

        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
            BindData();
        }



        protected void btnMenuView_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            TechnicalContactListId = Grid1.SelectedRowID.Split(',')[0];
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("TechnicalContactView.aspx?TechnicalContactListId={0}", TechnicalContactListId, "查看 - ")));
        }



        protected void btnRset_Click(object sender, EventArgs e)
        {
            txtStartTime.Text = "";
            txtEndTime.Text = "";
            drpProposeUnit.SelectedIndex = 0;
            drpContactListType.SelectedIndex = 0;
            drpIsReply.SelectedIndex = 0;
            drpCNProfessional.SelectedIndex = 0;
            drpUnitWork.SelectedIndex = 0;
            BindData();
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
                Model.Check_TechnicalContactList technicalContactList = TechnicalContactListService.GetTechnicalContactListByTechnicalContactListId(fileId);
                var app = TechnicalContactListApproveService.GetComplie(fileId);
                auditDate = string.Format("{0:yyyy-MM-dd}", app.ApproveDate);
                if (technicalContactList.IsReply == "1")   //需要回复
                {
                    initTemplatePath = Const.TechnicalContactListTemplateUrl;
                    uploadfilepath = rootPath + initTemplatePath;
                    newUrl = uploadfilepath.Replace(".doc", technicalContactList.Code.Replace("/", "-") + ".doc");
                    filePath = initTemplatePath.Replace(".doc", technicalContactList.Code.Replace("/", "-") + ".pdf");
                }
                else   //不需回复
                {
                    initTemplatePath = Const.TechnicalContactListTemplateUrl2;
                    uploadfilepath = rootPath + initTemplatePath;
                    newUrl = uploadfilepath.Replace("2.doc", technicalContactList.Code.Replace("/", "-") + ".doc");
                    filePath = initTemplatePath.Replace("2.doc", technicalContactList.Code.Replace("/", "-") + ".pdf");
                }
                File.Copy(uploadfilepath, newUrl);
                //更新书签内容
                Document doc = new Aspose.Words.Document(newUrl);
                Bookmark bookmarkProjectName = doc.Range.Bookmarks["ProjectName"];
                if (bookmarkProjectName != null)
                {
                    var project = ProjectService.GetProjectByProjectId(technicalContactList.ProjectId);
                    if (project != null)
                    {
                        bookmarkProjectName.Text = project.ProjectName;
                    }
                }
                Bookmark bookmarkCode = doc.Range.Bookmarks["Code"];
                if (bookmarkCode != null)
                {
                    bookmarkCode.Text = technicalContactList.Code;
                }
                Bookmark bookmarkProposedUnit = doc.Range.Bookmarks["ProposedUnit"];
                if (bookmarkProposedUnit != null)
                {
                    var unit = UnitService.GetUnitByUnitId(technicalContactList.ProposedUnitId);
                    var projectUnit = ProjectUnitService.GetProjectUnitByUnitIdProjectId(this.CurrUser.LoginProjectId, technicalContactList.ProposedUnitId);
                    if (unit != null)
                    {
                        bookmarkProposedUnit.Text = unit.UnitName;
                        unitType = projectUnit.UnitType;
                    }
                }
                Bookmark bookmarkUnitWork = doc.Range.Bookmarks["UnitWork"];
                if (bookmarkUnitWork != null)
                {
                    bookmarkUnitWork.Text = CQMSConstructSolutionService.GetUnitWorkName(technicalContactList.UnitWorkId);
                }
                Bookmark bookmarkCNProfessional = doc.Range.Bookmarks["CNProfessional"];
                if (bookmarkCNProfessional != null)
                {
                    bookmarkCNProfessional.Text = CQMSConstructSolutionService.GetProfessionalName(technicalContactList.CNProfessionalCode);
                }
                Bookmark bookmarkMainSendUnit = doc.Range.Bookmarks["MainSendUnit"];
                if (bookmarkMainSendUnit != null)
                {
                    bookmarkMainSendUnit.Text = UnitService.getUnitNamesUnitIds(technicalContactList.MainSendUnitId);
                }
                Bookmark bookmarkCCUnit = doc.Range.Bookmarks["CCUnit"];
                if (bookmarkCCUnit != null)
                {
                    bookmarkCCUnit.Text = UnitService.getUnitNamesUnitIds(technicalContactList.CCUnitIds);
                }
                Bookmark bookmarkContactListType = doc.Range.Bookmarks["ContactListType"];
                if (bookmarkContactListType != null)
                {
                    if (technicalContactList.ContactListType == "1")
                    {
                        bookmarkContactListType.Text = "■图纸类   □非图纸类";
                    }
                    else
                    {
                        bookmarkContactListType.Text = "□图纸类   ■非图纸类";
                    }
                }
                Bookmark bookmarkIsReply = doc.Range.Bookmarks["IsReply"];
                if (bookmarkIsReply != null)
                {
                    if (technicalContactList.IsReply == "1")
                    {
                        bookmarkIsReply.Text = "■需要回复   □不需回复";
                    }
                    else
                    {
                        bookmarkIsReply.Text = "□需要回复   ■不需回复";
                    }
                }
                //☑
                Bookmark bookmarkCause = doc.Range.Bookmarks["Cause"];
                if (bookmarkCause != null)
                {
                    bookmarkCause.Text = technicalContactList.Cause;
                }
                Bookmark bookmarkContents = doc.Range.Bookmarks["Contents"];
                if (bookmarkContents != null)
                {
                    bookmarkContents.Text = technicalContactList.Contents;
                    if (unitType == BLL.Const.ProjectUnitType_2)
                    {
                        Model.Check_TechnicalContactListApprove approve = TechnicalContactListApproveService.GetApprove(fileId);
                        if (approve != null)
                        {
                            if (!string.IsNullOrEmpty(approve.ApproveIdea))
                            {
                                bookmarkContents.Text += "\r\n" + approve.ApproveIdea;
                            }
                        }
                    }
                    else
                    {
                        Model.Check_TechnicalContactListApprove approve = TechnicalContactListApproveService.GetApprove2(fileId);
                        if (approve != null)
                        {
                            if (!string.IsNullOrEmpty(approve.ApproveIdea))
                            {
                                bookmarkContents.Text += "\r\n" + approve.ApproveIdea;
                            }
                        }
                    }
                }
                Bookmark bookmarkAttachUrl = doc.Range.Bookmarks["AttachUrl"];
                if (bookmarkAttachUrl != null)
                {
                    if (AttachFileService.Getfile(technicalContactList.TechnicalContactListId, Const.TechnicalContactListMenuId))
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
                    Model.Sys_User user = UserService.GetUserByUserId(technicalContactList.CompileMan);
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
                        bookmarkCompileMan.Text = UserService.GetUserNameByUserId(technicalContactList.CompileMan);
                    }

                }
                Bookmark bookmarkAuditMan1 = doc.Range.Bookmarks["AuditMan1"];
                if (bookmarkAuditMan1 != null)
                {
                    Model.Check_TechnicalContactListApprove approve = null;
                    if (unitType == BLL.Const.ProjectUnitType_2)   //施工分包发起
                    {
                        approve = TechnicalContactListApproveService.GetApprove(technicalContactList.TechnicalContactListId);
                    }
                    else
                    {
                        approve = TechnicalContactListApproveService.GetApprove2(technicalContactList.TechnicalContactListId);
                    }
                    if (approve != null)
                    {
                        Model.Sys_User user = UserService.GetUserByUserId(approve.ApproveMan);
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
                                    bookmarkAuditMan1.Text = user.UserName;
                                }

                            }
                        }
                        else
                        {
                            bookmarkAuditMan1.Text = UserService.GetUserNameByUserId(approve.ApproveMan);
                        }
                    }
                }
                Bookmark bookmarkCompileDate = doc.Range.Bookmarks["CompileDate"];
                if (bookmarkCompileDate != null)
                {
                    if (technicalContactList.CompileDate != null)
                    {
                        bookmarkCompileDate.Text = string.Format("{0:yyyy-MM-dd}", technicalContactList.CompileDate);
                    }
                }
                Bookmark bookmarkAuditMan2 = doc.Range.Bookmarks["AuditMan2"];
                if (bookmarkAuditMan2 != null)
                {
                    Model.Check_TechnicalContactListApprove approve = null;
                    if (unitType == BLL.Const.ProjectUnitType_2)   //施工分包发起
                    {
                        approve = TechnicalContactListApproveService.GetApprove3(technicalContactList.TechnicalContactListId);
                    }
                    else
                    {
                        approve = TechnicalContactListApproveService.GetApprove4(technicalContactList.TechnicalContactListId);
                    }
                    if (approve != null)
                    {
                        Model.Sys_User user = UserService.GetUserByUserId(approve.ApproveMan);
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
                Bookmark bookmarkReAttachUrl = doc.Range.Bookmarks["ReAttachUrl"];
                if (bookmarkReAttachUrl != null)
                {
                    if (AttachFileService.Getfile(technicalContactList.TechnicalContactListId + "r", Const.TechnicalContactListMenuId))
                    {
                        bookmarkReAttachUrl.Text = "见附页";
                    }
                    else
                    {
                        bookmarkReAttachUrl.Text = "无";
                    }
                }
                Bookmark bookmarkAuditMan3 = doc.Range.Bookmarks["AuditMan3"];
                if (bookmarkAuditMan3 != null)
                {
                    Model.Check_TechnicalContactListApprove approve = null;
                    if (unitType == BLL.Const.ProjectUnitType_2)   //施工分包发起
                    {
                        approve = TechnicalContactListApproveService.GetApprove2(technicalContactList.TechnicalContactListId);
                    }
                    else
                    {
                        approve = TechnicalContactListApproveService.GetApprove(technicalContactList.TechnicalContactListId);
                    }
                    if (approve != null)
                    {
                        Model.Sys_User user = UserService.GetUserByUserId(approve.ApproveMan);
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
                Bookmark bookmarkApproveIdea = doc.Range.Bookmarks["ApproveIdea"];
                if (bookmarkApproveIdea != null)
                {
                    if (!string.IsNullOrWhiteSpace(technicalContactList.ReOpinion))
                    {
                        bookmarkApproveIdea.Text = technicalContactList.ReOpinion;
                    }

                }
                Bookmark bookmarkAuditDate = doc.Range.Bookmarks["AuditDate"];
                if (bookmarkAuditDate != null)
                {
                    bookmarkAuditDate.Text = auditDate;
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
                string menuId = Const.TechnicalContactListMenuId;
                PageContext.RegisterStartupScript(Windowtt.GetShowReference(
                 String.Format("../../AttachFile/webuploader.aspx?type=-1&source=1&toKeyId={0}&path=FileUpload/TechnicalContactList&menuId={1}", fileId, menuId)));
            }
        }
    }
}