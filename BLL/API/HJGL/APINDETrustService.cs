using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public static class APINDETrustService
    {
        #region 选择单位工程、探伤类型、探伤比例、点口批号获取需要进行点口的批
        /// <summary>
        /// 选择单位工程、探伤类型、探伤比例、点口批号获取还未点口的批
        /// </summary>
        /// <param name="unitWorkId"></param>
        /// <param name="detectionTypeId"></param>
        /// <param name="detectionRateId"></param>
        /// <param name="pointBatchCode"></param>
        /// <returns></returns>
        public static List<Model.NDETrustItem> getAutoPointBatchCode(string unitWorkId,string detectionTypeId,string detectionRateId, string pointBatchCode)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var dataList = from x in db.HJGL_Batch_PointBatch
                               where x.UnitWorkId == unitWorkId && x.EndDate == null
                               select x;

                if (!string.IsNullOrEmpty(detectionTypeId))
                {
                    dataList = dataList.Where(e => e.DetectionTypeId == detectionTypeId);
                }
                if (!string.IsNullOrEmpty(detectionRateId))
                {
                    dataList = dataList.Where(e => e.DetectionRateId == detectionRateId);
                }
                if (!string.IsNullOrEmpty(pointBatchCode))
                {
                    dataList = dataList.Where(e => e.PointBatchCode.Contains(pointBatchCode));
                }

                var getDataLists = (from x in dataList
                                    orderby x.PointBatchCode
                                    select new Model.NDETrustItem
                                    {
                                        PointBatchId = x.PointBatchId,
                                        PointBatchCode = x.PointBatchCode,
                                    }).ToList();
                return getDataLists;
            }
        }
        #endregion

        #region 根据单位工程、点口批号、批开始时间获取需要调整的批（已点口但还未进行委托的批列表）
        /// <summary>
        /// 根据单位工程、点口批号、批开始时间获取需要调整的批（已点口但还未进行委托的批列表）
        /// </summary>
        /// <param name="unitWorkId"></param>
        /// <param name="pointBatchCode"></param>
        /// <param name="detectionRateId"></param>
        /// <param name="detectionTypeId"></param>
        /// <param name="startDate"></param>
        /// <returns></returns>
        public static List<Model.NDETrustItem> getPointBatchCode(string unitWorkId,string startDate, string detectionTypeId, string detectionRateId, string pointBatchCode)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var dataList = from x in db.HJGL_Batch_PointBatch
                               join y in db.HJGL_Batch_PointBatchItem on x.PointBatchId equals y.PointBatchId
                               where x.UnitWorkId == unitWorkId
                               //&& y.IsBuildTrust == null 
                               //&& x.EndDate.HasValue
                               select x;


                if (!string.IsNullOrEmpty(startDate))
                {
                    DateTime t = Convert.ToDateTime(startDate + "-01");
                    DateTime mt = t.AddMonths(1);
                    dataList = dataList.Where(e => e.StartDate >= t && e.StartDate < mt);
                }

                if (!string.IsNullOrEmpty(detectionTypeId))
                {
                    dataList = dataList.Where(e => e.DetectionTypeId == detectionTypeId);
                }
                if (!string.IsNullOrEmpty(detectionRateId))
                {
                    dataList = dataList.Where(e => e.DetectionRateId == detectionRateId);
                }
                if (!string.IsNullOrEmpty(pointBatchCode))
                {
                    dataList = dataList.Where(e => e.PointBatchCode.Contains(pointBatchCode));
                }

                var getDataLists = (from x in dataList
                                    orderby x.PointBatchCode
                                    select new Model.NDETrustItem
                                    {
                                        PointBatchId = x.PointBatchId,
                                        PointBatchCode = x.PointBatchCode,
                                    }).Distinct().ToList();
                return getDataLists;
            }
        }
        #endregion

        #region 根据点口批ID获取批明细（自动点口呈现）
        /// <summary>
        /// 根据点口批ID获取批明细（自动点口呈现）
        /// </summary>
        /// <param name="pointBatchCode"></param>
        /// <param name="startDate"></param>
        /// <returns></returns>
        public static List<Model.NDETrustItem> getPointBatchDetail(string pointBatchId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var getDataLists = (from x in db.HJGL_Batch_PointBatchItem
                                    join y in db.HJGL_WeldJoint on x.WeldJointId equals y.WeldJointId
                                    where x.PointBatchId == pointBatchId
                                    select new Model.NDETrustItem
                                    {
                                        WeldJointId = x.WeldJointId,
                                        WeldJointCode = y.WeldJointCode,
                                        PipelineCode = y.PipelineCode,
                                        JointArea = y.JointArea,
                                        AttachUrl = y.AttachUrl
                                    }).ToList();

                return getDataLists;
            }
        }
        #endregion

        #region 根据点口批ID获取已点口还未审批的焊口
        /// <summary>
        /// 根据点口批ID获取已点口还未审批的焊口
        /// </summary>
        /// <param name="pointBatchCode"></param>
        /// <param name="startDate"></param>
        /// <returns></returns>
        public static List<Model.NDETrustItem> getPointWeldJoint(string pointBatchId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var getDataLists = (from x in db.HJGL_Batch_PointBatchItem
                                    join y in db.HJGL_WeldJoint on x.WeldJointId equals y.WeldJointId
                                    where x.IsBuildTrust == null && x.PointBatchId == pointBatchId && x.PointState == "1"
                                    select new Model.NDETrustItem
                                    {
                                        WeldJointId = x.WeldJointId,
                                        WeldJointCode = y.WeldJointCode,
                                        PipelineCode = y.PipelineCode,
                                        JointArea = y.JointArea,
                                        AttachUrl = y.AttachUrl
                                    }).ToList();

                return getDataLists;
            }
        }
        #endregion

        #region 根据点口批ID和焊口ID获取待调整焊口
        /// <summary>
        /// 根据点口批ID和焊口ID获取待调整焊口
        /// </summary>
        /// <param name="pointBatchCode"></param>
        /// <param name="startDate"></param>
        /// <returns></returns>
        public static List<Model.NDETrustItem> getPointWeldJoint(string pointBatchId, string weldJointId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var jot = BLL.WeldJointService.GetWeldJointByWeldJointId(weldJointId);

                var getDataLists = (from x in Funs.DB.HJGL_Batch_PointBatchItem
                                    join y in Funs.DB.HJGL_WeldJoint on x.WeldJointId equals y.WeldJointId
                                    where x.PointBatchId == pointBatchId && x.PointState == null
                                          && y.JointAttribute == jot.JointAttribute
                                    select new Model.NDETrustItem
                                    {
                                        WeldJointId = x.WeldJointId,
                                        WeldJointCode = y.WeldJointCode,
                                        PipelineCode = y.PipelineCode,
                                        JointArea = y.JointArea,
                                        AttachUrl = y.AttachUrl
                                    }).ToList();

                return getDataLists;
            }
        }
        #endregion

        #region 对所选批次进行自动点口
        /// <summary>
        /// 对所选批次进行自动点口
        /// </summary>
        /// <param name="pointBatchId"></param>
        public static void AutoPointSave(string pointBatchId)
        {
            BLL.PointBatchDetailService.AutoPoint(pointBatchId);
        }
        #endregion

        #region 点口调整 
        /// <summary>
        /// 点口调整
        /// </summary>
        /// <param name="oldJointId">原来点的焊口</param>
        /// <param name="newJointId">调为新的焊口</param>
        public static void RePointSave(string oldJointId,string newJointId)
        {
            var oldPoint = BLL.PointBatchDetailService.GetBatchDetailByJotId(oldJointId);
            if (oldPoint != null)
            {
                oldPoint.PointDate = null;
                oldPoint.PointState = null;
                oldPoint.IsAudit = null;
                oldPoint.IsPipelineFirst = null;
                oldPoint.IsWelderFirst = null;
            }

            var newPoint = BLL.PointBatchDetailService.GetBatchDetailByJotId(newJointId);
            if (newPoint != null)
            {
                newPoint.PointState = "1";
                newPoint.PointDate = DateTime.Now;
            }

            Funs.DB.SubmitChanges();
        }
        #endregion

        #region 根据单位工程获取所有已点的焊口列表
        /// <summary>
        /// 根据单位工程获取所有已点的焊口列表
        /// </summary>
        /// <param name="unitWorkId"></param>
        /// <returns></returns>
        public static List<Model.NDETrustItem> GetPointWeldJointList(string unitWorkId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var getDataLists = (from x in db.HJGL_Batch_PointBatchItem
                                    join y in db.HJGL_WeldJoint on x.WeldJointId equals y.WeldJointId
                                    join z in db.HJGL_Batch_PointBatch on x.PointBatchId equals z.PointBatchId
                                    where x.IsBuildTrust == null && z.UnitWorkId == unitWorkId
                                           && x.PointState == "1"
                                    select new Model.NDETrustItem
                                    {
                                        WeldJointId = x.WeldJointId,
                                        PointBatchCode = z.PointBatchCode,
                                        WeldJointCode = y.WeldJointCode,
                                        PipelineCode = y.PipelineCode,
                                        AttachUrl = y.AttachUrl
                                    }).ToList();

                return getDataLists;
            }
        }
        #endregion

        #region 根据单位工程获取所有已点的焊口生成委托单
        /// <summary>
        /// 根据单位工程获取所有已点的焊口生成委托单
        /// </summary>
        /// <param name="unitWorkId"></param>
        public static void GenerateTrust(string unitWorkId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var getViewGenerateTrustLists = (from x in db.View_GenerateTrust where x.UnitWorkId == unitWorkId select x).ToList();

                foreach (var trust in getViewGenerateTrustLists)
                {
                    Model.HJGL_Batch_BatchTrust newBatchTrust = new Model.HJGL_Batch_BatchTrust();
                    var project = BLL.ProjectService.GetProjectByProjectId(trust.ProjectId);
                    var unit = BLL.UnitService.GetUnitByUnitId(trust.UnitId);
                    var unitWork = BLL.UnitWorkService.getUnitWorkByUnitWorkId(trust.UnitWorkId);
                    var ndt = BLL.Base_DetectionTypeService.GetDetectionTypeByDetectionTypeId(trust.DetectionTypeId);
                    var rate = BLL.Base_DetectionRateService.GetDetectionRateByDetectionRateId(trust.DetectionRateId);

                    string perfix = string.Empty;
                    perfix = unit.UnitCode + "-" + ndt.DetectionTypeCode + "-" + rate.DetectionRateValue.ToString() + "%-";
                    newBatchTrust.TrustBatchCode = BLL.SQLHelper.RunProcNewId("SpGetNewCode5ByProjectId", "dbo.HJGL_Batch_BatchTrust", "TrustBatchCode", project.ProjectId, perfix);

                    string trustBatchId = SQLHelper.GetNewID(typeof(Model.HJGL_Batch_BatchTrust));
                    newBatchTrust.TrustBatchId = trustBatchId;

                    newBatchTrust.TrustDate = DateTime.Now;
                    newBatchTrust.ProjectId = trust.ProjectId;
                    newBatchTrust.UnitId = trust.UnitId;
                    newBatchTrust.UnitWorkId = trust.UnitWorkId;
                    newBatchTrust.DetectionTypeId = trust.DetectionTypeId;
                    newBatchTrust.NDEUuit = unitWork.NDEUnit;

                    BLL.Batch_BatchTrustService.AddBatchTrust(newBatchTrust);  // 新增委托单

                    // 生成委托条件对比
                    var generateTrustItem = from x in db.View_GenerateTrustItem
                                            where x.ProjectId == trust.ProjectId
                                            && x.UnitWorkId == trust.UnitWorkId && x.UnitId == trust.UnitId
                                            && x.DetectionTypeId == trust.DetectionTypeId
                                            && x.DetectionRateId == trust.DetectionRateId
                                            select x;

                    List<string> toPointBatchList = generateTrustItem.Select(x => x.PointBatchId).Distinct().ToList();

                    // 生成委托明细，并回写点口明细信息
                    foreach (var item in generateTrustItem)
                    {
                        if (BLL.Batch_BatchTrustItemService.GetIsGenerateTrust(item.PointBatchItemId)) ////生成委托单的条件判断
                        {
                            Model.HJGL_Batch_BatchTrustItem trustItem = new Model.HJGL_Batch_BatchTrustItem
                            {
                                TrustBatchItemId = SQLHelper.GetNewID(typeof(Model.HJGL_Batch_BatchTrustItem)),
                                TrustBatchId = trustBatchId,
                                PointBatchItemId = item.PointBatchItemId,
                                WeldJointId = item.WeldJointId,
                                CreateDate = DateTime.Now
                            };
                            Batch_BatchTrustItemService.AddBatchTrustItem(trustItem);
                        }

                        Model.HJGL_Batch_PointBatchItem pointBatchItem = db.HJGL_Batch_PointBatchItem.FirstOrDefault(e => e.PointBatchItemId == item.PointBatchItemId);

                        pointBatchItem.IsBuildTrust = true;
                        db.SubmitChanges();
                    }


                    // 回写委托批对应点口信息
                    if (toPointBatchList.Count() > 0)
                    {
                        string toPointBatch = String.Join(",", toPointBatchList);

                        var updateTrut = BLL.Batch_BatchTrustService.GetBatchTrustById(trustBatchId);
                        if (updateTrut != null)
                        {
                            updateTrut.TopointBatch = toPointBatch;
                            BLL.Batch_BatchTrustService.UpdateBatchTrust(updateTrut);
                        }
                    }

                }
            }
        }
        #endregion

        //////////////////////////////////////委托单/////////////////////////////////////////////////////////////

        #region 选择单位工程、探伤类型、探伤比例、委托单号等获取委托单
        /// <summary>
        /// 选择单位工程、探伤类型、探伤比例、委托单号等获取委托单
        /// </summary>
        /// <param name="unitWorkId"></param>
        /// <param name="detectionTypeId"></param>
        /// <param name="detectionRateId"></param>
        /// <param name="isAudit"></param>
        /// <param name="pointBatchCode"></param>
        /// <returns></returns>
        public static List<Model.BaseInfoItem> getBatchTrustCode(string unitWorkId, string detectionTypeId, string detectionRateId, bool? isAudit ,string trustBatchCode)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var dataList = from x in db.HJGL_Batch_BatchTrust
                               where x.UnitWorkId == unitWorkId
                               select x;

                if (!string.IsNullOrEmpty(detectionTypeId))
                {
                    dataList = dataList.Where(e => e.DetectionTypeId == detectionTypeId);
                }
                if (!string.IsNullOrEmpty(detectionRateId))
                {
                    dataList = dataList.Where(e => e.DetectionRateId == detectionRateId);
                }

                if (isAudit == true)
                {
                    dataList = dataList.Where(e => e.IsAudit == true);
                }
                else
                {
                    dataList = dataList.Where(e => e.IsAudit == null || e.IsAudit == false);
                }

                if (!string.IsNullOrEmpty(trustBatchCode))
                {
                    dataList = dataList.Where(e => e.TrustBatchCode.Contains(trustBatchCode));
                }

                var getDataLists = (from x in dataList
                                    orderby x.TrustBatchCode
                                    select new Model.BaseInfoItem
                                    {
                                        BaseInfoId = x.TrustBatchId,
                                        BaseInfoCode = x.TrustBatchCode,
                                    }).ToList();
                return getDataLists;
            }
        }
        #endregion

        #region 选择委托单ID获取委托单明细信息
        /// <summary>
        /// 选择委托单ID获取委托单明细信息
        /// </summary>
        /// <param name="trustBatchId"></param>
        /// <returns></returns>
        public static List<Model.NDETrustItem> GetBatchTrustDetail(string trustBatchId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var getDataLists = (from x in db.HJGL_Batch_BatchTrustItem
                                    join y in db.HJGL_WeldJoint on x.WeldJointId equals y.WeldJointId
                                    where x.TrustBatchId == trustBatchId
                                    orderby y.WeldJointCode
                                    select new Model.NDETrustItem
                                    {
                                        WeldJointCode = y.WeldJointCode,
                                        PipelineCode = y.PipelineCode,
                                        JointArea = y.JointArea,
                                    }).ToList();

                return getDataLists;
            }
        }
        #endregion

        #region 对所选委托单进行审核
        /// <summary>
        /// 对所选委托单进行审核
        /// </summary>
        /// <param name="trustBatchId"></param>
        public static void BatchTrustAudit(string trustBatchId)
        {
            BLL.Batch_BatchTrustService.UpdateBatchTrustAudit(trustBatchId, true);
        }
        #endregion

        ///////////////////////////////////////无损检测单////////////////////////////////////////////
        #region 无损检测单
        /// <summary>
        /// 选择单位工程、探伤类型、检测单号获取检测单
        /// </summary>
        /// <param name="unitWorkId"></param>
        /// <param name="detectionTypeId"></param>
        /// <param name="ndeCode"></param>
        /// <returns></returns>
        public static List<Model.BaseInfoItem> getBatchNdeCode(string unitWorkId, string detectionTypeId, string ndeCode)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var dataList = from x in db.HJGL_Batch_NDE
                               join y in db.HJGL_Batch_BatchTrust on x.TrustBatchId equals y.TrustBatchId
                               where x.UnitWorkId == unitWorkId
                               select new Model.BaseInfoItem
                               {
                                   BaseInfoId = x.NDEID,
                                   BaseInfoCode = x.NDECode,
                                   BaseInfoName = y.DetectionTypeId
                               };

                if (!string.IsNullOrEmpty(detectionTypeId))
                {
                    dataList = dataList.Where(e => e.BaseInfoName == detectionTypeId);
                }

                if (!string.IsNullOrEmpty(ndeCode))
                {
                    dataList = dataList.Where(e => e.BaseInfoCode.Contains(ndeCode));
                }

                var getDataLists = (from x in dataList
                                    orderby x.BaseInfoCode
                                    select new Model.BaseInfoItem
                                    {
                                        BaseInfoId = x.BaseInfoId,
                                        BaseInfoCode = x.BaseInfoCode,
                                    }).ToList();
                return getDataLists;
            }
        }

        /// <summary>
        /// 根据检测单ID获取检测单明细
        /// </summary>
        /// <param name="ndeId"></param>
        /// <returns></returns>
        public static List<Model.NDETrustItem> GetBatchNDEDetail(string ndeId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var getDataLists = (from x in db.HJGL_Batch_NDEItem
                                    join y in db.HJGL_Batch_BatchTrustItem on x.TrustBatchItemId equals y.TrustBatchItemId
                                    join z in db.HJGL_WeldJoint on y.WeldJointId equals z.WeldJointId
                                    where x.NDEID == ndeId
                                    orderby z.WeldJointCode
                                    select new Model.NDETrustItem
                                    {
                                        WeldJointCode = z.WeldJointCode,
                                        PipelineCode = z.PipelineCode,
                                        JointArea = z.JointArea,
                                        CheckResult = x.CheckResult == "1" ? "合格" : "不合格"
                                    }).ToList();

                return getDataLists;
            }
        }
        #endregion

        //////////////////////////////////////返修/扩透/////////////////////////////////////

        #region 返修/扩透
        /// <summary>
        /// 获取返修单列表
        /// </summary>
        /// <param name="unitWorkId"></param>
        /// <param name="isAudit"></param>
        /// <param name="repairRecordId"></param>
        /// <returns></returns>
        public static List<Model.BaseInfoItem> GetRepairRecord(string unitWorkId, bool isAudit, string repairRecordCode)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var repair = (from x in db.HJGL_RepairRecord
                              where x.UnitWorkId == unitWorkId
                              orderby x.RepairRecordCode descending
                              select x).ToList();
                if (isAudit)
                {
                    repair = (from x in repair where x.AuditDate.HasValue select x).ToList();
                }
                else
                {
                    repair = (from x in repair where !x.AuditDate.HasValue select x).ToList();
                }

                if (!string.IsNullOrEmpty(repairRecordCode))
                {
                    repair = (from x in repair where x.RepairRecordCode.Contains(repairRecordCode) select x).ToList();
                }

                var getDataLists = (from x in repair
                                    select new Model.BaseInfoItem
                                    {
                                        BaseInfoId = x.RepairRecordId,
                                        BaseInfoCode = x.RepairRecordCode
                                    }).ToList();
                return getDataLists;
            }
        }

        /// <summary>
        /// 获取返修单信息
        /// </summary>
        /// <param name="repairRecordId"></param>
        /// <returns></returns>
        public static Model.WeldJointItem GetRepairInfoByRepairRecordId(string repairRecordId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var getDataLists = (from x in db.HJGL_RepairRecord
                                    join y in db.HJGL_WeldJoint on x.WeldJointId equals y.WeldJointId
                                    join z in db.SitePerson_Person on y.BackingWelderId equals z.PersonId
                                    where x.RepairRecordId == repairRecordId
                                    select new Model.WeldJointItem
                                    {
                                        PipelineCode = y.PipelineCode,
                                        WeldJointCode = y.WeldJointCode,
                                        BackingWelderCode = z.WelderCode,
                                        BackingWelderId = y.BackingWelderId
                                    }).FirstOrDefault();
                return getDataLists;
            }
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
        public static List<Model.NDETrustItem> GetRepairExpDetail(string repairRecordId, bool welder, bool pipeLine, bool daily, bool repairBefore, bool mat, bool spec)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var record = BLL.RepairRecordService.GetRepairRecordById(repairRecordId);
                var jot = BLL.WeldJointService.GetViewWeldJointById(record.WeldJointId);
                var day = BLL.WeldingDailyService.GetPipeline_WeldingDailyByWeldingDailyId(jot.WeldingDailyId);

                var repairExp = from x in db.HJGL_Batch_PointBatchItem
                                join z in db.HJGL_Batch_PointBatch on x.PointBatchId equals z.PointBatchId
                                join y in db.HJGL_WeldJoint on x.WeldJointId equals y.WeldJointId
                                join d in db.HJGL_WeldingDaily on y.WeldingDailyId equals d.WeldingDailyId
                                where z.DetectionTypeId == record.DetectionTypeId
                                 && !x.PointDate.HasValue || (x.PointDate.HasValue && x.RepairRecordId == repairRecordId)
                                select new
                                {
                                    x.PointBatchItemId,
                                    x.PointState,
                                    y.WeldJointCode,
                                    y.PipelineId,
                                    y.PipelineCode,
                                    y.JointArea,
                                    y.BackingWelderId,
                                    y.Material1Id,
                                    y.Specification,
                                    d.WeldingDate
                                };

                if (welder)
                {
                    repairExp = repairExp.Where(x => x.BackingWelderId == jot.BackingWelderId);
                }
                if (pipeLine)
                {
                    repairExp = repairExp.Where(x => x.PipelineId == jot.PipelineId);
                }
                if (daily)
                {
                    repairExp = repairExp.Where(x => x.WeldingDate == day.WeldingDate);
                }
                if (repairBefore)
                {
                    repairExp = repairExp.Where(x => x.WeldingDate <= day.WeldingDate);
                }
                if (mat)
                {
                    repairExp = repairExp.Where(x => x.Material1Id == jot.Material1Id);
                }
                if (spec)
                {
                    repairExp = repairExp.Where(x => x.Specification == jot.Specification);
                }

                var getDataLists = (from x in repairExp
                                    select new Model.NDETrustItem
                                    {
                                        PointBatchItemId = x.PointBatchItemId,
                                        PointState=x.PointState,
                                        WeldJointCode = x.WeldJointCode,
                                        PipelineCode = x.PipelineCode,
                                        JointArea = x.JointArea
                                    }).ToList();
                return getDataLists;
            }
        }

        /// <summary>
        /// 保存返修/扩透口信息
        /// </summary>
        /// <param name="repairRecordId">返修ID</param>
        /// <param name="expandId">扩透口ID（多个用“,”号隔开）</param>
        /// <param name="repairWelder">返修焊工</param>
        /// <param name="repairDate">返修日期</param>
        /// <param name="isCut">是否切除</param>
        public static void GetRepairExpSaveInfo(string repairRecordId, string expandId, string repairWelder, string repairDate, bool isCut)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                Model.HJGL_RepairRecord repairRecord = BLL.RepairRecordService.GetRepairRecordById(repairRecordId);
                // 更新返修记录
                var repair = db.HJGL_RepairRecord.FirstOrDefault(x => x.RepairRecordId == repairRecordId);
                if (repair != null)
                {
                    repair.RepairWelder = repairWelder;
                    repair.RepairDate = Convert.ToDateTime(repairDate);
                    if (isCut)
                    {
                        repair.IsCut = true;
                    }
                }

                // 更新返修口
                var batchItem = db.HJGL_Batch_PointBatchItem.FirstOrDefault(x => x.WeldJointId == repairRecord.WeldJointId);
                if (batchItem != null)
                {
                    batchItem.RepairDate = Convert.ToDateTime(repairDate);
                    if (isCut)
                    {
                        batchItem.CutDate = DateTime.Now.Date;
                    }
                }
                db.SubmitChanges();

                var exp = BLL.RepairRecordService.GetExportItem(repairRecordId);
                if (exp != null)
                {
                    foreach (Model.HJGL_Batch_PointBatchItem item in exp)
                    {
                        Model.HJGL_Batch_PointBatchItem newPointBatchItem = db.HJGL_Batch_PointBatchItem.FirstOrDefault(x => x.PointBatchItemId == item.PointBatchItemId);
                        newPointBatchItem.PointState = null;
                        newPointBatchItem.PointDate = null;
                        newPointBatchItem.RepairRecordId = null;
                        db.SubmitChanges();
                    }
                }
                // 更新扩透口
                string[] checkedRow = expandId.Split(',');
                if (checkedRow.Count() > 0)
                {
                    foreach (string item in checkedRow)
                    {
                        Model.HJGL_Batch_PointBatchItem newPointBatchItem = db.HJGL_Batch_PointBatchItem.FirstOrDefault(x => x.PointBatchItemId == item);
                        if (newPointBatchItem != null)
                        {
                            newPointBatchItem.PointState = "2";
                            newPointBatchItem.PointDate = DateTime.Now;
                            newPointBatchItem.RepairRecordId = repairRecordId;
                            db.SubmitChanges();
                        }
                    }
                }
            }
        }
        #endregion

        #region 返修单审核
        /// <summary>
        /// 返修单审核
        /// </summary>
        /// <param name="repairRecordId"></param>
        public static void RepairAudit(string repairRecordId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                // 更新返修记录
                var repair = db.HJGL_RepairRecord.FirstOrDefault(x => x.RepairRecordId == repairRecordId);
                if (!repair.AuditDate.HasValue)
                {
                    repair.AuditDate = DateTime.Now;
                    db.SubmitChanges();
                }
            }
        }
        #endregion

        #region 生成返修委托单
        /// <summary>
        /// 生成返修委托单
        /// </summary>
        /// <param name="repairRecordId"></param>
        /// <returns></returns>
        public static string GenerateRepairTrust(string repairRecordId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                string submitStr = string.Empty;
                Model.HJGL_RepairRecord repairRecord = BLL.RepairRecordService.GetRepairRecordById(repairRecordId);
                var trustItem = from x in Funs.DB.HJGL_Batch_BatchTrustItem where x.RepairRecordId == repairRecordId select x;
                if (trustItem.Count() == 0)
                {
                    if (!string.IsNullOrEmpty(repairRecordId) && repairRecord.AuditDate.HasValue)
                    {
                        // 返修委托
                        Model.HJGL_Batch_BatchTrust newRepairTrust = new Model.HJGL_Batch_BatchTrust();
                        string trustBatchId = SQLHelper.GetNewID(typeof(Model.HJGL_Batch_BatchTrust));
                        newRepairTrust.TrustBatchId = trustBatchId;
                        newRepairTrust.TrustBatchCode = repairRecord.RepairRecordCode;
                        newRepairTrust.TrustDate = DateTime.Now;
                        newRepairTrust.ProjectId = repairRecord.ProjectId;
                        newRepairTrust.UnitId = repairRecord.UnitId;
                        newRepairTrust.UnitWorkId = repairRecord.UnitWorkId;
                        newRepairTrust.DetectionTypeId = repairRecord.DetectionTypeId;

                        BLL.Batch_BatchTrustService.AddBatchTrust(newRepairTrust);  // 新增返修委托单

                        Model.HJGL_Batch_BatchTrustItem newRepairTrustItem = new Model.HJGL_Batch_BatchTrustItem();
                        newRepairTrustItem.TrustBatchItemId = SQLHelper.GetNewID(typeof(Model.HJGL_Batch_BatchTrustItem));
                        newRepairTrustItem.TrustBatchId = trustBatchId;
                        newRepairTrustItem.RepairRecordId = repairRecordId;
                        newRepairTrustItem.WeldJointId = repairRecord.WeldJointId;
                        newRepairTrustItem.CreateDate = DateTime.Now;
                        Batch_BatchTrustItemService.AddBatchTrustItem(newRepairTrustItem);

                        // 扩透委托
                        var exp = BLL.RepairRecordService.GetExportItem(repairRecordId);
                        if (exp != null)
                        {
                            foreach (var q in exp)
                            {
                                Model.HJGL_Batch_BatchTrustItem newExportTrustItem = new Model.HJGL_Batch_BatchTrustItem();
                                newExportTrustItem.TrustBatchItemId = SQLHelper.GetNewID(typeof(Model.HJGL_Batch_BatchTrustItem));
                                newExportTrustItem.TrustBatchId = trustBatchId;
                                newExportTrustItem.PointBatchItemId = q.PointBatchItemId;
                                newExportTrustItem.WeldJointId = q.WeldJointId;
                                newExportTrustItem.CreateDate = DateTime.Now;
                                Batch_BatchTrustItemService.AddBatchTrustItem(newExportTrustItem);

                                Model.HJGL_Batch_PointBatchItem pointBatchItem = db.HJGL_Batch_PointBatchItem.FirstOrDefault(x => x.PointBatchItemId == q.PointBatchItemId);
                                pointBatchItem.IsBuildTrust = true;
                                db.SubmitChanges();
                            }
                        }

                        submitStr = "成功生成委托单！";
                    }
                    else
                    {
                        submitStr = "选中返修单并确认已审核！";
                    }
                }
                else
                {
                    submitStr = "已生成委托单！";
                }

                return submitStr;
            }
        }
        #endregion

        #region 获取扩透口的随机数
        /// <summary>
        /// 获取扩透口的随机数
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static string RandomExport(int num)
        {
            string strNum = string.Empty;
            if (num > 0 && num <= 2)
            {
                if (num == 1)
                {
                    strNum = "0";
                }
                else
                {
                    strNum = "0,1";
                }
            }
            else
            {
                int[] r = Funs.GetRandomNum(2, 0, num - 1);
                strNum = r[0].ToString() + "," + r[1].ToString();
            }

            return strNum;

        }
        #endregion

        //////////////////////////////////////////// NDE预警//////////////////////////////////////
        
        #region NDE预警
        /// <summary>
        /// 无损检测不合格焊口信息
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public static List<Model.BaseInfoItem> GetNdeCheckNoPassWarn(string projectId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var onecheckNoPass = (from x in db.HJGL_Batch_NDEItem
                                      join y in db.HJGL_Batch_BatchTrustItem on x.TrustBatchItemId equals y.TrustBatchItemId
                                      join w in db.HJGL_WeldJoint on y.WeldJointId equals w.WeldJointId
                                      join z in db.HJGL_Batch_NDE on x.NDEID equals z.NDEID
                                      where y.RepairRecordId == null && x.CheckResult == "2"
                                      select new Model.BaseInfoItem
                                      {
                                          BaseInfoId = y.WeldJointId,
                                          BaseInfoCode = "检测单：" + z.NDECode,
                                          BaseInfoName = "不合格焊口：" + w.WeldJointCode
                                      }).ToList();

                var repairPass = (from x in db.HJGL_Batch_NDEItem
                                  join y in db.HJGL_Batch_BatchTrustItem on x.TrustBatchItemId equals y.TrustBatchItemId
                                  join w in db.HJGL_WeldJoint on y.WeldJointId equals w.WeldJointId
                                  join z in db.HJGL_Batch_NDE on x.NDEID equals z.NDEID
                                  where y.RepairRecordId != null && x.CheckResult == "1"
                                  select new Model.BaseInfoItem
                                  {
                                      BaseInfoId = y.WeldJointId,
                                      BaseInfoCode = "检测单：" + z.NDECode,
                                      BaseInfoName = "不合格焊口：" + w.WeldJointCode
                                  }).ToList();

                List<Model.BaseInfoItem> getDataLists = new List<Model.BaseInfoItem>();
                foreach (var q in onecheckNoPass)
                {
                    if (!repairPass.Contains(q))
                    {
                        getDataLists.Add(q);
                    }
                }

                return getDataLists;
            }
        }

        /// <summary>
        /// 未委托焊口预警
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public static List<Model.BaseInfoItem> GetNoTrustJointWarn(string projectId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var point = (from x in db.HJGL_Batch_PointBatchItem
                             join y in db.HJGL_Batch_PointBatch on x.PointBatchId equals y.PointBatchId
                             join z in db.HJGL_WeldJoint on x.WeldJointId equals z.WeldJointId
                             join t in db.HJGL_Batch_BatchTrustItem on x.PointBatchItemId equals t.PointBatchItemId
                             where y.ProjectId == projectId && t.TrustBatchItemId == null
                             orderby y.PointBatchCode, z.WeldJointCode
                             select new Model.BaseInfoItem
                             {
                                 BaseInfoId = x.WeldJointId,
                                 BaseInfoCode ="批号：" + y.PointBatchCode,
                                 BaseInfoName =  "未委托焊口：" + z.WeldJointCode,
                             }).ToList();

                var repair = (from x in db.HJGL_RepairRecord
                              join z in db.HJGL_WeldJoint on x.WeldJointId equals z.WeldJointId
                              join t in db.HJGL_Batch_BatchTrustItem on x.RepairRecordId equals t.RepairRecordId
                              where x.ProjectId == projectId && t.TrustBatchItemId == null
                              orderby x.RepairRecordCode, z.WeldJointCode
                              select new Model.BaseInfoItem
                              {
                                  BaseInfoId = x.WeldJointId,
                                  BaseInfoCode = "返修单号：" + x.RepairRecordCode,
                                  BaseInfoName = "未委托焊口：" + z.WeldJointCode,
                              }).ToList();

                return point.Concat(repair).ToList<Model.BaseInfoItem>();
            }
        }

        /// <summary>
        /// 未检测焊口预警
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public static List<Model.BaseInfoItem> GetNoCheckJointWarn(string projectId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var getDataLists = (from x in db.HJGL_Batch_BatchTrustItem
                                    join y in db.HJGL_Batch_BatchTrust on x.TrustBatchId equals y.TrustBatchId
                                    join z in db.HJGL_WeldJoint on x.WeldJointId equals z.WeldJointId
                                    join n in db.HJGL_Batch_NDEItem on x.TrustBatchItemId equals n.TrustBatchItemId
                                    where y.ProjectId == projectId && n.NDEItemID == null
                                    orderby y.TrustBatchCode, z.WeldJointCode
                                    select new Model.BaseInfoItem
                                    {
                                        BaseInfoId = x.WeldJointId,
                                        BaseInfoCode ="委托单号：" + y.TrustBatchCode,
                                        BaseInfoName = "焊口：" + z.WeldJointCode,
                                    }).ToList();

                return getDataLists;
            }
        }

        #endregion

    }
}
