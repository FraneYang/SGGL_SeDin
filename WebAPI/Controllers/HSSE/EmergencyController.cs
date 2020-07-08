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
    /// 应急信息
    /// </summary>
    public class EmergencyController : ApiController
    {
        #region 根据主键ID获取应急预案信息
        /// <summary>
        ///  根据主键ID获取应急预案信息
        /// </summary>
        /// <param name="emergencyListId"></param>
        /// <returns></returns>
        public Model.ResponeData getEmergencyListById(string emergencyListId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = APIEmergencyService.getEmergencyListByEmergencyListId(emergencyListId);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion

        #region 获取应急预案信息列表-查询
        /// <summary>
        /// 获取应急预案信息列表-查询
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="unitId"></param>
        /// <param name="states"></param>
        /// <param name="pageIndex"></param>
        ///  <param name="strParam">查询条件</param>
        /// <returns></returns>
        public Model.ResponeData getEmergencyList(string projectId, string unitId, string states, string strParam, int pageIndex)
        {
            var responeData = new Model.ResponeData();
            try
            {
                var getDataList = APIEmergencyService.getEmergencyList(projectId, unitId, states, strParam);
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

        #region 根据主键ID获取应急物资信息
        /// <summary>
        ///  根据主键ID获取应急物资信息
        /// </summary>
        /// <param name="emergencySupplyId"></param>
        /// <returns></returns>
        public Model.ResponeData getEmergencySupplyById(string emergencySupplyId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = APIEmergencyService.getEmergencySupplyByEmergencySupplyId(emergencySupplyId);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion

        #region 获取应急物资信息列表-查询
        /// <summary>
        /// 获取应急物资信息列表-查询
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="unitId"></param>
        /// <param name="states"></param>
        /// <param name="pageIndex"></param>
        ///  <param name="strParam">查询条件</param>
        /// <returns></returns>
        public Model.ResponeData getEmergencySupplyList(string projectId, string unitId, string states, string strParam, int pageIndex)
        {
            var responeData = new Model.ResponeData();
            try
            {
                var getDataList = APIEmergencyService.getEmergencySupplyList(projectId, unitId, states, strParam);
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

        #region 根据主键ID获取应急队伍信息
        /// <summary>
        ///  根据主键ID获取应急队伍信息
        /// </summary>
        /// <param name="emergencyTeamAndTrainId"></param>
        /// <returns></returns>
        public Model.ResponeData getEmergencyTeamAndTrainById(string emergencyTeamAndTrainId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = APIEmergencyService.getEmergencyTeamAndTrainByEmergencyTeamAndTrainId(emergencyTeamAndTrainId);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion

        #region 获取应急队伍信息列表-查询
        /// <summary>
        /// 获取应急队伍信息列表-查询
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="unitId"></param>
        /// <param name="states"></param>
        /// <param name="pageIndex"></param>
        ///  <param name="strParam">查询条件</param>
        /// <returns></returns>
        public Model.ResponeData getEmergencyTeamAndTrainList(string projectId, string unitId, string states, string strParam, int pageIndex)
        {
            var responeData = new Model.ResponeData();
            try
            {
                var getDataList = APIEmergencyService.getEmergencyTeamAndTrainList(projectId, unitId, states, strParam);
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

        #region 保存应急信息
        /// <summary>
        /// 保存应急信息
        /// </summary>
        /// <param name="emergencyInfo">应急信息(MenuType:1-预案,2-物资,3-队伍)</param>
        /// <returns></returns>
        [HttpPost]
        public Model.ResponeData SaveEmergency([FromBody] Model.FileInfoItem emergencyInfo)
        {
            var responeData = new Model.ResponeData();
            try
            {
                APIEmergencyService.SaveEmergency(emergencyInfo);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion

        #region 获取应急流程列表-查询
        /// <summary>
        /// 获取应急队伍信息列表-查询
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="processSteps">步骤</param>        
        /// <returns></returns>
        public Model.ResponeData getEmergencyProcessItem(string projectId, string processSteps)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = APIEmergencyService.getEmergencyProcessItem(projectId, processSteps);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }
            return responeData;
        }
        #endregion

        #region 获取应急流程列表-查询
        /// <summary>
        /// 获取应急队伍信息列表-查询
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="pageIndex"></param>
        ///  <param name="strParam">查询条件</param>
        /// <returns></returns>
        public Model.ResponeData getEmergencyProcessList(string projectId,  string strParam, int pageIndex)
        {
            var responeData = new Model.ResponeData();
            try
            {
                var getDataList = APIEmergencyService.getEmergencyProcessList(projectId,  strParam);
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
