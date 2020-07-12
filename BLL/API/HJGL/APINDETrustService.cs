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
        /// <param name="startDate"></param>
        /// <returns></returns>
        public static List<Model.NDETrustItem> getPointBatchCode(string unitWorkId, string detectionTypeId, string detectionRateId, string pointBatchCode)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var dataList = from x in db.HJGL_Batch_PointBatch
                               join y in db.HJGL_Batch_PointBatchItem on x.PointBatchId equals y.PointBatchId
                               where x.UnitWorkId == unitWorkId && y.IsBuildTrust == null && x.EndDate.HasValue
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
                var getDataLists = (from x in db.HJGL_Batch_PointBatchItem
                                    join y in db.HJGL_WeldJoint on x.WeldJointId equals y.WeldJointId
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
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
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

                db.SubmitChanges();
            }
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
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
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
        public static List<Model.BaseInfoItem> getBatchTrustCode(string unitWorkId, string detectionTypeId, string detectionRateId, bool? isAudit, string trustBatchCode)
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
    }
}
