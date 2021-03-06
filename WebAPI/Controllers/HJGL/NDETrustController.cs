﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BLL;
using Newtonsoft.Json;

namespace WebAPI.Controllers
{
    /// <summary>
    /// 点口，委托，无损检测
    /// </summary>
    public class NDETrustController : ApiController
    {
        #region 点口
        /// <summary>
        /// 根据单位工程、项目Id获取未点口的批
        /// </summary>
        /// <param name="unitWorkId">单位工程Id</param>
        /// <param name="projectId">项目Id</param>
        /// <returns></returns>
        public Model.ResponeData getNotEndPointBatch(string unitWorkId, string projectId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = APINDETrustService.getNotEndPointBatch(unitWorkId, projectId);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }

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
        public Model.ResponeData getPointBatchCode(string unitWorkId, string startDate, string detectionTypeId, string detectionRateId, string pointBatchCode)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = APINDETrustService.getPointBatchCode(unitWorkId, startDate, detectionTypeId, pointBatchCode, pointBatchCode);
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
                responeData.code = 1;
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }

        /// <summary>
        /// 对所选批次进行强制关闭批
        /// </summary>
        /// <param name="pointBatchId"></param>
        /// <returns></returns>
        public Model.ResponeData getCloseBatchSave(string pointBatchId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                var batch = BLL.PointBatchService.GetPointBatchById(pointBatchId);
                if (batch != null)
                {
                    if (!batch.EndDate.HasValue)
                    {
                        PointBatchService.UpdateBatchIsClosed2(pointBatchId, DateTime.Now);
                        //生成委托单
                        Model.HJGL_Batch_BatchTrust newBatchTrust = new Model.HJGL_Batch_BatchTrust();
                        var project = BLL.ProjectService.GetProjectByProjectId(batch.ProjectId);
                        var unit = BLL.UnitService.GetUnitByUnitId(batch.UnitId);
                        var area = BLL.UnitWorkService.getUnitWorkByUnitWorkId(batch.UnitWorkId);
                        var ndt = BLL.Base_DetectionTypeService.GetDetectionTypeByDetectionTypeId(batch.DetectionTypeId);
                        var rate = BLL.Base_DetectionRateService.GetDetectionRateByDetectionRateId(batch.DetectionRateId);

                        string perfix = string.Empty;

                        newBatchTrust.TrustBatchCode = batch.PointBatchCode.Replace("-DK-", "-WT-");
                        string trustBatchId = SQLHelper.GetNewID(typeof(Model.HJGL_Batch_BatchTrust));
                        newBatchTrust.TrustBatchId = trustBatchId;

                        newBatchTrust.TrustDate = DateTime.Now;
                        newBatchTrust.ProjectId = batch.ProjectId;
                        newBatchTrust.PointBatchId = batch.PointBatchId;
                        newBatchTrust.UnitId = batch.UnitId;
                        newBatchTrust.UnitWorkId = batch.UnitWorkId;
                        newBatchTrust.DetectionTypeId = batch.DetectionTypeId;
                        newBatchTrust.DetectionRateId = batch.DetectionRateId;
                        newBatchTrust.PointBatchId = pointBatchId;
                        BLL.Batch_BatchTrustService.AddBatchTrust(newBatchTrust);  // 新增委托单
                        responeData.code = 1;
                    }
                }
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
        /// <param name="items"></param>
        /// <returns></returns>
        public Model.ResponeData getRePointSave(string items)
        {
            var responeData = new Model.ResponeData();
            try
            {
                //APINDETrustService.RePointSave(oldJointId, newJointId);
                var j = JsonConvert.DeserializeObject<dynamic>(items);
                foreach (var item in j)
                {
                    if (item.selected == true)
                    {
                        BLL.PointBatchDetailService.UpdatePointBatchDetail(Convert.ToString(item.PointBatchItemId), "1", DateTime.Now);
                    }
                    else
                    {
                        BLL.PointBatchDetailService.UpdatePointBatchDetail(Convert.ToString(item.PointBatchItemId), null, null);
                    }
                }
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
        /// <param name="startDate"></param>
        /// <param name="trustBatchCode"></param>
        /// <returns></returns>
        public Model.ResponeData getBatchTrustCode(string unitWorkId, string detectionTypeId, string detectionRateId, string startDate, string trustBatchCode)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = APINDETrustService.getBatchTrustCode(unitWorkId, detectionTypeId, detectionRateId, startDate, trustBatchCode);
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

        #region 生成委托单
        /// <summary>
        /// 生成委托单
        /// </summary>
        /// <param name="newItem">委托单</param>
        /// <returns></returns>
        [HttpPost]
        public Model.ResponeData SaveTrust([FromBody] Model.BatchItem newItem)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = APINDETrustService.SaveTrust(newItem.PointBatchId, newItem.NDEUnit);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion

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
        public Model.ResponeData getBatchNdeCode(string unitWorkId, string startDate, string detectionTypeId, string ndeCode)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = APINDETrustService.getBatchNdeCode(unitWorkId, startDate, detectionTypeId, ndeCode);
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

        #region 返修/扩透
        /// <summary>
        /// 获取返修单列表
        /// </summary>
        /// <param name="unitWorkId"></param>
        /// <param name="isAudit"></param>
        /// <param name="repairRecordCode"></param>
        /// <returns></returns>
        public Model.ResponeData GetRepairRecord(string unitWorkId, bool isAudit, string repairRecordCode)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = APINDETrustService.GetRepairRecord(unitWorkId, isAudit, repairRecordCode);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }

        /// <summary>
        /// 获取返修单信息
        /// </summary>
        /// <param name="repairRecordId"></param>
        /// <returns></returns>
        public Model.ResponeData GetRepairInfoByRepairRecordId(string repairRecordId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = APINDETrustService.GetRepairInfoByRepairRecordId(repairRecordId);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }

        /// <summary>
        /// 根据条件获取可选取扩透口的批明细
        /// </summary>
        /// <param name="repairRecordId"></param>
        /// <param name="welder">同焊工</param>
        /// <param name="pipeLine">同管线</param>
        /// <param name="daily">同一天</param>
        /// <param name="repairBefore">返修前所焊</param>
        /// <param name="mat">同材质</param>
        /// <param name="spec">同规格</param>
        /// <returns></returns>
        public Model.ResponeData GetRepairExpDetail(string repairRecordId, bool welder, bool batch, bool pipeLine, bool daily, bool repairBefore, bool mat, bool spec)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = APINDETrustService.GetRepairExpDetail(repairRecordId, welder, batch, pipeLine, daily, repairBefore, mat, spec);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }

        /// <summary>
        /// 获取扩透口的随机数
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public Model.ResponeData GetRandomExport(int num)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = APINDETrustService.RandomExport(num);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }

        /// <summary>
        /// 保存返修/扩透口信息
        /// </summary>
        /// <param name="repairRecordId">返修ID</param>
        /// <param name="expandId">扩透口ID（多个用“,”号隔开）</param>
        /// <param name="repairWelder">返修焊工</param>
        /// <param name="repairDate">返修日期</param>
        /// <param name="isCut">是否切除</param>
        public Model.ResponeData GetRepairExpSaveInfo(string repairRecordId, string expandId, string repairWelder, string repairDate, bool isCut)
        {
            var responeData = new Model.ResponeData();
            try
            {
                APINDETrustService.GetRepairExpSaveInfo(repairRecordId, expandId, repairWelder, repairDate, isCut);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }

        /// <summary>
        /// 返修单审核
        /// </summary>
        /// <param name="repairRecordId"></param>
        /// <returns></returns>
        public Model.ResponeData GetRepairAudit(string repairRecordId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                APINDETrustService.RepairAudit(repairRecordId);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }

        /// <summary>
        /// 生成返修委托单
        /// </summary>
        /// <param name="repairRecordId"></param>
        /// <returns></returns>
        public Model.ResponeData GetGenerateRepairTrust(string repairRecordId)

        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = APINDETrustService.GenerateRepairTrust(repairRecordId);
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