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
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var getDataLists = from x in db.Training_Training
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
        }

        /// <summary>
        /// 根据培训教材类型id获取培训教材列表
        /// </summary>
        /// <param name="trainingId"></param>
        /// <returns></returns>
        public static List<Model.BaseInfoItem> getTrainingItemListByTrainingId(string trainingId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var getDataLists = (from x in db.Training_TrainingItem
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
        }

        /// <summary>
        /// 根据培训教材主键获取培训教材详细信息
        /// </summary>
        /// <param name="trainingId"></param>
        /// <returns></returns>
        public static Model.BaseInfoItem getTrainingItemByTrainingItemId(string trainingItemId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var getDataInfo = from x in db.Training_TrainingItem
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
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var getDataLists = from x in db.Training_CompanyTraining
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
        }

        /// <summary>
        /// 根据培训教材类型id获取公司培训教材列表
        /// </summary>
        /// <param name="trainingId"></param>
        /// <returns></returns>
        public static List<Model.BaseInfoItem> getCompanyTrainingItemListByTrainingId(string trainingId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var getDataLists = (from x in db.Training_CompanyTrainingItem
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
        }

        /// <summary>
        /// 根据培训教材主键获取公司培训教材详细信息
        /// </summary>
        /// <param name="trainingId"></param>
        /// <returns></returns>
        public static Model.BaseInfoItem getCompanyTrainingItemByTrainingItemId(string trainingItemId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var getDataInfo = from x in db.Training_CompanyTrainingItem
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
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var getDataLists = (from x in db.HSSESystem_SafetyInstitution
                                    orderby x.EffectiveDate descending
                                    select new Model.BaseInfoItem
                                    {
                                        BaseInfoId = x.SafetyInstitutionId,
                                        BaseInfoCode = string.Format("{0:yyyy-MM-dd}", x.EffectiveDate),
                                        BaseInfoName = x.SafetyInstitutionName,
                                        ImageUrl = APIUpLoadFileService.getFileUrl(x.SafetyInstitutionId, x.AttachUrl),
                                    }).ToList();
                return getDataLists;
            }
        }

        /// <summary>
        /// 获取公司制度详细信息
        /// </summary>
        /// <param name="safetyInstitutionId"></param>
        /// <returns></returns>
        public static Model.BaseInfoItem getCompanySafetyInstitutionInfo(string safetyInstitutionId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var getDataInfo = from x in db.HSSESystem_SafetyInstitution
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
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var getDataLists = from x in db.Training_TestTraining
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
        }

        /// <summary>
        /// 根据培训教材类型id获取考试试题列表
        /// </summary>
        /// <param name="testTrainingId">试题类型ID</param>
        /// <returns></returns>
        public static List<Model.BaseInfoItem> getTestTrainingItemListByTrainingId(string testTrainingId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var getDataLists = (from x in db.Training_TestTrainingItem
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
        }

        /// <summary>
        /// 根据培训教材主键获取考试试题详细信息
        /// </summary>
        /// <param name="trainingId"></param>
        /// <returns></returns>
        public static Model.TestTrainingResourcesItem getTestTrainingItemByTrainingItemId(string trainingItemId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var getDataInfo = from x in db.Training_TestTrainingItem
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
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var getDataLists = from x in db.EduTrain_AccidentCase
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
        }

        /// <summary>
        /// 根据事故案例类型id获取公司事故案例列表
        /// </summary>
        /// <param name="accidentCaseId"></param>
        /// <returns></returns>
        public static List<Model.BaseInfoItem> getAccidentCaseItemListById(string accidentCaseId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var getDataLists = (from x in db.EduTrain_AccidentCaseItem
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
        }

        /// <summary>
        /// 根据事故案例主键获取公司事故案例详细信息
        /// </summary>
        /// <param name="trainingId"></param>
        /// <returns></returns>
        public static Model.BaseInfoItem getAccidentCaseItemById(string accidentCaseItemId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var getDataInfo = from x in db.EduTrain_AccidentCaseItem
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
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var getDataLists = from x in db.Technique_CheckItemSet
                                   where x.CheckType == checkType && (x.SupCheckItem == supTypeId || (supTypeId == null && x.SupCheckItem == "0"))
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
        }

        /// <summary>
        /// 根据检查要点类型id获取检查要点列表
        /// </summary>
        /// <param name="checkItemSetId"></param>
        /// <returns></returns>
        public static List<Model.BaseInfoItem> getCheckItemSetItemListBycheckItemSetId(string checkItemSetId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var getDataLists = (from x in db.Technique_CheckItemDetail
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
        }

        #endregion

        #region 安全合规
        /// <summary>
        /// 获取安全合规列表
        /// </summary>
        /// <param name="type">类型（1-法律法规；2-标准规范；3-集团制度；4-赛鼎制度）</param>
        /// <returns></returns>
        public static List<Model.SafeLawItem> getSafeLawListByType(string type, string strParams)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                List<Model.SafeLawItem> returnList = new List<Model.SafeLawItem>();
                if (!string.IsNullOrEmpty(type))
                {
                    if (type == "1")
                    {
                        returnList = (from x in db.Law_LawRegulationList
                                      where strParams==null || x.LawRegulationName.Contains(strParams)
                                      orderby x.ApprovalDate descending
                                      select new Model.SafeLawItem
                                      {
                                          ID = x.LawRegulationId,
                                          ReleaseStates = x.ReleaseStates,
                                          ReleaseStatesName = db.Sys_Const.First(u => u.GroupId == ConstValue.Group_HSSE_ReleaseStates && u.ConstValue == x.ReleaseStates).ConstText,
                                          DataType = type,
                                          DataTypeName= "法律法规",
                                          Name = x.LawRegulationName,
                                      }).ToList();
                    }
                    else if (type == "2")
                    {
                        returnList = (from x in db.Law_HSSEStandardsList
                                      where strParams == null || x.StandardName.Contains(strParams)
                                      orderby x.ApprovalDate descending
                                      select new Model.SafeLawItem
                                      {
                                          ID = x.StandardId,
                                          ReleaseStates = x.ReleaseStates,
                                          ReleaseStatesName = db.Sys_Const.First(u => u.GroupId == ConstValue.Group_HSSE_ReleaseStates && u.ConstValue == x.ReleaseStates).ConstText,
                                          DataType = type,
                                          DataTypeName = "标准规范",
                                          Name = x.StandardName,
                                      }).ToList();
                    }
                    else if (type == "3")
                    {
                        returnList = (from x in db.Law_ManageRule
                                      where strParams == null || x.ManageRuleName.Contains(strParams)
                                      orderby x.ApprovalDate descending
                                      select new Model.SafeLawItem
                                      {
                                          ID = x.ManageRuleId,
                                          ReleaseStates = x.ReleaseStates,
                                          ReleaseStatesName = db.Sys_Const.First(u => u.GroupId == ConstValue.Group_HSSE_ReleaseStates && u.ConstValue == x.ReleaseStates).ConstText,
                                          DataType = type,
                                          DataTypeName = "集团制度",
                                          Name = x.ManageRuleName,
                                      }).ToList();
                    }
                    else if (type == "4")
                    {
                        returnList = (from x in db.HSSESystem_SafetyInstitution
                                      where strParams == null || x.SafetyInstitutionName.Contains(strParams)
                                      orderby x.ApprovalDate descending
                                      select new Model.SafeLawItem
                                      {
                                          ID = x.SafetyInstitutionId,
                                          ReleaseStates = x.ReleaseStates,
                                          ReleaseStatesName = db.Sys_Const.First(u => u.GroupId == ConstValue.Group_HSSE_ReleaseStates && u.ConstValue == x.ReleaseStates).ConstText,
                                          DataType = type,
                                          DataTypeName = "赛鼎制度",
                                          Name = x.SafetyInstitutionName,
                                      }).ToList();
                    }                  
                }
                else
                {
                   var returnList1 = (from x in db.Law_LawRegulationList
                                  where strParams == null || x.LawRegulationName.Contains(strParams)
                                  orderby x.ApprovalDate descending
                                  select new Model.SafeLawItem
                                  {
                                      ID = x.LawRegulationId,
                                      ReleaseStates = x.ReleaseStates,
                                      ReleaseStatesName = db.Sys_Const.First(u => u.GroupId == ConstValue.Group_HSSE_ReleaseStates && u.ConstValue == x.ReleaseStates).ConstText,
                                      DataType = "1",
                                      DataTypeName = "法律法规",
                                      Name = x.LawRegulationName,
                                  }).ToList();
                    if (returnList1.Count() > 0)
                    {
                        returnList.AddRange(returnList1);
                    }

                   var  returnList2 = (from x in db.Law_HSSEStandardsList
                                  where strParams == null || x.StandardName.Contains(strParams)
                                  orderby x.ApprovalDate descending
                                  select new Model.SafeLawItem
                                  {
                                      ID = x.StandardId,
                                      ReleaseStates = x.ReleaseStates,
                                      ReleaseStatesName = db.Sys_Const.First(u => u.GroupId == ConstValue.Group_HSSE_ReleaseStates && u.ConstValue == x.ReleaseStates).ConstText,
                                      DataType = "2",
                                      DataTypeName = "标准规范",
                                      Name = x.StandardName,
                                  }).ToList();
                    if (returnList2.Count() > 0)
                    {
                        returnList.AddRange(returnList2);
                    }

                   var returnList3 = (from x in db.Law_ManageRule
                                  where strParams == null || x.ManageRuleName.Contains(strParams)
                                  orderby x.ApprovalDate descending
                                  select new Model.SafeLawItem
                                  {
                                      ID = x.ManageRuleId,
                                      ReleaseStates = x.ReleaseStates,
                                      ReleaseStatesName = db.Sys_Const.First(u => u.GroupId == ConstValue.Group_HSSE_ReleaseStates && u.ConstValue == x.ReleaseStates).ConstText,
                                      DataType = "3",
                                      DataTypeName = "集团制度",
                                      Name = x.ManageRuleName,
                                  }).ToList();
                    if (returnList3.Count() > 0)
                    {
                        returnList.AddRange(returnList3);
                    }
                    var returnList4 = (from x in db.HSSESystem_SafetyInstitution
                                  where strParams == null || x.SafetyInstitutionName.Contains(strParams)
                                  orderby x.ApprovalDate descending
                                  select new Model.SafeLawItem
                                  {
                                      ID = x.SafetyInstitutionId,
                                      ReleaseStates = x.ReleaseStates,
                                      ReleaseStatesName = db.Sys_Const.First(u => u.GroupId == ConstValue.Group_HSSE_ReleaseStates && u.ConstValue == x.ReleaseStates).ConstText,
                                      DataType = "4",
                                      DataTypeName = "赛鼎制度",
                                      Name = x.SafetyInstitutionName,
                                  }).ToList();
                    if (returnList4.Count() > 0)
                    {
                        returnList.AddRange(returnList4);
                    }

                }
                return returnList;
            }
        }

        /// <summary>
        /// 获取公司制度详细信息
        /// </summary>
        /// <param name="safetyInstitutionId"></param>
        /// <returns></returns>
        public static Model.SafeLawItem getSafeLawInfo(string type, string id)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                if (type == "1")
                {
                    return (from x in db.Law_LawRegulationList
                            where x.LawRegulationId == id
                            orderby x.ApprovalDate descending
                            select new Model.SafeLawItem
                            {
                                ID = x.LawRegulationId,
                                ReleaseStates = x.ReleaseStates,
                                ReleaseStatesName = db.Sys_Const.First(u => u.GroupId == ConstValue.Group_HSSE_ReleaseStates && u.ConstValue == x.ReleaseStates).ConstText,
                                Name = x.LawRegulationName,
                                Code = x.LawRegulationCode,
                                TypeId = x.LawsRegulationsTypeId,
                                TypeName = db.Base_LawsRegulationsType.First(u => u.Id == x.LawsRegulationsTypeId).Name,
                                ReleaseUnit = x.ReleaseUnit,
                                ApprovalDate = x.ApprovalDate,
                                ApprovalDateStr = string.Format("{0:yyyy-MM-dd}", x.ApprovalDate),
                                EffectiveDate = x.EffectiveDate,
                                EffectiveDateStr = string.Format("{0:yyyy-MM-dd}", x.EffectiveDate),
                                AbolitionDate = x.AbolitionDate,
                                AbolitionDateStr = string.Format("{0:yyyy-MM-dd}", x.AbolitionDate),
                                ReplaceInfo = x.ReplaceInfo,
                                Description = x.Description,
                                IndexesIds = x.IndexesIds,
                                IndexesNames = ConstValue.getConstTextsConstValues(x.IndexesIds, ConstValue.Group_HSSE_Indexes),
                                UnitId = x.UnitId,
                                UnitName = db.Base_Unit.First(u => u.UnitId == x.UnitId).UnitName,
                                CompileManName = x.CompileMan,
                                CompileDate = x.CompileDate,
                                CompileDateStr = string.Format("{0:yyyy-MM-dd}", x.CompileDate),
                                DataType = type,
                                AttachUrl = db.AttachFile.First(z => z.ToKeyId == x.LawRegulationId).AttachUrl.Replace('\\', '/'),
                            }).FirstOrDefault();
                }
                else if (type == "2")
                {
                    return (from x in db.Law_HSSEStandardsList
                            where x.StandardId == id
                            orderby x.ApprovalDate descending
                            select new Model.SafeLawItem
                            {
                                ID = x.StandardId,
                                ReleaseStates = x.ReleaseStates,
                                ReleaseStatesName = db.Sys_Const.First(u => u.GroupId == ConstValue.Group_HSSE_ReleaseStates && u.ConstValue == x.ReleaseStates).ConstText,
                                Name = x.StandardName,
                                Code = x.StandardNo,
                                TypeId = x.TypeId,
                                TypeName = db.Base_HSSEStandardListType.First(u => u.TypeId == x.TypeId).TypeName,
                                ReleaseUnit = x.ReleaseUnit,
                                ApprovalDate = x.ApprovalDate,
                                ApprovalDateStr = string.Format("{0:yyyy-MM-dd}", x.ApprovalDate),
                                EffectiveDate = x.EffectiveDate,
                                EffectiveDateStr = string.Format("{0:yyyy-MM-dd}", x.EffectiveDate),
                                AbolitionDate = x.AbolitionDate,
                                AbolitionDateStr = string.Format("{0:yyyy-MM-dd}", x.AbolitionDate),
                                ReplaceInfo = x.ReplaceInfo,
                                Description = x.Description,
                                IndexesIds = x.IndexesIds,
                                IndexesNames = ConstValue.getConstTextsConstValues(x.IndexesIds, ConstValue.Group_HSSE_Indexes),
                                UnitId = x.UnitId,
                                UnitName = db.Base_Unit.First(u => u.UnitId == x.UnitId).UnitName,
                                CompileManName = x.CompileMan,
                                CompileDate = x.CompileDate,
                                CompileDateStr = string.Format("{0:yyyy-MM-dd}", x.CompileDate),
                                DataType = type,
                                AttachUrl = db.AttachFile.First(z => z.ToKeyId == x.StandardId).AttachUrl.Replace('\\', '/'),
                            }).FirstOrDefault();
                }
                else if (type == "3")
                {
                    return (from x in db.Law_ManageRule
                            where x.ManageRuleId == id
                            orderby x.ApprovalDate descending
                            select new Model.SafeLawItem
                            {
                                ID = x.ManageRuleId,
                                ReleaseStates = x.ReleaseStates,
                                ReleaseStatesName = db.Sys_Const.First(u => u.GroupId == ConstValue.Group_HSSE_ReleaseStates && u.ConstValue == x.ReleaseStates).ConstText,
                                Name = x.ManageRuleName,
                                Code = x.ManageRuleCode,
                                TypeId = x.ManageRuleTypeId,
                                TypeName = db.Base_ManageRuleType.First(u => u.ManageRuleTypeId == x.ManageRuleTypeId).ManageRuleTypeName,
                                ReleaseUnit = x.ReleaseUnit,
                                ApprovalDate = x.ApprovalDate,
                                ApprovalDateStr = string.Format("{0:yyyy-MM-dd}", x.ApprovalDate),
                                EffectiveDate = x.EffectiveDate,
                                EffectiveDateStr = string.Format("{0:yyyy-MM-dd}", x.EffectiveDate),
                                AbolitionDate = x.AbolitionDate,
                                AbolitionDateStr = string.Format("{0:yyyy-MM-dd}", x.AbolitionDate),
                                ReplaceInfo = x.ReplaceInfo,
                                Description = x.Description,
                                IndexesIds = x.IndexesIds,
                                IndexesNames = ConstValue.getConstTextsConstValues(x.IndexesIds, ConstValue.Group_HSSE_Indexes),
                                UnitId = x.UnitId,
                                UnitName = db.Base_Unit.First(u => u.UnitId == x.UnitId).UnitName,
                                CompileManName = x.CompileMan,
                                CompileDate = x.CompileDate,
                                CompileDateStr = string.Format("{0:yyyy-MM-dd}", x.CompileDate),
                                DataType = type,
                                AttachUrl = db.AttachFile.First(z => z.ToKeyId == x.ManageRuleId).AttachUrl.Replace('\\', '/'),
                            }).FirstOrDefault();
                }
                else if (type == "4")
                {
                    return (from x in db.HSSESystem_SafetyInstitution
                            where x.SafetyInstitutionId == id
                            orderby x.ApprovalDate descending
                            select new Model.SafeLawItem
                            {
                                ID = x.SafetyInstitutionId,
                                ReleaseStates = x.ReleaseStates,
                                ReleaseStatesName = db.Sys_Const.First(u => u.GroupId == ConstValue.Group_HSSE_ReleaseStates && u.ConstValue == x.ReleaseStates).ConstText,
                                Name = x.SafetyInstitutionName,
                                Code = x.Code,
                                TypeId = x.TypeId,
                                TypeName = db.Base_ManageRuleType.First(u => u.ManageRuleTypeId == x.TypeId).ManageRuleTypeName,
                                ReleaseUnit = x.ReleaseUnit,
                                ApprovalDate = x.ApprovalDate,
                                ApprovalDateStr = string.Format("{0:yyyy-MM-dd}", x.ApprovalDate),
                                EffectiveDate = x.EffectiveDate,
                                EffectiveDateStr = string.Format("{0:yyyy-MM-dd}", x.EffectiveDate),
                                AbolitionDate = x.AbolitionDate,
                                AbolitionDateStr = string.Format("{0:yyyy-MM-dd}", x.AbolitionDate),
                                ReplaceInfo = x.ReplaceInfo,
                                Description = x.Description,
                                IndexesIds = x.IndexesIds,
                                IndexesNames = ConstValue.getConstTextsConstValues(x.IndexesIds, ConstValue.Group_HSSE_Indexes),
                                UnitId = x.UnitId,
                                UnitName = db.Base_Unit.First(u => u.UnitId == x.UnitId).UnitName,
                                CompileManName = x.CompileMan,
                                CompileDate = x.CompileDate,
                                CompileDateStr = string.Format("{0:yyyy-MM-dd}", x.CompileDate),
                                DataType = type,
                                AttachUrl = db.AttachFile.First(z => z.ToKeyId == x.SafetyInstitutionId).AttachUrl.Replace('\\', '/'),
                            }).FirstOrDefault();
                }
                else
                {
                    return null;
                }
            }          
        }
        #endregion
    }
}
