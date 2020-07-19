using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 特种设备机具出场报批
    /// </summary>
    public static class EquipmentOutService
    {
        public static Model.SGGLDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取特种设备机具出场报批
        /// </summary>
        /// <param name="equipmentOutId"></param>
        /// <returns></returns>
        public static Model.InApproveManager_EquipmentOut GetEquipmentOutById(string equipmentOutId)
        {
            return Funs.DB.InApproveManager_EquipmentOut.FirstOrDefault(e => e.EquipmentOutId == equipmentOutId);
        }

        /// <summary>
        /// 添加特种设备机具出场报批
        /// </summary>
        /// <param name="equipmentOut"></param>
        public static void AddEquipmentOut(Model.InApproveManager_EquipmentOut equipmentOut)
        {
            Model.SGGLDB db = Funs.DB;
            Model.InApproveManager_EquipmentOut newEquipmentOut = new Model.InApproveManager_EquipmentOut
            {
                EquipmentOutId = equipmentOut.EquipmentOutId,
                ProjectId = equipmentOut.ProjectId,
                EquipmentOutCode = equipmentOut.EquipmentOutCode,
                UnitId = equipmentOut.UnitId,
                ApplicationDate = equipmentOut.ApplicationDate,
                CarNum = equipmentOut.CarNum,
                CarModel = equipmentOut.CarModel,
                DriverName = equipmentOut.DriverName,
                DriverNum = equipmentOut.DriverNum,
                TransPortStart = equipmentOut.TransPortStart,
                TransPortEnd = equipmentOut.TransPortEnd,
                CompileMan = equipmentOut.CompileMan,
                CompileDate = equipmentOut.CompileDate
            };
            db.InApproveManager_EquipmentOut.InsertOnSubmit(newEquipmentOut);
            db.SubmitChanges();

            BLL.CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(BLL.Const.EquipmentOutMenuId, equipmentOut.ProjectId, null, equipmentOut.EquipmentOutId, equipmentOut.CompileDate);
        }

        /// <summary>
        /// 修改特种设备机具出场报批
        /// </summary>
        /// <param name="equipmentOut"></param>
        public static void UpdateEquipmentOut(Model.InApproveManager_EquipmentOut equipmentOut)
        {
            Model.SGGLDB db = Funs.DB;
            Model.InApproveManager_EquipmentOut newEquipmentOut = db.InApproveManager_EquipmentOut.FirstOrDefault(e => e.EquipmentOutId == equipmentOut.EquipmentOutId);
            if (newEquipmentOut != null)
            {
                //newEquipmentOut.ProjectId = equipmentOut.ProjectId;
                newEquipmentOut.EquipmentOutCode = equipmentOut.EquipmentOutCode;
                newEquipmentOut.UnitId = equipmentOut.UnitId;
                newEquipmentOut.ApplicationDate = equipmentOut.ApplicationDate;
                newEquipmentOut.CarNum = equipmentOut.CarNum;
                newEquipmentOut.CarModel = equipmentOut.CarModel;
                newEquipmentOut.DriverName = equipmentOut.DriverName;
                newEquipmentOut.DriverNum = equipmentOut.DriverNum;
                newEquipmentOut.TransPortStart = equipmentOut.TransPortStart;
                newEquipmentOut.TransPortEnd = equipmentOut.TransPortEnd;
                newEquipmentOut.CompileMan = equipmentOut.CompileMan;
                newEquipmentOut.CompileDate = equipmentOut.CompileDate;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除特种设备机具出场报批
        /// </summary>
        /// <param name="equipmentOutId"></param>
        public static void DeleteEquipmentOutById(string equipmentOutId)
        {
            Model.SGGLDB db = Funs.DB;
            Model.InApproveManager_EquipmentOut equipmentOut = db.InApproveManager_EquipmentOut.FirstOrDefault(e => e.EquipmentOutId == equipmentOutId);
            if (equipmentOut != null)
            {
                CommonService.DeleteAttachFileById(equipmentOutId);
                CodeRecordsService.DeleteCodeRecordsByDataId(equipmentOutId);
                BLL.CommonService.DeleteFlowOperateByID(equipmentOutId);  ////删除审核流程表
                db.InApproveManager_EquipmentOut.DeleteOnSubmit(equipmentOut);
                db.SubmitChanges();
            }
        }
    }
}
