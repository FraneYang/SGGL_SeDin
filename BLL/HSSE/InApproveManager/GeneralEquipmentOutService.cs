using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 一般设备机具出场报批主表
    /// </summary>
    public static class GeneralEquipmentOutService
    {
        public static Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);

        /// <summary>
        /// 根据主键获取一般设备机具出场报批
        /// </summary>
        /// <param name="generalEquipmentOutId"></param>
        /// <returns></returns>
        public static Model.InApproveManager_GeneralEquipmentOut GetGeneralEquipmentOutById(string generalEquipmentOutId)
        {
            return new Model.SGGLDB(Funs.ConnString).InApproveManager_GeneralEquipmentOut.FirstOrDefault(e => e.GeneralEquipmentOutId == generalEquipmentOutId);
        }

        /// <summary>
        /// 添加一般设备机具出场报批
        /// </summary>
        /// <param name="generalEquipmentOut"></param>
        public static void AddGeneralEquipmentOut(Model.InApproveManager_GeneralEquipmentOut generalEquipmentOut)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.InApproveManager_GeneralEquipmentOut newGeneralEquipmentOut = new Model.InApproveManager_GeneralEquipmentOut
            {
                GeneralEquipmentOutId = generalEquipmentOut.GeneralEquipmentOutId,
                ProjectId = generalEquipmentOut.ProjectId,
                GeneralEquipmentOutCode = generalEquipmentOut.GeneralEquipmentOutCode,
                UnitId = generalEquipmentOut.UnitId,
                ApplicationDate = generalEquipmentOut.ApplicationDate,
                CarNum = generalEquipmentOut.CarNum,
                CarModel = generalEquipmentOut.CarModel,
                DriverName = generalEquipmentOut.DriverName,
                DriverNum = generalEquipmentOut.DriverNum,
                TransPortStart = generalEquipmentOut.TransPortStart,
                TransPortEnd = generalEquipmentOut.TransPortEnd,
                State = generalEquipmentOut.State,
                CompileMan = generalEquipmentOut.CompileMan,
                CompileDate = generalEquipmentOut.CompileDate
            };
            db.InApproveManager_GeneralEquipmentOut.InsertOnSubmit(newGeneralEquipmentOut);
            db.SubmitChanges();

            CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(Const.GeneralEquipmentOutMenuId, generalEquipmentOut.ProjectId, null, generalEquipmentOut.GeneralEquipmentOutId, generalEquipmentOut.CompileDate);
        }

        /// <summary>
        /// 修改一般设备机具出场报批
        /// </summary>
        /// <param name="generalEquipmentOut"></param>
        public static void UpdateGeneralEquipmentOut(Model.InApproveManager_GeneralEquipmentOut generalEquipmentOut)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.InApproveManager_GeneralEquipmentOut newGeneralEquipmentOut = db.InApproveManager_GeneralEquipmentOut.FirstOrDefault(e => e.GeneralEquipmentOutId == generalEquipmentOut.GeneralEquipmentOutId);
            if (newGeneralEquipmentOut != null)
            {
                //newGeneralEquipmentOut.ProjectId = generalEquipmentOut.ProjectId;
                newGeneralEquipmentOut.GeneralEquipmentOutCode = generalEquipmentOut.GeneralEquipmentOutCode;
                newGeneralEquipmentOut.UnitId = generalEquipmentOut.UnitId;
                newGeneralEquipmentOut.ApplicationDate = generalEquipmentOut.ApplicationDate;
                newGeneralEquipmentOut.CarNum = generalEquipmentOut.CarNum;
                newGeneralEquipmentOut.CarModel = generalEquipmentOut.CarModel;
                newGeneralEquipmentOut.DriverName = generalEquipmentOut.DriverName;
                newGeneralEquipmentOut.DriverNum = generalEquipmentOut.DriverNum;
                newGeneralEquipmentOut.TransPortStart = generalEquipmentOut.TransPortStart;
                newGeneralEquipmentOut.TransPortEnd = generalEquipmentOut.TransPortEnd;
                newGeneralEquipmentOut.State = generalEquipmentOut.State;
                newGeneralEquipmentOut.CompileMan = generalEquipmentOut.CompileMan;
                newGeneralEquipmentOut.CompileDate = generalEquipmentOut.CompileDate;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除一般设备机具出场报批
        /// </summary>
        /// <param name="generalEquipmentOutId"></param>
        public static void DeleteGeneralEquipmentOutById(string generalEquipmentOutId)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.InApproveManager_GeneralEquipmentOut generalEquipmentOut = db.InApproveManager_GeneralEquipmentOut.FirstOrDefault(e => e.GeneralEquipmentOutId == generalEquipmentOutId);
            if (generalEquipmentOut != null)
            {
                CommonService.DeleteAttachFileById(generalEquipmentOutId);
                CodeRecordsService.DeleteCodeRecordsByDataId(generalEquipmentOutId);
                BLL.CommonService.DeleteFlowOperateByID(generalEquipmentOutId);  ////删除审核流程表
                db.InApproveManager_GeneralEquipmentOut.DeleteOnSubmit(generalEquipmentOut);
                db.SubmitChanges();
            }
        }
    }
}