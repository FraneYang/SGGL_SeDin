using BLL;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using AspNet = System.Web.UI.WebControls;

namespace FineUIPro.Web.HSSE.SitePerson
{
    public partial class PersonStatistic : PageBase
    {
        /// <summary>
        /// 项目id
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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.ProjectId = this.CurrUser.LoginProjectId;
                if (!string.IsNullOrEmpty(Request.Params["projectId"]) && Request.Params["projectId"] != this.ProjectId)
                {
                    this.ProjectId = Request.Params["projectId"];
                }
                UnitService.InitUnitDropDownList(this.drpUnit, this.ProjectId, true);
                if (ProjectUnitService.GetProjectUnitTypeByProjectIdUnitId(this.ProjectId, this.CurrUser.UnitId))
                {
                    this.drpUnit.SelectedValue = this.CurrUser.UnitId;
                    this.drpUnit.Enabled = false;
                }

                WorkPostService.InitWorkPostDropDownList(this.drpWorkPost, true);
                var project = BLL.ProjectService.GetProjectByProjectId(this.ProjectId);
                if (project != null)
                {
                    this.txtStartDate.Text = string.Format("{0:yyyy-MM-dd}", project.StartDate);
                    this.txtEndDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now.AddDays(1));
                    GetPersonStatistic();
                }
            }
        }

        /// <summary>
        /// 获取数据，合并相同行
        /// </summary>
        private void GetPersonStatistic()
        {
            if (!string.IsNullOrEmpty(this.ProjectId))
            {
                string startTime = null;
                string endTime = null;
                string isIn = this.rblIsUsed.SelectedValue;
                string unitId = null;
                string workPostId = null;
                if (!string.IsNullOrEmpty(this.txtStartDate.Text.Trim()))
                {
                    startTime = this.txtStartDate.Text.Trim();
                }                
                if (!string.IsNullOrEmpty(this.txtEndDate.Text.Trim()))
                {
                    endTime = this.txtEndDate.Text.Trim();
                }
                if (this.drpUnit.SelectedValue != Const._Null)
                {
                    unitId = this.drpUnit.SelectedValue;
                }
                else
                {
                    unitId = null;
                }
                if (this.drpWorkPost.SelectedValue != Const._Null)
                {
                    workPostId = this.drpWorkPost.SelectedValue;
                }
                else
                {
                    workPostId = null;
                }
                SqlParameter[] values = new SqlParameter[]
                    {
                    new SqlParameter("@ProjectId", this.ProjectId),
                    new SqlParameter("@StartTime", startTime),
                    new SqlParameter("@EndTime", endTime),
                    new SqlParameter("@isIn", isIn),
                    new SqlParameter("@unitId", unitId),
                    new SqlParameter("@workPostId", workPostId),
                    };

                DataTable tb = SQLHelper.GetDataTableRunProc("SpPersonStatistic", values);
                // 2.获取当前分页数据
                //var table = this.GetPagedDataTable(Grid1, tb1);

                Grid1.RecordCount = tb.Rows.Count;
                var table = this.GetPagedDataTable(Grid1, tb);

                Grid1.DataSource = table;
                Grid1.DataBind();

                //DataTable dtPersonStatistic = BLL.SQLHelper.GetDataTableRunProc("SpPersonStatistic", values);
                //if (dtPersonStatistic.Rows.Count == 0)
                //{
                //    this.Grid1.DataSource = (from x in new Model.SGGLDB(Funs.ConnString).SitePerson_Checking where x.CardNo == null select x).ToList();
                //    this.Grid1.DataBind();
                //}
                //else
                //{
                //    this.Grid1.DataSource = dtPersonStatistic;
                //    this.Grid1.DataBind();
                //}
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            GetPersonStatistic();
        }

        #region 导出按钮
        /// 导出按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnOut_Click(object sender, EventArgs e)
        {
            Response.ClearContent();
            string filename = Funs.GetNewFileName();
            Response.AddHeader("content-disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode("现场人员统计" + filename, System.Text.Encoding.UTF8) + ".xls");
            Response.ContentType = "application/excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            this.Grid1.PageSize = 500;
            GetPersonStatistic();
            Response.Write(GetGridTableHtml(Grid1));
            Response.End();
        }

#pragma warning disable CS0108 // “PersonStatistic.GetGridTableHtml(Grid)”隐藏继承的成员“PageBase.GetGridTableHtml(Grid)”。如果是有意隐藏，请使用关键字 new。
        /// <summary>
        /// 导出方法
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        private string GetGridTableHtml(Grid grid)
#pragma warning restore CS0108 // “PersonStatistic.GetGridTableHtml(Grid)”隐藏继承的成员“PageBase.GetGridTableHtml(Grid)”。如果是有意隐藏，请使用关键字 new。
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
    }
}