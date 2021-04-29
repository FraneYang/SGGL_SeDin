using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{

    public static class PHTGL_ActionPlanFormation_Sch1Service
    {

        public static Model.PHTGL_ActionPlanFormation_Sch1 GetPHTGL_ActionPlanFormation_Sch1ByItemID(string ActionPlanItemID)

        {
            return Funs.DB.PHTGL_ActionPlanFormation_Sch1.FirstOrDefault(e => e.ActionPlanItemID == ActionPlanItemID);
        }


        public static Model.PHTGL_ActionPlanFormation_Sch1 GetPHTGL_ActionPlanFormation_Sch1ById(string ActionPlanID)

        {
            return Funs.DB.PHTGL_ActionPlanFormation_Sch1.FirstOrDefault(e => e.ActionPlanID == ActionPlanID);
        }


        public static void AddPHTGL_ActionPlanFormation_Sch1(Model.PHTGL_ActionPlanFormation_Sch1 newtable)
        {
            Model.PHTGL_ActionPlanFormation_Sch1 table = new Model.PHTGL_ActionPlanFormation_Sch1();
            table.ActionPlanItemID = newtable.ActionPlanItemID;
            table.ActionPlanID = newtable.ActionPlanID;
            table.PlanningContent = newtable.PlanningContent;
            table.ActionPlan = newtable.ActionPlan;
            table.Remarks = newtable.Remarks;
            Funs.DB.PHTGL_ActionPlanFormation_Sch1.InsertOnSubmit(table);
            Funs.DB.SubmitChanges();
        }


        public static void UpdatePHTGL_ActionPlanFormation_Sch1(Model.PHTGL_ActionPlanFormation_Sch1 newtable)
        {
            Model.PHTGL_ActionPlanFormation_Sch1 table = Funs.DB.PHTGL_ActionPlanFormation_Sch1.FirstOrDefault(e => e.ActionPlanItemID == newtable.ActionPlanItemID
);

            if (table != null)
            {
                table.ActionPlanItemID = newtable.ActionPlanItemID;
                table.ActionPlanID = newtable.ActionPlanID;
                table.PlanningContent = newtable.PlanningContent;
                table.ActionPlan = newtable.ActionPlan;
                table.Remarks = newtable.Remarks;
                Funs.DB.SubmitChanges();
            }

        }
		public static void DeletePHTGL_ActionPlanFormation_Sch1ByItemID(string ActionPlanItemID
)
		{
			Model.PHTGL_ActionPlanFormation_Sch1 table = Funs.DB.PHTGL_ActionPlanFormation_Sch1.FirstOrDefault(e => e.ActionPlanItemID == ActionPlanItemID
);
            if (table != null)
            {
                Funs.DB.PHTGL_ActionPlanFormation_Sch1.DeleteOnSubmit(table);
                Funs.DB.SubmitChanges();
            }

        }

        /// <summary>
        /// 根据实施计划Id删除相关
        /// </summary>
        /// <param name="attachUrlId"></param>
        public static void DeletePHTGL_ActionPlanFormation_Sch1ById(string ActionPlanID)
        {
            var q = (from x in Funs.DB.PHTGL_ActionPlanFormation_Sch1 where x.ActionPlanID == ActionPlanID select x).ToList();
            if (q != null)
            {
                Funs.DB.PHTGL_ActionPlanFormation_Sch1.DeleteAllOnSubmit(q);
                Funs.DB.SubmitChanges();
            }
        }

    }
}