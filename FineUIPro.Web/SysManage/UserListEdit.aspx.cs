using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.SysManage
{ 
    public partial class UserListEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 用户主键
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
        /// 用户编辑页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
            
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                string type = Request.Params["type"];
                ///权限
                this.GetButtonPower();
                this.UserId = Request.Params["userId"];
                this.UnitId = Request.Params["UnitId"];

                ConstValue.InitConstValueDropDownList(this.drpIsPost, ConstValue.Group_0001, false);
                ConstValue.InitConstValueDropDownList(this.drpIsOffice, ConstValue.Group_0001, false);
                UnitService.InitUnitDropDownList(this.drpUnit, this.CurrUser.LoginProjectId, true);
                DepartService.InitDepartDropDownList(this.drpDepart, true);
                if (!string.IsNullOrEmpty(this.UnitId))
                {
                    this.drpIsOffice.SelectedValue= "False";
                }

                if (!BLL.CommonService.IsMainUnitOrAdmin(this.CurrUser.UserId)) ///不是企业单位或者管理员
                {
                    this.drpUnit.Enabled = false;
                }
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
                            this.drpRole.SelectedValueArray = user.RoleId.Split(',');
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
                        if (!string.IsNullOrEmpty(user.SignatureUrl))
                        {
                            this.SignatureUrl = user.SignatureUrl;
                            this.Image2.ImageUrl = "~/" + this.SignatureUrl;
                        }
                        this.drpDepart.SelectedValue = user.DepartId;
                    }
                }

                if (type == "-1")
                {
                    this.trServer.Hidden = true;
                }
                else if (type == "0")
                {
                    this.drpUnit.SelectedValue = Const.UnitId_SEDIN;
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
            if (this.drpDepart.SelectedValue == Const._Null && !this.trServer.Hidden)
            {
                Alert.ShowInParent("请选择部门！", MessageBoxIcon.Warning);
                return;
            }
            var q = Funs.DB.Sys_User.FirstOrDefault(x => x.Account == this.txtAccount.Text.Trim() && (x.UserId != this.UserId || (this.UserId == null && x.UserId != null)));
            if (q != null)
            {
                Alert.ShowInParent("输入的账号已存在！", MessageBoxIcon.Warning);
                return;
            }
            if (!string.IsNullOrEmpty(this.txtUserCode.Text))
            {
                var q2 = Funs.DB.Sys_User.FirstOrDefault(x => x.UserCode == this.txtUserCode.Text.Trim() && (x.UserId != this.UserId || (this.UserId == null && x.UserId != null)));
                if (q2 != null)
                {
                    Alert.ShowInParent("输入的编号已存在！", MessageBoxIcon.Warning);
                    return;
                }
            }

            if (!string.IsNullOrEmpty(this.txtIdentityCard.Text) && BLL.UserService.IsExistUserIdentityCard(this.UserId, this.txtIdentityCard.Text.Trim()) == true)
            {
                Alert.ShowInParent("输入的身份证号码已存在！", MessageBoxIcon.Warning);
                return;
            }
            
            Model.Sys_User newUser = new Model.Sys_User
            {
                UserCode = this.txtUserCode.Text.Trim(),
                UserName = this.txtUserName.Text.Trim(),
                Account = this.txtAccount.Text.Trim(),
                IdentityCard = this.txtIdentityCard.Text.Trim(),
                Telephone = this.txtTelephone.Text.Trim(),
        };
            if (this.drpUnit.SelectedValue != Const._Null)
            {
                newUser.UnitId = this.drpUnit.SelectedValue;
            }
            if (!BLL.CommonService.IsMainUnitOrAdmin(this.CurrUser.UserId)) ///不是企业单位或者管理员
            {
                newUser.UnitId = this.CurrUser.UnitId;
            }
            string roleIds = string.Empty;
            foreach (var item in this.drpRole.SelectedValueArray)
            {
                var role = BLL.RoleService.GetRoleByRoleId(item);
                if (role != null)
                {
                    if (string.IsNullOrEmpty(newUser.RoleId))
                    {
                        newUser.RoleId = role.RoleId;
                    }
                    else
                    {
                        newUser.RoleId += "," + role.RoleId;
                    }
                }
            }
            if (this.drpDepart.SelectedValue != Const._Null)
            {
                newUser.DepartId = this.drpDepart.SelectedValue;
            }
          
            newUser.SignatureUrl = this.SignatureUrl;
            newUser.IsPost = Convert.ToBoolean(this.drpIsPost.SelectedValue);
            newUser.IsOffice = Convert.ToBoolean(this.drpIsOffice.SelectedValue);
           
            if (string.IsNullOrEmpty(this.UserId))
            {
                newUser.Password = Funs.EncryptionPassword(Const.Password);
                newUser.UserId = SQLHelper.GetNewID(typeof(Model.Sys_User));
                newUser.DataSources = this.CurrUser.LoginProjectId;
                UserService.AddUser(newUser);          
                LogService.AddSys_Log(this.CurrUser, newUser.UserCode, newUser.UserId, BLL.Const.UserMenuId, BLL.Const.BtnAdd);
            }
            else
            {
                newUser.UserId = this.UserId;
                UserService.UpdateUser(newUser);
                LogService.AddSys_Log(this.CurrUser, newUser.UserCode, newUser.UserId, BLL.Const.UserMenuId, BLL.Const.BtnModify);
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
                var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.UserMenuId);
                if (buttonList.Count() > 0)
                {
                    if (buttonList.Contains(BLL.Const.BtnSave))
                    {
                        this.btnSave.Hidden = false;
                        this.btnArrowRefresh.Hidden = false;
                    }
                }
            }
        }
        #endregion

        #region 验证用户编号、账号是否存在
        /// <summary>
        /// 验证用户编号、账号是否存在
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
           
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
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("RoleItem.aspx?userId={0}", this.UserId, "查看 - ")));
        }

        #region  重置
        /// <summary>
        /// 批量删除数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnArrowRefresh_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.UserId))
            {
                BLL.UserService.UpdatePassword(this.UserId, BLL.Const.Password);
                ShowNotify("密码已重置为原始密码!", MessageBoxIcon.Success);
            }
            else
            {
                ShowNotify("请至少选中一行！", MessageBoxIcon.Warning);
            }
        }
        #endregion
    }
}