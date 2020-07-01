using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 费用类型
    /// </summary>
    public static class CostTypeService
    {
        public static Model.SGGLDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取费用类型
        /// </summary>
        /// <param name="costTypeId"></param>
        /// <returns></returns>
        public static Model.Base_CostType GetCostTypeById(string costTypeId)
        {
            return Funs.DB.Base_CostType.FirstOrDefault(e => e.CostTypeId == costTypeId);
        }

        /// <summary>
        /// 添加费用类型
        /// </summary>
        /// <param name="costType"></param>
        public static void AddCostType(Model.Base_CostType costType)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Base_CostType newCostType = new Model.Base_CostType
            {
                CostTypeId = costType.CostTypeId,
                CostTypeCode = costType.CostTypeCode,
                CostTypeName = costType.CostTypeName,
                Remark = costType.Remark
            };
            db.Base_CostType.InsertOnSubmit(newCostType);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改费用类型
        /// </summary>
        /// <param name="costType"></param>
        public static void UpdateCostType(Model.Base_CostType costType)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Base_CostType newCostType = db.Base_CostType.FirstOrDefault(e => e.CostTypeId == costType.CostTypeId);
            if (newCostType != null)
            {
                newCostType.CostTypeCode = costType.CostTypeCode;
                newCostType.CostTypeName = costType.CostTypeName;
                newCostType.Remark = costType.Remark;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除费用类型
        /// </summary>
        /// <param name="costTypeId"></param>
        public static void DeleteCostTypeById(string costTypeId)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Base_CostType costType = db.Base_CostType.FirstOrDefault(e => e.CostTypeId == costTypeId);
            if (costType != null)
            {
                db.Base_CostType.DeleteOnSubmit(costType);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 获取费用类型下拉列表项
        /// </summary>
        /// <returns></returns>
        public static List<Model.Base_CostType> GetCostTypeList()
        {
            return (from x in Funs.DB.Base_CostType orderby x.CostTypeCode select x).ToList();
        }
    }
}
