using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    /// <summary>
    /// 安全专题例会
    /// </summary>
    public static class SpecialMeetingService
    {
        public static Model.SGGLDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取专题例会
        /// </summary>
        /// <param name="specialMeetingId"></param>
        /// <returns></returns>
        public static Model.Meeting_SpecialMeeting GetSpecialMeetingById(string specialMeetingId)
        {
            return Funs.DB.Meeting_SpecialMeeting.FirstOrDefault(e => e.SpecialMeetingId == specialMeetingId);
        }

        /// <summary>
        /// 根据时间段获取专题例会集合
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public static int GetCountByTime(DateTime startTime, DateTime endTime, string projectId)
        {
            return (from x in Funs.DB.Meeting_SpecialMeeting where x.SpecialMeetingDate >= startTime && x.SpecialMeetingDate < endTime && x.ProjectId == projectId && x.States == BLL.Const.State_2 select x).Count();
        }

        /// <summary>
        /// 根据时间段获取专题例会参会人数
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public static int? GetSumAttentPersonNumByMeetingDate(DateTime startTime, DateTime endTime, string projectId)
        {
            int? sumAttentPersonNum = (from x in Funs.DB.Meeting_SpecialMeeting where x.SpecialMeetingDate >= startTime && x.SpecialMeetingDate < endTime && x.ProjectId == projectId && x.States == BLL.Const.State_2 select x.AttentPersonNum).Sum();
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
        public static List<Model.Meeting_SpecialMeeting> GetMeetingListsByDate(DateTime startTime, DateTime endTime, string projectId)
        {
            return (from x in Funs.DB.Meeting_SpecialMeeting where x.SpecialMeetingDate >= startTime && x.SpecialMeetingDate <= endTime && x.ProjectId == projectId orderby x.SpecialMeetingDate select x).ToList();
        }

        /// <summary>
        /// 添加专题例会
        /// </summary>
        /// <param name="specialMeeting"></param>
        public static void AddSpecialMeeting(Model.Meeting_SpecialMeeting specialMeeting)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Meeting_SpecialMeeting newSpecialMeeting = new Model.Meeting_SpecialMeeting
            {
                SpecialMeetingId = specialMeeting.SpecialMeetingId,
                ProjectId = specialMeeting.ProjectId,
                UnitId = specialMeeting.UnitId,
                SpecialMeetingCode = specialMeeting.SpecialMeetingCode,
                SpecialMeetingName = specialMeeting.SpecialMeetingName,
                SpecialMeetingDate = specialMeeting.SpecialMeetingDate,
                CompileMan = specialMeeting.CompileMan,
                SpecialMeetingContents = specialMeeting.SpecialMeetingContents,
                CompileDate = specialMeeting.CompileDate,
                States = specialMeeting.States,
                AttentPersonNum = specialMeeting.AttentPersonNum,
                MeetingHours = specialMeeting.MeetingHours,
                MeetingHostMan = specialMeeting.MeetingHostMan,
                AttentPerson = specialMeeting.AttentPerson,
                MeetingPlace = specialMeeting.MeetingPlace,
                MeetingHostManId = specialMeeting.MeetingHostManId,
                AttentPersonIds = specialMeeting.AttentPersonIds,
            };
            db.Meeting_SpecialMeeting.InsertOnSubmit(newSpecialMeeting);
            db.SubmitChanges();
            ////增加一条编码记录
            BLL.CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(BLL.Const.ProjectSpecialMeetingMenuId, specialMeeting.ProjectId, null, specialMeeting.SpecialMeetingId, specialMeeting.CompileDate);
        }

        /// <summary>
        /// 修改专题例会
        /// </summary>
        /// <param name="specialMeeting"></param>
        public static void UpdateSpecialMeeting(Model.Meeting_SpecialMeeting specialMeeting)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Meeting_SpecialMeeting newSpecialMeeting = db.Meeting_SpecialMeeting.FirstOrDefault(e => e.SpecialMeetingId == specialMeeting.SpecialMeetingId);
            if (newSpecialMeeting != null)
            {
                //newSpecialMeeting.ProjectId = specialMeeting.ProjectId;
                newSpecialMeeting.UnitId = specialMeeting.UnitId;
                newSpecialMeeting.SpecialMeetingCode = specialMeeting.SpecialMeetingCode;
                newSpecialMeeting.SpecialMeetingName = specialMeeting.SpecialMeetingName;
                newSpecialMeeting.SpecialMeetingDate = specialMeeting.SpecialMeetingDate;
                newSpecialMeeting.CompileMan = specialMeeting.CompileMan;
                newSpecialMeeting.SpecialMeetingContents = specialMeeting.SpecialMeetingContents;
                newSpecialMeeting.CompileDate = specialMeeting.CompileDate;
                newSpecialMeeting.States = specialMeeting.States;
                newSpecialMeeting.AttentPersonNum = specialMeeting.AttentPersonNum;
                newSpecialMeeting.MeetingHours = specialMeeting.MeetingHours;
                newSpecialMeeting.MeetingHostMan = specialMeeting.MeetingHostMan;
                newSpecialMeeting.AttentPerson = specialMeeting.AttentPerson;
                newSpecialMeeting.MeetingPlace = specialMeeting.MeetingPlace;
                newSpecialMeeting.MeetingHostManId = specialMeeting.MeetingHostManId;
                newSpecialMeeting.AttentPersonIds = specialMeeting.AttentPersonIds;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除专题例会
        /// </summary>
        /// <param name="specialMeetingId"></param>
        public static void DeleteSpecialMeetingById(string specialMeetingId)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Meeting_SpecialMeeting spcialMeeting = db.Meeting_SpecialMeeting.FirstOrDefault(e => e.SpecialMeetingId == specialMeetingId);
            if (spcialMeeting != null)
            { 
                ///删除编码表记录
                BLL.CodeRecordsService.DeleteCodeRecordsByDataId(specialMeetingId);
                BLL.CommonService.DeleteAttachFileById(specialMeetingId);//删除附件
                 ////删除流程表
                BLL.CommonService.DeleteFlowOperateByID(spcialMeeting.SpecialMeetingId);
                db.Meeting_SpecialMeeting.DeleteOnSubmit(spcialMeeting);
                db.SubmitChanges();
            }
        }
    }
}