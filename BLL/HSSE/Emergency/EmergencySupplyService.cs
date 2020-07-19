using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 应急物资管理
    /// </summary>
    public static class EmergencySupplyService
    {
        public static Model.SGGLDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取应急物资管理
        /// </summary>
        /// <param name="EmergencySupplyId"></param>
        /// <returns></returns>
        public static Model.Emergency_EmergencySupply GetEmergencySupplyById(string fileId)
        {
            return Funs.DB.Emergency_EmergencySupply.FirstOrDefault(e => e.FileId == fileId);
        }

        /// <summary>
        /// 添加应急物资管理
        /// </summary>
        /// <param name="EmergencySupply"></param>
        public static void AddEmergencySupply(Model.Emergency_EmergencySupply EmergencySupply)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Emergency_EmergencySupply newEmergencySupply = new Model.Emergency_EmergencySupply
            {
                FileId = EmergencySupply.FileId,
                ProjectId = EmergencySupply.ProjectId,
                FileCode = EmergencySupply.FileCode,
                FileName = EmergencySupply.FileName,
                UnitId = EmergencySupply.UnitId,
                FileContent = EmergencySupply.FileContent,
                CompileMan = EmergencySupply.CompileMan,
                CompileDate = EmergencySupply.CompileDate,
                AttachUrl = EmergencySupply.AttachUrl,
                States = EmergencySupply.States
            };
            db.Emergency_EmergencySupply.InsertOnSubmit(newEmergencySupply);
            db.SubmitChanges();
            ////增加一条编码记录
            BLL.CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(BLL.Const.ProjectEmergencySupplyMenuId, EmergencySupply.ProjectId, null, EmergencySupply.FileId, EmergencySupply.CompileDate);
        }

        /// <summary>
        /// 修改应急物资管理
        /// </summary>
        /// <param name="EmergencySupply"></param>
        public static void UpdateEmergencySupply(Model.Emergency_EmergencySupply EmergencySupply)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Emergency_EmergencySupply newEmergencySupply = db.Emergency_EmergencySupply.FirstOrDefault(e => e.FileId == EmergencySupply.FileId);
            if (newEmergencySupply != null)
            {
                newEmergencySupply.FileCode = EmergencySupply.FileCode;
                newEmergencySupply.FileName = EmergencySupply.FileName;
                newEmergencySupply.UnitId = EmergencySupply.UnitId;
                newEmergencySupply.FileContent = EmergencySupply.FileContent;
                newEmergencySupply.CompileMan = EmergencySupply.CompileMan;
                newEmergencySupply.CompileDate = EmergencySupply.CompileDate;
                newEmergencySupply.AttachUrl = EmergencySupply.AttachUrl;
                newEmergencySupply.States = EmergencySupply.States;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除应急物资管理
        /// </summary>
        /// <param name="FileId"></param>
        public static void DeleteEmergencySupplyById(string FileId)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Emergency_EmergencySupply EmergencySupply = db.Emergency_EmergencySupply.FirstOrDefault(e => e.FileId == FileId);
            if (EmergencySupply != null)
            {
                ///删除编码表记录
                BLL.CodeRecordsService.DeleteCodeRecordsByDataId(EmergencySupply.FileId);
                ////删除附件表
                BLL.CommonService.DeleteAttachFileById(EmergencySupply.FileId);
                ////删除流程表
                BLL.CommonService.DeleteFlowOperateByID(EmergencySupply.FileId);
                db.Emergency_EmergencySupply.DeleteOnSubmit(EmergencySupply);
                db.SubmitChanges();
            }
        }
    }
}
