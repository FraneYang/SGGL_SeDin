using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 方案模板
    /// </summary>
    public static class SolutionTemplateService
    {
        public static Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);

        /// <summary>
        /// 根据主键获取方案模板
        /// </summary>
        /// <param name="solutionTemplateId"></param>
        /// <returns></returns>
        public static Model.Solution_SolutionTemplate GetSolutionTemplateById(string solutionTemplateId)
        {
            return new Model.SGGLDB(Funs.ConnString).Solution_SolutionTemplate.FirstOrDefault(e => e.SolutionTemplateId == solutionTemplateId);
        }

        /// <summary>
        /// 根据方案类别、项目id获取模板信息
        /// </summary>
        /// <param name="solutionTemplateType"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public static Model.Solution_SolutionTemplate GetSolutionTemplateBySolutionTemplateType(string solutionTemplateType, string projectId)
        {
            return new Model.SGGLDB(Funs.ConnString).Solution_SolutionTemplate.FirstOrDefault(e => e.SolutionTemplateType == solutionTemplateType && e.ProjectId == projectId);
        }

        /// <summary>
        /// 添加方案模板
        /// </summary>
        /// <param name="solutionTemplate"></param>
        public static void AddSolutionTemplate(Model.Solution_SolutionTemplate solutionTemplate)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Solution_SolutionTemplate newSolutionTemplate = new Model.Solution_SolutionTemplate
            {
                SolutionTemplateId = solutionTemplate.SolutionTemplateId,
                ProjectId = solutionTemplate.ProjectId,
                SolutionTemplateCode = solutionTemplate.SolutionTemplateCode,
                SolutionTemplateName = solutionTemplate.SolutionTemplateName,
                SolutionTemplateType = solutionTemplate.SolutionTemplateType,
                FileContents = solutionTemplate.FileContents,
                CompileMan = solutionTemplate.CompileMan,
                CompileDate = solutionTemplate.CompileDate
            };
            db.Solution_SolutionTemplate.InsertOnSubmit(newSolutionTemplate);
            db.SubmitChanges();
            //暂时不取统一编号，自己手动编号
            //CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(Const.SolutionTemplateMenuId, newSolutionTemplate.ProjectId, null, solutionTemplate.SolutionTemplateId, solutionTemplate.CompileDate);
        }

        /// <summary>
        /// 修改方案模板
        /// </summary>
        /// <param name="solutionTemplate"></param>
        public static void UpdateSolutionTemplate(Model.Solution_SolutionTemplate solutionTemplate)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Solution_SolutionTemplate newSolutionTemplate = db.Solution_SolutionTemplate.FirstOrDefault(e => e.SolutionTemplateId == solutionTemplate.SolutionTemplateId);
            if (newSolutionTemplate != null)
            {
                newSolutionTemplate.SolutionTemplateCode = solutionTemplate.SolutionTemplateCode;
                newSolutionTemplate.SolutionTemplateName = solutionTemplate.SolutionTemplateName;
                newSolutionTemplate.SolutionTemplateType = solutionTemplate.SolutionTemplateType;
                newSolutionTemplate.CompileMan = solutionTemplate.CompileMan;
                newSolutionTemplate.CompileDate = solutionTemplate.CompileDate;
                newSolutionTemplate.FileContents = solutionTemplate.FileContents;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 删除所有方案模板
        /// </summary>
        /// <param name="solutionTemplateId"></param>
        public static void DeleteSolutionTemplateById(string solutionTemplateId)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Solution_SolutionTemplate solutionTemplate = db.Solution_SolutionTemplate.FirstOrDefault(e => e.SolutionTemplateId == solutionTemplateId);
            if (solutionTemplate != null)
            {
                CodeRecordsService.DeleteCodeRecordsByDataId(solutionTemplateId);//删除编号
                CommonService.DeleteAttachFileById(solutionTemplateId);//删除附件
                CommonService.DeleteFlowOperateByID(solutionTemplateId);//删除流程
                db.Solution_SolutionTemplate.DeleteOnSubmit(solutionTemplate);
                db.SubmitChanges();
            }
        }
    }
}