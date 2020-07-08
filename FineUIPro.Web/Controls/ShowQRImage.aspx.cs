using BLL;
using System;
using System.IO;
using System.Text;
using System.Web.UI;
using ThoughtWorks.QRCode.Codec;

namespace FineUIPro.Web.Controls
{
    public partial class ShowQRImage : Page
    {
        #region 自定义
        /// <summary>
        /// 二维码路径id
        /// </summary>
        public string FileUrl
        {
            get
            {
                return (string)ViewState["FileUrl"];
            }
            set
            {
                ViewState["FileUrl"] = value;
            }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.txtName.InnerText = System.Web.HttpUtility.UrlDecode(Request.Params["title"]);
                this.FileUrl = "FileUpload\\ShowQRImage\\";
                string urlName = Request.Params["urlName"];               
                string filePath = this.FileUrl + urlName + ".jpg";
                if (!File.Exists(Funs.RootPath + filePath))
                {
                    this.CreateCode_Simple(Request.Params["strValue"], urlName);
                }

                this.divBeImageUrl.InnerHtml = UploadAttachmentService.ShowImage("../", filePath, 280, 280);
            }
        }

        /// <summary>
        /// 生成二维码方法
        /// </summary>
        /// <param name="nr"></param>
        /// <param name="urlName"></param>
        private void CreateCode_Simple(string nr, string urlName)
        {
            string imageUrl = string.Empty;
            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder
            {
                QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE,
                QRCodeScale = nr.Length,
                QRCodeVersion = 0,
                QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M
            };
            System.Drawing.Image image = qrCodeEncoder.Encode(nr, Encoding.UTF8);
            
            string filepath = Funs.RootPath + this.FileUrl;
            ////如果文件夹不存在，则创建
            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }

            string filename = urlName + ".jpg";
            imageUrl = filepath + filename;

            FileStream fs = new FileStream(imageUrl, FileMode.OpenOrCreate, FileAccess.Write);
            image.Save(fs, System.Drawing.Imaging.ImageFormat.Jpeg);

            fs.Close();
            image.Dispose();
            
        }
    }
}
