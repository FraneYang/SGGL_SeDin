using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace WebAPI.Controllers
{
    /// <summary>
    /// 安全巡检
    /// </summary>
    public class HazardRegisterController : ApiController
    {
        #region 根据hazardRegisterId获取风险信息
        /// <summary>
        /// 根据hazardRegisterId获取风险信息
        /// </summary>
        /// <param name="hazardRegisterId"></param>
        /// <returns></returns>
        public Model.ResponeData getHazardRegisterByHazardRegisterId(string hazardRegisterId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = BLL.APIHazardRegisterService.getHazardRegisterByHazardRegisterId(hazardRegisterId);              
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion

        #region 根据projectId、states获取风险信息
        /// <summary>
        /// 根据projectId、states获取风险信息
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="states"></param>
        /// <param name="pageIndex">页码</param>
        /// <returns></returns>
        public Model.ResponeData getHazardRegisterByProjectIdStates(string projectId, string states, int pageIndex)
        {
            var responeData = new Model.ResponeData();
            try
            {
                List<Model.HazardRegisterItem> getDataList = new List<Model.HazardRegisterItem>();
                int pageCount = Funs.DB.HSSE_Hazard_HazardRegister.Count(x => x.ProjectId == projectId && (x.States == states || states == null));
                if (pageCount > 0 && pageIndex > 0)
                {
                    getDataList = APIHazardRegisterService.getHazardRegisterByProjectIdStates(projectId, states, pageIndex);
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

        #region 根据projectId获取各状态风险数
        /// <summary>
        /// 根据projectId获取各状态风险数
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public Model.ResponeData getHazardRegisterCount(string projectId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                //总数
                var getDataList = Funs.DB.HSSE_Hazard_HazardRegister.Where(x => x.ProjectId == projectId);
                int tatalCount = getDataList.Count();
                //待整改
                int count1 = getDataList.Where(x => x.States == "1").Count();
                //待确认
                int count2 = getDataList.Where(x => x.States == "2").Count();
                //已闭环
                int count3 = getDataList.Where(x => x.States == "3").Count();
                //已作废
                int count4 = getDataList.Where(x => x.States == "4").Count();

                responeData.data = new { tatalCount, count1, count2, count3, count4 };
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion

        #region 保存HazardRegister
        /// <summary>
        /// 保存HazardRegister
        /// </summary>
        /// <param name="hazardRegister"></param>
        /// <returns></returns>
        [HttpPost]
        public Model.ResponeData SaveHazardRegister([FromBody] Model.HazardRegisterItem hazardRegister)
        {           
            var responeData = new Model.ResponeData();
            try
            {
                BLL.APIHazardRegisterService.SaveHazardRegister(hazardRegister);                 
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
