namespace BLL
{
    using System;
    using System.Collections;
    using System.Linq;
    using System.Net;

    public static class LogService
    {
        public static Model.SGGLDB db = Funs.DB;

        /// <summary>
        /// 添加操作日志
        /// </summary>
        /// <param name="CurrUser">操作人</param>
        /// <param name="code">编号</param>
        /// <param name="dataId">主键ID</param>
        /// <param name="strMenuId">菜单ID</param>
        /// <param name="strOperationName">操作名称</param>
        public static void AddSys_Log(Model.Sys_User CurrUser, string code, string dataId, string strMenuId, string strOperationName)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                if (CurrUser != null)
                {
                    Model.Sys_Log syslog = new Model.Sys_Log
                    {
                        LogId = SQLHelper.GetNewID(),
                        HostName = Dns.GetHostName(),
                        OperationTime = DateTime.Now,
                        UserId = CurrUser.UserId,
                        MenuId = strMenuId,
                        OperationName = strOperationName,
                        DataId = dataId,
                        LogSource = 1,
                    };

                    IPAddress[] ips = Dns.GetHostAddresses(syslog.HostName);
                    if (ips.Length > 0)
                    {
                        foreach (IPAddress ip in ips)
                        {
                            if (ip.ToString().IndexOf('.') != -1)
                            {
                                syslog.Ip = ip.ToString();
                            }
                        }
                    }
                    string opLog = string.Empty;
                    var menu = db.Sys_Menu.FirstOrDefault(x => x.MenuId == strMenuId);
                    if (menu != null)
                    {
                        opLog = menu.MenuName + ":";
                    }

                    if (!string.IsNullOrEmpty(strOperationName))
                    {
                        opLog += strOperationName;
                    }

                    if (!string.IsNullOrEmpty(code))
                    {
                        syslog.OperationLog = opLog + "；" + code + "。";
                    }

                    if (!string.IsNullOrEmpty(CurrUser.LoginProjectId) && CurrUser.LoginProjectId != "null")
                    {
                        syslog.ProjectId = CurrUser.LoginProjectId;
                    }

                    db.Sys_Log.InsertOnSubmit(syslog);
                    db.SubmitChanges();
                }
            }
        }

        /// <summary>
        /// 根据项目Id删除所有相关日志信息
        /// </summary>
        /// <param name="projectId"></param>
        public static void DeleteLog(string projectId)
        {
            Model.SGGLDB db = Funs.DB;
            var q = (from x in db.Sys_Log where x.ProjectId == projectId select x).ToList();
            if (q != null)
            {
                db.Sys_Log.DeleteAllOnSubmit(q);
                db.SubmitChanges();
            }
        }
    }
}