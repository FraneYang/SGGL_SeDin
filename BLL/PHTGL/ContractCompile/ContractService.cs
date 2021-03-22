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
        /// 增加合同基本信息
        /// </summary>
        /// <param name="contract"></param>
        public static void AddContract(Model.PHTGL_Contract contract)
        {
            Model.PHTGL_Contract newContract = new Model.PHTGL_Contract();
            newContract.ContractId = contract.ContractId;
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
                newContract.ContractName = contract.ContractName;
                newContract.ContractNum = contract.ContractNum;
                newContract.Parties = contract.Parties;
                newContract.Currency = contract.Currency;
                newContract.ContractAmount = contract.ContractAmount;
                newContract.DepartId = contract.DepartId;
                newContract.Agent = contract.Agent;
                newContract.ContractType = contract.ContractType;
                newContract.Remarks = contract.Remarks;
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