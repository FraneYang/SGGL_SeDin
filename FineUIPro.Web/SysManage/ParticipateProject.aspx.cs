using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace FineUIPro.Web.SysManage
{
    public partial class ParticipateProject :PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Funs.DropDownPageSize(this.ddlPageSize);
                if (this.CurrUser != null && this.CurrUser.PageSize.HasValue)
                {
                    Grid1.PageSize = this.CurrUser.PageSize.Value;
                }
                this.ddlPageSize.SelectedValue = Grid1.PageSize.ToString();
                // 绑定表格
                this.BindGrid();
            }
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            string strSql = @"SELECT ProjectUser.ProjectUserId,"
                          + @"Users.UserCode,"
                          + @"Users.UserName,"
                          + @"Users.RoleId,"
                          + @"Users.LoginProjectId,"
                          + @"Roles.RoleName,"
                          + @"Project.ProjectCode,"
                          + @"Project.ProjectName"
                          + @" FROM dbo.Project_ProjectUser AS ProjectUser "
                          + @" LEFT JOIN dbo.Sys_Role AS Roles ON Roles.RoleId = ProjectUser.RoleId"
                          + @" LEFT JOIN dbo.Sys_User AS Users ON ProjectUser.UserId = Users.UserId"
                          + @" LEFT JOIN dbo.Base_Project AS Project ON Project.ProjectId = ProjectUser.ProjectId"
                          + @" WHERE 1=1";
            List<SqlParameter> listStr = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(Request.Params["userId"]))
            {
                strSql += " AND ProjectUser.UserId = @UserId ";
                listStr.Add(new SqlParameter("@UserId", Request.Params["userId"]));
            }
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            Grid1.RecordCount = tb.Rows.Count;
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();
        }

        #region 分页
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
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
    }
}