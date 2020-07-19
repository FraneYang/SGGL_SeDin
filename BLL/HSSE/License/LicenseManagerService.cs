using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 安全许可证
    /// </summary>
    public static class LicenseManagerService
    {
        public static Model.SGGLDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取安全许可证
        /// </summary>
        /// <param name="licenseManagerId"></param>
        /// <returns></returns>
        public static Model.License_LicenseManager GetLicenseManagerById(string licenseManagerId)
        {
            return Funs.DB.License_LicenseManager.FirstOrDefault(e => e.LicenseManagerId == licenseManagerId);
        }

        /// <summary>
        /// 根据时间段获取HSE安全许可证集合
        /// </summary>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="projectId">项目Id</param>
        /// <returns>时间段内的HSE安全许可证集合</returns>
        public static int GetCountByDate(DateTime startTime, DateTime endTime, string projectId)
        {
            return (from x in Funs.DB.License_LicenseManager where x.CompileDate >= startTime && x.CompileDate <= endTime && x.ProjectId == projectId select x).Count();
        }

        /// <summary>
        /// 添加安全许可证
        /// </summary>
        /// <param name="licenseManager"></param>
        public static void AddLicenseManager(Model.License_LicenseManager licenseManager)
        {
            Model.SGGLDB db = Funs.DB;
            Model.License_LicenseManager newLicenseManager = new Model.License_LicenseManager
            {
                LicenseManagerId = licenseManager.LicenseManagerId,
                ProjectId = licenseManager.ProjectId,
                LicenseTypeId = licenseManager.LicenseTypeId,
                LicenseManagerCode = licenseManager.LicenseManagerCode,
                LicenseManageName = licenseManager.LicenseManageName,
                UnitId = licenseManager.UnitId,
                LicenseManageContents = licenseManager.LicenseManageContents,
                ApplicantMan = licenseManager.ApplicantMan,
                CompileMan = licenseManager.CompileMan,
                CompileDate = licenseManager.CompileDate,
                States = licenseManager.States,
                WorkAreaId = licenseManager.WorkAreaId,
                StartDate = licenseManager.StartDate,
                EndDate = licenseManager.EndDate,
                WorkStates=licenseManager.WorkStates,
            };
            db.License_LicenseManager.InsertOnSubmit(newLicenseManager);
            db.SubmitChanges();

            ////增加一条编码记录
            BLL.CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(BLL.Const.ProjectLicenseManagerMenuId, licenseManager.ProjectId, null, licenseManager.LicenseManagerId, licenseManager.CompileDate);
        }

        /// <summary>
        /// 修改安全许可证
        /// </summary>
        /// <param name="licenseManager"></param>
        public static void UpdateLicenseManager(Model.License_LicenseManager licenseManager)
        {
            Model.SGGLDB db = Funs.DB;
            Model.License_LicenseManager newLicenseManager = db.License_LicenseManager.FirstOrDefault(e => e.LicenseManagerId == licenseManager.LicenseManagerId);
            if (newLicenseManager != null)
            {
                newLicenseManager.LicenseTypeId = licenseManager.LicenseTypeId;
                newLicenseManager.LicenseManagerCode = licenseManager.LicenseManagerCode;
                newLicenseManager.LicenseManageName = licenseManager.LicenseManageName;
                newLicenseManager.UnitId = licenseManager.UnitId;
                newLicenseManager.LicenseManageContents = licenseManager.LicenseManageContents;
                newLicenseManager.ApplicantMan = licenseManager.ApplicantMan;
                newLicenseManager.CompileDate = licenseManager.CompileDate;
                newLicenseManager.States = licenseManager.States;
                newLicenseManager.WorkAreaId = licenseManager.WorkAreaId;
                newLicenseManager.StartDate = licenseManager.StartDate;
                newLicenseManager.EndDate = licenseManager.EndDate;
                newLicenseManager.WorkStates = licenseManager.WorkStates;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除安全许可证
        /// </summary>
        /// <param name="licenseManagerId"></param>
        public static void DeleteLicenseManagerById(string licenseManagerId)
        {
            Model.SGGLDB db = Funs.DB;
            Model.License_LicenseManager licenseManager = db.License_LicenseManager.FirstOrDefault(e => e.LicenseManagerId == licenseManagerId);
            if (licenseManager!=null)
            {
                ///删除编码表记录
                BLL.CodeRecordsService.DeleteCodeRecordsByDataId(licenseManagerId);
                BLL.CommonService.DeleteAttachFileById(licenseManagerId);//删除附件
                ///删除工程师日志收集记录
                var flowOperate = from x in db.Sys_FlowOperate where x.DataId == licenseManager.LicenseManagerId select x;
                if (flowOperate.Count() > 0)
                {
                    BLL.CommonService.DeleteFlowOperateByID(licenseManager.LicenseManagerId);  ////删除审核流程表
                }                     
                db.License_LicenseManager.DeleteOnSubmit(licenseManager);
                db.SubmitChanges();
            }
        }
    }
}
