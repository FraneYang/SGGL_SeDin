using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 特种设备机具出场报批明细
    /// </summary>
    public static class EquipmentOutItemService
    {
        public static Model.SGGLDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取特种设备机具出场报批明细
        /// </summary>
        /// <param name="equipmentOutItemId"></param>
        /// <returns></returns>
        public static Model.InApproveManager_EquipmentOutItem GetEquipmentOutItemById(string equipmentOutItemId)
        {
            return Funs.DB.InApproveManager_EquipmentOutItem.FirstOrDefault(e => e.EquipmentOutItemId == equipmentOutItemId);
        }

        /// <summary>
        /// 根据特种设备机具出场报批ID删除所有相关明细信息
        /// </summary>
        /// <param name="equipmentOutId"></param>
        /// <returns></returns>
        public static List<Model.InApproveManager_EquipmentOutItem> GetEquipmentOutItemByEquipmentOutId(string equipmentOutId)
        {
            return (from x in Funs.DB.InApproveManager_EquipmentOutItem where x.EquipmentOutId == equipmentOutId select x).ToList();
        }

        /// <summary>
        /// 添加特种设备机具出场报批明细信息
        /// </summary>
        /// <param name="equipmentOutItem"></param>
        public static void AddEquipmentOutItem(Model.InApproveManager_EquipmentOutItem equipmentOutItem)
        {
            Model.SGGLDB db = Funs.DB;
            Model.InApproveManager_EquipmentOutItem newEquipmentOutItem = new Model.InApproveManager_EquipmentOutItem
            {
                EquipmentOutItemId = equipmentOutItem.EquipmentOutItemId,
                EquipmentOutId = equipmentOutItem.EquipmentOutId,
                SpecialEquipmentId = equipmentOutItem.SpecialEquipmentId,
                SizeModel = equipmentOutItem.SizeModel,
                OwnerCheck = equipmentOutItem.OwnerCheck,
                CertificateNum = equipmentOutItem.CertificateNum,
                InsuranceNum = equipmentOutItem.InsuranceNum,
                OutReason = equipmentOutItem.OutReason
            };
            db.InApproveManager_EquipmentOutItem.InsertOnSubmit(newEquipmentOutItem);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改特种设备机具出场报批明细
        /// </summary>
        /// <param name="equipmentItemOut"></param>
        public static void UpdateEquipmentItemOut(Model.InApproveManager_EquipmentOutItem equipmentOutItem)
        {
            Model.SGGLDB db = Funs.DB;
            Model.InApproveManager_EquipmentOutItem newEquipmentOutItem = db.InApproveManager_EquipmentOutItem.FirstOrDefault(e => e.EquipmentOutItemId == equipmentOutItem.EquipmentOutItemId);
            if (newEquipmentOutItem != null)
            {
                newEquipmentOutItem.EquipmentOutId = equipmentOutItem.EquipmentOutId;
                newEquipmentOutItem.SpecialEquipmentId = equipmentOutItem.SpecialEquipmentId;
                newEquipmentOutItem.SizeModel = equipmentOutItem.SizeModel;
                newEquipmentOutItem.OwnerCheck = equipmentOutItem.OwnerCheck;
                newEquipmentOutItem.CertificateNum = equipmentOutItem.CertificateNum;
                newEquipmentOutItem.InsuranceNum = equipmentOutItem.InsuranceNum;
                newEquipmentOutItem.OutReason = equipmentOutItem.OutReason;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除特种设备机具出场报批明细
        /// </summary>
        /// <param name="equipmentItemOutId"></param>
        public static void DeleteEquipmentOutItemById(string equipmentOutItemId)
        {
            Model.SGGLDB db = Funs.DB;
            Model.InApproveManager_EquipmentOutItem equipmentOutItem = db.InApproveManager_EquipmentOutItem.FirstOrDefault(e => e.EquipmentOutItemId == equipmentOutItemId);
            if (equipmentOutItem != null)
            {
                db.InApproveManager_EquipmentOutItem.DeleteOnSubmit(equipmentOutItem);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据特种设备机具出场ID删除所有相关明细信息
        /// </summary>
        /// <param name="equipmentOutId"></param>
        public static void DeleteEquipmentOutItemByEqupmentOutId(string equipmentOutId)
        {
            Model.SGGLDB db = Funs.DB;
            var q = (from x in db.InApproveManager_EquipmentOutItem where x.EquipmentOutId == equipmentOutId select x).ToList();
            if (q != null)
            {
                db.InApproveManager_EquipmentOutItem.DeleteAllOnSubmit(q);
                db.SubmitChanges();
            }
        }
    }
}