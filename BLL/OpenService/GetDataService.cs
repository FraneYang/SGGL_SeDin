using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace BLL
{
    public static class GetDataService
    {        
        #region 定时推送待办 订阅服务内容
        /// <summary>
        /// 定时推送待办 订阅服务内容
        /// </summary>
        public static void SendSubscribeMessage()
        {
            try
            {
                string miniprogram_state = ConfigurationManager.AppSettings["miniprogram_state"];
                if (!string.IsNullOrEmpty(miniprogram_state) && miniprogram_state == "formal")
                {
                    ////// 获取所有待办事项
                    //var getToItems = from x in Funs.DB.View_APP_GetToDoItems select x;
                    //if (getToItems.Count() > 0)
                    //{
                        ////// 获取施工中的项目
                        //var getProjects = ProjectService.GetProjectWorkList();
                        //foreach (var item in getProjects)
                        //{
                        //    ////获取当前项目下的待办
                        //    var getPItems = getToItems.Where(x => x.ProjectId == item.ProjectId);
                        //    if (getPItems.Count() > 0)
                        //    {
                        //        foreach (var itemP in getPItems)
                        //        {
                        //            APICommonService.SendSubscribeMessage(itemP.UserId, "项目【" + item.ProjectCode + "】上有" + itemP.Counts.ToString() + "条待办事件，需要您处理！", "赛鼎施工管理系统", string.Format("{0:yyyy-MM-dd HH:mm:ss}", DateTime.Now));
                        //        }
                        //    }
                        //}
                  //  }
                }
            }
            catch (Exception ex)
            { }
        }

        #endregion
    }
}
