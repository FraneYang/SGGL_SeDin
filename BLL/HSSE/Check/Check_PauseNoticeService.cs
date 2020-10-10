using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace BLL
{
    /// <summary>
    /// 工程暂停令
    /// </summary>
    public static class Check_PauseNoticeService
    {
        /// <summary>
        /// 根据工程暂停令ID获取工程暂停令信息
        /// </summary>
        /// <param name="PauseNoticeName"></param>
        /// <returns></returns>
        public static Model.Check_PauseNotice GetPauseNoticeByPauseNoticeId(string pauseNoticeId)
        {
            return Funs.DB.Check_PauseNotice.FirstOrDefault(e => e.PauseNoticeId == pauseNoticeId);
        }

        /// <summary>
        /// 添加安全工程暂停令
        /// </summary>
        /// <param name="pauseNotice"></param>
        public static void AddPauseNotice(Model.Check_PauseNotice pauseNotice)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Check_PauseNotice newPauseNotice = new Model.Check_PauseNotice
            {
                PauseNoticeId = pauseNotice.PauseNoticeId,
                PauseNoticeCode = pauseNotice.PauseNoticeCode,
                ProjectId = pauseNotice.ProjectId,
                UnitId = pauseNotice.UnitId,
                ProjectPlace = pauseNotice.ProjectPlace,
                UnitWorkId=pauseNotice.UnitWorkId,
                WrongContent = pauseNotice.WrongContent,
                PauseTime = pauseNotice.PauseTime,
                PauseContent = pauseNotice.PauseContent,
                OneContent = pauseNotice.OneContent,
                SecondContent = pauseNotice.SecondContent,
                ThirdContent = pauseNotice.ThirdContent,
                IsConfirm = pauseNotice.IsConfirm,
                AttachUrl = pauseNotice.AttachUrl,
                States = pauseNotice.States,
                CompileManId = pauseNotice.CompileManId,
                CompileDate = pauseNotice.CompileDate,
                SignManId = pauseNotice.SignManId,
                SignDate = pauseNotice.SignDate,
                ApproveManId = pauseNotice.ApproveManId,
                ApproveDate = pauseNotice.ApproveDate,
                DutyPersonId = pauseNotice.DutyPersonId,
                //DutyPersonDate = pauseNotice.DutyPersonDate,
                ProfessionalEngineerId = pauseNotice.ProfessionalEngineerId,
                //ProfessionalEngineerTime = pauseNotice.ProfessionalEngineerTime,
                ConstructionManagerId = pauseNotice.ConstructionManagerId,
                //ConstructionManagerTime = pauseNotice.ConstructionManagerTime,
                UnitHeadManId = pauseNotice.UnitHeadManId,
                //UnitHeadManTime = pauseNotice.UnitHeadManTime,
                SupervisorManId = pauseNotice.SupervisorManId,
                //SupervisorManTime = pauseNotice.SupervisorManTime,
                OwnerId = pauseNotice.OwnerId,
                //OwnerTime = pauseNotice.OwnerTime,
                PauseStates = pauseNotice.PauseStates,
            };

            db.Check_PauseNotice.InsertOnSubmit(newPauseNotice);
            db.SubmitChanges();
            ////增加一条编码记录
            BLL.CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(BLL.Const.ProjectPauseNoticeMenuId, pauseNotice.ProjectId, null, pauseNotice.PauseNoticeId, pauseNotice.PauseTime);
        }


        /// <summary>
        /// 修改安全工程暂停令
        /// </summary>
        /// <param name="pauseNotice"></param>
        public static void UpdatePauseNotice(Model.Check_PauseNotice pauseNotice)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Check_PauseNotice newPauseNotice = db.Check_PauseNotice.FirstOrDefault(e => e.PauseNoticeId == pauseNotice.PauseNoticeId);
            if (newPauseNotice != null)
            {
                newPauseNotice.CompileDate = pauseNotice.CompileDate;
                newPauseNotice.UnitId = pauseNotice.UnitId;
                newPauseNotice.ProjectPlace = pauseNotice.ProjectPlace;
                newPauseNotice.UnitWorkId = pauseNotice.UnitWorkId;
                newPauseNotice.WrongContent = pauseNotice.WrongContent;
                newPauseNotice.PauseTime = pauseNotice.PauseTime;
                newPauseNotice.PauseContent = pauseNotice.PauseContent;
                newPauseNotice.OneContent = pauseNotice.OneContent;
                newPauseNotice.SecondContent = pauseNotice.SecondContent;
                newPauseNotice.ThirdContent = pauseNotice.ThirdContent;
                newPauseNotice.IsConfirm = pauseNotice.IsConfirm;
                newPauseNotice.AttachUrl = pauseNotice.AttachUrl;
                newPauseNotice.States = pauseNotice.States;
                newPauseNotice.SignManId = pauseNotice.SignManId;
                newPauseNotice.SignDate = pauseNotice.SignDate;
                newPauseNotice.ApproveManId = pauseNotice.ApproveManId;
                newPauseNotice.ApproveDate = pauseNotice.ApproveDate;
                newPauseNotice.DutyPersonId = pauseNotice.DutyPersonId;
                //newPauseNotice.DutyPersonDate = pauseNotice.DutyPersonDate;
                newPauseNotice.ProfessionalEngineerId = pauseNotice.ProfessionalEngineerId;
                //newPauseNotice.ProfessionalEngineerTime = pauseNotice.ProfessionalEngineerTime;
                newPauseNotice.ConstructionManagerId = pauseNotice.ConstructionManagerId;
                //newPauseNotice.ConstructionManagerTime = pauseNotice.ConstructionManagerTime;
                newPauseNotice.UnitHeadManId = pauseNotice.UnitHeadManId;
                //newPauseNotice.UnitHeadManTime = pauseNotice.UnitHeadManTime;
                newPauseNotice.SupervisorManId = pauseNotice.SupervisorManId;
                //newPauseNotice.SupervisorManTime = pauseNotice.SupervisorManTime;
                newPauseNotice.OwnerId = pauseNotice.OwnerId;
                //newPauseNotice.OwnerTime = pauseNotice.OwnerTime;
                newPauseNotice.PauseStates = pauseNotice.PauseStates;

                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据工程暂停令ID删除对应工程暂停令记录信息
        /// </summary>
        /// <param name="superviseCheckReportId"></param>
        public static void DeletePauseNotice(string pauseNoticeId)
        {
            Model.SGGLDB db = Funs.DB;
            var q = (from x in db.Check_PauseNotice where x.PauseNoticeId == pauseNoticeId select x).FirstOrDefault();
            if (q != null)
            {
                ///删除编码表记录
                BLL.CodeRecordsService.DeleteCodeRecordsByDataId(q.PauseNoticeId);
                ////删除附件表
                BLL.CommonService.DeleteAttachFileById(q.PauseNoticeId);
                ////删除审核流程表
                var getFlow = db.Check_PauseNoticeFlowOperate.Where(x => x.PauseNoticeId == q.PauseNoticeId);
                if (getFlow.Count() > 0)
                {
                    db.Check_PauseNoticeFlowOperate.DeleteAllOnSubmit(getFlow);
                }
                db.Check_PauseNotice.DeleteOnSubmit(q);
                db.SubmitChanges();
            }
        }
    }
}
