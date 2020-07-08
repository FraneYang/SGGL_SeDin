using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 体检管理
    /// </summary>
    public static class PhysicalExaminationService
    {
        public static Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);

        /// <summary>
        /// 根据主键获取体检管理
        /// </summary>
        /// <param name="PhysicalExaminationId"></param>
        /// <returns></returns>
        public static Model.OccupationHealth_PhysicalExamination GetPhysicalExaminationById(string fileId)
        {
            return new Model.SGGLDB(Funs.ConnString).OccupationHealth_PhysicalExamination.FirstOrDefault(e => e.FileId == fileId);
        }

        /// <summary>
        /// 添加体检管理
        /// </summary>
        /// <param name="PhysicalExamination"></param>
        public static void AddPhysicalExamination(Model.OccupationHealth_PhysicalExamination PhysicalExamination)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.OccupationHealth_PhysicalExamination newPhysicalExamination = new Model.OccupationHealth_PhysicalExamination
            {
                FileId = PhysicalExamination.FileId,
                FileCode = PhysicalExamination.FileCode,
                ProjectId = PhysicalExamination.ProjectId,
                FileName = PhysicalExamination.FileName,
                FileContent = PhysicalExamination.FileContent,
                CompileMan = PhysicalExamination.CompileMan,
                CompileDate = PhysicalExamination.CompileDate,
                AttachUrl = PhysicalExamination.AttachUrl,
                States = PhysicalExamination.States
            };
            db.OccupationHealth_PhysicalExamination.InsertOnSubmit(newPhysicalExamination);
            db.SubmitChanges();
            ////增加一条编码记录
            BLL.CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(BLL.Const.PhysicalExaminationMenuId, PhysicalExamination.ProjectId, null, PhysicalExamination.FileId, PhysicalExamination.CompileDate);
        }

        /// <summary>
        /// 修改体检管理
        /// </summary>
        /// <param name="PhysicalExamination"></param>
        public static void UpdatePhysicalExamination(Model.OccupationHealth_PhysicalExamination PhysicalExamination)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.OccupationHealth_PhysicalExamination newPhysicalExamination = db.OccupationHealth_PhysicalExamination.FirstOrDefault(e => e.FileId == PhysicalExamination.FileId);
            if (newPhysicalExamination != null)
            {
                newPhysicalExamination.FileCode = PhysicalExamination.FileCode;
                newPhysicalExamination.FileName = PhysicalExamination.FileName;
                newPhysicalExamination.FileContent = PhysicalExamination.FileContent;
                newPhysicalExamination.CompileMan = PhysicalExamination.CompileMan;
                newPhysicalExamination.CompileDate = PhysicalExamination.CompileDate;
                newPhysicalExamination.AttachUrl = PhysicalExamination.AttachUrl;
                newPhysicalExamination.States = PhysicalExamination.States;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除体检管理
        /// </summary>
        /// <param name="FileId"></param>
        public static void DeletePhysicalExaminationById(string FileId)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.OccupationHealth_PhysicalExamination PhysicalExamination = db.OccupationHealth_PhysicalExamination.FirstOrDefault(e => e.FileId == FileId);
            if (PhysicalExamination != null)
            {
                ///删除编码表记录
                BLL.CodeRecordsService.DeleteCodeRecordsByDataId(FileId);
                ////删除附件表
                BLL.CommonService.DeleteAttachFileById(PhysicalExamination.FileId);
                BLL.CommonService.DeleteFlowOperateByID(FileId);
                db.OccupationHealth_PhysicalExamination.DeleteOnSubmit(PhysicalExamination);
                db.SubmitChanges();
            }
        }
    }
}
