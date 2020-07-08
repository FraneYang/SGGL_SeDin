using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 环境监测数据
    /// </summary>
    public static class EnvironmentalMonitoringService
    {
        public static Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);

        /// <summary>
        /// 根据主键获取环境监测数据
        /// </summary>
        /// <param name="EnvironmentalMonitoringId"></param>
        /// <returns></returns>
        public static Model.Environmental_EnvironmentalMonitoring GetEnvironmentalMonitoringById(string fileId)
        {
            return new Model.SGGLDB(Funs.ConnString).Environmental_EnvironmentalMonitoring.FirstOrDefault(e => e.FileId == fileId);
        }

        /// <summary>
        /// 添加环境监测数据
        /// </summary>
        /// <param name="EnvironmentalMonitoring"></param>
        public static void AddEnvironmentalMonitoring(Model.Environmental_EnvironmentalMonitoring EnvironmentalMonitoring)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Environmental_EnvironmentalMonitoring newEnvironmentalMonitoring = new Model.Environmental_EnvironmentalMonitoring
            {
                FileId = EnvironmentalMonitoring.FileId,
                FileCode = EnvironmentalMonitoring.FileCode,
                ProjectId = EnvironmentalMonitoring.ProjectId,
                FileName = EnvironmentalMonitoring.FileName,
                FileContent = EnvironmentalMonitoring.FileContent,
                CompileMan = EnvironmentalMonitoring.CompileMan,
                CompileDate = EnvironmentalMonitoring.CompileDate,
                AttachUrl = EnvironmentalMonitoring.AttachUrl,
                States = EnvironmentalMonitoring.States
            };
            db.Environmental_EnvironmentalMonitoring.InsertOnSubmit(newEnvironmentalMonitoring);
            db.SubmitChanges();
            ////增加一条编码记录
            BLL.CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(BLL.Const.EnvironmentalMonitoringMenuId, EnvironmentalMonitoring.ProjectId, null, EnvironmentalMonitoring.FileId, EnvironmentalMonitoring.CompileDate);
        }

        /// <summary>
        /// 修改环境监测数据
        /// </summary>
        /// <param name="EnvironmentalMonitoring"></param>
        public static void UpdateEnvironmentalMonitoring(Model.Environmental_EnvironmentalMonitoring EnvironmentalMonitoring)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Environmental_EnvironmentalMonitoring newEnvironmentalMonitoring = db.Environmental_EnvironmentalMonitoring.FirstOrDefault(e => e.FileId == EnvironmentalMonitoring.FileId);
            if (newEnvironmentalMonitoring != null)
            {
                newEnvironmentalMonitoring.FileCode = EnvironmentalMonitoring.FileCode;
                newEnvironmentalMonitoring.FileName = EnvironmentalMonitoring.FileName;
                newEnvironmentalMonitoring.FileContent = EnvironmentalMonitoring.FileContent;
                newEnvironmentalMonitoring.CompileMan = EnvironmentalMonitoring.CompileMan;
                newEnvironmentalMonitoring.CompileDate = EnvironmentalMonitoring.CompileDate;
                newEnvironmentalMonitoring.AttachUrl = EnvironmentalMonitoring.AttachUrl;
                newEnvironmentalMonitoring.States = EnvironmentalMonitoring.States;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除环境监测数据
        /// </summary>
        /// <param name="FileId"></param>
        public static void DeleteEnvironmentalMonitoringById(string FileId)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Environmental_EnvironmentalMonitoring EnvironmentalMonitoring = db.Environmental_EnvironmentalMonitoring.FirstOrDefault(e => e.FileId == FileId);
            if (EnvironmentalMonitoring != null)
            {
                ///删除编码表记录
                BLL.CodeRecordsService.DeleteCodeRecordsByDataId(FileId);
                ////删除附件表
                BLL.CommonService.DeleteAttachFileById(EnvironmentalMonitoring.FileId);
                BLL.CommonService.DeleteFlowOperateByID(FileId);
                db.Environmental_EnvironmentalMonitoring.DeleteOnSubmit(EnvironmentalMonitoring);
                db.SubmitChanges();
            }
        }
    }
}
