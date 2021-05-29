using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    /// <summary>
    /// 合同基本信息
    /// </summary>
    public static class ContractService
    {
        public static string ContractId;
        /// <summary>
        /// true 是新建  false 是修改
        /// </summary>
        public static bool IsCreate; 


        /// <summary>
        /// 根据主键获取合同基本信息
        /// </summary>
        /// <param name="contractId"></param>
        /// <returns></returns>
        public static Model.PHTGL_Contract GetContractById(string contractId)
        {
            return Funs.DB.PHTGL_Contract.FirstOrDefault(e => e.ContractId == contractId);
        }
       /// <summary>
       /// 根据总包合同编号
       /// </summary>
       /// <param name="ProjectId"></param>
       /// <returns></returns>
        public static Model.PHTGL_Contract GetContractByProjectId(string ProjectId)
        {
            return Funs.DB.PHTGL_Contract.FirstOrDefault(e => e.ProjectId == ProjectId);
        }

        public static List<Model.Base_Project> GetProjectDropDownList()
        {
            var list = (from x in Funs.DB.PHTGL_Contract
                        join y in Funs.DB.Base_Project on  x.ProjectId equals y.ProjectId
                        where x.ApproveState>0
                        select y).ToList();
            return list;
        }
 

        public static void InitAllProjectCodeDropDownList(FineUIPro.DropDownList dropName, bool isShowPlease)
        {
            dropName.DataValueField = "ProjectId";
            dropName.DataTextField = "ProjectCode";
            var projectlist = BLL.ContractService.GetProjectDropDownList();
            dropName.DataSource = projectlist;
            dropName.DataBind();
            if (projectlist.Count() == 0)
            {
                isShowPlease = true;
            }
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }

 



        /// <summary>
        /// 增加合同基本信息
        /// </summary>
        /// <param name="contract"></param>
        public static void AddContract(Model.PHTGL_Contract contract)
        {
            Model.PHTGL_Contract newContract = new Model.PHTGL_Contract();
            newContract.ContractId = contract.ContractId;
            newContract.ContractCode = contract.ContractCode;
            newContract.ProjectId = contract.ProjectId;
            newContract.ContractName = contract.ContractName;
            newContract.ContractNum = contract.ContractNum;
            newContract.Parties = contract.Parties;
            newContract.Currency = contract.Currency;
            newContract.ContractAmount = contract.ContractAmount;
            newContract.DepartId = contract.DepartId;
            newContract.Agent = contract.Agent;
            newContract.ContractType = contract.ContractType;
            newContract.Remarks = contract.Remarks;
            newContract.ApproveState = contract.ApproveState;
            newContract.CreatUser = contract.CreatUser;
            Funs.DB.PHTGL_Contract.InsertOnSubmit(newContract);
            Funs.DB.SubmitChanges();
        }


        /// <summary>
        /// 修改合同基本信息
        /// </summary>
        /// <param name="contract"></param>
        public static void UpdateContract(Model.PHTGL_Contract contract)
        {
            Model.PHTGL_Contract newContract = Funs.DB.PHTGL_Contract.FirstOrDefault(e => e.ContractId == contract.ContractId);
            if (newContract != null)
            {
                newContract.ProjectId = contract.ProjectId;
                newContract.ContractCode = contract.ContractCode;
                newContract.ContractName = contract.ContractName;
                newContract.ContractNum = contract.ContractNum;
                newContract.Parties = contract.Parties;
                newContract.Currency = contract.Currency;
                newContract.ContractAmount = contract.ContractAmount;
                newContract.DepartId = contract.DepartId;
                newContract.Agent = contract.Agent;
                newContract.ContractType = contract.ContractType;
                newContract.Remarks = contract.Remarks;
                newContract.ApproveState = contract.ApproveState;
                newContract.CreatUser = contract.CreatUser;
                Funs.DB.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除合同基本信息
        /// </summary>
        /// <param name="contractId"></param>
        public static void DeleteContractById(string contractId)
        {

            Model.PHTGL_Contract contract = Funs.DB.PHTGL_Contract.FirstOrDefault(e => e.ContractId == contractId);
            if (contract != null)
            {
                Funs.DB.PHTGL_Contract.DeleteOnSubmit(contract);
                Funs.DB.SubmitChanges();
            }
        }

    }
}