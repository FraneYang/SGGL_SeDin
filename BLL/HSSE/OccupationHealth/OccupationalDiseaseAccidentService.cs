using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 职业病事故
    /// </summary>
    public static class OccupationalDiseaseAccidentService
    {
        public static Model.SGGLDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取职业病事故
        /// </summary>
        /// <param name="OccupationalDiseaseAccidentId"></param>
        /// <returns></returns>
        public static Model.OccupationHealth_OccupationalDiseaseAccident GetOccupationalDiseaseAccidentById(string fileId)
        {
            return Funs.DB.OccupationHealth_OccupationalDiseaseAccident.FirstOrDefault(e => e.FileId == fileId);
        }

        /// <summary>
        /// 添加职业病事故
        /// </summary>
        /// <param name="OccupationalDiseaseAccident"></param>
        public static void AddOccupationalDiseaseAccident(Model.OccupationHealth_OccupationalDiseaseAccident OccupationalDiseaseAccident)
        {
            Model.SGGLDB db = Funs.DB;
            Model.OccupationHealth_OccupationalDiseaseAccident newOccupationalDiseaseAccident = new Model.OccupationHealth_OccupationalDiseaseAccident
            {
                FileId = OccupationalDiseaseAccident.FileId,
                FileCode = OccupationalDiseaseAccident.FileCode,
                ProjectId = OccupationalDiseaseAccident.ProjectId,
                FileName = OccupationalDiseaseAccident.FileName,
                FileContent = OccupationalDiseaseAccident.FileContent,
                CompileMan = OccupationalDiseaseAccident.CompileMan,
                CompileDate = OccupationalDiseaseAccident.CompileDate,
                AttachUrl = OccupationalDiseaseAccident.AttachUrl,
                States = OccupationalDiseaseAccident.States
            };
            db.OccupationHealth_OccupationalDiseaseAccident.InsertOnSubmit(newOccupationalDiseaseAccident);
            db.SubmitChanges();
            ////增加一条编码记录
            BLL.CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(BLL.Const.OccupationalDiseaseAccidentMenuId, OccupationalDiseaseAccident.ProjectId, null, OccupationalDiseaseAccident.FileId, OccupationalDiseaseAccident.CompileDate);
        }

        /// <summary>
        /// 修改职业病事故
        /// </summary>
        /// <param name="OccupationalDiseaseAccident"></param>
        public static void UpdateOccupationalDiseaseAccident(Model.OccupationHealth_OccupationalDiseaseAccident OccupationalDiseaseAccident)
        {
            Model.SGGLDB db = Funs.DB;
            Model.OccupationHealth_OccupationalDiseaseAccident newOccupationalDiseaseAccident = db.OccupationHealth_OccupationalDiseaseAccident.FirstOrDefault(e => e.FileId == OccupationalDiseaseAccident.FileId);
            if (newOccupationalDiseaseAccident != null)
            {
                newOccupationalDiseaseAccident.FileCode = OccupationalDiseaseAccident.FileCode;
                newOccupationalDiseaseAccident.FileName = OccupationalDiseaseAccident.FileName;
                newOccupationalDiseaseAccident.FileContent = OccupationalDiseaseAccident.FileContent;
                newOccupationalDiseaseAccident.CompileMan = OccupationalDiseaseAccident.CompileMan;
                newOccupationalDiseaseAccident.CompileDate = OccupationalDiseaseAccident.CompileDate;
                newOccupationalDiseaseAccident.AttachUrl = OccupationalDiseaseAccident.AttachUrl;
                newOccupationalDiseaseAccident.States = OccupationalDiseaseAccident.States;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除职业病事故
        /// </summary>
        /// <param name="FileId"></param>
        public static void DeleteOccupationalDiseaseAccidentById(string FileId)
        {
            Model.SGGLDB db = Funs.DB;
            Model.OccupationHealth_OccupationalDiseaseAccident OccupationalDiseaseAccident = db.OccupationHealth_OccupationalDiseaseAccident.FirstOrDefault(e => e.FileId == FileId);
            if (OccupationalDiseaseAccident != null)
            {
                ///删除编码表记录
                BLL.CodeRecordsService.DeleteCodeRecordsByDataId(FileId);
                ////删除附件表
                BLL.CommonService.DeleteAttachFileById(OccupationalDiseaseAccident.FileId);
                BLL.CommonService.DeleteFlowOperateByID(FileId);
                db.OccupationHealth_OccupationalDiseaseAccident.DeleteOnSubmit(OccupationalDiseaseAccident);
                db.SubmitChanges();
            }
        }
    }
}
