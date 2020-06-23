using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Newtonsoft.Json.Linq;

namespace FineUIPro.Web.common
{
    public partial class loading : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
            }
        }

        private void LoadData()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("<ul class=\"loading\">");
            for (int i = 1; i <= 30; i++)
            {
                string imageUrl = PageManager1.GetEmbedLoadingImageUrl(i);

                sb.AppendFormat("<li class=\"ui-widget-content\"><img src=\"{0}\"><div class=\"title\">{1}</div></li>", imageUrl, i);
            }
            sb.Append("</ul>");

            litIcons.Text = sb.ToString();
        }

    }
}
