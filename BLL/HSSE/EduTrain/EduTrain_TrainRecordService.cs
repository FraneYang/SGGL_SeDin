using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace BLL
{
    /// <summary>
    /// 培训记录
    /// </summary>
    public static class EduTrain_TrainRecordService
    {
        public static Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);

        /// <summary>
        /// 根据教育培训主键获取教育培训信息
        /// </summary>
        /// <param name="trainingId">教育培训主键</param>
        /// <returns>教育培训信息</returns>
        public static Model.EduTrain_TrainRecord GetTrainingByTrainingId(string trainingId)
        {
            return (from x in new Model.SGGLDB(Funs.ConnString).EduTrain_TrainRecord where x.TrainingId == trainingId select x).FirstOrDefault();
        }

        /// <summary>
        /// 增加教育培训信息
        /// </summary>
        /// <param name="training">教育培训实体</param>
        public static void AddTraining(Model.EduTrain_TrainRecord training)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.EduTrain_TrainRecord newTraining = new Model.EduTrain_TrainRecord
            {
                TrainingId = training.TrainingId,
                TrainingCode = training.TrainingCode,
                ProjectId = training.ProjectId,
                TrainTitle = training.TrainTitle,
                TrainContent = training.TrainContent,
                TrainStartDate = training.TrainStartDate,
                TeachHour = training.TeachHour,
                TeachMan = training.TeachMan,
                TeachAddress = training.TeachAddress,
                Remark = training.Remark,
                AttachUrl = training.AttachUrl,
                TrainTypeId = training.TrainTypeId,
                TrainLevelId = training.TrainLevelId,
                UnitIds = training.UnitIds,
                States = training.States,
                CompileMan = training.CompileMan,
                TrainPersonNum = training.TrainPersonNum,
                FromRecordId = training.FromRecordId,
                WorkPostIds = training.WorkPostIds,
                PlanId = training.PlanId,
            };

            if (training.TrainEndDate.HasValue)
            {
                newTraining.TrainEndDate = training.TrainEndDate;
            }
            else
            {
                newTraining.TrainEndDate = training.TrainStartDate;
            }
            
            db.EduTrain_TrainRecord.InsertOnSubmit(newTraining);
            db.SubmitChanges();
            ////增加一条编码记录
            BLL.CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(Const.ProjectTrainRecordMenuId, training.ProjectId, null, training.TrainingId, training.TrainStartDate);
        }

        /// <summary>
        /// 修改教育培训信息
        /// </summary>
        /// <param name="training">教育培训实体</param>
        public static void UpdateTraining(Model.EduTrain_TrainRecord training)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.EduTrain_TrainRecord newTraining = db.EduTrain_TrainRecord.FirstOrDefault(e => e.TrainingId == training.TrainingId);
            if (newTraining != null)
            {
                newTraining.TrainingCode = training.TrainingCode;
                newTraining.TrainTitle = training.TrainTitle;
                newTraining.TrainContent = training.TrainContent;
                newTraining.TrainStartDate = training.TrainStartDate;
                if (training.TrainEndDate.HasValue)
                {
                    newTraining.TrainEndDate = training.TrainEndDate;
                }
                else
                {
                    newTraining.TrainEndDate = training.TrainStartDate;
                }
                newTraining.TeachHour = training.TeachHour;
                newTraining.TeachMan = training.TeachMan;
                newTraining.TeachAddress = training.TeachAddress;
                newTraining.Remark = training.Remark;
                newTraining.AttachUrl = training.AttachUrl;
                newTraining.TrainTypeId = training.TrainTypeId;
                newTraining.TrainLevelId = training.TrainLevelId;
                newTraining.UnitIds = training.UnitIds;
                newTraining.States = training.States;
                newTraining.TrainPersonNum = training.TrainPersonNum;
                newTraining.FromRecordId = training.FromRecordId;
                newTraining.WorkPostIds = training.WorkPostIds;
                db.SubmitChanges();
            }            
        }

        /// <summary>
        /// 根据教育培训主键删除一个教育培训信息
        /// </summary>
        /// <param name="trainingId">教育培训主键</param>
        public static void DeleteTrainingByTrainingId(string trainingId)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.EduTrain_TrainRecord training = db.EduTrain_TrainRecord.FirstOrDefault(e => e.TrainingId == trainingId);
            if (training != null)
            {
                ///删除培训明细
                EduTrain_TrainRecordDetailService.DeleteTrainDetailByTrainingId(trainingId);
                ///删除编码表记录
                CodeRecordsService.DeleteCodeRecordsByDataId(training.TrainingId);
                ////删除附件表
                CommonService.DeleteAttachFileById(training.TrainingId);
                ////删除流程表
                BLL.CommonService.DeleteFlowOperateByID(training.TrainingId);
                ///删除培训试卷
                EduTrain_TrainTestService.DeleteTrainTestByTrainingId(training.TrainingId);
                db.EduTrain_TrainRecord.DeleteOnSubmit(training);
                db.SubmitChanges();
            }
        }

        public static List<Model.EduTrain_TrainRecord> GetTrainingsByTrainDate(DateTime startTime, DateTime endTime, string projectId)
        {
            return (from x in new Model.SGGLDB(Funs.ConnString).EduTrain_TrainRecord where x.TrainStartDate >= startTime && x.TrainStartDate < endTime && x.ProjectId == projectId select x).ToList();
        }

        public static int GetCountByTrainDate(DateTime startTime, DateTime endTime, string projectId)
        {
            return (from x in new Model.SGGLDB(Funs.ConnString).EduTrain_TrainRecord where x.TrainStartDate >= startTime && x.TrainStartDate < endTime && x.ProjectId == projectId select x).Count();
        }

        public static List<Model.EduTrain_TrainRecord> GetTrainingsByTrainType(DateTime time, string projectId)
        {
            return (from x in new Model.SGGLDB(Funs.ConnString).EduTrain_TrainRecord where x.TrainStartDate < time && x.ProjectId == projectId select x).ToList();
        }

        public static int GetCount(DateTime time, string projectId)
        {
            return (from x in new Model.SGGLDB(Funs.ConnString).EduTrain_TrainRecord where x.TrainStartDate < time && x.ProjectId == projectId select x).Count();
        }
    }
}
