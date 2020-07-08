using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    /// <summary>
    /// 培训记录试卷
    /// </summary>
    public static class EduTrain_TrainTestService
    {
        public static Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);

        /// <summary>
        /// 根据教育培训主键获取所有的教育培训明细信息
        /// </summary>
        /// <param name="trainingId">教育培训主键</param>
        /// <returns>教育培训明细信息</returns>
        public static List<Model.EduTrain_TrainTest> GetTrainTestByTrainingId(string trainingId)
        {
            return (from x in new Model.SGGLDB(Funs.ConnString).EduTrain_TrainTest where x.TrainingId == trainingId select x).ToList();
        }

        /// <summary>
        /// 增加教育培训明细信息
        /// </summary>
        /// <param name="trainTest">教育培训明细信息实体</param>
        public static void AddTrainTest(Model.EduTrain_TrainTest trainTest)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.EduTrain_TrainTest newTrainTest = new Model.EduTrain_TrainTest
            {
                TrainTestId = trainTest.TrainTestId,
                TrainingId = trainTest.TrainingId,
                ExamNo = trainTest.ExamNo,
                GroupNo = trainTest.GroupNo,
                CourseID = trainTest.CourseID,
                COrder = trainTest.COrder,
                QsnCode = trainTest.QsnCode,
                QsnId = trainTest.QsnId,
                QsnContent = trainTest.QsnContent,
                QsnFileName = trainTest.QsnFileName,
                QsnAnswer = trainTest.QsnAnswer,
                QsnCategory = trainTest.QsnCategory,
                QsnKind = trainTest.QsnKind,
                Description = trainTest.Description,
                QsnImportant = trainTest.QsnImportant,
                Analysis = trainTest.Analysis,
                UploadTime = trainTest.UploadTime,
            };

            db.EduTrain_TrainTest.InsertOnSubmit(newTrainTest);
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据教育培训主键删除对应的所有教育培训明细信息
        /// </summary>
        /// <param name="trainingId">教育培训主键</param>
        public static void DeleteTrainTestByTrainingId(string trainingId)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            var q = (from x in db.EduTrain_TrainTest where x.TrainingId == trainingId select x).ToList();
            if (q.Count() > 0)
            {
                db.EduTrain_TrainTest.DeleteAllOnSubmit(q);
                db.SubmitChanges();
            }
        }
    }
}
