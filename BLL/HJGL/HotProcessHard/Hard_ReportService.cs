using Model;
using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    public static class Hard_ReportService
    {
        /// <summary>
        ///获取硬度报告信息
        /// </summary>
        /// <returns></returns>
        public static Model.HJGL_Hard_Report GetHardReportByHardReportId(string strHardReportId)
        {
            return Funs.DB.HJGL_Hard_Report.FirstOrDefault(e => e.HardReportId == strHardReportId);
        }

        /// <summary>
        ///获取硬度报告视图信息
        /// </summary>
        /// <returns></returns>
        public static Model.View_HJGL_Hard_Report GetViewHardReportByHardReportId(string strHardReportId)
        {
            return Funs.DB.View_HJGL_Hard_Report.FirstOrDefault(e => e.HardReportId == strHardReportId);
        }

        /// <summary>
        /// 增加硬度报告信息
        /// </summary>
        /// <param name="setHardReport"></param>
        public static void AddHard_Report(Model.HJGL_Hard_Report setHardReport)
        {
            Model.SGGLDB db = Funs.DB;
            Model.HJGL_Hard_Report newHardReport = new HJGL_Hard_Report
            {
                HardReportId = setHardReport.HardReportId,
                HardTrustItemID = setHardReport.HardTrustItemID,
                WeldJointId = setHardReport.WeldJointId,
                HardReportNo = setHardReport.HardReportNo,
                TestingPointNo = setHardReport.TestingPointNo,
                HardNessValue1 = setHardReport.HardNessValue1,
                HardNessValue2 = setHardReport.HardNessValue2,
                HardNessValue3 = setHardReport.HardNessValue3,
                Remark = setHardReport.Remark,
            };

            db.HJGL_Hard_Report.InsertOnSubmit(newHardReport);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改硬度报告信息 
        /// </summary>
        /// <param name="updateHardReport"></param>
        public static void UpdateHard_Report(Model.HJGL_Hard_Report updateHardReport)
        {
            Model.SGGLDB db = Funs.DB;
            Model.HJGL_Hard_Report newHardReport = db.HJGL_Hard_Report.FirstOrDefault(e => e.HardReportId == updateHardReport.HardReportId);
            if (newHardReport != null)
            {
                newHardReport.HardReportNo = updateHardReport.HardReportNo;
                newHardReport.TestingPointNo = updateHardReport.TestingPointNo;
                newHardReport.HardNessValue1 = updateHardReport.HardNessValue1;
                newHardReport.HardNessValue2 = updateHardReport.HardNessValue2;
                newHardReport.HardNessValue3 = updateHardReport.HardNessValue3;
                newHardReport.Remark = updateHardReport.Remark;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据硬度报告Id删除一个硬度报告信息
        /// </summary>
        /// <param name="strHardReportId">装置ID</param>
        public static void DeleteHard_ReportByHardReportId(string strHardReportId)
        {
            Model.SGGLDB db = Funs.DB;
            Model.HJGL_Hard_Report delHardReport = db.HJGL_Hard_Report.FirstOrDefault(e => e.HardReportId == strHardReportId);
            if (delHardReport != null)
            {
                db.HJGL_Hard_Report.DeleteOnSubmit(delHardReport);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据硬度委托明细Id删除一个硬度报告信息
        /// </summary>
        /// <param name="strHardReportId">装置ID</param>
        public static void DeleteHard_ReportsByHardTrustItemID(string strHardTrustItemID)
        {
            Model.SGGLDB db = Funs.DB;
            var delHardReports = from x in db.HJGL_Hard_Report where x.HardTrustItemID == strHardTrustItemID select x;
            if (delHardReports.Count() >0)
            {
                db.HJGL_Hard_Report.DeleteAllOnSubmit(delHardReports);
                db.SubmitChanges();
            }
        }
    }
}
