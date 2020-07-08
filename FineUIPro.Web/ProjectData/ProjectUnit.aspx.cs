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
    public partial class ProjectUnit : PageBase
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
                ProjectService.InitAllProjectDropDownList(this.drpProject, false);
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
                string strSql = @"SELECT ProjectUnit.ProjectUnitId,ProjectUnit.ProjectId,Project.ProjectCode,Project.ProjectName,ProjectUnit.UnitId,Unit.UnitCode,Unit.UnitName,ProjectUnit.UnitType,sysConst.ConstText AS UnitTypeName,ProjectUnit.InTime,ProjectUnit.OutTime "
                                + @" FROM Project_ProjectUnit AS ProjectUnit "
                                + @" LEFT JOIN Base_Project AS Project ON ProjectUnit.ProjectId = Project.ProjectId "
                                + @" LEFT JOIN Base_Unit AS Unit ON ProjectUnit.UnitId = Unit.UnitId "
                                + @" LEFT JOIN Sys_Const AS sysConst ON sysConst.GroupId = '" + BLL.ConstValue.Group_ProjectUnitType + "' AND ProjectUnit.UnitType=sysConst.ConstValue "
                                + @" WHERE 1=1 ";
                List<SqlParameter> listStr = new List<SqlParameter>();
                strSql += " AND ProjectUnit.ProjectId = @ProjectId";
                listStr.Add(new SqlParameter("@ProjectId", this.drpProject.SelectedValue));
                if (!string.IsNullOrEmpty(this.txtUnitName.Text.Trim()))
                {
                    strSql += " AND Unit.UnitName LIKE @UnitName";
                    listStr.Add(new SqlParameter("@UnitName", "%" + this.txtUnitName.Text.Trim() + "%"));
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
                    if (this.Grid1.Rows[i].Values[1].ToString() == "")
                    {
                        Grid1.Rows[i].CellCssClasses[1] = "red";
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
                string unitName = string.Empty;
                foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                {
                    string projectUnitId = Grid1.DataKeys[rowIndex][0].ToString();
                    var delProjectUnit = BLL.ProjectUnitService.GetProjectUnitById(projectUnitId);
                    if (delProjectUnit != null)
                    {
                        //var person = new Model.SGGLDB(Funs.ConnString).SitePerson_Person.FirstOrDefault(x => x.ProjectId == delProjectUnit.ProjectId && x.UnitId == delProjectUnit.UnitId);
                        //if (person == null)
                        //{
                        //    BLL.LogService.AddSys_Log(this.CurrUser, "删除项目单位！", delProjectUnit.ProjectUnitId,BLL.Const.ProjectUnitMenuId,BLL.Const.BtnDelete);
                        //    BLL.ProjectUnitService.DeleteProjectProjectUnitById(projectUnitId);
                           
                        //}
                        //else
                        //{
                        //    unitName = BLL.UnitService.GetUnitNameByUnitId(delProjectUnit.UnitId);
                        //    ShowNotify(unitName + "：存在现场人员不能删除！", MessageBoxIcon.Warning);
                        //}
                    }
                }

                BindGrid();
                if (string.IsNullOrEmpty(unitName))
                {
                    ShowNotify("删除数据成功!", MessageBoxIcon.Success);
                }
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
            BindGrid();
        }
        #endregion

        /// <summary>
        /// 增加按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSelect_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.drpProject.SelectedValue))
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("ProjectUnitSelect.aspx?ProjectId={0}", this.drpProject.SelectedValue), "单位选择", 900, 520));
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
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("ProjectUnitView.aspx?ProjectUnitId={0}", Grid1.SelectedRowID), "查看项目单位", 800, 500));
            }
            else
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("ProjectUnitSave.aspx?ProjectUnitId={0}", Grid1.SelectedRowID), "编辑项目单位", 800, 500));
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
            string menuId = BLL.Const.SeverProjectUnitMenuId;
            if (!string.IsNullOrEmpty(this.CurrUser.LoginProjectId))
            {
                menuId = BLL.Const.ProjectUnitMenuId;
            }
            var porject = BLL.ProjectService.GetProjectByProjectId(this.drpProject.SelectedValue);
            if (porject != null && (porject.ProjectState == BLL.Const.ProjectState_1 || string.IsNullOrEmpty(porject.ProjectState)))
            {
                var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, menuId);
                if (buttonList.Count() > 0)
                {
                    if (buttonList.Contains(BLL.Const.BtnSelect))
                    {
                        this.btnSelect.Hidden = false;
                        this.btnMenuEdit.Hidden = false;
                    }
                    if (buttonList.Contains(BLL.Const.BtnDelete))
                    {
                        this.btnMenuDelete.Hidden = false;
                    }
                }
            }
            else
            {
                this.btnSelect.Hidden = true;
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
            this.BindGrid();
            this.GetButtonPower();
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
            Response.AddHeader("content-disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode("项目单位" + filename, System.Text.Encoding.UTF8) + ".xls");
            Response.ContentType = "application/excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            this.Grid1.PageSize = 500;
            this.BindGrid();
            Response.Write(GetGridTableHtml(Grid1));
            Response.End();
        }

#pragma warning disable CS0108 // “ProjectUnit.GetGridTableHtml(Grid)”隐藏继承的成员“PageBase.GetGridTableHtml(Grid)”。如果是有意隐藏，请使用关键字 new。
        /// <summary>
        /// 导出方法
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        private string GetGridTableHtml(Grid grid)
#pragma warning restore CS0108 // “ProjectUnit.GetGridTableHtml(Grid)”隐藏继承的成员“PageBase.GetGridTableHtml(Grid)”。如果是有意隐藏，请使用关键字 new。
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
    }
}