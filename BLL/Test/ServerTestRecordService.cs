using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 考试记录
    /// </summary>
    public static class ServerTestRecordService
    {
        public static Model.SGGLDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取考试记录
        /// </summary>
        /// <param name="testRecordId"></param>
        /// <returns></returns>
        public static Model.Test_TestRecord GetTestRecordById(string testRecordId)
        {
            return Funs.DB.Test_TestRecord.FirstOrDefault(e => e.TestRecordId == testRecordId);
        }
        
        /// <summary>
        /// 新增考生记录信息
        /// </summary>
        /// <param name="Training"></param>
        public static void AddTestRecord(Model.Test_TestRecord testRecord)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Test_TestRecord newTestRecord = new Model.Test_TestRecord
            {
                TestRecordId = testRecord.TestRecordId,
                ProjectId = testRecord.ProjectId,
                TestPlanId = testRecord.TestPlanId,
                TestManId = testRecord.TestManId,
            };

            db.Test_TestRecord.InsertOnSubmit(newTestRecord);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改考试记录信息
        /// </summary>
        /// <param name="Training"></param>
        public static void UpdateTestRecord(Model.Test_TestRecord testRecord)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Test_TestRecord newTestRecord = db.Test_TestRecord.FirstOrDefault(e => e.TestRecordId == testRecord.TestRecordId);
            if (newTestRecord != null)
            {
                newTestRecord.TestScores = testRecord.TestScores;
                newTestRecord.TestEndTime = testRecord.TestEndTime;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据计划主键删除考试人员信息
        /// </summary>
        /// <param name="planId"></param>
        public static void DeleteTestRecordByTestPlanId(string testPlanId)
        {
            var deleteRecords = from x in Funs.DB.Test_TestRecord where x.TestPlanId == testPlanId select x;
            if (deleteRecords.Count() > 0)
            {
                foreach (var item in deleteRecords)
                {
                    DeleteTestRecordByTestRecordId(item.TestRecordId);
                }
            }
        }

        /// <summary>
        /// 根据考生主键删除考生信息
        /// </summary>
        /// <param name="planId"></param>
        public static void DeleteTestRecordByTestRecordId(string testRecordId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var testRecord = db.Test_TestRecord.FirstOrDefault(e => e.TestRecordId == testRecordId);
                if (testRecord != null)
                {
                    var testRecordItem = from x in db.Test_TestRecordItem where x.TestRecordId == testRecordId select x;
                    if (testRecordItem.Count() > 0)
                    {
                        db.Test_TestRecordItem.DeleteAllOnSubmit(testRecordItem);
                        db.SubmitChanges();
                    }

                    db.Test_TestRecord.DeleteOnSubmit(testRecord);
                    db.SubmitChanges();
                }
            }
        }

        /// <summary>
        /// 更新没有结束时间且超时的考试记录
        /// </summary>
        public static int UpdateTestEndTimeNull(string testRecordId)
        {
            int icount = 0;
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var testRecord = from x in db.Test_TestRecord
                                 where !x.TestEndTime.HasValue && x.TestStartTime.HasValue
                                 && x.TestStartTime.Value.AddMinutes(x.Duration.Value) < DateTime.Now
                                 && x.TestRecordId == testRecordId
                                 select x;
                if (testRecord.Count() > 0)
                {
                    foreach (var item in testRecord)
                    {
                        item.TestEndTime = item.TestStartTime.Value.AddMinutes(item.Duration.Value);
                        item.TestScores = db.Test_TestRecordItem.Where(x => x.TestRecordId == item.TestRecordId).Sum(x => x.SubjectScore) ?? 0;
                        db.SubmitChanges();
                        icount++;
                    }
                }
            }
            return icount;
        }
    }
}
