using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    /// <summary>
    /// 周例会
    /// </summary>
    public static class WeekMeetingService
    {
        public static Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);

        /// <summary>
        /// 根据主键获取周例会
        /// </summary>
        /// <param name="weekMeetingId"></param>
        /// <returns></returns>
        public static Model.Meeting_WeekMeeting GetWeekMeetingById(string weekMeetingId)
        {
            return new Model.SGGLDB(Funs.ConnString).Meeting_WeekMeeting.FirstOrDefault(e => e.WeekMeetingId == weekMeetingId);
        }

        /// <summary>
        /// 根据时间段获取周例会集合
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public static int GetCountByTime(DateTime startTime, DateTime endTime, string projectId)
        {
            return (from x in new Model.SGGLDB(Funs.ConnString).Meeting_WeekMeeting where x.WeekMeetingDate >= startTime && x.WeekMeetingDate < endTime && x.ProjectId == projectId && x.States == BLL.Const.State_2 select x).Count();
        }

        /// <summary>
        /// 根据时间段获取周例会参会人数
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public static int? GetSumAttentPersonNumByMeetingDate(DateTime startTime, DateTime endTime, string projectId)
        {
            int? sumAttentPersonNum = (from x in new Model.SGGLDB(Funs.ConnString).Meeting_WeekMeeting where x.WeekMeetingDate >= startTime && x.WeekMeetingDate < endTime && x.ProjectId == projectId && x.States == BLL.Const.State_2 select x.AttentPersonNum).Sum();
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
        public static List<Model.Meeting_WeekMeeting> GetMeetingListsByDate(DateTime startTime, DateTime endTime, string projectId)
        {
            return (from x in new Model.SGGLDB(Funs.ConnString).Meeting_WeekMeeting where x.WeekMeetingDate >= startTime && x.WeekMeetingDate <= endTime && x.ProjectId == projectId orderby x.WeekMeetingDate select x).ToList();
        }

        /// <summary>
        /// 添加周例会
        /// </summary>
        /// <param name="weekMeeting"></param>
        public static void AddWeekMeeting(Model.Meeting_WeekMeeting weekMeeting)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Meeting_WeekMeeting newWeekMeeting = new Model.Meeting_WeekMeeting
            {
                WeekMeetingId = weekMeeting.WeekMeetingId,
                ProjectId = weekMeeting.ProjectId,
                UnitId = weekMeeting.UnitId,
                WeekMeetingCode = weekMeeting.WeekMeetingCode,
                WeekMeetingName = weekMeeting.WeekMeetingName,
                WeekMeetingDate = weekMeeting.WeekMeetingDate,
                CompileMan = weekMeeting.CompileMan,
                WeekMeetingContents = weekMeeting.WeekMeetingContents,
                CompileDate = weekMeeting.CompileDate,
                States = weekMeeting.States,
                AttentPersonNum = weekMeeting.AttentPersonNum,
                MeetingHours = weekMeeting.MeetingHours,
                MeetingHostMan = weekMeeting.MeetingHostMan,
                AttentPerson = weekMeeting.AttentPerson,
                MeetingPlace=weekMeeting.MeetingPlace,
                MeetingHostManId = weekMeeting.MeetingHostManId,
                AttentPersonIds = weekMeeting.AttentPersonIds,
            };
            db.Meeting_WeekMeeting.InsertOnSubmit(newWeekMeeting);
            db.SubmitChanges();
            ////增加一条编码记录
            BLL.CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(BLL.Const.ProjectWeekMeetingMenuId, weekMeeting.ProjectId, null, weekMeeting.WeekMeetingId, weekMeeting.CompileDate);
        }

        /// <summary>
        /// 修改周例会
        /// </summary>
        /// <param name="weekMeeting"></param>
        public static void UpdateWeekMeeting(Model.Meeting_WeekMeeting weekMeeting)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Meeting_WeekMeeting newWeekMeeting = db.Meeting_WeekMeeting.FirstOrDefault(e => e.WeekMeetingId == weekMeeting.WeekMeetingId);
            if (newWeekMeeting != null)
            {
                //newWeekMeeting.ProjectId = weekMeeting.ProjectId;
                newWeekMeeting.UnitId = weekMeeting.UnitId;
                newWeekMeeting.WeekMeetingCode = weekMeeting.WeekMeetingCode;
                newWeekMeeting.WeekMeetingName = weekMeeting.WeekMeetingName;
                newWeekMeeting.WeekMeetingDate = weekMeeting.WeekMeetingDate;
                newWeekMeeting.CompileMan = weekMeeting.CompileMan;
                newWeekMeeting.WeekMeetingContents = weekMeeting.WeekMeetingContents;
                newWeekMeeting.CompileDate = weekMeeting.CompileDate;
                newWeekMeeting.States = weekMeeting.States;
                newWeekMeeting.AttentPersonNum = weekMeeting.AttentPersonNum;
                newWeekMeeting.MeetingHours = weekMeeting.MeetingHours;
                newWeekMeeting.MeetingHostMan = weekMeeting.MeetingHostMan;
                newWeekMeeting.AttentPerson = weekMeeting.AttentPerson;
                newWeekMeeting.MeetingPlace = weekMeeting.MeetingPlace;
                newWeekMeeting.MeetingHostManId = weekMeeting.MeetingHostManId;
                newWeekMeeting.AttentPersonIds = weekMeeting.AttentPersonIds;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除周例会
        /// </summary>
        /// <param name="weekMeetingId"></param>
        public static void DeleteWeekMeetingById(string weekMeetingId)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Meeting_WeekMeeting weekMeeting = db.Meeting_WeekMeeting.FirstOrDefault(e => e.WeekMeetingId == weekMeetingId);
            if (weekMeeting != null)
            {
                ///删除编码表记录
                BLL.CodeRecordsService.DeleteCodeRecordsByDataId(weekMeeting.WeekMeetingId);
                ////删除附件表
                BLL.CommonService.DeleteAttachFileById(weekMeeting.WeekMeetingId);
                ////删除流程表
                BLL.CommonService.DeleteFlowOperateByID(weekMeeting.WeekMeetingId);
                db.Meeting_WeekMeeting.DeleteOnSubmit(weekMeeting);
                db.SubmitChanges();
            }
        }
    }
}
