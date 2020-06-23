using System;
using System.IO;


namespace FineUIPro.Web.common
{
    public partial class ShowUpFile : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string fileUrl = Server.UrlDecode(Request.QueryString["fileUrl"]);
                if (!string.IsNullOrEmpty(fileUrl))
                {
                    this.hdFileUrl.Text = fileUrl;
                    //this.lbFileName.Text = this.hdFileUrl.Text.Substring(this.hdFileUrl.Text.IndexOf("~") + 1);
                    this.showPage(fileUrl);
                }
            }
        }

        /// <summary>
        /// 附件方法
        /// </summary>
        /// <param name="fileUrl"></param>
        private void showPage(string fileUrl)
        {
            if (!string.IsNullOrEmpty(fileUrl))
            {
                string[] strs = fileUrl.Trim().Split(',');
                foreach (var item in strs)
                {
                    string url = BLL.Funs.RootPath + item;
                    FileInfo info = new FileInfo(url);
                    if (!info.Exists)
                    {
                        try
                        {
                            url = BLL.Funs.CNCECPath + item;
                            url = url.Replace('\\', '/');
                            System.Web.HttpContext.Current.Response.Write("<script type='text/javascript' language='javascript'>window.open('" + url + "')</script>");
                        }
                        catch (Exception ex)
                        {
                            BLL.ErrLogInfo.WriteLog(string.Empty, ex);
                        }

                    }
                    else
                    {

                        long fileSize = info.Length;
                        Response.Clear();
                        Response.ContentType = "application/x-zip-compressed";
                        Response.AddHeader("Content-Disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode(this.hdFileUrl.Text.Substring(this.hdFileUrl.Text.IndexOf("~") + 1), System.Text.Encoding.UTF8));
                        Response.AddHeader("Content-Length", fileSize.ToString());
                        Response.TransmitFile(url, 0, fileSize);
                        Response.Flush();
                        Response.Close();

                    }
                }
            }
            else
            {
                PageBase.ShowFileEvent(fileUrl);
            }
        }
    }
}