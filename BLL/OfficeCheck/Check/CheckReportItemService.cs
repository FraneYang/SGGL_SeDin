using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    /// <summary>
    /// 检查报告明细
    /// </summary>
    public static class CheckReportItemService
    {
        /// <summary>
        /// 添加检查报告明细
        /// </summary>
        /// <param name="checkReportItem"></param>
        public static void AddCheckReportItem(Model.ProjectSupervision_CheckReportItem checkReportItem)
        {
            Model.ProjectSupervision_CheckReportItem newCheckItem = new Model.ProjectSupervision_CheckReportItem();
            newCheckItem.CheckReportItemId = checkReportItem.CheckReportItemId;
            newCheckItem.CheckReportId = checkReportItem.CheckReportId;
            newCheckItem.CheckReportCode = checkReportItem.CheckReportCode;
            newCheckItem.UnConformItem = checkReportItem.UnConformItem;
            Funs.DB.ProjectSupervision_CheckReportItem.InsertOnSubmit(newCheckItem);
            Funs.DB.SubmitChanges();
        }

        /// <summary>
        /// 根据检查报告Id删除相关明细信息
        /// </summary>
        /// <param name="checkReportId"></param>
        public static void DeleteCheckReportItemByCheckReportId(string checkReportId)
        {
            var q = (from x in Funs.DB.ProjectSupervision_CheckReportItem where x.CheckReportId == checkReportId select x).ToList();
            if (q != null)
            {
                Funs.DB.ProjectSupervision_CheckReportItem.DeleteAllOnSubmit(q);
                Funs.DB.SubmitChanges();
            }
        }
    }
}
