using System;
using System.Timers;
using System.DirectoryServices;
using System.Linq;

namespace BLL
{
    public class MonitorService
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
            //int adTimeJ = 60;
            //if (adomain.AdTimeH.HasValue)
            //{
            //    adTimeJ += adomain.AdTimeH.Value * 60;
            //}
            //if (adomain.AdTimeM.HasValue)
            //{
            //    adTimeJ += adomain.AdTimeM.Value;
            //}
            if (messageTimer != null)
            {
                messageTimer.Stop();
                messageTimer.Dispose();
                messageTimer = null;
            }

            messageTimer = new Timer
            {
                AutoReset = true
            };
            messageTimer.Elapsed += new ElapsedEventHandler(AdUserInProcess);
            messageTimer.Interval = 1000 * 60 * 90;// 60分钟 60000 * adTimeJ;
            messageTimer.Start();
        }

        /// <summary>
        /// 流程确认 定时执行 系统启动5分钟
        /// </summary>
        /// <param name="sender">Timer组件</param>
        /// <param name="e">事件参数</param>
        private static void AdUserInProcess(object sender, ElapsedEventArgs e)
        {
            //if (messageTimer != null)
            //{
            //    messageTimer.Stop();
            //}

            DoSynchData();           

            //if (messageTimer != null)
            //{
            //    messageTimer.Dispose();
            //    messageTimer = null;
            //}
        }
        #endregion

        #region 启动监视器 定时0:05执行
        /// <summary>
        /// 监视组件
        /// </summary>
        private static Timer messageTimerEve;

        /// <summary>
        /// 启动监视器,不一定能成功，根据系统设置决定对监视器执行的操作 定时
        /// </summary>
        public static void StartMonitorEve()
        {
            if (messageTimerEve != null)
            {
                messageTimerEve.Stop();
                messageTimerEve.Dispose();
                messageTimerEve = null;
            }

            messageTimerEve = new Timer
            {
                AutoReset = true
            };
            messageTimerEve.Elapsed += new ElapsedEventHandler(ColligateFormConfirmProcessEve);
            messageTimerEve.Interval = GetMessageTimerEveNextInterval();
            messageTimerEve.Start();
        }

        /// <summary>
        ///  流程确认 定时执行 定时00:05 执行
        /// </summary>
        /// <param name="sender">Timer组件</param>
        /// <param name="e">事件参数</param>
        private static void ColligateFormConfirmProcessEve(object sender, ElapsedEventArgs e)
        {
            try
            {
                if (messageTimerEve != null)
                {
                    messageTimerEve.Stop();
                }

                DoSynchData();
                
            }
            catch (Exception ex)
            {
                ErrLogInfo.WriteLog("定时器启动异常。",ex);
            }
            finally
            {
                messageTimerEve.Interval = GetMessageTimerEveNextInterval();
                messageTimerEve.Start();
            }
        }

        /// <summary>
        /// 计算MessageTimerEve定时器的执行间隔
        /// </summary>
        /// <returns>执行间隔</returns>
        private static double GetMessageTimerEveNextInterval()
        {
            double returnValue = 0;
            TimeSpan curentTime = DateTime.Now.TimeOfDay;
            int hour = 11;
            //if (!String.IsNullOrEmpty(Funs.AdTimeD))
            //{
            //    hour = int.Parse(Funs.AdTimeD);
            //}
            TimeSpan triggerTime = new TimeSpan(hour, 30, 0);
            if (curentTime > triggerTime)
            {
                // 超过了执行时间
                returnValue = (new TimeSpan(23, 59, 59) - curentTime + triggerTime.Add(new TimeSpan(0, 0, 1))).TotalMilliseconds / 2;
            }
            else
            {
                returnValue = (triggerTime - curentTime).TotalMilliseconds / 2;
            }

            if (returnValue <= 0)
            {
                // 误差纠正
                returnValue = 1;
            }

            return returnValue;
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        public static void DoSynchData()
        {
            ////推送订阅消息 
            GetDataService.SendSubscribeMessage();
        }
    }
}
