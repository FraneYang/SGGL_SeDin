using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using Model;

namespace FineUIPro.Web.ProjectData
{
    public partial class ProjectUserView : PageBase
    {
        /// <summary>
        /// 定义项
        /// </summary>
        public string ProjectUserId
        {
            get
            {
                return (string)ViewState["ProjectUserId"];
            }
            set
            {
                ViewState["ProjectUserId"] = value;
            }
        }

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
                this.ProjectUserId = Request.QueryString["ProjectUserId"];
                if (!String.IsNullOrEmpty(this.ProjectUserId))
                {
                    var projectUser = BLL.ProjectUserService.GetProjectUserById(this.ProjectUserId);
                    if (projectUser != null)
                    {
                        var project = BLL.ProjectService.GetProjectByProjectId(projectUser.ProjectId);
                        if (project != null)
                        {
                            this.lbProjectName.Text = project.ProjectName;
                        }
                        var unit = BLL.UnitService.GetUnitByUnitId(projectUser.UnitId);
                        if (unit != null)
                        {
                            this.lbUnitName.Text = unit.UnitName;
                        }
                        var User = BLL.UserService.GetUserByUserId(projectUser.UserId);
                        if (User != null)
                        {
                            this.lbUserCode.Text = User.UserCode;
                            this.lbUserName.Text = User.UserName;
                        }

                        var ispost = BLL.ConstValue.drpConstItemList(ConstValue.Group_0001).FirstOrDefault(x => x.ConstValue == Convert.ToString(projectUser.IsPost));
                        if (ispost != null)
                        {
                            this.drpIsPost.Text = ispost.ConstText;
                        }

                        var role = BLL.RoleService.GetRoleByRoleId(projectUser.RoleId);
                        if (role != null)
                        {
                            this.drpRole.Text = role.RoleName;
                        }
                    }
                }
            }
        }                
    }
}