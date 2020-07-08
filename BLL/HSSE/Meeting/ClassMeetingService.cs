using System.Linq;

namespace BLL
{
    /// <summary>
    /// 班前会
    /// </summary>
    public static class ClassMeetingService
    {
        public static Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);

        /// <summary>
        /// 根据主键获取班前会
        /// </summary>
        /// <param name="classMeetingId"></param>
        /// <returns></returns>
        public static Model.Meeting_ClassMeeting GetClassMeetingById(string classMeetingId)
        {
            return new Model.SGGLDB(Funs.ConnString).Meeting_ClassMeeting.FirstOrDefault(e => e.ClassMeetingId == classMeetingId);
        }

        /// <summary>
        /// 添加班前会
        /// </summary>
        /// <param name="classMeeting"></param>
        public static void AddClassMeeting(Model.Meeting_ClassMeeting classMeeting)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Meeting_ClassMeeting newClassMeeting = new Model.Meeting_ClassMeeting
            {
                ClassMeetingId = classMeeting.ClassMeetingId,
                ProjectId = classMeeting.ProjectId,
                UnitId = classMeeting.UnitId,
                TeamGroupId = classMeeting.TeamGroupId,
                ClassMeetingCode = classMeeting.ClassMeetingCode,
                ClassMeetingName = classMeeting.ClassMeetingName,
                ClassMeetingDate = classMeeting.ClassMeetingDate,
                ClassMeetingContents = classMeeting.ClassMeetingContents,
                CompileMan = classMeeting.CompileMan,
                CompileDate = classMeeting.CompileDate,
                States = classMeeting.States,
                MeetingPlace = classMeeting.MeetingPlace,
                MeetingHours = classMeeting.MeetingHours,
                MeetingHostMan = classMeeting.MeetingHostMan,
                AttentPerson = classMeeting.AttentPerson,
                AttentPersonNum=classMeeting.AttentPersonNum,
            };
            db.Meeting_ClassMeeting.InsertOnSubmit(newClassMeeting);
            db.SubmitChanges();
            ////增加一条编码记录
            BLL.CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(BLL.Const.ProjectClassMeetingMenuId, classMeeting.ProjectId, null, classMeeting.ClassMeetingId, classMeeting.CompileDate);
        }

        /// <summary>
        /// 修改班前会
        /// </summary>
        /// <param name="classMeeting"></param>
        public static void UpdateClassMeeting(Model.Meeting_ClassMeeting classMeeting)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Meeting_ClassMeeting newClassMeeting = db.Meeting_ClassMeeting.FirstOrDefault(e => e.ClassMeetingId == classMeeting.ClassMeetingId);
            if (newClassMeeting != null)
            {
                // newClassMeeting.ProjectId = classMeeting.ProjectId;
                newClassMeeting.UnitId = classMeeting.UnitId;
                newClassMeeting.TeamGroupId = classMeeting.TeamGroupId;
                newClassMeeting.ClassMeetingCode = classMeeting.ClassMeetingCode;
                newClassMeeting.ClassMeetingName = classMeeting.ClassMeetingName;
                newClassMeeting.ClassMeetingDate = classMeeting.ClassMeetingDate;
                newClassMeeting.ClassMeetingContents = classMeeting.ClassMeetingContents;
                newClassMeeting.CompileMan = classMeeting.CompileMan;
                newClassMeeting.CompileDate = classMeeting.CompileDate;
                newClassMeeting.States = classMeeting.States;
                newClassMeeting.MeetingPlace = classMeeting.MeetingPlace;
                newClassMeeting.MeetingHours = classMeeting.MeetingHours;
                newClassMeeting.MeetingHostMan = classMeeting.MeetingHostMan;
                newClassMeeting.AttentPerson = classMeeting.AttentPerson;
                newClassMeeting.AttentPersonNum = classMeeting.AttentPersonNum;

                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除班前会
        /// </summary>
        /// <param name="classMeetingId"></param>
        public static void DeleteClassMeetingById(string classMeetingId)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Meeting_ClassMeeting classMeeting = db.Meeting_ClassMeeting.FirstOrDefault(e => e.ClassMeetingId == classMeetingId);
            if (classMeeting != null)
            {
                ///删除编码表记录
                BLL.CodeRecordsService.DeleteCodeRecordsByDataId(classMeetingId);
                BLL.CommonService.DeleteAttachFileById(classMeetingId);
                ////删除审核流程表
                BLL.CommonService.DeleteFlowOperateByID(classMeetingId);
                db.Meeting_ClassMeeting.DeleteOnSubmit(classMeeting);
                db.SubmitChanges();
            }
        }
    }
}
