using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 知识竞赛计划题目类型
    /// </summary>
    public static class ServerTestPlanTrainingService
    {
        public static Model.SGGLDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取知识竞赛计划
        /// </summary>
        /// <param name="TestPlanTrainingId"></param>
        /// <returns></returns>
        public static Model.Test_TestPlanTraining GetTestPlanTrainingById(string TestPlanTrainingId)
        {
            return Funs.DB.Test_TestPlanTraining.FirstOrDefault(e => e.TestPlanTrainingId == TestPlanTrainingId);
        }

        /// <summary>
        /// 添加知识竞赛计划
        /// </summary>
        /// <param name="TestPlanTraining"></param>
        public static void AddTestPlanTraining(Model.Test_TestPlanTraining TestPlanTraining)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Test_TestPlanTraining newTestPlanTraining = new Model.Test_TestPlanTraining
            {
                TestPlanTrainingId = TestPlanTraining.TestPlanTrainingId,
                TestPlanId = TestPlanTraining.TestPlanId,
                TrainingId = TestPlanTraining.TrainingId,
                TestType1Count = TestPlanTraining.TestType1Count,
                TestType2Count = TestPlanTraining.TestType2Count,
                TestType3Count = TestPlanTraining.TestType3Count,
                UserType = TestPlanTraining.UserType,
            };
            db.Test_TestPlanTraining.InsertOnSubmit(newTestPlanTraining);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改知识竞赛计划
        /// </summary>
        /// <param name="TestPlanTraining"></param>
        public static void UpdateTestPlanTraining(Model.Test_TestPlanTraining TestPlanTraining)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Test_TestPlanTraining newTestPlanTraining = db.Test_TestPlanTraining.FirstOrDefault(e => e.TestPlanTrainingId == TestPlanTraining.TestPlanTrainingId);
            if (newTestPlanTraining != null)
            {
                newTestPlanTraining.TrainingId = TestPlanTraining.TrainingId;
                newTestPlanTraining.TestType1Count = TestPlanTraining.TestType1Count;
                newTestPlanTraining.TestType2Count = TestPlanTraining.TestType2Count;
                newTestPlanTraining.TestType3Count = TestPlanTraining.TestType3Count;
                newTestPlanTraining.UserType = TestPlanTraining.UserType;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除知识竞赛计划
        /// </summary>
        /// <param name="TestPlanTrainingId"></param>
        public static void DeleteTestPlanTrainingById(string TestPlanTrainingId)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Test_TestPlanTraining TestPlanTraining = db.Test_TestPlanTraining.FirstOrDefault(e => e.TestPlanTrainingId == TestPlanTrainingId);
            if (TestPlanTraining != null)
            { 
                db.Test_TestPlanTraining.DeleteOnSubmit(TestPlanTraining);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除知识竞赛计划
        /// </summary>
        /// <param name="TestPlanTrainingId"></param>
        public static void DeleteTestPlanTrainingByTestPlanId(string TestPlanId)
        {
            Model.SGGLDB db = Funs.DB;
            var TestPlanTraining = from x in db.Test_TestPlanTraining where x.TestPlanId == TestPlanId select x;
            if (TestPlanTraining.Count() > 0)
            {
                db.Test_TestPlanTraining.DeleteAllOnSubmit(TestPlanTraining);
                db.SubmitChanges();
            }
        }
    }
}
