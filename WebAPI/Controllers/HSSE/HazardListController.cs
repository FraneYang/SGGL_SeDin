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
    /// 危险源辨识与评价
    /// </summary>
    public class HazardListController : ApiController
    {
        #region 根据主键ID获取危险源辨识与评价详细
        /// <summary>
        ///  根据主键ID获取危险源辨识与评价详细
        /// </summary>
        /// <param name="hazardListId"></param>
        /// <returns></returns>
        public Model.ResponeData getHazardListInfoById(string hazardListId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                ////危险源辨识与评价主表信息
                var getHazardInfo= APIHazardListService.getHazardListInfoByHazardListId(hazardListId);
                ////危险源辨识与评价明细信息
                var getHazardSelectedInfo = APIHazardListService.getHazardListSelectedInfo(hazardListId);
                responeData.data = new { getHazardInfo , getHazardSelectedInfo };
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion

        #region 根据projectId获取危险源辨识评价列表
        /// <summary>
        /// 根据projectId、unitid获取施工方案信息
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="pageIndex"></param>        
        /// <returns></returns>
        public Model.ResponeData getHazardList(string projectId, int pageIndex)
        {
            var responeData = new Model.ResponeData();
            try
            {
                var getDataList = APIHazardListService.getHazardListList(projectId);
                int pageCount = getDataList.Count();
                if (pageCount > 0 && pageIndex > 0)
                {
                    getDataList = getDataList.Skip(Funs.PageSize * (pageIndex - 1)).Take(Funs.PageSize).ToList();
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
    }
}
