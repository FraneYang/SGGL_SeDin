using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace FineUIPro.Web.HSSE.Manager
{
    public partial class ManagerMonth_SeDin : PageBase
    {
        #region 项目主键
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
                if (!string.IsNullOrEmpty(Request.Params["projectId"]) && Request.Params["projectId"] != this.CurrUser.LoginProjectId)
                {
                    this.ProjectId = Request.Params["projectId"];
                }
                ////权限按钮方法
                this.GetButtonPower();
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
            string strSql = @"SELECT mr.MonthReportId,mr.ProjectId,mr.DueDate,mr.StartDate,mr.EndDate,mr.ReporMonth,mr.CompileManId,CompileMan.UserName AS CompileManName,mr.AuditManId
                                        ,AuditMan.UserName AS AuditManName,mr.ApprovalManId,mr.AuditManId,ApprovalMan.UserName AS ApprovalManName,mr.ThisSummary,mr.NextPlan,mr.States
                                        ,(CASE WHEN mr.States='0' THEN '待['+ (CASE WHEN nextMan.UserName IS NULL THEN CompileMan.UserName ELSE nextMan.UserName END) +']提交'
	                                        WHEN mr.States='1' OR mr.States='2' THEN '待['+nextMan.UserName+']审核'
	                                        WHEN mr.States='3' THEN '已完成'
	                                        ELSE '待['+(CASE WHEN nextMan.UserName IS NULL THEN CompileMan.UserName ELSE nextMan.UserName END)+']提交' END) AS StateName
                                        FROM dbo.SeDin_MonthReport AS mr 
                                        LEFT JOIN Sys_User AS CompileMan on mr.CompileManId = CompileMan.UserId
                                        LEFT JOIN Sys_User AS AuditMan on mr.AuditManId = AuditMan.UserId
                                        LEFT JOIN Sys_User AS ApprovalMan on mr.ApprovalManId = ApprovalMan.UserId
                                        LEFT JOIN Sys_User AS nextMan on mr.NextManId = nextMan.UserId
                                        WHERE 1=1  ";
            List<SqlParameter> listStr = new List<SqlParameter>();
            strSql += " AND mr.ProjectId = @ProjectId";
            listStr.Add(new SqlParameter("@ProjectId", this.ProjectId));

            if (!string.IsNullOrEmpty(this.txtReporMonth.Text.Trim()))
            {
                strSql += " AND mr.ReporMonth LIKE @ReporMonth";
                listStr.Add(new SqlParameter("@ReporMonth", "%" + this.txtReporMonth.Text.Trim() + "%"));
            }
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            Grid1.RecordCount = tb.Rows.Count;
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();
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

        #region 表排序、分页、关闭窗口
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
        /// 排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_Sort(object sender, GridSortEventArgs e)
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
            BindGrid();
        }

        /// <summary>
        /// 关闭弹出窗
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
            BindGrid();
        }
        #endregion

        #region Grid双击事件
        /// <summary>
        /// Grid行双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            btnMenuModify_Click(null, null);
        }
        #endregion

        #region 编辑
        /// <summary>
        /// 编辑按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuModify_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            string MonthReportId = Grid1.SelectedRowID;
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("ManagerMonth_SeDinEdit.aspx?MonthReportId={0}", MonthReportId, "编辑 - ")));           
        }
        #endregion

        #region 删除
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuDel_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length > 0)
            {
                foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                {
                    string rowID = Grid1.DataKeys[rowIndex][0].ToString();
                    var mont = ManagerMonth_SeDinService.GetMonthReportByMonthReportId(rowID);
                    if (mont != null)
                    {
                        BLL.HSEDataCollectService.DeleteHSEDataCollectItem(mont);
                        LogService.AddSys_Log(this.CurrUser, mont.ReporMonth.ToString(), mont.MonthReportId, BLL.Const.ProjectManagerMonth_SeDinMenuId, BLL.Const.BtnDelete);                  
                        ManagerMonth_SeDinService.DeleteMonthReportByMonthReportId(rowID);
                    }
                }
                BindGrid();
                ShowNotify("删除数据成功!（表格数据已重新绑定）");
            }
        }
        #endregion

        #region 新增
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNew_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMonth.Text.Trim())) {
                Alert.ShowInTop("请输入您要添加的月份！", MessageBoxIcon.Warning);
                return;
            }
            if (ManagerMonth_SeDinService.GetMonthReportByDate(Convert.ToDateTime(txtMonth.Text.Trim()), this.ProjectId))
            {
                Alert.ShowInTop("当前月份的月报已存在！", MessageBoxIcon.Warning);
                return;
            }
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("ManagerMonth_SeDinEdit.aspx?Month={0}", txtMonth.Text.Trim()), "添加月报表"));
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
            var buttonList = CommonService.GetAllButtonList(this.ProjectId, this.CurrUser.UserId, BLL.Const.ProjectManagerMonth_SeDinMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(Const.BtnAdd))
                {
                    this.btnNew.Hidden = false;
                }
                if (buttonList.Contains(Const.BtnModify))
                {
                    this.btnMenuModify.Hidden = false;
                }
                if (buttonList.Contains(Const.BtnDelete))
                {
                    this.btnMenuDel.Hidden = false;
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
            Response.AddHeader("content-disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode("管理月报" + filename, System.Text.Encoding.UTF8) + ".xls");
            Response.ContentType = "application/excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            this.Grid1.PageSize = this.Grid1.RecordCount;
            this.BindGrid();
            Response.Write(GetGridTableHtml(Grid1));
            Response.End();
        }
        #endregion

        protected void btnPrinter_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }

            PrinterDocService.PrinterDocMethod(Const.ProjectManagerMonth_SeDinMenuId, Grid1.SelectedRowID, "安全月报");
        }
    }
}