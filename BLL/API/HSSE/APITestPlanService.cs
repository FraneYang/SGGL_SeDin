using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmitMapper;
using EmitMapper.MappingConfiguration;

namespace BLL
{
    /// <summary>
    /// 考试计划
    /// </summary>
    public static class APITestPlanService
    {
        #region 根据projectId、states获取考试计划列表
        /// <summary>
        /// 根据projectId、states获取考试计划列表
        /// </summary>
        /// <param name="projectId">项目ID</param>
        /// <param name="states">状态（0-待提交；1-已发布；2-考试中；3考试结束）</param>
        /// <returns></returns>
        public static List<Model.TestPlanItem> getTestPlanListByProjectIdStates(string projectId, string states)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var getDataLists = (from x in db.Training_TestPlan
                                    where x.ProjectId == projectId && (x.States == states || states == null)
                                    orderby x.TestStartTime descending
                                    select new Model.TestPlanItem
                                    {
                                        TestPlanId = x.TestPlanId,
                                        TestPlanCode = x.PlanCode,
                                        TestPlanName = x.PlanName,
                                        ProjectId = x.ProjectId,
                                        TestPlanManId = x.PlanManId,
                                        TestPlanManName = db.Sys_User.First(y => y.UserId == x.PlanManId).UserName,
                                        TestPalce = x.TestPalce,
                                        TestStartTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.TestStartTime),
                                        TestEndTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.TestEndTime),
                                        States = x.States,
                                        QRCodeUrl = x.QRCodeUrl.Replace('\\', '/'),
                                    }).ToList();
                return getDataLists;
            }
        }
        #endregion

        #region 根据考试ID获取考试计划详细
        /// <summary>
        /// 根据考试ID获取考试计划详细
        /// </summary>
        /// <param name="testPlanId"></param>
        /// <returns></returns>
        public static Model.TestPlanItem getTestPlanByTestPlanId(string testPlanId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var getDataLists = from x in db.Training_TestPlan
                                   where x.TestPlanId == testPlanId
                                   select new Model.TestPlanItem
                                   {
                                       TestPlanId = x.TestPlanId,
                                       ProjectId = x.ProjectId,
                                       TestPlanCode = x.PlanCode,
                                       TestPlanName = x.PlanName,
                                       TestPlanManId = x.PlanManId,
                                       TestPlanManName = db.Sys_User.First(y => y.UserId == x.TestPlanId).UserName,
                                       TestPlanDate = string.Format("{0:yyyy-MM-dd HH:mm}", x.PlanDate),
                                       TestStartTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.TestStartTime),
                                       TestEndTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.TestEndTime),
                                       Duration = x.Duration,
                                       SValue = x.SValue,
                                       MValue = x.MValue,
                                       JValue = x.JValue,
                                       TotalScore = x.TotalScore ?? 0,
                                       QuestionCount = x.QuestionCount ?? 0,
                                       TestPalce = x.TestPalce,
                                       UnitIds = x.UnitIds,
                                       UnitNames = UnitService.getUnitNamesUnitIds(x.UnitIds),
                                       WorkPostIds = x.WorkPostIds,
                                       WorkPostNames = WorkPostService.getWorkPostNamesWorkPostIds(x.WorkPostIds),
                                       States = x.States,
                                       QRCodeUrl = x.QRCodeUrl.Replace('\\', '/'),
                                       TrainingPlanId = x.PlanId,
                                   };
                return getDataLists.FirstOrDefault();
            }
        }
        #endregion

        #region 保存考试计划
        /// <summary>
        /// 保存TestPlan
        /// </summary>
        /// <param name="getTestPlan">考试计划记录</param>
        public static string SaveTestPlan(Model.TestPlanItem getTestPlan)
        {
            string alterStr =string.Empty ;
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                Model.Training_TestPlan newTestPlan = new Model.Training_TestPlan
                {
                    TestPlanId = getTestPlan.TestPlanId,
                    ProjectId = getTestPlan.ProjectId,
                    PlanCode = getTestPlan.TestPlanCode,
                    PlanName = getTestPlan.TestPlanName,
                    PlanManId = getTestPlan.TestPlanManId,
                    //PlanDate= getTestPlan.TestPlanDate,
                    TestStartTime = Funs.GetNewDateTimeOrNow(getTestPlan.TestStartTime),
                    TestEndTime = Funs.GetNewDateTimeOrNow(getTestPlan.TestEndTime),
                    Duration = getTestPlan.Duration,
                    SValue = getTestPlan.SValue,
                    MValue = getTestPlan.MValue,
                    JValue = getTestPlan.JValue,
                    TotalScore = getTestPlan.TotalScore,
                    QuestionCount = getTestPlan.QuestionCount,
                    TestPalce = getTestPlan.TestPalce,
                    UnitIds = getTestPlan.UnitIds,
                    WorkPostIds = getTestPlan.WorkPostIds,
                    States = getTestPlan.States,
                    PlanDate = DateTime.Now,
                };

                if (!string.IsNullOrEmpty(getTestPlan.TrainingPlanId))
                {
                    newTestPlan.PlanId = getTestPlan.TrainingPlanId;
                }
                var isUpdate = db.Training_TestPlan.FirstOrDefault(x => x.TestPlanId == newTestPlan.TestPlanId);
                if (isUpdate == null)
                {
                    string unitId = string.Empty;
                    var user = db.Sys_User.FirstOrDefault(e => e.UserId == newTestPlan.PlanManId); 
                    if (user != null)
                    {
                        unitId = user.UnitId;
                    }
                    newTestPlan.PlanCode = CodeRecordsService.ReturnCodeByMenuIdProjectId(Const.ProjectTestPlanMenuId, newTestPlan.ProjectId, unitId);
                    if (string.IsNullOrEmpty(newTestPlan.TestPlanId))
                    {
                        newTestPlan.TestPlanId = SQLHelper.GetNewID();
                    }

                    db.Training_TestPlan.InsertOnSubmit(newTestPlan);
                    db.SubmitChanges();

                    CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(Const.ProjectTestPlanMenuId, newTestPlan.ProjectId, null, newTestPlan.TestPlanId, newTestPlan.PlanDate);
                }
                else
                {
                    isUpdate.States = newTestPlan.States;
                    if (isUpdate.States == "0" || isUpdate.States == "1")
                    {
                        isUpdate.PlanName = newTestPlan.PlanName;
                        isUpdate.PlanManId = newTestPlan.PlanManId;
                        isUpdate.PlanDate = newTestPlan.PlanDate;
                        isUpdate.TestStartTime = newTestPlan.TestStartTime;
                        isUpdate.TestEndTime = newTestPlan.TestEndTime;
                        isUpdate.Duration = newTestPlan.Duration;
                        isUpdate.TotalScore = newTestPlan.TotalScore;
                        isUpdate.QuestionCount = newTestPlan.QuestionCount;
                        isUpdate.TestPalce = newTestPlan.TestPalce;
                        isUpdate.UnitIds = newTestPlan.UnitIds;
                        isUpdate.WorkPostIds = newTestPlan.WorkPostIds;
                        ////删除 考生记录
                        var deleteRecords = from x in db.Training_TestRecord
                                            where x.TestPlanId == isUpdate.TestPlanId
                                            select x;
                        if (deleteRecords.Count() > 0)
                        {
                            foreach (var item in deleteRecords)
                            {
                                var testRecordItem = from x in db.Training_TestRecordItem
                                                     where x.TestRecordId == item.TestRecordId
                                                     select x;
                                if (testRecordItem.Count() > 0)
                                {
                                    db.Training_TestRecordItem.DeleteAllOnSubmit(testRecordItem);
                                    db.SubmitChanges();
                                }
                            }

                           db.Training_TestRecord.DeleteAllOnSubmit(deleteRecords);
                            db.SubmitChanges();
                        }

                        ////删除 考试题目类型
                        var deleteTestPlanTrainings = from x in db.Training_TestPlanTraining where x.TestPlanId == isUpdate.TestPlanId select x;
                        if (deleteTestPlanTrainings.Count() > 0)
                        {
                            db.Training_TestPlanTraining.DeleteAllOnSubmit(deleteTestPlanTrainings);
                            db.SubmitChanges();
                        }

                    }
                    else if (isUpdate.States == "3") ////考试状态3时 更新培训计划状态 把培训计划写入培训记录中
                    {
                        DateTime? endTime = Funs.GetNewDateTime(getTestPlan.TestEndTime);
                        ////判断是否有未考完的考生
                        var getTrainingTestRecords = db.Training_TestRecord.FirstOrDefault(x => x.TestPlanId == isUpdate.TestPlanId
                                            && (!x.TestStartTime.HasValue || ((!x.TestEndTime.HasValue || !x.TestScores.HasValue) && x.TestStartTime.Value.AddMinutes(isUpdate.Duration) >= DateTime.Now)));
                        if (getTrainingTestRecords != null && endTime.HasValue && endTime.Value.AddMinutes(isUpdate.Duration) < DateTime.Now)
                        {
                            alterStr = "当前存在未交卷考生，不能提前结束考试！";
                            isUpdate.States = "2";
                        }
                        else
                        {
                            SubmitTest(isUpdate);
                        }
                    }
                    else if (newTestPlan.States == "2") ////开始考试 只更新考试计划状态为考试中。
                    {
                        if (isUpdate.TestStartTime > DateTime.Now)
                        {
                            isUpdate.States = "1";
                            alterStr = "未到考试扫码开始时间，不能开始考试！";
                        }
                    }
                    if (string.IsNullOrEmpty(alterStr))
                    {
                        db.SubmitChanges();
                    }
                }

                if (newTestPlan.States == "0" || newTestPlan.States == "1")
                {
                    if (getTestPlan.TestRecordItems.Count() > 0)
                    {
                        ////新增考试人员明细                        
                        foreach (var item in getTestPlan.TestRecordItems)
                        {
                            var person = db.SitePerson_Person.FirstOrDefault(e => e.PersonId == item.TestManId); 
                            if (person != null)
                            {
                                Model.Training_TestRecord newTrainDetail = new Model.Training_TestRecord
                                {
                                    TestRecordId = SQLHelper.GetNewID(),
                                    ProjectId = newTestPlan.ProjectId,
                                    TestPlanId = newTestPlan.TestPlanId,
                                    TestManId = item.TestManId,
                                    TestType = item.TestType,
                                    Duration = newTestPlan.Duration,
                                };
                                db.Training_TestRecord.InsertOnSubmit(newTrainDetail);
                                db.SubmitChanges();
                            }
                        }
                    }
                    if (getTestPlan.TestPlanTrainingItems.Count() > 0)
                    {
                        foreach (var item in getTestPlan.TestPlanTrainingItems)
                        {
                            var trainingType = TestTrainingService.GetTestTrainingById(item.TrainingTypeId);
                            if (trainingType != null)
                            {
                                Model.Training_TestPlanTraining newPlanItem = new Model.Training_TestPlanTraining
                                {
                                    TestPlanTrainingId = SQLHelper.GetNewID(),
                                    TestPlanId = newTestPlan.TestPlanId,
                                    TrainingId = item.TrainingTypeId,
                                    TestType1Count = item.TestType1Count,
                                    TestType2Count = item.TestType2Count,
                                    TestType3Count = item.TestType3Count,
                                };

                                db.Training_TestPlanTraining.InsertOnSubmit(newPlanItem);
                                db.SubmitChanges();
                            }
                        }
                    }
                }
            }
            return alterStr;
        }

        /// <summary>
        ///  新增 考试人员明细
        /// </summary>
        public static void AddTrainingTestRecord(List<Model.TestRecordItem> testRecords, Model.Training_TestPlan newTestPlan)
        {
            foreach (var item in testRecords)
            {
                var person = PersonService.GetPersonById(item.TestManId);
                if (person != null)
                {
                    Model.Training_TestRecord newTrainDetail = new Model.Training_TestRecord
                    {
                        TestRecordId = SQLHelper.GetNewID(),
                        ProjectId = newTestPlan.ProjectId,
                        TestPlanId = newTestPlan.TestPlanId,
                        TestManId = item.TestManId,
                        TestType = item.TestType,
                        Duration= newTestPlan.Duration,
                    };
                    TestRecordService.AddTestRecord(newTrainDetail);
                }
            }
        }

        /// <summary>
        ///  新增 考试教材类型 明细
        /// </summary>
        public static void AddTrainingTestPlanTraining(List<Model.TestPlanTrainingItem> TestPlanItems, string testPlanId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                foreach (var item in TestPlanItems)
                {
                    var trainingType = db.Training_TestTraining.FirstOrDefault(e => e.TrainingId == item.TrainingTypeId); 
                    if (trainingType != null)
                    {
                        Model.Training_TestPlanTraining newPlanItem = new Model.Training_TestPlanTraining
                        {
                            TestPlanTrainingId = SQLHelper.GetNewID(),
                            TestPlanId = testPlanId,
                            TrainingId = item.TrainingTypeId,
                            TestType1Count = item.TestType1Count,
                            TestType2Count = item.TestType2Count,
                            TestType3Count = item.TestType3Count,
                        };

                        db.Training_TestPlanTraining.InsertOnSubmit(newPlanItem);
                        db.SubmitChanges();
                    }
                }
            }
        }
        #endregion

        #region 根据培训计划ID生成 考试计划信息
        /// <summary>
        /// 根据培训计划ID生成 考试计划信息
        /// </summary>
        /// <param name="getTestPlan"></param>
        public static string SaveTestPlanByTrainingPlanId(string trainingPlanId,string userId)
        {
            string testPlanId = string.Empty;
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                ////培训计划
                var getTrainingPlan = db.Training_Plan.FirstOrDefault(x => x.PlanId == trainingPlanId); ;
                if (getTrainingPlan != null && getTrainingPlan.States == "1")
                {
                    testPlanId = SQLHelper.GetNewID();
                    Model.Training_TestPlan newTestPlan = new Model.Training_TestPlan
                    {
                        TestPlanId = testPlanId,
                        ProjectId = getTrainingPlan.ProjectId,
                        //PlanCode = getTrainingPlan.PlanCode,
                        PlanName = getTrainingPlan.PlanName,
                        PlanManId = userId,
                        PlanDate = DateTime.Now,
                        TestStartTime = DateTime.Now,
                        TestPalce = getTrainingPlan.TeachAddress,
                        UnitIds = getTrainingPlan.UnitIds,
                        UnitNames = UnitService.getUnitNamesUnitIds(getTrainingPlan.UnitIds),
                        WorkPostIds = getTrainingPlan.WorkPostId,
                        WorkPostNames = WorkPostService.getWorkPostNamesWorkPostIds(getTrainingPlan.WorkPostId),
                        PlanId = getTrainingPlan.PlanId,
                        States = "0",
                    };

                    string unitId = string.Empty;
                    var user = db.Sys_User.FirstOrDefault(e => e.UserId == userId); 
                    if (user != null)
                    {
                        unitId = user.UnitId;
                    }
                    newTestPlan.PlanCode = CodeRecordsService.ReturnCodeByMenuIdProjectId(Const.ProjectTestPlanMenuId, newTestPlan.ProjectId, unitId);

                    int Duration = 90;
                    int SValue = 1;
                    int MValue = 2;
                    int JValue = 1;
                    int testType1Count = 40;
                    int testType2Count = 10;
                    int testType3Count = 40;
                    ////获取考试规则设置数据
                    var getTestRule = db.Sys_TestRule.FirstOrDefault(); ////考试数据设置
                    if (getTestRule != null)
                    {
                        Duration = getTestRule.Duration;
                        SValue = getTestRule.SValue;
                        MValue = getTestRule.MValue;
                        JValue = getTestRule.JValue;
                        testType1Count = getTestRule.SCount;
                        testType2Count = getTestRule.MCount;
                        testType3Count = getTestRule.JCount;
                    }
                    ////按照培训类型获取试题类型及题型数量
                    var getTrainTypeItems = from x in db.Base_TrainTypeItem
                                            where x.TrainTypeId == getTrainingPlan.TrainTypeId
                                            select x;
                    if (getTrainTypeItems.Count() > 0)
                    {
                        testType1Count = getTrainTypeItems.Sum(x => x.SCount);
                        testType2Count = getTrainTypeItems.Sum(x => x.MCount);
                        testType3Count = getTrainTypeItems.Sum(x => x.JCount);
                    }

                    newTestPlan.Duration = getTestRule.Duration;
                    newTestPlan.SValue = getTestRule.SValue;
                    newTestPlan.MValue = getTestRule.MValue;
                    newTestPlan.JValue = getTestRule.JValue;
                    newTestPlan.TotalScore = testType1Count * SValue + testType2Count * MValue + testType3Count * JValue;
                    newTestPlan.QuestionCount = testType1Count + testType2Count + testType3Count;
                    newTestPlan.TestEndTime = newTestPlan.TestStartTime.AddMinutes(newTestPlan.Duration);
                    ////新增考试计划记录
                    var getIsTestPlan = db.Training_TestPlan.FirstOrDefault(x => x.PlanId == newTestPlan.PlanId);
                    if (getIsTestPlan == null)
                    {
                        db.Training_TestPlan.InsertOnSubmit(newTestPlan);
                        db.SubmitChanges();
                        CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(Const.ProjectTestPlanMenuId, newTestPlan.ProjectId, null, testPlanId, newTestPlan.PlanDate);
                        ///培训人员
                        var getTrainingTask = (from x in db.Training_Task where x.PlanId == trainingPlanId select x).ToList();
                        foreach (var itemTask in getTrainingTask)
                        {
                            Model.Training_TestRecord newTestRecord = new Model.Training_TestRecord
                            {
                                TestRecordId = SQLHelper.GetNewID(),
                                ProjectId = getTrainingPlan.ProjectId,
                                TestPlanId = testPlanId,
                                TestManId = itemTask.UserId,
                                TestType= db.Base_TrainType.First(z=>z.TrainTypeId == getTrainingPlan.TrainTypeId).TrainTypeName,
                            };

                            db.Training_TestRecord.InsertOnSubmit(newTestRecord);
                            db.SubmitChanges();
                        }
                        if (getTrainTypeItems.Count() == 0)
                        {
                            /////考试题型类别及数量
                            Model.Training_TestPlanTraining newTestPlanTraining = new Model.Training_TestPlanTraining
                            {
                                TestPlanTrainingId = SQLHelper.GetNewID(),
                                TestPlanId = testPlanId,
                                TestType1Count = testType1Count,
                                TestType2Count = testType2Count,
                                TestType3Count = testType3Count,
                            };

                            db.Training_TestPlanTraining.InsertOnSubmit(newTestPlanTraining);
                            db.SubmitChanges();
                        }
                        else
                        {
                            foreach (var item in getTrainTypeItems)
                            {
                                /////考试题型类别及数量
                                Model.Training_TestPlanTraining newTestPlanTraining = new Model.Training_TestPlanTraining
                                {
                                    TestPlanTrainingId = SQLHelper.GetNewID(),
                                    TestPlanId = testPlanId,
                                    TrainingId = item.TrainingId,
                                    TestType1Count = item.SCount,
                                    TestType2Count = item.MCount,
                                    TestType3Count = item.JCount,
                                };
                                db.Training_TestPlanTraining.InsertOnSubmit(newTestPlanTraining);
                                db.SubmitChanges();
                            }
                        }
                    }
                    ////回写培训计划状态
                    getTrainingPlan.States = "2";
                    db.SubmitChanges();
                }
            }
            return testPlanId;
        }
        #endregion

        #region 根据TestPlanId获取考试试题类型列表
        /// <summary>
        /// 根据TestPlanId获取考试试题类型列表
        /// </summary>
        /// <param name="testPlanId">考试计划ID</param>
        /// <returns></returns>
        public static List<Model.TestPlanTrainingItem> getTestPlanTrainingListByTestPlanId(string testPlanId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var getDataLists = (from x in db.Training_TestPlanTraining
                                    join y in db.Training_TestTraining on x.TrainingId equals y.TrainingId
                                    where x.TestPlanId == testPlanId
                                    orderby y.TrainingCode
                                    select new Model.TestPlanTrainingItem
                                    {
                                        TestPlanTrainingId = x.TestPlanTrainingId,
                                        TestPlanId = x.TestPlanId,
                                        TrainingTypeId = x.TrainingId,
                                        TrainingTypeName = y.TrainingName,
                                        TestType1Count = x.TestType1Count ?? 0,
                                        TestType2Count = x.TestType2Count ?? 0,
                                        TestType3Count = x.TestType3Count ?? 0,
                                    }).ToList();
                return getDataLists;
            }
        }
        #endregion

        /// <summary>
        /// 结束考试
        /// </summary>
        public static void SubmitTest(Model.Training_TestPlan getTestPlan)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                ////所有交卷的人员 交卷 并计算分数
                var getTrainingTestRecords = from x in db.Training_TestRecord
                                             where x.TestPlanId == getTestPlan.TestPlanId && (!x.TestEndTime.HasValue || !x.TestScores.HasValue)
                                             select x;
                foreach (var itemRecord in getTrainingTestRecords)
                {
                    itemRecord.TestEndTime = DateTime.Now;
                    itemRecord.TestScores = db.Training_TestRecordItem.Where(x => x.TestRecordId == itemRecord.TestRecordId).Sum(x => x.SubjectScore) ?? 0;
                    db.SubmitChanges();
                }

                var getTrainingTasks = from x in db.Training_Task
                                       where x.PlanId == getTestPlan.PlanId && (x.States != "2" || x.States == null)
                                       select x;
                foreach (var item in getTrainingTasks)
                {
                    item.States = "2";
                    db.SubmitChanges();
                }

                ////TODO 讲培训计划 考试记录 写入到培训记录
                APITrainRecordService.InsertTrainRecord(getTestPlan);
            }
        }
    }
}
