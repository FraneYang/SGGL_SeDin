using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace Web.ReportPrint
{
    public partial class PrintDesigner2 : System.Web.UI.Page
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
                this.drpPrintReport.DataTextField = "ConstText";
                drpPrintReport.DataValueField = "ConstValue";
                drpPrintReport.DataSource = BLL.ConstValue.drpConstItemList(ConstValue.Group_Report);
                drpPrintReport.DataBind();
            }
        }

        /// <summary>
        /// 报表设计
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnReportDesigner_Click(object sender, EventArgs e)
        {
            if (this.drpPrintReport.SelectedValue != "0")
            {
                //BLL.LogService.AddSys_Log(this.CurrUser,, this.drpPrintReport.SelectedItem.Text);
                Response.Redirect("ExPrintSet.aspx?reportId=" + this.drpPrintReport.SelectedValue + "&reportName=" + this.drpPrintReport.SelectedItem.Text);
            }
        }
    }
}