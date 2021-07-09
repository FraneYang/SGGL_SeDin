using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{

    public static class PHTGL_SetSubReview_Sch2Service
    {

        public static Model.PHTGL_SetSubReview_Sch2 GetPHTGL_SetSubReview_Sch2ById(string ID)
        {
            return Funs.DB.PHTGL_SetSubReview_Sch2.FirstOrDefault(e => e.ID == ID);
        }

        public static Model.PHTGL_SetSubReview_Sch2 GetPHTGL_SetSubReview_Sch2BySetSubReviewID(string setSubReviewID)
        {
            return Funs.DB.PHTGL_SetSubReview_Sch2.FirstOrDefault(e => e.SetSubReviewID == setSubReviewID);
        }
        public static void AddPHTGL_SetSubReview_Sch2(Model.PHTGL_SetSubReview_Sch2 newtable)
        {
            Model.PHTGL_SetSubReview_Sch2 table = new Model.PHTGL_SetSubReview_Sch2();
            table.ID = newtable.ID;
            table.SetSubReviewID = newtable.SetSubReviewID;
            table.Company = newtable.Company;
            table.Price_ReviewResults = newtable.Price_ReviewResults;
            table.Skill_ReviewResults = newtable.Skill_ReviewResults;
            table.Business_ReviewResults = newtable.Business_ReviewResults;
            table.Synthesize_ReviewResults = newtable.Synthesize_ReviewResults;
            table.Remarks = newtable.Remarks;
            table.SortIndex = newtable.SortIndex;
            Funs.DB.PHTGL_SetSubReview_Sch2.InsertOnSubmit(table);
            Funs.DB.SubmitChanges();
        }


        public static void UpdatePHTGL_SetSubReview_Sch2(Model.PHTGL_SetSubReview_Sch2 newtable)
        {
            Model.PHTGL_SetSubReview_Sch2 table = Funs.DB.PHTGL_SetSubReview_Sch2.FirstOrDefault(e => e.ID == newtable.ID
);

            if (table != null)
            {
                table.ID = newtable.ID;
                table.SetSubReviewID = newtable.SetSubReviewID;
                table.Company = newtable.Company;
                table.Price_ReviewResults = newtable.Price_ReviewResults;
                table.Skill_ReviewResults = newtable.Skill_ReviewResults;
                table.Business_ReviewResults = newtable.Business_ReviewResults;
                table.Synthesize_ReviewResults = newtable.Synthesize_ReviewResults;
                table.Remarks = newtable.Remarks;
                table.SortIndex = newtable.SortIndex;
                Funs.DB.SubmitChanges();
            }

        }
        public static void DeletePHTGL_SetSubReview_Sch2ById(string ID)
        {
            Model.PHTGL_SetSubReview_Sch2 table = Funs.DB.PHTGL_SetSubReview_Sch2.FirstOrDefault(e => e.ID == ID);
            if (table != null)
            {
                Funs.DB.PHTGL_SetSubReview_Sch2.DeleteOnSubmit(table);
                Funs.DB.SubmitChanges();
            }

        }

        public static void DeletePHTGL_SetSubReview_Sch2BySetSubReviewID(string setSubReviewID)
        {
            var table = (from x in Funs.DB.PHTGL_SetSubReview_Sch2 where x.SetSubReviewID == setSubReviewID select x).ToList();
            if (table != null)
            {
                Funs.DB.PHTGL_SetSubReview_Sch2.DeleteAllOnSubmit(table);
                Funs.DB.SubmitChanges();
            }
        }

    }
}