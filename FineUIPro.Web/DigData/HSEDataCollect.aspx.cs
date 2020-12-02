using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace FineUIPro.Web.DigData
{
    public partial class HSEDataCollect : PageBase
    {
        #region 加载
        /// <summary>
        /// 加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ConstValue.InitConstValueDropDownList(this.drpYear, BLL.ConstValue.Group_0008, false);
                this.drpYear.SelectedValue = DateTime.Now.Year.ToString();
                var getHSEDataCollect = HSEDataCollectService.GetHSEDataCollectByYear(DateTime.Now.Year);
                if (getHSEDataCollect == null)
                {
                    HSEDataCollectService.CreateHSEDataCollectByYear(DateTime.Now.Year);
                }
                // 绑定表格
                this.BindGrid();
                this.BindGrid2();
                if (this.CurrUser.UserId == Const.sysglyId)
                {
                    this.btnRefresh.Hidden = false;
                }
            }
        }

        #endregion

        #region 绑定数据Grid1
        /// <summary>
        /// 绑定数据Grid1
        /// </summary>
        private void BindGrid()
        {
            string strSql = @"SELECT HSEDataCollectItemId,HSEDataCollectId,SortIndex,HSEContent,MeasureUnit,Month1,Month2,Month3,Month2,Month5,Month6,Month7,Month8,Month9,Month10,Month11,Month12
                                        ,(CASE WHEN SortIndex=7 or SortIndex=20 or SortIndex=36 or SortIndex=37 
                                        THEN CAST((ISNULL(CAST(Month1 AS decimal(18,2)),0)+ISNULL(CAST(Month2 AS decimal(18,2)),0)+ISNULL(CAST(Month3 AS decimal(18,2)),0)+ISNULL(CAST(Month2 AS decimal(18,2)),0)+ISNULL(CAST(Month5 AS decimal(18,2)),0)
                                        +ISNULL(CAST(Month6 AS decimal(18,2)),0)+ISNULL(CAST(Month7 AS decimal(18,2)),0)+ISNULL(CAST(Month8 AS decimal(18,2)),0)+ISNULL(CAST(Month9 AS decimal(18,2)),0)+ISNULL(CAST(Month10 AS decimal(18,2)),0)
                                        +ISNULL(CAST(Month11 AS decimal(18,2)),0)+ISNULL(CAST(Month12 AS decimal(18,2)),0)) AS NVARCHAR(50))
                                        ELSE  CAST((ISNULL(CAST(Month1 AS int),0)+ISNULL(CAST(Month2 AS int),0)+ISNULL(CAST(Month3 AS int),0)+ISNULL(CAST(Month2 AS int),0)+ISNULL(CAST(Month5 AS int),0)
                                        +ISNULL(CAST(Month6 AS int),0)+ISNULL(CAST(Month7 AS int),0)+ISNULL(CAST(Month8 AS int),0)+ISNULL(CAST(Month9 AS int),0)+ISNULL(CAST(Month10 AS int),0)
                                        +ISNULL(CAST(Month11 AS int),0)+ISNULL(CAST(Month12 AS int),0)) AS NVARCHAR(50))
                                        END) AS MonthSum
                                    FROM dbo.DigData_HSEDataCollectItem WHERE 1=1";
            List<SqlParameter> listStr = new List<SqlParameter>();
            if (this.drpYear.SelectedValue != BLL.Const._Null)
            {
                strSql += " AND Year = @Year";
                listStr.Add(new SqlParameter("@Year", Funs.GetNewInt(this.drpYear.SelectedValue)));
            }

            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            Grid1.RecordCount = tb.Rows.Count;
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();
        }
        #endregion

        #region 绑定数据Grid2
        /// <summary>
        /// 绑定数据Grid2
        /// </summary>
        private void BindGrid2()
        {
            string strSql = @"SELECT HSEDataCollectSubmissionId,HSEDataCollectId,Year,C.ProjectId,P.ProjectCode,P.ProjectName, Month1,Month2,Month3,Month4,Month5,Month6,Month7,Month8,Month9,Month10,Month11,Month12
                                FROM dbo.DigData_HSEDataCollectSubmission AS C 
                                LEFT JOIN Base_Project AS P ON C.ProjectId=P.ProjectId
                                WHERE 1=1 ";
            List<SqlParameter> listStr = new List<SqlParameter>();
            if (this.drpYear.SelectedValue != BLL.Const._Null)
            {
                strSql += " AND Year = @Year";
                listStr.Add(new SqlParameter("@Year", Funs.GetNewInt(this.drpYear.SelectedValue)));
            }
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            Grid2.RecordCount = tb.Rows.Count;
            var table = this.GetPagedDataTable(Grid2, tb);
            Grid2.DataSource = table;
            Grid2.DataBind();
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
            int year = Funs.GetNewInt(this.drpYear.SelectedValue) ?? DateTime.Now.Year;
            var getHSEDataCollect = HSEDataCollectService.GetHSEDataCollectByYear(year);
            if (getHSEDataCollect == null)
            {
                HSEDataCollectService.CreateHSEDataCollectByYear(year);
            }
            this.BindGrid();
            this.BindGrid2();
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            int year = Funs.GetNewInt(this.drpYear.SelectedValue) ?? DateTime.Now.Year;
            var getSeDin_MonthReports = from x in Funs.DB.SeDin_MonthReport
                                        where x.States == Const.State_3 && x.ReporMonth.Value.Year == year
                                        select x;
            if (getSeDin_MonthReports.Count() > 0)
            {
                foreach (var item in getSeDin_MonthReports)
                {
                    BLL.HSEDataCollectService.SaveHSEDataCollectItem(item);
                }
            }
            this.BindGrid();
            this.BindGrid2();
            ShowNotify("刷新完成！", MessageBoxIcon.Success);
        }

        protected void Grid2_RowCommand(object sender, GridCommandEventArgs e)
        {
            int? year = Funs.GetNewInt(this.drpYear.SelectedValue);
            int? month = Funs.GetNewInt(e.CommandName);
            if (year.HasValue && month.HasValue)
            {
                var getSubmission = Funs.DB.DigData_HSEDataCollectSubmission.FirstOrDefault(x => x.HSEDataCollectSubmissionId == e.RowID);
                if (getSubmission != null)
                {
                    var getMont = Funs.DB.SeDin_MonthReport.FirstOrDefault(x => x.ReporMonth.Value.Year == year.Value && x.ReporMonth.Value.Month == month.Value && x.ProjectId == getSubmission.ProjectId);
                    if (getMont != null)
                    {
                        //PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("../HSSE/Manager/ManagerMonth_SeDinEdit.aspx?MonthReportId={0}&projectId={1}", getMont.MonthReportId, getMont.ProjectId, "查看 - ")));
                        PrinterDocService.PrinterDocMethod(Const.ProjectManagerMonth_SeDinMenuId, getMont.MonthReportId, "安全月报");
                    }
                }
            }
            else
            {
                Alert.ShowInParent("当前月报不存在！", MessageBoxIcon.Warning);
                return;
            }
        }
    }
}