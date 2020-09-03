using BLL;
using System.Linq;
using System;

namespace Web.ReportPrint
{
    public partial class PrintDesigner : System.Web.UI.Page
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
                var hjgl = from x in Funs.DB.Sys_Const
                           where x.GroupId == ConstValue.Group_Report && x.SortIndex >= 100 && x.SortIndex <= 150
                           orderby x.SortIndex
                           select x;

                drpPrintReport.DataValueField = "ConstValue";
                drpPrintReport.DataTextField = "ConstText";
                drpPrintReport.DataSource = hjgl;
                drpPrintReport.DataBind();

                // BLL.ConstValue.InitConstValueDropDownList(this.drpPrintReport, ConstValue.Group_Report, true);
            }
        }

        protected void btnDesigner_Click(object sender, EventArgs e)
        {
            if (this.drpPrintReport.SelectedValue != BLL.Const._Null)
            {
                //BLL.LogService.AddSys_Log(this.CurrUser,, this.drpPrintReport.SelectedItem.Text);
                Response.Redirect("ExPrintSet.aspx?reportId=" + this.drpPrintReport.SelectedValue + "&reportName=" + this.drpPrintReport.SelectedItem.Text);
            }
        }

        protected void drpReportModule_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpReportModule.SelectedValue == "1")
            {
                var hjgl = from x in Funs.DB.Sys_Const
                           where x.GroupId == ConstValue.Group_Report && x.SortIndex >= 100 && x.SortIndex <= 150
                           orderby x.SortIndex
                           select x;
                drpPrintReport.DataValueField = "ConstValue";
                drpPrintReport.DataTextField = "ConstText";
                drpPrintReport.DataSource = hjgl;
                drpPrintReport.DataBind();
            }
        }
    }
}