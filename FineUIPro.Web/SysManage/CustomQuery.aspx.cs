namespace FineUIPro.Web.SysManage
{
    using BLL;
    using System;
    using System.Data;
    using System.Text;
    using System.Web.UI.WebControls;
    using AspNet = System.Web.UI.WebControls;

    public partial class CustomQuery : PageBase
    {
        /// <summary>
        /// 加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {           
            if (!IsPostBack)
            {
                // 绑定表格
                this.BindGrid();
            }
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            this.lbCount.Text = "0 行";
            gvHazard.DataSource = null;
            gvHazard.DataBind();
            string strSql = this.txtCustomQuery.Text.Trim();
            if (!string.IsNullOrEmpty(strSql))
            {
                DataTable table = SQLHelper.GetDataTableRunText(strSql, null);
                if (table.Rows.Count > 0)
                {
                    this.lbCount.Text = table.Rows.Count.ToString()+" 行";
                    gvHazard.DataSource = table;
                    gvHazard.DataBind();
                }
                else
                {
                    ShowNotify("没有满足条件的数据，请检查查询语句！", MessageBoxIcon.Warning);
                }
            }
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            BindGrid();
        }

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuOut_Click(object sender, EventArgs e)
        {
            string strSql = this.txtCustomQuery.Text.Trim();
            if (!string.IsNullOrEmpty(strSql))
            {
                DataTable table = SQLHelper.GetDataTableRunText(strSql, null);
                Response.ClearContent();
                string filename = Funs.GetNewFileName();
                Response.AddHeader("content-disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode("查询结果" + filename, System.Text.Encoding.UTF8) + ".xls");
                Response.ContentType = "application/excel";
                Response.ContentEncoding = System.Text.Encoding.UTF8;
                Response.Write(GetTableHtml(table));
                Response.End();
            }
        }
    }
}