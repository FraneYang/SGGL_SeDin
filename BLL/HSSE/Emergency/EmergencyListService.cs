using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 应急预案管理
    /// </summary>
    public static class EmergencyListService
    {
        public static Model.SGGLDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取应急预案管理
        /// </summary>
        /// <param name="EmergencyListId"></param>
        /// <returns></returns>
        public static Model.Emergency_EmergencyList GetEmergencyListById(string EmergencyListId)
        {
            return Funs.DB.Emergency_EmergencyList.FirstOrDefault(e => e.EmergencyListId == EmergencyListId);
        }

        /// <summary>
        /// 获取时间段文件、方案修编情况说明
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public static List<Model.Emergency_EmergencyList> GetEmergencyListByDate(string projectId, DateTime startTime, DateTime endTime)
        {
            return (from x in Funs.DB.Emergency_EmergencyList where x.ProjectId == projectId && x.CompileDate >= startTime && x.CompileDate <= endTime select x).ToList();
        }

        /// <summary>
        /// 根据应急预案类型获取应急预案信息集合
        /// </summary>
        /// <param name="emergencyType">应急预案类型</param>
        /// <param name="projectId">项目号</param>
        /// <returns>应急预案实体集合</returns>
        public static List<Model.Emergency_EmergencyList> GetEmergencyListsByEmergencyType(string emergencyType, string projectId, DateTime startTime, DateTime endTime)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                return (from x in db.Emergency_EmergencyList
                        join y in db.Base_EmergencyType
                        on x.EmergencyTypeId equals y.EmergencyTypeId
                        where y.EmergencyTypeName.Contains(emergencyType) && x.ProjectId == projectId && x.CompileDate >= startTime && x.CompileDate < endTime
                        select x).ToList();
            }
        }

        /// <summary>
        /// 根据应急预案类型获取其他应急预案信息集合
        /// </summary>
        /// <param name="emergencyType">应急预案类型</param>
        /// <param name="projectId">项目号</param>
        /// <returns>其他应急预案实体集合</returns>
        public static List<Model.Emergency_EmergencyList> GetOtherEmergencyListsByEmergencyType(string emergencyType, string projectId, DateTime startTime, DateTime endTime)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                return (from x in db.Emergency_EmergencyList
                        join y in db.Base_EmergencyType
                        on x.EmergencyTypeId equals y.EmergencyTypeId
                        where !y.EmergencyTypeName.Contains(emergencyType) && x.ProjectId == projectId && x.CompileDate >= startTime && x.CompileDate < endTime
                        select x).ToList();
            }
        }

        /// <summary>
        /// 添加应急预案管理
        /// </summary>
        /// <param name="EmergencyList"></param>
        public static void AddEmergencyList(Model.Emergency_EmergencyList EmergencyList)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Emergency_EmergencyList newEmergencyList = new Model.Emergency_EmergencyList
            {
                EmergencyListId = EmergencyList.EmergencyListId,
                ProjectId = EmergencyList.ProjectId,
                EmergencyCode = EmergencyList.EmergencyCode,
                EmergencyName = EmergencyList.EmergencyName,
                UnitId = EmergencyList.UnitId,
                EmergencyTypeId = EmergencyList.EmergencyTypeId,
                VersionCode = EmergencyList.VersionCode,
                EmergencyContents = EmergencyList.EmergencyContents,
                CompileMan = EmergencyList.CompileMan,
                CompileDate = EmergencyList.CompileDate,
                States = EmergencyList.States,
                AttachUrl = EmergencyList.AttachUrl,
                AuditMan = EmergencyList.AuditMan,
                ApproveMan = EmergencyList.ApproveMan
            };
            db.Emergency_EmergencyList.InsertOnSubmit(newEmergencyList);
            db.SubmitChanges();
            ////增加一条编码记录
            BLL.CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(BLL.Const.ProjectEmergencyListMenuId, EmergencyList.ProjectId, null, EmergencyList.EmergencyListId, EmergencyList.CompileDate);
        }

        /// <summary>
        /// 修改应急预案管理
        /// </summary>
        /// <param name="EmergencyList"></param>
        public static void UpdateEmergencyList(Model.Emergency_EmergencyList EmergencyList)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Emergency_EmergencyList newEmergencyList = db.Emergency_EmergencyList.FirstOrDefault(e => e.EmergencyListId == EmergencyList.EmergencyListId);
            if (newEmergencyList != null)
            {
                newEmergencyList.EmergencyCode = EmergencyList.EmergencyCode;
                newEmergencyList.EmergencyName = EmergencyList.EmergencyName;
                newEmergencyList.UnitId = EmergencyList.UnitId;
                newEmergencyList.EmergencyTypeId = EmergencyList.EmergencyTypeId;
                newEmergencyList.VersionCode = EmergencyList.VersionCode;
                newEmergencyList.EmergencyContents = EmergencyList.EmergencyContents;
                newEmergencyList.CompileMan = EmergencyList.CompileMan;
                newEmergencyList.CompileDate = EmergencyList.CompileDate;
                newEmergencyList.States = EmergencyList.States;
                newEmergencyList.AttachUrl = EmergencyList.AttachUrl;
                newEmergencyList.AuditMan = EmergencyList.AuditMan;
                newEmergencyList.ApproveMan = EmergencyList.ApproveMan;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除应急预案管理
        /// </summary>
        /// <param name="EmergencyListId"></param>
        public static void DeleteEmergencyListById(string EmergencyListId)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Emergency_EmergencyList EmergencyList = db.Emergency_EmergencyList.FirstOrDefault(e => e.EmergencyListId == EmergencyListId);
            if (EmergencyList != null)
            {
                ///删除编码表记录
                BLL.CodeRecordsService.DeleteCodeRecordsByDataId(EmergencyList.EmergencyListId);
                ////删除附件表
                BLL.CommonService.DeleteAttachFileById(EmergencyList.EmergencyListId);
                ////删除流程表
                BLL.CommonService.DeleteFlowOperateByID(EmergencyList.EmergencyListId);
                db.Emergency_EmergencyList.DeleteOnSubmit(EmergencyList);
                db.SubmitChanges();
            }
        }
    }
}
