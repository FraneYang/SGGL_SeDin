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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="FileName"></param>
        /// <param name="JpgSize"></param>
        /// <param name="Wpx"></param>
        /// <param name="Hpx"></param>
        /// <returns></returns>
        public static int getJpgSize(string FileName, out System.Drawing.Size JpgSize, out float Wpx, out float Hpx)
        {//C#快速获取JPG图片大小及英寸分辨率
            JpgSize = new System.Drawing.Size(0, 0);
            Wpx = 0; Hpx = 0;
            int rx = 0;
            if (!File.Exists(FileName)) return rx;
            FileStream F_Stream = File.OpenRead(FileName);
            int ff = F_Stream.ReadByte();
            int type = F_Stream.ReadByte();
            if (ff != 0xff || type != 0xd8)
            {//非JPG文件
                F_Stream.Close();
                return rx;
            }
            long ps = 0;
            do
            {
                do
                {
                    ff = F_Stream.ReadByte();
                    if (ff < 0) //文件结束
                    {
                        F_Stream.Close();
                        return rx;
                    }
                } while (ff != 0xff);
                do
                {
                    type = F_Stream.ReadByte();
                } while (type == 0xff);
                //MessageBox.Show(ff.ToString() + "," + type.ToString(), F_Stream.Position.ToString());
                ps = F_Stream.Position;
                switch (type)
                {
                    case 0x00:
                    case 0x01:
                    case 0xD0:
                    case 0xD1:
                    case 0xD2:
                    case 0xD3:
                    case 0xD4:
                    case 0xD5:
                    case 0xD6:
                    case 0xD7:
                        break;
                    case 0xc0: //SOF0段
                        ps = F_Stream.ReadByte() * 256;
                        ps = F_Stream.Position + ps + F_Stream.ReadByte() - 2; //加段长度
                        F_Stream.ReadByte(); //丢弃精度数据
                        //高度
                        JpgSize.Height = F_Stream.ReadByte() * 256;
                        JpgSize.Height = JpgSize.Height + F_Stream.ReadByte();
                        //宽度
                        JpgSize.Width = F_Stream.ReadByte() * 256;
                        JpgSize.Width = JpgSize.Width + F_Stream.ReadByte();
                        //后面信息忽略
                        if (rx != 1 && rx < 3) rx = rx + 1;
                        break;
                    case 0xe0: //APP0段
                        ps = F_Stream.ReadByte() * 256;
                        ps = F_Stream.Position + ps + F_Stream.ReadByte() - 2; //加段长度
                        F_Stream.Seek(5, SeekOrigin.Current); //丢弃APP0标记(5bytes)
                        F_Stream.Seek(2, SeekOrigin.Current); //丢弃主版本号(1bytes)及次版本号(1bytes)
                        int units = F_Stream.ReadByte(); //X和Y的密度单位,units=0：无单位,units=1：点数/英寸,units=2：点数/厘米
                        //水平方向(像素/英寸)分辨率
                        Wpx = F_Stream.ReadByte() * 256;
                        Wpx = Wpx + F_Stream.ReadByte();
                        if (units == 2) Wpx = (float)(Wpx * 2.54); //厘米变为英寸
                        //垂直方向(像素/英寸)分辨率
                        Hpx = F_Stream.ReadByte() * 256;
                        Hpx = Hpx + F_Stream.ReadByte();
                        if (units == 2) Hpx = (float)(Hpx * 2.54); //厘米变为英寸
                        //后面信息忽略
                        if (rx != 2 && rx < 3) rx = rx + 2;
                        break;
                    default: //别的段都跳过////////////////
                        ps = F_Stream.ReadByte() * 256;
                        ps = F_Stream.Position + ps + F_Stream.ReadByte() - 2; //加段长度
                        break;
                }
                if (ps + 1 >= F_Stream.Length) //文件结束
                {
                    F_Stream.Close();
                    return rx;
                }
                F_Stream.Position = ps; //移动指针
            } while (type != 0xda); // 扫描行开始
            F_Stream.Close();
            return rx;
        }
    }
}
