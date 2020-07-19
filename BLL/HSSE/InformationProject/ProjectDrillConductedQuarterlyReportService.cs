using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 应急演练开展情况季报
    /// </summary>
    public static class ProjectDrillConductedQuarterlyReportService
    {
        public static Model.SGGLDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取应急演练开展情况季报表
        /// </summary>
        /// <param name="drillConductedQuarterlyReportId"></param>
        /// <returns></returns>
        public static Model.InformationProject_DrillConductedQuarterlyReport GetDrillConductedQuarterlyReportById(string drillConductedQuarterlyReportId)
        {
            return Funs.DB.InformationProject_DrillConductedQuarterlyReport.FirstOrDefault(e => e.DrillConductedQuarterlyReportId == drillConductedQuarterlyReportId);
        }

        /// <summary>
        /// 添加应急演练开展情况季报表
        /// </summary>
        /// <param name="drillConductedQuarterlyReport"></param>
        public static void AddDrillConductedQuarterlyReport(Model.InformationProject_DrillConductedQuarterlyReport drillConductedQuarterlyReport)
        {
            Model.SGGLDB db = Funs.DB;
            Model.InformationProject_DrillConductedQuarterlyReport newDrillConductedQuarterlyReport = new Model.InformationProject_DrillConductedQuarterlyReport
            {
                DrillConductedQuarterlyReportId = drillConductedQuarterlyReport.DrillConductedQuarterlyReportId,
                ProjectId = drillConductedQuarterlyReport.ProjectId,
                UnitId = drillConductedQuarterlyReport.UnitId,
                YearId = drillConductedQuarterlyReport.YearId,
                Quarter = drillConductedQuarterlyReport.Quarter,
                CompileMan = drillConductedQuarterlyReport.CompileMan,
                CompileDate = drillConductedQuarterlyReport.CompileDate,
                States = drillConductedQuarterlyReport.States
            };
            db.InformationProject_DrillConductedQuarterlyReport.InsertOnSubmit(newDrillConductedQuarterlyReport);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改应急演练开展情况季报表
        /// </summary>
        /// <param name="drillConductedQuarterlyReport"></param>
        public static void UpdateDrillConductedQuarterlyReport(Model.InformationProject_DrillConductedQuarterlyReport drillConductedQuarterlyReport)
        {
            Model.SGGLDB db = Funs.DB;
            Model.InformationProject_DrillConductedQuarterlyReport newDrillConductedQuarterlyReport = db.InformationProject_DrillConductedQuarterlyReport.FirstOrDefault(e => e.DrillConductedQuarterlyReportId == drillConductedQuarterlyReport.DrillConductedQuarterlyReportId);
            if (newDrillConductedQuarterlyReport != null)
            {
                newDrillConductedQuarterlyReport.ProjectId = drillConductedQuarterlyReport.ProjectId;
                newDrillConductedQuarterlyReport.UnitId = drillConductedQuarterlyReport.UnitId;
                newDrillConductedQuarterlyReport.YearId = drillConductedQuarterlyReport.YearId;
                newDrillConductedQuarterlyReport.Quarter = drillConductedQuarterlyReport.Quarter;
                newDrillConductedQuarterlyReport.CompileMan = drillConductedQuarterlyReport.CompileMan;
                newDrillConductedQuarterlyReport.CompileDate = drillConductedQuarterlyReport.CompileDate;
                newDrillConductedQuarterlyReport.States = drillConductedQuarterlyReport.States;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除应急演练开展情况季报表
        /// </summary>
        /// <param name="drillConductedQuarterlyReportId"></param>
        public static void DeleteDrillConductedQuarterlyReportById(string drillConductedQuarterlyReportId)
        {
            Model.SGGLDB db = Funs.DB;
            Model.InformationProject_DrillConductedQuarterlyReport drillConductedQuarterlyReport = db.InformationProject_DrillConductedQuarterlyReport.FirstOrDefault(e => e.DrillConductedQuarterlyReportId == drillConductedQuarterlyReportId);
            if (drillConductedQuarterlyReport != null)
            {
                CommonService.DeleteFlowOperateByID(drillConductedQuarterlyReportId);
                db.InformationProject_DrillConductedQuarterlyReport.DeleteOnSubmit(drillConductedQuarterlyReport);
                db.SubmitChanges();
            }
        }
    }
}