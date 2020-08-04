using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.SysManage
{
    public partial class RoleItemEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        public string RoleItemId
        {
            get
            {
                return (string)ViewState["RoleItemId"];
            }
            set
            {
                ViewState["RoleItemId"] = value;
            }
        }

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
        /// 用户编辑页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                ///权限
                this.GetButtonPower();
                this.RoleItemId = Request.Params["roleItemId"];
                this.UserId = Request.Params["userId"];
                ///角色下拉框
                BLL.RoleService.InitRoleDropDownList(this.drpRole, string.Empty, false, true);

                if (!string.IsNullOrEmpty(this.RoleItemId))
                {
                    var roleItem = BLL.RoleItemService.GeRoleItemByRoleItemId(this.RoleItemId);
                    if (roleItem != null)
                    {
                        this.UserId = roleItem.UserId;
                        if (!string.IsNullOrEmpty(roleItem.ProjectId))
                        {
                            var project = BLL.ProjectService.GetProjectByProjectId(roleItem.ProjectId);
                            if (project != null)
                            {
                                this.txtProjectName.Text = project.ProjectName;
                            }
                        }
                        else
                        {
                            this.txtProjectName.Text = roleItem.ProjectName;
                        }
                        if (!string.IsNullOrEmpty(roleItem.RoleId))
                        {
                            this.drpRole.SelectedValueArray = roleItem.RoleId.Split(',');
                        }
                    }
                    if (roleItem.IntoDate != null)
                    {
                        this.txtIntoDate.Text = string.Format("{0:yyyy-MM-dd}", roleItem.IntoDate);
                    }
                    if (roleItem.OutDate != null)
                    {
                        this.txtOutDate.Text = string.Format("{0:yyyy-MM-dd}", roleItem.OutDate);
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
            Model.Sys_RoleItem newRoleItem = new Model.Sys_RoleItem();
            newRoleItem.ProjectName = this.txtProjectName.Text.Trim();
            foreach (var item in this.drpRole.SelectedValueArray)
            {
                var role = BLL.RoleService.GetRoleByRoleId(item);
                if (role != null)
                {
                    if (string.IsNullOrEmpty(newRoleItem.RoleId))
                    {
                        newRoleItem.RoleId = role.RoleId;
                    }
                    else
                    {
                        newRoleItem.RoleId += "," + role.RoleId;
                    }
                }
            }
            newRoleItem.UserId = UserId;
            if (!string.IsNullOrEmpty(this.txtIntoDate.Text.Trim()))
            {
                newRoleItem.IntoDate = Convert.ToDateTime(this.txtIntoDate.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.txtOutDate.Text.Trim()))
            {
                newRoleItem.OutDate = Convert.ToDateTime(this.txtOutDate.Text.Trim());
            }
            if (string.IsNullOrEmpty(this.RoleItemId))
            {
                RoleItemService.AddRoleItem(newRoleItem);
            }
            else
            {
                newRoleItem.RoleItemId = this.RoleItemId;
                RoleItemService.UpdateRoleItem(newRoleItem);
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
                    if (buttonList.Contains(BLL.Const.BtnSave))
                    {
                        this.btnSave.Hidden = false;
                    }
                }
            }
        }
        #endregion
    }
}