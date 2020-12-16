using System;
using System.Linq;
using System.Drawing;
using ThoughtWorks.QRCode.Codec;
using ThoughtWorks.QRCode.Codec.Data;
using System.Text;
using System.IO;
using BLL;

namespace FineUIPro.Web.HJGL.PersonManage
{
    public partial class WelderManageEdit : PageBase
    {
        #region 定义变量
        /// <summary>
        /// 照片附件路径
        /// </summary>
        public string PhotoAttachUrl
        {
            get
            {
                return (string)ViewState["PhotoAttachUrl"];
            }
            set
            {
                ViewState["PhotoAttachUrl"] = value;
            }
        }

        /// <summary>
        /// 二维码路径id
        /// </summary>
        public string QRCodeAttachUrl
        {
            get
            {
                return (string)ViewState["QRCodeAttachUrl"];
            }
            set
            {
                ViewState["QRCodeAttachUrl"] = value;
            }
        }

        #endregion

        #region 加载
        /// <summary>
        /// 加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();

                //BLL.Base_ProjectTypeService.InitProjectTypeDropDownList(this.drpPojectType, true, "请选择");//项目类型
                BLL.UnitService.InitUnitByProjectIdUnitTypeDropDownList(drpUnitId, this.CurrUser.LoginProjectId, Const.ProjectUnitType_2, true);

                string PersonId = Request.Params["PersonId"];
                if (!string.IsNullOrEmpty(PersonId))
                {
                    Model.SitePerson_Person welder = BLL.WelderService.GetWelderById(PersonId);
                    if (welder != null)
                    {
                        this.txtWelderCode.Text = welder.WelderCode;
                        this.txtWelderName.Text = welder.PersonName;

                        if (!string.IsNullOrEmpty(welder.UnitId))
                        {
                            this.drpUnitId.SelectedValue = welder.UnitId;
                        }
                        this.rblSex.SelectedValue = welder.Sex;

                        if (welder.Birthday.HasValue)
                        {
                            this.txtBirthday.Text = string.Format("{0:yyyy-MM-dd}", welder.Birthday);
                        }
                        this.txtIdentityCard.Text = welder.IdentityCard;
                        this.txtCertificateCode.Text = welder.CertificateCode;
                        if (string.IsNullOrEmpty(welder.CertificateCode))
                        {
                            this.txtCertificateCode.Text = welder.IdentityCard;
                        }
                        if (welder.CertificateLimitTime.HasValue)
                        {
                            this.txtCertificateLimitTime.Text = string.Format("{0:yyyy-MM-dd}", welder.CertificateLimitTime);
                        }
                        this.txtWelderLevel.Text = welder.WelderLevel;
                        if (welder.IsUsed == true)
                        {
                            cbIsOnDuty.Checked = true;
                        }
                        else
                        {
                            cbIsOnDuty.Checked = false;
                        }
                        this.txtRemark.Text = welder.Remark;
                        if (!string.IsNullOrEmpty(welder.PhotoUrl))
                        {
                            this.PhotoAttachUrl = welder.PhotoUrl;
                            this.Image1.ImageUrl = "~/" + this.PhotoAttachUrl;
                        }
                        if (!string.IsNullOrEmpty(welder.QRCodeAttachUrl))
                        {
                            this.QRCodeAttachUrl = welder.QRCodeAttachUrl;
                            this.btnSee.Hidden = false;
                            this.btnQR.Hidden = false;
                            this.btnQR.Text = "二维码重新生成";
                        }
                    }
                }
                else
                {
                    this.cbIsOnDuty.Checked = true;
                }
            }
        }
        #endregion

        #region 保存
        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string PersonId = Request.Params["PersonId"];
            var q = Funs.DB.SitePerson_Person.FirstOrDefault(x => x.WelderCode == this.txtWelderCode.Text.Trim()
           && (x.PersonId != PersonId || (PersonId == null && PersonId != null)));
            if (q != null)
            {
                Alert.ShowInTop("焊工号已经存在！", MessageBoxIcon.Warning);
                return;
            }
            if (this.drpUnitId.SelectedValue == BLL.Const._Null)
            {
                Alert.ShowInTop("选择单位", MessageBoxIcon.Warning);
                return;
            }

            PersonId = SaveData(PersonId);
            ShowNotify("保存成功！", MessageBoxIcon.Success);
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }

        private string SaveData(string PersonId)
        {
            Model.SitePerson_Person newWelder = new Model.SitePerson_Person();
            newWelder.ProjectId = this.CurrUser.LoginProjectId;
            newWelder.WelderCode = this.txtWelderCode.Text.Trim();
            newWelder.PersonName = this.txtWelderName.Text.Trim();
            newWelder.WorkPostId = Const.WorkPost_Welder;
            if (this.drpUnitId.SelectedValue != BLL.Const._Null)
            {
                newWelder.UnitId = this.drpUnitId.SelectedValue;
            }
            newWelder.Sex = this.rblSex.SelectedValue;
            newWelder.Birthday = Funs.GetNewDateTime(this.txtBirthday.Text.Trim());
            newWelder.IdentityCard = this.txtIdentityCard.Text.Trim();
            newWelder.CertificateCode = this.txtCertificateCode.Text.Trim();
            newWelder.CertificateLimitTime = Funs.GetNewDateTime(this.txtCertificateLimitTime.Text.Trim());
            newWelder.WelderLevel = this.txtWelderLevel.Text.Trim();
            if (this.cbIsOnDuty.Checked)
            {
                newWelder.IsUsed = true;
            }
            else
            {
                newWelder.IsUsed = false;
            }
            newWelder.Remark = this.txtRemark.Text.Trim();
            newWelder.PhotoUrl = this.PhotoAttachUrl;
            newWelder.Isprint = "0";
            if (!string.IsNullOrEmpty(PersonId))
            {
                if (!BLL.WelderService.IsExisWelderCode(PersonId, this.txtWelderCode.Text))
                {
                    newWelder.PersonId = PersonId;
                    BLL.WelderService.UpdateWelder(newWelder);
                    //BLL.Sys_LogService.AddLog(Const.System_1, this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.WelderManageMenuId, Const.BtnModify, PersonId);
                }
                else
                {
                    Alert.ShowInParent("焊工号已经存在！", MessageBoxIcon.Warning);
                }
            }
            else
            {
                PersonId = SQLHelper.GetNewID(typeof(Model.SitePerson_Person));
                if (!BLL.WelderService.IsExisWelderCode(PersonId, this.txtWelderCode.Text))
                {
                    newWelder.PersonId = PersonId;
                //    BLL.WelderService.AddWelder(newWelder);
                    //BLL.Sys_LogService.AddLog(Const.System_1, this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.WelderManageMenuId, Const.BtnAdd, PersonId);
                }
                else
                {
                    Alert.ShowInParent("焊工号已经存在！", MessageBoxIcon.Warning);
                }
            }
            return PersonId;
        }
        #endregion

        protected void drpUnitId_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.drpUnitId.SelectedValue != Const._Null)
            {
                var u = BLL.UnitService.GetUnitByUnitId(drpUnitId.SelectedValue);
                string prefix = u.UnitCode + "-HG-";
                txtWelderCode.Text = BLL.SQLHelper.RunProcNewId("SpGetThreeNumber", "SitePerson_Person", "WelderCode", prefix);
            }
        }


        #region 附件上传
        /// <summary>
        /// 上传附件资源
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAttachUrl_Click(object sender, EventArgs e)
        {
            string edit = "0";
            string PersonId = Request.Params["PersonId"];
            if (string.IsNullOrEmpty(PersonId))
            {
                SaveData(PersonId);
            }
            else
            {
                edit = "1";
            }
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("../../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/WelderManage&menuId={1}&edit={2}", PersonId, BLL.Const.WelderManageMenuId, edit)));
        }
        #endregion

        #region 照片上传
        /// <summary>
        /// 上传照片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnPhoto_Click(object sender, EventArgs e)
        {
            //if (filePhoto.HasFile)
            //{
            //    string fileName = filePhoto.ShortFileName;

            //    if (!ValidateFileType(fileName))
            //    {
            //        ShowNotify("无效的文件类型", MessageBoxIcon.Warning);
            //        return;
            //    }
            //    this.PhotoAttachUrl = BLL.UploadFileService.UploadAttachment(BLL.Funs.RootPath, this.filePhoto, this.PhotoAttachUrl, Const.WelderFilePath);
            //    this.Image1.ImageUrl = "~/" + this.PhotoAttachUrl;
            //}
        }
        #endregion

        protected void btnQR_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtWelderCode.Text.Trim()))
            {
                Alert.ShowInTop("焊工证不能为空！", MessageBoxIcon.Warning);
                return;
            }

            this.CreateCode_Simple(this.txtWelderCode.Text.Trim());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSee_Click(object sender, EventArgs e)
        {
            string url = string.Empty;
            string PersonId = Request.Params["PersonId"];
            Model.SitePerson_Person welder = BLL.WelderService.GetWelderById(PersonId);

            if (welder != null && !string.IsNullOrEmpty(welder.QRCodeAttachUrl))
            {
                url = welder.QRCodeAttachUrl;
                PageContext.RegisterStartupScript(Window2.GetShowReference(String.Format("SeeQRImage.aspx?PersonId={0}", PersonId), "二维码查看", 400, 400));
            }
            else
            {
                Alert.ShowInTop("二维码不存在！", MessageBoxIcon.Warning);
                return;
            }
        }

        //生成二维码方法一
        private void CreateCode_Simple(string nr)
        {
            string PersonId = Request.Params["PersonId"];

            Model.SitePerson_Person welder = BLL.WelderService.GetWelderById(PersonId);
            if (welder != null)
            {
                BLL.UploadFileService.DeleteFile(Funs.RootPath, welder.QRCodeAttachUrl);//删除二维码
                string imageUrl = string.Empty;
                QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
                qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
                qrCodeEncoder.QRCodeScale = nr.Length;
                qrCodeEncoder.QRCodeVersion = 0;
                qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
                System.Drawing.Image image = qrCodeEncoder.Encode(nr, Encoding.UTF8);

                string filepath = Server.MapPath("~/") + BLL.UploadFileService.QRCodeImageFilePath;

                //如果文件夹不存在，则创建
                if (!Directory.Exists(filepath))
                {
                    Directory.CreateDirectory(filepath);
                }

                string filename = DateTime.Now.ToString("yyyymmddhhmmssfff").ToString() + ".jpg";
                imageUrl = filepath + filename;

                System.IO.FileStream fs = new System.IO.FileStream(imageUrl, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.Write);
                image.Save(fs, System.Drawing.Imaging.ImageFormat.Jpeg);

                fs.Close();
                image.Dispose();
                this.QRCodeAttachUrl = BLL.UploadFileService.QRCodeImageFilePath + filename;

                BLL.WelderService.UpdateQRCode(PersonId, QRCodeAttachUrl);
                this.btnSee.Hidden = false;
                this.btnQR.Hidden = false;
                this.btnQR.Text = "二维码重新生成";
                PageContext.RegisterStartupScript(Window2.GetShowReference(String.Format("SeeQRImage.aspx?PersonId={0}", PersonId), "二维码查看", 400, 400));
            }
            else
            {
                Alert.ShowInTop("操作有误，重新生成！", MessageBoxIcon.Warning);
            }

            //二维码解码
            //var codeDecoder = CodeDecoder(filepath);

            //this.Image1.ImageUrl = "~/image/" + filename + ".jpg";
        }

        /// <summary>
        /// 二维码解码
        /// </summary>
        /// <param name="filePath">图片路径</param>
        /// <returns></returns>
        public string CodeDecoder(string filePath)
        {
            //if (!System.IO.File.Exists(filePath))
            //    return null;
            //Bitmap myBitmap = new Bitmap(Image.FromFile(filePath));
            //QRCodeDecoder decoder = new QRCodeDecoder();
            //string decodedString = decoder.decode(new QRCodeBitmapImage(myBitmap));
            //return decodedString;
            return "";
        }
    }
}