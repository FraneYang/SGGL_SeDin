using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public static class PHTGL_SpecialTermsConditionsService
    {
        public static string SpecialTermsConditionsId;
        /// <summary>
        /// 根据主键获取专业合同条款基本信息
        /// </summary>
        /// <param name="SpecialTermsConditionsId"></param>
        /// <returns></returns>
        public static Model.PHTGL_SpecialTermsConditions GetSpecialTermsConditionsById(string SpecialTermsConditionsId)
        {
            return Funs.DB.PHTGL_SpecialTermsConditions.FirstOrDefault(e => e.SpecialTermsConditionsId == SpecialTermsConditionsId);
        }
        /// <summary>
        /// 根据合同编号获取专业合同条款基本信息
        /// </summary>
        /// <param name="contractId"></param>
        /// <returns></returns>
        public static Model.PHTGL_SpecialTermsConditions GetSpecialTermsConditionsByContractId(string contractId)
        {
            return Funs.DB.PHTGL_SpecialTermsConditions.FirstOrDefault(e => e.ContractId == contractId);
        }

        /// <summary>
        /// 增加专业合同条款基本信息
        /// </summary>
        /// <param name="SpecialTermsConditions"></param>
        public static void AddSpecialTermsConditions(Model.PHTGL_SpecialTermsConditions SpecialTermsConditions)
        {
            Model.PHTGL_SpecialTermsConditions newSpecialTermsConditions = new Model.PHTGL_SpecialTermsConditions();
            newSpecialTermsConditions.SpecialTermsConditionsId = SpecialTermsConditions.SpecialTermsConditionsId;
            newSpecialTermsConditions.ContractId = SpecialTermsConditions.ContractId;
            newSpecialTermsConditions.TotalPackageContract = SpecialTermsConditions.TotalPackageContract;
            newSpecialTermsConditions.OtherSubDocuments = SpecialTermsConditions.OtherSubDocuments;
            newSpecialTermsConditions.OtherPlaces = SpecialTermsConditions.OtherPlaces;
            newSpecialTermsConditions.NormativeDocument = SpecialTermsConditions.NormativeDocument;
            newSpecialTermsConditions.StandardSpecification = SpecialTermsConditions.StandardSpecification;
            newSpecialTermsConditions.DrawingPeriod = SpecialTermsConditions.DrawingPeriod;
            newSpecialTermsConditions.DrawingCount = SpecialTermsConditions.DrawingCount;
            newSpecialTermsConditions.DrawingContents = SpecialTermsConditions.DrawingContents;
            newSpecialTermsConditions.DeepeningDesign = SpecialTermsConditions.DeepeningDesign;
            newSpecialTermsConditions.ConstructionSubFileCount = SpecialTermsConditions.ConstructionSubFileCount;
            newSpecialTermsConditions.ConstructionSubFileForm = SpecialTermsConditions.ConstructionSubFileForm;
            newSpecialTermsConditions.GeneralContractorAddress = SpecialTermsConditions.GeneralContractorAddress;
            newSpecialTermsConditions.GeneralContractorMan = SpecialTermsConditions.GeneralContractorMan;
            newSpecialTermsConditions.SubAddress = SpecialTermsConditions.SubAddress;
            newSpecialTermsConditions.SubMan = SpecialTermsConditions.SubMan;
            newSpecialTermsConditions.FacilitiesConditions = SpecialTermsConditions.FacilitiesConditions;
            newSpecialTermsConditions.ProjectManagerName = SpecialTermsConditions.ProjectManagerName;
            newSpecialTermsConditions.ProjectManagerTitle = SpecialTermsConditions.ProjectManagerTitle;
            newSpecialTermsConditions.BuilderQualificationLevel = SpecialTermsConditions.BuilderQualificationLevel;
            newSpecialTermsConditions.BuilderRegistrationCertificate = SpecialTermsConditions.BuilderRegistrationCertificate;
            newSpecialTermsConditions.ProjectManagerTel = SpecialTermsConditions.ProjectManagerTel;
            newSpecialTermsConditions.ProjectManagerEmail = SpecialTermsConditions.ProjectManagerEmail;
            newSpecialTermsConditions.ProjectManagerAddress = SpecialTermsConditions.ProjectManagerAddress;
            newSpecialTermsConditions.ScopeAuthorization = SpecialTermsConditions.ScopeAuthorization;
            newSpecialTermsConditions.PermitsApprovals = SpecialTermsConditions.PermitsApprovals;
            newSpecialTermsConditions.SubProjectManagerName = SpecialTermsConditions.SubProjectManagerName;
            newSpecialTermsConditions.SubProjectManagerTitle = SpecialTermsConditions.SubProjectManagerTitle;
            newSpecialTermsConditions.SubBuilderQualificationLevel = SpecialTermsConditions.SubBuilderQualificationLevel;
            newSpecialTermsConditions.SubBuilderRegistrationCertificate = SpecialTermsConditions.SubBuilderRegistrationCertificate;
            newSpecialTermsConditions.SubProjectManagerTel = SpecialTermsConditions.SubProjectManagerTel;
            newSpecialTermsConditions.SubProjectManagerEmail = SpecialTermsConditions.SubProjectManagerEmail;
            newSpecialTermsConditions.SubProjectManagerAddress = SpecialTermsConditions.SubProjectManagerAddress;
            newSpecialTermsConditions.SubScopeAuthorization = SpecialTermsConditions.SubScopeAuthorization;
            newSpecialTermsConditions.AttachmentName = SpecialTermsConditions.AttachmentName;
            newSpecialTermsConditions.DaysNum = SpecialTermsConditions.DaysNum;
            newSpecialTermsConditions.DefaultResponsibility = SpecialTermsConditions.DefaultResponsibility;
            newSpecialTermsConditions.LeaveSiteResponsibility = SpecialTermsConditions.LeaveSiteResponsibility;
            newSpecialTermsConditions.RefusedChangeResponsibility = SpecialTermsConditions.RefusedChangeResponsibility;
            newSpecialTermsConditions.WithoutCardMountGuard = SpecialTermsConditions.WithoutCardMountGuard;
            newSpecialTermsConditions.SubcontractWorks = SpecialTermsConditions.SubcontractWorks;
            newSpecialTermsConditions.IllegalSubcontracting = SpecialTermsConditions.IllegalSubcontracting;
            newSpecialTermsConditions.PerformanceWay = SpecialTermsConditions.PerformanceWay;
            newSpecialTermsConditions.PerformanceMoney = SpecialTermsConditions.PerformanceMoney;
            newSpecialTermsConditions.PerformanceTimelimit = SpecialTermsConditions.PerformanceTimelimit;
            newSpecialTermsConditions.AssociationAgreementAttachUrl = SpecialTermsConditions.AssociationAgreementAttachUrl;
            newSpecialTermsConditions.SpecialStandards = SpecialTermsConditions.SpecialStandards;
            newSpecialTermsConditions.ConstructionMeasures = SpecialTermsConditions.ConstructionMeasures;
            newSpecialTermsConditions.SubMeasures = SpecialTermsConditions.SubMeasures;
            newSpecialTermsConditions.LabourCost = SpecialTermsConditions.LabourCost;
            newSpecialTermsConditions.LaborSupervisorName = SpecialTermsConditions.LaborSupervisorName;
            newSpecialTermsConditions.LaborSupervisorTitle = SpecialTermsConditions.LaborSupervisorTitle;
            newSpecialTermsConditions.LaborSupervisorTel = SpecialTermsConditions.LaborSupervisorTel;
            newSpecialTermsConditions.LaborSupervisorEmail = SpecialTermsConditions.LaborSupervisorEmail;
            newSpecialTermsConditions.LaborSupervisorAddress = SpecialTermsConditions.LaborSupervisorAddress;
            newSpecialTermsConditions.OrganizationalDesign = SpecialTermsConditions.OrganizationalDesign;
            newSpecialTermsConditions.Amendments = SpecialTermsConditions.Amendments;
            newSpecialTermsConditions.Labour = SpecialTermsConditions.Labour;
            newSpecialTermsConditions.WithinTimeLimit = SpecialTermsConditions.WithinTimeLimit;
            newSpecialTermsConditions.AdverseMaterialConditions = SpecialTermsConditions.AdverseMaterialConditions;
            newSpecialTermsConditions.MaterialEquipmentSupplyRange = SpecialTermsConditions.MaterialEquipmentSupplyRange;
            newSpecialTermsConditions.UnloadingRateStandard = SpecialTermsConditions.UnloadingRateStandard;
            newSpecialTermsConditions.SecondaryHandlingCharges = SpecialTermsConditions.SecondaryHandlingCharges;
            newSpecialTermsConditions.SampleRequirements = SpecialTermsConditions.SampleRequirements;
            newSpecialTermsConditions.AlternativeAgreed = SpecialTermsConditions.AlternativeAgreed;
            newSpecialTermsConditions.Equipment = SpecialTermsConditions.Equipment;
            newSpecialTermsConditions.ChangeValuation = SpecialTermsConditions.ChangeValuation;
            newSpecialTermsConditions.Reward = SpecialTermsConditions.Reward;
            newSpecialTermsConditions.IncreaseDecreasePeriod = SpecialTermsConditions.IncreaseDecreasePeriod;
            newSpecialTermsConditions.RiskRange = SpecialTermsConditions.RiskRange;
            newSpecialTermsConditions.AdjustmentMethodA = SpecialTermsConditions.AdjustmentMethodA;
            newSpecialTermsConditions.AdjustmentMethodB = SpecialTermsConditions.AdjustmentMethodB;
            newSpecialTermsConditions.TotalPriceRiskRange = SpecialTermsConditions.TotalPriceRiskRange;
            newSpecialTermsConditions.TotalAdjustmentMethodA = SpecialTermsConditions.TotalAdjustmentMethodA;
            newSpecialTermsConditions.TotalAdjustmentMethodB = SpecialTermsConditions.TotalAdjustmentMethodB;
            newSpecialTermsConditions.OtherPriceForms = SpecialTermsConditions.OtherPriceForms;
            newSpecialTermsConditions.MarketPriceRange = SpecialTermsConditions.MarketPriceRange;
            newSpecialTermsConditions.DifferenceRange = SpecialTermsConditions.DifferenceRange;
            newSpecialTermsConditions.PricingWay = SpecialTermsConditions.PricingWay;
            newSpecialTermsConditions.QuantityCalculationRules = SpecialTermsConditions.QuantityCalculationRules;
            newSpecialTermsConditions.AdvancePayment = SpecialTermsConditions.AdvancePayment;
            newSpecialTermsConditions.AdvancePaymentPeriod = SpecialTermsConditions.AdvancePaymentPeriod;
            newSpecialTermsConditions.LatePaymentAdvance = SpecialTermsConditions.LatePaymentAdvance;
            newSpecialTermsConditions.ProgressPaymentContents = SpecialTermsConditions.ProgressPaymentContents;
            newSpecialTermsConditions.ProgressPaymentConvention = SpecialTermsConditions.ProgressPaymentConvention;
            newSpecialTermsConditions.GuaranteedScopeWork = SpecialTermsConditions.GuaranteedScopeWork;
            newSpecialTermsConditions.GuaranteedCostStandard = SpecialTermsConditions.GuaranteedCostStandard;
            newSpecialTermsConditions.AcceptanceCondition = SpecialTermsConditions.AcceptanceCondition;
            newSpecialTermsConditions.UnqualifiedResponsibility = SpecialTermsConditions.UnqualifiedResponsibility;
            newSpecialTermsConditions.CleanExitTimeLimit = SpecialTermsConditions.CleanExitTimeLimit;
            newSpecialTermsConditions.DataTransferTimeLimit = SpecialTermsConditions.DataTransferTimeLimit;
            newSpecialTermsConditions.DataNumContents = SpecialTermsConditions.DataNumContents;
            newSpecialTermsConditions.DataListing = SpecialTermsConditions.DataListing;
            newSpecialTermsConditions.FinalSettlementNum = SpecialTermsConditions.FinalSettlementNum;
            newSpecialTermsConditions.DefectLiabilityDate = SpecialTermsConditions.DefectLiabilityDate;
            newSpecialTermsConditions.DefectLiabilityPeriod = SpecialTermsConditions.DefectLiabilityPeriod;
            newSpecialTermsConditions.WarrantyPeriodDate = SpecialTermsConditions.WarrantyPeriodDate;
            newSpecialTermsConditions.WarrantyPeriodPeriod = SpecialTermsConditions.WarrantyPeriodPeriod;
            newSpecialTermsConditions.MarginDetainWay = SpecialTermsConditions.MarginDetainWay;
            newSpecialTermsConditions.DefaultMethod = SpecialTermsConditions.DefaultMethod;
            newSpecialTermsConditions.TerminationContract = SpecialTermsConditions.TerminationContract;
            newSpecialTermsConditions.DefaultLiability = SpecialTermsConditions.DefaultLiability;
            newSpecialTermsConditions.SubDefaultCancelContract = SpecialTermsConditions.SubDefaultCancelContract;
            newSpecialTermsConditions.ForceMajeure = SpecialTermsConditions.ForceMajeure;
            newSpecialTermsConditions.NotConsideredForceMajeure = SpecialTermsConditions.NotConsideredForceMajeure;
            newSpecialTermsConditions.GeneralContractorShallPay = SpecialTermsConditions.GeneralContractorShallPay;
            newSpecialTermsConditions.LimitIndemnity = SpecialTermsConditions.LimitIndemnity;
            newSpecialTermsConditions.InsuredAmount = SpecialTermsConditions.InsuredAmount;
            newSpecialTermsConditions.CertificateInsurance = SpecialTermsConditions.CertificateInsurance;
            newSpecialTermsConditions.ArbitrationCommission = SpecialTermsConditions.ArbitrationCommission;
            newSpecialTermsConditions.PeopleCourt = SpecialTermsConditions.PeopleCourt;
            Funs.DB.PHTGL_SpecialTermsConditions.InsertOnSubmit(newSpecialTermsConditions);
            Funs.DB.SubmitChanges();
        }


        /// <summary>
        /// 修改专业合同条款基本信息
        /// </summary>
        /// <param name="SpecialTermsConditions"></param>
        public static void UpdateSpecialTermsConditions(Model.PHTGL_SpecialTermsConditions SpecialTermsConditions)
        {
            Model.PHTGL_SpecialTermsConditions newSpecialTermsConditions = Funs.DB.PHTGL_SpecialTermsConditions.FirstOrDefault(e => e.SpecialTermsConditionsId == SpecialTermsConditions.SpecialTermsConditionsId);
            if (newSpecialTermsConditions != null)
            {
                newSpecialTermsConditions.SpecialTermsConditionsId = SpecialTermsConditions.SpecialTermsConditionsId;
                newSpecialTermsConditions.ContractId = SpecialTermsConditions.ContractId;
                newSpecialTermsConditions.TotalPackageContract = SpecialTermsConditions.TotalPackageContract;
                newSpecialTermsConditions.OtherSubDocuments = SpecialTermsConditions.OtherSubDocuments;
                newSpecialTermsConditions.OtherPlaces = SpecialTermsConditions.OtherPlaces;
                newSpecialTermsConditions.NormativeDocument = SpecialTermsConditions.NormativeDocument;
                newSpecialTermsConditions.StandardSpecification = SpecialTermsConditions.StandardSpecification;
                newSpecialTermsConditions.DrawingPeriod = SpecialTermsConditions.DrawingPeriod;
                newSpecialTermsConditions.DrawingCount = SpecialTermsConditions.DrawingCount;
                newSpecialTermsConditions.DrawingContents = SpecialTermsConditions.DrawingContents;
                newSpecialTermsConditions.DeepeningDesign = SpecialTermsConditions.DeepeningDesign;
                newSpecialTermsConditions.ConstructionSubFileCount = SpecialTermsConditions.ConstructionSubFileCount;
                newSpecialTermsConditions.ConstructionSubFileForm = SpecialTermsConditions.ConstructionSubFileForm;
                newSpecialTermsConditions.GeneralContractorAddress = SpecialTermsConditions.GeneralContractorAddress;
                newSpecialTermsConditions.GeneralContractorMan = SpecialTermsConditions.GeneralContractorMan;
                newSpecialTermsConditions.SubAddress = SpecialTermsConditions.SubAddress;
                newSpecialTermsConditions.SubMan = SpecialTermsConditions.SubMan;
                newSpecialTermsConditions.FacilitiesConditions = SpecialTermsConditions.FacilitiesConditions;
                newSpecialTermsConditions.ProjectManagerName = SpecialTermsConditions.ProjectManagerName;
                newSpecialTermsConditions.ProjectManagerTitle = SpecialTermsConditions.ProjectManagerTitle;
                newSpecialTermsConditions.BuilderQualificationLevel = SpecialTermsConditions.BuilderQualificationLevel;
                newSpecialTermsConditions.BuilderRegistrationCertificate = SpecialTermsConditions.BuilderRegistrationCertificate;
                newSpecialTermsConditions.ProjectManagerTel = SpecialTermsConditions.ProjectManagerTel;
                newSpecialTermsConditions.ProjectManagerEmail = SpecialTermsConditions.ProjectManagerEmail;
                newSpecialTermsConditions.ProjectManagerAddress = SpecialTermsConditions.ProjectManagerAddress;
                newSpecialTermsConditions.ScopeAuthorization = SpecialTermsConditions.ScopeAuthorization;
                newSpecialTermsConditions.PermitsApprovals = SpecialTermsConditions.PermitsApprovals;
                newSpecialTermsConditions.SubProjectManagerName = SpecialTermsConditions.SubProjectManagerName;
                newSpecialTermsConditions.SubProjectManagerTitle = SpecialTermsConditions.SubProjectManagerTitle;
                newSpecialTermsConditions.SubBuilderQualificationLevel = SpecialTermsConditions.SubBuilderQualificationLevel;
                newSpecialTermsConditions.SubBuilderRegistrationCertificate = SpecialTermsConditions.SubBuilderRegistrationCertificate;
                newSpecialTermsConditions.SubProjectManagerTel = SpecialTermsConditions.SubProjectManagerTel;
                newSpecialTermsConditions.SubProjectManagerEmail = SpecialTermsConditions.SubProjectManagerEmail;
                newSpecialTermsConditions.SubProjectManagerAddress = SpecialTermsConditions.SubProjectManagerAddress;
                newSpecialTermsConditions.SubScopeAuthorization = SpecialTermsConditions.SubScopeAuthorization;
                newSpecialTermsConditions.AttachmentName = SpecialTermsConditions.AttachmentName;
                newSpecialTermsConditions.DaysNum = SpecialTermsConditions.DaysNum;
                newSpecialTermsConditions.DefaultResponsibility = SpecialTermsConditions.DefaultResponsibility;
                newSpecialTermsConditions.LeaveSiteResponsibility = SpecialTermsConditions.LeaveSiteResponsibility;
                newSpecialTermsConditions.RefusedChangeResponsibility = SpecialTermsConditions.RefusedChangeResponsibility;
                newSpecialTermsConditions.WithoutCardMountGuard = SpecialTermsConditions.WithoutCardMountGuard;
                newSpecialTermsConditions.SubcontractWorks = SpecialTermsConditions.SubcontractWorks;
                newSpecialTermsConditions.IllegalSubcontracting = SpecialTermsConditions.IllegalSubcontracting;
                newSpecialTermsConditions.PerformanceWay = SpecialTermsConditions.PerformanceWay;
                newSpecialTermsConditions.PerformanceMoney = SpecialTermsConditions.PerformanceMoney;
                newSpecialTermsConditions.PerformanceTimelimit = SpecialTermsConditions.PerformanceTimelimit;
                newSpecialTermsConditions.AssociationAgreementAttachUrl = SpecialTermsConditions.AssociationAgreementAttachUrl;
                newSpecialTermsConditions.SpecialStandards = SpecialTermsConditions.SpecialStandards;
                newSpecialTermsConditions.ConstructionMeasures = SpecialTermsConditions.ConstructionMeasures;
                newSpecialTermsConditions.SubMeasures = SpecialTermsConditions.SubMeasures;
                newSpecialTermsConditions.LabourCost = SpecialTermsConditions.LabourCost;
                newSpecialTermsConditions.LaborSupervisorName = SpecialTermsConditions.LaborSupervisorName;
                newSpecialTermsConditions.LaborSupervisorTitle = SpecialTermsConditions.LaborSupervisorTitle;
                newSpecialTermsConditions.LaborSupervisorTel = SpecialTermsConditions.LaborSupervisorTel;
                newSpecialTermsConditions.LaborSupervisorEmail = SpecialTermsConditions.LaborSupervisorEmail;
                newSpecialTermsConditions.LaborSupervisorAddress = SpecialTermsConditions.LaborSupervisorAddress;
                newSpecialTermsConditions.OrganizationalDesign = SpecialTermsConditions.OrganizationalDesign;
                newSpecialTermsConditions.Amendments = SpecialTermsConditions.Amendments;
                newSpecialTermsConditions.Labour = SpecialTermsConditions.Labour;
                newSpecialTermsConditions.WithinTimeLimit = SpecialTermsConditions.WithinTimeLimit;
                newSpecialTermsConditions.AdverseMaterialConditions = SpecialTermsConditions.AdverseMaterialConditions;
                newSpecialTermsConditions.MaterialEquipmentSupplyRange = SpecialTermsConditions.MaterialEquipmentSupplyRange;
                newSpecialTermsConditions.UnloadingRateStandard = SpecialTermsConditions.UnloadingRateStandard;
                newSpecialTermsConditions.SecondaryHandlingCharges = SpecialTermsConditions.SecondaryHandlingCharges;
                newSpecialTermsConditions.SampleRequirements = SpecialTermsConditions.SampleRequirements;
                newSpecialTermsConditions.AlternativeAgreed = SpecialTermsConditions.AlternativeAgreed;
                newSpecialTermsConditions.Equipment = SpecialTermsConditions.Equipment;
                newSpecialTermsConditions.ChangeValuation = SpecialTermsConditions.ChangeValuation;
                newSpecialTermsConditions.Reward = SpecialTermsConditions.Reward;
                newSpecialTermsConditions.IncreaseDecreasePeriod = SpecialTermsConditions.IncreaseDecreasePeriod;
                newSpecialTermsConditions.RiskRange = SpecialTermsConditions.RiskRange;
                newSpecialTermsConditions.AdjustmentMethodA = SpecialTermsConditions.AdjustmentMethodA;
                newSpecialTermsConditions.AdjustmentMethodB = SpecialTermsConditions.AdjustmentMethodB;
                newSpecialTermsConditions.TotalPriceRiskRange = SpecialTermsConditions.TotalPriceRiskRange;
                newSpecialTermsConditions.TotalAdjustmentMethodA = SpecialTermsConditions.TotalAdjustmentMethodA;
                newSpecialTermsConditions.TotalAdjustmentMethodB = SpecialTermsConditions.TotalAdjustmentMethodB;
                newSpecialTermsConditions.OtherPriceForms = SpecialTermsConditions.OtherPriceForms;
                newSpecialTermsConditions.MarketPriceRange = SpecialTermsConditions.MarketPriceRange;
                newSpecialTermsConditions.DifferenceRange = SpecialTermsConditions.DifferenceRange;
                newSpecialTermsConditions.PricingWay = SpecialTermsConditions.PricingWay;
                newSpecialTermsConditions.QuantityCalculationRules = SpecialTermsConditions.QuantityCalculationRules;
                newSpecialTermsConditions.AdvancePayment = SpecialTermsConditions.AdvancePayment;
                newSpecialTermsConditions.AdvancePaymentPeriod = SpecialTermsConditions.AdvancePaymentPeriod;
                newSpecialTermsConditions.LatePaymentAdvance = SpecialTermsConditions.LatePaymentAdvance;
                newSpecialTermsConditions.ProgressPaymentContents = SpecialTermsConditions.ProgressPaymentContents;
                newSpecialTermsConditions.ProgressPaymentConvention = SpecialTermsConditions.ProgressPaymentConvention;
                newSpecialTermsConditions.GuaranteedScopeWork = SpecialTermsConditions.GuaranteedScopeWork;
                newSpecialTermsConditions.GuaranteedCostStandard = SpecialTermsConditions.GuaranteedCostStandard;
                newSpecialTermsConditions.AcceptanceCondition = SpecialTermsConditions.AcceptanceCondition;
                newSpecialTermsConditions.UnqualifiedResponsibility = SpecialTermsConditions.UnqualifiedResponsibility;
                newSpecialTermsConditions.CleanExitTimeLimit = SpecialTermsConditions.CleanExitTimeLimit;
                newSpecialTermsConditions.DataTransferTimeLimit = SpecialTermsConditions.DataTransferTimeLimit;
                newSpecialTermsConditions.DataNumContents = SpecialTermsConditions.DataNumContents;
                newSpecialTermsConditions.DataListing = SpecialTermsConditions.DataListing;
                newSpecialTermsConditions.FinalSettlementNum = SpecialTermsConditions.FinalSettlementNum;
                newSpecialTermsConditions.DefectLiabilityDate = SpecialTermsConditions.DefectLiabilityDate;
                newSpecialTermsConditions.DefectLiabilityPeriod = SpecialTermsConditions.DefectLiabilityPeriod;
                newSpecialTermsConditions.WarrantyPeriodDate = SpecialTermsConditions.WarrantyPeriodDate;
                newSpecialTermsConditions.WarrantyPeriodPeriod = SpecialTermsConditions.WarrantyPeriodPeriod;
                newSpecialTermsConditions.MarginDetainWay = SpecialTermsConditions.MarginDetainWay;
                newSpecialTermsConditions.DefaultMethod = SpecialTermsConditions.DefaultMethod;
                newSpecialTermsConditions.TerminationContract = SpecialTermsConditions.TerminationContract;
                newSpecialTermsConditions.DefaultLiability = SpecialTermsConditions.DefaultLiability;
                newSpecialTermsConditions.SubDefaultCancelContract = SpecialTermsConditions.SubDefaultCancelContract;
                newSpecialTermsConditions.ForceMajeure = SpecialTermsConditions.ForceMajeure;
                newSpecialTermsConditions.NotConsideredForceMajeure = SpecialTermsConditions.NotConsideredForceMajeure;
                newSpecialTermsConditions.GeneralContractorShallPay = SpecialTermsConditions.GeneralContractorShallPay;
                newSpecialTermsConditions.LimitIndemnity = SpecialTermsConditions.LimitIndemnity;
                newSpecialTermsConditions.InsuredAmount = SpecialTermsConditions.InsuredAmount;
                newSpecialTermsConditions.CertificateInsurance = SpecialTermsConditions.CertificateInsurance;
                newSpecialTermsConditions.ArbitrationCommission = SpecialTermsConditions.ArbitrationCommission;
                newSpecialTermsConditions.PeopleCourt = SpecialTermsConditions.PeopleCourt;
                try
                {
                    Funs.DB.SubmitChanges();
                }
                catch (System.Data.Linq.ChangeConflictException ex)
                {
                    Funs.DB.ChangeConflicts.ResolveAll(RefreshMode.KeepCurrentValues);  //保持当前的值

                    Funs.DB.ChangeConflicts.ResolveAll(RefreshMode.OverwriteCurrentValues);//保持原来的更新,放弃了当前的值.
                    Funs.DB.ChangeConflicts.ResolveAll(RefreshMode.KeepChanges);//保存原来的值 有冲突的话保存当前版本


                    Funs.DB.SubmitChanges();
                }

               // Funs.DB.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除专业合同条款基本信息
        /// </summary>
        /// <param name="SpecialTermsConditionsId"></param>
        public static void DeleteSpecialTermsConditionsById(string SpecialTermsConditionsId)
        {
            Model.PHTGL_SpecialTermsConditions SpecialTermsConditions = Funs.DB.PHTGL_SpecialTermsConditions.FirstOrDefault(e => e.SpecialTermsConditionsId == SpecialTermsConditionsId);
            if (SpecialTermsConditions != null)
            {
                Funs.DB.PHTGL_SpecialTermsConditions.DeleteOnSubmit(SpecialTermsConditions);
                try
                {
                    Funs.DB.SubmitChanges();
                }
                catch (System.Data.Linq.ChangeConflictException ex)
                {
                    Funs.DB.ChangeConflicts.ResolveAll(RefreshMode.KeepCurrentValues);  //保持当前的值

                    Funs.DB.ChangeConflicts.ResolveAll(RefreshMode.OverwriteCurrentValues);//保持原来的更新,放弃了当前的值.
                    Funs.DB.ChangeConflicts.ResolveAll(RefreshMode.KeepChanges);//保存原来的值 有冲突的话保存当前版本


                    Funs.DB.SubmitChanges();
                }
            }
        }
        /// <summary>
        /// 根据合同编号删除专业合同条款基本信息
        /// </summary>
        /// <param name="SpecialTermsConditionsId"></param>
        public static void DeleteSpecialTermsConditionsBycontractId(string contractId)
        {
            Model.PHTGL_SpecialTermsConditions SpecialTermsConditions = Funs.DB.PHTGL_SpecialTermsConditions.FirstOrDefault(e => e.ContractId == contractId);
            if (SpecialTermsConditions != null)
            {
                Funs.DB.PHTGL_SpecialTermsConditions.DeleteOnSubmit(SpecialTermsConditions);
                try
                {
                    Funs.DB.SubmitChanges();
                }
                catch (System.Data.Linq.ChangeConflictException ex)
                {
                    Funs.DB.ChangeConflicts.ResolveAll(RefreshMode.KeepCurrentValues);  //保持当前的值

                    Funs.DB.ChangeConflicts.ResolveAll(RefreshMode.OverwriteCurrentValues);//保持原来的更新,放弃了当前的值.
                    Funs.DB.ChangeConflicts.ResolveAll(RefreshMode.KeepChanges);//保存原来的值 有冲突的话保存当前版本


                    Funs.DB.SubmitChanges();
                }
            }
        }
    }
}
