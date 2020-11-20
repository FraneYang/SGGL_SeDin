using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace FineUIPro.Web.PersonManage.Test
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
                if (this.CurrUser.UserId == Const.sysglyId)
                {
                    this.btnMenuDelete.Hidden = false;
                }
                // 绑定表格
                BindGrid();
                GetDataService.UpdateServerTestPlanStates();
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
            string strSql = @"SELECT TestRecord.TestRecordId,TestRecord.TestPlanId, TestRecord.TestManId,TestRecord.TestStartTime,TestRecord.TestEndTime, TestRecord.TestScores,TestPlan.PlanName, 
                        ISNULL(TestPlan.Duration,90) AS Duration,ISNULL(TestRecord.TotalScore,100) AS TotalScore,TestPlan.TestPalce,ISNULL(TestRecord.QuestionCount,95) AS QuestionCount,TestManName,Unit.UnitName
                         FROM dbo.Test_TestRecord AS TestRecord
                         LEFT JOIN dbo.Test_TestPlan AS TestPlan ON TestPlan.TestPlanId=TestRecord.TestPlanId
                         LEFT JOIN dbo.Base_Unit AS Unit ON TestRecord.UnitId=Unit.UnitId
                         WHERE 1=1";
            List<SqlParameter> listStr = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(this.txtName.Text.Trim()))
            {
                strSql += " AND (TestRecord.TestManName LIKE @name OR TestPlan.PlanName LIKE @name)";
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

            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("~/HSSE/EduTrain/TestRecordPrint.aspx?TestRecordId={0}&type=1", Grid1.SelectedRowID, "编辑 - "), "考试试卷", 1200, 600));
        }
        #endregion

        #region 归档
        /// <summary>
        /// 右键编辑事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuFile_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length > 0)
            {
                string values = string.Empty;
                foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                {
                    string rowID = Grid1.DataKeys[rowIndex][0].ToString();
                    values += rowID + "|";
                }

                if (!string.IsNullOrEmpty(values) && values.Length <= 1850)
                {
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("~/HSSE/InformationProject/FileCabinetAChange.aspx?values={0}&menuId={1}", values, BLL.Const.ProjectTestRecordMenuId, "查看 - "), "归档", 600, 540));
                }
                else
                {
                    Alert.ShowInTop("请一次至少一条，最多50条记录归档！", MessageBoxIcon.Warning);
                }

                BindGrid();
            }
        }
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
                    var getV = BLL.ServerTestRecordService.GetTestRecordById(rowID);
                    if (getV != null)
                    {
                        BLL.LogService.AddSys_Log(this.CurrUser, "考试记录", rowID, BLL.Const.ProjectTestRecordMenuId, BLL.Const.BtnDelete);
                        BLL.ServerTestRecordService.DeleteTestRecordByTestRecordId(rowID);
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

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("~/HSSE/EduTrain/TestRecordPrint.aspx?TestRecordId={0}&type=1", Grid1.SelectedRowID, "编辑 - "), "考试试卷", 900, 650));
        }
    }
}