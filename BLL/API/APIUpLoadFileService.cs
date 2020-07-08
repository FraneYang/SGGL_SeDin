using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace BLL
{
    public static class APIUpLoadFileService
    {
        public static string Pic(string url, string filePath)
        {
            // 时间戳，用做boundary
            string timeStamp = DateTime.Now.Ticks.ToString("x");
            //根据uri创建HttpWebRequest对象
            HttpWebRequest httpReq = (HttpWebRequest)WebRequest.Create(new Uri(url));
            httpReq.Method = "POST";
            httpReq.AllowWriteStreamBuffering = false; //对发送的数据不使用缓存
            httpReq.Timeout = 300000;  //设置获得响应的超时时间（300秒）
            httpReq.ContentType = "multipart/form-data; boundary=" + timeStamp;
            //文件
            FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            BinaryReader binaryReader = new BinaryReader(fileStream);
            //头信息
            string boundary = "--" + timeStamp;
            string dataFormat = boundary + "\r\nContent-Disposition: form-data; name=imgs;filename=\"{1}\"\r\nContent-Type:application/octet-stream\r\n\r\n";
            string header = string.Format(dataFormat, "file", Path.GetFileName(filePath));
            byte[] postHeaderBytes = Encoding.UTF8.GetBytes(header);

            //结束边界
            byte[] boundaryBytes = Encoding.ASCII.GetBytes("\r\n--" + timeStamp + "--\r\n");

            long length = fileStream.Length + postHeaderBytes.Length + boundaryBytes.Length;

            httpReq.ContentLength = length;//请求内容长度
            string returnValue = "";
            try
            {
                //每次上传4k
                int bufferLength = 4096;
                byte[] buffer = new byte[bufferLength];

                //已上传的字节数
                long offset = 0;
                int size = binaryReader.Read(buffer, 0, bufferLength);
                Stream postStream = httpReq.GetRequestStream();

                //发送请求头部消息
                postStream.Write(postHeaderBytes, 0, postHeaderBytes.Length);

                while (size > 0)
                {
                    postStream.Write(buffer, 0, size);
                    offset += size;
                    size = binaryReader.Read(buffer, 0, bufferLength);
                }

                //添加尾部边界
                postStream.Write(boundaryBytes, 0, boundaryBytes.Length);
                postStream.Close();

                //获取服务器端的响应
                using (HttpWebResponse response = (HttpWebResponse)httpReq.GetResponse())
                {
                    Stream receiveStream = response.GetResponseStream();
                    StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
                    returnValue = readStream.ReadToEnd();
                    response.Close();
                    readStream.Close();
                }
            }
            catch (Exception ex)
            {
                returnValue = "";
            }
            finally
            {
                fileStream.Close();
                binaryReader.Close();
            }
            return returnValue;
        }


        public static String saveImg(String url)
        {
            WebClient mywebclient = new WebClient();
            string newfilename = Guid.NewGuid() + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + ".jpg";
            string subPath = @"d:\imgs\";
            if (false == System.IO.Directory.Exists(subPath))
            {
                //创建pic文件夹
                Directory.CreateDirectory(subPath);
            }
            string filepath = @"d:\imgs\" + newfilename;
            try
            {
                mywebclient.DownloadFile(url, filepath);
            }
            catch (Exception ex)
            {
                filepath = "";
            }
            return filepath;
        }

        /// <summary>
        /// 保存附件方法
        /// </summary>
        public static void SaveAttachUrl(Model.ToDoItem toDoItem)
        {
            ////保存附件
            if (!string.IsNullOrEmpty(toDoItem.UrlStr))
            {
                if (toDoItem.IsInsert == "1")
                {
                    var att = new Model.SGGLDB(Funs.ConnString).AttachFile.FirstOrDefault(x => x.ToKeyId == toDoItem.DataId);
                    if (att != null)
                    {
                        toDoItem.UrlStr = att.AttachUrl + "," + toDoItem.UrlStr;
                    }
                }
                UploadFileService.SaveAttachUrl(UploadFileService.GetSourceByAttachUrl(toDoItem.UrlStr, 10, null), toDoItem.UrlStr, toDoItem.MenuId, toDoItem.DataId);
            }
            else
            {
                CommonService.DeleteAttachFileById(toDoItem.DataId);
            }
        }

        public static void SaveAttachUrl(string menuId, string dataId, string url, string isInsert)
        {
            ////保存附件
            if (!string.IsNullOrEmpty(url))
            {
                if (isInsert == "1")
                {
                    var att = new Model.SGGLDB(Funs.ConnString).AttachFile.FirstOrDefault(x => x.ToKeyId == dataId);
                    if (att != null)
                    {
                        url = att.AttachUrl + "," + url;
                    }
                }
                UploadFileService.SaveAttachUrl(UploadFileService.GetSourceByAttachUrl(url, 10, null), url, menuId, dataId);
            }
            else
            {
                CommonService.DeleteAttachFileById(dataId);
            }
        }

        /// <summary>
        ///  获取附件路径
        /// </summary>
        /// <returns></returns>
        public static string getFileUrl(string tokeyId, string url)
        {
            string fileUrl = url;
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                if (!string.IsNullOrEmpty(tokeyId))
                {
                    var getAtt = db.AttachFile.FirstOrDefault(x => x.ToKeyId == tokeyId);
                    if (getAtt != null && !string.IsNullOrEmpty(getAtt.AttachUrl))
                    {
                        fileUrl = getAtt.AttachUrl;
                    }
                }

                if (!string.IsNullOrEmpty(fileUrl))
                {
                    fileUrl = fileUrl.Replace('\\', '/');
                }
            }
            return fileUrl;
        }

        /// <summary>
        ///  获取附件路径
        /// </summary>
        /// <returns></returns>
        public static string getFileUrl(string menuId, string tokeyId, string url)
        {
            string fileUrl = url;
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                if (!string.IsNullOrEmpty(tokeyId))
                {
                    var getAtt = db.AttachFile.FirstOrDefault(x =>x.MenuId== menuId &&  x.ToKeyId == tokeyId);
                    if (getAtt != null && !string.IsNullOrEmpty(getAtt.AttachUrl))
                    {
                        fileUrl = getAtt.AttachUrl;
                    }
                }

                if (!string.IsNullOrEmpty(fileUrl))
                {
                    fileUrl = fileUrl.Replace('\\', '/');
                }
            }
            return fileUrl;
        }
    }
}
