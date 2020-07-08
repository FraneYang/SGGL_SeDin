using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    /// <summary>
    /// 资源信息
    /// </summary>
    public static class APIResourcesService
    {
        #region 集团培训教材
        /// <summary>
        /// 根据父级类型ID获取培训教材类型
        /// </summary>
        /// <param name="supTypeId"></param>
        /// <returns></returns>
        public static List<Model.ResourcesItem> getTrainingListBySupTrainingId(string supTypeId)
        {
            var getDataLists = from x in new Model.SGGLDB(Funs.ConnString).Training_Training
                               where x.SupTrainingId == supTypeId || (supTypeId == null && x.SupTrainingId == "0")
                               orderby x.TrainingCode
                               select new Model.ResourcesItem
                               {
                                   ResourcesId = x.TrainingId,
                                   ResourcesCode = x.TrainingCode,
                                   ResourcesName = x.TrainingName,
                                   SupResourcesId = x.SupTrainingId,
                                   IsEndLever = x.IsEndLever,
                               };
            return getDataLists.ToList();
        }

        /// <summary>
        /// 根据培训教材类型id获取培训教材列表
        /// </summary>
        /// <param name="trainingId"></param>
        /// <returns></returns>
        public static List<Model.BaseInfoItem> getTrainingItemListByTrainingId(string trainingId)
        {
            var getDataLists = (from x in new Model.SGGLDB(Funs.ConnString).Training_TrainingItem
                                where x.TrainingId == trainingId && x.IsPass == true
                                orderby x.TrainingItemCode
                                select new Model.BaseInfoItem
                                {
                                    BaseInfoId = x.TrainingItemId,
                                    BaseInfoCode = x.TrainingItemCode,
                                    BaseInfoName = x.TrainingItemName,
                                    ImageUrl = x.AttachUrl
                                }).ToList();
            return getDataLists;
        }

        /// <summary>
        /// 根据培训教材主键获取培训教材详细信息
        /// </summary>
        /// <param name="trainingId"></param>
        /// <returns></returns>
        public static Model.BaseInfoItem getTrainingItemByTrainingItemId(string trainingItemId)
        {
            var getDataInfo = from x in new Model.SGGLDB(Funs.ConnString).Training_TrainingItem
                              where x.TrainingItemId == trainingItemId
                              select new Model.BaseInfoItem
                              {
                                  BaseInfoId = x.TrainingItemId,
                                  BaseInfoCode = x.TrainingItemCode,
                                  BaseInfoName = x.TrainingItemName,
                                  ImageUrl = APIUpLoadFileService.getFileUrl(x.TrainingItemId, x.AttachUrl),
                              };
            return getDataInfo.FirstOrDefault();
        }
        #endregion

        #region 公司培训教材
        /// <summary>
        /// 根据父级类型ID获取公司培训教材类型
        /// </summary>
        /// <param name="supTypeId"></param>
        /// <returns></returns>
        public static List<Model.ResourcesItem> getCompanyTrainingListBySupTrainingId(string supTypeId)
        {
            var getDataLists = from x in new Model.SGGLDB(Funs.ConnString).Training_CompanyTraining
                               where x.SupCompanyTrainingId == supTypeId || (supTypeId == null && x.SupCompanyTrainingId == "0")
                               orderby x.CompanyTrainingCode
                               select new Model.ResourcesItem
                               {
                                   ResourcesId = x.CompanyTrainingId,
                                   ResourcesCode = x.CompanyTrainingCode,
                                   ResourcesName = x.CompanyTrainingName,
                                   SupResourcesId = x.SupCompanyTrainingId,
                                   IsEndLever = x.IsEndLever,
                               };
            return getDataLists.ToList();
        }

        /// <summary>
        /// 根据培训教材类型id获取公司培训教材列表
        /// </summary>
        /// <param name="trainingId"></param>
        /// <returns></returns>
        public static List<Model.BaseInfoItem> getCompanyTrainingItemListByTrainingId(string trainingId)
        {
            var getDataLists = (from x in new Model.SGGLDB(Funs.ConnString).Training_CompanyTrainingItem
                                where x.CompanyTrainingId == trainingId
                                orderby x.CompanyTrainingItemCode
                                select new Model.BaseInfoItem
                                {
                                    BaseInfoId = x.CompanyTrainingItemId,
                                    BaseInfoCode = x.CompanyTrainingItemCode,
                                    BaseInfoName = x.CompanyTrainingItemName,
                                    ImageUrl = x.AttachUrl
                                }).ToList();
            return getDataLists;
        }

        /// <summary>
        /// 根据培训教材主键获取公司培训教材详细信息
        /// </summary>
        /// <param name="trainingId"></param>
        /// <returns></returns>
        public static Model.BaseInfoItem getCompanyTrainingItemByTrainingItemId(string trainingItemId)
        {
            var getDataInfo = from x in new Model.SGGLDB(Funs.ConnString).Training_CompanyTrainingItem
                              where x.CompanyTrainingItemId == trainingItemId
                              select new Model.BaseInfoItem
                              {
                                  BaseInfoId = x.CompanyTrainingItemId,
                                  BaseInfoCode = x.CompanyTrainingItemCode,
                                  BaseInfoName = x.CompanyTrainingItemName,
                                  ImageUrl = APIUpLoadFileService.getFileUrl(x.CompanyTrainingItemId, x.AttachUrl),
                                  
                              };
            return getDataInfo.FirstOrDefault();
        }
        #endregion

        #region 公司制度
        /// <summary>
        /// 获取公司制度列表
        /// </summary>
        /// <param name="trainingId"></param>
        /// <returns></returns>
        public static List<Model.BaseInfoItem> getCompanySafetyInstitutionList()
        {
            var getDataLists = (from x in new Model.SGGLDB(Funs.ConnString).HSSESystem_SafetyInstitution
                                orderby x.EffectiveDate descending
                                select new Model.BaseInfoItem
                                {
                                    BaseInfoId = x.SafetyInstitutionId,
                                    BaseInfoCode = string.Format("{0:yyyy-MM-dd}", x.EffectiveDate),
                                    BaseInfoName = x.SafetyInstitutionName,
                                    ImageUrl =APIUpLoadFileService.getFileUrl(x.SafetyInstitutionId,x.AttachUrl),
                                }).ToList();
            return getDataLists;
        }

        /// <summary>
        /// 获取公司制度详细信息
        /// </summary>
        /// <param name="safetyInstitutionId"></param>
        /// <returns></returns>
        public static Model.BaseInfoItem getCompanySafetyInstitutionInfo(string safetyInstitutionId)
        {
            var getDataInfo = from x in new Model.SGGLDB(Funs.ConnString).HSSESystem_SafetyInstitution
                              where x.SafetyInstitutionId == safetyInstitutionId
                              select new Model.BaseInfoItem
                              {
                                  BaseInfoId = x.SafetyInstitutionId,
                                  BaseInfoCode = string.Format("{0:yyyy-MM-dd}", x.EffectiveDate),
                                  BaseInfoName = x.SafetyInstitutionName,
                                  ImageUrl = APIUpLoadFileService.getFileUrl(x.SafetyInstitutionId, x.AttachUrl),
                              };
            return getDataInfo.FirstOrDefault();
        }
        #endregion

        #region 考试试题
        /// <summary>
        /// 根据父级类型ID获取考试试题类型
        /// </summary>
        /// <param name="supTypeId"></param>
        /// <returns></returns>
        public static List<Model.ResourcesItem> getTestTrainingListBySupTrainingId(string supTypeId)
        {
            var getDataLists = from x in new Model.SGGLDB(Funs.ConnString).Training_TestTraining
                               where x.SupTrainingId == supTypeId || (supTypeId == null && x.SupTrainingId == "0")
                               orderby x.TrainingCode
                               select new Model.ResourcesItem
                               {
                                   ResourcesId = x.TrainingId,
                                   ResourcesCode = x.TrainingCode,
                                   ResourcesName = x.TrainingName,
                                   SupResourcesId = x.SupTrainingId,
                                   IsEndLever = x.IsEndLever,
                               };
            return getDataLists.ToList();
        }

        /// <summary>
        /// 根据培训教材类型id获取考试试题列表
        /// </summary>
        /// <param name="testTrainingId">试题类型ID</param>
        /// <returns></returns>
        public static List<Model.BaseInfoItem> getTestTrainingItemListByTrainingId(string testTrainingId)
        {
            var getDataLists = (from x in new Model.SGGLDB(Funs.ConnString).Training_TestTrainingItem
                                where x.TrainingId == testTrainingId
                                orderby x.TrainingItemCode
                                select new Model.BaseInfoItem
                                {
                                    BaseInfoId = x.TrainingItemId,
                                    BaseInfoCode = x.TrainingItemCode,
                                    BaseInfoName = x.Abstracts,
                                    ImageUrl = x.AttachUrl
                                }).ToList();
            return getDataLists;
        }

        /// <summary>
        /// 根据培训教材主键获取考试试题详细信息
        /// </summary>
        /// <param name="trainingId"></param>
        /// <returns></returns>
        public static Model.TestTrainingResourcesItem getTestTrainingItemByTrainingItemId(string trainingItemId)
        {
            var getDataInfo = from x in new Model.SGGLDB(Funs.ConnString).Training_TestTrainingItem
                              where x.TrainingItemId == trainingItemId
                              select new Model.TestTrainingResourcesItem
                              {
                                  TrainingItemId = x.TrainingItemId,
                                  TrainingId = x.TrainingId,
                                  TrainingItemCode = x.TrainingItemCode,
                                  Abstracts = x.Abstracts,
                                  AttachUrl = x.AttachUrl.Replace('\\', '/'),
                                  TestType = x.TestType,
                                  TestTypeName = x.TestType == "1" ? "单选题" : (x.TestType == "2" ? "多选题" : "判断题"),
                                  WorkPostIds = x.WorkPostIds,
                                  WorkPostNames = WorkPostService.getWorkPostNamesWorkPostIds(x.WorkPostIds),
                                  AItem = x.AItem,
                                  BItem = x.BItem,
                                  CItem = x.CItem,
                                  DItem = x.DItem,
                                  EItem = x.EItem,
                                  AnswerItems = x.AnswerItems,
                              };
            return getDataInfo.FirstOrDefault();
        }
        #endregion

        #region 事故案例
        /// <summary>
        /// 根据父级类型ID获取事故案例类型
        /// </summary>
        /// <param name="supTypeId"></param>
        /// <returns></returns>
        public static List<Model.ResourcesItem> getAccidentCaseListBySupAccidentCaseId(string supTypeId)
        {
            var getDataLists = from x in new Model.SGGLDB(Funs.ConnString).EduTrain_AccidentCase
                               where x.SupAccidentCaseId == supTypeId || (supTypeId == null && x.SupAccidentCaseId == "0")
                               orderby x.AccidentCaseCode
                               select new Model.ResourcesItem
                               {
                                   ResourcesId = x.AccidentCaseId,
                                   ResourcesCode = x.AccidentCaseCode,
                                   ResourcesName = x.AccidentCaseName,
                                   SupResourcesId = x.SupAccidentCaseId,
                                   IsEndLever = x.IsEndLever,
                               };
            return getDataLists.ToList();
        }

        /// <summary>
        /// 根据事故案例类型id获取公司事故案例列表
        /// </summary>
        /// <param name="accidentCaseId"></param>
        /// <returns></returns>
        public static List<Model.BaseInfoItem> getAccidentCaseItemListById(string accidentCaseId)
        {
            var getDataLists = (from x in new Model.SGGLDB(Funs.ConnString).EduTrain_AccidentCaseItem
                                where x.AccidentCaseId == accidentCaseId
                                orderby x.CompileDate descending
                                select new Model.BaseInfoItem
                                {
                                    BaseInfoId = x.AccidentCaseItemId,
                                    BaseInfoCode = x.Activities,
                                    BaseInfoName = x.AccidentName,                                   
                                }).ToList();
            return getDataLists;
        }

        /// <summary>
        /// 根据事故案例主键获取公司事故案例详细信息
        /// </summary>
        /// <param name="trainingId"></param>
        /// <returns></returns>
        public static Model.BaseInfoItem getAccidentCaseItemById(string accidentCaseItemId)
        {
            var getDataInfo = from x in new Model.SGGLDB(Funs.ConnString).EduTrain_AccidentCaseItem
                              where x.AccidentCaseItemId == accidentCaseItemId
                              select new Model.BaseInfoItem
                              {
                                  BaseInfoId = x.AccidentCaseItemId,
                                  BaseInfoCode = x.Activities,
                                  BaseInfoName = x.AccidentName,
                                  Remark = x.AccidentProfiles,
                                  RemarkOther = x.AccidentReview,
                              };
            return getDataInfo.FirstOrDefault();
        }
        #endregion

        #region 检查要点
        /// <summary>
        /// 根据父级类型ID获取检查要点类型
        /// </summary>
        /// <param name="supTypeId"></param>
        /// <param name="checkType">1-checkType;2-专项检查;3-综合检查</param>
        /// <returns></returns>
        public static List<Model.ResourcesItem> getCheckItemSetListBySupCheckItemId(string supTypeId, string checkType)
        {
            var getDataLists = from x in new Model.SGGLDB(Funs.ConnString).Technique_CheckItemSet
                               where x.CheckType== checkType &&( x.SupCheckItem == supTypeId || (supTypeId == null && x.SupCheckItem == "0"))
                               orderby x.SortIndex
                               select new Model.ResourcesItem
                               {
                                   ResourcesId = x.CheckItemSetId,
                                   ResourcesCode = x.MapCode,
                                   ResourcesName = x.CheckItemName,
                                   SupResourcesId = x.SupCheckItem,
                                   IsEndLever = x.IsEndLever,
                               };
            return getDataLists.ToList();
        }

        /// <summary>
        /// 根据检查要点类型id获取检查要点列表
        /// </summary>
        /// <param name="checkItemSetId"></param>
        /// <returns></returns>
        public static List<Model.BaseInfoItem> getCheckItemSetItemListBycheckItemSetId(string checkItemSetId)
        {
            var getDataLists = (from x in new Model.SGGLDB(Funs.ConnString).Technique_CheckItemDetail
                                where x.CheckItemSetId == checkItemSetId
                                orderby x.SortIndex
                                select new Model.BaseInfoItem
                                {
                                    BaseInfoId = x.CheckItemDetailId,
                                    BaseInfoCode = x.SortIndex.ToString(),
                                    BaseInfoName = x.CheckContent,
                                }).ToList();
            return getDataLists;
        }

        #endregion
    }
}
