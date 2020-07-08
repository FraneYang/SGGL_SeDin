using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 措施费用使用计划
    /// </summary>
    public static class ExpenseService
    {
        public static Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);

        /// <summary>
        /// 根据主键获取措施费用使用计划
        /// </summary>
        /// <param name="expenseId"></param>
        /// <returns></returns>
        public static Model.CostGoods_Expense GetExpenseById(string expenseId)
        {
            return new Model.SGGLDB(Funs.ConnString).CostGoods_Expense.FirstOrDefault(e => e.ExpenseId == expenseId);
        }

        /// <summary>
        /// 根据时间获取当期单位Id集合
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public static List<string> GetUnitIdsByTime(DateTime startTime, DateTime endTime, string projectId)
        {
            return (from x in new Model.SGGLDB(Funs.ConnString).CostGoods_Expense
                    join y in new Model.SGGLDB(Funs.ConnString).Sys_FlowOperate
                    on x.ExpenseId equals y.DataId
                    where x.States == BLL.Const.State_2 && x.ProjectId == projectId && y.State == BLL.Const.State_2 && y.OperaterTime >= startTime && y.OperaterTime < endTime
                    select x.UnitId).Distinct().ToList();
        }

        /// <summary>
        /// 添加措施费用使用计划
        /// </summary>
        /// <param name="expense"></param>
        public static void AddExpense(Model.CostGoods_Expense expense)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.CostGoods_Expense newExpense = new Model.CostGoods_Expense
            {
                ExpenseId = expense.ExpenseId,
                ProjectId = expense.ProjectId,
                ExpenseCode = expense.ExpenseCode,
                UnitId = expense.UnitId,
                //newExpense.CompileMan = expense.CompileMan;
                CreateDate = expense.CreateDate,
                States = expense.States,
                Months = expense.Months,
                ReportDate = expense.ReportDate,
                PlanCostA = expense.PlanCostA,
                PlanCostB = expense.PlanCostB,
                CompileDate = expense.CompileDate,
                // newExpense.CheckMan = expense.CheckMan;
                CheckDate = expense.CheckDate,
                // newExpense.ApproveMan = expense.ApproveMan;
                ApproveDate = expense.ApproveDate
            };
            db.CostGoods_Expense.InsertOnSubmit(newExpense);
            db.SubmitChanges();
            BLL.CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(Const.ProjectExpenseMenuId, expense.ProjectId, null, expense.ExpenseId, expense.CreateDate);
        }

        /// <summary>
        /// 修改措施费用使用计划
        /// </summary>
        /// <param name="expense"></param>
        public static void UpdateExpense(Model.CostGoods_Expense expense)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.CostGoods_Expense newExpense = db.CostGoods_Expense.FirstOrDefault(e => e.ExpenseId == expense.ExpenseId);
            if (newExpense != null)
            {
                //newExpense.ProjectId = expense.ProjectId;
                newExpense.ExpenseCode = expense.ExpenseCode;
                newExpense.UnitId = expense.UnitId;
               // newExpense.CompileMan = expense.CompileMan;
                newExpense.CreateDate = expense.CreateDate;
                newExpense.States = expense.States;
                newExpense.Months = expense.Months;
                newExpense.ReportDate = expense.ReportDate;
                newExpense.PlanCostA = expense.PlanCostA;
                newExpense.PlanCostB = expense.PlanCostB;
                newExpense.CompileDate = expense.CompileDate;
                //newExpense.CheckMan = expense.CheckMan;
                newExpense.CheckDate = expense.CheckDate;
                //newExpense.ApproveMan = expense.ApproveMan;
                newExpense.ApproveDate = expense.ApproveDate;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除措施费用使用计划
        /// </summary>
        /// <param name="expenseId"></param>
        public static void DeleteExpenseById(string expenseId)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.CostGoods_Expense expense = db.CostGoods_Expense.FirstOrDefault(e => e.ExpenseId == expenseId);
            if (expense != null)
            {
                BLL.CodeRecordsService.DeleteCodeRecordsByDataId(expenseId);
                BLL.CommonService.DeleteAttachFileById(expenseId);
                ////删除审核流程表
                BLL.CommonService.DeleteFlowOperateByID(expenseId);
                db.CostGoods_Expense.DeleteOnSubmit(expense);
                db.SubmitChanges();
            }
        }
    }
}