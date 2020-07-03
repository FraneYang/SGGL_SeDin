using System;
using System.Collections.Generic;
using System.Text;
using BLL;
using System.Data.SqlClient;
using System.Data;
using AspNet = System.Web.UI.WebControls;

namespace FineUIPro.Web.HJGL.WeldingReport
{
    public partial class IsoCompreInfo : PageBase
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
                this.ddlPageSize.SelectedValue = this.Grid1.PageSize.ToString();
                BLL.UnitWorkService.InitUnitWorkDropDownList(this.drpWorkAreaId, this.CurrUser.LoginProjectId, true);//区域
            }
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            List<SqlParameter> listStr = new List<SqlParameter>();
            listStr.Add(new SqlParameter("@projectId", this.CurrUser.LoginProjectId));
            if (this.drpWorkAreaId.SelectedValue != BLL.Const._Null)
            {
                listStr.Add(new SqlParameter("@workAreaId", this.drpWorkAreaId.SelectedValue));
            }
            else
            {
                listStr.Add(new SqlParameter("@workAreaId", null));
            }
            if (!string.IsNullOrEmpty(this.txtPipelineCode.Text.Trim()))
            {
                listStr.Add(new SqlParameter("@pipelineCode", this.txtPipelineCode.Text.Trim()));
            }
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunProc("sp_rpt_IsoCompreInfo", parameter);
            this.Grid1.RecordCount = tb.Rows.Count;
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
            this.BindGrid();
        }
        #endregion

        #region 统计按钮事件
        /// <summary>
        /// 统计
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnAnalyse_Click(object sender, EventArgs e)
        {
            BindGrid();
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
            Response.AddHeader("content-disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode("管线综合分析" + filename, System.Text.Encoding.UTF8) + ".xls");
            Response.ContentType = "application/excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.Write(GetGridTableHtml(Grid1));
            Response.End();
        }

        /// <summary>
        /// 导出方法
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        private string GetGridTableHtml(Grid grid)
        {
            StringBuilder sb = new StringBuilder();
            grid.PageSize = 10000;
            BindGrid();
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
                        html = (row.FindControl("labNumber") as AspNet.Label).Text;
                    }
                    if (column.ColumnID == "tfIsHot")
                    {
                        html = (row.FindControl("lblIsHot") as AspNet.Label).Text;
                    }
                    sb.AppendFormat("<td>{0}</td>", html);
                }

                sb.Append("</tr>");
            }

            sb.Append("</table>");

            return sb.ToString();
        }
        #endregion

        #region 格式化字符串
        protected string ConvertDetectionType(object detectionType)
        {
            string detectionName = string.Empty;
            if (detectionType != null)
            {
                string[] types = detectionType.ToString().Split('|');
                foreach (string t in types)
                {
                    var type = BLL.Base_DetectionTypeService.GetDetectionTypeByDetectionTypeId(t);
                    if (type != null)
                    {
                        detectionName += type.DetectionTypeName + ",";
                    }
                }
            }
            if (detectionName != string.Empty)
            {
                return detectionName.Substring(0, detectionName.Length - 1);
            }
            else
            {
                return "";
            }
        }
        #endregion
    }
}