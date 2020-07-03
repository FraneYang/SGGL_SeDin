using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using AspNet = System.Web.UI.WebControls;

namespace FineUIPro.Web.HSSE.Technique
{
    public partial class HazardListOut : PageBase
    {
        #region 定义变量
        /// <summary>
        /// 主键
        /// </summary>
        public string IsCompany
        {
            get
            {
                return (string)ViewState["IsCompany"];
            }
            set
            {
                ViewState["IsCompany"] = value;
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
                this.IsCompany = Request.Params["IsCompany"];
                this.BindGrid();
            }
        }
        #endregion

        #region 绑定Grid
        /// <summary>
        /// 绑定Grid
        /// </summary>
        private void BindGrid()
        {
            string strSql = @"SELECT H.HazardId, ST.HazardListTypeCode AS STHazardListTypeCode,ST.HazardListTypeName AS STHazardListTypeName,T.HazardListTypeCode,T.HazardListTypeName,
                    H.HazardCode,H.HazardItems,H.DefectsType,H.MayLeadAccidents,H.HelperMethod,H.HazardJudge_L,H.HazardJudge_E,H.HazardJudge_C,H.HazardJudge_D,Const0007.ConstText AS HazardLevel,H.ControlMeasures
                    FROM Technique_HazardList AS H
                    LEFT JOIN Technique_HazardListType AS T ON T.HazardListTypeId=H.HazardListTypeId
                    LEFT JOIN Technique_HazardListType AS ST ON T.SupHazardListTypeId=ST.HazardListTypeId
                    LEFT JOIN Sys_Const AS Const0007 ON Const0007.ConstValue = H.HazardLevel and Const0007.GroupId='0007'
                    WHERE 1=1 ";
            List<SqlParameter> listStr = new List<SqlParameter>();
            //if (!string.IsNullOrEmpty(this.HazardCode.Text.Trim()))
            //{
            //    strSql += " AND HazardCode LIKE @HazardCode";
            //    listStr.Add(new SqlParameter("@HazardCode", "%" + this.HazardCode.Text.Trim() + "%"));
            //}
            //if (!string.IsNullOrEmpty(this.HazardListTypeCode.Text.Trim()))
            //{
            //    strSql += " AND HazardListTypeCode LIKE @HazardListTypeCode";
            //    listStr.Add(new SqlParameter("@HazardListTypeCode", "%" + this.HazardListTypeCode.Text.Trim() + "%"));
            //}
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            Grid1.RecordCount = tb.Rows.Count;
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();
        }
        #endregion
        
        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnOut_Click(object sender, EventArgs e)
        {
            Response.ClearContent();
            string filename = Funs.GetNewFileName();
            Response.AddHeader("content-disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode("危险源清单" + filename, System.Text.Encoding.UTF8) + ".xls");
            Response.ContentType = "application/excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            this.Grid1.PageSize = Grid1.RecordCount;
            BindGrid();
            Response.Write(GetGridTableHtmlPage(Grid1));
            Response.End();
        }

        #region 导出方法
        /// <summary>
        /// 导出方法
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        public static string GetGridTableHtmlPage(Grid grid)
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
                    if (column.ColumnID == "tfNumber" && (row.FindControl("lbNumber") as AspNet.Label) != null)
                    {
                        html = (row.FindControl("lbNumber") as AspNet.Label).Text;
                    }
                    if (column.ColumnID == "tfSTHazardListTypeCode" && (row.FindControl("lbSTHazardListTypeCode") as AspNet.Label) != null)
                    {
                        html = (row.FindControl("lbSTHazardListTypeCode") as AspNet.Label).Text;
                    }
                    if (column.ColumnID == "tfSTHazardListTypeName" && (row.FindControl("lbSTHazardListTypeName") as AspNet.Label) != null)
                    {
                        html = (row.FindControl("lbSTHazardListTypeName") as AspNet.Label).Text;
                    }
                    if (column.ColumnID == "tfHazardListTypeCode" && (row.FindControl("lbHazardListTypeCode") as AspNet.Label) != null)
                    {
                        html = (row.FindControl("lbHazardListTypeCode") as AspNet.Label).Text;
                    }
                    if (column.ColumnID == "tfHazardListTypeName" && (row.FindControl("lbHazardListTypeName") as AspNet.Label) != null)
                    {
                        html = (row.FindControl("lbHazardListTypeName") as AspNet.Label).Text;
                    }
                    if (column.ColumnID == "tfHazardCode" && (row.FindControl("lbHazardCode") as AspNet.Label) != null)
                    {
                        html = (row.FindControl("lbHazardCode") as AspNet.Label).Text;
                    }
                    if (column.ColumnID == "tfHazardItems" && (row.FindControl("lbHazardItems") as AspNet.Label) != null)
                    {
                        html = (row.FindControl("lbHazardItems") as AspNet.Label).Text;
                    }
                    if (column.ColumnID == "tfDefectsType" && (row.FindControl("lbDefectsType") as AspNet.Label) != null)
                    {
                        html = (row.FindControl("lbDefectsType") as AspNet.Label).Text;
                    }
                    if (column.ColumnID == "tfMayLeadAccidents" && (row.FindControl("lbMayLeadAccidents") as AspNet.Label) != null)
                    {
                        html = (row.FindControl("lbMayLeadAccidents") as AspNet.Label).Text;
                    }
                    if (column.ColumnID == "tfHelperMethod" && (row.FindControl("lbHelperMethod") as AspNet.Label) != null)
                    {
                        html = (row.FindControl("lbHelperMethod") as AspNet.Label).Text;
                    }
                    if (column.ColumnID == "tfControlMeasures" && (row.FindControl("lbControlMeasures") as AspNet.Label) != null)
                    {
                        html = (row.FindControl("lbControlMeasures") as AspNet.Label).Text;
                    }
                    sb.AppendFormat("<td>{0}</td>", html);
                }

                sb.Append("</tr>");
            }

            sb.Append("</table>");

            return sb.ToString();
        }
        #endregion

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
    }
}