using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 特殊岗位人员资质
    /// </summary>
    public static class PersonQualityService
    {
        public static Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);

        /// <summary>
        /// 根据主键获取特殊岗位人员资质
        /// </summary>
        /// <param name="personQualityId"></param>
        /// <returns></returns>
        public static Model.QualityAudit_PersonQuality GetPersonQualityByPersonId(string personId)
        {
            return new Model.SGGLDB(Funs.ConnString).QualityAudit_PersonQuality.FirstOrDefault(e => e.PersonId == personId);
        }

        /// <summary>
        /// 获取时间段的特岗资质集合
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public static List<Model.QualityAudit_PersonQuality> GetListByDate(string projectId, DateTime startTime, DateTime endTime)
        {
            return (from x in new Model.SGGLDB(Funs.ConnString).QualityAudit_PersonQuality
                    join y in new Model.SGGLDB(Funs.ConnString).SitePerson_Person
                    on x.PersonId equals y.PersonId
                    where y.ProjectId == projectId && x.AuditDate >= startTime && x.AuditDate <= endTime
                    orderby x.AuditDate
                    select x).Distinct().ToList();
        }

        /// <summary>
        /// 获取时间段的特岗资质数量
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public static int GetCountByDate(string projectId, DateTime startTime, DateTime endTime)
        {
            var q = (from x in new Model.SGGLDB(Funs.ConnString).QualityAudit_PersonQuality 
                     join y in new Model.SGGLDB(Funs.ConnString).SitePerson_Person
                     on x.PersonId equals y.PersonId
                     where y.ProjectId == projectId && x.AuditDate >= startTime && x.AuditDate <= endTime orderby x.AuditDate select x).Distinct().ToList();
            return q.Count();
        }

        /// <summary>
        /// 添加特殊岗位人员资质
        /// </summary>
        /// <param name="personQuality"></param>
        public static void AddPersonQuality(Model.QualityAudit_PersonQuality personQuality)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.QualityAudit_PersonQuality newPersonQuality = new Model.QualityAudit_PersonQuality
            {
                PersonQualityId = personQuality.PersonQualityId,
                PersonId = personQuality.PersonId,
                CertificateId = personQuality.CertificateId,
                CertificateNo = personQuality.CertificateNo,
                CertificateName = personQuality.CertificateName,
                Grade = personQuality.Grade,
                SendUnit = personQuality.SendUnit,
                SendDate = personQuality.SendDate,
                LimitDate = personQuality.LimitDate,
                LateCheckDate = personQuality.LateCheckDate,
                ApprovalPerson = personQuality.ApprovalPerson,
                Remark = personQuality.Remark,
                CompileMan = personQuality.CompileMan,
                CompileDate = personQuality.CompileDate,
                AuditDate = personQuality.AuditDate,
                AuditorId = personQuality.AuditorId,
            };
            db.QualityAudit_PersonQuality.InsertOnSubmit(newPersonQuality);
            db.SubmitChanges();
            
        }

        /// <summary>
        /// 修改特殊岗位人员资质
        /// </summary>
        /// <param name="personQuality"></param>
        public static void UpdatePersonQuality(Model.QualityAudit_PersonQuality personQuality)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.QualityAudit_PersonQuality newPersonQuality = db.QualityAudit_PersonQuality.FirstOrDefault(e => e.PersonQualityId == personQuality.PersonQualityId);
            if (newPersonQuality != null)
            {
                newPersonQuality.CertificateId = personQuality.CertificateId;
                newPersonQuality.CertificateNo = personQuality.CertificateNo;
                newPersonQuality.CertificateName = personQuality.CertificateName;
                newPersonQuality.Grade = personQuality.Grade;
                newPersonQuality.SendUnit = personQuality.SendUnit;
                newPersonQuality.SendDate = personQuality.SendDate;
                newPersonQuality.LimitDate = personQuality.LimitDate;
                newPersonQuality.LateCheckDate = personQuality.LateCheckDate;
                newPersonQuality.ApprovalPerson = personQuality.ApprovalPerson;
                newPersonQuality.Remark = personQuality.Remark;
                newPersonQuality.CompileMan = personQuality.CompileMan;
                newPersonQuality.CompileDate = personQuality.CompileDate;
                newPersonQuality.AuditDate = personQuality.AuditDate;
                newPersonQuality.AuditorId = personQuality.AuditorId;
                db.SubmitChanges();
            }
        }
    }
}
