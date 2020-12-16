using System;
using System.Timers;
using System.DirectoryServices;
using System.Linq;
using System.Web.UI.WebControls;
using System.Collections.Generic;

namespace BLL
{
    public class RealNameMonitorService
    {

        #region 启动监视器 系统启动5分钟
        /// <summary>
        /// 监视组件
        /// </summary>
        private static Timer messageTimer;

        /// <summary>
        /// 启动监视器,不一定能成功，根据系统设置决定对监视器执行的操作 系统启动5分钟
        /// </summary>
        public static void StartMonitor()
        {
            int adTimeJ = 0;
            var getSynchroSet = Funs.DB.RealName_SynchroSet.FirstOrDefault();
            if (getSynchroSet != null && getSynchroSet.Intervaltime.HasValue)
            {
                adTimeJ = getSynchroSet.Intervaltime.Value;
            }
            if (messageTimer != null)
            {
                messageTimer.Stop();
                messageTimer.Dispose();
                messageTimer = null;
            }
            if (adTimeJ > 0)
            {
                messageTimer = new Timer
                {
                    AutoReset = true
                };
                messageTimer.Elapsed += new ElapsedEventHandler(AdUserInProcess);
                messageTimer.Interval = 1000 * 60 * adTimeJ;// 60分钟 60000 * adTimeJ;
                messageTimer.Start();
            }
        }

        /// <summary>
        /// 流程确认 定时执行 系统启动5分钟
        /// </summary>
        /// <param name="sender">Timer组件</param>
        /// <param name="e">事件参数</param>
        private static void AdUserInProcess(object sender, ElapsedEventArgs e)
        {
            var getRProjects = from x in Funs.DB.RealName_Project select x;
            if (getRProjects.Count() > 0)
            {
                foreach (var item in getRProjects)
                {
                    if (!string.IsNullOrEmpty(item.ProCode))
                    {
                        SynchroSetService.PushCollCompany();
                        SynchroSetService.PushProCollCompany(item.ProCode);
                        SynchroSetService.PushCollTeam(item.ProCode);
                        SynchroSetService.getCollTeam(item.ProCode);
                        SynchroSetService.PushPersons(Const.BtnAdd, item.ProCode);
                        SynchroSetService.PushPersons(Const.BtnModify, item.ProCode);
                        SynchroSetService.PushAttendance(item.ProCode);
                    }
                }
            }
        }
        #endregion
    }
}
