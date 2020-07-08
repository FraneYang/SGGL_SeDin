using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace FineUIPro.Web.HSSE.InformationProject
{
    public partial class DrillPlanHalfYearReport : PageBase
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
                this.ProjectId = this.CurrUser.LoginProjectId;
                if (!string.IsNullOrEmpty(Request.Params["projectId"]) && Request.Params["projectId"] != this.CurrUser.LoginProjectId)
                {
                    this.ProjectId = Request.Params["projectId"];
                }
                ////权限按钮方法
                this.GetButtonPower();
                BLL.ConstValue.InitConstValueDropDownList(this.drpYear, BLL.ConstValue.Group_0008, true);
                BLL.ConstValue.InitConstValueDropDownList(this.drpQuarters, BLL.ConstValue.Group_0010, true);

                this.btnNew.OnClientClick = Window1.GetShowReference("DrillPlanHalfYearReportEdit.aspx") + "return false;";
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
            string strSql = @"SELECT DrillPlanHalfYearReport.DrillPlanHalfYearReportId, "
                          + @"DrillPlanHalfYearReport.ProjectId,"
                          + @"DrillPlanHalfYearReport.UnitId,"
                          + @"DrillPlanHalfYearReport.YearId,"
                          + @"DrillPlanHalfYearReport.HalfYearId,"
                          + @"(CAST(DrillPlanHalfYearReport.YearId AS NVARCHAR(4))+'年 '+CAST((case DrillPlanHalfYearReport.HalfYearId when '1' then '上' else '下' end) AS NVARCHAR(4))+'半年') AS YearAndHalfYear,"
                          + @"DrillPlanHalfYearReport.Telephone,"
                          + @"DrillPlanHalfYearReport.CompileMan,"
                          + @"DrillPlanHalfYearReport.CompileDate,"
                          + @"DrillPlanHalfYearReport.States,"
                          + @"Unit.UnitName,"
                          + @"(CASE WHEN DrillPlanHalfYearReport.States = " + BLL.Const.State_0 + " OR DrillPlanHalfYearReport.States IS NULL THEN '待['+OperateUser.UserName+']提交' WHEN DrillPlanHalfYearReport.States =  " + BLL.Const.State_2 + " THEN '审核/审批完成' ELSE '待['+OperateUser.UserName+']办理' END) AS  FlowOperateName"
                          + @" FROM InformationProject_DrillPlanHalfYearReport AS DrillPlanHalfYearReport "
                          + @" LEFT JOIN Sys_FlowOperate AS FlowOperate ON DrillPlanHalfYearReport.DrillPlanHalfYearReportId = FlowOperate.DataId AND FlowOperate.IsClosed <> 1"
                          + @" LEFT JOIN Sys_User AS OperateUser ON FlowOperate.OperaterId=OperateUser.UserId"
                          + @" LEFT JOIN Base_Unit AS Unit ON Unit.UnitId = DrillPlanHalfYearReport.UnitId WHERE 1=1 ";
            List<SqlParameter> listStr = new List<SqlParameter>();
            strSql += " AND DrillPlanHalfYearReport.ProjectId  = '" + this.ProjectId + "'";
            if (!string.IsNullOrEmpty(Request.Params["projectId"]))  ///是否文件柜查看页面传项目值
            {
                strSql += " AND DrillPlanHalfYearReport.States = @States";  ///状态为已完成
                listStr.Add(new SqlParameter("@States", BLL.Const.State_2));
            }

            if (this.drpYear.SelectedValue != BLL.Const._Null)
            {
                strSql += " AND DrillPlanHalfYearReport.YearId = @Year";
                listStr.Add(new SqlParameter("@Year", Funs.GetNewInt(this.drpYear.SelectedValue)));
            }
            if (this.drpQuarters.SelectedValue != BLL.Const._Null)
            {
                strSql += " AND DrillPlanHalfYearReport.HalfYearId = @HalfYearId ";
                listStr.Add(new SqlParameter("@HalfYearId", Funs.GetNewInt(this.drpQuarters.SelectedValue)));
            }
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            Grid1.RecordCount = tb.Rows.Count;
            
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
            var projectDrillPlanHalfYearReport = BLL.ProjectDrillPlanHalfYearReportService.GetDrillPlanHalfYearReportById(id);
            if (projectDrillPlanHalfYearReport != null)
            {
                if (this.btnMenuEdit.Hidden || projectDrillPlanHalfYearReport.States == BLL.Const.State_2)   ////双击事件 编辑权限有：编辑页面，无：查看页面 或者状态是完成时查看页面
                {
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("DrillPlanHalfYearReportView.aspx?DrillPlanHalfYearReportId={0}", id, "查看 - ")));
                }
                else
                {
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("DrillPlanHalfYearReportEdit.aspx?DrillPlanHalfYearReportId={0}", id, "编辑 - ")));
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
                foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                {
                    string rowID = Grid1.DataKeys[rowIndex][0].ToString();
                    var getV = BLL.ProjectDrillPlanHalfYearReportService.GetDrillPlanHalfYearReportById(rowID);
                    if (getV != null)
                    {
                        BLL.LogService.AddSys_Log(this.CurrUser, getV.YearId.ToString() + "-" +getV.HalfYearId.ToString(), getV.DrillPlanHalfYearReportId, BLL.Const.ProjectDrillPlanHalfYearReportMenuId, BLL.Const.BtnDelete);
                        BLL.ProjectDrillPlanHalfYearReportItemService.DeleteDrillPlanHalfYearReportItemList(rowID);
                        BLL.ProjectDrillPlanHalfYearReportService.DeleteDrillPlanHalfYearReportById(rowID);
                    } }

                this.BindGrid();
                ShowNotify("删除数据成功!", MessageBoxIcon.Success);
            }
        }
        #endregion

        #region 打印
        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnPrint_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Grid1.SelectedRowID))
            {
                PageContext.RegisterStartupScript(Window2.GetShowReference(String.Format("~/ReportPrint/ExReportPrint.aspx?reportId={0}&&replaceParameter={1}&&varValue={2}", Const.ProjectInformation_DrillPlanHalfYearReportId, Grid1.SelectedRowID, "", "打印 - ")));
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
            var buttonList = BLL.CommonService.GetAllButtonList(this.ProjectId, this.CurrUser.UserId, BLL.Const.ProjectDrillPlanHalfYearReportMenuId);
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
    }
}