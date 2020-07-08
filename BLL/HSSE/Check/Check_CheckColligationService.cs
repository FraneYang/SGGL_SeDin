using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace BLL
{
    /// <summary>
    /// 综合检查
    /// </summary>
    public static class Check_CheckColligationService
    {
        /// <summary>
        /// 根据综合检查ID获取综合检查信息
        /// </summary>
        /// <param name="CheckColligationName"></param>
        /// <returns></returns>
        public static Model.Check_CheckColligation GetCheckColligationByCheckColligationId(string checkColligationId)
        {
            return new Model.SGGLDB(Funs.ConnString).Check_CheckColligation.FirstOrDefault(e => e.CheckColligationId == checkColligationId);
        }

        /// <summary>
        /// 根据时间段及检查类型获取综合大检查信息集合
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="projectId"></param>
        /// <param name="checkType"></param>
        /// <returns></returns>
        public static int GetCountByCheckTimeAndCheckType(DateTime startTime, DateTime endTime, string projectId, string checkType)
        {
            return (from x in new Model.SGGLDB(Funs.ConnString).Check_CheckColligation where x.CheckTime >= startTime && x.CheckTime < endTime && x.ProjectId == projectId && x.CheckType == checkType && x.States == BLL.Const.State_2 select x).Count();
        }

        /// <summary>
        /// 根据时间段获取综合大检查信息集合
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public static int GetCountByCheckTime(DateTime startTime, DateTime endTime, string projectId)
        {
            return (from x in new Model.SGGLDB(Funs.ConnString).Check_CheckColligation where x.CheckTime >= startTime && x.CheckTime < endTime && x.ProjectId == projectId select x).Count();
        }

        /// <summary>
        /// 根据时间段获取已完成的综合大检查整改数量
        /// </summary>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="projectId">项目号</param>
        /// <returns>已完成的综合大检查整改数量</returns>
        public static int GetIsOKViolationCountByCheckTime(DateTime startTime, DateTime endTime, string projectId)
        {
            return (from x in new Model.SGGLDB(Funs.ConnString).Check_CheckColligation
                    join y in new Model.SGGLDB(Funs.ConnString).Check_CheckColligationDetail on x.CheckColligationId equals y.CheckColligationId
                    where x.CheckTime >= startTime && x.CheckTime <= endTime && x.ProjectId == projectId && y.CompleteStatus != null && y.CompleteStatus == true
                    select y).Count();
        }

        /// <summary>
        /// 添加安全综合检查
        /// </summary>
        /// <param name="checkColligation"></param>
        public static void AddCheckColligation(Model.Check_CheckColligation checkColligation)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Check_CheckColligation newCheckColligation = new Model.Check_CheckColligation
            {
                CheckColligationId = checkColligation.CheckColligationId,
                CheckColligationCode = checkColligation.CheckColligationCode,
                ProjectId = checkColligation.ProjectId,
                CheckType = checkColligation.CheckType,
                CheckPerson = checkColligation.CheckPerson,
                CheckTime = checkColligation.CheckTime,
                ScanUrl = checkColligation.ScanUrl,
                DaySummary = checkColligation.DaySummary,
                PartInUnits = checkColligation.PartInUnits,
                PartInPersons = checkColligation.PartInPersons,
                PartInPersonIds = checkColligation.PartInPersonIds,
                PartInPersonNames = checkColligation.PartInPersonNames,
                CheckAreas = checkColligation.CheckAreas,
                States = checkColligation.States,
                CompileMan = checkColligation.CompileMan
            };
            db.Check_CheckColligation.InsertOnSubmit(newCheckColligation);
            db.SubmitChanges();
            ////增加一条编码记录
            BLL.CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(BLL.Const.ProjectCheckColligationMenuId, checkColligation.ProjectId, null, checkColligation.CheckColligationId, checkColligation.CheckTime);
        }

        /// <summary>
        /// 修改安全综合检查
        /// </summary>
        /// <param name="checkColligation"></param>
        public static void UpdateCheckColligation(Model.Check_CheckColligation checkColligation)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Check_CheckColligation newCheckColligation = db.Check_CheckColligation.FirstOrDefault(e => e.CheckColligationId == checkColligation.CheckColligationId);
            if (newCheckColligation != null)
            {
                newCheckColligation.CheckColligationCode = checkColligation.CheckColligationCode;
                //newCheckColligation.ProjectId = checkColligation.ProjectId;
                newCheckColligation.CheckType = checkColligation.CheckType;
                newCheckColligation.CheckPerson = checkColligation.CheckPerson;
                newCheckColligation.CheckTime = checkColligation.CheckTime;
                newCheckColligation.ScanUrl = checkColligation.ScanUrl;
                newCheckColligation.DaySummary = checkColligation.DaySummary;
                newCheckColligation.PartInUnits = checkColligation.PartInUnits;
                newCheckColligation.PartInPersons = checkColligation.PartInPersons;
                newCheckColligation.PartInPersonNames = checkColligation.PartInPersonNames;
                newCheckColligation.PartInPersonIds = checkColligation.PartInPersonIds;
                newCheckColligation.CheckAreas = checkColligation.CheckAreas;
                newCheckColligation.States = checkColligation.States;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据综合检查ID删除对应综合检查记录信息
        /// </summary>
        /// <param name="superviseCheckReportId"></param>
        public static void DeleteCheckColligation(string checkColligationId)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            var checkColligation = (from x in db.Check_CheckColligation where x.CheckColligationId == checkColligationId select x).FirstOrDefault();
            if (checkColligation != null)
            {
                ///删除编码表记录
                BLL.CodeRecordsService.DeleteCodeRecordsByDataId(checkColligation.CheckColligationId);
                ////删除附件表
                BLL.CommonService.DeleteAttachFileById(checkColligation.CheckColligationId);                            
                BLL.CommonService.DeleteFlowOperateByID(checkColligation.CheckColligationId);
                db.Check_CheckColligation.DeleteOnSubmit(checkColligation);
                db.SubmitChanges();
            }
        }
    }
}
