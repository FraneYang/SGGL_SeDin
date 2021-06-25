using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{

    public static class PHTGL_ContractReviewService
    {

        

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
        public static Dictionary<int, string> Get_DicApproveman( string ContractReviewId)
        {
            Dictionary<int, string> Dic_Approveman = new Dictionary<int, string>();

             Model.PHTGL_ContractReview table = table = GetPHTGL_ContractReviewById(ContractReviewId);
             Dic_Approveman.Add(7,  table.Approval_ProjectManager);
            Dic_Approveman.Add(8,  table.Countersign_Construction);
            Dic_Approveman.Add(9,  table.Countersign_Law);
            Dic_Approveman.Add(10, table.Approval_Construction);
            Dic_Approveman.Add(11, table.Approval_Law);
            Dic_Approveman.Add(12, table.Approval_ProjectManager);
            Dic_Approveman.Add(13, table.Approval_DeputyGeneralManager);
            Dic_Approveman.Add(14, table.Approval_GeneralAccountant);
            Dic_Approveman.Add(15, table.Approval_GeneralManager);
            Dic_Approveman.Add(16, table.Approval_Chairman);
            return Dic_Approveman;
        }
        public static List<ApproveManModel> GetApproveManModels(string ContractReviewId)
        {

            Model.PHTGL_ContractReview table = table = GetPHTGL_ContractReviewById(ContractReviewId);


            List<ApproveManModel> approveManModels = new List<ApproveManModel>();
            approveManModels.Add(new ApproveManModel { Number = 7, userid = table.Approval_ProjectManager, Rolename = "会签项目经理" });
            approveManModels.Add(new ApproveManModel { Number = 8, userid = table.Countersign_Construction, Rolename = "会签施工管理部" });
            approveManModels.Add(new ApproveManModel { Number = 9, userid = table.Countersign_Law, Rolename = "会签法律合规部1" });
            approveManModels.Add(new ApproveManModel { Number = 10, userid = table.Countersign_Law2, Rolename = "会签法律合规部2" });
            approveManModels.Add(new ApproveManModel { Number = 11, userid = table.Approval_Construction, Rolename = "签订施工管理部" });
            approveManModels.Add(new ApproveManModel { Number = 12, userid = table.Approval_Law, Rolename = "签订法律合规部" });
            approveManModels.Add(new ApproveManModel { Number = 13, userid = table.Approval_ProjectManager, Rolename = "签订项目经理" });
            approveManModels.Add(new ApproveManModel { Number = 14, userid = table.Approval_DeputyGeneralManager, Rolename = "分管副总经理" });
            approveManModels.Add(new ApproveManModel { Number = 15, userid = table.Approval_GeneralAccountant, Rolename = "总会计师" });
            approveManModels.Add(new ApproveManModel { Number = 16, userid = table.Approval_GeneralManager, Rolename = "总经理" });
            approveManModels.Add(new ApproveManModel { Number = 17, userid = table.Approval_Chairman, Rolename = "董事长" });
            return approveManModels;
        }
        public static List<ApproveManModel> GetApproveManModels__Countersigner(string ContractReviewId)
        {

            Model.PHTGL_ContractReview table = table = GetPHTGL_ContractReviewById(ContractReviewId);


            List<ApproveManModel> approveManModels = new List<ApproveManModel>();
            approveManModels.Add(new ApproveManModel { Number = 1, userid = table.Countersign_ConstructionManager, Rolename = "会签施工经理" });
            approveManModels.Add(new ApproveManModel { Number = 2, userid = table.Countersign_HSSEManager, Rolename = "会签HSE经理" });
            approveManModels.Add(new ApproveManModel { Number = 3, userid = table.Countersign_QAManager, Rolename = "会签质量经理" });
            approveManModels.Add(new ApproveManModel { Number = 4, userid = table.Countersign_PurchasingManager, Rolename = "会签采购经理" });
            approveManModels.Add(new ApproveManModel { Number = 5, userid = table.Countersign_ControlManager, Rolename = "会签控制经理" });
            approveManModels.Add(new ApproveManModel { Number = 6, userid = table.Countersign_FinancialManager, Rolename = "会签财务经理" });
            
            return approveManModels;
        }
        public static Dictionary<int, string> Get_Countersigner( string ContractReviewId)
        {
             
            Dictionary<int, string> Dic_Countersigner = new Dictionary<int, string>();
            Model.PHTGL_ContractReview table = table = GetPHTGL_ContractReviewById(ContractReviewId);

            Dic_Countersigner.Add(1, table.Countersign_ConstructionManager);
            Dic_Countersigner.Add(2, table.Countersign_HSSEManager);
            Dic_Countersigner.Add(3, table.Countersign_QAManager);
            Dic_Countersigner.Add(4, table.Countersign_PurchasingManager);
            Dic_Countersigner.Add(5, table.Countersign_ControlManager);
            Dic_Countersigner.Add(6, table.Countersign_FinancialManager);
            return Dic_Countersigner;
 
        }

        public static void AddPHTGL_ContractReview(Model.PHTGL_ContractReview newtable)
        {
            Model.PHTGL_ContractReview table = new Model.PHTGL_ContractReview();
            table.ContractReviewId = newtable.ContractReviewId;
            table.Countersign_PurchasingManager = newtable.Countersign_PurchasingManager;
            table.Countersign_ControlManager = newtable.Countersign_ControlManager;
            table.Countersign_FinancialManager = newtable.Countersign_FinancialManager;
            table.Countersign_Construction = newtable.Countersign_Construction;
            table.Countersign_Law = newtable.Countersign_Law;
            table.Approval_Construction = newtable.Approval_Construction;
            table.Approval_Law = newtable.Approval_Law;
            table.Approval_SubProjectManager = newtable.Approval_SubProjectManager;
            table.Approval_ProjectManager = newtable.Approval_ProjectManager;
            table.Approval_DeputyGeneralManager = newtable.Approval_DeputyGeneralManager;
            table.ContractId = newtable.ContractId;
            table.Approval_GeneralAccountant = newtable.Approval_GeneralAccountant;
            table.Approval_GeneralManager = newtable.Approval_GeneralManager;
            table.Approval_Chairman = newtable.Approval_Chairman;
            table.SetSubReviewId = newtable.SetSubReviewId;
            table.DocumentNumber = newtable.DocumentNumber;
            table.State = newtable.State;
            table.CreateUser = newtable.CreateUser;
            table.Countersign_ConstructionManager = newtable.Countersign_ConstructionManager;
            table.Countersign_HSSEManager = newtable.Countersign_HSSEManager;
            table.Countersign_QAManager = newtable.Countersign_QAManager;
            table.Countersign_Law2 = newtable.Countersign_Law2;
            Funs.DB.PHTGL_ContractReview.InsertOnSubmit(table);
            Funs.DB.SubmitChanges();
        }


        public static void UpdatePHTGL_ContractReview(Model.PHTGL_ContractReview newtable)
        {
            Model.PHTGL_ContractReview table = Funs.DB.PHTGL_ContractReview.FirstOrDefault(e => e.ContractReviewId == newtable.ContractReviewId);

            if (table != null)
            {
                table.ContractReviewId = newtable.ContractReviewId;
                table.Countersign_PurchasingManager = newtable.Countersign_PurchasingManager;
                table.Countersign_ControlManager = newtable.Countersign_ControlManager;
                table.Countersign_FinancialManager = newtable.Countersign_FinancialManager;
                table.Countersign_Construction = newtable.Countersign_Construction;
                table.Countersign_Law = newtable.Countersign_Law;
                table.Approval_Construction = newtable.Approval_Construction;
                table.Approval_Law = newtable.Approval_Law;
                table.Approval_SubProjectManager = newtable.Approval_SubProjectManager;
                table.Approval_ProjectManager = newtable.Approval_ProjectManager;
                table.Approval_DeputyGeneralManager = newtable.Approval_DeputyGeneralManager;
                table.ContractId = newtable.ContractId;
                table.Approval_GeneralAccountant = newtable.Approval_GeneralAccountant;
                table.Approval_GeneralManager = newtable.Approval_GeneralManager;
                table.Approval_Chairman = newtable.Approval_Chairman;
                table.SetSubReviewId = newtable.SetSubReviewId;
                table.DocumentNumber = newtable.DocumentNumber;
                table.State = newtable.State;
                table.CreateUser = newtable.CreateUser;
                table.Countersign_ConstructionManager = newtable.Countersign_ConstructionManager;
                table.Countersign_HSSEManager = newtable.Countersign_HSSEManager;
                table.Countersign_QAManager = newtable.Countersign_QAManager;
                table.Countersign_Law2 = newtable.Countersign_Law2;
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
