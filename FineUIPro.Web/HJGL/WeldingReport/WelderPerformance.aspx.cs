using System;
using System.Collections.Generic;
using AspNet = System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using BLL;
using System.Text;

namespace FineUIPro.Web.HJGL.WeldingReport
{
    public partial class WelderPerformance : PageBase
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

                BLL.UnitService.InitUnitByProjectIdUnitTypeDropDownList(this.drpUnitId, this.CurrUser.LoginProjectId, BLL.Const.ProjectUnitType_2, true);
                //BLL.Project_InstallationService.InitInstallationDropDownList(this.drpInstallationId, true, this.CurrUser.LoginProjectId, Resources.Lan.PleaseSelect);
                BLL.UnitWorkService.InitUnitWorkDropDownList(this.drpWorkAreaId, this.CurrUser.LoginProjectId, true);//区域

                BLL.Base_MaterialService.InitMaterialDropDownList(this.drpMaterialId, true,"请选择");//材质
                BLL.WelderService.InitProjectWelderDropDownList(this.drpWelderId, true, this.CurrUser.LoginProjectId, this.drpUnitId.SelectedValue, "请选择");//焊工
            }
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            List<SqlParameter> listStr = new List<SqlParameter>();

            listStr.Add(new SqlParameter("@projectId", this.CurrUser.LoginProjectId));
            if (this.drpUnitId.SelectedValue != BLL.Const._Null)
            {
                listStr.Add(new SqlParameter("@unitcode", this.drpUnitId.SelectedValue));
            }
            else
            {
                listStr.Add(new SqlParameter("@unitcode", null));
            }
            if (this.drpInstallationId.SelectedValue != BLL.Const._Null)
            {
                listStr.Add(new SqlParameter("@installationId", this.drpInstallationId.SelectedValue));
            }
            else
            {
                listStr.Add(new SqlParameter("@installationId", null));
            }
            if (this.drpWorkAreaId.SelectedValue != BLL.Const._Null)
            {
                listStr.Add(new SqlParameter("@workareacode", this.drpWorkAreaId.SelectedValue));
            }
            else
            {
                listStr.Add(new SqlParameter("@workareacode", null));
            }
            if (!string.IsNullOrEmpty(this.txtStarTime.Text))
            {
                listStr.Add(new SqlParameter("@date1", this.txtStarTime.Text.Trim()));
            }
            else
            {
                listStr.Add(new SqlParameter("@date1", null));
            }
            if (!string.IsNullOrEmpty(this.txtEndTime.Text))
            {
                listStr.Add(new SqlParameter("@date2", this.txtEndTime.Text.Trim()));
            }
            else
            {
                listStr.Add(new SqlParameter("@date2", null));
            }
            if (this.drpMaterialId.SelectedValue != BLL.Const._Null)
            {
                listStr.Add(new SqlParameter("@steel", this.drpMaterialId.SelectedValue));
            }
            else
            {
                listStr.Add(new SqlParameter("@steel", null));
            }
            if (this.drpWelderId.SelectedValue != BLL.Const._Null)
            {
                listStr.Add(new SqlParameter("@wloName", this.drpWelderId.SelectedValue));
            }
            else
            {
                listStr.Add(new SqlParameter("@wloName", null));
            }
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunProc("sp_rpt_WelderPerformance", parameter);
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
            Response.AddHeader("content-disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode("焊工业绩分析" + filename, System.Text.Encoding.UTF8) + ".xls");
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
            sb.Append("<meta http-equiv=\"content-type\" content=\"application/excel; charset=UTF-8\"/>");
            sb.Append("<table cellspacing=\"0\" rules=\"all\" border=\"1\" style=\"border-collapse:collapse;\">");
            sb.Append("<tr>");
            grid.PageSize = 10000;
            BindGrid();
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
                    sb.AppendFormat("<td>{0}</td>", html);
                }

                sb.Append("</tr>");
            }

            sb.Append("</table>");

            return sb.ToString();
        }
        #endregion

        #region 下拉选择事件
        /// <summary>
        /// 单位下拉选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpUnitId_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.drpWorkAreaId.Items.Clear();
            this.drpWelderId.Items.Clear();
            if (this.drpUnitId.SelectedValue != BLL.Const._Null && this.drpInstallationId.SelectedValue != BLL.Const._Null)
            {
                BLL.UnitWorkService.InitUnitWorkDropDownList(this.drpWorkAreaId, this.CurrUser.LoginProjectId, true);//区域
            }
            else
            {
                Funs.FineUIPleaseSelect(this.drpWorkAreaId, "请选择");
            }
            this.drpWorkAreaId.SelectedValue = BLL.Const._Null;

            if (this.drpUnitId.SelectedValue != BLL.Const._Null)
            {
                BLL.WelderService.InitProjectWelderDropDownList(this.drpWelderId, true, this.CurrUser.LoginProjectId, this.drpUnitId.SelectedValue, "请选择");//焊工
            }
            else
            {
                Funs.FineUIPleaseSelect(this.drpWelderId, "请选择");
            }
            this.drpWelderId.SelectedValue = BLL.Const._Null;
        }
        #endregion

        #region 格式化字符串
        /// <summary>
        /// 是否在岗
        /// </summary>
        /// <param name="isOnDuty"></param>
        /// <returns></returns>
        protected string ConvertIsOnDuty(object isOnDuty)
        {
            if (isOnDuty != null)
            {
                if (isOnDuty.ToString() == "True")
                {
                    return "是";
                }
                else
                {
                    return "否";
                }
            }
            return null;
        }
        #endregion
    }
}