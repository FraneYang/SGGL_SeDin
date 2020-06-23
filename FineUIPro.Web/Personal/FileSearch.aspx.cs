using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace FineUIPro.Web.Personal
{
    public partial class FileSearch : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (this.CurrUser != null && this.CurrUser.PageSize.HasValue)
                {
                    Grid1.PageSize = this.CurrUser.PageSize.Value;
                }
                this.ddlPageSize.SelectedValue = Grid1.PageSize.ToString();                    
            }
        }

        #region BindGrid
        /// <summary>
        ///  gv 绑定数据
        /// </summary>
        private void BindGrid()
        {
            if (!string.IsNullOrEmpty(this.txtName.Text.Trim()))
            {
                SqlParameter[] parameter = new SqlParameter[]
                       {
                    new SqlParameter("@Name", this.txtName.Text.Trim()),
                           //new SqlParameter("@StartTime", !string.IsNullOrEmpty(this.txtStartTime.Text)?this.txtStartTime.Text:null),                  
                       };
                DataTable tb = SQLHelper.GetDataTableRunProc("SpFileSearch", parameter);
                // 2.获取当前分页数据
                //var table = this.GetPagedDataTable(Grid1, tb1);

                Grid1.RecordCount = tb.Rows.Count;
                tb = GetFilteredTable(Grid1.FilteredData, tb);
                var table = this.GetPagedDataTable(Grid1, tb);

                Grid1.DataSource = table;
                Grid1.DataBind();
            }
            else
            {
                Grid1.DataSource = null;
                Grid1.DataBind();
            }

            if (Grid1.RecordCount == 0)
            {
                Alert.ShowInTop("没有找到符合条件的数据！", MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region 查询
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.BindGrid();
        }
        #endregion

        #region 分页下拉选择
        /// <summary>
        /// 分页下拉选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Grid1.PageSize = Funs.GetNewIntOrZero(this.ddlPageSize.SelectedValue);
            this.BindGrid();
        }

        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            BindGrid();
        }


        protected void Grid1_Sort(object sender, FineUIPro.GridSortEventArgs e)
        {
            BindGrid();
        }
        #endregion

        #region 数据编辑事件
        /// <summary>
        /// Grid行双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            this.ViewData();
        }
      
        /// <summary>
        /// 右键编辑事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuView_Click(object sender, EventArgs e)
        {
            this.ViewData();
        }

        /// <summary>
        /// 编辑数据方法
        /// </summary>
        private void ViewData()
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
                       
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format(this.Grid1.SelectedRow.Values[6].ToString()+ "&value=0", Grid1.SelectedRowID, "查看 - ")));
        }
        #endregion
    }
}