using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{

    public static class PHTGL_AttachUrl15_Sch4Service
    {

        public static Model.PHTGL_AttachUrl15_Sch4 GetPHTGL_AttachUrl15_Sch4ById(string AttachUrlId
)

        {
            return Funs.DB.PHTGL_AttachUrl15_Sch4.FirstOrDefault(e => e.AttachUrlId == AttachUrlId
);
        }


        public static void AddPHTGL_AttachUrl15_Sch4(Model.PHTGL_AttachUrl15_Sch4 newtable)
        {
            Model.PHTGL_AttachUrl15_Sch4 table = new Model.PHTGL_AttachUrl15_Sch4();
            table.AttachUrlItemId = newtable.AttachUrlItemId;
            table.Position = newtable.Position;
            table.ImpPlan = newtable.ImpPlan;
            table.Recoverymeasures = newtable.Recoverymeasures;
            table.Caption = newtable.Caption;
            table.AttachUrlId = newtable.AttachUrlId;
            table.AttachUrlContent = newtable.AttachUrlContent;
            table.ProjectName = newtable.ProjectName;
            table.ContractId = newtable.ContractId;
            table.SubcontractorsName = newtable.SubcontractorsName;
            table.Type = newtable.Type;
            table.Time = newtable.Time;
            table.Reason = newtable.Reason;
            Funs.DB.PHTGL_AttachUrl15_Sch4.InsertOnSubmit(table);
            Funs.DB.SubmitChanges();
        }


        public static void UpdatePHTGL_AttachUrl15_Sch4(Model.PHTGL_AttachUrl15_Sch4 newtable)
        {
            Model.PHTGL_AttachUrl15_Sch4 table = Funs.DB.PHTGL_AttachUrl15_Sch4.FirstOrDefault(e => e.AttachUrlItemId == newtable.AttachUrlItemId
);

            if (table != null)
            {
                table.AttachUrlItemId = newtable.AttachUrlItemId;
                table.Position = newtable.Position;
                table.ImpPlan = newtable.ImpPlan;
                table.Recoverymeasures = newtable.Recoverymeasures;
                table.Caption = newtable.Caption;
                table.AttachUrlId = newtable.AttachUrlId;
                table.AttachUrlContent = newtable.AttachUrlContent;
                table.ProjectName = newtable.ProjectName;
                table.ContractId = newtable.ContractId;
                table.SubcontractorsName = newtable.SubcontractorsName;
                table.Type = newtable.Type;
                table.Time = newtable.Time;
                table.Reason = newtable.Reason;
                Funs.DB.SubmitChanges();
            }

        }
        public static void DeleteAttachUrl15_Sch4ByAttachUrlId(string attachUrlId)
        {
            var q = (from x in Funs.DB.PHTGL_AttachUrl15_Sch4 where x.AttachUrlId == attachUrlId select x).ToList();
            if (q != null)
            {
                Funs.DB.PHTGL_AttachUrl15_Sch4.DeleteAllOnSubmit(q);
                Funs.DB.SubmitChanges();
            }
        }
    }
}
