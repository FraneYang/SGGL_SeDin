using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 危险源类型
    /// </summary>
    public static class HazardListTypeService
    {
        public static Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);

        /// <summary>
        /// 根据主键获取危险源类型
        /// </summary>
        /// <param name="hazardListTypeId"></param>
        /// <returns></returns>
        public static Model.Technique_HazardListType GetHazardListTypeById(string hazardListTypeId)
        {
            return new Model.SGGLDB(Funs.ConnString).Technique_HazardListType.FirstOrDefault(e => e.HazardListTypeId == hazardListTypeId);
        }

        /// <summary>
        /// 根据上一节点Id获取所有相关危险源类型
        /// </summary>
        /// <param name="supItem"></param>
        /// <returns></returns>
        public static List<Model.Technique_HazardListType> GetHazardListTypeBySupItem(string supItem)
        {
            return (from x in new Model.SGGLDB(Funs.ConnString).Technique_HazardListType where x.SupHazardListTypeId == supItem select x).ToList();
        }

        /// <summary>
        /// 添加危险源类型
        /// </summary>
        /// <param name="hazardListType"></param>
        public static void AddHazardListType(Model.Technique_HazardListType hazardListType)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Technique_HazardListType newHazardListType = new Model.Technique_HazardListType
            {
                HazardListTypeId = hazardListType.HazardListTypeId,
                HazardListTypeCode = hazardListType.HazardListTypeCode,
                HazardListTypeName = hazardListType.HazardListTypeName,
                SupHazardListTypeId = hazardListType.SupHazardListTypeId,
                IsEndLevel = hazardListType.IsEndLevel,
                WorkStage = hazardListType.WorkStage,
                IsCompany = hazardListType.IsCompany
            };
            db.Technique_HazardListType.InsertOnSubmit(newHazardListType);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改危险源类别
        /// </summary>
        /// <param name="hazardListType"></param>
        public static void UpdateHazardListType(Model.Technique_HazardListType hazardListType)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Technique_HazardListType newHazardListType = db.Technique_HazardListType.FirstOrDefault(e => e.HazardListTypeId == hazardListType.HazardListTypeId);
            if (newHazardListType != null)
            {
                newHazardListType.HazardListTypeCode = hazardListType.HazardListTypeCode;
                newHazardListType.HazardListTypeName = hazardListType.HazardListTypeName;
                newHazardListType.SupHazardListTypeId = hazardListType.SupHazardListTypeId;
                newHazardListType.IsEndLevel = hazardListType.IsEndLevel;
                newHazardListType.WorkStage = hazardListType.WorkStage;
                //newHazardListType.IsCompany = hazardListType.IsCompany;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除
        /// </summary>
        /// <param name="hazardListTypeId"></param>
        public static void DeleteHazardListTypeById(string hazardListTypeId)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Technique_HazardListType hazardListType = db.Technique_HazardListType.FirstOrDefault(e => e.HazardListTypeId == hazardListTypeId);
            if (hazardListType != null)
            {
                db.Technique_HazardListType.DeleteOnSubmit(hazardListType);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 是否可删除资源节点
        /// </summary>
        /// <param name="postName"></param>
        /// <returns>true-可以，false-不可以</returns>
        public static bool IsDeleteHazardListType(string hazardListTypeId)
        {
            bool isDelete = true;
            var hazardListType = BLL.HazardListTypeService.GetHazardListTypeById(hazardListTypeId);
            if (hazardListType != null)
            {
                if (hazardListType.IsBuild == true)
                {
                    isDelete = false;
                }
                if (hazardListType.IsEndLevel == true)
                {
                    var detailCout = new Model.SGGLDB(Funs.ConnString).Technique_HazardList.FirstOrDefault(x => x.HazardListTypeId == hazardListTypeId);
                    if (detailCout != null)
                    {
                        isDelete = false;
                    }
                }
                else
                {
                    var supItemSetCount = GetHazardListTypeBySupItem(hazardListTypeId);
                    if (supItemSetCount.Count() > 0)
                    {
                        isDelete = false;
                    }
                }
            }
            return isDelete;
        }

        /// <summary>
        /// 获取是否末级项
        /// </summary>
        /// <returns></returns>
        public static List<Model.Base_HSSEStandardListType> GetIsEndLeverList()
        {
            Model.Base_HSSEStandardListType t1 = new Model.Base_HSSEStandardListType
            {
                TypeId = "true",
                TypeName = "是"
            };
            Model.Base_HSSEStandardListType t2 = new Model.Base_HSSEStandardListType
            {
                TypeId = "false",
                TypeName = "否"
            };
            List<Model.Base_HSSEStandardListType> list = new List<Model.Base_HSSEStandardListType>();
            list.Add(t1);
            list.Add(t2);
            return list;
        }

        /// <summary>
        /// 根据类型名称获取
        /// </summary>
        /// <param name="hazardListTypeName"></param>
        /// <returns></returns>
        public static Model.Technique_HazardListType GetHazardListTypeByHazardListTypeName(string hazardListTypeName)
        {
            return new Model.SGGLDB(Funs.ConnString).Technique_HazardListType.FirstOrDefault(e => e.HazardListTypeName == hazardListTypeName);
        }
    }
}
