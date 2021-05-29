using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{

    public static class PHTGL_SetSubReview_Sch1Service
    {

        public static Model.PHTGL_SetSubReview_Sch1 GetPHTGL_SetSubReview_Sch1ById(string ID
)

        {
            return Funs.DB.PHTGL_SetSubReview_Sch1.FirstOrDefault(e => e.ID == ID
);
        }


        public static void AddPHTGL_SetSubReview_Sch1(Model.PHTGL_SetSubReview_Sch1 newtable)
        {
            Model.PHTGL_SetSubReview_Sch1 table = new Model.PHTGL_SetSubReview_Sch1();
            table.ID = newtable.ID;
            table.SetSubReviewID = newtable.SetSubReviewID;
            table.Company = newtable.Company;
            table.ReviewResults = newtable.ReviewResults;
            table.Remarks = newtable.Remarks;
            Funs.DB.PHTGL_SetSubReview_Sch1.InsertOnSubmit(table);
            Funs.DB.SubmitChanges();
        }


        public static void UpdatePHTGL_SetSubReview_Sch1(Model.PHTGL_SetSubReview_Sch1 newtable)
        {
            Model.PHTGL_SetSubReview_Sch1 table = Funs.DB.PHTGL_SetSubReview_Sch1.FirstOrDefault(e => e.ID == newtable.ID
);

            if (table != null)
            {
                table.ID = newtable.ID;
                table.SetSubReviewID = newtable.SetSubReviewID;
                table.Company = newtable.Company;
                table.ReviewResults = newtable.ReviewResults;
                table.Remarks = newtable.Remarks;
                Funs.DB.SubmitChanges();
            }

        }
        public static void DeletePHTGL_SetSubReview_Sch1ById(string ID)
        {
            Model.PHTGL_SetSubReview_Sch1 table = Funs.DB.PHTGL_SetSubReview_Sch1.FirstOrDefault(e => e.ID == ID);
            if (table != null)
            {
                Funs.DB.PHTGL_SetSubReview_Sch1.DeleteOnSubmit(table);
                Funs.DB.SubmitChanges();
            }

        }
        public static void DeletePHTGL_SetSubReview_Sch1BySetSubReviewID(string setSubReviewID)
        {
            var table = (from x in Funs.DB.PHTGL_SetSubReview_Sch1 where x.SetSubReviewID == setSubReviewID select x).ToList();
            if (table != null)
            {
                Funs.DB.PHTGL_SetSubReview_Sch1.DeleteAllOnSubmit(table);
                Funs.DB.SubmitChanges();
            }
        }

    }
}