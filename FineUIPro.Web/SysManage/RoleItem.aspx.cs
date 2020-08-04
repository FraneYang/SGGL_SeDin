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
            string strSql = @"SELECT RoleItemId,UserId,RoleId,IntoDate,OutDate,UserName,ProjectName
                            FROM dbo.View_Sys_RoleItem";
            List<SqlParameter> listStr = new List<SqlParameter>();
            strSql += " where UserId= @UserId";
            listStr.Add(new SqlParameter("@UserId", Request.Params["userId"]));
            if (!string.IsNullOrEmpty(this.txtProjectName.Text))
            {
                strSql += " AND ProjectName LIKE @ProjectName";
                listStr.Add(new SqlParameter("@ProjectName", "%" + this.txtProjectName.Text.Trim() + "%"));
            }
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

        #region 增加历史记录
        /// <summary>
        /// 增加历史记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNew_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("RoleItemEdit.aspx?userId={0}", Request.Params["userId"], "编辑 - ")));
        }
        #endregion

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
        protected void btnMenuEdit_Click(object sender, EventArgs e)
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
                Alert.ShowInParent("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            string Id = Grid1.SelectedRowID;
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("RoleItemEdit.aspx?roleItemId={0}", Id, "编辑 - ")));
        }

        #region  删除数据
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
                string strShowNotify = string.Empty;
                foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                {
                    string rowID = Grid1.DataKeys[rowIndex][0].ToString();
                    BLL.RoleItemService.DeleteRoleItem(rowID);
                }

                BindGrid();
                if (!string.IsNullOrEmpty(strShowNotify))
                {
                    Alert.ShowInTop(strShowNotify, MessageBoxIcon.Warning);
                }
                else
                {
                    ShowNotify("删除数据成功!", MessageBoxIcon.Success);
                }
            }
        }
        #endregion

        protected void txtProjectName_Blur(object sender, EventArgs e)
        {
            BindGrid();
        }
    }
}