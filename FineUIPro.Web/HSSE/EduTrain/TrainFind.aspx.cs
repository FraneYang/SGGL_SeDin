using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using AspNet = System.Web.UI.WebControls;

namespace FineUIPro.Web.HSSE.EduTrain
{
    public partial class TrainFind : PageBase
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

        #region 加载页面
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
                this.ProjectId = this.CurrUser.LoginProjectId;
                if (!string.IsNullOrEmpty(Request.Params["projectId"]) && Request.Params["projectId"] != this.CurrUser.LoginProjectId)
                {
                    this.ProjectId = Request.Params["projectId"];
                }
                //单位
                BLL.UnitService.InitUnitDropDownList(this.drpUnitId, this.ProjectId, true);
                if (BLL.ProjectUnitService.GetProjectUnitTypeByProjectIdUnitId(this.ProjectId, this.CurrUser.UnitId))
                {
                    this.drpUnitId.SelectedValue = this.CurrUser.UnitId;
                    this.drpUnitId.Enabled = false;
                }
                //培训类型
                BLL.TrainTypeService.InitTrainTypeDropDownList(this.drpTrainType, true);
                //培训级别;
                //BLL.TrainLevelService.InitTrainLevelDropDownList(this.drpTrainLevel, true);
                ddlPageSize.SelectedValue = Grid1.PageSize.ToString();
                // 绑定表格
                BindGrid();
            }
        }

        /// <summary>
        /// 加载数据
        /// </summary>
        private void BindGrid()
        {
            string strSql = @"SELECT NEWID() AS ID, CardNo,PersonName,ProjectId,UnitId,UnitName,WorkPostName,TrainTitle ,TrainStartDate,TrainEndDate,TrainTypeId,TeachHour,TrainTypeName,CheckScore,CheckResult,TeachMan,UnitType,TrainLevelName" +
                            @" FROM dbo.View_EduTrain_TrainFind" +
                            @" WHERE 1=1";
            List<SqlParameter> listStr = new List<SqlParameter>();
            strSql += " AND ProjectId = @ProjectId";
            listStr.Add(new SqlParameter("@ProjectId", this.ProjectId));

            if (!string.IsNullOrEmpty(this.txtPersonName.Text.Trim()))
            {
                strSql += " AND PersonName LIKE @PersonName";
                listStr.Add(new SqlParameter("@PersonName", "%" + this.txtPersonName.Text.Trim() + "%"));
            }
            if (this.drpUnitId.SelectedValue != BLL.Const._Null)
            {
                strSql += " AND UnitId = @UnitId";
                listStr.Add(new SqlParameter("@UnitId", this.drpUnitId.SelectedValue.Trim()));
            }
            if (this.drpTrainType.SelectedValue != BLL.Const._Null)
            {
                strSql += " AND TrainTypeId = @TrainTypeId";
                listStr.Add(new SqlParameter("@TrainTypeId", this.drpTrainType.SelectedValue.Trim()));
            }
            //if (this.drpTrainLevel.SelectedValue != BLL.Const._Null)
            //{
            //    strSql += " AND TrainLevelId = @TrainLevel";
            //    listStr.Add(new SqlParameter("@TrainLevel", this.drpTrainLevel.SelectedValue));
            //}

            if (!string.IsNullOrEmpty(this.txtTeachMan.Text.Trim()))
            {
                strSql += " AND TeachMan LIKE @TeachMan";
                listStr.Add(new SqlParameter("@TeachMan", "%" + this.txtTeachMan.Text.Trim() + "%"));
            }

            if (this.cbIssue.SelectedValueArray.Length == 1)
            {
                ///是否通过
                string selectValue = String.Join(", ", this.cbIssue.SelectedValueArray);
                if (selectValue == "1")
                {
                    strSql += " AND CheckResult = 1 ";
                }
                else
                {
                    strSql += " AND (CheckResult = 0 OR CheckResult IS NULL) ";
                }
            }
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            Grid1.RecordCount = tb.Rows.Count;
            var table = this.GetPagedDataTable(Grid1, tb);

            Grid1.DataSource = table;
            Grid1.DataBind();
        }
        #endregion

        #region 页索引改变事件
        /// <summary>
        /// 页索引改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            Grid1.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        #endregion

        #region 排序
        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_Sort(object sender, GridSortEventArgs e)
        {
            Grid1.SortDirection = e.SortDirection;
            Grid1.SortField = e.SortField;
            BindGrid();
        }
        #endregion

        #region 分页选择下拉改变事件
        /// <summary>
        /// 分页选择下拉改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid1.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            BindGrid();
        }
        #endregion

        #region 查询
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Text_OnTextChanged(object sender, EventArgs e)
        {
            BindGrid();
        }
        #endregion

        #region 转换字符串
        /// <summary>
        /// 考核结果
        /// </summary>
        /// <param name="checkResult"></param>
        /// <returns></returns>
        protected string ConvertCheckResult(object checkResult)
        {
            if (checkResult != null)
            {
                if (checkResult.ToString() == "True")
                {
                    return "合格";
                }
                else
                {
                    return "不合格";
                }
            }
            return "";
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
            Response.AddHeader("content-disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode("人员培训" + filename, System.Text.Encoding.UTF8) + ".xls");
            Response.ContentType = "application/excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            this.Grid1.PageSize = 500;
            BindGrid();
            Response.Write(GetGridTableHtml(Grid1));
            Response.End();
        }


#pragma warning disable CS0108 // “TrainFind.GetGridTableHtml(Grid)”隐藏继承的成员“PageBase.GetGridTableHtml(Grid)”。如果是有意隐藏，请使用关键字 new。
        /// <summary>
        /// 导出方法
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        private string GetGridTableHtml(Grid grid)
#pragma warning restore CS0108 // “TrainFind.GetGridTableHtml(Grid)”隐藏继承的成员“PageBase.GetGridTableHtml(Grid)”。如果是有意隐藏，请使用关键字 new。
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
                    if (column.ColumnID == "tfUnitName")
                    {
                        html = (row.FindControl("lbUnitName") as AspNet.Label).Text;
                    }
                    if (column.ColumnID == "tfWorkPostName")
                    {
                        html = (row.FindControl("lbWorkPostName") as AspNet.Label).Text;
                    }
                    if (column.ColumnID == "tfTrainTypeName")
                    {
                        html = (row.FindControl("lbTrainTypeName") as AspNet.Label).Text;
                    }
                    if (column.ColumnID == "tfTrainLevelName")
                    {
                        html = (row.FindControl("lbTrainLevelName") as AspNet.Label).Text;
                    }
                    if (column.ColumnID == "tfTrainTitle")
                    {
                        html = (row.FindControl("lbTrainTitle") as AspNet.Label).Text;
                    }
                    if (column.ColumnID == "tfCheckResult")
                    {
                        html = (row.FindControl("lbCheckResult") as AspNet.Label).Text;
                    }
                    if (column.ColumnID == "tfI")
                    {
                        html = "'" + (row.FindControl("lbI") as AspNet.Label).Text;
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