using BLL;
using System;

namespace FineUIPro.Web.SysManage
{
    public partial class UpdatePasswordEdit : PageBase
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
        #endregion

        /// <summary>
        /// 修改密码页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {                
                LoadData();
                this.UserId = Request.Params["userId"];
                if (this.CurrUser.UserId == BLL.Const.sysglyId)
                {
                    this.txtOldPassword.Hidden = true;
                    this.txtNewPassword.Focus();
                }

                if (!string.IsNullOrEmpty(this.UserId))
                {
                    var user = BLL.UserService.GetUserByUserId(this.UserId);
                    if (user != null)
                    {
                        this.txtUserName.Text = user.UserName;
                        this.txtAccount.Text = user.Account;                        
                    }
                }
            }
        }

        /// <summary>
        /// 加载页面
        /// </summary>
        private void LoadData()
        {
            btnClose.OnClientClick = ActiveWindow.GetHideReference();
        }

        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            var user = BLL.UserService.GetUserByUserId(this.UserId);
            if (user != null)
            {
                if (this.CurrUser.UserId != BLL.Const.sysglyId)
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
                }
                if (this.txtNewPassword.Text != this.txtConfirmPassword.Text)
                {
                    Alert.ShowInParent("确认密码输入不一致！");
                    return;
                }

                BLL.UserService.UpdatePassword(user.UserId, this.txtNewPassword.Text);

                BLL.LogService.AddSys_Log(this.CurrUser, "修改密码", string.Empty, BLL.Const.UserMenuId, Const.BtnModify);
                PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
            }
        }

        /// <summary>
        ///  确认密码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void txtConfirmPassword_Blur(object sender, EventArgs e)
        {
            if (this.txtNewPassword.Text != this.txtConfirmPassword.Text)
            {
                Alert.ShowInParent("确认密码输入不一致！");
                return;
            }
        }
    }
}