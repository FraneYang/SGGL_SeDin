using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 事故调查报告
    /// </summary>
    public static class AccidentReportService
    {
        public static Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);

        /// <summary>
        /// 根据主键获取事故调查报告
        /// </summary>
        /// <param name="accidentReportId"></param>
        /// <returns></returns>
        public static Model.Accident_AccidentReport GetAccidentReportById(string accidentReportId)
        {
            return new Model.SGGLDB(Funs.ConnString).Accident_AccidentReport.FirstOrDefault(e => e.AccidentReportId == accidentReportId);
        }

        /// <summary>
        /// 根据时间段获取事故集合
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public static List<Model.Accident_AccidentReport> GetAccidentReportsByAccidentTime(DateTime startTime, DateTime endTime, string projectId)
        {
            return (from x in new Model.SGGLDB(Funs.ConnString).Accident_AccidentReport where x.AccidentDate >= startTime && x.AccidentDate < endTime && x.ProjectId == projectId && x.States == BLL.Const.State_2 select x).ToList();
        }

        /// <summary>
        /// 获取最近时间的轻重死事故时间
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public static Model.Accident_AccidentReport GetMaxAccidentTimeReport(DateTime time, string projectId)
        {
            var a = (from x in new Model.SGGLDB(Funs.ConnString).Accident_AccidentReport
                     where (x.AccidentTypeId == "1" || x.AccidentTypeId == "2" || x.AccidentTypeId == "3")
                      && x.ProjectId == projectId
                      && x.AccidentDate < time
                     orderby x.AccidentDate descending
                     select x).FirstOrDefault();
            return a;
        }

        /// <summary>
        /// 获取时间段内的轻重死事故时间
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public static Model.Accident_AccidentReport GetMaxAccidentTimeReportByTime(DateTime startTime, DateTime endTime)
        {
            var a = (from x in new Model.SGGLDB(Funs.ConnString).Accident_AccidentReport
                     where (x.AccidentTypeId == "1" || x.AccidentTypeId == "2" || x.AccidentTypeId == "3")
                     && x.AccidentDate >= startTime && x.AccidentDate < endTime
                     orderby x.AccidentDate descending
                     select x).FirstOrDefault();
            return a;
        }

        /// <summary>
        /// 根据时间段获取可记录事故集合
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="accidentType"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public static List<Model.Accident_AccidentReport> GetRecordAccidentReportsByAccidentTime(DateTime startTime, DateTime endTime, string projectId)
        {
            return (from x in new Model.SGGLDB(Funs.ConnString).Accident_AccidentReport where x.AccidentDate >= startTime && x.AccidentDate < endTime && x.ProjectId == projectId && (x.AccidentTypeId == "1" || x.AccidentTypeId == "2" || x.AccidentTypeId == "3") && x.States == BLL.Const.State_2 select x).ToList();
        }

        /// <summary>
        /// 根据时间段获取事故集合
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="accidentType"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public static List<Model.Accident_AccidentReport> GetAccidentReportsByAccidentTypeAndTime(DateTime startTime, DateTime endTime, string projectId, string accidentType)
        {
            return (from x in new Model.SGGLDB(Funs.ConnString).Accident_AccidentReport
                    join y in new Model.SGGLDB(Funs.ConnString).Sys_FlowOperate
                    on x.AccidentReportId equals y.DataId
                    where x.AccidentTypeId == accidentType && x.ProjectId == projectId && x.States == BLL.Const.State_2
                    && y.State == BLL.Const.State_2 && y.OperaterTime >= startTime && y.OperaterTime < endTime
                    select x).Distinct().ToList();
        }

        /// <summary>
        /// 根据时间段和类型获取事故数量
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="accidentType"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public static int GetCountByAccidentTimeAndAccidentType(DateTime startTime, DateTime endTime, string accidentType, string projectId)
        {
            return (from x in new Model.SGGLDB(Funs.ConnString).Accident_AccidentReport where x.AccidentDate >= startTime && x.AccidentDate < endTime && x.AccidentTypeId == accidentType && x.ProjectId == projectId && x.States == BLL.Const.State_2 select x).Count();
            //if (accidentType == "1" || accidentType == "2" || accidentType == "3")  //轻重死事故按审批完成时间
            //{
            //    return (from x in new Model.SGGLDB(Funs.ConnString).Accident_AccidentReport
            //            join y in new Model.SGGLDB(Funs.ConnString).Sys_FlowOperate
            //            on x.AccidentReportId equals y.DataId
            //            where x.AccidentTypeId == accidentType && x.ProjectId == projectId && x.States == BLL.Const.State_2
            //            && y.State == BLL.Const.State_2 && y.OperaterTime >= startTime && y.OperaterTime < endTime
            //            select x).Count();
            //}
            //else
            //{
            //    return (from x in new Model.SGGLDB(Funs.ConnString).Accident_AccidentReport where x.AccidentDate >= startTime && x.AccidentDate < endTime && x.AccidentTypeId == accidentType && x.ProjectId == projectId && x.States == BLL.Const.State_2 select x).Count();
            //}
        }

        /// <summary>
        /// 根据时间段和类型获取事故数量
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="accidentType"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public static int GetCountByAccidentType(string accidentType, string projectId)
        {
            return (from x in new Model.SGGLDB(Funs.ConnString).Accident_AccidentReport where x.AccidentTypeId == accidentType && x.ProjectId == projectId && x.States == BLL.Const.State_2 select x).Count();
            //if (accidentType == "1" || accidentType == "2" || accidentType == "3")  //轻重死事故按审批完成时间
            //{
            //    return (from x in new Model.SGGLDB(Funs.ConnString).Accident_AccidentReport
            //            join y in new Model.SGGLDB(Funs.ConnString).Sys_FlowOperate
            //            on x.AccidentReportId equals y.DataId
            //            where x.AccidentTypeId == accidentType && x.ProjectId == projectId && x.States == BLL.Const.State_2
            //            && y.State == BLL.Const.State_2 && y.OperaterTime >= startTime && y.OperaterTime < endTime
            //            select x).Count();
            //}
            //else
            //{
            //    return (from x in new Model.SGGLDB(Funs.ConnString).Accident_AccidentReport where x.AccidentDate >= startTime && x.AccidentDate < endTime && x.AccidentTypeId == accidentType && x.ProjectId == projectId && x.States == BLL.Const.State_2 select x).Count();
            //}
        }

        /// <summary>
        /// 根据时间段和类型获取事故人数
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="accidentType"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public static int GetPersonNumByAccidentTimeAndAccidentType(DateTime startTime, DateTime endTime, string accidentType, string projectId)
        {
            int num = 0;
            var q = from x in new Model.SGGLDB(Funs.ConnString).Accident_AccidentReport where x.AccidentDate >= startTime && x.AccidentDate < endTime && x.AccidentTypeId == accidentType && x.ProjectId == projectId && x.States == BLL.Const.State_2 select x.PeopleNum;
            foreach (var item in q)
            {
                if (item != null)
                {
                    num += Funs.GetNewIntOrZero(item.ToString());
                }
            }
            //if (accidentType == "1" || accidentType == "2" || accidentType == "3")  //轻重死事故按审批完成时间
            //{
            //    var a = from x in new Model.SGGLDB(Funs.ConnString).Accident_AccidentReport
            //            join y in new Model.SGGLDB(Funs.ConnString).Sys_FlowOperate
            //            on x.AccidentReportId equals y.DataId
            //            where x.AccidentTypeId == accidentType && x.ProjectId == projectId && x.States == BLL.Const.State_2
            //            && y.State == BLL.Const.State_2 && y.OperaterTime >= startTime && y.OperaterTime < endTime
            //            select x.PeopleNum;
            //    foreach (var item in a)
            //    {
            //        if (item != null)
            //        {
            //            num += Funs.GetNewIntOrZero(item.ToString());
            //        }
            //    }
            //}
            //else
            //{
            //    var q = from x in new Model.SGGLDB(Funs.ConnString).Accident_AccidentReport where x.AccidentDate >= startTime && x.AccidentDate < endTime && x.AccidentTypeId == accidentType && x.ProjectId == projectId && x.States == BLL.Const.State_2 select x.PeopleNum;
            //    foreach (var item in q)
            //    {
            //        if (item != null)
            //        {
            //            num += Funs.GetNewIntOrZero(item.ToString());
            //        }
            //    }
            //}
            return num;
        }

        /// <summary>
        /// 根据时间段和类型获取事故损失工时
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="accidentType"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public static decimal GetSumLoseWorkTimeByAccidentTimeAndAccidentType(DateTime startTime, DateTime endTime, string accidentType, string projectId)
        {
            decimal loseTime = 0;
            var q = from x in new Model.SGGLDB(Funs.ConnString).Accident_AccidentReport where x.AccidentDate >= startTime && x.AccidentDate < endTime && x.AccidentTypeId == accidentType && x.ProjectId == projectId && x.States == BLL.Const.State_2 select x.WorkingHoursLoss;
            foreach (var item in q)
            {
                if (item != null)
                {
                    loseTime += Funs.GetNewDecimalOrZero(item.ToString());
                }
            }
            //if (accidentType == "1" || accidentType == "2" || accidentType == "3")  //轻重死事故按审批完成时间
            //{
            //    var a = from x in new Model.SGGLDB(Funs.ConnString).Accident_AccidentReport
            //            join y in new Model.SGGLDB(Funs.ConnString).Sys_FlowOperate
            //            on x.AccidentReportId equals y.DataId
            //            where x.AccidentTypeId == accidentType && x.ProjectId == projectId && x.States == BLL.Const.State_2
            //            && y.State == BLL.Const.State_2 && y.OperaterTime >= startTime && y.OperaterTime < endTime
            //            select x.WorkingHoursLoss;
            //    foreach (var item in a)
            //    {
            //        if (item != null)
            //        {
            //            loseTime += Funs.GetNewDecimal(item.ToString());
            //        }
            //    }
            //}
            //else
            //{
            //    var q = from x in new Model.SGGLDB(Funs.ConnString).Accident_AccidentReport where x.AccidentDate >= startTime && x.AccidentDate < endTime && x.AccidentTypeId == accidentType && x.ProjectId == projectId && x.States == BLL.Const.State_2 select x.WorkingHoursLoss;
            //    foreach (var item in q)
            //    {
            //        if (item != null)
            //        {
            //            loseTime += Funs.GetNewDecimal(item.ToString());
            //        }
            //    }
            //}
            return loseTime;
        }

        /// <summary>
        /// 根据时间段和类型获取事故损失金额
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="accidentType"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public static decimal GetSumLosMoneyByAccidentTimeAndAccidentType(DateTime startTime, DateTime endTime, string accidentType, string projectId)
        {
            decimal loseMoney = 0;
            var q = from x in new Model.SGGLDB(Funs.ConnString).Accident_AccidentReport where x.AccidentDate >= startTime && x.AccidentDate < endTime && x.AccidentTypeId == accidentType && x.ProjectId == projectId && x.States == BLL.Const.State_2 select x.EconomicLoss;
            foreach (var item in q)
            {
                if (item != null)
                {
                    loseMoney += Funs.GetNewDecimalOrZero(item.ToString());
                }
            }
            var c = from x in new Model.SGGLDB(Funs.ConnString).Accident_AccidentReport where x.AccidentDate >= startTime && x.AccidentDate < endTime && x.AccidentTypeId == accidentType && x.ProjectId == projectId && x.States == BLL.Const.State_2 select x.EconomicOtherLoss;
            foreach (var item in c)
            {
                if (item != null)
                {
                    loseMoney += Funs.GetNewDecimalOrZero(item.ToString());
                }
            }
            //if (accidentType == "1" || accidentType == "2" || accidentType == "3")  //轻重死事故按审批完成时间
            //{
            //    var a = from x in new Model.SGGLDB(Funs.ConnString).Accident_AccidentReport
            //            join y in new Model.SGGLDB(Funs.ConnString).Sys_FlowOperate
            //            on x.AccidentReportId equals y.DataId
            //            where x.AccidentTypeId == accidentType && x.ProjectId == projectId && x.States == BLL.Const.State_2
            //            && y.State == BLL.Const.State_2 && y.OperaterTime >= startTime && y.OperaterTime < endTime
            //            select x.EconomicLoss;
            //    foreach (var item in a)
            //    {
            //        if (item != null)
            //        {
            //            loseMoney += Funs.GetNewDecimal(item.ToString());
            //        }
            //    }
            //    var b = from x in new Model.SGGLDB(Funs.ConnString).Accident_AccidentReport
            //            join y in new Model.SGGLDB(Funs.ConnString).Sys_FlowOperate
            //            on x.AccidentReportId equals y.DataId
            //            where x.AccidentTypeId == accidentType && x.ProjectId == projectId && x.States == BLL.Const.State_2
            //            && y.State == BLL.Const.State_2 && y.OperaterTime >= startTime && y.OperaterTime < endTime
            //            select x.EconomicOtherLoss;
            //    foreach (var item in b)
            //    {
            //        if (item != null)
            //        {
            //            loseMoney += Funs.GetNewDecimal(item.ToString());
            //        }
            //    }
            //}
            //else
            //{
            //    var q = from x in new Model.SGGLDB(Funs.ConnString).Accident_AccidentReport where x.AccidentDate >= startTime && x.AccidentDate < endTime && x.AccidentTypeId == accidentType && x.ProjectId == projectId && x.States == BLL.Const.State_2 select x.EconomicLoss;
            //    foreach (var item in q)
            //    {
            //        if (item != null)
            //        {
            //            loseMoney += Funs.GetNewDecimal(item.ToString());
            //        }
            //    }
            //    var c = from x in new Model.SGGLDB(Funs.ConnString).Accident_AccidentReport where x.AccidentDate >= startTime && x.AccidentDate < endTime && x.AccidentTypeId == accidentType && x.ProjectId == projectId && x.States == BLL.Const.State_2 select x.EconomicOtherLoss;
            //    foreach (var item in c)
            //    {
            //        if (item != null)
            //        {
            //            loseMoney += Funs.GetNewDecimal(item.ToString());
            //        }
            //    }
            //}
            return loseMoney;
        }

        /// <summary>
        /// 根据时间和事故类型名称获取事故调查报告集合
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="accidentTypeName"></param>
        /// <returns></returns>                        
        public static List<Model.Accident_AccidentReport> GetAccidentReportsByTimeAndAccidentTypeId(DateTime startTime, DateTime endTime, string projectId, string accidentTypeId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                if (accidentTypeId == "1" || accidentTypeId == "2" || accidentTypeId == "3")  //轻重死事故按审批完成时间
                {
                    return (from x in db.Accident_AccidentReport
                            join y in db.Sys_FlowOperate
                            on x.AccidentReportId equals y.DataId
                            where x.AccidentTypeId == accidentTypeId && x.ProjectId == projectId && x.States == BLL.Const.State_2
                            && y.State == BLL.Const.State_2 && y.OperaterTime >= startTime && y.OperaterTime < endTime
                            select x).Distinct().ToList();
                }
                else
                {
                    return (from x in db.Accident_AccidentReport where x.AccidentDate >= startTime && x.AccidentDate < endTime && x.AccidentTypeId == accidentTypeId && x.ProjectId == projectId && x.States == BLL.Const.State_2 select x).ToList();
                }
            }
        }

        /// <summary>
        /// 添加事故调查报告
        /// </summary>
        /// <param name="accidentReport"></param>
        public static void AddAccidentReport(Model.Accident_AccidentReport accidentReport)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Accident_AccidentReport newAccidentReport = new Model.Accident_AccidentReport
            {
                AccidentReportId = accidentReport.AccidentReportId,
                ProjectId = accidentReport.ProjectId,
                AccidentReportCode = accidentReport.AccidentReportCode,
                UnitId = accidentReport.UnitId,
                AccidentReportType = accidentReport.AccidentReportType,
                FileContent = accidentReport.FileContent,
                CompileMan = accidentReport.CompileMan,
                CompileDate = accidentReport.CompileDate,
                States = accidentReport.States,
                AccidentReportName = accidentReport.AccidentReportName,
                AccidentTypeId = accidentReport.AccidentTypeId,
                Abstract = accidentReport.Abstract,
                AccidentDate = accidentReport.AccidentDate,
                WorkAreaId = accidentReport.WorkAreaId,
                PeopleNum = accidentReport.PeopleNum,
                WorkingHoursLoss = accidentReport.WorkingHoursLoss,
                EconomicLoss = accidentReport.EconomicLoss,
                EconomicOtherLoss = accidentReport.EconomicOtherLoss,
                ReportMan = accidentReport.ReportMan,
                ReporterUnit = accidentReport.ReporterUnit,
                ReportDate = accidentReport.ReportDate,
                ProcessDescription = accidentReport.ProcessDescription,
                EmergencyMeasures = accidentReport.EmergencyMeasures,
                WorkArea = accidentReport.WorkArea,
                IsNotConfirm = accidentReport.IsNotConfirm,
                NotConfirmWorkingHoursLoss = accidentReport.NotConfirmWorkingHoursLoss,
                NotConfirmEconomicLoss = accidentReport.NotConfirmEconomicLoss,
                NotConfirmEconomicOtherLoss = accidentReport.NotConfirmEconomicOtherLoss,
                NotConfirmed = accidentReport.NotConfirmed
            };
            db.Accident_AccidentReport.InsertOnSubmit(newAccidentReport);
            db.SubmitChanges();
            CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(Const.ProjectAccidentReportMenuId, accidentReport.ProjectId, null, accidentReport.AccidentReportId, accidentReport.CompileDate);
        }

        /// <summary>
        /// 修改事故调查报告
        /// </summary>
        /// <param name="accidentReport"></param>
        public static void UpdateAccidentReport(Model.Accident_AccidentReport accidentReport)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Accident_AccidentReport newAccidentReport = db.Accident_AccidentReport.FirstOrDefault(e => e.AccidentReportId == accidentReport.AccidentReportId);
            if (newAccidentReport != null)
            {
                //newAccidentReport.ProjectId = accidentReport.ProjectId;
                newAccidentReport.AccidentReportCode = accidentReport.AccidentReportCode;
                newAccidentReport.UnitId = accidentReport.UnitId;
                newAccidentReport.AccidentReportType = accidentReport.AccidentReportType;
                newAccidentReport.FileContent = accidentReport.FileContent;
                newAccidentReport.CompileMan = accidentReport.CompileMan;
                newAccidentReport.CompileDate = accidentReport.CompileDate;
                newAccidentReport.States = accidentReport.States;
                newAccidentReport.AccidentReportName = accidentReport.AccidentReportName;
                newAccidentReport.AccidentTypeId = accidentReport.AccidentTypeId;
                newAccidentReport.Abstract = accidentReport.Abstract;
                newAccidentReport.AccidentDate = accidentReport.AccidentDate;
                newAccidentReport.WorkAreaId = accidentReport.WorkAreaId;
                newAccidentReport.PeopleNum = accidentReport.PeopleNum;
                newAccidentReport.WorkingHoursLoss = accidentReport.WorkingHoursLoss;
                newAccidentReport.EconomicLoss = accidentReport.EconomicLoss;
                newAccidentReport.EconomicOtherLoss = accidentReport.EconomicOtherLoss;
                newAccidentReport.ReportMan = accidentReport.ReportMan;
                newAccidentReport.ReporterUnit = accidentReport.ReporterUnit;
                newAccidentReport.ReportDate = accidentReport.ReportDate;
                newAccidentReport.ProcessDescription = accidentReport.ProcessDescription;
                newAccidentReport.EmergencyMeasures = accidentReport.EmergencyMeasures;
                newAccidentReport.WorkArea = accidentReport.WorkArea;
                newAccidentReport.IsNotConfirm = accidentReport.IsNotConfirm;
                newAccidentReport.NotConfirmWorkingHoursLoss = accidentReport.NotConfirmWorkingHoursLoss;
                newAccidentReport.NotConfirmEconomicLoss = accidentReport.NotConfirmEconomicLoss;
                newAccidentReport.NotConfirmEconomicOtherLoss = accidentReport.NotConfirmEconomicOtherLoss;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除事故调查报告
        /// </summary>
        /// <param name="accidentReportId"></param>
        public static void DeleteAccidentReportById(string accidentReportId)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Accident_AccidentReport accidentReport = db.Accident_AccidentReport.FirstOrDefault(e => e.AccidentReportId == accidentReportId);
            if (accidentReport != null)
            {
                CodeRecordsService.DeleteCodeRecordsByDataId(accidentReportId);
                CommonService.DeleteFlowOperateByID(accidentReportId);
                db.Accident_AccidentReport.DeleteOnSubmit(accidentReport);
                db.SubmitChanges();
            }
        }
    }
}
