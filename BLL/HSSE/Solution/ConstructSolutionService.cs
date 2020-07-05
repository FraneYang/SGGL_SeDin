using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 施工方案/审查    
    /// </summary>
    public static class ConstructSolutionService
    {
        public static Model.SGGLDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取施工方案/审查
        /// </summary>
        /// <param name="constructSolutionId"></param>
        /// <returns></returns>
        public static Model.Solution_ConstructSolution GetConstructSolutionById(string constructSolutionId)
        {
            return Funs.DB.Solution_ConstructSolution.FirstOrDefault(e => e.ConstructSolutionId == constructSolutionId);
        }

        /// <summary>
        /// 获取时间段的方案审查
        /// </summary>
        /// <param name="p"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public static List<Model.Solution_ConstructSolution> GetConstructSolutionListByDate(string p, DateTime startTime, DateTime endTime)
        {
            return (from x in Funs.DB.Solution_ConstructSolution where x.ProjectId == p && x.CompileDate >= startTime && x.CompileDate <= endTime select x).ToList();
        }

        /// <summary>
        /// 获取时间段的施工方案审查数量
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public static int GetConstructSolutionCountByDate(string projectId, DateTime startTime, DateTime endTime)
        {
            var q = (from x in Funs.DB.Solution_ConstructSolution where x.ProjectId == projectId && x.CompileDate >= startTime && x.CompileDate <= endTime orderby x.CompileDate select x).ToList();
            return q.Count();
        }

        /// <summary>
        /// 添加施工方案/审查
        /// </summary>
        /// <param name="constructSolution"></param>
        public static void AddConstructSolution(Model.Solution_ConstructSolution constructSolution)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Solution_ConstructSolution newConstructSolution = new Model.Solution_ConstructSolution
            {
                ConstructSolutionId = constructSolution.ConstructSolutionId,
                ProjectId = constructSolution.ProjectId,
                ConstructSolutionCode = constructSolution.ConstructSolutionCode,
                ConstructSolutionName = constructSolution.ConstructSolutionName,
                VersionNo = constructSolution.VersionNo,
                UnitId = constructSolution.UnitId,
                InvestigateType = constructSolution.InvestigateType,
                SolutinType = constructSolution.SolutinType,
                FileContents = constructSolution.FileContents,
                Remark = constructSolution.Remark,
                CompileMan = constructSolution.CompileMan,
                CompileManName = constructSolution.CompileManName,
                CompileDate = constructSolution.CompileDate,
                States = constructSolution.States
            };
            db.Solution_ConstructSolution.InsertOnSubmit(newConstructSolution);
            db.SubmitChanges();
            if (constructSolution.ConstructSolutionCode == BLL.CodeRecordsService.ReturnCodeByMenuIdProjectId(Const.ProjectConstructSolutionMenuId, constructSolution.ProjectId, null))
            {
                CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(Const.ProjectConstructSolutionMenuId, constructSolution.ProjectId, null, constructSolution.ConstructSolutionId, constructSolution.CompileDate);
            }
        }

        /// <summary>
        /// 修改施工方案/审查
        /// </summary>
        /// <param name="constructSolution"></param>
        public static void UpdateConstructSolution(Model.Solution_ConstructSolution constructSolution)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Solution_ConstructSolution newConstructSolution = db.Solution_ConstructSolution.FirstOrDefault(e => e.ConstructSolutionId == constructSolution.ConstructSolutionId);
            if (newConstructSolution != null)
            {                
                newConstructSolution.ConstructSolutionName = constructSolution.ConstructSolutionName;
                newConstructSolution.VersionNo = constructSolution.VersionNo;
                newConstructSolution.UnitId = constructSolution.UnitId;
                newConstructSolution.InvestigateType = constructSolution.InvestigateType;
                newConstructSolution.SolutinType = constructSolution.SolutinType;
                newConstructSolution.FileContents = constructSolution.FileContents;
                newConstructSolution.Remark = constructSolution.Remark;            
                newConstructSolution.States = constructSolution.States;
                if(!string.IsNullOrEmpty(constructSolution.QRCodeAttachUrl))
                {
                    newConstructSolution.QRCodeAttachUrl = constructSolution.QRCodeAttachUrl;
                }
                
                db.SubmitChanges();
                if (constructSolution.ConstructSolutionCode != CodeRecordsService.ReturnCodeByDataId(constructSolution.ConstructSolutionId))
                {
                    CodeRecordsService.DeleteCodeRecordsByDataId(constructSolution.ConstructSolutionId);//删除编号
                }
            }
        }

        /// <summary>
        /// 根据主键删除施工方案/审查
        /// </summary>
        /// <param name="constructSolutionId"></param>
        public static void DeleteConstructSolutionById(string constructSolutionId)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Solution_ConstructSolution constructSolution = db.Solution_ConstructSolution.FirstOrDefault(e => e.ConstructSolutionId == constructSolutionId);
            if (constructSolution != null)
            {
                ////删除审核流程表
                CommonService.DeleteFlowOperateByID(constructSolutionId);
                CodeRecordsService.DeleteCodeRecordsByDataId(constructSolutionId);//删除编号
                CommonService.DeleteAttachFileById(constructSolutionId);//删除附件
              
                db.Solution_ConstructSolution.DeleteOnSubmit(constructSolution);
                db.SubmitChanges();
            }
        }
    }
}