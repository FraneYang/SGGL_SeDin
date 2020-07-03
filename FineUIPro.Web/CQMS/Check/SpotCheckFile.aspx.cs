using Aspose.Words;
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
    public partial class SpotCheckFile : PageBase
    {
        /// <summary>
        /// 工序验收记录主键
        /// </summary>
        public string SpotCheckCode
        {
            get
            {
                return (string)ViewState["SpotCheckCode"];
            }
            set
            {
                ViewState["SpotCheckCode"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                UnitService.GetUnit(drpUnit, CurrUser.LoginProjectId, true);
                ddlPageSize.SelectedValue = Grid1.PageSize.ToString();
                BindData();

            }
        }


        protected DataTable ChecklistData()
        {
            string strSql = @"SELECT chec.SpotCheckCode,chec.ProjectId,chec.UnitId,"
                          + @" chec.DocCode,chec.State,chec.SpotCheckDate,cn.ProfessionalName,"
                          + @" unit.UnitName,u.userName as CreateMan "
                          + @" FROM Check_SpotCheck chec "
                          + @" left join Base_Unit unit on unit.unitId=chec.UnitId "
                          + @" left join sys_User u on u.userId = chec.CreateMan"
                          + @" left join Base_CNProfessional cn on cn.CNProfessionalId = chec.CNProfessionalCode"
                          + @" where chec.ProjectId=@ProjectId";

            List<SqlParameter> listStr = new List<SqlParameter>();
            listStr.Add(new SqlParameter("@ProjectId", CurrUser.LoginProjectId));
            strSql += " AND (chec.SpotCheckDate>=@startTime or @startTime='') and (chec.SpotCheckDate<=@endTime or @endTime='') ";
            listStr.Add(new SqlParameter("@startTime", !string.IsNullOrEmpty(txtStartTime.Text.Trim()) ? txtStartTime.Text.Trim() + " 00:00:00" : ""));
            listStr.Add(new SqlParameter("@endTime", !string.IsNullOrEmpty(txtEndTime.Text.Trim()) ? txtEndTime.Text.Trim() + " 23:59:59" : ""));
            if (drpUnit.SelectedValue != Const._Null)
            {
                strSql += " AND chec.UnitId=@unitId";
                listStr.Add(new SqlParameter("@unitId", drpUnit.SelectedValue));
            }
            strSql += " AND chec.State=" + Const.SpotCheck_Complete;
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            return tb;
        }


        private void BindData()
        {
            var list = ChecklistData();
            Grid1.RecordCount = list.Rows.Count;
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
            SpotCheckCode = Grid1.SelectedRowID.Split(',')[0];
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("SpotCheckView.aspx?SpotCheckCode={0}", SpotCheckCode, "查看 - ")));
        }


        protected void btnRset_Click(object sender, EventArgs e)
        {
            txtStartTime.Text = "";
            txtEndTime.Text = "";
            drpUnit.SelectedIndex = 0;
            BindData();
        }

        /// <summary>
        /// 根据主键返回共检日期
        /// </summary>
        /// <param name="SpotCheckCode"></param>
        /// <returns></returns>
        protected string ConvertSpotCheckDate(object SpotCheckCode)
        {
            if (SpotCheckCode != null)
            {
                Model.Check_SpotCheck spotCheck = BLL.SpotCheckService.GetSpotCheckBySpotCheckCode(SpotCheckCode.ToString());
                if (spotCheck != null)
                {
                    if (spotCheck.CheckDateType == "1")
                    {
                        return string.Format("{0:yyyy-MM-dd HH:mm}", spotCheck.SpotCheckDate);
                    }
                    else
                    {
                        return string.Format("{0:yyyy-MM-dd HH:mm}", spotCheck.SpotCheckDate) + "—" + string.Format("{0:yyyy-MM-dd HH:mm}", spotCheck.SpotCheckDate2);
                    }
                }
            }
            return "";
        }

        //<summary>
        //获取办理人姓名
        //</summary>
        //<param name="state"></param>
        //<returns></returns>
        public static string ConvertMan(object SpotCheckCode)
        {
            if (SpotCheckCode != null)
            {
                Model.Check_SpotCheckApprove a = BLL.SpotCheckApproveService.GetSpotCheckApproveBySpotCheckCode(SpotCheckCode.ToString());
                if (a != null)
                {
                    if (a.ApproveMan != null)
                    {
                        return BLL.UserService.GetUserByUserId(a.ApproveMan).UserName;
                    }
                }
                else
                {
                    return "";
                }
            }
            return "";
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
                Model.Check_SpotCheck spCheck = SpotCheckService.GetSpotCheckBySpotCheckCode(fileId);
                initTemplatePath = Const.SpotCheckTemplateUrl;
                uploadfilepath = rootPath + initTemplatePath;
                newUrl = uploadfilepath.Replace(".doc", spCheck.DocCode + ".doc");
                filePath = initTemplatePath.Replace(".doc", spCheck.DocCode + ".pdf");
                File.Copy(uploadfilepath, newUrl);
                //更新书签内容
                Document doc = new Aspose.Words.Document(newUrl);
                Bookmark bookmarkProjectName = doc.Range.Bookmarks["ProjectName"];
                if (bookmarkProjectName != null)
                {
                    var project = ProjectService.GetProjectByProjectId(spCheck.ProjectId);
                    if (project != null)
                    {
                        bookmarkProjectName.Text = project.ProjectName;
                    }
                }
                Bookmark bookmarkDocCode = doc.Range.Bookmarks["DocCode"];
                if (bookmarkDocCode != null)
                {
                    if (bookmarkDocCode != null)
                    {
                        bookmarkDocCode.Text = spCheck.DocCode;
                    }
                }
                Bookmark bookmarkCNProfessional = doc.Range.Bookmarks["CNProfessional"];
                if (bookmarkCNProfessional != null)
                {
                    var cNProfessional = CNProfessionalService.GetCNProfessional(spCheck.CNProfessionalCode);
                    if (cNProfessional != null)
                    {
                        bookmarkCNProfessional.Text = cNProfessional.ProfessionalName;
                    }
                }
                Bookmark bookmarkCheckArea = doc.Range.Bookmarks["CheckArea"];
                if (bookmarkCheckArea != null)
                {
                    bookmarkCheckArea.Text = spCheck.CheckArea;
                }
                Bookmark bookmarkProjectCode = doc.Range.Bookmarks["ProjectCode"];
                if (bookmarkProjectName != null)
                {
                    var project = ProjectService.GetProjectByProjectId(spCheck.ProjectId);
                    if (project != null)
                    {
                        bookmarkProjectCode.Text = project.ProjectCode;
                    }
                }
                Bookmark bookmarkSpotCheckDate = doc.Range.Bookmarks["SpotCheckDate"];
                if (bookmarkSpotCheckDate != null)
                {
                    if (spCheck.SpotCheckDate != null)
                    {
                        var day = Convert.ToDateTime(spCheck.SpotCheckDate).ToString("yyyy年MM月dd日hh时");
                        //var hour = Convert.ToDateTime(spCheck.SpotCheckDate).Hour.ToString();
                        bookmarkSpotCheckDate.Text = day;
                    }
                }
                Bookmark bookmarkCreateDate = doc.Range.Bookmarks["CreateDate"];
                if (bookmarkSpotCheckDate != null)
                {
                    if (spCheck.CreateDate != null)
                    {
                        bookmarkCreateDate.Text = Convert.ToDateTime(spCheck.SpotCheckDate).ToString("yyyy年MM月dd日");
                    }
                }
                Bookmark bookmarkJointCheckMan = doc.Range.Bookmarks["JointCheckMan"];
                if (bookmarkJointCheckMan != null)
                {
                    //业主
                    StringBuilder sMan = new StringBuilder();
                    sMan.Append(!string.IsNullOrWhiteSpace(spCheck.JointCheckMans3) ? "■业主" : "□业主");
                    sMan.Append(' ', 10);
                    sMan.Append(!string.IsNullOrWhiteSpace(spCheck.JointCheckMans2) ? "■监理" : "□监理");
                    sMan.Append(' ', 10);
                    sMan.Append(!string.IsNullOrWhiteSpace(spCheck.JointCheckMans) ? "■总承包商" : "□总承包商");
                    bookmarkJointCheckMan.Text = sMan.ToString();
                }
                Bookmark bookmarkCreateMan = doc.Range.Bookmarks["CreateMan"];
                if (bookmarkCreateMan != null)
                {
                    var file = AttachFileService.GetfileUrl(spCheck.CreateMan);
                    if (!string.IsNullOrWhiteSpace(file))
                    {
                        string url = rootPath + file;
                        DocumentBuilder builders = new DocumentBuilder(doc);
                        builders.MoveToBookmark("CreateMan");
                        if (!string.IsNullOrEmpty(url))
                        {
                            System.Drawing.Size JpgSize;
                            float Wpx;
                            float Hpx;
                            UploadAttachmentService.getJpgSize(url, out JpgSize, out Wpx, out Hpx);
                            double i = 1;
                            if (JpgSize.Width >= JpgSize.Height)
                            {
                                i = JpgSize.Width / 320;
                            }
                            else
                            {
                                i = JpgSize.Height / 320;
                            }
                            if (File.Exists(url))
                            {
                                builders.InsertImage(url, 50, 50);
                            }
                            else
                            {
                                bookmarkCreateMan.Text = UserService.GetUserNameByUserId(spCheck.CreateMan);
                            }

                        }
                    }
                    else
                    {
                        bookmarkCreateMan.Text = UserService.GetUserNameByUserId(spCheck.CreateMan);
                    }
                }
                //Bookmark bookmarkTable = doc.Range.Bookmarks["Table"];
                //Aspose.Words.Tables.Table tables = new Aspose.Words.Tables.Table(doc);
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
                dt.Columns.Add("序号", typeof(string));
                dt.Columns.Add("共检项目名称", typeof(string));
                dt.Columns.Add("控制点级别", typeof(string));
                var sports = SpotCheckDetailService.GetSpotCheckDetails(spCheck.SpotCheckCode);
                if (sports.Count > 0)
                {
                    foreach (var item in sports)
                    {
                        int i = sports.IndexOf(item) + 1;
                        dt.Rows.Add(new string[] { i.ToString(), ConvertDetailName(item.ControlItemAndCycleId), ConvertControlPoint(item.ControlItemAndCycleId) });
                    }
                }
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
                            builder.CellFormat.Width = 30;
                        }
                        else if (column.ColumnName == "共检项目名称")
                        {
                            builder.CellFormat.Width = 318;
                        }
                        else if (column.ColumnName == "控制点级别")
                        {
                            builder.CellFormat.Width = 65;
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
                string menuId = Const.SpotCheckMenuId;
                PageContext.RegisterStartupScript(Windowtt.GetShowReference(
                 String.Format("../../AttachFile/webuploader.aspx?type=-1&source=1&toKeyId={0}&path=FileUpload/SpotCheck&menuId={1}", fileId, menuId)));
            }
        }

        /// <summary>
        /// 获取共检内容
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        protected string ConvertDetailName(object ControlItemAndCycleId)
        {
            string name = string.Empty;
            if (ControlItemAndCycleId != null)
            {
                Model.WBS_ControlItemAndCycle c = BLL.ControlItemAndCycleService.GetControlItemAndCycleById(ControlItemAndCycleId.ToString());
                if (c != null)
                {
                    name = c.ControlItemContent;
                    Model.WBS_WorkPackage w = BLL.WorkPackageService.GetWorkPackageByWorkPackageId(c.WorkPackageId);
                    if (w != null)
                    {
                        name = w.PackageContent + "/" + name;
                        Model.WBS_WorkPackage pw = BLL.WorkPackageService.GetWorkPackageByWorkPackageId(w.SuperWorkPackageId);
                        if (pw != null)
                        {
                            name = pw.PackageContent + "/" + name;
                            Model.WBS_WorkPackage ppw = BLL.WorkPackageService.GetWorkPackageByWorkPackageId(pw.SuperWorkPackageId);
                            if (ppw != null)
                            {
                                name = ppw.PackageContent + "/" + name;
                                Model.WBS_UnitWork u = BLL.UnitWorkService.GetUnitWorkByUnitWorkId(ppw.UnitWorkId);
                                if (u != null)
                                {
                                    name = u.UnitWorkName + "/" + name;
                                }
                            }
                            else
                            {
                                Model.WBS_UnitWork u = BLL.UnitWorkService.GetUnitWorkByUnitWorkId(pw.UnitWorkId);
                                if (u != null)
                                {
                                    name = u.UnitWorkName + "/" + name;
                                }
                            }
                        }
                        else
                        {
                            Model.WBS_UnitWork u = BLL.UnitWorkService.GetUnitWorkByUnitWorkId(w.UnitWorkId);
                            if (u != null)
                            {
                                name = u.UnitWorkName + "/" + name;
                            }
                        }
                    }
                }
            }
            return name;
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
    }
}