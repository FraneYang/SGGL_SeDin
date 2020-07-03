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
    public partial class EnvironmentalRiskList : PageBase
    {
        #region 定义项
        /// <summary>
        /// 清单主键
        /// </summary>
        public string EnvironmentalRiskListId
        {
            get
            {
                return (string)ViewState["EnvironmentalRiskListId"];
            }
            set
            {
                ViewState["EnvironmentalRiskListId"] = value;
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
                BLL.UnitService.InitUnitDropDownList(this.drpUnitId, this.ProjectId, true);
                if (BLL.ProjectUnitService.GetProjectUnitTypeByProjectIdUnitId(this.ProjectId, this.CurrUser.UnitId))
                {
                    this.drpUnitId.SelectedValue = this.CurrUser.UnitId;
                    this.drpUnitId.Enabled = false;
                }
                ////权限按钮方法
                this.GetButtonPower();
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
            if (!string.IsNullOrEmpty(this.ProjectId))
            {
                string strSql = "SELECT ERiskList.EnvironmentalRiskListId,ERiskList.ProjectId,ERiskList.WorkAreaName,Unit.UnitId,ERiskList.IdentificationDate,CompileManUsers.UserName AS CompileManName,ERiskList.CompileDate,CodeRecords.Code AS EnvironmentalRiskListCode,ControllingPersonUsers.UserName AS ControllingPersonName "
                              + @" ,(CASE WHEN ERiskList.States = " + BLL.Const.State_0 + " OR ERiskList.States IS NULL THEN '待['+OperateUser.UserName+']提交' WHEN ERiskList.States =  " + BLL.Const.State_2 + " THEN '审核/审批完成' ELSE '待['+OperateUser.UserName+']办理' END) AS  FlowOperateName"
                              + @" from Hazard_EnvironmentalRiskList AS ERiskList "
                              + @" LEFT JOIN Sys_FlowOperate AS FlowOperate ON ERiskList.EnvironmentalRiskListId=FlowOperate.DataId AND FlowOperate.IsClosed <> 1"
                              + @" LEFT JOIN Sys_User AS OperateUser ON FlowOperate.OperaterId=OperateUser.UserId "
                              + @" LEFT JOIN Sys_User AS CompileManUsers ON ERiskList.CompileMan=CompileManUsers.UserId  "
                              + @" LEFT JOIN Base_Unit AS Unit ON Unit.UnitId=CompileManUsers.UnitId  "
                              + @" LEFT JOIN Sys_User AS ControllingPersonUsers ON ERiskList.ControllingPerson=ControllingPersonUsers.UserId "
                              + @" LEFT JOIN Sys_CodeRecords AS CodeRecords ON ERiskList.EnvironmentalRiskListId=CodeRecords.DataId WHERE 1=1 ";
                List<SqlParameter> listStr = new List<SqlParameter>();
                strSql += " AND ERiskList.ProjectId = @ProjectId";
                listStr.Add(new SqlParameter("@ProjectId", this.ProjectId));
                if (!string.IsNullOrEmpty(Request.Params["projectId"]))  ///是否文件柜查看页面传项目值
                {
                    strSql += " AND ERiskList.States = @States";  ///状态为已完成
                    listStr.Add(new SqlParameter("@States", BLL.Const.State_2));
                }
                if (!string.IsNullOrEmpty(this.txtWorkAreaName.Text.Trim()))
                {
                    strSql += " AND ERiskList.WorkAreaName LIKE @WorkAreaName";
                    listStr.Add(new SqlParameter("@WorkAreaName", "%" + this.txtWorkAreaName.Text.Trim() + "%"));
                }
                if (this.drpUnitId.SelectedValue != BLL.Const._Null)
                {
                    strSql += " AND Unit.UnitId = @UnitId";
                    listStr.Add(new SqlParameter("@UnitId", this.drpUnitId.SelectedValue.Trim()));
                }
                if (!string.IsNullOrEmpty(this.txtStartDate.Text.Trim()))
                {
                    strSql += " AND ERiskList.IdentificationDate >= @StartDate";
                    listStr.Add(new SqlParameter("@StartDate", this.txtStartDate.Text.Trim()));
                }
                if (!string.IsNullOrEmpty(this.txtEndDate.Text.Trim()))
                {
                    strSql += " AND ERiskList.IdentificationDate <= @EndDate";
                    listStr.Add(new SqlParameter("@EndDate", this.txtEndDate.Text.Trim()));
                }
                SqlParameter[] parameter = listStr.ToArray();
                DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
                Grid1.RecordCount = tb.Rows.Count;
                var table = this.GetPagedDataTable(Grid1, tb);

                Grid1.DataSource = table;
                Grid1.DataBind();
            }
            else
            {
                Grid1.DataSource = null;
                Grid1.DataBind();
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
            Grid1.SortDirection = e.SortDirection;
            Grid1.SortField = e.SortField;
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

        #region 弹出编辑窗口关闭事件
        /// <summary>
        /// 弹出编辑窗体关闭事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
            BindGrid();
        }
        #endregion

        #region 编制
        /// <summary>
        /// 编制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNew_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("EnvironmentalRiskListEdit.aspx", "编辑 - ")));
        }
        #endregion

        #region 删除
        /// <summary>
        /// 右键删除事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuDel_Click(object sender, EventArgs e)
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
                foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                {
                    string rowID = Grid1.DataKeys[rowIndex][0].ToString();
                    if (this.judgementDelete(rowID, isShow))
                    {
                        var newEnvironmentalRiskList = BLL.Hazard_EnvironmentalRiskListService.GetEnvironmentalRiskList(rowID);
                        if (newEnvironmentalRiskList != null)
                        {
                            BLL.LogService.AddSys_Log(this.CurrUser, newEnvironmentalRiskList.RiskCode, newEnvironmentalRiskList.EnvironmentalRiskListId, BLL.Const.ProjectEnvironmentalRiskListMenuId, BLL.Const.BtnDelete);
                            BLL.Hazard_EnvironmentalRiskItemService.DeleteEnvironmentalRiskItemByRiskListId(rowID);
                            BLL.Hazard_EnvironmentalRiskListService.DeleteEnvironmentalRiskListById(rowID);
                        }
                    }
                }
                BindGrid();
                ShowNotify("删除数据成功!（表格数据已重新绑定）", MessageBoxIcon.Success);
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
            string EnvironmentalRiskListId = Grid1.SelectedRowID;
            var hazardList = BLL.Hazard_EnvironmentalRiskListService.GetEnvironmentalRiskList(EnvironmentalRiskListId);
            if (hazardList != null)
            {
                if (this.btnMenuModify.Hidden || hazardList.States == BLL.Const.State_2)   ////双击事件 编辑权限有：编辑页面，无：查看页面 或者状态是完成时查看页面
                {
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("EnvironmentalRiskListView.aspx?EnvironmentalRiskListId={0}", EnvironmentalRiskListId, "查看 - ")));
                }
                else
                {
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("EnvironmentalRiskListEdit.aspx?EnvironmentalRiskListId={0}", EnvironmentalRiskListId, "编辑 - ")));
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
            var buttonList = BLL.CommonService.GetAllButtonList(this.ProjectId, this.CurrUser.UserId, BLL.Const.ProjectEnvironmentalRiskListMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnAdd))
                {
                    this.btnNew.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnModify))
                {
                    this.btnMenuModify.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnDelete))
                {
                    this.btnMenuDel.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnPrint))
                {
                    this.btnMenuAllPrint.Hidden = false;
                    this.btnMenuImportantPrint.Hidden = false;
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
            Response.AddHeader("content-disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode("环境危险源辨识与评价" + filename, System.Text.Encoding.UTF8) + ".xls");
            Response.ContentType = "application/excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            this.Grid1.PageSize = 500;
            this.BindGrid();
            Response.Write(GetGridTableHtml(Grid1));
            Response.End();
        }
        #endregion        

        #region 打印
        /// <summary>
        /// 打印全部危险源
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuAllPrint_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Grid1.SelectedRowID))
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("../../ReportPrint/ExReportPrint.aspx?reportId={0}&&replaceParameter={1}&&varValue={2}", Const.EnvironmentalRiskReportId, Grid1.SelectedRowID, "", "打印 - ")));
            }
        }

        /// <summary>
        /// 打印重要危险源
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuImportantPrint_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Grid1.SelectedRowID))
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("../../ReportPrint/ExReportPrint.aspx?reportId={0}&&replaceParameter={1}&&varValue={2}", Const.EnvironmentalRiskImportantReportId, Grid1.SelectedRowID, "", "打印 - ")));
            }
        }
        #endregion
    }
}