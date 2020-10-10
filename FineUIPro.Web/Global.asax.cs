namespace FineUIPro.Web
{
    using System;
    using System.Configuration;
    using System.Globalization;
    using System.Text;
    using BLL;
    using Model;

    public class Global : System.Web.HttpApplication
    {
        /// <summary>
        /// 自动启用插件标志文件路径
        /// </summary>
        //private static string applicationActiveFlagFilePhysicalPath = String.Empty;

        protected void Application_Start(object sender, EventArgs e)
        {
            Application["OnlineUserCount"] = 0;
            try
            {
                Funs.RootPath = Server.MapPath("~/");
               
                // 日志文件所在目录
                ErrLogInfo.DefaultErrLogFullPath = Server.MapPath("~/ErrLog.txt");
                Funs.ConnString = ConfigurationManager.AppSettings["ConnectionString"];
                Funs.SystemName = ConfigurationManager.AppSettings["SystemName"];
                Funs.SGGLUrl = ConfigurationManager.AppSettings["SGGLUrl"];            
            }
            catch (Exception ex)
            {
                ErrLogInfo.WriteLog(string.Empty, ex);
                //AppDomain.Unload(AppDomain.CurrentDomain);
            }

            //////得到集团服务器路径
            try
            {
                string address = ConfigurationManager.AppSettings["endpoint"];
                Funs.SystemVersion = ConfigurationManager.AppSettings["SystemVersion"]; 
            }
            catch (Exception ex)
            {
                ErrLogInfo.WriteLog("得到集团服务器地址失败!", ex);
            }

            ////日志定时上报自动启动
            try
            {                
                BLL.MonitorService.StartMonitor();
                BLL.MonitorService.StartMonitorEve();
                BLL.MonitorService.StartPersonQuarterCheck();
            }
            catch (Exception ex)
            {
                ErrLogInfo.WriteLog("日志定时上报失败!", ex);
            }
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            Session.Timeout = 60;
            // 这种统计在线人数的做法会有一定的误差
            Application.Lock();
            Application["OnlineUserCount"] = (int)Application["OnlineUserCount"] + 1;
            Application.UnLock();
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {            
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {
            StringBuilder errLog = null;
            Exception ex = null;
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                Sys_ErrLogInfo newErr = new Sys_ErrLogInfo
                {
                    ErrLogId = SQLHelper.GetNewID()
                };
                try
                {
                    // 获取错误类
                    ex = Server.GetLastError().InnerException;
                    if (ex == null)
                    {
                        ex = Server.GetLastError().GetBaseException();
                    }
                    errLog = new StringBuilder();
                    errLog.Append(String.Format(CultureInfo.InvariantCulture, "出错文件:{0}\r\n", Request.Url.AbsoluteUri));
                    newErr.ErrUrl = Request.Url.AbsoluteUri;

                    if (Request.UserHostAddress != null)
                    {
                        errLog.Append(String.Format(CultureInfo.InvariantCulture, "IP地址:{0}\r\n", Request.UserHostAddress));
                        newErr.ErrIP = Request.UserHostAddress;
                    }

                    if (Session != null && Session["CurrUser"] != null)
                    {
                        errLog.Append(String.Format(CultureInfo.InvariantCulture, "操作人员:{0}\r\n", ((Model.Sys_User)Session["CurrUser"]).UserName));
                        newErr.UserName = ((Model.Sys_User)Session["CurrUser"]).UserId;
                    }
                    else
                    {
                        PageBase.PageRefresh(Request.ApplicationPath + "/LogOff.aspx");
                    }
                }
                catch
                {
                    try
                    {
                        PageBase.PageRefresh(Request.ApplicationPath + "/LogOff.aspx");
                    }
                    catch
                    {
                    }
                }
                finally
                {
                    if (errLog != null)
                    {
                       db.Sys_ErrLogInfo.InsertOnSubmit(newErr);
                        db.SubmitChanges();
                    }

                    ErrLogInfo.WriteLog(newErr.ErrLogId, ex, errLog == null ? null : errLog.ToString());
                    Server.ClearError();

                    PageBase.PageRefresh(Request.ApplicationPath + "/LogOff.aspx");
                }
            }
        }

        /// <summary>
        /// 缓存结束
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Session_End(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 活动结束
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_End(object sender, EventArgs e)
        {
        }
    }
}
