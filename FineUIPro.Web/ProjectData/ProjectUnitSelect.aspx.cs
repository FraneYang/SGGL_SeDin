using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace FineUIPro.Web.ProjectData
{
    public partial class ProjectUnitSelect : PageBase
    {
        /// <summary>
        /// 定义项
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
                this.ProjectId = Request.QueryString["ProjectId"];
                this.btnNew.OnClientClick = Window1.GetShowReference("../SysManage/UnitEdit.aspx") + "return false;";
                if (this.CurrUser != null && this.CurrUser.PageSize.HasValue)
                {
                    Grid1.PageSize = this.CurrUser.PageSize.Value;
                } 
                this.ddlPageSize.SelectedValue = Grid1.PageSize.ToString();             
                // 绑定表格
                this.BindGrid();
            }
        }
        #endregion

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            if (!string.IsNullOrEmpty(this.ProjectId))
            {
                string strSql = @"SELECT UnitId,UnitCode,UnitName,ProjectRange,Address,UnitType.UnitTypeName"
                                + @" FROM Base_Unit AS Unit LEFT JOIN Base_UnitType AS UnitType ON Unit.UnitTypeId =UnitType.UnitTypeId"
                                + @" WHERE  UnitId NOT IN (SELECT UnitId FROM Project_ProjectUnit WHERE ProjectId =@ProjectId) ";
                List<SqlParameter> listStr = new List<SqlParameter>();
                listStr.Add(new SqlParameter("@ProjectId", this.ProjectId));
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
            }
        }
                
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

        /// <summary>
        /// 确定按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSure_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            foreach (int rowIndex in Grid1.SelectedRowIndexArray)
            {
                this.SaveData(Grid1.DataKeys[rowIndex][0].ToString());
            }
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }

        /// <summary>
        /// 双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }

            this.SaveData(Grid1.SelectedRowID);
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }
                                
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

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="unitId"></param>
        private void SaveData(string unitId)
        {
            var projectUnit = BLL.ProjectUnitService.GetProjectUnitByUnitIdProjectId(this.ProjectId, unitId);
            if (projectUnit == null)
            {
                Model.Project_ProjectUnit newProjectUnit = new Model.Project_ProjectUnit
                {
                    ProjectId = this.ProjectId,
                    UnitId = unitId,
                    InTime = System.DateTime.Now
                };
                BLL.ProjectUnitService.AddProjectUnit(newProjectUnit);
            }
        }
    }
}