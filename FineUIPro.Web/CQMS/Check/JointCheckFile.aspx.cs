using Aspose.Words;
using Aspose.Words.Tables;
using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FineUIPro.Web.CQMS.Check
{
    public partial class JointCheckFile : PageBase
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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ProjectId = CurrUser.LoginProjectId;
                //if (!string.IsNullOrEmpty(Request.Params["projectId"]) && Request.Params["projectId"] != this.ProjectId)
                //{
                //    this.ProjectId = Request.Params["projectId"];
                //}
                //权限按钮方法
                UnitService.InitUnitByProjectIdUnitTypeDropDownList(drpSponsorUnit, this.CurrUser.LoginProjectId,BLL.Const.ProjectUnitType_2, true);
                JointCheckService.Init(drpCheckType, true);
                JointCheckService.InitState(drpState, true);
                bindata();
            }
        }
        //<summary>
        //获取办理人姓名
        //</summary>
        //<param name="state"></param>
        //<returns></returns>
        protected string ConvertMan(object JointCheckId)
        {
            string userNames = string.Empty;
            if (JointCheckId != null)
            {
                List<Model.Check_JointCheckApprove> list = BLL.JointCheckApproveService.GetJointCheckApprovesByJointCheckId(JointCheckId.ToString());
                foreach (var a in list)
                {
                    if (a != null)
                    {
                        if (a.ApproveMan != null)
                        {
                            if (!userNames.Contains(BLL.UserService.GetUserByUserId(a.ApproveMan).UserName))
                            {
                                userNames += UserService.GetUserByUserId(a.ApproveMan).UserName + ",";
                            }
                        }
                    }
                }
                if (!string.IsNullOrEmpty(userNames))
                {
                    userNames = userNames.Substring(0, userNames.LastIndexOf(","));
                }
            }
            return userNames;
        }
        /// <summary>
        /// 把状态转换代号为文字形式
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        protected string ConvertState(object state)
        {
            if (state != null)
            {
                if (state.ToString() == BLL.Const.JointCheck_ReCompile)
                {
                    return "重新编制";
                }
                else if (state.ToString() == BLL.Const.JointCheck_Compile)
                {
                    return "编制";
                }
                else if (state.ToString() == BLL.Const.JointCheck_Audit1)
                {
                    return "分包专工回复";
                }
                else if (state.ToString() == BLL.Const.JointCheck_Audit2)
                {
                    return "分包负责人审批";
                }
                else if (state.ToString() == BLL.Const.JointCheck_Audit3)
                {
                    return "总包专工回复";
                }
                else if (state.ToString() == BLL.Const.JointCheck_Audit4)
                {
                    return "总包负责人审批";
                }
                else if (state.ToString() == BLL.Const.JointCheck_Complete)
                {
                    return "审批完成";
                }
                else if (state.ToString() == BLL.Const.JointCheck_Z)
                {
                    return "整改中";
                }
                else if (state.ToString() == BLL.Const.JointCheck_Audit1R)
                {
                    return "分包专工重新回复";
                }
                else
                {
                    return "";
                }
            }
            return "";
        }
        //<summary>
        //获取检查类别
        //</summary>
        //<param name="state"></param>
        //<returns></returns>
        protected string ConvertCheckType(object CheckType)
        {
            if (CheckType != null)
            {
                string checkType = CheckType.ToString();
                if (checkType == "1")
                {
                    return "周检查";
                }
                else if (checkType == "2")
                {
                    return "月检查";
                }
                else if (checkType == "3")
                {
                    return "不定期检查";
                }
                else if (checkType == "4")
                {
                    return "专业检查";
                }
            }
            return "";
        }
        /// <summary>
        /// 列表数据
        /// </summary>
        private void bindata()
        {

            string strSql = @"SELECT chec.JointCheckId,chec.JointCheckCode,chec.State,chec.CheckDate,chec.CheckName,chec.unitId,"
                          + @" unit.UnitName,u.userName as CheckMan,chec.CheckType "
                          + @" FROM Check_JointCheck chec"
                          + @" left join Base_Unit unit on unit.unitId=chec.unitId"
                          + @" left join sys_User u on u.userId = chec.CheckMan"
                          + @" where chec.ProjectId=@ProjectId";
            List<SqlParameter> listStr = new List<SqlParameter>();
            listStr.Add(new SqlParameter("@ProjectId", this.CurrUser.LoginProjectId));
            strSql += " AND (chec.CheckDate>=@startTime or @startTime='') and (chec.CheckDate<=@endTime or @endTime='') ";
            listStr.Add(new SqlParameter("@startTime", !string.IsNullOrEmpty(txtStartTime.Text.Trim()) ? txtStartTime.Text.Trim() + " 00:00:00" : ""));
            listStr.Add(new SqlParameter("@endTime", !string.IsNullOrEmpty(txtEndTime.Text.Trim()) ? txtEndTime.Text.Trim() + " 23:59:59" : ""));
            if (drpSponsorUnit.SelectedValue != BLL.Const._Null)
            {
                strSql += " AND chec.unitId=@unitId";
                listStr.Add(new SqlParameter("@unitId", drpSponsorUnit.SelectedValue));
            }
            if (drpCheckType.SelectedValue != BLL.Const._Null)
            {
                strSql += " AND chec.CheckType=@CheckType";
                listStr.Add(new SqlParameter("@CheckType", drpCheckType.SelectedValue));
            }
            strSql += " AND chec.State=@State";
            listStr.Add(new SqlParameter("@State", Const.JointCheck_Complete));
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
            GvJoinCheck.RecordCount = tb.Rows.Count;
            ddlPageSize.SelectedValue = GvJoinCheck.PageSize.ToString();
            tb = GetFilteredTable(GvJoinCheck.FilteredData, tb);
            var table = GetPagedDataTable(GvJoinCheck, tb);
            GvJoinCheck.DataSource = table;
            GvJoinCheck.DataBind();

        }


        protected void btnQuery_Click(object sender, EventArgs e)
        {
            bindata();
        }

        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            bindata();
        }

        protected void GvJoinCheck_PageIndexChange(object sender, GridPageEventArgs e)
        {
            bindata();
        }

        protected void GvJoinCheck_FilterChange(object sender, EventArgs e)
        {

        }

        protected void GvJoinCheck_Sort(object sender, GridSortEventArgs e)
        {

        }

        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
            bindata();
        }



        protected void btnMenuView_Click(object sender, EventArgs e)
        {
            if (GvJoinCheck.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            string jointCheckId = GvJoinCheck.SelectedRowID.Split(',')[0];
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("JointCheckView.aspx?JointCheckId={0}", jointCheckId, "查看 - ")));
        }


        protected void btnRset_Click(object sender, EventArgs e)
        {
            drpSponsorUnit.SelectedIndex = 0;
            drpCheckType.SelectedIndex = 0;
            drpState.SelectedIndex = 0;
            txtStartTime.Text = "";
            txtEndTime.Text = "";
            bindata();
        }

        /// <summary>
        /// 获取控制点级别
        /// </summary>
        /// <param name="IsOK"></param>
        /// <returns></returns>
        protected string ConvertControlPoint(object ControlItemAndCycleId)
        {
            string controlPoint = string.Empty;
            if (ControlItemAndCycleId != null)
            {
                Model.WBS_ControlItemAndCycle c = BLL.ControlItemAndCycleService.GetControlItemAndCycleById(ControlItemAndCycleId.ToString());
                if (c != null)
                {
                    controlPoint = c.ControlPoint;
                }
            }
            return controlPoint;
        }

        protected void GvJoinCheck_RowCommand(object sender, GridCommandEventArgs e)
        {
            string fileId = GvJoinCheck.Rows[e.RowIndex].DataKeys[0].ToString();
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
                Model.Check_JointCheck jointCheck = JointCheckService.GetJointCheck(fileId);
                initTemplatePath = Const.JointCheckTemplateUrl;
                uploadfilepath = rootPath + initTemplatePath;
                newUrl = uploadfilepath.Replace(".doc", jointCheck.JointCheckCode.Replace("/", "-") + ".doc");
                filePath = initTemplatePath.Replace(".doc", jointCheck.JointCheckCode.Replace("/", "-") + ".pdf");
                File.Copy(uploadfilepath, newUrl);
                Document doc = new Aspose.Words.Document(newUrl);
                Bookmark bookmarkProjectName = doc.Range.Bookmarks["ProjectName"];
                if (bookmarkProjectName != null)
                {
                    var project = ProjectService.GetProjectByProjectId(jointCheck.ProjectId);
                    if (project != null)
                    {
                        bookmarkProjectName.Text = project.ProjectName;
                    }
                }
                Bookmark bookmarkJointCheckCode = doc.Range.Bookmarks["JointCheckCode"];
                if (bookmarkJointCheckCode != null)
                {
                    bookmarkJointCheckCode.Text = jointCheck.JointCheckCode;
                }
                Bookmark bookmarkUnit = doc.Range.Bookmarks["Unit"];
                if (bookmarkUnit != null)
                {
                    var unit = UnitService.GetUnitByUnitId(jointCheck.UnitId);
                    if (unit != null)
                    {
                        bookmarkUnit.Text = unit.UnitName;
                    }
                }
                Bookmark bookmarkCheckDate = doc.Range.Bookmarks["CheckDate"];
                if (bookmarkCheckDate != null)
                {
                    if (jointCheck.CheckDate != null)
                    {
                        bookmarkCheckDate.Text = string.Format("{0:yyyy-MM-dd}", jointCheck.CheckDate);
                    }
                }
                Bookmark bookmarkCheckType = doc.Range.Bookmarks["CheckType"];
                if (bookmarkCheckType != null)
                {
                    if (jointCheck.CheckType == "1")
                    {
                        bookmarkCheckType.Text = "■周检查     □月检查     □不定期检查     □专业检查";
                    }
                    else if (jointCheck.CheckType == "2")
                    {
                        bookmarkCheckType.Text = "□周检查     ■月检查     □不定期检查     □专业检查";
                    }
                    else if (jointCheck.CheckType == "3")
                    {
                        bookmarkCheckType.Text = "□周检查     □月检查     ■不定期检查     □专业检查";
                    }
                    else if (jointCheck.CheckType == "4")
                    {
                        bookmarkCheckType.Text = "□周检查     □月检查     □不定期检查     ■专业检查";
                    }
                }
                Aspose.Words.DocumentBuilder builder = new Aspose.Words.DocumentBuilder(doc);
                bool isbool = builder.MoveToBookmark("Table");
                if (isbool)
                {
                    builder.StartTable();
                    builder.RowFormat.Alignment = Aspose.Words.Tables.RowAlignment.Center;
                    builder.CellFormat.Borders.LineStyle = LineStyle.Single;
                    builder.CellFormat.Borders.Color = System.Drawing.Color.Black;
                    builder.RowFormat.LeftIndent = 5;
                    //builder.RowFormat.RightPadding = 50;
                    builder.Bold = false;
                    //builder.RowFormat.Height = 20;
                    //builder.CellFormat.Width = 80;
                }
                DataTable dt = new DataTable();
                dt.Columns.Add("序号");
                dt.Columns.Add("单位工程");
                dt.Columns.Add("专业");
                dt.Columns.Add("部位");
                dt.Columns.Add("问题描述");
                dt.Columns.Add("实际整改时间");
                dt.Columns.Add("整改方案");
                dt.Columns.Add("签字栏1");
                dt.Columns.Add("签字栏2");
                dt.Columns.Add("签字栏3");
                dt.Columns.Add("签字栏4");
                List<Model.Check_JointCheckDetail> details = JointCheckDetailService.GetLists(fileId);
                int i = 1;
                foreach (var detail in details)
                {
                    DataRow row1 = dt.NewRow();
                    row1[0] = i;
                    var unitWork = UnitWorkService.GetUnitWorkByUnitWorkId(detail.UnitWorkId.ToString());
                    if (unitWork != null)
                    {
                        row1[1] = unitWork.UnitWorkName;
                    }
                    var cNProfessional = CNProfessionalService.GetCNProfessional(detail.CNProfessionalCode);
                    if (cNProfessional != null)
                    {
                        row1[2] = cNProfessional.ProfessionalName;
                    }
                    row1[3] = detail.CheckSite;
                    row1[4] = detail.QuestionDef;
                    if (detail.RectifyDate != null)
                    {
                        row1[5] = string.Format("{0:yyyy-MM-dd}", detail.RectifyDate);
                    }
                    row1[6] = detail.HandleWay;
                    var approve1 = JointCheckApproveService.GetAudit1(fileId, detail.JointCheckDetailId);
                    if (approve1 != null)
                    {
                        var user1 = UserService.GetUserByUserId(approve1.ApproveMan);
                        if (user1 != null)
                        {
                            row1[7] = user1.UserName;
                        }
                    }
                    var approve2 = JointCheckApproveService.GetAudit2(fileId, detail.JointCheckDetailId);
                    if (approve2 != null)
                    {
                        var user2 = UserService.GetUserByUserId(approve2.ApproveMan);
                        if (user2 != null)
                        {
                            row1[8] = user2.UserName;
                        }
                    }
                    var approve3 = JointCheckApproveService.GetAudit3(fileId, detail.JointCheckDetailId);
                    if (approve3 != null)
                    {
                        var user3 = UserService.GetUserByUserId(approve3.ApproveMan);
                        if (user3 != null)
                        {
                            row1[9] = user3.UserName;
                        }
                    }
                    var approve4 = JointCheckApproveService.GetAudit4(fileId, detail.JointCheckDetailId);
                    if (approve4 != null)
                    {
                        var user4 = UserService.GetUserByUserId(approve4.ApproveMan);
                        if (user4 != null)
                        {
                            row1[10] = user4.UserName;
                        }
                    }
                    dt.Rows.Add(row1);
                    i++;
                }

                builder.InsertCell();
                builder.CellFormat.VerticalMerge = Aspose.Words.Tables.CellMerge.None;
                builder.CellFormat.HorizontalMerge = Aspose.Words.Tables.CellMerge.First;
                builder.CellFormat.VerticalAlignment = Aspose.Words.Tables.CellVerticalAlignment.Center;//垂直居中对齐
                builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;//水平居中对齐
                builder.CellFormat.Width = 35;
                builder.Write("序号");

                builder.InsertCell();
                builder.CellFormat.VerticalMerge = Aspose.Words.Tables.CellMerge.None;
                builder.CellFormat.HorizontalMerge = Aspose.Words.Tables.CellMerge.First;
                builder.CellFormat.VerticalAlignment = Aspose.Words.Tables.CellVerticalAlignment.Center;//垂直居中对齐
                builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;//水平居中对齐
                builder.CellFormat.Width = 70;
                builder.Write("单位工程");

                builder.InsertCell();
                builder.CellFormat.VerticalMerge = Aspose.Words.Tables.CellMerge.None;
                builder.CellFormat.HorizontalMerge = Aspose.Words.Tables.CellMerge.First;
                builder.CellFormat.VerticalAlignment = Aspose.Words.Tables.CellVerticalAlignment.Center;//垂直居中对齐
                builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;//水平居中对齐
                builder.CellFormat.Width = 40;
                builder.Write("专业");

                builder.InsertCell();
                builder.CellFormat.VerticalMerge = Aspose.Words.Tables.CellMerge.None;
                builder.CellFormat.HorizontalMerge = Aspose.Words.Tables.CellMerge.First;
                builder.CellFormat.VerticalAlignment = Aspose.Words.Tables.CellVerticalAlignment.Center;//垂直居中对齐
                builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;//水平居中对齐
                builder.CellFormat.Width = 70;
                builder.Write("部位");

                builder.InsertCell();
                builder.CellFormat.VerticalMerge = Aspose.Words.Tables.CellMerge.None;
                builder.CellFormat.HorizontalMerge = Aspose.Words.Tables.CellMerge.First;
                builder.CellFormat.VerticalAlignment = Aspose.Words.Tables.CellVerticalAlignment.Center;//垂直居中对齐
                builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;//水平居中对齐
                builder.CellFormat.Width = 110;
                builder.Write("问题描述");

                builder.InsertCell();
                builder.CellFormat.VerticalMerge = Aspose.Words.Tables.CellMerge.None;
                builder.CellFormat.HorizontalMerge = Aspose.Words.Tables.CellMerge.First;
                builder.CellFormat.VerticalAlignment = Aspose.Words.Tables.CellVerticalAlignment.Center;//垂直居中对齐
                builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;//水平居中对齐
                builder.CellFormat.Width = 80;
                builder.Write("实际整改时间");

                builder.InsertCell();
                builder.CellFormat.VerticalMerge = Aspose.Words.Tables.CellMerge.None;
                builder.CellFormat.HorizontalMerge = Aspose.Words.Tables.CellMerge.First;
                builder.CellFormat.VerticalAlignment = Aspose.Words.Tables.CellVerticalAlignment.Center;//垂直居中对齐
                builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;//水平居中对齐
                builder.CellFormat.Width = 110;
                builder.Write("整改方案");

                builder.InsertCell();
                builder.CellFormat.Borders.LineStyle = LineStyle.Single;
                builder.CellFormat.HorizontalMerge = Aspose.Words.Tables.CellMerge.First;
                builder.CellFormat.VerticalAlignment = Aspose.Words.Tables.CellVerticalAlignment.Center;
                builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;
                builder.CellFormat.Width = 205;
                builder.Write("签字栏");
                builder.EndRow();
                builder.RowFormat.Height = 20;
                builder.Bold = false;
                foreach (DataRow row in dt.Rows)
                {
                    foreach (DataColumn column in dt.Columns)
                    {
                        builder.InsertCell();

                        builder.CellFormat.VerticalMerge = Aspose.Words.Tables.CellMerge.None;
                        builder.CellFormat.HorizontalMerge = Aspose.Words.Tables.CellMerge.First;
                        builder.CellFormat.VerticalAlignment = Aspose.Words.Tables.CellVerticalAlignment.Center;//垂直居中对齐
                        builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;//水平居中对齐
                        if (column.ColumnName == "序号")
                        {
                            builder.CellFormat.Width = 35;
                        }
                        else if (column.ColumnName == "单位工程")
                        {
                            builder.CellFormat.Width = 70;
                        }
                        else if (column.ColumnName == "专业")
                        {
                            builder.CellFormat.Width = 40;
                        }
                        else if (column.ColumnName == "部位")
                        {
                            builder.CellFormat.Width = 70;
                        }
                        else if (column.ColumnName == "问题描述")
                        {
                            builder.CellFormat.Width = 110;
                        }
                        else if (column.ColumnName == "实际整改时间")
                        {
                            builder.CellFormat.Width = 80;
                        }
                        else if (column.ColumnName == "整改方案")
                        {
                            builder.CellFormat.Width = 110;
                        }
                        else if (column.ColumnName == "签字栏1")
                        {
                            builder.CellFormat.Width = 51;
                        }
                        else if (column.ColumnName == "签字栏2")
                        {
                            builder.CellFormat.Width = 51;
                        }
                        else if (column.ColumnName == "签字栏3")
                        {
                            builder.CellFormat.Width = 51;
                        }
                        else if (column.ColumnName == "签字栏4")
                        {
                            builder.CellFormat.Width = 52;
                        }
                        builder.Write(row[column.ColumnName].ToString());
                    }
                    builder.EndRow();
                }
                builder.EndTable();
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
                string menuId = Const.JointCheckMenuId;
                PageContext.RegisterStartupScript(Windowtt.GetShowReference(
                 String.Format("../../AttachFile/webuploader.aspx?type=-1&toKeyId={0}&JointCheck=JointCheck&source=1&path=FileUpload/JointCheck&menuId={1}", fileId, menuId)));
            }
        }
    }
}