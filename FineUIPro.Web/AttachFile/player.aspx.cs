using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FineUIPro.Web.AttachFile
{
    public partial class player : PageBase
    {
        public string videoUrl;
        public string videoTitle;
        protected void Page_Load(object sender, EventArgs e)
        {
            videoTitle = "播放";            
            string httpstring = Request.Url.ToString();
            httpstring = httpstring.Substring(0, httpstring.IndexOf(":"));

            string urls = HttpUtility.UrlDecode(Request.QueryString["url"]);
            //videoUrl = httpstring + "://" + Request.ServerVariables["SERVER_NAME"] + ":" + Request.ServerVariables["SERVER_PORT"]+"/"+ urls;            
            videoUrl = "https://c.sedin.com/hsse/" + urls;
        }
    }
}