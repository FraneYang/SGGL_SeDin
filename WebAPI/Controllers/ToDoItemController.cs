using BLL;
using System;
using System.Linq;
using System.Web.Http;

namespace WebAPI.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class ToDoItemController : ApiController
    {
        /// <summary>
        /// 根据projectId,userId获取待办事项
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Model.ResponeData getToDoItemByProjectIdUserId(string projectId, string userId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                var getDataList = new Model.SGGLDB(Funs.ConnString).Sp_APP_GetToDoItems(projectId, userId).ToList();
                responeData.data = new { getDataList.Count, getDataList };
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
    }
}
