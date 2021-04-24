using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{

    public static class PHTGL_AttachUrl15_Sch2Service
    {

        public static Model.PHTGL_AttachUrl15_Sch2 GetPHTGL_AttachUrl15_Sch2ById(string AttachUrlId
)

        {
            return Funs.DB.PHTGL_AttachUrl15_Sch2.FirstOrDefault(e => e.AttachUrlId == AttachUrlId
);
        }


        public static void AddPHTGL_AttachUrl15_Sch2(Model.PHTGL_AttachUrl15_Sch2 newtable)
        {
            Model.PHTGL_AttachUrl15_Sch2 table = new Model.PHTGL_AttachUrl15_Sch2();
            table.AttachUrlItemId = newtable.AttachUrlItemId;
            table.ElemeterPosition = newtable.ElemeterPosition;
            table.WatermeterPosition = newtable.WatermeterPosition;
            table.ElemeterRead = newtable.ElemeterRead;
            table.WatermeterRead = newtable.WatermeterRead;
            table.IsApproval = newtable.IsApproval;
            table.IsLineLayout = newtable.IsLineLayout;
            table.IsPowerBox = newtable.IsPowerBox;
            table.IsProfessional_ele = newtable.IsProfessional_ele;
            table.IsLineInstall = newtable.IsLineInstall;
            table.IsValve = newtable.IsValve;
            table.AttachUrlId = newtable.AttachUrlId;
            table.Terminalnumber = newtable.Terminalnumber;
            table.LineCabinetNumber = newtable.LineCabinetNumber;
            table.ElectricPrice = newtable.ElectricPrice;
            table.WaterPrice = newtable.WaterPrice;
            table.AttachUrlContent = newtable.AttachUrlContent;
            table.ProjectName = newtable.ProjectName;
            table.ContractId = newtable.ContractId;
            table.Company = newtable.Company;
            table.ConstructionTask = newtable.ConstructionTask;
            table.Maxcapacitance = newtable.Maxcapacitance;
            table.MaxuseWtater = newtable.MaxuseWtater;
            Funs.DB.PHTGL_AttachUrl15_Sch2.InsertOnSubmit(table);
            Funs.DB.SubmitChanges();
        }


        public static void UpdatePHTGL_AttachUrl15_Sch2(Model.PHTGL_AttachUrl15_Sch2 newtable)
        {
            Model.PHTGL_AttachUrl15_Sch2 table = Funs.DB.PHTGL_AttachUrl15_Sch2.FirstOrDefault(e => e.AttachUrlItemId == newtable.AttachUrlItemId
);

            if (table != null)
            {
                table.AttachUrlItemId = newtable.AttachUrlItemId;
                table.ElemeterPosition = newtable.ElemeterPosition;
                table.WatermeterPosition = newtable.WatermeterPosition;
                table.ElemeterRead = newtable.ElemeterRead;
                table.WatermeterRead = newtable.WatermeterRead;
                table.IsApproval = newtable.IsApproval;
                table.IsLineLayout = newtable.IsLineLayout;
                table.IsPowerBox = newtable.IsPowerBox;
                table.IsProfessional_ele = newtable.IsProfessional_ele;
                table.IsLineInstall = newtable.IsLineInstall;
                table.IsValve = newtable.IsValve;
                table.AttachUrlId = newtable.AttachUrlId;
                table.Terminalnumber = newtable.Terminalnumber;
                table.LineCabinetNumber = newtable.LineCabinetNumber;
                table.ElectricPrice = newtable.ElectricPrice;
                table.WaterPrice = newtable.WaterPrice;
                table.AttachUrlContent = newtable.AttachUrlContent;
                table.ProjectName = newtable.ProjectName;
                table.ContractId = newtable.ContractId;
                table.Company = newtable.Company;
                table.ConstructionTask = newtable.ConstructionTask;
                table.Maxcapacitance = newtable.Maxcapacitance;
                table.MaxuseWtater = newtable.MaxuseWtater;
                Funs.DB.SubmitChanges();
            }

        }
        public static void DeleteAttachUrl15_Sch2ByAttachUrlId(string attachUrlId)
        {
            var q = (from x in Funs.DB.PHTGL_AttachUrl15_Sch2 where x.AttachUrlId == attachUrlId select x).ToList();
            if (q != null)
            {
                Funs.DB.PHTGL_AttachUrl15_Sch2.DeleteAllOnSubmit(q);
                Funs.DB.SubmitChanges();
            }
        }
    }
}
