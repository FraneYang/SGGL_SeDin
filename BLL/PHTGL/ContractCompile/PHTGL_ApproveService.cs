using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
        /// <summary>
        /// 添加审批人员记录
        /// </summary>
        /// <param name="newtable"></param>
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

        /// <summary>
        /// 修改审批人员记录
        /// </summary>
        /// <param name="newtable"></param>
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
        ///  删除记录
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

        /// <summary>
        /// 获取未推送oa 的审批记录
        /// </summary>
        /// <returns></returns>
        public static List<Model.PHTGL_Approve> GetApproves_NopushOa()
        {
            var q = (from x in Funs.DB.PHTGL_Approve where x.IsPushOa == 0 &&x.ApproveMan!="" select x).ToList();
            return q;
        }
        /// <summary>
        /// 获取当前审批合同所以人员的最后一次审批记录
        /// </summary>
        /// <param name="ContractId"></param>
        /// <returns></returns>
        public static DataTable GetFinalApproveData(string ContractId)
        {
            string strSql = @"  select  a.ApproveId ,a.ApproveMan,a.ApproveType,a.ApproveDate ,a.ApproveIdea,a.ApproveForm
                                from PHTGL_Approve a "
                             + @"  where not exists (select 1 from PHTGL_Approve b where a.ApproveType=b.ApproveType and a.ContractId=b.ContractId and a.ApproveDate<b.ApproveDate ) and ContractId=@ContractId and a.State=1"
                             + @"  order by    CONVERT(datetime,a.ApproveDate)   desc       ";
            List<SqlParameter> listStr = new List<SqlParameter>();
            listStr.Add(new SqlParameter("@ContractId", ContractId));

            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            return tb;
        }
        /// <summary>
        /// 获取当前审批合同所全部审批记录
        /// </summary>
        /// <param name="ContractId"></param>
        /// <returns></returns>
        public static DataTable GetAllApproveData(string ContractId)
        {
            string strSql = @"  select u.UserName as  ApproveMan,
                                       App.ApproveDate,
                                      (CASE App.IsAgree WHEN '1' THEN '不同意'
                                        WHEN '2' THEN '同意' END) AS IsAgree,
                                        App.ApproveIdea,
                                        App.ApproveId,
                                        App.ApproveType
                                       from PHTGL_Approve as App"
                               + @"   left join Sys_User AS U ON U.UserId = App.ApproveMan WHERE 1=1   and App.IsAgree <>0 and app.ContractId= @ContractId order by convert(datetime ,App.ApproveDate)  ";
            List<SqlParameter> listStr = new List<SqlParameter>();
            listStr.Add(new SqlParameter("@ContractId", ContractId));
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            return tb;
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
