using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    /// <summary>
    /// 考试计划
    /// </summary>
    public static class TestPlanService
    {
        public static Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);

        /// <summary>
        /// 根据主键获取培训计划
        /// </summary>
        /// <param name="TestPlanId"></param>
        /// <returns></returns>
        public static Model.Training_TestPlan GetTestPlanById(string TestPlanId)
        {
            return new Model.SGGLDB(Funs.ConnString).Training_TestPlan.FirstOrDefault(e => e.TestPlanId == TestPlanId);
        }

        /// <summary>
        /// 添加培训计划
        /// </summary>
        /// <param name="testPlan"></param>
        public static void AddTestPlan(Model.Training_TestPlan testPlan)
        {
            Model.Training_TestPlan newTestPlan = new Model.Training_TestPlan
            {
                TestPlanId = testPlan.TestPlanId,
                ProjectId = testPlan.ProjectId,
                PlanCode = testPlan.PlanCode,
                PlanName = testPlan.PlanName,
                PlanManId = testPlan.PlanManId,
                PlanDate = testPlan.PlanDate,
                TestStartTime = testPlan.TestStartTime,
                TestEndTime = testPlan.TestEndTime,
                Duration = testPlan.Duration,
                SValue = testPlan.SValue,
                MValue = testPlan.MValue,
                JValue = testPlan.JValue,
                TotalScore = testPlan.TotalScore,
                QuestionCount = testPlan.QuestionCount,
                TestPalce = testPlan.TestPalce,
                UnitIds = testPlan.UnitIds,
                UnitNames = testPlan.UnitNames,
                DepartIds = testPlan.DepartIds,
                DepartNames = testPlan.DepartNames,
                WorkPostIds = testPlan.WorkPostIds,
                WorkPostNames = testPlan.WorkPostNames,
                PlanId= testPlan.PlanId,
                States = testPlan.States
            };

            db.Training_TestPlan.InsertOnSubmit(newTestPlan);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改培训计划
        /// </summary>
        /// <param name="TestPlan"></param>
        public static void UpdateTestPlan(Model.Training_TestPlan TestPlan)
        {
            Model.Training_TestPlan newTestPlan = db.Training_TestPlan.FirstOrDefault(e => e.TestPlanId == TestPlan.TestPlanId);
            if (newTestPlan != null)
            {
                newTestPlan.PlanCode = TestPlan.PlanCode;
                newTestPlan.PlanName = TestPlan.PlanName;
                newTestPlan.PlanManId = TestPlan.PlanManId;
                newTestPlan.PlanDate = TestPlan.PlanDate;
                newTestPlan.TestStartTime = TestPlan.TestStartTime;
                newTestPlan.TestEndTime = TestPlan.TestEndTime;
                newTestPlan.Duration = TestPlan.Duration;
                newTestPlan.TotalScore = TestPlan.TotalScore;
                newTestPlan.QuestionCount = TestPlan.QuestionCount;
                newTestPlan.TestPalce = TestPlan.TestPalce;
                newTestPlan.UnitIds = TestPlan.UnitIds;
                newTestPlan.UnitNames = TestPlan.UnitNames;
                newTestPlan.DepartIds = TestPlan.DepartIds;
                newTestPlan.DepartNames = TestPlan.DepartNames;
                newTestPlan.WorkPostIds = TestPlan.WorkPostIds;
                newTestPlan.WorkPostNames = TestPlan.WorkPostNames;
                newTestPlan.States = TestPlan.States;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除培训计划信息
        /// </summary>
        /// <param name="TestPlanId"></param>
        public static void DeleteTestPlanById(string TestPlanId)
        {

            var getTestPlan = GetTestPlanById(TestPlanId);
            if (getTestPlan != null)
            {
                var testPlanTrainings = from x in new Model.SGGLDB(Funs.ConnString).Training_TestPlanTraining where x.TestPlanId == TestPlanId select x;
                if (testPlanTrainings.Count() > 0)
                {
                    new Model.SGGLDB(Funs.ConnString).Training_TestPlanTraining.DeleteAllOnSubmit(testPlanTrainings);
                    new Model.SGGLDB(Funs.ConnString).SubmitChanges();
                }
                if (getTestPlan.States == "3") //状态考试结束
                {
                    var updateTrainingPlan = TrainingPlanService.GetPlanById(getTestPlan.PlanId);
                    if (updateTrainingPlan != null)
                    {
                        updateTrainingPlan.States = "2";
                        TrainingPlanService.UpdatePlan(updateTrainingPlan);
                        var getTrainingTasks = from x in new Model.SGGLDB(Funs.ConnString).Training_Task
                                               where x.PlanId == updateTrainingPlan.PlanId 
                                               select x;
                        foreach (var item in getTrainingTasks)
                        {
                            item.States = "1";
                            TrainingTaskService.UpdateTask(item);
                        }
                        ////删除归档的培训记录
                        var trainRecord = new Model.SGGLDB(Funs.ConnString).EduTrain_TrainRecord.FirstOrDefault(x => x.PlanId == getTestPlan.PlanId);
                        if (trainRecord != null)
                        {
                            EduTrain_TrainRecordService.DeleteTrainingByTrainingId(trainRecord.TrainingId);
                        }
                    }
                }

                new Model.SGGLDB(Funs.ConnString).Training_TestPlan.DeleteOnSubmit(getTestPlan);
                new Model.SGGLDB(Funs.ConnString).SubmitChanges();              
            }
        }

        /// <summary>
        /// 获取培训计划列
        /// </summary>
        /// <returns></returns>
        public static List<Model.Training_TestPlan> GetTestPlanList()
        {
            return (from x in db.Training_TestPlan orderby x.PlanCode select x).ToList();
        }
    }
}
