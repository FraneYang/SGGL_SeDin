using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 一般机具设备资质
    /// </summary>
    public static class GeneralEquipmentQualityService
    {
        public static Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);

        /// <summary>
        /// 根据主键获取一般机具设备资质
        /// </summary>
        /// <param name="generalEquipmentQualityId"></param>
        /// <returns></returns>
        public static Model.QualityAudit_GeneralEquipmentQuality GetGeneralEquipmentQualityById(string generalEquipmentQualityId)
        {
            return new Model.SGGLDB(Funs.ConnString).QualityAudit_GeneralEquipmentQuality.FirstOrDefault(e => e.GeneralEquipmentQualityId == generalEquipmentQualityId);
        }

        /// <summary>
        /// 获取时间段的一般机具设备资质集合
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public static List<Model.QualityAudit_GeneralEquipmentQuality> GetListByDate(string projectId, DateTime startTime, DateTime endTime)
        {
            return (from x in new Model.SGGLDB(Funs.ConnString).QualityAudit_GeneralEquipmentQuality where x.ProjectId == projectId && x.InDate >= startTime && x.InDate <= endTime orderby x.InDate select x).ToList();
        }

        /// <summary>
        /// 获取时间段的一般机具设备资质数量
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public static int GetSumCountByDate(string projectId, DateTime startTime, DateTime endTime)
        {
            int sum = 0;
            var q= (from x in new Model.SGGLDB(Funs.ConnString).QualityAudit_GeneralEquipmentQuality where x.ProjectId == projectId && x.InDate >= startTime && x.InDate <= endTime select x).ToList();
            foreach (var item in q)
            {
                sum += item.EquipmentCount ?? 0;
            }
            return sum;
        }

        /// <summary>
        /// 添加一般机具设备资质
        /// </summary>
        /// <param name="generalEquipmentQuality"></param>
        public static void AddGeneralEquipmentQuality(Model.QualityAudit_GeneralEquipmentQuality generalEquipmentQuality)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.QualityAudit_GeneralEquipmentQuality newGeneralEquipmentQuality = new Model.QualityAudit_GeneralEquipmentQuality
            {
                GeneralEquipmentQualityId = generalEquipmentQuality.GeneralEquipmentQualityId,
                ProjectId = generalEquipmentQuality.ProjectId,
                GeneralEquipmentQualityCode = generalEquipmentQuality.GeneralEquipmentQualityCode,
                UnitId = generalEquipmentQuality.UnitId,
                SpecialEquipmentId = generalEquipmentQuality.SpecialEquipmentId,
                EquipmentCount = generalEquipmentQuality.EquipmentCount,
                IsQualified = generalEquipmentQuality.IsQualified,
                Remark = generalEquipmentQuality.Remark,
                CompileMan = generalEquipmentQuality.CompileMan,
                CompileDate = generalEquipmentQuality.CompileDate,
                InDate = generalEquipmentQuality.InDate
            };
            db.QualityAudit_GeneralEquipmentQuality.InsertOnSubmit(newGeneralEquipmentQuality);
            db.SubmitChanges();
            CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(Const.GeneralEquipmentQualityMenuId, generalEquipmentQuality.ProjectId, null, generalEquipmentQuality.GeneralEquipmentQualityId, generalEquipmentQuality.CompileDate);
        }

        /// <summary>
        /// 修改一般机具设备资质
        /// </summary>
        /// <param name="generalEquipmentQuality"></param>
        public static void UpdateGeneralEquipmentQuality(Model.QualityAudit_GeneralEquipmentQuality generalEquipmentQuality)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.QualityAudit_GeneralEquipmentQuality newGeneralEquipmentQuality = db.QualityAudit_GeneralEquipmentQuality.FirstOrDefault(e => e.GeneralEquipmentQualityId == generalEquipmentQuality.GeneralEquipmentQualityId);
            if (newGeneralEquipmentQuality != null)
            {
                newGeneralEquipmentQuality.ProjectId = generalEquipmentQuality.ProjectId;
                newGeneralEquipmentQuality.GeneralEquipmentQualityCode = generalEquipmentQuality.GeneralEquipmentQualityCode;
                newGeneralEquipmentQuality.UnitId = generalEquipmentQuality.UnitId;
                newGeneralEquipmentQuality.SpecialEquipmentId = generalEquipmentQuality.SpecialEquipmentId;
                newGeneralEquipmentQuality.EquipmentCount = generalEquipmentQuality.EquipmentCount;
                newGeneralEquipmentQuality.IsQualified = generalEquipmentQuality.IsQualified;
                newGeneralEquipmentQuality.Remark = generalEquipmentQuality.Remark;
                newGeneralEquipmentQuality.CompileMan = generalEquipmentQuality.CompileMan;
                newGeneralEquipmentQuality.CompileDate = generalEquipmentQuality.CompileDate;
                newGeneralEquipmentQuality.InDate = generalEquipmentQuality.InDate;
                newGeneralEquipmentQuality.QRCodeAttachUrl = generalEquipmentQuality.QRCodeAttachUrl;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除一般机具设备资质
        /// </summary>
        /// <param name="generalEquipmentQualityId"></param>
        public static void DeleteGeneralEquipmentQualityById(string generalEquipmentQualityId)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.QualityAudit_GeneralEquipmentQuality generalEquipmentQuality = db.QualityAudit_GeneralEquipmentQuality.FirstOrDefault(e => e.GeneralEquipmentQualityId == generalEquipmentQualityId);
            if (generalEquipmentQuality != null)
            {
                CodeRecordsService.DeleteCodeRecordsByDataId(generalEquipmentQualityId);
                CommonService.DeleteAttachFileById(generalEquipmentQualityId);
                db.QualityAudit_GeneralEquipmentQuality.DeleteOnSubmit(generalEquipmentQuality);
                db.SubmitChanges();
            }
        }
    }
}