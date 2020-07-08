using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// HSSE事故(对人员)记录
    /// </summary>
    public static class AccidentPersonRecordService
    {
        public static Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);

        /// <summary>
        /// 根据主键获取HSSE事故（对人员）记录
        /// </summary>
        /// <param name="accidentPersonRecordId"></param>
        /// <returns></returns>
        public static Model.Accident_AccidentPersonRecord GetAccidentPersonRecordById(string accidentPersonRecordId)
        {
            return new Model.SGGLDB(Funs.ConnString).Accident_AccidentPersonRecord.FirstOrDefault(e => e.AccidentPersonRecordId == accidentPersonRecordId);
        }

        /// <summary>
        /// 添加HSSE事故（对人员）记录
        /// </summary>
        /// <param name="accidentPersonRecord"></param>
        public static void AddAccidentPersonRecord(Model.Accident_AccidentPersonRecord accidentPersonRecord)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Accident_AccidentPersonRecord newAccidentPersonRecord = new Model.Accident_AccidentPersonRecord
            {
                AccidentPersonRecordId = accidentPersonRecord.AccidentPersonRecordId,
                ProjectId = accidentPersonRecord.ProjectId,
                AccidentTypeId = accidentPersonRecord.AccidentTypeId,
                WorkAreaId = accidentPersonRecord.WorkAreaId,
                AccidentDate = accidentPersonRecord.AccidentDate,
                PersonId = accidentPersonRecord.PersonId,
                Injury = accidentPersonRecord.Injury,
                InjuryPart = accidentPersonRecord.InjuryPart,
                HssePersons = accidentPersonRecord.HssePersons,
                InjuryResult = accidentPersonRecord.InjuryResult,
                PreventiveAction = accidentPersonRecord.PreventiveAction,
                HandleOpinion = accidentPersonRecord.HandleOpinion,
                FileContent = accidentPersonRecord.FileContent,
                CompileMan = accidentPersonRecord.CompileMan,
                CompileDate = accidentPersonRecord.CompileDate,
                States = accidentPersonRecord.States
            };
            db.Accident_AccidentPersonRecord.InsertOnSubmit(newAccidentPersonRecord);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改HSSE事故（对人员）记录
        /// </summary>
        /// <param name="accidentPersonRecord"></param>
        public static void UpdateAccidentPersonRecord(Model.Accident_AccidentPersonRecord accidentPersonRecord)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Accident_AccidentPersonRecord newAccidentPersonRecord = db.Accident_AccidentPersonRecord.FirstOrDefault(e => e.AccidentPersonRecordId == accidentPersonRecord.AccidentPersonRecordId);
            if (newAccidentPersonRecord != null)
            {
                //newAccidentPersonRecord.ProjectId = accidentPersonRecord.ProjectId;
                newAccidentPersonRecord.AccidentTypeId = accidentPersonRecord.AccidentTypeId;
                newAccidentPersonRecord.WorkAreaId = accidentPersonRecord.WorkAreaId;
                newAccidentPersonRecord.AccidentDate = accidentPersonRecord.AccidentDate;
                newAccidentPersonRecord.PersonId = accidentPersonRecord.PersonId;
                newAccidentPersonRecord.Injury = accidentPersonRecord.Injury;
                newAccidentPersonRecord.InjuryPart = accidentPersonRecord.InjuryPart;
                newAccidentPersonRecord.HssePersons = accidentPersonRecord.HssePersons;
                newAccidentPersonRecord.InjuryResult = accidentPersonRecord.InjuryResult;
                newAccidentPersonRecord.PreventiveAction = accidentPersonRecord.PreventiveAction;
                newAccidentPersonRecord.HandleOpinion = accidentPersonRecord.HandleOpinion;
                newAccidentPersonRecord.FileContent = accidentPersonRecord.FileContent;
                newAccidentPersonRecord.CompileMan = accidentPersonRecord.CompileMan;
                newAccidentPersonRecord.CompileDate = accidentPersonRecord.CompileDate;
                newAccidentPersonRecord.States = accidentPersonRecord.States;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除HSSE事故（对人员）记录
        /// </summary>
        /// <param name="accidentPersonRecordId"></param>
        public static void DeleteAccidentPersonRecordById(string accidentPersonRecordId)
        { 
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Accident_AccidentPersonRecord accidentPersonRecord = db.Accident_AccidentPersonRecord.FirstOrDefault(e => e.AccidentPersonRecordId == accidentPersonRecordId);
            if (accidentPersonRecord != null)
            {
                CommonService.DeleteFlowOperateByID(accidentPersonRecordId);//删除流程
                db.Accident_AccidentPersonRecord.DeleteOnSubmit(accidentPersonRecord);
                db.SubmitChanges();
            }        
        }
    }
}
