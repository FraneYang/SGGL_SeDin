using System;
using System.IO;
using System.Linq;
using System.Web.UI.WebControls;

namespace BLL
{
    /// <summary>
    /// 上传附件相关
    /// </summary>
    public class UploadAttachmentService
    {
        #region 附件显示不带删除
        /// <summary>
        /// 附件显示
        /// </summary>
        public static string ShowAttachment(string rootPath, string path)
        {
            string htmlStr = string.Empty;
            if (!string.IsNullOrEmpty(path))
            {
                htmlStr = "<table runat='server' cellpadding='5' cellspacing='5' style=\"width: 100%\">";
                string[] arrStr = path.Split(new string[] { "," }, System.StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < arrStr.Length; i++)
                {
                    if (!string.IsNullOrEmpty(arrStr[i]))
                    {
                        string[] urlArray = arrStr[i].Split('\\');
                        string scanUrl = string.Empty;
                        for (int j = 0; j < urlArray.Length; j++)
                        {
                            scanUrl += urlArray[j] + "|";
                        }

                        string url = rootPath + arrStr[i].Replace('\\', '/');
                        string[] subUrl = url.Split('/');
                        string fileName = subUrl[subUrl.Count() - 1];
                        string newFileName = fileName.Substring(fileName.IndexOf("~") + 1);
                        htmlStr += "<tr><td style=\"width: 60%\" align=\"left\"><span style='cursor:pointer;cursor:pointer;cursor:pointer;TEXT-DECORATION: underline;color:blue' onclick=\"window.open('" + url + "')\">" + newFileName + "</span></td>";
                    }
                }

                htmlStr += "</table>";
            }

            return htmlStr;
        }
        #endregion

        #region 附件显示带删除 页面单个附件
        /// <summary>
        /// 附件显示
        /// </summary>
        public static string ShowAndDeleteAttachment(string rootPath, string path)
        {
            string htmlStr = string.Empty;
            if (!string.IsNullOrEmpty(path))
            {
                htmlStr = "<table runat='server' cellpadding='5' cellspacing='5' style=\"width: 100%\">";
                string[] arrStr = path.Split(new string[] { "," }, System.StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < arrStr.Length; i++)
                {
                    if (!string.IsNullOrEmpty(arrStr[i]))
                    {
                        string[] urlArray = arrStr[i].Split('\\');
                        string scanUrl = string.Empty;
                        for (int j = 0; j < urlArray.Length; j++)
                        {
                            scanUrl += urlArray[j] + "|";
                        }
                        string url = rootPath + arrStr[i].Replace('\\', '/');
                        string[] subUrl = url.Split('/');
                        string fileName = subUrl[subUrl.Count() - 1];
                        string newFileName = fileName.Substring(fileName.IndexOf("~") + 1);

                        htmlStr += "<tr><td style=\"width: 60%\" align=\"left\"><span style='cursor:pointer;cursor:pointer;cursor:pointer;TEXT-DECORATION: underline;color:blue' onclick=\"window.open('" + url + "')\">" + newFileName + "</span></td>";
                        htmlStr += "<td style=\"width: 40%\" align=\"left\"><a style='cursor:pointer;cursor:pointer;cursor:pointer;TEXT-DECORATION: underline;color:blue' onclick='DelAttachment(" + "\"" + scanUrl + "\"" + ")' >删除</a></td></tr>";
                    }
                }

                htmlStr += "</table>";
            }

            return htmlStr;
        }
        #endregion

        #region 附件显示带删除 页面存在多个附件
        /// <summary>
        /// 附件显示
        /// </summary>
        public static string ShowAndDeleteNameAttachment(string rootPath, string path, string delName)
        {
            string htmlStr = string.Empty;
            if (!string.IsNullOrEmpty(path))
            {
                htmlStr = "<table runat='server' cellpadding='5' cellspacing='5' style=\"width: 100%\">";
                string[] arrStr = path.Split(new string[] { "," }, System.StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < arrStr.Length; i++)
                {
                    if (!string.IsNullOrEmpty(arrStr[i]))
                    {
                        string[] urlArray = arrStr[i].Split('\\');
                        string scanUrl = string.Empty;
                        for (int j = 0; j < urlArray.Length; j++)
                        {
                            scanUrl += urlArray[j] + "|";
                        }
                        string url = rootPath + arrStr[i].Replace('\\', '/');
                        string[] subUrl = url.Split('/');
                        string fileName = subUrl[subUrl.Count() - 1];
                        string newFileName = fileName.Substring(fileName.IndexOf("~") + 1);

                        htmlStr += "<tr><td style=\"width: 60%\" align=\"left\"><span style='cursor:pointer;cursor:pointer;cursor:pointer;TEXT-DECORATION: underline;color:blue' onclick=\"window.open('" + url + "')\">" + newFileName + "</span></td>";
                        htmlStr += "<td style=\"width: 40%\" align=\"left\"><a style='cursor:pointer;cursor:pointer;cursor:pointer;TEXT-DECORATION: underline;color:blue' onclick='" + delName + "(" + "\"" + scanUrl + "\"" + ")' >删除</a></td></tr>";
                    }
                }

                htmlStr += "</table>";
            }

            return htmlStr;
        }
        #endregion

        #region 附件在Image中显示
        /// <summary>
        /// 附件在Image中显示
        /// </summary>
        /// <param name="rootValue">文件夹路径</param>
        /// <param name="path">附件路径</param>
        /// <returns>附件显示HTML</returns>
        public static string ShowImage(string rootValue, string path)
        {
            string htmlStr = string.Empty;
            if (!string.IsNullOrEmpty(path))
            {
                htmlStr = "<table runat='server' cellpadding='5' cellspacing='5' style=\"width: 100%\">";
                string[] arrStr = path.Split(new string[] { "," }, System.StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < arrStr.Length; i++)
                {
                    if (!string.IsNullOrEmpty(arrStr[i]))
                    {
                        string[] urlArray = arrStr[i].Split('\\');
                        string scanUrl = string.Empty;
                        for (int j = 0; j < urlArray.Length; j++)
                        {
                            scanUrl += urlArray[j] + "|";
                        }

                        string url = rootValue + arrStr[i].Replace('\\', '/');
                        string[] subUrl = url.Split('/');
                        string fileName = subUrl[subUrl.Count() - 1];
                        string newFileName = fileName.Substring(fileName.IndexOf("~") + 1);

                        htmlStr += "<tr><td style=\"width: 60%\" align=\"left\"><img width='100' height='100' src='" + url + "'></img></td>";
                    }
                }

                htmlStr += "</table>";
            }

            return htmlStr;
        }
        #endregion

        #region 附件上传
        /// <summary>
        /// 附件上传
        /// </summary>
        /// <param name="fileUpload">上传控件</param>
        /// <param name="fileUrl">上传路径</param>
        /// <param name="constUrl">定义路径</param>
        /// <returns></returns>
        public static string UploadAttachment(string rootPath, FileUpload fileUpload, string fileUrl, string constUrl)
        {
            string initFullPath = rootPath + constUrl;
            if (!Directory.Exists(initFullPath))
            {
                Directory.CreateDirectory(initFullPath);
            }

            string filePath = fileUpload.PostedFile.FileName;
            string fileName = Funs.GetNewFileName() + "~" + Path.GetFileName(filePath);
            int count = fileUpload.PostedFile.ContentLength;
            string savePath = constUrl + fileName;
            string fullPath = initFullPath + fileName;

            if (!File.Exists(fullPath))
            {
                byte[] buffer = new byte[count];
                Stream stream = fileUpload.PostedFile.InputStream;

                stream.Read(buffer, 0, count);
                MemoryStream memoryStream = new MemoryStream(buffer);
                FileStream fs = new FileStream(fullPath, FileMode.Create, FileAccess.Write);
                memoryStream.WriteTo(fs);
                memoryStream.Flush();
                memoryStream.Close();
                fs.Flush();
                fs.Close();
                memoryStream = null;
                fs = null;
                if (!string.IsNullOrEmpty(fileUrl))
                {
                    fileUrl += "," + savePath;
                }
                else
                {
                    fileUrl += savePath;
                }
            }
            else
            {
                fileUrl = string.Empty;
            }

            return fileUrl;
        }
        #endregion

        #region 附件上传 同时上传到服务器端
        /// <summary>
        /// 附件上传 同时上传到服务器端
        /// </summary>
        /// <param name="fileUpload">上传控件</param>
        /// <param name="fileUrl">上传路径</param>
        /// <param name="constUrl">定义路径</param>
        /// <param name="serverUrl">服务端地址</param>
        /// <returns></returns>
        public static string UploadAttachmentAndServer(string rootPath, FileUpload fileUpload, string fileUrl, string constUrl, string serverUrl)
        {
            string initFullPath = rootPath + constUrl;
            if (!Directory.Exists(initFullPath))
            {
                Directory.CreateDirectory(initFullPath);
            }

            string initFullPathServer = serverUrl + constUrl;
            if (!Directory.Exists(initFullPathServer))
            {
                Directory.CreateDirectory(initFullPathServer);
            }

            string filePath = fileUpload.PostedFile.FileName;
            string fileName = Funs.GetNewFileName() + "~" + Path.GetFileName(filePath);
            int count = fileUpload.PostedFile.ContentLength;
            string savePath = constUrl + fileName;
            string fullPath = initFullPath + fileName;
            string fullPathServer = initFullPathServer + fileName;

            if (!File.Exists(fullPath))
            {
                byte[] buffer = new byte[count];
                Stream stream = fileUpload.PostedFile.InputStream;

                stream.Read(buffer, 0, count);
                MemoryStream memoryStream = new MemoryStream(buffer);
                FileStream fs = new FileStream(fullPath, FileMode.Create, FileAccess.Write);
                memoryStream.WriteTo(fs);
                memoryStream.Flush();
                memoryStream.Close();
                fs.Flush();
                fs.Close();
                memoryStream = null;
                fs = null;
                if (!string.IsNullOrEmpty(fileUrl))
                {
                    fileUrl += "," + savePath;
                }
                else
                {
                    fileUrl += savePath;
                }

                MemoryStream memoryStreamEng = new MemoryStream(buffer);
                FileStream fsServer = new FileStream(fullPathServer, FileMode.Create, FileAccess.Write);
                memoryStreamEng.WriteTo(fsServer);
                memoryStreamEng.Flush();
                memoryStreamEng.Close();
                fsServer.Flush();
                fsServer.Close();
                memoryStreamEng = null;
                fsServer = null;
            }
            else
            {
                fileUrl = string.Empty;
            }

            return fileUrl;
        }
        #endregion

        #region 附件虚删除
        /// <summary>
        ///  附件删除
        /// </summary>
        /// <param name="fileUrl">附件路径</param>
        /// <param name="hiddenUrl">隐藏列路径</param>
        /// <returns></returns>
        public static string DeleteAttachment(string fileUrl, string hiddenUrl)
        {
            string hdAttachUrlStr = hiddenUrl;
            string[] urlArray = hdAttachUrlStr.Split('|');
            string scanUrl = string.Empty;
            for (int j = 0; j < urlArray.Length; j++)
            {
                if (!string.IsNullOrEmpty(urlArray[j]))
                {
                    if (j == 0)
                    {
                        scanUrl += urlArray[j];
                    }
                    else
                    {
                        scanUrl += "\\" + urlArray[j];
                    }
                }
            }

            if (!String.IsNullOrEmpty(fileUrl))
            {
                string[] arrStr = fileUrl.Split(new string[] { "," }, System.StringSplitOptions.RemoveEmptyEntries);
                fileUrl = null;
                for (int i = 0; i < arrStr.Length; i++)
                {
                    if (scanUrl != arrStr[i])
                    {
                        if (i != arrStr.Length - 1)
                        {
                            fileUrl += arrStr[i] + ",";
                        }
                        else
                        {
                            fileUrl += arrStr[i];
                        }
                    }
                }
            }

            return fileUrl;
        }
        #endregion

        #region 附件资源删除
        /// <summary>
        ///  附件资源删除
        /// </summary>
        /// <param name="fileUrl">附件路径</param>
        /// <param name="hiddenUrl">隐藏列路径</param>
        /// <returns></returns>
        public static void DeleteFile(string rootPath, string fileUrl)
        {
            if (!string.IsNullOrEmpty(fileUrl))
            {
                string[] strs = fileUrl.Trim().Split(',');
                foreach (var item in strs)
                {
                    string urlFullPath = rootPath + item;
                    if (File.Exists(urlFullPath))
                    {
                        File.Delete(urlFullPath);
                    }
                }
            }
        }
        #endregion

        #region 附件打开公共方法
        /// <summary>
        /// 显示附件文件
        /// </summary>
        public static void ShowAttachmentsFile(string rootPath, string path)
        {
            string[] arrStr = path.Split(new string[] { "," }, System.StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < arrStr.Length; i++)
            {
                if (!string.IsNullOrEmpty(arrStr[i]))
                {
                    string[] urlArray = arrStr[i].Split('\\');
                    string scanUrl = string.Empty;
                    for (int j = 0; j < urlArray.Length; j++)
                    {
                        scanUrl += urlArray[j] + "|";
                    }
                    string url = rootPath + arrStr[i].Replace('\\', '/');
                    System.Web.HttpContext.Current.Response.Write("<script type='text/javascript' language='javascript'>window.open('" + url + "')</script>");
                }
            }
        }
        #endregion

        #region 附件在Image中显示
        /// <summary>
        /// 附件在Image中显示
        /// </summary>
        /// <param name="rootValue">文件夹路径</param>
        /// <param name="path">附件路径</param>
        /// <returns>附件显示HTML</returns>
        public static string ShowImage(string rootValue, string path, decimal width, decimal height)
        {
            string htmlStr = string.Empty;
            if (!string.IsNullOrEmpty(path))
            {
                htmlStr = "<table runat='server' cellpadding='5' cellspacing='5' style=\"width: 100%\">";
                string[] arrStr = path.Split(new string[] { "," }, System.StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < arrStr.Length; i++)
                {
                    if (!string.IsNullOrEmpty(arrStr[i]))
                    {
                        string[] urlArray = arrStr[i].Split('\\');
                        string scanUrl = string.Empty;
                        for (int j = 0; j < urlArray.Length; j++)
                        {
                            scanUrl += urlArray[j] + "|";
                        }

                        string url = rootValue + arrStr[i].Replace('\\', '/');
                        string[] subUrl = url.Split('/');
                        string fileName = subUrl[subUrl.Count() - 1];
                        string newFileName = fileName.Substring(fileName.IndexOf("~") + 1);

                        htmlStr += "<tr><td style=\"width: 100%\" align=\"left\"><img width='" + width + "' height='" + height + "' src='" + url + "'></img></td>";
                    }
                }

                htmlStr += "</table>";
            }

            return htmlStr;
        }
        #endregion
    }
}
