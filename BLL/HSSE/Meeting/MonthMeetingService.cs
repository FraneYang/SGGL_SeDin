using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    /// <summary>
    /// 安全月例会
    /// </summary>
    public static class MonthMeetingService
    {
        public static Model.SGGLDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取安全月例会
        /// </summary>
        /// <param name="monthMeetingId"></param>
        /// <returns></returns>
        public static Model.Meeting_MonthMeeting GetMonthMeetingById(string monthMeetingId)
        {
            return Funs.DB.Meeting_MonthMeeting.FirstOrDefault(e => e.MonthMeetingId == monthMeetingId);
        }

        /// <summary>
        /// 根据时间段获取月例会集合
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public static int GetCountByTime(DateTime startTime, DateTime endTime, string projectId)
        {
            return (from x in Funs.DB.Meeting_MonthMeeting where x.MonthMeetingDate >= startTime && x.MonthMeetingDate < endTime && x.ProjectId == projectId && x.States == BLL.Const.State_2 select x).Count();
        }

        /// <summary>
        /// 根据时间段获取月例会参会人数
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public static int? GetSumAttentPersonNumByMeetingDate(DateTime startTime, DateTime endTime, string projectId)
        {
            int? sumAttentPersonNum = (from x in Funs.DB.Meeting_MonthMeeting where x.MonthMeetingDate >= startTime && x.MonthMeetingDate < endTime && x.ProjectId == projectId && x.States == BLL.Const.State_2 select x.AttentPersonNum).Sum();
            if (sumAttentPersonNum == null)
            {
                return 0;
            }
            return sumAttentPersonNum;
        }

        /// <summary>
        /// 根据日期和类型获取会议记录集合
        /// </summary>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="projectId">项目号</param>
        /// <returns>会议记录集合</returns>
        public static List<Model.Meeting_MonthMeeting> GetMeetingListsByDate(DateTime startTime, DateTime endTime, string projectId)
        {
            return (from x in Funs.DB.Meeting_MonthMeeting where x.MonthMeetingDate >= startTime && x.MonthMeetingDate <= endTime && x.ProjectId == projectId orderby x.MonthMeetingDate select x).ToList();
        }

        /// <summary>
        /// 添加安全月例会
        /// </summary>
        /// <param name="monthMeeting"></param>
        public static void AddMonthMeeting(Model.Meeting_MonthMeeting monthMeeting)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Meeting_MonthMeeting newMonthMeeting = new Model.Meeting_MonthMeeting
            {
                MonthMeetingId = monthMeeting.MonthMeetingId,
                ProjectId = monthMeeting.ProjectId,
                UnitId = monthMeeting.UnitId,
                MonthMeetingCode = monthMeeting.MonthMeetingCode,
                MonthMeetingName = monthMeeting.MonthMeetingName,
                MonthMeetingDate = monthMeeting.MonthMeetingDate,
                CompileMan = monthMeeting.CompileMan,
                MonthMeetingContents = monthMeeting.MonthMeetingContents,
                CompileDate = monthMeeting.CompileDate,
                States = monthMeeting.States,
                AttentPersonNum = monthMeeting.AttentPersonNum,
                MeetingHours = monthMeeting.MeetingHours,
                MeetingHostMan = monthMeeting.MeetingHostMan,
                AttentPerson = monthMeeting.AttentPerson,
                MeetingPlace=monthMeeting.MeetingPlace,
                MeetingHostManId = monthMeeting.MeetingHostManId,
                AttentPersonIds = monthMeeting.AttentPersonIds,
            };
            db.Meeting_MonthMeeting.InsertOnSubmit(newMonthMeeting);
            db.SubmitChanges();
            ////增加一条编码记录
            BLL.CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(BLL.Const.ProjectMonthMeetingMenuId, monthMeeting.ProjectId, null, monthMeeting.MonthMeetingId, monthMeeting.CompileDate);
        }

        /// <summary>
        /// 修改安全月例会
        /// </summary>
        /// <param name="monthMeeting"></param>
        public static void UpdateMonthMeeting(Model.Meeting_MonthMeeting monthMeeting)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Meeting_MonthMeeting newMonthMeeting = db.Meeting_MonthMeeting.FirstOrDefault(e => e.MonthMeetingId == monthMeeting.MonthMeetingId);
            if (newMonthMeeting != null)
            {
                //newMonthMeeting.ProjectId = monthMeeting.ProjectId;
                newMonthMeeting.UnitId = monthMeeting.UnitId;
                newMonthMeeting.MonthMeetingCode = monthMeeting.MonthMeetingCode;
                newMonthMeeting.MonthMeetingName = monthMeeting.MonthMeetingName;
                newMonthMeeting.MonthMeetingDate = monthMeeting.MonthMeetingDate;
                newMonthMeeting.CompileMan = monthMeeting.CompileMan;
                newMonthMeeting.MonthMeetingContents = monthMeeting.MonthMeetingContents;
                newMonthMeeting.CompileDate = monthMeeting.CompileDate;
                newMonthMeeting.States = monthMeeting.States;
                newMonthMeeting.AttentPersonNum = monthMeeting.AttentPersonNum;
                newMonthMeeting.MeetingHours = monthMeeting.MeetingHours;
                newMonthMeeting.MeetingHostMan = monthMeeting.MeetingHostMan;
                newMonthMeeting.AttentPerson = monthMeeting.AttentPerson;
                newMonthMeeting.MeetingPlace = monthMeeting.MeetingPlace;
                newMonthMeeting.MeetingHostManId = monthMeeting.MeetingHostManId;
                newMonthMeeting.AttentPersonIds = monthMeeting.AttentPersonIds;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除安全月例会
        /// </summary>
        /// <param name="monthMeetingId"></param>
        public static void DeleteMonthMeetingById(string monthMeetingId)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Meeting_MonthMeeting monthMeeting = db.Meeting_MonthMeeting.FirstOrDefault(e => e.MonthMeetingId == monthMeetingId);
            if (monthMeeting != null)
            {
                ///删除编码表记录
                BLL.CodeRecordsService.DeleteCodeRecordsByDataId(monthMeetingId);
                BLL.CommonService.DeleteAttachFileById(monthMeetingId);
                ////删除流程表
                BLL.CommonService.DeleteFlowOperateByID(monthMeeting.MonthMeetingId);
                db.Meeting_MonthMeeting.DeleteOnSubmit(monthMeeting);
                db.SubmitChanges();
            }
        }
    }
}
