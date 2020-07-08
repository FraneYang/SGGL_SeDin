using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 环评报告
    /// </summary>
    public static class EIAReportService
    {
        public static Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);

        /// <summary>
        /// 根据主键获取环评报告
        /// </summary>
        /// <param name="EIAReportId"></param>
        /// <returns></returns>
        public static Model.Environmental_EIAReport GetEIAReportById(string fileId)
        {
            return new Model.SGGLDB(Funs.ConnString).Environmental_EIAReport.FirstOrDefault(e => e.FileId == fileId);
        }

        /// <summary>
        /// 添加环评报告
        /// </summary>
        /// <param name="EIAReport"></param>
        public static void AddEIAReport(Model.Environmental_EIAReport EIAReport)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Environmental_EIAReport newEIAReport = new Model.Environmental_EIAReport
            {
                FileId = EIAReport.FileId,
                FileCode = EIAReport.FileCode,
                ProjectId = EIAReport.ProjectId,
                FileName = EIAReport.FileName,
                FileContent = EIAReport.FileContent,
                CompileMan = EIAReport.CompileMan,
                CompileDate = EIAReport.CompileDate,
                AttachUrl = EIAReport.AttachUrl,
                States = EIAReport.States
            };
            db.Environmental_EIAReport.InsertOnSubmit(newEIAReport);
            db.SubmitChanges();
            ////增加一条编码记录
            BLL.CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(BLL.Const.EIAReportMenuId, EIAReport.ProjectId, null, EIAReport.FileId, EIAReport.CompileDate);
        }

        /// <summary>
        /// 修改环评报告
        /// </summary>
        /// <param name="EIAReport"></param>
        public static void UpdateEIAReport(Model.Environmental_EIAReport EIAReport)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Environmental_EIAReport newEIAReport = db.Environmental_EIAReport.FirstOrDefault(e => e.FileId == EIAReport.FileId);
            if (newEIAReport != null)
            {
                newEIAReport.FileCode = EIAReport.FileCode;
                newEIAReport.FileName = EIAReport.FileName;
                newEIAReport.FileContent = EIAReport.FileContent;
                newEIAReport.CompileMan = EIAReport.CompileMan;
                newEIAReport.CompileDate = EIAReport.CompileDate;
                newEIAReport.AttachUrl = EIAReport.AttachUrl;
                newEIAReport.States = EIAReport.States;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除环评报告
        /// </summary>
        /// <param name="FileId"></param>
        public static void DeleteEIAReportById(string FileId)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Environmental_EIAReport EIAReport = db.Environmental_EIAReport.FirstOrDefault(e => e.FileId == FileId);
            if (EIAReport != null)
            {
                ///删除编码表记录
                BLL.CodeRecordsService.DeleteCodeRecordsByDataId(FileId);
                ////删除附件表
                BLL.CommonService.DeleteAttachFileById(EIAReport.FileId);
                BLL.CommonService.DeleteFlowOperateByID(FileId);
                db.Environmental_EIAReport.DeleteOnSubmit(EIAReport);
                db.SubmitChanges();
            }
        }
    }
}
