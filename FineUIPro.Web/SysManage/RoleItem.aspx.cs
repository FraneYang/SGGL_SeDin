using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FineUIPro.Web.SysManage
{
    public partial class RoleItem : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Funs.DropDownPageSize(this.ddlPageSize);
                this.ddlPageSize.SelectedValue = Grid1.PageSize.ToString();
                BindGrid();
            }
        }
        private void BindGrid()
        {
            string strSql = @"SELECT RoleItemId,R.UserId,R.RoleId,R.IntoDate,OutDate,U.UserName FROM Sys_RoleItem AS R
                              LEFT JOIN Sys_User AS U ON R.UserId=U.UserId";

            List<SqlParameter> listStr = new List<SqlParameter>();
            strSql += " where R.UserId= @UserId";
            listStr.Add(new SqlParameter("@UserId", Request.Params["userId"]));
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            Grid1.RecordCount = tb.Rows.Count;
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();
        }
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Grid1.PageSize = Convert.ToInt32(this.ddlPageSize.SelectedValue);
            BindGrid();
        }
        //<summary>
        //获取角色名称
        //</summary>
        //<param name="state"></param>
        //<returns></returns>
        protected string ConvertRoleName(object RoleId)
        {
            string RoleName = string.Empty;
            if (RoleId != null)
            {
                string[] Ids = RoleId.ToString().Split(',');
                foreach (string t in Ids)
                {
                    var type = BLL.RoleService.GetRoleByRoleId(t);
                    if (type != null)
                    {
                        RoleName += type.RoleName + ",";
                    }
                }
            }
            if (RoleName != string.Empty)
            {
                return RoleName.Substring(0, RoleName.Length - 1);
            }
            else
            {
                return "";
            }
        }
    }
}