using BLL;
using System;
using System.Linq;

namespace FineUIPro.Web.SysManage
{
    public partial class RoleListEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 角色主键
        /// </summary>
        public string RoleId
        {
            get
            {
                return (string)ViewState["RoleId"];
            }
            set
            {
                ViewState["RoleId"] = value;
            }
        }
        #endregion

        /// <summary>
        /// 角色编辑页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                this.RoleId = Request.Params["roleId"];
                ///权限
                this.GetButtonPower();
                if (!string.IsNullOrEmpty(this.RoleId))
                {
                    var role = BLL.RoleService.GetRoleByRoleId(this.RoleId);
                    if (role != null)
                    {
                        this.txtRoleCode.Text = role.RoleCode;
                        this.txtRoleName.Text = role.RoleName;                        
                        this.txtDef.Text = role.Def;
                        if (role.IsOffice == true)
                        {
                            this.rbIsOfficce.SelectedValue = "1";
                        }
                    }
                }
                else
                {
                    this.txtRoleCode.Text = SQLHelper.RunProcNewId("SpGetNewCode3", "Sys_Role", "RoleCode", "");
                }
            }
        }

        #region 获取按钮权限
        /// <summary>
        /// 获取按钮权限
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        private void GetButtonPower()
        {
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.RoleMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnSave))
                {
                    this.btnSave.Hidden = false;
                }               
            }
        }
        #endregion

        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (BLL.RoleService.IsExistRoleName(this.RoleId, this.txtRoleName.Text.Trim()))
            {
                Alert.ShowInParent("角色名称已存在！", MessageBoxIcon.Warning);
                return;
            }
            else
            {
                Model.Sys_Role newRole = new Model.Sys_Role
                {
                    RoleCode = this.txtRoleCode.Text.Trim(),
                    RoleName = this.txtRoleName.Text.Trim(),
                    Def = this.txtDef.Text.Trim(),
                    IsOffice = (this.rbIsOfficce.SelectedValue == "1" ? true : false),
            };
                if (string.IsNullOrEmpty(this.RoleId))
                {
                    newRole.RoleId = SQLHelper.GetNewID(typeof(Model.Sys_Role));
                    newRole.IsSystemBuilt = false;
                    BLL.RoleService.AddRole(newRole);
                    BLL.LogService.AddSys_Log(this.CurrUser, newRole.RoleCode, newRole.RoleId, BLL.Const.RoleMenuId, Const.BtnAdd);
                }
                else
                {
                    newRole.RoleId = this.RoleId;
                    BLL.RoleService.UpdateRole(newRole);
                    BLL.LogService.AddSys_Log(this.CurrUser, newRole.RoleCode, newRole.RoleId, BLL.Const.RoleMenuId, Const.BtnModify);
                }
                PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
            }
        }

        #region 验证角色编号、名称是否存在
        /// <summary>
        /// 验证角色编号、名称是否存在
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            var q2 = Funs.DB.Sys_Role.FirstOrDefault(x => x.RoleName == this.txtRoleName.Text.Trim() && (x.RoleId != this.RoleId || (this.RoleId == null && x.RoleId != null)));
            if (q2 != null)
            {
                ShowNotify("输入的角色名称已存在！", MessageBoxIcon.Warning);
            }
        }
        #endregion       
    }
}