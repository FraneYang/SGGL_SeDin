using BLL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Text;
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
                    string rootUrl = string.Empty;
                    string fileName = string.Empty;
                    HttpPostedFile file = files[key];//file.ContentLength文件长度
                    if (!string.IsNullOrEmpty(file.FileName))
                    {
                        fileName = SQLHelper.GetNewID() + Path.GetExtension(file.FileName);
                        rootUrl = localRoot + fileName;
                        file.SaveAs(localRoot + fileName);                      
                    }

                    string TakePicDateTime = string.Empty;
                    System.Drawing.Image image = System.Drawing.Image.FromStream(file.InputStream, true, false);
                    Encoding ascii = Encoding.ASCII;
                    //遍历图像文件元数据，检索所有属性
                    foreach (System.Drawing.Imaging.PropertyItem p in image.PropertyItems)
                    {
                        //如果是PropertyTagDateTime，则返回该属性所对应的值
                        if (p.Id == 0x0132)
                        {
                            TakePicDateTime = ascii.GetString(p.Value);
                        }
                    }

                    if (!string.IsNullOrEmpty(TakePicDateTime))
                    {
                        ////获取元数据中的拍照日期时间，以字符串形式保存
                        //TakePicDateTime = GetTakePicDateTime(pi);
                        //分析字符串分别保存拍照日期和时间的标准格式
                        var SpaceLocation = TakePicDateTime.IndexOf(" ");
                        var dt = TakePicDateTime.Substring(0, SpaceLocation);
                        dt = dt.Replace(":", "-");
                        var tm = TakePicDateTime.Substring(SpaceLocation + 1, TakePicDateTime.Length - SpaceLocation - 2);
                        TakePicDateTime = dt + " " + tm;
                        //由列表中的文件创建内存位图对象
                        var Pic = new Bitmap(rootUrl);
                        //由位图对象创建Graphics对象的实例
                        var g = Graphics.FromImage(Pic);
                        Font ft = new Font("宋体", 50, FontStyle.Regular, GraphicsUnit.Point, ((byte)(134)));//定义字体
                         //在Graphics表面绘制数码照片的日期/时间戳
                        g.DrawString(TakePicDateTime, ft, Brushes.Gold, 0, Pic.Height-100);
                        //  - 50);
                        string newRoot = localRoot + "newfile/";
                        if (!Directory.Exists(newRoot))
                        {
                            Directory.CreateDirectory(newRoot);
                        }

                        //将添加日期/时间戳后的图像进行保存                       
                        Pic.Save(newRoot + fileName);
                        fileName = "newfile/" + fileName;
                        //释放内存位图对象
                        Pic.Dispose();
                    }

                    if (!string.IsNullOrEmpty(fileName))
                    {
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
