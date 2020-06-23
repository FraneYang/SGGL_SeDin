using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

namespace FineUIPro.Web
{
    public partial class icons_font : PageBase
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

            string iconName = String.Empty;
            string iconClassName = "&nbsp;";

            sb.Append("<ul class=\"icons\">");
            foreach (string icon in Enum.GetNames(typeof(IconFont)))
            {
                IconFont iconType = (IconFont)Enum.Parse(typeof(IconFont), icon);

                if (iconType != IconFont.None)
                {
                    iconName = IconFontHelper.GetName(iconType);
                    iconClassName = iconName;
                }

                sb.AppendFormat("<li class=\"ui-state-default\"><i class=\"ui-icon f-icon-{0}\"></i><div class=\"title\">{1}</div><div class=\"subtitle\">{2}</div></li>", iconName, icon, iconClassName);
            }
            sb.Append("</ul>");

            litIcons.Text = sb.ToString();
        }

    }
}
