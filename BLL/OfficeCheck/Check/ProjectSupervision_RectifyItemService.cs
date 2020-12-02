using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
  public static class ProjectSupervision_RectifyItemService
    {
        /// <summary>
        /// 根据主键获取整改明细
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public static Model.ProjectSupervision_RectifyItem GeRectifyItemById(string itemId)
        {
            return Funs.DB.ProjectSupervision_RectifyItem.FirstOrDefault(e => e.RectifyItemId == itemId);
        }

        public static List<Model.ProjectSupervision_RectifyItem> GetRectifyItemByRectifyId(string rectifyId)
        {
            return (from x in Funs.DB.ProjectSupervision_RectifyItem where x.RectifyId == rectifyId select x).ToList();
        }

        public static void AddRectifyItem(Model.ProjectSupervision_RectifyItem rectifyItem)
        {
            Model.ProjectSupervision_RectifyItem newRectifyItem = new Model.ProjectSupervision_RectifyItem();
            newRectifyItem.RectifyItemId = rectifyItem.RectifyItemId;
            newRectifyItem.RectifyId = rectifyItem.RectifyId;
            newRectifyItem.WrongContent = rectifyItem.WrongContent;
            newRectifyItem.Requirement = rectifyItem.Requirement;
            newRectifyItem.LimitTime = rectifyItem.LimitTime;
            newRectifyItem.RectifyResults = rectifyItem.RectifyResults;
            newRectifyItem.IsRectify = rectifyItem.IsRectify;
            Funs.DB.ProjectSupervision_RectifyItem.InsertOnSubmit(newRectifyItem);
            Funs.DB.SubmitChanges();
        }

        public static void DeleteRectifyItemByRectifyId(string rectifyId)
        {
            var q = (from x in Funs.DB.ProjectSupervision_RectifyItem where x.RectifyId == rectifyId select x).ToList();
            if (q!=null)
            {
                Funs.DB.ProjectSupervision_RectifyItem.DeleteAllOnSubmit(q);
                Funs.DB.SubmitChanges();
            }
        }

        public static void DeleteRectifyItemById(string id)
        {
            Model.ProjectSupervision_RectifyItem item = Funs.DB.ProjectSupervision_RectifyItem.FirstOrDefault(e => e.RectifyItemId == id);
            if (item!=null)
            {
                Funs.DB.ProjectSupervision_RectifyItem.DeleteOnSubmit(item);
                Funs.DB.SubmitChanges();
            }
        }
    }
}
