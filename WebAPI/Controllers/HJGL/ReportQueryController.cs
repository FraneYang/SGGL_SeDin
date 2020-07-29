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
    /// 报表查询统计
    /// </summary>
    public class ReportQueryController: ApiController
    {
        #region 根据人员二维码获取焊工业绩
        /// <summary>
        /// 根据人员二维码获取焊工业绩
        /// </summary>
        /// <param name="personId"></param>
        /// <returns></returns>
        public Model.ResponeData GetWelderPerformanceByQRC(string personId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = APIReportQueryService.GetWelderPerformanceByQRC(personId);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion

        #region 根据焊工号获取焊工业绩
        /// <summary>
        /// 根据焊工号获取焊工业绩
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="welderCode"></param>
        /// <returns></returns>
        public Model.ResponeData GetWelderPerformanceByWelderCode(string projectId, string welderCode)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = APIReportQueryService.GetWelderPerformanceByWelderCode(projectId, welderCode);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion

        #region  根据人员ID获取焊工合格项目
        /// <summary>
        ///  根据人员ID获取焊工合格项目
        /// </summary>
        /// <param name="personId"></param>
        /// <returns></returns>
        public Model.ResponeData GetWelderQualify(string personId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = APIReportQueryService.getWelderQualify(personId);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion

        #region 焊工资质预警
        /// <summary>
        /// 焊工资质预警
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public Model.ResponeData GetWelderQualifyWarning(string projectId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = APIReportQueryService.GetWelderQualifyWarning(projectId);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }

        #endregion

        #region 焊工一次合格率低于96%预警
        /// <summary>
        /// 焊工一次合格率低于96%预警
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public Model.ResponeData GetWelderOnePassRateWarning(string projectId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = APIReportQueryService.GetWelderOnePassRateWarning(projectId);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }

        #endregion

        #region 根据焊口ID获取焊口信息和焊接信息
        /// <summary>
        /// 根据焊口ID获取焊口信息和焊接信息
        /// </summary>
        /// <param name="weldJointId"></param>
        /// <returns></returns>
        public Model.ResponeData GetJointCompreInfo(string weldJointId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = APIReportQueryService.GetJointCompreInfo(weldJointId);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion


        #region 多维度查询报表
        /// <summary>
        /// 多维度查询报表
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="unitId"></param>
        /// <param name="unitWorkId"></param>
        /// <param name="pipeLineId"></param>
        /// <param name="material"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public Model.ResponeData GetReportQueryByRequir(string projectId, string unitId, string unitWorkId, string pipeLineId, string material, string startTime, string endTime)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = APIReportQueryService.GetReportQueryByRequir(projectId, unitId, unitWorkId, pipeLineId, material, startTime, endTime);
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