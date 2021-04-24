using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{

    public static class PHTGL_AttachUrl15_Sch3Service
    {

        public static Model.PHTGL_AttachUrl15_Sch3 GetPHTGL_AttachUrl15_Sch3ById(string AttachUrlId)
        {
            return Funs.DB.PHTGL_AttachUrl15_Sch3.FirstOrDefault(e => e.AttachUrlId == AttachUrlId);
        }

        public static List<Model.PHTGL_AttachUrl15_Sch3> GetPHTGL_AttachUrl15ByAttachUrlId(string attachUrlId)
        {
            return (from x in Funs.DB.PHTGL_AttachUrl15_Sch3 where x.AttachUrlId == attachUrlId select x).ToList();
        }

        public static void AddPHTGL_AttachUrl15_Sch3(Model.PHTGL_AttachUrl15_Sch3 newtable)
        {
            Model.PHTGL_AttachUrl15_Sch3 table = new Model.PHTGL_AttachUrl15_Sch3();
            table.AttachUrlItemId = newtable.AttachUrlItemId;
            table.Elemeter_Start = newtable.Elemeter_Start;
            table.Elemeter_End = newtable.Elemeter_End;
            table.Elemeter_Read = newtable.Elemeter_Read;
            table.GeneralContractorName = newtable.GeneralContractorName;
            table.SubcontractorsName = newtable.SubcontractorsName;
            table.Remark = newtable.Remark;
            table.AttachUrlId = newtable.AttachUrlId;
            table.AttachUrlContent = newtable.AttachUrlContent;
            table.SerialNumber = newtable.SerialNumber;
            table.StartTime = newtable.StartTime;
            table.Endtime = newtable.Endtime;
            table.Watermeter_Start = newtable.Watermeter_Start;
            table.Watermeter_End = newtable.Watermeter_End;
            table.Watermeter_Read = newtable.Watermeter_Read;
            Funs.DB.PHTGL_AttachUrl15_Sch3.InsertOnSubmit(table);
            Funs.DB.SubmitChanges();
        }


        public static void UpdatePHTGL_AttachUrl15_Sch3(Model.PHTGL_AttachUrl15_Sch3 newtable)
        {
            Model.PHTGL_AttachUrl15_Sch3 table = Funs.DB.PHTGL_AttachUrl15_Sch3.FirstOrDefault(e => e.AttachUrlItemId == newtable.AttachUrlItemId
);

            if (table != null)
            {
                table.AttachUrlItemId = newtable.AttachUrlItemId;
                table.Elemeter_Start = newtable.Elemeter_Start;
                table.Elemeter_End = newtable.Elemeter_End;
                table.Elemeter_Read = newtable.Elemeter_Read;
                table.GeneralContractorName = newtable.GeneralContractorName;
                table.SubcontractorsName = newtable.SubcontractorsName;
                table.Remark = newtable.Remark;
                table.AttachUrlId = newtable.AttachUrlId;
                table.AttachUrlContent = newtable.AttachUrlContent;
                table.SerialNumber = newtable.SerialNumber;
                table.StartTime = newtable.StartTime;
                table.Endtime = newtable.Endtime;
                table.Watermeter_Start = newtable.Watermeter_Start;
                table.Watermeter_End = newtable.Watermeter_End;
                table.Watermeter_Read = newtable.Watermeter_Read;
                Funs.DB.SubmitChanges();
            }

        }
        public static void DeleteAttachUrl15_Sch3ByAttachUrlId(string attachUrlId)
        {
            var q = (from x in Funs.DB.PHTGL_AttachUrl15_Sch3 where x.AttachUrlId == attachUrlId select x).ToList();
            if (q != null)
            {
                Funs.DB.PHTGL_AttachUrl15_Sch3.DeleteAllOnSubmit(q);
                Funs.DB.SubmitChanges();
            }
        }
    }
}
