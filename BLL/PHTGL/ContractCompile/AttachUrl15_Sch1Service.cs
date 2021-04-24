using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{

    public static class PHTGL_AttachUrl15_Sch1Service
    {

        public static Model.PHTGL_AttachUrl15_Sch1 GetPHTGL_AttachUrl15_Sch1ById(string AttachUrlId)
        {
            return Funs.DB.PHTGL_AttachUrl15_Sch1.FirstOrDefault(e => e.AttachUrlId == AttachUrlId);
        }

        public static List<Model.PHTGL_AttachUrl15_Sch1> GetPHTGL_AttachUrl15ByAttachUrlId(string attachUrlId)
        {
            return (from x in Funs.DB.PHTGL_AttachUrl15_Sch1 where x.AttachUrlId == attachUrlId select x).ToList();
        }

        public static void AddPHTGL_AttachUrl15_Sch1(Model.PHTGL_AttachUrl15_Sch1 newtable)
        {
            Model.PHTGL_AttachUrl15_Sch1 table = new Model.PHTGL_AttachUrl15_Sch1();
            table.AttachUrlItemId = newtable.AttachUrlItemId;
            table.AttachUrlId = newtable.AttachUrlId;
            table.AttachUrlContent = newtable.AttachUrlContent;
            table.ProjectName = newtable.ProjectName;
            table.ContractId = newtable.ContractId;
            table.OrderNumber = newtable.OrderNumber;
            table.Type = newtable.Type;
            table.MainPoints = newtable.MainPoints;
            table.Opinion = newtable.Opinion;
            Funs.DB.PHTGL_AttachUrl15_Sch1.InsertOnSubmit(table);
            Funs.DB.SubmitChanges();
        }


        public static void UpdatePHTGL_AttachUrl15_Sch1(Model.PHTGL_AttachUrl15_Sch1 newtable)
        {
            Model.PHTGL_AttachUrl15_Sch1 table = Funs.DB.PHTGL_AttachUrl15_Sch1.FirstOrDefault(e => e.AttachUrlItemId == newtable.AttachUrlItemId);

            if (table != null)
            {
                table.AttachUrlItemId = newtable.AttachUrlItemId;
                table.AttachUrlId = newtable.AttachUrlId;
                table.AttachUrlContent = newtable.AttachUrlContent;
                table.ProjectName = newtable.ProjectName;
                table.ContractId = newtable.ContractId;
                table.OrderNumber = newtable.OrderNumber;
                table.Type = newtable.Type;
                table.MainPoints = newtable.MainPoints;
                table.Opinion = newtable.Opinion;
                Funs.DB.SubmitChanges();
            }

        }
        public static void DeleteAttachUrl15_Sch1ByAttachUrlId(string attachUrlId)
        {
            var q = (from x in Funs.DB.PHTGL_AttachUrl15_Sch1 where x.AttachUrlId == attachUrlId select x).ToList();
            if (q != null)
            {
                Funs.DB.PHTGL_AttachUrl15_Sch1.DeleteAllOnSubmit(q);
                Funs.DB.SubmitChanges();
            }
        }
    }
}
