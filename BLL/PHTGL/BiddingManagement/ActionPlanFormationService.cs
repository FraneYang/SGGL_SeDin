using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{

    public static class PHTGL_ActionPlanFormationService
    {
        /// <summary>
        /// 根据主键查询
        /// </summary>
        /// <param name="ActionPlanID"></param>
        /// <returns></returns>
        public static Model.PHTGL_ActionPlanFormation GetPHTGL_ActionPlanFormationById(string ActionPlanID)
        {
            return Funs.DB.PHTGL_ActionPlanFormation.FirstOrDefault(e => e.ActionPlanID == ActionPlanID);
        }

        /// <summary>
        /// 根据编号查询
        /// </summary>
        /// <param name="actionPlanCode"></param>
        /// <returns></returns>
        public static Model.PHTGL_ActionPlanFormation GetPHTGL_ActionPlanFormationByCode(string actionPlanCode)
        {
            return Funs.DB.PHTGL_ActionPlanFormation.FirstOrDefault(e => e.ActionPlanCode == actionPlanCode);
        }



        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="newtable"></param>
        public static void AddPHTGL_ActionPlanFormation(Model.PHTGL_ActionPlanFormation newtable)
        {
            Model.PHTGL_ActionPlanFormation table = new Model.PHTGL_ActionPlanFormation();
            table.ActionPlanID = newtable.ActionPlanID;
            table.ActionPlanCode = newtable.ActionPlanCode;
            table.CreateTime = newtable.CreateTime;
            table.CreatUser = newtable.CreatUser;
            table.State = newtable.State;
            table.ProjectID = newtable.ProjectID;
            table.ProjectName = newtable.ProjectName;
            table.Unit = newtable.Unit;
            table.ConstructionSite = newtable.ConstructionSite;
            table.BiddingProjectScope = newtable.BiddingProjectScope;
            table.BiddingProjectContent = newtable.BiddingProjectContent;
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
            table.ImportExplain = newtable.ImportExplain;
            table.ShortNameList = newtable.ShortNameList;
            table.EvaluationMethods = newtable.EvaluationMethods;
            table.EvaluationPlan = newtable.EvaluationPlan;
            table.BiddingMethods_Select = newtable.BiddingMethods_Select;
            table.SchedulePlan = newtable.SchedulePlan;
            table.BidProject = newtable.BidProject;
            table.BidPrice = newtable.BidPrice;
            table.PriceType = newtable.PriceType;
            table.BidType = newtable.BidType;
            Funs.DB.PHTGL_ActionPlanFormation.InsertOnSubmit(table);
            Funs.DB.SubmitChanges();
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="newtable"></param>
        public static void UpdatePHTGL_ActionPlanFormation(Model.PHTGL_ActionPlanFormation newtable)
        {
            Model.PHTGL_ActionPlanFormation table = Funs.DB.PHTGL_ActionPlanFormation.FirstOrDefault(e => e.ActionPlanID == newtable.ActionPlanID
);

            if (table != null)
            {
                table.ActionPlanID = newtable.ActionPlanID;
                table.ActionPlanCode = newtable.ActionPlanCode;
                table.CreateTime = newtable.CreateTime;
                table.CreatUser = newtable.CreatUser;
                table.State = newtable.State;
                table.ProjectID = newtable.ProjectID;
                table.ProjectName = newtable.ProjectName;
                table.Unit = newtable.Unit;
                table.ConstructionSite = newtable.ConstructionSite;
                table.BiddingProjectScope = newtable.BiddingProjectScope;
                table.BiddingProjectContent = newtable.BiddingProjectContent;
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
                table.ImportExplain = newtable.ImportExplain;
                table.ShortNameList = newtable.ShortNameList;
                table.EvaluationMethods = newtable.EvaluationMethods;
                table.EvaluationPlan = newtable.EvaluationPlan;
                table.BiddingMethods_Select = newtable.BiddingMethods_Select;
                table.SchedulePlan = newtable.SchedulePlan;
                table.BidProject = newtable.BidProject;
                table.BidPrice = newtable.BidPrice;
                table.PriceType = newtable.PriceType;
                table.BidType = newtable.BidType;
                Funs.DB.SubmitChanges();
            }

        }

        public static void DeletePHTGL_ActionPlanFormationById(string ActionPlanID)
        {
            Model.PHTGL_ActionPlanFormation table = Funs.DB.PHTGL_ActionPlanFormation.FirstOrDefault(e => e.ActionPlanID == ActionPlanID);
            if (table != null)
            {
                Funs.DB.PHTGL_ActionPlanFormation.DeleteOnSubmit(table);
                Funs.DB.SubmitChanges();
            }

        }
        public static void InitGetAcpCompleteDropDownList(FineUIPro.DropDownList dropName, bool isShowPlease)
        {
            dropName.DataValueField = "ActionPlanCode";
            dropName.DataTextField = "ActionPlanCode";
            dropName.DataSource = GetCompleteActionPlanFormat();
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }
        public static object GetCompleteActionPlanFormat()
        {
            var list = (from x in Funs.DB.PHTGL_ActionPlanFormation
                        join y in Funs.DB.PHTGL_ActionPlanReview on x.ActionPlanID equals y.ActionPlanID
                        where y.State == Const.ContractReview_Complete
                        select x).ToList();
            return list;
        }

    }
}