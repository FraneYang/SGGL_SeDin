using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 检查项
    /// </summary>
    public static class Technique_CheckItemSetService
    {
        public static Model.SGGLDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取检查项
        /// </summary>
        /// <param name="checkItemSetId"></param>
        /// <returns></returns>
        public static Model.Technique_CheckItemSet GetCheckItemSetById(string checkItemSetId)
        {
            return Funs.DB.Technique_CheckItemSet.FirstOrDefault(e => e.CheckItemSetId == checkItemSetId);
        }
        /// <summary>
        /// 根据主键获取检查项名称
        /// </summary>
        /// <param name="checkItemSetId"></param>
        /// <returns></returns>
        public static string GetCheckItemSetNameById(string checkItemSetId)
        {
            string name = string.Empty;
            var getItem= Funs.DB.Technique_CheckItemSet.FirstOrDefault(e => e.CheckItemSetId == checkItemSetId);
            if (getItem != null)
            {
                name = getItem.CheckItemName;
            }
            return name;
        }
        /// <summary>
        /// 根据上一节点id获取检查项
        /// </summary>
        /// <param name="supCheckItemSetId"></param>
        /// <returns></returns>
        public static List<Model.Technique_CheckItemSet> GetCheckItemSetBySupCheckItemSetId(string supCheckItemSetId)
        {
            return (from x in Funs.DB.Technique_CheckItemSet where x.SupCheckItem == supCheckItemSetId select x).ToList();
        }

        /// <summary>
        ///  根据类型上级节点获取检查列表
        /// </summary>
        /// <param name="checkType"></param>
        /// <param name="supCheckItemSetId"></param>
        /// <returns></returns>
        public static List<Model.Technique_CheckItemSet> GetCheckItemSetListByCheckTypeSupCheckItem(string checkType, string supCheckItemSetId)
        {
            return (from x in Funs.DB.Technique_CheckItemSet
                    where x.CheckType == checkType && x.SupCheckItem == supCheckItemSetId
                    orderby x.MapCode
                    select x).ToList();
        }


        /// <summary>
        /// 添加检查项
        /// </summary>
        /// <param name="checkItemSet"></param>
        public static void AddCheckItemSet(Model.Technique_CheckItemSet checkItemSet)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Technique_CheckItemSet newCheckItemSet = new Model.Technique_CheckItemSet
            {
                CheckItemSetId = checkItemSet.CheckItemSetId,
                CheckItemName = checkItemSet.CheckItemName,
                SupCheckItem = checkItemSet.SupCheckItem,
                CheckType = checkItemSet.CheckType,
                MapCode = checkItemSet.MapCode,
                IsEndLever = checkItemSet.IsEndLever,
                SortIndex = checkItemSet.SortIndex,
                IsBuiltIn = checkItemSet.IsBuiltIn
            };
            db.Technique_CheckItemSet.InsertOnSubmit(newCheckItemSet);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改检查项
        /// </summary>
        /// <param name="checkItemSet"></param>
        public static void UpdateCheckItemSet(Model.Technique_CheckItemSet checkItemSet)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Technique_CheckItemSet newCheckItemSet = db.Technique_CheckItemSet.FirstOrDefault(e => e.CheckItemSetId == checkItemSet.CheckItemSetId);
            if (newCheckItemSet != null)
            {
                newCheckItemSet.CheckItemName = checkItemSet.CheckItemName;
                newCheckItemSet.SupCheckItem = checkItemSet.SupCheckItem;
                newCheckItemSet.MapCode = checkItemSet.MapCode;
                newCheckItemSet.IsEndLever = checkItemSet.IsEndLever;
                newCheckItemSet.SortIndex = checkItemSet.SortIndex;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除检查项
        /// </summary>
        /// <param name="checkItemSetId"></param>
        public static void DeleteCheckItemSet(string checkItemSetId)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Technique_CheckItemSet checkItemSet = db.Technique_CheckItemSet.FirstOrDefault(e => e.CheckItemSetId == checkItemSetId);
            if (checkItemSet != null)
            {
                db.Technique_CheckItemSet.DeleteOnSubmit(checkItemSet);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 是否末级
        /// </summary>
        /// <param name="checkItemSetId">检查项目主键</param>
        /// <returns>布尔值</returns>
        public static bool IsEndLevel(string checkItemSetId)
        {
            if (checkItemSetId == "0")
            {
                return false;
            }
            else
            {
                Model.Technique_CheckItemSet checkItemSet = Funs.DB.Technique_CheckItemSet.FirstOrDefault(e => e.CheckItemSetId == checkItemSetId);
                return Convert.ToBoolean(checkItemSet.IsEndLever);
            }
        }

        /// <summary>
        /// 是否可删除资源节点
        /// </summary>
        /// <param name="postName"></param>
        /// <returns>true-可以，false-不可以</returns>
        public static bool IsDeleteCheckItemSet(string checkItemSetId)
        {
            bool isDelete = true;
            var checkItemSet = BLL.Technique_CheckItemSetService.GetCheckItemSetById(checkItemSetId);
            if (checkItemSet != null)
            {
                //if (checkItemSet.IsBuiltIn == true)
                //{
                //    isDelete = false;
                //}
                if (checkItemSet.IsEndLever == true)
                {
                    var detailCout = Funs.DB.Technique_CheckItemDetail.FirstOrDefault(x => x.CheckItemSetId == checkItemSetId);
                    if (detailCout != null)
                    {
                        isDelete = false;
                    }
                }
                else
                {
                    var supItemSetCount = BLL.Technique_CheckItemSetService.GetCheckItemSetBySupCheckItemSetId(checkItemSetId);
                    if (supItemSetCount.Count() > 0)
                    {
                        isDelete = false;
                    }
                }
            }
            return isDelete;
        }

        /// <summary>
        /// 是否存在检查项名称
        /// </summary>
        /// <param name="postName"></param>
        /// <returns>true-存在，false-不存在</returns>
        public static bool IsExistCheckItemName(string checkItemSetId, string supCheckItem, string checkItemName)
        {
            var q = Funs.DB.Technique_CheckItemSet.FirstOrDefault(x => x.SupCheckItem == supCheckItem && x.CheckItemName == checkItemName
                    && x.CheckItemSetId != checkItemSetId);
            if (q != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #region  检查项表下拉框
        /// <summary>
        ///  检查项表下拉框
        /// </summary>
        /// <param name="dropName">下拉框名字</param>
        /// <param name="isShowPlease">是否显示请选择</param>
        public static void InitCheckItemSetDropDownList(FineUIPro.DropDownList dropName, string checkType, string supCheckItem, bool isShowPlease)
        {
            dropName.DataValueField = "CheckItemSetId";
            dropName.DataTextField = "CheckItemName";
            dropName.DataSource = GetCheckItemSetListByCheckTypeSupCheckItem(checkType, supCheckItem);
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }

        /// <summary>
        ///  检查项表下拉框
        /// </summary>
        /// <param name="dropName">下拉框名字</param>
        /// <param name="isShowPlease">是否显示请选择</param>
        public static void InitCheckItemSetNameDropDownList(FineUIPro.DropDownList dropName, string checkType, string supCheckItem, bool isShowPlease)
        {
            dropName.DataValueField = "CheckItemName";
            dropName.DataTextField = "CheckItemName";
            dropName.DataSource = GetCheckItemSetListByCheckTypeSupCheckItem(checkType, supCheckItem);
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }
        #endregion
    }
}
