using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.Personal
{
    public partial class PersonalSet : PageBase
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
        /// 签名附件路径
        /// </summary>
        public string SignatureUrl
        {
            get
            {
                return (string)ViewState["SignatureUrl"];
            }
            set
            {
                ViewState["SignatureUrl"] = value;
            }
        }
        #endregion

        /// <summary>
        /// 加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {              
                /// Tab1加载页面方法
                this.Tab1LoadData();
            }
        }

        #region Tab1
        /// <summary>
        /// Tab1加载页面方法
        /// </summary>
        private void Tab1LoadData()
        {
            //性别       
            BLL.ConstValue.InitConstValueDropDownList(this.drpSex, ConstValue.Group_0002, true);
            //婚姻状况       
            BLL.ConstValue.InitConstValueDropDownList(this.drpMarriage, ConstValue.Group_0003, true);
            //民族           
            BLL.ConstValue.InitConstValueDropDownList(this.drpNation, ConstValue.Group_0005, true);
            //所在单位
            //BLL.UnitService.InitUnitDropDownList(this.drpUnit, this.CurrUser.LoginProjectId, true);          
            //文化程度         
            BLL.ConstValue.InitConstValueDropDownList(this.drpEducation, ConstValue.Group_0004, true);
            //职务          
            // BLL.PositionService.InitPositionDropDownList(this.drpPosition, true);

            var user = Funs.DB.Sys_User.FirstOrDefault(x => x.UserId == this.CurrUser.UserId);
            if (user != null)
            {
                this.txtUserName.Text = user.UserName;
                this.txtUserCode.Text = user.UserCode;
                if (!string.IsNullOrEmpty(user.Sex))
                {
                    this.drpSex.SelectedValue = user.Sex;
                }
                this.dpBirthDay.Text = string.Format("{0:yyyy-MM-dd}", user.BirthDay);
                if (!string.IsNullOrEmpty(user.Marriage))
                {
                    this.drpMarriage.SelectedValue = user.Marriage;
                }
                if (!string.IsNullOrEmpty(user.Nation))
                {
                    this.drpNation.SelectedValue = user.Nation;
                }
                if (!string.IsNullOrEmpty(user.UnitId))
                {
                    this.drpUnit.SelectedValue = user.UnitId;
                }
                this.txtAccount.Text = user.Account;
                this.txtIdentityCard.Text = user.IdentityCard;
                this.txtEmail.Text = user.Email;
                this.txtTelephone.Text = user.Telephone;
                if (!string.IsNullOrEmpty(user.Education))
                {
                    this.drpEducation.SelectedValue = user.Education;
                }
                this.txtHometown.Text = user.Hometown;
                if (!string.IsNullOrEmpty(user.PositionId))
                {
                    this.drpPosition.SelectedValue = user.PositionId;
                }
                this.txtPerformance.Text = user.Performance;
                if (!string.IsNullOrEmpty(user.PhotoUrl))
                {
                    this.PhotoAttachUrl = user.PhotoUrl;
                    this.Image1.ImageUrl = "~/" + this.PhotoAttachUrl;
                }
                if (!string.IsNullOrEmpty(user.SignatureUrl))
                {
                    this.SignatureUrl = user.SignatureUrl;
                    this.Image2.ImageUrl = "~/" + this.SignatureUrl;
                }
                this.LabelName.Text = user.UserName;
                this.LabelAccount.Text = user.Account;
                if (user.PageSize.HasValue)
                {
                    this.drpPageSize.SelectedValue = user.PageSize.ToString();
                }
                else
                {
                    this.drpPageSize.SelectedValue = "10";
                }
            }
        }

        #region 照片上传
        /// <summary>
        /// 上传照片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnPhoto_Click(object sender, EventArgs e)
        {
            if (filePhoto.HasFile)
            {
                string fileName = filePhoto.ShortFileName;
                if (!ValidateFileType(fileName))
                {
                    ShowNotify("无效的文件类型！", MessageBoxIcon.Warning);
                    return;
                }
              //  this.PhotoAttachUrl = UploadFileService.UploadAttachment(Funs.RootPath, this.filePhoto, this.PhotoAttachUrl, UploadFileService.UserFilePath);
                this.Image1.ImageUrl = "~/" + this.PhotoAttachUrl;
            }
        }
        /// <summary>
        /// 上传签名
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSignature_Click(object sender, EventArgs e)
        {
            if (fileSignature.HasFile)
            {
                string fileName = fileSignature.ShortFileName;
                if (!ValidateFileType(fileName))
                {
                    ShowNotify("无效的文件类型！", MessageBoxIcon.Warning);
                    return;
                }
                //this.SignatureUrl = UploadFileService.UploadAttachment(Funs.RootPath, this.fileSignature, this.SignatureUrl, UploadFileService.UserFilePath);
                this.Image2.ImageUrl = "~/" + this.SignatureUrl;
            }
        }
        #endregion     

        #region Tab1保存按钮
        /// <summary>
        /// Tab1保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnTab1Save_Click(object sender, EventArgs e)
        {
            //if (BLL.UserService.IsExistUserAccount(this.CurrUser.UserId, this.txtAccount.Text.Trim()) == true)
            //{
            //    ShowNotify("登录账号已存在，请修改后再保存！", MessageBoxIcon.Warning);
            //    return;
            //}
            //if (!string.IsNullOrEmpty(this.txtIdentityCard.Text) && BLL.UserService.IsExistUserIdentityCard(this.CurrUser.UserId, this.txtIdentityCard.Text.Trim()) == true)
            //{
            //    ShowNotify("身份证号码已存在，请修改后再保存！", MessageBoxIcon.Warning);
            //    return;
            //}
            var newUser = Funs.DB.Sys_User.FirstOrDefault(x => x.UserId == this.CurrUser.UserId);
            if (newUser != null)
            {
                newUser.UserName = this.txtUserName.Text.Trim();
                newUser.UserCode = this.txtUserCode.Text.Trim();
                if (this.drpSex.SelectedValue != BLL.Const._Null)
                {
                    newUser.Sex = this.drpSex.SelectedValue;
                }
                newUser.BirthDay = Funs.GetNewDateTime(this.dpBirthDay.Text);
                if (this.drpMarriage.SelectedValue != BLL.Const._Null)
                {
                    newUser.Marriage = this.drpMarriage.SelectedValue;
                }
                if (this.drpNation.SelectedValue != BLL.Const._Null)
                {
                    newUser.Nation = this.drpNation.SelectedValue;
                }
                if (this.drpUnit.SelectedValue != BLL.Const._Null)
                {
                    newUser.UnitId = this.drpUnit.SelectedValue;
                }
                newUser.Account = this.txtAccount.Text.Trim();
                newUser.IdentityCard = this.txtIdentityCard.Text.Trim();
                newUser.Email = this.txtEmail.Text.Trim();
                newUser.Telephone = this.txtTelephone.Text.Trim();
                if (this.drpEducation.SelectedValue != BLL.Const._Null)
                {
                    newUser.Education = this.drpEducation.SelectedValue;
                }
                newUser.Hometown = this.txtHometown.Text.Trim();
                if (this.drpPosition.SelectedValue != BLL.Const._Null)
                {
                    newUser.PositionId = this.drpPosition.SelectedValue;
                }
                newUser.Performance = this.txtPerformance.Text.Trim();
                newUser.PhotoUrl = this.PhotoAttachUrl;
                newUser.SignatureUrl = this.SignatureUrl;
                newUser.PageSize = Funs.GetNewInt(this.drpPageSize.SelectedValue);
                ShowNotify("保存成功！", MessageBoxIcon.Success);
                //LogService.AddSys_Log(this.CurrUser, newUser.UserCode, newUser.UserId, BLL.Const.UserMenuId, BLL.Const.BtnModify);
                PageContext.RegisterStartupScript(ActiveWindow.GetHideReference());
            }            
        }
        #endregion
        #endregion
       
        #region Tab2保存按钮
        /// <summary>
        /// Tab2保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnTab2Save_Click(object sender, EventArgs e)
        {
            var user =  Funs.DB.Sys_User.FirstOrDefault(x => x.UserId == this.CurrUser.UserId);
            if (user != null)
            {
                if (string.IsNullOrEmpty(this.txtOldPassword.Text))
                {
                    Alert.ShowInParent("请输入原密码！");
                    return;
                }

                if (user.Password != Funs.EncryptionPassword(this.txtOldPassword.Text))
                {
                    Alert.ShowInParent("原密码输入不正确！");
                    return;
                }
                if (this.txtNewPassword.Text != this.txtConfirmPassword.Text)
                {
                    Alert.ShowInParent("确认密码输入不一致！");
                    return;
                }

                //BLL.UserService.UpdatePassword(user.UserId, this.txtNewPassword.Text);
                //BLL.LogService.AddSys_Log(this.CurrUser, user.UserCode, user.UserId, BLL.Const.UserMenuId, BLL.Const.BtnModify);
                PageContext.RegisterStartupScript(ActiveWindow.GetHideReference());
            }
        }
        #endregion

    }
}