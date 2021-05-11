using BLL;
using System;
using System.Linq;

namespace FineUIPro.Web.ProjectData
{
    public partial class ProjectUserSave : PageBase
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
                ///角色下拉框
                BLL.RoleService.InitRoleDropDownList(this.drpRole, string.Empty, false, true);
                BLL.ConstValue.InitConstValueDropDownList(this.drpIsPost, ConstValue.Group_0001, false);
                BLL.WorkPostService.InitWorkPostDropDownList(this.drpWorkPost, true);
                gvUnitWork.DataSource = (from x in Funs.DB.WBS_UnitWork where x.SuperUnitWork == null && x.ProjectId == this.CurrUser.LoginProjectId select x);
                gvUnitWork.DataBind();
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
                        if (!string.IsNullOrEmpty(projectUser.WorkAreaId))
                        {
                            txtUnitWork.Values = projectUser.WorkAreaId.Split(',');
                        }
                        if (projectUser.IsPost.HasValue)
                        {
                            this.drpIsPost.SelectedValue = Convert.ToString(projectUser.IsPost);
                        }
                        if (!string.IsNullOrEmpty(projectUser.RoleId))
                        {
                            this.drpRole.SelectedValue = projectUser.RoleId;
                        }
                        if (!string.IsNullOrEmpty(projectUser.RoleId))
                        {
                            this.drpRole.SelectedValueArray = projectUser.RoleId.Split(',');
                        }

                        var user = BLL.UserService.GetUserByUserId(projectUser.UserId);
                        if (user != null && !string.IsNullOrEmpty(user.IdentityCard))
                        {
                            var sitePerson = BLL.PersonService.GetPersonByIdentityCard(projectUser.ProjectId, user.IdentityCard);
                            if (sitePerson != null && !string.IsNullOrEmpty(sitePerson.WorkPostId))
                            {
                                this.drpWorkPost.SelectedValue = sitePerson.WorkPostId;
                            }     
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            var newProjectUser = BLL.ProjectUserService.GetProjectUserById(this.ProjectUserId);
            if (newProjectUser != null)
            {
                //if (this.drpRole.SelectedValue != BLL.Const._Null)
                //{
                //    newProjectUser.RoleId = this.drpRole.SelectedValue;
                //    //newProjectUser.RoleName = this.drpRole.SelectedText;
                //}
                ///角色
                string roleIds = string.Empty;
                foreach (var item in this.drpRole.SelectedValueArray)
                {
                    var role = BLL.RoleService.GetRoleByRoleId(item);
                    if (role != null)
                    {
                        if (string.IsNullOrEmpty(roleIds))
                        {
                            roleIds = role.RoleId;
                        }
                        else
                        {
                            roleIds += "," + role.RoleId;
                        }
                    }
                }
                newProjectUser.RoleId = roleIds;
                if (!string.IsNullOrWhiteSpace(String.Join(",", this.txtUnitWork.Values)))
                {
                    newProjectUser.WorkAreaId = string.Join(",", txtUnitWork.Values);
                }
                newProjectUser.IsPost = Convert.ToBoolean(this.drpIsPost.SelectedValue);
                BLL.ProjectUserService.UpdateProjectUser(newProjectUser);
                Model.Sys_RoleItem roleItem = BLL.RoleItemService.GeRoleItemByUserIdAndProjectId(newProjectUser.UserId, newProjectUser.ProjectId);
                if (roleItem != null)
                {
                    roleItem.RoleId = newProjectUser.RoleId;
                    if (newProjectUser.IsPost == false) //离岗
                    {
                        roleItem.OutDate = DateTime.Now;
                    }
                    roleItem.RoleId = newProjectUser.RoleId;
                    BLL.RoleItemService.UpdateRoleItem(roleItem);
                }
                this.SetWorkPost(newProjectUser);
                //更新用户的项目id和项目角色
                Model.Sys_User user = BLL.UserService.GetUserByUserId(newProjectUser.UserId);
                if (user != null)
                {
                    user.ProjectId = newProjectUser.ProjectId;
                    user.ProjectRoleId = newProjectUser.RoleId;
                    if (this.drpWorkPost.SelectedValue != BLL.Const._Null)
                    {
                        user.ProjectWorkPostId = this.drpWorkPost.SelectedValue;
                    }
                    BLL.UserService.UpdateUser(user);
                }
                BLL.LogService.AddSys_Log(this.CurrUser, this.lbUserCode.Text, newProjectUser.UserId, BLL.Const.ProjectUserMenuId, BLL.Const.BtnModify);
                ShowNotify("保存数据成功!", MessageBoxIcon.Success);
                // 2. 关闭本窗体，然后回发父窗体
                PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
            }
        }

        /// <summary>
        /// 更新用户的项目岗位
        /// </summary>
        /// <param name="projectUser"></param>
        private void SetWorkPost(Model.Project_ProjectUser projectUser)
        {
            var user = BLL.UserService.GetUserByUserId(projectUser.UserId);
            if (user != null && !string.IsNullOrEmpty(user.IdentityCard))
            {
                var sitePerson = BLL.PersonService.GetPersonByIdentityCard(projectUser.ProjectId, user.IdentityCard);
                if (sitePerson != null)
                {
                    if (this.drpWorkPost.SelectedValue != BLL.Const._Null)
                    {
                        sitePerson.WorkPostId = this.drpWorkPost.SelectedValue;
                    }
                    else
                    {
                        sitePerson.WorkPostId = null;
                    }    
                    BLL.PersonService.UpdatePerson(sitePerson);
                }
                else
                {
                    Model.SitePerson_Person newPerson = new Model.SitePerson_Person
                    {
                        PersonId = SQLHelper.GetNewID(typeof(Model.SitePerson_Person)),
                        PersonName = user.UserName,
                        Sex = user.Sex,
                        IdentityCard = user.IdentityCard,
                        ProjectId = projectUser.ProjectId,
                        UnitId = user.UnitId,
                        IsUsed = true
                    };
                    if (this.drpWorkPost.SelectedValue != BLL.Const._Null)
                    {
                        newPerson.WorkPostId = this.drpWorkPost.SelectedValue;
                    }
                    else
                    {
                        newPerson.WorkPostId = null;
                    }
                    BLL.PersonService.AddPerson(newPerson);
                }
            }
        }
    }
}