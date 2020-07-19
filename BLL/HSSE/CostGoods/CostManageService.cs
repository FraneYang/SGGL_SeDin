using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 安全费用管理
    /// </summary>
    public static class CostManageService
    {
        public static Model.SGGLDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取安全费用管理
        /// </summary>
        /// <param name="costManageId"></param>
        /// <returns></returns>
        public static Model.CostGoods_CostManage GetCostManageById(string costManageId)
        {
            return Funs.DB.CostGoods_CostManage.FirstOrDefault(e => e.CostManageId == costManageId);
        }

        /// <summary>
        /// 根据项目Id，单位Id和月份获取安全费用管理
        /// </summary>
        /// <param name="costManageId"></param>
        /// <returns></returns>
        public static Model.CostGoods_CostManage GetCostManageByUnitIdAndDate(string projectId, string unidId, DateTime date)
        {
            return Funs.DB.CostGoods_CostManage.FirstOrDefault(e => e.CostManageId == projectId && e.UnitId == unidId && e.CostManageDate.Value.Year == date.Year && e.CostManageDate.Value.Month == date.Month);
        }

        /// <summary>
        /// 添加安全费用管理
        /// </summary>
        /// <param name="costManage"></param>
        public static void AddCostManage(Model.CostGoods_CostManage costManage)
        {
            Model.SGGLDB db = Funs.DB;
            Model.CostGoods_CostManage newCostManage = new Model.CostGoods_CostManage
            {
                CostManageId = costManage.CostManageId,
                ProjectId = costManage.ProjectId,
                CostManageCode = costManage.CostManageCode,
                CostManageName = costManage.CostManageName,
                UnitId = costManage.UnitId,
                ContractNum = costManage.ContractNum,
                CostManageDate = costManage.CostManageDate,
                Opinion = costManage.Opinion,
                SubCN = costManage.SubCN,
                SubHSE = costManage.SubHSE,
                SubProject = costManage.SubProject,
                States = costManage.States,
                CompileMan = costManage.CompileMan,
                CompileDate = costManage.CompileDate
            };
            db.CostGoods_CostManage.InsertOnSubmit(newCostManage);
            db.SubmitChanges();
            CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(Const.ProjectCostManageMenuId, costManage.ProjectId, null, costManage.CostManageId, costManage.CompileDate);
        }

        /// <summary>
        /// 修改费用管理
        /// </summary>
        /// <param name="costManage"></param>
        public static void UpdateCostManage(Model.CostGoods_CostManage costManage)
        {
            Model.SGGLDB db = Funs.DB;
            Model.CostGoods_CostManage newCostManage = db.CostGoods_CostManage.FirstOrDefault(e => e.CostManageId == costManage.CostManageId);
            if (newCostManage != null)
            {
                //newCostManage.ProjectId = costManage.ProjectId;
                newCostManage.CostManageCode = costManage.CostManageCode;
                newCostManage.CostManageName = costManage.CostManageName;
                newCostManage.UnitId = costManage.UnitId;
                newCostManage.ContractNum = costManage.ContractNum;
                newCostManage.CostManageDate = costManage.CostManageDate;
                newCostManage.Opinion = costManage.Opinion;
                newCostManage.SubCN = costManage.SubCN;
                newCostManage.SubHSE = costManage.SubHSE;
                newCostManage.SubProject = costManage.SubProject;
                newCostManage.States = costManage.States;
                newCostManage.CompileMan = costManage.CompileMan;
                newCostManage.CompileDate = costManage.CompileDate;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除费用管理
        /// </summary>
        /// <param name="costManageId"></param>
        public static void DeleteCostManageById(string costManageId)
        {
            Model.SGGLDB db = Funs.DB;
            Model.CostGoods_CostManage costManage = db.CostGoods_CostManage.FirstOrDefault(e => e.CostManageId == costManageId);
            if (costManage != null)
            {
                CodeRecordsService.DeleteCodeRecordsByDataId(costManageId);//删除编号
                ProjectDataFlowSetService.DeleteFlowSetByDataId(costManageId);//删除流程
                CommonService.DeleteFlowOperateByID(costManageId);   //删除审核流程
                CommonService.DeleteAttachFileById(costManageId);//删除附件
                db.CostGoods_CostManage.DeleteOnSubmit(costManage);
                db.SubmitChanges();
            }
        }
    }
}
