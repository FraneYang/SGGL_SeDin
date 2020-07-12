using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmitMapper;
using EmitMapper.MappingConfiguration;

namespace BLL
{
    public static class APITrainingPlanService
    {
        #region 根据projectId、trainTypeId、TrainStates获取培训计划列表
        /// <summary>
        /// 根据projectId、trainTypeId、TrainStates获取培训计划列表
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public static List<Model.TrainingPlanItem> getTrainingPlanListByProjectIdTrainTypeIdTrainStates(string projectId, string trainTypeId, string states)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var getDataLists = (from x in db.Training_Plan
                                    where x.ProjectId == projectId && (x.States == states || states == null) && x.TrainTypeId == trainTypeId
                                    orderby x.TrainStartDate descending
                                    select new Model.TrainingPlanItem
                                    {
                                        PlanId = x.PlanId,
                                        PlanCode = x.PlanCode,
                                        PlanName = x.PlanName,
                                        ProjectId = x.ProjectId,
                                        TrainTypeId = x.TrainTypeId,
                                        TrainTypeName = db.Base_TrainType.First(y => y.TrainTypeId == x.TrainTypeId).TrainTypeName,
                                        TrainLevelId = x.TrainLevelId,
                                        TrainLevelName = db.Base_TrainLevel.First(y => y.TrainLevelId == x.TrainLevelId).TrainLevelName,
                                        DesignerName = db.Sys_User.First(y => y.UserId == x.DesignerId).UserName,
                                        DesignerId = x.DesignerId,
                                        DesignerDate = string.Format("{0:yyyy-MM-dd HH:mm}", x.DesignerDate),
                                        TeachHour = x.TeachHour ?? 0,
                                        TeachAddress = x.TeachAddress,
                                        TeachMan = x.TeachMan,
                                        TrainStartDate = string.Format("{0:yyyy-MM-dd HH:mm}", x.TrainStartDate),
                                        States = x.States,
                                        QRCodeUrl = x.QRCodeUrl.Replace('\\', '/'),
                                    }).ToList();
                return getDataLists;
            }
        }
        #endregion

        #region 根据培训ID获取培训计划详细
        /// <summary>
        /// 根据培训ID获取培训计划详细
        /// </summary>
        /// <param name="planId"></param>
        /// <returns></returns>
        public static Model.TrainingPlanItem getTrainingPlanByTrainingId(string planId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var getDataLists = from x in db.Training_Plan
                                   where x.PlanId == planId
                                   select new Model.TrainingPlanItem
                                   {
                                       PlanId = x.PlanId,
                                       PlanCode = x.PlanCode,
                                       PlanName = x.PlanName,
                                       ProjectId = x.ProjectId,
                                       TrainTypeId = x.TrainTypeId,
                                       TrainTypeName = db.Base_TrainType.First(y => y.TrainTypeId == x.TrainTypeId).TrainTypeName,
                                       TrainLevelId = x.TrainLevelId,
                                       TrainLevelName = db.Base_TrainLevel.First(y => y.TrainLevelId == x.TrainLevelId).TrainLevelName,
                                       TeachHour = x.TeachHour ?? 0,
                                       TeachAddress = x.TeachAddress,
                                       TrainStartDate = string.Format("{0:yyyy-MM-dd HH:mm}", x.TrainStartDate),
                                       TeachMan = x.TeachMan,
                                       UnitIds = x.UnitIds,
                                       WorkPostId = x.WorkPostId,
                                       TrainContent = x.TrainContent,
                                       UnitNames = UnitService.getUnitNamesUnitIds(x.UnitIds),
                                       WorkPostNames = WorkPostService.getWorkPostNamesWorkPostIds(x.WorkPostId),
                                       DesignerId = x.DesignerId,
                                       DesignerName = db.Sys_User.First(y => y.UserId == x.DesignerId).UserName,
                                       DesignerDate = string.Format("{0:yyyy-MM-dd HH:mm}", x.TrainStartDate),
                                       States = x.States,
                                       QRCodeUrl = x.QRCodeUrl.Replace('\\', '/'),
                                   };
                return getDataLists.FirstOrDefault();
            }
        }
        #endregion

        #region 根据TrainingPlanId获取培训教材类型列表
        /// <summary>
        /// 根据TrainingPlanId获取培训教材类型列表
        /// </summary>
        /// <param name="trainingPlanId"></param>
        /// <returns>培训计划人员</returns>
        public static List<Model.TrainingPlanItemItem> getTrainingPlanItemListByTrainingPlanId(string trainingPlanId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var getDataLists = (from x in db.Training_PlanItem
                                    join y in db.Training_CompanyTraining on x.CompanyTrainingId equals y.CompanyTrainingId
                                    join z in db.Training_CompanyTrainingItem on x.CompanyTrainingItemId equals z.CompanyTrainingItemId
                                    where x.PlanId == trainingPlanId
                                    orderby y.CompanyTrainingCode
                                    select new Model.TrainingPlanItemItem
                                    {
                                        PlanItemId = x.PlanItemId,
                                        PlanId = x.PlanId,
                                        CompanyTrainingId = x.CompanyTrainingId,
                                        CompanyTrainingName = y.CompanyTrainingName,
                                        CompanyTrainingCode = y.CompanyTrainingCode,
                                        CompanyTrainingItemId = x.CompanyTrainingItemId,
                                        CompanyTrainingItemCode = z.CompanyTrainingItemCode,
                                        CompanyTrainingItemName = z.CompanyTrainingItemName,
                                    }).ToList();
                return getDataLists;
            }
        }
        #endregion

        #region 保存培训计划
        /// <summary>
        /// 保存TrainingPlan
        /// </summary>
        /// <param name="trainingPlan">培训计划记录</param>
        /// <param name="trainingTasks">培训人员list</param>
        /// <param name="trainingPlanItems">培训教材类型list</param>
        public static void SaveTrainingPlan(Model.TrainingPlanItem trainingPlan)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                Model.Training_Plan newTrainingPlan = new Model.Training_Plan
                {
                    PlanId = trainingPlan.PlanId,
                    PlanCode = trainingPlan.PlanCode,
                    ProjectId = trainingPlan.ProjectId,
                    DesignerId = trainingPlan.DesignerId,
                    PlanName = trainingPlan.PlanName,
                    TrainContent = trainingPlan.TrainContent,
                    TrainStartDate = Funs.GetNewDateTime(trainingPlan.TrainStartDate),
                    TeachHour = trainingPlan.TeachHour,
                    TeachMan = trainingPlan.TeachMan,
                    TeachAddress = trainingPlan.TeachAddress,
                    TrainTypeId = trainingPlan.TrainTypeId,
                    UnitIds = trainingPlan.UnitIds,
                    WorkPostId = trainingPlan.WorkPostId,
                    States = trainingPlan.States,
                };

                if (!string.IsNullOrEmpty(trainingPlan.TrainLevelId))
                {
                    newTrainingPlan.TrainLevelId = trainingPlan.TrainLevelId;
                }

                if (newTrainingPlan.TrainStartDate.HasValue && newTrainingPlan.TeachHour.HasValue)
                {
                    double dd = (double)((decimal)newTrainingPlan.TeachHour.Value);
                    newTrainingPlan.TrainEndDate = newTrainingPlan.TrainStartDate.Value.AddHours(dd);
                }

                List<Model.TrainingTaskItem> trainingTasks = trainingPlan.TrainingTasks;
                List<Model.TrainingPlanItemItem> trainingPlanItems = trainingPlan.TrainingPlanItems;

                var isUpdate = db.Training_Plan.FirstOrDefault(x => x.PlanId == newTrainingPlan.PlanId);
                if (isUpdate == null)
                {
                    newTrainingPlan.DesignerDate = DateTime.Now;
                    string unitId = string.Empty;
                    var user = UserService.GetUserByUserId(newTrainingPlan.DesignerId);
                    if (user != null)
                    {
                        unitId = user.UnitId;
                    }
                    newTrainingPlan.PlanCode = CodeRecordsService.ReturnCodeByMenuIdProjectId(Const.ProjectTrainingPlanMenuId, newTrainingPlan.ProjectId, unitId);
                    if (string.IsNullOrEmpty(newTrainingPlan.PlanId))
                    {
                        newTrainingPlan.PlanId = SQLHelper.GetNewID();
                    }
                    db.Training_Plan.InsertOnSubmit(newTrainingPlan);
                    db.SubmitChanges();

                    CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(Const.ProjectTrainingPlanMenuId, newTrainingPlan.ProjectId, null, newTrainingPlan.PlanId, newTrainingPlan.DesignerDate);
                }
                else
                {
                    if (newTrainingPlan.States == "0" || newTrainingPlan.States == "1")
                    {
                        isUpdate.PlanName = newTrainingPlan.PlanName;
                        isUpdate.TrainContent = newTrainingPlan.TrainContent;
                        isUpdate.TrainStartDate = newTrainingPlan.TrainStartDate;
                        isUpdate.TeachHour = newTrainingPlan.TeachHour;
                        isUpdate.TrainEndDate = newTrainingPlan.TrainEndDate;
                        isUpdate.TeachMan = newTrainingPlan.TeachMan;
                        isUpdate.TeachAddress = newTrainingPlan.TeachAddress;
                        isUpdate.TrainTypeId = newTrainingPlan.TrainTypeId;
                        isUpdate.TrainLevelId = newTrainingPlan.TrainLevelId;
                        isUpdate.UnitIds = newTrainingPlan.UnitIds;
                        isUpdate.WorkPostId = newTrainingPlan.WorkPostId;
                        isUpdate.States = newTrainingPlan.States;
                        db.SubmitChanges();
                    }
                    ////删除培训任务
                    var tasks = from x in db.Training_Task where x.PlanId == newTrainingPlan.PlanId select x;
                    if (tasks.Count() > 0)
                    {
                        var taskItems = from x in db.Training_TaskItem where x.PlanId == newTrainingPlan.PlanId select x;
                        if (tasks.Count() > 0)
                        {
                            db.Training_TaskItem.DeleteAllOnSubmit(taskItems);
                            db.SubmitChanges();
                        }
                        db.Training_Task.DeleteAllOnSubmit(tasks);
                        db.SubmitChanges();
                    }

                    ////删除培训教材类型
                    var planItem = (from x in db.Training_PlanItem where x.PlanId == newTrainingPlan.PlanId select x).ToList();
                    if (planItem.Count() > 0)
                    {
                        db.Training_PlanItem.DeleteAllOnSubmit(planItem);
                        db.SubmitChanges();
                    }
                }

                if (trainingTasks.Count() > 0)
                {
                    ////新增培训人员明细
                    foreach (var item in trainingTasks)
                    {
                        if (!string.IsNullOrEmpty(item.PersonId))
                        {
                            Model.Training_Task newTrainDetail = new Model.Training_Task
                            {
                                TaskId = SQLHelper.GetNewID(),
                                ProjectId = newTrainingPlan.ProjectId,
                                PlanId = newTrainingPlan.PlanId,
                                UserId = item.PersonId,
                                TaskDate = DateTime.Now,
                                States = Const.State_0, ////未生成培训教材明细
                            };
                            db.Training_Task.InsertOnSubmit(newTrainDetail);
                            db.SubmitChanges();
                        }
                    }
                }
                if (trainingPlanItems.Count() > 0)
                {
                    ////新增培训教材类型明细
                    foreach (var item in trainingPlanItems)
                    {
                        if (!string.IsNullOrEmpty(item.CompanyTrainingId) || !string.IsNullOrEmpty(item.CompanyTrainingItemId))
                        {
                            Model.Training_PlanItem newPlanItem = new Model.Training_PlanItem
                            {
                                PlanItemId = SQLHelper.GetNewID(),
                                PlanId = newTrainingPlan.PlanId,

                            };
                            if (!string.IsNullOrEmpty(item.CompanyTrainingId))
                            {
                                newPlanItem.CompanyTrainingId = item.CompanyTrainingId;
                            }
                            if (!string.IsNullOrEmpty(item.CompanyTrainingItemId))
                            {
                                newPlanItem.CompanyTrainingItemId = item.CompanyTrainingItemId;
                            }
                            db.Training_PlanItem.InsertOnSubmit(newPlanItem);
                            db.SubmitChanges();
                        }
                    }
                }
            }
        }

        /// <summary>
        ///  新增 培训人员明细
        /// </summary>
        public static void AddTraining_Task(List<Model.TrainingTaskItem> trainingTasks, string planId, string projectId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                foreach (var item in trainingTasks)
                {
                    if (!string.IsNullOrEmpty(item.PersonId))
                    {
                        Model.Training_Task newTrainDetail = new Model.Training_Task
                        {
                            TaskId = SQLHelper.GetNewID(),
                            ProjectId = projectId,
                            PlanId = planId,
                            UserId = item.PersonId,
                            TaskDate = DateTime.Now,
                            States = Const.State_0, ////未生成培训教材明细
                        };

                        db.Training_Task.InsertOnSubmit(newTrainDetail);
                        db.SubmitChanges();
                    }
                }
            }
        }

        /// <summary>
        ///  新增 培训教材类型 明细
        /// </summary>
        public static void AddTraining_PlanItem(List<Model.TrainingPlanItemItem> trainingPlanItems, string planId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                foreach (var item in trainingPlanItems)
            {
                    if (!string.IsNullOrEmpty(item.CompanyTrainingId) || !string.IsNullOrEmpty(item.CompanyTrainingItemId))
                    {
                        Model.Training_PlanItem newPlanItem = new Model.Training_PlanItem
                        {
                            PlanItemId = SQLHelper.GetNewID(),
                            PlanId = planId,

                        };
                        if (!string.IsNullOrEmpty(item.CompanyTrainingId))
                        {
                            newPlanItem.CompanyTrainingId = item.CompanyTrainingId;
                        }
                        if (!string.IsNullOrEmpty(item.CompanyTrainingItemId))
                        {
                            newPlanItem.CompanyTrainingItemId = item.CompanyTrainingItemId;
                        }
                        db.Training_PlanItem.InsertOnSubmit(newPlanItem);
                        db.SubmitChanges();
                    }
                }
            }
        }
        #endregion
    }
}
