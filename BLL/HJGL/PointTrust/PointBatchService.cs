using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace BLL
{
    public class PointBatchService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="batchId"></param>
        /// <returns></returns>
        public static Model.HJGL_Batch_PointBatch GetPointBatchById(string pointBatchId)
        {
            return Funs.DB.HJGL_Batch_PointBatch.FirstOrDefault(e => e.PointBatchId == pointBatchId);
        }


        /// <summary>
        /// 修改批主表批关闭状态
        /// </summary>
        /// <param name="batch"></param>
        public static void UpdateBatchIsClosed(string PointBatchId, DateTime endDate)
        {
            Model.HJGL_Batch_PointBatch newBatch = Funs.DB.HJGL_Batch_PointBatch.FirstOrDefault(e => e.PointBatchId == PointBatchId);
            if (newBatch != null)
            {
                newBatch.EndDate = endDate;
                Funs.DB.SubmitChanges();
            }
        }

        /// <summary>
        /// 修改批主表批关闭状态
        /// </summary>
        /// <param name="batch"></param>
        public static void UpdateBatchIsClosed2(string PointBatchId, DateTime endDate)
        {
            Model.HJGL_Batch_PointBatch newBatch = Funs.DB.HJGL_Batch_PointBatch.FirstOrDefault(e => e.PointBatchId == PointBatchId);
            if (newBatch != null)
            {
                newBatch.EndDate = endDate;
                newBatch.IsClosed = true;
                Funs.DB.SubmitChanges();
            }
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="check"></param>
        public static void AddPointBatch(Model.HJGL_Batch_PointBatch batch)
        {
            Model.SGGLDB db = Funs.DB;
            Model.HJGL_Batch_PointBatch newBatch = new Model.HJGL_Batch_PointBatch();
            newBatch.PointBatchId = batch.PointBatchId;
            newBatch.PointBatchCode = batch.PointBatchCode;
            newBatch.BatchCondition = batch.BatchCondition;
            newBatch.ProjectId = batch.ProjectId;
            newBatch.UnitWorkId = batch.UnitWorkId;
            newBatch.UnitId = batch.UnitId;
            newBatch.DetectionTypeId = batch.DetectionTypeId;

            newBatch.DetectionRateId = batch.DetectionRateId;
            newBatch.PipingClassId = batch.PipingClassId;
            newBatch.PipelineId = batch.PipelineId;
            newBatch.WelderId = batch.WelderId;
            newBatch.StartDate = batch.StartDate;
            
            db.HJGL_Batch_PointBatch.InsertOnSubmit(newBatch);
            db.SubmitChanges();
        }


        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="checkId"></param>
        public static void DeleteBatch(string batchId)
        {
            Model.SGGLDB db = Funs.DB;
            Model.HJGL_Batch_PointBatch batch = db.HJGL_Batch_PointBatch.FirstOrDefault(e => e.PointBatchId == batchId);

            db.HJGL_Batch_PointBatch.DeleteOnSubmit(batch);
            db.SubmitChanges();
        }

    }
}
