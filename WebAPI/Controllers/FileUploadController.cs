using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.IO;
using System.Web;
using BLL;
using System.Configuration;

namespace WebAPI.Controllers
{
    /// <summary>
    /// 附件上传
    /// </summary>
    public class FileUploadController : ApiController
    {
        #region 附件上传
        /// <summary>
        /// 附件上传
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult Post()
        {
            HttpFileCollection files = HttpContext.Current.Request.Files;
            string typeName = HttpContext.Current.Request["typeName"];
            if (string.IsNullOrEmpty(typeName))
            {
                typeName = "WebApi";
            }
            string reUrl = string.Empty;
            if (files != null && files.Count > 0)
            {
                string folderUrl = "FileUpLoad/" + typeName + "/" + DateTime.Now.ToString("yyyy-MM") + "/";
                string localRoot = ConfigurationManager.AppSettings["localRoot"] + folderUrl; //物理路径
                if (!Directory.Exists(localRoot))
                {
                    Directory.CreateDirectory(localRoot);
                }
                foreach (string key in files.AllKeys)
                {
                    HttpPostedFile file = files[key];//file.ContentLength文件长度
                    if (!string.IsNullOrEmpty(file.FileName))
                    {
                        string fileName = SQLHelper.GetNewID()+ Path.GetExtension(file.FileName);
                        file.SaveAs(localRoot + fileName);
                        if (string.IsNullOrEmpty(reUrl))
                        {
                            reUrl += folderUrl + fileName;
                        }
                        else
                        {
                            reUrl += "," + folderUrl + fileName;
                        }
                    }
                }
            }

            return Ok(reUrl);
        }
        #endregion

        #region 附件上传
        /// <summary>
        /// 附件上传
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult PostSmall()
        {
            HttpFileCollection files = HttpContext.Current.Request.Files;
            string typeName = HttpContext.Current.Request["typeName"];
            if (string.IsNullOrEmpty(typeName))
            {
                typeName = "WebApi";
            }
            string reUrl = string.Empty;
            if (files != null && files.Count > 0)
            {
                string folderUrl = "FileUpLoad/" + typeName + "/" + DateTime.Now.ToString("yyyy-MM") + "/";
                string localRoot = ConfigurationManager.AppSettings["localRoot"] + folderUrl; //物理路径
                if (!Directory.Exists(localRoot))
                {
                    Directory.CreateDirectory(localRoot);
                }
                foreach (string key in files.AllKeys)
                {
                    HttpPostedFile file = files[key];//file.ContentLength文件长度
                    reUrl = UpLoadImageService.UpLoadImage(file, folderUrl, folderUrl + "Small/", 80, 30);
                }
            }

            return Ok(reUrl);
        }
        #endregion

    }
}
