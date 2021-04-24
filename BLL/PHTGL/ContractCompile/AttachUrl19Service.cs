using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{

    public static class AttachUrl19Service
    {

        public static Model.PHTGL_AttachUrl19 GetPHTGL_AttachUrl19ById(string attachUrlId)
        {
            return Funs.DB.PHTGL_AttachUrl19.FirstOrDefault(e => e.AttachUrlId == attachUrlId);
        }


        public static void AddPHTGL_AttachUrl19(Model.PHTGL_AttachUrl19 newtable)
        {
            Model.PHTGL_AttachUrl19 table = new Model.PHTGL_AttachUrl19();
            table.AttachUrlItemId = newtable.AttachUrlItemId;
            table.AttachUrlId = newtable.AttachUrlId;
            table.AttachUrlContent = newtable.AttachUrlContent;
            Funs.DB.PHTGL_AttachUrl19.InsertOnSubmit(table);
            Funs.DB.SubmitChanges();
        }


        public static void UpdatePHTGL_AttachUrl19(Model.PHTGL_AttachUrl19 newtable)
        {
            Model.PHTGL_AttachUrl19 table = Funs.DB.PHTGL_AttachUrl19.FirstOrDefault(e => e.AttachUrlId == newtable.AttachUrlId);

            if (table != null)
            {
                table.AttachUrlItemId = newtable.AttachUrlItemId;
                table.AttachUrlId = newtable.AttachUrlId;
                table.AttachUrlContent = newtable.AttachUrlContent;
                Funs.DB.SubmitChanges();
            }

        }
    }
}
