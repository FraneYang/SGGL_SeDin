using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{

    public static class PHTGL_ActionPlanReviewService
    {

        public static Model.PHTGL_ActionPlanReview GetPHTGL_ActionPlanReviewById(string ActionPlanReviewId
)

        {
            return Funs.DB.PHTGL_ActionPlanReview.FirstOrDefault(e => e.ActionPlanReviewId == ActionPlanReviewId
);
        }


        public static Model.PHTGL_ActionPlanReview GetPHTGL_ActionPlanReviewByActionPlanID(string actionPlanID
)

        {
            return Funs.DB.PHTGL_ActionPlanReview.FirstOrDefault(e => e.ActionPlanID == actionPlanID
);
        }

        public static void AddPHTGL_ActionPlanReview(Model.PHTGL_ActionPlanReview newtable)
        {
            Model.PHTGL_ActionPlanReview table = new Model.PHTGL_ActionPlanReview();
            table.ActionPlanReviewId = newtable.ActionPlanReviewId;
            table.ActionPlanID = newtable.ActionPlanID;
            table.State = newtable.State;
            table.Approval_Construction = newtable.Approval_Construction;
            table.CreateUser = newtable.CreateUser;
            table.ConstructionManager = newtable.ConstructionManager;
            table.PreliminaryMan = newtable.PreliminaryMan;
            table.ProjectManager = newtable.ProjectManager;
            table.DeputyGeneralManager = newtable.DeputyGeneralManager;
            Funs.DB.PHTGL_ActionPlanReview.InsertOnSubmit(table);
            Funs.DB.SubmitChanges();
        }


        public static void UpdatePHTGL_ActionPlanReview(Model.PHTGL_ActionPlanReview newtable)
        {
            Model.PHTGL_ActionPlanReview table = Funs.DB.PHTGL_ActionPlanReview.FirstOrDefault(e => e.ActionPlanReviewId == newtable.ActionPlanReviewId
);

            if (table != null)
            {
                table.ActionPlanReviewId = newtable.ActionPlanReviewId;
                table.ActionPlanID = newtable.ActionPlanID;
                table.State = newtable.State;
                table.Approval_Construction = newtable.Approval_Construction;
                table.CreateUser = newtable.CreateUser;
                table.ConstructionManager = newtable.ConstructionManager;
                table.PreliminaryMan = newtable.PreliminaryMan;
                table.ProjectManager = newtable.ProjectManager;
                table.DeputyGeneralManager = newtable.DeputyGeneralManager;
                Funs.DB.SubmitChanges();
            }

        }
        public static void DeletePHTGL_ActionPlanReviewById(string ActionPlanReviewId
)
        {
            Model.PHTGL_ActionPlanReview table = Funs.DB.PHTGL_ActionPlanReview.FirstOrDefault(e => e.ActionPlanReviewId == ActionPlanReviewId
);
            if (table != null)
            {
                Funs.DB.PHTGL_ActionPlanReview.DeleteOnSubmit(table);
                Funs.DB.SubmitChanges();
            }

        }

        /// <summary>
        /// 获取审批人员
        /// </summary>
        /// <param name="ContractId"></param>
        /// <returns></returns>
        public static Dictionary<int, string> Get_DicApproveman(string projectid, string ContractId)
        {
            Dictionary<int, string> Dic_Approveman = new Dictionary<int, string>();

            Model.PHTGL_ActionPlanReview table = GetPHTGL_ActionPlanReviewById(ContractId);
            string UnitID = BLL.ProjectService.GetProjectByProjectId(projectid).UnitId;
            Dic_Approveman.Add(1,table.ConstructionManager);
            Dic_Approveman.Add(2,table.PreliminaryMan);
            Dic_Approveman.Add(3,table.Approval_Construction);
            Dic_Approveman.Add(4,table.ProjectManager);
            Dic_Approveman.Add(5,table.DeputyGeneralManager);
 
            return Dic_Approveman;
        }

        public static List<ApproveManModel> GetApproveManModels(string projectid, string ContractId)
        {
 
            Model.PHTGL_ActionPlanReview table = GetPHTGL_ActionPlanReviewById(ContractId);
            string UnitID = BLL.ProjectService.GetProjectByProjectId(projectid).UnitId;
        
            List<ApproveManModel> approveManModels = new List<ApproveManModel>();
            approveManModels.Add(new ApproveManModel { Number = 1, userid = table.ConstructionManager, Rolename = "施工经理" });
            approveManModels.Add(new ApproveManModel { Number = 2, userid = table.PreliminaryMan, Rolename = "预审人员" });
            approveManModels.Add(new ApproveManModel { Number = 3, userid = table.Approval_Construction, Rolename = "施工管理部" });
            approveManModels.Add(new ApproveManModel { Number = 4, userid = table.ProjectManager, Rolename = "项目经理" });
            approveManModels.Add(new ApproveManModel { Number = 5, userid = table.DeputyGeneralManager, Rolename = "分管副总经理" });
            return approveManModels;
        }

    }

}