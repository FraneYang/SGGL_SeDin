using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    /// <summary>
    /// 合同协议书
    /// </summary>
    public static class SubcontractAgreementService
    {
        /// <summary>
        /// 根据主键获取合同协议书
        /// </summary>
        /// <param name="subcontractAgreementId"></param>
        /// <returns></returns>
        public static Model.PHTGL_SubcontractAgreement GetSubcontractAgreementById(string subcontractAgreementId)
        {
            return Funs.DB.PHTGL_SubcontractAgreement.FirstOrDefault(e => e.SubcontractAgreementId == subcontractAgreementId);
        }

        /// <summary>
        /// 根据合同Id获取合同协议书
        /// </summary>
        /// <param name="contractId"></param>
        /// <returns></returns>
        public static Model.PHTGL_SubcontractAgreement GetSubcontractAgreementByContractId(string contractId)
        {
            return Funs.DB.PHTGL_SubcontractAgreement.FirstOrDefault(e => e.ContractId == contractId);
        }

        /// <summary>
        /// 添加合同协议书
        /// </summary>
        /// <param name="sub"></param>
        public static void AddSubcontractAgreement(Model.PHTGL_SubcontractAgreement sub)
        {
            Model.PHTGL_SubcontractAgreement newSub = new Model.PHTGL_SubcontractAgreement();

            newSub.SubcontractAgreementId = sub.SubcontractAgreementId;
            newSub.ContractId = sub.ContractId;
            newSub.GeneralContractor = sub.GeneralContractor;
            newSub.SubConstruction = sub.SubConstruction;
            newSub.Contents = sub.Contents;
            newSub.ContractProject = sub.ContractProject;
            newSub.ContractProjectOwner = sub.ContractProjectOwner;
            newSub.SubProject = sub.SubProject;
            newSub.SubProjectAddress = sub.SubProjectAddress;
            newSub.FundingSources = sub.FundingSources;
            newSub.SubProjectContractScope = sub.SubProjectContractScope;
            newSub.SubProjectContent = sub.SubProjectContent;
            newSub.PlanStartYear = sub.PlanStartYear;
            newSub.PlanStartMonth = sub.PlanStartMonth;
            newSub.PlanStartDay = sub.PlanStartDay;
            newSub.PlanEndYear = sub.PlanEndYear;
            newSub.PlanEndMonth = sub.PlanEndMonth;
            newSub.PlanEndDay = sub.PlanEndDay;
            newSub.Limit = sub.Limit;
            newSub.QualityStandards = sub.QualityStandards;
            newSub.HSEManageStandards = sub.HSEManageStandards;
            newSub.SubcontractPriceForm = sub.SubcontractPriceForm;
            newSub.ContractPriceCapital = sub.ContractPriceCapital;
            newSub.ContractPriceCNY = sub.ContractPriceCNY;
            newSub.ContractPriceDesc = sub.ContractPriceDesc;
            newSub.Invoice = sub.Invoice;
            newSub.Law = sub.Law;
            newSub.SignedYear = sub.SignedYear;
            newSub.SignedMonth = sub.SignedMonth;
            newSub.SignedAddress = sub.SignedAddress;
            newSub.AgreementNum = sub.AgreementNum;
            newSub.GeneralContractorNum = sub.GeneralContractorNum;
            newSub.SubContractorNum = sub.SubContractorNum;
            newSub.SocialCreditCode1 = sub.SocialCreditCode1;
            newSub.SocialCreditCode2 = sub.SocialCreditCode2;
            newSub.Address1 = sub.Address1;
            newSub.Address2 = sub.Address2;
            newSub.ZipCode1 = sub.ZipCode1;
            newSub.ZipCode2 = sub.ZipCode2;
            newSub.LegalRepresentative1 = sub.LegalRepresentative1;
            newSub.LegalRepresentative2 = sub.LegalRepresentative2;
            newSub.EntrustedAgent1 = sub.EntrustedAgent1;
            newSub.EntrustedAgent2 = sub.EntrustedAgent2;
            newSub.Telephone1 = sub.Telephone1;
            newSub.Telephone2 = sub.Telephone2;
            newSub.Fax1 = sub.Fax1;
            newSub.Fax2 = sub.Fax2;
            newSub.Email1 = sub.Email1;
            newSub.Email2 = sub.Email2;
            newSub.Bank1 = sub.Bank1;
            newSub.Bank2 = sub.Bank2;
            newSub.Account1 = sub.Account1;
            newSub.Account2 = sub.Account2;
            Funs.DB.PHTGL_SubcontractAgreement.InsertOnSubmit(newSub);
            Funs.DB.SubmitChanges();
        }

        /// <summary>
        /// 修改合同协议书
        /// </summary>
        /// <param name="sub"></param>
        public static void UpdateSubcontractAgreement(Model.PHTGL_SubcontractAgreement sub)
        {
            Model.PHTGL_SubcontractAgreement newSub = Funs.DB.PHTGL_SubcontractAgreement.FirstOrDefault(e => e.SubcontractAgreementId == sub.SubcontractAgreementId);
            if (newSub != null)
            {
                newSub.GeneralContractor = sub.GeneralContractor;
                newSub.SubConstruction = sub.SubConstruction;
                newSub.Contents = sub.Contents;
                newSub.ContractProject = sub.ContractProject;
                newSub.ContractProjectOwner = sub.ContractProjectOwner;
                newSub.SubProject = sub.SubProject;
                newSub.SubProjectAddress = sub.SubProjectAddress;
                newSub.FundingSources = sub.FundingSources;
                newSub.SubProjectContractScope = sub.SubProjectContractScope;
                newSub.SubProjectContent = sub.SubProjectContent;
                newSub.PlanStartYear = sub.PlanStartYear;
                newSub.PlanStartMonth = sub.PlanStartMonth;
                newSub.PlanStartDay = sub.PlanStartDay;
                newSub.PlanEndYear = sub.PlanEndYear;
                newSub.PlanEndMonth = sub.PlanEndMonth;
                newSub.PlanEndDay = sub.PlanEndDay;
                newSub.Limit = sub.Limit;
                newSub.QualityStandards = sub.QualityStandards;
                newSub.HSEManageStandards = sub.HSEManageStandards;
                newSub.SubcontractPriceForm = sub.SubcontractPriceForm;
                newSub.ContractPriceCapital = sub.ContractPriceCapital;
                newSub.ContractPriceCNY = sub.ContractPriceCNY;
                newSub.ContractPriceDesc = sub.ContractPriceDesc;
                newSub.Invoice = sub.Invoice;
                newSub.Law = sub.Law;
                newSub.SignedYear = sub.SignedYear;
                newSub.SignedMonth = sub.SignedMonth;
                newSub.SignedAddress = sub.SignedAddress;
                newSub.AgreementNum = sub.AgreementNum;
                newSub.GeneralContractorNum = sub.GeneralContractorNum;
                newSub.SubContractorNum = sub.SubContractorNum;
                newSub.SocialCreditCode1 = sub.SocialCreditCode1;
                newSub.SocialCreditCode2 = sub.SocialCreditCode2;
                newSub.Address1 = sub.Address1;
                newSub.Address2 = sub.Address2;
                newSub.ZipCode1 = sub.ZipCode1;
                newSub.ZipCode2 = sub.ZipCode2;
                newSub.LegalRepresentative1 = sub.LegalRepresentative1;
                newSub.LegalRepresentative2 = sub.LegalRepresentative2;
                newSub.EntrustedAgent1 = sub.EntrustedAgent1;
                newSub.EntrustedAgent2 = sub.EntrustedAgent2;
                newSub.Telephone1 = sub.Telephone1;
                newSub.Telephone2 = sub.Telephone2;
                newSub.Fax1 = sub.Fax1;
                newSub.Fax2 = sub.Fax2;
                newSub.Email1 = sub.Email1;
                newSub.Email2 = sub.Email2;
                newSub.Bank1 = sub.Bank1;
                newSub.Bank2 = sub.Bank2;
                newSub.Account1 = sub.Account1;
                newSub.Account2 = sub.Account2;
                Funs.DB.SubmitChanges();
            }
        }
    }
}
