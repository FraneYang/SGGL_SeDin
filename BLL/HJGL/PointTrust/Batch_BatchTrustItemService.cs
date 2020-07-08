using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 无损委托明细
    /// </summary>
    public static class Batch_BatchTrustItemService
    {
        /// <summary>
        /// 根据主键获取无损委托明细
        /// </summary>
        /// <param name="batchTrustItemId"></param>
        /// <returns></returns>
        public static Model.HJGL_Batch_BatchTrustItem GetBatchTrustItemById(string trustBatchItemId)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            return db.HJGL_Batch_BatchTrustItem.FirstOrDefault(e => e.TrustBatchItemId == trustBatchItemId);
        }

        /// <summary>
        /// 根据无损委托Id获取相关明细信息
        /// </summary>
        /// <param name="hardTrustId"></param>
        /// <returns></returns>
        public static List<Model.HJGL_Batch_BatchTrustItem> GetBatchTrustItemByTrustBatchId(string trustBatchId)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            return (from x in db.HJGL_Batch_BatchTrustItem where x.TrustBatchId == trustBatchId select x).ToList();
        }

        /// <summary>
        /// 添加无损委托明细
        /// </summary>
        /// <param name="batchTrustItem"></param>
        public static void AddBatchTrustItem(Model.HJGL_Batch_BatchTrustItem batchTrustItem)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.HJGL_Batch_BatchTrustItem newTrustBatchItem = new Model.HJGL_Batch_BatchTrustItem
            {
                TrustBatchItemId = batchTrustItem.TrustBatchItemId,
                TrustBatchId = batchTrustItem.TrustBatchId,
                PointBatchItemId = batchTrustItem.PointBatchItemId,
                RepairRecordId = batchTrustItem.RepairRecordId,
                WeldJointId = batchTrustItem.WeldJointId,
                CreateDate = batchTrustItem.CreateDate,
                RepairNum = batchTrustItem.RepairNum,
            };
            db.HJGL_Batch_BatchTrustItem.InsertOnSubmit(newTrustBatchItem);
            db.SubmitChanges();
            UpdateTrustNum(batchTrustItem.TrustBatchItemId, 1);

        }


        /// <summary>
        /// 修改无损委托
        /// </summary>
        /// <param name="batchTrustItem"></param>
        public static void UpdateBatchTrustItem(Model.HJGL_Batch_BatchTrustItem batchTrustItem)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.HJGL_Batch_BatchTrustItem newBatchTrustItem = db.HJGL_Batch_BatchTrustItem.FirstOrDefault(e => e.TrustBatchItemId == batchTrustItem.TrustBatchItemId);
            if (newBatchTrustItem != null)
            {
                newBatchTrustItem.PointBatchItemId = batchTrustItem.PointBatchItemId;
                newBatchTrustItem.WeldJointId = batchTrustItem.WeldJointId;
                newBatchTrustItem.CreateDate = batchTrustItem.CreateDate;
                newBatchTrustItem.RepairNum = batchTrustItem.RepairNum;
                db.SubmitChanges();
            }
        }


        /// <summary>
        /// 根据无损委托Id获取相关明细视图信息
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="hardTrustId"></param>
        /// <returns></returns>
        public static List<Model.View_Batch_BatchTrustItem> GetViewBatchTrustItem(string trustBatchId)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            return (from x in db.View_Batch_BatchTrustItem where x.TrustBatchId == trustBatchId select x).ToList();
        }

 
	/// <summary>
        /// 根据Id删除明细信息
        /// </summary>
        /// <param name="checkItemId"></param>
        public static void DeleteTrustItemByTrustBatchItemId(string trustBatchItemId)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.HJGL_Batch_BatchTrustItem trustItem = db.HJGL_Batch_BatchTrustItem.FirstOrDefault(e => e.TrustBatchItemId == trustBatchItemId);
            if (trustItem != null)
            {
                UpdateTrustNum(trustBatchItemId, -1);
                db.HJGL_Batch_BatchTrustItem.DeleteOnSubmit(trustItem);
                db.SubmitChanges();

            }
        }

        /// <summary>
        /// 更新委托次数
        /// </summary>
        /// <param name="trustBatchItemId">委托批明细ID</param>
        /// <param name="num"></param>
        public static void UpdateTrustNum(string trustBatchItemId, int num)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            var trustBatchItem = db.HJGL_Batch_BatchTrustItem.FirstOrDefault(x => x.TrustBatchItemId == trustBatchItemId);
            if (trustBatchItem != null)
            {
                trustBatchItem.TrustNum = trustBatchItem.TrustNum ?? 0 + num;
                if (trustBatchItem.TrustNum < 0)
                {
                    trustBatchItem.TrustNum = 0;
                }
            }
            db.SubmitChanges();
        }

        #region 是否满足生成委托条件
        /// <summary>
        /// 是否满足生成委托条件
        /// </summary>
        /// <returns></returns>
        public static bool GetIsGenerateTrust(string pointBatchItemId)
        {
            bool isShow = true;
            var trustBatchItem = new Model.SGGLDB(Funs.ConnString).HJGL_Batch_BatchTrustItem.FirstOrDefault(x => x.PointBatchItemId == pointBatchItemId);
            if (trustBatchItem != null)
            {
                var checkItem = new Model.SGGLDB(Funs.ConnString).HJGL_Batch_NDEItem.FirstOrDefault(x => x.TrustBatchItemId == trustBatchItem.TrustBatchItemId && x.CheckResult == "1");
                if (checkItem != null)
                {
                    isShow = false;
                }
            }

            return isShow;
        }
        #endregion
    }
}
