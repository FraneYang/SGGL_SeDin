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
    public partial class ProjectShutdown : PageBase
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
                ////权限按钮方法
                this.GetButtonPower();
                Funs.DropDownPageSize(this.ddlPageSize);
                this.btnNew.OnClientClick = Window1.GetShowReference("ProjectShutdownEdit.aspx") + "return false;";
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
            string strSql = @"SELECT ProjectSate.ProjectStateId,ProjectSate.ProjectId,Project.ProjectName,ProjectSate.ProjectState,ProjectSate.OldProjectState,ProjectSate.CompileMan,Users.UserName AS CompileManName,ProjectSate.CompileDate,ProjectSate.ShutdownDate,ProjectSate.States"
                          + @" ,(CASE WHEN ProjectSate.States = " + BLL.Const.State_0 + " OR ProjectSate.States IS NULL THEN '待['+OperateUser.UserName+']提交' WHEN ProjectSate.States =  " + BLL.Const.State_2 + " THEN '审核/审批完成' ELSE '待['+OperateUser.UserName+']办理' END) AS  FlowOperateName"
                          + @" FROM dbo.Base_ProjectSate AS ProjectSate"
                          + @" LEFT JOIN dbo.Base_Project AS Project ON ProjectSate.ProjectId=Project.ProjectId"
                          + @" LEFT JOIN Sys_FlowOperate AS FlowOperate ON ProjectSate.ProjectStateId=FlowOperate.DataId AND FlowOperate.IsClosed <> 1"
                          + @" LEFT JOIN Sys_User AS OperateUser ON FlowOperate.OperaterId=OperateUser.UserId"
                          + @" LEFT JOIN dbo.Sys_User AS Users ON ProjectSate.CompileMan=Users.UserId WHERE 1=1 ";
            List<SqlParameter> listStr = new List<SqlParameter>();
            strSql += " AND ProjectSate.ProjectId = @ProjectId";
            if (!string.IsNullOrEmpty(Request.Params["projectId"]))  ///是否文件柜查看页面传项目值
            {
                listStr.Add(new SqlParameter("@ProjectId", Request.Params["projectId"]));
                strSql += " AND ProjectSate.States = @States";  ///状态为已完成
                listStr.Add(new SqlParameter("@States", BLL.Const.State_2)); 
            }
            else
            {
                listStr.Add(new SqlParameter("@ProjectId", this.CurrUser.LoginProjectId));
            }
                      
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            Grid1.RecordCount = tb.Rows.Count;
            tb = GetFilteredTable(Grid1.FilteredData, tb);
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();
        }

        #region 分页 排序
        /// <summary>
        /// 改变索引事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            BindGrid();
        }

        /// <summary>
        /// 分页下拉选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Grid1.PageSize = Convert.ToInt32(this.ddlPageSize.SelectedValue);
            BindGrid();
        }

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_Sort(object sender, FineUIPro.GridSortEventArgs e)
        {
            this.BindGrid();
        }
        #endregion
        #endregion       

        #region 编辑
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
            string id = Grid1.SelectedRowID;
            var ProjectData = BLL.ProjectSateService.GetProjectSateById(id);
            if (ProjectData != null)
            {
                if (this.btnMenuEdit.Hidden || ProjectData.States == BLL.Const.State_2)   ////双击事件 编辑权限有：编辑页面，无：查看页面 或者状态是完成时查看页面
                {
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("ProjectShutdownView.aspx?ProjectStateId={0}", id, "查看 - ")));                    
                }
                else
                {
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("ProjectShutdownEdit.aspx?ProjectStateId={0}", id, "编辑 - ")));
                }
            }            
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
            if (Grid1.SelectedRowIndexArray.Length > 0)
            {
                string info = string.Empty;
                foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                {
                    string rowID = Grid1.DataKeys[rowIndex][0].ToString();
                    var projectSate = BLL.ProjectSateService.GetProjectSateById(rowID);
                    if (projectSate != null)
                    {
                        if (projectSate.States != BLL.Const.State_2)
                        {
                            BLL.LogService.AddSys_Log(this.CurrUser, "删除项目状态及软件关闭", projectSate.ProjectStateId, BLL.Const.ProjectShutdownMenuId, BLL.Const.BtnDelete);
                            BLL.ProjectSateService.DeleteProjectSateById(rowID);
                          
                        }
                        else
                        {
                            info = (rowIndex + 1).ToString() + "行；";
                        }
                    }
                    else
                    {
                    }

                    this.BindGrid();
                    if (!string.IsNullOrEmpty(info))
                    {
                        Alert.ShowInTop("删除已完成，其中第" + info + "单据状态已生效，不能删除！", MessageBoxIcon.Warning);
                    }
                    else
                    {
                        ShowNotify("删除数据成功!", MessageBoxIcon.Success);
                    }
                }
            }
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
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.ProjectShutdownMenuId);
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

        #region 导出按钮
        /// 导出按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnOut_Click(object sender, EventArgs e)
        {
            Response.ClearContent();
            string filename = Funs.GetNewFileName();
            Response.AddHeader("content-disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode("项目状态及软件关闭" + filename, System.Text.Encoding.UTF8) + ".xls");
            Response.ContentType = "application/excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            this.Grid1.PageSize = 500;
            this.BindGrid();
            Response.Write(GetGridTableHtml(Grid1));
            Response.End();
        }

#pragma warning disable CS0108 // “ProjectShutdown.GetGridTableHtml(Grid)”隐藏继承的成员“PageBase.GetGridTableHtml(Grid)”。如果是有意隐藏，请使用关键字 new。
        /// <summary>
        /// 导出方法
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        private string GetGridTableHtml(Grid grid)
#pragma warning restore CS0108 // “ProjectShutdown.GetGridTableHtml(Grid)”隐藏继承的成员“PageBase.GetGridTableHtml(Grid)”。如果是有意隐藏，请使用关键字 new。
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
                    if (column.ColumnID == "OldProjectState")
                    {
                        html = (row.FindControl("Label3") as AspNet.Label).Text;
                    }
                    if (column.ColumnID == "ProjectState")
                    {
                        html = (row.FindControl("Label1") as AspNet.Label).Text;
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