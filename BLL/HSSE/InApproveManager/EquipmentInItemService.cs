using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 特种设备机具入场报批明细表
    /// </summary>
    public static class EquipmentInItemService
    {
        public static Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);

        /// <summary>
        /// 根据主键获取特种设备机具入场报批明细信息
        /// </summary>
        /// <param name="equipmentInItemId"></param>
        /// <returns></returns>
        public static Model.InApproveManager_EquipmentInItem GetEquipmentInItemById(string equipmentInItemId)
        {
            return new Model.SGGLDB(Funs.ConnString).InApproveManager_EquipmentInItem.FirstOrDefault(e => e.EquipmentInItemId == equipmentInItemId);
        }

        /// <summary>
        /// 根据特种设备机具入场报批ID获取所有相关明细信息
        /// </summary>
        /// <param name="equipmentInId"></param>
        /// <returns></returns>
        public static List<Model.InApproveManager_EquipmentInItem> GetEquipmentInItemByEquipmentInId(string equipmentInId)
        {
            return (from x in new Model.SGGLDB(Funs.ConnString).InApproveManager_EquipmentInItem where x.EquipmentInId == equipmentInId select x).ToList();
        }

        /// <summary>
        /// 添加特种机具设备入场报批明细
        /// </summary>
        /// <param name="equipmentInItem"></param>
        public static void AddEquipmentInItem(Model.InApproveManager_EquipmentInItem equipmentInItem)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.InApproveManager_EquipmentInItem newEquipmentItem = new Model.InApproveManager_EquipmentInItem
            {
                EquipmentInItemId = equipmentInItem.EquipmentInItemId,
                EquipmentInId = equipmentInItem.EquipmentInId,
                SpecialEquipmentId = equipmentInItem.SpecialEquipmentId,
                SizeModel = equipmentInItem.SizeModel,
                OwnerCheck = equipmentInItem.OwnerCheck,
                CertificateNum = equipmentInItem.CertificateNum,
                SafetyInspectionNum = equipmentInItem.SafetyInspectionNum,
                DrivingLicenseNum = equipmentInItem.DrivingLicenseNum,
                RegistrationNum = equipmentInItem.RegistrationNum,
                OperationQualificationNum = equipmentInItem.OperationQualificationNum,
                InsuranceNum = equipmentInItem.InsuranceNum,
                CommercialInsuranceNum = equipmentInItem.CommercialInsuranceNum
            };
            db.InApproveManager_EquipmentInItem.InsertOnSubmit(newEquipmentItem);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改特种机具设备入场报批明细
        /// </summary>
        /// <param name="equipmentInItem"></param>
        public static void UpdateEquipmentInItem(Model.InApproveManager_EquipmentInItem equipmentInItem)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.InApproveManager_EquipmentInItem newEquipmentItem = db.InApproveManager_EquipmentInItem.FirstOrDefault(e => e.EquipmentInItemId == equipmentInItem.EquipmentInItemId);
            if (newEquipmentItem != null)
            {
                newEquipmentItem.EquipmentInId = equipmentInItem.EquipmentInId;
                newEquipmentItem.SpecialEquipmentId = equipmentInItem.SpecialEquipmentId;
                newEquipmentItem.SizeModel = equipmentInItem.SizeModel;
                newEquipmentItem.OwnerCheck = equipmentInItem.OwnerCheck;
                newEquipmentItem.CertificateNum = equipmentInItem.CertificateNum;
                newEquipmentItem.SafetyInspectionNum = equipmentInItem.SafetyInspectionNum;
                newEquipmentItem.DrivingLicenseNum = equipmentInItem.DrivingLicenseNum;
                newEquipmentItem.RegistrationNum = equipmentInItem.RegistrationNum;
                newEquipmentItem.OperationQualificationNum = equipmentInItem.OperationQualificationNum;
                newEquipmentItem.InsuranceNum = equipmentInItem.InsuranceNum;
                newEquipmentItem.CommercialInsuranceNum = equipmentInItem.CommercialInsuranceNum;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除特种机具设备入场报批明细
        /// </summary>
        /// <param name="equipmentInItemId"></param>
        public static void DeleteEquipmentInItemById(string equipmentInItemId)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.InApproveManager_EquipmentInItem equipmenetInItem = db.InApproveManager_EquipmentInItem.FirstOrDefault(e => e.EquipmentInItemId == equipmentInItemId);
            if (equipmenetInItem != null)
            {
                db.InApproveManager_EquipmentInItem.DeleteOnSubmit(equipmenetInItem);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据特种机具设备入场报批ID删除所有相关明细信息
        /// </summary>
        /// <param name="equipmentInId"></param>
        public static void DeleteEquipmentInItemByEquipmentInId(string equipmentInId)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            var q = (from x in new Model.SGGLDB(Funs.ConnString).InApproveManager_EquipmentInItem where x.EquipmentInId == equipmentInId select x).ToList();
            if (q != null)
            {
                db.InApproveManager_EquipmentInItem.DeleteAllOnSubmit(q);
                db.SubmitChanges();
            }
        }
    }
}
