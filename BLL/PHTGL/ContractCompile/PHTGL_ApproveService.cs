using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{

    public static class PHTGL_ApproveService
    {

 

        public static Model.PHTGL_Approve GetPHTGL_ApproveById(string ApproveId)

        {
              
            return Funs.DB.PHTGL_Approve.FirstOrDefault(e => e.ApproveId == ApproveId);
        }


        public static Model.PHTGL_Approve GetPHTGL_ApproveByContractId(string contractId)

        {
            return Funs.DB.PHTGL_Approve.FirstOrDefault(e => e.ContractId == contractId);
        }
        public static Model.PHTGL_Approve GetPHTGL_ApproveByContractIdandUserId(string contractId,string  UserID)

        {
            return Funs.DB.PHTGL_Approve.FirstOrDefault(e => e.ContractId == contractId&&e.ApproveMan==UserID);
        }

        public static Model.PHTGL_Approve GetPHTGL_ApproveByContractIdAndType(string contractId,string approveType)

        {
            return Funs.DB.PHTGL_Approve.FirstOrDefault(e => e.ContractId == contractId&&e.ApproveType==approveType);
        }

        public static Model.PHTGL_Approve GetPHTGL_ApproveByUserId(string contractId,string approveMan)

        {
            return Funs.DB.PHTGL_Approve.FirstOrDefault(e => e.ContractId == contractId &&e.ApproveMan== approveMan);
        }

        public static void AddPHTGL_Approve(Model.PHTGL_Approve newtable)
        {
            Model.PHTGL_Approve table = new Model.PHTGL_Approve();
            table.ApproveId = newtable.ApproveId;
            table.ContractId = newtable.ContractId;
            table.ApproveMan = newtable.ApproveMan;
            table.ApproveDate = newtable.ApproveDate;
            table.State = newtable.State;
            table.IsAgree = newtable.IsAgree;
            table.ApproveIdea = newtable.ApproveIdea;
            table.ApproveType = newtable.ApproveType;
            table.ApproveForm = newtable.ApproveForm;
            Funs.DB.PHTGL_Approve.InsertOnSubmit(table);
            Funs.DB.SubmitChanges();
        }


        public static void UpdatePHTGL_Approve(Model.PHTGL_Approve newtable)
        {
            Model.PHTGL_Approve table = Funs.DB.PHTGL_Approve.FirstOrDefault(e => e.ApproveId == newtable.ApproveId);

            if (table != null)
            {
                table.ApproveId = newtable.ApproveId;
                table.ContractId = newtable.ContractId;
                table.ApproveMan = newtable.ApproveMan;
                table.ApproveDate = newtable.ApproveDate;
                table.State = newtable.State;
                table.IsAgree = newtable.IsAgree;
                table.ApproveIdea = newtable.ApproveIdea;
                table.ApproveType = newtable.ApproveType;
                table.ApproveForm = newtable.ApproveForm;
                Funs.DB.SubmitChanges();
            }

        }

        /// <summary>
        ///  
        /// </summary>
        /// <param name="contractId"></param>
        public static void DeletePHTGL_ApproveBycontractId(string contractId)
        {
             var q = (from x in Funs.DB.PHTGL_Approve where x.ContractId == contractId select x).ToList();
            if (q != null)
            {
                Funs.DB.PHTGL_Approve.DeleteAllOnSubmit(q);
                Funs.DB.SubmitChanges();
            }
        }
    }
}
