using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using AspNet = System.Web.UI.WebControls;

namespace FineUIPro.Web.HSSE.QualityAudit
{
    public partial class SubUnitQuality : PageBase
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
                ////权限按钮方法
                this.GetButtonPower();
                this.ProjectId = this.CurrUser.LoginProjectId;
                if (!string.IsNullOrEmpty(Request.Params["projectId"]) && Request.Params["projectId"] != this.CurrUser.LoginProjectId)
                {
                    this.ProjectId = Request.Params["projectId"];
                }
                //this.btnNew.OnClientClick = Window1.GetShowReference("SubUnitQualityEdit.aspx") + "return false;";
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
            string strSql = @"SELECT Unit.UnitId,"
                          + @"Unit.UnitCode,"
                          + @"Unit.UnitName,"
                          + @"Unit.UnitTypeId,"
                          + @"Unit.Telephone,"
                          + @"Unit.EMail,"
                          + @"ProjectUnit.ProjectId,"
                          + @"SubUnitQuality.SubUnitQualityId, "
                          + @"SubUnitQuality.SubUnitQualityName, "
                          + @"SubUnitQuality.BusinessLicense, "
                          + @"SubUnitQuality.BL_EnableDate, "
                          + @"SubUnitQuality.OrganCode, "
                          + @"SubUnitQuality.O_EnableDate, "
                          + @"SubUnitQuality.Certificate, "
                          + @"SubUnitQuality.C_EnableDate, "
                          + @"SubUnitQuality.QualityLicense, "
                          + @"SubUnitQuality.QL_EnableDate, "
                          + @"SubUnitQuality.HSELicense, "
                          + @"SubUnitQuality.H_EnableDate, "
                          + @"SubUnitQuality.SecurityLicense, "
                          + @"SubUnitQuality.SL_EnableDate "
                          + @" FROM Base_Unit AS Unit "
                          + @" LEFT JOIN Base_UnitType AS UnitType ON UnitType.UnitTypeId = Unit.UnitTypeId"
                          + @" LEFT JOIN Project_ProjectUnit AS ProjectUnit ON ProjectUnit.UnitId = Unit.UnitId"
                          + @" LEFT JOIN QualityAudit_SubUnitQuality AS SubUnitQuality ON SubUnitQuality.UnitId = Unit.UnitId WHERE ProjectUnit.UnitType='" + BLL.Const.ProjectUnitType_2 +"'";
            List<SqlParameter> listStr = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(this.CurrUser.LoginProjectId))
            {
                strSql += " AND ProjectUnit.ProjectId = @ProjectId";
                listStr.Add(new SqlParameter("@ProjectId", this.CurrUser.LoginProjectId));
            }
            if (BLL.ProjectUnitService.GetProjectUnitTypeByProjectIdUnitId(this.ProjectId, this.CurrUser.UnitId))
            {
                strSql += " AND Unit.UnitId = @UnitId";  ///状态为已完成
                listStr.Add(new SqlParameter("@UnitId", this.CurrUser.UnitId));
            }
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            Grid1.RecordCount = tb.Rows.Count;
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
            var unit = BLL.UnitService.GetUnitByUnitId(id);
            if (unit != null)
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("SubUnitQualityEdit.aspx?UnitId={0}", id, "编辑 - ")));
            }
        }
        #endregion

        #region Grid点击事件
        /// <summary>
        /// Grid1行点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_RowCommand(object sender, GridCommandEventArgs e)
        {
            string id = Grid1.DataKeys[e.RowIndex][0].ToString();
            if (e.CommandName == "auditDetail")
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("SubUnitQualityAudit.aspx?UnitId={0}", id, "审查记录 - ")));
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
            //if (Request.Params["value"] == "0")
            //{
            //    return;
            //}
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.SubUnitQualityMenuId);
            if (buttonList.Count() > 0)
            {
                //if (buttonList.Contains(BLL.Const.BtnAdd))
                //{
                //    this.btnNew.Hidden = false;
                //}
                if (buttonList.Contains(BLL.Const.BtnModify))
                {
                    this.btnMenuEdit.Hidden = false;
                }
                //if (buttonList.Contains(BLL.Const.BtnDelete))
                //{
                //    this.btnMenuDelete.Hidden = false;
                //}
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
            Response.AddHeader("content-disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode("分包商资质" + filename, System.Text.Encoding.UTF8) + ".xls");
            Response.ContentType = "application/excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            this.Grid1.PageSize = 5000;
            BindGrid();
            Response.Write(GetGridTableHtml(Grid1));
            Response.End();
        }
        #endregion
    }
}