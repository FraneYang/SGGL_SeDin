using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace BLL
{
    public class PointBatchDetailService
    {
        /// <summary>
        /// 根据批明细ID获取批明细信息
        /// </summary>
        /// <param name="batchId"></param>
        /// <returns></returns>
        public static Model.HJGL_Batch_PointBatchItem GetBatchDetailById(string batchDetailId)
        {
            return Funs.DB.HJGL_Batch_PointBatchItem.FirstOrDefault(e => e.PointBatchItemId == batchDetailId);
        }

        /// <summary>
        /// 根据批焊口ID获取批明细信息
        /// </summary>
        /// <param name="jotId"></param>
        /// <returns></returns>
        public static Model.HJGL_Batch_PointBatchItem GetBatchDetailByJotId(string jotId)
        {
            return Funs.DB.HJGL_Batch_PointBatchItem.FirstOrDefault(e => e.WeldJointId == jotId);
        }

        public static List<Model.HJGL_Batch_PointBatchItem> GetBatchDetailByBatchId(string batchId)
        {
            return Funs.DB.HJGL_Batch_PointBatchItem.Where(e => e.PointBatchId == batchId).ToList();
        }

        public static List<Model.HJGL_Batch_PointBatchItem> GetGBatchDetailByBatchId(string batchId)
        {
            return (from x in Funs.DB.HJGL_Batch_PointBatchItem
                    join y in Funs.DB.HJGL_WeldJoint
                    on x.WeldJointId equals y.WeldJointId
                    where y.WeldJointCode.Contains("G") && x.PointBatchId == batchId
                    select x).ToList();
        }


        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="batchDetail"></param>
        public static void UpdatePointBatchDetail(Model.HJGL_Batch_PointBatchItem batchDetail)
        {
            Model.HJGL_Batch_PointBatchItem newBatchDetail = Funs.DB.HJGL_Batch_PointBatchItem.FirstOrDefault(e => e.PointBatchItemId == batchDetail.PointBatchItemId);
            if (newBatchDetail != null)
            {
                newBatchDetail.PointBatchId = batchDetail.PointBatchId;
                newBatchDetail.WeldJointId = batchDetail.WeldJointId;
                newBatchDetail.PointState = batchDetail.PointState;
                newBatchDetail.PointDate = batchDetail.PointDate;
                newBatchDetail.PointDate = batchDetail.PointDate;
                newBatchDetail.RepairDate = batchDetail.RepairDate;
                newBatchDetail.CutDate = batchDetail.CutDate;
                newBatchDetail.CreatDate = batchDetail.CreatDate;
                newBatchDetail.IsBuildTrust = batchDetail.IsBuildTrust;
                newBatchDetail.IsWelderFirst = batchDetail.IsWelderFirst;
                newBatchDetail.IsPipelineFirst = batchDetail.IsPipelineFirst;
                newBatchDetail.Remark = batchDetail.Remark;

                Funs.DB.SubmitChanges();
            }
        }

        public static void UpdatePointBatchDetail(string pointBatchItemId, string pointState, DateTime? pointDate)
        {
            Model.HJGL_Batch_PointBatchItem newBatchDetail = Funs.DB.HJGL_Batch_PointBatchItem.FirstOrDefault(e => e.PointBatchItemId == pointBatchItemId);
            if (newBatchDetail != null)
            {
                newBatchDetail.PointState = pointState;
                newBatchDetail.PointDate = pointDate;
                Funs.DB.SubmitChanges();
            }
        }

        #region 点口审核
        /// <summary>
        /// 点口审核
        /// </summary>
        /// <param name="pointBatchItemId"></param>
        /// <param name="isAudit"></param>
        public static void PointAudit(string pointBatchItemId, bool isAudit)
        {
            Model.SGGLDB db = Funs.DB;
            Model.HJGL_Batch_PointBatchItem newPointBatchItem = db.HJGL_Batch_PointBatchItem.FirstOrDefault(e => e.PointBatchItemId == pointBatchItemId);
            if (newPointBatchItem != null)
            {
                newPointBatchItem.IsAudit = isAudit;
                db.SubmitChanges();
            }
        }
        #endregion


        #region 首件制
        /// <summary>
        /// 焊工首两件
        /// </summary>
        /// <param name="pointBatchItemId"></param>
        /// <param name="welderFirst"></param>
        public static void UpdateWelderFirst(string pointBatchItemId, bool? welderFirst)
        {
            Model.SGGLDB db = Funs.DB;
            Model.HJGL_Batch_PointBatchItem newPointBatchItem = db.HJGL_Batch_PointBatchItem.FirstOrDefault(e => e.PointBatchItemId == pointBatchItemId);
            newPointBatchItem.IsWelderFirst = welderFirst;

            db.SubmitChanges();
        }

        /// <summary>
        /// 管线首件
        /// </summary>
        /// <param name="pointBatchItemId"></param>
        /// <param name="pipelineFirst"></param>
        public static void UpdatePipelineFirst(string pointBatchItemId, bool? pipelineFirst)
        {
            Model.SGGLDB db = Funs.DB;
            Model.HJGL_Batch_PointBatchItem newPointBatchItem = db.HJGL_Batch_PointBatchItem.FirstOrDefault(e => e.PointBatchItemId == pointBatchItemId);
            newPointBatchItem.IsPipelineFirst = pipelineFirst;

            db.SubmitChanges();
        }
        #endregion

        /// <summary>
        /// 根据批焊口ID删除批明细信息
        /// </summary>
        /// <param name="checkId"></param>
        public static void DeleteBatchDetail(string jotId)
        {
            Model.SGGLDB db = Funs.DB;
            Model.HJGL_Batch_PointBatchItem batch = db.HJGL_Batch_PointBatchItem.FirstOrDefault(e => e.WeldJointId == jotId);
            if (batch != null)
            {
                db.HJGL_Batch_PointBatchItem.DeleteOnSubmit(batch);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="batchDetail"></param>
        public static void AddBatchDetail(Model.HJGL_Batch_PointBatchItem batchDetail)
        {
            Model.HJGL_Batch_PointBatchItem newBatchDetail = new Model.HJGL_Batch_PointBatchItem();
            newBatchDetail.PointBatchItemId = batchDetail.PointBatchItemId;
            newBatchDetail.PointBatchId = batchDetail.PointBatchId;
            newBatchDetail.WeldJointId = batchDetail.WeldJointId;
            newBatchDetail.PointState = batchDetail.PointState;
            newBatchDetail.PointDate = batchDetail.PointDate;
            newBatchDetail.PointDate = batchDetail.PointDate;
            newBatchDetail.RepairDate = batchDetail.RepairDate;
            newBatchDetail.CutDate = batchDetail.CutDate;
            newBatchDetail.CreatDate = batchDetail.CreatDate;
            newBatchDetail.IsBuildTrust = batchDetail.IsBuildTrust;
            newBatchDetail.IsWelderFirst = batchDetail.IsWelderFirst;
            newBatchDetail.IsPipelineFirst = batchDetail.IsPipelineFirst;
            newBatchDetail.Remark = batchDetail.Remark;
            Funs.DB.HJGL_Batch_PointBatchItem.InsertOnSubmit(newBatchDetail);
            Funs.DB.SubmitChanges();
        }

        /// <summary>
        /// 根据主键删除明细
        /// </summary>
        /// <param name="batchDetailId"></param>
        public static void DeleteBatchDetailById(string batchDetailId)
        {
            Model.SGGLDB db = Funs.DB;
            Model.HJGL_Batch_PointBatchItem batch = db.HJGL_Batch_PointBatchItem.FirstOrDefault(e => e.PointBatchItemId == batchDetailId);
            if (batch != null)
            {
                db.HJGL_Batch_PointBatchItem.DeleteOnSubmit(batch);
                db.SubmitChanges();
            }
        }
        /// <summary>
        /// 检验批自动点口算法
        /// </summary>
        /// <param name="pointBatchId"></param>
        public static void AutoPoint(string pointBatchId)
        {
            int pointNum = 0;
            int pointNumG = 0;
            int pointNumA = 0;

            var batch = PointBatchService.GetPointBatchById(pointBatchId);
            var batchItemNum = PointBatchDetailService.GetBatchDetailByBatchId(pointBatchId);

            if (batch.DetectionRateId != null && batchItemNum.Count() > 0)
            {
                var rate = Base_DetectionRateService.GetDetectionRateByDetectionRateId(batch.DetectionRateId);
                // 批里要检测的数量
                pointNum = Convert.ToInt32(Math.Ceiling((batchItemNum.Count() * rate.DetectionRateValue.Value) * 0.01));
            }

            var weldG = from x in Funs.DB.HJGL_Batch_PointBatchItem
                        join y in Funs.DB.HJGL_WeldJoint on x.WeldJointId equals y.WeldJointId
                        where y.JointAttribute == "固定口"
                        select x;
            var weldA = from x in Funs.DB.HJGL_Batch_PointBatchItem
                        join y in Funs.DB.HJGL_WeldJoint on x.WeldJointId equals y.WeldJointId
                        where y.JointAttribute == "活动口"
                        select x;
            if (weldG.Count() > 0)
            {
                // 固定口检测数量
                pointNumG = Convert.ToInt32(Math.Ceiling(weldG.Count() * 0.4));
            }

            // 活动口要检测的数量
            pointNumA = pointNum - pointNumG;

            if (pointNumG > 0)
            {
                int[] r = Funs.GetRandomNum(pointNumG, 1, weldG.Count());
                int i = 0;
                foreach (var p in weldG)
                {
                    i++;
                    if (r.Contains(i))
                    {
                        PointBatchDetailService.UpdatePointBatchDetail(p.PointBatchItemId, "1", System.DateTime.Now);
                    }
                }
            }

            if (pointNumA > 0)
            {
                int[] r = Funs.GetRandomNum(pointNumA, 1, weldA.Count());
                int j = 0;
                foreach (var p in weldA)
                {
                    j++;
                    if (r.Contains(j))
                    {
                        PointBatchDetailService.UpdatePointBatchDetail(p.PointBatchItemId, "1", System.DateTime.Now);
                    }
                }
            }

            PointBatchService.UpdateBatchIsClosed(pointBatchId, DateTime.Now);
        }

    }
}
