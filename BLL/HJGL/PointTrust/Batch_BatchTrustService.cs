using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 无损委托
    /// </summary>
    public static class Batch_BatchTrustService
    {        
        /// <summary>
        /// 根据主键获取无损委托
        /// </summary>
        /// <param name="batchTrustID"></param>
        /// <returns></returns>
        public static Model.HJGL_Batch_BatchTrust GetBatchTrustById(string trustBatchId)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            return db.HJGL_Batch_BatchTrust.FirstOrDefault(e => e.TrustBatchId == trustBatchId);
        }

        /// <summary>
        /// 根据主键获取无损委托视图
        /// </summary>
        /// <param name="batchTrustID"></param>
        /// <returns></returns>
        public static Model.View_Batch_BatchTrust GetBatchTrustViewById(string trustBatchId)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            return db.View_Batch_BatchTrust.FirstOrDefault(e => e.TrustBatchId == trustBatchId);
        }

        #region 更新委托单 检测状态
        /// <summary>
        /// 更新委托单 检测状态
        /// </summary>
        /// <param name="pointBatchId"></param>
        /// <param name="isTrust"></param>
        public static void UpdatTrustBatchtState(string trustBatchId, bool? isCheck)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.HJGL_Batch_BatchTrust update = db.HJGL_Batch_BatchTrust.FirstOrDefault(e => e.TrustBatchId == trustBatchId);
            if (update != null)
            {
                update.IsCheck = isCheck;
                db.SubmitChanges();
            }
        }
        #endregion

        /// <summary>
        /// 添加无损委托
        /// </summary>
        /// <param name="batchTrust"></param>
        public static void AddBatchTrust(Model.HJGL_Batch_BatchTrust batchTrust)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.HJGL_Batch_BatchTrust newBatchTrust = new Model.HJGL_Batch_BatchTrust
            {
                TrustBatchId = batchTrust.TrustBatchId,
                TrustBatchCode = batchTrust.TrustBatchCode,
                TrustDate = batchTrust.TrustDate,
                ProjectId = batchTrust.ProjectId,
                UnitId = batchTrust.UnitId,
                UnitWorkId = batchTrust.UnitWorkId,
                DetectionRateId = batchTrust.DetectionRateId,
                NDEUuit=batchTrust.NDEUuit,
                TrustType = batchTrust.TrustType,
                DetectionTypeId = batchTrust.DetectionTypeId,
                IsCheck = batchTrust.IsCheck,
                TopointBatch = batchTrust.TopointBatch
            };

            db.HJGL_Batch_BatchTrust.InsertOnSubmit(newBatchTrust);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改无损委托
        /// </summary>
        /// <param name="batchTrust"></param>
        public static void UpdateBatchTrust(Model.HJGL_Batch_BatchTrust batchTrust)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.HJGL_Batch_BatchTrust newBatchTrust = db.HJGL_Batch_BatchTrust.FirstOrDefault(e => e.TrustBatchId == batchTrust.TrustBatchId);
            if (newBatchTrust != null)
            {
                newBatchTrust.TrustBatchCode = batchTrust.TrustBatchCode;
                newBatchTrust.TrustDate = batchTrust.TrustDate;
                newBatchTrust.ProjectId = batchTrust.ProjectId;
                newBatchTrust.UnitId = batchTrust.UnitId;
                newBatchTrust.UnitWorkId = batchTrust.UnitWorkId;
                newBatchTrust.DetectionRateId = batchTrust.DetectionRateId;
                newBatchTrust.NDEUuit = batchTrust.NDEUuit;
                newBatchTrust.TrustType = batchTrust.TrustType;
                newBatchTrust.DetectionTypeId = batchTrust.DetectionTypeId;
                newBatchTrust.IsCheck = batchTrust.IsCheck;
                newBatchTrust.TopointBatch = batchTrust.TopointBatch;
                db.SubmitChanges();
            }
        }

        public static void UpdateBatchTrustAudit(string batchTrustId, bool isAudit)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.HJGL_Batch_BatchTrust newBatchTrust = db.HJGL_Batch_BatchTrust.FirstOrDefault(e => e.TrustBatchId == batchTrustId);
            if (newBatchTrust != null)
            {
                newBatchTrust.IsAudit = isAudit;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除无损委托
        /// </summary>
        /// <param name="batchTrustID"></param>
        public static void DeleteBatchTrustById(string trustBatchId)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.HJGL_Batch_BatchTrust batchTrust = db.HJGL_Batch_BatchTrust.FirstOrDefault(e => e.TrustBatchId == trustBatchId);
            if (batchTrust != null)
            {
                db.HJGL_Batch_BatchTrust.DeleteOnSubmit(batchTrust);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 无损委托委托单编号是否存在
        /// </summary>
        /// <param name="pointNo"></param>
        /// <param name="pointId"></param>
        /// <returns></returns>
        public static bool IsExistTrustCode(string trustBatchCode, string trustBatchId, string projectId)
        {
            var q = new Model.SGGLDB(Funs.ConnString).HJGL_Batch_BatchTrust.FirstOrDefault(x => x.TrustBatchCode == trustBatchCode && x.ProjectId == projectId && x.TrustBatchId != trustBatchId);
            if (q != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #region 委托单下拉项
        /// <summary>
        /// 委托单下拉项
        /// </summary>
        /// <param name="dropName">下拉框名称</param>
        /// <param name="isShowPlease">是否显示请选择</param>
        /// <param name="ComponentsType">耗材类型</param>
        public static void InitTrustBatchDropDownList(FineUIPro.DropDownList dropName, bool isShowPlease, string unitId, string detectionTypeId, string pipelineCode, string itemText)
        {
            dropName.DataValueField = "TrustBatchId";
            dropName.DataTextField = "TrustBatchCode";

            var q = from x in new Model.SGGLDB(Funs.ConnString).View_Batch_BatchTrust
                    where x.UnitId == unitId  && x.DetectionTypeId == detectionTypeId
                          && x.CheckTrustBatchId == null
                    select x;       // 管线TODO

            dropName.DataSource = q;
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName, itemText);
            }
        }
        #endregion
    }
}
