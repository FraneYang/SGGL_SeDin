using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace FineUIPro.Web
{
    public partial class source_file : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string file = Request.QueryString["file"];

                if (file.StartsWith("http://") || file.StartsWith("https://"))
                {
                    desc.Text = String.Format("<br/><br/><a href=\"{0}\" target=\"_blank\">在新窗口打开</a>", file);
                    return;
                }

                string content = File.ReadAllText(Server.MapPath(file));
                desc.Text = "<pre class=\"prettyprint linenums\">" + HttpUtility.HtmlEncode(content) + "</pre>";

            }
        }
    }
}
