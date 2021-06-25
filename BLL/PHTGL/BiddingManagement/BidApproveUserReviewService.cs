using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{

    public static class PHTGL_BidApproveUserReviewService
    {

        public static Model.PHTGL_BidApproveUserReview GetPHTGL_BidApproveUserReviewById(string ApproveUserReviewID)

        {
            return Funs.DB.PHTGL_BidApproveUserReview.FirstOrDefault(e => e.ApproveUserReviewID == ApproveUserReviewID);
        }


        public static void AddPHTGL_BidApproveUserReview(Model.PHTGL_BidApproveUserReview newtable)
        {
            Model.PHTGL_BidApproveUserReview table = new Model.PHTGL_BidApproveUserReview();
            table.ApproveUserReviewID = newtable.ApproveUserReviewID;
            table.BidDocumentsReviewId = newtable.BidDocumentsReviewId;
            table.ActionPlanID = newtable.ActionPlanID;
            table.ProjectId = newtable.ProjectId;
            table.BidProject = newtable.BidProject;
            table.State = newtable.State;
            table.CreateUser = newtable.CreateUser;
            table.ConstructionManager = newtable.ConstructionManager;
            table.ProjectManager = newtable.ProjectManager;
            table.Approval_Construction = newtable.Approval_Construction;
            table.DeputyGeneralManager = newtable.DeputyGeneralManager;
            Funs.DB.PHTGL_BidApproveUserReview.InsertOnSubmit(table);
            Funs.DB.SubmitChanges();
        }


        public static void UpdatePHTGL_BidApproveUserReview(Model.PHTGL_BidApproveUserReview newtable)
        {
            Model.PHTGL_BidApproveUserReview table = Funs.DB.PHTGL_BidApproveUserReview.FirstOrDefault(e => e.ApproveUserReviewID == newtable.ApproveUserReviewID);

            if (table != null)
            {
                table.ApproveUserReviewID = newtable.ApproveUserReviewID;
                table.BidDocumentsReviewId = newtable.BidDocumentsReviewId;
                table.ActionPlanID = newtable.ActionPlanID;
                table.ProjectId = newtable.ProjectId;
                table.BidProject = newtable.BidProject;
                table.State = newtable.State;
                table.CreateUser = newtable.CreateUser;
                table.ConstructionManager = newtable.ConstructionManager;
                table.ProjectManager = newtable.ProjectManager;
                table.Approval_Construction = newtable.Approval_Construction;
                table.DeputyGeneralManager = newtable.DeputyGeneralManager;
                Funs.DB.SubmitChanges();
            }

        }
        public static void DeletePHTGL_BidApproveUserReviewById(string ApproveUserReviewID)
        {
            Model.PHTGL_BidApproveUserReview table = Funs.DB.PHTGL_BidApproveUserReview.FirstOrDefault(e => e.ApproveUserReviewID == ApproveUserReviewID);
            if (table != null)
            {
                Funs.DB.PHTGL_BidApproveUserReview.DeleteOnSubmit(table);
                Funs.DB.SubmitChanges();
            }

        }
        public static Dictionary<int, string> Get_DicApproveman(string ApproveUserReviewID)
        {
            Dictionary<int, string> Dic_Approveman = new Dictionary<int, string>();

            Model.PHTGL_BidApproveUserReview table = GetPHTGL_BidApproveUserReviewById(ApproveUserReviewID);

            Dic_Approveman.Add(1, table.ConstructionManager);
            Dic_Approveman.Add(2, table.ProjectManager);
            Dic_Approveman.Add(3, table.Approval_Construction);
            Dic_Approveman.Add(4, table.DeputyGeneralManager);

            return Dic_Approveman;
        }
        public static List<ApproveManModel> GetApproveManModels(string ApproveUserReviewID)
        {

            Model.PHTGL_BidApproveUserReview table = GetPHTGL_BidApproveUserReviewById(ApproveUserReviewID);

            List<ApproveManModel> approveManModels = new List<ApproveManModel>();
            approveManModels.Add(new ApproveManModel { Number = 1, userid = table.ConstructionManager, Rolename = "施工经理" });
           // approveManModels.Add(new ApproveManModel { Number = 2, userid = table.ProjectManager, Rolename = "项目经理" });
            approveManModels.Add(new ApproveManModel { Number = 2, userid = table.Approval_Construction, Rolename = "施工管理部" });
            //approveManModels.Add(new ApproveManModel { Number = 4, userid = table.DeputyGeneralManager, Rolename = "分管副总经理" });
             return approveManModels;
        }

        public static void InitGetBidCompleteDropDownList(FineUIPro.DropDownList dropName, bool isShowPlease)
        {
            dropName.DataValueField = "ApproveUserReviewID";
            dropName.DataTextField = "BidDocumentsCode";
            dropName.DataSource = GetCompleteBidDocument();
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }

        public static object GetCompleteBidDocument()
        {
            var list = (from x in Funs.DB.PHTGL_BidApproveUserReview
                        join y in Funs.DB.PHTGL_BidDocumentsReview on x.BidDocumentsReviewId equals y.BidDocumentsReviewId
                        where x.State == Const.ContractReview_Complete
                        select new
                        {
                            x.ApproveUserReviewID,
                            y.BidDocumentsCode
                        });
            return list;
        }


    }
}