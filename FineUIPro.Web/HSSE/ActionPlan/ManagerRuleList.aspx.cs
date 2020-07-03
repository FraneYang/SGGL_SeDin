using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using AspNet = System.Web.UI.WebControls;

namespace FineUIPro.Web.HSSE.ActionPlan
{
    public partial class ManagerRuleList : PageBase
    {
        #region 定义项
        /// <summary>
        /// 管理规定发布主键
        /// </summary>
        public string ManagerRuleId
        {
            get
            {
                return (string)ViewState["ManagerRuleId"];
            }
            set
            {
                ViewState["ManagerRuleId"] = value;
            }
        }

        public string ManagerRuleListCode
        {
            get
            {
                return (string)ViewState["ManagerRuleListCode"];
            }
            set
            {
                ViewState["ManagerRuleListCode"] = value;
            }
        }
        #endregion

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
                GetButtonPower();
                this.btnMenuDelete.OnClientClick = Grid1.GetNoSelectionAlertReference("请至少选择一项！");
                this.btnMenuDelete.ConfirmText = String.Format("你确定要删除选中的&nbsp;<b><script>{0}</script></b>&nbsp;行数据吗？", Grid1.GetSelectedCountReference());

                this.ManagerRuleListCode = Request.Params["managerRuleListCode"];

                // 绑定表格
                BindGrid();
            }
        }

        /// <summary>
        /// 加载数据
        /// </summary>
        private void BindGrid()
        {
            string strSql = "select ManagerRule.ManagerRuleId,ManagerRule.ProjectId,CodeRecords.Code AS ManageRuleCode,ManagerRule.OldManageRuleCode,ManagerRule.ManageRuleName,ManagerRule.ManageRuleTypeName,ManagerRule.CompileDate,ManagerRule.VersionNo,ManagerRule.ShortRemark,ManagerRule.Remark from View_ActionPlan_ManagerRule ManagerRule left join Sys_CodeRecords AS CodeRecords ON ManagerRule.ManagerRuleId=CodeRecords.DataId where  IsIssue=1 ";

            List<SqlParameter> listStr = new List<SqlParameter>();
            strSql += " AND ManagerRule.ProjectId = @ProjectId";
            if (!string.IsNullOrEmpty(Request.Params["projectId"]))  ///是否文件柜查看页面传项目值
            {
                listStr.Add(new SqlParameter("@ProjectId", Request.Params["projectId"]));              
            }
            else
            {
                listStr.Add(new SqlParameter("@ProjectId", this.CurrUser.LoginProjectId));
            }

            if (!string.IsNullOrEmpty(this.txtManageRuleCode.Text.Trim()))
            {
                strSql += " AND CodeRecords.Code LIKE @ManageRuleCode";
                listStr.Add(new SqlParameter("@ManageRuleCode", "%" + this.txtManageRuleCode.Text.Trim() + "%"));
            }
            if (!string.IsNullOrEmpty(this.txtManageRuleName.Text.Trim()))
            {
                strSql += " AND ManageRuleName LIKE @ManageRuleName";
                listStr.Add(new SqlParameter("@ManageRuleName", "%" + this.txtManageRuleName.Text.Trim() + "%"));
            }
            if (!string.IsNullOrEmpty(this.txtManageRuleTypeName.Text.Trim()))
            {
                strSql += " AND ManageRuleTypeName LIKE @ManageRuleTypeName";
                listStr.Add(new SqlParameter("@ManageRuleTypeName", "%" + this.txtManageRuleTypeName.Text.Trim() + "%"));
            }
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            Grid1.RecordCount = tb.Rows.Count;
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();
        }
        #endregion

        #region 排序、分页
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
        protected void Grid1_Sort(object sender, GridSortEventArgs e)
        {
            Grid1.SortDirection = e.SortDirection;
            Grid1.SortField = e.SortField;
            BindGrid();
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
            this.DeleteData();
        }

        /// <summary>
        /// 删除方法
        /// </summary>
        private void DeleteData()
        {
            if (Grid1.SelectedRowIndexArray.Length > 0)
            {
                bool isShow = false;
                if (Grid1.SelectedRowIndexArray.Length == 1)
                {
                    isShow = true;
                }
                foreach (string id in Grid1.SelectedRowIDArray)
                {
                    string rowID = id;
                    if (this.judgementDelete(rowID, isShow))
                    {
                        //Model.HSSE_ActionPlan_ManagerRule managerRule = BLL.HSSE_ActionPlan_ManagerRuleService.GetManagerRuleByManagerRuleId(rowID);
                        //if (managerRule.State == "1")
                        //{
                        var getV = BLL.ActionPlan_ManagerRuleService.GetManagerRuleById(rowID);
                        if (getV != null)
                        {
                            BLL.LogService.AddSys_Log(this.CurrUser, getV.ManageRuleCode, getV.ManagerRuleId, BLL.Const.ActionPlan_ManagerRuleListMenuId, Const.BtnDelete);
                            BLL.ActionPlan_ManagerRuleService.DeleteManageRuleById(rowID);
                            BindGrid();
                            ShowNotify("删除成功！", MessageBoxIcon.Success);
                        }
                        //}
                        //else
                        //{
                        //    Alert.ShowInTop("已经进入流程，不能删除！", MessageBoxIcon.Warning);
                        //    return;
                        //}
                    }
                }
            }
        }

        /// <summary>
        /// 判断是否可删除
        /// </summary>
        /// <param name="rowID"></param>
        /// <param name="isShow"></param>
        /// <returns></returns>
        private bool judgementDelete(string rowID, bool isShow)
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

        #region 编辑
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
        /// 编辑数据方法
        /// </summary>
        private void EditData()
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            string managerRuleId = Grid1.SelectedRowID;
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("ManagerRuleEdit.aspx?type=see&ManagerRuleId={0}", managerRuleId, "查看 - ")));
        }
        #endregion

        #region 关闭弹出窗
        /// <summary>
        /// 关闭弹出窗
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
            BindGrid();
        }

        /// <summary>
        /// 关闭弹出窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window2_Close(object sender, WindowCloseEventArgs e)
        {
            BindGrid();
        }
        #endregion

        #region 格式化字符串
        /// <summary>
        /// 获取办理人
        /// </summary>
        /// <param name="manaerRuleId"></param>
        /// <returns></returns>
        protected string ConvertHandleManName(object manaerRuleId)
        {
            if (manaerRuleId != null)
            {
                return "";
            }
            return null;
        }

        /// <summary>
        /// 办理状态
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        protected string ConvertState(object state)
        {
            if (state != null)
            {
                return "";
            }
            return "";
        }
        #endregion

        #region 获取权限按钮
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

            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.ActionPlan_ManagerRuleListMenuId);
            if (buttonList.Count() > 0)
            {
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
            Response.AddHeader("content-disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode("管理规定清单" + filename, System.Text.Encoding.UTF8) + ".xls");
            Response.ContentType = "application/excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            this.Grid1.PageSize = 500;
            this.BindGrid();
            Response.Write(GetGridTableHtml(Grid1));
            Response.End();
            BLL.LogService.AddSys_Log(this.CurrUser, string.Empty, string.Empty, BLL.Const.ActionPlan_ManagerRuleListMenuId, Const.BtnOut);
        }

#pragma warning disable CS0108 // “ManagerRuleList.GetGridTableHtml(Grid)”隐藏继承的成员“PageBase.GetGridTableHtml(Grid)”。如果是有意隐藏，请使用关键字 new。
        /// <summary>
        /// 导出方法
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        private string GetGridTableHtml(Grid grid)
#pragma warning restore CS0108 // “ManagerRuleList.GetGridTableHtml(Grid)”隐藏继承的成员“PageBase.GetGridTableHtml(Grid)”。如果是有意隐藏，请使用关键字 new。
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
                    if (column.ColumnID == "tfManageRuleCode")
                    {
                        html = (row.FindControl("lblManageRuleCode") as AspNet.Label).Text;
                    }
                    if (column.ColumnID == "tfOldManageRuleCode")
                    {
                        html = (row.FindControl("lblOldManageRuleCode") as AspNet.Label).Text;
                    }
                    if (column.ColumnID == "tfManageRuleName")
                    {
                        html = (row.FindControl("lblManageRuleName") as AspNet.Label).Text;
                    }
                    if (column.ColumnID == "tfManageRuleTypeName")
                    {
                        html = (row.FindControl("lblManageRuleTypeName") as AspNet.Label).Text;
                    }
                    if (column.ColumnID == "tfVersionNo")
                    {
                        html = (row.FindControl("lblVersionNo") as AspNet.Label).Text;
                    }
                    if (column.ColumnID == "tfRemark")
                    {
                        html = (row.FindControl("lblRemark") as AspNet.Label).Text;
                    }
                    sb.AppendFormat("<td>{0}</td>", html);
                }

                sb.Append("</tr>");
            }

            sb.Append("</table>");

            return sb.ToString();
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
            BLL.LogService.AddSys_Log(this.CurrUser, string.Empty, string.Empty, BLL.Const.ActionPlan_ManagerRuleListMenuId, Const.BtnQuery);
        }
        #endregion
    }
}