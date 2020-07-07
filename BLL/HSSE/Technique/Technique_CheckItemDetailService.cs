using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 检查项明细
    /// </summary>
    public static class Technique_CheckItemDetailService
    {
        public static Model.SGGLDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取检查项明细
        /// </summary>
        /// <param name="checkItemDetailId"></param>
        /// <returns></returns>
        public static Model.Technique_CheckItemDetail GetCheckItemDetailById(string checkItemDetailId)
        {
            return Funs.DB.Technique_CheckItemDetail.FirstOrDefault(e => e.CheckItemDetailId == checkItemDetailId);
        }

        /// <summary>
        /// 添加检查项明细
        /// </summary>
        /// <param name="checkItemDetail"></param>
        public static void AddCheckItemDetail(Model.Technique_CheckItemDetail checkItemDetail)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Technique_CheckItemDetail newCheckItemDetail = new Model.Technique_CheckItemDetail
            {
                CheckItemDetailId = checkItemDetail.CheckItemDetailId,
                CheckItemSetId = checkItemDetail.CheckItemSetId,
                CheckContent = checkItemDetail.CheckContent,
                SortIndex = checkItemDetail.SortIndex,
                IsBuiltIn = checkItemDetail.IsBuiltIn
            };
            db.Technique_CheckItemDetail.InsertOnSubmit(newCheckItemDetail);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改检查项明细
        /// </summary>
        /// <param name="checkItemDetail"></param>
        public static void UpdateCheckItemDetail(Model.Technique_CheckItemDetail checkItemDetail)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Technique_CheckItemDetail newCheckItemDetail = db.Technique_CheckItemDetail.FirstOrDefault(e => e.CheckItemDetailId == checkItemDetail.CheckItemDetailId);
            if (newCheckItemDetail != null)
            {
                newCheckItemDetail.CheckContent = checkItemDetail.CheckContent;
                newCheckItemDetail.SortIndex = checkItemDetail.SortIndex;
                newCheckItemDetail.IsBuiltIn = checkItemDetail.IsBuiltIn;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除检查项明细
        /// </summary>
        /// <param name="checkItemDetailId"></param>
        public static void DeleteCheckItemDetail(string checkItemDetailId)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Technique_CheckItemDetail checkItemDetail = db.Technique_CheckItemDetail.FirstOrDefault(e => e.CheckItemDetailId == checkItemDetailId);
            if (checkItemDetail != null)
            {
                db.Technique_CheckItemDetail.DeleteOnSubmit(checkItemDetail);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据检查项主键删除所有相关明细信息
        /// </summary>
        /// <param name="rectifyId"></param>
        public static void DeleteCheckItemDetailByCheckItemSetId(string checkItemSetId)
        {
            Model.SGGLDB db = Funs.DB;
            var q = (from x in db.Technique_CheckItemDetail where x.CheckItemSetId == checkItemSetId select x).ToList();
            if (q.Count() > 0)
            {
                db.Technique_CheckItemDetail.DeleteAllOnSubmit(q);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 获取一级节点检查类型
        /// </summary>
        /// <param name="CheckItem"></param>
        /// <returns></returns>
        public static string ConvertCheckItemType(object CheckItem)
        {
            string type = string.Empty;
            if (CheckItem != null)
            {
                var detail = Funs.DB.Technique_CheckItemDetail.FirstOrDefault(e => e.CheckItemDetailId == CheckItem.ToString());
                if (detail != null)
                {
                    var item = Funs.DB.Technique_CheckItemSet.FirstOrDefault (x=>x.CheckItemSetId == detail.CheckItemSetId);
                    if (item != null)
                    {
                        if (item.SupCheckItem == "0")
                        {
                            type = item.CheckItemName;
                        }
                        else
                        {
                            type = GetCheckItemNameBySupCheckItem(item.SupCheckItem);
                        }
                    }
                }
                else
                {
                    var  item = Funs.DB.Technique_CheckItemSet.FirstOrDefault(x => x.CheckItemSetId == CheckItem.ToString());
                    if (item != null)
                    {
                        if (item.SupCheckItem == "0")
                        {
                            type = item.CheckItemName;
                        }
                        else
                        {
                            type = GetCheckItemNameBySupCheckItem(item.SupCheckItem);
                        }
                    }
                }
            }
            return type;
        }

        /// <summary>
        /// 根据主键获取顶级检查项名称
        /// </summary>
        /// <param name="checkItemSetId"></param>
        /// <returns></returns>
        public static string GetCheckItemNameBySupCheckItem(string supCheckItem)
        {
            string name = string.Empty;
            var checkItemSet = Funs.DB.Technique_CheckItemSet.FirstOrDefault(e => e.CheckItemSetId == supCheckItem);
            if (checkItemSet != null)
            {
                if (checkItemSet.SupCheckItem == "0")
                {
                    name = checkItemSet.CheckItemName;
                }
                else
                {
                    name = GetCheckItemNameBySupCheckItem(checkItemSet.SupCheckItem);
                }
            }
            return name;
        }
    }
}
