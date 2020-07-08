using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 特殊机具设备资质
    /// </summary>
    public static class EquipmentQualityService
    {
        public static Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);

        /// <summary>
        /// 根据主键获取特殊机具设备资质
        /// </summary>
        /// <param name="equipmentQualityId"></param>
        /// <returns></returns>
        public static Model.QualityAudit_EquipmentQuality GetEquipmentQualityById(string equipmentQualityId)
        {
            return new Model.SGGLDB(Funs.ConnString).QualityAudit_EquipmentQuality.FirstOrDefault(e => e.EquipmentQualityId == equipmentQualityId);
        }

        /// <summary>
        /// 获取时间段的特种设备数量
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public static int GetCountByDate(string projectId, DateTime startTime, DateTime endTime)
        {
            var q = (from x in new Model.SGGLDB(Funs.ConnString).QualityAudit_EquipmentQuality where x.ProjectId == projectId && x.InDate >= startTime && x.InDate <= endTime orderby x.InDate select x).ToList();
            return q.Count();
        }

        /// <summary>
        /// 添加特殊机具设备资质
        /// </summary>
        /// <param name="equipmentQuality"></param>
        public static void AddEquipmentQuality(Model.QualityAudit_EquipmentQuality equipmentQuality)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.QualityAudit_EquipmentQuality newEquipmentQuality = new Model.QualityAudit_EquipmentQuality
            {
                EquipmentQualityId = equipmentQuality.EquipmentQualityId,
                ProjectId = equipmentQuality.ProjectId,
                EquipmentQualityCode = equipmentQuality.EquipmentQualityCode,
                UnitId = equipmentQuality.UnitId,
                SpecialEquipmentId = equipmentQuality.SpecialEquipmentId,
                EquipmentQualityName = equipmentQuality.EquipmentQualityName,
                SizeModel = equipmentQuality.SizeModel,
                FactoryCode = equipmentQuality.FactoryCode,
                CertificateCode = equipmentQuality.CertificateCode,
                CheckDate = equipmentQuality.CheckDate,
                LimitDate = equipmentQuality.LimitDate,
                InDate = equipmentQuality.InDate,
                OutDate = equipmentQuality.OutDate,
                ApprovalPerson = equipmentQuality.ApprovalPerson,
                CarNum = equipmentQuality.CarNum,
                Remark = equipmentQuality.Remark,
                CompileMan = equipmentQuality.CompileMan,
                CompileDate = equipmentQuality.CompileDate
            };
            db.QualityAudit_EquipmentQuality.InsertOnSubmit(newEquipmentQuality);
            db.SubmitChanges();
            CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(Const.EquipmentQualityMenuId, equipmentQuality.ProjectId, null, equipmentQuality.EquipmentQualityId, equipmentQuality.CompileDate);
        }

        /// <summary>
        /// 修改特殊机具设备资质
        /// </summary>
        /// <param name="equipmentQuality"></param>
        public static void UpdateEquipmentQuality(Model.QualityAudit_EquipmentQuality equipmentQuality)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.QualityAudit_EquipmentQuality newEquipmentQuality = db.QualityAudit_EquipmentQuality.FirstOrDefault(e => e.EquipmentQualityId == equipmentQuality.EquipmentQualityId);
            if (newEquipmentQuality != null)
            {
                newEquipmentQuality.ProjectId = equipmentQuality.ProjectId;
                newEquipmentQuality.EquipmentQualityCode = equipmentQuality.EquipmentQualityCode;
                newEquipmentQuality.UnitId = equipmentQuality.UnitId;
                newEquipmentQuality.SpecialEquipmentId = equipmentQuality.SpecialEquipmentId;
                newEquipmentQuality.EquipmentQualityName = equipmentQuality.EquipmentQualityName;
                newEquipmentQuality.SizeModel = equipmentQuality.SizeModel;
                newEquipmentQuality.FactoryCode = equipmentQuality.FactoryCode;
                newEquipmentQuality.CertificateCode = equipmentQuality.CertificateCode;
                newEquipmentQuality.CheckDate = equipmentQuality.CheckDate;
                newEquipmentQuality.LimitDate = equipmentQuality.LimitDate;
                newEquipmentQuality.InDate = equipmentQuality.InDate;
                newEquipmentQuality.OutDate = equipmentQuality.OutDate;
                newEquipmentQuality.ApprovalPerson = equipmentQuality.ApprovalPerson;
                newEquipmentQuality.CarNum = equipmentQuality.CarNum;
                newEquipmentQuality.Remark = equipmentQuality.Remark;
                newEquipmentQuality.CompileMan = equipmentQuality.CompileMan;
                newEquipmentQuality.CompileDate = equipmentQuality.CompileDate;
                newEquipmentQuality.QRCodeAttachUrl = equipmentQuality.QRCodeAttachUrl;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除特殊机具设备资质
        /// </summary>
        /// <param name="equipmentQualityId"></param>
        public static void DeleteEquipmentQualityById(string equipmentQualityId)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.QualityAudit_EquipmentQuality equipmentQuality = db.QualityAudit_EquipmentQuality.FirstOrDefault(e => e.EquipmentQualityId == equipmentQualityId);
            if (equipmentQuality != null)
            {
                CodeRecordsService.DeleteCodeRecordsByDataId(equipmentQualityId);//删除编号
                CommonService.DeleteAttachFileById(equipmentQualityId);//删除附件
                db.QualityAudit_EquipmentQuality.DeleteOnSubmit(equipmentQuality);
                db.SubmitChanges();
            }
        }
    }
}