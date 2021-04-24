using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{

    public static class PHTGL_ContractReviewService
    {

        /// <summary>
        /// 会签人员
        /// </summary>
        public static Dictionary<int, string> Countersigner = new Dictionary<int, string>()
        {

                            {1,BLL.Const.ConstructionManager},
                            {2,BLL.Const.HSSEManager},
                            {3,BLL.Const.QAManager},
                            {4,BLL.Const.PurchasingManager},
                            {5,BLL.Const.ControlManager},
                            {6,BLL.Const.FinancialManager}

        };

        public static Model.PHTGL_ContractReview GetPHTGL_ContractReviewById(string ContractReviewId)

        {
            return Funs.DB.PHTGL_ContractReview.FirstOrDefault(e => e.ContractReviewId == ContractReviewId);
        }

        public static Model.PHTGL_ContractReview GetPHTGL_ContractReviewByContractId(string ContractId)

        {
            return Funs.DB.PHTGL_ContractReview.FirstOrDefault(e => e.ContractId == ContractId);
        }

        /// <summary>
        /// 获取审批人员
        /// </summary>
        /// <param name="ContractId"></param>
        /// <returns></returns>
        public static Dictionary<int, string> Get_DicApproveman(string projectid, string ContractId)
        {
            Dictionary<int, string> Dic_Approveman = new Dictionary<int, string>();

             Model.PHTGL_ContractReview table= GetPHTGL_ContractReviewByContractId(ContractId);
           
            Dic_Approveman.Add(7, BLL.ProjectService.GetRoleID (projectid, BLL.Const.SubProjectManager));
            Dic_Approveman.Add(8,   table.Countersign_Construction  );
            Dic_Approveman.Add(9,   table.Countersign_Law );
            Dic_Approveman.Add(10,  table.Approval_Construction );
            Dic_Approveman.Add(11,  table.Approval_Law );
            Dic_Approveman.Add(12, BLL.ProjectService.GetRoleID(projectid,BLL.Const.ProjectManager));
            Dic_Approveman.Add(13, BLL.ProjectService.GetRoleID(projectid,BLL.Const.DeputyGeneralManager));
            Dic_Approveman.Add(14, BLL.ProjectService.GetRoleID(projectid,BLL.Const.GeneralAccountant));
            Dic_Approveman.Add(15, BLL.ProjectService.GetRoleID(projectid,BLL.Const.GeneralManager));
            Dic_Approveman.Add(16, BLL.ProjectService.GetRoleID(projectid, BLL.Const.Chairman));
            return Dic_Approveman;
        }

        public static void AddPHTGL_ContractReview(Model.PHTGL_ContractReview newtable)
        {
            Model.PHTGL_ContractReview table = new Model.PHTGL_ContractReview();
            table.ContractReviewId = newtable.ContractReviewId;
            table.ContractId = newtable.ContractId;
            table.DocumentNumber = newtable.DocumentNumber;
            table.State = newtable.State;
            table.Countersign_Construction = newtable.Countersign_Construction;
            table.Countersign_Law = newtable.Countersign_Law;
            table.Approval_Construction = newtable.Approval_Construction;
            table.Approval_Law = newtable.Approval_Law;
            Funs.DB.PHTGL_ContractReview.InsertOnSubmit(table);
            Funs.DB.SubmitChanges();
        }


        public static void UpdatePHTGL_ContractReview(Model.PHTGL_ContractReview newtable)
        {
            Model.PHTGL_ContractReview table = Funs.DB.PHTGL_ContractReview.FirstOrDefault(e => e.ContractReviewId == newtable.ContractReviewId);

            if (table != null)
            {
                table.ContractReviewId = newtable.ContractReviewId;
                table.ContractId = newtable.ContractId;
                table.DocumentNumber = newtable.DocumentNumber;
                table.State = newtable.State;
                table.Countersign_Construction = newtable.Countersign_Construction;
                table.Countersign_Law = newtable.Countersign_Law;
                table.Approval_Construction = newtable.Approval_Construction;
                table.Approval_Law = newtable.Approval_Law;
                Funs.DB.SubmitChanges();
            }

        }
        /// <summary>
        /// 根据主键删除审批信息
        /// </summary>
        /// <param name="contractId"></param>
        public static void DeletePHTGL_ContractReviewById(string contractReviewId)
        {
            Model.PHTGL_ContractReview contract = Funs.DB.PHTGL_ContractReview.FirstOrDefault(e => e.ContractReviewId == contractReviewId);
            if (contract != null)
            {
                Funs.DB.PHTGL_ContractReview.DeleteOnSubmit(contract);
                Funs.DB.SubmitChanges();
            }
        }
    }
}
