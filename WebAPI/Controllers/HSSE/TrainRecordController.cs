using BLL;
using System;
using System.Linq;
using System.Web.Http;

namespace WebAPI.Controllers
{
    /// <summary>
    /// 培训记录
    /// </summary>
    public class TrainRecordController : ApiController
    {
        #region 根据projectId、trainTypeId、TrainStates获取培训记录列表
        /// <summary>
        /// 根据projectId、trainTypeId、TrainStates获取培训记录列表
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="trainTypeId"></param>
        /// <param name="trainStates"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public Model.ResponeData getTrainRecordListByProjectIdTrainTypeIdTrainStates(string projectId, string trainTypeId, string trainStates, int pageIndex)
        {
            var responeData = new Model.ResponeData();
            try
            {
                var getQualityLists = APITrainRecordService.getTrainRecordListByProjectIdTrainTypeIdTrainStates(projectId, trainTypeId, trainStates);
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

        #region 根据培训ID获取培训详细
        /// <summary>
        ///  根据noticeId获取通知通告详细
        /// </summary>
        /// <param name="trainRecordId"></param>
        /// <returns></returns>
        public Model.ResponeData getTrainRecordByTrainingId(string trainRecordId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = APITrainRecordService.getTrainRecordByTrainingId(trainRecordId);
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
