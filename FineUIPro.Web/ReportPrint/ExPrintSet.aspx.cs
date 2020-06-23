using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.ReportPrint
{
    public partial class ExPrintSet : System.Web.UI.Page
    {
        
        /// <summary>
        /// 报表Id
        /// </summary>
        protected string reportId
        {
            get
            {
                return (string)ViewState["reportId"];
            }
            set
            {
                ViewState["reportId"] = value;
            }
        }

        /// <summary>
        /// rb
        /// </summary>
        protected string reportName
        {
            get
            {
                return (string)ViewState["reportName"];
            }
            set
            {
                ViewState["reportName"] = value;
            }
        }

        /// <summary>
        /// 替换参数
        /// </summary>
        protected string ReplaceParameter
        {
            get
            {
                return (string)ViewState["ReplaceParameter"];
            }
            set
            {
                ViewState["ReplaceParameter"] = value;
            }
        }

        /// <summary>
        /// 隐藏值
        /// </summary>
        protected string HideValue
        {
            get
            {
                return (string)ViewState["HideValue"];
            }
            set
            {
                ViewState["HideValue"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                reportId = Request.QueryString["reportId"];
                reportName = Request.QueryString["reportName"];
                ReplaceParameter = Request.QueryString["replaceParameter"];
                HideValue = Request.QueryString["rd"];

                this.lbReportName.Text = "报表名称：" + reportName;
            }
        }

        /// <summary>
        /// 恢复初始设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void undo_Click(object sender, ImageClickEventArgs e)
        {
            string str = "UPDATE dbo.ReportServer SET TabContent = InitTabContent WHERE ReportId = '" + reportId + "'";
            try
            {
                BLL.SQLHelper.RunSqlString(str, "ReportServer");
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
                Response.Write(ex.Source);
                Response.End();

            }
            finally
            {
            }
        }

        /// <summary>
        /// 返回
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void imgReturn_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("PrintDesigner.aspx");
        }
    }
}