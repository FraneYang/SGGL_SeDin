using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace BLL
{
    public class MonthRectifyService
    {
        public static Model.SGGLDB db = Funs.DB;

        /// <summary>
        /// 获取月报质量巡检情况模板列表
        /// </summary>
        /// <param name="satartRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <returns></returns>
        public static List<Model.Check_MonthRectify> getListData(string CheckMonthId)
        {
            return (from x in db.Check_MonthRectify
                   where x.CheckMonthId == CheckMonthId
                   select x).ToList();
        }

        /// <summary>
        /// 增加月报质量巡检情况
        /// </summary>
        /// <param name="managerRuleApprove">月报质量巡检情况实体</param>
        public static void AddMonthRectify(Model.Check_MonthRectify monthSpotCheckDetail)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Check_MonthRectify newApprove = new Model.Check_MonthRectify();
            newApprove.MonthRectifyId = monthSpotCheckDetail.MonthRectifyId;
            newApprove.CheckMonthId = monthSpotCheckDetail.CheckMonthId;
            newApprove.Depart = monthSpotCheckDetail.Depart;
            newApprove.ThisRectifyNum = monthSpotCheckDetail.ThisRectifyNum;
            newApprove.ThisOKRectifyNum = monthSpotCheckDetail.ThisOKRectifyNum;
            newApprove.TotalRectifyNum = monthSpotCheckDetail.TotalRectifyNum;
            newApprove.TotalOKRectifyNum = monthSpotCheckDetail.TotalOKRectifyNum;

            db.Check_MonthRectify.InsertOnSubmit(newApprove);
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据月报id删除对应的所有月报质量巡检情况
        /// </summary>
        /// <param name="CheckMonthId">月报质量巡检情况编号</param>
        public static void DeleteMonthRectifysByCheckMonthId(string CheckMonthId)
        {
            Model.SGGLDB db = Funs.DB;
            var q = (from x in db.Check_MonthRectify where x.CheckMonthId == CheckMonthId select x).ToList();
            if (q.Count() > 0)
            {
                db.Check_MonthRectify.DeleteAllOnSubmit(q);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据月报id获取对应的所有月报质量巡检情况
        /// </summary>
        /// <param name="CheckMonthId">月报质量巡检情况编号</param>
        public static List<Model.Check_MonthRectify> GetMonthRectifysByCheckMonthId(string CheckMonthId)
        {
            Model.SGGLDB db = Funs.DB;
            return (from x in db.Check_MonthRectify where x.CheckMonthId == CheckMonthId select x).ToList();
        }
    }
}
