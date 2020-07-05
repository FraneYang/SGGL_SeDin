using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using AspNet = System.Web.UI.WebControls;

namespace FineUIPro.Web.HSSE.Hazard
{
    public partial class HazardPrompt : PageBase
    {
        #region 定义项
        /// <summary>
        /// 危险源id
        /// </summary>
        public string HazardListId
        {
            get
            {
                return (string)ViewState["HazardListId"];
            }
            set
            {
                ViewState["HazardListId"] = value;
            }
        }
        /// <summary>
        /// 项目主键
        /// </summary>
        public string ProjectId
        {
            get
            {
                return (string)ViewState["ProjectId"];
            }
            set
            {
                ViewState["ProjectId"] = value;
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
                this.ProjectId = this.CurrUser.LoginProjectId;
                if (!string.IsNullOrEmpty(Request.Params["projectId"]) && Request.Params["projectId"] != this.ProjectId)
                {
                    this.ProjectId = Request.Params["projectId"];
                } 
                GetButtonPower();
                ddlPageSize.SelectedValue = Grid1.PageSize.ToString();
                // 绑定表格
                BindGrid();
            }
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            string strSql = "select HazardList.HazardListId,HazardList.VersionNo,HazardList.WorkStage,Users.UserName,HazardList.CompileDate,CodeRecords.Code AS HazardListCode "
                           + @" ,(CASE WHEN HazardList.States = " + BLL.Const.State_0 + " OR HazardList.States IS NULL THEN '待['+OperateUser.UserName+']提交' WHEN HazardList.States =  " + BLL.Const.State_2 + " THEN '审核/审批完成' ELSE '待['+OperateUser.UserName+']办理' END) AS  FlowOperateName"
                           + @" from Hazard_HazardList AS HazardList "
                           + @" LEFT JOIN Sys_FlowOperate AS FlowOperate ON HazardList.HazardListId=FlowOperate.DataId AND FlowOperate.IsClosed <> 1"
                           + @" LEFT JOIN Sys_User AS OperateUser ON FlowOperate.OperaterId=OperateUser.UserId "
                           + @" LEFT JOIN Sys_User AS Users ON HazardList.CompileMan=Users.UserId "
                           + @" LEFT JOIN Sys_CodeRecords AS CodeRecords ON HazardList.HazardListId=CodeRecords.DataId WHERE HazardList.States='"+BLL.Const.State_2+"'";
            List<SqlParameter> listStr = new List<SqlParameter>();
            strSql += " AND HazardList.ProjectId = @ProjectId";
            listStr.Add(new SqlParameter("@ProjectId", this.ProjectId));
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            Grid1.RecordCount = tb.Rows.Count;       
            var table = this.GetPagedDataTable(Grid1, tb);

            Grid1.DataSource = table;
            Grid1.DataBind();

            for (int i = 0; i < Grid1.Rows.Count; i++)
            {
                string hazardId = Grid1.Rows[i].DataKeys[0].ToString();
                var hazardSelectedItem = BLL.Hazard_HazardSelectedItemService.GetHazardSelectedItemsByHazardListId(hazardId); ///危险源明细
                if (hazardSelectedItem.Count() > 0)
                {
                    var item1 = hazardSelectedItem.Where(x => x.PromptTime.HasValue);     ///未提醒
                    if (item1.Count() == 0)
                    {
                        Grid1.Rows[i].RowCssClass = "Yellow";
                    }
                    else
                    {
                        var item2 = hazardSelectedItem.FirstOrDefault(x => x.PromptTime.HasValue && x.IsResponse == false);     ///未响应
                        if (item2 != null)
                        {
                            Grid1.Rows[i].RowCssClass = "Red";
                        }
                    }
                }
            }
        }
        #endregion        

        #region 页索引改变事件
        /// <summary>
        /// 页索引改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            Grid1.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        #endregion

        #region 排序
        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_Sort(object sender, GridSortEventArgs e)
        {
            BindGrid();
        }
        #endregion

        #region 分页选择下拉改变事件
        /// <summary>
        /// 分页选择下拉改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid1.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            BindGrid();
        }
        #endregion

        #region 提示
        /// <summary>
        /// 右键提示事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuToolTip_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            this.HazardListId = Grid1.SelectedRowID;
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("SetToolTipTime.aspx?HazardListId={0}", this.HazardListId, "提示 - ")));
        }
        #endregion

        #region 响应
        /// <summary>
        /// 右键响应事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuResponse_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            this.HazardListId = Grid1.SelectedRowID;
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("Response.aspx?HazardListId={0}", this.HazardListId, "响应 - ")));
        }
        #endregion

        #region 转换字符串
        /// <summary>
        /// 转换工作阶段
        /// </summary>
        /// <param name="workStage"></param>
        /// <returns></returns>
        protected string ConvertWorkStage(object workStage)
        {
            if (workStage != null)
            {
                string workStages = string.Empty;
                string[] strList = workStage.ToString().Split(',');
                foreach (string str in strList)
                {
                    Model.Base_WorkStage c = BLL.WorkStageService.GetWorkStageById(str);
                    if (c != null)
                    {
                        workStages += c.WorkStageName + ",";
                    }
                }
                if (!string.IsNullOrEmpty(workStages))
                {
                    workStages = workStages.Substring(0, workStages.LastIndexOf(","));
                }
                return workStages;
            }
            return "";
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
            var buttonList = BLL.CommonService.GetAllButtonList(this.ProjectId, this.CurrUser.UserId, BLL.Const.HazardPromptMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnToolTip))
                {
                    this.btnMenuToolTip.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnResponse))
                {
                    this.btnMenuResponse.Hidden = false;
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
            Response.AddHeader("content-disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode("风险提示" + filename, System.Text.Encoding.UTF8) + ".xls");
            Response.ContentType = "application/excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            this.Grid1.PageSize = this.Grid1.RecordCount;
            this.BindGrid();
            Response.Write(GetGridTableHtml(Grid1));
            Response.End();
        }

#pragma warning disable CS0108 // “HazardPrompt.GetGridTableHtml(Grid)”隐藏继承的成员“PageBase.GetGridTableHtml(Grid)”。如果是有意隐藏，请使用关键字 new。
        /// <summary>
        /// 导出方法
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        private string GetGridTableHtml(Grid grid)
#pragma warning restore CS0108 // “HazardPrompt.GetGridTableHtml(Grid)”隐藏继承的成员“PageBase.GetGridTableHtml(Grid)”。如果是有意隐藏，请使用关键字 new。
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
                    if (column.ColumnID == "tfVersionNo")
                    {
                        html = (row.FindControl("lblVersionNo") as AspNet.Label).Text;
                    }
                    if (column.ColumnID == "tfWorkStage")
                    {
                        html = (row.FindControl("lblWorkStage") as AspNet.Label).Text;
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