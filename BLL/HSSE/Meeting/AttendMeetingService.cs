using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 其他会议记录
    /// </summary>
    public static class AttendMeetingService
    {
        public static Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);

        /// <summary>
        /// 根据主键获取其他会议记录
        /// </summary>
        /// <param name="attendMeetingId"></param>
        /// <returns></returns>
        public static Model.Meeting_AttendMeeting GetAttendMeetingById(string attendMeetingId)
        {
            return new Model.SGGLDB(Funs.ConnString).Meeting_AttendMeeting.FirstOrDefault(e => e.AttendMeetingId == attendMeetingId);
        }

        /// <summary>
        /// 根据时间段获取其他会议集合
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public static int GetCountByTime(DateTime startTime, DateTime endTime, string projectId)
        {
            return (from x in new Model.SGGLDB(Funs.ConnString).Meeting_AttendMeeting where x.AttendMeetingDate >= startTime && x.AttendMeetingDate < endTime && x.ProjectId == projectId select x).Count();
        }

        /// <summary>
        /// 根据日期和类型获取会议记录集合
        /// </summary>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="projectId">项目号</param>
        /// <returns>会议记录集合</returns>
        public static List<Model.Meeting_AttendMeeting> GetMeetingListsByDate(DateTime startTime, DateTime endTime, string projectId)
        {
            return (from x in new Model.SGGLDB(Funs.ConnString).Meeting_AttendMeeting where x.AttendMeetingDate >= startTime && x.AttendMeetingDate <= endTime && x.ProjectId == projectId orderby x.AttendMeetingDate select x).ToList();
        }

        /// <summary>
        /// 添加其他会议记录
        /// </summary>
        /// <param name="attendMeeting"></param>
        public static void AddAttendMeeting(Model.Meeting_AttendMeeting attendMeeting)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Meeting_AttendMeeting newAttendMeeting = new Model.Meeting_AttendMeeting
            {
                AttendMeetingId = attendMeeting.AttendMeetingId,
                ProjectId = attendMeeting.ProjectId,
                UnitId = attendMeeting.UnitId,
                AttendMeetingCode = attendMeeting.AttendMeetingCode,
                AttendMeetingName = attendMeeting.AttendMeetingName,
                AttendMeetingDate = attendMeeting.AttendMeetingDate,
                CompileMan = attendMeeting.CompileMan,
                AttendMeetingContents = attendMeeting.AttendMeetingContents,
                CompileDate = attendMeeting.CompileDate,
                States = attendMeeting.States,
                MeetingHours = attendMeeting.MeetingHours,
                MeetingHostMan = attendMeeting.MeetingHostMan,
                AttentPerson = attendMeeting.AttentPerson,
                MeetingPlace = attendMeeting.MeetingPlace,
                MeetingHostManId = attendMeeting.MeetingHostManId,
                AttentPersonIds=attendMeeting.AttentPersonIds,
                AttentPersonNum = attendMeeting.AttentPersonNum,
            };
            db.Meeting_AttendMeeting.InsertOnSubmit(attendMeeting);
            db.SubmitChanges();
            ////增加一条编码记录
            CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(Const.ProjectAttendMeetingMenuId, attendMeeting.ProjectId, null, attendMeeting.AttendMeetingId, attendMeeting.CompileDate);
        }

        /// <summary>
        /// 修改其他会议记录
        /// </summary>
        /// <param name="attendMeeting"></param>
        public static void UpdateAttendMeeting(Model.Meeting_AttendMeeting attendMeeting)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Meeting_AttendMeeting newAttendMeeting = db.Meeting_AttendMeeting.FirstOrDefault(e => e.AttendMeetingId == attendMeeting.AttendMeetingId);
            if (newAttendMeeting != null)
            {
                //newAttendMeeting.ProjectId = attendMeeting.ProjectId;
                newAttendMeeting.UnitId = attendMeeting.UnitId;
                newAttendMeeting.AttendMeetingCode = attendMeeting.AttendMeetingCode;
                newAttendMeeting.AttendMeetingName = attendMeeting.AttendMeetingName;
                newAttendMeeting.AttendMeetingDate = attendMeeting.AttendMeetingDate;
                newAttendMeeting.CompileMan = attendMeeting.CompileMan;
                newAttendMeeting.AttendMeetingContents = attendMeeting.AttendMeetingContents;
                newAttendMeeting.CompileDate = attendMeeting.CompileDate;
                newAttendMeeting.States = attendMeeting.States;
                newAttendMeeting.MeetingHours = attendMeeting.MeetingHours;
                newAttendMeeting.MeetingHostMan = attendMeeting.MeetingHostMan;
                newAttendMeeting.AttentPerson = attendMeeting.AttentPerson;
                newAttendMeeting.MeetingPlace = attendMeeting.MeetingPlace;
                newAttendMeeting.MeetingHostManId = attendMeeting.MeetingHostManId;
                newAttendMeeting.AttentPersonIds = attendMeeting.AttentPersonIds;
                newAttendMeeting.AttentPersonNum = attendMeeting.AttentPersonNum;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除其他会议记录
        /// </summary>
        /// <param name="attendMeetingId"></param>
        public static void DeleteAttendMeetingById(string attendMeetingId)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Meeting_AttendMeeting attendMeeting = db.Meeting_AttendMeeting.FirstOrDefault(e => e.AttendMeetingId == attendMeetingId);
            if (attendMeeting != null)
            {
                ///删除编码表记录
                BLL.CodeRecordsService.DeleteCodeRecordsByDataId(attendMeetingId);
                BLL.CommonService.DeleteAttachFileById(attendMeetingId);//删除附件
               ////删除流程表
                BLL.CommonService.DeleteFlowOperateByID(attendMeeting.AttendMeetingId);
                db.Meeting_AttendMeeting.DeleteOnSubmit(attendMeeting);
                db.SubmitChanges();
            }
        }
    }
}
