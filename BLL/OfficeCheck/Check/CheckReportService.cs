using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    /// <summary>
    /// 检查报告
    /// </summary>
    public static class CheckReportService
    {
        /// <summary>
        /// 根据检查Id获取检查报告
        /// </summary>
        /// <param name="checkNoticeId"></param>
        /// <returns></returns>
        public static Model.ProjectSupervision_CheckReport GetCheckReportByCheckNoticeId(string checkNoticeId)
        {
            return Funs.DB.ProjectSupervision_CheckReport.FirstOrDefault(e => e.CheckNoticeId == checkNoticeId);
        }

        /// <summary>
        /// 添加检查报告
        /// </summary>
        /// <param name="checkReport"></param>
        public static void AddCheckReport(Model.ProjectSupervision_CheckReport checkReport)
        {
            Model.ProjectSupervision_CheckReport newCheckReport = new Model.ProjectSupervision_CheckReport();
            newCheckReport.CheckReportId = checkReport.CheckReportId;
            newCheckReport.CheckNoticeId = checkReport.CheckNoticeId;
            newCheckReport.CheckPurpose = checkReport.CheckPurpose;
            newCheckReport.Basis = checkReport.Basis;
            newCheckReport.BasicInfo = checkReport.BasicInfo;
            newCheckReport.ConformItem = checkReport.ConformItem;
            newCheckReport.Opinion = checkReport.Opinion;
            newCheckReport.CheckResult = checkReport.CheckResult;
            Funs.DB.ProjectSupervision_CheckReport.InsertOnSubmit(newCheckReport);
            Funs.DB.SubmitChanges();
        }

        /// <summary>
        /// 修改检查报告
        /// </summary>
        /// <param name="checkReport"></param>
        public static void UpdateCheckReport(Model.ProjectSupervision_CheckReport checkReport)
        {
            Model.ProjectSupervision_CheckReport newCheckReport = Funs.DB.ProjectSupervision_CheckReport.FirstOrDefault(e => e.CheckReportId == checkReport.CheckReportId);
            if (newCheckReport != null)
            {
                newCheckReport.CheckPurpose = checkReport.CheckPurpose;
                newCheckReport.Basis = checkReport.Basis;
                newCheckReport.BasicInfo = checkReport.BasicInfo;
                newCheckReport.ConformItem = checkReport.ConformItem;
                newCheckReport.Opinion = checkReport.Opinion;
                newCheckReport.CheckResult = checkReport.CheckResult;
                Funs.DB.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据检查主键删除检查报告信息
        /// </summary>
        /// <param name="checkNoticeId"></param>
        public static void DeleteCheckReportByCheckNoticeId(string checkNoticeId)
        {
            Model.ProjectSupervision_CheckReport checkReport = Funs.DB.ProjectSupervision_CheckReport.FirstOrDefault(e => e.CheckNoticeId == checkNoticeId);
            if (checkReport != null)
            {
                Funs.DB.ProjectSupervision_CheckReport.DeleteOnSubmit(checkReport);
                Funs.DB.SubmitChanges();
            }
        }
    }
}
