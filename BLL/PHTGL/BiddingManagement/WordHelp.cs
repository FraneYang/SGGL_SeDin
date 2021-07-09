using Aspose.Words;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BLL 
{
    /// <summary>
    /// word文档操作辅助类
    /// </summary>
    public class AsposeWordHelper
    {
        /// <summary>
        /// Word
        /// </summary>
        private Document wordDoc;

        /// <summary>
        /// 基于模版新建Word文件
        /// </summary>
        /// <param name="path">模板路径</param>
        public void OpenTempelte(string path)
        {
            wordDoc = new Document(path);
        }

        /// <summary>
        /// 书签赋值用法
        /// </summary>
        /// <param name="LabelId">书签名</param>
        /// <param name="Content">内容</param>
        public void WriteBookMark(string LabelId, string Content)
        {
            if (wordDoc.Range.Bookmarks[LabelId] != null)
            {
                wordDoc.Range.Bookmarks[LabelId].Text = Content;
            }
        }

        /// <summary>
        /// 列表赋值用法
        /// </summary>
        /// <param name="dt"></param>
        public void WriteTable(DataTable dt)
        {
            wordDoc.MailMerge.ExecuteWithRegions(dt);
        }

        /// <summary>
        /// 文本域赋值用法
        /// </summary>
        /// <param name="fieldNames">key</param>
        /// <param name="fieldValues">value</param>
        public void Executefield(string[] fieldNames, object[] fieldValues)
        {
            wordDoc.MailMerge.Execute(fieldNames, fieldValues);
        }

        /// <summary>
        /// Pdf文件保存
        /// </summary>
        /// <param name="filename">文件路径+文件名</param>
        public void SavePdf(string filename)
        {
            wordDoc.Save(filename, SaveFormat.Pdf);
        }

        /// <summary>
        /// Doc文件保存
        /// </summary>
        /// <param name="filename">文件路径+文件名</param>
        public void SaveDoc(string filename)
        {
            wordDoc.Save(filename, SaveFormat.Doc);
        }

        /// <summary>
        /// 不可编辑受保护，需输入密码
        /// </summary>
        /// <param name="pwd">密码</param>
        public void NoEdit(string pwd)
        {
            wordDoc.Protect(ProtectionType.ReadOnly, pwd);
        }

        /// <summary>
        /// 只读
        /// </summary>
        public void ReadOnly()
        {
            wordDoc.Protect(ProtectionType.ReadOnly);
        }

        /// <summary>
        /// 通过流导出word文件
        /// </summary>
        /// <param name="stream">流</param>
        /// <param name="fileName">文件名</param>
        public static HttpResponseMessage ExportWord(Stream stream, string fileName)
        {
            var file = stream;
            fileName += DateTime.Now.ToString("yyyyMMddHHmmss");
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new StreamContent(file);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/msword");
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
            result.Content.Headers.ContentDisposition.FileName = fileName + ".doc";
            return result;
        }

        /// <summary>
        /// 通过流导出pdf文件
        /// </summary>
        /// <param name="stream">流</param>
        /// <param name="fileName">文件名</param>
        public static HttpResponseMessage ExportPdf(Stream stream, string fileName)
        {
            var file = stream;
            fileName += DateTime.Now.ToString("yyyyMMddHHmmss");
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new StreamContent(file);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
            result.Content.Headers.ContentDisposition.FileName = fileName + ".pdf";
            return result;
        }
        public static  void aa(string aa)
        {
            Document doc = new Document();
            doc.RemoveAllChildren();
 
                Document srcDoc = new Document(aa);

                doc.AppendDocument(srcDoc, ImportFormatMode.UseDestinationStyles);
           

        }
        public static  void InsertImg(Document doc, string rootPath, string BookmarksName, string ManId, string Idea)
        {
             Bookmark bookmarkCreateMan = doc.Range.Bookmarks[BookmarksName];

            if (bookmarkCreateMan != null)
            {
                var user = UserService.GetUserByUserId(ManId);
                if (user != null)
                {
                    if (!string.IsNullOrEmpty(user.SignatureUrl))
                    {
                        var file = user.SignatureUrl;
                        if (!string.IsNullOrWhiteSpace(file))
                        {
                            string url = rootPath + file;
                            DocumentBuilder builders = new DocumentBuilder(doc);
                            builders.MoveToBookmark(BookmarksName);
                            if (!string.IsNullOrEmpty(url))
                            {
                                System.Drawing.Size JpgSize;
                                float Wpx;
                                float Hpx;
                                UploadAttachmentService.getJpgSize(url, out JpgSize, out Wpx, out Hpx);
                                double i = 1;
                                i = JpgSize.Width / 50.0;
                                if (File.Exists(url))
                                {
                                    bookmarkCreateMan.Text = Idea;
                                    builders.InsertImage(url, JpgSize.Width *1.6/ i, JpgSize.Height * 1.2 / i);
                                    
                                }
                                else
                                {
                                    bookmarkCreateMan.Text = user.UserName;
                                }

                            }
                        }
                    }
                    else
                    {
                        bookmarkCreateMan.Text = user.UserName;
                    }
                }
            }
        }

        /// <summary>
        /// html代码转word
        /// </summary>
        /// <param name="Html">html代码</param>
        /// <param name="path">保存路径</param>
        public static void HtmlIntoWord(string Html,string path)
        {

            StringBuilder sb = new StringBuilder();
            sb.Append(
                "<html xmlns:v=\"urn:schemas-microsoft-com:vml\"  xmlns:o=\"urn:schemas-microsoft-com:office:office\" xmlns:w=\"urn:schemas-microsoft-com:office:word\" xmlns:m=\"http://schemas.microsoft.com/office/2004/12/omml\"xmlns = \"http://www.w3.org/TR/REC-html40\">");
            sb.Append(Html);
            sb.Append("</html>");
            var html = sb.ToString();//读取html内容
             StreamWriter sw = new StreamWriter(path, false, Encoding.GetEncoding("utf-8"));
            sw.WriteLine(html);
            sw.Flush();
            sw.Close();
        }

    }
}
