using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public static class SpecialSchemeService
    {

        public static Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);

        /// <summary>
        /// 根据主键获取专项方案
        /// </summary>
        /// <param name="specialSchemeId"></param>
        /// <returns></returns>
        public static Model.Technique_SpecialScheme GetSpecialSchemeListById(string specialSchemeId)
        {
            return new Model.SGGLDB(Funs.ConnString).Technique_SpecialScheme.FirstOrDefault(e => e.SpecialSchemeId == specialSchemeId);
        }

        /// <summary>
        /// 根据整理人获取专项方案
        /// </summary>
        /// <param name="compileMan"></param>
        /// <returns></returns>
        public static List<Model.Technique_SpecialScheme> GetSpecialSchemeByCompileMan(string compileMan)
        {
            return (from x in new Model.SGGLDB(Funs.ConnString).Technique_SpecialScheme where x.CompileMan == compileMan select x).ToList();
        }

        /// <summary>
        /// 添加专项方案
        /// </summary>
        /// <param name="specialSchemeList"></param>
        public static void AddSpecialSchemeList(Model.Technique_SpecialScheme specialSchemeList)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Technique_SpecialScheme newSpecialSchemeList = new Model.Technique_SpecialScheme
            {
                SpecialSchemeId = specialSchemeList.SpecialSchemeId,
                SpecialSchemeTypeId = specialSchemeList.SpecialSchemeTypeId,
                SpecialSchemeCode = specialSchemeList.SpecialSchemeCode,
                SpecialSchemeName = specialSchemeList.SpecialSchemeName,
                Summary = specialSchemeList.Summary,
                AttachUrl = specialSchemeList.AttachUrl,
                CompileMan = specialSchemeList.CompileMan,
                CompileDate = specialSchemeList.CompileDate,
                UnitId = specialSchemeList.UnitId
            };
            newSpecialSchemeList.CompileMan = specialSchemeList.CompileMan;
            newSpecialSchemeList.CompileDate = specialSchemeList.CompileDate;
            newSpecialSchemeList.IsPass = specialSchemeList.IsPass;
            newSpecialSchemeList.UpState = specialSchemeList.UpState;
            db.Technique_SpecialScheme.InsertOnSubmit(newSpecialSchemeList);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改专项方案
        /// </summary>
        /// <param name="specialSchemeList"></param>
        public static void UpdateSpecialSchemeList(Model.Technique_SpecialScheme specialSchemeList)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Technique_SpecialScheme newSpecialSchemeList = db.Technique_SpecialScheme.FirstOrDefault(e => e.SpecialSchemeId == specialSchemeList.SpecialSchemeId);
            if (newSpecialSchemeList != null)
            {
                newSpecialSchemeList.SpecialSchemeTypeId = specialSchemeList.SpecialSchemeTypeId;
                newSpecialSchemeList.SpecialSchemeCode = specialSchemeList.SpecialSchemeCode;
                newSpecialSchemeList.SpecialSchemeName = specialSchemeList.SpecialSchemeName;
                newSpecialSchemeList.Summary = specialSchemeList.Summary;
                newSpecialSchemeList.AttachUrl = specialSchemeList.AttachUrl;
                newSpecialSchemeList.CompileMan = specialSchemeList.CompileMan;
                newSpecialSchemeList.CompileDate = specialSchemeList.CompileDate;
                newSpecialSchemeList.UnitId = specialSchemeList.UnitId;
                newSpecialSchemeList.UpState = specialSchemeList.UpState;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 修改专项方案 是否采用
        /// </summary>
        /// <param name="specialSchemeList"></param>
        public static void UpdateSpecialSchemeListIsPass(Model.Technique_SpecialScheme specialSchemeList)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Technique_SpecialScheme newSpecialSchemeList = db.Technique_SpecialScheme.FirstOrDefault(e => e.SpecialSchemeId == specialSchemeList.SpecialSchemeId);
            if (newSpecialSchemeList != null)
            {
                newSpecialSchemeList.AuditMan = specialSchemeList.AuditMan;
                newSpecialSchemeList.AuditDate = specialSchemeList.AuditDate;
                newSpecialSchemeList.IsPass = specialSchemeList.IsPass;
                db.SubmitChanges();
            }
        }

        /// <summary>
        ///根据主键删除专项方案
        /// </summary>
        /// <param name="specialSchemeId"></param>
        public static void DeleteSpecialSchemeListById(string specialSchemeId)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Technique_SpecialScheme specialSchemeList = db.Technique_SpecialScheme.FirstOrDefault(e => e.SpecialSchemeId == specialSchemeId);
            if (specialSchemeList != null)
            {
                if (!string.IsNullOrEmpty(specialSchemeList.AttachUrl))
                {
                    BLL.UploadFileService.DeleteFile(Funs.RootPath, specialSchemeList.AttachUrl);
                }

                ////删除附件表
                BLL.CommonService.DeleteAttachFileById(specialSchemeList.SpecialSchemeId);

                db.Technique_SpecialScheme.DeleteOnSubmit(specialSchemeList);
                db.SubmitChanges();
            }
        }
        /// <summary>
        ///  外键判断
        /// </summary>
        /// <param name="SpecialSchemeTypeId"></param>
        /// <returns></returns>
        public static bool IsDelteBySpecialSchemeTypeId(string SpecialSchemeTypeId)
        {
            List<Model.Technique_SpecialScheme> type = (from x in new Model.SGGLDB(Funs.ConnString).Technique_SpecialScheme where x.SpecialSchemeTypeId == SpecialSchemeTypeId select x).ToList();
            if (type.Count() > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
