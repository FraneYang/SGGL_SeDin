using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 一般设备机具入场报批主表
    /// </summary>
    public static class GeneralEquipmentInService
    {
        public static Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);

        /// <summary>
        /// 根据主键获取一般设备机具入场报批
        /// </summary>
        /// <param name="GeneralEquipmentInId"></param>
        /// <returns></returns>
        public static Model.InApproveManager_GeneralEquipmentIn GetGeneralEquipmentInById(string generalEquipmentInId)
        {
            return new Model.SGGLDB(Funs.ConnString).InApproveManager_GeneralEquipmentIn.FirstOrDefault(e => e.GeneralEquipmentInId == generalEquipmentInId);
        }

        /// <summary>
        /// 添加一般设备机具入场报批
        /// </summary>
        /// <param name="GeneralGeneralEquipmentIn"></param>
        public static void AddGeneralEquipmentIn(Model.InApproveManager_GeneralEquipmentIn generalEquipmentIn)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.InApproveManager_GeneralEquipmentIn newGeneralEquipmentIn = new Model.InApproveManager_GeneralEquipmentIn
            {
                GeneralEquipmentInId = generalEquipmentIn.GeneralEquipmentInId,
                ProjectId = generalEquipmentIn.ProjectId,
                GeneralEquipmentInCode = generalEquipmentIn.GeneralEquipmentInCode,
                UnitId = generalEquipmentIn.UnitId,
                CarNumber = generalEquipmentIn.CarNumber,
                SubProjectName = generalEquipmentIn.SubProjectName,
                ContentDef = generalEquipmentIn.ContentDef,
                OtherDef = generalEquipmentIn.OtherDef,
                State = generalEquipmentIn.State,
                CompileMan = generalEquipmentIn.CompileMan,
                CompileDate = generalEquipmentIn.CompileDate
            };
            db.InApproveManager_GeneralEquipmentIn.InsertOnSubmit(newGeneralEquipmentIn);
            db.SubmitChanges();

            CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(Const.GeneralEquipmentInMenuId, generalEquipmentIn.ProjectId, null, generalEquipmentIn.GeneralEquipmentInId, generalEquipmentIn.CompileDate);
        }

        /// <summary>
        /// 修改一般设备机具入场报批
        /// </summary>
        /// <param name="GeneralGeneralEquipmentIn"></param>
        public static void UpdateGeneralEquipmentIn(Model.InApproveManager_GeneralEquipmentIn generalEquipmentIn)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.InApproveManager_GeneralEquipmentIn newGeneralEquipmentIn = db.InApproveManager_GeneralEquipmentIn.FirstOrDefault(e => e.GeneralEquipmentInId == generalEquipmentIn.GeneralEquipmentInId);
            if (newGeneralEquipmentIn != null)
            {
                //newGeneralEquipmentIn.ProjectId = generalEquipmentIn.ProjectId;
                newGeneralEquipmentIn.GeneralEquipmentInCode = generalEquipmentIn.GeneralEquipmentInCode;
                newGeneralEquipmentIn.UnitId = generalEquipmentIn.UnitId;
                newGeneralEquipmentIn.CarNumber = generalEquipmentIn.CarNumber;
                newGeneralEquipmentIn.SubProjectName = generalEquipmentIn.SubProjectName;
                newGeneralEquipmentIn.ContentDef = generalEquipmentIn.ContentDef;
                newGeneralEquipmentIn.OtherDef = generalEquipmentIn.OtherDef;
                newGeneralEquipmentIn.State = generalEquipmentIn.State;
                newGeneralEquipmentIn.CompileMan = generalEquipmentIn.CompileMan;
                newGeneralEquipmentIn.CompileDate = generalEquipmentIn.CompileDate;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除一般设备机具入场报批
        /// </summary>
        /// <param name="GeneralEquipmentInId"></param>
        public static void DeleteGeneralEquipmentInById(string generalEquipmentInId)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.InApproveManager_GeneralEquipmentIn generalEquipmentIn = db.InApproveManager_GeneralEquipmentIn.FirstOrDefault(e => e.GeneralEquipmentInId == generalEquipmentInId);
            if (generalEquipmentIn != null)
            {
                CodeRecordsService.DeleteCodeRecordsByDataId(generalEquipmentInId);//删除编号
                CommonService.DeleteAttachFileById(generalEquipmentInId);//删除附件
                BLL.CommonService.DeleteFlowOperateByID(generalEquipmentInId);  ////删除审核流程表
                db.InApproveManager_GeneralEquipmentIn.DeleteOnSubmit(generalEquipmentIn);
                db.SubmitChanges();
            }
        }
    }
}