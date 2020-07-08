using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 培训任务
    /// </summary>
    public static class TrainingTaskService
    {
        public static Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);

        /// <summary>
        /// 根据主键获取培训任务
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        public static Model.Training_Task GetTaskById(string taskId)
        {
            return db.Training_Task.FirstOrDefault(e => e.TaskId == taskId);
        }

        /// <summary>
        /// 根据培训计划主键获取培训任务
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        public static List<Model.Training_Task> GetTaskListByPlanId(string planId)
        {
            return (from x in new Model.SGGLDB(Funs.ConnString).Training_Task where x.PlanId == planId select x).ToList();
        }

        /// <summary>
        /// 添加培训任务
        /// </summary>
        /// <param name="task"></param>
        public static void AddTask(Model.Training_Task task)
        {
            Model.Training_Task newTask = new Model.Training_Task
            {
                TaskId = task.TaskId,
                PlanId = task.PlanId,
                UserId = task.UserId,
                TaskDate = task.TaskDate,
                States = task.States
            };
            db.Training_Task.InsertOnSubmit(newTask);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改培训任务
        /// </summary>
        /// <param name="task"></param>
        public static void UpdateTask(Model.Training_Task task)
        {
            Model.Training_Task newTask = new Model.SGGLDB(Funs.ConnString).Training_Task.FirstOrDefault(e => e.TaskId == task.TaskId);
            if (newTask != null  && !string.IsNullOrEmpty(task.UserId))
            {
                newTask.PlanId = task.PlanId;
                newTask.UserId = task.UserId;
                newTask.TaskDate = task.TaskDate;
                //newTask.States = task.States;
                new Model.SGGLDB(Funs.ConnString).SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除培训任务
        /// </summary>
        /// <param name="taskId"></param>
        public static void DeleteTaskById(string taskId)
        {
            Model.Training_Task task = db.Training_Task.FirstOrDefault(e => e.TaskId == taskId);
            if (task != null)
            {
                db.Training_Task.DeleteOnSubmit(task);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据培训计划主键删除培训任务
        /// </summary>
        /// <param name="planId"></param>
        public static void DeleteTaskByPlanId(string planId)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            var tasks = from x in db.Training_Task where x.PlanId == planId select x;
            if (tasks.Count() > 0)
            {
                var taskItems = from x in db.Training_TaskItem where x.PlanId == planId select x;
                if (tasks.Count() > 0)
                {
                    db.Training_TaskItem.DeleteAllOnSubmit(taskItems);
                }

                db.Training_Task.DeleteAllOnSubmit(tasks);
            }
        }
    }
}
