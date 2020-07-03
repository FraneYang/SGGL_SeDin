using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 安全生产规章制度表
    /// </summary>
    public static class RulesRegulationsService
    {
        public static Model.SGGLDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取生产规章制度
        /// </summary>
        /// <param name="rulesRegulationsId"></param>
        /// <returns></returns>
        public static Model.Law_RulesRegulations GetRulesRegulationsById(string rulesRegulationsId)
        {
            return Funs.DB.Law_RulesRegulations.FirstOrDefault(e => e.RulesRegulationsId == rulesRegulationsId);
        }

        /// <summary>
        /// 根据编制人获取生产规章制度
        /// </summary>
        /// <param name="compileMan"></param>
        /// <returns></returns>
        public static List<Model.Law_RulesRegulations> GetRulesRegulationByCompileMan(string compileMan)
        {
            return (from x in Funs.DB.Law_RulesRegulations where x.CompileMan == compileMan select x).ToList();
        }

        /// <summary>
        /// 添加生产规章制度
        /// </summary>
        /// <param name="rulesRegulations"></param>
        public static void AddRulesRegulations(Model.Law_RulesRegulations rulesRegulations)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Law_RulesRegulations newRuleRegulations = new Model.Law_RulesRegulations
            {
                RulesRegulationsId = rulesRegulations.RulesRegulationsId,
                RulesRegulationsCode = rulesRegulations.RulesRegulationsCode,
                RulesRegulationsName = rulesRegulations.RulesRegulationsName,
                RulesRegulationsTypeId = rulesRegulations.RulesRegulationsTypeId,
                CustomDate = rulesRegulations.CustomDate,
                ApplicableScope = rulesRegulations.ApplicableScope,
                Remark = rulesRegulations.Remark,
                AttachUrl = rulesRegulations.AttachUrl,
                IsPass = rulesRegulations.IsPass,
                CompileMan = rulesRegulations.CompileMan,
                CompileDate = rulesRegulations.CompileDate,
                UnitId = rulesRegulations.UnitId,
                UpState = rulesRegulations.UpState
            };
            db.Law_RulesRegulations.InsertOnSubmit(newRuleRegulations);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改规章制度
        /// </summary>
        /// <param name="rulesRegulations"></param>
        public static void UpdateRulesRegulations(Model.Law_RulesRegulations rulesRegulations)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Law_RulesRegulations newRuleRegulations = db.Law_RulesRegulations.FirstOrDefault(e => e.RulesRegulationsId == rulesRegulations.RulesRegulationsId);
            if (newRuleRegulations != null)
            {
                newRuleRegulations.RulesRegulationsCode = rulesRegulations.RulesRegulationsCode;
                newRuleRegulations.RulesRegulationsName = rulesRegulations.RulesRegulationsName;
                newRuleRegulations.RulesRegulationsTypeId = rulesRegulations.RulesRegulationsTypeId;
                newRuleRegulations.CustomDate = rulesRegulations.CustomDate;
                newRuleRegulations.ApplicableScope = rulesRegulations.ApplicableScope;
                newRuleRegulations.Remark = rulesRegulations.Remark;
                newRuleRegulations.AttachUrl = rulesRegulations.AttachUrl;
                newRuleRegulations.UpState = rulesRegulations.UpState;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 修改规章制度 是否采用
        /// </summary>
        /// <param name="rulesRegulations"></param>
        public static void UpdateRulesRegulationsIsPass(Model.Law_RulesRegulations rulesRegulations)
        { 
         Model.SGGLDB db = Funs.DB;
            Model.Law_RulesRegulations newRuleRegulations = db.Law_RulesRegulations.FirstOrDefault(e => e.RulesRegulationsId == rulesRegulations.RulesRegulationsId);
            if (newRuleRegulations != null)
            {
                newRuleRegulations.AuditMan = rulesRegulations.AuditMan;
                newRuleRegulations.AuditDate = rulesRegulations.AuditDate;
                newRuleRegulations.IsPass = rulesRegulations.IsPass;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除规章制度
        /// </summary>
        /// <param name="ruleRegulationsId"></param>
        public static void DeleteRuleRegulationsById(string ruleRegulationsId)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Law_RulesRegulations rulesRegulations = db.Law_RulesRegulations.FirstOrDefault(e => e.RulesRegulationsId == ruleRegulationsId);
            if (rulesRegulations != null)
            {
                if (!string.IsNullOrEmpty(rulesRegulations.AttachUrl))
                {
                    BLL.UploadFileService.DeleteFile(Funs.RootPath, rulesRegulations.AttachUrl);//删除附件
                }
                ////删除附件表
                BLL.CommonService.DeleteAttachFileById(rulesRegulations.RulesRegulationsId);

                db.Law_RulesRegulations.DeleteOnSubmit(rulesRegulations);
                db.SubmitChanges();
            }
        }
    }
}
