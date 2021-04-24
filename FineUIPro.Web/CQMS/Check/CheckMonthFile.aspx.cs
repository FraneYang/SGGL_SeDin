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
    public partial class CheckMonthFile : PageBase
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
                this.ProjectId = this.CurrUser.LoginProjectId;
                BindGrid();
            }
        }
        #region 绑定数据
        /// <summary>
        /// 绑定数据
        /// </summary>

        public void BindGrid()
        {
            DataTable tb = ChecklistData();
            Grid1.RecordCount = tb.Rows.Count;
            tb = GetFilteredTable(Grid1.FilteredData, tb);
            var table = this.GetPagedDataTable(Grid1, tb);

            Grid1.DataSource = table;
            Grid1.DataBind();
        }
        protected DataTable ChecklistData()
        {
            string strSql = @"select C.CheckMonthId,C.ProjectId,C.Months,C.CompileDate,C.CompileMan, U.UserName from Check_CheckMonth C  left join Sys_User U on  U.UserId = C.CompileMan where 1=1";

            List<SqlParameter> listStr = new List<SqlParameter>();
            strSql += " AND C.ProjectId = @ProjectId";
            listStr.Add(new SqlParameter("@ProjectId", this.ProjectId));
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            return tb;
        }
        #endregion
        #region 操作数据

        //查看
        protected void btnMenuView_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录", MessageBoxIcon.Warning);
                return;
            }
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("EditCheckMonth.aspx?see=see&CheckMonthId={0}", Grid1.SelectedRowID, "查看 - ")));
        }


        #endregion
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            BindGrid();
        }

        protected void Window1_Close(object sender, WindowCloseEventArgs e)
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
                Model.Check_CheckMonth checkMonth = CheckMonthService.GetCheckMonth(fileId);
                Model.Project_Sys_Set CheckMonthStartDay = BLL.Project_SysSetService.GetSysSetBySetName("月报开始日期", this.CurrUser.LoginProjectId);
                Model.Project_Sys_Set CheckMonthEndDay = BLL.Project_SysSetService.GetSysSetBySetName("月报结束日期", this.CurrUser.LoginProjectId);
                DateTime startTime = Convert.ToDateTime(checkMonth.Months.Value.AddMonths(-1).Year.ToString() + "-" + checkMonth.Months.Value.AddMonths(-1).Month.ToString() + "-25");
                DateTime endTime = startTime.AddMonths(1);
                if (CheckMonthStartDay != null)
                {
                    if (CheckMonthStartDay.SetValue != "")
                    {
                        if (CheckMonthEndDay != null)
                        {
                            if (CheckMonthEndDay.SetValue != "")
                            {
                                startTime = Convert.ToDateTime(checkMonth.Months.Value.AddMonths(-1).Year.ToString() + "-" + checkMonth.Months.Value.AddMonths(-1).Month.ToString() + "-" + CheckMonthStartDay.SetValue);
                                endTime = Convert.ToDateTime(checkMonth.Months.Value.AddMonths(-1).Year.ToString() + "-" + checkMonth.Months.Value.Month.ToString() + "-" + CheckMonthEndDay.SetValue);
                            }
                            else
                            {
                                startTime = Convert.ToDateTime(checkMonth.Months.Value.AddMonths(-1).Year.ToString() + "-" + checkMonth.Months.Value.AddMonths(-1).Month.ToString() + "-" + CheckMonthStartDay.SetValue);
                                endTime = startTime.AddMonths(1);
                            }

                        }
                        else
                        {
                            startTime = Convert.ToDateTime(checkMonth.Months.Value.AddMonths(-1).Year.ToString() + "-" + checkMonth.Months.Value.AddMonths(-1).Month.ToString() + "-" + CheckMonthStartDay.SetValue);
                            endTime = startTime.AddMonths(1);
                        }

                    }
                    else
                    {
                        if (CheckMonthEndDay != null)
                        {
                            if (CheckMonthEndDay.SetValue != "")
                            {
                                startTime = Convert.ToDateTime(checkMonth.Months.Value.AddMonths(-1).Year.ToString() + "-" + checkMonth.Months.Value.AddMonths(-1).Month.ToString() + "-" + CheckMonthEndDay.SetValue);
                                endTime = startTime.AddMonths(1);
                            }
                        }
                    }
                }
                else
                {
                    if (CheckMonthEndDay != null)
                    {
                        if (CheckMonthEndDay.SetValue != null)
                        {
                            startTime = Convert.ToDateTime(checkMonth.Months.Value.AddMonths(-1).Year.ToString() + "-" + checkMonth.Months.Value.AddMonths(-1).Month.ToString() + "-" + CheckMonthEndDay.SetValue);
                            endTime = startTime.AddMonths(1);
                        }
                    }
                }

                initTemplatePath = Const.CheckMonthTemplateUrl;
                uploadfilepath = rootPath + initTemplatePath;
                newUrl = uploadfilepath.Replace(".doc", string.Format("{0:yyyy-MM}", checkMonth.Months) + ".doc");
                filePath = initTemplatePath.Replace(".doc", string.Format("{0:yyyy-MM}", checkMonth.Months) + ".pdf");
                File.Copy(uploadfilepath, newUrl);
                //更新书签内容
                Document doc = new Aspose.Words.Document(newUrl);
                Bookmark bookmarkProjectName = doc.Range.Bookmarks["ProjectName"];
                if (bookmarkProjectName != null)
                {
                    var project = ProjectService.GetProjectByProjectId(checkMonth.ProjectId);
                    if (project != null)
                    {
                        bookmarkProjectName.Text = project.ProjectName;
                    }
                }
                Bookmark bookmarkYear1 = doc.Range.Bookmarks["Year1"];
                if (bookmarkYear1 != null)
                {
                    bookmarkYear1.Text = startTime.Year.ToString();
                }
                Bookmark bookmarkMonth1 = doc.Range.Bookmarks["Month1"];
                if (bookmarkMonth1 != null)
                {
                    bookmarkMonth1.Text = startTime.Month.ToString();
                }
                Bookmark bookmarkDay1 = doc.Range.Bookmarks["Day1"];
                if (bookmarkDay1 != null)
                {
                    bookmarkDay1.Text = startTime.Day.ToString();
                }
                Bookmark bookmarkYear2 = doc.Range.Bookmarks["Year2"];
                if (bookmarkYear2 != null)
                {
                    bookmarkYear2.Text = endTime.Year.ToString();
                }
                Bookmark bookmarkMonth2 = doc.Range.Bookmarks["Month2"];
                if (bookmarkMonth2 != null)
                {
                    bookmarkMonth2.Text = endTime.Month.ToString();
                }
                Bookmark bookmarkDay2 = doc.Range.Bookmarks["Day2"];
                if (bookmarkDay2 != null)
                {
                    bookmarkDay2.Text = (endTime.Day).ToString();
                }
                Bookmark bookmarkManagementOverview = doc.Range.Bookmarks["ManagementOverview"];
                if (bookmarkManagementOverview != null)
                {
                    bookmarkManagementOverview.Text = checkMonth.ManagementOverview ?? "";
                }
                Bookmark bookmarkAccidentSituation = doc.Range.Bookmarks["AccidentSituation"];
                if (bookmarkAccidentSituation != null)
                {
                    bookmarkAccidentSituation.Text = checkMonth.AccidentSituation ?? "";
                }
                Aspose.Words.DocumentBuilder builder = new Aspose.Words.DocumentBuilder(doc);
                //质量缺陷/不合格项整改关闭情况
                var MonthRectifylist = MonthRectifyService.getListData(fileId);
                List<Model.View_Check_JointCheckDetail> checkLists = JointCheckDetailService.GetJointCheckDetailListByTime(CurrUser.LoginProjectId, startTime, endTime);
                List<Model.View_Check_JointCheckDetail> totalCheckLists = JointCheckDetailService.GetTotalJointCheckDetailListByTime(CurrUser.LoginProjectId, endTime);
                var MonthRectify1 = MonthRectifylist.FirstOrDefault(x => x.Depart == "质量监督站");
                Bookmark bookmarkThisCheck1 = doc.Range.Bookmarks["ThisCheck1"];
                if (bookmarkThisCheck1 != null)
                {
                    if (MonthRectify1 != null)
                    {
                        bookmarkThisCheck1.Text = MonthRectify1.ThisRectifyNum.ToString();
                    }
                    else
                    {
                        bookmarkThisCheck1.Text = checkLists.Where(x => x.ProposeUnitType == "11").Count().ToString();
                    }
                }
                Bookmark bookmarkThisOKCheck1 = doc.Range.Bookmarks["ThisOKCheck1"];
                if (bookmarkThisOKCheck1 != null)
                {
                    if (MonthRectify1 != null)
                    {
                        bookmarkThisOKCheck1.Text = MonthRectify1.ThisOKRectifyNum.ToString();
                    }
                    else
                    {
                        bookmarkThisOKCheck1.Text = checkLists.Where(x => x.ProposeUnitType == "11" && x.OK == 1).Count().ToString();
                    }
                }
                Bookmark bookmarkTotalCheck1 = doc.Range.Bookmarks["TotalCheck1"];
                if (bookmarkTotalCheck1 != null)
                {
                    if (MonthRectify1 != null)
                    {
                        bookmarkTotalCheck1.Text = MonthRectify1.TotalRectifyNum.ToString();
                    }
                    else
                    {
                        bookmarkTotalCheck1.Text = totalCheckLists.Where(x => x.ProposeUnitType == "11").Count().ToString();
                    }
                }
                Bookmark bookmarkTotalOKCheck1 = doc.Range.Bookmarks["TotalOKCheck1"];
                if (bookmarkTotalOKCheck1 != null)
                {
                    if (MonthRectify1 != null)
                    {
                        bookmarkTotalOKCheck1.Text = MonthRectify1.TotalOKRectifyNum.ToString();
                    }
                    else
                    {
                        bookmarkTotalOKCheck1.Text = totalCheckLists.Where(x => x.ProposeUnitType == "11" && x.OK == 1).Count().ToString();
                    }
                }
                var MonthRectify2 = MonthRectifylist.FirstOrDefault(x => x.Depart == "集团公司");
                Bookmark bookmarkThisCheck2 = doc.Range.Bookmarks["ThisCheck2"];
                if (bookmarkThisCheck2 != null)
                {
                    if (MonthRectify2 != null)
                    {
                        bookmarkThisCheck2.Text = MonthRectify2.ThisRectifyNum.ToString();
                    }
                    else
                    {
                        bookmarkThisCheck2.Text = checkLists.Where(x => x.ProposeUnitType == "8").Count().ToString();
                    }
                }
                Bookmark bookmarkThisOKCheck2 = doc.Range.Bookmarks["ThisOKCheck2"];
                if (bookmarkThisOKCheck2 != null)
                {
                    if (MonthRectify2 != null)
                    {
                        bookmarkThisOKCheck2.Text = MonthRectify2.ThisOKRectifyNum.ToString();
                    }
                    else
                    {
                        bookmarkThisOKCheck2.Text = checkLists.Where(x => x.ProposeUnitType == "8" && x.OK == 1).Count().ToString();
                    }
                }
                Bookmark bookmarkTotalCheck2 = doc.Range.Bookmarks["TotalCheck2"];
                if (bookmarkTotalCheck2 != null)
                {
                    if (MonthRectify2 != null)
                    {
                        bookmarkTotalCheck2.Text = MonthRectify2.TotalRectifyNum.ToString();
                    }
                    else
                    {
                        bookmarkTotalCheck2.Text = totalCheckLists.Where(x => x.ProposeUnitType == "8").Count().ToString();
                    }
                }
                Bookmark bookmarkTotalOKCheck2 = doc.Range.Bookmarks["TotalOKCheck2"];
                if (bookmarkTotalOKCheck2 != null)
                {
                    if (MonthRectify2 != null)
                    {
                        bookmarkTotalOKCheck2.Text = MonthRectify2.TotalOKRectifyNum.ToString();
                    }
                    else
                    {
                        bookmarkTotalOKCheck2.Text = totalCheckLists.Where(x => x.ProposeUnitType == "8" && x.OK == 1).Count().ToString();
                    }
                }
                var MonthRectify3 = MonthRectifylist.FirstOrDefault(x => x.Depart == "公司本部");
                Bookmark bookmarkThisCheck3 = doc.Range.Bookmarks["ThisCheck3"];
                if (bookmarkThisCheck3 != null)
                {
                    if (MonthRectify3 != null)
                    {
                        bookmarkThisCheck3.Text = MonthRectify3.ThisRectifyNum.ToString();
                    }
                    else
                    {
                        bookmarkThisCheck3.Text = checkLists.Where(x => x.ProposeUnitType == "10").Count().ToString();
                    }
                }
                Bookmark bookmarkThisOKCheck3 = doc.Range.Bookmarks["ThisOKCheck3"];
                if (bookmarkThisOKCheck3 != null)
                {
                    if (MonthRectify3 != null)
                    {
                        bookmarkThisOKCheck3.Text = MonthRectify3.ThisOKRectifyNum.ToString();
                    }
                    else
                    {
                        bookmarkThisOKCheck3.Text = checkLists.Where(x => x.ProposeUnitType == "10" && x.OK == 1).Count().ToString();
                    }
                }
                Bookmark bookmarkTotalCheck3 = doc.Range.Bookmarks["TotalCheck3"];
                if (bookmarkTotalCheck3 != null)
                {
                    if (MonthRectify3 != null)
                    {
                        bookmarkTotalCheck3.Text = MonthRectify3.TotalRectifyNum.ToString();
                    }
                    else
                    {
                        bookmarkTotalCheck3.Text = totalCheckLists.Where(x => x.ProposeUnitType == "10").Count().ToString();
                    }
                }
                Bookmark bookmarkTotalOKCheck3 = doc.Range.Bookmarks["TotalOKCheck3"];
                if (bookmarkTotalOKCheck3 != null)
                {
                    if (MonthRectify3 != null)
                    {
                        bookmarkTotalOKCheck3.Text = MonthRectify3.TotalOKRectifyNum.ToString();
                    }
                    else
                    {
                        bookmarkTotalOKCheck3.Text = totalCheckLists.Where(x => x.ProposeUnitType == "10" && x.OK == 1).Count().ToString();
                    }
                }
                var MonthRectify4 = MonthRectifylist.FirstOrDefault(x => x.Depart == "建设单位");
                Bookmark bookmarkThisCheck4 = doc.Range.Bookmarks["ThisCheck4"];
                if (bookmarkThisCheck4 != null)
                {
                    if (MonthRectify4 != null)
                    {
                        bookmarkThisCheck4.Text = MonthRectify4.ThisRectifyNum.ToString();
                    }
                    else
                    {
                        bookmarkThisCheck4.Text = checkLists.Where(x => x.ProposeUnitType == "1").Count().ToString();
                    }
                }
                Bookmark bookmarkThisOKCheck4 = doc.Range.Bookmarks["ThisOKCheck4"];
                if (bookmarkThisOKCheck4 != null)
                {
                    if (MonthRectify4 != null)
                    {
                        bookmarkThisOKCheck4.Text = MonthRectify4.ThisOKRectifyNum.ToString();
                    }
                    else
                    {
                        bookmarkThisOKCheck4.Text = checkLists.Where(x => x.ProposeUnitType == "1" && x.OK == 1).Count().ToString();
                    }
                }
                Bookmark bookmarkTotalCheck4 = doc.Range.Bookmarks["TotalCheck4"];
                if (bookmarkTotalCheck4 != null)
                {
                    if (MonthRectify4 != null)
                    {
                        bookmarkTotalCheck4.Text = MonthRectify4.TotalRectifyNum.ToString();
                    }
                    else
                    {
                        bookmarkTotalCheck4.Text = totalCheckLists.Where(x => x.ProposeUnitType == "1").Count().ToString();
                    }
                }
                Bookmark bookmarkTotalOKCheck4 = doc.Range.Bookmarks["TotalOKCheck4"];
                if (bookmarkTotalOKCheck4 != null)
                {
                    if (MonthRectify4 != null)
                    {
                        bookmarkTotalOKCheck4.Text = MonthRectify4.TotalOKRectifyNum.ToString();
                    }
                    else
                    {
                        bookmarkTotalOKCheck4.Text = totalCheckLists.Where(x => x.ProposeUnitType == "1" && x.OK == 1).Count().ToString();
                    }
                }
                var MonthRectify5 = MonthRectifylist.FirstOrDefault(x => x.Depart == "监理单位");
                Bookmark bookmarkThisCheck5 = doc.Range.Bookmarks["ThisCheck5"];
                if (bookmarkThisCheck5 != null)
                {
                    if (MonthRectify5 != null)
                    {
                        bookmarkThisCheck5.Text = MonthRectify5.ThisRectifyNum.ToString();
                    }
                    else
                    {
                        bookmarkThisCheck5.Text = checkLists.Where(x => x.ProposeUnitType == "2").Count().ToString();
                    }
                }
                Bookmark bookmarkThisOKCheck5 = doc.Range.Bookmarks["ThisOKCheck5"];
                if (bookmarkThisOKCheck5 != null)
                {
                    if (MonthRectify5 != null)
                    {
                        bookmarkThisOKCheck5.Text = MonthRectify5.ThisOKRectifyNum.ToString();
                    }
                    else
                    {
                        bookmarkThisOKCheck5.Text = checkLists.Where(x => x.ProposeUnitType == "2" && x.OK == 1).Count().ToString();
                    }
                }
                Bookmark bookmarkTotalCheck5 = doc.Range.Bookmarks["TotalCheck5"];
                if (bookmarkTotalCheck5 != null)
                {
                    if (MonthRectify5 != null)
                    {
                        bookmarkTotalCheck5.Text = MonthRectify5.TotalRectifyNum.ToString();
                    }
                    else
                    {
                        bookmarkTotalCheck5.Text = totalCheckLists.Where(x => x.ProposeUnitType == "2").Count().ToString();
                    }
                }
                Bookmark bookmarkTotalOKCheck5 = doc.Range.Bookmarks["TotalOKCheck5"];
                if (bookmarkTotalOKCheck5 != null)
                {
                    if (MonthRectify5 != null)
                    {
                        bookmarkTotalOKCheck5.Text = MonthRectify5.TotalOKRectifyNum.ToString();
                    }
                    else
                    {
                        bookmarkTotalOKCheck5.Text = totalCheckLists.Where(x => x.ProposeUnitType == "2" && x.OK == 1).Count().ToString();
                    }
                }
                var MonthRectify6 = MonthRectifylist.FirstOrDefault(x => x.Depart == "总承包商项目部");
                Bookmark bookmarkThisCheck6 = doc.Range.Bookmarks["ThisCheck6"];
                if (bookmarkThisCheck6 != null)
                {
                    if (MonthRectify6 != null)
                    {
                        bookmarkThisCheck6.Text = MonthRectify6.ThisRectifyNum.ToString();
                    }
                    else
                    {
                        bookmarkThisCheck6.Text = checkLists.Where(x => x.ProposeUnitType == "5").Count().ToString();
                    }
                }
                Bookmark bookmarkThisOKCheck6 = doc.Range.Bookmarks["ThisOKCheck6"];
                if (bookmarkThisOKCheck6 != null)
                {
                    if (MonthRectify6 != null)
                    {
                        bookmarkThisOKCheck6.Text = MonthRectify6.ThisOKRectifyNum.ToString();
                    }
                    else
                    {
                        bookmarkThisOKCheck6.Text = checkLists.Where(x => x.ProposeUnitType == "5" && x.OK == 1).Count().ToString();
                    }
                }
                Bookmark bookmarkTotalCheck6 = doc.Range.Bookmarks["TotalCheck6"];
                if (bookmarkTotalCheck6 != null)
                {
                    if (MonthRectify6 != null)
                    {
                        bookmarkTotalCheck6.Text = MonthRectify6.TotalRectifyNum.ToString();
                    }
                    else
                    {
                        bookmarkTotalCheck6.Text = totalCheckLists.Where(x => x.ProposeUnitType == "5").Count().ToString();
                    }
                }
                Bookmark bookmarkTotalOKCheck6 = doc.Range.Bookmarks["TotalOKCheck6"];
                if (bookmarkTotalOKCheck6 != null)
                {
                    if (MonthRectify6 != null)
                    {
                        bookmarkTotalOKCheck6.Text = MonthRectify6.TotalOKRectifyNum.ToString();
                    }
                    else
                    {
                        bookmarkTotalOKCheck6.Text = totalCheckLists.Where(x => x.ProposeUnitType == "5" && x.OK == 1).Count().ToString();
                    }
                }
                //无损检测情况
                builder.MoveToBookmark("Table2");
                builder.StartTable();
                builder.RowFormat.Alignment = Aspose.Words.Tables.RowAlignment.Center;
                builder.CellFormat.Borders.LineStyle = LineStyle.Single;
                builder.CellFormat.Borders.Color = System.Drawing.Color.Black;
                builder.RowFormat.LeftIndent = 5;
                builder.Bold = false;
                builder.RowFormat.Height = 20;
                builder.Bold = false;
                List<Model.Base_Unit> units = UnitService.GetUnitByProjectIdUnitTypeList(CurrUser.LoginProjectId, BLL.Const.ProjectUnitType_2);
                var ndtList = BLL.MonthNDTCheckService.getListData(fileId);
                foreach (Model.Base_Unit unit in units)
                {
                    var ndt = ndtList.FirstOrDefault(x => x.UnitId == unit.UnitId);
                    //施工分包商
                    builder.InsertCell();
                    builder.CellFormat.VerticalMerge = Aspose.Words.Tables.CellMerge.None;
                    builder.CellFormat.HorizontalMerge = Aspose.Words.Tables.CellMerge.First;
                    builder.CellFormat.VerticalAlignment = Aspose.Words.Tables.CellVerticalAlignment.Center;//垂直居中对齐
                    builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;//水平居中对齐
                    builder.CellFormat.Width = 102;
                    builder.Write(unit.UnitName);
                    //拍片总数
                    builder.InsertCell();
                    builder.CellFormat.VerticalMerge = Aspose.Words.Tables.CellMerge.None;
                    builder.CellFormat.HorizontalMerge = Aspose.Words.Tables.CellMerge.First;
                    builder.CellFormat.VerticalAlignment = Aspose.Words.Tables.CellVerticalAlignment.Center;//垂直居中对齐
                    builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;//水平居中对齐
                    builder.CellFormat.Width = 47;
                    if (ndt != null)
                    {
                        builder.Write(ndt.FilmNum.ToString());
                    }
                    else
                    {
                        builder.Write("0");
                    }
                    //不合格（张）
                    builder.InsertCell();
                    builder.CellFormat.VerticalMerge = Aspose.Words.Tables.CellMerge.None;
                    builder.CellFormat.HorizontalMerge = Aspose.Words.Tables.CellMerge.First;
                    builder.CellFormat.VerticalAlignment = Aspose.Words.Tables.CellVerticalAlignment.Center;//垂直居中对齐
                    builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;//水平居中对齐
                    builder.CellFormat.Width = 56;
                    if (ndt != null)
                    {
                        builder.Write(ndt.NotOKFileNum.ToString());
                    }
                    else
                    {
                        builder.Write("0");
                    }
                    //已返修（张）
                    builder.InsertCell();
                    builder.CellFormat.VerticalMerge = Aspose.Words.Tables.CellMerge.None;
                    builder.CellFormat.HorizontalMerge = Aspose.Words.Tables.CellMerge.First;
                    builder.CellFormat.VerticalAlignment = Aspose.Words.Tables.CellVerticalAlignment.Center;//垂直居中对齐
                    builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;//水平居中对齐
                    builder.CellFormat.Width = 54;
                    if (ndt != null)
                    {
                        builder.Write(ndt.RepairFileNum.ToString());
                    }
                    else
                    {
                        builder.Write("0");
                    }
                    //一次合格率
                    builder.InsertCell();
                    builder.CellFormat.VerticalMerge = Aspose.Words.Tables.CellMerge.None;
                    builder.CellFormat.HorizontalMerge = Aspose.Words.Tables.CellMerge.First;
                    builder.CellFormat.VerticalAlignment = Aspose.Words.Tables.CellVerticalAlignment.Center;//垂直居中对齐
                    builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;//水平居中对齐
                    builder.CellFormat.Width = 55;
                    if (ndt != null)
                    {
                        builder.Write(ndt.OneOKRate);
                    }
                    else
                    {
                        builder.Write("0");
                    }
                    //累计拍片总数
                    builder.InsertCell();
                    builder.CellFormat.VerticalMerge = Aspose.Words.Tables.CellMerge.None;
                    builder.CellFormat.HorizontalMerge = Aspose.Words.Tables.CellMerge.First;
                    builder.CellFormat.VerticalAlignment = Aspose.Words.Tables.CellVerticalAlignment.Center;//垂直居中对齐
                    builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;//水平居中对齐
                    builder.CellFormat.Width = 48;
                    if (ndt != null)
                    {
                        builder.Write(ndt.TotalFilmNum.ToString());
                    }
                    else
                    {
                        builder.Write("0");
                    }
                    //累计不合格（张）
                    builder.InsertCell();
                    builder.CellFormat.VerticalMerge = Aspose.Words.Tables.CellMerge.None;
                    builder.CellFormat.HorizontalMerge = Aspose.Words.Tables.CellMerge.First;
                    builder.CellFormat.VerticalAlignment = Aspose.Words.Tables.CellVerticalAlignment.Center;//垂直居中对齐
                    builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;//水平居中对齐
                    builder.CellFormat.Width = 63;
                    if (ndt != null)
                    {
                        builder.Write(ndt.TotalNotOKFileNum.ToString());
                    }
                    else
                    {
                        builder.Write("0");
                    }
                    //累计一次合格率
                    builder.InsertCell();
                    builder.CellFormat.VerticalMerge = Aspose.Words.Tables.CellMerge.None;
                    builder.CellFormat.HorizontalMerge = Aspose.Words.Tables.CellMerge.First;
                    builder.CellFormat.VerticalAlignment = Aspose.Words.Tables.CellVerticalAlignment.Center;//垂直居中对齐
                    builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;//水平居中对齐
                    builder.CellFormat.Width = 63;
                    if (ndt != null)
                    {
                        builder.Write(ndt.TotalOneOKRate);
                    }
                    else
                    {
                        builder.Write("0");
                    }
                    builder.EndRow();
                }
                //合计
                builder.InsertCell();
                builder.CellFormat.VerticalMerge = Aspose.Words.Tables.CellMerge.None;
                builder.CellFormat.HorizontalMerge = Aspose.Words.Tables.CellMerge.First;
                builder.CellFormat.VerticalAlignment = Aspose.Words.Tables.CellVerticalAlignment.Center;//垂直居中对齐
                builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;//水平居中对齐
                builder.CellFormat.Width = 102;
                builder.Write("合计");
                //拍片总数
                builder.InsertCell();
                builder.CellFormat.VerticalMerge = Aspose.Words.Tables.CellMerge.None;
                builder.CellFormat.HorizontalMerge = Aspose.Words.Tables.CellMerge.First;
                builder.CellFormat.VerticalAlignment = Aspose.Words.Tables.CellVerticalAlignment.Center;//垂直居中对齐
                builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;//水平居中对齐
                builder.CellFormat.Width = 47;
                int sumFilmNum = ndtList.Sum(x => x.FilmNum ?? 0);
                builder.Write(sumFilmNum.ToString());
                //不合格（张）
                builder.InsertCell();
                builder.CellFormat.VerticalMerge = Aspose.Words.Tables.CellMerge.None;
                builder.CellFormat.HorizontalMerge = Aspose.Words.Tables.CellMerge.First;
                builder.CellFormat.VerticalAlignment = Aspose.Words.Tables.CellVerticalAlignment.Center;//垂直居中对齐
                builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;//水平居中对齐
                builder.CellFormat.Width = 56;
                int sumNotOKFileNum = ndtList.Sum(x => x.NotOKFileNum ?? 0);
                builder.Write(sumNotOKFileNum.ToString());
                //已返修（张）
                builder.InsertCell();
                builder.CellFormat.VerticalMerge = Aspose.Words.Tables.CellMerge.None;
                builder.CellFormat.HorizontalMerge = Aspose.Words.Tables.CellMerge.First;
                builder.CellFormat.VerticalAlignment = Aspose.Words.Tables.CellVerticalAlignment.Center;//垂直居中对齐
                builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;//水平居中对齐
                builder.CellFormat.Width = 54;
                int sumRepairFileNum = ndtList.Sum(x => x.RepairFileNum ?? 0);
                builder.Write(sumRepairFileNum.ToString());
                //一次合格率
                builder.InsertCell();
                builder.CellFormat.VerticalMerge = Aspose.Words.Tables.CellMerge.None;
                builder.CellFormat.HorizontalMerge = Aspose.Words.Tables.CellMerge.First;
                builder.CellFormat.VerticalAlignment = Aspose.Words.Tables.CellVerticalAlignment.Center;//垂直居中对齐
                builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;//水平居中对齐
                builder.CellFormat.Width = 55;
                string sumOneOKRate = "0";
                if (sumFilmNum > 0)
                {
                    var a = Convert.ToDouble(sumFilmNum - sumNotOKFileNum);
                    var b = Convert.ToDouble(sumFilmNum);
                    decimal result = decimal.Round(decimal.Parse((a / b * 100).ToString()), 2);
                    sumOneOKRate = result.ToString() + "%";
                }
                builder.Write(sumOneOKRate);
                //累计拍片总数
                builder.InsertCell();
                builder.CellFormat.VerticalMerge = Aspose.Words.Tables.CellMerge.None;
                builder.CellFormat.HorizontalMerge = Aspose.Words.Tables.CellMerge.First;
                builder.CellFormat.VerticalAlignment = Aspose.Words.Tables.CellVerticalAlignment.Center;//垂直居中对齐
                builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;//水平居中对齐
                builder.CellFormat.Width = 48;
                int sumTotalFilmNum = ndtList.Sum(x => x.TotalFilmNum ?? 0);
                builder.Write(sumTotalFilmNum.ToString());
                //累计不合格（张）
                builder.InsertCell();
                builder.CellFormat.VerticalMerge = Aspose.Words.Tables.CellMerge.None;
                builder.CellFormat.HorizontalMerge = Aspose.Words.Tables.CellMerge.First;
                builder.CellFormat.VerticalAlignment = Aspose.Words.Tables.CellVerticalAlignment.Center;//垂直居中对齐
                builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;//水平居中对齐
                builder.CellFormat.Width = 63;
                int sumTotalNotOKFileNum = ndtList.Sum(x => x.TotalNotOKFileNum ?? 0);
                builder.Write(sumTotalNotOKFileNum.ToString());
                //累计一次合格率
                builder.InsertCell();
                builder.CellFormat.VerticalMerge = Aspose.Words.Tables.CellMerge.None;
                builder.CellFormat.HorizontalMerge = Aspose.Words.Tables.CellMerge.First;
                builder.CellFormat.VerticalAlignment = Aspose.Words.Tables.CellVerticalAlignment.Center;//垂直居中对齐
                builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;//水平居中对齐
                builder.CellFormat.Width = 63;
                string sumTotalOneOKRate = "0";
                if (sumTotalFilmNum > 0)
                {
                    var a = Convert.ToDouble(sumTotalFilmNum - sumTotalNotOKFileNum);
                    var b = Convert.ToDouble(sumTotalFilmNum);
                    decimal result = decimal.Round(decimal.Parse((a / b * 100).ToString()), 2);
                    sumOneOKRate = result.ToString() + "%";
                }
                builder.Write(sumTotalOneOKRate);
                builder.EndRow();
                builder.EndTable();
                //焊工资格评定情况
                builder.MoveToBookmark("Table3");
                builder.StartTable();
                builder.RowFormat.Alignment = Aspose.Words.Tables.RowAlignment.Center;
                builder.CellFormat.Borders.LineStyle = LineStyle.Single;
                builder.CellFormat.Borders.Color = System.Drawing.Color.Black;
                builder.RowFormat.LeftIndent = 5;
                builder.Bold = false;
                builder.RowFormat.Height = 20;
                builder.Bold = false;
                var welderList = BLL.MonthWelderService.getListData(fileId);
                foreach (Model.Base_Unit unit in units)
                {
                    var welder = welderList.FirstOrDefault(x => x.UnitId == unit.UnitId);
                    //施工分包商
                    builder.InsertCell();
                    builder.CellFormat.VerticalMerge = Aspose.Words.Tables.CellMerge.None;
                    builder.CellFormat.HorizontalMerge = Aspose.Words.Tables.CellMerge.First;
                    builder.CellFormat.VerticalAlignment = Aspose.Words.Tables.CellVerticalAlignment.Center;//垂直居中对齐
                    builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;//水平居中对齐
                    builder.CellFormat.Width = 102;
                    builder.Write(unit.UnitName);
                    //总人数
                    builder.InsertCell();
                    builder.CellFormat.VerticalMerge = Aspose.Words.Tables.CellMerge.None;
                    builder.CellFormat.HorizontalMerge = Aspose.Words.Tables.CellMerge.First;
                    builder.CellFormat.VerticalAlignment = Aspose.Words.Tables.CellVerticalAlignment.Center;//垂直居中对齐
                    builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;//水平居中对齐
                    builder.CellFormat.Width = 51;
                    if (welder != null)
                    {
                        builder.Write(welder.ThisPersonNum.ToString());
                    }
                    else
                    {
                        builder.Write("0");
                    }
                    //合格人数
                    builder.InsertCell();
                    builder.CellFormat.VerticalMerge = Aspose.Words.Tables.CellMerge.None;
                    builder.CellFormat.HorizontalMerge = Aspose.Words.Tables.CellMerge.First;
                    builder.CellFormat.VerticalAlignment = Aspose.Words.Tables.CellVerticalAlignment.Center;//垂直居中对齐
                    builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;//水平居中对齐
                    builder.CellFormat.Width = 72;
                    if (welder != null)
                    {
                        builder.Write(welder.ThisOKPersonNum.ToString());
                    }
                    else
                    {
                        builder.Write("0");
                    }
                    //合格率
                    builder.InsertCell();
                    builder.CellFormat.VerticalMerge = Aspose.Words.Tables.CellMerge.None;
                    builder.CellFormat.HorizontalMerge = Aspose.Words.Tables.CellMerge.First;
                    builder.CellFormat.VerticalAlignment = Aspose.Words.Tables.CellVerticalAlignment.Center;//垂直居中对齐
                    builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;//水平居中对齐
                    builder.CellFormat.Width = 71;
                    if (welder != null)
                    {
                        builder.Write(welder.ThisOKRate);
                    }
                    else
                    {
                        builder.Write("0");
                    }
                    //总人数
                    builder.InsertCell();
                    builder.CellFormat.VerticalMerge = Aspose.Words.Tables.CellMerge.None;
                    builder.CellFormat.HorizontalMerge = Aspose.Words.Tables.CellMerge.First;
                    builder.CellFormat.VerticalAlignment = Aspose.Words.Tables.CellVerticalAlignment.Center;//垂直居中对齐
                    builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;//水平居中对齐
                    builder.CellFormat.Width = 53;
                    if (welder != null)
                    {
                        builder.Write(welder.TotalPersonNum.ToString());
                    }
                    else
                    {
                        builder.Write("0");
                    }
                    //合格人数
                    builder.InsertCell();
                    builder.CellFormat.VerticalMerge = Aspose.Words.Tables.CellMerge.None;
                    builder.CellFormat.HorizontalMerge = Aspose.Words.Tables.CellMerge.First;
                    builder.CellFormat.VerticalAlignment = Aspose.Words.Tables.CellVerticalAlignment.Center;//垂直居中对齐
                    builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;//水平居中对齐
                    builder.CellFormat.Width = 77;
                    if (welder != null)
                    {
                        builder.Write(welder.TotalOKPersonNum.ToString());
                    }
                    else
                    {
                        builder.Write("0");
                    }
                    //合格率
                    builder.InsertCell();
                    builder.CellFormat.VerticalMerge = Aspose.Words.Tables.CellMerge.None;
                    builder.CellFormat.HorizontalMerge = Aspose.Words.Tables.CellMerge.First;
                    builder.CellFormat.VerticalAlignment = Aspose.Words.Tables.CellVerticalAlignment.Center;//垂直居中对齐
                    builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;//水平居中对齐
                    builder.CellFormat.Width = 62;
                    if (welder != null)
                    {
                        builder.Write(welder.TotalOKRate);
                    }
                    else
                    {
                        builder.Write("0");
                    }
                    builder.EndRow();
                }
                //合计
                builder.InsertCell();
                builder.CellFormat.VerticalMerge = Aspose.Words.Tables.CellMerge.None;
                builder.CellFormat.HorizontalMerge = Aspose.Words.Tables.CellMerge.First;
                builder.CellFormat.VerticalAlignment = Aspose.Words.Tables.CellVerticalAlignment.Center;//垂直居中对齐
                builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;//水平居中对齐
                builder.CellFormat.Width = 102;
                builder.Write("合计");
                //总人数
                builder.InsertCell();
                builder.CellFormat.VerticalMerge = Aspose.Words.Tables.CellMerge.None;
                builder.CellFormat.HorizontalMerge = Aspose.Words.Tables.CellMerge.First;
                builder.CellFormat.VerticalAlignment = Aspose.Words.Tables.CellVerticalAlignment.Center;//垂直居中对齐
                builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;//水平居中对齐
                builder.CellFormat.Width = 51;
                int sumThisPersonNum = welderList.Sum(x => x.ThisPersonNum ?? 0);
                builder.Write(sumThisPersonNum.ToString());
                //合格人数
                builder.InsertCell();
                builder.CellFormat.VerticalMerge = Aspose.Words.Tables.CellMerge.None;
                builder.CellFormat.HorizontalMerge = Aspose.Words.Tables.CellMerge.First;
                builder.CellFormat.VerticalAlignment = Aspose.Words.Tables.CellVerticalAlignment.Center;//垂直居中对齐
                builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;//水平居中对齐
                builder.CellFormat.Width = 72;
                int sumThisOKPersonNum = welderList.Sum(x => x.ThisOKPersonNum ?? 0);
                builder.Write(sumThisOKPersonNum.ToString());
                //合格率
                builder.InsertCell();
                builder.CellFormat.VerticalMerge = Aspose.Words.Tables.CellMerge.None;
                builder.CellFormat.HorizontalMerge = Aspose.Words.Tables.CellMerge.First;
                builder.CellFormat.VerticalAlignment = Aspose.Words.Tables.CellVerticalAlignment.Center;//垂直居中对齐
                builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;//水平居中对齐
                builder.CellFormat.Width = 71;
                string sumThisOKRate = "0";
                if (sumThisPersonNum > 0)
                {
                    var a = Convert.ToDouble(sumThisOKPersonNum);
                    var b = Convert.ToDouble(sumThisPersonNum);
                    decimal result = decimal.Round(decimal.Parse((a / b * 100).ToString()), 2);
                    sumThisOKRate = result.ToString() + "%";
                }
                builder.Write(sumThisOKRate);
                //总人数
                builder.InsertCell();
                builder.CellFormat.VerticalMerge = Aspose.Words.Tables.CellMerge.None;
                builder.CellFormat.HorizontalMerge = Aspose.Words.Tables.CellMerge.First;
                builder.CellFormat.VerticalAlignment = Aspose.Words.Tables.CellVerticalAlignment.Center;//垂直居中对齐
                builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;//水平居中对齐
                builder.CellFormat.Width = 53;
                int sumTotalPersonNum = welderList.Sum(x => x.TotalPersonNum ?? 0);
                builder.Write(sumTotalPersonNum.ToString());
                //合格人数
                builder.InsertCell();
                builder.CellFormat.VerticalMerge = Aspose.Words.Tables.CellMerge.None;
                builder.CellFormat.HorizontalMerge = Aspose.Words.Tables.CellMerge.First;
                builder.CellFormat.VerticalAlignment = Aspose.Words.Tables.CellVerticalAlignment.Center;//垂直居中对齐
                builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;//水平居中对齐
                builder.CellFormat.Width = 77;
                int sumTotalOKPersonNum = welderList.Sum(x => x.TotalOKPersonNum ?? 0);
                builder.Write(sumTotalOKPersonNum.ToString());
                //合格率
                builder.InsertCell();
                builder.CellFormat.VerticalMerge = Aspose.Words.Tables.CellMerge.None;
                builder.CellFormat.HorizontalMerge = Aspose.Words.Tables.CellMerge.First;
                builder.CellFormat.VerticalAlignment = Aspose.Words.Tables.CellVerticalAlignment.Center;//垂直居中对齐
                builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;//水平居中对齐
                builder.CellFormat.Width = 62;
                string sumTotalOKRate = "0";
                if (sumTotalPersonNum > 0)
                {
                    var a = Convert.ToDouble(sumTotalOKPersonNum);
                    var b = Convert.ToDouble(sumTotalPersonNum);
                    decimal result = decimal.Round(decimal.Parse((a / b * 100).ToString()), 2);
                    sumTotalOKRate = result.ToString() + "%";
                }
                builder.Write(sumTotalOKRate);
                builder.EndRow();
                builder.EndTable();
                //质量验收情况
                var spotCheckDetails = MonthSpotCheckDetailService.GetMonthSpotCheckDetailsByCheckMonthId(fileId);
                if (spotCheckDetails.Count > 0)
                {
                    Bookmark bookmarkTotalNum1 = doc.Range.Bookmarks["TotalNum1"];
                    if (bookmarkTotalNum1 != null)
                    {
                        bookmarkTotalNum1.Text = spotCheckDetails[0].TotalNum ?? "";
                    }
                    Bookmark bookmarkThisOKNum1 = doc.Range.Bookmarks["ThisOKNum1"];
                    if (bookmarkThisOKNum1 != null)
                    {
                        bookmarkThisOKNum1.Text = spotCheckDetails[0].ThisOKNum ?? "";
                    }
                    Bookmark bookmarkTotalOKNum1 = doc.Range.Bookmarks["TotalOKNum1"];
                    if (bookmarkTotalOKNum1 != null)
                    {
                        bookmarkTotalOKNum1.Text = spotCheckDetails[0].TotalOKNum ?? "";
                    }
                    Bookmark bookmarkTotalOKRate1 = doc.Range.Bookmarks["TotalOKRate1"];
                    if (bookmarkTotalOKNum1 != null)
                    {
                        bookmarkTotalOKRate1.Text = spotCheckDetails[0].TotalOKRate ?? "";
                    }
                    Bookmark bookmarkTotalNum2 = doc.Range.Bookmarks["TotalNum2"];
                    if (bookmarkTotalNum2 != null)
                    {
                        bookmarkTotalNum2.Text = spotCheckDetails[1].TotalNum ?? "";
                    }
                    Bookmark bookmarkThisOKNum2 = doc.Range.Bookmarks["ThisOKNum2"];
                    if (bookmarkThisOKNum2 != null)
                    {
                        bookmarkThisOKNum2.Text = spotCheckDetails[1].ThisOKNum ?? "";
                    }
                    Bookmark bookmarkTotalOKNum2 = doc.Range.Bookmarks["TotalOKNum2"];
                    if (bookmarkTotalOKNum2 != null)
                    {
                        bookmarkTotalOKNum2.Text = spotCheckDetails[1].TotalOKNum ?? "";
                    }
                    Bookmark bookmarkTotalOKRate2 = doc.Range.Bookmarks["TotalOKRate2"];
                    if (bookmarkTotalOKNum2 != null)
                    {
                        bookmarkTotalOKRate2.Text = spotCheckDetails[1].TotalOKRate;
                    }
                    Bookmark bookmarkTotalNum3 = doc.Range.Bookmarks["TotalNum3"];
                    if (bookmarkTotalNum3 != null)
                    {
                        bookmarkTotalNum3.Text = spotCheckDetails[2].TotalNum;
                    }
                    Bookmark bookmarkThisOKNum3 = doc.Range.Bookmarks["ThisOKNum3"];
                    if (bookmarkThisOKNum3 != null)
                    {
                        bookmarkThisOKNum3.Text = spotCheckDetails[2].ThisOKNum;
                    }
                    Bookmark bookmarkTotalOKNum3 = doc.Range.Bookmarks["TotalOKNum3"];
                    if (bookmarkTotalOKNum3 != null)
                    {
                        bookmarkTotalOKNum3.Text = spotCheckDetails[2].TotalOKNum;
                    }
                    Bookmark bookmarkTotalOKRate3 = doc.Range.Bookmarks["TotalOKRate3"];
                    if (bookmarkTotalOKNum3 != null)
                    {
                        bookmarkTotalOKRate3.Text = spotCheckDetails[2].TotalOKRate;
                    }
                    Bookmark bookmarkTotalNum4 = doc.Range.Bookmarks["TotalNum4"];
                    if (bookmarkTotalNum4 != null)
                    {
                        bookmarkTotalNum4.Text = spotCheckDetails[3].TotalNum;
                    }
                    Bookmark bookmarkThisOKNum4 = doc.Range.Bookmarks["ThisOKNum4"];
                    if (bookmarkThisOKNum4 != null)
                    {
                        bookmarkThisOKNum4.Text = spotCheckDetails[3].ThisOKNum;
                    }
                    Bookmark bookmarkTotalOKNum4 = doc.Range.Bookmarks["TotalOKNum4"];
                    if (bookmarkTotalOKNum4 != null)
                    {
                        bookmarkTotalOKNum4.Text = spotCheckDetails[3].TotalOKNum;
                    }
                    Bookmark bookmarkTotalOKRate4 = doc.Range.Bookmarks["TotalOKRate4"];
                    if (bookmarkTotalOKNum4 != null)
                    {
                        bookmarkTotalOKRate4.Text = spotCheckDetails[3].TotalOKRate;
                    }
                }
                List<Model.View_Check_SoptCheckDetail> spotCheckDetailOKLists = SpotCheckDetailService.GetOKSpotCheckDetailListByTime1(CurrUser.LoginProjectId, startTime, endTime);
                List<Model.View_Check_SoptCheckDetail> spotCheckDetailLists = SpotCheckDetailService.GetAllSpotCheckDetailListByTime(CurrUser.LoginProjectId, startTime, endTime);
                List<Model.View_Check_SoptCheckDetail> TotalCheckDetailOKLists = SpotCheckDetailService.GetTotalOKSpotCheckDetailListByTime1(CurrUser.LoginProjectId, endTime);
                List<Model.View_Check_SoptCheckDetail> TotalCheckDetailLists = SpotCheckDetailService.GetTotalAllSpotCheckDetailListByTime(CurrUser.LoginProjectId, endTime);
                //质量记录本月同步率
                List<Model.View_Check_SoptCheckDetail> spotCheckDetailDataOKLists = SpotCheckDetailService.GetMonthDataOkSpotCheckDetailListByTime(CurrUser.LoginProjectId, startTime, endTime);
                //质量记录累计同步率
                List<Model.View_Check_SoptCheckDetail> TotalCheckDetailDataOKLists = SpotCheckDetailService.GetAllDataOkSpotCheckDetailListByTime(CurrUser.LoginProjectId, endTime);
                Bookmark bookmarkMonthOkqualificationRate = doc.Range.Bookmarks["MonthOkqualificationRate"];
                if (bookmarkMonthOkqualificationRate != null)
                {
                    decimal result = 12.30M;
                    if (!string.IsNullOrEmpty(checkMonth.MonthOk))
                    {
                        bookmarkMonthOkqualificationRate.Text = checkMonth.MonthOk;
                    }
                    else
                    {
                        if (spotCheckDetailOKLists.Count > 0 && spotCheckDetailLists.Count > 0)
                        {
                            var a = Convert.ToDouble(spotCheckDetailOKLists.Count);
                            var b = Convert.ToDouble(spotCheckDetailLists.Count);
                            result = decimal.Round(decimal.Parse((a / b * 100).ToString()), 2);
                            bookmarkMonthOkqualificationRate.Text = result.ToString() + "%" ?? "";
                        }
                    }
                }
                Bookmark bookmarkOkqualificationRate = doc.Range.Bookmarks["OkqualificationRate"];
                if (bookmarkOkqualificationRate != null)
                {
                    decimal result = 12.30M;
                    if (!string.IsNullOrEmpty(checkMonth.AllOk))
                    {
                        bookmarkOkqualificationRate.Text = checkMonth.AllOk;
                    }
                    else
                    {
                        if (TotalCheckDetailOKLists.Count > 0 && TotalCheckDetailLists.Count > 0)
                        {
                            var a = Convert.ToDouble(TotalCheckDetailOKLists.Count);
                            var b = Convert.ToDouble(TotalCheckDetailLists.Count);
                            result = decimal.Round(decimal.Parse((a / b * 100).ToString()), 2);
                            bookmarkOkqualificationRate.Text = result.ToString() + "%" ?? "";
                        }
                    }
                }
                Bookmark bookmarkMonthDataOkqualificationRate = doc.Range.Bookmarks["MonthDataOkqualificationRate"];
                if (bookmarkMonthDataOkqualificationRate != null)
                {
                    decimal result = 12.30M;
                    if (!string.IsNullOrEmpty(checkMonth.MonthDataOk))
                    {
                        bookmarkMonthDataOkqualificationRate.Text = checkMonth.MonthDataOk;
                    }
                    else
                    {
                        if (spotCheckDetailOKLists.Count > 0 && spotCheckDetailDataOKLists.Count > 0)
                        {
                            var a = Convert.ToDouble(spotCheckDetailDataOKLists.Count);
                            var b = Convert.ToDouble(spotCheckDetailOKLists.Where(x => x.IsDataOK != "2").ToList().Count);
                            result = decimal.Round(decimal.Parse((a / b * 100).ToString()), 2);
                            bookmarkMonthDataOkqualificationRate.Text = result.ToString() + "%" ?? "";
                        }
                    }
                }
                Bookmark bookmarkDataOkqualificationRate = doc.Range.Bookmarks["DataOkqualificationRate"];
                if (bookmarkDataOkqualificationRate != null)
                {
                    decimal result = 12.30M;
                    if (!string.IsNullOrEmpty(checkMonth.AllDataOk))
                    {
                        bookmarkDataOkqualificationRate.Text = checkMonth.AllDataOk;
                    }
                    else
                    {
                        if (TotalCheckDetailOKLists.Count > 0 && TotalCheckDetailDataOKLists.Count > 0)
                        {
                            var a = Convert.ToDouble(TotalCheckDetailDataOKLists.Count);
                            var b = Convert.ToDouble(TotalCheckDetailOKLists.Where(x => x.IsDataOK != "2").ToList().Count);
                            result = decimal.Round(decimal.Parse((a / b * 100).ToString()), 2);
                            bookmarkDataOkqualificationRate.Text = result.ToString() + "%" ?? "";
                        }
                    }
                }

                //特种设备安装告知、监检情况
                var specialEquipmentDetails = SpecialEquipmentDetailService.GetList(fileId);
                if (specialEquipmentDetails.Count > 0)
                {
                    Bookmark bookmarkETotalNum1 = doc.Range.Bookmarks["ETotalNum1"];
                    if (bookmarkETotalNum1 != null)
                    {
                        bookmarkETotalNum1.Text = specialEquipmentDetails[0].TotalNum;
                    }
                    Bookmark bookmarkThisCompleteNum1 = doc.Range.Bookmarks["ThisCompleteNum1"];
                    if (bookmarkThisCompleteNum1 != null)
                    {
                        bookmarkThisCompleteNum1.Text = specialEquipmentDetails[0].ThisCompleteNum1;
                    }
                    Bookmark bookmarkTotalCompleteNum1 = doc.Range.Bookmarks["TotalCompleteNum1"];
                    if (bookmarkTotalCompleteNum1 != null)
                    {
                        bookmarkTotalCompleteNum1.Text = specialEquipmentDetails[0].TotalCompleteNum1;
                    }
                    Bookmark bookmarkTotalRate1 = doc.Range.Bookmarks["TotalRate1"];
                    if (bookmarkTotalRate1 != null)
                    {
                        bookmarkTotalRate1.Text = specialEquipmentDetails[0].TotalRate1;
                    }
                    Bookmark bookmarkThisCompleteNum6 = doc.Range.Bookmarks["ThisCompleteNum6"];
                    if (bookmarkThisCompleteNum6 != null)
                    {
                        bookmarkThisCompleteNum6.Text = specialEquipmentDetails[0].ThisCompleteNum2;
                    }
                    Bookmark bookmarkTotalCompleteNum6 = doc.Range.Bookmarks["TotalCompleteNum6"];
                    if (bookmarkTotalCompleteNum6 != null)
                    {
                        bookmarkTotalCompleteNum6.Text = specialEquipmentDetails[0].TotalCompleteNum2;
                    }
                    Bookmark bookmarkTotalRate6 = doc.Range.Bookmarks["TotalRate6"];
                    if (bookmarkTotalRate6 != null)
                    {
                        bookmarkTotalRate6.Text = specialEquipmentDetails[0].TotalRate2;
                    }
                    Bookmark bookmarkETotalNum2 = doc.Range.Bookmarks["ETotalNum2"];
                    if (bookmarkETotalNum2 != null)
                    {
                        bookmarkETotalNum2.Text = specialEquipmentDetails[1].TotalNum;
                    }
                    Bookmark bookmarkThisCompleteNum2 = doc.Range.Bookmarks["ThisCompleteNum2"];
                    if (bookmarkThisCompleteNum2 != null)
                    {
                        bookmarkThisCompleteNum2.Text = specialEquipmentDetails[1].ThisCompleteNum1;
                    }
                    Bookmark bookmarkTotalCompleteNum2 = doc.Range.Bookmarks["TotalCompleteNum2"];
                    if (bookmarkTotalCompleteNum2 != null)
                    {
                        bookmarkTotalCompleteNum2.Text = specialEquipmentDetails[1].TotalCompleteNum1;
                    }
                    Bookmark bookmarkTotalRate2 = doc.Range.Bookmarks["TotalRate2"];
                    if (bookmarkTotalRate2 != null)
                    {
                        bookmarkTotalRate2.Text = specialEquipmentDetails[1].TotalRate1;
                    }
                    Bookmark bookmarkThisCompleteNum7 = doc.Range.Bookmarks["ThisCompleteNum7"];
                    if (bookmarkThisCompleteNum7 != null)
                    {
                        bookmarkThisCompleteNum7.Text = specialEquipmentDetails[1].ThisCompleteNum2;
                    }
                    Bookmark bookmarkTotalCompleteNum7 = doc.Range.Bookmarks["TotalCompleteNum7"];
                    if (bookmarkTotalCompleteNum7 != null)
                    {
                        bookmarkTotalCompleteNum7.Text = specialEquipmentDetails[1].TotalCompleteNum2;
                    }
                    Bookmark bookmarkTotalRate7 = doc.Range.Bookmarks["TotalRate7"];
                    if (bookmarkTotalRate7 != null)
                    {
                        bookmarkTotalRate7.Text = specialEquipmentDetails[1].TotalRate2;
                    }
                    Bookmark bookmarkETotalNum3 = doc.Range.Bookmarks["ETotalNum3"];
                    if (bookmarkETotalNum3 != null)
                    {
                        bookmarkETotalNum3.Text = specialEquipmentDetails[2].TotalNum;
                    }
                    Bookmark bookmarkThisCompleteNum3 = doc.Range.Bookmarks["ThisCompleteNum3"];
                    if (bookmarkThisCompleteNum3 != null)
                    {
                        bookmarkThisCompleteNum3.Text = specialEquipmentDetails[2].ThisCompleteNum1;
                    }
                    Bookmark bookmarkTotalCompleteNum3 = doc.Range.Bookmarks["TotalCompleteNum3"];
                    if (bookmarkTotalCompleteNum3 != null)
                    {
                        bookmarkTotalCompleteNum3.Text = specialEquipmentDetails[2].TotalCompleteNum1;
                    }
                    Bookmark bookmarkTotalRate3 = doc.Range.Bookmarks["TotalRate3"];
                    if (bookmarkTotalRate3 != null)
                    {
                        bookmarkTotalRate3.Text = specialEquipmentDetails[2].TotalRate1;
                    }
                    Bookmark bookmarkThisCompleteNum8 = doc.Range.Bookmarks["ThisCompleteNum8"];
                    if (bookmarkThisCompleteNum8 != null)
                    {
                        bookmarkThisCompleteNum8.Text = specialEquipmentDetails[2].ThisCompleteNum2;
                    }
                    Bookmark bookmarkTotalCompleteNum8 = doc.Range.Bookmarks["TotalCompleteNum8"];
                    if (bookmarkTotalCompleteNum8 != null)
                    {
                        bookmarkTotalCompleteNum8.Text = specialEquipmentDetails[2].TotalCompleteNum2;
                    }
                    Bookmark bookmarkTotalRate8 = doc.Range.Bookmarks["TotalRate8"];
                    if (bookmarkTotalRate8 != null)
                    {
                        bookmarkTotalRate8.Text = specialEquipmentDetails[2].TotalRate2;
                    }
                    Bookmark bookmarkETotalNum4 = doc.Range.Bookmarks["ETotalNum4"];
                    if (bookmarkETotalNum4 != null)
                    {
                        bookmarkETotalNum4.Text = specialEquipmentDetails[3].TotalNum;
                    }
                    Bookmark bookmarkThisCompleteNum4 = doc.Range.Bookmarks["ThisCompleteNum4"];
                    if (bookmarkThisCompleteNum4 != null)
                    {
                        bookmarkThisCompleteNum4.Text = specialEquipmentDetails[3].ThisCompleteNum1;
                    }
                    Bookmark bookmarkTotalCompleteNum4 = doc.Range.Bookmarks["TotalCompleteNum4"];
                    if (bookmarkTotalCompleteNum4 != null)
                    {
                        bookmarkTotalCompleteNum4.Text = specialEquipmentDetails[3].TotalCompleteNum1;
                    }
                    Bookmark bookmarkTotalRate4 = doc.Range.Bookmarks["TotalRate4"];
                    if (bookmarkTotalRate4 != null)
                    {
                        bookmarkTotalRate4.Text = specialEquipmentDetails[3].TotalRate1;
                    }
                    Bookmark bookmarkThisCompleteNum9 = doc.Range.Bookmarks["ThisCompleteNum9"];
                    if (bookmarkThisCompleteNum9 != null)
                    {
                        bookmarkThisCompleteNum9.Text = specialEquipmentDetails[3].ThisCompleteNum2;
                    }
                    Bookmark bookmarkTotalCompleteNum9 = doc.Range.Bookmarks["TotalCompleteNum9"];
                    if (bookmarkTotalCompleteNum9 != null)
                    {
                        bookmarkTotalCompleteNum9.Text = specialEquipmentDetails[3].TotalCompleteNum2;
                    }
                    Bookmark bookmarkTotalRate9 = doc.Range.Bookmarks["TotalRate9"];
                    if (bookmarkTotalRate9 != null)
                    {
                        bookmarkTotalRate9.Text = specialEquipmentDetails[3].TotalRate2;
                    }
                    Bookmark bookmarkETotalNum5 = doc.Range.Bookmarks["ETotalNum5"];
                    if (bookmarkETotalNum5 != null)
                    {
                        bookmarkETotalNum5.Text = specialEquipmentDetails[4].TotalNum;
                    }
                    Bookmark bookmarkThisCompleteNum5 = doc.Range.Bookmarks["ThisCompleteNum5"];
                    if (bookmarkThisCompleteNum5 != null)
                    {
                        bookmarkThisCompleteNum5.Text = specialEquipmentDetails[4].ThisCompleteNum1;
                    }
                    Bookmark bookmarkTotalCompleteNum5 = doc.Range.Bookmarks["TotalCompleteNum5"];
                    if (bookmarkTotalCompleteNum5 != null)
                    {
                        bookmarkTotalCompleteNum5.Text = specialEquipmentDetails[4].TotalCompleteNum1;
                    }
                    Bookmark bookmarkTotalRate5 = doc.Range.Bookmarks["TotalRate5"];
                    if (bookmarkTotalRate5 != null)
                    {
                        bookmarkTotalRate5.Text = specialEquipmentDetails[4].TotalRate1;
                    }
                    Bookmark bookmarkThisCompleteNum10 = doc.Range.Bookmarks["ThisCompleteNum10"];
                    if (bookmarkThisCompleteNum10 != null)
                    {
                        bookmarkThisCompleteNum10.Text = specialEquipmentDetails[4].ThisCompleteNum2;
                    }
                    Bookmark bookmarkTotalCompleteNum10 = doc.Range.Bookmarks["TotalCompleteNum10"];
                    if (bookmarkTotalCompleteNum10 != null)
                    {
                        bookmarkTotalCompleteNum10.Text = specialEquipmentDetails[4].TotalCompleteNum2;
                    }
                    Bookmark bookmarkTotalRate10 = doc.Range.Bookmarks["TotalRate10"];
                    if (bookmarkTotalRate10 != null)
                    {
                        bookmarkTotalRate10.Text = specialEquipmentDetails[4].TotalRate2;
                    }
                }

                //设计变更情况
                builder.MoveToBookmark("Table4");
                builder.StartTable();
                builder.RowFormat.Alignment = Aspose.Words.Tables.RowAlignment.Center;
                builder.CellFormat.Borders.LineStyle = LineStyle.Single;
                builder.CellFormat.Borders.Color = System.Drawing.Color.Black;
                builder.RowFormat.LeftIndent = 5;
                builder.Bold = false;
                builder.RowFormat.Height = 20;
                builder.Bold = false;
                var designs = from x in Funs.DB.Check_Design
                              where x.ProjectId == CurrUser.LoginProjectId && x.DesignDate >= startTime && x.DesignDate < endTime
                              select x;
                int num = 1;
                string mainItemName = string.Empty;
                string CNProfessionalName = string.Empty;
                string state = string.Empty;
                var designList = BLL.MonthDesignService.getListData(fileId);
                if (designList.Count > 0)
                {
                    foreach (Model.Check_MonthDesign design in designList)
                    {
                        //序号
                        builder.InsertCell();
                        builder.CellFormat.VerticalMerge = Aspose.Words.Tables.CellMerge.None;
                        builder.CellFormat.HorizontalMerge = Aspose.Words.Tables.CellMerge.First;
                        builder.CellFormat.VerticalAlignment = Aspose.Words.Tables.CellVerticalAlignment.Center;//垂直居中对齐
                        builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;//水平居中对齐
                        builder.CellFormat.Width = 42;
                        builder.Write(num.ToString());
                        //编号
                        builder.InsertCell();
                        builder.CellFormat.VerticalMerge = Aspose.Words.Tables.CellMerge.None;
                        builder.CellFormat.HorizontalMerge = Aspose.Words.Tables.CellMerge.First;
                        builder.CellFormat.VerticalAlignment = Aspose.Words.Tables.CellVerticalAlignment.Center;//垂直居中对齐
                        builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;//水平居中对齐
                        builder.CellFormat.Width = 132;
                        builder.Write(design.DesignCode);
                        //主项
                        builder.InsertCell();
                        builder.CellFormat.VerticalMerge = Aspose.Words.Tables.CellMerge.None;
                        builder.CellFormat.HorizontalMerge = Aspose.Words.Tables.CellMerge.First;
                        builder.CellFormat.VerticalAlignment = Aspose.Words.Tables.CellVerticalAlignment.Center;//垂直居中对齐
                        builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;//水平居中对齐
                        builder.CellFormat.Width = 67;
                        if (!string.IsNullOrEmpty(design.MainItemId))
                        {
                            var mainItem = MainItemService.GetMainItemByMainItemId(design.MainItemId);
                            if (mainItem != null)
                            {
                                mainItemName = mainItem.MainItemName;
                            }
                        }

                        builder.Write(mainItemName);
                        //专业
                        builder.InsertCell();
                        builder.CellFormat.VerticalMerge = Aspose.Words.Tables.CellMerge.None;
                        builder.CellFormat.HorizontalMerge = Aspose.Words.Tables.CellMerge.First;
                        builder.CellFormat.VerticalAlignment = Aspose.Words.Tables.CellVerticalAlignment.Center;//垂直居中对齐
                        builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;//水平居中对齐
                        builder.CellFormat.Width = 67;
                        var cn = DesignProfessionalService.GetDesignProfessional(design.DesignProfessionalId);
                        if (cn != null)
                        {
                            CNProfessionalName = cn.ProfessionalName;
                        }
                        builder.Write(CNProfessionalName);
                        //施工完成情况
                        builder.InsertCell();
                        builder.CellFormat.VerticalMerge = Aspose.Words.Tables.CellMerge.None;
                        builder.CellFormat.HorizontalMerge = Aspose.Words.Tables.CellMerge.First;
                        builder.CellFormat.VerticalAlignment = Aspose.Words.Tables.CellVerticalAlignment.Center;//垂直居中对齐
                        builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;//水平居中对齐
                        builder.CellFormat.Width = 112;
                        builder.Write(design.State);
                        //备注
                        builder.InsertCell();
                        builder.CellFormat.VerticalMerge = Aspose.Words.Tables.CellMerge.None;
                        builder.CellFormat.HorizontalMerge = Aspose.Words.Tables.CellMerge.First;
                        builder.CellFormat.VerticalAlignment = Aspose.Words.Tables.CellVerticalAlignment.Center;//垂直居中对齐
                        builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;//水平居中对齐
                        builder.CellFormat.Width = 68;
                        builder.Write(design.Remark);
                        builder.EndRow();
                        num++;
                    }
                }
                else
                {
                    foreach (Model.Check_Design design in designs)
                    {
                        //序号
                        builder.InsertCell();
                        builder.CellFormat.VerticalMerge = Aspose.Words.Tables.CellMerge.None;
                        builder.CellFormat.HorizontalMerge = Aspose.Words.Tables.CellMerge.First;
                        builder.CellFormat.VerticalAlignment = Aspose.Words.Tables.CellVerticalAlignment.Center;//垂直居中对齐
                        builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;//水平居中对齐
                        builder.CellFormat.Width = 42;
                        builder.Write(num.ToString());
                        //编号
                        builder.InsertCell();
                        builder.CellFormat.VerticalMerge = Aspose.Words.Tables.CellMerge.None;
                        builder.CellFormat.HorizontalMerge = Aspose.Words.Tables.CellMerge.First;
                        builder.CellFormat.VerticalAlignment = Aspose.Words.Tables.CellVerticalAlignment.Center;//垂直居中对齐
                        builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;//水平居中对齐
                        builder.CellFormat.Width = 132;
                        builder.Write(design.DesignCode);
                        //主项
                        builder.InsertCell();
                        builder.CellFormat.VerticalMerge = Aspose.Words.Tables.CellMerge.None;
                        builder.CellFormat.HorizontalMerge = Aspose.Words.Tables.CellMerge.First;
                        builder.CellFormat.VerticalAlignment = Aspose.Words.Tables.CellVerticalAlignment.Center;//垂直居中对齐
                        builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;//水平居中对齐
                        builder.CellFormat.Width = 67;
                        if (!string.IsNullOrEmpty(design.MainItemId))
                        {
                            var mainItem = MainItemService.GetMainItemByMainItemId(design.MainItemId);
                            if (mainItem != null)
                            {
                                mainItemName = mainItem.MainItemName;
                            }
                        }

                        builder.Write(mainItemName);
                        //专业
                        builder.InsertCell();
                        builder.CellFormat.VerticalMerge = Aspose.Words.Tables.CellMerge.None;
                        builder.CellFormat.HorizontalMerge = Aspose.Words.Tables.CellMerge.First;
                        builder.CellFormat.VerticalAlignment = Aspose.Words.Tables.CellVerticalAlignment.Center;//垂直居中对齐
                        builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;//水平居中对齐
                        builder.CellFormat.Width = 67;
                        var cn = DesignProfessionalService.GetDesignProfessional(design.CNProfessionalCode);
                        if (cn != null)
                        {
                            CNProfessionalName = cn.ProfessionalName;
                        }
                        builder.Write(CNProfessionalName);
                        //施工完成情况
                        builder.InsertCell();
                        builder.CellFormat.VerticalMerge = Aspose.Words.Tables.CellMerge.None;
                        builder.CellFormat.HorizontalMerge = Aspose.Words.Tables.CellMerge.First;
                        builder.CellFormat.VerticalAlignment = Aspose.Words.Tables.CellVerticalAlignment.Center;//垂直居中对齐
                        builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;//水平居中对齐
                        builder.CellFormat.Width = 112;
                        if (design.State == Const.Design_Complete)
                        {
                            state = "已完成";
                        }
                        else
                        {
                            state = "未完成";
                        }
                        builder.Write(state);
                        //备注
                        builder.InsertCell();
                        builder.CellFormat.VerticalMerge = Aspose.Words.Tables.CellMerge.None;
                        builder.CellFormat.HorizontalMerge = Aspose.Words.Tables.CellMerge.First;
                        builder.CellFormat.VerticalAlignment = Aspose.Words.Tables.CellVerticalAlignment.Center;//垂直居中对齐
                        builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;//水平居中对齐
                        builder.CellFormat.Width = 68;
                        builder.Write(string.Empty);
                        builder.EndRow();
                        num++;
                    }
                }
                builder.EndTable();
                Bookmark bookmarkConstructionData = doc.Range.Bookmarks["ConstructionData"];
                if (bookmarkConstructionData != null)
                {
                    if (!string.IsNullOrEmpty(checkMonth.ConstructionData))
                    {
                        bookmarkConstructionData.Text = checkMonth.ConstructionData;
                    }

                }
                Bookmark bookmarkNextMonthPlan = doc.Range.Bookmarks["NextMonthPlan"];
                if (bookmarkNextMonthPlan != null)
                {
                    if (!string.IsNullOrEmpty(checkMonth.NextMonthPlan))
                    {
                        bookmarkNextMonthPlan.Text = checkMonth.NextMonthPlan;
                    }

                }
                Bookmark bookmarkNeedSolved = doc.Range.Bookmarks["NeedSolved"];
                if (bookmarkNeedSolved != null)
                {
                    if (!string.IsNullOrEmpty(checkMonth.NeedSolved))
                    {
                        bookmarkNeedSolved.Text = checkMonth.NeedSolved;
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
                string menuId = Const.CheckMonthMenuId;
                PageContext.RegisterStartupScript(Windowtt.GetShowReference(
                 String.Format("../../AttachFile/webuploader.aspx?type=-1&source=1&toKeyId={0}&path=FileUpload/CheckControl&menuId={1}", fileId, menuId)));
            }
        }
    }
}