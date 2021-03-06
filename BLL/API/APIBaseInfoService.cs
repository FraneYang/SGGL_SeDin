﻿using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    public static class APIBaseInfoService
    {
        #region 获取常量
        /// <summary>
        /// 获取培训级别
        /// </summary>
        /// <returns></returns>
        public static List<Model.BaseInfoItem> getSysConst(string groupId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var getDataLists = (from x in db.Sys_Const
                                    where x.GroupId == groupId
                                    orderby x.SortIndex
                                    select new Model.BaseInfoItem { BaseInfoId = x.ConstValue, BaseInfoCode = x.SortIndex.ToString(), BaseInfoName = x.ConstText }).ToList();
                return getDataLists;
            }
        }
        #endregion

        #region 获取项目列表
        /// <summary>
        /// 获取项目列表
        /// </summary>
        /// <returns></returns>
        public static List<Model.BaseInfoItem> getProjectList()
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var getDataLists = (from x in db.Base_Project
                                    orderby x.ProjectCode
                                    select new Model.BaseInfoItem
                                    {
                                        BaseInfoId = x.ProjectId,
                                        BaseInfoCode = x.ProjectCode,
                                        BaseInfoName = x.ShortName ?? x.ProjectName,
                                    }
                                ).ToList();
                return getDataLists;
            }
        }
        #endregion

        #region 根据项目号获取项目信息
        /// <summary>
        /// 根据项目号获取项目信息
        /// </summary>
        /// <param name="projectCode"></param>
        /// <returns></returns>
        public static Model.BaseInfoItem getProjectByCode(string projectCode)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var getDataLists = (from x in db.Base_Project
                                    where x.ProjectCode == projectCode
                                    select new Model.BaseInfoItem
                                    {
                                        BaseInfoId = x.ProjectId,
                                        BaseInfoCode = x.ProjectCode,
                                        BaseInfoName = x.ProjectName
                                    }
                                ).FirstOrDefault();
                return getDataLists;
            }
        }
        #endregion

        #region 根据项目id获取区域表
        /// <summary>
        /// 根据项目id获取区域表
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public static List<Model.BaseInfoItem> getProjectWorkArea(string projectId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var getDataLists = (from x in db.WBS_UnitWork
                                    where x.ProjectId == projectId && x.SuperUnitWork == null
                                    orderby x.UnitWorkCode
                                    select new Model.BaseInfoItem
                                    {
                                        BaseInfoId = x.UnitWorkId,
                                        BaseInfoCode = x.UnitWorkCode,
                                        BaseInfoName = BLL.UnitWorkService.GetUnitWorkALLName(x.UnitWorkId)
                                    }
                                ).ToList();
                return getDataLists;
            }
        }

        /// <summary>
        /// 根据项目、单位ID获取单位工程
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public static List<Model.BaseInfoItem> GetProjecUnitWorkByUnitId(string projectId, string unitId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var getDataLists = (from x in db.WBS_UnitWork
                                    where x.ProjectId == projectId && x.UnitId == unitId && x.SuperUnitWork == null
                                    orderby x.UnitWorkCode
                                    select new Model.BaseInfoItem
                                    {
                                        BaseInfoId = x.UnitWorkId,
                                        BaseInfoCode = x.UnitWorkCode,
                                        BaseInfoName = BLL.UnitWorkService.GetUnitWorkALLName(x.UnitWorkId)
                                    }
                                ).ToList();
                return getDataLists;
            }
        }

        #endregion

        #region 获取材质列表
        /// <summary>
        /// 获取材质列表
        /// </summary>
        /// <returns></returns>
        public static List<Model.BaseInfoItem> GetMaterial()
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var getDataLists = (from x in db.Base_Material
                                    orderby x.MaterialCode
                                    select new Model.BaseInfoItem
                                    {
                                        BaseInfoId = x.MaterialId,
                                        BaseInfoCode = x.MaterialCode,
                                        BaseInfoName = x.MetalType
                                    }
                                ).ToList();
                return getDataLists;
            }
        }
        #endregion

        #region 焊接探伤类型，探伤比例
        /// <summary>
        /// 获取探伤类型
        /// </summary>
        /// <returns></returns>
        public static List<Model.BaseInfoItem> getDetectionType()
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var getDataLists = (from x in db.Base_DetectionType
                                    orderby x.DetectionTypeCode
                                    select new Model.BaseInfoItem
                                    {
                                        BaseInfoId = x.DetectionTypeId,
                                        BaseInfoCode = x.DetectionTypeCode,
                                        BaseInfoName = x.DetectionTypeName
                                    }
                                ).ToList();
                return getDataLists;
            }
        }

        /// <summary>
        /// 获取探伤比例
        /// </summary>
        /// <returns></returns>
        public static List<Model.BaseInfoItem> getDetectionRate()
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var getDataLists = (from x in db.Base_DetectionRate
                                    orderby x.DetectionRateCode
                                    select new Model.BaseInfoItem
                                    {
                                        BaseInfoId = x.DetectionRateId,
                                        BaseInfoCode = x.DetectionRateCode,
                                        BaseInfoName = x.DetectionRateValue.Value.ToString() + "%"
                                    }
                                ).ToList();
                return getDataLists;
            }
        }
        #endregion

        #region 根据类型获取巡检隐患类型表
        /// <summary>
        /// 根据类型获取巡检隐患类型表
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public static List<Model.BaseInfoItem> getHazardRegisterTypes(string type)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var getDataLists = (from x in db.HSSE_Hazard_HazardRegisterTypes
                                    where x.HazardRegisterType == type
                                    orderby x.TypeCode
                                    select new Model.BaseInfoItem { BaseInfoId = x.RegisterTypesId, BaseInfoCode = x.TypeCode, BaseInfoName = x.RegisterTypesName }).ToList();
                return getDataLists;
            }
        }
        #endregion                     

        #region 根据项目id获取项目图片
        /// <summary>
        /// 根据项目id获取项目图片
        /// </summary>
        /// <param name="projectId"></param>        
        /// <returns></returns>
        public static List<Model.BaseInfoItem> getProjectPictureByProjectId(string projectId, string pictureType, string strParam)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var getDataLists = (from x in db.InformationProject_Picture
                                    join y in db.AttachFile on x.PictureId equals y.ToKeyId
                                    where x.States == Const.State_2 && y.AttachUrl != null && x.ProjectId == projectId && x.PictureType == pictureType
                                    orderby x.UploadDate descending
                                    select new Model.BaseInfoItem
                                    {
                                        BaseInfoId = x.PictureId,
                                        BaseInfoName = x.Title,
                                        BaseInfoCode = string.Format("{0:yyyy-MM-dd}", x.UploadDate),
                                        ImageUrl = y.AttachUrl.Replace('\\', '/'),
                                    });
                if (!string.IsNullOrEmpty(strParam))
                {
                    getDataLists = getDataLists.Where(x => x.BaseInfoName.Contains(strParam));
                }
                return getDataLists.ToList();
            }
        }

        /// <summary>
        /// 项目图片信息保存方法
        /// </summary>
        /// <param name="picture">图片信息</param>
        public static void SaveProjectPicture(Model.PictureItem picture)
        {
            Model.InformationProject_Picture newPicture = new Model.InformationProject_Picture
            {
                PictureId = picture.PictureId,
                ProjectId = picture.ProjectId,
                Title = picture.Title,
                ContentDef = picture.ContentDef,
                PictureType = picture.PictureTypeId,
                UploadDate = System.DateTime.Now,
                States = Const.State_2,
                CompileMan = picture.CompileManId,
            };

            if (string.IsNullOrEmpty(newPicture.PictureId))
            {
                newPicture.PictureId = SQLHelper.GetNewID();
                PictureService.AddPicture(newPicture);
            }
            else
            {
                PictureService.UpdatePicture(newPicture);
            }

            CommonService.btnSaveData(newPicture.ProjectId, Const.ProjectPictureMenuId, newPicture.PictureId, newPicture.CompileMan, true, newPicture.Title, "../InformationProject/PictureView.aspx?PictureId={0}");
            //// 保存附件
            APIUpLoadFileService.SaveAttachUrl(Const.ProjectPictureMenuId, newPicture.PictureId, picture.AttachUrl, "0");
        }
        #endregion

        #region 根据项目id获取项目地图
        /// <summary>
        /// 根据项目id获取项目地图
        /// </summary>
        /// <param name="projectId"></param>        
        /// <returns></returns>
        public static List<Model.BaseInfoItem> getProjectMapByProjectId(string projectId, string mapType)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var getDataLists = (from x in db.InformationProject_ProjectMap
                                    join y in db.AttachFile on x.ProjectMapId equals y.ToKeyId
                                    where y.AttachUrl != null && x.ProjectId == projectId && x.MapType == mapType
                                    orderby x.UploadDate descending
                                    select new Model.BaseInfoItem
                                    {
                                        BaseInfoId = x.ProjectMapId,
                                        BaseInfoName = x.Title,
                                        BaseInfoCode = string.Format("{0:yyyy-MM-dd}", x.UploadDate),
                                        ImageUrl = y.AttachUrl.Replace('\\', '/'),
                                    }).Take(5).ToList();
                return getDataLists;
            }
        }

        /// <summary>
        /// 项目地图信息保存方法
        /// </summary>
        /// <param name="projectMap">地图信息</param>
        public static void SaveProjectMap(Model.PictureItem projectMap)
        {
            Model.InformationProject_ProjectMap newProjectMap = new Model.InformationProject_ProjectMap
            {
                ProjectMapId = projectMap.PictureId,
                ProjectId = projectMap.ProjectId,
                Title = projectMap.Title,
                ContentDef = projectMap.ContentDef,
                MapType = projectMap.PictureTypeId,
                UploadDate = System.DateTime.Now,
                CompileMan = projectMap.CompileManId,
            };

            if (string.IsNullOrEmpty(newProjectMap.ProjectMapId))
            {
                newProjectMap.ProjectMapId = SQLHelper.GetNewID();
                ProjectMapService.AddProjectMap(newProjectMap);
            }
            else
            {
                ProjectMapService.UpdateProjectMap(newProjectMap);
            }
            //// 保存附件
            APIUpLoadFileService.SaveAttachUrl(Const.ProjectProjectMapMenuId, newProjectMap.ProjectMapId, projectMap.AttachUrl, "0");
        }
        #endregion

        #region 获取通知通告
        /// <summary>
        /// 根据项目id获取通知通告
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public static List<Model.NoticeItem> getNoticesList(string projectId, string strParam)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var getDataLists = (from x in db.InformationProject_Notice
                                    where x.AccessProjectId.Contains(projectId) && x.IsRelease == true
                                    && (strParam == null || x.NoticeTitle.Contains(strParam))
                                    orderby x.ReleaseDate descending
                                    select new Model.NoticeItem
                                    {
                                        NoticeId = x.NoticeId,
                                        NoticeCode = x.NoticeCode,
                                        NoticeTitle = x.NoticeTitle,
                                        ReleaseDate = string.Format("{0:yyyy-MM-dd HH:mm}", x.ReleaseDate)
                                    }).ToList();
                return getDataLists;
            }
        }

        /// <summary>
        /// 根据项目id获取通知通告
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public static List<Model.NoticeItem> getNoticesList(string projectId, string userId, string strParam)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var getDataLists = (from x in db.InformationProject_Notice
                                    where x.AccessProjectId.Contains(projectId) && x.IsRelease == true
                                    && (strParam == null || x.NoticeTitle.Contains(strParam))
                                    select new Model.NoticeItem
                                    {
                                        NoticeId = x.NoticeId,
                                        NoticeCode = x.NoticeCode,
                                        NoticeTitle = x.NoticeTitle,
                                        ReleaseDate = string.Format("{0:yyyy-MM-dd HH:mm}", x.ReleaseDate),
                                        IsRead = db.Sys_UserRead.FirstOrDefault(y => y.DataId == x.NoticeId && y.ProjectId == projectId && y.UserId == userId) == null ? false : true,
                                    }).ToList();
                return getDataLists.OrderBy(x => x.IsRead).ThenByDescending(x => x.ReleaseDate).ToList();
            }
        }

        /// <summary>
        /// 根据项目id获取通知通告
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public static Model.NoticeItem getNoticesByNoticeId(string noticeId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var getDataLists = (from x in db.InformationProject_Notice
                                    where x.NoticeId == noticeId
                                    select new Model.NoticeItem
                                    {
                                        NoticeId = x.NoticeId,
                                        NoticeCode = x.NoticeCode,
                                        NoticeTitle = x.NoticeTitle,
                                        ReleaseDate = string.Format("{0:yyyy-MM-dd HH:mm}", x.ReleaseDate),
                                        MainContent = x.MainContent,
                                        AttachUrl = db.AttachFile.FirstOrDefault(y => y.ToKeyId == x.NoticeId).AttachUrl.Replace("\\", "/"),
                                    }).FirstOrDefault();
                return getDataLists;
            }
        }
        #endregion

        #region 获取岗位信息
        /// <summary>
        /// 获取岗位信息
        /// </summary>
        /// <returns></returns>
        public static List<Model.BaseInfoItem> getWorkPost(string strParam)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var getDataLists = (from x in db.Base_WorkPost
                                    where strParam == null || x.WorkPostName.Contains(strParam)
                                    orderby x.WorkPostName
                                    select new Model.BaseInfoItem { BaseInfoId = x.WorkPostId, BaseInfoCode = x.WorkPostCode, BaseInfoName = x.WorkPostName }).ToList();
                return getDataLists;
            }
        }
        #endregion

        #region 获取培训类别
        /// <summary>
        /// 获取培训类别
        /// </summary>
        /// <returns></returns>
        public static List<Model.BaseInfoItem> getTrainType()
        {
            var getDataLists = (from x in Funs.DB.Base_TrainType
                                orderby x.TrainTypeCode
                                select new Model.BaseInfoItem { BaseInfoId = x.TrainTypeId, BaseInfoCode = x.TrainTypeCode, BaseInfoName = x.TrainTypeName }).ToList();
            return getDataLists;
        }
        #endregion

        #region 获取培训级别
        /// <summary>
        /// 获取培训级别
        /// </summary>
        /// <returns></returns>
        public static List<Model.BaseInfoItem> getTrainLevel()
        {
            var getDataLists = (from x in Funs.DB.Base_TrainLevel
                                orderby x.TrainLevelCode
                                select new Model.BaseInfoItem { BaseInfoId = x.TrainLevelId, BaseInfoCode = x.TrainLevelCode, BaseInfoName = x.TrainLevelName }).ToList();
            return getDataLists;
        }
        #endregion

        #region 获取法律法规类型
        /// <summary>
        /// 获取法律法规类型
        /// </summary>
        /// <returns></returns>
        public static List<Model.BaseInfoItem> getLawsRegulationsType()
        {
            var getDataLists = (from x in Funs.DB.Base_LawsRegulationsType
                                orderby x.Code
                                select new Model.BaseInfoItem { BaseInfoId = x.Id, BaseInfoCode = x.Code, BaseInfoName = x.Name }).ToList();
            return getDataLists;
        }
        #endregion

        #region 获取标准规范类型
        /// <summary>
        /// 获取标准规范类型
        /// </summary>
        /// <returns></returns>
        public static List<Model.BaseInfoItem> getHSSEStandardListType()
        {
            var getDataLists = (from x in Funs.DB.Base_HSSEStandardListType
                                orderby x.TypeCode
                                select new Model.BaseInfoItem { BaseInfoId = x.TypeId, BaseInfoCode = x.TypeCode, BaseInfoName = x.TypeName }).ToList();
            return getDataLists;
        }
        #endregion

        #region 获取规章制度类型
        /// <summary>
        /// 获取规章制度类型
        /// </summary>
        /// <returns></returns>
        public static List<Model.BaseInfoItem> getRulesRegulationsType()
        {
            var getDataLists = (from x in Funs.DB.Base_RulesRegulationsType
                                orderby x.RulesRegulationsTypeCode
                                select new Model.BaseInfoItem { BaseInfoId = x.RulesRegulationsTypeId, BaseInfoCode = x.RulesRegulationsTypeCode, BaseInfoName = x.RulesRegulationsTypeName }).ToList();
            return getDataLists;
        }
        #endregion

        #region 获取管理规定类型
        /// <summary>
        /// 获取管理规定类型
        /// </summary>
        /// <returns></returns>
        public static List<Model.BaseInfoItem> getManageRuleType()
        {
            var getDataLists = (from x in Funs.DB.Base_ManageRuleType
                                orderby x.ManageRuleTypeCode
                                select new Model.BaseInfoItem { BaseInfoId = x.ManageRuleTypeId, BaseInfoCode = x.ManageRuleTypeCode, BaseInfoName = x.ManageRuleTypeName }).ToList();
            return getDataLists;
        }
        #endregion

        #region 获取机具设备类型
        /// <summary>
        /// 获取机具设备类型
        /// </summary>
        /// <returns></returns>
        public static List<Model.BaseInfoItem> getSpecialEquipment(bool isSpecial)
        {
            var getDataLists = (from x in Funs.DB.Base_SpecialEquipment
                                where x.IsSpecial == isSpecial
                                orderby x.SpecialEquipmentCode
                                select new Model.BaseInfoItem { BaseInfoId = x.SpecialEquipmentId, BaseInfoCode = x.SpecialEquipmentCode, BaseInfoName = x.SpecialEquipmentName }).ToList();
            return getDataLists;
        }
        #endregion

        #region 获取工作阶段
        /// <summary>
        /// 获取工作阶段
        /// </summary>
        /// <returns></returns>
        public static List<Model.BaseInfoItem> getWorkStage()
        {
            var getDataLists = (from x in Funs.DB.Base_WorkStage
                                orderby x.WorkStageCode
                                select new Model.BaseInfoItem { BaseInfoId = x.WorkStageId, BaseInfoCode = x.WorkStageCode, BaseInfoName = x.WorkStageName }).ToList();
            return getDataLists;
        }
        #endregion

        #region 获取项目班组
        /// <summary>
        /// 获取项目班组
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public static List<Model.BaseInfoItem> getTeamGroup(string projectId, string unitId)
        {
            var getDataLists = (from x in Funs.DB.ProjectData_TeamGroup
                                where x.ProjectId == projectId && (unitId == null || x.UnitId == unitId)
                                orderby x.TeamGroupCode
                                select new Model.BaseInfoItem { BaseInfoId = x.TeamGroupId, BaseInfoCode = x.TeamGroupCode, BaseInfoName = x.TeamGroupName }).ToList();
            return getDataLists;
        }

        /// <summary>
        /// 获取项目班组组长
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public static Model.BaseInfoItem getTeamGroupLeader(string teamGroupId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var getDataLists = (from x in db.ProjectData_TeamGroup
                                    join y in db.SitePerson_Person on x.GroupLeaderId equals y.PersonId
                                    where x.TeamGroupId == teamGroupId && y.PersonId != null
                                    select new Model.BaseInfoItem { BaseInfoId = y.PersonId, BaseInfoCode = y.CardNo, BaseInfoName = y.PersonName }).FirstOrDefault();
                return getDataLists;
            }
        }
        #endregion

        #region 获取项目区域
        /// <summary>
        /// 获取项目区域
        /// </summary>
        /// <returns></returns>
        public static List<Model.BaseInfoItem> getWorkArea(string projectId, string unitId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var getDataLists = (from x in db.WBS_UnitWork
                                    where x.ProjectId == projectId && (unitId == null || x.UnitId == unitId)
                                    orderby x.UnitWorkCode
                                    select new Model.BaseInfoItem { BaseInfoId = x.UnitWorkId, BaseInfoCode = x.UnitWorkCode, BaseInfoName = x.UnitWorkName }).ToList();
                return getDataLists;
            }
        }
        #endregion

        #region 获取作业票-安全措施
        /// <summary>
        /// 获取作业票-安全措施
        /// </summary>
        /// <returns></returns>
        public static List<Model.BaseInfoItem> getSafetyMeasures(string licenseType)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var getDataLists = (from x in db.Base_SafetyMeasures
                                    where x.LicenseType == licenseType
                                    orderby x.SortIndex
                                    select new Model.BaseInfoItem { BaseInfoId = x.SafetyMeasuresId, BaseInfoCode = x.SortIndex.ToString(), BaseInfoName = x.SafetyMeasures }).ToList();
                return getDataLists;
            }
        }
        #endregion

        #region 获取图片分类
        /// <summary>
        /// 获取图片分类
        /// </summary>
        /// <returns></returns>
        public static List<Model.BaseInfoItem> getPictureType(string menuType)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var getDataLists = (from x in db.Base_PictureType
                                    where menuType == null || x.MenuType == menuType
                                    orderby x.Code
                                    select new Model.BaseInfoItem { BaseInfoId = x.PictureTypeId, BaseInfoCode = x.Code, BaseInfoName = x.Name }).ToList();
                return getDataLists;
            }
        }
        #endregion

        #region 获取作业许可证(定稿)类型分类
        /// <summary>
        /// 获取作业许可证类型分类
        /// </summary>
        /// <returns></returns>
        public static List<Model.BaseInfoItem> getBase_LicenseType()
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var getDataLists = (from x in db.Base_LicenseType
                                    orderby x.LicenseTypeCode
                                    select new Model.BaseInfoItem { BaseInfoId = x.LicenseTypeId, BaseInfoCode = x.LicenseTypeCode, BaseInfoName = x.LicenseTypeName }).ToList();
                return getDataLists;
            }
        }
        #endregion

        #region 获取特岗证书
        /// <summary>
        /// 获取图片分类
        /// </summary>
        /// <returns></returns>
        public static List<Model.BaseInfoItem> getCertificate()
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var getDataLists = (from x in db.Base_Certificate
                                    orderby x.CertificateCode
                                    select new Model.BaseInfoItem { BaseInfoId = x.CertificateId, BaseInfoCode = x.CertificateCode, BaseInfoName = x.CertificateName }).ToList();
                return getDataLists;
            }
        }
        #endregion

        #region 获取部门
        /// <summary>
        /// 获取图片分类
        /// </summary>
        /// <returns></returns>
        public static List<Model.BaseInfoItem> getDepart()
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var getDataLists = (from x in db.Base_Depart
                                    orderby x.DepartCode
                                    select new Model.BaseInfoItem { BaseInfoId = x.DepartId, BaseInfoCode = x.DepartCode, BaseInfoName = x.DepartName }).ToList();
                return getDataLists;
            }
        }
        #endregion

        #region 获取应急预案类型
        /// <summary>
        /// 获取图片分类
        /// </summary>
        /// <returns></returns>
        public static List<Model.BaseInfoItem> getEmergencyType()
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var getDataLists = (from x in db.Base_EmergencyType
                                    orderby x.EmergencyTypeCode
                                    select new Model.BaseInfoItem { BaseInfoId = x.EmergencyTypeId, BaseInfoCode = x.EmergencyTypeCode, BaseInfoName = x.EmergencyTypeName }).ToList();
                return getDataLists;
            }
        }
        #endregion

        #region 获取国家基础数据
        /// <summary>
        /// 获取国家基础数据
        /// </summary>
        /// <returns></returns>
        public static List<Model.BaseInfoItem> getCountry()
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var getDataLists = (from x in db.RealName_Country
                                    orderby x.Name
                                    select new Model.BaseInfoItem { BaseInfoId = x.CountryId, BaseInfoCode = x.Name, BaseInfoName = x.Cname }).ToList();
                return getDataLists;
            }
        }
        #endregion

        #region 获取省份基础数据
        /// <summary>
        /// 获取省份基础数据
        /// </summary>
        /// <returns></returns>
        public static List<Model.BaseInfoItem> getProvinceByCountry(string countryId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var getDataLists = (from x in db.RealName_City
                                    where countryId == null || countryId == "" || x.CountryId == countryId
                                    orderby x.ProvinceCode
                                    select new Model.BaseInfoItem { BaseInfoId = x.ProvinceCode, BaseInfoCode = x.Name, BaseInfoName = x.Cname }).ToList();
                return getDataLists;
            }
        }
        #endregion

        #region 获取实名制数据字典信息
        /// <summary>
        /// 获取实名制数据字典信息
        /// </summary>
        /// <returns></returns>
        public static List<Model.BaseInfoItem> getBasicDataByDictTypeCode(string dictTypeCode)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var getDataLists = (from x in db.RealName_BasicData
                                    where x.DictTypeCode == dictTypeCode
                                    orderby x.DictCode
                                    select new Model.BaseInfoItem { BaseInfoId = x.DictCode, BaseInfoCode = x.DictCode, BaseInfoName = x.DictName }).ToList();
                return getDataLists;
            }
        }
        #endregion
    }
}
