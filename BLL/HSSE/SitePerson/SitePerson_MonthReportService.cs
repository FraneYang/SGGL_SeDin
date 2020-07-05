using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace BLL
{
    public class SitePerson_MonthReportService
    {
        /// <summary>
        /// 获取工作月报信息
        /// </summary>
        /// <param name="monthReportId">工作月报Id</param>
        /// <returns></returns>
        public static Model.SitePerson_MonthReport GetMonthReportByMonthReportId(string monthReportId)
        {
            return Funs.DB.SitePerson_MonthReport.FirstOrDefault(x => x.MonthReportId == monthReportId);
        }

        /// <summary>
        /// 增加工作月报信息
        /// </summary>
        /// <param name="monthReport">工作月报实体</param>
        public static void AddMonthReport(Model.SitePerson_MonthReport monthReport)
        {
            Model.SGGLDB db = Funs.DB;
            Model.SitePerson_MonthReport newMonthReport = new Model.SitePerson_MonthReport
            {
                MonthReportId = monthReport.MonthReportId,
                ProjectId = monthReport.ProjectId,
                CompileMan = monthReport.CompileMan,
                CompileDate = monthReport.CompileDate,
                States = monthReport.States
            };
            db.SitePerson_MonthReport.InsertOnSubmit(newMonthReport);
            db.SubmitChanges();

            ////增加一条编码记录
            BLL.CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(BLL.Const.ProjectMonthReportMenuId, monthReport.ProjectId, null, monthReport.MonthReportId, monthReport.CompileDate);
        }

        /// <summary>
        /// 修改工作月报信息
        /// </summary>
        /// <param name="monthReport">工作月报实体</param>
        public static void UpdateMonthReport(Model.SitePerson_MonthReport monthReport)
        {
            Model.SGGLDB db = Funs.DB;
            Model.SitePerson_MonthReport newMonthReport = db.SitePerson_MonthReport.FirstOrDefault(e => e.MonthReportId == monthReport.MonthReportId);
            if (newMonthReport != null)
            {
                //newMonthReport.ProjectId = monthReport.ProjectId;
                newMonthReport.CompileMan = monthReport.CompileMan;
                newMonthReport.CompileDate = monthReport.CompileDate;
                newMonthReport.States = monthReport.States;

                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据工作月报主键删除一个工作月报信息
        /// </summary>
        /// <param name="monthReportId">工作月报主键</param>
        public static void DeleteMonthReportByMonthReportId(string monthReportId)
        {
            Model.SGGLDB db = Funs.DB;
            Model.SitePerson_MonthReport monthReport = db.SitePerson_MonthReport.FirstOrDefault(e => e.MonthReportId == monthReportId);
            if (monthReport != null)
            {
                ///删除编码表记录
                BLL.CodeRecordsService.DeleteCodeRecordsByDataId(monthReportId);
                BLL.CommonService.DeleteFlowOperateByID(monthReportId);
                db.SitePerson_MonthReport.DeleteOnSubmit(monthReport);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 判断人工月报是否存在
        /// </summary>
        /// <param name="monthDate"></param>
        /// <param name="projectId"></param>
        /// <returns>true 存在；false：不存在</returns>
        public static bool IsExistMonthReport(DateTime compileDate, string projectId)
        {
            var q = from x in Funs.DB.SitePerson_MonthReport
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
        /// 根据时间获取工作月报信息
        /// </summary>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns>工作月报信息</returns>
        public static Model.SitePerson_MonthReport GetMonthReportsByCompileDate(DateTime startTime, string projectId)
        {
            return Funs.DB.SitePerson_MonthReport.FirstOrDefault(x => x.CompileDate.Value.Year >= startTime.Year && x.CompileDate.Value.Month >= startTime.Month && x.ProjectId == projectId && x.States == BLL.Const.State_2);
        }
    }
}
