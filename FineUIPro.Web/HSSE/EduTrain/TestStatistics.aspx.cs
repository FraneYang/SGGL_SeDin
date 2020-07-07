using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace FineUIPro.Web.HSSE.EduTrain
{
    public partial class TestStatistics : PageBase
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
                this.ddlPageSize.SelectedValue = Grid1.PageSize.ToString();
                // 绑定表格
                this.BindGrid();
            }
            else
            {
                if (GetRequestEventArgument() == "reloadGrid")
                {
                    BindGrid();
                }
            }
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            string strSql = @"SELECT PersonId,CardNo,PersonName,person.UnitId,Unit.UnitCode,Unit.UnitName,person.DepartId,Depart.DepartCode,Depart.DepartName"
                        + @",WorkPostName,ISNULL(TestCount,0) AS TestCount,ISNULL(TestQualifyCount,0) AS TestQualifyCount "
                        + @" FROM SitePerson_Person AS person "
                        + @" LEFT JOIN Base_Unit AS Unit ON person.UnitId=Unit.UnitId"
                        + @" LEFT JOIN Base_WorkPost AS WorkPost ON person.WorkPostId=WorkPost.WorkPostId"
                        + @" LEFT JOIN Base_Depart AS Depart ON person.DepartId=Depart.DepartId"
                        + @" LEFT JOIN (SELECT COUNT(TestRecordId) AS TestCount,TestManId  FROM Training_TestRecord GROUP BY TestManId) AS TestCount"
                        + @" ON person.PersonId=TestCount.TestManId"
                        + @" LEFT JOIN (SELECT COUNT(TestRecordId) AS TestQualifyCount,TestManId  FROM Training_TestRecord WHERE TestScores>= 60 GROUP BY TestManId) AS TestQualifyCount"
                        + @" ON person.PersonId=TestQualifyCount.TestManId"
                        + @" WHERE PersonId <> '" + BLL.Const.sysglyId + "' and person.projectid='" + this.CurrUser.LoginProjectId + "'";
            List<SqlParameter> listStr = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(this.txtName.Text.Trim()))
            {
                strSql += " AND (PersonName LIKE @name OR CardNo LIKE @name OR Unit.UnitName LIKE @name OR Depart.DepartName LIKE @name OR WorkPostName LIKE @name)";
                listStr.Add(new SqlParameter("@name", "%" + this.txtName.Text.Trim() + "%"));
            }

            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            Grid1.RecordCount = tb.Rows.Count;
            var table = this.GetPagedDataTable(Grid1, tb);

            Grid1.DataSource = table;
            Grid1.DataBind();
        }
        #endregion

        #region 分页排序
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

        #region 输入框查询事件
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
    }
}