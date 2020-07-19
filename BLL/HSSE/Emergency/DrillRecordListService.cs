using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 应急演练
    /// </summary>
    public static class DrillRecordListService
    {
        public static Model.SGGLDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取应急演练
        /// </summary>
        /// <param name="DrillRecordListId"></param>
        /// <returns></returns>
        public static Model.Emergency_DrillRecordList GetDrillRecordListById(string DrillRecordListId)
        {
            return Funs.DB.Emergency_DrillRecordList.FirstOrDefault(e => e.DrillRecordListId == DrillRecordListId);
        }

        /// <summary>
        /// 根据时间获取应急演练信息
        /// </summary>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns>应急演练信息</returns>
        public static List<Model.Emergency_DrillRecordList> GetDrillRecordListsByDrillRecordDate(DateTime startTime, DateTime endTime, string projectId)
        {
            return (from x in Funs.DB.Emergency_DrillRecordList where x.DrillRecordDate >= startTime && x.DrillRecordDate < endTime && x.ProjectId == projectId && x.States == BLL.Const.State_2 select x).ToList();
        }

        /// <summary>
        /// 根据时间段获取HSE应急演练
        /// </summary>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="projectId">项目Id</param>
        /// <returns>时间段内的HSE应急演练数量</returns>
        public static int GetCountByDate(DateTime startTime, DateTime endTime, string projectId)
        {
            return (from x in Funs.DB.Emergency_DrillRecordList where x.DrillRecordDate >= startTime && x.DrillRecordDate <= endTime && x.ProjectId == projectId select x).Count();
        }

        /// <summary>
        /// 根据时间段获取HSE应急演练
        /// </summary>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="projectId">项目Id</param>
        /// <returns>时间段内的HSE应急演练数量</returns>
        public static int GetCountByDate2(DateTime startTime, DateTime endTime, string projectId)
        {
            return (from x in Funs.DB.Emergency_DrillRecordList where x.DrillRecordDate >= startTime && x.DrillRecordDate < endTime && x.ProjectId == projectId select x).Count();
        }

        /// <summary>
        /// 获取HSE应急演练
        /// </summary>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="projectId">项目Id</param>
        /// <returns>时间段内的HSE应急演练数量</returns>
        public static int GetCount(string projectId)
        {
            return (from x in Funs.DB.Emergency_DrillRecordList where x.ProjectId == projectId select x).Count();
        }

        /// <summary>
        /// 添加应急演练
        /// </summary>
        /// <param name="DrillRecordList"></param>
        public static void AddDrillRecordList(Model.Emergency_DrillRecordList DrillRecordList)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Emergency_DrillRecordList newDrillRecordList = new Model.Emergency_DrillRecordList
            {
                DrillRecordListId = DrillRecordList.DrillRecordListId,
                ProjectId = DrillRecordList.ProjectId,
                DrillRecordCode = DrillRecordList.DrillRecordCode,
                DrillRecordName = DrillRecordList.DrillRecordName,
                UnitIds = DrillRecordList.UnitIds,
                UnitNames = DrillRecordList.UnitNames,
                UserIds = DrillRecordList.UserIds,
                UserNames = DrillRecordList.UserNames,
                DrillRecordDate = DrillRecordList.DrillRecordDate,
                DrillRecordContents = DrillRecordList.DrillRecordContents,
                CompileMan = DrillRecordList.CompileMan,
                CompileDate = System.DateTime.Now,
                States = DrillRecordList.States,
                AttachUrl = DrillRecordList.AttachUrl,
                DrillRecordType = DrillRecordList.DrillRecordType,
                JointPersonNum = DrillRecordList.JointPersonNum,
                DrillCost = DrillRecordList.DrillCost
            };
            db.Emergency_DrillRecordList.InsertOnSubmit(newDrillRecordList);
            db.SubmitChanges();
            ////增加一条编码记录
            BLL.CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(BLL.Const.ProjectDrillRecordListMenuId, DrillRecordList.ProjectId, null, DrillRecordList.DrillRecordListId, DrillRecordList.DrillRecordDate);
        }

        /// <summary>
        /// 修改应急演练
        /// </summary>
        /// <param name="DrillRecordList"></param>
        public static void UpdateDrillRecordList(Model.Emergency_DrillRecordList DrillRecordList)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Emergency_DrillRecordList newDrillRecordList = db.Emergency_DrillRecordList.FirstOrDefault(e => e.DrillRecordListId == DrillRecordList.DrillRecordListId);
            if (newDrillRecordList != null)
            {
                newDrillRecordList.DrillRecordCode = DrillRecordList.DrillRecordCode;
                newDrillRecordList.DrillRecordName = DrillRecordList.DrillRecordName;
                newDrillRecordList.UnitIds = DrillRecordList.UnitIds;
                newDrillRecordList.UnitNames = DrillRecordList.UnitNames;
                newDrillRecordList.UserIds = DrillRecordList.UserIds;
                newDrillRecordList.UserNames = DrillRecordList.UserNames;
                newDrillRecordList.DrillRecordDate = DrillRecordList.DrillRecordDate;
                newDrillRecordList.DrillRecordContents = DrillRecordList.DrillRecordContents;
                newDrillRecordList.CompileMan = DrillRecordList.CompileMan;
                //newDrillRecordList.CompileDate = DrillRecordList.CompileDate;
                newDrillRecordList.States = DrillRecordList.States;
                newDrillRecordList.AttachUrl = DrillRecordList.AttachUrl;
                newDrillRecordList.DrillRecordType = DrillRecordList.DrillRecordType;
                newDrillRecordList.JointPersonNum = DrillRecordList.JointPersonNum;
                newDrillRecordList.DrillCost = DrillRecordList.DrillCost;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除应急演练
        /// </summary>
        /// <param name="DrillRecordListId"></param>
        public static void DeleteDrillRecordListById(string DrillRecordListId)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Emergency_DrillRecordList DrillRecordList = db.Emergency_DrillRecordList.FirstOrDefault(e => e.DrillRecordListId == DrillRecordListId);
            if (DrillRecordList != null)
            {
                ///删除编码表记录
                BLL.CodeRecordsService.DeleteCodeRecordsByDataId(DrillRecordList.DrillRecordListId);
                ////删除附件表
                BLL.CommonService.DeleteAttachFileById(DrillRecordList.DrillRecordListId);
                ////删除流程表
                BLL.CommonService.DeleteFlowOperateByID(DrillRecordList.DrillRecordListId);
                db.Emergency_DrillRecordList.DeleteOnSubmit(DrillRecordList);
                db.SubmitChanges();
            }
        }
    }
}
