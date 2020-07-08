using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 分包商资质审查明细
    /// </summary>
    public static class SubUnitQualityAuditDetailService
    {
        public static Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);

        /// <summary>
        /// 根据主键获取分包商资质审查明细
        /// </summary>
        /// <param name="SubUnitQualityAuditDetailId"></param>
        /// <returns></returns>
        public static Model.QualityAudit_SubUnitQualityAuditDetail GetSubUnitQualityAuditDetailById(string auditDetailId)
        {
            return new Model.SGGLDB(Funs.ConnString).QualityAudit_SubUnitQualityAuditDetail.FirstOrDefault(e => e.AuditDetailId == auditDetailId);
        }

        /// <summary>
        /// 根据单位ID获取分包商资质审查明细
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public static Model.QualityAudit_SubUnitQualityAuditDetail GetSubUnitQualityAuditDetailByUnitId(string unitId)
        {
            return new Model.SGGLDB(Funs.ConnString).QualityAudit_SubUnitQualityAuditDetail.FirstOrDefault(e => e.UnitId == unitId);
        }

        /// <summary>
        /// 获取时间段的审查明细集合
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public static List<Model.QualityAudit_SubUnitQualityAuditDetail> GetListByDate(string projectId, DateTime startTime, DateTime endTime)
        {
            return (from x in new Model.SGGLDB(Funs.ConnString).QualityAudit_SubUnitQualityAuditDetail where x.ProjectId == projectId && x.AuditDate >= startTime && x.AuditDate <= endTime orderby x.AuditDate select x).ToList();
        }

        /// <summary>
        /// 获取时间段的审查明细数量
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public static int GetCountByDate(string projectId, DateTime startTime, DateTime endTime)
        {
            var q = (from x in new Model.SGGLDB(Funs.ConnString).QualityAudit_SubUnitQualityAuditDetail where x.ProjectId == projectId && x.AuditDate >= startTime && x.AuditDate <= endTime orderby x.AuditDate select x).ToList();
            return q.Count();
        }

        /// <summary>
        /// 添加分包商资质审查明细
        /// </summary>
        /// <param name="auditDetail"></param>
        public static void AddSubUnitQualityAuditDetail(Model.QualityAudit_SubUnitQualityAuditDetail auditDetail)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.QualityAudit_SubUnitQualityAuditDetail newSubUnitQualityAuditDetail = new Model.QualityAudit_SubUnitQualityAuditDetail
            {
                AuditDetailId = auditDetail.AuditDetailId,
                ProjectId = auditDetail.ProjectId,
                UnitId = auditDetail.UnitId,
                AuditContent = auditDetail.AuditContent,
                AuditMan = auditDetail.AuditMan,
                AuditDate = auditDetail.AuditDate,
                AuditResult = auditDetail.AuditResult
            };
            db.QualityAudit_SubUnitQualityAuditDetail.InsertOnSubmit(newSubUnitQualityAuditDetail);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改分包商资质审查明细
        /// </summary>
        /// <param name="auditDetail"></param>
        public static void UpdateSubUnitQualityAuditDetail(Model.QualityAudit_SubUnitQualityAuditDetail auditDetail)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.QualityAudit_SubUnitQualityAuditDetail newSubUnitQualityAuditDetail = db.QualityAudit_SubUnitQualityAuditDetail.FirstOrDefault(e => e.AuditDetailId == auditDetail.AuditDetailId);
            if (newSubUnitQualityAuditDetail != null)
            {
                newSubUnitQualityAuditDetail.AuditContent = auditDetail.AuditContent;
                newSubUnitQualityAuditDetail.AuditMan = auditDetail.AuditMan;
                newSubUnitQualityAuditDetail.AuditDate = auditDetail.AuditDate;
                newSubUnitQualityAuditDetail.AuditResult = auditDetail.AuditResult;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除分包商资质审查明细
        /// </summary>
        /// <param name="auditDetailId"></param>
        public static void DeleteSubUnitQualityAuditDetailById(string auditDetailId)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.QualityAudit_SubUnitQualityAuditDetail auditDetail = db.QualityAudit_SubUnitQualityAuditDetail.FirstOrDefault(e => e.AuditDetailId == auditDetailId);
            if (auditDetail != null)
            {
                ////删除附件表
                BLL.CommonService.DeleteAttachFileById(auditDetail.AuditDetailId);
                db.QualityAudit_SubUnitQualityAuditDetail.DeleteOnSubmit(auditDetail);
                db.SubmitChanges();
            }
        }
    }
}
