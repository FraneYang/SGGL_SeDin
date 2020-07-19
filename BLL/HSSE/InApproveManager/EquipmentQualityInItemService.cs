using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 特种设备明细
    /// </summary>
   public static class EquipmentQualityInItemService
   {
       public static Model.SGGLDB db = Funs.DB;

       /// <summary>
       /// 根据主表ID获取明细信息
       /// </summary>
       /// <param name="EquipmentQualityInId"></param>
       /// <returns></returns>
       public static List<Model.InApproveManager_EquipmentQualityInItem> GetEquipmentQualityInItemByEquipmentQualityInId(string EquipmentQualityInId)
       {
           return (from x in Funs.DB.InApproveManager_EquipmentQualityInItem where x.EquipmentQualityInId == EquipmentQualityInId select x).ToList();
       }

       /// <summary>
       /// 添加特种设备明细
       /// </summary>
       /// <param name="EquipmentQualityInItem"></param>
       public static void AddEquipmentQualityInItem(Model.InApproveManager_EquipmentQualityInItem EquipmentQualityInItem)
       {
           Model.SGGLDB db = Funs.DB;
            Model.InApproveManager_EquipmentQualityInItem newEquipmentQualityInItem = new Model.InApproveManager_EquipmentQualityInItem
            {
                EquipmentQualityInItemId = EquipmentQualityInItem.EquipmentQualityInItemId,
                EquipmentQualityInId = EquipmentQualityInItem.EquipmentQualityInId,
                CheckItem1 = EquipmentQualityInItem.CheckItem1,
                CheckItem2 = EquipmentQualityInItem.CheckItem2,
                CheckItem3 = EquipmentQualityInItem.CheckItem3,
                CheckItem4 = EquipmentQualityInItem.CheckItem4,
                CheckItem5 = EquipmentQualityInItem.CheckItem5,
                CheckItem6 = EquipmentQualityInItem.CheckItem6,
                CheckItem7 = EquipmentQualityInItem.CheckItem7,
                CheckItem8 = EquipmentQualityInItem.CheckItem8,
                CheckItem9 = EquipmentQualityInItem.CheckItem9,
                CheckItem10 = EquipmentQualityInItem.CheckItem10,
                CheckItem11 = EquipmentQualityInItem.CheckItem11,
                CheckItem12 = EquipmentQualityInItem.CheckItem12,
                CheckItem13 = EquipmentQualityInItem.CheckItem13,
                CheckItem14 = EquipmentQualityInItem.CheckItem14,
                CheckItem15 = EquipmentQualityInItem.CheckItem15,
                CheckItem16 = EquipmentQualityInItem.CheckItem16,
                CheckItem17 = EquipmentQualityInItem.CheckItem17,
                CheckItem18 = EquipmentQualityInItem.CheckItem18,
                CheckItem19 = EquipmentQualityInItem.CheckItem19,
                CheckItem20 = EquipmentQualityInItem.CheckItem20
            };
            db.InApproveManager_EquipmentQualityInItem.InsertOnSubmit(newEquipmentQualityInItem);
           db.SubmitChanges();
       }

       /// <summary>
       /// 根据特种设备审批ID删除相关明细信息
       /// </summary>
       /// <param name="EquipmentQualityInId"></param>
       public static void DeleteEquipmentQualityInItemByEquipmentQualityInId(string EquipmentQualityInId)
       {
           Model.SGGLDB db = Funs.DB;
           var q = (from x in db.InApproveManager_EquipmentQualityInItem where x.EquipmentQualityInId == EquipmentQualityInId select x).ToList();
           if (q != null)
           {
               db.InApproveManager_EquipmentQualityInItem.DeleteAllOnSubmit(q);
               db.SubmitChanges();
           }
       }
   }
}
