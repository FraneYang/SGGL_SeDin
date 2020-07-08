using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 应急队伍/培训
    /// </summary>
    public static class EmergencyTeamAndTrainService
    {
        public static Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);

        /// <summary>
        /// 根据主键获取应急队伍/培训
        /// </summary>
        /// <param name="EmergencyTeamAndTrainId"></param>
        /// <returns></returns>
        public static Model.Emergency_EmergencyTeamAndTrain GetEmergencyTeamAndTrainById(string fileId)
        {
            return new Model.SGGLDB(Funs.ConnString).Emergency_EmergencyTeamAndTrain.FirstOrDefault(e => e.FileId == fileId);
        }

        /// <summary>
        /// 添加应急队伍/培训
        /// </summary>
        /// <param name="EmergencyTeamAndTrain"></param>
        public static void AddEmergencyTeamAndTrain(Model.Emergency_EmergencyTeamAndTrain EmergencyTeamAndTrain)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Emergency_EmergencyTeamAndTrain newEmergencyTeamAndTrain = new Model.Emergency_EmergencyTeamAndTrain
            {
                FileId = EmergencyTeamAndTrain.FileId,
                ProjectId = EmergencyTeamAndTrain.ProjectId,
                FileCode = EmergencyTeamAndTrain.FileCode,
                FileName = EmergencyTeamAndTrain.FileName,
                UnitId = EmergencyTeamAndTrain.UnitId,
                FileContent = EmergencyTeamAndTrain.FileContent,
                CompileMan = EmergencyTeamAndTrain.CompileMan,
                CompileDate = EmergencyTeamAndTrain.CompileDate,
                AttachUrl = EmergencyTeamAndTrain.AttachUrl,
                States = EmergencyTeamAndTrain.States
            };
            db.Emergency_EmergencyTeamAndTrain.InsertOnSubmit(newEmergencyTeamAndTrain);
            db.SubmitChanges();
            ////增加一条编码记录
            BLL.CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(BLL.Const.ProjectEmergencyTeamAndTrainMenuId, EmergencyTeamAndTrain.ProjectId, null, EmergencyTeamAndTrain.FileId, EmergencyTeamAndTrain.CompileDate);
        }

        /// <summary>
        /// 修改应急队伍/培训
        /// </summary>
        /// <param name="EmergencyTeamAndTrain"></param>
        public static void UpdateEmergencyTeamAndTrain(Model.Emergency_EmergencyTeamAndTrain EmergencyTeamAndTrain)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Emergency_EmergencyTeamAndTrain newEmergencyTeamAndTrain = db.Emergency_EmergencyTeamAndTrain.FirstOrDefault(e => e.FileId == EmergencyTeamAndTrain.FileId);
            if (newEmergencyTeamAndTrain != null)
            {
                newEmergencyTeamAndTrain.FileCode = EmergencyTeamAndTrain.FileCode;
                newEmergencyTeamAndTrain.FileName = EmergencyTeamAndTrain.FileName;
                newEmergencyTeamAndTrain.UnitId = EmergencyTeamAndTrain.UnitId;
                newEmergencyTeamAndTrain.FileContent = EmergencyTeamAndTrain.FileContent;
                newEmergencyTeamAndTrain.CompileMan = EmergencyTeamAndTrain.CompileMan;
                newEmergencyTeamAndTrain.CompileDate = EmergencyTeamAndTrain.CompileDate;
                newEmergencyTeamAndTrain.AttachUrl = EmergencyTeamAndTrain.AttachUrl;
                newEmergencyTeamAndTrain.States = EmergencyTeamAndTrain.States;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除应急队伍/培训
        /// </summary>
        /// <param name="FileId"></param>
        public static void DeleteEmergencyTeamAndTrainById(string FileId)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Emergency_EmergencyTeamAndTrain EmergencyTeamAndTrain = db.Emergency_EmergencyTeamAndTrain.FirstOrDefault(e => e.FileId == FileId);
            if (EmergencyTeamAndTrain != null)
            {
                ///删除编码表记录
                BLL.CodeRecordsService.DeleteCodeRecordsByDataId(EmergencyTeamAndTrain.FileId);
                ////删除附件表
                BLL.CommonService.DeleteAttachFileById(EmergencyTeamAndTrain.FileId);
                ////删除流程表
                BLL.CommonService.DeleteFlowOperateByID(EmergencyTeamAndTrain.FileId);
                db.Emergency_EmergencyTeamAndTrain.DeleteOnSubmit(EmergencyTeamAndTrain);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        public static void AddEmergencyTeamItem(Model.Emergency_EmergencyTeamItem item)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Emergency_EmergencyTeamItem newItem = new Model.Emergency_EmergencyTeamItem
            {
                EmergencyTeamItemId = item.EmergencyTeamItemId,
                FileId = item.FileId,
                PersonId = item.PersonId,
                Job = item.Job,
                Tel = item.Tel,
            };
            db.Emergency_EmergencyTeamItem.InsertOnSubmit(newItem);
            db.SubmitChanges();
        }
        /// <summary>
        ///  删除
        /// </summary>
        /// <param name="FileId"></param>
        public static void DeleteEmergency_EmergencyTeamItem(string FileId)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            var delItem = from x in db.Emergency_EmergencyTeamItem where x.FileId == FileId select x;
            if (delItem.Count() > 0)
            {
                db.Emergency_EmergencyTeamItem.DeleteAllOnSubmit(delItem);
                db.SubmitChanges();
            }
        }
    }
}
