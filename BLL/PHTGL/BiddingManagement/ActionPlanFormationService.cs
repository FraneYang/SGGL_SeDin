using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{

    public static class PHTGL_ActionPlanFormationService
    {

        public static Model.PHTGL_ActionPlanFormation GetPHTGL_ActionPlanFormationById(string ActionPlanID
)

        {
            return Funs.DB.PHTGL_ActionPlanFormation.FirstOrDefault(e => e.ActionPlanID == ActionPlanID
);
        }


        public static void AddPHTGL_ActionPlanFormation(Model.PHTGL_ActionPlanFormation newtable)
        {
            Model.PHTGL_ActionPlanFormation table = new Model.PHTGL_ActionPlanFormation();
            table.ActionPlanID = newtable.ActionPlanID;
            table.TimeRequirements = newtable.TimeRequirements;
            table.QualityRequirement = newtable.QualityRequirement;
            table.HSERequirement = newtable.HSERequirement;
            table.TechnicalRequirement = newtable.TechnicalRequirement;
            table.CurrentRequirement = newtable.CurrentRequirement;
            table.Sub_Selection = newtable.Sub_Selection;
            table.Bid_Selection = newtable.Bid_Selection;
            table.ContractingMode_Select = newtable.ContractingMode_Select;
            table.PriceMode_Select = newtable.PriceMode_Select;
            table.MaterialsDifferentiate = newtable.MaterialsDifferentiate;
            table.CreateTime = newtable.CreateTime;
            table.ImportExplain = newtable.ImportExplain;
            table.ShortNameList = newtable.ShortNameList;
            table.EvaluationMethods = newtable.EvaluationMethods;
            table.EvaluationPlan = newtable.EvaluationPlan;
            table.BiddingMethods_Select = newtable.BiddingMethods_Select;
            table.SchedulePlan = newtable.SchedulePlan;
            table.CreatUser = newtable.CreatUser;
            table.ProjectID = newtable.ProjectID;
            table.ProjectName = newtable.ProjectName;
            table.Unit = newtable.Unit;
            table.ConstructionSite = newtable.ConstructionSite;
            table.BiddingProjectScope = newtable.BiddingProjectScope;
            table.BiddingProjectContent = newtable.BiddingProjectContent;
            Funs.DB.PHTGL_ActionPlanFormation.InsertOnSubmit(table);
            Funs.DB.SubmitChanges();
        }


        public static void UpdatePHTGL_ActionPlanFormation(Model.PHTGL_ActionPlanFormation newtable)
        {
            Model.PHTGL_ActionPlanFormation table = Funs.DB.PHTGL_ActionPlanFormation.FirstOrDefault(e => e.ActionPlanID == newtable.ActionPlanID
);

            if (table != null)
            {
                table.ActionPlanID = newtable.ActionPlanID;
                table.TimeRequirements = newtable.TimeRequirements;
                table.QualityRequirement = newtable.QualityRequirement;
                table.HSERequirement = newtable.HSERequirement;
                table.TechnicalRequirement = newtable.TechnicalRequirement;
                table.CurrentRequirement = newtable.CurrentRequirement;
                table.Sub_Selection = newtable.Sub_Selection;
                table.Bid_Selection = newtable.Bid_Selection;
                table.ContractingMode_Select = newtable.ContractingMode_Select;
                table.PriceMode_Select = newtable.PriceMode_Select;
                table.MaterialsDifferentiate = newtable.MaterialsDifferentiate;
                table.CreateTime = newtable.CreateTime;
                table.ImportExplain = newtable.ImportExplain;
                table.ShortNameList = newtable.ShortNameList;
                table.EvaluationMethods = newtable.EvaluationMethods;
                table.EvaluationPlan = newtable.EvaluationPlan;
                table.BiddingMethods_Select = newtable.BiddingMethods_Select;
                table.SchedulePlan = newtable.SchedulePlan;
                table.CreatUser = newtable.CreatUser;
                table.ProjectID = newtable.ProjectID;
                table.ProjectName = newtable.ProjectName;
                table.Unit = newtable.Unit;
                table.ConstructionSite = newtable.ConstructionSite;
                table.BiddingProjectScope = newtable.BiddingProjectScope;
                table.BiddingProjectContent = newtable.BiddingProjectContent;
                Funs.DB.SubmitChanges();
            }

        }
        public static void DeletePHTGL_ActionPlanFormationById(string ActionPlanID
)
        {
            Model.PHTGL_ActionPlanFormation table = Funs.DB.PHTGL_ActionPlanFormation.FirstOrDefault(e => e.ActionPlanID == ActionPlanID
);
            if (table != null)
            {
                Funs.DB.PHTGL_ActionPlanFormation.DeleteOnSubmit(table);
                Funs.DB.SubmitChanges();
            }

        }

    }
}