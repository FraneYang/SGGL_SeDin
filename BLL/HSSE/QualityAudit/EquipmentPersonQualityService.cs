using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 特种设备作业人员资质
    /// </summary>
    public static class EquipmentPersonQualityService
    {
        public static Model.SGGLDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取特种设备作业人员资质
        /// </summary>
        /// <param name="EquipmentPersonQualityId"></param>
        /// <returns></returns>
        public static Model.QualityAudit_EquipmentPersonQuality GetEquipmentPersonQualityByPersonId(string personId)
        {
            return Funs.DB.QualityAudit_EquipmentPersonQuality.FirstOrDefault(e => e.PersonId == personId);
        }

        /// <summary>
        /// 获取时间段的特岗资质集合
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public static List<Model.QualityAudit_EquipmentPersonQuality> GetListByDate(string projectId, DateTime startTime, DateTime endTime)
        {
            return (from x in Funs.DB.QualityAudit_EquipmentPersonQuality
                    join y in Funs.DB.SitePerson_Person
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
            var q = (from x in Funs.DB.QualityAudit_EquipmentPersonQuality 
                     join y in Funs.DB.SitePerson_Person
                     on x.PersonId equals y.PersonId
                     where y.ProjectId == projectId && x.AuditDate >= startTime && x.AuditDate <= endTime orderby x.AuditDate select x).Distinct().ToList();
            return q.Count();
        }

        /// <summary>
        /// 添加特种设备作业人员资质
        /// </summary>
        /// <param name="EquipmentPersonQuality"></param>
        public static void AddEquipmentPersonQuality(Model.QualityAudit_EquipmentPersonQuality EquipmentPersonQuality)
        {
            Model.SGGLDB db = Funs.DB;
            Model.QualityAudit_EquipmentPersonQuality newEquipmentPersonQuality = new Model.QualityAudit_EquipmentPersonQuality
            {
                EquipmentPersonQualityId = EquipmentPersonQuality.EquipmentPersonQualityId,
                PersonId = EquipmentPersonQuality.PersonId,
                CertificateId = EquipmentPersonQuality.CertificateId,
                CertificateNo = EquipmentPersonQuality.CertificateNo,
                CertificateName = EquipmentPersonQuality.CertificateName,
                Grade = EquipmentPersonQuality.Grade,
                SendUnit = EquipmentPersonQuality.SendUnit,
                SendDate = EquipmentPersonQuality.SendDate,
                LimitDate = EquipmentPersonQuality.LimitDate,
                LateCheckDate = EquipmentPersonQuality.LateCheckDate,
                ApprovalPerson = EquipmentPersonQuality.ApprovalPerson,
                Remark = EquipmentPersonQuality.Remark,
                CompileMan = EquipmentPersonQuality.CompileMan,
                CompileDate = EquipmentPersonQuality.CompileDate,
                AuditDate = EquipmentPersonQuality.AuditDate,
                AuditorId = EquipmentPersonQuality.AuditorId,
            };
            db.QualityAudit_EquipmentPersonQuality.InsertOnSubmit(newEquipmentPersonQuality);
            db.SubmitChanges();
            
        }

        /// <summary>
        /// 修改特种设备作业人员资质
        /// </summary>
        /// <param name="EquipmentPersonQuality"></param>
        public static void UpdateEquipmentPersonQuality(Model.QualityAudit_EquipmentPersonQuality EquipmentPersonQuality)
        {
            Model.SGGLDB db = Funs.DB;
            Model.QualityAudit_EquipmentPersonQuality newEquipmentPersonQuality = db.QualityAudit_EquipmentPersonQuality.FirstOrDefault(e => e.EquipmentPersonQualityId == EquipmentPersonQuality.EquipmentPersonQualityId);
            if (newEquipmentPersonQuality != null)
            {
                newEquipmentPersonQuality.CertificateId = EquipmentPersonQuality.CertificateId;
                newEquipmentPersonQuality.CertificateNo = EquipmentPersonQuality.CertificateNo;
                newEquipmentPersonQuality.CertificateName = EquipmentPersonQuality.CertificateName;
                newEquipmentPersonQuality.Grade = EquipmentPersonQuality.Grade;
                newEquipmentPersonQuality.SendUnit = EquipmentPersonQuality.SendUnit;
                newEquipmentPersonQuality.SendDate = EquipmentPersonQuality.SendDate;
                newEquipmentPersonQuality.LimitDate = EquipmentPersonQuality.LimitDate;
                newEquipmentPersonQuality.LateCheckDate = EquipmentPersonQuality.LateCheckDate;
                newEquipmentPersonQuality.ApprovalPerson = EquipmentPersonQuality.ApprovalPerson;
                newEquipmentPersonQuality.Remark = EquipmentPersonQuality.Remark;
                newEquipmentPersonQuality.CompileMan = EquipmentPersonQuality.CompileMan;
                newEquipmentPersonQuality.CompileDate = EquipmentPersonQuality.CompileDate;
                newEquipmentPersonQuality.AuditDate = EquipmentPersonQuality.AuditDate;
                newEquipmentPersonQuality.AuditorId = EquipmentPersonQuality.AuditorId;
                db.SubmitChanges();
            }
        }
    }
}
