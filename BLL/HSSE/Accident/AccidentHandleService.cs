using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// HSSE事故(含未遂)处理
    /// </summary>
    public static class AccidentHandleService
    {
        public static Model.SGGLDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取HSSE事故（含未遂）处理
        /// </summary>
        /// <param name="accidentHandleId"></param>
        /// <returns></returns>
        public static Model.Accident_AccidentHandle GetAccidentHandleById(string accidentHandleId)
        {
            return Funs.DB.Accident_AccidentHandle.FirstOrDefault(e => e.AccidentHandleId == accidentHandleId);
        }

        /// <summary>
        /// 添加HSSE事故（含未遂）处理
        /// </summary>
        /// <param name="accidentHandle"></param>
        public static void AddAccidentHandle(Model.Accident_AccidentHandle accidentHandle)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Accident_AccidentHandle newAccidentHandle = new Model.Accident_AccidentHandle
            {
                AccidentHandleId = accidentHandle.AccidentHandleId,
                ProjectId = accidentHandle.ProjectId,
                UnitId = accidentHandle.UnitId,
                AccidentHandleCode = accidentHandle.AccidentHandleCode,
                AccidentHandleName = accidentHandle.AccidentHandleName,
                AccidentDate = accidentHandle.AccidentDate,
                AccidentDef = accidentHandle.AccidentDef,
                Death = accidentHandle.Death,
                MoneyLoss = accidentHandle.MoneyLoss,
                AccidentHandle = accidentHandle.AccidentHandle,
                Remark = accidentHandle.Remark,
                States = accidentHandle.States,
                CompileMan = accidentHandle.CompileMan,
                CompileDate = accidentHandle.CompileDate,
                DeathPersonNum = accidentHandle.DeathPersonNum,
                InjuriesPersonNum = accidentHandle.InjuriesPersonNum,
                MinorInjuriesPersonNum = accidentHandle.MinorInjuriesPersonNum,
                WorkHoursLoss = accidentHandle.WorkHoursLoss
            };
            db.Accident_AccidentHandle.InsertOnSubmit(newAccidentHandle);
            db.SubmitChanges();
            CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(Const.ProjectAccidentHandleMenuId, accidentHandle.ProjectId, accidentHandle.UnitId, accidentHandle.AccidentHandleId, accidentHandle.CompileDate);
        }

        /// <summary>
        /// 修改HSSE事故（含未遂）处理
        /// </summary>
        /// <param name="accidentHandle"></param>
        public static void UpdateAccidentHandle(Model.Accident_AccidentHandle accidentHandle)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Accident_AccidentHandle newAccidentHandle = db.Accident_AccidentHandle.FirstOrDefault(e => e.AccidentHandleId == accidentHandle.AccidentHandleId);
            if (newAccidentHandle != null)
            {
                //newAccidentHandle.ProjectId = accidentHandle.ProjectId;
                newAccidentHandle.AccidentHandleCode = accidentHandle.AccidentHandleCode;
                newAccidentHandle.AccidentHandleName = accidentHandle.AccidentHandleName;
                newAccidentHandle.UnitId = accidentHandle.UnitId;
                newAccidentHandle.AccidentDate = accidentHandle.AccidentDate;
                newAccidentHandle.AccidentDef = accidentHandle.AccidentDef;
                newAccidentHandle.Death = accidentHandle.Death;
                newAccidentHandle.MoneyLoss = accidentHandle.MoneyLoss;
                newAccidentHandle.AccidentHandle = accidentHandle.AccidentHandle;
                newAccidentHandle.Remark = accidentHandle.Remark;
                newAccidentHandle.States = accidentHandle.States;
                newAccidentHandle.CompileMan = accidentHandle.CompileMan;
                newAccidentHandle.CompileDate = accidentHandle.CompileDate;
                newAccidentHandle.DeathPersonNum = accidentHandle.DeathPersonNum;
                newAccidentHandle.InjuriesPersonNum = accidentHandle.InjuriesPersonNum;
                newAccidentHandle.MinorInjuriesPersonNum = accidentHandle.MinorInjuriesPersonNum;
                newAccidentHandle.WorkHoursLoss = accidentHandle.WorkHoursLoss;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除HSSE事故（含未遂）处理
        /// </summary>
        /// <param name="accidentHandleId"></param>
        public static void DeleteAccidentHandleById(string accidentHandleId)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Accident_AccidentHandle accidentHandle = db.Accident_AccidentHandle.FirstOrDefault(e => e.AccidentHandleId == accidentHandleId);
            if (accidentHandle != null)
            {
                CodeRecordsService.DeleteCodeRecordsByDataId(accidentHandleId);
                CommonService.DeleteFlowOperateByID(accidentHandleId);
                db.Accident_AccidentHandle.DeleteOnSubmit(accidentHandle);
                db.SubmitChanges();
            }
        }
    }
}