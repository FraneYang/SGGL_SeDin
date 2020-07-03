using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
   public static class ConstructionStandardSelectedItemService
    {
       public static Model.SGGLDB db = Funs.DB;

       /// <summary>
       /// 根据标准规范辨识Id获取对应所有标准规范项信息
       /// </summary>
       /// <param name="constructionStandardIdentifyId">标准规范辨识Id</param>
       /// <returns>所有标准规范项信息</returns>
       public static List<Model.InformationProject_ConstructionStandardSelectedItem> GetConstructionStandardSelectedItemsByConstructionStandardIdentifyId(string constructionStandardIdentifyId)
       {
           return (from x in BLL.Funs.DB.InformationProject_ConstructionStandardSelectedItem where x.ConstructionStandardIdentifyId == constructionStandardIdentifyId select x).ToList();
       }

       /// <summary>
       /// 增加标准规范项信息
       /// </summary>
       /// <param name="lawRegulationSelectedItem">标准规范项实体</param>
       public static void AddConstructionStandardSelectedItem(Model.InformationProject_ConstructionStandardSelectedItem constructionStandardSelectedItem)
       {
           Model.SGGLDB db = Funs.DB;
           string newKeyID = SQLHelper.GetNewID(typeof(Model.InformationProject_ConstructionStandardSelectedItem));
            Model.InformationProject_ConstructionStandardSelectedItem newLawRegulationSelectedItem = new Model.InformationProject_ConstructionStandardSelectedItem
            {
                ConstructionStandardSelectedItemId = newKeyID,
                ConstructionStandardIdentifyId = constructionStandardSelectedItem.ConstructionStandardIdentifyId,
                StandardId = constructionStandardSelectedItem.StandardId
            };
            db.InformationProject_ConstructionStandardSelectedItem.InsertOnSubmit(newLawRegulationSelectedItem);
           db.SubmitChanges();
       }

       /// <summary>
       /// 根据标准规范辨识主键删除对应的所有标准规范项信息
       /// </summary>
       /// <param name="constructionStandardIdentifyId">标准规范辨识主键</param>
       public static void DeleteConstructionStandardSelectedItemByConstructionStandardIdentifyId(string constructionStandardIdentifyId)
       {
           Model.SGGLDB db = Funs.DB;
           var q = (from x in db.InformationProject_ConstructionStandardSelectedItem where x.ConstructionStandardIdentifyId == constructionStandardIdentifyId select x).ToList();
           if (q != null)
           {
               db.InformationProject_ConstructionStandardSelectedItem.DeleteAllOnSubmit(q);
               db.SubmitChanges();
           }
       }
    }
}
