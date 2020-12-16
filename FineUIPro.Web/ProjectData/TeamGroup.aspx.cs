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
    public partial class TeamGroup : PageBase
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
                this.btnNew.OnClientClick = Window1.GetShowReference("TeamGroupEdit.aspx") + "return false;";
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
            string strSql = "SELECT TeamGroupId,ProjectId,UnitId,TeamGroupCode,TeamGroupName,Remark,TeamTypeId,GroupLeaderId FROM ProjectData_TeamGroup WHERE 1=1 ";
            List<SqlParameter> listStr = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(Request.Params["projectId"]))  ///是否文件柜查看页面传项目值
            {
                strSql += " AND ProjectId = @ProjectId";
                listStr.Add(new SqlParameter("@ProjectId", Request.Params["projectId"]));
            }
            else
            {
                strSql += " AND ProjectId = @ProjectId";
                listStr.Add(new SqlParameter("@ProjectId", this.CurrUser.LoginProjectId));
            }
            if (!string.IsNullOrEmpty(this.txtTeamGroupName.Text.Trim()))
            {
                strSql += " AND TeamGroupName LIKE @TeamGroupName";
                listStr.Add(new SqlParameter("@TeamGroupName", "%" + this.txtTeamGroupName.Text.Trim() + "%"));
            }
            if (BLL.ProjectUnitService.GetProjectUnitTypeByProjectIdUnitId(this.CurrUser.LoginProjectId, this.CurrUser.UnitId))
            {
                strSql += " AND UnitId =@unitId";
                listStr.Add(new SqlParameter("@unitId", this.CurrUser.UnitId));
            }

            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            Grid1.RecordCount = tb.Rows.Count;
            tb = GetFilteredTable(Grid1.FilteredData, tb);
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();
        }

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

        /// <summary>
        /// 关闭弹出窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window1_Close(object sender, EventArgs e)
        {
            BindGrid();
        }
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

            if (this.btnMenuEdit.Hidden)   ////双击事件 编辑权限有：编辑页面，无：查看页面 或者状态是完成时查看页面
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("TeamGroupView.aspx?TeamGroupId={0}", Grid1.SelectedRowID, "查看 - ")));
            }
            else
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("TeamGroupEdit.aspx?TeamGroupId={0}", Grid1.SelectedRowID, "编辑 - ")));
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
                bool isShow = false;
                if (Grid1.SelectedRowIndexArray.Length == 1)
                {
                    isShow = true;
                }
                foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                {
                    string rowID = Grid1.DataKeys[rowIndex][0].ToString();
                    if (this.judgementDelete(rowID, isShow))
                    {
                        var TeamGroup = BLL.TeamGroupService.GetTeamGroupById(rowID);
                        if (TeamGroup != null)
                        {
                            BLL.LogService.AddSys_Log(this.CurrUser, TeamGroup.TeamGroupCode, TeamGroup.TeamGroupId, BLL.Const.TeamGroupMenuId, BLL.Const.BtnDelete);
                            BLL.TeamGroupService.DeleteTeamGroupById(rowID);
                        }
                    }
                }
                BindGrid();
                ShowNotify("删除数据成功!", MessageBoxIcon.Success);
            }
        }

        /// <summary>
        /// 判断是否可以删除
        /// </summary>
        /// <returns></returns>
        private bool judgementDelete(string id, bool isShow)
        {
            string content = string.Empty;

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

        #region 格式化字符串
        /// <summary>
        /// 根据单位Id获取单位名称字符串
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        protected string ConvertUnitName(object unitId)
        {
            string unitName = string.Empty;
            if (unitId != null)
            {
                unitName = UnitService.GetUnitNameByUnitId(unitId.ToString());
            }
            return unitName;
        }
        /// <summary>
        /// 根据施工队类型Id获取施工队类型名称字符串
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        protected string ConvertTeamType(object teamTypeId)
        {
            string teamType = string.Empty;
            if (teamTypeId != null)
            {
                teamType = BLL.BasicDataService.GetDictNameByDictCode(teamTypeId.ToString());
            }
            return teamType;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="groupLeaderId"></param>
        /// <returns></returns>
        protected string ConvertGroupLeader(object groupLeaderId)
        {
            string name = string.Empty;
            if (groupLeaderId != null)
            {
                name = PersonService.GetPersonNameById(groupLeaderId.ToString());
            }
            return name;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="groupLeaderId"></param>
        /// <returns></returns>
        protected string ConvertPersonNum(object teamGroupId)
        {
            string name = string.Empty;
            if (teamGroupId != null)
            {
                name = TeamGroupService.getTeamGroupPersonNum(teamGroupId.ToString()).ToString();
            }
            return name;
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
            if (Request.Params["value"] == "0")  ///文件柜时 没有操作权限
            {
                return;
            }
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.TeamGroupMenuId);
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
            Response.AddHeader("content-disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode("班组信息" + filename, System.Text.Encoding.UTF8) + ".xls");
            Response.ContentType = "application/excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            this.Grid1.PageSize = 500;
            this.BindGrid();
            Response.Write(GetGridTableHtml(Grid1));
            Response.End();
        }

#pragma warning disable CS0108 // “TeamGroup.GetGridTableHtml(Grid)”隐藏继承的成员“PageBase.GetGridTableHtml(Grid)”。如果是有意隐藏，请使用关键字 new。
        /// <summary>
        /// 导出方法
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        private string GetGridTableHtml(Grid grid)
#pragma warning restore CS0108 // “TeamGroup.GetGridTableHtml(Grid)”隐藏继承的成员“PageBase.GetGridTableHtml(Grid)”。如果是有意隐藏，请使用关键字 new。
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
                    if (column.ColumnID == "tfUnitId")
                    {
                        html = (row.FindControl("lblUnitName") as AspNet.Label).Text;
                    }
                    if (column.ColumnID == "TeamTypeId")
                    {
                        html = (row.FindControl("lblTeamType") as AspNet.Label).Text;
                    }
                    if (column.ColumnID == "tfGroupLeader")
                    {
                        html = (row.FindControl("lbGroupLeader") as AspNet.Label).Text;
                    }
                    if (column.ColumnID == "tfPersonNum")
                    {
                        html = (row.FindControl("lbPersonNum") as AspNet.Label).Text;
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