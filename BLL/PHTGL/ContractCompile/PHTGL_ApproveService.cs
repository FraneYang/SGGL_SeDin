using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{

    public static class PHTGL_ApproveService
    {
        public const string ActionPlanReview= "ActionPlanReview";
        public const string ApproveUserReview = "ApproveUserReview";
        public const string BidDocumentsReview = "BidDocumentsReview";
        public const string SetSubReview = "SetSubReview";
        public const string ContractReview = "ContractReview";
 
        public static Model.PHTGL_Approve GetPHTGL_ApproveById(string ApproveId)
        {
              
            return Funs.DB.PHTGL_Approve.FirstOrDefault(e => e.ApproveId == ApproveId);
        }


        public static List<Model.PHTGL_Approve> GetPHTGL_ApproveByContractId(string contractId)
        {
            var q = (from x in Funs.DB.PHTGL_Approve where x.ContractId == contractId && x.State==0  select x).ToList();

            return q ;
        }
        /// <summary>
        ///获取当前人员的所有审批信息
        /// </summary>
        /// <param name="contractId"></param>
        /// <param name="approveMan"></param>
        /// <returns></returns>
        public static List<Model.PHTGL_Approve> GetListPHTGL_ApproveByUserId(string contractId, string approveMan)
        {
            var q = (from x in Funs.DB.PHTGL_Approve where x.ContractId == contractId && x.ApproveMan == approveMan select x).ToList();

            return q;
        }
        public static Model.PHTGL_Approve GetPHTGL_ApproveByContractIdandUserId(string contractId,string  UserID)

        {
            return Funs.DB.PHTGL_Approve.FirstOrDefault(e => e.ContractId == contractId&&e.ApproveMan==UserID);
        }

        public static Model.PHTGL_Approve GetPHTGL_ApproveByContractIdAndType(string contractId,string approveType)

        {
            return Funs.DB.PHTGL_Approve.FirstOrDefault(e => e.ContractId == contractId&&e.ApproveType==approveType);
        }

        /// <summary>
        /// 获取当前人员正在审批的信息
        /// </summary>
        /// <param name="contractId"></param>
        /// <param name="approveMan"></param>
        /// <returns></returns>
        public static Model.PHTGL_Approve GetPHTGL_ApproveByUserId(string contractId,string approveMan)
        {
            return Funs.DB.PHTGL_Approve.FirstOrDefault(e => e.ContractId == contractId &&e.ApproveMan== approveMan&&e.State==0);
        }

        /// <summary>
        /// 判断当前人员是否是审批相关人员
        /// </summary>
        /// <param name="contractId"></param>
        /// <param name="approveMan"></param>
        /// <returns></returns>
        public static bool IsApproveMan(string contractId, string approveMan)
        {
            bool IsExit = false;
            var q = (from x in Funs.DB.PHTGL_Approve where x.ContractId == contractId && x.ApproveMan==approveMan select x).ToList();
            if (q!=null)
            {
                IsExit = true;
            }
            return IsExit;
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
            table.IsPushOa = newtable.IsPushOa;
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
                table.IsPushOa = newtable.IsPushOa;
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


        public static List<Model.PHTGL_Approve> GetApproves_NopushOa()
        {
            var q = (from x in Funs.DB.PHTGL_Approve where x.IsPushOa == 0 &&x.ApproveMan!="" select x).ToList();
            return q;
        }
    }
    public class ApproveManModel
    {
        public int Number
        {
            get;
            set;
        }
        public string userid
        {
            get;
            set;
        }
        public string Rolename
        {
            get;
            set;
        }

    }
}
