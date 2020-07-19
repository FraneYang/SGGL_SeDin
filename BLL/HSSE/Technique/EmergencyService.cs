using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public static class EmergencyService
    {
        public static Model.SGGLDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取应急预案
        /// </summary>
        /// <param name="emergencyId"></param>
        /// <returns></returns>
        public static Model.Technique_Emergency GetEmergencyListById(string emergencyId)
        {
            return Funs.DB.Technique_Emergency.FirstOrDefault(e => e.EmergencyId == emergencyId);
        }

        /// <summary>
        /// 根据整理人获取应急预案
        /// </summary>
        /// <param name="compileMan"></param>
        /// <returns></returns>
        public static List<Model.Technique_Emergency> GetEmergencyByCompileMan(string compileMan)
        {
            return (from x in Funs.DB.Technique_Emergency where x.CompileMan == compileMan select x).ToList();
        }

        /// <summary>
        /// 添加应急预案
        /// </summary>
        /// <param name="emergencyList"></param>
        public static void AddEmergencyList(Model.Technique_Emergency emergencyList)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Technique_Emergency newEmergencyList = new Model.Technique_Emergency
            {
                EmergencyId = emergencyList.EmergencyId,
                EmergencyTypeId = emergencyList.EmergencyTypeId,
                EmergencyCode = emergencyList.EmergencyCode,
                EmergencyName = emergencyList.EmergencyName,
                Summary = emergencyList.Summary,
                AttachUrl = emergencyList.AttachUrl,
                Remark = emergencyList.Remark,
                CompileMan = emergencyList.CompileMan,
                CompileDate = emergencyList.CompileDate,
                IsPass = emergencyList.IsPass,
                UnitId = emergencyList.UnitId,
                UpState = emergencyList.UpState
            };

            db.Technique_Emergency.InsertOnSubmit(newEmergencyList);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改应急预案
        /// </summary>
        /// <param name="emergencyList"></param>
        public static void UpdateEmergencyList(Model.Technique_Emergency emergencyList)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Technique_Emergency newEmergencyList = db.Technique_Emergency.FirstOrDefault(e => e.EmergencyId == emergencyList.EmergencyId);
            if (newEmergencyList != null)
            {
                newEmergencyList.EmergencyTypeId = emergencyList.EmergencyTypeId;
                newEmergencyList.EmergencyCode = emergencyList.EmergencyCode;
                newEmergencyList.EmergencyName = emergencyList.EmergencyName;
                newEmergencyList.Summary = emergencyList.Summary;
                newEmergencyList.AttachUrl = emergencyList.AttachUrl;
                newEmergencyList.Remark = emergencyList.Remark;
                newEmergencyList.UpState = emergencyList.UpState;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 修改应急预案 是否采用
        /// </summary>
        /// <param name="emergencyList"></param>
        public static void UpdateEmergencyListIsPass(Model.Technique_Emergency emergencyList)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Technique_Emergency newEmergencyList = db.Technique_Emergency.FirstOrDefault(e => e.EmergencyId == emergencyList.EmergencyId);
            if (newEmergencyList != null)
            {
                newEmergencyList.AuditMan = emergencyList.AuditMan;
                newEmergencyList.AuditDate = emergencyList.AuditDate;
                newEmergencyList.IsPass = emergencyList.IsPass;
                newEmergencyList.UpState = emergencyList.UpState;
                db.SubmitChanges();
            }
        }
        /// <summary>
        ///根据主键删除应急预案
        /// </summary>
        /// <param name="emergencyId"></param>
        public static void DeleteEmergencyListById(string emergencyId)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Technique_Emergency emergencyList = db.Technique_Emergency.FirstOrDefault(e => e.EmergencyId == emergencyId);
            if (emergencyList != null)
            {
                if (!string.IsNullOrEmpty(emergencyList.AttachUrl))
                {
                    BLL.UploadFileService.DeleteFile(Funs.RootPath, emergencyList.AttachUrl);
                }
                ////删除附件表
                BLL.CommonService.DeleteAttachFileById(emergencyList.EmergencyId);

                db.Technique_Emergency.DeleteOnSubmit(emergencyList);
                db.SubmitChanges();
            }
        }

    }
}
