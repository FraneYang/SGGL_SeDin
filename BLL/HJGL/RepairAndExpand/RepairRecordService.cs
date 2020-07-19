using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public static class RepairRecordService
    {

        /// <summary>
        /// 根据主建获取信息
        /// </summary>
        /// <param name="repairRecordId"></param>
        /// <returns></returns>
        public static Model.HJGL_RepairRecord GetRepairRecordById(string repairRecordId)
        {
            return Funs.DB.HJGL_RepairRecord.FirstOrDefault(e => e.RepairRecordId == repairRecordId);
        }

        /// <summary>
        /// 根据检测明细ID获取返修记录
        /// </summary>
        /// <param name="ndeItemID"></param>
        /// <returns></returns>
        public static Model.HJGL_RepairRecord GetRepairRecordByNdeItemId(string ndeItemID)
        {
            return Funs.DB.HJGL_RepairRecord.FirstOrDefault(e => e.NDEItemID == ndeItemID);
        }

        /// <summary>
        /// 添加检测单明细
        /// </summary>
        /// <param name="NDEItem"></param>
        public static void AddRepairRecord(Model.HJGL_RepairRecord repair)
        {
            Model.SGGLDB db = Funs.DB;
            Model.HJGL_RepairRecord newRepair = new Model.HJGL_RepairRecord();
            newRepair.RepairRecordId = repair.RepairRecordId;
            newRepair.RepairRecordCode = repair.RepairRecordCode;
            newRepair.ProjectId = repair.ProjectId;
            newRepair.UnitId = repair.UnitId;
            newRepair.UnitWorkId = repair.UnitWorkId;
            newRepair.NoticeDate = repair.NoticeDate;
            newRepair.NDEItemID = repair.NDEItemID;
            newRepair.WeldJointId = repair.WeldJointId;
            newRepair.DetectionTypeId = repair.DetectionTypeId;
            newRepair.RepairLocation = repair.RepairLocation;
            newRepair.WelderId = repair.WelderId;
            newRepair.RepairMark = repair.RepairMark;
            newRepair.CheckDefects = repair.CheckDefects;
            newRepair.RepairWelder = repair.RepairWelder;
            newRepair.RepairDate = repair.RepairDate;
            newRepair.PhotoUrl = repair.PhotoUrl;
            newRepair.Ex_ToPointBatchItemId = repair.Ex_ToPointBatchItemId;

            db.HJGL_RepairRecord.InsertOnSubmit(newRepair);
            db.SubmitChanges();
        }

        public static void UpdateRepairRecord(Model.HJGL_RepairRecord repair)
        {
            Model.SGGLDB db = Funs.DB;
            Model.HJGL_RepairRecord newRepair = db.HJGL_RepairRecord.FirstOrDefault(e => e.RepairRecordId == repair.RepairRecordId);
            if (newRepair != null)
            {
                newRepair.ProjectId = repair.ProjectId;
                newRepair.UnitId = repair.UnitId;
                newRepair.NDEItemID = repair.NDEItemID;
                newRepair.WeldJointId = repair.WeldJointId;
                newRepair.UnitWorkId = repair.UnitWorkId;
                newRepair.NoticeDate = repair.NoticeDate;
                newRepair.DetectionTypeId = repair.DetectionTypeId;
                newRepair.RepairLocation = repair.RepairLocation;
                newRepair.WelderId = repair.WelderId;
                newRepair.RepairMark = repair.RepairMark;
                newRepair.CheckDefects = repair.CheckDefects;
                newRepair.RepairWelder = repair.RepairWelder;
                newRepair.RepairDate = repair.RepairDate;
                newRepair.PhotoUrl = repair.PhotoUrl;
                newRepair.Ex_ToPointBatchItemId = repair.Ex_ToPointBatchItemId;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 获取返修口对应的扩透口
        /// </summary>
        /// <param name="repairRecordId"></param>
        /// <returns></returns>
        public static List<Model.HJGL_Batch_PointBatchItem> GetExportItem(string repairRecordId)
        {
            var exp = from x in Funs.DB.HJGL_Batch_PointBatchItem where x.RepairRecordId == repairRecordId select x;
            if (exp.Count() > 0)
            {
                return exp.ToList();
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获取返修口对应的扩透口的数量
        /// </summary>
        /// <param name="repairRecordId"></param>
        /// <returns></returns>
        public static int GetExportNum(string repairRecordId)
        {
            var exp = from x in Funs.DB.HJGL_Batch_PointBatchItem where x.RepairRecordId == repairRecordId select x;
            return exp.Count();
        }
    }
}
