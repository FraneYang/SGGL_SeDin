using Aspose.Words;
using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
namespace FineUIPro.Web.HSSE.EduTrain
{
    public partial class TestRecord : PageBase
    {
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
                ddlPageSize.SelectedValue = Grid1.PageSize.ToString();
                if (this.CurrUser.UserId == BLL.Const.sysglyId)
                {
                    this.btnMenuDelete.Hidden = false;
                }

                ///更新没有结束时间且超时的考试记录
                GetDataService.UpdateTestPlanStates();
                // 绑定表格
                BindGrid();
            }
            else
            {
                if (GetRequestEventArgument() == "reloadGrid")
                {
                    BindGrid();
                }
            }
        }
        #endregion

        #region 绑定数据
        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            string strSql = @"SELECT TestRecord.TestRecordId,TestRecord.TestPlanId, TestRecord.TestManId,TestRecord.TestStartTime,TestRecord.TestEndTime, TestRecord.TestScores,
                            (CASE WHEN TestPlan.PlanName IS NULL THEN Training.TrainingName ELSE TestPlan.PlanName END) AS PlanName, 
                            ISNULL(TestPlan.Duration,90) AS Duration,ISNULL(TestPlan.TotalScore,100) AS TotalScore,TestPlan.TestPalce,ISNULL(TestPlan.QuestionCount,95) AS QuestionCount,TestRecord.TemporaryUser,Person.PersonName AS TestManName
                            ,Unit.UnitName"
                         + @" FROM dbo.Training_TestRecord AS TestRecord"
                         + @" LEFT JOIN dbo.Training_TestPlan AS TestPlan ON TestPlan.TestPlanId=TestRecord.TestPlanId"
                         + @" LEFT JOIN dbo.Training_TestTraining AS Training ON Training.TrainingId = TestRecord.TestType"
                         + @" LEFT JOIN dbo.SitePerson_Person AS Person ON Person.PersonId = TestRecord.TestManId "
                         + @" LEFT JOIN dbo.Base_Unit AS Unit ON Person.UnitId=Unit.UnitId"
                         + @" WHERE (isFiled IS NULL OR isFiled = 0) and TestRecord.ProjectId = '" + this.CurrUser.LoginProjectId + "'";
            List<SqlParameter> listStr = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(this.txtName.Text.Trim()))
            {
                strSql += " AND (Person.PersonName LIKE @name OR TestPlan.PlanName LIKE @name OR Training.TrainingName LIKE @name)";
                listStr.Add(new SqlParameter("@name", "%" + this.txtName.Text.Trim() + "%"));
            }
            if (!string.IsNullOrEmpty(this.txtMinScores.Text.Trim()))
            {
                strSql += " AND TestRecord.TestScores >= @MinScores";
                listStr.Add(new SqlParameter("@MinScores", Funs.GetNewDecimalOrZero(this.txtMinScores.Text.Trim())));
            }
            if (!string.IsNullOrEmpty(this.txtMaxScores.Text.Trim()))
            {
                strSql += " AND TestRecord.TestScores <= @MaxScores";
                listStr.Add(new SqlParameter("@MaxScores", Funs.GetNewDecimalOrZero(this.txtMaxScores.Text.Trim())));
            }
            //if (this.IsTemp.Checked)
            //{
            //    strSql += " AND Users.IsTemp = 1 ";
            //}
            //else
            //{
            //    strSql += " AND (Users.IsTemp = 0 OR Users.IsTemp IS NULL)";
            //}
            if (this.ckIsNULL.Checked)
            {
                strSql += " AND (TestRecord.TestStartTime IS NULL OR TestRecord.TestEndTime IS NULL) ";
            }
            if (!string.IsNullOrEmpty(this.txtStartDate.Text))
            {
                strSql += " AND TestRecord.TestStartTime >= @StartDate";
                listStr.Add(new SqlParameter("@StartDate", Funs.GetNewDateTime(this.txtStartDate.Text)));
            }
            if (!string.IsNullOrEmpty(this.txtEndDate.Text))
            {
                strSql += " AND TestRecord.TestEndTime <= @EndDate ";
                listStr.Add(new SqlParameter("@EndDate", Funs.GetNewDateTime(this.txtEndDate.Text)));
            }
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            Grid1.RecordCount = tb.Rows.Count;
            var table = this.GetPagedDataTable(Grid1, tb);

            Grid1.DataSource = table;
            Grid1.DataBind();
            for (int i = 0; i < Grid1.Rows.Count; i++)
            {
                string testRecordId = Grid1.Rows[i].DataKeys[0].ToString();
                var testRecord = BLL.TestRecordService.GetTestRecordById(testRecordId);
                if (testRecord != null)
                {
                    if (testRecord.TestScores < SysConstSetService.getPassScore())
                    {
                        Grid1.Rows[i].RowCssClass = "Red";
                    }
                }
            }
        }
        #endregion

        #region 分页、关闭窗口
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
        /// 排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_Sort(object sender, FineUIPro.GridSortEventArgs e)
        {
            BindGrid();
        }

        #endregion

        #region 编辑
        /// <summary>
        /// Grid行双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            this.EditData();
        }

        /// <summary>
        /// 右键编辑事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuView_Click(object sender, EventArgs e)
        {
            this.EditData();
        }

        /// <summary>
        /// 编辑数据方法
        /// </summary>
        private void EditData()
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("TestRecordPrint.aspx?TestRecordId={0}", Grid1.SelectedRowID, "编辑 - ")));
        }
        #endregion

        #region 归档
        /// <summary>
        /// 右键编辑事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //protected void btnMenuFile_Click(object sender, EventArgs e)
        //{
        //    if (Grid1.SelectedRowIndexArray.Length > 0)
        //    {
        //        string values = string.Empty;
        //        foreach (int rowIndex in Grid1.SelectedRowIndexArray)
        //        {
        //            string rowID = Grid1.DataKeys[rowIndex][0].ToString();
        //            values += rowID + "|";
        //        }

        //        if (!string.IsNullOrEmpty(values) && values.Length <= 1850)
        //        {
        //            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("~/InformationProject/FileCabinetAChange.aspx?values={0}&menuId={1}", values, BLL.Const.ProjectTestRecordMenuId, "查看 - "), "归档", 600, 540));
        //        }
        //        else
        //        {
        //            Alert.ShowInTop("请一次至少一条，最多50条记录归档！", MessageBoxIcon.Warning);
        //        }

        //        BindGrid();
        //    }
        //}
        #endregion

        #region 查询事件
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            this.BindGrid();
        }

        protected void IsTemp_CheckedChanged(object sender, CheckedEventArgs e)
        {
            this.BindGrid();
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
            this.DeleteData();
        }

        /// <summary>
        /// 删除方法
        /// </summary>
        private void DeleteData()
        {
            if (Grid1.SelectedRowIndexArray.Length > 0)
            {
                foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                {
                    string rowID = Grid1.DataKeys[rowIndex][0].ToString();
                    var getV = BLL.TestRecordService.GetTestRecordById(rowID);
                    if (getV != null)
                    {
                        BLL.LogService.AddSys_Log(this.CurrUser, "考试记录", rowID, BLL.Const.ProjectTestRecordMenuId, BLL.Const.BtnDelete);
                        BLL.TestRecordService.DeleteTestRecordByTestRecordId(rowID);
                    }
                }
                BindGrid();
                ShowNotify("删除数据成功!");
            }
        }
        #endregion

        #region 导出按钮
        /// <summary>
        /// 导出事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuOut_Click(object sender, EventArgs e)
        {
            Response.ClearContent();
            string filename = Funs.GetNewFileName();
            Response.AddHeader("content-disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode("考试记录" + filename, System.Text.Encoding.UTF8) + ".xls");
            Response.ContentType = "application/excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            this.Grid1.PageSize = Grid1.RecordCount;
            BindGrid();
            Response.Write(GetGridTableHtml(Grid1));
            Response.End();
        }
        #endregion

        //protected void btnPrint_Click(object sender, EventArgs e)
        //{
        //    if (Grid1.SelectedRowIndexArray.Length == 0)
        //    {
        //        Alert.ShowInTop("请选择一条记录！", MessageBoxIcon.Warning);
        //        return;
        //    }
        //    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("TestRecordPrint.aspx?TestRecordId={0}", Grid1.SelectedRowID, "编辑 - "), "考试试卷", 900, 650));
        //}

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            //PrinterDocService.PrinterDocMethod(Const.ProjectTestRecordMenuId, Grid1.SelectedRowID, "试卷");
            string personName = string.Empty;
            string unitName = string.Empty;
            string postName = string.Empty;
            string identityCard = string.Empty;
            string newUrl = string.Empty;
            string filePath = string.Empty;
            string rootPath = Server.MapPath("~/");
            string initTemplatePath = Const.TestRecordTemplateUrl;
            string uploadfilepath = rootPath + initTemplatePath;
            Model.SGGLDB db = Funs.DB;
            var getTestRecord = TestRecordService.GetTestRecordById(Grid1.SelectedRowID);
            if (getTestRecord != null)
            {
                var getTestItems = from x in Funs.DB.Training_TestRecordItem
                                   where x.TestRecordId == Grid1.SelectedRowID
                                   select x;
                var person = db.SitePerson_Person.FirstOrDefault(x => x.PersonId == getTestRecord.TestManId);
                if (person != null)
                {
                    unitName = BLL.UnitService.GetUnitNameByUnitId(person.UnitId);
                    postName = WorkPostService.getWorkPostNamesWorkPostIds(person.WorkPostId);
                    personName = person.PersonName;
                    identityCard = person.IdentityCard;
                }
                newUrl = uploadfilepath.Replace(".doc", "-" + personName + ".doc");
                filePath = initTemplatePath.Replace(".doc", "-" + personName + ".pdf");
                File.Copy(uploadfilepath, newUrl);
                //更新书签内容
                Document doc = new Aspose.Words.Document(newUrl);
                Bookmark bookmarkProjectName = doc.Range.Bookmarks["ProjectName"];
                if (bookmarkProjectName != null)
                {
                    bookmarkProjectName.Text = "赛鼎工程有限公司" + ProjectService.GetProjectNameByProjectId(getTestRecord.ProjectId);
                }
                Bookmark bookmarkTrainName = doc.Range.Bookmarks["TrainName"];
                if (bookmarkTrainName != null)
                {
                    var getTrainTypeName = (from x in db.Training_TestPlan
                                            join z in db.Training_Plan on x.PlanId equals z.PlanId
                                            join t in db.Base_TrainType on z.TrainTypeId equals t.TrainTypeId
                                            where x.TestPlanId == getTestRecord.TestPlanId
                                            select t.TrainTypeName).FirstOrDefault();
                    bookmarkTrainName.Text = getTrainTypeName ?? "" + "培训试题";
                }
                Bookmark bookmarkUnitName = doc.Range.Bookmarks["UnitName"];
                if (bookmarkUnitName != null)
                {
                    bookmarkUnitName.Text = unitName;
                }
                Bookmark bookmarkPostName = doc.Range.Bookmarks["PostName"];
                if (bookmarkPostName != null)
                {
                    bookmarkPostName.Text = postName;
                }
                Bookmark bookmarkDate = doc.Range.Bookmarks["Date"];
                if (bookmarkDate != null)
                {
                    bookmarkDate.Text = string.Format("{0:yyyy-MM-dd}", getTestRecord.TestStartTime);
                }
                Bookmark bookmarkPersonName = doc.Range.Bookmarks["PersonName"];
                if (bookmarkPersonName != null)
                {
                    bookmarkPersonName.Text = personName;
                }
                Bookmark bookmarkIdentityCard = doc.Range.Bookmarks["IdentityCard"];
                if (bookmarkIdentityCard != null)
                {
                    bookmarkIdentityCard.Text = identityCard;
                }
                Bookmark bookmarkScores = doc.Range.Bookmarks["Scores"];
                if (bookmarkScores != null)
                {
                    bookmarkScores.Text = (getTestRecord.TestScores ?? 0).ToString();
                }
                Aspose.Words.DocumentBuilder builder = new Aspose.Words.DocumentBuilder(doc);
                bool isbool = builder.MoveToBookmark("Content");
                if (isbool)
                {
                    builder.StartTable();
                    builder.RowFormat.Alignment = Aspose.Words.Tables.RowAlignment.Center;
                    builder.CellFormat.Borders.LineStyle = LineStyle.None;
                    builder.CellFormat.Borders.Color = System.Drawing.Color.Black;
                    builder.RowFormat.LeftIndent = 5;
                    builder.Bold = false;
                    builder.RowFormat.Height = 20;
                    builder.Bold = false;
                    builder.Font.Size = 10.5;
                    builder.InsertCell();
                    builder.CellFormat.VerticalMerge = Aspose.Words.Tables.CellMerge.None;
                    builder.CellFormat.HorizontalMerge = Aspose.Words.Tables.CellMerge.First;
                    builder.CellFormat.VerticalAlignment = Aspose.Words.Tables.CellVerticalAlignment.Center;//垂直居中对齐
                    builder.ParagraphFormat.Alignment = ParagraphAlignment.Left;//水平居左对齐
                    builder.CellFormat.Width = 420;
                    builder.Write("一、单项选择题 (每题2分，共50分)");
                    builder.EndRow();
                    var getSingleItem = getTestItems.Where(x => x.TestType == "1").ToList();
                    if (getSingleItem.Count > 0)
                    {
                        int num = 1;
                        foreach (var item in getSingleItem)
                        {
                            string Avstracts = item.Abstracts.Replace("　", "").Replace(" ", "").Replace("（", "(").Replace("）", ")").Replace("()", "(" + item.SelectedItem + ")");
                            builder.InsertCell();
                            builder.CellFormat.VerticalMerge = Aspose.Words.Tables.CellMerge.None;
                            builder.CellFormat.HorizontalMerge = Aspose.Words.Tables.CellMerge.First;
                            builder.CellFormat.VerticalAlignment = Aspose.Words.Tables.CellVerticalAlignment.Center;//垂直居中对齐
                            builder.ParagraphFormat.Alignment = ParagraphAlignment.Left;//水平居左对齐
                            builder.CellFormat.Width = 420;
                            builder.Write(num + "、" + Avstracts);
                            //builder.InsertBreak(BreakType.LineBreak);
                            builder.EndRow();
                            string str = string.Empty;
                            if (!string.IsNullOrEmpty(item.AItem))
                            {
                                str += "A." + item.AItem;
                            }
                            if (!string.IsNullOrEmpty(item.BItem))
                            {
                                str += "    B." + item.BItem;
                            }
                            if (!string.IsNullOrEmpty(item.CItem))
                            {
                                str += "    C." + item.CItem;
                            }
                            if (!string.IsNullOrEmpty(item.DItem))
                            {
                                str += "    D." + item.DItem;
                            }
                            builder.InsertCell();
                            builder.CellFormat.VerticalMerge = Aspose.Words.Tables.CellMerge.None;
                            builder.CellFormat.HorizontalMerge = Aspose.Words.Tables.CellMerge.First;
                            builder.CellFormat.VerticalAlignment = Aspose.Words.Tables.CellVerticalAlignment.Center;//垂直居中对齐
                            builder.ParagraphFormat.Alignment = ParagraphAlignment.Left;//水平居左对齐
                            builder.CellFormat.Width = 420;
                            builder.Write(str);
                            builder.EndRow();
                            num++;
                        }
                    }
                    builder.InsertCell();
                    builder.CellFormat.VerticalMerge = Aspose.Words.Tables.CellMerge.None;
                    builder.CellFormat.HorizontalMerge = Aspose.Words.Tables.CellMerge.First;
                    builder.CellFormat.VerticalAlignment = Aspose.Words.Tables.CellVerticalAlignment.Center;//垂直居中对齐
                    builder.ParagraphFormat.Alignment = ParagraphAlignment.Left;//水平居左对齐
                    builder.CellFormat.Width = 420;
                    builder.Write("二、多项选择题 (每题3分，共30分)");
                    builder.EndRow();
                    var getMultipleItem = getTestItems.Where(x => x.TestType == "2").ToList();
                    if (getMultipleItem.Count > 0)
                    {
                        int num = 1;
                        foreach (var item in getMultipleItem)
                        {
                            string Avstracts = item.Abstracts.Replace("　", "").Replace(" ", "").Replace("（", "(").Replace("）", ")").Replace("()", "(" + item.SelectedItem + ")");
                            builder.InsertCell();
                            builder.CellFormat.VerticalMerge = Aspose.Words.Tables.CellMerge.None;
                            builder.CellFormat.HorizontalMerge = Aspose.Words.Tables.CellMerge.First;
                            builder.CellFormat.VerticalAlignment = Aspose.Words.Tables.CellVerticalAlignment.Center;//垂直居中对齐
                            builder.ParagraphFormat.Alignment = ParagraphAlignment.Left;//水平居左对齐
                            builder.CellFormat.Width = 420;
                            builder.Write(num + "、" + Avstracts);
                            //builder.InsertBreak(BreakType.LineBreak);
                            builder.EndRow();
                            string str = string.Empty;
                            if (!string.IsNullOrEmpty(item.AItem))
                            {
                                str += "A." + item.AItem;
                            }
                            if (!string.IsNullOrEmpty(item.BItem))
                            {
                                str += "    B." + item.BItem;
                            }
                            if (!string.IsNullOrEmpty(item.CItem))
                            {
                                str += "    C." + item.CItem;
                            }
                            if (!string.IsNullOrEmpty(item.DItem))
                            {
                                str += "    D." + item.DItem;
                            }
                            if (!string.IsNullOrEmpty(item.EItem))
                            {
                                str += "    E." + item.EItem;
                            }
                            builder.InsertCell();
                            builder.CellFormat.VerticalMerge = Aspose.Words.Tables.CellMerge.None;
                            builder.CellFormat.HorizontalMerge = Aspose.Words.Tables.CellMerge.First;
                            builder.CellFormat.VerticalAlignment = Aspose.Words.Tables.CellVerticalAlignment.Center;//垂直居中对齐
                            builder.ParagraphFormat.Alignment = ParagraphAlignment.Left;//水平居左对齐
                            builder.CellFormat.Width = 420;
                            builder.Write(str);
                            builder.EndRow();
                            num++;
                        }
                    }
                    builder.InsertCell();
                    builder.CellFormat.VerticalMerge = Aspose.Words.Tables.CellMerge.None;
                    builder.CellFormat.HorizontalMerge = Aspose.Words.Tables.CellMerge.First;
                    builder.CellFormat.VerticalAlignment = Aspose.Words.Tables.CellVerticalAlignment.Center;//垂直居中对齐
                    builder.ParagraphFormat.Alignment = ParagraphAlignment.Left;//水平居左对齐
                    builder.CellFormat.Width = 420;
                    builder.Write("三、判断题 (每题1分，共20分)");
                    builder.EndRow();
                    var getIsTrueItem = getTestItems.Where(x => x.TestType == "3").ToList();
                    if (getIsTrueItem.Count > 0)
                    {
                        int num = 1;
                        foreach (var item in getIsTrueItem)
                        {
                            var Avstracts = item.Abstracts;
                            if (Avstracts.IndexOf("(") > -1)
                            {
                                Avstracts = Avstracts.Replace("(", "（" + item.SelectedItem == "（A" ? "（√" : "（×");
                            }
                            else
                            {
                                if (Avstracts.IndexOf("（") > -1)
                                    Avstracts = Avstracts.Replace("（", "（" + item.SelectedItem == "（A" ? "（√" : "（×");
                            }
                            builder.InsertCell();
                            builder.CellFormat.VerticalMerge = Aspose.Words.Tables.CellMerge.None;
                            builder.CellFormat.HorizontalMerge = Aspose.Words.Tables.CellMerge.First;
                            builder.CellFormat.VerticalAlignment = Aspose.Words.Tables.CellVerticalAlignment.Center;//垂直居中对齐
                            builder.ParagraphFormat.Alignment = ParagraphAlignment.Left;//水平居左对齐
                            builder.CellFormat.Width = 420;
                            builder.Write(num + "、" + Avstracts);
                            builder.EndRow();
                            num++;
                        }
                    }
                }
                builder.EndTable();
                builder.MoveToBookmark("Photo");
                var attachFile = Funs.DB.AttachFile.FirstOrDefault(x => x.ToKeyId == Grid1.SelectedRowID);
                if (attachFile != null && !string.IsNullOrEmpty(attachFile.AttachUrl))
                {
                    List<string> listUrl = Funs.GetStrListByStr(attachFile.AttachUrl, ',');
                    int count = listUrl.Count();
                    if (count > 0)
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            if (!string.IsNullOrWhiteSpace(listUrl[i]))
                            {
                                string nUrl = (Funs.SGGLUrl + listUrl[i]).Replace('\\', '/');
                                //if (File.Exists(nUrl))
                                //{
                                    builder.InsertImage(nUrl, 100, 100);
                                    builder.Write("    ");
                                //}
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