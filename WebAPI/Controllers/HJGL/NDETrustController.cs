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
    /// 点口，委托，无损检测
    /// </summary>
    public class NDETrustController : ApiController
    {
        #region 点口
        /// <summary>
        /// 选择单位工程、探伤类型、探伤比例、点口批号获取需要进行点口的批，自动点口调用
        /// </summary>
        /// <param name="unitWorkId"></param>
        /// <param name="detectionTypeId"></param>
        /// <param name="detectionRateId"></param>
        /// <param name="pointBatchCode"></param>
        /// <returns></returns>
        public Model.ResponeData getAutoPointBatchCode(string unitWorkId, string detectionTypeId, string detectionRateId, string pointBatchCode)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = APINDETrustService.getAutoPointBatchCode(unitWorkId, detectionTypeId, pointBatchCode, pointBatchCode);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }

        /// <summary>
        /// 根据单位工程、点口批号、批开始时间获取需要调整的批（已点口但还未进行委托的批列表）
        /// </summary>
        /// <param name="unitWorkId"></param>
        /// <param name="detectionTypeId"></param>
        /// <param name="detectionRateId"></param>
        /// <param name="pointBatchCode"></param>
        /// <returns></returns>
        public Model.ResponeData getPointBatchCode(string unitWorkId, string detectionTypeId, string detectionRateId, string pointBatchCode)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = APINDETrustService.getPointBatchCode(unitWorkId, detectionTypeId, pointBatchCode, pointBatchCode);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }

        /// <summary>
        /// 根据点口批ID获取批明细（自动点口调用呈现）
        /// </summary>
        /// <param name="pointBatchId"></param>
        /// <returns></returns>
        public Model.ResponeData getPointBatchDetail(string pointBatchId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = APINDETrustService.getPointBatchDetail(pointBatchId);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }


        /// <summary>
        /// 根据点口批ID获取已点口还未审批的焊口
        /// </summary>
        /// <param name="pointBatchId"></param>
        /// <returns></returns>
        public Model.ResponeData getPointWeldJoint(string pointBatchId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = APINDETrustService.getPointWeldJoint(pointBatchId);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }

        /// <summary>
        /// 根据点口批ID和焊口ID获取待调整焊口
        /// </summary>
        /// <param name="pointBatchId"></param>
        /// <param name="weldJointId"></param>
        /// <returns></returns>
        public Model.ResponeData getPointWeldJoint(string pointBatchId, string weldJointId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = APINDETrustService.getPointWeldJoint(pointBatchId, weldJointId);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }

        /// <summary>
        /// 根据单位工程获取所有已点的焊口列表
        /// </summary>
        /// <param name="unitWorkId"></param>
        /// <returns></returns>
        public Model.ResponeData GetPointWeldJointList(string unitWorkId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = APINDETrustService.GetPointWeldJointList(unitWorkId);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }

        /// <summary>
        /// 对所选批次进行自动点口
        /// </summary>
        /// <param name="pointBatchId"></param>
        /// <returns></returns>
        public Model.ResponeData getAutoPointSave(string pointBatchId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                APINDETrustService.AutoPointSave(pointBatchId);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }

        /// <summary>
        /// 点口调整 
        /// </summary>
        /// <param name="oldJointId"></param>
        /// <param name="newJointId"></param>
        /// <returns></returns>
        public Model.ResponeData getRePointSave(string oldJointId, string newJointId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                APINDETrustService.RePointSave(oldJointId, newJointId);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }

        /// <summary>
        /// 根据单位工程获取所有已点的焊口生成委托单
        /// </summary>
        /// <param name="unitWorkId"></param>
        /// <returns></returns>
        public Model.ResponeData getGenerateTrust(string unitWorkId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                APINDETrustService.GenerateTrust(unitWorkId);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion

        #region 无损委托
        /// <summary>
        /// 选择单位工程、探伤类型、探伤比例、委托单号等获取委托单
        /// </summary>
        /// <param name="unitWorkId"></param>
        /// <param name="detectionTypeId"></param>
        /// <param name="detectionRateId"></param>
        /// <param name="isAudit"></param>
        /// <param name="trustBatchCode"></param>
        /// <returns></returns>
        public Model.ResponeData getBatchTrustCode(string unitWorkId, string detectionTypeId, string detectionRateId, bool? isAudit, string trustBatchCode)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = APINDETrustService.getBatchTrustCode(unitWorkId, detectionTypeId, detectionRateId, isAudit, trustBatchCode);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }


        /// <summary>
        /// 选择委托单ID获取委托单明细信息
        /// </summary>
        /// <param name="trustBatchId"></param>
        /// <returns></returns>
        public Model.ResponeData getBatchTrustDetail(string trustBatchId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = APINDETrustService.GetBatchTrustDetail(trustBatchId);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }

        /// <summary>
        /// 对所选委托单进行审核
        /// </summary>
        /// <param name="trustBatchId"></param>
        /// <returns></returns>
        public Model.ResponeData getBatchTrustAudit(string trustBatchId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                APINDETrustService.BatchTrustAudit(trustBatchId);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion

        #region 无损检测单
        /// <summary>
        /// 选择单位工程、探伤类型、检测单号获取检测单
        /// </summary>
        /// <param name="unitWorkId"></param>
        /// <param name="detectionTypeId"></param>
        /// <param name="ndeCode"></param>
        /// <returns></returns>
        public Model.ResponeData getBatchNdeCode(string unitWorkId, string detectionTypeId, string ndeCode)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = APINDETrustService.getBatchNdeCode(unitWorkId, detectionTypeId, ndeCode);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }

        /// <summary>
        /// 根据检测单ID获取检测单明细
        /// </summary>
        /// <param name="ndeId"></param>
        /// <returns></returns>
        public Model.ResponeData getBatchNDEDetail(string ndeId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = APINDETrustService.GetBatchNDEDetail(ndeId);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion

        #region NDE预警
        /// <summary>
        /// 无损检测不合格焊口信息
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public Model.ResponeData GetNdeCheckNoPassWarn(string projectId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = APINDETrustService.GetNdeCheckNoPassWarn(projectId);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }

        /// <summary>
        /// 未委托焊口预警
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public Model.ResponeData GetNoTrustJointWarn(string projectId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = APINDETrustService.GetNoTrustJointWarn(projectId);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }

        /// <summary>
        /// 未检测焊口预警
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public Model.ResponeData GetNoCheckJointWarn(string projectId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = APINDETrustService.GetNoTrustJointWarn(projectId);
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