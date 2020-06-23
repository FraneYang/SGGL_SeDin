using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.SessionState;

namespace FineUIPro.Web.AttachFile
{
    /// <summary>
    /// fileupload 的摘要说明
    /// </summary>
    public class fileupload : IHttpHandler, IRequiresSessionState
    {
        private void ResponseError(HttpContext context)
        {
            // 出错了
            context.Response.StatusCode = 500;
            context.Response.Write("No file");
        }

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            string owner = context.Request.Form["owner"];
            string sessionName = owner.Split('|')[0];
            string attachPath = owner.Split('|')[1];

            string initFullPath = BLL.Funs.RootPath + attachPath;
            if (!Directory.Exists(initFullPath))
            {
                Directory.CreateDirectory(initFullPath);
            }
            if (context.Request.Files.Count == 0)
            {
                ResponseError(context);
                return;
            }

            if (String.IsNullOrEmpty(owner))
            {
                ResponseError(context);
                return;
            }

            HttpPostedFile postedFile = context.Request.Files[0];
            // 文件名完整路径
            string fileName = postedFile.FileName;
            // 文件名保存的服务器路径
            string savedFileName = GetSavedFileName(fileName);
            postedFile.SaveAs(context.Server.MapPath("~/" + attachPath + "/" + savedFileName));

            string shortFileName = GetFileName(fileName);
            string fileType = GetFileType(fileName);
            int fileSize = postedFile.ContentLength;


            JObject fileObj = new JObject();
            string fileId = Guid.NewGuid().ToString();

            fileObj.Add("name", shortFileName);
            fileObj.Add("type", fileType);
            fileObj.Add("savedName", savedFileName);
            fileObj.Add("size", fileSize);
            fileObj.Add("id", fileId);

            SaveToDatabase(context, sessionName, fileObj);

            context.Response.Write("Success");
        }

        private void SaveToDatabase(HttpContext context, string sessionName, JObject fileObj)
        {
            if (context.Session[sessionName] == null)
            {
                context.Session[sessionName] = new JArray();
            }

            JArray source = context.Session[sessionName] as JArray;
            source.Add(fileObj);

            context.Session[sessionName] = source;
        }


        private string GetFileType(string fileName)
        {
            string fileType = String.Empty;
            int lastDotIndex = fileName.LastIndexOf(".");
            if (lastDotIndex >= 0)
            {
                fileType = fileName.Substring(lastDotIndex + 1).ToLower();
            }

            return fileType;
        }

        private string GetFileName(string fileName)
        {
            string shortFileName = fileName;
            int lastSlashIndex = shortFileName.LastIndexOf("\\");
            if (lastSlashIndex >= 0)
            {
                shortFileName = shortFileName.Substring(lastSlashIndex + 1);
            }

            return shortFileName;
        }

        private string GetSavedFileName(string fileName)
        {
            fileName = fileName.Replace(":", "_").Replace(" ", "_").Replace("\\", "_").Replace("/", "_");
            fileName = DateTime.Now.Ticks.ToString() + "_" + fileName;

            return fileName;
        }


        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}