using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmitMapper;
using EmitMapper.MappingConfiguration;

namespace BLL
{
    public static class APIServerTestRecordService
    {
        #region 获取考生信息
        /// <summary>
        /// 获取考生信息
        /// </summary>
        /// <param name="testPlanId"></param>
        /// <param name="testManId"></param>
        /// <param name="userType"></param>
        /// <param name="identityCard"></param>
        /// <returns></returns>
        public static Model.TestRecordItem getTestRecordInfo(string testPlanId, string testManId, string userType, string identityCard)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                Model.TestRecordItem newTestRecord = new Model.TestRecordItem();
                var getTestPlan = db.Test_TestPlan.FirstOrDefault(x => x.TestPlanId == testPlanId);
                if (getTestPlan != null)
                {
                    newTestRecord.TestPlanName = getTestPlan.PlanName;
                    newTestRecord.TestPlanId = testPlanId;
                    newTestRecord.TestPlanStartTime = string.Format("{0:yyyy-MM-dd HH:mm:ss}", getTestPlan.TestStartTime);
                    newTestRecord.TestPlanEndTime = string.Format("{0:yyyy-MM-dd HH:mm:ss}", getTestPlan.TestEndTime);                   
                    newTestRecord.TestManId = testManId;                   
                    newTestRecord.UserType = userType;
                    var getUpdateTestRecord = db.Test_TestRecord.FirstOrDefault(x => x.TestPlanId == testPlanId && x.IdentityCard == identityCard);                    
                    if (getUpdateTestRecord == null && userType != "2")
                    {
                        getUpdateTestRecord = db.Test_TestRecord.FirstOrDefault(x => x.TestPlanId == testPlanId && x.TestManId == testManId);
                    }
                    if (getUpdateTestRecord != null)
                    {
                        newTestRecord.TestRecordId = getUpdateTestRecord.TestRecordId;
                        newTestRecord.DepartId = getUpdateTestRecord.DepartId;                     
                        newTestRecord.UnitId = getUpdateTestRecord.UnitId;                        
                        newTestRecord.ProjectId = getUpdateTestRecord.ProjectId;                        
                        newTestRecord.TestManName = getUpdateTestRecord.TestManName;
                        newTestRecord.Telephone = getUpdateTestRecord.Telephone;
                        newTestRecord.IdentityCard = getUpdateTestRecord.IdentityCard;
                        newTestRecord.TestScores = getUpdateTestRecord.TestScores ?? 0;
                    }
                    else
                    {
                        if (userType == "1")
                        {
                            var getUser = db.Sys_User.FirstOrDefault(x => x.UserId == testManId);
                            if (getUser != null)
                            {
                                newTestRecord.TestManName = getUser.UserName;
                                newTestRecord.UnitId = getUser.UnitId;
                                newTestRecord.DepartId = getUser.DepartId;
                                newTestRecord.IdentityCard = getUser.IdentityCard;
                                newTestRecord.Telephone = getUser.Telephone;
                            }
                        }
                        else if (userType == "3")
                        {
                            var getPerson = db.SitePerson_Person.FirstOrDefault(x => x.PersonId == testManId);
                            if (getPerson != null)
                            {
                                newTestRecord.TestManName = getPerson.PersonName;
                                newTestRecord.ProjectId = getPerson.ProjectId;
                                newTestRecord.UnitId = getPerson.UnitId;
                                newTestRecord.WorkPostId = getPerson.WorkPostId;
                                newTestRecord.IdentityCard = getPerson.IdentityCard;
                                newTestRecord.Telephone = getPerson.Telephone;
                            }
                        }
                        else
                        {
                            newTestRecord.UnitId = newTestRecord.UnitId = Const.UnitId_SEDIN;
                        }
                    }
                    newTestRecord.DepartName = DepartService.getDepartNameById(newTestRecord.DepartId);
                    newTestRecord.UnitName = UnitService.GetUnitNameByUnitId(newTestRecord.UnitId);
                    newTestRecord.ProjectName = ProjectService.GetProjectNameByProjectId(newTestRecord.ProjectId);
                    if (newTestRecord.UnitId == Const.UnitId_SEDIN)
                    {
                        newTestRecord.IsThiUnit = true;
                    }
                    else
                    {
                        newTestRecord.IsThiUnit = false;
                    }                    
                }
                return newTestRecord;
            }
        }
        #endregion

        #region 保存考生信息
        /// <summary>
        /// 保存考生信息
        /// </summary>
        /// <param name="testRecord">考试计划记录</param>
        public static Model.TestRecordItem SaveTestRecord(Model.TestRecordItem testRecord)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var getTestPlan = db.Test_TestPlan.FirstOrDefault(x => x.TestPlanId == testRecord.TestPlanId);
                if (getTestPlan != null)
                {
                    testRecord.TestPlanName = getTestPlan.PlanName;
                    testRecord.TestPlanStartTime = string.Format("{0:yyyy-MM-dd HH:mm:ss}", getTestPlan.TestStartTime);
                    testRecord.TestPlanEndTime = string.Format("{0:yyyy-MM-dd HH:mm:ss}", getTestPlan.TestEndTime);
                    Model.Test_TestRecord newTestRecord = new Model.Test_TestRecord
                    {
                        TestPlanId = testRecord.TestPlanId,
                        TestManId = testRecord.TestManId,
                        TestManName = testRecord.TestManName,
                        IdentityCard = testRecord.IdentityCard,
                        Telephone = testRecord.Telephone,
                        ManType = testRecord.UserType,
                        Duration = getTestPlan.Duration,
                    };
                    if (!string.IsNullOrEmpty(testRecord.UnitId))
                    {
                        newTestRecord.UnitId = testRecord.UnitId;
                    }
                    if (!string.IsNullOrEmpty(testRecord.DepartId))
                    {
                        newTestRecord.DepartId = testRecord.DepartId;
                    }
                    if (!string.IsNullOrEmpty(testRecord.WorkPostId))
                    {
                        newTestRecord.WorkPostId = testRecord.WorkPostId;
                    }
                    if (!string.IsNullOrEmpty(testRecord.ProjectId))
                    {
                        newTestRecord.ProjectId = testRecord.ProjectId;
                    }

                    if (string.IsNullOrEmpty(newTestRecord.ManType))
                    {
                        if (testRecord.TestManId == Const.sedinId)
                        {
                            newTestRecord.ManType = "2";
                        }
                        else if (UserService.GetUserByUserId(testRecord.TestManId) != null)
                        {
                            newTestRecord.ManType = "1";
                        }
                        else
                        {
                            newTestRecord.ManType = "3";
                        }
                    }

                    Model.Test_TestRecord getUpdateTestRecord = new Model.Test_TestRecord();
;                   getUpdateTestRecord = db.Test_TestRecord.FirstOrDefault(x => x.TestRecordId == testRecord.TestRecordId);
                    if (getUpdateTestRecord == null)
                    {
                        getUpdateTestRecord = db.Test_TestRecord.FirstOrDefault(x => x.TestPlanId == testRecord.TestPlanId && x.IdentityCard == testRecord.IdentityCard);
                    }
                    if (getUpdateTestRecord != null)
                    {
                        testRecord.TestRecordId = getUpdateTestRecord.TestRecordId;
                        testRecord.TestEndTime = string.Format("{0:yyyy-MM:dd HH:mm:ss}", getUpdateTestRecord.TestEndTime);
                        testRecord.TestStartTime = string.Format("{0:yyyy-MM:dd HH:mm:ss}", getUpdateTestRecord.TestStartTime);
                        testRecord.TestScores = getUpdateTestRecord.TestScores ?? 0;

                        if (string.IsNullOrEmpty(getUpdateTestRecord.ManType))
                        {
                            getUpdateTestRecord.ManType = newTestRecord.ManType;
                        }
                        if (!getUpdateTestRecord.TestEndTime.HasValue && getTestPlan.TestEndTime < DateTime.Now)
                        {
                            getUpdateTestRecord.DepartId = newTestRecord.DepartId;
                            getUpdateTestRecord.TestManName = newTestRecord.TestManName;
                            getUpdateTestRecord.IdentityCard = newTestRecord.IdentityCard;
                            getUpdateTestRecord.Telephone = newTestRecord.Telephone;
                            db.SubmitChanges();
                        }
                    }
                    else
                    {
                        if (getTestPlan.TestEndTime > DateTime.Now)
                        {
                            var getTestPlanTraining = db.Test_TestPlanTraining.Where(x => x.TestPlanId == getTestPlan.TestPlanId  && x.UserType == testRecord.UserType );
                            if (getTestPlanTraining.Count() > 0)
                            {
                                int cout1 = getTestPlanTraining.Sum(x => x.TestType1Count ?? 0);
                                int cout2 = getTestPlanTraining.Sum(x => x.TestType2Count ?? 0);
                                int cout3 = getTestPlanTraining.Sum(x => x.TestType3Count ?? 0);
                                newTestRecord.QuestionCount = cout1 + cout2 + cout3;
                                newTestRecord.TotalScore = (getTestPlan.SValue ?? 0) * cout1 + (getTestPlan.MValue ?? 0) * cout2 + (getTestPlan.JValue ?? 0) * cout3;
                            }

                            testRecord.TestRecordId = newTestRecord.TestRecordId = SQLHelper.GetNewID();
                            db.Test_TestRecord.InsertOnSubmit(newTestRecord);
                            db.SubmitChanges();
                        }
                    }

                    if (!string.IsNullOrEmpty(testRecord.TestEndTime))
                    {
                        testRecord.TestStates = "3";
                    }
                    else if (getTestPlan.States == Const.State_2 && getTestPlan.TestEndTime >= DateTime.Now)
                    {
                        testRecord.TestStates = "2";
                    }
                    else if (getTestPlan.States != Const.State_2 || getTestPlan.TestStartTime < DateTime.Now)
                    {
                        testRecord.TestStates = "1";
                    }
                    else if (getTestPlan.TestEndTime < DateTime.Now)
                    {
                        testRecord.TestStates = "0";
                    }
                }
            }
            return testRecord;
        }
        #endregion

        #region 根据TestPlanId获取考试试卷人员列表
        /// <summary>
        /// 根据TestPlanId获取考试人员列表
        /// </summary>
        /// <param name="testPlanId"></param>
        /// <returns>考试人员</returns>
        public static List<Model.TestRecordItem> getTestRecordListByTestPlanId(string testPlanId)
        {
            var getDataLists = (from x in new Model.SGGLDB(Funs.ConnString).Test_TestRecord
                                where x.TestPlanId == testPlanId
                                orderby x.TestStartTime descending
                                select new Model.TestRecordItem
                                {
                                    TestRecordId = x.TestRecordId,
                                    ProjectId = x.ProjectId,
                                    ProjectName = new Model.SGGLDB(Funs.ConnString).Base_Project.First(z => z.ProjectId == x.ProjectId).ProjectName,
                                    UnitId = x.UnitId,
                                    UnitName = new Model.SGGLDB(Funs.ConnString).Base_Unit.First(z => z.UnitId == x.UnitId).UnitName,
                                    DepartId = x.DepartId,
                                    DepartName = new Model.SGGLDB(Funs.ConnString).Base_Depart.First(z => z.DepartId == x.DepartId).DepartName,
                                    TestPlanId = x.TestPlanId,
                                    TestManId = x.TestManId,
                                    TestManName = x.TestManName,
                                    TestStartTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.TestStartTime),
                                    TestEndTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.TestEndTime),
                                    TestScores = x.TestScores ?? 0,
                                }).ToList();
            return getDataLists;
        }
        #endregion

        #region 根据试卷ID获取试卷记录详细
        /// <summary>
        /// 根据试卷ID获取试卷记录详细
        /// </summary>
        /// <param name="testRecordId"></param>
        /// <returns></returns>
        public static Model.TestRecordItem getTestRecordByTestRecordId(string testRecordId)
        {
            var getDataLists = from x in new Model.SGGLDB(Funs.ConnString).Test_TestRecord
                               join y in new Model.SGGLDB(Funs.ConnString).Training_TestPlan on x.TestPlanId equals y.TestPlanId
                               where x.TestRecordId == testRecordId
                               select new Model.TestRecordItem
                               {
                                   TestRecordId = x.TestRecordId,
                                   TestPlanId = x.TestPlanId,
                                   TestManId = x.TestManId,
                                   TestManName = x.TestManName,
                                   UserType = x.ManType,
                                   TestStartTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.TestStartTime),
                                   TestEndTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.TestEndTime),
                                   TotalScore=x.TotalScore ?? 0,
                                   QuestionCount = x.QuestionCount ?? 0,
                                   Duration = x.Duration ?? 0,
                                   TestScores = x.TestScores ?? 0,
                                   ProjectId = x.ProjectId,
                                   ProjectName = new Model.SGGLDB(Funs.ConnString).Base_Project.First(z => z.ProjectId == x.ProjectId).ProjectName,
                                   UnitId = x.UnitId,
                                   UnitName = new Model.SGGLDB(Funs.ConnString).Base_Unit.First(z => z.UnitId == x.UnitId).UnitName,
                                   DepartId = x.DepartId,
                                   DepartName = new Model.SGGLDB(Funs.ConnString).Base_Depart.First(z => z.DepartId == x.DepartId).DepartName,
                                   IdentityCard=x.IdentityCard,
                                   Telephone=x.Telephone,
                               };
            return getDataLists.FirstOrDefault();
        }
        #endregion

        #region 生成试卷开始考试
        /// <summary>
        /// 生成试卷开始考试
        /// </summary>
        /// <param name="testPlanId">培训考试计划ID</param>
        /// <param name="testRecordId">考生信息ID</param>
        /// <returns></returns>
        public static string CreateTestRecordItem(string testPlanId, string testRecordId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var getTestPlan = db.Test_TestPlan.FirstOrDefault(x => x.TestPlanId == testPlanId);
                var getTestRecord = db.Test_TestRecord.FirstOrDefault(x => x.TestRecordId == testRecordId);
                if(getTestPlan != null && getTestRecord != null)
                {                  
                    ////是否已经存在试卷 
                    var item = db.Test_TestRecordItem.FirstOrDefault(x => x.TestRecordId == testRecordId);
                    if (item == null)
                    {
                        var getTestPlanTraining = db.Test_TestPlanTraining.Where(x => x.TestPlanId == testPlanId && x.UserType == getTestRecord.ManType);
                        if (getTestPlanTraining.Count() > 0)
                        {
                            List<Model.Training_TestTrainingItem> getTestTrainingItemList = new List<Model.Training_TestTrainingItem>();
                            List<string> listTrainingId = getTestPlanTraining.Select(x => x.TrainingId).ToList();
                            //// 计划考试中单选、多选、判断题总数
                            int sumTestType1Count = getTestPlanTraining.Sum(x => x.TestType1Count) ?? 0;
                            int sumTestType2Count = getTestPlanTraining.Sum(x => x.TestType2Count) ?? 0;
                            int sumTestType3Count = getTestPlanTraining.Sum(x => x.TestType3Count) ?? 0;

                            ////获取类型下适合岗位试题集合
                            var getTestTrainingItemALLs = from x in db.Training_TestTrainingItem
                                                          where listTrainingId.Contains(x.TrainingId)
                                                          select x;
                            foreach (var itemT in getTestPlanTraining)
                            {
                                //// 获取类型下的题目
                                var getTestTrainingItems = getTestTrainingItemALLs.Where(x => x.TrainingId == itemT.TrainingId).ToList();
                                if (getTestTrainingItems.Count() > 0)
                                {
                                    ////单选题
                                    var getSItem = getTestTrainingItems.Where(x => x.TestType == "1").OrderBy(x => Guid.NewGuid()).Take(itemT.TestType1Count ?? 1);
                                    if (getSItem.Count() > 0)
                                    {
                                        getTestTrainingItemList.AddRange(getSItem);
                                    }
                                    ///多选题
                                    var getMItem = getTestTrainingItems.Where(x => x.TestType == "2").OrderBy(x => Guid.NewGuid()).Take(itemT.TestType2Count ?? 1);
                                    if (getMItem.Count() > 0)
                                    {
                                        getTestTrainingItemList.AddRange(getMItem);
                                    }
                                    ///判断题
                                    var getJItem = getTestTrainingItems.Where(x => x.TestType == "3").OrderBy(x => Guid.NewGuid()).Take(itemT.TestType3Count ?? 1);
                                    if (getJItem.Count() > 0)
                                    {
                                        getTestTrainingItemList.AddRange(getJItem);
                                    }
                                }
                            }

                            //// 获取得到的单选题、多选题、判断题 数量
                            int getDiffTestType1Count = sumTestType1Count - getTestTrainingItemList.Where(x => x.TestType == "1").Count();
                            int getDiffTestType2Count = sumTestType2Count - getTestTrainingItemList.Where(x => x.TestType == "2").Count();
                            int getDiffTestType3Count = sumTestType3Count - getTestTrainingItemList.Where(x => x.TestType == "3").Count();
                            if (getDiffTestType1Count > 0 || getDiffTestType2Count > 0 || getDiffTestType3Count > 0)
                            {
                                var getTestTrainingItemNulls = db.Training_TestTrainingItem.Where(x => x.WorkPostIds == null).ToList();
                                if (getTestTrainingItemNulls.Count() > 0)
                                {
                                    /// 通用且未选择的题目
                                    var getTestTrainingItemDiffs = getTestTrainingItemNulls.Except(getTestTrainingItemList).ToList();
                                    ////单选题
                                    if (getDiffTestType1Count > 0)
                                    {
                                        var getSItemD = getTestTrainingItemDiffs.Where(x => x.TestType == "1").OrderBy(x => Guid.NewGuid()).Take(getDiffTestType1Count);
                                        if (getSItemD.Count() > 0)
                                        {
                                            getTestTrainingItemList.AddRange(getSItemD);
                                        }
                                    }
                                    ///多选题
                                    if (getDiffTestType2Count > 0)
                                    {
                                        var getMItemD = getTestTrainingItemDiffs.Where(x => x.TestType == "2").OrderBy(x => Guid.NewGuid()).Take(getDiffTestType2Count);
                                        if (getMItemD.Count() > 0)
                                        {
                                            getTestTrainingItemList.AddRange(getMItemD);
                                        }
                                    }
                                    ///判断题
                                    if (getDiffTestType3Count > 0)
                                    {
                                        var getJItemD = getTestTrainingItemDiffs.Where(x => x.TestType == "3").OrderBy(x => Guid.NewGuid()).Take(getDiffTestType3Count);
                                        if (getJItemD.Count() > 0)
                                        {
                                            getTestTrainingItemList.AddRange(getJItemD);
                                        }
                                    }
                                }
                            }

                            if (getTestTrainingItemList.Count() > 0)
                            {
                                var getItems = from x in getTestTrainingItemList
                                               select new Model.Test_TestRecordItem
                                               {
                                                   TestRecordItemId = SQLHelper.GetNewID(),
                                                   TestRecordId = getTestRecord.TestRecordId,
                                                   TrainingItemCode = x.TrainingItemCode,
                                                   Abstracts = x.Abstracts,
                                                   AttachUrl = x.AttachUrl,
                                                   TestType = x.TestType,
                                                   AItem = x.AItem,
                                                   BItem = x.BItem,
                                                   CItem = x.CItem,
                                                   DItem = x.DItem,
                                                   EItem = x.EItem,
                                                   AnswerItems = x.AnswerItems,
                                                   Score = x.TestType == "1" ? getTestPlan.SValue : (x.TestType == "2" ? getTestPlan.MValue : getTestPlan.JValue),
                                               };

                                db.Test_TestRecordItem.InsertAllOnSubmit(getItems);                               
                                getTestRecord.TestStartTime = DateTime.Now;
                                db.SubmitChanges();
                            }
                        }
                    }
                }
            }
            return testRecordId;
        }
        #endregion        

        #region 根据TestRecordId获取试卷题目列表
        /// <summary>
        /// 根据TestRecordId获取试卷题目列表
        /// </summary>
        /// <param name="testPlanId"></param>
        /// <returns>考试人员</returns>
        public static List<Model.TestRecordItemItem> geTestRecordItemListByTestRecordId(string testRecordId)
        {
            var getDataLists = (from x in new Model.SGGLDB(Funs.ConnString).Test_TestRecordItem
                                where x.TestRecordId == testRecordId
                                orderby x.TestType, x.TrainingItemCode
                                select new Model.TestRecordItemItem
                                {
                                    TestRecordItemId = x.TestRecordItemId,
                                    TestRecordId = x.TestRecordId,
                                    TrainingItemCode = x.TrainingItemCode,                                 
                                    Abstracts = x.Abstracts,
                                    AttachUrl = x.AttachUrl.Replace("\\", "/") ?? "",
                                    TestType = x.TestType,
                                    TestTypeName = x.TestType == "1" ? "单选题" : (x.TestType == "2" ? "多选题" : "判断题"),
                                    AItem = x.AItem ?? "",
                                    BItem = x.BItem ?? "",
                                    CItem = x.CItem ?? "",
                                    DItem = x.DItem ?? "",
                                    EItem = x.EItem ?? "",
                                    AnswerItems = x.AnswerItems ?? "",
                                    Score = x.Score ?? 0,
                                    SubjectScore = x.SubjectScore ?? 0,
                                    SelectedItem = x.SelectedItem ?? "",
                                }).ToList();
            return getDataLists;
        }
        #endregion

        #region 根据TestRecordItemId、AnswerItems 考生答题
        /// <summary>
        /// 根据TestRecordItemId、AnswerItems 考生答题
        /// </summary>
        /// <param name="testRecordItemId"></param>
        /// <param name="answerItems"></param>
        public static void getTestRecordItemAnswerBySelectedItem(Model.Test_TestRecordItem getTItemT, string selectedItem)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var getTItem = db.Test_TestRecordItem.FirstOrDefault(x => x.TestRecordItemId == getTItemT.TestRecordItemId);
                if (getTItem != null)
                {
                    getTItem.SubjectScore = 0;
                    getTItem.SelectedItem = selectedItem;
                    if (!string.IsNullOrEmpty(selectedItem))
                    {
                        if (getTItem.AnswerItems == selectedItem)
                        {
                            getTItem.SubjectScore = getTItem.Score ?? 0;
                        }
                        else
                        {
                            var listA = Funs.GetStrListByStr(getTItem.AnswerItems.ToUpper(), ',');
                            var listS = Funs.GetStrListByStr(selectedItem.ToUpper(), ',');
                            if (getTItem.TestType == "2" && listA.Count >= listS.Count)
                            {
                                int i = 0;
                                foreach (var item in listS)
                                {
                                    if (!listA.Contains(item))
                                    {
                                        i++;
                                        break;
                                    }
                                }
                                if (i == 0)
                                {
                                    if (listA.Count == listS.Count)
                                    {
                                        getTItem.SubjectScore = getTItem.Score ?? 0;
                                    }
                                    else
                                    {
                                        getTItem.SubjectScore = Convert.ToDecimal((getTItem.Score ?? 0) * 1.0 / 2);
                                    }
                                }
                            }
                        }
                    }
                    db.SubmitChanges();
                }
            }
        }
        #endregion

        #region 根据TestRecordId 提交试卷
        /// <summary>
        /// 根据TestRecordId 提交试卷
        /// </summary>
        /// <param name="testRecordId"></param>
        public static decimal getSubmitTestRecord(string testRecordId)
        {
            decimal getCode = 0;
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var getTestRecord = db.Test_TestRecord.FirstOrDefault(e => e.TestRecordId == testRecordId);
                /////试卷
                if (getTestRecord.TestStartTime.HasValue)
                {
                    getTestRecord.TestEndTime = DateTime.Now;
                    var getRItem = db.Test_TestRecordItem.Where(x => x.TestRecordId == getTestRecord.TestRecordId);
                    if (getRItem.Count() > 0)
                    {
                        getTestRecord.TestScores = getRItem.Sum(x => x.SubjectScore ?? 0);
                    }
                    db.SubmitChanges();

                    getCode = getTestRecord.TestScores ?? 0;

                }
            }
            return getCode;
        }
        #endregion

    }
}