using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 单据代码记录表
    /// </summary>
    public class CodeRecordsService
    {
        /// <summary>
        /// 根据主键读取编码
        /// </summary>
        /// <param name="menuId"></param>
        /// <param name="projectId"></param>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public static string ReturnCodeByDataId(string dataId)
        {
            string code = string.Empty;
            var codeRecords = new Model.SGGLDB(Funs.ConnString).Sys_CodeRecords.FirstOrDefault(x => x.DataId == dataId);
            if (codeRecords != null)
            {
                code = codeRecords.Code;
            }

            return code;
        }

        /// <summary>
        /// 根据主键删除编码表记录
        /// </summary>
        /// <param name="dataId"></param>
        /// <returns></returns>
        public static void DeleteCodeRecordsByDataId(string dataId)
        {
            var codeRecords = new Model.SGGLDB(Funs.ConnString).Sys_CodeRecords.FirstOrDefault(x => x.DataId == dataId);
            if (codeRecords != null)
            {
                ///删除文件柜A中数据
               // BLL.FileCabinetAItemService.DeleteFileCabinetAItemByID(dataId);
                new Model.SGGLDB(Funs.ConnString).Sys_CodeRecords.DeleteOnSubmit(codeRecords);
            }
        }
        
        #region 根据菜单id、项目id返回编码 (用于页面新增显示)
        /// <summary>
        /// 根据菜单id、项目id返回编码 (用于页面新增显示)
        /// </summary>
        /// <param name="menuId"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public static string ReturnCodeByMenuIdProjectId(string menuId, string projectId, string unitId)
        {
            string code = string.Empty;
            string ruleCodes = string.Empty;
            int digit = 4;
            string symbol = "-"; ///间隔符 
            var codeRecords = (from x in new Model.SGGLDB(Funs.ConnString).Sys_CodeRecords where x.MenuId == menuId && x.ProjectId == projectId orderby x.CompileDate descending select x).FirstOrDefault();
            if (codeRecords != null && !string.IsNullOrEmpty(codeRecords.RuleCodes))
            {
                ruleCodes = codeRecords.RuleCodes;
                if (codeRecords.Digit.HasValue)
                {
                    digit = codeRecords.Digit.Value;
                }
            }
            else
            {
                ////项目
                string ruleCode = string.Empty;
                var codeTempRule = new Model.SGGLDB(Funs.ConnString).Sys_CodeTemplateRule.FirstOrDefault(x => x.MenuId == menuId);
                if (codeTempRule != null && !string.IsNullOrEmpty(codeTempRule.Prefix))
                {
                    if (!string.IsNullOrEmpty(codeTempRule.Symbol))
                    {
                        symbol = codeTempRule.Symbol;
                    }
                    ruleCodes = codeTempRule.Prefix + symbol;
                    if (codeTempRule.Digit.HasValue)
                    {
                        digit = codeTempRule.Digit.Value;
                    }
                }
            }

            ////获取编码记录表最大排列序号              
            int maxNewSortIndex = 0;
            var maxSortIndex = new Model.SGGLDB(Funs.ConnString).Sys_CodeRecords.Where(x => (x.ProjectId == projectId || projectId == null) && x.MenuId == menuId).Select(x => x.SortIndex).Max();
            
            if (maxSortIndex.HasValue)
            {
                maxNewSortIndex = maxSortIndex.Value;
            }
            maxNewSortIndex = maxNewSortIndex + 1;
            code = (maxNewSortIndex.ToString().PadLeft(digit, '0'));   ///字符自动补零
            if (!string.IsNullOrEmpty(ruleCodes))
            {
                code = ruleCodes + code;
            }

            return code;
        }
        #endregion

        #region 根据菜单id、项目id插入一条编码记录（数据新增到数据库 生成编码）
        /// <summary>
        /// 根据菜单id、项目id插入一条编码记录（数据新增到数据库 生成编码）
        /// </summary>
        /// <param name="menuId"></param>
        /// <param name="projectId"></param>
        /// <param name="unitId"></param>
        public static void InsertCodeRecordsByMenuIdProjectIdUnitId(string menuId, string projectId, string unitId, string dataId, DateTime? compileDate)
        {
            var IsHaveCodeRecords = new Model.SGGLDB(Funs.ConnString).Sys_CodeRecords.FirstOrDefault(x => x.DataId == dataId);
            if (IsHaveCodeRecords == null)  ///是否已存在编码
            {
                string ruleCode = string.Empty;
                string ruleCodeower = string.Empty;
                int digit = 4; ///流水位数
                string symbolower = "-"; ///业主间隔符
                int digitower = 4; ///业主流水位数
                string symbol = "-"; ///间隔符
                var project = BLL.ProjectService.GetProjectByProjectId(projectId); ////项目
                if (project != null && !string.IsNullOrEmpty(dataId))
                {
                    string projectCode = project.ProjectCode; ///项目编号               
                    //////编码规则表
                    //var sysCodeTemplateRule = BLL.ProjectData_CodeTemplateRuleService.GetProjectData_CodeTemplateRuleByMenuIdProjectId(menuId, projectId);
                    //if (sysCodeTemplateRule != null)
                    //{
                    //    symbol = sysCodeTemplateRule.Symbol;
                    //    symbolower = sysCodeTemplateRule.OwerSymbol;

                    //    if (sysCodeTemplateRule.Digit.HasValue)
                    //    {
                    //        digit = sysCodeTemplateRule.Digit.Value;
                    //    }
                    //    if (sysCodeTemplateRule.OwerDigit.HasValue)
                    //    {
                    //        digitower = sysCodeTemplateRule.OwerDigit.Value;
                    //    }
                    //    if (sysCodeTemplateRule.IsProjectCode == true)
                    //    {
                    //        ruleCode = projectCode + symbol;
                    //    }
                    //    if (sysCodeTemplateRule.OwerIsProjectCode == true)
                    //    {
                    //        ruleCodeower = projectCode + symbolower;
                    //    }
                    //    if (!string.IsNullOrEmpty(sysCodeTemplateRule.Prefix))
                    //    {
                    //        ruleCode += sysCodeTemplateRule.Prefix + symbol;
                    //    }
                    //    if (!string.IsNullOrEmpty(sysCodeTemplateRule.OwerPrefix))
                    //    {
                    //        ruleCodeower += sysCodeTemplateRule.OwerPrefix + symbolower;
                    //    }
                    //    if (sysCodeTemplateRule.IsUnitCode == true || sysCodeTemplateRule.OwerIsUnitCode == true)
                    //    {
                    //        var unit = BLL.UnitService.GetUnitByUnitId(unitId);
                    //        if (unit != null)
                    //        {
                    //            if (sysCodeTemplateRule.IsUnitCode == true)
                    //            { ruleCode = unit.UnitCode + symbol; }

                    //            if (sysCodeTemplateRule.OwerIsUnitCode == true)
                    //            {
                    //                ruleCodeower = unit.UnitCode + symbolower;
                    //            }
                    //        }
                    //    }
                    //}
                }

                ////获取编码记录表最大排列序号              
                int maxNewSortIndex = 0;
                if (!String.IsNullOrEmpty(projectId))
                {
                    var maxSortIndex = new Model.SGGLDB(Funs.ConnString).Sys_CodeRecords.Where(x => x.ProjectId == projectId && x.MenuId == menuId).Select(x => x.SortIndex).Max();
                    if (maxSortIndex.HasValue)
                    {
                        maxNewSortIndex = maxSortIndex.Value;
                    }
                }
                else
                {
                    var maxSortIndexNull = new Model.SGGLDB(Funs.ConnString).Sys_CodeRecords.Where(x => x.MenuId == menuId).Select(x => x.SortIndex).Max();
                    if (maxSortIndexNull.HasValue)
                    {
                        maxNewSortIndex = maxSortIndexNull.Value;
                    }
                }
                maxNewSortIndex = maxNewSortIndex + 1;
                ////插入数据库
                Model.Sys_CodeRecords newCodeRecords = new Model.Sys_CodeRecords
                {
                    CodeRecordId = SQLHelper.GetNewID(typeof(Model.Sys_CodeRecords))
                };
                if (project != null)
                {
                    newCodeRecords.ProjectId = project.ProjectId;
                }
                newCodeRecords.MenuId = menuId;
                newCodeRecords.DataId = dataId;
                newCodeRecords.UnitId = unitId;
                newCodeRecords.SortIndex = maxNewSortIndex;
                newCodeRecords.CompileDate = compileDate;
                newCodeRecords.RuleCodes = ruleCode;
                newCodeRecords.Digit = digit;
                newCodeRecords.OwnerRuleCodes = ruleCodeower;
                newCodeRecords.OwerDigit = digitower;
                if (!string.IsNullOrEmpty(ruleCode))
                {
                    newCodeRecords.Code = ruleCode + (maxNewSortIndex.ToString().PadLeft(digit, '0'));   ///字符自动补零   编码
                }
                else
                {
                    newCodeRecords.Code = (maxNewSortIndex.ToString().PadLeft(digit, '0'));
                }
                if (!string.IsNullOrEmpty(ruleCodeower))
                {
                    newCodeRecords.OwnerCode = ruleCodeower + (maxNewSortIndex.ToString().PadLeft(digitower, '0'));   ///字符自动补零  业主编码
                }
                else
                {
                    newCodeRecords.OwnerCode = (maxNewSortIndex.ToString().PadLeft(digitower, '0'));
                }
                new Model.SGGLDB(Funs.ConnString).Sys_CodeRecords.InsertOnSubmit(newCodeRecords);
                new Model.SGGLDB(Funs.ConnString).SubmitChanges();
            }
        }
        #endregion

        #region 根据菜单id,项目id 更新编码记录（当编码规则改变的时候 更新编码）
        /// <summary>
        /// 根据菜单id 更新编码记录（当编码规则改变的时候 更新编码）
        /// </summary>
        /// <param name="menuId"></param>
        /// <param name="projectId"></param>
        /// <param name="unitId"></param>
        public static void UpdateCodeRecordsByMenuIdProjectId(string menuId, string projectId)
        {
            var codeRecords = from x in new Model.SGGLDB(Funs.ConnString).Sys_CodeRecords where x.MenuId == menuId && x.ProjectId == projectId select x;
            if (codeRecords.Count() > 0)
            {
                foreach (var itemRecords in codeRecords)
                {
                    string ruleCode = string.Empty;
                    string ruleCodeower = string.Empty;
                    string symbol = "-"; ///间隔符
                    int digit = 4; ///流水位数
                    string symbolower = "-"; ///业主间隔符
                    int digitower = 4; ///业主流水位数
                    var project = BLL.ProjectService.GetProjectByProjectId(projectId);
                    if (project != null)
                    {
                        string projectCode = project.ProjectCode; ///项目编号  
                        //var sysCodeTemplateRule = BLL.ProjectData_CodeTemplateRuleService.GetProjectData_CodeTemplateRuleByMenuIdProjectId(menuId, projectId);////编码规则表
                        //if (sysCodeTemplateRule != null)
                        //{
                        //    symbol = sysCodeTemplateRule.Symbol;
                        //    symbolower = sysCodeTemplateRule.OwerSymbol;
                        //    if (sysCodeTemplateRule.Digit.HasValue)
                        //    {
                        //        digit = sysCodeTemplateRule.Digit.Value;
                        //    }
                        //    if (sysCodeTemplateRule.OwerDigit.HasValue)
                        //    {
                        //        digitower = sysCodeTemplateRule.OwerDigit.Value;
                        //    }
                        //    if (sysCodeTemplateRule.IsProjectCode == true)
                        //    {
                        //        ruleCode = projectCode + symbol;
                        //    }
                        //    if (sysCodeTemplateRule.OwerIsProjectCode == true)
                        //    {
                        //        ruleCodeower = projectCode + symbolower;
                        //    }
                        //    if (!string.IsNullOrEmpty(sysCodeTemplateRule.Prefix))
                        //    {
                        //        ruleCode += sysCodeTemplateRule.Prefix + symbol;
                        //    }
                        //    if (!string.IsNullOrEmpty(sysCodeTemplateRule.OwerPrefix))
                        //    {
                        //        ruleCodeower += sysCodeTemplateRule.OwerPrefix + symbolower;
                        //    }
                        //    if (sysCodeTemplateRule.IsUnitCode == true || sysCodeTemplateRule.OwerIsUnitCode == true)
                        //    {
                        //        var unit = BLL.UnitService.GetUnitByUnitId(itemRecords.UnitId);
                        //        if (unit != null)
                        //        {
                        //            if (sysCodeTemplateRule.IsUnitCode == true)
                        //            {
                        //                if (sysCodeTemplateRule.IsProjectCode == true)
                        //                {
                        //                    ruleCode = projectCode + symbol + unit.UnitCode + symbol;
                        //                }
                        //                else
                        //                {
                        //                    ruleCode = unit.UnitCode + symbol;
                        //                }
                        //            }

                        //            if (sysCodeTemplateRule.OwerIsUnitCode == true)
                        //            { 
                        //                if (sysCodeTemplateRule.OwerIsProjectCode == true)
                        //                {
                        //                    ruleCode = projectCode + symbolower + unit.UnitCode + symbolower;
                        //                }
                        //                else
                        //                {
                        //                    ruleCode = unit.UnitCode + symbolower;
                        //                }  
                        //            }
                        //        }
                        //    }
                        //}
                    }

                    if (ruleCode != itemRecords.RuleCodes || ruleCodeower != itemRecords.OwnerRuleCodes)
                    {
                        itemRecords.RuleCodes = ruleCode;
                        if (!string.IsNullOrEmpty(ruleCode))
                        {
                            itemRecords.Code = ruleCode + ((itemRecords.SortIndex).ToString().PadLeft(digit, '0'));   ///字符自动补零   编码
                        }
                        else
                        {
                            itemRecords.Code = ((itemRecords.SortIndex).ToString().PadLeft(digit, '0'));
                        }
                        itemRecords.OwnerRuleCodes = ruleCodeower;
                        if (!string.IsNullOrEmpty(ruleCodeower))
                        {
                            itemRecords.OwnerCode = ruleCodeower + ((itemRecords.SortIndex).ToString().PadLeft(digitower, '0'));   ///字符自动补零  业主编码
                        }
                        else
                        {
                            itemRecords.OwnerCode = ((itemRecords.SortIndex).ToString().PadLeft(digitower, '0')); ;
                        }
                        new Model.SGGLDB(Funs.ConnString).SubmitChanges();
                    }
                }
            }
        }
        #endregion

        #region 根据菜单id，项目id 重新排序并生成编码（当编码规则改变的时候 重新排序并生成编码）
        /// <summary>
        /// 根据菜单id 重新排序并生成编码（当编码规则改变的时候 重新排序并生成编码）
        /// </summary>
        /// <param name="menuId"></param>
        /// <param name="projectId"></param>
        /// <param name="unitId"></param>
        public static void DatabaseRefreshCodeRecordsByMenuIdProjectId(string menuId, string projectId)
        {
            var codeRecords = from x in new Model.SGGLDB(Funs.ConnString).Sys_CodeRecords orderby x.CompileDate where x.MenuId == menuId && x.ProjectId == projectId select x;
            if (codeRecords.Count() > 0)
            {
                int sortIndex = 0;
                foreach (var itemCodeRecord in codeRecords)
                {
                    sortIndex++;
                    itemCodeRecord.SortIndex = sortIndex;
                    if (!string.IsNullOrEmpty(itemCodeRecord.RuleCodes))
                    {
                        itemCodeRecord.Code = itemCodeRecord.RuleCodes + ((sortIndex).ToString().PadLeft(itemCodeRecord.Digit ?? 4, '0'));   ///字符自动补零   编码
                    }
                    else
                    {
                        itemCodeRecord.Code = ((sortIndex).ToString().PadLeft(itemCodeRecord.Digit ?? 4, '0'));
                    }
                    if (!string.IsNullOrEmpty(itemCodeRecord.OwnerRuleCodes))
                    {
                        itemCodeRecord.OwnerCode = itemCodeRecord.OwnerRuleCodes + ((sortIndex).ToString().PadLeft(itemCodeRecord.OwerDigit ?? 4, '0'));   ///字符自动补零  业主编码
                    }
                    else
                    {
                        itemCodeRecord.OwnerCode = ((sortIndex).ToString().PadLeft(itemCodeRecord.OwerDigit ?? 4, '0'));
                    }
                    new Model.SGGLDB(Funs.ConnString).SubmitChanges();
                }
            }
        }
        #endregion
    }
}