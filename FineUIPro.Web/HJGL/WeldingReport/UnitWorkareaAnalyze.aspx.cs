using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Data;
using BLL;
using System.Text;
using Newtonsoft.Json.Linq;
using AspNet = System.Web.UI.WebControls;

namespace FineUIPro.Web.HJGL.WeldingReport
{
    public partial class UnitWorkareaAnalyze : PageBase
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
                BLL.UnitWorkService.InitUnitWorkDropDownList(this.drpUnitWork, this.CurrUser.LoginProjectId,  true);//区域
                //钢材类型
                this.drpSteelType.DataTextField = "Text";
                this.drpSteelType.DataValueField = "Value";
                this.drpSteelType.DataSource = BLL.DropListService.HJGL_GetSteTypeList();
                this.drpSteelType.DataBind();
                Funs.FineUIPleaseSelect(this.drpSteelType, "请选择");

                //显示列
                Model.Sys_UserShowColumns c = BLL.UserShowColumnsService.GetColumnsByUserId(this.CurrUser.UserId, "UnitWorkAreaAnalyze");
                if (c != null)
                {
                    this.GetShowColumn(c.Columns);
                }
            }
        }


        /// <summary>
        /// 数据表
        /// </summary>
        private DataTable tb = null;

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            List<SqlParameter> listStr = new List<SqlParameter>();
            listStr.Add(new SqlParameter("@projectId", this.CurrUser.LoginProjectId));
            if (this.drpUnitId.SelectedValue != BLL.Const._Null)
            {
                listStr.Add(new SqlParameter("@UnitNo", this.drpUnitId.SelectedValue));
            }
            else
            {
                listStr.Add(new SqlParameter("@UnitNo", null));
            }
            if (this.drpUnitWork.SelectedValue != BLL.Const._Null)
            {
                listStr.Add(new SqlParameter("@UnitWorkId", this.drpUnitWork.SelectedValue));
            }
            else
            {
                listStr.Add(new SqlParameter("@UnitWorkId", null));
            }
            if (this.drpSteelType.SelectedValue != BLL.Const._Null)
            {
                listStr.Add(new SqlParameter("@ste_steeltype", this.drpSteelType.SelectedValue));
            }
            else
            {
                listStr.Add(new SqlParameter("@ste_steeltype", null));
            }
            if (!string.IsNullOrEmpty(this.txtStarTime.Text))
            {
                listStr.Add(new SqlParameter("@startTime", this.txtStarTime.Text.Trim()));
            }
            else
            {
                listStr.Add(new SqlParameter("@startTime", null));
            }
            if (!string.IsNullOrEmpty(this.txtEndTime.Text))
            {
                listStr.Add(new SqlParameter("@endTime", this.txtEndTime.Text.Trim()));
            }
            else
            {
                listStr.Add(new SqlParameter("@endTime", null));
            }
            SqlParameter[] parameter = listStr.ToArray();
            tb = SQLHelper.GetDataTableRunProc("sp_rpt_UnitWorkareaAnalyze", parameter);
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


        /// <summary>
        /// 全部行合计
        /// </summary>
        private void OutputSummaryData()
        {
            if (tb != null)
            {
               
                int total_jot = 0;
                int total_sjot = 0;
                int total_fjot = 0;
                int cut_total_jot = 0;
                double total_din = 0.0f;
                double total_Sdin = 0.0f;
                double total_Fdin = 0.0f;
                int finished_total_jot_bq = 0;
                int finished_total_sjot_bq = 0;
                int finished_total_fjot_bq = 0;
                double finished_total_din_bq = 0.0f;
                double finished_total_Sdin_bq = 0.0f;
                double finished_total_Fdin_bq = 0.0f;

                int finished_total_jot = 0;
                int finished_total_sjot = 0;
                int finished_total_fjot = 0;
                double finished_total_din = 0.0f;
                double finished_total_sdin = 0.0f;
                double finished_total_Fdin = 0.0f;

                //string filmPassRate = "";
                //string jointPassRate = "";

                foreach (DataRow row in tb.Rows)
                {
                    total_jot += Convert.ToInt32(row["total_jot"]);
                    total_sjot += Convert.ToInt32(row["total_sjot"]);
                    total_fjot += Convert.ToInt32(row["total_fjot"]);
                    cut_total_jot += Convert.ToInt32(row["cut_total_jot"]);
                    total_din += Convert.ToDouble(row["total_din"]);
                    total_Sdin += Convert.ToDouble(row["total_Sdin"]);
                    total_Fdin += Convert.ToDouble(row["total_Fdin"]);
                    finished_total_jot_bq += Convert.ToInt32(row["finished_total_jot_bq"]);
                    finished_total_sjot_bq += Convert.ToInt32(row["finished_total_sjot_bq"]);
                    finished_total_fjot_bq += Convert.ToInt32(row["finished_total_fjot_bq"]);
                    finished_total_din_bq += Convert.ToDouble(row["finished_total_din_bq"]);
                    finished_total_Sdin_bq += Convert.ToDouble(row["finished_total_Sdin_bq"]);
                    finished_total_Fdin_bq += Convert.ToDouble(row["finished_total_Fdin_bq"]);

                    finished_total_jot += Convert.ToInt32(row["finished_total_jot"]);
                    finished_total_sjot += Convert.ToInt32(row["finished_total_sjot"]);
                    finished_total_fjot += Convert.ToInt32(row["finished_total_fjot"]);
                    finished_total_din += Convert.ToDouble(row["finished_total_din"]);
                    finished_total_sdin += Convert.ToDouble(row["finished_total_sdin"]);
                    finished_total_Fdin += Convert.ToDouble(row["finished_total_Fdin"]);

                }
                //if (totalfilm != 0)
                //{
                //    filmPassRate = (totalPassfilm * 100.0 / totalfilm * 1.0).ToString("0.00") + "%";
                //}
                //if (JointNum1 != 0)
                //{
                //    jointPassRate = (JointPassNum1 * 100.0 / JointNum1 * 1.0).ToString("0.00") + "%";
                //}

                JObject summary = new JObject();
                summary.Add("tfNumber", "合计");
                summary.Add("total_jot", total_jot.ToString());
                summary.Add("total_sjot", total_sjot.ToString());
                summary.Add("total_fjot", total_fjot.ToString());
                summary.Add("cut_total_jot", cut_total_jot.ToString());
                summary.Add("total_din", total_din.ToString("F2"));
                summary.Add("total_Sdin", total_Sdin.ToString("F2"));
                summary.Add("total_Fdin", total_Fdin.ToString("F2"));
                summary.Add("finished_total_jot_bq", finished_total_jot_bq.ToString());
                summary.Add("finished_total_sjot_bq", finished_total_sjot_bq.ToString());
                summary.Add("finished_total_fjot_bq", finished_total_fjot_bq.ToString());
                summary.Add("finished_total_din_bq", finished_total_din_bq.ToString("F2"));
                summary.Add("finished_total_Sdin_bq", finished_total_Sdin_bq.ToString("F2"));
                summary.Add("finished_total_Fdin_bq", finished_total_Fdin_bq.ToString("F2"));

                summary.Add("finished_total_jot", finished_total_jot.ToString());
                summary.Add("finished_total_sjot", finished_total_sjot.ToString());
                summary.Add("finished_total_fjot", finished_total_fjot.ToString());
                summary.Add("finished_total_din", finished_total_din.ToString("F2"));
                summary.Add("finished_total_sdin", finished_total_sdin.ToString("F2"));
                summary.Add("finished_total_Fdin", finished_total_Fdin.ToString("F2"));

                Grid1.SummaryData = summary;
            }

        }

        #region 选择要显示列
        /// <summary>
        /// 选择显示列
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSelectColumn_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("UnitWorkAreaShowColumn.aspx", "显示列 - ")));
        }

        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
            this.BindGrid();
            // 显示列
            Model.Sys_UserShowColumns c = BLL.UserShowColumnsService.GetColumnsByUserId(this.CurrUser.UserId, "UnitWorkAreaAnalyze");
            if (c != null)
            {
                this.GetShowColumn(c.Columns);
            }
        }

        /// <summary>
        /// 显示的列
        /// </summary>
        /// <param name="column"></param>
        private void GetShowColumn(string column)
        {
            if (!string.IsNullOrEmpty(column))
            {
                this.Grid1.Columns[0].Hidden = true;
                this.Grid1.Columns[1].Hidden = true;
                this.Grid1.Columns[2].Hidden = true;
                this.Grid1.Columns[3].Hidden = true;
                this.Grid1.Columns[4].Hidden = true;
                this.Grid1.Columns[5].Hidden = true;
                this.Grid1.Columns[6].Hidden = true;
                this.Grid1.Columns[7].Hidden = true;
                this.Grid1.Columns[8].Hidden = true;
                this.Grid1.Columns[9].Hidden = true;
                this.Grid1.Columns[10].Hidden = true;
                this.Grid1.Columns[11].Hidden = true;
                this.Grid1.Columns[12].Hidden = true;
                this.Grid1.Columns[13].Hidden = true;
                this.Grid1.Columns[14].Hidden = true;
                this.Grid1.Columns[15].Hidden = true;
                this.Grid1.Columns[16].Hidden = true;
                this.Grid1.Columns[17].Hidden = true;
                this.Grid1.Columns[18].Hidden = true;
                this.Grid1.Columns[19].Hidden = true;
                this.Grid1.Columns[20].Hidden = true;
                this.Grid1.Columns[21].Hidden = true;
                this.Grid1.Columns[22].Hidden = true;
                this.Grid1.Columns[23].Hidden = true;
                this.Grid1.Columns[24].Hidden = true;
                this.Grid1.Columns[25].Hidden = true;
                this.Grid1.Columns[26].Hidden = true;
                this.Grid1.Columns[27].Hidden = true;
                this.Grid1.Columns[28].Hidden = true;
                this.Grid1.Columns[29].Hidden = true;
                this.Grid1.Columns[30].Hidden = true;
                this.Grid1.Columns[31].Hidden = true;
                this.Grid1.Columns[32].Hidden = true;
                this.Grid1.Columns[33].Hidden = true;
                this.Grid1.Columns[34].Hidden = true;
                
                List<string> columns = column.Split(',').ToList();
                foreach (var item in columns)
                {
                    this.Grid1.Columns[Convert.ToInt32(item)].Hidden = false;
                }
            }
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
            OutputSummaryData();
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
            Response.AddHeader("content-disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode("单位工区进度分析" + filename, System.Text.Encoding.UTF8) + ".xls");
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
        //protected void drpUnitId_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (this.drpUnitId.SelectedValue != BLL.Const._Null && this.drpInstallationId.SelectedValue != BLL.Const._Null)
        //    {
        //        BLL.UnitWorkService.InitUnitWorkDropDownList(this.drpWorkAreaId, this.CurrUser.LoginProjectId,  true);//区域
        //    }
        //    else
        //    {
        //        Funs.FineUIPleaseSelect(this.drpWorkAreaId, "请选择");
        //    }
        //    this.drpWorkAreaId.SelectedValue = BLL.Const._Null;
        //}
        #endregion
    }
}