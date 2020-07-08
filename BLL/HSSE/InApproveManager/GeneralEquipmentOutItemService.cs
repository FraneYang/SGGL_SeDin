using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 一般设备机具出场报批明细
    /// </summary>
    public static class GeneralEquipmentOutItemService
    {
        public static Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);

        /// <summary>
        /// 根据主键获取一般设备机具出场报批
        /// </summary>
        /// <param name="generalEquipmentOutItemId"></param>
        /// <returns></returns>
        public static Model.InApproveManager_GeneralEquipmentOutItem GetGeneralEquipmentOutItemById(string generalEquipmentOutItemId)
        {
            return new Model.SGGLDB(Funs.ConnString).InApproveManager_GeneralEquipmentOutItem.FirstOrDefault(e => e.GeneralEquipmentOutItemId == generalEquipmentOutItemId);
        }

        /// <summary>
        /// 根据一般设备机具出场报批ID获取所有相关明细信息
        /// </summary>
        /// <param name="generalEquipmentOutId"></param>
        /// <returns></returns>
        public static List<Model.InApproveManager_GeneralEquipmentOutItem> GetGeneralEquipmentOutItemByGeneralEquipmentOutId(string generalEquipmentOutId)
        {
            return (from x in new Model.SGGLDB(Funs.ConnString).InApproveManager_GeneralEquipmentOutItem where x.GeneralEquipmentOutId == generalEquipmentOutId select x).ToList();
        }

        /// <summary>
        /// 添加一般设备机具出场报批明细
        /// </summary>
        /// <param name="generalEquipmentOutItem"></param>
        public static void AddGeneralEquipmentOutItem(Model.InApproveManager_GeneralEquipmentOutItem generalEquipmentOutItem)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.InApproveManager_GeneralEquipmentOutItem newGeneralEquipmentOutItem = new Model.InApproveManager_GeneralEquipmentOutItem
            {
                GeneralEquipmentOutItemId = generalEquipmentOutItem.GeneralEquipmentOutItemId,
                GeneralEquipmentOutId = generalEquipmentOutItem.GeneralEquipmentOutId,
                SpecialEquipmentId = generalEquipmentOutItem.SpecialEquipmentId,
                SizeModel = generalEquipmentOutItem.SizeModel,
                OwnerCheck = generalEquipmentOutItem.OwnerCheck,
                CertificateNum = generalEquipmentOutItem.CertificateNum,
                InsuranceNum = generalEquipmentOutItem.InsuranceNum,
                OutReason = generalEquipmentOutItem.OutReason
            };
            db.InApproveManager_GeneralEquipmentOutItem.InsertOnSubmit(newGeneralEquipmentOutItem);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改一般设备机具出场报批明细
        /// </summary>
        /// <param name="generalEquipmentOutItem"></param>
        public static void UpdateGeneralEquipmentOutItem(Model.InApproveManager_GeneralEquipmentOutItem generalEquipmentOutItem)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.InApproveManager_GeneralEquipmentOutItem newGeneralEquipmentOutItem = db.InApproveManager_GeneralEquipmentOutItem.FirstOrDefault(e => e.GeneralEquipmentOutItemId == generalEquipmentOutItem.GeneralEquipmentOutItemId);
            if (newGeneralEquipmentOutItem != null)
            {
                newGeneralEquipmentOutItem.GeneralEquipmentOutId = generalEquipmentOutItem.GeneralEquipmentOutId;
                newGeneralEquipmentOutItem.SpecialEquipmentId = generalEquipmentOutItem.SpecialEquipmentId;
                newGeneralEquipmentOutItem.SizeModel = generalEquipmentOutItem.SizeModel;
                newGeneralEquipmentOutItem.OwnerCheck = generalEquipmentOutItem.OwnerCheck;
                newGeneralEquipmentOutItem.CertificateNum = generalEquipmentOutItem.CertificateNum;
                newGeneralEquipmentOutItem.InsuranceNum = generalEquipmentOutItem.InsuranceNum;
                newGeneralEquipmentOutItem.OutReason = generalEquipmentOutItem.OutReason;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除一般设备机具出场报批明细
        /// </summary>
        /// <param name="generalEquipmentOutItemId"></param>
        public static void DeleteGeneralEquipmentOutItemById(string generalEquipmentOutItemId)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.InApproveManager_GeneralEquipmentOutItem generalEquipmentOutItem = db.InApproveManager_GeneralEquipmentOutItem.FirstOrDefault(e => e.GeneralEquipmentOutItemId == generalEquipmentOutItemId);
            if (generalEquipmentOutItem != null)
            {
                db.InApproveManager_GeneralEquipmentOutItem.DeleteOnSubmit(generalEquipmentOutItem);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据一般设备报批主键删除所有相关明细信息
        /// </summary>
        /// <param name="generalEquipmentOutId"></param>
        public static void DeleteGeneralEquipmentOutItemByGeneralEquipmentOutId(string generalEquipmentOutId)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            var q = (from x in db.InApproveManager_GeneralEquipmentOutItem where x.GeneralEquipmentOutId == generalEquipmentOutId select x).ToList();
            if (q != null)
            {
                db.InApproveManager_GeneralEquipmentOutItem.DeleteAllOnSubmit(q);
                db.SubmitChanges();
            }
        }
    }
}

