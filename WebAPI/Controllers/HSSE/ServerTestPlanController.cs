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
    /// 知识竞赛制定
    /// </summary>
    public class ServerTestPlanController : ApiController
    {
        #region 根据projectId、states获取考试计划列表
        /// <summary>
        /// 根据projectId、states获取考试计划列表
        /// </summary>
        /// <param name="states">状态（0-待发布；1-待考试；2-考试中；3已结束；-1作废）</param>
        /// <param name="pageIndex">分页</param>
        /// <returns></returns>
        public Model.ResponeData getTestPlanListByProjectIdStates(string states, int pageIndex)
        {
            var responeData = new Model.ResponeData();
            try
            {
                var getQualityLists = APIServerTestPlanService.getTestPlanList(states);
                int pageCount = getQualityLists.Count;
                if (pageCount > 0 && pageIndex > 0)
                {
                    var getdata = from x in getQualityLists.OrderByDescending(u => u.TestStartTime).Skip(Funs.PageSize * (pageIndex - 1)).Take(Funs.PageSize)
                                  select x;
                    responeData.data = new { pageCount, getdata };
                }
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }
            return responeData;
        }
        #endregion

        #region 根据考试计划ID获取考试详细
        /// <summary>
        ///  根据考试计划ID获取考试详细
        /// </summary>
        /// <param name="testPlanId">考试计划ID</param>
        /// <returns></returns>
        public Model.ResponeData getTestPlanByTestPlanId(string testPlanId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = APIServerTestPlanService.getTestPlanByTestPlanId(testPlanId);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion
        
        #region 根据TestPlanId获取考试试题类型列表
        /// <summary>
        /// 根据TestPlanId获取考试试题类型列表
        /// </summary>
        /// <param name="testPlanId">考试计划ID</param>
        /// <returns>试题类型</returns>
        public Model.ResponeData getTestPlanTrainingListByTestPlanId(string testPlanId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                var getDataList = APIServerTestPlanService.getTestPlanTrainingListByTestPlanId(testPlanId);
                int pageCount = getDataList.Count();
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
