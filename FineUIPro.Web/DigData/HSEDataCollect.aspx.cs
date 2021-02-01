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
            var getData = Funs.DB.Sp_DigData_HSEDataCollect(Funs.GetNewIntOrZero(this.drpYear.SelectedValue));
            DataTable tb = GetTreeDataTable(getData.ToList()); //this.LINQToDataTable(getData.ToList());
            Grid1.RecordCount = tb.Rows.Count;
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();
        }

        /// <summary>
        /// 获取模拟树表格
        /// </summary>
        /// <returns></returns>
        public DataTable GetTreeDataTable(List<Model.DigDataHSEDataCollectItem> getData)
        {
            DataTable table = new DataTable();
            table.Columns.Add(new DataColumn("HSEDataCollectItemId", typeof(string)));
            table.Columns.Add(new DataColumn("ParentId", typeof(string)));
            table.Columns.Add(new DataColumn("SortIndex", typeof(int)));
            table.Columns.Add(new DataColumn("HSEContent", typeof(string)));
            table.Columns.Add(new DataColumn("MeasureUnit", typeof(string)));
            table.Columns.Add(new DataColumn("Month1", typeof(string)));
            table.Columns.Add(new DataColumn("Month2", typeof(string)));
            table.Columns.Add(new DataColumn("Month3", typeof(string)));
            table.Columns.Add(new DataColumn("Month4", typeof(string)));
            table.Columns.Add(new DataColumn("Month5", typeof(string)));
            table.Columns.Add(new DataColumn("Month6", typeof(string)));
            table.Columns.Add(new DataColumn("Month7", typeof(string)));
            table.Columns.Add(new DataColumn("Month8", typeof(string)));
            table.Columns.Add(new DataColumn("Month9", typeof(string)));
            table.Columns.Add(new DataColumn("Month10", typeof(string)));
            table.Columns.Add(new DataColumn("Month11", typeof(string)));
            table.Columns.Add(new DataColumn("Month12", typeof(string)));
            table.Columns.Add(new DataColumn("MonthSum", typeof(string)));
            DataRow row;
            for (int i = 1; i <= 11; i++)
            {
                row = table.NewRow();
                row[0] = i.ToString();
                row[1] = "-1";
                row[2] = i;
                row[3] = getSupHSEContent(i);
                table.Rows.Add(row);
            }

            foreach (var item in getData)
            {
                row = table.NewRow();
                row[0] = item.HSEDataCollectItemId;
                row[1] = getSupID(item.SortIndex);
                row[2] = item.SortIndex;
                row[3] = item.HSEContent;
                row[4] = item.MeasureUnit;
                row[5] = item.Month1;
                row[6] = item.Month2;
                row[7] = item.Month3;
                row[8] = item.Month4;
                row[9] = item.Month5;
                row[10] = item.Month6;
                row[11] = item.Month7;
                row[12] = item.Month8;
                row[13] = item.Month9;
                row[14] = item.Month10;
                row[15] = item.Month11;
                row[16] = item.Month12;
                row[17] = item.MonthSum;
                table.Rows.Add(row);          
            }
            return table;
        }

        #region 获取父级名称
        /// <summary>
        /// 获取父级名称
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        private string getSupHSEContent(int i)
        {
            string name = string.Empty;
            switch (i)
            {
                case 1:
                    name= "人员信息";
                    break;
                case 2:
                    name = "安全人工时";
                    break;
                case 3:
                    name = "事故信息";
                    break;
                case 4:
                    name = "施工机具设备";
                    break;
                case 5:
                    name = "安全生产费用";
                    break;
                case 6:
                    name = "安全培训信息";
                    break;
                case 7:
                    name = "安全会议信息";
                    break;
                case 8:
                    name = "安全检查信息";
                    break;
                case 9:
                    name = "安全奖惩信息";
                    break;
                case 10:
                    name = "危大工程信息";
                    break;
                case 11:
                    name = "应急信息";
                    break;

                default:
                    name="";
                    break;
            }
            return name;
        }
        #endregion
        #region 获取父级ID
        /// <summary>
        /// 获取父级名称
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        private string getSupID(int sortIndex)
        {
            string supId = "-1";
            if (sortIndex <= 6)
            {
                supId = "1";
            }
            else if (sortIndex == 7)
            {
                supId = "2";
            }
            else if (sortIndex >= 8  && sortIndex <= 18 )
            {
                supId = "3";
            }
            else if (sortIndex == 19)
            {
                supId = "4";
            }
            else if (sortIndex == 20)                
            {
                supId = "5";
            }
            else if (sortIndex == 21)
            {
                supId = "6";
            }
            else if (sortIndex >= 22 && sortIndex <= 24)
            {
                supId = "7";
            }
            else if (sortIndex >= 25 && sortIndex <= 35)
            {
                supId = "8";
            }
            else if (sortIndex >= 36 && sortIndex <= 37)
            {
                supId = "9";
            }
            else if (sortIndex >= 38 && sortIndex <= 39)
            {
                supId = "10";
            }
            else if (sortIndex >= 40)
            {
                supId = "11";
            }
            return supId;
        }
        #endregion
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