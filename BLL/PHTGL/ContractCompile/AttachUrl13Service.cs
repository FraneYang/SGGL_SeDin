using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{

    public static class AttachUrl13Service
    {

        public static Model.PHTGL_AttachUrl13 GetPHTGL_AttachUrl13ById(string AttachUrlId)

        {
            return Funs.DB.PHTGL_AttachUrl13.FirstOrDefault(e => e.AttachUrlId == AttachUrlId);
        }


        public static void AddPHTGL_AttachUrl13(Model.PHTGL_AttachUrl13 newtable)
        {
            Model.PHTGL_AttachUrl13 table = new Model.PHTGL_AttachUrl13();
            table.AttachUrlItemId = newtable.AttachUrlItemId;
            table.DefectLiabilityDate = newtable.DefectLiabilityDate;
            table.DefectLiabilityPeriod = newtable.DefectLiabilityPeriod;
            table.OtherqualityWarranty = newtable.OtherqualityWarranty;
            table.AttachUrlId = newtable.AttachUrlId;
            table.AttachUrlContent = newtable.AttachUrlContent;
            table.GeneralContractorName = newtable.GeneralContractorName;
            table.SubcontractorsName = newtable.SubcontractorsName;
            table.ProjectName = newtable.ProjectName;
            table.WarrantyContent = newtable.WarrantyContent;
            table.OtherWarrantyPeriod = newtable.OtherWarrantyPeriod;
            table.WarrantyPeriodDate = newtable.WarrantyPeriodDate;
            Funs.DB.PHTGL_AttachUrl13.InsertOnSubmit(table);
            Funs.DB.SubmitChanges();
        }


        public static void UpdatePHTGL_AttachUrl13(Model.PHTGL_AttachUrl13 newtable)
        {
            Model.PHTGL_AttachUrl13 table = Funs.DB.PHTGL_AttachUrl13.FirstOrDefault(e => e.AttachUrlItemId == newtable.AttachUrlItemId);

            if (table != null)
            {
                table.AttachUrlItemId = newtable.AttachUrlItemId;
                table.DefectLiabilityDate = newtable.DefectLiabilityDate;
                table.DefectLiabilityPeriod = newtable.DefectLiabilityPeriod;
                table.OtherqualityWarranty = newtable.OtherqualityWarranty;
                table.AttachUrlId = newtable.AttachUrlId;
                table.AttachUrlContent = newtable.AttachUrlContent;
                table.GeneralContractorName = newtable.GeneralContractorName;
                table.SubcontractorsName = newtable.SubcontractorsName;
                table.ProjectName = newtable.ProjectName;
                table.WarrantyContent = newtable.WarrantyContent;
                table.OtherWarrantyPeriod = newtable.OtherWarrantyPeriod;
                table.WarrantyPeriodDate = newtable.WarrantyPeriodDate;
                Funs.DB.SubmitChanges();
            }

        }
    }
}
