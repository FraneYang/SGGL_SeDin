using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 安全费用投入登记明细
    /// </summary>
    public static class CostSmallDetailItemService
    {
        public static Model.SGGLDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取安全费用投入登记明细
        /// </summary>
        /// <param name="costSmallDetailItemId"></param>
        /// <returns></returns>
        public static Model.CostGoods_CostSmallDetailItem GetCostSmallDetailItemById(string costSmallDetailItemId)
        {
            return Funs.DB.CostGoods_CostSmallDetailItem.FirstOrDefault(e => e.CostSmallDetailItemId == costSmallDetailItemId);
        }

        public static List<Model.CostGoods_CostSmallDetailItem> GetCostDetailsByUnitId(string projectId,string unitId, DateTime? startTime, DateTime? endTime)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                return (from x in db.CostGoods_CostSmallDetailItem
                        join y in db.CostGoods_CostSmallDetail on x.CostSmallDetailId equals y.CostSmallDetailId
                        join z in db.Sys_FlowOperate
                        on y.CostSmallDetailId equals z.DataId
                        where y.ProjectId == projectId && y.UnitId == unitId && y.States == BLL.Const.State_2 && z.OperaterTime >= startTime && z.OperaterTime < endTime
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
        public static decimal GetCostDetailsByUnitIdAndCostType(string projectId, string unitId, DateTime startTime, DateTime endTime, string costType)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                decimal cost = 0;
                var q = from x in db.CostGoods_CostSmallDetailItem
                        join y in db.CostGoods_CostSmallDetail on x.CostSmallDetailId equals y.CostSmallDetailId
                        join z in db.Sys_FlowOperate
                        on y.CostSmallDetailId equals z.DataId
                        where y.ProjectId == projectId && y.UnitId == unitId && y.States == BLL.Const.State_2 && z.State == BLL.Const.State_2 && z.OperaterTime >= startTime && z.OperaterTime < endTime && x.CostType.Contains(costType)
                        select x.CostMoney;
                foreach (var item in q)
                {
                    if (item != null)
                    {
                        cost += Funs.GetNewDecimalOrZero(item.ToString());
                    }
                }
                return cost;
                //return (from x in Funs.DB.CostGoods_CostSmallDetailItem
                //        join y in Funs.DB.CostGoods_CostSmallDetail on x.CostSmallDetailId equals y.CostSmallDetailId
                //        where y.UnitId == unitId && y.States == BLL.Const.State_2 && y.ApproveDate >= startTime && y.ApproveDate < endTime && x.CostType.Contains(costType)
                //        select x.CostMoney ?? 0).Sum();
            }
        }

        /// <summary>
        /// 根据时间及费用类型获取当期费用
        /// </summary>
        /// <param name="unitId"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="costType"></param>
        /// <returns></returns>
        public static decimal GetCostDetailsByCostType(string projectId, DateTime startTime, DateTime endTime, string costType)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                decimal cost = 0;
                var q = from x in db.CostGoods_CostSmallDetailItem
                        join y in db.CostGoods_CostSmallDetail on x.CostSmallDetailId equals y.CostSmallDetailId
                        join z in db.Sys_FlowOperate
                        on y.CostSmallDetailId equals z.DataId
                        where y.ProjectId == projectId && y.States == BLL.Const.State_2 && z.State == BLL.Const.State_2 && z.OperaterTime >= startTime && z.OperaterTime < endTime && x.CostType.Contains(costType)
                        select x.CostMoney;
                foreach (var item in q)
                {
                    if (item != null)
                    {
                        cost += Funs.GetNewDecimalOrZero(item.ToString());
                    }
                }
                return cost;
                //return (from x in Funs.DB.CostGoods_CostSmallDetailItem
                //        join y in Funs.DB.CostGoods_CostSmallDetail on x.CostSmallDetailId equals y.CostSmallDetailId
                //        where y.UnitId == unitId && y.States == BLL.Const.State_2 && y.ApproveDate >= startTime && y.ApproveDate < endTime && x.CostType.Contains(costType)
                //        select x.CostMoney ?? 0).Sum();
            }
        }

        /// <summary>
        /// 根据费用类型获取所有费用
        /// </summary>
        /// <param name="unitId"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="costType"></param>
        /// <returns></returns>
        public static decimal GetTotalCostDetailsByCostType(string projectId, string costType)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                decimal cost = 0;
                var q = from x in db.CostGoods_CostSmallDetailItem
                        join y in db.CostGoods_CostSmallDetail on x.CostSmallDetailId equals y.CostSmallDetailId
                        join z in db.Sys_FlowOperate
                        on y.CostSmallDetailId equals z.DataId
                        where y.ProjectId == projectId && y.States == BLL.Const.State_2 && z.State == BLL.Const.State_2 && x.CostType.Contains(costType)
                        select x.CostMoney;
                foreach (var item in q)
                {
                    if (item != null)
                    {
                        cost += Funs.GetNewDecimalOrZero(item.ToString());
                    }
                }
                return cost;
                //return (from x in Funs.DB.CostGoods_CostSmallDetailItem
                //        join y in Funs.DB.CostGoods_CostSmallDetail on x.CostSmallDetailId equals y.CostSmallDetailId
                //        where y.UnitId == unitId && y.States == BLL.Const.State_2 && y.ApproveDate >= startTime && y.ApproveDate < endTime && x.CostType.Contains(costType)
                //        select x.CostMoney ?? 0).Sum();
            }
        }

        /// <summary>
        /// 根据费用投入登记ID获取所有相关明细信息
        /// </summary>
        /// <param name="costSmallDetailId"></param>
        /// <returns></returns>
        public static List<Model.CostGoods_CostSmallDetailItem> GetCostSmallDetailItemByCostSmallDetailId(string costSmallDetailId)
        {
            return (from x in Funs.DB.CostGoods_CostSmallDetailItem where x.CostSmallDetailId == costSmallDetailId select x).ToList();
        }

        /// <summary>
        /// 增加费用明细信息
        /// </summary>
        /// <param name="pauseNotice">费用明细实体</param>
        public static void AddCostDetail(string costSmallDetailId, string costType, decimal costMoney, string costDef)
        {
            Model.SGGLDB db = Funs.DB;
            Model.CostGoods_CostSmallDetailItem newCostSmallDetailItem = new Model.CostGoods_CostSmallDetailItem
            {
                CostSmallDetailItemId = SQLHelper.GetNewID(typeof(Model.CostGoods_CostSmallDetailItem)),
                CostSmallDetailId = costSmallDetailId,
                CostType = costType,
                CostMoney = costMoney,
                CostDef = costDef
            };
            db.CostGoods_CostSmallDetailItem.InsertOnSubmit(newCostSmallDetailItem);
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据主键删除费用投入登记明细
        /// </summary>
        /// <param name="costSmallDetailItemId"></param>
        public static void DeleteCostSmallDetailItemById(string costSmallDetailItemId)
        {
            Model.SGGLDB db = Funs.DB;
            Model.CostGoods_CostSmallDetailItem costSmallDetailItem = db.CostGoods_CostSmallDetailItem.FirstOrDefault(e => e.CostSmallDetailItemId == costSmallDetailItemId);
            if (costSmallDetailItem != null)
            {
                db.CostGoods_CostSmallDetailItem.DeleteOnSubmit(costSmallDetailItem);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除所有相关明细信息
        /// </summary>
        /// <param name="costSmallDetailId"></param>
        public static void DeleteCostSmallDetailItemByCostSmallDetailId(string costSmallDetailId)
        {
            Model.SGGLDB db = Funs.DB;
            var q = (from x in db.CostGoods_CostSmallDetailItem where x.CostSmallDetailId == costSmallDetailId select x).ToList();
            if (q != null)
            {
                db.CostGoods_CostSmallDetailItem.DeleteAllOnSubmit(q);
                db.SubmitChanges();
            }
        }
    }
}
