using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 分包商资质
    /// </summary>
    public static class SubUnitQualityService
    {
        public static Model.SGGLDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取分包商资质
        /// </summary>
        /// <param name="SubUnitQualityId"></param>
        /// <returns></returns>
        public static Model.QualityAudit_SubUnitQuality GetSubUnitQualityById(string subUnitQualityId)
        {
            return Funs.DB.QualityAudit_SubUnitQuality.FirstOrDefault(e => e.SubUnitQualityId == subUnitQualityId);
        }

        /// <summary>
        /// 根据单位ID获取分包商资质
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public static Model.QualityAudit_SubUnitQuality GetSubUnitQualityByUnitId(string unitId)
        {
            return Funs.DB.QualityAudit_SubUnitQuality.FirstOrDefault(e => e.UnitId == unitId);
        }

        /// <summary>
        /// 添加分包商资质
        /// </summary>
        /// <param name="subUnitQuality"></param>
        public static void AddSubUnitQuality(Model.QualityAudit_SubUnitQuality subUnitQuality)
        {
            Model.SGGLDB db = Funs.DB;
            Model.QualityAudit_SubUnitQuality newSubUnitQuality = new Model.QualityAudit_SubUnitQuality
            {
                SubUnitQualityId = subUnitQuality.SubUnitQualityId,
                UnitId = subUnitQuality.UnitId,
                SubUnitQualityCode = subUnitQuality.SubUnitQualityCode,
                SubUnitQualityName = subUnitQuality.SubUnitQualityName,
                BusinessLicense = subUnitQuality.BusinessLicense,
                BL_EnableDate = subUnitQuality.BL_EnableDate,
                BL_ScanUrl = subUnitQuality.BL_ScanUrl,
                OrganCode = subUnitQuality.OrganCode,
                O_EnableDate = subUnitQuality.O_EnableDate,
                O_ScanUrl = subUnitQuality.O_ScanUrl,
                Certificate = subUnitQuality.Certificate,
                C_EnableDate = subUnitQuality.C_EnableDate,
                C_ScanUrl = subUnitQuality.C_ScanUrl,
                QualityLicense = subUnitQuality.QualityLicense,
                QL_EnableDate = subUnitQuality.QL_EnableDate,
                QL_ScanUrl = subUnitQuality.QL_ScanUrl,
                HSELicense = subUnitQuality.HSELicense,
                H_EnableDate = subUnitQuality.H_EnableDate,
                H_ScanUrl = subUnitQuality.H_ScanUrl,
                HSELicense2 = subUnitQuality.HSELicense2,
                H_EnableDate2 = subUnitQuality.H_EnableDate2,
                H_ScanUrl2 = subUnitQuality.H_ScanUrl2,
                SecurityLicense = subUnitQuality.SecurityLicense,
                SL_EnableDate = subUnitQuality.SL_EnableDate,
                SL_ScanUrl = subUnitQuality.SL_ScanUrl,
                CompileMan = subUnitQuality.CompileMan,
                CompileDate = subUnitQuality.CompileDate
            };
            db.QualityAudit_SubUnitQuality.InsertOnSubmit(newSubUnitQuality);
            db.SubmitChanges();            
        }

        /// <summary>
        /// 修改分包商资质
        /// </summary>
        /// <param name="subUnitQuality"></param>
        public static void UpdateSubUnitQuality(Model.QualityAudit_SubUnitQuality subUnitQuality)
        {
            Model.SGGLDB db = Funs.DB;
            Model.QualityAudit_SubUnitQuality newSubUnitQuality = db.QualityAudit_SubUnitQuality.FirstOrDefault(e => e.SubUnitQualityId == subUnitQuality.SubUnitQualityId);
            if (newSubUnitQuality != null)
            {                
                newSubUnitQuality.UnitId = subUnitQuality.UnitId;
                newSubUnitQuality.SubUnitQualityCode = subUnitQuality.SubUnitQualityCode;
                newSubUnitQuality.SubUnitQualityName = subUnitQuality.SubUnitQualityName;
                newSubUnitQuality.BusinessLicense = subUnitQuality.BusinessLicense;
                newSubUnitQuality.BL_EnableDate = subUnitQuality.BL_EnableDate;
                newSubUnitQuality.BL_ScanUrl = subUnitQuality.BL_ScanUrl;
                newSubUnitQuality.OrganCode = subUnitQuality.OrganCode;
                newSubUnitQuality.O_EnableDate = subUnitQuality.O_EnableDate;
                newSubUnitQuality.O_ScanUrl = subUnitQuality.O_ScanUrl;
                newSubUnitQuality.Certificate = subUnitQuality.Certificate;
                newSubUnitQuality.C_EnableDate = subUnitQuality.C_EnableDate;
                newSubUnitQuality.C_ScanUrl = subUnitQuality.C_ScanUrl;
                newSubUnitQuality.QualityLicense = subUnitQuality.QualityLicense;
                newSubUnitQuality.QL_EnableDate = subUnitQuality.QL_EnableDate;
                newSubUnitQuality.QL_ScanUrl = subUnitQuality.QL_ScanUrl;
                newSubUnitQuality.HSELicense = subUnitQuality.HSELicense;
                newSubUnitQuality.H_EnableDate = subUnitQuality.H_EnableDate;
                newSubUnitQuality.H_ScanUrl = subUnitQuality.H_ScanUrl;
                newSubUnitQuality.HSELicense2 = subUnitQuality.HSELicense2;
                newSubUnitQuality.H_EnableDate2 = subUnitQuality.H_EnableDate2;
                newSubUnitQuality.H_ScanUrl2 = subUnitQuality.H_ScanUrl2;
                newSubUnitQuality.SecurityLicense = subUnitQuality.SecurityLicense;
                newSubUnitQuality.SL_EnableDate = subUnitQuality.SL_EnableDate;
                newSubUnitQuality.SL_ScanUrl = subUnitQuality.SL_ScanUrl;
                newSubUnitQuality.CompileMan = subUnitQuality.CompileMan;
                newSubUnitQuality.CompileDate = subUnitQuality.CompileDate;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除分包商资质
        /// </summary>
        /// <param name="subUnitQualityId"></param>
        public static void DeleteSubUnitQualityById(string subUnitQualityId)
        {
            Model.SGGLDB db = Funs.DB;
            Model.QualityAudit_SubUnitQuality subUnitQuality = db.QualityAudit_SubUnitQuality.FirstOrDefault(e => e.SubUnitQualityId == subUnitQualityId);
            if (subUnitQuality != null)
            {
                if (!string.IsNullOrEmpty(subUnitQuality.BL_ScanUrl))
                {
                    UploadFileService.DeleteFile(Funs.RootPath, subUnitQuality.BL_ScanUrl);
                }
                if (!string.IsNullOrEmpty(subUnitQuality.O_ScanUrl))
                {
                    UploadFileService.DeleteFile(Funs.RootPath, subUnitQuality.O_ScanUrl);
                }
                if (!string.IsNullOrEmpty(subUnitQuality.C_ScanUrl))
                {
                    UploadFileService.DeleteFile(Funs.RootPath, subUnitQuality.C_ScanUrl);
                }
                if (!string.IsNullOrEmpty(subUnitQuality.QL_ScanUrl))
                {
                    UploadFileService.DeleteFile(Funs.RootPath, subUnitQuality.QL_ScanUrl);
                }
                if (!string.IsNullOrEmpty(subUnitQuality.H_ScanUrl))
                {
                    UploadFileService.DeleteFile(Funs.RootPath, subUnitQuality.H_ScanUrl);
                }
                if (!string.IsNullOrEmpty(subUnitQuality.H_ScanUrl2))
                {
                    UploadFileService.DeleteFile(Funs.RootPath, subUnitQuality.H_ScanUrl2);
                }
                if (!string.IsNullOrEmpty(subUnitQuality.SL_ScanUrl))
                {
                    UploadFileService.DeleteFile(Funs.RootPath, subUnitQuality.SL_ScanUrl);
                }
                CodeRecordsService.DeleteCodeRecordsByDataId(subUnitQualityId);
                db.QualityAudit_SubUnitQuality.DeleteOnSubmit(subUnitQuality);
                db.SubmitChanges();
            }
        }
    }
}