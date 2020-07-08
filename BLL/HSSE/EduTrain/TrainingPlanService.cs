using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 培训计划
    /// </summary>
    public static class TrainingPlanService
    {
        public static Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);

        /// <summary>
        /// 根据主键获取培训计划
        /// </summary>
        /// <param name="planId"></param>
        /// <returns></returns>
        public static Model.Training_Plan GetPlanById(string planId)
        {
            return new Model.SGGLDB(Funs.ConnString).Training_Plan.FirstOrDefault(e => e.PlanId == planId);
        }

        /// <summary>
        /// 添加培训计划
        /// </summary>
        /// <param name="plan"></param>
        public static void AddPlan(Model.Training_Plan plan)
        {
            Model.Training_Plan newPlan = new Model.Training_Plan
            {
                PlanId = plan.PlanId,
                PlanCode = plan.PlanCode,
                PlanName = plan.PlanName,
                DesignerId = plan.DesignerId,
                DesignerDate = plan.DesignerDate,
                WorkPostId = plan.WorkPostId,
                States = plan.States
            };
            db.Training_Plan.InsertOnSubmit(newPlan);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改培训计划
        /// </summary>
        /// <param name="plan"></param>
        public static void UpdatePlan(Model.Training_Plan plan)
        {
            Model.Training_Plan newPlan = new Model.SGGLDB(Funs.ConnString).Training_Plan.FirstOrDefault(e => e.PlanId == plan.PlanId);
            if (newPlan != null)
            {
                newPlan.PlanCode = plan.PlanCode;
                newPlan.PlanName = plan.PlanName;
                newPlan.DesignerId = plan.DesignerId;
                newPlan.DesignerDate = plan.DesignerDate;
                newPlan.WorkPostId = plan.WorkPostId;
                newPlan.QRCodeUrl = plan.QRCodeUrl;
                newPlan.States = plan.States;
                new Model.SGGLDB(Funs.ConnString).SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除培训计划信息
        /// </summary>
        /// <param name="planId"></param>
        public static void DeletePlanById(string planId)
        {
            Model.Training_Plan plan = new Model.SGGLDB(Funs.ConnString).Training_Plan.FirstOrDefault(e => e.PlanId == planId);
            if (plan != null)
            {
                new Model.SGGLDB(Funs.ConnString).Training_Plan.DeleteOnSubmit(plan);
                new Model.SGGLDB(Funs.ConnString).SubmitChanges();
            }
        }

        /// <summary>
        /// 获取培训计划列
        /// </summary>
        /// <returns></returns>
        public static List<Model.Training_Plan> GetPlanList()
        {
            return (from x in db.Training_Plan orderby x.PlanCode select x).ToList();
        }
    }
}
