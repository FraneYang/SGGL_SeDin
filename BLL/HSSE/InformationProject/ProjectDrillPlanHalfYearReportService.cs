using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 应急演练工作计划半年报
    /// </summary>
    public static class ProjectDrillPlanHalfYearReportService
    {
        public static Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);

        /// <summary>
        /// 根据主键获取应急演练工作计划半年报
        /// </summary>
        /// <param name="drillPlanHalfYearReportId"></param>
        /// <returns></returns>
        public static Model.InformationProject_DrillPlanHalfYearReport GetDrillPlanHalfYearReportById(string drillPlanHalfYearReportId)
        {
            return new Model.SGGLDB(Funs.ConnString).InformationProject_DrillPlanHalfYearReport.FirstOrDefault(e => e.DrillPlanHalfYearReportId == drillPlanHalfYearReportId);
        }

        /// <summary>
        /// 添加应急演练工作计划半年报
        /// </summary>
        /// <param name="drillPlanHalfYearReport"></param>
        public static void AddDrillPlanHalfYearReport(Model.InformationProject_DrillPlanHalfYearReport drillPlanHalfYearReport)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.InformationProject_DrillPlanHalfYearReport newDrillPlanHalfYearReport = new Model.InformationProject_DrillPlanHalfYearReport
            {
                DrillPlanHalfYearReportId = drillPlanHalfYearReport.DrillPlanHalfYearReportId,
                ProjectId = drillPlanHalfYearReport.ProjectId,
                UnitId = drillPlanHalfYearReport.UnitId,
                YearId = drillPlanHalfYearReport.YearId,
                HalfYearId = drillPlanHalfYearReport.HalfYearId,
                Telephone = drillPlanHalfYearReport.Telephone,
                CompileMan = drillPlanHalfYearReport.CompileMan,
                CompileDate = drillPlanHalfYearReport.CompileDate,
                States = drillPlanHalfYearReport.States
            };
            db.InformationProject_DrillPlanHalfYearReport.InsertOnSubmit(newDrillPlanHalfYearReport);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改应急演练工作计划半年报
        /// </summary>
        /// <param name="drillPlanHalfYearReport"></param>
        public static void UpdateDrillPlanHalfYearReport(Model.InformationProject_DrillPlanHalfYearReport drillPlanHalfYearReport)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.InformationProject_DrillPlanHalfYearReport newDrillPlanHalfYearReport = db.InformationProject_DrillPlanHalfYearReport.FirstOrDefault(e => e.DrillPlanHalfYearReportId == drillPlanHalfYearReport.DrillPlanHalfYearReportId);
            if (newDrillPlanHalfYearReport != null)
            {
                newDrillPlanHalfYearReport.ProjectId = drillPlanHalfYearReport.ProjectId;
                newDrillPlanHalfYearReport.UnitId = drillPlanHalfYearReport.UnitId;
                newDrillPlanHalfYearReport.YearId = drillPlanHalfYearReport.YearId;
                newDrillPlanHalfYearReport.HalfYearId = drillPlanHalfYearReport.HalfYearId;
                newDrillPlanHalfYearReport.Telephone = drillPlanHalfYearReport.Telephone;
                newDrillPlanHalfYearReport.CompileMan = drillPlanHalfYearReport.CompileMan;
                newDrillPlanHalfYearReport.CompileDate = drillPlanHalfYearReport.CompileDate;
                newDrillPlanHalfYearReport.States = drillPlanHalfYearReport.States;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除应急演练工作计划半年报
        /// </summary>
        /// <param name="drillPlanHalfYearReportId"></param>
        public static void DeleteDrillPlanHalfYearReportById(string drillPlanHalfYearReportId)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.InformationProject_DrillPlanHalfYearReport drillPlanHalfYearReport = db.InformationProject_DrillPlanHalfYearReport.FirstOrDefault(e => e.DrillPlanHalfYearReportId == drillPlanHalfYearReportId);
            if (drillPlanHalfYearReport != null)
            {
                CommonService.DeleteFlowOperateByID(drillPlanHalfYearReportId);
                db.InformationProject_DrillPlanHalfYearReport.DeleteOnSubmit(drillPlanHalfYearReport);
                db.SubmitChanges();
            }
        }
    }
}