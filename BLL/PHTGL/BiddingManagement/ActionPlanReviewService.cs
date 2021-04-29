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

            Dic_Approveman.Add(1, BLL.ProjectService.GetRoleID(projectid, BLL.Const.ConstructionManager));
              Dic_Approveman.Add(2, table.Approval_Construction);
             Dic_Approveman.Add(3, BLL.ProjectService.GetRoleID(projectid, BLL.Const.ProjectManager));
            Dic_Approveman.Add(4, BLL.ProjectService.GetRoleID(projectid, BLL.Const.DeputyGeneralManager));
 
            return Dic_Approveman;
        }

    }
}