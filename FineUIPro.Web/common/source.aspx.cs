using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace FineUIPro.Web
{
    public partial class source : PageBase
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string files = Request.QueryString["files"];

                if (String.IsNullOrEmpty(files))
                {
                    return;
                }

                if (!String.IsNullOrEmpty(files))
                {
                    string[] fileNames = files.Split(';');

                    for (int i = 0; i < fileNames.Length; i++)
                    {
                        string fileName = fileNames[i].Trim();
                        if (String.IsNullOrEmpty(fileName))
                        {
                            continue;
                        }

                        // 处理编辑页面，类似 /demo_pro/grid/grid_newtab_window.aspx?id=101&name=张三 
                        int lastQuestionMaskPosition = fileName.IndexOf("?");
                        if (lastQuestionMaskPosition >= 0)
                        {
                            fileName = fileName.Substring(0, lastQuestionMaskPosition);
                        }

                        string shortFileName = GetShortFileName(fileName);
                        string iframeUrl = "./source_file.aspx?file=" + fileName;

                        Tab tab = new Tab
                        {
                            Title = shortFileName,
                            EnableIFrame = true,
                            IFrameUrl = iframeUrl
                        };
                        tab.IconUrl = GetIconUrl(tab.IFrameUrl);
                        TabStrip1.Tabs.Add(tab);

                        // End with .aspx.
                        if (fileName.ToLower().EndsWith(".aspx") 
                            || fileName.ToLower().EndsWith(".ascx") 
                            || fileName.ToLower().EndsWith(".master")
                            || fileName.ToLower().EndsWith(".ashx"))
                        {
                            tab = new FineUIPro.Tab
                            {
                                Title = shortFileName + ".cs",
                                EnableIFrame = true,
                                IFrameUrl = iframeUrl + ".cs"
                            };
                            tab.IconUrl = GetIconUrl(tab.IFrameUrl);
                            TabStrip1.Tabs.Add(tab);
                        }
                    }
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private string GetIconUrl(string url)
        {
            string suffix = url.Substring(url.LastIndexOf('.') + 1);
            return "~/res/images/filetype/vs_" + suffix + ".png";
        }

        private string GetShortFileName(string fileName)
        {
            int index = fileName.LastIndexOf("/");

            if (index >= 0)
            {
                return fileName.Substring(index + 1);
            }

            return fileName;
        }
    }
}
