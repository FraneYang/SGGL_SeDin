using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public static class Person_TrainingPlanService
    {
        public static Model.SGGLDB db = Funs.DB;
        /// <summary>
        /// 获取人员信息
        /// </summary>
        /// <param name="TrainingPlanId">人员Id</param>
        /// <returns>人员信息</returns>
        public static Model.Person_TrainingPlan GetPersonTrainingPlanById(string TrainingPlanId)
        {
            return Funs.DB.Person_TrainingPlan.FirstOrDefault(e => e.TrainingPlanId == TrainingPlanId);
        }

        /// <summary>
        /// 增加员工培训信息
        /// </summary>
        /// <param name="user">人员实体</param>
        public static void AddPersonTrainingPlan(Model.Person_TrainingPlan TrainingPlan)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Person_TrainingPlan newTrainingPlan = new Model.Person_TrainingPlan
            {
                TrainingPlanId = TrainingPlan.TrainingPlanId,
                TrainingPlanCode = TrainingPlan.TrainingPlanCode,
                TrainingPlanTitle=TrainingPlan.TrainingPlanTitle,
                TrainingPlanContent = TrainingPlan.TrainingPlanContent,
                StartTime= TrainingPlan.StartTime,
                EndTime= TrainingPlan.EndTime,
                CompilePersonId = TrainingPlan.CompilePersonId,
                CompileTime = TrainingPlan.CompileTime,
                ApprovePersonId = TrainingPlan.ApprovePersonId,
                State = TrainingPlan.State,

            };
            db.Person_TrainingPlan.InsertOnSubmit(newTrainingPlan);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改员工培训信息
        /// </summary>
        /// <param name="user">实体</param>
        public static void UpdatePersonTrainingPlan(Model.Person_TrainingPlan TrainingPlan)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Person_TrainingPlan newTrainingPlan = db.Person_TrainingPlan.FirstOrDefault(e => e.TrainingPlanId == TrainingPlan.TrainingPlanId);
            if (newTrainingPlan != null)
            {
                newTrainingPlan.TrainingPlanId = TrainingPlan.TrainingPlanId;
                newTrainingPlan.TrainingPlanCode = TrainingPlan.TrainingPlanCode;
                newTrainingPlan.TrainingPlanTitle = TrainingPlan.TrainingPlanTitle;
                newTrainingPlan.TrainingPlanContent = TrainingPlan.TrainingPlanContent;
                newTrainingPlan.StartTime = TrainingPlan.StartTime;
                newTrainingPlan.EndTime = TrainingPlan.EndTime;
                newTrainingPlan.CompilePersonId = TrainingPlan.CompilePersonId;
                newTrainingPlan.CompileTime = TrainingPlan.CompileTime;
                newTrainingPlan.ApprovePersonId = TrainingPlan.ApprovePersonId;
                newTrainingPlan.State = TrainingPlan.State;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据人员Id删除一个人员信息
        /// </summary>
        /// <param name="Person_TrainingPlanId"></param>
        public static void DeletePersonTrainingPlan(string TrainingPlanId)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Person_TrainingPlan plan = db.Person_TrainingPlan.FirstOrDefault(e => e.TrainingPlanId == TrainingPlanId);
            if (plan != null)
            {
                ///删除培训人员
                var TrainingPerson = db.Person_TrainingPerson.Where(x => x.TrainingPlanId == plan.TrainingPlanId);
                if (TrainingPerson.Count() > 0)
                {
                    foreach (var item in TrainingPerson)
                    {
                        var PersonItem = db.Person_TrainingTask.Where(x => x.TrainingPersonId == item.TrainingPersonId);
                        if (PersonItem.Count() > 0) {
                            db.Person_TrainingTask.DeleteAllOnSubmit(PersonItem);
                        }
                    }
                    db.Person_TrainingPerson.DeleteAllOnSubmit(TrainingPerson);
                }
                ///删除培训教材
                var TrainingCompany = db.Person_TrainingCompany.Where(x => x.TrainingPlanId == plan.TrainingPlanId);
                if (TrainingCompany.Count() > 0)
                {
                    db.Person_TrainingCompany.DeleteAllOnSubmit(TrainingCompany);
                }
                db.Person_TrainingPlan.DeleteOnSubmit(plan);
                db.SubmitChanges();
            }
        }
    }
}
