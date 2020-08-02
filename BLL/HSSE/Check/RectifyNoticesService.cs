using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 隐患整改通知单
    /// </summary>
    public static class RectifyNoticesService
    {
        /// <summary>
        /// 根据主键获取隐患整改通知单
        /// </summary>
        /// <param name="rectifyNoticesId"></param>
        /// <returns></returns>
        public static Model.Check_RectifyNotices GetRectifyNoticesById(string rectifyNoticesId)
        {
            return Funs.DB.Check_RectifyNotices.FirstOrDefault(e => e.RectifyNoticesId == rectifyNoticesId);
        }

        /// <summary>
        /// 添加安全隐患通知单
        /// </summary>
        /// <param name="rectifyNotice"></param>
        public static void AddRectifyNotices(Model.Check_RectifyNotices rectifyNotice)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Check_RectifyNotices newRectifyNotices = new Model.Check_RectifyNotices
            {
                RectifyNoticesId = rectifyNotice.RectifyNoticesId,
                RectifyNoticesCode = rectifyNotice.RectifyNoticesCode,
                ProjectId = rectifyNotice.ProjectId,
                UnitId = rectifyNotice.UnitId,
                WorkAreaId = rectifyNotice.WorkAreaId,
                CheckedDate = rectifyNotice.CheckedDate,
                WrongContent = rectifyNotice.WrongContent,
                SignPerson = rectifyNotice.SignPerson,
                SignDate = rectifyNotice.SignDate,
                CompleteStatus = rectifyNotice.CompleteStatus,
                DutyPerson = rectifyNotice.DutyPerson,
                CompleteDate = rectifyNotice.CompleteDate,
                IsRectify = rectifyNotice.IsRectify,
                CheckPerson = rectifyNotice.CheckPerson,
                DutyPersonId = rectifyNotice.DutyPersonId,
                ReCheckDate = rectifyNotice.ReCheckDate,
                CompleteManId = rectifyNotice.CompleteManId,
                CheckManNames = rectifyNotice.CheckManNames,
                HiddenHazardType = rectifyNotice.HiddenHazardType,
                ProfessionalEngineerId = rectifyNotice.ProfessionalEngineerId,
                ProfessionalEngineerTime2 = rectifyNotice.ProfessionalEngineerTime2,
                ConstructionManagerId = rectifyNotice.ConstructionManagerId,
                ConstructionManagerTime2 = rectifyNotice.ConstructionManagerTime2,
                ProjectManagerId = rectifyNotice.ProjectManagerId,
                ProjectManagerTime2 = rectifyNotice.ProjectManagerTime2,
                UnitHeadManId = rectifyNotice.UnitHeadManId,
                UnitHeadManDate = rectifyNotice.UnitHeadManDate,
                ReCheckOpinion = rectifyNotice.ReCheckOpinion,
                CheckManIds = rectifyNotice.CheckManIds,
                Isprint = rectifyNotice.Isprint,
                Isprintf = rectifyNotice.Isprintf,
                States = rectifyNotice.States,
            };
            db.Check_RectifyNotices.InsertOnSubmit(newRectifyNotices);
            db.SubmitChanges();
            ////增加一条编码记录
            CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(Const.ProjectRectifyNoticesMenuId, rectifyNotice.ProjectId, rectifyNotice.UnitId, rectifyNotice.RectifyNoticesId, rectifyNotice.CheckedDate);
        }

        /// <summary>
        /// 修改隐患整改通知单
        /// </summary>
        /// <param name="rectifyNotices"></param>
        public static void UpdateRectifyNotices(Model.Check_RectifyNotices rectifyNotices)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Check_RectifyNotices newRectifyNotices = db.Check_RectifyNotices.FirstOrDefault(e => e.RectifyNoticesId == rectifyNotices.RectifyNoticesId);
            if (newRectifyNotices != null)
            {
                newRectifyNotices.RectifyNoticesId = rectifyNotices.RectifyNoticesId;
                newRectifyNotices.RectifyNoticesCode = rectifyNotices.RectifyNoticesCode;
                newRectifyNotices.ProjectId = rectifyNotices.ProjectId;
                newRectifyNotices.UnitId = rectifyNotices.UnitId;
                newRectifyNotices.WorkAreaId = rectifyNotices.WorkAreaId;
                newRectifyNotices.CheckedDate = rectifyNotices.CheckedDate;
                newRectifyNotices.WrongContent = rectifyNotices.WrongContent;
                newRectifyNotices.SignPerson = rectifyNotices.SignPerson;
                newRectifyNotices.SignDate = rectifyNotices.SignDate;
                newRectifyNotices.CompleteStatus = rectifyNotices.CompleteStatus;
                newRectifyNotices.DutyPerson = rectifyNotices.DutyPerson;
                newRectifyNotices.CompleteDate = rectifyNotices.CompleteDate;
                newRectifyNotices.IsRectify = rectifyNotices.IsRectify;
                newRectifyNotices.CheckPerson = rectifyNotices.CheckPerson;
                newRectifyNotices.DutyPersonId = rectifyNotices.DutyPersonId;
                newRectifyNotices.ReCheckDate = rectifyNotices.ReCheckDate;
                newRectifyNotices.CompleteManId = rectifyNotices.CompleteManId;
                newRectifyNotices.CheckManNames = rectifyNotices.CheckManNames;
                newRectifyNotices.HiddenHazardType = rectifyNotices.HiddenHazardType;
                newRectifyNotices.ProfessionalEngineerId = rectifyNotices.ProfessionalEngineerId;
                newRectifyNotices.ProfessionalEngineerTime2 = rectifyNotices.ProfessionalEngineerTime2;
                newRectifyNotices.ConstructionManagerId = rectifyNotices.ConstructionManagerId;
                newRectifyNotices.ConstructionManagerTime2 = rectifyNotices.ConstructionManagerTime2;
                newRectifyNotices.ProjectManagerId = rectifyNotices.ProjectManagerId;
                newRectifyNotices.ProjectManagerTime2 = rectifyNotices.ProjectManagerTime2;
                newRectifyNotices.UnitHeadManId = rectifyNotices.UnitHeadManId;
                newRectifyNotices.UnitHeadManDate = rectifyNotices.UnitHeadManDate;
                newRectifyNotices.ReCheckOpinion = rectifyNotices.ReCheckOpinion;
                newRectifyNotices.CheckManIds = rectifyNotices.CheckManIds;
                newRectifyNotices.Isprint = rectifyNotices.Isprint;
                newRectifyNotices.Isprintf = rectifyNotices.Isprintf;
                newRectifyNotices.States = rectifyNotices.States;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除隐患整改通知单
        /// </summary>
        /// <param name="rectifyNoticesId"></param>
        public static void DeleteRectifyNoticesById(string rectifyNoticesId)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Check_RectifyNotices rectifyNotices = db.Check_RectifyNotices.FirstOrDefault(e => e.RectifyNoticesId == rectifyNoticesId);
            if (rectifyNotices != null)
            {
                CodeRecordsService.DeleteCodeRecordsByDataId(rectifyNoticesId);
                UploadFileService.DeleteFile(Funs.RootPath, rectifyNotices.AttachUrl);
                CommonService.DeleteAttachFileById(rectifyNoticesId);

                var getCheck_RectifyNoticesItem = from x in db.Check_RectifyNoticesItem
                                                  where x.RectifyNoticesId == rectifyNoticesId
                                                  select x;
                if (getCheck_RectifyNoticesItem.Count() > 0)
                {
                    db.Check_RectifyNoticesItem.DeleteAllOnSubmit(getCheck_RectifyNoticesItem);
                    db.SubmitChanges();
                }

                var getRectifyNoticesFlowOperate = from x in db.Check_RectifyNoticesFlowOperate
                                                   where x.RectifyNoticesId == rectifyNoticesId
                                                   select x;
                if (getRectifyNoticesFlowOperate.Count() > 0)
                {
                    db.Check_RectifyNoticesFlowOperate.DeleteAllOnSubmit(getRectifyNoticesFlowOperate);
                    db.SubmitChanges();
                }

                db.Check_RectifyNotices.DeleteOnSubmit(rectifyNotices);
                db.SubmitChanges();
            }
        }

        public static void DeleteRectifyNoticesRectifyNoticesItemById(string rectifyNoticesId)
        {
            var deleteItem = from x in Funs.DB.Check_RectifyNoticesItem where x.RectifyNoticesId == rectifyNoticesId select x;
            if (deleteItem.Count() > 0)
            {
                Funs.DB.Check_RectifyNoticesItem.DeleteAllOnSubmit(deleteItem);
                Funs.DB.SubmitChanges();
            }
        }
    }
}
