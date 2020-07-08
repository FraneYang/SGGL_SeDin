using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 危害检测
    /// </summary>
    public static class HazardDetectionService
    {
        public static Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);

        /// <summary>
        /// 根据主键获取危害检测
        /// </summary>
        /// <param name="HazardDetectionId"></param>
        /// <returns></returns>
        public static Model.OccupationHealth_HazardDetection GetHazardDetectionById(string fileId)
        {
            return new Model.SGGLDB(Funs.ConnString).OccupationHealth_HazardDetection.FirstOrDefault(e => e.FileId == fileId);
        }

        /// <summary>
        /// 添加危害检测
        /// </summary>
        /// <param name="HazardDetection"></param>
        public static void AddHazardDetection(Model.OccupationHealth_HazardDetection HazardDetection)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.OccupationHealth_HazardDetection newHazardDetection = new Model.OccupationHealth_HazardDetection
            {
                FileId = HazardDetection.FileId,
                FileCode = HazardDetection.FileCode,
                ProjectId = HazardDetection.ProjectId,
                FileName = HazardDetection.FileName,
                FileContent = HazardDetection.FileContent,
                CompileMan = HazardDetection.CompileMan,
                CompileDate = HazardDetection.CompileDate,
                AttachUrl = HazardDetection.AttachUrl,
                States = HazardDetection.States
            };
            db.OccupationHealth_HazardDetection.InsertOnSubmit(newHazardDetection);
            db.SubmitChanges();
            ////增加一条编码记录
            BLL.CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(BLL.Const.HazardDetectionMenuId, HazardDetection.ProjectId, null, HazardDetection.FileId, HazardDetection.CompileDate);
        }

        /// <summary>
        /// 修改危害检测
        /// </summary>
        /// <param name="HazardDetection"></param>
        public static void UpdateHazardDetection(Model.OccupationHealth_HazardDetection HazardDetection)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.OccupationHealth_HazardDetection newHazardDetection = db.OccupationHealth_HazardDetection.FirstOrDefault(e => e.FileId == HazardDetection.FileId);
            if (newHazardDetection != null)
            {
                newHazardDetection.FileCode = HazardDetection.FileCode;
                newHazardDetection.FileName = HazardDetection.FileName;
                newHazardDetection.FileContent = HazardDetection.FileContent;
                newHazardDetection.CompileMan = HazardDetection.CompileMan;
                newHazardDetection.CompileDate = HazardDetection.CompileDate;
                newHazardDetection.AttachUrl = HazardDetection.AttachUrl;
                newHazardDetection.States = HazardDetection.States;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除危害检测
        /// </summary>
        /// <param name="FileId"></param>
        public static void DeleteHazardDetectionById(string FileId)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.OccupationHealth_HazardDetection HazardDetection = db.OccupationHealth_HazardDetection.FirstOrDefault(e => e.FileId == FileId);
            if (HazardDetection != null)
            {
                ///删除编码表记录
                BLL.CodeRecordsService.DeleteCodeRecordsByDataId(FileId);
                ////删除附件表
                BLL.CommonService.DeleteAttachFileById(HazardDetection.FileId);
                BLL.CommonService.DeleteFlowOperateByID(FileId);
                db.OccupationHealth_HazardDetection.DeleteOnSubmit(HazardDetection);
                db.SubmitChanges();
            }
        }
    }
}
