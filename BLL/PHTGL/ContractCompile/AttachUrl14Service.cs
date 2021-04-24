using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{

    public static class AttachUrl14Service
    {

        public static Model.PHTGL_AttachUrl14 GetPHTGL_AttachUrl14ById(string AttachUrlId)

        {
            return Funs.DB.PHTGL_AttachUrl14.FirstOrDefault(e => e.AttachUrlId == AttachUrlId);
        }


        public static void AddPHTGL_AttachUrl14(Model.PHTGL_AttachUrl14 newtable)
        {
            Model.PHTGL_AttachUrl14 table = new Model.PHTGL_AttachUrl14();
            table.AttachUrlItemId = newtable.AttachUrlItemId;
            table.AttachUrlId = newtable.AttachUrlId;
            table.AttachUrlContent = newtable.AttachUrlContent;
            table.ProjectName = newtable.ProjectName;
            table.PersonAmount = newtable.PersonAmount;
            table.SafetyManagerNumber = newtable.SafetyManagerNumber;
            table.SystemManagerNumber = newtable.SystemManagerNumber;
            Funs.DB.PHTGL_AttachUrl14.InsertOnSubmit(table);
            Funs.DB.SubmitChanges();
        }


        public static void UpdatePHTGL_AttachUrl14(Model.PHTGL_AttachUrl14 newtable)
        {
            Model.PHTGL_AttachUrl14 table = Funs.DB.PHTGL_AttachUrl14.FirstOrDefault(e => e.AttachUrlId == newtable.AttachUrlId);

            if (table != null)
            {
                table.AttachUrlItemId = newtable.AttachUrlItemId;
                table.AttachUrlId = newtable.AttachUrlId;
                table.AttachUrlContent = newtable.AttachUrlContent;
                table.ProjectName = newtable.ProjectName;
                table.PersonAmount = newtable.PersonAmount;
                table.SafetyManagerNumber = newtable.SafetyManagerNumber;
                table.SystemManagerNumber = newtable.SystemManagerNumber;
                Funs.DB.SubmitChanges();
            }

        }
    }
}
