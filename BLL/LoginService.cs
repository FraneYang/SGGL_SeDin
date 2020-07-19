namespace BLL
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Security;
    using System.Web;
    using EmitMapper;
    using Model;

    public static class LoginService
    {
        public static SGGLDB db = Funs.DB;       

        /// <summary>
        /// 用户登录成功方法
        /// </summary>
        /// <param name="loginname">登录成功名</param>
        /// <param name="password">未加密密码</param>
        /// <param name="rememberMe">记住我开关</param>
        /// <param name="page">调用页面</param>
        /// <returns>是否登录成功</returns>
        public static bool UserLogOn(string account, string password, bool rememberMe, System.Web.UI.Page page)
        {
            List<Sys_User> x = (from y in Funs.DB.Sys_User
                    where y.Account == account && y.IsPost == true && y.Password == Funs.EncryptionPassword(password)
                    select y).ToList();
            if (x.Any())
            {
                string accValue = HttpUtility.UrlEncode(account);
                FormsAuthentication.SetAuthCookie(accValue, false);
                page.Session[SessionName.CurrUser] = x.First();
                //if (rememberMe)
                //{
                //    HttpCookie u = new HttpCookie("UserInfo");
                //    u["username"] = accValue;
                //    u["password"] = password;
                //    // Cookies过期时间设置为一月.
                //    u.Expires = DateTime.Now.AddMonths(1);
                //    page.Response.Cookies.Add(u);
                //}
                //else
                //{
                //    // 当选择不保存用户名时,Cookies过期时间设置为昨天.
                //    page.Response.Cookies["UserInfo"].Expires = DateTime.Now.AddDays(-1);
                //}

                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 用户登录成功方法
        /// </summary>
        /// <param name="loginname">登录成功名</param>
        /// <param name="password">未加密密码</param>
        /// <param name="rememberMe">记住我开关</param>
        /// <param name="page">调用页面</param>
        /// <returns>是否登录成功</returns>
        public static bool UserLogOn(string account, bool rememberMe, System.Web.UI.Page page)
        {
            List<Sys_User> x = (from y in Funs.DB.Sys_User
                                      where y.Account == account  && y.IsPost == true
                                      select y).ToList();
            if (x.Any())
            {
                string accValue = HttpUtility.UrlEncode(account);
                FormsAuthentication.SetAuthCookie(accValue, false);
                page.Session[SessionName.CurrUser] = x.First();
                if (rememberMe)
                {
                    System.Web.HttpCookie u = new System.Web.HttpCookie("UserInfo");
                    u["username"] = accValue;
                    //u["password"] = password;
                    // Cookies过期时间设置为一年.
                    u.Expires = DateTime.Now.AddYears(1);
                    page.Response.Cookies.Add(u);
                }
                else
                {
                    // 当选择不保存用户名时,Cookies过期时间设置为昨天.
                    page.Response.Cookies["UserInfo"].Expires = DateTime.Now.AddDays(-1);
                }

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
