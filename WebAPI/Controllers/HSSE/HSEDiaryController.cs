using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BLL;

namespace WebAPI.Controllers
{
    /// <summary>
    /// HSE日志
    /// </summary>
    public class HSEDiaryController : ApiController
    {
        #region 获取HSE日志列表
        /// <summary>
        /// 获取HSE日志列表
        /// </summary>
        /// <param name="projectId">项目ID</param>
        /// <param name="userId">用户ID</param>
        /// <param name="diaryDate">日期</param>
        /// <param name="pageIndex">页码</param>
        /// <returns></returns>
        public Model.ResponeData getHSEDiaryList(string projectId, string userId, string diaryDate, int pageIndex)
        {
            var responeData = new Model.ResponeData();
            try
            {
                var getDataList = APIHSEDiaryService.getHSEDiaryList(projectId, userId, diaryDate);
                int pageCount = getDataList.Count;
                if (pageCount > 0 && pageIndex > 0)
                {
                    getDataList = (from x in getDataList.Skip(Funs.PageSize * (pageIndex - 1)).Take(Funs.PageSize)
                                   select x).ToList();
                }
                responeData.data = new { pageCount, getDataList };
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }
            return responeData;
        }
        #endregion

        #region 获取HSE日志详细信息
        /// <summary>
        /// 获取HSE日志详细信息
        /// </summary>
        /// <param name="projectId">项目ID</param>
        /// <param name="userId">用户ID</param>
        /// <param name="diaryDate">日期</param>
        /// <returns></returns>
        public Model.ResponeData getHSEDiary(string projectId, string userId, string diaryDate)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = APIHSEDiaryService.getHSEDiary(projectId, userId, diaryDate);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }
            return responeData;
        }
        #endregion

        #region 保存HSE日志信息
        /// <summary>
        /// 保存HSE日志信息
        /// </summary>
        /// <param name="hseDiaryItem">HSE日志</param>
        [HttpPost]
        public Model.ResponeData SaveHSEDiary([FromBody] Model.HSEDiaryItem hseDiaryItem)
        {
            var responeData = new Model.ResponeData();
            try
            {
                APIHSEDiaryService.SaveHSEDiary(hseDiaryItem);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion
    }
}
