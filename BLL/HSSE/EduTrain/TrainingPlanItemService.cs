using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 培训计划明细表
    /// </summary>
    public static class TrainingPlanItemService
    {
        public static Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);

        /// <summary>
        /// 根据培训计划明细表Id获取所有相关明细信息
        /// </summary>
        /// <param name="planId"></param>
        /// <returns></returns>
        public static List<Model.Training_PlanItem> GetPlanItemByPlanId(string planId)
        {
            return (from x in db.Training_PlanItem where x.PlanId == planId select x).ToList();
        }

        /// <summary>
        /// 添加培训计划明细信息
        /// </summary>
        /// <param name="planItem"></param>
        public static void AddPlanItem(Model.Training_PlanItem planItem)
        {
            Model.Training_PlanItem newPlanItem = new Model.Training_PlanItem
            {
                PlanItemId = planItem.PlanItemId,
                PlanId = planItem.PlanId,
                TrainingEduId = planItem.TrainingEduId
            };
            db.Training_PlanItem.InsertOnSubmit(newPlanItem);
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据明细键删除培训计划明细
        /// </summary>
        /// <param name="planItemId"></param>
        public static void DeletePlanItemById(string planItemId)
        {
            var planItem = db.Training_PlanItem.FirstOrDefault(e => e.PlanItemId == planItemId);
            if (planItem != null)
            {
                db.Training_PlanItem.DeleteOnSubmit(planItem);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据培训ID删除所有相关明细信息
        /// </summary>
        /// <param name="planId"></param>
        public static void DeletePlanItemByPlanId(string planId)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            var planItem = (from x in db.Training_PlanItem where x.PlanId == planId select x).ToList();
            if (planItem.Count() > 0)
            {                
                db.Training_PlanItem.DeleteAllOnSubmit(planItem);
                db.SubmitChanges();
            }
        }
    }
}
