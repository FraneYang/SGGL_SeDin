using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace BLL
{
    public class MonthSpotCheckDetailService
    {
        public static Model.SGGLDB db = Funs.DB;

        /// <summary>
        /// 获取月报质量验收情况模板列表
        /// </summary>
        /// <param name="satartRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <returns></returns>
        public static List<Model.Check_MonthSpotCheckDetail> getListData(string CheckMonthId)
        {
            return (from x in db.Check_MonthSpotCheckDetail
                   where x.CheckMonthId == CheckMonthId
                   select x).ToList();
        }

        /// <summary>
        /// 增加月报质量验收情况
        /// </summary>
        /// <param name="managerRuleApprove">月报质量验收情况实体</param>
        public static void AddMonthSpotCheckDetail(Model.Check_MonthSpotCheckDetail monthSpotCheckDetail)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Check_MonthSpotCheckDetail newApprove = new Model.Check_MonthSpotCheckDetail();
            newApprove.MonthSpotCheckDetailId = monthSpotCheckDetail.MonthSpotCheckDetailId;
            newApprove.CheckMonthId = monthSpotCheckDetail.CheckMonthId;
            newApprove.ControlPoint = monthSpotCheckDetail.ControlPoint;
            newApprove.TotalNum = monthSpotCheckDetail.TotalNum;
            newApprove.ThisOKNum = monthSpotCheckDetail.ThisOKNum;
            newApprove.TotalOKNum = monthSpotCheckDetail.TotalOKNum;
            newApprove.TotalOKRate = monthSpotCheckDetail.TotalOKRate;
            newApprove.Remark = monthSpotCheckDetail.Remark;

            db.Check_MonthSpotCheckDetail.InsertOnSubmit(newApprove);
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据月报id删除对应的所有月报质量验收情况
        /// </summary>
        /// <param name="CheckMonthId">月报质量验收情况编号</param>
        public static void DeleteMonthSpotCheckDetailsByCheckMonthId(string CheckMonthId)
        {
            Model.SGGLDB db = Funs.DB;
            var q = (from x in db.Check_MonthSpotCheckDetail where x.CheckMonthId == CheckMonthId select x).ToList();
            if (q.Count() > 0)
            {
                db.Check_MonthSpotCheckDetail.DeleteAllOnSubmit(q);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据月报id获取对应的所有月报质量验收情况
        /// </summary>
        /// <param name="CheckMonthId">月报质量验收情况编号</param>
        public static List<Model.Check_MonthSpotCheckDetail> GetMonthSpotCheckDetailsByCheckMonthId(string CheckMonthId)
        {
            Model.SGGLDB db = Funs.DB;
            return (from x in db.Check_MonthSpotCheckDetail where x.CheckMonthId == CheckMonthId select x).ToList();
        }
    }
}
