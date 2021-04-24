using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class CheckMonthService
    {
        public static Model.SGGLDB db = Funs.DB;

        /// <summary>
        /// 记录数
        /// </summary>
        private static int count
        {
            get;
            set;
        }

        /// <summary>
        /// 定义变量
        /// </summary>
        public static IQueryable<Model.Check_CheckMonth> qq = from x in db.Check_CheckMonth orderby x.Months descending select x;

        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="searchItem"></param>
        /// <param name="searchValue"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <returns></returns>
        public static IEnumerable GetListData(string projectId, int startRowIndex, int maximumRows)
        {
            IQueryable<Model.Check_CheckMonth> q = qq;
            if (!string.IsNullOrEmpty(projectId))
            {
                q = q.Where(e => e.ProjectId == projectId);
            }
            count = q.Count();
            if (count == 0)
            {
                return new object[] { "" };
            }
            return from x in q.Skip(startRowIndex).Take(maximumRows)
                   select new
                   {
                       x.CheckMonthId,
                       x.ProjectId,
                       x.ThisRectifyNum,
                       x.ThisOKRectifyNum,
                       x.TotalRectifyNum,
                       x.TotalOKRectifyNum,
                       x.ThisSpotCheckNum,
                       x.TotalSpotCheckNum,
                       x.TotalCompletedRate,
                       x.OnesOKRate,
                       CompileMan = (from y in db.Sys_User where y.UserId == x.CompileMan select y.UserName).First(),
                       x.CompileDate,
                       Months = Convert.ToDateTime(x.Months).Year.ToString() + "-" + Convert.ToDateTime(x.Months).Month.ToString(),
                   };
        }

        /// <summary>
        /// 获取列表数
        /// </summary>
        /// <param name="searchItem"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public static int GetListCount(string projectId)
        {
            return count;
        }

        /// <summary>
        /// 添加质量月报
        /// </summary>
        /// <param name="CheckMonth"></param>
        public static void AddCheckMonth(Model.Check_CheckMonth CheckMonth)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Check_CheckMonth newCheckMonth = new Model.Check_CheckMonth();
            newCheckMonth.CheckMonthId = CheckMonth.CheckMonthId;
            newCheckMonth.ProjectId = CheckMonth.ProjectId;
            newCheckMonth.Months = CheckMonth.Months;
            newCheckMonth.ThisRectifyNum = CheckMonth.ThisRectifyNum;
            newCheckMonth.ThisOKRectifyNum = CheckMonth.ThisOKRectifyNum;
            newCheckMonth.TotalRectifyNum = CheckMonth.TotalRectifyNum;
            newCheckMonth.TotalOKRectifyNum = CheckMonth.TotalOKRectifyNum;
            newCheckMonth.ThisSpotCheckNum = CheckMonth.ThisSpotCheckNum;
            newCheckMonth.TotalSpotCheckNum = CheckMonth.TotalSpotCheckNum;
            newCheckMonth.TotalCompletedRate = CheckMonth.TotalCompletedRate;
            newCheckMonth.OnesOKRate = CheckMonth.OnesOKRate;
            newCheckMonth.CompileMan = CheckMonth.CompileMan;
            newCheckMonth.CompileDate = CheckMonth.CompileDate;
            newCheckMonth.ManagementOverview = CheckMonth.ManagementOverview;
            newCheckMonth.AccidentSituation = CheckMonth.AccidentSituation;
            newCheckMonth.ConstructionData = CheckMonth.ConstructionData;
            newCheckMonth.NextMonthPlan = CheckMonth.NextMonthPlan;
            newCheckMonth.NeedSolved = CheckMonth.NeedSolved;
            newCheckMonth.MonthOk = CheckMonth.MonthOk;
            newCheckMonth.AllOk = CheckMonth.AllOk;
            newCheckMonth.MonthDataOk = CheckMonth.MonthDataOk;
            newCheckMonth.AllDataOk = CheckMonth.AllDataOk;

            db.Check_CheckMonth.InsertOnSubmit(newCheckMonth);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改质量月报
        /// </summary>
        /// <param name="CheckMonth"></param>
        public static void UpdateCheckMonth(Model.Check_CheckMonth CheckMonth)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Check_CheckMonth newCheckMonth = db.Check_CheckMonth.First(e => e.CheckMonthId == CheckMonth.CheckMonthId);
            newCheckMonth.ThisRectifyNum = CheckMonth.ThisRectifyNum;
            newCheckMonth.ThisOKRectifyNum = CheckMonth.ThisOKRectifyNum;
            newCheckMonth.TotalRectifyNum = CheckMonth.TotalRectifyNum;
            newCheckMonth.TotalOKRectifyNum = CheckMonth.TotalOKRectifyNum;
            newCheckMonth.ThisSpotCheckNum = CheckMonth.ThisSpotCheckNum;
            newCheckMonth.TotalSpotCheckNum = CheckMonth.TotalSpotCheckNum;
            newCheckMonth.TotalCompletedRate = CheckMonth.TotalCompletedRate;
            newCheckMonth.OnesOKRate = CheckMonth.OnesOKRate;
            newCheckMonth.ManagementOverview = CheckMonth.ManagementOverview;
            newCheckMonth.AccidentSituation = CheckMonth.AccidentSituation;
            newCheckMonth.ConstructionData = CheckMonth.ConstructionData;
            newCheckMonth.NextMonthPlan = CheckMonth.NextMonthPlan;
            newCheckMonth.NeedSolved = CheckMonth.NeedSolved;
            newCheckMonth.MonthOk = CheckMonth.MonthOk;
            newCheckMonth.AllOk = CheckMonth.AllOk;
            newCheckMonth.MonthDataOk = CheckMonth.MonthDataOk;
            newCheckMonth.AllDataOk = CheckMonth.AllDataOk;

            db.SubmitChanges();
        }

        /// <summary>
        /// 根据质量月报Id删除一个质量月报信息
        /// </summary>
        /// <param name="CheckMonthId"></param>
        public static void DeleteCheckMonth(string CheckMonthId)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Check_CheckMonth CheckMonth = db.Check_CheckMonth.First(e => e.CheckMonthId == CheckMonthId);
            db.Check_CheckMonth.DeleteOnSubmit(CheckMonth);
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据质量月报Id获取一个质量月报信息
        /// </summary>
        /// <param name="CheckMonthDetailId"></param>
        public static Model.Check_CheckMonth GetCheckMonth(string CheckMonthId)
        {
            return Funs.DB.Check_CheckMonth.FirstOrDefault(e => e.CheckMonthId == CheckMonthId);
        }

        /// <summary>
        /// 根据月份获取一个质量月报信息
        /// </summary>
        /// <param name="months">月份</param>
        public static Model.Check_CheckMonth GetCheckMonthByMonths(DateTime months, string projectId)
        {
            return Funs.DB.Check_CheckMonth.FirstOrDefault(e => e.Months == months && e.ProjectId == projectId);
        }
    }
}
