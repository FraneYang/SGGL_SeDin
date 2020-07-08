using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 特种设备机具入场报批主表
    /// </summary>
    public static class EquipmentInService
    {
        public static Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);

        /// <summary>
        /// 根据主键获取特种设备机具入场报批
        /// </summary>
        /// <param name="equipmentInId"></param>
        /// <returns></returns>
        public static Model.InApproveManager_EquipmentIn GetEquipmentInById(string equipmentInId)
        {
            return new Model.SGGLDB(Funs.ConnString).InApproveManager_EquipmentIn.FirstOrDefault(e => e.EquipmentInId == equipmentInId);
        }

        /// <summary>
        /// 添加特种设备机具入场报批
        /// </summary>
        /// <param name="equipmentIn"></param>
        public static void AddEquipmentIn(Model.InApproveManager_EquipmentIn equipmentIn)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.InApproveManager_EquipmentIn newEquipmenteIn = new Model.InApproveManager_EquipmentIn
            {
                EquipmentInId = equipmentIn.EquipmentInId,
                ProjectId = equipmentIn.ProjectId,
                EquipmentInCode = equipmentIn.EquipmentInCode,
                UnitId = equipmentIn.UnitId,
                CarNumber = equipmentIn.CarNumber,
                SubProjectName = equipmentIn.SubProjectName,
                ContentDef = equipmentIn.ContentDef,
                OtherDef = equipmentIn.OtherDef,
                State = equipmentIn.State,
                CompileMan = equipmentIn.CompileMan,
                CompileDate = equipmentIn.CompileDate
            };
            db.InApproveManager_EquipmentIn.InsertOnSubmit(newEquipmenteIn);
            db.SubmitChanges();

            CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(Const.EquipmentInMenuId, equipmentIn.ProjectId, null, equipmentIn.EquipmentInId, equipmentIn.CompileDate);
        }

        /// <summary>
        /// 修改特种设备机具入场报批
        /// </summary>
        /// <param name="equipmentIn"></param>
        public static void UpdateEquipmentIn(Model.InApproveManager_EquipmentIn equipmentIn)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.InApproveManager_EquipmentIn newEquipmenteIn = db.InApproveManager_EquipmentIn.FirstOrDefault(e => e.EquipmentInId == equipmentIn.EquipmentInId);
            if (newEquipmenteIn != null)
            {
                //newEquipmenteIn.ProjectId = equipmentIn.ProjectId;
                newEquipmenteIn.EquipmentInCode = equipmentIn.EquipmentInCode;
                newEquipmenteIn.UnitId = equipmentIn.UnitId;
                newEquipmenteIn.CarNumber = equipmentIn.CarNumber;
                newEquipmenteIn.SubProjectName = equipmentIn.SubProjectName;
                newEquipmenteIn.ContentDef = equipmentIn.ContentDef;
                newEquipmenteIn.OtherDef = equipmentIn.OtherDef;
                newEquipmenteIn.State = equipmentIn.State;
                newEquipmenteIn.CompileMan = equipmentIn.CompileMan;
                newEquipmenteIn.CompileDate = equipmentIn.CompileDate;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除特种设备机具入场报批
        /// </summary>
        /// <param name="equipmentInId"></param>
        public static void DeleteEquipmentInById(string equipmentInId)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.InApproveManager_EquipmentIn equipmentIn = db.InApproveManager_EquipmentIn.FirstOrDefault(e => e.EquipmentInId == equipmentInId);
            if (equipmentIn != null)
            {
                CodeRecordsService.DeleteCodeRecordsByDataId(equipmentInId);//删除编号
                CommonService.DeleteAttachFileById(equipmentInId);//删除附件

                BLL.CommonService.DeleteFlowOperateByID(equipmentInId);  ////删除审核流程表
                db.InApproveManager_EquipmentIn.DeleteOnSubmit(equipmentIn);
                db.SubmitChanges();
            }
        }
    }
}
