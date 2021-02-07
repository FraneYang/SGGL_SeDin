using System.Linq;

namespace BLL
{
    /// <summary>
    /// 安全制度
    /// </summary>
    public static class ServerSafetyInstitutionService
    {
        public static Model.SGGLDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取安全制度
        /// </summary>
        /// <param name="safetyInstitutionId"></param>
        /// <returns></returns>
        public static Model.HSSESystem_SafetyInstitution GetSafetyInstitutionById(string safetyInstitutionId)
        {
            return Funs.DB.HSSESystem_SafetyInstitution.FirstOrDefault(e => e.SafetyInstitutionId == safetyInstitutionId);
        }

        /// <summary>
        /// 添加安全制度
        /// </summary>
        /// <param name="SafetyInstitution"></param>
        public static void AddSafetyInstitution(Model.HSSESystem_SafetyInstitution safetyInstitution)
        {
            Model.SGGLDB db = Funs.DB;
            Model.HSSESystem_SafetyInstitution newSafetyInstitution = new Model.HSSESystem_SafetyInstitution
            {
                SafetyInstitutionId = safetyInstitution.SafetyInstitutionId,
                SafetyInstitutionName = safetyInstitution.SafetyInstitutionName,
                Code = safetyInstitution.Code,
                Scope = safetyInstitution.Scope,
                Remark = safetyInstitution.Remark,
                FileContents = safetyInstitution.FileContents,

                TypeId = safetyInstitution.TypeId,
                CompileMan = safetyInstitution.CompileMan,
                CompileDate = safetyInstitution.CompileDate,
                UnitId = safetyInstitution.UnitId,
                ApprovalDate = safetyInstitution.ApprovalDate,
                EffectiveDate = safetyInstitution.EffectiveDate,
                Description = safetyInstitution.Description,
                ReleaseStates = safetyInstitution.ReleaseStates,
                ReleaseUnit = safetyInstitution.ReleaseUnit,
                AbolitionDate = safetyInstitution.AbolitionDate,
                ReplaceInfo = safetyInstitution.ReplaceInfo,
                IndexesIds = safetyInstitution.IndexesIds,
               
            };
            db.HSSESystem_SafetyInstitution.InsertOnSubmit(newSafetyInstitution);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改安全制度
        /// </summary>
        /// <param name="safetyInstitution"></param>
        public static void UpdateSafetyInstitution(Model.HSSESystem_SafetyInstitution safetyInstitution)
        {
            Model.SGGLDB db = Funs.DB;
            Model.HSSESystem_SafetyInstitution newSafetyInstitution = db.HSSESystem_SafetyInstitution.FirstOrDefault(e => e.SafetyInstitutionId == safetyInstitution.SafetyInstitutionId);
            if (newSafetyInstitution != null)
            {
                newSafetyInstitution.Code = safetyInstitution.Code;
                newSafetyInstitution.SafetyInstitutionName = safetyInstitution.SafetyInstitutionName;
                newSafetyInstitution.EffectiveDate = safetyInstitution.EffectiveDate;
                newSafetyInstitution.Scope = safetyInstitution.Scope;
                newSafetyInstitution.Remark = safetyInstitution.Remark;
                newSafetyInstitution.FileContents = safetyInstitution.FileContents;

                newSafetyInstitution.TypeId = safetyInstitution.TypeId;
                newSafetyInstitution.CompileMan = safetyInstitution.CompileMan;
                newSafetyInstitution.CompileDate = safetyInstitution.CompileDate;
                newSafetyInstitution.UnitId = safetyInstitution.UnitId;
                newSafetyInstitution.ApprovalDate = safetyInstitution.ApprovalDate;
                newSafetyInstitution.EffectiveDate = safetyInstitution.EffectiveDate;
                newSafetyInstitution.Description = safetyInstitution.Description;
                newSafetyInstitution.ReleaseStates = safetyInstitution.ReleaseStates;
                newSafetyInstitution.ReleaseUnit = safetyInstitution.ReleaseUnit;
                newSafetyInstitution.AbolitionDate = safetyInstitution.AbolitionDate;
                newSafetyInstitution.ReplaceInfo = safetyInstitution.ReplaceInfo;
                newSafetyInstitution.IndexesIds = safetyInstitution.IndexesIds;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除安全制度
        /// </summary>
        /// <param name="safetyInstitutionId"></param>
        public static void DeleteSafetyInstitutionById(string safetyInstitutionId)
        {
            Model.SGGLDB db = Funs.DB;
            Model.HSSESystem_SafetyInstitution safetyInstitution = db.HSSESystem_SafetyInstitution.FirstOrDefault(e => e.SafetyInstitutionId == safetyInstitutionId);
            if (safetyInstitution != null)
            {
                BLL.CommonService.DeleteAttachFileById(safetyInstitutionId);
                db.HSSESystem_SafetyInstitution.DeleteOnSubmit(safetyInstitution);
                db.SubmitChanges();
            }
        }
    }
}
