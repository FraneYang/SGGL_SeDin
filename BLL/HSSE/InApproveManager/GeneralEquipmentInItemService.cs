using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    /// <summary>
    /// 一般设备机具入场报批明细
    /// </summary>
    public static class GeneralEquipmentInItemService
    {
        public static Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);

        /// <summary>
        /// 根据主键获取一般设备机具入场报批明细信息
        /// </summary>
        /// <param name="GeneralEquipmentInItemId"></param>
        /// <returns></returns>
        public static Model.InApproveManager_GeneralEquipmentInItem GetGeneralEquipmentInItemById(string generalEquipmentInItemId)
        {
            return new Model.SGGLDB(Funs.ConnString).InApproveManager_GeneralEquipmentInItem.FirstOrDefault(e => e.GeneralEquipmentInItemId == generalEquipmentInItemId);
        }

        /// <summary>
        /// 根据一般设备机具入场报批ID获取所有相关明细信息
        /// </summary>
        /// <param name="equipmentInId"></param>
        /// <returns></returns>
        public static List<Model.InApproveManager_GeneralEquipmentInItem> GetGeneralEquipmentInItemByGeneralEquipmentInId(string generalEquipmentInId)
        {
            return (from x in new Model.SGGLDB(Funs.ConnString).InApproveManager_GeneralEquipmentInItem where x.GeneralEquipmentInId == generalEquipmentInId select x).ToList();
        }

        /// <summary>
        /// 添加一般机具设备入场报批明细
        /// </summary>
        /// <param name="GeneralEquipmentInItem"></param>
        public static void AddGeneralEquipmentInItem(Model.InApproveManager_GeneralEquipmentInItem generalEquipmentInItem)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.InApproveManager_GeneralEquipmentInItem newEquipmentItem = new Model.InApproveManager_GeneralEquipmentInItem
            {
                GeneralEquipmentInItemId = generalEquipmentInItem.GeneralEquipmentInItemId,
                GeneralEquipmentInId = generalEquipmentInItem.GeneralEquipmentInId,
                SpecialEquipmentId = generalEquipmentInItem.SpecialEquipmentId,
                SizeModel = generalEquipmentInItem.SizeModel,
                OwnerCheck = generalEquipmentInItem.OwnerCheck,
                CertificateNum = generalEquipmentInItem.CertificateNum
            };
            db.InApproveManager_GeneralEquipmentInItem.InsertOnSubmit(newEquipmentItem);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改一般机具设备入场报批明细
        /// </summary>
        /// <param name="GeneralEquipmentInItem"></param>
        public static void UpdateGeneralEquipmentInItem(Model.InApproveManager_GeneralEquipmentInItem generalEquipmentInItem)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.InApproveManager_GeneralEquipmentInItem newGeneralEquipmentInItem = db.InApproveManager_GeneralEquipmentInItem.FirstOrDefault(e => e.GeneralEquipmentInItemId == generalEquipmentInItem.GeneralEquipmentInItemId);
            if (newGeneralEquipmentInItem != null)
            {
                newGeneralEquipmentInItem.GeneralEquipmentInId = generalEquipmentInItem.GeneralEquipmentInId;
                newGeneralEquipmentInItem.SpecialEquipmentId = generalEquipmentInItem.SpecialEquipmentId;
                newGeneralEquipmentInItem.SizeModel = generalEquipmentInItem.SizeModel;
                newGeneralEquipmentInItem.OwnerCheck = generalEquipmentInItem.OwnerCheck;
                newGeneralEquipmentInItem.CertificateNum = generalEquipmentInItem.CertificateNum;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除一般机具设备入场报批明细
        /// </summary>
        /// <param name="GeneralEquipmentInItemId"></param>
        public static void DeleteGeneralEquipmentInItemById(string generalEquipmentInItemId)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.InApproveManager_GeneralEquipmentInItem generalEquipmentInItem = db.InApproveManager_GeneralEquipmentInItem.FirstOrDefault(e => e.GeneralEquipmentInItemId == generalEquipmentInItemId);
            if (generalEquipmentInItem != null)
            {
                db.InApproveManager_GeneralEquipmentInItem.DeleteOnSubmit(generalEquipmentInItem);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据一般机具设备入场报批ID删除所有相关明细信息
        /// </summary>
        /// <param name="equipmentInId"></param>
        public static void DeleteGeneralEquipmentInItemByEquipmentInId(string generalEquipmentInId)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            var q = (from x in new Model.SGGLDB(Funs.ConnString).InApproveManager_GeneralEquipmentInItem where x.GeneralEquipmentInId == generalEquipmentInId select x).ToList();
            if (q != null)
            {
                db.InApproveManager_GeneralEquipmentInItem.DeleteAllOnSubmit(q);
                db.SubmitChanges();
            }
        }
    }
}
