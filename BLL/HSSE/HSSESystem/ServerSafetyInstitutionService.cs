using System.Linq;

namespace BLL
{
    /// <summary>
    /// 安全制度
    /// </summary>
    public static class ServerSafetyInstitutionService
    {
        public static Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);

        /// <summary>
        /// 根据主键获取安全制度
        /// </summary>
        /// <param name="safetyInstitutionId"></param>
        /// <returns></returns>
        public static Model.HSSESystem_SafetyInstitution GetSafetyInstitutionById(string safetyInstitutionId)
        {
            return new Model.SGGLDB(Funs.ConnString).HSSESystem_SafetyInstitution.FirstOrDefault(e => e.SafetyInstitutionId == safetyInstitutionId);
        }

        /// <summary>
        /// 添加安全制度
        /// </summary>
        /// <param name="SafetyInstitution"></param>
        public static void AddSafetyInstitution(Model.HSSESystem_SafetyInstitution safetyInstitution)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.HSSESystem_SafetyInstitution newSafetyInstitution = new Model.HSSESystem_SafetyInstitution
            {
                SafetyInstitutionId = safetyInstitution.SafetyInstitutionId,
                SafetyInstitutionName = safetyInstitution.SafetyInstitutionName,
                EffectiveDate = safetyInstitution.EffectiveDate,
                Scope = safetyInstitution.Scope,
                Remark = safetyInstitution.Remark,
                FileContents = safetyInstitution.FileContents
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
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.HSSESystem_SafetyInstitution newSafetyInstitution = db.HSSESystem_SafetyInstitution.FirstOrDefault(e => e.SafetyInstitutionId == safetyInstitution.SafetyInstitutionId);
            if (newSafetyInstitution != null)
            {
                newSafetyInstitution.SafetyInstitutionName = safetyInstitution.SafetyInstitutionName;
                newSafetyInstitution.EffectiveDate = safetyInstitution.EffectiveDate;
                newSafetyInstitution.Scope = safetyInstitution.Scope;
                newSafetyInstitution.Remark = safetyInstitution.Remark;
                newSafetyInstitution.FileContents = safetyInstitution.FileContents;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除安全制度
        /// </summary>
        /// <param name="safetyInstitutionId"></param>
        public static void DeleteSafetyInstitutionById(string safetyInstitutionId)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
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
