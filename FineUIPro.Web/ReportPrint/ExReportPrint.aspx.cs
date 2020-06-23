using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.ReportPrint
{
    public partial class ExReportPrint : System.Web.UI.Page
    {
        protected string replaceParameter;
        protected string reportId;
        protected string varValue;
        //protected string hideValue;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                replaceParameter = Request.QueryString["replaceParameter"];
                reportId = Request.QueryString["reportId"];
                varValue = HttpUtility.UrlDecode(Request.QueryString["varValue"]).Replace(",","/");
                //hideValue = Server.UrlDecode(Request.QueryString["rd"]);
                //varValue=new string[2]{"您好！","中国"};
                //varValue = "您好！" + "|" + "中国";
            }
        }
    }
}