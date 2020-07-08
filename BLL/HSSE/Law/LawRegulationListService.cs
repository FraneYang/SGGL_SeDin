using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 法律法规表
    /// </summary>
    public static class LawRegulationListService
    {
        public static Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);

        /// <summary>
        /// 根据主键获取法律法规
        /// </summary>
        /// <param name="lawRegulationId"></param>
        /// <returns></returns>
        public static Model.Law_LawRegulationList GetLawRegulationListById(string lawRegulationId)
        {
            return new Model.SGGLDB(Funs.ConnString).Law_LawRegulationList.FirstOrDefault(e => e.LawRegulationId == lawRegulationId);
        }

        /// <summary>
        /// 根据主键获取法律法规
        /// </summary>
        /// <param name="lawRegulationId"></param>
        /// <returns></returns>
        public static Model.View_Law_LawRegulationList GetViewLawRegulationListById(string lawRegulationId)
        {
            return new Model.SGGLDB(Funs.ConnString).View_Law_LawRegulationList.FirstOrDefault(e => e.LawRegulationId == lawRegulationId);
        }

        /// <summary>
        /// 根据编制人获取法律法规
        /// </summary>
        /// <param name="lawRegulationId"></param>
        /// <returns></returns>
        public static List<Model.Law_LawRegulationList> GetLawRegulationListByCompileMan(string compileMan)
        {
            return (from x in new Model.SGGLDB(Funs.ConnString).Law_LawRegulationList where x.CompileMan== compileMan select x).ToList();
        }

        /// <summary>
        /// 添加法律法规
        /// </summary>
        /// <param name="lawRegulationList"></param>
        public static void AddLawRegulationList(Model.Law_LawRegulationList lawRegulationList)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Law_LawRegulationList newLawRegulationList = new Model.Law_LawRegulationList
            {
                LawRegulationId = lawRegulationList.LawRegulationId,
                LawRegulationCode = lawRegulationList.LawRegulationCode,
                LawRegulationName = lawRegulationList.LawRegulationName,
                ApprovalDate = lawRegulationList.ApprovalDate,
                EffectiveDate = lawRegulationList.EffectiveDate,
                Description = lawRegulationList.Description,
                AttachUrl = lawRegulationList.AttachUrl,
                LawsRegulationsTypeId = lawRegulationList.LawsRegulationsTypeId,
                CompileMan = lawRegulationList.CompileMan,
                CompileDate = lawRegulationList.CompileDate,
                UnitId = lawRegulationList.UnitId,
                IsPass = lawRegulationList.IsPass,
                UpState = lawRegulationList.UpState,
                IsBuild = false
            };
            db.Law_LawRegulationList.InsertOnSubmit(newLawRegulationList);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改法律法规
        /// </summary>
        /// <param name="lawRegulationList"></param>
        public static void UpdateLawRegulationList(Model.Law_LawRegulationList lawRegulationList)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Law_LawRegulationList newLawRegulationList = db.Law_LawRegulationList.FirstOrDefault(e => e.LawRegulationId == lawRegulationList.LawRegulationId);
            if (newLawRegulationList != null)
            {
                newLawRegulationList.LawRegulationCode = lawRegulationList.LawRegulationCode;
                newLawRegulationList.LawRegulationName = lawRegulationList.LawRegulationName;
                newLawRegulationList.ApprovalDate = lawRegulationList.ApprovalDate;
                newLawRegulationList.EffectiveDate = lawRegulationList.EffectiveDate;
                newLawRegulationList.Description = lawRegulationList.Description;
                newLawRegulationList.AttachUrl = lawRegulationList.AttachUrl;
                newLawRegulationList.LawsRegulationsTypeId = lawRegulationList.LawsRegulationsTypeId;
                newLawRegulationList.UpState = lawRegulationList.UpState;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 修改法律法规 是否采用
        /// </summary>
        /// <param name="lawRegulationList"></param>
        public static void UpdateLawRegulationListIsPass(Model.Law_LawRegulationList lawRegulationList)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Law_LawRegulationList newLawRegulationList = db.Law_LawRegulationList.FirstOrDefault(e => e.LawRegulationId == lawRegulationList.LawRegulationId);
            if (newLawRegulationList != null)
            {              
                newLawRegulationList.AuditMan = lawRegulationList.AuditMan;
                newLawRegulationList.AuditDate = lawRegulationList.AuditDate;
                newLawRegulationList.IsPass = lawRegulationList.IsPass;
                newLawRegulationList.UpState = lawRegulationList.UpState;
                db.SubmitChanges();
            }
        }

        /// <summary>
        ///根据主键删除法律法规
        /// </summary>
        /// <param name="lawRegulationId"></param>
        public static void DeleteLawRegulationListById(string lawRegulationId)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Law_LawRegulationList lawRegulationList = db.Law_LawRegulationList.FirstOrDefault(e => e.LawRegulationId == lawRegulationId);
            if (lawRegulationList!=null)
            {
                //if (!string.IsNullOrEmpty(lawRegulationList.AttachUrl))
                //{
                //    BLL.UploadFileService.DeleteFile(Funs.RootPath, lawRegulationList.AttachUrl);
                //}

                ////删除附件表
               // BLL.CommonService.DeleteAttachFileById(lawRegulationList.LawRegulationId);

                db.Law_LawRegulationList.DeleteOnSubmit(lawRegulationList);
                db.SubmitChanges();
            }
        }
    }
}
