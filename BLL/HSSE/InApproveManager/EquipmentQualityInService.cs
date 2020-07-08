using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 特种设备审批
    /// </summary>
   public static class EquipmentQualityInService
   {
       public static Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);

       /// <summary>
       /// 根据主键获取特种设备审批
       /// </summary>
       /// <param name="EquipmentQualityInId"></param>
       /// <returns></returns>
       public static Model.InApproveManager_EquipmentQualityIn GetEquipmentQualityInById(string EquipmentQualityInId)
       {
           return new Model.SGGLDB(Funs.ConnString).InApproveManager_EquipmentQualityIn.FirstOrDefault(e => e.EquipmentQualityInId == EquipmentQualityInId);
       }

       /// <summary>
       /// 添加特种设备审批
       /// </summary>
       /// <param name="EquipmentQualityIn"></param>
       public static void AddEquipmentQualityIn(Model.InApproveManager_EquipmentQualityIn EquipmentQualityIn)
       {
           Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.InApproveManager_EquipmentQualityIn newEquipmentQualityIn = new Model.InApproveManager_EquipmentQualityIn
            {
                EquipmentQualityInId = EquipmentQualityIn.EquipmentQualityInId,
                ProjectId = EquipmentQualityIn.ProjectId,
                UnitId = EquipmentQualityIn.UnitId,
                DriverName = EquipmentQualityIn.DriverName,
                CarNum = EquipmentQualityIn.CarNum,
                CarType = EquipmentQualityIn.CarType,
                States = EquipmentQualityIn.States,
                CompileMan = EquipmentQualityIn.CompileMan,
                CompileDate = EquipmentQualityIn.CompileDate,
                DutyMan = EquipmentQualityIn.DutyMan
            };
            db.InApproveManager_EquipmentQualityIn.InsertOnSubmit(newEquipmentQualityIn);
           db.SubmitChanges();
       }

       /// <summary>
       /// 修改特种设备审批
       /// </summary>
       /// <param name="EquipmentQualityIn"></param>
       public static void UpdateEquipmentQualityIn(Model.InApproveManager_EquipmentQualityIn EquipmentQualityIn)
       {
           Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
           Model.InApproveManager_EquipmentQualityIn newEquipmentQualityIn = db.InApproveManager_EquipmentQualityIn.FirstOrDefault(e => e.EquipmentQualityInId == EquipmentQualityIn.EquipmentQualityInId);
           if (newEquipmentQualityIn != null)
           {
               //newEquipmentQualityIn.ProjectId = EquipmentQualityIn.ProjectId;
               newEquipmentQualityIn.UnitId = EquipmentQualityIn.UnitId;
               newEquipmentQualityIn.DriverName = EquipmentQualityIn.DriverName;
               newEquipmentQualityIn.CarNum = EquipmentQualityIn.CarNum;
               newEquipmentQualityIn.CarType = EquipmentQualityIn.CarType;
               newEquipmentQualityIn.States = EquipmentQualityIn.States;
               newEquipmentQualityIn.CompileMan = EquipmentQualityIn.CompileMan;
               newEquipmentQualityIn.CompileDate = EquipmentQualityIn.CompileDate;
               newEquipmentQualityIn.DutyMan = EquipmentQualityIn.DutyMan;
               db.SubmitChanges();
           }
       }

       /// <summary>
       /// 根据主键删除特种设备审批
       /// </summary>
       /// <param name="EquipmentQualityInId"></param>
       public static void DeleteEquipmentQualityInById(string EquipmentQualityInId)
       {
           Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
           Model.InApproveManager_EquipmentQualityIn EquipmentQualityIn = db.InApproveManager_EquipmentQualityIn.FirstOrDefault(e => e.EquipmentQualityInId == EquipmentQualityInId);
           if (EquipmentQualityIn != null)
           {
               CommonService.DeleteAttachFileById(EquipmentQualityInId);
               BLL.CommonService.DeleteFlowOperateByID(EquipmentQualityInId);  ////删除审核流程表
               db.InApproveManager_EquipmentQualityIn.DeleteOnSubmit(EquipmentQualityIn);
               db.SubmitChanges();
           }
       }
   }
}
