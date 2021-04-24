using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
 
    public static class AttachUrl12Service
    {
 
        public static Model.PHTGL_AttachUrl12 GetPHTGL_AttachUrl12ById ( string AttachUrlId)
 
        {
            return Funs.DB.PHTGL_AttachUrl12.FirstOrDefault(e=>e.AttachUrlId == AttachUrlId);
        }

 
        public static void AddPHTGL_AttachUrl12(Model.PHTGL_AttachUrl12 newtable)
        {
                Model.PHTGL_AttachUrl12 table = new Model.PHTGL_AttachUrl12();
            	table.AttachUrlItemId=newtable.AttachUrlItemId;
	            table.AttachUrlId=newtable.AttachUrlId;
	            table.AttachUrlContent=newtable.AttachUrlContent;
                Funs.DB.PHTGL_AttachUrl12.InsertOnSubmit(table);
                Funs.DB.SubmitChanges();
        }

 
        public static void UpdatePHTGL_AttachUrl12(Model.PHTGL_AttachUrl12 newtable)
        {
            Model.PHTGL_AttachUrl12 table = Funs.DB.PHTGL_AttachUrl12.FirstOrDefault(e=>e.AttachUrlItemId==newtable.AttachUrlItemId);
       
            if (table != null)
            {
                     table.AttachUrlItemId=newtable.AttachUrlItemId;
                     table.AttachUrlId=newtable.AttachUrlId;
                     table.AttachUrlContent=newtable.AttachUrlContent;
                     Funs.DB.SubmitChanges();
            }
            
        }
    }
}
