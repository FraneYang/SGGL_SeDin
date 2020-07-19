using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    public static class Hazard_EnvironmentalRiskItemService
    {
        public static Model.SGGLDB db = Funs.DB;

        /// <summary>
        /// 根据危险源明细表id得到明细信息
        /// </summary>
        /// <param name="environmentalRiskListId"></param>
        /// <returns></returns>
        public static Model.Hazard_EnvironmentalRiskItem GetEnvironmentalRiskItemListByEnvironmentalRiskItemId(string environmentalRiskItemId)
        {
            return (from x in Funs.DB.Hazard_EnvironmentalRiskItem where x.EnvironmentalRiskItemId == environmentalRiskItemId select x).FirstOrDefault();
        }

        /// <summary>
        /// 根据危险源主表id得到明细list
        /// </summary>
        /// <param name="environmentalRiskListId"></param>
        /// <returns></returns>
        public static List<Model.Hazard_EnvironmentalRiskItem> GetEnvironmentalRiskItemListByRiskListId(string environmentalRiskListId)
        {
            return (from x in Funs.DB.Hazard_EnvironmentalRiskItem where x.EnvironmentalRiskListId == environmentalRiskListId select x).ToList();
        }

        /// <summary>
        /// 根据危险源主表id得到明细list
        /// </summary>
        /// <param name="environmentalRiskListId"></param>
        /// <returns></returns>
        public static Model.Hazard_EnvironmentalRiskItem GetEnvironmentalRiskItemListByRiskListIdEnvironmentalId(string environmentalRiskListId, string environmentalId)
        {
            return Funs.DB.Hazard_EnvironmentalRiskItem.FirstOrDefault(x => x.EnvironmentalRiskListId == environmentalRiskListId && x.EnvironmentalId == environmentalId);
        }

        /// <summary>
        /// 增加危险源辨识与评价清单明细信息
        /// </summary>
        /// <param name="environmentalRiskItem">危险源辨识与评价清单实体</param>
        public static void AddEnvironmentalRiskItem(Model.Hazard_EnvironmentalRiskItem environmentalRiskItem)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Hazard_EnvironmentalRiskItem newEnvironmentalRiskItem = new Model.Hazard_EnvironmentalRiskItem
            {
                EnvironmentalRiskItemId = environmentalRiskItem.EnvironmentalRiskItemId,
                EnvironmentalRiskListId = environmentalRiskItem.EnvironmentalRiskListId,
                EnvironmentalId = environmentalRiskItem.EnvironmentalId,
                EType = environmentalRiskItem.EType,
                ActivePoint = environmentalRiskItem.ActivePoint,
                EnvironmentalFactors = environmentalRiskItem.EnvironmentalFactors,
                AValue = environmentalRiskItem.AValue,
                BValue = environmentalRiskItem.BValue,
                CValue = environmentalRiskItem.CValue,
                DValue = environmentalRiskItem.DValue,
                EValue = environmentalRiskItem.EValue,
                FValue = environmentalRiskItem.FValue,
                GValue = environmentalRiskItem.GValue,
                SmallType = environmentalRiskItem.SmallType,
                IsImportant = environmentalRiskItem.IsImportant,
                Code = environmentalRiskItem.Code,
                ControlMeasures = environmentalRiskItem.ControlMeasures,
                Remark = environmentalRiskItem.Remark,
                EnvironmentEffect = environmentalRiskItem.EnvironmentEffect
            };
            Funs.DB.Hazard_EnvironmentalRiskItem.InsertOnSubmit(newEnvironmentalRiskItem);
            Funs.DB.SubmitChanges();
        }

        /// <summary>
        /// 修改危险源辨识与评价清单明细信息
        /// </summary>
        /// <param name="environmentalRiskItem">危险源辨识与评价清单实体</param>
        public static void UpdateEnvironmentalRiskItem(Model.Hazard_EnvironmentalRiskItem environmentalRiskItem)
        {
            Model.Hazard_EnvironmentalRiskItem newEnvironmentalRiskItem = Funs.DB.Hazard_EnvironmentalRiskItem.FirstOrDefault(e => e.EnvironmentalRiskItemId == environmentalRiskItem.EnvironmentalRiskItemId);
            if (newEnvironmentalRiskItem != null)
            {
                newEnvironmentalRiskItem.EType = environmentalRiskItem.EType;
                newEnvironmentalRiskItem.ActivePoint = environmentalRiskItem.ActivePoint;
                newEnvironmentalRiskItem.EnvironmentalFactors = environmentalRiskItem.EnvironmentalFactors;
                newEnvironmentalRiskItem.AValue = environmentalRiskItem.AValue;
                newEnvironmentalRiskItem.BValue = environmentalRiskItem.BValue;
                newEnvironmentalRiskItem.CValue = environmentalRiskItem.CValue;
                newEnvironmentalRiskItem.DValue = environmentalRiskItem.DValue;
                newEnvironmentalRiskItem.EValue = environmentalRiskItem.EValue;
                newEnvironmentalRiskItem.FValue = environmentalRiskItem.FValue;
                newEnvironmentalRiskItem.GValue = environmentalRiskItem.GValue;
                newEnvironmentalRiskItem.SmallType = environmentalRiskItem.SmallType;
                newEnvironmentalRiskItem.IsImportant = environmentalRiskItem.IsImportant;
                newEnvironmentalRiskItem.Code = environmentalRiskItem.Code;
                newEnvironmentalRiskItem.ControlMeasures = environmentalRiskItem.ControlMeasures;
                newEnvironmentalRiskItem.Remark = environmentalRiskItem.Remark;
                newEnvironmentalRiskItem.EnvironmentEffect = environmentalRiskItem.EnvironmentEffect;
                Funs.DB.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据危险源辨识与评价清单Id删除一个危险源辨识与评价清单明细信息
        /// </summary>
        /// <param name="environmentalRiskItemId">危险源辨识与评价清单Id</param>
        public static void DeleteEnvironmentalRiskItemById(string environmentalRiskItemId)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Hazard_EnvironmentalRiskItem newEnvironmentalRiskItem = db.Hazard_EnvironmentalRiskItem.FirstOrDefault(e => e.EnvironmentalRiskItemId == environmentalRiskItemId);
            if (newEnvironmentalRiskItem != null)
            {
                db.Hazard_EnvironmentalRiskItem.DeleteOnSubmit(newEnvironmentalRiskItem);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据危险源辨识与评价清单Id删除一个危险源辨识与评价清单明细信息
        /// </summary>
        /// <param name="environmentalRiskItemId">危险源辨识与评价清单Id</param>
        public static void DeleteEnvironmentalRiskItemByRiskListId(string environmentalRiskListId)
        {
            Model.SGGLDB db = Funs.DB;
            var newEnvironmentalRiskItem = from x in db.Hazard_EnvironmentalRiskItem where x.EnvironmentalRiskListId == environmentalRiskListId select x;
            if (newEnvironmentalRiskItem.Count() > 0)
            {
                db.Hazard_EnvironmentalRiskItem.DeleteAllOnSubmit(newEnvironmentalRiskItem);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据环境因素危险源获取环境因素危险源识别与评价
        /// </summary>
        /// <param name="environmentalId"></param>
        /// <returns></returns>
        public static List<Model.Hazard_EnvironmentalRiskItem> GetEnvironmentalRiskItemByEnvironmentalId(string environmentalId)
        {
            return (from x in db.Hazard_EnvironmentalRiskItem where x.EnvironmentalId == environmentalId select x).ToList();
        }
    }
}
