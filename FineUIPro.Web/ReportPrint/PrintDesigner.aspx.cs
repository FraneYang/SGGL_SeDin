using BLL;
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
                BLL.ConstValue.InitConstValueDropDownList(this.drpPrintReport, ConstValue.Group_Report, true);
            }
        }
        
        protected void drpPrintReport_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.drpPrintReport.SelectedValue != BLL.Const._Null)
            {
                //BLL.LogService.AddSys_Log(this.CurrUser,, this.drpPrintReport.SelectedItem.Text);
                Response.Redirect("ExPrintSet.aspx?reportId=" + this.drpPrintReport.SelectedValue + "&reportName=" + this.drpPrintReport.SelectedItem.Text);
            }
        }
    }
}