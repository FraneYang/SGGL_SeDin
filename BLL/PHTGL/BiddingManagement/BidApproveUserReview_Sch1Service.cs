using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{

    public static class PHTGL_BidApproveUserReview_Sch1Service
    {

        public static Model.PHTGL_BidApproveUserReview_Sch1 GetPHTGL_BidApproveUserReview_Sch1ById(string ID)

        {
            return Funs.DB.PHTGL_BidApproveUserReview_Sch1.FirstOrDefault(e => e.ID == ID);
        }

        public static Model.PHTGL_BidApproveUserReview_Sch1 GetPHTGL_BidApproveUserReview_Sch1ByReviewID(string approveUserReviewID)

        {
            return Funs.DB.PHTGL_BidApproveUserReview_Sch1.FirstOrDefault(e => e.ApproveUserReviewID == approveUserReviewID);
        }


        public static void AddPHTGL_BidApproveUserReview_Sch1(Model.PHTGL_BidApproveUserReview_Sch1 newtable)
        {
            Model.PHTGL_BidApproveUserReview_Sch1 table = new Model.PHTGL_BidApproveUserReview_Sch1();
            table.ID = newtable.ID;
            table.ApproveUserReviewID = newtable.ApproveUserReviewID;
            table.ApproveUserName = newtable.ApproveUserName;
            table.ApproveUserSpecial = newtable.ApproveUserSpecial;
            table.ApproveUserUnit = newtable.ApproveUserUnit;
            table.Remarks = newtable.Remarks;
            Funs.DB.PHTGL_BidApproveUserReview_Sch1.InsertOnSubmit(table);
            Funs.DB.SubmitChanges();
        }


        public static void UpdatePHTGL_BidApproveUserReview_Sch1(Model.PHTGL_BidApproveUserReview_Sch1 newtable)
        {
            Model.PHTGL_BidApproveUserReview_Sch1 table = Funs.DB.PHTGL_BidApproveUserReview_Sch1.FirstOrDefault(e => e.ID == newtable.ID
);

            if (table != null)
            {
                table.ID = newtable.ID;
                table.ApproveUserReviewID = newtable.ApproveUserReviewID;
                table.ApproveUserName = newtable.ApproveUserName;
                table.ApproveUserSpecial = newtable.ApproveUserSpecial;
                table.ApproveUserUnit = newtable.ApproveUserUnit;
                table.Remarks = newtable.Remarks;
                Funs.DB.SubmitChanges();
            }

        }
        public static void DeletePHTGL_BidApproveUserReview_Sch1ById(string ID)
        {
            Model.PHTGL_BidApproveUserReview_Sch1 table = Funs.DB.PHTGL_BidApproveUserReview_Sch1.FirstOrDefault(e => e.ID == ID);
            if (table != null)
            {
                Funs.DB.PHTGL_BidApproveUserReview_Sch1.DeleteOnSubmit(table);
                Funs.DB.SubmitChanges();
            }

        }
        public static void DeletePHTGL_BidApproveUserReview_Sch1ByReviewID(string approveUserReviewID)
        {
            var table = (from x in Funs.DB.PHTGL_BidApproveUserReview_Sch1 where x.ApproveUserReviewID == approveUserReviewID select x).ToList();
            if (table != null)
            {
                Funs.DB.PHTGL_BidApproveUserReview_Sch1.DeleteAllOnSubmit(table);
                Funs.DB.SubmitChanges();
            }

        }

    }
}