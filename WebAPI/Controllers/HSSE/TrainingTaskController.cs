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
    /// 培训任务
    /// </summary>
    public class TrainingTaskController : ApiController
    {
        #region 根据ProjectId、PersonId获取培训任务教材明细列表
        /// <summary>
        /// 根据ProjectId、PersonId获取培训任务教材明细列表
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="personId"></param>
        /// <param name="pageIndex">1-培训中；2-已完成</param>
        /// <returns></returns>
        public Model.ResponeData getTrainingTaskListByProjectIdPersonId(string projectId, string personId, int pageIndex)
        {
            var responeData = new Model.ResponeData();
            try
            {
                var getQualityLists = APITrainingTaskService.getTrainingTaskListByProjectIdPersonId(projectId, personId);
                int pageCount = getQualityLists.Count;
                if (pageCount > 0 && pageIndex > 0)
                {
                    var getdata = from x in getQualityLists.OrderByDescending(u => u.TrainStartDate).Skip(BLL.Funs.PageSize * (pageIndex - 1)).Take(BLL.Funs.PageSize)
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

        #region 根据TaskId获取培训任务教材明细列表
        /// <summary>
        /// 根据TaskId获取培训任务教材明细列表
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public Model.ResponeData getTrainingTaskItemListByTaskId(string taskId, int pageIndex)
        {
            var responeData = new Model.ResponeData();
            try
            {
                var getQualityLists = APITrainingTaskService.getTrainingTaskItemListByTaskId(taskId);
                int pageCount = getQualityLists.Count;
                if (pageCount > 0 && pageIndex > 0)
                {
                    var getdata = from x in getQualityLists.OrderByDescending(u => u.TrainingItemCode).Skip(Funs.PageSize * (pageIndex - 1)).Take(Funs.PageSize)
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

        #region 根据PlanId、PersonId将人员加入培训任务(扫码)
        /// <summary>
        /// 根据PlanId、PersonId将人员加入培训任务(扫码)
        /// </summary>
        /// <param name="planId">培训计划ID</param>
        /// <param name="personId">人员ID</param>
        /// <returns></returns>
        public Model.ResponeData getTrainingTaskByPlanIdPersonId(string planId, string personId)
        {
            var responeData = new Model.ResponeData();
            try
            {
               string newPersonId = PersonService.GetPersonIdByUserId(personId);
                responeData.message = APITrainingTaskService.getTrainingTaskByPlanIdPersonIdCondition(planId, newPersonId);
                if (string.IsNullOrEmpty(responeData.message))
                {
                    APITrainingTaskService.getTrainingTaskByPlanIdPersonId(planId, newPersonId);
                }
                else
                {
                    responeData.code = 0;
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

        #region 根据TrainingPlanId获取培训人员任务列表
        /// <summary>
        /// 根据TrainingPlanId获取培训人员任务列表
        /// </summary>
        /// <param name="trainingPlanId"></param>
        /// <returns>培训人员</returns>
        public Model.ResponeData getTrainingTaskListByTrainingPlanId(string trainingPlanId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = APITrainingTaskService.getTrainingTaskListByTrainingPlanId(trainingPlanId);
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
