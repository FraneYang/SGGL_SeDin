using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 检测单明细
    /// </summary>
    public static class Batch_NDEItemService
    {
        /// <summary>
        /// 根据主键获取检测单明细
        /// </summary>
        /// <param name="NDEItemId"></param>
        /// <returns></returns>
        public static Model.HJGL_Batch_NDEItem GetNDEItemById(string ndeItemID)
        {
            return Funs.DB.HJGL_Batch_NDEItem.FirstOrDefault(e => e.NDEItemID == ndeItemID);
        }

        /// <summary>
        /// 根据主键获取检测单明细视图信息
        /// </summary>
        /// <param name="ndeItemID"></param>
        /// <returns></returns>
        public static Model.View_Batch_NDEItem GetNDEItemViewById (string ndeItemID)
        {
            return Funs.DB.View_Batch_NDEItem.FirstOrDefault(e => e.NDEItemID == ndeItemID);
        }

        /// <summary>
        /// 根据检测单Id获取相关明细信息
        /// </summary>
        /// <param name="hardTrustId"></param>
        /// <returns></returns>
        public static List<Model.HJGL_Batch_NDEItem> GetNDEItemByNDEID(string NDEID)
        {
            return (from x in Funs.DB.HJGL_Batch_NDEItem where x.NDEID == NDEID select x).ToList();
        }

        /// <summary>
        /// 添加检测单明细
        /// </summary>
        /// <param name="NDEItem"></param>
        public static void AddNDEItem(Model.HJGL_Batch_NDEItem NDEItem)
        {
            Model.SGGLDB db = Funs.DB;
            Model.HJGL_Batch_NDEItem newNDEItem = new Model.HJGL_Batch_NDEItem();
            newNDEItem.NDEItemID = NDEItem.NDEItemID;
            newNDEItem.NDEID = NDEItem.NDEID;
            newNDEItem.TrustBatchItemId = NDEItem.TrustBatchItemId;
            newNDEItem.DetectionTypeId = NDEItem.DetectionTypeId;
            newNDEItem.RequestDate = NDEItem.RequestDate;
            newNDEItem.RepairLocation = NDEItem.RepairLocation;
            newNDEItem.TotalFilm = NDEItem.TotalFilm;
            newNDEItem.PassFilm = NDEItem.PassFilm;
            newNDEItem.CheckResult = NDEItem.CheckResult;
            newNDEItem.NDEReportNo = NDEItem.NDEReportNo;
            newNDEItem.FilmDate = NDEItem.FilmDate;
            newNDEItem.ReportDate = NDEItem.ReportDate;
            newNDEItem.SubmitDate = NDEItem.SubmitDate;
            newNDEItem.CheckDefects = NDEItem.CheckDefects;
            newNDEItem.JudgeGrade = NDEItem.JudgeGrade;
            newNDEItem.Remark = NDEItem.Remark;

            db.HJGL_Batch_NDEItem.InsertOnSubmit(newNDEItem);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改检测单
        /// </summary>
        /// <param name="NDEItem"></param>
        public static void UpdateNDEItem(Model.HJGL_Batch_NDEItem NDEItem)
        {
            Model.SGGLDB db = Funs.DB;
            Model.HJGL_Batch_NDEItem newNDEItem = db.HJGL_Batch_NDEItem.FirstOrDefault(e => e.NDEItemID == NDEItem.NDEItemID);
            if (newNDEItem != null)
            {
                newNDEItem.TrustBatchItemId = NDEItem.TrustBatchItemId;
                newNDEItem.DetectionTypeId = NDEItem.DetectionTypeId;
                newNDEItem.RequestDate = NDEItem.RequestDate;
                newNDEItem.RepairLocation = NDEItem.RepairLocation;
                newNDEItem.TotalFilm = NDEItem.TotalFilm;
                newNDEItem.PassFilm = NDEItem.PassFilm;
                newNDEItem.CheckResult = NDEItem.CheckResult;
                newNDEItem.NDEReportNo = NDEItem.NDEReportNo;
                newNDEItem.FilmDate = NDEItem.FilmDate;
                newNDEItem.ReportDate = NDEItem.ReportDate;
                newNDEItem.SubmitDate = NDEItem.SubmitDate;
                newNDEItem.CheckDefects = NDEItem.CheckDefects;
                newNDEItem.JudgeGrade = NDEItem.JudgeGrade;
                newNDEItem.Remark = NDEItem.Remark;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据检测单明细主键删除明细信息
        /// </summary>
        /// <param name="ndeItemID"></param>
        public static void DeleteNDEItemByNDEItemID(string ndeItemID)
        {
            Model.SGGLDB db = Funs.DB;
            var ndeItem = (from x in db.HJGL_Batch_NDEItem where x.NDEItemID == ndeItemID select x).FirstOrDefault();
            if (ndeItem != null)
            {
                db.HJGL_Batch_NDEItem.DeleteOnSubmit(ndeItem);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据检测单主键删除相关明细信息
        /// </summary>
        /// <param name="ndeId"></param>
        public static void DeleteNDEItemById(string ndeId)
        {
            Model.SGGLDB db = Funs.DB;
            var ndeItem = (from x in db.HJGL_Batch_NDEItem where x.NDEID == ndeId select x).ToList();
            if (ndeItem != null)
            {
                db.HJGL_Batch_NDEItem.DeleteAllOnSubmit(ndeItem);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据检测单Id获取相关明细视图信息
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="hardTrustId"></param>
        /// <returns></returns>
        public static List<Model.View_Batch_NDEItem> GetViewNDEItem(string NDEID)
        {
            return (from x in Funs.DB.View_Batch_NDEItem where x.NDEID == NDEID select x).ToList();
        }

        /// <summary>
        /// 根据焊口ID判断该焊口是否已检测并审核
        /// </summary>
        /// <param name="weldJointId">焊口ID</param>
        /// <returns>true-已审核，false-未审核</returns>
        public static bool IsCheckedByWeldJoint(string weldJointId)
        {
            Model.SGGLDB db = Funs.DB;
            bool isChecked = false;
            var ndtItemView = from x in db.View_Batch_NDEItem where x.WeldJointId == weldJointId && x.SubmitDate != null select x;
            if(ndtItemView.Count()>0)
            {
                isChecked = true;
            }
            return isChecked;
        }

        /// <summary>
        /// 根据日报ID判断该日报下是否有焊口已检测并审核
        /// </summary>
        /// <param name="weldingDailyId">日报ID</param>
        /// <returns>true-已审核，false-未审核</returns>
        public static bool IsCheckedByWeldingDaily(string weldingDailyId)
        {
            Model.SGGLDB db = Funs.DB;
            bool isChecked = false;
            var ndtItemView = from x in db.View_Batch_NDEItem where x.WeldingDailyId == weldingDailyId && x.SubmitDate != null select x;
            if (ndtItemView.Count() > 0)
            {
                isChecked = true;
            }
            return isChecked;
        }

        #region 删除焊口所在批和委托检测单里信息
        /// <summary>
        /// 删除批中焊口信息
        /// </summary>
        /// <param name="weldJointId">焊口id</param>
        public static void DeleteAllNDEInfoToWeldJoint(string weldJointId)
        {
            //Model.HJGLDB db = Funs.DB;
            //var pointBatchItems = from x in db.Batch_PointBatchItem where x.WeldJointId == weldJointId select x;

            //if (pointBatchItems.Count() > 0)
            //{
            //    foreach (var pointBatchItem in pointBatchItems)
            //    {
            //        Model.Batch_BatchTrustItem trustItem = db.Batch_BatchTrustItem.FirstOrDefault(x => x.PointBatchItemId == pointBatchItem.PointBatchItemId);
            //        string pointBatchItemId = pointBatchItem.PointBatchItemId;
            //        string pointBatchId = pointBatchItem.PointBatchId;

            //        if (trustItem != null)
            //        {
            //            Model.Batch_NDEItem checkItem = db.Batch_NDEItem.FirstOrDefault(x => x.TrustBatchItemId == trustItem.TrustBatchItemId);
            //            if (checkItem != null)
            //            {
            //                // 删除检测单里明细
            //                DeleteNDEItemByNDEItemID(checkItem.NDEItemID);
            //                var ndeItem = from y in db.Batch_NDEItem where y.NDEID == checkItem.NDEID select y;
            //                // 当检测单没有明细时，删除检测单
            //                if (ndeItem.Count() == 0)
            //                {
            //                    BLL.Batch_NDEService.DeleteNDEById(checkItem.NDEID);
            //                }
            //            }

            //            // 删除委托单里明细
            //            BLL.Batch_BatchTrustItemService.DeleteTrustItemByTrustBatchItemId(trustItem.TrustBatchItemId);
                       
            //            var t = from y in db.Batch_BatchTrustItem where y.TrustBatchId == trustItem.TrustBatchId select y;
            //            // 当委托单只有没有明细时，删除委托单
            //            if (t.Count() == 0)
            //            {
            //                BLL.Batch_BatchTrustService.DeleteBatchTrustById(trustItem.TrustBatchId);
            //            }
            //            else
            //            {   // 去掉委托单里包含的点口单
            //                string topoint = (from x in db.Batch_BatchTrust where x.TrustBatchId == trustItem.TrustBatchId select x.TopointBatch).FirstOrDefault();
            //                if (!String.IsNullOrEmpty(topoint))
            //                {
            //                    topoint = topoint.Replace(pointBatchId + ",", "").Replace("," + pointBatchId, "");
            //                    Model.Batch_BatchTrust trust = db.Batch_BatchTrust.FirstOrDefault(x => x.TrustBatchId == trustItem.TrustBatchId);
            //                    trust.TopointBatch = topoint;
            //                    db.SubmitChanges();
            //                }
            //            }
            //        }

            //        // 批明细
            //        var pointBatchItemnews = from x in db.Batch_PointBatchItem where x.PointBatchId == pointBatchId select x;
            //        int count = pointBatchItemnews.Count();
            //        BLL.Batch_PointBatchItemService.DeletePointBatchItemById(pointBatchItemId);// 删除此条明细

            //        if (count == 1) ////批明细中只有一条焊口时
            //        {
            //            BLL.Batch_PointBatchService.DeletePointBatchById(pointBatchId); // 删除批信息
            //        }

            //        else  // 批明细中存在多条时
            //        {
            //            // 所在批
            //            var pointBatch = db.Batch_PointBatch.FirstOrDefault(x => x.PointBatchId == pointBatchId && x.ClearDate == null);
            //            if (pointBatch != null)
            //            {
            //                var pointBatchItemUpdate = from x in Funs.DB.Batch_PointBatchItem
            //                                           where x.PointBatchId == pointBatchId
            //                                               && x.PointState != null && x.PointDate.HasValue
            //                                           select x;
            //                if (pointBatchItemUpdate.Count() == 0)
            //                {
            //                    //  如果批里没有口被点中，则修改主表委托状态，这样就可重新生成委托
            //                    BLL.Batch_PointBatchService.UpdatePointTrustState(pointBatch.PointBatchId, null);
            //                }
            //            }
            //        }
            //    }
            //}
        }
        #endregion
    }
}
