using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmitMapper;
using EmitMapper.MappingConfiguration;

namespace BLL
{
    /// <summary>
    /// 考试计划
    /// </summary>
    public static class APIServerTestPlanService
    {
        #region 获取考试计划列表
        /// <summary>
        /// 获取考试计划列表
        /// </summary>
        /// <param name="states">状态（0-待发布；1-待考试；2-考试中；3已结束；-1作废）</param>
        /// <returns></returns>
        public static List<Model.TestPlanItem> getTestPlanList(string states)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var getDataLists = (from x in db.Test_TestPlan
                                    where (x.States == states || states == null)
                                    orderby x.TestStartTime descending
                                    select new Model.TestPlanItem
                                    {
                                        TestPlanId = x.TestPlanId,
                                        TestPlanCode = x.PlanCode,
                                        TestPlanName = x.PlanName,
                                        TestPlanManId = x.PlanManId,
                                        TestPlanManName = db.Sys_User.First(y => y.UserId == x.PlanManId).UserName,
                                        TestPalce = x.TestPalce,
                                        Duration = x.Duration ?? 60,
                                        TestStartTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.TestStartTime),
                                        TestEndTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.TestEndTime),
                                        ActualTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.ActualTime),
                                        States = x.States,
                                        QRCodeUrl = x.QRCodeUrl.Replace('\\', '/'),
                                    }).ToList();
                return getDataLists;
            }
        }
        #endregion

        #region 根据考试ID获取考试计划详细
        /// <summary>
        /// 根据考试ID获取考试计划详细
        /// </summary>
        /// <param name="testPlanId"></param>
        /// <returns></returns>
        public static Model.TestPlanItem getTestPlanByTestPlanId(string testPlanId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var getDataLists = from x in db.Test_TestPlan
                                   where x.TestPlanId == testPlanId
                                   select new Model.TestPlanItem
                                   {
                                       TestPlanId = x.TestPlanId,
                                       TestPlanCode = x.PlanCode,
                                       TestPlanName = x.PlanName,
                                       TestPlanManId = x.PlanManId,
                                       TestPlanManName = db.Sys_User.First(y => y.UserId == x.TestPlanId).UserName,
                                       TestPlanDate = string.Format("{0:yyyy-MM-dd HH:mm}", x.PlanDate),
                                       TestStartTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.TestStartTime),
                                       TestEndTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.TestEndTime),
                                       ActualTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.ActualTime),
                                       Duration = x.Duration ?? 60,
                                       SValue = x.SValue ?? 0,
                                       MValue = x.MValue ?? 0,
                                       JValue = x.JValue ?? 0,
                                       TestPalce = x.TestPalce,
                                       States = x.States,
                                       QRCodeUrl = x.QRCodeUrl.Replace('\\', '/'),
                                   };
                return getDataLists.FirstOrDefault();
            }
        }
        #endregion

        #region 根据TestPlanId获取考试试题类型列表
        /// <summary>
        /// 根据TestPlanId获取考试试题类型列表
        /// </summary>
        /// <param name="testPlanId">考试计划ID</param>
        /// <returns></returns>
        public static List<Model.TestPlanTrainingItem> getTestPlanTrainingListByTestPlanId(string testPlanId)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            var getDataLists = (from x in db.Test_TestPlanTraining
                                join y in db.Training_TestTraining on x.TrainingId equals y.TrainingId
                                where x.TestPlanId == testPlanId
                                orderby y.TrainingCode
                                select new Model.TestPlanTrainingItem
                                {
                                    TestPlanTrainingId = x.TestPlanTrainingId,
                                    TestPlanId = x.TestPlanId,
                                    TrainingTypeId = x.TrainingId,
                                    TrainingTypeName = y.TrainingName,
                                    TestType1Count = x.TestType1Count ?? 0,
                                    TestType2Count = x.TestType2Count ?? 0,
                                    TestType3Count = x.TestType3Count ?? 0,
                                    UserTypeId = x.UserType,
                                    UserTypeName = x.UserType == null ? "" : (x.UserType == "1" ? "管理人员" : (x.UserType == "2" ? "临时用户" : "作业人员")),
                                }).ToList();
            return getDataLists;
        }
        #endregion
    }
}
