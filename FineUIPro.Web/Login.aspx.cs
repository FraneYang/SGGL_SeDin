namespace FineUIPro.Web
{
    using BLL;
    using System;
    using System.Configuration;
    using System.Linq;
    using System.Web;
    using System.Web.Services;

    public partial class Login : PageBase
    {

        #region
        /// <summary>
        /// 是否本部
        /// </summary>
        public string IsOffice
        {
            get
            {
                return (string)ViewState["IsOffice"];
            }
            set
            {
                ViewState["IsOffice"] = value;
            }
        }
        /// <summary>
        /// 菜单类型
        /// </summary>
        public string MenuType
        {
            get
            {
                return (string)ViewState["MenuType"];
            }
            set
            {
                ViewState["MenuType"] = value;
            }
        }
        /// <summary>
        /// 项目ID
        /// </summary>
        public string ProjectId
        {
            get
            {
                return (string)ViewState["ProjectId"];
            }
            set
            {
                ViewState["ProjectId"] = value;
            }
        }
        #endregion

        #region 页面加载
        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {             
            }
        }
        #endregion

        [WebMethod]
        public static string LoginPost(string user, string pwd)
        {
            return new Login().btnLogin_Click(user, pwd);
        }

        private string btnLogin_Click(string user, string pwd)
        {
            string url = "";
            if (LoginService.UserLogOn(user, pwd, true, this.Page))
            {
                //PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("../SysManage/UpdatePasswordEdit.aspx?userId={0}", this.CurrUser.UserId, "编辑 - ")));
                if (!this.CurrUser.LastIsOffice.HasValue)
                {
                    this.CurrUser.LastIsOffice = this.CurrUser.IsOffice;
                }
                if (this.CurrUser.Password == Const.MD5pwd)
                {
                    if (this.CurrUser.LastIsOffice == true)
                    {
                        this.CurrUser.LoginProjectId = null;                      
                        url = "index.aspx#/SysManage/UpdatePassword.aspx";
                    }
                    else
                    {
                        this.CurrUser.LoginProjectId = this.CurrUser.LastProjectId;
                        url = "indexProject.aspx?projectId=" + this.CurrUser.LastProjectId + "#/SysManage/UpdatePassword.aspx";
                    }
                }
                else
                {
                    if (this.CurrUser.LastIsOffice == true)
                    {
                        this.CurrUser.LoginProjectId = null;
                        url = "index.aspx";
                    }
                    else
                    {
                        this.CurrUser.LoginProjectId = this.CurrUser.LastProjectId;
                        url = "indexProject.aspx?projectId=" + this.CurrUser.LastProjectId;
                    }
                }

                LogService.AddSys_Log(this.CurrUser, this.CurrUser.UserName, this.CurrUser.UserId, Const.UserMenuId, Const.BtnLogin);
            }

            return url;
        }
    }
}
