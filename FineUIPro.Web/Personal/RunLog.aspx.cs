using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace FineUIPro.Web.Personal
{
    public partial class RunLog : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (this.CurrUser != null && this.CurrUser.PageSize.HasValue)
                {
                    Grid1.PageSize = this.CurrUser.PageSize.Value;
                }
                this.ddlPageSize.SelectedValue = Grid1.PageSize.ToString();
                this.BindGrid();
                if (this.CurrUser.UserId != BLL.Const.sysglyId)
                {
                    this.txtUser.Hidden = true;
                }
            }
        }

        #region BindGrid
        /// <summary>
        ///  gv 绑定数据
        /// </summary>
        private void BindGrid()
        {
            string strSql = @"SELECT sysLog.LogId,sysLog.UserId,sysLog.OperationTime,sysLog.Ip,sysLog.HostName,sysLog.OperationLog,users.UserName,ISNULL(units.UnitName,(SELECT TOP 1 UnitName FROM Base_Unit WHERE IsThisUnit = 1)) AS  UnitName,Project.ProjectId,Project.ProjectName"
                         + @" FROM dbo.Sys_Log as sysLog"
                         + @" LEFT JOIN Sys_User as users ON users.UserId=sysLog.UserId "
                         + @" LEFT JOIN Base_Unit as units on users.UnitId=units.UnitId"
                         + @" LEFT JOIN Base_Project as Project on sysLog.ProjectId=Project.ProjectId"
                         + @" WHERE sysLog.UserId != '"+Const.hfnbdId + "' AND  sysLog.UserId !='" + Const.sedinId + "'";
            List<SqlParameter> listStr = new List<SqlParameter>();
            if (this.CurrUser.UserId != BLL.Const.sysglyId)
            {
                strSql += " AND sysLog.UserId = @UserId";
                listStr.Add(new SqlParameter("@UserId", this.CurrUser.UserId));
            }
            if (!string.IsNullOrEmpty(this.txtProject.Text.Trim()))
            {
                strSql += " AND Project.ProjectName LIKE @ProjectName";
                listStr.Add(new SqlParameter("@ProjectName", "%" + this.txtProject.Text.Trim() + "%"));
            }
            if (!string.IsNullOrEmpty(this.txtUser.Text.Trim()))
            {
                strSql += " AND users.UserName LIKE @UserName";
                listStr.Add(new SqlParameter("@UserName", "%" + this.txtUser.Text.Trim() + "%"));
            }
            if (!string.IsNullOrEmpty(this.txtOperationLog.Text.Trim()))
            {
                strSql += " AND sysLog.OperationLog LIKE @OperationLog";
                listStr.Add(new SqlParameter("@OperationLog", "%" + this.txtOperationLog.Text.Trim() + "%"));
            }
            
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            Grid1.RecordCount = tb.Rows.Count;
           // tb = GetFilteredTable(Grid1.FilteredData, tb);
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();

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
            this.BindGrid();
        }
        #endregion

        #region 分页下拉选择
        /// <summary>
        /// 分页下拉选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Grid1.PageSize = Funs.GetNewIntOrZero(this.ddlPageSize.SelectedValue);
            this.BindGrid();
        }

        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            BindGrid();
        }


        protected void Grid1_Sort(object sender, FineUIPro.GridSortEventArgs e)
        {
            BindGrid();
        }
        #endregion
    }
}