using System;
using System.IO;
using System.Text;
using ThoughtWorks.QRCode.Codec;
using System.Web.UI;

namespace BLL
{
    /// <summary>
    /// 上传附件相关
    /// </summary>
    public class CreateQRCodeService
    {
        /// <summary>
        /// 生成二维码方法一
        /// </summary>
        public static string CreateCode_Simple(string nr)
        {
            string imageUrl = string.Empty;
            if (!string.IsNullOrEmpty(nr))
            {
                QRCodeEncoder qrCodeEncoder = new QRCodeEncoder
                {
                    QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE,
                    QRCodeScale = nr.Length,
                    QRCodeVersion = 0,
                    QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M
                };
                System.Drawing.Image image = qrCodeEncoder.Encode(nr, Encoding.UTF8);
                string filepath = Funs.RootPath + UploadFileService.QRCodeImageFilePath;
                //如果文件夹不存在，则创建
                if (!Directory.Exists(filepath))
                {
                    Directory.CreateDirectory(filepath);
                }
                string filename = DateTime.Now.ToString("yyyymmddhhmmssfff").ToString() + ".jpg";
                FileStream fs = new FileStream(filepath + filename, FileMode.OpenOrCreate, FileAccess.Write);
                image.Save(fs, System.Drawing.Imaging.ImageFormat.Jpeg);
                fs.Close();
                image.Dispose();
                imageUrl= UploadFileService.QRCodeImageFilePath + filename;
            }
            return imageUrl;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static bool isHaveImage(string url)
        {
            string filepath = Funs.RootPath + url;
            return File.Exists(filepath);
        }
    }
}