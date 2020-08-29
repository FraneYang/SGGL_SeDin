using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace BLL
{
    /// <summary>
    /// 作业票数据
    /// </summary>
    public static class APILicenseDataService
    {
        #region 根据主键ID获取作业票
        /// <summary>
        ///  根据主键ID获取作业票
        /// </summary>
        /// <param name="strMenuId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Model.LicenseDataItem getLicenseDataById(string strMenuId, string id)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                Model.LicenseDataItem getInfo = new Model.LicenseDataItem();
                #region 动火作业票
                if (strMenuId == Const.ProjectFireWorkMenuId)
                {
                    getInfo = (from x in db.License_FireWork
                               where x.FireWorkId == id
                               select new Model.LicenseDataItem
                               {
                                   LicenseId = x.FireWorkId,
                                   MenuId = strMenuId,
                                   ProjectId = x.ProjectId,
                                   LicenseCode = x.LicenseCode,
                                   ApplyUnitId = x.ApplyUnitId,
                                   ApplyUnitName = db.Base_Unit.First(u => u.UnitId == x.ApplyUnitId).UnitName,
                                   ApplyManId = x.ApplyManId,
                                   ApplyManName = db.Sys_User.First(u => u.UserId == x.ApplyManId).UserName,
                                   ApplyDate = string.Format("{0:yyyy-MM-dd HH:mm}", x.ApplyDate),
                                   WorkPalce = x.WorkPalce,
                                   FireWatchManId = x.FireWatchManId,
                                   FireWatchManName = x.FireWatchManName,
                                   ValidityStartTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.ValidityStartTime),
                                   ValidityEndTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.ValidityEndTime),
                                   WorkMeasures = x.WorkMeasures,
                                   CancelManId = x.CancelManId,
                                   CancelManName = db.Sys_User.First(u => u.UserId == x.CancelManId).UserName,
                                   CancelReasons = x.CancelReasons,
                                   CancelTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.CancelTime),
                                   CloseManId = x.CloseManId,
                                   CloseManName = db.Sys_User.First(u => u.UserId == x.CloseManId).UserName,
                                   CloseReasons = x.CloseReasons,
                                   CloseTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.CloseTime),
                                   NextManId = x.NextManId,
                                   NextManName = db.Sys_User.First(u => u.UserId == x.NextManId).UserName,
                                   AttachUrl = db.AttachFile.First(z => z.ToKeyId == x.FireWorkId).AttachUrl.Replace('\\', '/'),
                                   States = x.States,
                                   OldLicenseId = x.OldLicenseId,
                               }).FirstOrDefault();
                }
                #endregion
                #region 高处作业票
                else if (strMenuId == Const.ProjectHeightWorkMenuId)
                {
                    getInfo = (from x in db.License_HeightWork
                               where x.HeightWorkId == id
                               select new Model.LicenseDataItem
                               {
                                   LicenseId = x.HeightWorkId,
                                   MenuId = strMenuId,
                                   ProjectId = x.ProjectId,
                                   LicenseCode = x.LicenseCode,
                                   ApplyUnitId = x.ApplyUnitId,
                                   ApplyUnitName = db.Base_Unit.First(u => u.UnitId == x.ApplyUnitId).UnitName,
                                   ApplyManId = x.ApplyManId,
                                   ApplyManName = db.Sys_User.First(u => u.UserId == x.ApplyManId).UserName,
                                   ApplyDate = string.Format("{0:yyyy-MM-dd HH:mm}", x.ApplyDate),
                                   WorkPalce = x.WorkPalce,
                                   WorkType = x.WorkType,
                                   ValidityStartTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.ValidityStartTime),
                                   ValidityEndTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.ValidityEndTime),
                                   WorkMeasures = x.WorkMeasures,
                                   EquipmentTools = x.EquipmentTools,
                                   CancelManId = x.CancelManId,
                                   CancelManName = db.Sys_User.First(u => u.UserId == x.CancelManId).UserName,
                                   CancelReasons = x.CancelReasons,
                                   CancelTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.CancelTime),
                                   CloseManId = x.CloseManId,
                                   CloseManName = db.Sys_User.First(u => u.UserId == x.CloseManId).UserName,
                                   CloseReasons = x.CloseReasons,
                                   CloseTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.CloseTime),
                                   NextManId = x.NextManId,
                                   NextManName = db.Sys_User.First(u => u.UserId == x.NextManId).UserName,
                                   AttachUrl = db.AttachFile.First(z => z.ToKeyId == x.HeightWorkId).AttachUrl.Replace('\\', '/'),
                                   States = x.States,
                                   OldLicenseId = x.OldLicenseId,
                               }).FirstOrDefault();
                }
                #endregion
                #region 受限空间作业票
                if (strMenuId == Const.ProjectLimitedSpaceMenuId)
                {
                    getInfo = (from x in db.License_LimitedSpace
                               where x.LimitedSpaceId == id
                               select new Model.LicenseDataItem
                               {
                                   LicenseId = x.LimitedSpaceId,
                                   MenuId = strMenuId,
                                   ProjectId = x.ProjectId,
                                   LicenseCode = x.LicenseCode,
                                   ApplyUnitId = x.ApplyUnitId,
                                   ApplyUnitName = db.Base_Unit.First(u => u.UnitId == x.ApplyUnitId).UnitName,
                                   ApplyManId = x.ApplyManId,
                                   ApplyManName = db.Sys_User.First(u => u.UserId == x.ApplyManId).UserName,
                                   ApplyDate = string.Format("{0:yyyy-MM-dd HH:mm}", x.ApplyDate),
                                   WorkPalce = x.WorkPalce,
                                   FireWatchManId = x.FireWatchManId,
                                   FireWatchManName = x.FireWatchManName,
                                   ValidityStartTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.ValidityStartTime),
                                   ValidityEndTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.ValidityEndTime),
                                   WorkMeasures = x.WorkMeasures,
                                   CancelManId = x.CancelManId,
                                   CancelManName = db.Sys_User.First(u => u.UserId == x.CancelManId).UserName,
                                   CancelReasons = x.CancelReasons,
                                   CancelTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.CancelTime),
                                   CloseManId = x.CloseManId,
                                   CloseManName = db.Sys_User.First(u => u.UserId == x.CloseManId).UserName,
                                   CloseReasons = x.CloseReasons,
                                   CloseTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.CloseTime),
                                   NextManId = x.NextManId,
                                   NextManName = db.Sys_User.First(u => u.UserId == x.NextManId).UserName,
                                   AttachUrl = db.AttachFile.First(z => z.ToKeyId == x.LimitedSpaceId).AttachUrl.Replace('\\', '/'),
                                   States = x.States,
                                   OldLicenseId = x.OldLicenseId,
                               }).FirstOrDefault();
                }
                #endregion
                #region 射线作业票
                if (strMenuId == Const.ProjectRadialWorkMenuId)
                {
                    getInfo = (from x in db.License_RadialWork
                               where x.RadialWorkId == id
                               select new Model.LicenseDataItem
                               {
                                   LicenseId = x.RadialWorkId,
                                   MenuId = strMenuId,
                                   ProjectId = x.ProjectId,
                                   LicenseCode = x.LicenseCode,
                                   ApplyUnitId = x.ApplyUnitId,
                                   ApplyUnitName = db.Base_Unit.First(u => u.UnitId == x.ApplyUnitId).UnitName,
                                   ApplyManId = x.ApplyManId,
                                   ApplyManName = db.Sys_User.First(u => u.UserId == x.ApplyManId).UserName,
                                   ApplyDate = string.Format("{0:yyyy-MM-dd HH:mm}", x.ApplyDate),
                                   RadialType = x.RadialType,
                                   WorkLeaderId = x.WorkLeaderId,
                                   WorkLeaderName = db.Sys_User.First(u => u.UserId == x.WorkLeaderId).UserName,
                                   WorkLeaderTel = x.WorkLeaderTel,
                                   ValidityStartTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.ValidityStartTime),
                                   ValidityEndTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.ValidityEndTime),
                                   WorkPalce = x.WorkPalce,
                                   WorkMeasures = x.WorkMeasures,
                                   FireWatchManId = x.FireWatchManId,
                                   FireWatchManName = db.Sys_User.First(u => u.UserId == x.FireWatchManId).UserName,
                                   WatchManContact = x.WatchManContact,
                                   CancelManId = x.CancelManId,
                                   CancelManName = db.Sys_User.First(u => u.UserId == x.CancelManId).UserName,
                                   CancelReasons = x.CancelReasons,
                                   CancelTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.CancelTime),
                                   CloseManId = x.CloseManId,
                                   CloseManName = db.Sys_User.First(u => u.UserId == x.CloseManId).UserName,
                                   CloseReasons = x.CloseReasons,
                                   CloseTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.CloseTime),
                                   NextManId = x.NextManId,
                                   NextManName = db.Sys_User.First(u => u.UserId == x.NextManId).UserName,
                                   AttachUrl = db.AttachFile.First(z => z.ToKeyId == x.RadialWorkId).AttachUrl.Replace('\\', '/'),
                                   States = x.States,
                                   OldLicenseId = x.OldLicenseId,
                               }).FirstOrDefault();
                }
                #endregion
                #region 断路(占道)作业票
                if (strMenuId == Const.ProjectOpenCircuitMenuId)
                {
                    getInfo = (from x in db.License_OpenCircuit
                               where x.OpenCircuitId == id
                               select new Model.LicenseDataItem
                               {
                                   LicenseId = x.OpenCircuitId,
                                   MenuId = strMenuId,
                                   ProjectId = x.ProjectId,
                                   LicenseCode = x.LicenseCode,
                                   ApplyUnitId = x.ApplyUnitId,
                                   ApplyUnitName = db.Base_Unit.First(u => u.UnitId == x.ApplyUnitId).UnitName,
                                   ApplyManId = x.ApplyManId,
                                   ApplyManName = db.Sys_User.First(u => u.UserId == x.ApplyManId).UserName,
                                   ApplyDate = string.Format("{0:yyyy-MM-dd HH:mm}", x.ApplyDate),
                                   WorkPalce = x.WorkPalce,
                                   WorkMeasures = x.WorkMeasures,
                                   ValidityStartTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.ValidityStartTime),
                                   ValidityEndTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.ValidityEndTime),
                                   RoadName = x.RoadName,
                                   SafeMeasures = x.SafeMeasures,
                                   CancelManId = x.CancelManId,
                                   CancelManName = db.Sys_User.First(u => u.UserId == x.CancelManId).UserName,
                                   CancelReasons = x.CancelReasons,
                                   CancelTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.CancelTime),
                                   CloseManId = x.CloseManId,
                                   CloseManName = db.Sys_User.First(u => u.UserId == x.CloseManId).UserName,
                                   CloseReasons = x.CloseReasons,
                                   CloseTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.CloseTime),
                                   NextManId = x.NextManId,
                                   NextManName = db.Sys_User.First(u => u.UserId == x.NextManId).UserName,
                                   AttachUrl = db.AttachFile.First(z => z.ToKeyId == x.OpenCircuitId).AttachUrl.Replace('\\', '/'),
                                   States = x.States,
                                   OldLicenseId = x.OldLicenseId,
                               }).FirstOrDefault();
                }
                #endregion
                #region 动土作业票
                if (strMenuId == Const.ProjectBreakGroundMenuId)
                {
                    getInfo = (from x in db.License_BreakGround
                               where x.BreakGroundId == id
                               select new Model.LicenseDataItem
                               {
                                   LicenseId = x.BreakGroundId,
                                   MenuId = strMenuId,
                                   ProjectId = x.ProjectId,
                                   LicenseCode = x.LicenseCode,
                                   ApplyUnitId = x.ApplyUnitId,
                                   ApplyUnitName = db.Base_Unit.First(u => u.UnitId == x.ApplyUnitId).UnitName,
                                   ApplyManId = x.ApplyManId,
                                   ApplyManName = db.Sys_User.First(u => u.UserId == x.ApplyManId).UserName,
                                   ApplyDate = string.Format("{0:yyyy-MM-dd HH:mm}", x.ApplyDate),
                                   WorkPalce = x.WorkPalce,
                                   WorkDepth = x.WorkDepth,
                                   ValidityStartTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.ValidityStartTime),
                                   ValidityEndTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.ValidityEndTime),
                                   WorkMeasures = x.WorkMeasures,
                                   CancelManId = x.CancelManId,
                                   CancelManName = db.Sys_User.First(u => u.UserId == x.CancelManId).UserName,
                                   CancelReasons = x.CancelReasons,
                                   CancelTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.CancelTime),
                                   CloseManId = x.CloseManId,
                                   CloseManName = db.Sys_User.First(u => u.UserId == x.CloseManId).UserName,
                                   CloseReasons = x.CloseReasons,
                                   CloseTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.CloseTime),
                                   NextManId = x.NextManId,
                                   NextManName = db.Sys_User.First(u => u.UserId == x.NextManId).UserName,
                                   AttachUrl = db.AttachFile.First(z => z.ToKeyId == x.BreakGroundId).AttachUrl.Replace('\\', '/'),
                                   States = x.States,
                                   OldLicenseId = x.OldLicenseId,
                               }).FirstOrDefault();
                }
                #endregion
                #region 夜间施工作业票
                if (strMenuId == Const.ProjectNightWorkMenuId)
                {
                    getInfo = (from x in db.License_NightWork
                               where x.NightWorkId == id
                               select new Model.LicenseDataItem
                               {
                                   LicenseId = x.NightWorkId,
                                   MenuId = strMenuId,
                                   ProjectId = x.ProjectId,
                                   LicenseCode = x.LicenseCode,
                                   ApplyUnitId = x.ApplyUnitId,
                                   ApplyUnitName = db.Base_Unit.First(u => u.UnitId == x.ApplyUnitId).UnitName,
                                   ApplyManId = x.ApplyManId,
                                   ApplyManName = db.Sys_User.First(u => u.UserId == x.ApplyManId).UserName,
                                   ApplyDate = string.Format("{0:yyyy-MM-dd HH:mm}", x.ApplyDate),
                                   WorkPalce = x.WorkPalce,
                                   WorkMeasures = x.WorkMeasures,
                                   WorkLeaderId = x.WorkLeaderId,
                                   WorkLeaderName = db.Sys_User.First(u => u.UserId == x.WorkLeaderId).UserName,
                                   WorkLeaderTel = x.WorkLeaderTel,
                                   SafeLeaderId = x.SafeLeaderId,
                                   SafeLeaderName = db.Sys_User.First(u => u.UserId == x.SafeLeaderId).UserName,
                                   SafeLeaderTel = x.SafeLeaderTel,
                                   ValidityStartTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.ValidityStartTime),
                                   ValidityEndTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.ValidityEndTime),
                                   CancelManId = x.CancelManId,
                                   CancelManName = db.Sys_User.First(u => u.UserId == x.CancelManId).UserName,
                                   CancelReasons = x.CancelReasons,
                                   CancelTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.CancelTime),
                                   CloseManId = x.CloseManId,
                                   CloseManName = db.Sys_User.First(u => u.UserId == x.CloseManId).UserName,
                                   CloseReasons = x.CloseReasons,
                                   CloseTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.CloseTime),
                                   NextManId = x.NextManId,
                                   NextManName = db.Sys_User.First(u => u.UserId == x.NextManId).UserName,
                                   AttachUrl = db.AttachFile.First(z => z.ToKeyId == x.NightWorkId).AttachUrl.Replace('\\', '/'),
                                   States = x.States,
                                   OldLicenseId = x.OldLicenseId,
                               }).FirstOrDefault();
                }
                #endregion
                #region 吊装作业票
                if (strMenuId == Const.ProjectLiftingWorkMenuId)
                {
                    getInfo = (from x in db.License_LiftingWork
                               where x.LiftingWorkId == id
                               select new Model.LicenseDataItem
                               {
                                   LicenseId = x.LiftingWorkId,
                                   MenuId = strMenuId,
                                   ProjectId = x.ProjectId,
                                   LicenseCode = x.LicenseCode,
                                   ApplyUnitId = x.ApplyUnitId,
                                   ApplyUnitName = db.Base_Unit.First(u => u.UnitId == x.ApplyUnitId).UnitName,
                                   ApplyManId = x.ApplyManId,
                                   ApplyManName = db.Sys_User.First(u => u.UserId == x.ApplyManId).UserName,
                                   ApplyDate = string.Format("{0:yyyy-MM-dd HH:mm}", x.ApplyDate),
                                   WorkPalce = x.WorkPalce,
                                   WorkLevel = x.WorkLevel,
                                   ValidityStartTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.ValidityStartTime),
                                   ValidityEndTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.ValidityEndTime),
                                   WorkMeasures = x.WorkMeasures,
                                   CraneCapacity = x.CraneCapacity,
                                   CancelManId = x.CancelManId,
                                   CancelManName = db.Sys_User.First(u => u.UserId == x.CancelManId).UserName,
                                   CancelReasons = x.CancelReasons,
                                   CancelTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.CancelTime),
                                   CloseManId = x.CloseManId,
                                   CloseManName = db.Sys_User.First(u => u.UserId == x.CloseManId).UserName,
                                   CloseReasons = x.CloseReasons,
                                   CloseTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.CloseTime),
                                   NextManId = x.NextManId,
                                   NextManName = db.Sys_User.First(u => u.UserId == x.NextManId).UserName,
                                   AttachUrl = db.AttachFile.First(z => z.ToKeyId == x.LiftingWorkId).AttachUrl.Replace('\\', '/'),
                                   States = x.States,
                                   OldLicenseId = x.OldLicenseId,
                               }).FirstOrDefault();
                }
                #endregion

                #region 作业票【定稿】
                if (strMenuId == Const.ProjectLicenseManagerMenuId)
                {
                    getInfo = (from x in db.License_LicenseManager
                               where x.LicenseManagerId == id
                               select new Model.LicenseDataItem
                               {
                                   LicenseId = x.LicenseManagerId,
                                   MenuId = Const.ProjectLicenseManagerMenuId,
                                   MenuName = db.Base_LicenseType.FirstOrDefault(y => y.LicenseTypeId == x.LicenseTypeId).LicenseTypeName,
                                   LicenseTypeId = x.LicenseTypeId,
                                   ProjectId = x.ProjectId,
                                   LicenseCode = x.LicenseManagerCode,
                                   ApplyUnitId = x.UnitId,
                                   ApplyManId = x.CompileMan,
                                   ApplyUnitName = db.Base_Unit.First(u => u.UnitId == x.UnitId).UnitName,
                                   ApplyManName = x.ApplicantMan,
                                   ApplyDate = string.Format("{0:yyyy-MM-dd HH:mm}", x.CompileDate),
                                   WorkAreaIds = x.WorkAreaId,
                                   WorkPalce = UnitWorkService.GetUnitWorkName(x.WorkAreaId),
                                   ValidityStartTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.StartDate),
                                   ValidityEndTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.EndDate),
                                   States = x.WorkStates,
                                   AttachUrl = APIUpLoadFileService.getFileUrl(x.LicenseManagerId, null),
                               }).FirstOrDefault();
                }
                #endregion
                return getInfo;
            }
        }
        #endregion        

        #region 获取作业票列表信息
        /// <summary>
        /// 获取作业票列表信息
        /// </summary>
        /// <param name="strMenuId"></param>
        /// <param name="projectId"></param>
        /// <param name="unitId"></param>
        /// <param name="states"></param>
        /// <returns></returns>
        public static List<Model.LicenseDataItem> getLicenseDataList(string strMenuId, string projectId, string unitId, string states)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                if (!ProjectUnitService.GetProjectUnitTypeByProjectIdUnitId(projectId, unitId))
                {
                    unitId = null;
                }
                List<Model.LicenseDataItem> getInfoList = new List<Model.LicenseDataItem>();
                #region 动火作业票
                if (strMenuId == Const.ProjectFireWorkMenuId)
                {
                    getInfoList = (from x in db.License_FireWork
                                   where x.ProjectId == projectId && (x.ApplyUnitId == unitId || unitId == null)
                                        && (states == null || x.States == states)
                                   orderby x.LicenseCode descending
                                   select new Model.LicenseDataItem
                                   {
                                       LicenseId = x.FireWorkId,
                                       MenuId = strMenuId,
                                       ProjectId = x.ProjectId,
                                       LicenseCode = x.LicenseCode,
                                       ApplyUnitId = x.ApplyUnitId,
                                       ApplyUnitName = db.Base_Unit.First(u => u.UnitId == x.ApplyUnitId).UnitName,
                                       ApplyManId = x.ApplyManId,
                                       ApplyManName = db.Sys_User.First(u => u.UserId == x.ApplyManId).UserName,
                                       ApplyDate = string.Format("{0:yyyy-MM-dd HH:mm}", x.ApplyDate),
                                       WorkPalce = x.WorkPalce,
                                       FireWatchManId = x.FireWatchManId,
                                       FireWatchManName = x.FireWatchManName,
                                       ValidityStartTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.ValidityStartTime),
                                       ValidityEndTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.ValidityEndTime),
                                       WorkMeasures = x.WorkMeasures,
                                       CancelManId = x.CancelManId,
                                       CancelManName = db.Sys_User.First(u => u.UserId == x.CancelManId).UserName,
                                       CancelReasons = x.CancelReasons,
                                       CancelTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.CancelTime),
                                       CloseManId = x.CloseManId,
                                       CloseManName = db.Sys_User.First(u => u.UserId == x.CloseManId).UserName,
                                       CloseReasons = x.CloseReasons,
                                       CloseTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.CloseTime),
                                       States = x.States,
                                   }).ToList();
                }
                #endregion
                #region 高处作业票
                else if (strMenuId == Const.ProjectHeightWorkMenuId)
                {
                    getInfoList = (from x in db.License_HeightWork
                                   where x.ProjectId == projectId && (x.ApplyUnitId == unitId || unitId == null)
                                        && (states == null || x.States == states)
                                   orderby x.LicenseCode descending
                                   select new Model.LicenseDataItem
                                   {
                                       LicenseId = x.HeightWorkId,
                                       MenuId = strMenuId,
                                       ProjectId = x.ProjectId,
                                       LicenseCode = x.LicenseCode,
                                       ApplyUnitId = x.ApplyUnitId,
                                       ApplyUnitName = db.Base_Unit.First(u => u.UnitId == x.ApplyUnitId).UnitName,
                                       ApplyManId = x.ApplyManId,
                                       ApplyManName = db.Sys_User.First(u => u.UserId == x.ApplyManId).UserName,
                                       ApplyDate = string.Format("{0:yyyy-MM-dd HH:mm}", x.ApplyDate),
                                       WorkPalce = x.WorkPalce,
                                       WorkType = x.WorkType,
                                       ValidityStartTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.ValidityStartTime),
                                       ValidityEndTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.ValidityEndTime),
                                       WorkMeasures = x.WorkMeasures,
                                       EquipmentTools = x.EquipmentTools,
                                       CancelManId = x.CancelManId,
                                       CancelManName = db.Sys_User.First(u => u.UserId == x.CancelManId).UserName,
                                       CancelReasons = x.CancelReasons,
                                       CancelTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.CancelTime),
                                       CloseManId = x.CloseManId,
                                       CloseManName = db.Sys_User.First(u => u.UserId == x.CloseManId).UserName,
                                       CloseReasons = x.CloseReasons,
                                       CloseTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.CloseTime),
                                       States = x.States,
                                   }).ToList();
                }
                #endregion
                #region 受限空间作业票
                if (strMenuId == Const.ProjectLimitedSpaceMenuId)
                {
                    getInfoList = (from x in db.License_LimitedSpace
                                   where x.ProjectId == projectId && (x.ApplyUnitId == unitId || unitId == null)
                                        && (states == null || x.States == states)
                                   orderby x.LicenseCode descending
                                   select new Model.LicenseDataItem
                                   {
                                       LicenseId = x.LimitedSpaceId,
                                       MenuId = strMenuId,
                                       ProjectId = x.ProjectId,
                                       LicenseCode = x.LicenseCode,
                                       ApplyUnitId = x.ApplyUnitId,
                                       ApplyUnitName = db.Base_Unit.First(u => u.UnitId == x.ApplyUnitId).UnitName,
                                       ApplyManId = x.ApplyManId,
                                       ApplyManName = db.Sys_User.First(u => u.UserId == x.ApplyManId).UserName,
                                       ApplyDate = string.Format("{0:yyyy-MM-dd HH:mm}", x.ApplyDate),
                                       WorkPalce = x.WorkPalce,
                                       FireWatchManId = x.FireWatchManId,
                                       FireWatchManName = x.FireWatchManName,
                                       ValidityStartTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.ValidityStartTime),
                                       ValidityEndTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.ValidityEndTime),
                                       WorkMeasures = x.WorkMeasures,
                                       CancelManId = x.CancelManId,
                                       CancelManName = db.Sys_User.First(u => u.UserId == x.CancelManId).UserName,
                                       CancelReasons = x.CancelReasons,
                                       CancelTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.CancelTime),
                                       CloseManId = x.CloseManId,
                                       CloseManName = db.Sys_User.First(u => u.UserId == x.CloseManId).UserName,
                                       CloseReasons = x.CloseReasons,
                                       CloseTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.CloseTime),
                                       States = x.States,
                                   }).ToList();
                }
                #endregion
                #region 射线作业票
                if (strMenuId == Const.ProjectRadialWorkMenuId)
                {
                    getInfoList = (from x in db.License_RadialWork
                                   where x.ProjectId == projectId && (x.ApplyUnitId == unitId || unitId == null)
                                        && (states == null || x.States == states)
                                   orderby x.LicenseCode descending
                                   select new Model.LicenseDataItem
                                   {
                                       LicenseId = x.RadialWorkId,
                                       MenuId = strMenuId,
                                       ProjectId = x.ProjectId,
                                       LicenseCode = x.LicenseCode,
                                       ApplyUnitId = x.ApplyUnitId,
                                       ApplyUnitName = db.Base_Unit.First(u => u.UnitId == x.ApplyUnitId).UnitName,
                                       ApplyManId = x.ApplyManId,
                                       ApplyManName = db.Sys_User.First(u => u.UserId == x.ApplyManId).UserName,
                                       ApplyDate = string.Format("{0:yyyy-MM-dd HH:mm}", x.ApplyDate),
                                       RadialType = x.RadialType,
                                       WorkLeaderId = x.WorkLeaderId,
                                       WorkLeaderName = db.Sys_User.First(u => u.UserId == x.WorkLeaderId).UserName,
                                       WorkLeaderTel = x.WorkLeaderTel,
                                       ValidityStartTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.ValidityStartTime),
                                       ValidityEndTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.ValidityEndTime),
                                       WorkPalce = x.WorkPalce,
                                       WorkMeasures = x.WorkMeasures,
                                       FireWatchManId = x.FireWatchManId,
                                       FireWatchManName = db.Sys_User.First(u => u.UserId == x.FireWatchManId).UserName,
                                       WatchManContact = x.WatchManContact,
                                       CancelManId = x.CancelManId,
                                       CancelManName = db.Sys_User.First(u => u.UserId == x.CancelManId).UserName,
                                       CancelReasons = x.CancelReasons,
                                       CancelTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.CancelTime),
                                       CloseManId = x.CloseManId,
                                       CloseManName = db.Sys_User.First(u => u.UserId == x.CloseManId).UserName,
                                       CloseReasons = x.CloseReasons,
                                       CloseTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.CloseTime),
                                       NextManId = x.NextManId,
                                       NextManName = db.Sys_User.First(u => u.UserId == x.NextManId).UserName,
                                       States = x.States,
                                   }).ToList();
                }
                #endregion
                #region 断路(占道)作业票
                if (strMenuId == Const.ProjectOpenCircuitMenuId)
                {
                    getInfoList = (from x in db.License_OpenCircuit
                                   where x.ProjectId == projectId && (x.ApplyUnitId == unitId || unitId == null)
                                        && (states == null || x.States == states)
                                   orderby x.LicenseCode descending
                                   select new Model.LicenseDataItem
                                   {
                                       LicenseId = x.OpenCircuitId,
                                       MenuId = strMenuId,
                                       ProjectId = x.ProjectId,
                                       LicenseCode = x.LicenseCode,
                                       ApplyUnitId = x.ApplyUnitId,
                                       ApplyUnitName = db.Base_Unit.First(u => u.UnitId == x.ApplyUnitId).UnitName,
                                       ApplyManId = x.ApplyManId,
                                       ApplyManName = db.Sys_User.First(u => u.UserId == x.ApplyManId).UserName,
                                       ApplyDate = string.Format("{0:yyyy-MM-dd HH:mm}", x.ApplyDate),
                                       WorkPalce = x.WorkPalce,
                                       WorkMeasures = x.WorkMeasures,
                                       ValidityStartTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.ValidityStartTime),
                                       ValidityEndTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.ValidityEndTime),
                                       RoadName = x.RoadName,
                                       SafeMeasures = x.SafeMeasures,
                                       CancelManId = x.CancelManId,
                                       CancelManName = db.Sys_User.First(u => u.UserId == x.CancelManId).UserName,
                                       CancelReasons = x.CancelReasons,
                                       CancelTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.CancelTime),
                                       CloseManId = x.CloseManId,
                                       CloseManName = db.Sys_User.First(u => u.UserId == x.CloseManId).UserName,
                                       CloseReasons = x.CloseReasons,
                                       CloseTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.CloseTime),
                                       States = x.States,
                                   }).ToList();
                }
                #endregion
                #region 动土作业票
                if (strMenuId == Const.ProjectBreakGroundMenuId)
                {
                    getInfoList = (from x in db.License_BreakGround
                                   where x.ProjectId == projectId && (x.ApplyUnitId == unitId || unitId == null)
                                        && (states == null || x.States == states)
                                   orderby x.LicenseCode descending
                                   select new Model.LicenseDataItem
                                   {
                                       LicenseId = x.BreakGroundId,
                                       MenuId = strMenuId,
                                       ProjectId = x.ProjectId,
                                       LicenseCode = x.LicenseCode,
                                       ApplyUnitId = x.ApplyUnitId,
                                       ApplyUnitName = db.Base_Unit.First(u => u.UnitId == x.ApplyUnitId).UnitName,
                                       ApplyManId = x.ApplyManId,
                                       ApplyManName = db.Sys_User.First(u => u.UserId == x.ApplyManId).UserName,
                                       ApplyDate = string.Format("{0:yyyy-MM-dd HH:mm}", x.ApplyDate),
                                       WorkPalce = x.WorkPalce,
                                       WorkDepth = x.WorkDepth,
                                       ValidityStartTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.ValidityStartTime),
                                       ValidityEndTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.ValidityEndTime),
                                       WorkMeasures = x.WorkMeasures,
                                       CancelManId = x.CancelManId,
                                       CancelManName = db.Sys_User.First(u => u.UserId == x.CancelManId).UserName,
                                       CancelReasons = x.CancelReasons,
                                       CancelTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.CancelTime),
                                       CloseManId = x.CloseManId,
                                       CloseManName = db.Sys_User.First(u => u.UserId == x.CloseManId).UserName,
                                       CloseReasons = x.CloseReasons,
                                       CloseTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.CloseTime),
                                       States = x.States,
                                   }).ToList();
                }
                #endregion
                #region 夜间作业票
                if (strMenuId == Const.ProjectNightWorkMenuId)
                {
                    getInfoList = (from x in db.License_NightWork
                                   where x.ProjectId == projectId && (x.ApplyUnitId == unitId || unitId == null)
                                        && (states == null || x.States == states)
                                   orderby x.LicenseCode descending
                                   select new Model.LicenseDataItem
                                   {
                                       LicenseId = x.NightWorkId,
                                       MenuId = strMenuId,
                                       ProjectId = x.ProjectId,
                                       LicenseCode = x.LicenseCode,
                                       ApplyUnitId = x.ApplyUnitId,
                                       ApplyUnitName = db.Base_Unit.First(u => u.UnitId == x.ApplyUnitId).UnitName,
                                       ApplyManId = x.ApplyManId,
                                       ApplyManName = db.Sys_User.First(u => u.UserId == x.ApplyManId).UserName,
                                       ApplyDate = string.Format("{0:yyyy-MM-dd HH:mm}", x.ApplyDate),
                                       WorkPalce = x.WorkPalce,
                                       WorkMeasures = x.WorkMeasures,
                                       WorkLeaderId = x.WorkLeaderId,
                                       WorkLeaderName = db.Sys_User.First(u => u.UserId == x.WorkLeaderId).UserName,
                                       WorkLeaderTel = x.WorkLeaderTel,
                                       SafeLeaderId = x.SafeLeaderId,
                                       SafeLeaderName = db.Sys_User.First(u => u.UserId == x.SafeLeaderId).UserName,
                                       SafeLeaderTel = x.SafeLeaderTel,
                                       ValidityStartTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.ValidityStartTime),
                                       ValidityEndTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.ValidityEndTime),
                                       CancelManId = x.CancelManId,
                                       CancelManName = db.Sys_User.First(u => u.UserId == x.CancelManId).UserName,
                                       CancelReasons = x.CancelReasons,
                                       CancelTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.CancelTime),
                                       CloseManId = x.CloseManId,
                                       CloseManName = db.Sys_User.First(u => u.UserId == x.CloseManId).UserName,
                                       CloseReasons = x.CloseReasons,
                                       CloseTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.CloseTime),
                                       NextManId = x.NextManId,
                                       NextManName = db.Sys_User.First(u => u.UserId == x.NextManId).UserName,
                                       States = x.States,
                                   }).ToList();
                }
                #endregion
                #region 吊装作业票
                if (strMenuId == Const.ProjectLiftingWorkMenuId)
                {
                    getInfoList = (from x in db.License_LiftingWork
                                   where x.ProjectId == projectId && (x.ApplyUnitId == unitId || unitId == null)
                                        && (states == null || x.States == states)
                                   orderby x.LicenseCode descending
                                   select new Model.LicenseDataItem
                                   {
                                       LicenseId = x.LiftingWorkId,
                                       MenuId = strMenuId,
                                       ProjectId = x.ProjectId,
                                       LicenseCode = x.LicenseCode,
                                       ApplyUnitId = x.ApplyUnitId,
                                       ApplyUnitName = db.Base_Unit.First(u => u.UnitId == x.ApplyUnitId).UnitName,
                                       ApplyManId = x.ApplyManId,
                                       ApplyManName = db.Sys_User.First(u => u.UserId == x.ApplyManId).UserName,
                                       ApplyDate = string.Format("{0:yyyy-MM-dd HH:mm}", x.ApplyDate),
                                       WorkPalce = x.WorkPalce,
                                       WorkLevel = x.WorkLevel,
                                       ValidityStartTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.ValidityStartTime),
                                       ValidityEndTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.ValidityEndTime),
                                       WorkMeasures = x.WorkMeasures,
                                       CraneCapacity = x.CraneCapacity,
                                       CancelManId = x.CancelManId,
                                       CancelManName = db.Sys_User.First(u => u.UserId == x.CancelManId).UserName,
                                       CancelReasons = x.CancelReasons,
                                       CancelTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.CancelTime),
                                       CloseManId = x.CloseManId,
                                       CloseManName = db.Sys_User.First(u => u.UserId == x.CloseManId).UserName,
                                       CloseReasons = x.CloseReasons,
                                       CloseTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.CloseTime),
                                       NextManId = x.NextManId,
                                       NextManName = db.Sys_User.First(u => u.UserId == x.NextManId).UserName,
                                       States = x.States,
                                   }).ToList();
                }
                #endregion

                #region 作业票 【定稿】"待开工"="1";"作业中"="2";"已关闭"="3";"已取消"="-1"
                if (strMenuId == Const.ProjectLicenseManagerMenuId)
                {
                    getInfoList = (from x in db.License_LicenseManager
                                   where x.ProjectId == projectId && (x.UnitId == unitId || unitId == null)
                                        && (states == null || x.WorkStates == states || (states == "1" && (x.WorkStates == "1" || x.WorkStates == "0")))
                                   select new Model.LicenseDataItem
                                   {
                                       LicenseId = x.LicenseManagerId,
                                       MenuId = Const.ProjectLicenseManagerMenuId,
                                       MenuName = db.Base_LicenseType.FirstOrDefault(y => y.LicenseTypeId == x.LicenseTypeId).LicenseTypeName,
                                       LicenseTypeId = x.LicenseTypeId,
                                       ProjectId = x.ProjectId,
                                       LicenseCode = x.LicenseManagerCode,
                                       ApplyUnitId = x.UnitId,
                                       ApplyUnitName = db.Base_Unit.First(u => u.UnitId == x.UnitId).UnitName,
                                       ApplyManId = x.CompileMan,
                                       ApplyManName = x.ApplicantMan,
                                       ApplyDate = string.Format("{0:yyyy-MM-dd HH:mm}", x.CompileDate),
                                       WorkPalce = UnitWorkService.GetUnitWorkName(x.WorkAreaId),
                                       ValidityStartTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.StartDate),
                                       ValidityEndTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.EndDate),
                                       States = x.WorkStates,
                                       AttachUrl = APIUpLoadFileService.getFileUrl(x.LicenseManagerId, null),
                                   }).ToList(); ;
                }
                #endregion

                return getInfoList;
            }
        }
        #endregion 

        #region 获取作业票列表信息-按状态
        /// <summary>
        /// 获取作业票列表信息-按状态
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="unitId"></param>
        /// <param name="states"></param>
        /// <returns></returns>
        public static List<Model.LicenseDataItem> getLicenseDataListByStates(string projectId, string unitId, string states)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                if (!ProjectUnitService.GetProjectUnitTypeByProjectIdUnitId(projectId, unitId))
                {
                    unitId = null;
                }
                List<Model.LicenseDataItem> getInfoList = new List<Model.LicenseDataItem>();

                #region 动火作业票
                var getFireWork = (from x in db.License_FireWork
                                   where x.ProjectId == projectId && (x.ApplyUnitId == unitId || unitId == null)
                                        && (states == null || x.States == states)
                                   select new Model.LicenseDataItem
                                   {
                                       LicenseId = x.FireWorkId,
                                       MenuId = Const.ProjectFireWorkMenuId,
                                       MenuName = "动火作业",
                                       ProjectId = x.ProjectId,
                                       LicenseCode = x.LicenseCode,
                                       ApplyUnitId = x.ApplyUnitId,
                                       ApplyUnitName = db.Base_Unit.First(u => u.UnitId == x.ApplyUnitId).UnitName,
                                       ApplyManId = x.ApplyManId,
                                       ApplyManName = db.Sys_User.First(u => u.UserId == x.ApplyManId).UserName,
                                       ApplyDate = string.Format("{0:yyyy-MM-dd HH:mm}", x.ApplyDate),
                                       WorkPalce = x.WorkPalce,
                                       ValidityStartTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.ValidityStartTime),
                                       ValidityEndTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.ValidityEndTime),
                                       States = x.States,
                                   }).ToList();
                if (getFireWork.Count() > 0)
                {
                    getInfoList.AddRange(getFireWork);
                }
                #endregion

                #region 高处作业票            
                var getHeightWork = (from x in db.License_HeightWork
                                     where x.ProjectId == projectId && (x.ApplyUnitId == unitId || unitId == null)
                                          && (states == null || x.States == states)
                                     select new Model.LicenseDataItem
                                     {
                                         LicenseId = x.HeightWorkId,
                                         MenuId = Const.ProjectHeightWorkMenuId,
                                         MenuName = "高处作业",
                                         ProjectId = x.ProjectId,
                                         LicenseCode = x.LicenseCode,
                                         ApplyUnitId = x.ApplyUnitId,
                                         ApplyUnitName = db.Base_Unit.First(u => u.UnitId == x.ApplyUnitId).UnitName,
                                         ApplyManId = x.ApplyManId,
                                         ApplyManName = db.Sys_User.First(u => u.UserId == x.ApplyManId).UserName,
                                         ApplyDate = string.Format("{0:yyyy-MM-dd HH:mm}", x.ApplyDate),
                                         WorkPalce = x.WorkPalce,
                                         WorkType = x.WorkType,
                                         ValidityStartTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.ValidityStartTime),
                                         ValidityEndTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.ValidityEndTime),
                                         States = x.States,
                                     }).ToList();
                if (getHeightWork.Count() > 0)
                {
                    getInfoList.AddRange(getHeightWork);
                }
                #endregion

                #region 受限空间作业票
                var getLimitedSpace = (from x in db.License_LimitedSpace
                                       where x.ProjectId == projectId && (x.ApplyUnitId == unitId || unitId == null)
                                            && (states == null || x.States == states)
                                       select new Model.LicenseDataItem
                                       {
                                           LicenseId = x.LimitedSpaceId,
                                           MenuId = Const.ProjectLimitedSpaceMenuId,
                                           MenuName = "受限空间",
                                           ProjectId = x.ProjectId,
                                           LicenseCode = x.LicenseCode,
                                           ApplyUnitId = x.ApplyUnitId,
                                           ApplyUnitName = db.Base_Unit.First(u => u.UnitId == x.ApplyUnitId).UnitName,
                                           ApplyManId = x.ApplyManId,
                                           ApplyManName = db.Sys_User.First(u => u.UserId == x.ApplyManId).UserName,
                                           ApplyDate = string.Format("{0:yyyy-MM-dd HH:mm}", x.ApplyDate),
                                           WorkPalce = x.WorkPalce,
                                           ValidityStartTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.ValidityStartTime),
                                           ValidityEndTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.ValidityEndTime),
                                           States = x.States,
                                       }).ToList();
                if (getLimitedSpace.Count() > 0)
                {
                    getInfoList.AddRange(getLimitedSpace);
                }
                #endregion

                #region 射线作业票
                var getRadialWork = (from x in db.License_RadialWork
                                     where x.ProjectId == projectId && (x.ApplyUnitId == unitId || unitId == null)
                                          && (states == null || x.States == states)
                                     select new Model.LicenseDataItem
                                     {
                                         LicenseId = x.RadialWorkId,
                                         MenuId = Const.ProjectRadialWorkMenuId,
                                         MenuName = "射线作业",
                                         ProjectId = x.ProjectId,
                                         LicenseCode = x.LicenseCode,
                                         ApplyUnitId = x.ApplyUnitId,
                                         ApplyUnitName = db.Base_Unit.First(u => u.UnitId == x.ApplyUnitId).UnitName,
                                         ApplyManId = x.ApplyManId,
                                         ApplyManName = db.Sys_User.First(u => u.UserId == x.ApplyManId).UserName,
                                         ApplyDate = string.Format("{0:yyyy-MM-dd HH:mm}", x.ApplyDate),
                                         RadialType = x.RadialType,
                                         ValidityStartTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.ValidityStartTime),
                                         ValidityEndTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.ValidityEndTime),
                                         WorkPalce = x.WorkPalce,
                                         States = x.States,
                                     }).ToList();
                if (getRadialWork.Count() > 0)
                {
                    getInfoList.AddRange(getRadialWork);
                }
                #endregion

                #region 断路(占道)作业票
                var getOpenCircuit = (from x in db.License_OpenCircuit
                                      where x.ProjectId == projectId && (x.ApplyUnitId == unitId || unitId == null)
                                          && (states == null || x.States == states)
                                      select new Model.LicenseDataItem
                                      {
                                          LicenseId = x.OpenCircuitId,
                                          MenuId = Const.ProjectOpenCircuitMenuId,
                                          MenuName = "断路(占道)",
                                          ProjectId = x.ProjectId,
                                          LicenseCode = x.LicenseCode,
                                          ApplyUnitId = x.ApplyUnitId,
                                          ApplyUnitName = db.Base_Unit.First(u => u.UnitId == x.ApplyUnitId).UnitName,
                                          ApplyManId = x.ApplyManId,
                                          ApplyManName = db.Sys_User.First(u => u.UserId == x.ApplyManId).UserName,
                                          ApplyDate = string.Format("{0:yyyy-MM-dd HH:mm}", x.ApplyDate),
                                          WorkPalce = x.WorkPalce,
                                          ValidityStartTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.ValidityStartTime),
                                          ValidityEndTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.ValidityEndTime),
                                          RoadName = x.RoadName,
                                          States = x.States,
                                      }).ToList();
                if (getOpenCircuit.Count() > 0)
                {
                    getInfoList.AddRange(getOpenCircuit);
                }
                #endregion

                #region 动土作业票            
                var getBreakGround = (from x in db.License_BreakGround
                                      where x.ProjectId == projectId && (x.ApplyUnitId == unitId || unitId == null)
                                           && (states == null || x.States == states)
                                      select new Model.LicenseDataItem
                                      {
                                          LicenseId = x.BreakGroundId,
                                          MenuId = Const.ProjectBreakGroundMenuId,
                                          MenuName = "动土作业",
                                          ProjectId = x.ProjectId,
                                          LicenseCode = x.LicenseCode,
                                          ApplyUnitId = x.ApplyUnitId,
                                          ApplyUnitName = db.Base_Unit.First(u => u.UnitId == x.ApplyUnitId).UnitName,
                                          ApplyManId = x.ApplyManId,
                                          ApplyManName = db.Sys_User.First(u => u.UserId == x.ApplyManId).UserName,
                                          ApplyDate = string.Format("{0:yyyy-MM-dd HH:mm}", x.ApplyDate),
                                          WorkPalce = x.WorkPalce,
                                          WorkDepth = x.WorkDepth,
                                          ValidityStartTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.ValidityStartTime),
                                          ValidityEndTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.ValidityEndTime),
                                          States = x.States,
                                      }).ToList();
                if (getBreakGround.Count() > 0)
                {
                    getInfoList.AddRange(getBreakGround);
                }
                #endregion

                #region 夜间作业票
                var getNightWork = (from x in db.License_NightWork
                                    where x.ProjectId == projectId && (x.ApplyUnitId == unitId || unitId == null)
                                         && (states == null || x.States == states)
                                    select new Model.LicenseDataItem
                                    {
                                        LicenseId = x.NightWorkId,
                                        MenuId = Const.ProjectNightWorkMenuId,
                                        MenuName = "夜间作业",
                                        ProjectId = x.ProjectId,
                                        LicenseCode = x.LicenseCode,
                                        ApplyUnitId = x.ApplyUnitId,
                                        ApplyUnitName = db.Base_Unit.First(u => u.UnitId == x.ApplyUnitId).UnitName,
                                        ApplyManId = x.ApplyManId,
                                        ApplyManName = db.Sys_User.First(u => u.UserId == x.ApplyManId).UserName,
                                        ApplyDate = string.Format("{0:yyyy-MM-dd HH:mm}", x.ApplyDate),
                                        WorkPalce = x.WorkPalce,
                                        ValidityStartTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.ValidityStartTime),
                                        ValidityEndTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.ValidityEndTime),
                                        States = x.States,
                                    }).ToList();
                if (getNightWork.Count() > 0)
                {
                    getInfoList.AddRange(getNightWork);
                }
                #endregion

                #region 吊装作业票
                var getLiftingWork = (from x in db.License_LiftingWork
                                      where x.ProjectId == projectId && (x.ApplyUnitId == unitId || unitId == null)
                                           && (states == null || x.States == states)
                                      select new Model.LicenseDataItem
                                      {
                                          LicenseId = x.LiftingWorkId,
                                          MenuId = Const.ProjectLiftingWorkMenuId,
                                          MenuName = "吊装作业",
                                          ProjectId = x.ProjectId,
                                          LicenseCode = x.LicenseCode,
                                          ApplyUnitId = x.ApplyUnitId,
                                          ApplyUnitName = db.Base_Unit.First(u => u.UnitId == x.ApplyUnitId).UnitName,
                                          ApplyManId = x.ApplyManId,
                                          ApplyManName = db.Sys_User.First(u => u.UserId == x.ApplyManId).UserName,
                                          ApplyDate = string.Format("{0:yyyy-MM-dd HH:mm}", x.ApplyDate),
                                          WorkPalce = x.WorkPalce,
                                          WorkLevel = x.WorkLevel,
                                          ValidityStartTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.ValidityStartTime),
                                          ValidityEndTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.ValidityEndTime),
                                          States = x.States,
                                      }).ToList();
                if (getLiftingWork.Count() > 0)
                {
                    getInfoList.AddRange(getLiftingWork);
                }
                #endregion

                #region 作业票【定稿 】"待开工"="1";"作业中"="2";"已关闭"="3";"已取消"="-1"
                var getLicenseManager = (from x in db.License_LicenseManager
                                         where x.ProjectId == projectId && (x.UnitId == unitId || unitId == null)
                                             && (states == null || x.WorkStates == states || (states == "1" && (x.WorkStates == "1" || x.WorkStates == "0")))
                                         select new Model.LicenseDataItem
                                         {
                                             LicenseId = x.LicenseManagerId,
                                             MenuId = Const.ProjectLicenseManagerMenuId,
                                             MenuName = "[定稿]" + db.Base_LicenseType.FirstOrDefault(y => y.LicenseTypeId == x.LicenseTypeId).LicenseTypeName,
                                             LicenseTypeId = x.LicenseTypeId,
                                             ProjectId = x.ProjectId,
                                             LicenseCode = x.LicenseManagerCode,
                                             ApplyUnitId = x.UnitId,
                                             ApplyUnitName = db.Base_Unit.First(u => u.UnitId == x.UnitId).UnitName,
                                             ApplyManName = x.ApplicantMan,
                                             ApplyDate = string.Format("{0:yyyy-MM-dd HH:mm}", x.CompileDate),
                                             WorkPalce = UnitWorkService.GetUnitWorkName(x.WorkAreaId),
                                             ValidityStartTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.StartDate),
                                             ValidityEndTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.EndDate),
                                             States = x.WorkStates,
                                             AttachUrl = APIUpLoadFileService.getFileUrl(x.LicenseManagerId, null),
                                         }).ToList();
                if (getLicenseManager.Count() > 0)
                {
                    getInfoList.AddRange(getLicenseManager);
                }
                #endregion

                return getInfoList.OrderByDescending(x => x.ValidityStartTime).ToList();
            }
        }
        #endregion 

        #region 获取作业票检查项列表信息
        /// <summary>
        /// 获取作业票检查项列表信息
        /// </summary>
        /// <param name="dataId"></param>
        /// <returns></returns>
        public static List<Model.LicenseItem> getLicenseLicenseItemList(string dataId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var getInfoList = (from x in db.License_LicenseItem
                                   where x.DataId == dataId
                                   orderby x.SortIndex
                                   select new Model.LicenseItem
                                   {
                                       LicenseItemId = x.LicenseItemId,
                                       DataId = x.DataId,
                                       SortIndex = x.SortIndex ?? 0,
                                       SafetyMeasures = x.SafetyMeasures,
                                       IsUsed = x.IsUsed ?? false,
                                       ConfirmManId = x.ConfirmManId,
                                       ConfirmManName = db.Sys_User.First(u => u.UserId == x.ConfirmManId).UserName,
                                   }).ToList();

                return getInfoList;
            }
        }
        #endregion        

        #region 获取作业票审核列表信息
        /// <summary>
        /// 获取作业票审核列表信息
        /// </summary>
        /// <param name="dataId"></param>
        /// <returns></returns>
        public static List<Model.FlowOperateItem> getLicenseFlowOperateList(string dataId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var getInfoList = (from x in db.License_FlowOperate
                                   where x.DataId == dataId && (!x.IsFlowEnd.HasValue || x.IsFlowEnd == false)
                                   orderby x.SortIndex, x.GroupNum, x.OrderNum
                                   select new Model.FlowOperateItem
                                   {
                                       FlowOperateId = x.FlowOperateId,
                                       AuditFlowName = x.AuditFlowName,
                                       SortIndex = x.SortIndex ?? 0,
                                       GroupNum = x.GroupNum ?? 1,
                                       OrderNum = x.OrderNum ?? 1,
                                       OperaterId = x.OperaterId,
                                       OperaterName = db.Sys_User.First(u => u.UserId == x.OperaterId).UserName,
                                       OperaterTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.OperaterTime),
                                       IsAgree = x.IsAgree,
                                       Opinion = x.Opinion,
                                       IsFlowEnd = x.IsFlowEnd ?? false,
                                   }).ToList();

                return getInfoList;
            }
        }
        #endregion        

        #region 保存作业票信息
        /// <summary>
        /// 保存作业票信息
        /// </summary>
        /// <param name="newItem">作业票</param>
        /// <returns></returns>
        public static string SaveLicenseData(Model.LicenseDataItem newItem)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                string strLicenseId = newItem.LicenseId;
                string projectId = newItem.ProjectId;
                if (string.IsNullOrEmpty(newItem.CancelManId))
                {
                    newItem.CancelManId = null;
                }
                if (string.IsNullOrEmpty(newItem.CloseManId))
                {
                    newItem.CloseManId = null;
                }
                if (string.IsNullOrEmpty(newItem.NextManId))
                {
                    newItem.NextManId = null;
                }

                #region 动火作业票
                if (newItem.MenuId == Const.ProjectFireWorkMenuId)
                {
                    Model.License_FireWork newFireWork = new Model.License_FireWork
                    {
                        FireWorkId = strLicenseId,
                        ProjectId = projectId,
                        LicenseCode = newItem.LicenseCode,
                        ApplyUnitId = newItem.ApplyUnitId,
                        ApplyManId = newItem.ApplyManId,
                        ApplyDate = Funs.GetNewDateTime(newItem.ApplyDate),
                        WorkPalce = newItem.WorkPalce,
                        FireWatchManId = newItem.FireWatchManId,
                        FireWatchManName = newItem.FireWatchManName,
                        ValidityStartTime = Funs.GetNewDateTime(newItem.ValidityStartTime),
                        ValidityEndTime = Funs.GetNewDateTime(newItem.ValidityEndTime),
                        WorkMeasures = newItem.WorkMeasures,
                        CancelManId = newItem.CancelManId,
                        CancelReasons = newItem.CancelReasons,
                        CancelTime = Funs.GetNewDateTime(newItem.CancelTime),
                        CloseManId = newItem.CloseManId,
                        CloseReasons = newItem.CloseReasons,
                        CloseTime = Funs.GetNewDateTime(newItem.CloseTime),
                        NextManId = newItem.NextManId,
                        States = newItem.States,
                    };

                    if (newItem.States == Const.State_0)
                    {
                        newFireWork.NextManId = newItem.ApplyManId;
                    }
                    ////保存
                    var updateFireWork = db.License_FireWork.FirstOrDefault(x => x.FireWorkId == strLicenseId);
                    if (updateFireWork == null)
                    {
                        newFireWork.ApplyDate = DateTime.Now;
                        strLicenseId = newFireWork.FireWorkId = SQLHelper.GetNewID();
                        newFireWork.OldLicenseId = newItem.OldLicenseId;
                        newItem.LicenseCode=newFireWork.LicenseCode = CodeRecordsService.ReturnCodeByMenuIdProjectId(Const.ProjectFireWorkMenuId, newFireWork.ProjectId, newFireWork.ApplyUnitId);
                        db.License_FireWork.InsertOnSubmit(newFireWork);
                        ////增加一条编码记录
                        CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(Const.ProjectFireWorkMenuId, newFireWork.ProjectId, newFireWork.ApplyUnitId, newFireWork.FireWorkId, newFireWork.ApplyDate);
                    }
                    else
                    {
                        if (newItem.States == Const.State_3)
                        {
                            updateFireWork.CloseManId = newFireWork.CloseManId;
                            updateFireWork.CloseReasons = newFireWork.CloseReasons;
                            updateFireWork.CloseTime = DateTime.Now;
                        }
                        else if (newItem.States == Const.State_R)
                        {
                            updateFireWork.CancelManId = newFireWork.CancelManId;
                            updateFireWork.CancelReasons = newFireWork.CancelReasons;
                            updateFireWork.CancelTime = DateTime.Now;
                        }
                        else
                        {
                            updateFireWork.WorkPalce = newFireWork.WorkPalce;
                            updateFireWork.FireWatchManId = newFireWork.FireWatchManId;
                            updateFireWork.FireWatchManName = newFireWork.FireWatchManName;
                            updateFireWork.ValidityStartTime = newFireWork.ValidityStartTime;
                            updateFireWork.ValidityEndTime = newFireWork.ValidityEndTime;
                            updateFireWork.WorkMeasures = newFireWork.WorkMeasures;
                            updateFireWork.NextManId = newFireWork.NextManId;
                            updateFireWork.States = newFireWork.States;
                        }
                        updateFireWork.States = newFireWork.States;
                    }
                }
                #endregion
                #region 高处作业票
                else if (newItem.MenuId == Const.ProjectHeightWorkMenuId)
                {
                    Model.License_HeightWork newHeightWork = new Model.License_HeightWork
                    {
                        HeightWorkId = strLicenseId,
                        ProjectId = projectId,
                        LicenseCode = newItem.LicenseCode,
                        ApplyUnitId = newItem.ApplyUnitId,
                        ApplyManId = newItem.ApplyManId,
                        ApplyDate = Funs.GetNewDateTime(newItem.ApplyDate),
                        WorkPalce = newItem.WorkPalce,
                        WorkType = newItem.WorkType,
                        ValidityStartTime = Funs.GetNewDateTime(newItem.ValidityStartTime),
                        ValidityEndTime = Funs.GetNewDateTime(newItem.ValidityEndTime),
                        WorkMeasures = newItem.WorkMeasures,
                        EquipmentTools = newItem.EquipmentTools,
                        CancelManId = newItem.CancelManId,
                        CancelReasons = newItem.CancelReasons,
                        CancelTime = Funs.GetNewDateTime(newItem.CancelTime),
                        CloseManId = newItem.CloseManId,
                        CloseReasons = newItem.CloseReasons,
                        CloseTime = Funs.GetNewDateTime(newItem.CloseTime),
                        NextManId = newItem.NextManId,
                        States = newItem.States,
                    };
                    if (newItem.States == Const.State_0)
                    {
                        newHeightWork.NextManId = newItem.ApplyManId;
                    }
                    ////保存
                    var updateHeightWork = db.License_HeightWork.FirstOrDefault(x => x.HeightWorkId == strLicenseId);
                    if (updateHeightWork == null)
                    {
                        newHeightWork.ApplyDate = DateTime.Now;
                        strLicenseId = newHeightWork.HeightWorkId = SQLHelper.GetNewID();
                        newHeightWork.OldLicenseId = newItem.OldLicenseId;
                        newItem.LicenseCode= newHeightWork.LicenseCode = CodeRecordsService.ReturnCodeByMenuIdProjectId(Const.ProjectHeightWorkMenuId, newHeightWork.ProjectId, newHeightWork.ApplyUnitId);
                        db.License_HeightWork.InsertOnSubmit(newHeightWork);

                        ////增加一条编码记录
                        CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(Const.ProjectHeightWorkMenuId, newHeightWork.ProjectId, newHeightWork.ApplyUnitId, newHeightWork.HeightWorkId, newHeightWork.ApplyDate);
                    }
                    else
                    {
                        if (newItem.States == Const.State_3)
                        {
                            updateHeightWork.CloseManId = newHeightWork.CloseManId;
                            updateHeightWork.CloseReasons = newHeightWork.CloseReasons;
                            updateHeightWork.CloseTime = DateTime.Now;
                        }
                        else if (newItem.States == Const.State_R)
                        {
                            updateHeightWork.CancelManId = newHeightWork.CancelManId;
                            updateHeightWork.CancelReasons = newHeightWork.CancelReasons;
                            updateHeightWork.CancelTime = DateTime.Now;
                        }
                        else
                        {
                            updateHeightWork.WorkPalce = newHeightWork.WorkPalce;
                            updateHeightWork.WorkType = newHeightWork.WorkType;
                            updateHeightWork.ValidityStartTime = newHeightWork.ValidityStartTime;
                            updateHeightWork.ValidityEndTime = newHeightWork.ValidityEndTime;
                            updateHeightWork.WorkMeasures = newHeightWork.WorkMeasures;
                            updateHeightWork.EquipmentTools = newHeightWork.EquipmentTools;
                            updateHeightWork.NextManId = newHeightWork.NextManId;
                            updateHeightWork.States = newHeightWork.States;
                        }
                        updateHeightWork.States = newHeightWork.States;
                    }
                }
                #endregion
                #region 受限空间作业票           
                if (newItem.MenuId == Const.ProjectLimitedSpaceMenuId)
                {
                    Model.License_LimitedSpace newLimitedSpace = new Model.License_LimitedSpace
                    {
                        LimitedSpaceId = strLicenseId,
                        ProjectId = projectId,
                        LicenseCode = newItem.LicenseCode,
                        ApplyUnitId = newItem.ApplyUnitId,
                        ApplyManId = newItem.ApplyManId,
                        ApplyDate = Funs.GetNewDateTime(newItem.ApplyDate),
                        WorkPalce = newItem.WorkPalce,
                        FireWatchManId = newItem.FireWatchManId,
                        FireWatchManName = newItem.FireWatchManName,
                        ValidityStartTime = Funs.GetNewDateTime(newItem.ValidityStartTime),
                        ValidityEndTime = Funs.GetNewDateTime(newItem.ValidityEndTime),
                        WorkMeasures = newItem.WorkMeasures,
                        CancelManId = newItem.CancelManId,
                        CancelReasons = newItem.CancelReasons,
                        CancelTime = Funs.GetNewDateTime(newItem.CancelTime),
                        CloseManId = newItem.CloseManId,
                        CloseReasons = newItem.CloseReasons,
                        CloseTime = Funs.GetNewDateTime(newItem.CloseTime),
                        NextManId = newItem.NextManId,
                        States = newItem.States,
                    };
                    if (newItem.States == Const.State_0)
                    {
                        newLimitedSpace.NextManId = newItem.ApplyManId;
                    }
                    ////保存
                    var updateLimitedSpace = db.License_LimitedSpace.FirstOrDefault(x => x.LimitedSpaceId == strLicenseId);
                    if (updateLimitedSpace == null)
                    {
                        newLimitedSpace.ApplyDate = DateTime.Now;
                        strLicenseId = newLimitedSpace.LimitedSpaceId = SQLHelper.GetNewID();
                        newLimitedSpace.OldLicenseId = newItem.OldLicenseId;
                        newItem.LicenseCode = newLimitedSpace.LicenseCode = CodeRecordsService.ReturnCodeByMenuIdProjectId(Const.ProjectLimitedSpaceMenuId, newLimitedSpace.ProjectId, newLimitedSpace.ApplyUnitId);
                        db.License_LimitedSpace.InsertOnSubmit(newLimitedSpace);
                        ////增加一条编码记录
                        CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(Const.ProjectLimitedSpaceMenuId, newLimitedSpace.ProjectId, newLimitedSpace.ApplyUnitId, newLimitedSpace.LimitedSpaceId, newLimitedSpace.ApplyDate);
                    }
                    else
                    {
                        if (newItem.States == Const.State_3)
                        {
                            updateLimitedSpace.CloseManId = newLimitedSpace.CloseManId;
                            updateLimitedSpace.CloseReasons = newLimitedSpace.CloseReasons;
                            updateLimitedSpace.CloseTime = DateTime.Now;
                        }
                        else if (newItem.States == Const.State_R)
                        {
                            updateLimitedSpace.CancelManId = newLimitedSpace.CancelManId;
                            updateLimitedSpace.CancelReasons = newLimitedSpace.CancelReasons;
                            updateLimitedSpace.CancelTime = DateTime.Now;
                        }
                        else
                        {
                            updateLimitedSpace.WorkPalce = newLimitedSpace.WorkPalce;
                            updateLimitedSpace.FireWatchManId = newLimitedSpace.FireWatchManId;
                            updateLimitedSpace.FireWatchManName = newLimitedSpace.FireWatchManName;
                            updateLimitedSpace.ValidityStartTime = newLimitedSpace.ValidityStartTime;
                            updateLimitedSpace.ValidityEndTime = newLimitedSpace.ValidityEndTime;
                            updateLimitedSpace.WorkMeasures = newLimitedSpace.WorkMeasures;
                            updateLimitedSpace.NextManId = newLimitedSpace.NextManId;
                            updateLimitedSpace.States = newLimitedSpace.States;
                        }
                        updateLimitedSpace.States = newLimitedSpace.States;
                    }
                }
                #endregion
                #region 射线作业票
                if (newItem.MenuId == Const.ProjectRadialWorkMenuId)
                {
                    Model.License_RadialWork newRadialWork = new Model.License_RadialWork
                    {
                        RadialWorkId = strLicenseId,
                        ProjectId = projectId,
                        LicenseCode = newItem.LicenseCode,
                        ApplyUnitId = newItem.ApplyUnitId,
                        ApplyManId = newItem.ApplyManId,
                        ApplyDate = Funs.GetNewDateTime(newItem.ApplyDate),
                        RadialType = newItem.RadialType,
                        WorkLeaderId = newItem.WorkLeaderId,
                        WorkLeaderTel = newItem.WorkLeaderTel,
                        ValidityStartTime = Funs.GetNewDateTime(newItem.ValidityStartTime),
                        ValidityEndTime = Funs.GetNewDateTime(newItem.ValidityEndTime),
                        WorkPalce = newItem.WorkPalce,
                        WorkMeasures = newItem.WorkMeasures,
                        FireWatchManId = newItem.FireWatchManId,
                        WatchManContact = newItem.WatchManContact,
                        CancelManId = newItem.CancelManId,
                        CancelReasons = newItem.CancelReasons,
                        CancelTime = Funs.GetNewDateTime(newItem.CancelTime),
                        CloseManId = newItem.CloseManId,
                        CloseReasons = newItem.CloseReasons,
                        CloseTime = Funs.GetNewDateTime(newItem.CloseTime),
                        NextManId = newItem.NextManId,
                        States = newItem.States,
                    };
                    if (newItem.States == Const.State_0)
                    {
                        newRadialWork.NextManId = newItem.ApplyManId;
                    }
                    ////保存
                    var updateRadialWork = db.License_RadialWork.FirstOrDefault(x => x.RadialWorkId == strLicenseId);
                    if (updateRadialWork == null)
                    {
                        newRadialWork.ApplyDate = DateTime.Now;
                        strLicenseId = newRadialWork.RadialWorkId = SQLHelper.GetNewID();
                        newRadialWork.OldLicenseId = newItem.OldLicenseId;
                        newItem.LicenseCode = newRadialWork.LicenseCode = CodeRecordsService.ReturnCodeByMenuIdProjectId(Const.ProjectRadialWorkMenuId, newRadialWork.ProjectId, newRadialWork.ApplyUnitId);
                        db.License_RadialWork.InsertOnSubmit(newRadialWork);
                        ////增加一条编码记录
                        CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(Const.ProjectRadialWorkMenuId, newRadialWork.ProjectId, newRadialWork.ApplyUnitId, newRadialWork.RadialWorkId, newRadialWork.ApplyDate);
                    }
                    else
                    {
                        if (newItem.States == Const.State_3)
                        {
                            updateRadialWork.CloseManId = newRadialWork.CloseManId;
                            updateRadialWork.CloseReasons = newRadialWork.CloseReasons;
                            updateRadialWork.CloseTime = DateTime.Now;
                        }
                        else if (newItem.States == Const.State_R)
                        {
                            updateRadialWork.CancelManId = newRadialWork.CancelManId;
                            updateRadialWork.CancelReasons = newRadialWork.CancelReasons;
                            updateRadialWork.CancelTime = DateTime.Now;
                        }
                        else
                        {
                            updateRadialWork.RadialType = newRadialWork.RadialType;
                            updateRadialWork.WorkLeaderId = newRadialWork.WorkLeaderId;
                            updateRadialWork.WorkLeaderTel = newRadialWork.WorkLeaderTel;
                            updateRadialWork.ValidityStartTime = newRadialWork.ValidityStartTime;
                            updateRadialWork.ValidityEndTime = newRadialWork.ValidityEndTime;
                            updateRadialWork.WorkPalce = newRadialWork.WorkPalce;
                            updateRadialWork.WorkMeasures = newRadialWork.WorkMeasures;
                            updateRadialWork.FireWatchManId = newRadialWork.FireWatchManId;
                            updateRadialWork.WatchManContact = newRadialWork.WatchManContact;
                            updateRadialWork.NextManId = newRadialWork.NextManId;
                            updateRadialWork.States = newRadialWork.States;
                        }
                        updateRadialWork.States = newRadialWork.States;
                    }
                }
                #endregion
                #region 断路(占道)作业票
                if (newItem.MenuId == Const.ProjectOpenCircuitMenuId)
                {
                    Model.License_OpenCircuit newOpenCircuit = new Model.License_OpenCircuit
                    {
                        OpenCircuitId = strLicenseId,
                        ProjectId = projectId,
                        LicenseCode = newItem.LicenseCode,
                        ApplyUnitId = newItem.ApplyUnitId,
                        ApplyManId = newItem.ApplyManId,
                        ApplyDate = Funs.GetNewDateTime(newItem.ApplyDate),
                        WorkPalce = newItem.WorkPalce,
                        WorkMeasures = newItem.WorkMeasures,
                        ValidityStartTime = Funs.GetNewDateTime(newItem.ValidityStartTime),
                        ValidityEndTime = Funs.GetNewDateTime(newItem.ValidityEndTime),
                        RoadName = newItem.RoadName,
                        SafeMeasures = newItem.SafeMeasures,
                        CancelManId = newItem.CancelManId,
                        CancelReasons = newItem.CancelReasons,
                        CancelTime = Funs.GetNewDateTime(newItem.CancelTime),
                        CloseManId = newItem.CloseManId,
                        CloseReasons = newItem.CloseReasons,
                        CloseTime = Funs.GetNewDateTime(newItem.CloseTime),
                        NextManId = newItem.NextManId,
                        States = newItem.States,
                    };
                    if (newItem.States == Const.State_0)
                    {
                        newOpenCircuit.NextManId = newItem.ApplyManId;
                    }
                    ////保存
                    var updateOpenCircuit = db.License_OpenCircuit.FirstOrDefault(x => x.OpenCircuitId == strLicenseId);
                    if (updateOpenCircuit == null)
                    {
                        newOpenCircuit.ApplyDate = DateTime.Now;
                        strLicenseId = newOpenCircuit.OpenCircuitId = SQLHelper.GetNewID();
                        newOpenCircuit.OldLicenseId = newItem.OldLicenseId;
                        newItem.LicenseCode = newOpenCircuit.LicenseCode = CodeRecordsService.ReturnCodeByMenuIdProjectId(Const.ProjectOpenCircuitMenuId, newOpenCircuit.ProjectId, newOpenCircuit.ApplyUnitId);
                        db.License_OpenCircuit.InsertOnSubmit(newOpenCircuit);
                        ////增加一条编码记录
                        CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(Const.ProjectOpenCircuitMenuId, newOpenCircuit.ProjectId, newOpenCircuit.ApplyUnitId, newOpenCircuit.OpenCircuitId, newOpenCircuit.ApplyDate);
                    }
                    else
                    {
                        if (newItem.States == Const.State_3)
                        {
                            updateOpenCircuit.CloseManId = newOpenCircuit.CloseManId;
                            updateOpenCircuit.CloseReasons = newOpenCircuit.CloseReasons;
                            updateOpenCircuit.CloseTime = DateTime.Now;
                        }
                        else if (newItem.States == Const.State_R)
                        {
                            updateOpenCircuit.CancelManId = newOpenCircuit.CancelManId;
                            updateOpenCircuit.CancelReasons = newOpenCircuit.CancelReasons;
                            updateOpenCircuit.CancelTime = DateTime.Now;
                        }
                        else
                        {
                            updateOpenCircuit.WorkPalce = newOpenCircuit.WorkPalce;
                            updateOpenCircuit.WorkMeasures = newOpenCircuit.WorkMeasures;
                            updateOpenCircuit.ValidityStartTime = newOpenCircuit.ValidityStartTime;
                            updateOpenCircuit.ValidityEndTime = newOpenCircuit.ValidityEndTime;
                            updateOpenCircuit.RoadName = newOpenCircuit.RoadName;
                            updateOpenCircuit.SafeMeasures = newOpenCircuit.SafeMeasures;
                            updateOpenCircuit.NextManId = newOpenCircuit.NextManId;
                            updateOpenCircuit.States = newOpenCircuit.States;
                        }
                        updateOpenCircuit.States = newOpenCircuit.States;
                    }
                }
                #endregion
                #region 动土作业票
                if (newItem.MenuId == Const.ProjectBreakGroundMenuId)
                {
                    Model.License_BreakGround newBreakGround = new Model.License_BreakGround
                    {
                        BreakGroundId = strLicenseId,
                        ProjectId = projectId,
                        LicenseCode = newItem.LicenseCode,
                        ApplyUnitId = newItem.ApplyUnitId,
                        ApplyManId = newItem.ApplyManId,
                        ApplyDate = Funs.GetNewDateTime(newItem.ApplyDate),
                        WorkPalce = newItem.WorkPalce,
                        WorkDepth = newItem.WorkDepth,
                        ValidityStartTime = Funs.GetNewDateTime(newItem.ValidityStartTime),
                        ValidityEndTime = Funs.GetNewDateTime(newItem.ValidityEndTime),
                        WorkMeasures = newItem.WorkMeasures,
                        CancelManId = newItem.CancelManId,
                        CancelReasons = newItem.CancelReasons,
                        CancelTime = Funs.GetNewDateTime(newItem.CancelTime),
                        CloseManId = newItem.CloseManId,
                        CloseReasons = newItem.CloseReasons,
                        CloseTime = Funs.GetNewDateTime(newItem.CloseTime),
                        NextManId = newItem.NextManId,
                        States = newItem.States,
                    };
                    if (newItem.States == Const.State_0)
                    {
                        newBreakGround.NextManId = newItem.ApplyManId;
                    }
                    ////保存
                    var updateBreakGround = db.License_BreakGround.FirstOrDefault(x => x.BreakGroundId == strLicenseId);
                    if (updateBreakGround == null)
                    {
                        newBreakGround.ApplyDate = DateTime.Now;
                        strLicenseId = newBreakGround.BreakGroundId = SQLHelper.GetNewID();
                        newBreakGround.OldLicenseId = newItem.OldLicenseId;
                        newItem.LicenseCode = newBreakGround.LicenseCode = CodeRecordsService.ReturnCodeByMenuIdProjectId(Const.ProjectBreakGroundMenuId, newBreakGround.ProjectId, newBreakGround.ApplyUnitId);
                        db.License_BreakGround.InsertOnSubmit(newBreakGround);
                        ////增加一条编码记录
                        CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(Const.ProjectBreakGroundMenuId, newBreakGround.ProjectId, newBreakGround.ApplyUnitId, newBreakGround.BreakGroundId, newBreakGround.ApplyDate);
                    }
                    else
                    {
                        if (newItem.States == Const.State_3)
                        {
                            updateBreakGround.CloseManId = newBreakGround.CloseManId;
                            updateBreakGround.CloseReasons = newBreakGround.CloseReasons;
                            updateBreakGround.CloseTime = DateTime.Now;
                        }
                        else if (newItem.States == Const.State_R)
                        {
                            updateBreakGround.CancelManId = newBreakGround.CancelManId;
                            updateBreakGround.CancelReasons = newBreakGround.CancelReasons;
                            updateBreakGround.CancelTime = DateTime.Now;
                        }
                        else
                        {
                            updateBreakGround.WorkPalce = newBreakGround.WorkPalce;
                            updateBreakGround.WorkDepth = newBreakGround.WorkDepth;
                            updateBreakGround.ValidityStartTime = newBreakGround.ValidityStartTime;
                            updateBreakGround.ValidityEndTime = newBreakGround.ValidityEndTime;
                            updateBreakGround.WorkMeasures = newBreakGround.WorkMeasures;
                            updateBreakGround.NextManId = newBreakGround.NextManId;
                            updateBreakGround.States = newBreakGround.States;
                        }
                        updateBreakGround.States = newBreakGround.States;
                    }
                }
                #endregion
                #region 夜间施工作业票
                if (newItem.MenuId == Const.ProjectNightWorkMenuId)
                {
                    Model.License_NightWork newNightWork = new Model.License_NightWork
                    {
                        NightWorkId = strLicenseId,
                        ProjectId = projectId,
                        LicenseCode = newItem.LicenseCode,
                        ApplyUnitId = newItem.ApplyUnitId,
                        ApplyManId = newItem.ApplyManId,
                        ApplyDate = Funs.GetNewDateTime(newItem.ApplyDate),
                        WorkPalce = newItem.WorkPalce,
                        WorkMeasures = newItem.WorkMeasures,
                        ValidityStartTime = Funs.GetNewDateTime(newItem.ValidityStartTime),
                        ValidityEndTime = Funs.GetNewDateTime(newItem.ValidityEndTime),
                        WorkLeaderId = newItem.WorkLeaderId,
                        WorkLeaderTel = newItem.WorkLeaderTel,
                        SafeLeaderId = newItem.SafeLeaderId,
                        SafeLeaderTel = newItem.SafeLeaderTel,
                        CancelManId = newItem.CancelManId,
                        CancelReasons = newItem.CancelReasons,
                        CancelTime = Funs.GetNewDateTime(newItem.CancelTime),
                        CloseManId = newItem.CloseManId,
                        CloseReasons = newItem.CloseReasons,
                        CloseTime = Funs.GetNewDateTime(newItem.CloseTime),
                        NextManId = newItem.NextManId,
                        States = newItem.States,
                    };
                    if (newItem.States == Const.State_0)
                    {
                        newNightWork.NextManId = newItem.ApplyManId;
                    }
                    ////保存
                    var updateNightWork = db.License_NightWork.FirstOrDefault(x => x.NightWorkId == strLicenseId);
                    if (updateNightWork == null)
                    {
                        newNightWork.ApplyDate = DateTime.Now;
                        strLicenseId = newNightWork.NightWorkId = SQLHelper.GetNewID();
                        newNightWork.OldLicenseId = newItem.OldLicenseId;
                        newItem.LicenseCode = newNightWork.LicenseCode = CodeRecordsService.ReturnCodeByMenuIdProjectId(Const.ProjectNightWorkMenuId, newNightWork.ProjectId, newNightWork.ApplyUnitId);
                        db.License_NightWork.InsertOnSubmit(newNightWork);
                        ////增加一条编码记录
                        CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(Const.ProjectNightWorkMenuId, newNightWork.ProjectId, newNightWork.ApplyUnitId, newNightWork.NightWorkId, newNightWork.ApplyDate);
                    }
                    else
                    {
                        if (newItem.States == Const.State_3)
                        {
                            updateNightWork.CloseManId = newNightWork.CloseManId;
                            updateNightWork.CloseReasons = newNightWork.CloseReasons;
                            updateNightWork.CloseTime = DateTime.Now;
                        }
                        else if (newItem.States == Const.State_R)
                        {
                            updateNightWork.CancelManId = newNightWork.CancelManId;
                            updateNightWork.CancelReasons = newNightWork.CancelReasons;
                            updateNightWork.CancelTime = DateTime.Now;
                        }
                        else
                        {
                            updateNightWork.WorkPalce = newNightWork.WorkPalce;
                            updateNightWork.WorkMeasures = newNightWork.WorkMeasures;
                            updateNightWork.ValidityStartTime = newNightWork.ValidityStartTime;
                            updateNightWork.ValidityEndTime = newNightWork.ValidityEndTime;
                            updateNightWork.WorkLeaderId = newItem.WorkLeaderId;
                            updateNightWork.WorkLeaderTel = newItem.WorkLeaderTel;
                            updateNightWork.SafeLeaderId = newItem.SafeLeaderId;
                            updateNightWork.SafeLeaderTel = newItem.SafeLeaderTel;
                            updateNightWork.NextManId = newNightWork.NextManId;
                            updateNightWork.States = newNightWork.States;
                        }
                        updateNightWork.States = newNightWork.States;
                    }
                }
                #endregion
                #region 吊装作业票
                if (newItem.MenuId == Const.ProjectLiftingWorkMenuId)
                {
                    Model.License_LiftingWork newLiftingWork = new Model.License_LiftingWork
                    {
                        LiftingWorkId = strLicenseId,
                        ProjectId = projectId,
                        LicenseCode = newItem.LicenseCode,
                        ApplyUnitId = newItem.ApplyUnitId,
                        ApplyManId = newItem.ApplyManId,
                        ApplyDate = Funs.GetNewDateTime(newItem.ApplyDate),
                        WorkPalce = newItem.WorkPalce,
                        WorkLevel = newItem.WorkLevel,
                        ValidityStartTime = Funs.GetNewDateTime(newItem.ValidityStartTime),
                        ValidityEndTime = Funs.GetNewDateTime(newItem.ValidityEndTime),
                        WorkMeasures = newItem.WorkMeasures,
                        CraneCapacity = newItem.CraneCapacity,
                        CancelManId = newItem.CancelManId,
                        CancelReasons = newItem.CancelReasons,
                        CancelTime = Funs.GetNewDateTime(newItem.CancelTime),
                        CloseManId = newItem.CloseManId,
                        CloseReasons = newItem.CloseReasons,
                        CloseTime = Funs.GetNewDateTime(newItem.CloseTime),
                        NextManId = newItem.NextManId,
                        States = newItem.States,
                    };
                    if (newItem.States == Const.State_0)
                    {
                        newLiftingWork.NextManId = newItem.ApplyManId;
                    }
                    ////保存
                    var updateLiftingWork = db.License_LiftingWork.FirstOrDefault(x => x.LiftingWorkId == strLicenseId);
                    if (updateLiftingWork == null)
                    {
                        newLiftingWork.ApplyDate = DateTime.Now;
                        strLicenseId = newLiftingWork.LiftingWorkId = SQLHelper.GetNewID();
                        newLiftingWork.OldLicenseId = newItem.OldLicenseId;
                        newItem.LicenseCode = newLiftingWork.LicenseCode = CodeRecordsService.ReturnCodeByMenuIdProjectId(Const.ProjectLiftingWorkMenuId, newLiftingWork.ProjectId, newLiftingWork.ApplyUnitId);
                        db.License_LiftingWork.InsertOnSubmit(newLiftingWork);
                        ////增加一条编码记录
                        CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(Const.ProjectLiftingWorkMenuId, newLiftingWork.ProjectId, newLiftingWork.ApplyUnitId, newLiftingWork.LiftingWorkId, newLiftingWork.ApplyDate);
                    }
                    else
                    {
                        if (newItem.States == Const.State_3)
                        {
                            updateLiftingWork.CloseManId = newLiftingWork.CloseManId;
                            updateLiftingWork.CloseReasons = newLiftingWork.CloseReasons;
                            updateLiftingWork.CloseTime = DateTime.Now;
                        }
                        else if (newItem.States == Const.State_R)
                        {
                            updateLiftingWork.CancelManId = newLiftingWork.CancelManId;
                            updateLiftingWork.CancelReasons = newLiftingWork.CancelReasons;
                            updateLiftingWork.CancelTime = DateTime.Now;
                        }
                        else
                        {
                            updateLiftingWork.WorkPalce = newLiftingWork.WorkPalce;
                            updateLiftingWork.WorkLevel = newLiftingWork.WorkLevel;
                            updateLiftingWork.ValidityStartTime = newLiftingWork.ValidityStartTime;
                            updateLiftingWork.ValidityEndTime = newLiftingWork.ValidityEndTime;
                            updateLiftingWork.WorkMeasures = newLiftingWork.WorkMeasures;
                            updateLiftingWork.CraneCapacity = newLiftingWork.CraneCapacity;
                            updateLiftingWork.NextManId = newLiftingWork.NextManId;
                            updateLiftingWork.States = newLiftingWork.States;
                        }
                        updateLiftingWork.States = newLiftingWork.States;
                    }
                }
                #endregion

                #region 作业票【定稿】
                if (newItem.MenuId == Const.ProjectLicenseManagerMenuId)
                {
                    Model.License_LicenseManager newLicenseManager = new Model.License_LicenseManager
                    {
                        LicenseManagerId = strLicenseId,
                        ProjectId = projectId,
                        LicenseManagerCode = newItem.LicenseCode,
                        UnitId = newItem.ApplyUnitId,
                        ApplicantMan = newItem.ApplyManName,
                        WorkAreaId = newItem.WorkAreaIds,
                        CompileMan = newItem.ApplyManId,
                        CompileDate = Funs.GetNewDateTime(newItem.ApplyDate),
                        StartDate = Funs.GetNewDateTime(newItem.ValidityStartTime),
                        EndDate = Funs.GetNewDateTime(newItem.ValidityEndTime),
                        WorkStates = newItem.States,
                        States = Const.State_0,
                    };
                    newLicenseManager.LicenseTypeId = string.IsNullOrEmpty(newItem.LicenseTypeId) ? null : newItem.LicenseTypeId;
                  
                    if (newLicenseManager.WorkStates == Const.State_3 || newLicenseManager.WorkStates == Const.State_R)
                    {
                        newLicenseManager.States = Const.State_2;
                    }
                    ////保存
                    var updateLicenseManager = db.License_LicenseManager.FirstOrDefault(x => x.LicenseManagerId == strLicenseId);
                    if (updateLicenseManager == null)
                    {
                        strLicenseId = newLicenseManager.LicenseManagerId = SQLHelper.GetNewID();
                        newItem.LicenseCode = newLicenseManager.LicenseManagerCode = CodeRecordsService.ReturnCodeByMenuIdProjectId(Const.ProjectLicenseManagerMenuId, newLicenseManager.ProjectId, newLicenseManager.UnitId);
                        db.License_LicenseManager.InsertOnSubmit(newLicenseManager);
                        ////增加一条编码记录
                        CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(Const.ProjectLicenseManagerMenuId, newLicenseManager.ProjectId, newLicenseManager.UnitId, newLicenseManager.LicenseManagerId, newLicenseManager.CompileDate);
                    }
                    else
                    {
                        if (newLicenseManager.WorkStates != Const.State_3 && newLicenseManager.WorkStates != Const.State_R)
                        {
                            updateLicenseManager.WorkAreaId = newLicenseManager.WorkAreaId;
                            updateLicenseManager.LicenseTypeId = newLicenseManager.LicenseTypeId;
                            updateLicenseManager.CompileMan = newLicenseManager.CompileMan;
                            updateLicenseManager.CompileDate = newLicenseManager.CompileDate;
                            updateLicenseManager.StartDate = newLicenseManager.StartDate;
                            updateLicenseManager.EndDate = newLicenseManager.EndDate;
                        }
                       
                        updateLicenseManager.WorkStates = newLicenseManager.WorkStates;
                        updateLicenseManager.States = newLicenseManager.States;
                    }
                    if (newLicenseManager.States == Const.State_2)
                    {
                        CommonService.btnSaveData(newLicenseManager.ProjectId, Const.ProjectLicenseManagerMenuId, newLicenseManager.LicenseManagerId, newLicenseManager.CompileMan, true, newLicenseManager.LicenseManagerCode, "../License/LicenseManagerView.aspx?LicenseManagerId={0}");
                    }
                }
                #endregion

                db.SubmitChanges();
                #region 保存安全措施明细
                if (newItem.States == Const.State_0 || newItem.States == Const.State_1)
                {
                    ////删除安全措施                       
                    var licenseItems = from x in db.License_LicenseItem where x.DataId == strLicenseId select x;
                    if (licenseItems.Count() > 0)
                    {
                        db.License_LicenseItem.DeleteAllOnSubmit(licenseItems);
                        db.SubmitChanges();
                    }
                    ///// 新增安全措施
                    var getLicenseItemList = newItem.LicenseItems;
                    if (newItem.LicenseItems != null && getLicenseItemList.Count() > 0)
                    {
                        foreach (var item in getLicenseItemList)
                        {
                            Model.License_LicenseItem newLicenseItem = new Model.License_LicenseItem
                            {
                                LicenseItemId = SQLHelper.GetNewID(),
                                DataId = strLicenseId,
                                SortIndex = item.SortIndex,
                                SafetyMeasures = item.SafetyMeasures,
                                IsUsed = item.IsUsed,
                                ConfirmManId = item.ConfirmManId,
                            };
                            if (string.IsNullOrEmpty(newLicenseItem.ConfirmManId))
                            {
                                newLicenseItem.ConfirmManId = newItem.ApplyManId;
                            }
                            db.License_LicenseItem.InsertOnSubmit(newLicenseItem);
                            db.SubmitChanges();
                        }
                    }
                }
                #endregion

                if (newItem.States != Const.State_3 && newItem.States != Const.State_R)
                {
                    //// 保存附件
                    APIUpLoadFileService.SaveAttachUrl(newItem.MenuId, strLicenseId, newItem.AttachUrl, "0");
                }
                if (!string.IsNullOrEmpty(newItem.NextManId) && (newItem.States == Const.State_1 || newItem.States == Const.State_0))
                {
                    List<string> getUserIds = Funs.GetStrListByStr(newItem.NextManId, ',');
                    foreach (var item in getUserIds)
                    {
                        APICommonService.SendSubscribeMessage(item, "作业票待审核", newItem.ApplyManName, string.Format("{0:yyyy-MM-dd HH:mm:ss}", DateTime.Now));
                    }
                }

                return strLicenseId;
            }
        }
        #endregion

        #region 保存作业票审核信息
        /// <summary>
        /// 保存作业票审核信息
        /// </summary>
        /// <param name="newItem">保存作业票审核信息</param>
        /// <returns></returns>
        public static void SaveLicenseFlowOperate(Model.FlowOperateItem newItem)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                string strMenuId = string.Empty;
                bool boolIsFlowEnd = false;
                string applyManId = string.Empty;
                var updateFlowOperate = db.License_FlowOperate.FirstOrDefault(x => x.FlowOperateId == newItem.FlowOperateId);
                if (updateFlowOperate != null && !string.IsNullOrEmpty(newItem.OperaterId))
                {
                    strMenuId = updateFlowOperate.MenuId;
                    updateFlowOperate.OperaterId = newItem.OperaterId;
                    updateFlowOperate.OperaterTime = DateTime.Now;
                    updateFlowOperate.IsAgree = newItem.IsAgree;
                    updateFlowOperate.Opinion = newItem.Opinion;
                    updateFlowOperate.IsClosed = true;

                    db.SubmitChanges();

                    /////增加一条审核明细记录
                    Model.License_FlowOperateItem newFlowOperateItem = new Model.License_FlowOperateItem
                    {
                        FlowOperateItemId = SQLHelper.GetNewID(),
                        FlowOperateId = updateFlowOperate.FlowOperateId,
                        OperaterId = updateFlowOperate.OperaterId,
                        OperaterTime = updateFlowOperate.OperaterTime,
                        IsAgree = updateFlowOperate.IsAgree,
                        Opinion = updateFlowOperate.Opinion,
                    };
                    db.License_FlowOperateItem.InsertOnSubmit(newFlowOperateItem);
                
                    #region 新增下一步审核记录
                    if (newItem.IsAgree == true)
                    {
                        var getCloseAllOperate = db.License_FlowOperate.FirstOrDefault(x => x.DataId == updateFlowOperate.DataId && (!x.IsClosed.HasValue || x.IsClosed == false) && (!x.IsFlowEnd.HasValue || x.IsFlowEnd == false));
                        if (getCloseAllOperate == null)
                        {
                            var getNextFlowOperate = db.License_FlowOperate.FirstOrDefault(x => x.DataId == updateFlowOperate.DataId && x.IsFlowEnd == true);
                            ////判断审核步骤是否结束
                            if (getNextFlowOperate != null)
                            {
                                /////最后一步是关闭所有 步骤
                                getNextFlowOperate.IsClosed = true;
                                getNextFlowOperate.OperaterTime = DateTime.Now;
                                getNextFlowOperate.IsAgree = true;
                                getNextFlowOperate.OperaterId = newItem.OperaterId;
                                getNextFlowOperate.Opinion = "审核完成！";
                                boolIsFlowEnd = true;
                                db.SubmitChanges();                          
                            }
                        }
                        
                        #region 动火作业票
                        if (strMenuId == Const.ProjectFireWorkMenuId)
                        {
                            var getFireWork = db.License_FireWork.FirstOrDefault(x => x.FireWorkId == updateFlowOperate.DataId);
                            if (getFireWork != null)
                            {
                                getFireWork.NextManId = newItem.NextOperaterId;
                                if (newItem.IsAgree == true)
                                {
                                    if (boolIsFlowEnd == true)
                                    {
                                        getFireWork.NextManId = null;
                                        getFireWork.States = Const.State_2;
                                        if (getFireWork.ValidityStartTime.HasValue && getFireWork.ValidityStartTime < DateTime.Now)
                                        {
                                            int days = 7;
                                            if (getFireWork.ValidityEndTime.HasValue)
                                            {
                                                days = Convert.ToInt32((getFireWork.ValidityEndTime - getFireWork.ValidityStartTime).Value.TotalDays);
                                            }
                                            getFireWork.ValidityStartTime = DateTime.Now;
                                            getFireWork.ValidityEndTime = DateTime.Now.AddDays(days);
                                        }
                                    }
                                }
                                else
                                {
                                    getFireWork.States = Const.State_0;
                                }
                                db.SubmitChanges();
                            }
                        }
                        #endregion
                        #region 高处作业票
                        else if (strMenuId == Const.ProjectHeightWorkMenuId)
                        {
                            var getHeightWork = db.License_HeightWork.FirstOrDefault(x => x.HeightWorkId == updateFlowOperate.DataId);
                            if (getHeightWork != null)
                            {
                                getHeightWork.NextManId = newItem.NextOperaterId;
                                if (newItem.IsAgree == true)
                                {
                                    if (boolIsFlowEnd == true)
                                    {
                                        getHeightWork.NextManId = null;
                                        getHeightWork.States = Const.State_2;
                                        if (getHeightWork.ValidityStartTime.HasValue && getHeightWork.ValidityStartTime < DateTime.Now)
                                        {
                                            int days = 7;
                                            if (getHeightWork.ValidityEndTime.HasValue)
                                            {
                                                days = Convert.ToInt32((getHeightWork.ValidityEndTime - getHeightWork.ValidityStartTime).Value.TotalDays);
                                            }
                                            getHeightWork.ValidityStartTime = DateTime.Now;
                                            getHeightWork.ValidityEndTime = DateTime.Now.AddDays(days);
                                        }
                                    }
                                }
                                else
                                {
                                    getHeightWork.States = Const.State_0;
                                }
                                db.SubmitChanges();
                            }
                        }
                        #endregion
                        #region 受限空间作业票
                        if (strMenuId == Const.ProjectLimitedSpaceMenuId)
                        {
                            var getLimitedSpace = db.License_LimitedSpace.FirstOrDefault(x => x.LimitedSpaceId == updateFlowOperate.DataId);
                            if (getLimitedSpace != null)
                            {
                                getLimitedSpace.NextManId = newItem.NextOperaterId;
                                if (newItem.IsAgree == true)
                                {
                                    if (boolIsFlowEnd == true)
                                    {
                                        getLimitedSpace.NextManId = null;
                                        getLimitedSpace.States = Const.State_2;
                                        if (getLimitedSpace.ValidityStartTime.HasValue && getLimitedSpace.ValidityStartTime < DateTime.Now)
                                        {
                                            int days = 1;
                                            if (getLimitedSpace.ValidityEndTime.HasValue)
                                            {
                                                days = Convert.ToInt32((getLimitedSpace.ValidityEndTime - getLimitedSpace.ValidityStartTime).Value.TotalDays);
                                            }
                                            getLimitedSpace.ValidityStartTime = DateTime.Now;
                                            getLimitedSpace.ValidityEndTime = DateTime.Now.AddDays(days);
                                        }
                                    }
                                }
                                else
                                {
                                    getLimitedSpace.States = Const.State_0;
                                }
                                db.SubmitChanges();
                            }
                        }
                        #endregion
                        #region 射线作业票
                        if (strMenuId == Const.ProjectRadialWorkMenuId)
                        {
                            var getRadialWork = db.License_RadialWork.FirstOrDefault(x => x.RadialWorkId == updateFlowOperate.DataId);
                            if (getRadialWork != null)
                            {
                                getRadialWork.NextManId = newItem.NextOperaterId;
                                if (newItem.IsAgree == true)
                                {
                                    if (boolIsFlowEnd == true)
                                    {
                                        getRadialWork.NextManId = null;
                                        getRadialWork.States = Const.State_2;
                                        if (getRadialWork.ValidityStartTime.HasValue && getRadialWork.ValidityStartTime < DateTime.Now)
                                        {
                                            int hours = 24;
                                            if (getRadialWork.ValidityEndTime.HasValue)
                                            {
                                                hours = Convert.ToInt32((getRadialWork.ValidityEndTime - getRadialWork.ValidityStartTime).Value.TotalHours);
                                            }
                                            getRadialWork.ValidityStartTime = DateTime.Now;
                                            getRadialWork.ValidityEndTime = DateTime.Now.AddHours(hours);
                                        }
                                    }
                                }
                                else
                                {
                                    getRadialWork.States = Const.State_0;
                                }
                                db.SubmitChanges();
                            }
                        }
                        #endregion
                        #region 断路(占道)作业票
                        if (strMenuId == Const.ProjectOpenCircuitMenuId)
                        {
                            var getOpenCircuit = db.License_OpenCircuit.FirstOrDefault(x => x.OpenCircuitId == updateFlowOperate.DataId);
                            if (getOpenCircuit != null)
                            {
                                getOpenCircuit.NextManId = newItem.NextOperaterId;
                                if (newItem.IsAgree == true)
                                {
                                    if (boolIsFlowEnd == true)
                                    {
                                        getOpenCircuit.NextManId = null;
                                        getOpenCircuit.States = Const.State_2;
                                        if (getOpenCircuit.ValidityStartTime.HasValue && getOpenCircuit.ValidityStartTime < DateTime.Now)
                                        {
                                            int days = 7;
                                            if (getOpenCircuit.ValidityEndTime.HasValue)
                                            {
                                                days = Convert.ToInt32((getOpenCircuit.ValidityEndTime - getOpenCircuit.ValidityStartTime).Value.TotalDays);
                                            }
                                            getOpenCircuit.ValidityStartTime = DateTime.Now;
                                            getOpenCircuit.ValidityEndTime = DateTime.Now.AddDays(days);
                                        }
                                    }
                                }
                                else
                                {
                                    getOpenCircuit.States = Const.State_0;
                                }
                                db.SubmitChanges();
                            }
                        }
                        #endregion
                        #region 动土作业票
                        if (strMenuId == Const.ProjectBreakGroundMenuId)
                        {
                            var getBreakGround = db.License_BreakGround.FirstOrDefault(x => x.BreakGroundId == updateFlowOperate.DataId);
                            if (getBreakGround != null)
                            {
                                getBreakGround.NextManId = newItem.NextOperaterId;
                                if (newItem.IsAgree == true)
                                {
                                    if (boolIsFlowEnd == true)
                                    {
                                        getBreakGround.NextManId = null;
                                        getBreakGround.States = Const.State_2;
                                        if (getBreakGround.ValidityStartTime.HasValue && getBreakGround.ValidityStartTime < DateTime.Now)
                                        {
                                            int days = 7;
                                            if (getBreakGround.ValidityEndTime.HasValue)
                                            {
                                                days = Convert.ToInt32((getBreakGround.ValidityEndTime - getBreakGround.ValidityStartTime).Value.TotalDays);
                                            }
                                            getBreakGround.ValidityStartTime = DateTime.Now;
                                            getBreakGround.ValidityEndTime = DateTime.Now.AddDays(days);
                                        }
                                    }
                                }
                                else
                                {
                                    getBreakGround.States = Const.State_0;
                                }
                                db.SubmitChanges();
                            }
                        }
                        #endregion
                        #region 夜间施工作业票
                        if (strMenuId == Const.ProjectNightWorkMenuId)
                        {
                            var getNightWork = db.License_NightWork.FirstOrDefault(x => x.NightWorkId == updateFlowOperate.DataId);
                            if (getNightWork != null)
                            {
                                getNightWork.NextManId = newItem.NextOperaterId;
                                if (newItem.IsAgree == true)
                                {
                                    if (boolIsFlowEnd == true)
                                    {
                                        getNightWork.NextManId = null;
                                        getNightWork.States = Const.State_2;
                                        if (getNightWork.ValidityStartTime.HasValue && getNightWork.ValidityStartTime < DateTime.Now)
                                        {
                                            int days = 7;
                                            if (getNightWork.ValidityEndTime.HasValue)
                                            {
                                                days = Convert.ToInt32((getNightWork.ValidityEndTime - getNightWork.ValidityStartTime).Value.TotalDays);
                                            }
                                            getNightWork.ValidityStartTime = DateTime.Now;
                                            getNightWork.ValidityEndTime = DateTime.Now.AddDays(days);
                                        }
                                    }
                                }
                                else
                                {
                                    getNightWork.States = Const.State_0;
                                }
                                db.SubmitChanges();
                            }
                        }
                        #endregion
                        #region 吊装作业票
                        if (strMenuId == Const.ProjectLiftingWorkMenuId)
                        {
                            var getLiftingWork = db.License_LiftingWork.FirstOrDefault(x => x.LiftingWorkId == updateFlowOperate.DataId);
                            if (getLiftingWork != null)
                            {
                                getLiftingWork.NextManId = newItem.NextOperaterId;
                                if (newItem.IsAgree == true)
                                {
                                    if (boolIsFlowEnd == true)
                                    {
                                        getLiftingWork.NextManId = null;
                                        getLiftingWork.States = Const.State_2;
                                        if (getLiftingWork.ValidityStartTime.HasValue && getLiftingWork.ValidityStartTime < DateTime.Now)
                                        {
                                            int days = 7;
                                            if (getLiftingWork.ValidityEndTime.HasValue)
                                            {
                                                days = Convert.ToInt32((getLiftingWork.ValidityEndTime - getLiftingWork.ValidityStartTime).Value.TotalDays);
                                            }
                                            getLiftingWork.ValidityStartTime = DateTime.Now;
                                            getLiftingWork.ValidityEndTime = DateTime.Now.AddDays(days);
                                        }
                                    }
                                }
                                else
                                {
                                    getLiftingWork.States = Const.State_0;
                                }
                                db.SubmitChanges();
                            }
                        }
                        #endregion

                        if (!boolIsFlowEnd && !string.IsNullOrEmpty(newItem.NextOperaterId))
                        {
                            List<string> getUserIds = Funs.GetStrListByStr(newItem.NextOperaterId, ',');
                            foreach (var item in getUserIds)
                            {
                                APICommonService.SendSubscribeMessage(item, "作业票待您审核", UserService.GetUserNameByUserId(newItem.OperaterId), string.Format("{0:yyyy-MM-dd HH:mm:ss}", DateTime.Now));
                            }
                        }
                    }
                    #endregion
                }
            }
        }
        #endregion

        #region 保存下一步审核流程
        /// <summary>
        /// 保存下一步审核流程
        /// </summary>
        /// <param name="newItemList">下一步流程集合</param>
        public static void SaveNextLicenseFlowOperate(List<Model.FlowOperateItem> newItemList)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                string dataId = newItemList.FirstOrDefault().DataId;
                string menuId = newItemList.FirstOrDefault().MenuId;
                string projectId = newItemList.FirstOrDefault().ProjectId;
                if (!string.IsNullOrEmpty(dataId))
                {
                    var getFlowOperateLists = (from x in db.License_FlowOperate where x.DataId == dataId select x).ToList();
                    if (getFlowOperateLists.Count() == 0)
                    {
                        var sysMenuFlowOperate = from x in db.Sys_MenuFlowOperate
                                                 where x.MenuId == menuId
                                                 select x;
                        if (sysMenuFlowOperate.Count() > 0)
                        {
                            foreach (var item in sysMenuFlowOperate)
                            {
                                Model.License_FlowOperate newFlowOperate = new Model.License_FlowOperate
                                {
                                    FlowOperateId = SQLHelper.GetNewID(),
                                    ProjectId = projectId,
                                    DataId = dataId,
                                    MenuId = item.MenuId,
                                    AuditFlowName = item.AuditFlowName,
                                    SortIndex = item.FlowStep,
                                    GroupNum = item.GroupNum,
                                    OrderNum = item.OrderNum,
                                    RoleIds = item.RoleId,
                                    IsFlowEnd = item.IsFlowEnd,
                                };
                                db.License_FlowOperate.InsertOnSubmit(newFlowOperate);
                                db.SubmitChanges();

                                getFlowOperateLists.Add(newFlowOperate);
                            }
                        }
                    }

                    foreach (var item in newItemList)
                    {
                        var getUpdateNextFlow = getFlowOperateLists.FirstOrDefault(x => x.SortIndex == item.SortIndex && x.GroupNum == item.GroupNum && x.OrderNum == item.OrderNum);
                        if (getUpdateNextFlow != null)
                        {
                            getUpdateNextFlow.IsAgree = null;
                            getUpdateNextFlow.Opinion = null;
                            getUpdateNextFlow.OperaterTime = null;
                            getUpdateNextFlow.OperaterId = string.IsNullOrEmpty(item.OperaterId) ? null : item.OperaterId;
                            db.SubmitChanges();
                        }
                    }
                }
            }
        }
        #endregion

        #region 获取当前审核记录
        /// <summary>
        /// 获取当前审核记录
        /// </summary>
        /// <param name="dataId">主键ID</param>
        /// <returns></returns>
        public static Model.FlowOperateItem getLicenseFlowOperate(string dataId, string userId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                ////审核记录
                var getFlowOperate = from x in db.License_FlowOperate
                                     where x.DataId == dataId && x.OperaterId == userId
                                        && (!x.IsClosed.HasValue || x.IsClosed == false)
                                     orderby x.SortIndex, x.GroupNum, x.OrderNum
                                     select new Model.FlowOperateItem
                                     {
                                         FlowOperateId = x.FlowOperateId,
                                         MenuId = x.MenuId,
                                         DataId = x.DataId,
                                         AuditFlowName = x.AuditFlowName,
                                         SortIndex = x.SortIndex ?? 0,
                                         GroupNum = x.GroupNum ?? 0,
                                         OrderNum = x.OrderNum ?? 0,
                                         RoleIds = x.RoleIds,
                                         OperaterId = x.OperaterId,
                                         OperaterName = db.Sys_User.First(y => y.UserId == x.OperaterId).UserName,
                                         IsAgree = x.IsAgree,
                                         Opinion = x.Opinion,
                                         IsFlowEnd = x.IsFlowEnd ?? false,
                                         SignatureUrl = db.Sys_User.First(y => y.UserId == x.OperaterId).SignatureUrl.Replace('\\', '/'),
                                     };
                return getFlowOperate.FirstOrDefault();
            }
        }
        #endregion

        #region 获取下一步审核
        /// <summary>
        /// 获取下一步审核
        /// </summary>
        /// <param name="dataId">主键ID</param>
        /// <returns></returns>
        public static List<Model.FlowOperateItem> getNextLicenseFlowOperate(string strMenuId, Model.LicenseDataItem licenseInfo, Model.FlowOperateItem getNowFlowOperate)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                List<Model.FlowOperateItem> getFlowOperate = new List<Model.FlowOperateItem>();
                var getAllFlows = from x in db.License_FlowOperate
                                  where x.DataId == licenseInfo.LicenseId && (!x.IsFlowEnd.HasValue || x.IsFlowEnd == false)
                                  select x;
                if (licenseInfo == null || string.IsNullOrEmpty(licenseInfo.LicenseId) || getAllFlows.Count() == 0)
                {
                    getFlowOperate = (from x in db.Sys_MenuFlowOperate
                                      where x.MenuId == strMenuId && x.FlowStep == 1 && x.OrderNum == 1
                                      orderby x.FlowStep, x.GroupNum, x.OrderNum
                                      select new Model.FlowOperateItem
                                      {
                                          MenuId = x.MenuId,
                                          AuditFlowName = x.AuditFlowName,
                                          SortIndex = x.FlowStep ?? 0,
                                          GroupNum = x.GroupNum ?? 1,
                                          OrderNum = x.OrderNum ?? 1,
                                          RoleIds = x.RoleId,
                                          IsFlowEnd = x.IsFlowEnd ?? false,
                                      }).ToList();
                }
                else
                {
                    if (licenseInfo.States == Const.State_0)
                    {
                        var getNoCloseFlow = getAllFlows.Where(x => x.OperaterId != null && (!x.IsClosed.HasValue || x.IsClosed == false));
                        if (getNoCloseFlow.Count() > 0)
                        {
                            var getMinSortIndex = getNoCloseFlow.Min(x => x.SortIndex);
                            if (getMinSortIndex.HasValue)
                            {
                                var getGroupList = getNoCloseFlow.Where(x => x.SortIndex == getMinSortIndex).Select(x => x.GroupNum).Distinct();
                                foreach (var item in getGroupList)
                                {
                                    var getMinOrder = (from x in getNoCloseFlow
                                                       where x.SortIndex == getMinSortIndex && x.GroupNum == item
                                                       orderby x.OrderNum
                                                       select new Model.FlowOperateItem
                                                       {
                                                           FlowOperateId = x.FlowOperateId,
                                                           MenuId = x.MenuId,
                                                           DataId = x.DataId,
                                                           ProjectId = x.ProjectId,
                                                           AuditFlowName = x.AuditFlowName,
                                                           SortIndex = x.SortIndex ?? 0,
                                                           GroupNum = x.GroupNum ?? 1,
                                                           OrderNum = x.OrderNum ?? 1,
                                                           RoleIds = x.RoleIds,
                                                           OperaterId = x.OperaterId,
                                                           OperaterName = db.Sys_User.First(y => y.UserId == x.OperaterId).UserName,
                                                           IsAgree = x.IsAgree,
                                                           Opinion = x.Opinion,
                                                           IsFlowEnd = x.IsFlowEnd ?? false,
                                                       }).FirstOrDefault();

                                    if (getMinOrder != null)
                                    {
                                        getFlowOperate.Add(getMinOrder);
                                    }
                                }

                                getFlowOperate = getFlowOperate.OrderBy(x => x.GroupNum).ToList();
                            }
                        }
                    }
                    else if (licenseInfo.States == Const.State_1 && getNowFlowOperate != null)
                    {
                        getFlowOperate = (from x in getAllFlows
                                          where (!x.IsClosed.HasValue || x.IsClosed == false)
                                          && x.SortIndex == getNowFlowOperate.SortIndex && x.GroupNum == getNowFlowOperate.GroupNum && x.OrderNum == (getNowFlowOperate.OrderNum + 1)
                                          orderby x.SortIndex, x.GroupNum, x.OrderNum
                                          select new Model.FlowOperateItem
                                          {
                                              FlowOperateId = x.FlowOperateId,
                                              MenuId = x.MenuId,
                                              DataId = x.DataId,
                                              ProjectId = x.ProjectId,
                                              AuditFlowName = x.AuditFlowName,
                                              SortIndex = x.SortIndex ?? 0,
                                              GroupNum = x.GroupNum ?? 1,
                                              OrderNum = x.OrderNum ?? 1,
                                              RoleIds = x.RoleIds,
                                              OperaterId = x.OperaterId,
                                              OperaterName = db.Sys_User.First(y => y.UserId == x.OperaterId).UserName,
                                              IsAgree = x.IsAgree,
                                              Opinion = x.Opinion,
                                              IsFlowEnd = x.IsFlowEnd ?? false,
                                          }).ToList();
                        if (getFlowOperate.Count() == 0)
                        {
                            var getGroupFlowOperate = getAllFlows.FirstOrDefault(x => (!x.IsClosed.HasValue || x.IsClosed == false)
                                                        && x.SortIndex == getNowFlowOperate.SortIndex && x.FlowOperateId != getNowFlowOperate.FlowOperateId);
                            if (getGroupFlowOperate == null)
                            {
                                getFlowOperate = (from x in getAllFlows
                                                  where (!x.IsClosed.HasValue || x.IsClosed == false)
                                                  && x.SortIndex == (getNowFlowOperate.SortIndex + 1) && x.OrderNum == 1
                                                  orderby x.SortIndex, x.GroupNum, x.OrderNum
                                                  select new Model.FlowOperateItem
                                                  {
                                                      FlowOperateId = x.FlowOperateId,
                                                      MenuId = x.MenuId,
                                                      DataId = x.DataId,
                                                      ProjectId = x.ProjectId,
                                                      AuditFlowName = x.AuditFlowName,
                                                      SortIndex = x.SortIndex ?? 0,
                                                      GroupNum = x.GroupNum ?? 1,
                                                      OrderNum = x.OrderNum ?? 1,
                                                      RoleIds = x.RoleIds,
                                                      OperaterId = x.OperaterId,
                                                      OperaterName = db.Sys_User.First(y => y.UserId == x.OperaterId).UserName,
                                                      IsAgree = x.IsAgree,
                                                      Opinion = x.Opinion,
                                                      IsFlowEnd = x.IsFlowEnd ?? false,
                                                  }).ToList();
                            }
                        }
                    }
                }
                return getFlowOperate;
            }
        }
        #endregion

        #region 删除审核步骤
        /// <summary>
        /// 删除审核步骤
        /// </summary>
        /// <param name="flowOperateId">主键ID</param>
        /// <returns></returns>
        public static void getDeleteLicenseFlowOperate(string flowOperateId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var delteFlow = db.License_FlowOperate.FirstOrDefault(x => x.FlowOperateId == flowOperateId);
                if (delteFlow != null)
                {
                    var isSort = db.License_FlowOperate.FirstOrDefault(x => x.DataId == delteFlow.DataId && x.SortIndex == delteFlow.SortIndex);
                    if (isSort == null)
                    {
                        var updateSort = from x in db.License_FlowOperate
                                         where x.DataId == delteFlow.DataId && x.SortIndex > delteFlow.SortIndex
                                         select x;
                        foreach (var item in updateSort)
                        {
                            item.SortIndex -= 1;
                        }
                    }
                    else
                    {
                        var isGroup = db.License_FlowOperate.FirstOrDefault(x => x.DataId == delteFlow.DataId && x.SortIndex == delteFlow.SortIndex && x.GroupNum == delteFlow.GroupNum);
                        if (isGroup == null)
                        {
                            var updateGroup = from x in db.License_FlowOperate
                                              where x.DataId == delteFlow.DataId && x.SortIndex == delteFlow.SortIndex && x.GroupNum > delteFlow.GroupNum
                                              select x;
                            foreach (var item in updateGroup)
                            {
                                item.GroupNum -= 1;
                            }
                        }
                        else
                        {
                            var isOrder = db.License_FlowOperate.FirstOrDefault(x => x.DataId == delteFlow.DataId && x.SortIndex == delteFlow.SortIndex && x.GroupNum == delteFlow.GroupNum && x.OrderNum > delteFlow.OrderNum);
                            if (isOrder != null)
                            {
                                isOrder.OrderNum -= 1;
                            }
                        }
                    }

                    db.License_FlowOperate.DeleteOnSubmit(delteFlow);
                    db.SubmitChanges();
                }
            }
        }
        #endregion

        #region 获取作业票审核步骤下步分支流程
        /// <summary>
        /// 获取作业票审核步骤下步分支
        /// </summary>
        /// <param name="dataId"></param>
        /// <returns></returns>
        public static List<Model.FlowOperateItem> getNextLicenseFlowOperateGroupList(string flowOperateId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                List<Model.FlowOperateItem> getNextFlowsList = new List<Model.FlowOperateItem>();
                var getFlow = db.License_FlowOperate.FirstOrDefault(x => x.FlowOperateId == flowOperateId);
                if (getFlow != null)
                {
                    getNextFlowsList = (from x in db.License_FlowOperate
                                        where x.DataId == getFlow.DataId && x.SortIndex == (getFlow.SortIndex + 1)
                                        orderby x.SortIndex, x.GroupNum, x.OrderNum
                                        select new Model.FlowOperateItem
                                        {
                                            FlowOperateId = x.FlowOperateId,
                                            MenuId = x.MenuId,
                                            ProjectId = x.ProjectId,
                                            DataId = x.DataId,
                                            AuditFlowName = x.AuditFlowName,
                                            SortIndex = x.SortIndex ?? 0,
                                            GroupNum = x.GroupNum ?? 1,
                                            OrderNum = x.OrderNum ?? 1,
                                            RoleIds = x.RoleIds,
                                            OperaterId = x.OperaterId,
                                            OperaterName = db.Sys_User.First(u => u.UserId == x.OperaterId).UserName,
                                            OperaterTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.OperaterTime),
                                            IsAgree = x.IsAgree,
                                            Opinion = x.Opinion,
                                            IsFlowEnd = x.IsFlowEnd ?? false,
                                        }).ToList();
                }

                return getNextFlowsList;
            }
        }
        #endregion  

        #region 重申请作业票信息
        /// <summary>
        /// 重申请作业票信息
        /// </summary>
        /// <param name="newItem">作业票</param>
        /// <returns></returns>
        public static string SaveLicenseDataReApply(Model.LicenseDataItem newItem)
        {
            string strLicenseId = newItem.LicenseId;
            string projectId = newItem.ProjectId;
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {              
                //// 删除未审核的流程记录
                var getDelFlows = from x in db.License_FlowOperate
                                  where x.DataId == strLicenseId && (!x.IsClosed.HasValue || x.IsClosed == false)
                                  select x;
                if (getDelFlows.Count() > 0)
                {
                    db.License_FlowOperate.DeleteAllOnSubmit(getDelFlows);
                }
                //// 删除编码表记录
                CodeRecordsService.DeleteCodeRecordsByDataId(strLicenseId);
                #region 动火作业票
                if (newItem.MenuId == Const.ProjectFireWorkMenuId)
                {
                    var updateFireWork = db.License_FireWork.FirstOrDefault(x => x.FireWorkId == strLicenseId);
                    if (updateFireWork != null)
                    {
                        updateFireWork.NextManId = null;
                        updateFireWork.States = Const.State_C;
                    }
                }
                #endregion
                #region 高处作业票
                else if (newItem.MenuId == Const.ProjectHeightWorkMenuId)
                {
                    var updateHeightWork = db.License_HeightWork.FirstOrDefault(x => x.HeightWorkId == strLicenseId);
                    if (updateHeightWork != null)
                    {
                        updateHeightWork.NextManId = null;
                        updateHeightWork.States = Const.State_C;
                    }
                }
                #endregion
                #region 受限空间作业票           
                else if (newItem.MenuId == Const.ProjectLimitedSpaceMenuId)
                {
                    var updateLimitedSpace = db.License_LimitedSpace.FirstOrDefault(x => x.LimitedSpaceId == strLicenseId);
                    if (updateLimitedSpace != null)
                    {
                        updateLimitedSpace.NextManId = null;
                        updateLimitedSpace.States = Const.State_C;
                    }
                }
                #endregion
                #region 射线作业票
                else if (newItem.MenuId == Const.ProjectRadialWorkMenuId)
                {
                    var updateRadialWork = db.License_RadialWork.FirstOrDefault(x => x.RadialWorkId == strLicenseId);
                    if (updateRadialWork == null)
                    {
                        updateRadialWork.NextManId = null;
                        updateRadialWork.States = Const.State_C;
                    }
                }
                #endregion
                #region 断路(占道)作业票
                else if (newItem.MenuId == Const.ProjectOpenCircuitMenuId)
                {
                    var updateOpenCircuit = db.License_OpenCircuit.FirstOrDefault(x => x.OpenCircuitId == strLicenseId);
                    if (updateOpenCircuit == null)
                    {
                        updateOpenCircuit.NextManId = null;
                        updateOpenCircuit.States = Const.State_C;
                    }
                }
                #endregion
                #region 动土作业票
                else if (newItem.MenuId == Const.ProjectBreakGroundMenuId)
                {
                    var updateBreakGround = db.License_BreakGround.FirstOrDefault(x => x.BreakGroundId == strLicenseId);
                    if (updateBreakGround == null)
                    {
                        updateBreakGround.NextManId = null;
                        updateBreakGround.States = Const.State_C;
                    }
                }
                #endregion
                #region 夜间施工作业票
                else if (newItem.MenuId == Const.ProjectNightWorkMenuId)
                {
                    var updateNightWork = db.License_NightWork.FirstOrDefault(x => x.NightWorkId == strLicenseId);
                    if (updateNightWork != null)
                    {
                        updateNightWork.NextManId = null;
                        updateNightWork.States = Const.State_C;
                    }
                }
                #endregion
                #region 吊装作业票
                else if (newItem.MenuId == Const.ProjectLiftingWorkMenuId)
                {
                    var updateLiftingWork = db.License_LiftingWork.FirstOrDefault(x => x.LiftingWorkId == strLicenseId);
                    if (updateLiftingWork != null)
                    {
                        updateLiftingWork.NextManId = null;
                        updateLiftingWork.States = Const.State_C;
                    }
                }
                #endregion

                db.SubmitChanges();
            }

            newItem.OldLicenseId = strLicenseId;
            newItem.LicenseId = string.Empty;
            newItem.States = Const.State_0;
            return SaveLicenseData(newItem);
        }
        #endregion
    }
}
