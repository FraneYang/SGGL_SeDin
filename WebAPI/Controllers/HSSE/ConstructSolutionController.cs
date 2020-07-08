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
    /// 施工方案
    /// </summary>
    public class ConstructSolutionController : ApiController
    {
        #region 根据主键ID获取施工方案
        /// <summary>
        ///  根据主键ID获取施工方案
        /// </summary>
        /// <param name="constructSolutionId"></param>
        /// <returns></returns>
        public Model.ResponeData getConstructSolutionByConstructSolutionId(string constructSolutionId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                 responeData.data = APIConstructSolutionService.getConstructSolutionByConstructSolutionId(constructSolutionId);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion

        #region 根据projectId、unitid获取施工方案信息-查询
        /// <summary>
        /// 根据projectId、unitid获取施工方案信息
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="unitId"></param>
        ///  <param name="strParam">查询条件</param>
        ///  <param name="states">查询条件</param>
        /// <param name="pageIndex"></param>  
        /// <returns></returns>
        public Model.ResponeData getConstructSolutionListQuery(string projectId, string unitId, string strParam, string states, int pageIndex)
        {
            var responeData = new Model.ResponeData();
            try
            {
                var getDataList = APIConstructSolutionService.getConstructSolutionList(projectId, unitId, strParam, states);
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

        #region 保存施工方案 Solution_ConstructSolution
        /// <summary>
        /// 保存施工方案 Solution_ConstructSolution
        /// </summary>
        /// <param name="newItem">施工方案</param>
        /// <returns></returns>
        [HttpPost]
        public Model.ResponeData SaveConstructSolution([FromBody] Model.ConstructSolutionItem newItem)
        {
            var responeData = new Model.ResponeData();
            try
            {
                APIConstructSolutionService.SaveConstructSolution(newItem);
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
