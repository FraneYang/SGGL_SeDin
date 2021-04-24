using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace BLL
{
    public class MonthNDTCheckService
    {
        public static Model.SGGLDB db = Funs.DB;

        /// <summary>
        /// 获取月报无损检测情况模板列表
        /// </summary>
        /// <param name="satartRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <returns></returns>
        public static List<Model.Check_MonthNDTCheck> getListData(string CheckMonthId)
        {
            return (from x in db.Check_MonthNDTCheck
                    where x.CheckMonthId == CheckMonthId
                    select x).ToList();
        }

        /// <summary>
        /// 增加月报无损检测情况
        /// </summary>
        /// <param name="managerRuleApprove">月报无损检测情况实体</param>
        public static void AddMonthNDTCheck(Model.Check_MonthNDTCheck monthSpotCheckDetail)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Check_MonthNDTCheck newApprove = new Model.Check_MonthNDTCheck();
            newApprove.MonthNDTCheckId = monthSpotCheckDetail.MonthNDTCheckId;
            newApprove.CheckMonthId = monthSpotCheckDetail.CheckMonthId;
            newApprove.UnitId = monthSpotCheckDetail.UnitId;
            newApprove.FilmNum = monthSpotCheckDetail.FilmNum;
            newApprove.NotOKFileNum = monthSpotCheckDetail.NotOKFileNum;
            newApprove.RepairFileNum = monthSpotCheckDetail.RepairFileNum;
            newApprove.OneOKRate = monthSpotCheckDetail.OneOKRate;
            newApprove.TotalFilmNum = monthSpotCheckDetail.TotalFilmNum;
            newApprove.TotalNotOKFileNum = monthSpotCheckDetail.TotalNotOKFileNum;
            newApprove.TotalOneOKRate = monthSpotCheckDetail.TotalOneOKRate;

            db.Check_MonthNDTCheck.InsertOnSubmit(newApprove);
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据月报id删除对应的所有月报无损检测情况
        /// </summary>
        /// <param name="CheckMonthId">月报无损检测情况编号</param>
        public static void DeleteMonthNDTChecksByCheckMonthId(string CheckMonthId)
        {
            Model.SGGLDB db = Funs.DB;
            var q = (from x in db.Check_MonthNDTCheck where x.CheckMonthId == CheckMonthId select x).ToList();
            if (q.Count() > 0)
            {
                db.Check_MonthNDTCheck.DeleteAllOnSubmit(q);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据月报id获取对应的所有月报无损检测情况
        /// </summary>
        /// <param name="CheckMonthId">月报无损检测情况编号</param>
        public static List<Model.Check_MonthNDTCheck> GetMonthNDTChecksByCheckMonthId(string CheckMonthId)
        {
            Model.SGGLDB db = Funs.DB;
            return (from x in db.Check_MonthNDTCheck where x.CheckMonthId == CheckMonthId select x).ToList();
        }
    }
}
