using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Aspose.Words;
using BLL;
using Newtonsoft.Json.Linq;

namespace FineUIPro.Web.Personal
{
    public partial class PersonCheckInfo : PageBase
    {
        #region 加载

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.ddlPageSize.SelectedValue = this.Grid1.PageSize.ToString();
                BindGrid();
                if (this.CurrUser.UserId == BLL.Const.sysglyId)
                {
                    GridColumn columnexport = Grid1.FindColumn("export");
                    columnexport.Hidden = false;
                }
            }
        }
        private void BindGrid()
        {
            string strSql = @"select QuarterCheckId, QuarterCheckName, C.UserId, C.ProjectId,     
                            StartTime,EndTime,State,R.RoleName,U.UserName,P.ProjectName from [dbo].[Person_QuarterCheck] C 
                            left join  Sys_Role R  on C.RoleId=R.RoleId 
                            left join  Sys_User U  on C.UserId=U.UserId 
                            left join Base_Project P on C.ProjectId=P.ProjectId where 1=1 ";
            List<SqlParameter> listStr = new List<SqlParameter>();
            strSql += " AND C.UserId=@UserId ";
            listStr.Add(new SqlParameter("@UserId", this.CurrUser.UserId));
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            Grid1.RecordCount = tb.Rows.Count;
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();
        }
        protected string ConvertApprove(object State)
        {
            if (!string.IsNullOrEmpty(State.ToString()))
            {
                return State.ToString() == "1" ? "考核结束" : "正在考核";
            }
            return "";
        }
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            BindGrid();
        }
        #endregion

        #region 数据操作

        protected void Grid1_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            btnMenuEdit_Click(null, null);
        }

        protected void btnMenuEdit_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInParent("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            string Id = Grid1.SelectedRowID;
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("../Person/PersonCheckingView.aspx?QuarterCheckId={0}", Id, "编辑 - ")));
        }
        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
            BindGrid();
        }
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid1.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            BindGrid();
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuDelete_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInParent("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            string rowID = Grid1.SelectedRowID;
            var QuarterCheck = BLL.Person_QuarterCheckService.GetPerson_QuarterCheckById(rowID);
            if (QuarterCheck != null)
            {
                var CheckItem = BLL.Person_QuarterCheckItemService.GetCheckItemListById(rowID);
                if (CheckItem.Count > 0)
                {
                    foreach (var item in CheckItem)
                    {
                        BLL.Person_QuarterCheckItemService.DeleteCheckItem(item.QuarterCheckItemId);
                    }
                }
                var CheckApprove = BLL.Person_QuarterCheckApproveService.GetCheckApproveListById(rowID);
                if (CheckApprove.Count > 0)
                {
                    foreach (var item in CheckApprove)
                    {
                        BLL.Person_QuarterCheckApproveService.DeleteCheckApprove(item.ApproveId);
                    }
                }
                BLL.Person_QuarterCheckService.DeleteQuarterCheck(rowID);



                BindGrid();
                ShowNotify("删除数据成功!", MessageBoxIcon.Success);
            }
        }
        #endregion

        #region 导出

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
                string filePath = string.Empty;


                string Id = Grid1.SelectedRowID;
                var GetQuarterCheck = BLL.Person_QuarterCheckService.GetPerson_QuarterCheckById(Id);
                if (GetQuarterCheck != null)
                {
                    if (GetQuarterCheck.CheckType == "1")
                    {
                        initTemplatePath = BLL.Const.ConsturctTemplateUrl;
                    }
                    else if (GetQuarterCheck.CheckType == "2")
                    {
                        initTemplatePath = BLL.Const.HSSETemplateUrl;
                    }
                    else if (GetQuarterCheck.CheckType == "3")
                    {
                        initTemplatePath = BLL.Const.QATemplateUrl;
                    }
                    else if (GetQuarterCheck.CheckType == "4")
                    {
                        initTemplatePath = BLL.Const.TestTemplateUrl;
                    }
                    else if (GetQuarterCheck.CheckType == "5")
                    {
                        initTemplatePath = BLL.Const.ConsturctEgTemplateUrl;
                    }
                    else if (GetQuarterCheck.CheckType == "6")
                    {
                        initTemplatePath = BLL.Const.HSSEEgTemplateUrl;
                    }
                    else if (GetQuarterCheck.CheckType == "7")
                    {
                        initTemplatePath = BLL.Const.QAEgTemplateUrl;
                    }
                    else if (GetQuarterCheck.CheckType == "8")
                    {
                        initTemplatePath = BLL.Const.TestEgTemplateUrl;
                    }
                    else if (GetQuarterCheck.CheckType == "9")
                    {
                        initTemplatePath = BLL.Const.SGALLEgTemplateUrl;
                    }
                    else if (GetQuarterCheck.CheckType == "10")
                    {
                        initTemplatePath = BLL.Const.SGContractEgTemplateUrl;
                    }
                    else if (GetQuarterCheck.CheckType == "11")
                    {
                        initTemplatePath = BLL.Const.SGHSSEQAEgTemplateUrl;
                    }
                    uploadfilepath = rootPath + initTemplatePath;
                    newUrl = uploadfilepath.Replace(".doc", string.Format("{0:yyyy-MM}", DateTime.Now) + ".doc");
                    filePath = initTemplatePath.Replace(".doc", string.Format("{0:yyyy-MM}", DateTime.Now) + ".pdf");
                    File.Copy(uploadfilepath, newUrl);
                    //更新书签内容
                    Document doc = new Aspose.Words.Document(newUrl);
                    Bookmark bookmarkUserName = doc.Range.Bookmarks["UserName"];
                    if (bookmarkUserName != null)
                    {
                        var userName = BLL.UserService.GetUserByUserId(GetQuarterCheck.UserId).UserName;
                        if (userName != null)
                        {
                            bookmarkUserName.Text = userName;
                        }
                    }
                    Bookmark bookmarkProjectName = doc.Range.Bookmarks["ProjectName"];
                    if (bookmarkProjectName != null)
                    {
                        var ProjectName = BLL.ProjectService.GetProjectByProjectId(GetQuarterCheck.ProjectId).ProjectName;
                        if (ProjectName != null)
                        {
                            bookmarkProjectName.Text = ProjectName;
                        }
                    }
                    var GetCheckItemList = Person_QuarterCheckItemService.GetCheckItemListById(GetQuarterCheck.QuarterCheckId);
                    decimal totalValue = 0;
                    if (GetCheckItemList.Count > 0)
                    {
                        Bookmark bookmarkGrade1 = doc.Range.Bookmarks["Grade1"];
                        if (bookmarkGrade1 != null)
                        {
                            var item = GetCheckItemList.FirstOrDefault(x => x.SortId == 1);
                            if (item != null)
                            {

                                if (!string.IsNullOrEmpty(item.Grade.ToString()))
                                {
                                    var a = Convert.ToDouble(Convert.ToDouble(item.Grade) * (Convert.ToDouble(item.StandardGrade) / 100));
                                    bookmarkGrade1.Text = (decimal.Round(decimal.Parse(a.ToString()), 1)).ToString();
                                    totalValue += Convert.ToDecimal(bookmarkGrade1.Text);
                                }

                            }
                        }
                        Bookmark bookmarkGrade2 = doc.Range.Bookmarks["Grade2"];
                        if (bookmarkGrade2 != null)
                        {
                            var item = GetCheckItemList.FirstOrDefault(x => x.SortId == 2);
                            if (item != null)
                            {
                                if (!string.IsNullOrEmpty(item.Grade.ToString()))
                                {
                                    var a = Convert.ToDouble(Convert.ToDouble(item.Grade) * (Convert.ToDouble(item.StandardGrade) / 100));
                                    bookmarkGrade2.Text = (decimal.Round(decimal.Parse(a.ToString()), 1)).ToString();
                                    totalValue += Convert.ToDecimal(bookmarkGrade2.Text);
                                }
                            }

                        }
                        Bookmark bookmarkGrade3 = doc.Range.Bookmarks["Grade3"];
                        if (bookmarkGrade3 != null)
                        {
                            var item = GetCheckItemList.FirstOrDefault(x => x.SortId == 3);
                            if (item != null)
                            {
                                if (!string.IsNullOrEmpty(item.Grade.ToString()))
                                {
                                    var a = Convert.ToDouble(Convert.ToDouble(item.Grade) * (Convert.ToDouble(item.StandardGrade) / 100));
                                    bookmarkGrade3.Text = (decimal.Round(decimal.Parse(a.ToString()), 1)).ToString();
                                    totalValue += Convert.ToDecimal(bookmarkGrade3.Text);
                                }
                            }

                        }
                        Bookmark bookmarkGrade4 = doc.Range.Bookmarks["Grade4"];
                        if (bookmarkGrade4 != null)
                        {
                            var item = GetCheckItemList.FirstOrDefault(x => x.SortId == 4);
                            if (item != null)
                            {
                                if (!string.IsNullOrEmpty(item.Grade.ToString()))
                                {
                                    var a = Convert.ToDouble(Convert.ToDouble(item.Grade) * (Convert.ToDouble(item.StandardGrade) / 100));
                                    bookmarkGrade4.Text = (decimal.Round(decimal.Parse(a.ToString()), 1)).ToString();
                                    totalValue += Convert.ToDecimal(bookmarkGrade4.Text);
                                }
                            }

                        }
                        Bookmark bookmarkGrade5 = doc.Range.Bookmarks["Grade5"];
                        if (bookmarkGrade5 != null)
                        {
                            var item = GetCheckItemList.FirstOrDefault(x => x.SortId == 5);
                            if (item != null)
                            {
                                if (!string.IsNullOrEmpty(item.Grade.ToString()))
                                {
                                    var a = Convert.ToDouble(Convert.ToDouble(item.Grade) * (Convert.ToDouble(item.StandardGrade) / 100));
                                    bookmarkGrade5.Text = (decimal.Round(decimal.Parse(a.ToString()), 1)).ToString();
                                    totalValue += Convert.ToDecimal(bookmarkGrade5.Text);
                                }
                            }

                        }
                        Bookmark bookmarkGrade6 = doc.Range.Bookmarks["Grade6"];
                        if (bookmarkGrade6 != null)
                        {
                            var item = GetCheckItemList.FirstOrDefault(x => x.SortId == 6);
                            if (item != null)
                            {
                                if (!string.IsNullOrEmpty(item.Grade.ToString()))
                                {
                                    var a = Convert.ToDouble(Convert.ToDouble(item.Grade) * (Convert.ToDouble(item.StandardGrade) / 100));
                                    bookmarkGrade6.Text = (decimal.Round(decimal.Parse(a.ToString()), 1)).ToString();
                                    totalValue += Convert.ToDecimal(bookmarkGrade6.Text);
                                }
                            }

                        }
                        Bookmark bookmarkGrade7 = doc.Range.Bookmarks["Grade7"];
                        if (bookmarkGrade7 != null)
                        {
                            var item = GetCheckItemList.FirstOrDefault(x => x.SortId == 7);
                            if (item != null)
                            {
                                if (!string.IsNullOrEmpty(item.Grade.ToString()))
                                {
                                    var a = Convert.ToDouble(Convert.ToDouble(item.Grade) * (Convert.ToDouble(item.StandardGrade) / 100));
                                    bookmarkGrade7.Text = (decimal.Round(decimal.Parse(a.ToString()), 1)).ToString();
                                    totalValue += Convert.ToDecimal(bookmarkGrade7.Text);
                                }
                            }

                        }
                        Bookmark bookmarkGrade8 = doc.Range.Bookmarks["Grade8"];
                        if (bookmarkGrade1 != null)
                        {
                            var item = GetCheckItemList.FirstOrDefault(x => x.SortId == 8);
                            if (item != null)
                            {
                                if (!string.IsNullOrEmpty(item.Grade.ToString()))
                                {
                                    var a = Convert.ToDouble(Convert.ToDouble(item.Grade) * (Convert.ToDouble(item.StandardGrade) / 100));
                                    bookmarkGrade8.Text = (decimal.Round(decimal.Parse(a.ToString()), 1)).ToString();
                                    totalValue += Convert.ToDecimal(bookmarkGrade8.Text);
                                }
                            }

                        }
                        Bookmark bookmarkGrade9 = doc.Range.Bookmarks["Grade9"];
                        if (bookmarkGrade9 != null)
                        {
                            var item = GetCheckItemList.FirstOrDefault(x => x.SortId == 9);
                            if (item != null)
                            {
                                if (!string.IsNullOrEmpty(item.Grade.ToString()))
                                {
                                    var a = Convert.ToDouble(Convert.ToDouble(item.Grade) * (Convert.ToDouble(item.StandardGrade) / 100));
                                    bookmarkGrade9.Text = (decimal.Round(decimal.Parse(a.ToString()), 1)).ToString();
                                    totalValue += Convert.ToDecimal(bookmarkGrade9.Text);
                                }
                            }

                        }
                        Bookmark bookmarkGrade10 = doc.Range.Bookmarks["Grade10"];
                        if (bookmarkGrade10 != null)
                        {
                            var item = GetCheckItemList.FirstOrDefault(x => x.SortId == 10);
                            if (item != null)
                            {
                                if (!string.IsNullOrEmpty(item.Grade.ToString()))
                                {
                                    var a = Convert.ToDouble(Convert.ToDouble(item.Grade) * (Convert.ToDouble(item.StandardGrade) / 100));
                                    bookmarkGrade10.Text = (decimal.Round(decimal.Parse(a.ToString()), 1)).ToString();
                                    totalValue += Convert.ToDecimal(bookmarkGrade10.Text);
                                }
                            }

                        }


                        Bookmark bookmarkGradeTotal = doc.Range.Bookmarks["GradeTotal"];
                        if (bookmarkGradeTotal != null)
                        {

                            bookmarkGradeTotal.Text = totalValue.ToString();

                        }
                        var approves = Person_QuarterCheckApproveService.GetCheckApproveListById(GetQuarterCheck.QuarterCheckId);
                        foreach (var approve in approves)
                        {
                            var projectUser = ProjectUserService.GetProjectUserByUserIdProjectId(GetQuarterCheck.ProjectId, approve.UserId);
                            if (projectUser != null)
                            {
                                if (projectUser.RoleId == BLL.Const.ConstructionManager)
                                {
                                    if (approve.ApproveDate != null)
                                    {
                                        Bookmark ConstructUser = doc.Range.Bookmarks["ConstructUser"];
                                        if (ConstructUser != null)
                                        {
                                            var user = BLL.UserService.GetUserByUserId(approve.UserId);
                                            if (!string.IsNullOrEmpty(user.SignatureUrl))
                                            {
                                                var file = user.SignatureUrl;
                                                if (!string.IsNullOrWhiteSpace(file))
                                                {
                                                    string url = rootPath + file;
                                                    DocumentBuilder builders = new DocumentBuilder(doc);
                                                    builders.MoveToBookmark("ConstructUser");
                                                    builders.InsertImage(url, 100, 20);
                                                }
                                            }
                                            else
                                            {
                                                ConstructUser.Text = user.UserName;
                                            }
                                        }
                                        Bookmark ConstructDate = doc.Range.Bookmarks["ConstructDate"];
                                        if (ConstructDate != null)
                                        {
                                            ConstructDate.Text = string.Format("{0:yyyy-MM-dd}", approve.ApproveDate);
                                        }
                                    }


                                }
                                else if (projectUser.RoleId == BLL.Const.ProjectManager)
                                {
                                    if (approve.ApproveDate != null)
                                    {
                                        Bookmark ProjectUser = doc.Range.Bookmarks["ProjectUser"];
                                        if (ProjectUser != null)
                                        {
                                            var user = BLL.UserService.GetUserByUserId(approve.UserId);
                                            if (!string.IsNullOrEmpty(user.SignatureUrl))
                                            {
                                                var file = user.SignatureUrl;
                                                if (!string.IsNullOrWhiteSpace(file))
                                                {
                                                    string url = rootPath + file;
                                                    DocumentBuilder builders = new DocumentBuilder(doc);
                                                    builders.MoveToBookmark("ProjectUser");
                                                    builders.InsertImage(url, 100, 20);
                                                }
                                            }
                                            else
                                            {
                                                ProjectUser.Text = user.UserName;
                                            }
                                        }
                                        Bookmark ProjectDate = doc.Range.Bookmarks["ProjectDate"];
                                        if (ProjectDate != null)
                                        {
                                            ProjectDate.Text = string.Format("{0:yyyy-MM-dd}", approve.ApproveDate);
                                        }
                                    }
                                }
                                else if (projectUser.RoleId == BLL.Const.HSSEManager)
                                {
                                    if (approve.ApproveDate != null)
                                    {


                                        Bookmark HSSEUser = doc.Range.Bookmarks["HSSEUser"];
                                        if (HSSEUser != null)
                                        {
                                            var user = BLL.UserService.GetUserByUserId(approve.UserId);
                                            if (!string.IsNullOrEmpty(user.SignatureUrl))
                                            {
                                                var file = user.SignatureUrl;
                                                if (!string.IsNullOrWhiteSpace(file))
                                                {
                                                    string url = rootPath + file;
                                                    DocumentBuilder builders = new DocumentBuilder(doc);
                                                    builders.MoveToBookmark("HSSEUser");
                                                    builders.InsertImage(url, 100, 20);
                                                }
                                            }
                                            else
                                            {
                                                HSSEUser.Text = user.UserName;
                                            }
                                        }
                                        Bookmark HSSEDate = doc.Range.Bookmarks["HSSEDate"];
                                        if (HSSEDate != null)
                                        {
                                            HSSEDate.Text = string.Format("{0:yyyy-MM-dd}", approve.ApproveDate);
                                        }
                                    }
                                }
                                else if (projectUser.RoleId == BLL.Const.QAManager)
                                {
                                    if (approve.ApproveDate != null)
                                    {


                                        Bookmark QAUser = doc.Range.Bookmarks["QAUser"];
                                        if (QAUser != null)
                                        {
                                            var user = BLL.UserService.GetUserByUserId(approve.UserId);
                                            if (!string.IsNullOrEmpty(user.SignatureUrl))
                                            {
                                                var file = user.SignatureUrl;
                                                if (!string.IsNullOrWhiteSpace(file))
                                                {
                                                    string url = rootPath + file;
                                                    DocumentBuilder builders = new DocumentBuilder(doc);
                                                    builders.MoveToBookmark("QAUser");
                                                    builders.InsertImage(url, 100, 20);
                                                }
                                            }
                                            else
                                            {
                                                QAUser.Text = user.UserName;
                                            }
                                        }
                                        Bookmark QADate = doc.Range.Bookmarks["QADate"];
                                        if (QADate != null)
                                        {
                                            QADate.Text = string.Format("{0:yyyy-MM-dd}", approve.ApproveDate);
                                        }
                                    }
                                }
                                else if (projectUser.RoleId == BLL.Const.TestManager)
                                {
                                    if (approve.ApproveDate != null)
                                    {
                                        Bookmark TestUser = doc.Range.Bookmarks["TestUser"];
                                        if (TestUser != null)
                                        {
                                            var user = BLL.UserService.GetUserByUserId(approve.UserId);
                                            if (!string.IsNullOrEmpty(user.SignatureUrl))
                                            {
                                                var file = user.SignatureUrl;
                                                if (!string.IsNullOrWhiteSpace(file))
                                                {
                                                    string url = rootPath + file;
                                                    DocumentBuilder builders = new DocumentBuilder(doc);
                                                    builders.MoveToBookmark("TestUser");
                                                    builders.InsertImage(url, 100, 20);
                                                }
                                            }
                                            else
                                            {
                                                TestUser.Text = user.UserName;
                                            }
                                        }
                                        Bookmark TestDate = doc.Range.Bookmarks["TestDate"];
                                        if (TestDate != null)
                                        {
                                            TestDate.Text = string.Format("{0:yyyy-MM-dd}", approve.ApproveDate);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                var SGUser = UserService.GetUserByUserId(BLL.Const.SGGLB);
                                if (approve.UserId == SGUser.UserId)
                                {
                                    if (approve.ApproveDate != null)
                                    {


                                        Bookmark SGGLUser = doc.Range.Bookmarks["SGUser"];
                                        if (SGGLUser != null)
                                        {
                                            var user = BLL.UserService.GetUserByUserId(approve.UserId);
                                            if (!string.IsNullOrEmpty(user.SignatureUrl))
                                            {
                                                var file = user.SignatureUrl;
                                                if (!string.IsNullOrWhiteSpace(file))
                                                {
                                                    string url = rootPath + file;
                                                    DocumentBuilder builders = new DocumentBuilder(doc);
                                                    builders.MoveToBookmark("SGUser");
                                                    builders.InsertImage(url, 100, 20);
                                                }
                                            }
                                            else
                                            {
                                                SGGLUser.Text = user.UserName;
                                            }
                                        }
                                        Bookmark SGDate = doc.Range.Bookmarks["SGDate"];
                                        if (SGDate != null)
                                        {
                                            SGDate.Text = string.Format("{0:yyyy-MM-dd}", approve.ApproveDate);
                                        }
                                    }
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
                    }
                }
            }
        }

        #endregion

        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            BindGrid();
        }
    }
}