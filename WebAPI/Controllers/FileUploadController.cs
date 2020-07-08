using BLL;
using System;
using System.Configuration;
using System.IO;
using System.Web;
using System.Web.Http;

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

        #region 保存附件信息
        /// <summary>
        /// 保存附件信息
        /// </summary>
        /// <param name="toDoItem">附件</param>
        [HttpPost]
        public Model.ResponeData SaveAttachUrl([FromBody] Model.ToDoItem toDoItem)
        {
            var responeData = new Model.ResponeData();
            try
            {
                APIUpLoadFileService.SaveAttachUrl(toDoItem);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }
            return responeData;
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
