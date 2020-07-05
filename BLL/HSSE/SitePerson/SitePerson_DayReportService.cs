using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace BLL
{
    public class SitePerson_DayReportService
    {
        /// <summary>
        /// 获取工作日报信息
        /// </summary>
        /// <param name="dayReportId">工作日报Id</param>
        /// <returns></returns>
        public static Model.SitePerson_DayReport GetDayReportByDayReportId(string dayReportId)
        {
            return Funs.DB.SitePerson_DayReport.FirstOrDefault(x => x.DayReportId == dayReportId);
        }

        /// <summary>
        /// 增加工作日报信息
        /// </summary>
        /// <param name="dayReport">工作日报实体</param>
        public static void AddDayReport(Model.SitePerson_DayReport dayReport)
        {
            Model.SGGLDB db = Funs.DB;
            Model.SitePerson_DayReport newDayReport = new Model.SitePerson_DayReport
            {
                DayReportId = dayReport.DayReportId,
                ProjectId = dayReport.ProjectId,
                CompileMan = dayReport.CompileMan,
                CompileDate = dayReport.CompileDate,
                States = dayReport.States
            };
            db.SitePerson_DayReport.InsertOnSubmit(newDayReport);
            db.SubmitChanges();

            ////增加一条编码记录
            BLL.CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(BLL.Const.DayReportMenuId, dayReport.ProjectId, null, dayReport.DayReportId, dayReport.CompileDate);
        }

        /// <summary>
        /// 修改工作日报信息
        /// </summary>
        /// <param name="dayReport">工作日报实体</param>
        public static void UpdateDayReport(Model.SitePerson_DayReport dayReport)
        {
            Model.SGGLDB db = Funs.DB;
            Model.SitePerson_DayReport newDayReport = db.SitePerson_DayReport.First(e => e.DayReportId == dayReport.DayReportId);
            newDayReport.ProjectId = dayReport.ProjectId;
            newDayReport.CompileMan = dayReport.CompileMan;
            newDayReport.CompileDate = dayReport.CompileDate;
            newDayReport.States = dayReport.States;

            db.SubmitChanges();
        }

        /// <summary>
        /// 根据工作日报主键删除一个工作日报信息
        /// </summary>
        /// <param name="dayReportId">工作日报主键</param>
        public static void DeleteDayReportByDayReportId(string dayReportId)
        {
            Model.SGGLDB db = Funs.DB;
            Model.SitePerson_DayReport dayReport = db.SitePerson_DayReport.FirstOrDefault(e => e.DayReportId == dayReportId);
            if (dayReport != null)
            {
                ///删除编码表记录
                BLL.CodeRecordsService.DeleteCodeRecordsByDataId(dayReportId);
                BLL.CommonService.DeleteFlowOperateByID(dayReportId);
                db.SitePerson_DayReport.DeleteOnSubmit(dayReport);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 判断人工月报是否存在
        /// </summary>
        /// <param name="monthDate"></param>
        /// <param name="projectId"></param>
        /// <returns>true 存在；false：不存在</returns>
        public static bool IsExistDayReport(DateTime compileDate, string projectId)
        {
            var q = from x in Funs.DB.SitePerson_DayReport
                    where x.CompileDate == compileDate && x.ProjectId == projectId
                    select x;
            if (q.Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 根据时间获取工作日报信息
        /// </summary>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns>工作日报信息</returns>
        public static List<Model.SitePerson_DayReport> GetDayReportsByCompileDate(DateTime startTime, DateTime endTime, string projectId)
        {
            return (from x in Funs.DB.SitePerson_DayReport where x.CompileDate >= startTime && x.CompileDate < endTime && x.ProjectId == projectId && x.States == BLL.Const.State_2 select x).ToList();
        }

        /// <summary>
        /// 根据时间获取工作日报信息
        /// </summary>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns>工作日报信息</returns>
        public static List<Model.SitePerson_DayReport> GetDayReportsByCompileDate2(DateTime startTime, string projectId)
        {
            return (from x in Funs.DB.SitePerson_DayReport where x.CompileDate >= startTime && x.ProjectId == projectId && x.States == BLL.Const.State_2 select x).ToList();
        }

        /// <summary>
        /// 获取工作日报信息
        /// </summary>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns>工作日报信息</returns>
        public static List<Model.SitePerson_DayReport> GetDayReports(string projectId)
        {
            return (from x in Funs.DB.SitePerson_DayReport where x.ProjectId == projectId && x.States == BLL.Const.State_2 select x).ToList();
        }

        /// <summary>
        /// 根据时间获取工作日报信息
        /// </summary>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns>工作日报信息</returns>
        public static List<Model.SitePerson_DayReport> GetDayReportsByCompileDateAndUnitId(DateTime startTime, DateTime endTime, string projectId, string unitId)
        {
            return (from x in Funs.DB.SitePerson_DayReport
                    join y in Funs.DB.SitePerson_DayReportDetail
                    on x.DayReportId equals y.DayReportId
                    where x.CompileDate >= startTime && x.CompileDate < endTime && x.ProjectId == projectId && x.States == BLL.Const.State_2
                    && y.UnitId == unitId
                    select x).Distinct().ToList();
        }
    }
}
