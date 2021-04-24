using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace BLL
{
    public class MonthWelderService
    {
        public static Model.SGGLDB db = Funs.DB;

        /// <summary>
        /// 获取月报焊工资格评定情况模板列表
        /// </summary>
        /// <param name="satartRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <returns></returns>
        public static List<Model.Check_MonthWelder> getListData(string CheckMonthId)
        {
            return (from x in db.Check_MonthWelder
                    where x.CheckMonthId == CheckMonthId
                    select x).ToList();
        }

        /// <summary>
        /// 增加月报焊工资格评定情况
        /// </summary>
        /// <param name="managerRuleApprove">月报焊工资格评定情况实体</param>
        public static void AddMonthWelder(Model.Check_MonthWelder monthSpotCheckDetail)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Check_MonthWelder newApprove = new Model.Check_MonthWelder();
            newApprove.MonthWelderId = monthSpotCheckDetail.MonthWelderId;
            newApprove.CheckMonthId = monthSpotCheckDetail.CheckMonthId;
            newApprove.UnitId = monthSpotCheckDetail.UnitId;
            newApprove.ThisPersonNum = monthSpotCheckDetail.ThisPersonNum;
            newApprove.ThisOKPersonNum = monthSpotCheckDetail.ThisOKPersonNum;
            newApprove.ThisOKRate = monthSpotCheckDetail.ThisOKRate;
            newApprove.TotalPersonNum = monthSpotCheckDetail.TotalPersonNum;
            newApprove.TotalOKPersonNum = monthSpotCheckDetail.TotalOKPersonNum;
            newApprove.TotalOKRate = monthSpotCheckDetail.TotalOKRate;

            db.Check_MonthWelder.InsertOnSubmit(newApprove);
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据月报id删除对应的所有月报焊工资格评定情况
        /// </summary>
        /// <param name="CheckMonthId">月报焊工资格评定情况编号</param>
        public static void DeleteMonthWeldersByCheckMonthId(string CheckMonthId)
        {
            Model.SGGLDB db = Funs.DB;
            var q = (from x in db.Check_MonthWelder where x.CheckMonthId == CheckMonthId select x).ToList();
            if (q.Count() > 0)
            {
                db.Check_MonthWelder.DeleteAllOnSubmit(q);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据月报id获取对应的所有月报焊工资格评定情况
        /// </summary>
        /// <param name="CheckMonthId">月报焊工资格评定情况编号</param>
        public static List<Model.Check_MonthWelder> GetMonthWeldersByCheckMonthId(string CheckMonthId)
        {
            Model.SGGLDB db = Funs.DB;
            return (from x in db.Check_MonthWelder where x.CheckMonthId == CheckMonthId select x).ToList();
        }
    }
}
