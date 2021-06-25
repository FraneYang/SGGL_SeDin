using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

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
            table.EPCCode = newtable.EPCCode;
            table.ProjectCode = newtable.ProjectCode;
            table.ProjectShortName = newtable.ProjectShortName;
            table.AttachUrlContent = newtable.AttachUrlContent;
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
                table.EPCCode = newtable.EPCCode;
                table.ProjectCode = newtable.ProjectCode;
                table.ProjectShortName = newtable.ProjectShortName;
                table.AttachUrlContent = newtable.AttachUrlContent;

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
        public static List<Model.PHTGL_ActionPlanFormation> getEpcCode()
        {
            var q = (from x in Funs.DB.PHTGL_ActionPlanFormation where x.State == Const.ContractCreat_Complete select x).ToList();
            return q;
        }

        /// <summary>
        /// 获取所有项目号
        /// </summary>
        /// <param name="dropName"></param>
        /// <param name="isShowPlease"></param>
        public static void InitAllProjectCodeDropDownList(FineUIPro.DropDownList dropName, bool isShowPlease)
        {
            dropName.DataValueField = "ProjectId";
            dropName.DataTextField = "ProjectCode";
            var projectlist = getEpcCode();
            dropName.DataSource = projectlist;
            dropName.DataBind();
            if (projectlist.Count() == 0)
            {
                isShowPlease = true;
            }
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
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

        public static ListItem[] GetPriceType()
        {
            ListItem[] list = new ListItem[5];
            list[0] = new ListItem("固定总价", "固定总价");
            list[1] = new ListItem("全费用固定综合单价", "全费用固定综合单价");
            list[2] = new ListItem("定额计价总价下浮(税前)", "定额计价总价下浮(税前)");
            list[3] = new ListItem("综合费率", "综合费率");
            list[4] = new ListItem("费率下浮", "费率下浮");
            return list;
        }
        /// <summary>
        /// 招标方式下拉框
        /// </summary>
        /// <param name="dropName">下拉框名字</param>
        /// <param name="RoleId">角色id</param>
        /// <param name="unitId">单位id</param>
        /// <param name="isShowPlease">是否显示请选择</param>
        public static void InitGetBidTypeDropDownList(FineUIPro.DropDownList dropName, bool isShowPlease)
        {
            dropName.DataValueField = "Text";
            dropName.DataTextField = "Value";
            dropName.DataSource = BLL.DropListService.GetBidType();
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }

        public static void InitGetPriceTypeDropDownList(FineUIPro.DropDownList dropName, bool isShowPlease)
        {
            dropName.DataValueField = "Text";
            dropName.DataTextField = "Value";
            dropName.DataSource = GetPriceType();
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }


    }
}