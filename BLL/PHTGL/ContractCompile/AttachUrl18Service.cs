using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{

    public static class AttachUrl18Service
    {

        public static Model.PHTGL_AttachUrl18 GetPHTGL_AttachUrl18ById(string AttachUrlId)

        {
            return Funs.DB.PHTGL_AttachUrl18.FirstOrDefault(e => e.AttachUrlId == AttachUrlId);
        }


        public static void AddPHTGL_AttachUrl18(Model.PHTGL_AttachUrl18 newtable)
        {
            Model.PHTGL_AttachUrl18 table = new Model.PHTGL_AttachUrl18();
            table.AttachUrlItemId = newtable.AttachUrlItemId;
            table.PersonSum = newtable.PersonSum;
            table.AttachUrlId = newtable.AttachUrlId;
            table.AttachUrlContent = newtable.AttachUrlContent;
            table.GeneralContractorName = newtable.GeneralContractorName;
            table.SubcontractorsName = newtable.SubcontractorsName;
            table.ProjectName = newtable.ProjectName;
            table.ContractId = newtable.ContractId;
            table.StartDate = newtable.StartDate;
            table.EndDate = newtable.EndDate;
            Funs.DB.PHTGL_AttachUrl18.InsertOnSubmit(table);
            Funs.DB.SubmitChanges();
        }


        public static void UpdatePHTGL_AttachUrl18(Model.PHTGL_AttachUrl18 newtable)
        {
            Model.PHTGL_AttachUrl18 table = Funs.DB.PHTGL_AttachUrl18.FirstOrDefault(e => e.AttachUrlId == newtable.AttachUrlId);

            if (table != null)
            {
                table.AttachUrlItemId = newtable.AttachUrlItemId;
                table.PersonSum = newtable.PersonSum;
                table.AttachUrlId = newtable.AttachUrlId;
                table.AttachUrlContent = newtable.AttachUrlContent;
                table.GeneralContractorName = newtable.GeneralContractorName;
                table.SubcontractorsName = newtable.SubcontractorsName;
                table.ProjectName = newtable.ProjectName;
                table.ContractId = newtable.ContractId;
                table.StartDate = newtable.StartDate;
                table.EndDate = newtable.EndDate;
                Funs.DB.SubmitChanges();
            }

        }
    }
}
