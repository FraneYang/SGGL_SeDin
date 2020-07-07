using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    /// <summary>
    /// 培训记录明细
    /// </summary>
    public static class EduTrain_TrainRecordDetailService
    {
        public static Model.SGGLDB db = Funs.DB;

        /// <summary>
        /// 根据教育培训主键获取所有的教育培训明细信息
        /// </summary>
        /// <param name="trainingId">教育培训主键</param>
        /// <returns>教育培训明细信息</returns>
        public static List<Model.EduTrain_TrainRecordDetail> GetTrainRecordDetailByTrainingId(string trainingId)
        {
            return (from x in Funs.DB.EduTrain_TrainRecordDetail where x.TrainingId == trainingId select x).ToList();
        }

        public static Model.EduTrain_TrainRecordDetail GetTrainDetailByTrainDetailId(string trainDetailId)
        {
            return Funs.DB.EduTrain_TrainRecordDetail.FirstOrDefault(x => x.TrainDetailId == trainDetailId);
        }

        /// <summary>
        /// 增加教育培训明细信息
        /// </summary>
        /// <param name="trainDetail">教育培训明细信息实体</param>
        public static void AddTrainDetail(Model.EduTrain_TrainRecordDetail trainDetail)
        {
            Model.SGGLDB db = Funs.DB;
            string newKeyID = SQLHelper.GetNewID(typeof(Model.EduTrain_TrainRecordDetail));
            Model.EduTrain_TrainRecordDetail newTrainDetail = new Model.EduTrain_TrainRecordDetail
            {
                TrainDetailId = newKeyID,
                TrainingId = trainDetail.TrainingId,
                PersonId = trainDetail.PersonId,
                CheckScore = trainDetail.CheckScore,
                CheckResult = trainDetail.CheckResult
            };

            db.EduTrain_TrainRecordDetail.InsertOnSubmit(newTrainDetail);
            db.SubmitChanges();

            var rainRecord = EduTrain_TrainRecordService.GetTrainingByTrainingId(trainDetail.TrainingId);
            if (rainRecord != null)
            {
                rainRecord.TrainPersonNum += 1;
                EduTrain_TrainRecordService.UpdateTraining(rainRecord);                
            }
            ///// 培训考试 通过 更新卡号
            if (newTrainDetail.CheckResult)
            {
                var getPerson = PersonService.GetPersonById(trainDetail.PersonId);
                if (getPerson != null && string.IsNullOrEmpty(getPerson.CardNo))
                {
                    getPerson.CardNo = SQLHelper.RunProcNewId("SpGetNewNumber", "SitePerson_Person", "CardNo", rainRecord.ProjectId, UnitService.GetUnitCodeByUnitId(getPerson.UnitId));
                }
            }
        }

        /// <summary>
        /// 修改教育培训明细信息
        /// </summary>
        /// <param name="trainDetail">教育培训明细信息</param>
        public static void UpdateTrainDetail(Model.EduTrain_TrainRecordDetail trainDetail)
        {
            Model.SGGLDB db = Funs.DB;
            Model.EduTrain_TrainRecordDetail newTrainRecordDetail = db.EduTrain_TrainRecordDetail.FirstOrDefault(e => e.TrainDetailId == trainDetail.TrainDetailId);
            if (newTrainRecordDetail != null)
            {
                newTrainRecordDetail.CheckResult = trainDetail.CheckResult;
                newTrainRecordDetail.CheckScore = trainDetail.CheckScore;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据教育培训主键删除对应的所有教育培训明细信息
        /// </summary>
        /// <param name="trainingId">教育培训主键</param>
        public static void DeleteTrainDetailByTrainingId(string trainingId)
        {
            Model.SGGLDB db = Funs.DB;
            var q = (from x in db.EduTrain_TrainRecordDetail where x.TrainingId == trainingId select x).ToList();
            if (q.Count() > 0)
            {
                db.EduTrain_TrainRecordDetail.DeleteAllOnSubmit(q);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除培训记录
        /// </summary>
        /// <param name="trainingId"></param>
        public static void DeleteTrainDetailByTrainDetail(string trainDetailId)
        {
            Model.SGGLDB db = Funs.DB;
            Model.EduTrain_TrainRecordDetail trainDetails = Funs.DB.EduTrain_TrainRecordDetail.FirstOrDefault(e => e.TrainDetailId == trainDetailId);
            if (trainDetails != null)
            {
                db.EduTrain_TrainRecordDetail.DeleteOnSubmit(trainDetails);
                db.SubmitChanges();

                var rainRecord = EduTrain_TrainRecordService.GetTrainingByTrainingId(trainDetails.TrainingId);
                if (rainRecord != null)
                {
                    rainRecord.TrainPersonNum -= 1;
                    BLL.EduTrain_TrainRecordService.UpdateTraining(rainRecord);
                }
            }
        }

        public static Model.EduTrain_TrainRecordDetail GetTrainDetailByPersonIdTrainingId(string trainingId, string personId)
        {
            return Funs.DB.EduTrain_TrainRecordDetail.FirstOrDefault(e => e.TrainingId == trainingId && e.PersonId == personId);
        }
    }
}
