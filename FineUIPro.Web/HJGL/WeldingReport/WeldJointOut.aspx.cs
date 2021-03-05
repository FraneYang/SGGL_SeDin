using System;
using System.Collections.Generic;
using AspNet = System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Linq;
using System.Data;
using BLL;
using System.Text;

namespace FineUIPro.Web.HJGL.WeldingReport
{
    public partial class WeldJointOut : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.ddlPageSize.SelectedValue = this.Grid1.PageSize.ToString();

                BLL.UnitWorkService.InitAZUnitWorkDownList(this.drpUnitWork, this.CurrUser.LoginProjectId, true);
                Funs.FineUIPleaseSelect(this.drpPipeline);
                //显示列
                Model.Sys_UserShowColumns c = BLL.UserShowColumnsService.GetColumnsByUserId(this.CurrUser.UserId, "JointOut");
                if (c != null)
                {
                    this.GetShowColumn(c.Columns);
                }
            }
        }

        #region
        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            string strSql = @"SELECT WeldJointId,WeldJointCode,PipelineId,PipelineCode,JointAttribute,
                                     ComponentsCode1,ComponentsCode2,IsWelding,IsHotProessStr,Material1Code,Material2Code,
                                     WeldTypeCode,Specification,HeartNo1,HeartNo2,Size,Dia,Thickness,GrooveTypeCode,
                                     WeldingMethodCode,WeldingWireCode,WeldingRodCode,WeldingDate,WeldingDailyCode,
                                     BackingWelderCode,CoverWelderCode,MediumCode ,PreTemperature,JointArea,WPQCode,Remark
                              FROM View_HJGL_WeldJoint WHERE 1= 1";
            List<SqlParameter> listStr = new List<SqlParameter> { };
            if (this.drpUnitWork.SelectedValue != BLL.Const._Null)
            {
                strSql += " AND UnitWorkId = @UnitWorkId";
                listStr.Add(new SqlParameter("@UnitWorkId", this.drpUnitWork.SelectedValue));
            }
            if (this.drpPipeline.SelectedValueArray.Length > 1 || (this.drpPipeline.SelectedValueArray.Length == 1 && this.drpPipeline.SelectedValue != BLL.Const._Null))
            {
                string pipelineIds = string.Empty;
                foreach (var item in this.drpPipeline.SelectedValueArray)
                {
                    pipelineIds += item + ",";
                }
                strSql += " AND CHARINDEX(PipelineId,@PipelineId)>0";
                listStr.Add(new SqlParameter("@PipelineId", pipelineIds));
            }
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            // 2.获取当前分页数据
            Grid1.RecordCount = tb.Rows.Count;
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();
        }

        #region 选择要显示列
        /// <summary>
        /// 选择显示列
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSelectColumn_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("JointShowColumn.aspx", "显示列 - ")));
        }
        #endregion

        #region 显示列
        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
            this.BindGrid();
            //显示列
            Model.Sys_UserShowColumns c = BLL.UserShowColumnsService.GetColumnsByUserId(this.CurrUser.UserId, "JointOut");
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
                List<string> columns = column.Split(',').ToList();
                foreach (var item in columns)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        this.Grid1.Columns[Convert.ToInt32(item)].Hidden = false;
                    }
                }
            }
        }
        #endregion

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
            Response.AddHeader("content-disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode("焊口信息导出" + filename, System.Text.Encoding.UTF8) + ".xls");
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
                if (column.Hidden == false)
                {
                    sb.AppendFormat("<td>{0}</td>", column.HeaderText);
                }
            }
            sb.Append("</tr>");
            foreach (GridRow row in grid.Rows)
            {
                sb.Append("<tr>");
                foreach (GridColumn column in grid.Columns)
                {
                    if (column.Hidden == false)
                    {
                        string html = row.Values[column.ColumnIndex].ToString();
                        if (column.ColumnID == "tfNumber")
                        {
                            html = (row.FindControl("labNumber") as AspNet.Label).Text;
                        }
                        sb.AppendFormat("<td>{0}</td>", html);
                    }
                }

                sb.Append("</tr>");
            }

            sb.Append("</table>");

            return sb.ToString();
        }
        #endregion

        protected void drpUnitWork_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.drpPipeline.Items.Clear();
            if (this.drpUnitWork.SelectedValue != BLL.Const._Null)
            {
                PipelineService.InitPipelineDownList(this.drpPipeline, this.drpUnitWork.SelectedValue, true);
            }
            else
            {
                Funs.FineUIPleaseSelect(this.drpPipeline);
            }
        }
    }
}