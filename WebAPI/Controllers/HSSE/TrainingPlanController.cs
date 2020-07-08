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
    /// 培训计划
    /// </summary>
    public class TrainingPlanController : ApiController
    {
        #region 根据projectId、trainTypeId、states获取培训计划列表
        /// <summary>
        /// 根据projectId、trainTypeId、states获取培训记录列表
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="trainTypeId"></param>
        /// <param name="states"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public Model.ResponeData getTrainingPlanListByProjectIdTrainTypeIdTrainStates(string projectId, string trainTypeId, string states, int pageIndex)
        {
            var responeData = new Model.ResponeData();
            try
            {
                var getDataLists = APITrainingPlanService.getTrainingPlanListByProjectIdTrainTypeIdTrainStates(projectId, trainTypeId, states);
                int pageCount = getDataLists.Count;
                if (pageCount > 0 && pageIndex > 0)
                {
                    var getdata = from x in getDataLists.Skip(BLL.Funs.PageSize * (pageIndex - 1)).Take(BLL.Funs.PageSize)
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

        #region 根据培训计划ID获取培训详细
        /// <summary>
        ///  根据培训ID获取培训计划详细
        /// </summary>
        /// <param name="planId">培训计划ID</param>
        /// <returns></returns>
        public Model.ResponeData getTrainingPlanByTrainingId(string planId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = APITrainingPlanService.getTrainingPlanByTrainingId(planId);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion

        #region 根据TrainingPlanId获取培训教材类型列表
        /// <summary>
        /// 根据TrainingPlanId获取培训教材类型列表
        /// </summary>
        /// <param name="trainingPlanId"></param>
        /// <returns>培训教材类型</returns>
        public Model.ResponeData getTrainingPlanItemListByTrainingPlanId(string trainingPlanId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = APITrainingPlanService.getTrainingPlanItemListByTrainingPlanId(trainingPlanId);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }
            return responeData;
        }
        #endregion

        #region 保存 TrainingPlan
        /// <summary>
        /// 保存TrainingPlan
        /// </summary>
        /// <param name="trainingPlan">培训计划记录</param>
        [HttpPost]
        public Model.ResponeData SaveTrainingPlan([FromBody] Model.TrainingPlanItem trainingPlan)
        {
            var responeData = new Model.ResponeData();
            try
            {
                if (CommonService.IsMainUnitOrAdmin(trainingPlan.DesignerId))
                {
                    APITrainingPlanService.SaveTrainingPlan(trainingPlan);
                }
                else
                {
                    responeData.code = 0;
                    responeData.message = "非本单位用户，不能制定培训计划！";
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
    }
}
