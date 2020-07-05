using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 安全人员资质
    /// </summary>
    public static class SafePersonQualityService
    {
        public static Model.SGGLDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取安全人员资质
        /// </summary>
        /// <param name="SafePersonQualityId"></param>
        /// <returns></returns>
        public static Model.QualityAudit_SafePersonQuality GetSafePersonQualityByPersonId(string personId)
        {
            return Funs.DB.QualityAudit_SafePersonQuality.FirstOrDefault(e => e.PersonId == personId);
        }

        /// <summary>
        /// 添加安全人员资质
        /// </summary>
        /// <param name="SafePersonQuality"></param>
        public static void AddSafePersonQuality(Model.QualityAudit_SafePersonQuality SafePersonQuality)
        {
            Model.SGGLDB db = Funs.DB;
            Model.QualityAudit_SafePersonQuality newSafePersonQuality = new Model.QualityAudit_SafePersonQuality
            {
                SafePersonQualityId = SafePersonQuality.SafePersonQualityId,
                PersonId = SafePersonQuality.PersonId,
                CertificateNo = SafePersonQuality.CertificateNo,
                CertificateName = SafePersonQuality.CertificateName,
                Grade = SafePersonQuality.Grade,
                SendUnit = SafePersonQuality.SendUnit,
                SendDate = SafePersonQuality.SendDate,
                LimitDate = SafePersonQuality.LimitDate,
                LateCheckDate = SafePersonQuality.LateCheckDate,
                ApprovalPerson = SafePersonQuality.ApprovalPerson,
                Remark = SafePersonQuality.Remark,
                CompileMan = SafePersonQuality.CompileMan,
                CompileDate = SafePersonQuality.CompileDate,
                AuditDate = SafePersonQuality.AuditDate,
                AuditorId = SafePersonQuality.AuditorId,
            };
            db.QualityAudit_SafePersonQuality.InsertOnSubmit(newSafePersonQuality);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改安全人员资质
        /// </summary>
        /// <param name="SafePersonQuality"></param>
        public static void UpdateSafePersonQuality(Model.QualityAudit_SafePersonQuality SafePersonQuality)
        {
            Model.SGGLDB db = Funs.DB;
            Model.QualityAudit_SafePersonQuality newSafePersonQuality = db.QualityAudit_SafePersonQuality.FirstOrDefault(e => e.SafePersonQualityId == SafePersonQuality.SafePersonQualityId);
            if (newSafePersonQuality != null)
            {
                newSafePersonQuality.CertificateNo = SafePersonQuality.CertificateNo;
                newSafePersonQuality.CertificateName = SafePersonQuality.CertificateName;
                newSafePersonQuality.Grade = SafePersonQuality.Grade;
                newSafePersonQuality.SendUnit = SafePersonQuality.SendUnit;
                newSafePersonQuality.SendDate = SafePersonQuality.SendDate;
                newSafePersonQuality.LimitDate = SafePersonQuality.LimitDate;
                newSafePersonQuality.LateCheckDate = SafePersonQuality.LateCheckDate;
                newSafePersonQuality.ApprovalPerson = SafePersonQuality.ApprovalPerson;
                newSafePersonQuality.Remark = SafePersonQuality.Remark;
                newSafePersonQuality.CompileMan = SafePersonQuality.CompileMan;
                newSafePersonQuality.CompileDate = SafePersonQuality.CompileDate;
                newSafePersonQuality.AuditDate = SafePersonQuality.AuditDate;
                newSafePersonQuality.AuditorId = SafePersonQuality.AuditorId;
                db.SubmitChanges();
            }
        }
    }
}
