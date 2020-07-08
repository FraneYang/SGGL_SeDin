using System.Linq;

namespace BLL
{
    /// <summary>
    /// 热处理报告
    /// </summary>
    public static class HotProessReportService
    {
        /// <summary>
        /// 根据主键获取热处理报告
        /// </summary>
        /// <param name="hotProessReportId"></param>
        /// <returns></returns>
        public static Model.HJGL_HotProess_Report GetHotProessReportById(string hotProessReportId)
        {
            return new Model.SGGLDB(Funs.ConnString).HJGL_HotProess_Report.FirstOrDefault(e => e.HotProessReportId == hotProessReportId);
        }

        /// <summary>
        /// 添加热处理报告
        /// </summary>
        /// <param name="hotProessReport"></param>
        public static void AddHotProessReport(Model.HJGL_HotProess_Report hotProessReport)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.HJGL_HotProess_Report newHotProessReport = new Model.HJGL_HotProess_Report();
            newHotProessReport.HotProessReportId = hotProessReport.HotProessReportId;
            newHotProessReport.HotProessTrustItemId = hotProessReport.HotProessTrustItemId;
            newHotProessReport.WeldJointId = hotProessReport.WeldJointId;
            newHotProessReport.PointCount = hotProessReport.PointCount;
            newHotProessReport.RequiredT = hotProessReport.RequiredT;
            newHotProessReport.ActualT = hotProessReport.ActualT;
            newHotProessReport.RequestTime = hotProessReport.RequestTime;
            newHotProessReport.ActualTime = hotProessReport.ActualTime;
            newHotProessReport.RecordChartNo = hotProessReport.RecordChartNo;
            db.HJGL_HotProess_Report.InsertOnSubmit(newHotProessReport);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改热处理报告
        /// </summary>
        /// <param name="hotProessReport"></param>
        public static void UpdateHotProessReport(Model.HJGL_HotProess_Report hotProessReport)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.HJGL_HotProess_Report newHotProessReport = db.HJGL_HotProess_Report.FirstOrDefault(e => e.HotProessReportId == hotProessReport.HotProessReportId);
            if (newHotProessReport != null)
            {
                newHotProessReport.PointCount = hotProessReport.PointCount;
                newHotProessReport.RequiredT = hotProessReport.RequiredT;
                newHotProessReport.ActualT = hotProessReport.ActualT;
                newHotProessReport.RequestTime = hotProessReport.RequestTime;
                newHotProessReport.ActualTime = hotProessReport.ActualTime;
                newHotProessReport.RecordChartNo = hotProessReport.RecordChartNo;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除热处理报告
        /// </summary>
        /// <param name="hotProessReportId"></param>
        public static void DeleteHotProessReportById(string hotProessReportId)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.HJGL_HotProess_Report hotProessReport = db.HJGL_HotProess_Report.FirstOrDefault(e => e.HotProessReportId == hotProessReportId);
            if (hotProessReport != null)
            {
                db.HJGL_HotProess_Report.DeleteOnSubmit(hotProessReport);
                db.SubmitChanges();
            }
        }
    }
}
