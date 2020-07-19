using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 职工伤亡事故原因分析报
    /// </summary>
    public static class ProjectAccidentCauseReportService
    {
        public static Model.SGGLDB db = Funs.DB;

        /// <summary>
        /// 职工伤亡事故原因分析报表
        /// </summary>
        /// <param name="AccidentCauseReportId">职工伤亡事故原因分析报表Id</param>
        /// <returns>职工伤亡事故原因分析报表</returns>
        public static Model.InformationProject_AccidentCauseReport GetAccidentCauseReportById(string accidentCauseReportId)
        {
            return Funs.DB.InformationProject_AccidentCauseReport.FirstOrDefault(e => e.AccidentCauseReportId == accidentCauseReportId);
        }

        /// <summary>
        /// 增加职工伤亡事故原因分析报表
        /// </summary>
        /// <param name="AccidentCauseReport">职工伤亡事故原因分析报表实体</param>
        public static void AddAccidentCauseReport(Model.InformationProject_AccidentCauseReport AccidentCauseReport)
        {
            Model.SGGLDB db = Funs.DB;
            Model.InformationProject_AccidentCauseReport newAccidentCauseReport = new Model.InformationProject_AccidentCauseReport
            {
                AccidentCauseReportId = AccidentCauseReport.AccidentCauseReportId,
                ProjectId = AccidentCauseReport.ProjectId,
                AccidentCauseReportCode = AccidentCauseReport.AccidentCauseReportCode,
                Year = AccidentCauseReport.Year,
                Month = AccidentCauseReport.Month,
                UnitId = AccidentCauseReport.UnitId,
                DeathAccident = AccidentCauseReport.DeathAccident,
                DeathToll = AccidentCauseReport.DeathToll,
                InjuredAccident = AccidentCauseReport.InjuredAccident,
                InjuredToll = AccidentCauseReport.InjuredToll,
                MinorWoundAccident = AccidentCauseReport.MinorWoundAccident,
                MinorWoundToll = AccidentCauseReport.MinorWoundToll,
                AverageTotalHours = AccidentCauseReport.AverageTotalHours,
                AverageManHours = AccidentCauseReport.AverageManHours,
                TotalLossMan = AccidentCauseReport.TotalLossMan,
                LastMonthLossHoursTotal = AccidentCauseReport.LastMonthLossHoursTotal,
                KnockOffTotal = AccidentCauseReport.KnockOffTotal,
                DirectLoss = AccidentCauseReport.DirectLoss,
                IndirectLosses = AccidentCauseReport.IndirectLosses,
                TotalLoss = AccidentCauseReport.TotalLoss,
                TotalLossTime = AccidentCauseReport.TotalLossTime,
                CompileMan = AccidentCauseReport.CompileMan,
                CompileDate = AccidentCauseReport.CompileDate,
                States = AccidentCauseReport.States
            };

            db.InformationProject_AccidentCauseReport.InsertOnSubmit(newAccidentCauseReport);
            db.SubmitChanges();
            CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(Const.ProjectAccidentCauseReportMenuId, AccidentCauseReport.ProjectId, null, AccidentCauseReport.AccidentCauseReportId, AccidentCauseReport.CompileDate);
        }

        /// <summary>
        /// 修改职工伤亡事故原因分析报表
        /// </summary>
        /// <param name="AccidentCauseReport">职工伤亡事故原因分析报表实体</param>
        public static void UpdateAccidentCauseReport(Model.InformationProject_AccidentCauseReport AccidentCauseReport)
        {
            Model.SGGLDB db = Funs.DB;
            Model.InformationProject_AccidentCauseReport newAccidentCauseReport = db.InformationProject_AccidentCauseReport.FirstOrDefault(e => e.AccidentCauseReportId == AccidentCauseReport.AccidentCauseReportId);
            if (newAccidentCauseReport != null)
            {
                newAccidentCauseReport.ProjectId = AccidentCauseReport.ProjectId;
                newAccidentCauseReport.AccidentCauseReportCode = AccidentCauseReport.AccidentCauseReportCode;
                newAccidentCauseReport.Year = AccidentCauseReport.Year;
                newAccidentCauseReport.Month = AccidentCauseReport.Month;
                newAccidentCauseReport.UnitId = AccidentCauseReport.UnitId;
                newAccidentCauseReport.DeathAccident = AccidentCauseReport.DeathAccident;
                newAccidentCauseReport.DeathToll = AccidentCauseReport.DeathToll;
                newAccidentCauseReport.InjuredAccident = AccidentCauseReport.InjuredAccident;
                newAccidentCauseReport.InjuredToll = AccidentCauseReport.InjuredToll;
                newAccidentCauseReport.MinorWoundAccident = AccidentCauseReport.MinorWoundAccident;
                newAccidentCauseReport.MinorWoundToll = AccidentCauseReport.MinorWoundToll;
                newAccidentCauseReport.AverageTotalHours = AccidentCauseReport.AverageTotalHours;
                newAccidentCauseReport.AverageManHours = AccidentCauseReport.AverageManHours;
                newAccidentCauseReport.TotalLossMan = AccidentCauseReport.TotalLossMan;
                newAccidentCauseReport.LastMonthLossHoursTotal = AccidentCauseReport.LastMonthLossHoursTotal;
                newAccidentCauseReport.KnockOffTotal = AccidentCauseReport.KnockOffTotal;
                newAccidentCauseReport.DirectLoss = AccidentCauseReport.DirectLoss;
                newAccidentCauseReport.IndirectLosses = AccidentCauseReport.IndirectLosses;
                newAccidentCauseReport.TotalLoss = AccidentCauseReport.TotalLoss;
                newAccidentCauseReport.TotalLossTime = AccidentCauseReport.TotalLossTime;
                newAccidentCauseReport.CompileMan = AccidentCauseReport.CompileMan;
                newAccidentCauseReport.CompileDate = AccidentCauseReport.CompileDate;
                newAccidentCauseReport.States = AccidentCauseReport.States;

                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据Id获取数据
        /// </summary>
        /// <param name="AccidentCauseReportId"></param>
        public static void DeleteAccidentCauseReportById(string AccidentCauseReportId)
        {
            Model.SGGLDB db = Funs.DB;
            Model.InformationProject_AccidentCauseReport newAccidentCauseReport = db.InformationProject_AccidentCauseReport.FirstOrDefault(e => e.AccidentCauseReportId == AccidentCauseReportId);
            if (newAccidentCauseReport != null)
            {
                CodeRecordsService.DeleteCodeRecordsByDataId(AccidentCauseReportId);
                CommonService.DeleteFlowOperateByID(AccidentCauseReportId);
                db.InformationProject_AccidentCauseReport.DeleteOnSubmit(newAccidentCauseReport);
                db.SubmitChanges();
            }
        }
    }
}