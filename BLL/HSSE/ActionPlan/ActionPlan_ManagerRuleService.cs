using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 管理规定发布
    /// </summary>
    public static class ActionPlan_ManagerRuleService
    {
        public static Model.SGGLDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取管理规定
        /// </summary>
        /// <param name="manageRuleId"></param>
        /// <returns></returns>
        public static Model.ActionPlan_ManagerRule GetManagerRuleById(string managerRuleId)
        {
            return Funs.DB.ActionPlan_ManagerRule.FirstOrDefault(e => e.ManagerRuleId == managerRuleId);
        }

        /// <summary>
        /// 根据名称获取已发布管理规定的集合
        /// </summary>
        /// <param name="managerRuleName"></param>
        /// <returns></returns>
        public static List<Model.ActionPlan_ManagerRule> GetIsIssueManagerRulesByName(string managerRuleName)
        {
            return (from x in Funs.DB.ActionPlan_ManagerRule where x.ManageRuleName == managerRuleName && x.IsIssue == true orderby x.IssueDate select x).ToList();
        }

        /// <summary>
        /// 根据日期获取管理规定集合
        /// </summary>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="projectId">项目号</param>
        /// <returns>管理规定集合</returns>
        public static List<Model.ActionPlan_ManagerRule> GetManagerRuleListsByDate(DateTime startTime, DateTime endTime, string projectId)
        {
            return (from x in Funs.DB.ActionPlan_ManagerRule where x.CompileDate >= startTime && x.CompileDate <= endTime && x.ProjectId == projectId orderby x.CompileDate select x).ToList();
        }

        /// <summary>
        /// 根据整理人获取管理规定
        /// </summary>
        /// <param name="compileMan"></param>
        /// <returns></returns>
        public static List<Model.ActionPlan_ManagerRule> GetManageRuleByCompileMan(string compileMan)
        {
            return (from x in Funs.DB.ActionPlan_ManagerRule where x.CompileMan == compileMan select x).ToList();
        }

        /// <summary>
        /// 添加管理规定
        /// </summary>
        /// <param name="manageRule"></param>
        public static void AddManageRule(Model.ActionPlan_ManagerRule manageRule)
        {
            Model.SGGLDB db = Funs.DB;
            Model.ActionPlan_ManagerRule newManageRule = new Model.ActionPlan_ManagerRule
            {
                ManagerRuleId = manageRule.ManagerRuleId,
                ManageRuleCode = manageRule.ManageRuleCode,
                OldManageRuleId = manageRule.OldManageRuleId,
                ProjectId = manageRule.ProjectId,
                ManageRuleName = manageRule.ManageRuleName,
                ManageRuleTypeId = manageRule.ManageRuleTypeId,
                VersionNo = manageRule.VersionNo,
                AttachUrl = manageRule.AttachUrl,
                Remark = manageRule.Remark,
                CompileMan = manageRule.CompileMan,
                CompileDate = manageRule.CompileDate,
                Flag = manageRule.Flag,
                State = manageRule.State,
                SeeFile = manageRule.SeeFile
            };
            db.ActionPlan_ManagerRule.InsertOnSubmit(newManageRule);
            db.SubmitChanges();

            ////增加一条编码记录
            BLL.CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(BLL.Const.ActionPlan_ManagerRuleMenuId, manageRule.ProjectId, null, manageRule.ManagerRuleId, manageRule.CompileDate);
        }

        /// <summary>
        /// 修改管理规定
        /// </summary>
        /// <param name="manageRule"></param>
        public static void UpdateManageRule(Model.ActionPlan_ManagerRule manageRule)
        {
            Model.SGGLDB db = Funs.DB;
            Model.ActionPlan_ManagerRule newManageRule = db.ActionPlan_ManagerRule.FirstOrDefault(e => e.ManagerRuleId == manageRule.ManagerRuleId);
            if (newManageRule != null)
            {
                newManageRule.ManageRuleName = manageRule.ManageRuleName;
                newManageRule.ManageRuleTypeId = manageRule.ManageRuleTypeId;
                newManageRule.VersionNo = manageRule.VersionNo;
                newManageRule.AttachUrl = manageRule.AttachUrl;
                newManageRule.Remark = manageRule.Remark;
                newManageRule.CompileMan = manageRule.CompileMan;
                newManageRule.CompileDate = manageRule.CompileDate;
                newManageRule.IsIssue = manageRule.IsIssue;
                newManageRule.IssueDate = manageRule.IssueDate;
                newManageRule.Flag = manageRule.Flag;
                newManageRule.State = manageRule.State;
                newManageRule.SeeFile = manageRule.SeeFile;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除管理规定
        /// </summary>
        /// <param name="manageRuleId"></param>
        public static void DeleteManageRuleById(string managerRuleId)
        {
            Model.SGGLDB db = Funs.DB;
            Model.ActionPlan_ManagerRule manageRule = db.ActionPlan_ManagerRule.FirstOrDefault(e => e.ManagerRuleId == managerRuleId);
            if (manageRule != null)
            {
                if (!string.IsNullOrEmpty(manageRule.AttachUrl))
                {
                    BLL.UploadFileService.DeleteFile(Funs.RootPath, manageRule.AttachUrl);
                }
                ///删除编码表记录
                CodeRecordsService.DeleteCodeRecordsByDataId(managerRuleId);
                ////删除附件表
                CommonService.DeleteAttachFileById(manageRule.ManagerRuleId);                
                ////删除审核流程表
                CommonService.DeleteFlowOperateByID(manageRule.ManagerRuleId);
                db.ActionPlan_ManagerRule.DeleteOnSubmit(manageRule);
                db.SubmitChanges();
            }           
        }
    }
}
