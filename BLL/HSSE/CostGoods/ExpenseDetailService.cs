using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class ExpenseDetailService
    {
        public static decimal? GetSumCostMoneyByExpenseId(string expenseId)
        {
            return (from x in new Model.SGGLDB(Funs.ConnString).CostGoods_ExpenseDetail where x.ExpenseId == expenseId select x.CostMoney).Sum();
        }

        public static List<Model.CostGoods_ExpenseDetail> GetExpenseDetailsByExpenseId(string expenseId)
        {
            return (from x in new Model.SGGLDB(Funs.ConnString).CostGoods_ExpenseDetail where x.ExpenseId == expenseId select x).ToList();
        }

        public static decimal GetSumCostMoneyByExpenseIdAndType(string expenseId, string costType)
        {
            Model.CostGoods_ExpenseDetail detail = (from x in new Model.SGGLDB(Funs.ConnString).CostGoods_ExpenseDetail where x.ExpenseId == expenseId && x.CostType == costType select x).FirstOrDefault();
            if (detail != null)
            {
                return detail.CostMoney ?? 0;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 根据单位和时间获取费用明细集合
        /// </summary>
        /// <param name="unitId"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public static List<Model.CostGoods_ExpenseDetail> GetCostDetailsByUnitId(string unitId, DateTime startTime, DateTime endTime)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                return (from x in db.CostGoods_ExpenseDetail
                        join y in db.CostGoods_Expense on x.ExpenseId equals y.ExpenseId
                        where y.UnitId == unitId && y.States == BLL.Const.State_2 && y.ApproveDate >= startTime && y.ApproveDate < endTime
                        select x).Distinct().ToList();
            }
        }

        /// <summary>
        /// 根据单位和时间及费用类型获取当期费用
        /// </summary>
        /// <param name="unitId"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="costType"></param>
        /// <returns></returns>
        public static decimal GetCostDetailsByUnitIdAndCostType(string unitId, DateTime startTime, DateTime endTime, string costType)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                decimal cost = 0;
                var q = from x in db.CostGoods_ExpenseDetail
                        join y in db.CostGoods_Expense on x.ExpenseId equals y.ExpenseId
                        join z in db.Sys_FlowOperate
                        on y.ExpenseId equals z.DataId
                        where y.UnitId == unitId && y.States == BLL.Const.State_2 && z.State == BLL.Const.State_2 && z.OperaterTime >= startTime && z.OperaterTime < endTime && x.CostType.Contains(costType)
                        select x.CostMoney;
                foreach (var item in q)
                {
                    if (item != null)
                    {
                        cost += Funs.GetNewDecimalOrZero(item.ToString());
                    }
                }
                return cost;
                //return (from x in new Model.SGGLDB(Funs.ConnString).CostGoods_ExpenseDetail
                //        join y in new Model.SGGLDB(Funs.ConnString).CostGoods_Expense on x.ExpenseId equals y.ExpenseId
                //        where y.UnitId == unitId && y.States == BLL.Const.State_2 && y.ApproveDate >= startTime && y.ApproveDate < endTime && x.CostType.Contains(costType)
                //        select x.CostMoney ?? 0).Sum();
            }
        }

        /// <summary>
        /// 增加费用明细信息
        /// </summary>
        /// <param name="pauseNotice">费用明细实体</param>
        public static void AddCostDetail(string expenseId, string costType, decimal costMoney, string costDef)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.CostGoods_ExpenseDetail newExpenseDetail = new Model.CostGoods_ExpenseDetail
            {
                ExpenseDetailId = SQLHelper.GetNewID(typeof(Model.CostGoods_ExpenseDetail)),
                ExpenseId = expenseId,
                CostType = costType,
                CostMoney = costMoney,
                CostDef = costDef
            };
            db.CostGoods_ExpenseDetail.InsertOnSubmit(newExpenseDetail);
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据费用编号和费用类型获取费用明细信息
        /// </summary>
        /// <param name="expenseId"></param>
        /// <param name="costType"></param>
        /// <returns></returns>
        public static Model.CostGoods_ExpenseDetail GetCostDetailByExpenseIdAndCostType(string expenseId, string costType)
        {
            return new Model.SGGLDB(Funs.ConnString).CostGoods_ExpenseDetail.FirstOrDefault(e => (e.ExpenseId == expenseId && e.CostType == costType));
        }

        /// <summary>
        /// 根据费用编号删除对应的费用明细信息
        /// </summary>
        /// <param name="expenseId">编号</param>
        public static void DeleteCostDetailByExpenseId(string expenseId)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            var q = (from x in db.CostGoods_ExpenseDetail where x.ExpenseId == expenseId select x).ToList();
            db.CostGoods_ExpenseDetail.DeleteAllOnSubmit(q);
            db.SubmitChanges();
        }
    }
}
