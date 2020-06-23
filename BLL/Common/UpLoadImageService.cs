using System;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Web;

namespace BLL
{
    public static class UpLoadImageService
    {
        /// <summary>
        ///  asp.net上传图片并生成缩略图 
        /// </summary>
        /// <param name="myFile">文件</param>
        /// <param name="sSavePath">保存的路径,些为相对服务器路径的下的文件夹</param>
        /// <param name="sThumbExtension">缩略图的thumb</param>
        /// <param name="intThumbWidth">生成缩略图的宽度</param>
        /// <param name="intThumbHeight">生成缩略图的高度</param>
        /// <returns></returns>
        public static string UpLoadImage(HttpPostedFile myFile, string sSavePath, string sThumbExtension, int intThumbWidth, int intThumbHeight)
        {
            string sThumbFile = "";
            string sFilename = "";
            int nFileLen = myFile.ContentLength;
            if (nFileLen == 0)
            {
                return "没有选择上传图片";
            }

            //获取upImage选择文件的扩展名 
            string extendName = Path.GetExtension(myFile.FileName).ToLower();
            //判断是否为图片格式 
            if (extendName != ".jpg" && extendName != ".jpge" && extendName != ".gif" && extendName != ".bmp" && extendName != ".png")
            { return "图片格式不正确"; }

            byte[] myData = new Byte[nFileLen];
            myFile.InputStream.Read(myData, 0, nFileLen);
            sFilename = SQLHelper.GetNewID();
            string localRoot = ConfigurationManager.AppSettings["localRoot"];
             string bigPath = localRoot + sSavePath;//物理路径
            if (!Directory.Exists(bigPath))
            {
                Directory.CreateDirectory(bigPath);
            }

            FileStream newFile = new FileStream(bigPath + sFilename+ extendName, FileMode.Create, FileAccess.Write);
            newFile.Write(myData, 0, myData.Length);
            newFile.Close();
            //以上为上传原图 

            try
            {
                //原图加载 
                using (Image sourceImage = Image.FromFile(bigPath + sFilename + extendName))
                {
                    //原图宽度和高度 
                    int width = sourceImage.Width;
                    int height = sourceImage.Height;
                    int smallWidth;
                    int smallHeight;

                    //获取第一张绘制图的大小,(比较 原图的宽/缩略图的宽   和 原图的高/缩略图的高) 
                    if (((decimal)width) / height <= ((decimal)intThumbWidth) / intThumbHeight)
                    {
                        smallWidth = intThumbWidth;
                        smallHeight = intThumbWidth * height / width;
                    }
                    else
                    {
                        smallWidth = intThumbHeight * width / height;
                        smallHeight = intThumbHeight;
                    }

                    string localRoots = localRoot + sThumbExtension;
                    if (!Directory.Exists(localRoots))
                    {
                        Directory.CreateDirectory(localRoots);
                    }
                    //判断缩略图在当前文件夹下是否同名称文件存在                
                    sThumbFile = localRoots + SQLHelper.GetNewID() + extendName;
                    //缩略图保存的绝对路径 
                    string smallImagePath = sThumbFile;

                    //新建一个图板,以最小等比例压缩大小绘制原图 
                    using (Image bitmap = new Bitmap(smallWidth, smallHeight))
                    {
                        //绘制中间图 
                        using (Graphics g = Graphics.FromImage(bitmap))
                        {
                            //高清,平滑 
                            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                            g.Clear(Color.Black);
                            g.DrawImage(
                            sourceImage,
                            new System.Drawing.Rectangle(0, 0, smallWidth, smallHeight),
                            new System.Drawing.Rectangle(0, 0, width, height),
                            System.Drawing.GraphicsUnit.Pixel
                            );
                        }
                        //新建一个图板,以缩略图大小绘制中间图 
                        using (System.Drawing.Image bitmap1 = new System.Drawing.Bitmap(intThumbWidth, intThumbHeight))
                        {
                            //绘制缩略图 
                            using (System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bitmap1))
                            {
                                //高清,平滑 
                                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                                g.Clear(Color.Black);
                                int lwidth = (smallWidth - intThumbWidth) / 2;
                                int bheight = (smallHeight - intThumbHeight) / 2;
                                g.DrawImage(bitmap, new Rectangle(0, 0, intThumbWidth, intThumbHeight), lwidth, bheight, intThumbWidth, intThumbHeight, GraphicsUnit.Pixel);
                                g.Dispose();
                                bitmap1.Save(smallImagePath, System.Drawing.Imaging.ImageFormat.Jpeg);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //出错则删除 
                File.Delete(localRoot + sFilename);
                return "图片格式不正确";
            }
            //返回缩略图名称 
            return sThumbFile;
        }
    }
}