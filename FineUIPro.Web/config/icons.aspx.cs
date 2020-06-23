using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

namespace FineUIPro.Web
{
    public partial class icons : PageBase
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

            string iconUrl = ResolveUrl("~/res/images/empty.png");

            sb.Append("<ul class=\"icons\">");
            foreach (string icon in Enum.GetNames(typeof(Icon)))
            {
                Icon iconType = (Icon)Enum.Parse(typeof(Icon), icon);

                if (iconType != Icon.None)
                {
                    iconUrl = ResolveUrl(IconHelper.GetIconUrl(iconType));
                }

                sb.AppendFormat("<li class=\"ui-state-default\"><img src=\"{0}\"/><div class=\"title\">{1}</div></li>", iconUrl, icon);
            }
            sb.Append("</ul>");

            litIcons.Text = sb.ToString();
        }

    }
}
