using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace BLL
{
    public static class Hazard_HazardSelectedItemService
    {
        public static Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);

        /// <summary>
        /// 根据危险源类别编号查询危险源类别
        /// </summary>
        /// <param name="hazardListTypeId"></param>
        /// <returns></returns>
        public static IEnumerable getHazardSelectedItemByHazardListTypeId(string hazardListTypeId, string hazardListId)
        {
            return from x in db.Hazard_HazardSelectedItem
                   where x.HazardListTypeId == hazardListTypeId && x.HazardListId == hazardListId
                   select new
                   {
                       x.HazardId,
                       x.HazardListTypeId,
                       x.HazardListId,
                       x.HazardItems,
                       x.DefectsType,
                       x.MayLeadAccidents,
                       x.HelperMethod,
                       x.HazardJudge_L,
                       x.HazardJudge_E,
                       x.HazardJudge_C,
                       x.HazardJudge_D,
                       x.HazardLevel,
                       x.ControlMeasures,
                       x.IsResponse,
                       x.ResponseRecode,
                       x.PromptTime,
                       x.Remark,
                       x.WorkStage,
                       //HazardLevelName = (from y in db.Hazard_HazardLevel where y.HazardLevelId == x.HazardLevel select y.HazardLevelName).First(),
                   };
        }

        /// <summary>
        /// 根据危险源类别编号查询危险源类别
        /// </summary>
        /// <param name="hazardListTypeId"></param>
        /// <returns></returns>
        public static IEnumerable getHazardSelectedItemByHazardListTypeId(string hazardListTypeId, string hazardListId, string workStage)
        {
            return from x in db.Hazard_HazardSelectedItem
                   where x.HazardListTypeId == hazardListTypeId && x.HazardListId == hazardListId && x.WorkStage == workStage
                   select new
                   {
                       x.HazardId,
                       x.HazardListTypeId,
                       x.HazardListId,
                       x.HazardItems,
                       x.DefectsType,
                       x.MayLeadAccidents,
                       x.HelperMethod,
                       x.HazardJudge_L,
                       x.HazardJudge_E,
                       x.HazardJudge_C,
                       x.HazardJudge_D,
                       x.HazardLevel,
                       x.ControlMeasures,
                       x.IsResponse,
                       x.ResponseRecode,
                       x.PromptTime,
                       x.Remark,
                       x.WorkStage,
                       //HazardLevelName = (from y in db.Hazard_HazardLevel where y.HazardLevelId == x.HazardLevel select y.HazardLevelName).First(),
                   };
        }

        /// <summary>
        /// 危险源类别编号查询危险源类别
        /// </summary>
        /// <returns>危险因素明细集合</returns>
        public static List<Model.Hazard_HazardSelectedItem> GetHazardSelectedItemByHazardListTypeIdAndHazardListId(string hazardListTypeId, string hazardListId)
        {
            return (from x in db.Hazard_HazardSelectedItem where x.HazardListTypeId == hazardListTypeId && x.HazardListId == hazardListId select x).ToList();
        }

        /// <summary>
        /// 危险源类别编号查询危险源类别
        /// </summary>
        /// <returns>危险因素明细集合</returns>
        public static List<Model.Hazard_HazardSelectedItem> GetPromptTimeIsNotNullHazardSelectedItemByHazardListTypeIdAndHazardListId(string hazardListTypeId, string hazardListId)
        {
            return (from x in db.Hazard_HazardSelectedItem where x.HazardListTypeId == hazardListTypeId && x.HazardListId == hazardListId && x.PromptTime != null select x).ToList();
        }

        /// <summary>
        /// 根据危险源辨识与评价清单Id获取一个危险源辨识与评价清单审批信息
        /// </summary>
        /// <param name="hazardId">危险源辨识与评价清单Id</param>
        /// <returns>一个危险源辨识与评价清单审批实体</returns>
        public static Model.Hazard_HazardSelectedItem GetHazardSelectedItemByHazardId(string hazardId, string hazardListId, string workStage)
        {
            return db.Hazard_HazardSelectedItem.FirstOrDefault(x => x.HazardId == hazardId && x.HazardListId == hazardListId && x.WorkStage == workStage);
        }

        /// <summary>
        /// 根据危险源辨识与评价清单Id获取一个危险源辨识与评价清单审批信息
        /// </summary>
        /// <param name="hazardId">危险源辨识与评价清单Id</param>
        /// <returns>一个危险源辨识与评价清单审批实体</returns>
        public static Model.Hazard_HazardSelectedItem GetHazardSelectedItemByHazardId(string hazardId)
        {
            return db.Hazard_HazardSelectedItem.FirstOrDefault(x => x.HazardId == hazardId);
        }

        /// <summary>
        /// 根据危险源辨识与评价清单编号获取危险因素明细集合
        /// </summary>
        /// <param name="hazardListCode">危险源辨识与评价清单编号</param>
        /// <returns>危险因素明细集合</returns>
        public static List<Model.Hazard_HazardSelectedItem> GetHazardSelectedItemsByHazardListId(string hazardListId)
        {
            return (from x in new Model.SGGLDB(Funs.ConnString).Hazard_HazardSelectedItem where x.HazardListId == hazardListId orderby x.WorkStage select x).ToList();
        }

        /// <summary>
        /// 根据危险源辨识与评价清单编号和工作阶段获取危险因素明细集合
        /// </summary>
        /// <param name="hazardListCode">危险源辨识与评价清单编号</param>
        /// <param name="workStage">工作阶段</param>
        /// <returns>危险因素明细集合</returns>
        public static List<Model.Hazard_HazardSelectedItem> GetHazardSelectedItemsByHazardListIdAndWorkStage(string hazardListId, string workStage)
        {
            return (from x in new Model.SGGLDB(Funs.ConnString).Hazard_HazardSelectedItem where x.HazardListId == hazardListId && x.WorkStage == workStage orderby x.WorkStage select x).ToList();
        }

        /// <summary>
        /// 根据危险源辨识与评价清单编号，种类和工作阶段获取危险因素明细集合
        /// </summary>
        /// <param name="hazardListTypeId"></param>
        /// <param name="hazardListCode"></param>
        /// <param name="workStage"></param>
        /// <returns></returns>
        public static List<Model.Hazard_HazardSelectedItem> GetHazardSelectedItemBySortAndListIdAndWorkStage(string hazardListTypeId, string hazardListId, string workStage)
        {
            return (from x in db.Hazard_HazardSelectedItem where x.HazardListTypeId == hazardListTypeId && x.HazardListId == hazardListId && x.WorkStage == workStage select x).ToList();
        }

        /// <summary>
        /// 增加危险源信息
        /// </summary>
        /// <param name="noticeSign">危险源实体</param>
        public static void AddHazardSelectedItem(Model.Hazard_HazardSelectedItem hazardSelectedItem)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Hazard_HazardSelectedItem newHazardSelectedItem = new Model.Hazard_HazardSelectedItem
            {
                HazardId = hazardSelectedItem.HazardId,
                HazardListTypeId = hazardSelectedItem.HazardListTypeId,
                HazardListId = hazardSelectedItem.HazardListId,
                HazardItems = hazardSelectedItem.HazardItems,
                DefectsType = hazardSelectedItem.DefectsType,
                MayLeadAccidents = hazardSelectedItem.MayLeadAccidents,
                HelperMethod = hazardSelectedItem.HelperMethod,
                HazardJudge_L = hazardSelectedItem.HazardJudge_L,
                HazardJudge_E = hazardSelectedItem.HazardJudge_E,
                HazardJudge_D = hazardSelectedItem.HazardJudge_D,
                HazardJudge_C = hazardSelectedItem.HazardJudge_C,
                HazardLevel = hazardSelectedItem.HazardLevel,
                ControlMeasures = hazardSelectedItem.ControlMeasures,
                IsResponse = hazardSelectedItem.IsResponse,
                ResponseRecode = hazardSelectedItem.ResponseRecode,
                PromptTime = hazardSelectedItem.PromptTime,
                Remark = hazardSelectedItem.Remark,
                WorkStage = hazardSelectedItem.WorkStage
            };

            new Model.SGGLDB(Funs.ConnString).Hazard_HazardSelectedItem.InsertOnSubmit(newHazardSelectedItem);
            new Model.SGGLDB(Funs.ConnString).SubmitChanges();
        }

        /// <summary>
        /// 修改危险源信息
        /// </summary>
        /// <param name="hazardSelectedItem">危险源实体</param>
        public static void UpdateHazardSelectedItem(Model.Hazard_HazardSelectedItem hazardSelectedItem)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Hazard_HazardSelectedItem newHazardSelectedItem = db.Hazard_HazardSelectedItem.FirstOrDefault(e => e.HazardId == hazardSelectedItem.HazardId && e.HazardListId == hazardSelectedItem.HazardListId && e.WorkStage == hazardSelectedItem.WorkStage);
            if (newHazardSelectedItem != null)
            {
                newHazardSelectedItem.HazardListTypeId = hazardSelectedItem.HazardListTypeId;
                newHazardSelectedItem.HazardListId = hazardSelectedItem.HazardListId;
                newHazardSelectedItem.HazardItems = hazardSelectedItem.HazardItems;
                newHazardSelectedItem.DefectsType = hazardSelectedItem.DefectsType;
                newHazardSelectedItem.MayLeadAccidents = hazardSelectedItem.MayLeadAccidents;
                newHazardSelectedItem.HelperMethod = hazardSelectedItem.HelperMethod;
                newHazardSelectedItem.HazardJudge_L = hazardSelectedItem.HazardJudge_L;
                newHazardSelectedItem.HazardJudge_E = hazardSelectedItem.HazardJudge_E;
                newHazardSelectedItem.HazardJudge_D = hazardSelectedItem.HazardJudge_D;
                newHazardSelectedItem.HazardJudge_C = hazardSelectedItem.HazardJudge_C;
                newHazardSelectedItem.HazardLevel = hazardSelectedItem.HazardLevel;
                newHazardSelectedItem.ControlMeasures = hazardSelectedItem.ControlMeasures;
                newHazardSelectedItem.IsResponse = hazardSelectedItem.IsResponse;
                newHazardSelectedItem.ResponseRecode = hazardSelectedItem.ResponseRecode;
                newHazardSelectedItem.PromptTime = hazardSelectedItem.PromptTime;
                newHazardSelectedItem.Remark = hazardSelectedItem.Remark;
                newHazardSelectedItem.WorkStage = hazardSelectedItem.WorkStage;

                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据危险源类别主键删除对应的所有危险源信息
        /// </summary>
        /// <param name="hazardListCode">危险源类别主键</param>
        public static void DeleteHazardSelectedItemByHazardListId(string hazardListId)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            var q = (from x in db.Hazard_HazardSelectedItem where x.HazardListId == hazardListId select x).ToList();
            if (q.Count() > 0)
            {
                db.Hazard_HazardSelectedItem.DeleteAllOnSubmit(q);
                db.SubmitChanges();
            }
        }
    }
}
