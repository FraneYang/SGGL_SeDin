namespace BLL
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public static class ProjectData_CodeTemplateRuleService
    {
        #region 项目菜单编码模板
        /// <summary>
        /// 项目获取菜单编码模板信息 根据MenuId
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        public static Model.ProjectData_CodeTemplateRule GetProjectData_CodeTemplateRuleByMenuIdProjectId(string menuId, string projectId)
        {
            return Funs.DB.ProjectData_CodeTemplateRule.FirstOrDefault(x => x.MenuId == menuId && x.ProjectId == projectId);
        }
        
        /// <summary>
        /// 项目菜单编码模板信息
        /// </summary>
        /// <param name="codeTemplateRule"></param>
        public static void AddProjectData_CodeTemplateRule(Model.ProjectData_CodeTemplateRule codeTemplateRule)
        {
            Model.SGGLDB db = Funs.DB;
            Model.ProjectData_CodeTemplateRule newCodeTemplateRule = new Model.ProjectData_CodeTemplateRule
            {
                CodeTemplateRuleId = SQLHelper.GetNewID(typeof(Model.ProjectData_CodeTemplateRule)),
                MenuId = codeTemplateRule.MenuId,
                ProjectId = codeTemplateRule.ProjectId,
                Template = codeTemplateRule.Template,
                Symbol = codeTemplateRule.Symbol,
                IsProjectCode = codeTemplateRule.IsProjectCode,
                Prefix = codeTemplateRule.Prefix,
                IsUnitCode = codeTemplateRule.IsUnitCode,
                Digit = codeTemplateRule.Digit,
                OwerSymbol = codeTemplateRule.OwerSymbol,
                OwerIsProjectCode = codeTemplateRule.OwerIsProjectCode,
                OwerPrefix = codeTemplateRule.OwerPrefix,
                OwerIsUnitCode = codeTemplateRule.OwerIsUnitCode,
                OwerDigit = codeTemplateRule.OwerDigit
            };
            db.ProjectData_CodeTemplateRule.InsertOnSubmit(newCodeTemplateRule);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改项目菜单编码模板信息
        /// </summary>
        /// <param name="codeTemplateRule"></param>
        public static void UpdateProjectData_CodeTemplateRule(Model.ProjectData_CodeTemplateRule codeTemplateRule)
        {
            Model.SGGLDB db = Funs.DB;
            Model.ProjectData_CodeTemplateRule updateCodeTemplateRule = db.ProjectData_CodeTemplateRule.FirstOrDefault(e => e.MenuId == codeTemplateRule.MenuId && e.ProjectId == codeTemplateRule.ProjectId);
            if (updateCodeTemplateRule != null)
            {
                updateCodeTemplateRule.Template = codeTemplateRule.Template;
                updateCodeTemplateRule.Symbol = codeTemplateRule.Symbol;
                updateCodeTemplateRule.IsProjectCode = codeTemplateRule.IsProjectCode;
                updateCodeTemplateRule.Prefix = codeTemplateRule.Prefix;
                updateCodeTemplateRule.IsUnitCode = codeTemplateRule.IsUnitCode;
                updateCodeTemplateRule.Digit = codeTemplateRule.Digit;
                updateCodeTemplateRule.OwerSymbol = codeTemplateRule.OwerSymbol;
                updateCodeTemplateRule.OwerIsProjectCode = codeTemplateRule.OwerIsProjectCode;
                updateCodeTemplateRule.OwerPrefix = codeTemplateRule.OwerPrefix;
                updateCodeTemplateRule.OwerIsUnitCode = codeTemplateRule.OwerIsUnitCode;
                updateCodeTemplateRule.OwerDigit = codeTemplateRule.OwerDigit;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 删除项目菜单编码模板信息
        /// </summary>
        /// <param name="codeTemplateRule"></param>
        public static void DeleteProjectData_CodeTemplateRule(string projectId)
        {
            var deleteCodeTemplateRule = from x in  Funs.DB.ProjectData_CodeTemplateRule where x.ProjectId == projectId select x;
            if (deleteCodeTemplateRule.Count() >0 )
            {
                Funs.DB.ProjectData_CodeTemplateRule.DeleteAllOnSubmit(deleteCodeTemplateRule);

            }
        }

        /// <summary>
        /// 根据项目Id 将编码/模板写入到项目菜单编码模板表中
        /// </summary>
        /// <param name="codeTemplateRule"></param>
        public static void InertProjectData_CodeTemplateRuleByProjectId(string projectId)
        {
            var sysCodeTemplateRule = from x in Funs.DB.Sys_CodeTemplateRule select x;
            if (sysCodeTemplateRule.Count() > 0)
            {
                foreach (var item in sysCodeTemplateRule)
                {
                    Model.ProjectData_CodeTemplateRule newCodeTemplateRule = new Model.ProjectData_CodeTemplateRule
                    {
                        MenuId = item.MenuId,
                        ProjectId = projectId,
                        Template = item.Template,
                        Symbol = item.Symbol,
                        IsProjectCode = item.IsProjectCode,
                        Prefix = item.Prefix,
                        IsUnitCode = item.IsUnitCode,
                        Digit = item.Digit,
                        OwerSymbol = item.Symbol,
                        OwerIsProjectCode = item.IsProjectCode,
                        OwerPrefix = item.Prefix,
                        OwerIsUnitCode = item.IsUnitCode,
                        OwerDigit = item.Digit
                    };
                    AddProjectData_CodeTemplateRule(newCodeTemplateRule);
                }
            }
        }
        #endregion
    }
}
