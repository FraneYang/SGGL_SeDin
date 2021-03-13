using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using AspNet = System.Web.UI.WebControls;

namespace FineUIPro.Web.ProjectData
{
    public partial class ProjectUser : PageBase
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
                Funs.DropDownPageSize(this.ddlPageSize);
                if (this.CurrUser != null && this.CurrUser.PageSize.HasValue)
                {
                    Grid1.PageSize = this.CurrUser.PageSize.Value;
                }
                this.ddlPageSize.SelectedValue = Grid1.PageSize.ToString();
                BLL.ProjectService.InitAllProjectDropDownList(this.drpProject, false);
                if (!string.IsNullOrEmpty(this.CurrUser.LoginProjectId))
                {
                    this.drpProject.SelectedValue = this.CurrUser.LoginProjectId;
                    this.drpProject.Enabled = false;
                }
                if (!string.IsNullOrEmpty(Request.Params["projectId"]))  ///是否文件柜查看页面传项目值
                {
                    this.drpProject.SelectedValue = this.CurrUser.LoginProjectId;
                    this.drpProject.Enabled = false;
                }

                BLL.UnitService.InitUnitDropDownList(this.drpUnit, this.drpProject.SelectedValue, true);
                // 绑定表格
                this.BindGrid();
                ////权限按钮方法
                this.GetButtonPower();
            }
        }
        #endregion

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            if (this.drpProject.Items.Count() > 0)
            {
                string strSql = @"SELECT DISTINCT ProjectUser.ProjectUserId,ProjectUser.ProjectId,ProjectUser.UserId,ProjectUser.WorkAreaId,Users.UserCode,Users.UserName,ProjectUser.UnitId,Unit.UnitCode,Unit.UnitName,ProjectUnit.UnitType,sysConst.ConstText AS UnitTypeName,ProjectUser.RoleId,ProjectUser.IsPost,(CASE WHEN ProjectUser.IsPost = 1 THEN '在岗' ELSE '离岗' END) AS IsPostName,WorkPost.WorkPostName"
                                + @" ,CRoleName=ISNULL(STUFF((SELECT ',' + RoleName FROM dbo.Sys_Role where PATINDEX('%,' + RTRIM(RoleId) + ',%',',' +Users.RoleId + ',')>0 FOR XML PATH('')), 1, 1,''),'') 
                                    ,RoleName=ISNULL(STUFF(( SELECT ',' + RoleName FROM dbo.Sys_Role where PATINDEX('%,' + RTRIM(RoleId) + ',%',',' +ProjectUser.RoleId + ',')>0 FOR XML PATH('')), 1, 1,''),'') "
                                + @" FROM Project_ProjectUser AS ProjectUser "
                                + @" LEFT JOIN Base_Project AS Project ON ProjectUser.ProjectId = Project.ProjectId "
                                + @" LEFT JOIN Sys_User AS Users ON ProjectUser.UserId = Users.UserId "
                                + @" LEFT JOIN Sys_Role AS Role ON ProjectUser.RoleId = Role.RoleId "
                                + @" LEFT JOIN Project_ProjectUnit AS ProjectUnit ON ProjectUser.UnitId = ProjectUnit.UnitId AND ProjectUser.ProjectId= ProjectUnit.ProjectId "
                                + @" LEFT JOIN Base_Unit AS Unit ON ProjectUser.UnitId = Unit.UnitId "
                                + @" LEFT JOIN SitePerson_Person AS Person ON ProjectUser.ProjectId =Person.ProjectId AND Users.IdentityCard = Person.IdentityCard "
                                + @" LEFT JOIN Base_WorkPost AS WorkPost ON Person.WorkPostId =WorkPost.WorkPostId "
                                + @" LEFT JOIN Sys_Const AS sysConst ON sysConst.GroupId = '" + BLL.ConstValue.Group_ProjectUnitType + "' AND ProjectUnit.UnitType=sysConst.ConstValue "
                                + @" WHERE 1=1 ";
                List<SqlParameter> listStr = new List<SqlParameter>();
                strSql += " AND ProjectUser.ProjectId = @ProjectId";
                listStr.Add(new SqlParameter("@ProjectId", this.drpProject.SelectedValue));

                if (!string.IsNullOrEmpty(this.drpUnit.SelectedValue) && this.drpUnit.SelectedValue != BLL.Const._Null)
                {
                    strSql += " AND ProjectUser.UnitId = @UnitId";
                    listStr.Add(new SqlParameter("@UnitId", this.drpUnit.SelectedValue));
                }

                if (!string.IsNullOrEmpty(this.txtUserName.Text.Trim()))
                {
                    strSql += " AND Users.UserName LIKE @UserName";
                    listStr.Add(new SqlParameter("@UserName", "%" + this.txtUserName.Text.Trim() + "%"));
                }
                SqlParameter[] parameter = listStr.ToArray();
                DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

                Grid1.RecordCount = tb.Rows.Count;
                tb = GetFilteredTable(Grid1.FilteredData, tb);
                var table = this.GetPagedDataTable(Grid1, tb);
                Grid1.DataSource = table;
                Grid1.DataBind();
                for (int i = 0; i < this.Grid1.Rows.Count; i++)
                {
                    if (this.Grid1.Rows[i].Values[4].ToString() == "")
                    {
                        Grid1.Rows[i].CellCssClasses[4] = "red";
                    }
                }
            }
        }


        #region 操作 Events
        /// <summary>
        /// 右键删除事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuDelete_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length > 0)
            {
                foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                {
                    BLL.LogService.AddSys_Log(this.CurrUser, "删除项目用户！", null, BLL.Const.ProjectUserMenuId, BLL.Const.BtnDelete);
                    var projectUser = BLL.ProjectUserService.GetProjectUserById(Grid1.DataKeys[rowIndex][0].ToString());
                    if (projectUser != null)
                    {
                        Model.Sys_RoleItem roleItem = BLL.RoleItemService.GeRoleItemByUserIdAndProjectId(projectUser.UserId, projectUser.ProjectId);
                        if (roleItem != null)
                        {
                            BLL.RoleItemService.DeleteRoleItem(roleItem.RoleItemId);
                        }
                    }
                    BLL.ProjectUserService.DeleteProjectUserById(Grid1.DataKeys[rowIndex][0].ToString());

                }

                BindGrid();
                ShowNotify("删除数据成功!", MessageBoxIcon.Success);
            }
        }
        #endregion

        #region 排序 分页
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            Grid1.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid1.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            BindGrid();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_Sort(object sender, FineUIPro.GridSortEventArgs e)
        {
            Grid1.SortDirection = e.SortDirection;
            Grid1.SortField = e.SortField;
            BindGrid();
        }
        #endregion

        #region 增加编辑
        /// <summary>
        /// 增加按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNew_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.drpProject.SelectedValue))
            {
                var punit = Funs.DB.Project_ProjectUnit.FirstOrDefault(x => x.ProjectId == this.drpProject.SelectedValue);
                if (punit != null)
                {
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("ProjectUserSelect.aspx?ProjectId={0}", this.drpProject.SelectedValue), "选择项目用户", 800, 500));
                }
                else
                {
                    Alert.ShowInTop("请先选择项目单位！", MessageBoxIcon.Warning);
                    return;
                }
            }
            else
            {
                Alert.ShowInTop("请选择项目！", MessageBoxIcon.Warning);
                return;
            }
        }

        /// <summary>
        /// 双击事件
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
                Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            if (this.btnMenuEdit.Hidden)   ////双击事件 编辑权限有：编辑页面，无：查看页面 或者状态是完成时查看页面
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("ProjectUserView.aspx?ProjectUserId={0}", Grid1.SelectedRowID), "查看项目用户", 800, 500));
            }
            else
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("ProjectUserSave.aspx?ProjectUserId={0}", Grid1.SelectedRowID), "编辑项目用户", 800, 500));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window1_Close(object sender, EventArgs e)
        {
            BindGrid();
        }
        #endregion

        #region 获取按钮权限
        /// <summary>
        /// 获取按钮权限
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        private void GetButtonPower()
        {
            if (Request.Params["value"] == "0")
            {
                return;
            }
            string menuId = BLL.Const.SeverProjectUserMenuId;
            if (!string.IsNullOrEmpty(this.CurrUser.LoginProjectId))
            {
                menuId = BLL.Const.ProjectUserMenuId;
            }
            var porject = BLL.ProjectService.GetProjectByProjectId(this.drpProject.SelectedValue);
            if (porject != null && (porject.ProjectState == BLL.Const.ProjectState_1 || string.IsNullOrEmpty(porject.ProjectState)))
            {
                var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, menuId);
                if (buttonList.Count() > 0)
                {
                    if (buttonList.Contains(BLL.Const.BtnSelect))
                    {
                        this.btnNew.Hidden = false;
                        this.btnMenuEdit.Hidden = false;
                        this.btnEdit.Hidden = false;
                    }
                    if (buttonList.Contains(BLL.Const.BtnDelete))
                    {
                        this.btnMenuDelete.Hidden = false;
                        this.btnDelete.Hidden = false;
                    }
                }
            }
            else
            {
                this.btnNew.Hidden = true;
                this.btnMenuEdit.Hidden = true;
                this.btnMenuDelete.Hidden = true;
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
            if (sender.GetType().Name == "DropDownList" && ((FineUIPro.DropDownList)(sender)).DataTextField == "ProjectName")
            {
                BLL.UnitService.InitUnitDropDownList(this.drpUnit, this.drpProject.SelectedValue, true);
            }
            this.BindGrid();
            this.GetButtonPower();
        }
        #endregion

        #region 获取分管范围
        /// <summary>
        /// 获取分管范围
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        protected string ConvertWorkAreaId(object WorkAreaId)
        {
            string UnitWorkName = string.Empty;
            if (WorkAreaId != null)
            {
                string[] Ids = WorkAreaId.ToString().Split(',');
                foreach (string t in Ids)
                {
                    var type = BLL.UnitWorkService.GetUnitWorkByUnitWorkId(t);
                    if (type != null)
                    {
                        UnitWorkName += BLL.UnitWorkService.GetUnitWorkByUnitWorkId(t).UnitWorkName + ",";
                    }
                }
            }
            if (UnitWorkName != string.Empty)
            {
                return UnitWorkName.Substring(0, UnitWorkName.Length - 1);
            }
            else
            {
                return "";
            }
        }
        #endregion

        #region 导出按钮
        /// 导出按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnOut_Click(object sender, EventArgs e)
        {
            Response.ClearContent();
            string filename = Funs.GetNewFileName();
            Response.AddHeader("content-disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode("项目用户" + filename, System.Text.Encoding.UTF8) + ".xls");
            Response.ContentType = "application/excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            this.Grid1.PageSize = 500;
            this.BindGrid();
            Response.Write(GetGridTableHtml(Grid1));
            Response.End();
        }

#pragma warning disable CS0108 // “ProjectUser.GetGridTableHtml(Grid)”隐藏继承的成员“PageBase.GetGridTableHtml(Grid)”。如果是有意隐藏，请使用关键字 new。
        /// <summary>
        /// 导出方法
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        private string GetGridTableHtml(Grid grid)
#pragma warning restore CS0108 // “ProjectUser.GetGridTableHtml(Grid)”隐藏继承的成员“PageBase.GetGridTableHtml(Grid)”。如果是有意隐藏，请使用关键字 new。
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<meta http-equiv=\"content-type\" content=\"application/excel; charset=UTF-8\"/>");
            sb.Append("<table cellspacing=\"0\" rules=\"all\" border=\"1\" style=\"border-collapse:collapse;\">");
            sb.Append("<tr>");
            foreach (GridColumn column in grid.Columns)
            {
                sb.AppendFormat("<td>{0}</td>", column.HeaderText);
            }
            sb.Append("</tr>");
            foreach (GridRow row in grid.Rows)
            {
                sb.Append("<tr>");
                foreach (GridColumn column in grid.Columns)
                {
                    string html = row.Values[column.ColumnIndex].ToString();
                    if (column.ColumnID == "tfNumber")
                    {
                        html = (row.FindControl("lblNumber") as AspNet.Label).Text;
                    }
                    sb.AppendFormat("<td>{0}</td>", html);
                }

                sb.Append("</tr>");
            }

            sb.Append("</table>");

            return sb.ToString();
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }

            string projectUserId = Grid1.SelectedRowID;
            var pu = ProjectUserService.GetProjectUserById(projectUserId);
            if (pu != null)
            {
                var users = UserService.GetUserByUserId(pu.UserId);
                if (users != null && CommonService.IsMainUnitOrAdmin(this.CurrUser.UserId) && users.UnitId != Const.UnitId_SEDIN)
                {
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("../SysManage/UserListEdit.aspx?userId={0}&type=-1", users.UserId, "编辑 - ")));
                }
                else
                {
                    Alert.ShowInTop("本单位人员信息请在系统设置中修改！", MessageBoxIcon.Warning);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }

            string projectUserId = Grid1.SelectedRowID;
            var pu = ProjectUserService.GetProjectUserById(projectUserId);
            if (pu != null)
            {
                var users = UserService.GetUserByUserId(pu.UserId);
                if (users != null && CommonService.IsMainUnitOrAdmin(this.CurrUser.UserId) && users.UnitId != Const.UnitId_SEDIN)
                {
                    string cont = judgementDelete(users.UserId);
                    if (string.IsNullOrEmpty(cont))
                    {
                        BLL.LogService.AddSys_Log(this.CurrUser, users.UserCode, users.UserId, BLL.Const.UserMenuId, BLL.Const.BtnDelete);
                        BLL.ProjectUserService.DeleteProjectUserById(projectUserId);
                        BLL.UserService.DeleteUser(users.UserId);
                        ShowNotify("删除用户成功！", MessageBoxIcon.Success);
                        BindGrid();
                    }
                    else
                    {
                        Alert.ShowInParent("用户：" + users.UserName + cont, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    Alert.ShowInTop("本单位人员信息请在系统设置中删除！", MessageBoxIcon.Warning);
                }
            }
        }

        #region 判断是否可删除
        /// <summary>
        /// 判断是否可以删除
        /// </summary>
        /// <returns></returns>
        private string judgementDelete(string id)
        {
            string content = string.Empty;
            if (Funs.DB.Project_ProjectUser.FirstOrDefault(x => x.UserId == id && x.ProjectId != this.CurrUser.LoginProjectId) != null)
            {
                content += "已在【项目用户】中使用，不能删除！";
            }
            if (Funs.DB.ProjectData_FlowOperate.FirstOrDefault(x => x.OperaterId == id) != null)
            {
                content += "已在【审核流程】中使用，不能删除！";
            }
            return content;
        }
        #endregion
    }
}