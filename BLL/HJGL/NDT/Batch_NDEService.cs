using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 检测单
    /// </summary>
    public static class Batch_NDEService
    {
        /// <summary>
        /// 根据主键获取检测单
        /// </summary>
        /// <param name="NDEID"></param>
        /// <returns></returns>
        public static Model.HJGL_Batch_NDE GetNDEById(string NDEID)
        {
            return Funs.DB.HJGL_Batch_NDE.FirstOrDefault(e => e.NDEID == NDEID);
        }

        /// <summary>
        /// 根据委托单主键获取检测单
        /// </summary>
        /// <param name="NDEID"></param>
        /// <returns></returns>
        public static Model.HJGL_Batch_NDE GetNDEByTrustBatchId(string TrustBatchId)
        {
            return Funs.DB.HJGL_Batch_NDE.FirstOrDefault(e => e.TrustBatchId == TrustBatchId);
        }

        /// <summary>
        /// 根据委托单主键获取检测单
        /// </summary>
        /// <param name="TrustBatchId"></param>
        /// <returns></returns>
        public static Model.View_Batch_NDE GetNDEViewByTrustBatchId(string TrustBatchId)
        {
            return Funs.DB.View_Batch_NDE.FirstOrDefault(e => e.TrustBatchId == TrustBatchId);
        }

        /// <summary>
        /// 根据主键获取检测单视图
        /// </summary>
        /// <param name="NDEID"></param>
        /// <returns></returns>
        public static Model.View_Batch_NDE GetNDEViewById(string NDEID)
        {
            return Funs.DB.View_Batch_NDE.FirstOrDefault(e => e.NDEID == NDEID);
        }

        /// <summary>
        /// 添加检测单
        /// </summary>
        /// <param name="NDE"></param>
        public static void AddNDE(Model.HJGL_Batch_NDE NDE)
        {
            Model.SGGLDB db = Funs.DB;
            Model.HJGL_Batch_NDE newNDE = new Model.HJGL_Batch_NDE();
            newNDE.NDEID = NDE.NDEID;
            newNDE.TrustBatchId = NDE.TrustBatchId;
            newNDE.ProjectId = NDE.ProjectId;
            newNDE.UnitId = NDE.UnitId;
            newNDE.UnitWorkId = NDE.UnitWorkId;

            newNDE.NDEUnit = NDE.NDEUnit;
            newNDE.NDECode = NDE.NDECode;
            newNDE.NDEDate = NDE.NDEDate;
            newNDE.NDEMan = NDE.NDEMan;
            newNDE.AuditDate = NDE.AuditDate;
            newNDE.Remark = NDE.Remark;

            db.HJGL_Batch_NDE.InsertOnSubmit(newNDE);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改检测单
        /// </summary>
        /// <param name="NDE"></param>
        public static void UpdateNDE(Model.HJGL_Batch_NDE NDE)
        {
            Model.SGGLDB db = Funs.DB;
            Model.HJGL_Batch_NDE newNDE = db.HJGL_Batch_NDE.FirstOrDefault(e => e.NDEID == NDE.NDEID);
            if (newNDE != null)
            {
                newNDE.TrustBatchId = NDE.TrustBatchId;
                newNDE.ProjectId = NDE.ProjectId;
                newNDE.UnitId = NDE.UnitId;
                newNDE.NDEUnit = NDE.NDEUnit;
                newNDE.UnitWorkId = NDE.UnitWorkId;
                newNDE.NDECode = NDE.NDECode;
                newNDE.NDEDate = NDE.NDEDate;
                newNDE.NDEMan = NDE.NDEMan;
                newNDE.AuditDate = NDE.AuditDate;
                newNDE.Remark = NDE.Remark;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除检测单
        /// </summary>
        /// <param name="NDEID"></param>
        public static void DeleteNDEById(string NDEID)
        {
            Model.SGGLDB db = Funs.DB;
            Model.HJGL_Batch_NDE NDE = db.HJGL_Batch_NDE.FirstOrDefault(e => e.NDEID == NDEID);
            if (NDE != null)
            {
                db.HJGL_Batch_NDE.DeleteOnSubmit(NDE);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 检测单委托单编号是否存在
        /// </summary>
        /// <param name="pointNo"></param>
        /// <param name="pointId"></param>
        /// <returns></returns>
        public static bool IsExistNDECode(string NDECode, string NDEID, string projectId)
        {
            var q = Funs.DB.HJGL_Batch_NDE.FirstOrDefault(x => x.NDECode == NDECode && x.ProjectId == projectId && x.NDEID != NDEID);
            if (q != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
