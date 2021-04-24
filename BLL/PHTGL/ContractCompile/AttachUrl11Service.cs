using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{

    public static class AttachUrl11Service
    {

        public static Model.PHTGL_AttachUrl11 GetPHTGL_AttachUrl11ById(string AttachUrlId)

        {
            return Funs.DB.PHTGL_AttachUrl11.FirstOrDefault(e => e.AttachUrlId == AttachUrlId);
        }


        public static void AddPHTGL_AttachUrl11(Model.PHTGL_AttachUrl11 newtable)
        {
            Model.PHTGL_AttachUrl11 table = new Model.PHTGL_AttachUrl11();
            table.AttachUrlItemId = newtable.AttachUrlItemId;
            table.AttachUrlId = newtable.AttachUrlId;
            table.AttachUrlContent = newtable.AttachUrlContent;
            Funs.DB.PHTGL_AttachUrl11.InsertOnSubmit(table);
            Funs.DB.SubmitChanges();
        }


        public static void UpdatePHTGL_AttachUrl11(Model.PHTGL_AttachUrl11 newtable)
        {
            Model.PHTGL_AttachUrl11 table = Funs.DB.PHTGL_AttachUrl11.FirstOrDefault(e => e.AttachUrlId == newtable.AttachUrlId
);

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
