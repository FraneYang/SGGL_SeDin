﻿using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FineUIPro.Web.Person
{
    public partial class PersonSetEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 员工主键
        /// </summary>
        public string UserId
        {
            get
            {
                return (string)ViewState["UserId"];
            }
            set
            {
                ViewState["UserId"] = value;
            }
        }
        /// <summary>
        /// 单位主键
        /// </summary>
        public string UnitId
        {
            get
            {
                return (string)ViewState["UnitId"];
            }
            set
            {
                ViewState["UnitId"] = value;
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
        /// 员工编辑页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PostTitleService.InitPostTitleDropDownList(this.drpPostTitle, true);
                PracticeCertificateService.InitPracticeCertificateDropDownList(this.drpCertificate, true);
                WorkPostService.InitWorkPostDropDownList(this.drpWorkPost, true);
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                ///权限
                this.GetButtonPower();
                this.UserId = Request.Params["userId"];
                this.UnitId = Request.Params["UnitId"];
                ConstValue.InitConstValueDropDownList(this.drpIsPost, ConstValue.Group_0001, false);
                ConstValue.InitConstValueDropDownList(this.drpIsOffice, ConstValue.Group_0001, false);
                UnitService.InitUnitDropDownList(this.drpUnit, this.CurrUser.LoginProjectId, true);
                DepartService.InitDepartDropDownList(this.drpDepart, true);
                if (!string.IsNullOrEmpty(Const.UnitId_SEDIN))
                {
                    this.drpUnit.SelectedValue =Const.UnitId_SEDIN;
                }
                this.drpDepart.SelectedValue =Const.Depart_constructionId;
                ///角色下拉框
                BLL.RoleService.InitRoleDropDownList(this.drpRole, string.Empty, true, true);
                if (!string.IsNullOrEmpty(this.UserId))
                {
                    var user = BLL.UserService.GetUserByUserId(this.UserId);
                    if (user != null)
                    {
                        if (!string.IsNullOrEmpty(user.UnitId))
                        {
                            this.drpUnit.SelectedValue = user.UnitId;
                        }
                        this.txtUserCode.Text = user.UserCode;
                        this.txtUserName.Text = user.UserName;
                        this.txtAccount.Text = user.Account;
                        if (!string.IsNullOrEmpty(user.RoleId))
                        {
                            this.drpRole.SelectedValue = user.RoleId;
                        }
                        if (user.IsPost.HasValue)
                        {
                            this.drpIsPost.SelectedValue = Convert.ToString(user.IsPost);
                        }
                        this.txtTelephone.Text = user.Telephone;
                        if (user.IsOffice == true)
                        {
                            this.drpIsOffice.SelectedValue = "True";
                        }
                        else
                        {
                            this.drpIsOffice.SelectedValue = "False";
                        }
                        this.txtIdentityCard.Text = user.IdentityCard;
                        this.txtPoliticalstatus.Text = user.Politicalstatus;
                        this.txtHometown.Text = user.Hometown;
                        this.txtEducation.Text = user.Education;
                        this.txtGraduate.Text = user.Graduate;
                        this.txtMajor.Text = user.Major;
                        if (!string.IsNullOrEmpty(user.PostTitleId))
                        {
                            this.drpPostTitle.SelectedValue = user.PostTitleId;
                        }
                        if (!string.IsNullOrEmpty(user.CertificateId))
                        {
                            this.drpCertificate.SelectedValueArray = user.CertificateId.Split(',');
                        }
                        if (!string.IsNullOrEmpty(user.WorkPostId))
                        {
                            this.drpWorkPost.SelectedValueArray = user.WorkPostId.Split(',');
                        }
                        this.rblSex.SelectedValue = user.Sex;
                        if (user.BirthDay.HasValue)
                        {
                            this.txtBirthday.Text = string.Format("{0:yyyy-MM-dd}", user.BirthDay);
                        }
                        if (user.IntoDate.HasValue)
                        {
                            this.txtIntoDate.Text = string.Format("{0:yyyy-MM-dd}", user.IntoDate);
                        }
                        if (user.ValidityDate.HasValue)
                        {
                            this.txtValidityDate.Text = string.Format("{0:yyyy-MM-dd}", user.ValidityDate);
                        }
                        if (!string.IsNullOrEmpty(user.SignatureUrl))
                        {
                            this.SignatureUrl = user.SignatureUrl;
                            this.Image2.ImageUrl = "~/" + this.SignatureUrl;
                        }
                        this.txtProjectId.Text = BLL.ProjectService.GetProjectNameByProjectId(user.ProjectId);
                        this.txtProjectRoleId.Text =RoleService.getRoleNamesRoleIds(user.ProjectRoleId);

                        var roleItem = BLL.RoleItemService.GeRoleItemByUserId(this.UserId);
                        if (roleItem != null)
                        {
                            this.txtIntoProjectDate.Text = roleItem.IntoDate.ToString();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (this.drpUnit.SelectedValue == Const._Null)
            {
                Alert.ShowInParent("请选择单位！", MessageBoxIcon.Warning);
                return;
            }
            if (BLL.UserService.IsExistUserAccount(this.UserId, this.txtAccount.Text.Trim()))
            {
                Alert.ShowInParent("员工账号已存在，请修改后再保存！", MessageBoxIcon.Warning);
                return;
            }
            if (!string.IsNullOrEmpty(this.txtIdentityCard.Text) && BLL.UserService.IsExistUserIdentityCard(this.UserId, this.txtIdentityCard.Text.Trim()) == true)
            {
                ShowNotify("身份证号码已存在，请修改后再保存！", MessageBoxIcon.Warning);
                return;
            }
            //if (this.txtIdentityCard.Text.Trim().Length!=18)
            //{
            //    ShowNotify("身份证号码必须是18位！", MessageBoxIcon.Warning);
            //    return;
            //}
            Model.Sys_User newUser = new Model.Sys_User
            {
                UserCode = this.txtUserCode.Text.Trim(),
                UserName = this.txtUserName.Text.Trim(),
                Account = this.txtAccount.Text.Trim(),
                IdentityCard = this.txtIdentityCard.Text.Trim(),
                Telephone = this.txtTelephone.Text.Trim(),
                Politicalstatus = this.txtPoliticalstatus.Text.Trim(),
                Hometown = this.txtHometown.Text.Trim(),
                Education = this.txtEducation.Text.Trim(),
                Graduate = this.txtGraduate.Text.Trim(),
                Major = this.txtMajor.Text.Trim(),
                Sex = this.rblSex.SelectedValue,
            };
            if (this.drpUnit.SelectedValue != Const._Null)
            {
                newUser.UnitId = this.drpUnit.SelectedValue;
            }
            if (!BLL.CommonService.IsMainUnitOrAdmin(this.CurrUser.UserId)) ///不是企业单位或者管理员
            {
                newUser.UnitId = this.CurrUser.UnitId;
            }
            if (this.drpRole.SelectedValue != Const._Null)
            {
                newUser.RoleId = this.drpRole.SelectedValue;
            }
            if (this.drpDepart.SelectedValue != Const._Null)
            {
                newUser.DepartId = this.drpDepart.SelectedValue;
            }
            foreach (var item in this.drpCertificate.SelectedValueArray)
            {
                if (item != BLL.Const._Null)
                {
                    if (string.IsNullOrEmpty(newUser.CertificateId))
                    {
                        newUser.CertificateId = item;
                    }
                    else
                    {
                        newUser.CertificateId += "," + item;
                    }
                }
            }
            foreach (var item in this.drpWorkPost.SelectedValueArray)
            {
                if (item != BLL.Const._Null)
                {
                    if (string.IsNullOrEmpty(newUser.WorkPostId))
                    {
                        newUser.WorkPostId = item;
                    }
                    else
                    {
                        newUser.WorkPostId += "," + item;
                    }
                }
            }
            if (this.drpPostTitle.SelectedValue != Const._Null)
            {
                newUser.PostTitleId = this.drpPostTitle.SelectedValue;
            }
            newUser.SignatureUrl = this.SignatureUrl;
            newUser.IsPost = Convert.ToBoolean(this.drpIsPost.SelectedValue);
            newUser.IsOffice = Convert.ToBoolean(this.drpIsOffice.SelectedValue);
            if (!string.IsNullOrEmpty(txtBirthday.Text.Trim()))
            {
                newUser.BirthDay = Funs.GetNewDateTime(this.txtBirthday.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.txtIntoDate.Text.Trim()))
            {
                newUser.IntoDate = Convert.ToDateTime(this.txtIntoDate.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.txtValidityDate.Text.Trim()))
            {
                newUser.ValidityDate = Convert.ToDateTime(this.txtValidityDate.Text.Trim());
            }
            if (string.IsNullOrEmpty(this.UserId))
            {
                newUser.Password = Funs.EncryptionPassword(Const.Password);
                newUser.UserId = SQLHelper.GetNewID(typeof(Model.Sys_User));
                newUser.DataSources = this.CurrUser.LoginProjectId;
                UserService.AddUser(newUser);
                LogService.AddSys_Log(this.CurrUser, newUser.UserCode, newUser.UserId, BLL.Const.PersonSetMenuId, BLL.Const.BtnAdd);
            }
            else
            {
                newUser.UserId = this.UserId;
                UserService.UpdateUser(newUser);
                LogService.AddSys_Log(this.CurrUser, newUser.UserCode, newUser.UserId, BLL.Const.PersonSetMenuId, BLL.Const.BtnModify);
            }
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
        }

        #region 获取按钮权限
        /// <summary>
        /// 获取按钮权限
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        private void GetButtonPower()
        {
            if (!string.IsNullOrEmpty(this.CurrUser.LoginProjectId))
            {
                this.btnSave.Hidden = false;
            }
            else
            {
                var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.PersonSetMenuId);
                if (buttonList.Count() > 0)
                {
                    if (buttonList.Contains(BLL.Const.BtnAdd) || buttonList.Contains(BLL.Const.BtnModify))
                    {
                        this.btnSave.Hidden = false;
                    }
                }
            }
        }
        #endregion

        #region 验证员工编号、工号是否存在
        /// <summary>
        /// 验证员工编号、账号是否存在
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            var q = Funs.DB.Sys_User.FirstOrDefault(x => x.Account == this.txtAccount.Text.Trim() && (x.UserId != this.UserId || (this.UserId == null && x.UserId != null)));
            if (q != null)
            {
                ShowNotify("输入的工号已存在！", MessageBoxIcon.Warning);
            }

            if (!string.IsNullOrEmpty(this.txtUserCode.Text))
            {
                var q2 = Funs.DB.Sys_User.FirstOrDefault(x => x.UserCode == this.txtUserCode.Text.Trim() && (x.UserId != this.UserId || (this.UserId == null && x.UserId != null)));
                if (q2 != null)
                {
                    ShowNotify("输入的编号已存在！", MessageBoxIcon.Warning);
                }
            }

            if (!string.IsNullOrEmpty(this.txtIdentityCard.Text) && BLL.UserService.IsExistUserIdentityCard(this.UserId, this.txtIdentityCard.Text.Trim()) == true)
            {
                ShowNotify("输入的身份证号码已存在！", MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region 上传签名
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
                this.SignatureUrl = UploadFileService.UploadAttachment(Funs.RootPath, this.fileSignature, this.SignatureUrl, UploadFileService.UserFilePath);
                this.Image2.ImageUrl = "~/" + this.SignatureUrl;
            }
        }
        #endregion

        protected void BtnRole_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("../SysManage/RoleItem.aspx?userId={0}", this.UserId, "查看 - ")));
        }
    }
}