using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FineUIPro.Web.Person
{
    public partial class ShuntView : PageBase
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string ShuntId
        {
            get
            {
                return (string)ViewState["ShuntId"];
            }
            set
            {
                ViewState["ShuntId"] = value;
            }
        }
        /// <summary>
        /// 办理类型
        /// </summary>
        public string State
        {
            get
            {
                return (string)ViewState["State"];
            }
            set
            {
                ViewState["State"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShuntId = Request.Params["ShuntId"];
                if (!string.IsNullOrEmpty(ShuntId))
                {
                    Model.Person_Shunt shunt = Person_ShuntService.GetShunt(ShuntId);
                    this.txtCode.Text = shunt.Code;
                    if (!string.IsNullOrEmpty(shunt.ProjectId))
                    {
                        this.drpProject.Text = BLL.ProjectService.GetProjectNameByProjectId(shunt.ProjectId);
                    }
                    if (shunt.CompileDate != null)
                    {
                        this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", shunt.CompileDate);
                    }
                    if (!string.IsNullOrEmpty(shunt.State))
                    {
                        State = shunt.State;
                    }
                    else
                    {
                        State = BLL.Const.Shunt_Compile;
                    }
                    var details = BLL.Person_ShuntDetailService.GetLists(ShuntId);
                    this.Grid1.DataSource = details;
                    this.Grid1.DataBind();
                }
            }
        }

        #region 转换字符串
        //<summary>
        //获取姓名
        //</summary>
        //<param name="state"></param>
        //<returns></returns>
        protected string ConvertUserName(object UserId)
        {
            string UserName = string.Empty;
            if (UserId != null)
            {
                var user = BLL.UserService.GetUserByUserId(UserId.ToString());
                if (user != null)
                {
                    UserName = user.UserName;
                }
            }
            return UserName;
        }

        //<summary>
        //获取职业资格证书名称
        //</summary>
        //<param name="state"></param>
        //<returns></returns>
        protected string ConvertCertificateName(object UserId)
        {
            string CertificateName = string.Empty;
            if (UserId != null)
            {
                var user = BLL.UserService.GetUserByUserId(UserId.ToString());
                if (user != null && !string.IsNullOrEmpty(user.CertificateId))
                {
                    string[] Ids = user.CertificateId.Split(',');
                    foreach (string t in Ids)
                    {
                        var type = BLL.PracticeCertificateService.GetPracticeCertificateById(t);
                        if (type != null)
                        {
                            CertificateName += type.PracticeCertificateName + ",";
                        }
                    }
                }
            }
            if (CertificateName != string.Empty)
            {
                return CertificateName.Substring(0, CertificateName.Length - 1);
            }
            else
            {
                return "";
            }
        }

        //<summary>
        //获取职称名称
        //</summary>
        //<param name="state"></param>
        //<returns></returns>
        protected string ConvertPostTitleName(object UserId)
        {
            string PostTitleName = string.Empty;
            if (UserId != null)
            {
                var user = BLL.UserService.GetUserByUserId(UserId.ToString());
                if (user != null && !string.IsNullOrEmpty(user.PostTitleId))
                {
                    var postTitle = BLL.PostTitleService.GetPostTitleById(user.PostTitleId);
                    if (postTitle != null)
                    {
                        PostTitleName = postTitle.PostTitleName;
                    }
                }
            }
            return PostTitleName;
        }

        //<summary>
        //获取当前项目名称
        //</summary>
        //<param name="state"></param>
        //<returns></returns>
        protected string ConvertCurrProject(object UserId)
        {
            string ProjectName = string.Empty;
            if (UserId != null)
            {
                var roleItem = BLL.RoleItemService.GeCurrRoleItemByUserId(UserId.ToString());
                if (roleItem != null && roleItem.OutDate != null)
                {
                    if (!string.IsNullOrEmpty(roleItem.ProjectId))
                    {
                        ProjectName = BLL.ProjectService.GetProjectNameByProjectId(roleItem.ProjectId);
                    }
                    else
                    {
                        ProjectName = roleItem.ProjectName;
                    }
                }
            }
            return ProjectName;
        }

        //<summary>
        //获取当前岗位名称
        //</summary>
        //<param name="state"></param>
        //<returns></returns>
        protected string ConvertCurrWorkPost(object UserId)
        {
            string WorkPostName = string.Empty;
            if (UserId != null)
            {
                var roleItem = BLL.RoleItemService.GeCurrRoleItemByUserId(UserId.ToString());
                if (roleItem != null && roleItem.OutDate != null)
                {
                    var projectUser = BLL.ProjectUserService.GetProjectUserByUserIdProjectId(roleItem.ProjectId, roleItem.UserId);
                    if (projectUser != null)
                    {
                        var user = BLL.UserService.GetUserByUserId(projectUser.UserId);
                        var sitePerson = BLL.PersonService.GetPersonByIdentityCard(projectUser.ProjectId, user.IdentityCard);
                        if (sitePerson != null && !string.IsNullOrEmpty(sitePerson.WorkPostId))
                        {
                            var workPost = BLL.WorkPostService.GetWorkPostById(sitePerson.WorkPostId);
                            if (workPost != null)
                            {
                                WorkPostName = workPost.WorkPostName;
                            }
                        }
                    }
                }
            }
            return WorkPostName;
        }

        //<summary>
        //获取历史岗位名称
        //</summary>
        //<param name="state"></param>
        //<returns></returns>
        protected string ConvertOldWorkPost(object UserId)
        {
            string WorkPostName = string.Empty;
            if (UserId != null)
            {
                var roleItems = BLL.RoleItemService.GeOldRoleItemsByUserId(UserId.ToString());
                if (roleItems.Count > 3)
                {
                    roleItems = roleItems.Take(3).ToList();
                }
                foreach (var roleItem in roleItems)
                {
                    if (roleItem != null && roleItem.OutDate != null)
                    {
                        var projectUser = BLL.ProjectUserService.GetProjectUserByUserIdProjectId(roleItem.ProjectId, roleItem.UserId);
                        if (projectUser != null)
                        {
                            var user = BLL.UserService.GetUserByUserId(projectUser.UserId);
                            var sitePerson = BLL.PersonService.GetPersonByIdentityCard(projectUser.ProjectId, user.IdentityCard);
                            if (sitePerson != null && !string.IsNullOrEmpty(sitePerson.WorkPostId))
                            {
                                var workPost = BLL.WorkPostService.GetWorkPostById(sitePerson.WorkPostId);
                                if (workPost != null)
                                {
                                    WorkPostName += workPost.WorkPostName + ",";
                                }
                            }
                        }
                    }
                }
            }
            if (!string.IsNullOrEmpty(WorkPostName))
            {
                WorkPostName = WorkPostName.Substring(0, WorkPostName.Length - 1);
            }
            return WorkPostName;
        }
        //<summary>
        //获取岗位名称
        //</summary>
        //<param name="state"></param>
        //<returns></returns>
        protected string ConvertUserWorkPost(object UserId)
        {
            string WorkPostName = string.Empty;
            if (UserId != null)
            {
                var user = BLL.UserService.GetUserByUserId(UserId.ToString());
                if (user != null && !string.IsNullOrEmpty(user.WorkPostId))
                {
                    string[] Ids = user.WorkPostId.Split(',');
                    foreach (string t in Ids)
                    {
                        var type = BLL.WorkPostService.GetWorkPostById(t);
                        if (type != null)
                        {
                            WorkPostName += type.WorkPostName + ",";
                        }
                    }
                }
            }
            return WorkPostName;
        }
        //<summary>
        //获取拟聘岗位名称
        //</summary>
        //<param name="state"></param>
        //<returns></returns>
        protected string ConvertWorkPost(object WorkPostId)
        {
            string WorkPostName = string.Empty;
            if (WorkPostId != null)
            {
                var workPost = BLL.WorkPostService.GetWorkPostById(WorkPostId.ToString());
                if (workPost != null)
                {
                    WorkPostName = workPost.WorkPostName;

                }
            }
            return WorkPostName;
        }
        /// <summary>
        /// 把状态转换代号为文字形式
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        protected string ConvertState(object state)
        {
            if (state != null)
            {
                if (state.ToString() == BLL.Const.Shunt_ReCompile)
                {
                    return "重新编制";
                }
                else if (state.ToString() == BLL.Const.Shunt_Compile)
                {
                    return "编制";
                }
                else if (state.ToString() == BLL.Const.Shunt_Audit)
                {
                    return "审核";
                }
                else if (state.ToString() == BLL.Const.Shunt_Complete)
                {
                    return "审批完成";
                }
                else
                {
                    return "";
                }
            }
            return "";
        }
        #endregion
    }
}