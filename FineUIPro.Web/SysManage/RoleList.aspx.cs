namespace FineUIPro.Web.SysManage
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using BLL;

    public partial class RoleList : PageBase
    {
        /// <summary>
        /// 加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {           
            if (!IsPostBack)
            {
                ////权限按钮方法
                this.GetButtonPower();
                Funs.DropDownPageSize(this.ddlPageSize);
                this.btnNew.OnClientClick = Window1.GetShowReference("RoleListEdit.aspx") + "return false;";
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
            string strSql = @"SELECT Roles.RoleId,Roles.RoleName,Roles.RoleCode,Roles.RoleType,Roles.CNCodes,Roles.Def,Roles.IsAuditFlow,Const13.ConstText AS RoleTypeName,Roles.IsSystemBuilt"
                          + @" FROM dbo.Sys_Role AS Roles "
                          + @" LEFT JOIN Sys_Const AS Const13 ON Roles.RoleType=Const13.ConstValue AND Const13.GroupId='" + BLL.ConstValue.Group_0013 + "'"
                          + @" WHERE 1=1 ";
            List<SqlParameter> listStr = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(this.txtRoleName.Text.Trim()))
            {
                strSql += " AND RoleName LIKE @RoleName";
                listStr.Add(new SqlParameter("@RoleName", "%" + this.txtRoleName.Text.Trim() + "%"));
            }       
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            Grid1.RecordCount = tb.Rows.Count;
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();
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
                foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                {
                    string rowID = Grid1.DataKeys[rowIndex][0].ToString();
                    if (judgementDelete(rowID, false))
                    {
                        var roles = BLL.RoleService.GetRoleByRoleId(rowID);
                        if (roles != null && ((!roles.IsSystemBuilt.HasValue || roles.IsSystemBuilt == false)))
                        {
                            BLL.LogService.AddSys_Log(this.CurrUser, roles.RoleCode, roles.RoleId, Const.RoleMenuId, Const.BtnDelete);
                            BLL.RoleService.DeleteRole(rowID);
                          
                        }
                    }
                }

                BindGrid();
                ShowNotify("删除数据成功!", MessageBoxIcon.Success);
            }
        }
        #endregion

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
            var roles = BLL.RoleService.GetRoleByRoleId(Id);
            if (roles != null && (!roles.IsSystemBuilt.HasValue || roles.IsSystemBuilt == false || this.CurrUser.UserId == BLL.Const.sysglyId ))
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("RoleListEdit.aspx?roleId={0}", Id, "编辑 - ")));
            }
            else
            {
                Alert.ShowInParent("系统内置角色不允许再修改！", MessageBoxIcon.Warning);
                return;
            }
        }

        #region 获取按钮权限
        /// <summary>
        /// 获取按钮权限
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        private void GetButtonPower()
        {
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.RoleMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnAdd))
                {
                    this.btnNew.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnModify))
                {
                    this.btnMenuEdit.Hidden = false;                   
                }
                if (buttonList.Contains(BLL.Const.BtnDelete))
                {
                    this.btnMenuDelete.Hidden = false;
                }
            }
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

        #region 判断是否可删除
        /// <summary>
        /// 判断是否可以删除
        /// </summary>
        /// <returns></returns>
        private bool judgementDelete(string id, bool isShow)
        {
            string content = string.Empty;
            if (Funs.DB.Sys_User.FirstOrDefault(x => x.RoleId == id) != null)
            {
                content = "该角色已在【用户信息】中使用，不能删除！";
            }
          
            if (string.IsNullOrEmpty(content))
            {
                return true;
            }
            else
            {
                if (isShow)
                {
                    Alert.ShowInTop(content);
                }
                return false;
            }
        }
        #endregion

        /// <summary>
        /// 获取角色对口专业设置
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        protected string ConvertCNCodes(object CNCodes)
        {
            string ProfessionalName = string.Empty;
            if (CNCodes != null)
            {
                string[] Ids = CNCodes.ToString().Split(',');
                foreach (string t in Ids)
                {
                    var type = BLL.CNProfessionalService.GetCNProfessional(t);
                    if (type != null)
                    {
                        ProfessionalName += type.ProfessionalName + ",";
                    }
                }
            }
            if (ProfessionalName != string.Empty)
            {
                return ProfessionalName.Substring(0, ProfessionalName.Length - 1);
            }
            else
            {
                return "";
            }
        }
    }
}