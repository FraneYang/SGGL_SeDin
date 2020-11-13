using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class WorkHandoverDetailService
    {
        public static Model.SGGLDB db = Funs.DB;

        /// <summary>
        /// 获取工作交接明细列表
        /// </summary>
        /// <param name="satartRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <returns></returns>
        public static IEnumerable getListData(string WorkHandoverId)
        {
            return from x in db.ZHGL_WorkHandoverDetail
                   where x.WorkHandoverId == WorkHandoverId
                   select new
                   {
                       x.WorkHandoverDetailId,
                       x.WorkHandoverId,
                       x.SortIndex,
                       x.HandoverContent,
                       x.Num,
                   };
        }

        /// <summary>
        /// 增加月报质量验收情况
        /// </summary>
        /// <param name="managerRuleApprove">月报质量验收情况实体</param>
        public static void AddMonthSpotCheckDetail(Model.ZHGL_WorkHandoverDetail monthSpotCheckDetail)
        {
            Model.SGGLDB db = Funs.DB;
            Model.ZHGL_WorkHandoverDetail newApprove = new Model.ZHGL_WorkHandoverDetail();
            newApprove.WorkHandoverDetailId = monthSpotCheckDetail.WorkHandoverDetailId;
            newApprove.WorkHandoverId = monthSpotCheckDetail.WorkHandoverId;
            newApprove.SortIndex = monthSpotCheckDetail.SortIndex;
            newApprove.HandoverContent = monthSpotCheckDetail.HandoverContent;
            newApprove.Num = monthSpotCheckDetail.Num;

            db.ZHGL_WorkHandoverDetail.InsertOnSubmit(newApprove);
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据月报id删除对应的所有月报质量验收情况
        /// </summary>
        /// <param name="WorkHandoverId">月报质量验收情况编号</param>
        public static void DeleteMonthSpotCheckDetailsByWorkHandoverId(string WorkHandoverId)
        {
            Model.SGGLDB db = Funs.DB;
            var q = (from x in db.ZHGL_WorkHandoverDetail where x.WorkHandoverId == WorkHandoverId select x).ToList();
            if (q.Count() > 0)
            {
                db.ZHGL_WorkHandoverDetail.DeleteAllOnSubmit(q);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据月报id获取对应的所有月报质量验收情况
        /// </summary>
        /// <param name="WorkHandoverId">月报质量验收情况编号</param>
        public static List<Model.ZHGL_WorkHandoverDetail> GetWorkHandoverDetailsByWorkHandoverId(string WorkHandoverId)
        {
            Model.SGGLDB db = Funs.DB;
            return (from x in db.ZHGL_WorkHandoverDetail where x.WorkHandoverId == WorkHandoverId orderby x.SortIndex select x).ToList();
        }
    }
}
