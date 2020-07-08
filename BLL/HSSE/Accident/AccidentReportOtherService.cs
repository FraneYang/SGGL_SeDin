using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 事故调查处理报告
    /// </summary>
    public static class AccidentReportOtherService
    {
        public static Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);

        /// <summary>
        /// 根据主键获取事故调查处理报告
        /// </summary>
        /// <param name="accidentReportOtherId"></param>
        /// <returns></returns>
        public static Model.Accident_AccidentReportOther GetAccidentReportOtherById(string accidentReportOtherId)
        {
            return new Model.SGGLDB(Funs.ConnString).Accident_AccidentReportOther.FirstOrDefault(e => e.AccidentReportOtherId == accidentReportOtherId);
        }

        /// <summary>
        /// 获取最近时间的工作受限或医疗处理事故调查处理报告
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public static DateTime? GetLastNoStartAccidentReportOther(string projectId)
        {
            var q = (from x in new Model.SGGLDB(Funs.ConnString).Accident_AccidentReportOther
                     where (x.AccidentTypeId == "1" || x.AccidentTypeId == "2")
                        && x.ProjectId == projectId
                     orderby x.AccidentDate descending
                     select x).FirstOrDefault();
            if (q == null)
            {
                return null;
            }
            else
            {
                return q.AccidentDate;
            }
        }

        /// <summary>
        /// 根据时间段获取事故集合
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public static List<Model.Accident_AccidentReportOther> GetAccidentReportOthersByAccidentTime(DateTime startTime, DateTime endTime, string projectId)
        {
            return (from x in new Model.SGGLDB(Funs.ConnString).Accident_AccidentReportOther where x.AccidentDate >= startTime && x.AccidentDate < endTime && x.ProjectId == projectId && x.States == BLL.Const.State_2 select x).ToList();
        }

        /// <summary>
        /// 根据时间段获取事故集合
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public static List<Model.Accident_AccidentReportOther> GetRecordAccidentReportOthersByAccidentTime(DateTime startTime, DateTime endTime, string projectId)
        {
            return (from x in new Model.SGGLDB(Funs.ConnString).Accident_AccidentReportOther where x.AccidentDate >= startTime && x.AccidentDate < endTime && x.ProjectId == projectId && (x.AccidentTypeId == "1" || x.AccidentTypeId == "2") && x.States == BLL.Const.State_2 select x).ToList();
        }

        /// <summary>
        /// 根据类型获取事故数量
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="accidentType"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public static int GetCountByAccidentType(string accidentType, string projectId)
        {
            return (from x in new Model.SGGLDB(Funs.ConnString).Accident_AccidentReportOther where x.AccidentTypeId == accidentType && x.ProjectId == projectId && x.States == BLL.Const.State_2 select x).Count();
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
            return (from x in new Model.SGGLDB(Funs.ConnString).Accident_AccidentReportOther where x.AccidentDate >= startTime && x.AccidentDate < endTime && x.AccidentTypeId == accidentType && x.ProjectId == projectId && x.States == BLL.Const.State_2 select x).Count();
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
            var q = from x in new Model.SGGLDB(Funs.ConnString).Accident_AccidentReportOther where x.AccidentDate >= startTime && x.AccidentDate < endTime && x.AccidentTypeId == accidentType && x.ProjectId == projectId && x.States == BLL.Const.State_2 select x.PeopleNum;
            foreach (var item in q)
            {
                if (item != null)
                {
                    num += Funs.GetNewIntOrZero(item.ToString());
                }
            }
            return num;
            //return (from x in new Model.SGGLDB(Funs.ConnString).Accident_AccidentReportOther where x.AccidentDate >= startTime && x.AccidentDate < endTime && x.AccidentTypeId == accidentType && x.ProjectId == projectId select x.PeopleNum ?? 0).Sum();
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
            var q = from x in new Model.SGGLDB(Funs.ConnString).Accident_AccidentReportOther where x.AccidentDate >= startTime && x.AccidentDate < endTime && x.AccidentTypeId == accidentType && x.ProjectId == projectId && x.States == BLL.Const.State_2 select x.WorkingHoursLoss;
            foreach (var item in q)
            {
                if (item != null)
                {
                    loseTime += Funs.GetNewDecimalOrZero(item.ToString());
                }
            }
            return loseTime;
            //return (from x in new Model.SGGLDB(Funs.ConnString).Accident_AccidentReportOther where x.AccidentDate >= startTime && x.AccidentDate < endTime && x.AccidentTypeId == accidentType && x.ProjectId == projectId select x.WorkingHoursLoss ?? 0).Sum();
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
            var q = from x in new Model.SGGLDB(Funs.ConnString).Accident_AccidentReportOther where x.AccidentDate >= startTime && x.AccidentDate < endTime && x.AccidentTypeId == accidentType && x.ProjectId == projectId && x.States == BLL.Const.State_2 select x.EconomicLoss;
            foreach (var item in q)
            {
                if (item != null)
                {
                    loseMoney += Funs.GetNewDecimalOrZero(item.ToString());
                }
            }
            var c = from x in new Model.SGGLDB(Funs.ConnString).Accident_AccidentReportOther where x.AccidentDate >= startTime && x.AccidentDate < endTime && x.AccidentTypeId == accidentType && x.ProjectId == projectId && x.States == BLL.Const.State_2 select x.EconomicOtherLoss;
            foreach (var item in c)
            {
                if (item != null)
                {
                    loseMoney += Funs.GetNewDecimalOrZero(item.ToString());
                }
            }
            return loseMoney;
            //return (from x in new Model.SGGLDB(Funs.ConnString).Accident_AccidentReportOther where x.AccidentDate >= startTime && x.AccidentDate < endTime && x.AccidentTypeId == accidentType && x.ProjectId == projectId select x.EconomicLoss ?? 0).Sum();
        }

        /// <summary>
        /// 根据时间和事故类型名称获取事故调查处理报告集合
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="accidentTypeName"></param>
        /// <returns></returns>
        public static List<Model.Accident_AccidentReportOther> GetAccidentReportOthersByTimeAndAccidentTypeId(DateTime startTime, DateTime endTime, string projectId, string accidentTypeId)
        {
            return (from x in new Model.SGGLDB(Funs.ConnString).Accident_AccidentReportOther
                    where x.AccidentDate >= startTime && x.AccidentDate < endTime && x.ProjectId == projectId && x.AccidentTypeId == accidentTypeId && x.States == BLL.Const.State_2
                    select x).ToList();
        }

        /// <summary>
        /// 添加事故调查处理报告
        /// </summary>
        /// <param name="accidentReportOther"></param>
        public static void AddAccidentReportOther(Model.Accident_AccidentReportOther accidentReportOther)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Accident_AccidentReportOther newAccidentReportOther = new Model.Accident_AccidentReportOther
            {
                AccidentReportOtherId = accidentReportOther.AccidentReportOtherId,
                ProjectId = accidentReportOther.ProjectId,
                AccidentReportOtherCode = accidentReportOther.AccidentReportOtherCode,
                AccidentReportOtherName = accidentReportOther.AccidentReportOtherName,
                AccidentTypeId = accidentReportOther.AccidentTypeId,
                Abstract = accidentReportOther.Abstract,
                AccidentDate = accidentReportOther.AccidentDate,
                WorkAreaId = accidentReportOther.WorkAreaId,
                PeopleNum = accidentReportOther.PeopleNum,
                UnitId = accidentReportOther.UnitId,
                WorkingHoursLoss = accidentReportOther.WorkingHoursLoss,
                EconomicLoss = accidentReportOther.EconomicLoss,
                EconomicOtherLoss = accidentReportOther.EconomicOtherLoss,
                ReportMan = accidentReportOther.ReportMan,
                ReporterUnit = accidentReportOther.ReporterUnit,
                ReportDate = accidentReportOther.ReportDate,
                ProcessDescription = accidentReportOther.ProcessDescription,
                EmergencyMeasures = accidentReportOther.EmergencyMeasures,
                ImmediateCause = accidentReportOther.ImmediateCause,
                IndirectReason = accidentReportOther.IndirectReason,
                CorrectivePreventive = accidentReportOther.CorrectivePreventive,
                FileContent = accidentReportOther.FileContent,
                CompileMan = accidentReportOther.CompileMan,
                CompileDate = accidentReportOther.CompileDate,
                States = accidentReportOther.States,
                WorkArea = accidentReportOther.WorkArea
            };
            db.Accident_AccidentReportOther.InsertOnSubmit(newAccidentReportOther);
            db.SubmitChanges();
            CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(Const.ProjectAccidentReportOtherMenuId, accidentReportOther.ProjectId, null, accidentReportOther.AccidentReportOtherId, accidentReportOther.CompileDate);
        }

        /// <summary>
        /// 修改事故调查处理报告
        /// </summary>
        /// <param name="accidentReportOther"></param>
        public static void UpdateAccidentReportOther(Model.Accident_AccidentReportOther accidentReportOther)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Accident_AccidentReportOther newAccidentReportOther = db.Accident_AccidentReportOther.FirstOrDefault(e => e.AccidentReportOtherId == accidentReportOther.AccidentReportOtherId);
            if (newAccidentReportOther != null)
            {
                newAccidentReportOther.AccidentReportOtherCode = accidentReportOther.AccidentReportOtherCode;
                newAccidentReportOther.AccidentReportOtherName = accidentReportOther.AccidentReportOtherName;
                newAccidentReportOther.AccidentTypeId = accidentReportOther.AccidentTypeId;
                newAccidentReportOther.Abstract = accidentReportOther.Abstract;
                newAccidentReportOther.AccidentDate = accidentReportOther.AccidentDate;
                newAccidentReportOther.WorkAreaId = accidentReportOther.WorkAreaId;
                newAccidentReportOther.PeopleNum = accidentReportOther.PeopleNum;
                newAccidentReportOther.UnitId = accidentReportOther.UnitId;
                newAccidentReportOther.WorkingHoursLoss = accidentReportOther.WorkingHoursLoss;
                newAccidentReportOther.EconomicLoss = accidentReportOther.EconomicLoss;
                newAccidentReportOther.EconomicOtherLoss = accidentReportOther.EconomicOtherLoss;
                newAccidentReportOther.ReportMan = accidentReportOther.ReportMan;
                newAccidentReportOther.ReporterUnit = accidentReportOther.ReporterUnit;
                newAccidentReportOther.ReportDate = accidentReportOther.ReportDate;
                newAccidentReportOther.ProcessDescription = accidentReportOther.ProcessDescription;
                newAccidentReportOther.EmergencyMeasures = accidentReportOther.EmergencyMeasures;
                newAccidentReportOther.ImmediateCause = accidentReportOther.ImmediateCause;
                newAccidentReportOther.IndirectReason = accidentReportOther.IndirectReason;
                newAccidentReportOther.CorrectivePreventive = accidentReportOther.CorrectivePreventive;
                newAccidentReportOther.FileContent = accidentReportOther.FileContent;
                newAccidentReportOther.CompileMan = accidentReportOther.CompileMan;
                newAccidentReportOther.CompileDate = accidentReportOther.CompileDate;
                newAccidentReportOther.States = accidentReportOther.States;
                newAccidentReportOther.WorkArea = accidentReportOther.WorkArea;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除事故调查处理报告
        /// </summary>
        /// <param name="accidentReportOtherId"></param>
        public static void DeleteAccidentReportOtherById(string accidentReportOtherId)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Accident_AccidentReportOther accidentReportOther = db.Accident_AccidentReportOther.FirstOrDefault(e => e.AccidentReportOtherId == accidentReportOtherId);
            if (accidentReportOther != null)
            {
                CodeRecordsService.DeleteCodeRecordsByDataId(accidentReportOtherId);
                CommonService.DeleteFlowOperateByID(accidentReportOtherId);
                db.Accident_AccidentReportOther.DeleteOnSubmit(accidentReportOther);
                db.SubmitChanges();
            }
        }
    }
}
