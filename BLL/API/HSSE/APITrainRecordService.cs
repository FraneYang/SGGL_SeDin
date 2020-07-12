using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmitMapper;
using EmitMapper.MappingConfiguration;

namespace BLL
{
    public static class APITrainRecordService
    {
        #region 根据projectId、trainTypeId、TrainStates获取培训记录列表
        /// <summary>
        /// 根据projectId、trainTypeId、TrainStates获取培训记录列表
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public static List<Model.TrainRecordItem> getTrainRecordListByProjectIdTrainTypeIdTrainStates(string projectId, string trainTypeId, string trainStates)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var getDataLists = (from x in db.EduTrain_TrainRecord
                                    where x.ProjectId == projectId && x.TrainTypeId == trainTypeId
                                    orderby x.TrainStartDate descending
                                    select new Model.TrainRecordItem
                                    {
                                        TrainRecordId = x.TrainingId,
                                        TrainingCode = x.TrainingCode,
                                        TrainTitle = x.TrainTitle,
                                        ProjectId = x.ProjectId,
                                        TrainTypeId = x.TrainTypeId,
                                        TrainTypeName = db.Base_TrainType.First(y => y.TrainTypeId == x.TrainTypeId).TrainTypeName,
                                        TrainLevelId = x.TrainLevelId,
                                        TrainLevelName = db.Base_TrainLevel.First(y => y.TrainLevelId == x.TrainLevelId).TrainLevelName,
                                        TeachHour = x.TeachHour ?? 0,
                                        TeachAddress = x.TeachAddress,
                                        TrainStartDate = string.Format("{0:yyyy-MM-dd HH:mm}", x.TrainStartDate),
                                    }).ToList();
                return getDataLists;
            }
        }
        #endregion

        #region 根据培训ID获取培训记录详细
        /// <summary>
        /// 根据培训ID获取培训记录详细
        /// </summary>
        /// <param name="trainRecordId"></param>
        /// <returns></returns>
        public static Model.TrainRecordItem getTrainRecordByTrainingId(string trainRecordId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var getDataLists = from x in db.EduTrain_TrainRecord
                                   where x.TrainingId == trainRecordId
                                   select new Model.TrainRecordItem
                                   {
                                       TrainRecordId = x.TrainingId,
                                       TrainingCode = x.TrainingCode,
                                       TrainTitle = x.TrainTitle,
                                       ProjectId = x.ProjectId,
                                       TrainTypeId = x.TrainTypeId,
                                       TrainTypeName = db.Base_TrainType.First(y => y.TrainTypeId == x.TrainTypeId).TrainTypeName,
                                       TrainLevelId = x.TrainLevelId,
                                       TrainLevelName = db.Base_TrainLevel.First(y => y.TrainLevelId == x.TrainLevelId).TrainLevelName,
                                       TeachHour = x.TeachHour ?? 0,
                                       TeachAddress = x.TeachAddress,
                                       TrainStartDate = string.Format("{0:yyyy-MM-dd HH:mm}", x.TrainStartDate),
                                       UnitIds = x.UnitIds,
                                       WorkPostIds = x.WorkPostIds,
                                       TrainContent = x.TrainContent,
                                       AttachUrl = db.AttachFile.First(y => y.ToKeyId == x.TrainingId).AttachUrl.Replace('\\', '/'),
                                       UnitNames = UnitService.getUnitNamesUnitIds(x.UnitIds),
                                       WorkPostNames = WorkPostService.getWorkPostNamesWorkPostIds(x.WorkPostIds),
                                   };
                return getDataLists.FirstOrDefault();
            }
        }
        #endregion

        #region 根据考生计划结束时 将相关培训考生内容 写培训记录归档
        /// <summary>
        /// 根据考生计划结束时 将相关培训考生内容 写培训记录归档
        /// </summary>
        /// <param name="getTestPlan"></param>

        public static void InsertTrainRecord(Model.Training_TestPlan getTestPlan)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                ////获取培训计划
                var getTrainingPlan = db.Training_Plan.FirstOrDefault(e => e.PlanId == getTestPlan.PlanId);
                var getTrainRecord = db.EduTrain_TrainRecord.FirstOrDefault(x => x.PlanId == getTestPlan.PlanId);
                if (getTrainingPlan != null && getTrainRecord == null)
                {
                    getTrainingPlan.States = "3";
                    db.SubmitChanges();

                    Model.EduTrain_TrainRecord newTrainRecord = new Model.EduTrain_TrainRecord
                    {
                        TrainingId = SQLHelper.GetNewID(),
                        TrainingCode = getTrainingPlan.PlanCode,
                        ProjectId = getTrainingPlan.ProjectId,
                        TrainTitle = getTrainingPlan.PlanName,
                        TrainContent = getTrainingPlan.TrainContent,
                        TrainStartDate = getTrainingPlan.TrainStartDate,
                        TeachHour = getTrainingPlan.TeachHour,
                        TrainEndDate = getTrainingPlan.TrainEndDate,
                        TeachMan = getTrainingPlan.TeachMan,
                        TeachAddress = getTrainingPlan.TeachAddress,
                        Remark = "来源：培训计划",
                        TrainTypeId = getTrainingPlan.TrainTypeId,
                        TrainLevelId = getTrainingPlan.TrainLevelId,
                        UnitIds = getTrainingPlan.UnitIds,
                        States = Const.State_2,
                        WorkPostIds = getTrainingPlan.WorkPostId,
                        PlanId = getTrainingPlan.PlanId,
                    };
                    newTrainRecord.CompileMan = UserService.GetUserNameByUserId(getTrainingPlan.DesignerId);
                    ///获取培训人员
                    var getTrainingTasks = from x in db.Training_Task where x.PlanId == getTrainingPlan.PlanId
                                           select x;
                    newTrainRecord.TrainPersonNum = getTrainingTasks.Count();
                    ///新增培训记录
                    db.EduTrain_TrainRecord.InsertOnSubmit(newTrainRecord);
                    db.SubmitChanges();

                    ////及格分数                       
                    int passScore = 80;
                    var testRule = db.Sys_TestRule.FirstOrDefault();
                    if (testRule != null)
                    {
                        passScore = testRule.PassingScore;
                    }

                    foreach (var item in getTrainingTasks)
                    {
                        decimal gScores = 0;
                        bool result = false;
                        ////获取 考生试卷
                        var getTestRecord = db.Training_TestRecord.Where(x => x.TestPlanId == getTestPlan.TestPlanId && x.TestManId == item.UserId);
                       foreach(var itemR in getTestRecord)
                        {
                            if (itemR.TestScores > gScores)
                            {
                                gScores = itemR.TestScores ?? 0;
                            } 
                        }

                        if (gScores >= passScore)
                        {
                            result = true;
                        }

                        Model.EduTrain_TrainRecordDetail newDetail = new Model.EduTrain_TrainRecordDetail
                        {
                            TrainDetailId = SQLHelper.GetNewID(),
                            TrainingId = newTrainRecord.TrainingId,
                            PersonId = item.UserId,
                            CheckScore= gScores,
                            CheckResult=result,
                        };
                        db.EduTrain_TrainRecordDetail.InsertOnSubmit(newDetail);
                        db.SubmitChanges();
                     
                        ///// 培训考试 通过 更新卡号
                        if (result)
                        {
                            var getPerson = db.SitePerson_Person.FirstOrDefault(e => e.PersonId == newDetail.PersonId);
                            if (getPerson != null && string.IsNullOrEmpty(getPerson.CardNo))
                            {
                                getPerson.CardNo = SQLHelper.RunProcNewId("SpGetNewNumber", "SitePerson_Person", "CardNo", getPerson.ProjectId, UnitService.GetUnitCodeByUnitId(getPerson.UnitId));
                                db.SubmitChanges();
                            }
                        }
                    }

                    ////增加一条编码记录
                    CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(Const.ProjectTrainRecordMenuId, newTrainRecord.ProjectId, null, newTrainRecord.TrainingId, newTrainRecord.TrainStartDate);
                    CommonService.btnSaveData(newTrainRecord.ProjectId, Const.ProjectTrainRecordMenuId, newTrainRecord.TrainingId, getTrainingPlan.DesignerId, true, newTrainRecord.TrainTitle, "../EduTrain/TrainRecordView.aspx?TrainingId={0}");
                }
            }
        }
        #endregion
    }
}
