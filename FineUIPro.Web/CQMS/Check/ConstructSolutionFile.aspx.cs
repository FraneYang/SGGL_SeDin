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
    public partial class ConstructSolutionFile : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BLL.SolutionTempleteTypeService.InitSolutionTempleteDropDownList(drpSolutionType, true);
                UnitService.InitUnitByProjectIdUnitTypeDropDownList(drpProposeUnit, CurrUser.LoginProjectId, BLL.Const.ProjectUnitType_2, true);
                BindGrid();
            }

        }
        protected DataTable ChecklistData()
        {
            string strSql = @"SELECT chec.ConstructSolutionId,chec.ProjectId,chec.UnitId,"
                          + @" chec.CompileMan,chec.CompileDate,chec.code,chec.state,chec.SolutionType,chec.SolutionName,"
                          + @" unit.UnitName,u.userName as CompileManName,s.SolutionTempleteTypeName"
                          + @" FROM Solution_CQMSConstructSolution chec "
                          + @" left join Base_Unit unit on unit.unitId=chec.UnitId "
                          + @" left join sys_User u on u.userId = chec.CompileMan"
                          + @" left join[dbo].[Base_SolutionTempleteType] s on chec.SolutionType=s.SolutionTempleteTypeCode"
                          + @" where chec.ProjectId=@ProjectId";

            List<SqlParameter> listStr = new List<SqlParameter>();
            listStr.Add(new SqlParameter("@ProjectId", CurrUser.LoginProjectId));
            if (drpProposeUnit.SelectedValue != Const._Null)
            {
                strSql += " AND chec.UnitId=@unitId";
                listStr.Add(new SqlParameter("@unitId", drpProposeUnit.SelectedValue));
            }
            if (drpSolutionType.SelectedValue != Const._Null)
            {
                strSql += " AND chec.SolutionType=@SolutionType";
                listStr.Add(new SqlParameter("@SolutionType", drpSolutionType.SelectedValue));
            }
            strSql += " AND chec.State=@State";
            listStr.Add(new SqlParameter("@State", Const.CQMSConstructSolution_Complete));
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
            //var unit = UnitService.GetUnitByProjectIdList(CurrUser.LoginProjectId);
            //if (list.Rows.Count > 0)
            //{
            //    for (int i = 0; i < list.Rows.Count; i++)
            //    {
            //        //if (list.Rows[i]["MainSendUnitIds"] != null)
            //        //{
            //        //    var unitIds = list.Rows[i]["MainSendUnitIds"].ToString().Split(',');
            //        //    var listf = unit.Where(p => unitIds.Contains(p.UnitId)).Select(p => p.UnitName).ToArray();
            //        //    list.Rows[i]["MainSendUnitIds"] = string.Join(",", listf);
            //        //}
            //        //if (list.Rows[i]["CCUnitIds"] != null)
            //        //{
            //        //    var unitIds = list.Rows[i]["CCUnitIds"].ToString().Split(',');
            //        //    var listf = unit.Where(p => unitIds.Contains(p.UnitId)).Select(p => p.UnitName).ToArray();
            //        //    list.Rows[i]["CCUnitIds"] = string.Join(",", listf);
            //        //}
            //        //if (list.Rows[i]["IsReply"] != null)
            //        //{
            //        //    list.Rows[i]["IsReply"] = list.Rows[i]["IsReply"].ToString() == "1" ? "需要回复" : "不需回复";
            //        //}
            //    }
            //}
            list = GetFilteredTable(Grid1.FilteredData, list);
            var table = GetPagedDataTable(Grid1, list);
            Grid1.DataSource = table;
            Grid1.DataBind();
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void btnRset_Click(object sender, EventArgs e)
        {
            drpSolutionType.SelectedIndex = 0;
            drpProposeUnit.SelectedIndex = 0;
            BindGrid();
        }

        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
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
            string id = Grid1.SelectedRowID.Split(',')[0];
            PageContext.RegisterStartupScript(window_tt.GetShowReference(String.Format("../Solution/ConstructSolutionView.aspx?constructSolutionId={0}", id)));

        }



        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
            BindGrid();
        }

        protected void window_tt_Close(object sender, WindowCloseEventArgs e)
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
                Model.Solution_CQMSConstructSolution constructSolution = CQMSConstructSolutionService.GetConstructSolutionByConstructSolutionId(fileId);
                initTemplatePath = Const.ConstructSolutionTemplateUrl;
                uploadfilepath = rootPath + initTemplatePath;
                newUrl = uploadfilepath.Replace(".doc", constructSolution.Code.Replace("/", "-") + ".doc");
                filePath = initTemplatePath.Replace(".doc", constructSolution.Code.Replace("/", "-") + ".pdf");
                File.Copy(uploadfilepath, newUrl);
                //更新书签内容
                Document doc = new Aspose.Words.Document(newUrl);
                Bookmark bookmarkProjectName = doc.Range.Bookmarks["ProjectName"];
                if (bookmarkProjectName != null)
                {
                    var project = ProjectService.GetProjectByProjectId(constructSolution.ProjectId);
                    if (project != null)
                    {
                        bookmarkProjectName.Text = project.ProjectName;
                    }
                }
                Bookmark bookmarkCode = doc.Range.Bookmarks["Code"];
                if (bookmarkCode != null)
                {
                    bookmarkCode.Text = constructSolution.Code;
                }
                Bookmark bookmarkUnit = doc.Range.Bookmarks["Unit"];
                if (bookmarkUnit != null)
                {
                    var unit = UnitService.GetUnitByUnitId(constructSolution.UnitId);
                    if (unit != null)
                    {
                        bookmarkUnit.Text = unit.UnitName;
                    }
                }
                Bookmark bookmarkCompileDate = doc.Range.Bookmarks["CompileDate"];
                if (bookmarkCompileDate != null)
                {
                    if (constructSolution.CompileDate != null)
                    {
                        bookmarkCompileDate.Text = string.Format("{0:yyyy-MM-dd}", constructSolution.CompileDate);
                    }
                }
                Bookmark bookmarkSolutionName = doc.Range.Bookmarks["SolutionName"];
                if (bookmarkSolutionName != null)
                {
                    bookmarkSolutionName.Text = constructSolution.SolutionName;
                }
                Bookmark bookmarkSolutionType = doc.Range.Bookmarks["SolutionType"];
                if (bookmarkSolutionType != null)
                {
                    string SolutionType = string.Empty;
                    List<Model.Base_SolutionTempleteType> Solution = BLL.SolutionTempleteTypeService.GetSolutionTempleteList();
                    if (Solution.Count > 0)
                    {
                        for (int i = 0; i < Solution.Count; i++)
                        {
                            if (constructSolution.SolutionType == Solution[i].SolutionTempleteTypeCode)
                            {
                                SolutionType += "■" + Solution[i].SolutionTempleteTypeName + "   ";
                            }
                            else
                            {
                                SolutionType += "□" + Solution[i].SolutionTempleteTypeName + "   ";
                            }

                        }
                    }
                    //if (constructSolution.SolutionType == "1")
                    //{
                    //    bookmarkSolutionType.Text = "■施工组织设计   □专项施工方案   □施工方案";
                    //}
                    //else if (constructSolution.SolutionType == "1")
                    //{
                    //    bookmarkSolutionType.Text = "□施工组织设计   ■专项施工方案   □施工方案";
                    //}
                    //else
                    //{
                    //    bookmarkSolutionType.Text = "□施工组织设计   □专项施工方案   ■施工方案";
                    //}
                    bookmarkSolutionType.Text = SolutionType;
                }
                var reDate = (from x in Funs.DB.Solution_CQMSConstructSolutionApprove where x.ConstructSolutionId == fileId && x.ApproveType == Const.CQMSConstructSolution_ReCompile orderby x.ApproveDate descending select x.ApproveDate).FirstOrDefault();
                List<Model.Solution_CQMSConstructSolutionApprove> approves1 = new List<Model.Solution_CQMSConstructSolutionApprove>();
                List<Model.Solution_CQMSConstructSolutionApprove> approves2 = new List<Model.Solution_CQMSConstructSolutionApprove>();
                List<Model.Solution_CQMSConstructSolutionApprove> approves3 = new List<Model.Solution_CQMSConstructSolutionApprove>();
                List<Model.Solution_CQMSConstructSolutionApprove> approves4 = new List<Model.Solution_CQMSConstructSolutionApprove>();
                List<Model.Solution_CQMSConstructSolutionApprove> approves5 = new List<Model.Solution_CQMSConstructSolutionApprove>();
                List<Model.Solution_CQMSConstructSolutionApprove> approves6 = new List<Model.Solution_CQMSConstructSolutionApprove>();
                if (reDate == null)   //没有重新编制
                {
                    approves1 = (from x in Funs.DB.Solution_CQMSConstructSolutionApprove where x.ConstructSolutionId == fileId && x.ApproveType == Const.CQMSConstructSolution_Audit && x.SignType == "ZY" orderby x.ApproveDate descending select x).ToList();
                    approves2 = (from x in Funs.DB.Solution_CQMSConstructSolutionApprove where x.ConstructSolutionId == fileId && x.ApproveType == Const.CQMSConstructSolution_Audit && x.SignType == "ZL" orderby x.ApproveDate descending select x).ToList();
                    approves3 = (from x in Funs.DB.Solution_CQMSConstructSolutionApprove where x.ConstructSolutionId == fileId && x.ApproveType == Const.CQMSConstructSolution_Audit && x.SignType == "AQ" orderby x.ApproveDate descending select x).ToList();
                    approves4 = (from x in Funs.DB.Solution_CQMSConstructSolutionApprove where x.ConstructSolutionId == fileId && x.ApproveType == Const.CQMSConstructSolution_Audit && x.SignType == "KZ" orderby x.ApproveDate descending select x).ToList();
                    approves5 = (from x in Funs.DB.Solution_CQMSConstructSolutionApprove where x.ConstructSolutionId == fileId && x.ApproveType == Const.CQMSConstructSolution_Audit && x.SignType == "SG" orderby x.ApproveDate descending select x).ToList();
                    approves6 = (from x in Funs.DB.Solution_CQMSConstructSolutionApprove where x.ConstructSolutionId == fileId && x.ApproveType == Const.CQMSConstructSolution_Audit && x.SignType == "XM" orderby x.ApproveDate descending select x).ToList();
                }
                else
                {
                    approves1 = (from x in Funs.DB.Solution_CQMSConstructSolutionApprove where x.ConstructSolutionId == fileId && x.ApproveType == Const.CQMSConstructSolution_Audit && x.SignType == "ZY" && x.ApproveDate > reDate orderby x.ApproveDate descending select x).ToList();
                    approves2 = (from x in Funs.DB.Solution_CQMSConstructSolutionApprove where x.ConstructSolutionId == fileId && x.ApproveType == Const.CQMSConstructSolution_Audit && x.SignType == "ZL" && x.ApproveDate > reDate orderby x.ApproveDate descending select x).ToList();
                    approves3 = (from x in Funs.DB.Solution_CQMSConstructSolutionApprove where x.ConstructSolutionId == fileId && x.ApproveType == Const.CQMSConstructSolution_Audit && x.SignType == "AQ" && x.ApproveDate > reDate orderby x.ApproveDate descending select x).ToList();
                    approves4 = (from x in Funs.DB.Solution_CQMSConstructSolutionApprove where x.ConstructSolutionId == fileId && x.ApproveType == Const.CQMSConstructSolution_Audit && x.SignType == "KZ" && x.ApproveDate > reDate orderby x.ApproveDate descending select x).ToList();
                    approves5 = (from x in Funs.DB.Solution_CQMSConstructSolutionApprove where x.ConstructSolutionId == fileId && x.ApproveType == Const.CQMSConstructSolution_Audit && x.SignType == "SG" && x.ApproveDate > reDate orderby x.ApproveDate descending select x).ToList();
                    approves6 = (from x in Funs.DB.Solution_CQMSConstructSolutionApprove where x.ConstructSolutionId == fileId && x.ApproveType == Const.CQMSConstructSolution_Audit && x.SignType == "XM" && x.ApproveDate > reDate orderby x.ApproveDate descending select x).ToList();
                }
                if (approves1.Count > 0)
                {
                    foreach (var approve in approves1)
                    {
                        Model.Sys_User user = UserService.GetUserByUserId(approve.ApproveMan);
                        if (user != null)
                        {
                            auditMan1 += user.UserName + ",";
                        }
                        if (string.IsNullOrEmpty(approveIdea1))
                        {
                            if (!string.IsNullOrEmpty(approve.ApproveIdea))
                            {
                                approveIdea1 += approve.ApproveIdea;
                            }
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(approve.ApproveIdea))
                            {
                                approveIdea1 += "\r\n" + approve.ApproveIdea;
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty(auditMan1))
                    {
                        auditMan1 = auditMan1.Substring(0, auditMan1.LastIndexOf(","));
                    }
                    if (string.IsNullOrEmpty(approveIdea1))
                    {
                        approveIdea1 = "同意";
                    }
                    auditDate1 = string.Format("{0:yyyy-MM-dd}", approves1[0].ApproveDate);
                }
                if (approves2.Count > 0)
                {
                    foreach (var approve in approves2)
                    {
                        Model.Sys_User user = UserService.GetUserByUserId(approve.ApproveMan);
                        if (user != null)
                        {
                            auditMan2 += user.UserName + ",";
                        }
                        if (string.IsNullOrEmpty(approveIdea2))
                        {
                            if (!string.IsNullOrEmpty(approve.ApproveIdea))
                            {
                                approveIdea2 += approve.ApproveIdea;
                            }
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(approve.ApproveIdea))
                            {
                                approveIdea2 += "\r\n" + approve.ApproveIdea;
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty(auditMan2))
                    {
                        auditMan2 = auditMan2.Substring(0, auditMan2.LastIndexOf(","));
                    }
                    if (string.IsNullOrEmpty(approveIdea2))
                    {
                        approveIdea2 = "同意";
                    }
                    auditDate2 = string.Format("{0:yyyy-MM-dd}", approves2[0].ApproveDate);
                }
                if (approves3.Count > 0)
                {
                    foreach (var approve in approves3)
                    {
                        Model.Sys_User user = UserService.GetUserByUserId(approve.ApproveMan);
                        if (user != null)
                        {
                            auditMan3 += user.UserName + ",";
                        }
                        if (string.IsNullOrEmpty(approveIdea3))
                        {
                            if (!string.IsNullOrEmpty(approve.ApproveIdea))
                            {
                                approveIdea3 += approve.ApproveIdea;
                            }
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(approve.ApproveIdea))
                            {
                                approveIdea3 += "\r\n" + approve.ApproveIdea;
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty(auditMan3))
                    {
                        auditMan3 = auditMan3.Substring(0, auditMan3.LastIndexOf(","));
                    }
                    if (string.IsNullOrEmpty(approveIdea3))
                    {
                        approveIdea3 = "同意";
                    }
                    auditDate3 = string.Format("{0:yyyy-MM-dd}", approves3[0].ApproveDate);
                }
                string approveIdea4 = string.Empty;
                string auditDate4 = string.Empty;
                if (approves4.Count > 0)
                {
                    foreach (var approve in approves4)
                    {
                        Model.Sys_User user = UserService.GetUserByUserId(approve.ApproveMan);
                        if (user != null)
                        {
                            auditMan4 += user.UserName + ",";
                        }
                        if (string.IsNullOrEmpty(approveIdea4))
                        {
                            if (!string.IsNullOrEmpty(approve.ApproveIdea))
                            {
                                approveIdea4 += approve.ApproveIdea;
                            }
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(approve.ApproveIdea))
                            {
                                approveIdea4 += "\r\n" + approve.ApproveIdea;
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty(auditMan4))
                    {
                        auditMan4 = auditMan4.Substring(0, auditMan4.LastIndexOf(","));
                    }
                    if (string.IsNullOrEmpty(approveIdea4))
                    {
                        approveIdea4 = "同意";
                    }
                    auditDate4 = string.Format("{0:yyyy-MM-dd}", approves4[0].ApproveDate);
                }
                string auditMan5 = string.Empty;
                string approveIdea5 = string.Empty;
                string auditDate5 = string.Empty;
                if (approves5.Count > 0)
                {
                    foreach (var approve in approves5)
                    {
                        Model.Sys_User user = UserService.GetUserByUserId(approve.ApproveMan);
                        if (user != null)
                        {
                            auditMan5 += user.UserName + ",";
                        }
                        if (string.IsNullOrEmpty(approveIdea5))
                        {
                            if (!string.IsNullOrEmpty(approve.ApproveIdea))
                            {
                                approveIdea5 += approve.ApproveIdea;
                            }
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(approve.ApproveIdea))
                            {
                                approveIdea5 += "\r\n" + approve.ApproveIdea;
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty(auditMan5))
                    {
                        auditMan5 = auditMan5.Substring(0, auditMan5.LastIndexOf(","));
                    }
                    if (string.IsNullOrEmpty(approveIdea5))
                    {
                        approveIdea5 = "同意";
                    }
                    auditDate5 = string.Format("{0:yyyy-MM-dd}", approves5[0].ApproveDate);
                }
                string auditMan6 = string.Empty;
                string approveIdea6 = string.Empty;
                string auditDate6 = string.Empty;
                if (approves6.Count > 0)
                {
                    foreach (var approve in approves6)
                    {
                        Model.Sys_User user = UserService.GetUserByUserId(approve.ApproveMan);
                        if (user != null)
                        {
                            auditMan6 += user.UserName + ",";
                        }
                        if (string.IsNullOrEmpty(approveIdea6))
                        {
                            if (!string.IsNullOrEmpty(approve.ApproveIdea))
                            {
                                approveIdea6 += approve.ApproveIdea;
                            }
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(approve.ApproveIdea))
                            {
                                approveIdea6 += "\r\n" + approve.ApproveIdea;
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty(auditMan6))
                    {
                        auditMan6 = auditMan6.Substring(0, auditMan6.LastIndexOf(","));
                    }
                    if (string.IsNullOrEmpty(approveIdea6))
                    {
                        approveIdea6 = "同意";
                    }
                    auditDate6 = string.Format("{0:yyyy-MM-dd}", approves6[0].ApproveDate);
                }
                Bookmark bookmarkApproveIdea1 = doc.Range.Bookmarks["ApproveIdea1"];
                if (bookmarkApproveIdea1 != null)
                {
                    bookmarkApproveIdea1.Text = approveIdea1;
                }
                Bookmark bookmarkAuditMan1 = doc.Range.Bookmarks["AuditMan1"];
                if (bookmarkAuditMan1 != null)
                {
                    foreach (var approve in approves1)
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
                    }
                    //bookmarkAuditMan1.Text = auditMan1;
                }
                Bookmark bookmarkAuditDate1 = doc.Range.Bookmarks["AuditDate1"];
                if (bookmarkAuditDate1 != null)
                {
                    bookmarkAuditDate1.Text = auditDate1;
                }
                Bookmark bookmarkApproveIdea2 = doc.Range.Bookmarks["ApproveIdea2"];
                if (bookmarkApproveIdea2 != null)
                {
                    bookmarkApproveIdea2.Text = approveIdea2;
                }
                Bookmark bookmarkAuditMan2 = doc.Range.Bookmarks["AuditMan2"];
                if (bookmarkAuditMan2 != null)
                {
                    foreach (var approve in approves2)
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
                    //bookmarkAuditMan2.Text = auditMan2;
                }
                Bookmark bookmarkAuditDate2 = doc.Range.Bookmarks["AuditDate2"];
                if (bookmarkAuditDate2 != null)
                {
                    bookmarkAuditDate2.Text = auditDate2;
                }
                Bookmark bookmarkApproveIdea3 = doc.Range.Bookmarks["ApproveIdea3"];
                if (bookmarkApproveIdea3 != null)
                {
                    bookmarkApproveIdea3.Text = approveIdea3;
                }
                Bookmark bookmarkAuditMan3 = doc.Range.Bookmarks["AuditMan3"];
                if (bookmarkAuditMan3 != null)
                {
                    foreach (var approve in approves3)
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
                    //bookmarkAuditMan3.Text = auditMan3;
                }
                Bookmark bookmarkAuditDate3 = doc.Range.Bookmarks["AuditDate3"];
                if (bookmarkAuditDate3 != null)
                {
                    bookmarkAuditDate3.Text = auditDate3;
                }
                Bookmark bookmarkApproveIdea4 = doc.Range.Bookmarks["ApproveIdea4"];
                if (bookmarkApproveIdea4 != null)
                {
                    bookmarkApproveIdea4.Text = approveIdea4;
                }
                Bookmark bookmarkAuditMan4 = doc.Range.Bookmarks["AuditMan4"];
                if (bookmarkAuditMan4 != null)
                {
                    foreach (var approve in approves4)
                    {
                        Model.Sys_User user = UserService.GetUserByUserId(approve.ApproveMan);
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
                                        bookmarkAuditMan4.Text = user.UserName;
                                    }

                                }
                            }
                            else
                            {
                                bookmarkAuditMan4.Text = user.UserName;
                            }
                        }
                    }
                    //bookmarkAuditMan4.Text = auditMan4;
                }
                Bookmark bookmarkAuditDate4 = doc.Range.Bookmarks["AuditDate4"];
                if (bookmarkAuditDate4 != null)
                {
                    bookmarkAuditDate4.Text = auditDate4;
                }
                Bookmark bookmarkApproveIdea5 = doc.Range.Bookmarks["ApproveIdea5"];
                if (bookmarkApproveIdea5 != null)
                {
                    bookmarkApproveIdea5.Text = approveIdea5;
                }
                Bookmark bookmarkAuditMan5 = doc.Range.Bookmarks["AuditMan5"];
                if (bookmarkAuditMan5 != null)
                {
                    foreach (var approve in approves5)
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
                    //bookmarkAuditMan5.Text = auditMan5;
                }
                Bookmark bookmarkAuditDate5 = doc.Range.Bookmarks["AuditDate5"];
                if (bookmarkAuditDate5 != null)
                {
                    bookmarkAuditDate5.Text = auditDate5;
                }
                Bookmark bookmarkApproveIdea6 = doc.Range.Bookmarks["ApproveIdea6"];
                if (bookmarkApproveIdea6 != null)
                {
                    bookmarkApproveIdea6.Text = approveIdea6;
                }
                Bookmark bookmarkAuditMan6 = doc.Range.Bookmarks["AuditMan6"];
                if (bookmarkAuditMan6 != null)
                {
                    foreach (var approve in approves6)
                    {
                        Model.Sys_User user = UserService.GetUserByUserId(approve.ApproveMan);
                        if (user != null)
                        {
                            var file = user.SignatureUrl;
                            if (!string.IsNullOrWhiteSpace(file))
                            {
                                string url = rootPath + file;
                                DocumentBuilder builder = new DocumentBuilder(doc);
                                builder.MoveToBookmark("AuditMan6");
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
                                        bookmarkAuditMan6.Text = user.UserName;
                                    }

                                }
                            }
                            else
                            {
                                bookmarkAuditMan6.Text = user.UserName;
                            }
                        }
                    }
                    //bookmarkAuditMan6.Text = auditMan6;
                }
                Bookmark bookmarkAuditDate6 = doc.Range.Bookmarks["AuditDate6"];
                if (bookmarkAuditDate6 != null)
                {
                    bookmarkAuditDate6.Text = auditDate6;
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
                string menuId = Const.CQMSConstructSolutionMenuId;
                PageContext.RegisterStartupScript(Windowtt.GetShowReference(
                 String.Format("../../AttachFile/webuploader.aspx?type=-1&source=1&toKeyId={0}&path=FileUpload/CheckControl&menuId={1}", fileId, menuId)));
            }
        }
    }
}