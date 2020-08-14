using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace BLL
{
    public static class GetDataService
    {      
        #region 培训计划提交后 按照培训任务 生成培训人员的培训教材明细
        /// <summary>
        /// 生成培训人员的培训教材明细
        /// </summary>
        /// <param name="taskId"></param>
        public static void CreateTrainingTaskItemByTaskId(string taskId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                /////查找未生成教材明细的 培训任务
                var getTasks = from x in db.Training_Task
                               where x.States == Const.State_0 && (x.TaskId == taskId || taskId == null)
                               select x;
                if (getTasks.Count() > 0)
                {
                    foreach (var item in getTasks)
                    {
                        var getPerson = db.SitePerson_Person.FirstOrDefault(e => e.PersonId == item.UserId);
                        if (getPerson != null)
                        {
                            ////获取计划下 人员培训教材明细
                            var getDataList = db.Sp_GetTraining_TaskItemTraining(item.PlanId, getPerson.WorkPostId);
                            foreach (var dataItem in getDataList)
                            {
                                Model.Training_TaskItem newTaskItem = new Model.Training_TaskItem
                                {
                                    TaskId = item.TaskId,
                                    PlanId = item.PlanId,
                                    PersonId = item.UserId,
                                    TrainingItemCode = dataItem.TrainingItemCode,
                                    TrainingItemName = dataItem.TrainingItemName,
                                    AttachUrl = dataItem.AttachUrl,
                                };

                                var getTaskItem = db.Training_TaskItem.FirstOrDefault(x => x.TaskId == item.TaskId && x.TrainingItemName == newTaskItem.TrainingItemName && x.AttachUrl == newTaskItem.AttachUrl);
                                if (getTaskItem == null)
                                {
                                    newTaskItem.TaskItemId = SQLHelper.GetNewID();
                                    db.Training_TaskItem.InsertOnSubmit(newTaskItem);
                                    db.SubmitChanges();
                                }
                            }
                        }

                        ////更新培训任务
                        item.States = Const.State_1;
                        db.SubmitChanges();
                    }
                }
            }
        }
        #endregion

        #region 自动结束考试
        /// <summary>
        ///  自动结束考试
        /// </summary>
        public static void UpdateTestPlanStates()
        {
            Model.SGGLDB db = Funs.DB;
            var getTestPlans = from x in db.Training_TestPlan
                               where x.States == Const.State_2 && x.TestEndTime.AddMinutes(x.Duration) < DateTime.Now
                               select x;
            if (getTestPlans.Count() > 0)
            {
                foreach (var item in getTestPlans)
                {
                  //  APITestPlanService.SubmitTest(item);
                    item.States = "3";
                    db.SubmitChanges();
                }
            }

            var getTrainingTestRecords = from x in db.Training_TestRecord
                                         where x.TestStartTime.Value.AddMinutes(x.Duration) < DateTime.Now
                                         && (!x.TestEndTime.HasValue || !x.TestScores.HasValue)
                                         select x;
            foreach (var itemRecord in getTrainingTestRecords)
            {
                itemRecord.TestEndTime = itemRecord.TestStartTime.Value.AddMinutes(itemRecord.Duration);
                itemRecord.TestScores = db.Training_TestRecordItem.Where(x => x.TestRecordId == itemRecord.TestRecordId).Sum(x => x.SubjectScore) ?? 0;
                TestRecordService.UpdateTestRecord(itemRecord);
            }
        }
        #endregion

        #region 自动结束考试-知识竞赛
        /// <summary>
        ///  自动结束考试
        /// </summary>
        public static void UpdateServerTestPlanStates()
        {
            Model.SGGLDB db = Funs.DB;

            var getTrainingTestRecords = from x in db.Test_TestRecord
                                         where x.TestStartTime.Value.AddMinutes(x.Duration.Value) < DateTime.Now
                                         && (!x.TestEndTime.HasValue || !x.TestScores.HasValue)
                                         select x;
            foreach (var itemRecord in getTrainingTestRecords)
            {
                itemRecord.TestEndTime = itemRecord.TestStartTime.Value.AddMinutes(itemRecord.Duration.Value);
                itemRecord.TestScores = db.Test_TestRecordItem.Where(x => x.TestRecordId == itemRecord.TestRecordId).Sum(x => x.SubjectScore) ?? 0;
                db.SubmitChanges();
            }
        }
        #endregion

        #region 自动校正出入场人数及工时
        /// <summary>
        ///  自动校正出入场人数及工时
        /// </summary>
        public static void CorrectingPersonInOutNumber(string projectId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var getProjects = (from x in db.Base_Project
                                   where x.ProjectState == null || x.ProjectState == Const.ProjectState_1
                                   orderby x.ProjectCode descending
                                   select x).ToList();
                if (!string.IsNullOrEmpty(projectId))
                {
                    getProjects = getProjects.Where(x => x.ProjectId == projectId).ToList();
                }
                foreach (var projectItem in getProjects)
                {
                    var getAllPersonInOutList = from x in db.SitePerson_PersonInOut
                                                where x.ProjectId == projectItem.ProjectId && x.ChangeTime <= DateTime.Now
                                                select x;
                    if (getAllPersonInOutList.Count() > 0)
                    {
                        #region 现场当前人员数
                        int SitePersonNum = 0;
                        var getDayAll = from x in db.SitePerson_PersonInOutNow
                                        where x.ChangeTime.Value.Year == DateTime.Now.Year && x.ChangeTime.Value.Month == DateTime.Now.Month
                                        && x.ChangeTime.Value.Day == DateTime.Now.Day
                                        select x;
                        if (getDayAll.Count() > 0)
                        {
                            var getInMaxs = from x in getDayAll
                                            group x by x.PersonId into g
                                            select new { g.First().PersonId, ChangeTime = g.Max(x => x.ChangeTime) };
                            if (getInMaxs.Count() > 0)
                            {
                                SitePersonNum = (from x in getInMaxs
                                                 join y in getDayAll on new { x.PersonId, x.ChangeTime } equals new { y.PersonId, y.ChangeTime }
                                                 where y.IsIn == true
                                                 select y).Count();
                            }
                        }
                        #endregion

                        #region 获取工时                  
                        int SafeHours = 0;
                        var getPersonOutTimes = from x in getAllPersonInOutList
                                                where x.IsIn == false
                                                select x;
                        var getInLists = getAllPersonInOutList.Where(x => x.IsIn == true);
                        //// 查找当前项目 最新的人工时数量记录
                        var getMaxInOutDate = db.SitePerson_PersonInOutNumber.Where(x => x.ProjectId == projectItem.ProjectId).OrderByDescending(x => x.InOutDate).FirstOrDefault();
                        if (getMaxInOutDate != null)
                        {
                            SafeHours = (getMaxInOutDate.WorkHours ?? 0) * 60;
                            getPersonOutTimes = from x in getPersonOutTimes
                                                where x.ChangeTime > getMaxInOutDate.InOutDate
                                                select x;
                            if (getPersonOutTimes.Count() > 0)
                            {
                                getInLists = from x in getInLists
                                             where x.ChangeTime > getMaxInOutDate.InOutDate
                                             select x;
                            }
                        }

                        if (getPersonOutTimes.Count() > 0)
                        {
                            List<string> personIdList = new List<string>();
                            foreach (var item in getPersonOutTimes)
                            {
                                var getMaxInTime = getInLists.Where(x => x.ChangeTime < item.ChangeTime
                                            && x.PersonId == item.PersonId && x.ChangeTime.Value.AddDays(1) >= item.ChangeTime).Max(x => x.ChangeTime);
                                if (getMaxInTime.HasValue)
                                {
                                    SafeHours += Convert.ToInt32((item.ChangeTime - getMaxInTime).Value.TotalMinutes);
                                }
                                else
                                {
                                    personIdList.Add(item.PersonId);
                                }
                            }
                            if (personIdList.Count() > 0)
                            {
                                SafeHours += (personIdList.Distinct().Count() * 8 * 60);
                            }
                        }
                        #endregion

                        SafeHours = Convert.ToInt32(SafeHours * 1.0 / 60);
                        var getPersonInOutNumber = db.SitePerson_PersonInOutNumber.FirstOrDefault(x => x.ProjectId == projectItem.ProjectId
                                                                            && x.InOutDate.Year == DateTime.Now.Year
                                                                            && x.InOutDate.Month == DateTime.Now.Month
                                                                            && x.InOutDate.Day == DateTime.Now.Day);
                        if (getPersonInOutNumber == null)
                        {
                            Model.SitePerson_PersonInOutNumber newNum = new Model.SitePerson_PersonInOutNumber
                            {
                                PersonInOutNumberId = SQLHelper.GetNewID(),
                                ProjectId = projectItem.ProjectId,
                                InOutDate = DateTime.Now,
                                PersonNum = SitePersonNum,
                                WorkHours = SafeHours,
                            };

                            db.SitePerson_PersonInOutNumber.InsertOnSubmit(newNum);
                            db.SubmitChanges();
                        }
                        else
                        {
                            getPersonInOutNumber.InOutDate = DateTime.Now;
                            getPersonInOutNumber.PersonNum = SitePersonNum;
                            getPersonInOutNumber.WorkHours = SafeHours;
                            db.SubmitChanges();
                        }
                    }
                }
            }
        }
        #endregion

        #region 自动批量生成人员二维码
        /// <summary>
        ///  自动校正出入场人数及工时
        /// </summary>
        public static void CreateQRCode()
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var getPersons = from x in db.SitePerson_Person
                                 where x.IdentityCard != null && x.QRCodeAttachUrl == null
                                 select x;
                if (getPersons.Count() > 0)
                {
                    foreach (var item in getPersons)
                    {
                        string url = CreateQRCodeService.CreateCode_Simple("person$" + item.IdentityCard);
                        if (!string.IsNullOrEmpty(url))
                        {
                            item.QRCodeAttachUrl = url;
                            db.SubmitChanges();
                        }
                    }
                }
            }
        }
        #endregion

        #region 自动 出场人员 自动离场
        /// <summary>
        ///  自动校正出入场人数及工时
        /// </summary>
        public static void SitePersonjAutomaticOut()
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var getPersons = from x in db.SitePerson_Person
                                 where x.OutTime < DateTime.Now && x.IsUsed == true
                                 select x;
                if (getPersons.Count() > 0)
                {
                    foreach (var item in getPersons)
                    {
                        item.IsUsed = false;
                        item.ExchangeTime2 = null;
                        db.SubmitChanges();
                    }
                }
            }
        }
        #endregion

        #region 定时推送待办 订阅服务内容
        /// <summary>
        /// 定时推送待办 订阅服务内容
        /// </summary>
        public static void SendSubscribeMessage()
        {
            try
            {
                string miniprogram_state = ConfigurationManager.AppSettings["miniprogram_state"];
                if (!string.IsNullOrEmpty(miniprogram_state) && miniprogram_state == "formal")
                {
                    //// 获取所有待办事项
                    var getToItems = from x in Funs.DB.View_APP_GetToDoItems select x;
                    if (getToItems.Count() > 0)
                    {
                        //// 获取施工中的项目
                        var getProjects = ProjectService.GetProjectWorkList();
                        foreach (var item in getProjects)
                        {
                            ////获取当前项目下的待办
                            var getPItems = getToItems.Where(x => x.ProjectId == item.ProjectId);
                            if (getPItems.Count() > 0)
                            {
                                foreach (var itemP in getPItems)
                                {
                                    APICommonService.SendSubscribeMessage(itemP.UserId, "项目【" + item.ProjectCode + "】上有" + itemP.Counts.ToString() + "条待办事件，需要您处理！", "赛鼎施工管理系统", string.Format("{0:yyyy-MM-dd HH:mm:ss}", DateTime.Now));
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            { }
        }

        #endregion
    }
}
