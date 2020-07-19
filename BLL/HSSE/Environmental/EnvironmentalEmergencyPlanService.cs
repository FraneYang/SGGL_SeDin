using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 环境事件应急预案
    /// </summary>
    public static class EnvironmentalEmergencyPlanService
    {
        public static Model.SGGLDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取环境事件应急预案
        /// </summary>
        /// <param name="EnvironmentalEmergencyPlanId"></param>
        /// <returns></returns>
        public static Model.Environmental_EnvironmentalEmergencyPlan GetEnvironmentalEmergencyPlanById(string fileId)
        {
            return Funs.DB.Environmental_EnvironmentalEmergencyPlan.FirstOrDefault(e => e.FileId == fileId);
        }

        /// <summary>
        /// 添加环境事件应急预案
        /// </summary>
        /// <param name="EnvironmentalEmergencyPlan"></param>
        public static void AddEnvironmentalEmergencyPlan(Model.Environmental_EnvironmentalEmergencyPlan EnvironmentalEmergencyPlan)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Environmental_EnvironmentalEmergencyPlan newEnvironmentalEmergencyPlan = new Model.Environmental_EnvironmentalEmergencyPlan
            {
                FileId = EnvironmentalEmergencyPlan.FileId,
                FileCode = EnvironmentalEmergencyPlan.FileCode,
                ProjectId = EnvironmentalEmergencyPlan.ProjectId,
                FileName = EnvironmentalEmergencyPlan.FileName,
                FileContent = EnvironmentalEmergencyPlan.FileContent,
                CompileMan = EnvironmentalEmergencyPlan.CompileMan,
                CompileDate = EnvironmentalEmergencyPlan.CompileDate,
                AttachUrl = EnvironmentalEmergencyPlan.AttachUrl,
                States = EnvironmentalEmergencyPlan.States
            };
            db.Environmental_EnvironmentalEmergencyPlan.InsertOnSubmit(newEnvironmentalEmergencyPlan);
            db.SubmitChanges();
            ////增加一条编码记录
            BLL.CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(BLL.Const.EnvironmentalEmergencyPlanMenuId, EnvironmentalEmergencyPlan.ProjectId, null, EnvironmentalEmergencyPlan.FileId, EnvironmentalEmergencyPlan.CompileDate);
        }

        /// <summary>
        /// 修改环境事件应急预案
        /// </summary>
        /// <param name="EnvironmentalEmergencyPlan"></param>
        public static void UpdateEnvironmentalEmergencyPlan(Model.Environmental_EnvironmentalEmergencyPlan EnvironmentalEmergencyPlan)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Environmental_EnvironmentalEmergencyPlan newEnvironmentalEmergencyPlan = db.Environmental_EnvironmentalEmergencyPlan.FirstOrDefault(e => e.FileId == EnvironmentalEmergencyPlan.FileId);
            if (newEnvironmentalEmergencyPlan != null)
            {
                newEnvironmentalEmergencyPlan.FileCode = EnvironmentalEmergencyPlan.FileCode;
                newEnvironmentalEmergencyPlan.FileName = EnvironmentalEmergencyPlan.FileName;
                newEnvironmentalEmergencyPlan.FileContent = EnvironmentalEmergencyPlan.FileContent;
                newEnvironmentalEmergencyPlan.CompileMan = EnvironmentalEmergencyPlan.CompileMan;
                newEnvironmentalEmergencyPlan.CompileDate = EnvironmentalEmergencyPlan.CompileDate;
                newEnvironmentalEmergencyPlan.AttachUrl = EnvironmentalEmergencyPlan.AttachUrl;
                newEnvironmentalEmergencyPlan.States = EnvironmentalEmergencyPlan.States;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除环境事件应急预案
        /// </summary>
        /// <param name="FileId"></param>
        public static void DeleteEnvironmentalEmergencyPlanById(string FileId)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Environmental_EnvironmentalEmergencyPlan EnvironmentalEmergencyPlan = db.Environmental_EnvironmentalEmergencyPlan.FirstOrDefault(e => e.FileId == FileId);
            if (EnvironmentalEmergencyPlan != null)
            {
                ///删除编码表记录
                BLL.CodeRecordsService.DeleteCodeRecordsByDataId(FileId);
                ////删除附件表
                BLL.CommonService.DeleteAttachFileById(EnvironmentalEmergencyPlan.FileId);
                BLL.CommonService.DeleteFlowOperateByID(FileId);
                db.Environmental_EnvironmentalEmergencyPlan.DeleteOnSubmit(EnvironmentalEmergencyPlan);
                db.SubmitChanges();
            }
        }
    }
}
