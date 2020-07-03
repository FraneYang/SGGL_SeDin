using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FineUIPro.Web.CQMS.WBS
{
    public partial class ControlItemAndCycleSearch : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                UnitWorkService.InitUnitWorkDownList(drpUnitWork, this.CurrUser.LoginProjectId, true);//单位工程
                Funs.FineUIPleaseSelect(this.drpControlPoint);//控制点等级
                BindGrid();
            }

        }
        public void BindGrid()
        {
            string strSql = @"select * from View_WBS_ControlItemAndCycle c where c.IsApprove=1";
            List<SqlParameter> listStr = new List<SqlParameter>();
            strSql += " AND c.ProjectId = @ProjectId";
            listStr.Add(new SqlParameter("@ProjectId", this.CurrUser.LoginProjectId));

            if (this.drpUnitWork.SelectedValue != BLL.Const._Null)
            {
                strSql += " AND CHARINDEX(c.UnitWorkId,@UnitWorkId)>0";
                string unitWorkIds = string.Empty;
                var unitWork = BLL.UnitWorkService.GetUnitWorkByUnitWorkId(this.drpUnitWork.SelectedValue);
                if (unitWork != null)
                {
                    var unitWorks = BLL.UnitWorkService.GetUnitWorkByUnitWorkCode(unitWork.UnitWorkCode);
                    foreach (var item in unitWorks)
                    {
                        unitWorkIds += item.UnitWorkId + ",";
                    }
                    if (!string.IsNullOrEmpty(unitWorkIds))
                    {
                        unitWorkIds = unitWorkIds.Substring(0, unitWorkIds.LastIndexOf(","));
                    }
                }
                listStr.Add(new SqlParameter("@UnitWorkId", unitWorkIds));
            }
            if (!string.IsNullOrEmpty(this.txtControlItemContent.Text.Trim()))
            {
                strSql += @" and c.ControlItemContent like @ControlItemContent ";
                listStr.Add(new SqlParameter("@ControlItemContent", "%" + this.txtControlItemContent.Text.Trim() + "%"));
            }
            string controlPoint = string.Empty;
            string[] strs = this.drpControlPoint.SelectedValueArray;
            foreach (var item in strs)
            {
                controlPoint += item + ",";
            }
            if (!string.IsNullOrEmpty(controlPoint))
            {
                controlPoint = controlPoint.Substring(0, controlPoint.LastIndexOf(","));
            }
            if (controlPoint != BLL.Const._Null)
            {
                strSql += " AND CHARINDEX(c.ControlPoint,@ControlPoint)>0";
                listStr.Add(new SqlParameter("@ControlPoint", controlPoint));
            }
            if (!string.IsNullOrEmpty(this.txtForms.Text.Trim()))
            {
                strSql += @" and (c.HGForms like @Forms or c.SHForms like @Forms)";
                listStr.Add(new SqlParameter("@Forms", "%" + this.txtForms.Text.Trim() + "%"));
            }
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            Grid1.RecordCount = tb.Rows.Count;
            tb = GetFilteredTable(Grid1.FilteredData, tb);
            var table = this.GetPagedDataTable(Grid1, tb);

            Grid1.DataSource = table;
            Grid1.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void btnRset_Click(object sender, EventArgs e)
        {
            drpUnitWork.SelectedIndex = 0;
            drpControlPoint.SelectedIndex = 0;
            this.txtControlItemContent.Text = string.Empty;
            this.txtForms.Text = string.Empty;
            BindGrid();
        }

        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid1.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            BindGrid();
        }

        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            BindGrid();
        }
    }
}