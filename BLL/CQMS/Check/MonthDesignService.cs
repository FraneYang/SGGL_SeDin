using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace BLL
{
    public class MonthDesignService
    {
        public static Model.SGGLDB db = Funs.DB;

        /// <summary>
        /// 获取月报设计变更情况列表
        /// </summary>
        /// <param name="satartRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <returns></returns>
        public static List<Model.Check_MonthDesign> getListData(string CheckMonthId)
        {
            return (from x in db.Check_MonthDesign
                    where x.CheckMonthId == CheckMonthId
                    select x).ToList();
        }

        /// <summary>
        /// 增加月报设计变更情况
        /// </summary>
        /// <param name="managerRuleApprove">月报设计变更情况实体</param>
        public static void AddMonthDesign(Model.Check_MonthDesign monthSpotCheckDetail)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Check_MonthDesign newApprove = new Model.Check_MonthDesign();
            newApprove.MonthDesignId = monthSpotCheckDetail.MonthDesignId;
            newApprove.CheckMonthId = monthSpotCheckDetail.CheckMonthId;
            newApprove.DesignCode = monthSpotCheckDetail.DesignCode;
            newApprove.MainItemId = monthSpotCheckDetail.MainItemId;
            newApprove.DesignProfessionalId = monthSpotCheckDetail.DesignProfessionalId;
            newApprove.State = monthSpotCheckDetail.State;
            newApprove.Remark = monthSpotCheckDetail.Remark;

            db.Check_MonthDesign.InsertOnSubmit(newApprove);
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据月报id删除对应的所有月报设计变更情况
        /// </summary>
        /// <param name="CheckMonthId">月报设计变更情况编号</param>
        public static void DeleteMonthDesignsByCheckMonthId(string CheckMonthId)
        {
            Model.SGGLDB db = Funs.DB;
            var q = (from x in db.Check_MonthDesign where x.CheckMonthId == CheckMonthId select x).ToList();
            if (q.Count() > 0)
            {
                db.Check_MonthDesign.DeleteAllOnSubmit(q);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据月报id获取对应的所有月报设计变更情况
        /// </summary>
        /// <param name="CheckMonthId">月报设计变更情况编号</param>
        public static List<Model.Check_MonthDesign> GetMonthDesignsByCheckMonthId(string CheckMonthId)
        {
            Model.SGGLDB db = Funs.DB;
            return (from x in db.Check_MonthDesign where x.CheckMonthId == CheckMonthId select x).ToList();
        }
    }
}
