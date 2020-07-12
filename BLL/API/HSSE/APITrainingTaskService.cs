using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    public static class APITrainingTaskService
    {
        #region 根据ProjectId、PersonId获取培训任务教材明细列表
        /// <summary>
        /// 根据ProjectId、PersonId获取培训任务教材明细列表
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="personId"></param>
        /// <param name="states">1-培训中；2-已完成</param>
        /// <returns></returns>
        public static List<Model.TrainingTaskItem> getTrainingTaskListByProjectIdPersonId(string projectId, string personId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                personId = PersonService.GetPersonIdByUserId(personId);
                var getDataLists = (from x in db.Training_Task
                                    join y in db.Training_Plan on x.PlanId equals y.PlanId
                                    where x.ProjectId == projectId && x.UserId == personId && y.States != "0"
                                    orderby x.TaskDate descending
                                    select new Model.TrainingTaskItem
                                    {
                                        TaskId = x.TaskId,
                                        //PlanId = x.PlanId,
                                        PlanCode = y.PlanCode,
                                        PlanName = y.PlanName,
                                        TrainStartDate = string.Format("{0:yyyy-MM-dd HH:mm}", y.TrainStartDate),
                                        TeachAddress = y.TeachAddress,
                                        //PersonId = x.UserId,
                                        PersonName = db.SitePerson_Person.FirstOrDefault(p => p.PersonId == x.UserId).PersonName,
                                        TaskDate = string.Format("{0:yyyy-MM-dd HH:mm}", x.TaskDate),
                                        TrainTypeName = db.Base_TrainType.FirstOrDefault(b => b.TrainTypeId == y.TrainTypeId).TrainTypeName,
                                        TrainLevelName = db.Base_TrainLevel.FirstOrDefault(b => b.TrainLevelId == y.TrainLevelId).TrainLevelName,
                                        PlanStatesName = y.States == "3" ? "已完成" : "培训中",
                                    }).ToList();
                return getDataLists;
            }
        }
        #endregion

        #region 根据TaskId获取培训任务教材明细列表
        /// <summary>
        /// 根据TaskId获取培训任务列表
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        public static List<Model.TrainingTaskItemItem> getTrainingTaskItemListByTaskId(string taskId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                ////生成培训任务下培训明细
                GetDataService.CreateTrainingTaskItemByTaskId(taskId);

                var getDataLists = (from x in db.Training_TaskItem
                                    where x.TaskId == taskId
                                    orderby x.TrainingItemCode
                                    select new Model.TrainingTaskItemItem
                                    {
                                        TaskItemId = x.TaskItemId,
                                        TaskId = x.TaskId,
                                        PlanId = x.PlanId,
                                        PersonId = x.PersonId,
                                        TrainingItemCode = x.TrainingItemCode,
                                        TrainingItemName = x.TrainingItemName,
                                        AttachUrl = x.AttachUrl.Replace('\\', '/'),
                                    }).ToList();
                return getDataLists;
            }
        }
        #endregion

        #region 根据PlanId、PersonId将人员加入培训任务条件
        /// <summary>
        /// 根据PlanId、PersonId将人员加入培训任务条件
        /// </summary>
        /// <param name="planId">培训计划ID</param>
        /// <param name="personId">人员ID</param>
        /// <returns></returns>
        public static string getTrainingTaskByPlanIdPersonIdCondition(string planId, string personId)
        {
            string alterString = string.Empty;
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var task = db.Training_Task.FirstOrDefault(x => x.PlanId == planId && x.UserId == personId);
                if (task != null)
                {
                    alterString = "人员已在培训任务中！";
                }
                else
                {
                    var plan = db.Training_Plan.FirstOrDefault(e => e.PlanId == planId);
                    if (plan != null)
                    {
                        if (plan.States != "0" && plan.States != "1")
                        {
                            alterString = "当前培训不能再加入人员!";
                        }
                        else
                        {
                            var person = db.SitePerson_Person.FirstOrDefault(e => e.PersonId == personId);
                            if (person != null && plan.ProjectId == person.ProjectId && plan.UnitIds.Contains(person.UnitId)
                                    && (string.IsNullOrEmpty(plan.WorkPostId) || plan.WorkPostId.Contains(person.WorkPostId)))
                            {
                                var trainType = db.Base_TrainType.FirstOrDefault(e => e.TrainTypeId == plan.TrainTypeId); 
                                if (trainType != null)
                                {
                                    if (!trainType.IsRepeat.HasValue || trainType.IsRepeat == false)
                                    {
                                        var trainRecord = (from x in db.EduTrain_TrainRecord
                                                           join y in db.EduTrain_TrainRecordDetail on x.TrainingId equals y.TrainingId
                                                           where x.TrainTypeId == plan.TrainTypeId && x.ProjectId == person.ProjectId
                                                           && y.PersonId == personId && y.CheckResult == true
                                                           select x).FirstOrDefault();
                                        if (trainRecord != null)
                                        {
                                            alterString = "人员已参加过当前类型的培训!";
                                        }
                                        else
                                        {
                                            var getTask = (from x in db.Training_Plan
                                                           join y in db.Training_Task on x.PlanId equals y.PlanId
                                                           where x.TrainTypeId == plan.TrainTypeId && y.UserId == personId && x.States != "3"
                                                           select x).FirstOrDefault();
                                            if (getTask != null)
                                            {
                                                alterString = "人员已再同培训类型的培训中!";
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    alterString = "当前培训类型有问题!";
                                }
                            }
                            else
                            {
                                alterString = "人员与培训计划不匹配!";
                            }
                        }
                    }
                    else
                    {
                        alterString = "当前培训计划不允许再增加人员!";
                    }
                }
            }
            return alterString;
        }
        #endregion

        #region 根据PlanId、PersonId将人员加入培训任务
        /// <summary>
        /// 根据PlanId、PersonId将人员加入培训任务
        /// </summary>
        /// <param name="planId">培训计划ID</param>
        /// <param name="personId">人员ID</param>
        /// <returns></returns>
        public static void getTrainingTaskByPlanIdPersonId(string planId, string personId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var plan = db.Training_Plan.FirstOrDefault(e => e.PlanId == planId);
                Model.Training_Task newTask = new Model.Training_Task
                {
                    TaskId = SQLHelper.GetNewID(),
                    PlanId = planId,
                    UserId = personId,
                    TaskDate = DateTime.Now,
                    States = "0",
                };
                if (plan != null)
                {
                    newTask.ProjectId = plan.ProjectId;
                    db.Training_Task.InsertOnSubmit(newTask);
                    db.SubmitChanges();

                    ////生成培训任务下培训明细
                    GetDataService.CreateTrainingTaskItemByTaskId(newTask.TaskId);
                }
            }
        }
        #endregion

        #region 根据TrainingPlanId获取培训任务教材明细列表
        /// <summary>
        /// 根据TrainingPlanId获取培训任务教材明细列表
        /// </summary>
        /// <param name="trainingPlanId"></param>
        /// <returns>培训计划人员</returns>
        public static List<Model.TrainingTaskItem> getTrainingTaskListByTrainingPlanId(string trainingPlanId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var getDataLists = (from x in db.Training_Task
                                    where x.PlanId == trainingPlanId
                                    orderby x.TaskDate descending
                                    select new Model.TrainingTaskItem
                                    {
                                        TaskId = x.TaskId,
                                        PlanId = x.PlanId,
                                        PersonId = x.UserId,
                                        PersonName = db.SitePerson_Person.FirstOrDefault(p => p.PersonId == x.UserId).PersonName,
                                        TaskDate = string.Format("{0:yyyy-MM-dd HH:mm}", x.TaskDate),
                                        States = x.States,
                                    }).ToList();
                return getDataLists;
            }
        }
        #endregion
    }
}
