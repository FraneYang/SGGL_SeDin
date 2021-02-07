using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 管理规定
    /// </summary>
    public static class ManageRuleService
    {
        public static Model.SGGLDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取管理规定
        /// </summary>
        /// <param name="manageRuleId"></param>
        /// <returns></returns>
        public static Model.Law_ManageRule GetManageRuleById(string manageRuleId)
        {
            return Funs.DB.Law_ManageRule.FirstOrDefault(e => e.ManageRuleId == manageRuleId);
        }

        /// <summary>
        /// 根据整理人获取管理规定
        /// </summary>
        /// <param name="compileMan"></param>
        /// <returns></returns>
        public static List<Model.Law_ManageRule> GetManageRuleByCompileMan(string compileMan)
        {
            return (from x in Funs.DB.Law_ManageRule where x.CompileMan == compileMan select x).ToList();
        }

        /// <summary>
        /// 添加管理规定
        /// </summary>
        /// <param name="manageRule"></param>
        public static void AddManageRule(Model.Law_ManageRule manageRule)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Law_ManageRule newManageRule = new Model.Law_ManageRule
            {
                ManageRuleId = manageRule.ManageRuleId,
                ManageRuleCode = manageRule.ManageRuleCode,
                ManageRuleName = manageRule.ManageRuleName,
                ManageRuleTypeId = manageRule.ManageRuleTypeId,
                VersionNo = manageRule.VersionNo,
                AttachUrl = manageRule.AttachUrl,
                Remark = manageRule.Remark,
                CompileMan = manageRule.CompileMan,
                CompileDate = manageRule.CompileDate,
                IsPass = manageRule.IsPass,
                UnitId = manageRule.UnitId,
                UpState = manageRule.UpState,
                SeeFile = manageRule.SeeFile,

                ApprovalDate = manageRule.ApprovalDate,
                EffectiveDate = manageRule.EffectiveDate,
                Description = manageRule.Description,
                ReleaseStates = manageRule.ReleaseStates,
                ReleaseUnit = manageRule.ReleaseUnit,
                AbolitionDate = manageRule.AbolitionDate,
                ReplaceInfo = manageRule.ReplaceInfo,
                IndexesIds = manageRule.IndexesIds,
            };
            db.Law_ManageRule.InsertOnSubmit(newManageRule);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改管理规定
        /// </summary>
        /// <param name="manageRule"></param>
        public static void UpdateManageRule(Model.Law_ManageRule manageRule)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Law_ManageRule newManageRule = db.Law_ManageRule.FirstOrDefault(e => e.ManageRuleId == manageRule.ManageRuleId);
            if (newManageRule != null)
            {
                newManageRule.ManageRuleCode = manageRule.ManageRuleCode;
                newManageRule.ManageRuleName = manageRule.ManageRuleName;
                newManageRule.ManageRuleTypeId = manageRule.ManageRuleTypeId;
                newManageRule.VersionNo = manageRule.VersionNo;
                //newManageRule.CompileMan = manageRule.CompileMan;
                //newManageRule.CompileDate = manageRule.CompileDate;
                newManageRule.AttachUrl = manageRule.AttachUrl;
                newManageRule.Remark = manageRule.Remark;
                newManageRule.UpState = manageRule.UpState;
                newManageRule.SeeFile = manageRule.SeeFile;

                newManageRule.ApprovalDate = manageRule.ApprovalDate;
                newManageRule.EffectiveDate = manageRule.EffectiveDate;
                newManageRule.Description = manageRule.Description;
                newManageRule.ReleaseStates = manageRule.ReleaseStates;
                newManageRule.ReleaseUnit = manageRule.ReleaseUnit;
                newManageRule.AbolitionDate = manageRule.AbolitionDate;
                newManageRule.ReplaceInfo = manageRule.ReplaceInfo;
                newManageRule.IndexesIds = manageRule.IndexesIds;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 修改管理规定   是否采用
        /// </summary>
        /// <param name="manageRule"></param>
        public static void UpdateManageRuleIsPass(Model.Law_ManageRule manageRule)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Law_ManageRule newManageRule = db.Law_ManageRule.FirstOrDefault(e => e.ManageRuleId == manageRule.ManageRuleId);
            if (newManageRule!=null)
            {
                newManageRule.IsPass = manageRule.IsPass;
                newManageRule.AuditMan = manageRule.AuditMan;
                newManageRule.AuditDate = manageRule.AuditDate;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除管理规定
        /// </summary>
        /// <param name="manageRuleId"></param>
        public static void DeleteManageRuleById(string manageRuleId)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Law_ManageRule manageRule = db.Law_ManageRule.FirstOrDefault(e => e.ManageRuleId == manageRuleId);
            if (manageRule != null)
            {
                //if (!string.IsNullOrEmpty(manageRule.AttachUrl))
                //{
                //    BLL.UploadFileService.DeleteFile(Funs.RootPath, manageRule.AttachUrl);
                //}

                ////删除附件表
                //BLL.CommonService.DeleteAttachFileById(manageRule.ManageRuleId);

                db.Law_ManageRule.DeleteOnSubmit(manageRule);
                db.SubmitChanges();
            }
        }
    }
}
