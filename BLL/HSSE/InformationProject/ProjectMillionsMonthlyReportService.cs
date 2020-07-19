using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 百万工时安全统计月报
    /// </summary>
    public static class ProjectMillionsMonthlyReportService
    {
        public static Model.SGGLDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取百万工时安全统计月报
        /// </summary>
        /// <param name="millionsMonthlyReport"></param>
        /// <returns></returns>
        public static Model.InformationProject_MillionsMonthlyReport GetMillionsMonthlyReportById(string millionsMonthlyReportId)
        {
            return Funs.DB.InformationProject_MillionsMonthlyReport.FirstOrDefault(e => e.MillionsMonthlyReportId == millionsMonthlyReportId);
        }

        /// <summary>
        /// 添加百万工时安全统计月报
        /// </summary>
        /// <param name="millionsMonthlyReport"></param>
        public static void AddMillionsMonthlyReport(Model.InformationProject_MillionsMonthlyReport millionsMonthlyReport)
        {
            Model.SGGLDB db = Funs.DB;
            Model.InformationProject_MillionsMonthlyReport newMillionsMonthlyReport = new Model.InformationProject_MillionsMonthlyReport
            {
                MillionsMonthlyReportId = millionsMonthlyReport.MillionsMonthlyReportId,
                ProjectId = millionsMonthlyReport.ProjectId,
                Year = millionsMonthlyReport.Year,
                Month = millionsMonthlyReport.Month,
                CompileMan = millionsMonthlyReport.CompileMan,
                CompileDate = millionsMonthlyReport.CompileDate,
                States = millionsMonthlyReport.States,
                Affiliation = millionsMonthlyReport.Affiliation,
                Name = millionsMonthlyReport.Name,
                PostPersonNum = millionsMonthlyReport.PostPersonNum,
                SnapPersonNum = millionsMonthlyReport.SnapPersonNum,
                ContractorNum = millionsMonthlyReport.ContractorNum,
                SumPersonNum = millionsMonthlyReport.SumPersonNum,
                TotalWorkNum = millionsMonthlyReport.TotalWorkNum,
                SeriousInjuriesNum = millionsMonthlyReport.SeriousInjuriesNum,
                SeriousInjuriesPersonNum = millionsMonthlyReport.SeriousInjuriesPersonNum,
                SeriousInjuriesLossHour = millionsMonthlyReport.SeriousInjuriesLossHour,
                MinorAccidentNum = millionsMonthlyReport.MinorAccidentNum,
                MinorAccidentPersonNum = millionsMonthlyReport.MinorAccidentPersonNum,
                MinorAccidentLossHour = millionsMonthlyReport.MinorAccidentLossHour,
                OtherAccidentNum = millionsMonthlyReport.OtherAccidentNum,
                OtherAccidentPersonNum = millionsMonthlyReport.OtherAccidentPersonNum,
                OtherAccidentLossHour = millionsMonthlyReport.OtherAccidentLossHour,
                RestrictedWorkPersonNum = millionsMonthlyReport.RestrictedWorkPersonNum,
                RestrictedWorkLossHour = millionsMonthlyReport.RestrictedWorkLossHour,
                MedicalTreatmentPersonNum = millionsMonthlyReport.MedicalTreatmentPersonNum,
                MedicalTreatmentLossHour = millionsMonthlyReport.MedicalTreatmentLossHour,
                FireNum = millionsMonthlyReport.FireNum,
                ExplosionNum = millionsMonthlyReport.ExplosionNum,
                TrafficNum = millionsMonthlyReport.TrafficNum,
                EquipmentNum = millionsMonthlyReport.EquipmentNum,
                QualityNum = millionsMonthlyReport.QualityNum,
                OtherNum = millionsMonthlyReport.OtherNum,
                FirstAidDressingsNum = millionsMonthlyReport.FirstAidDressingsNum,
                AttemptedEventNum = millionsMonthlyReport.AttemptedEventNum,
                LossDayNum = millionsMonthlyReport.LossDayNum
            };
            db.InformationProject_MillionsMonthlyReport.InsertOnSubmit(newMillionsMonthlyReport);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改百万工时安全统计月报
        /// </summary>
        /// <param name="millionsMonthlyReport"></param>
        public static void UpdateMillionsMonthlyReport(Model.InformationProject_MillionsMonthlyReport millionsMonthlyReport)
        {
            Model.SGGLDB db = Funs.DB;
            Model.InformationProject_MillionsMonthlyReport newMillionsMonthlyReport = db.InformationProject_MillionsMonthlyReport.FirstOrDefault(e => e.MillionsMonthlyReportId == millionsMonthlyReport.MillionsMonthlyReportId);
            if (newMillionsMonthlyReport != null)
            {
                newMillionsMonthlyReport.ProjectId = millionsMonthlyReport.ProjectId;
                newMillionsMonthlyReport.Year = millionsMonthlyReport.Year;
                newMillionsMonthlyReport.Month = millionsMonthlyReport.Month;
                newMillionsMonthlyReport.CompileMan = millionsMonthlyReport.CompileMan;
                newMillionsMonthlyReport.CompileDate = millionsMonthlyReport.CompileDate;
                newMillionsMonthlyReport.States = millionsMonthlyReport.States;
                newMillionsMonthlyReport.Affiliation = millionsMonthlyReport.Affiliation;
                newMillionsMonthlyReport.Name = millionsMonthlyReport.Name;
                newMillionsMonthlyReport.PostPersonNum = millionsMonthlyReport.PostPersonNum;
                newMillionsMonthlyReport.SnapPersonNum = millionsMonthlyReport.SnapPersonNum;
                newMillionsMonthlyReport.ContractorNum = millionsMonthlyReport.ContractorNum;
                newMillionsMonthlyReport.SumPersonNum = millionsMonthlyReport.SumPersonNum;
                newMillionsMonthlyReport.TotalWorkNum = millionsMonthlyReport.TotalWorkNum;
                newMillionsMonthlyReport.SeriousInjuriesNum = millionsMonthlyReport.SeriousInjuriesNum;
                newMillionsMonthlyReport.SeriousInjuriesPersonNum = millionsMonthlyReport.SeriousInjuriesPersonNum;
                newMillionsMonthlyReport.SeriousInjuriesLossHour = millionsMonthlyReport.SeriousInjuriesLossHour;
                newMillionsMonthlyReport.MinorAccidentNum = millionsMonthlyReport.MinorAccidentNum;
                newMillionsMonthlyReport.MinorAccidentPersonNum = millionsMonthlyReport.MinorAccidentPersonNum;
                newMillionsMonthlyReport.MinorAccidentLossHour = millionsMonthlyReport.MinorAccidentLossHour;
                newMillionsMonthlyReport.OtherAccidentNum = millionsMonthlyReport.OtherAccidentNum;
                newMillionsMonthlyReport.OtherAccidentPersonNum = millionsMonthlyReport.OtherAccidentPersonNum;
                newMillionsMonthlyReport.OtherAccidentLossHour = millionsMonthlyReport.OtherAccidentLossHour;
                newMillionsMonthlyReport.RestrictedWorkPersonNum = millionsMonthlyReport.RestrictedWorkPersonNum;
                newMillionsMonthlyReport.RestrictedWorkLossHour = millionsMonthlyReport.RestrictedWorkLossHour;
                newMillionsMonthlyReport.MedicalTreatmentPersonNum = millionsMonthlyReport.MedicalTreatmentPersonNum;
                newMillionsMonthlyReport.MedicalTreatmentLossHour = millionsMonthlyReport.MedicalTreatmentLossHour;
                newMillionsMonthlyReport.FireNum = millionsMonthlyReport.FireNum;
                newMillionsMonthlyReport.ExplosionNum = millionsMonthlyReport.ExplosionNum;
                newMillionsMonthlyReport.TrafficNum = millionsMonthlyReport.TrafficNum;
                newMillionsMonthlyReport.EquipmentNum = millionsMonthlyReport.EquipmentNum;
                newMillionsMonthlyReport.QualityNum = millionsMonthlyReport.QualityNum;
                newMillionsMonthlyReport.OtherNum = millionsMonthlyReport.OtherNum;
                newMillionsMonthlyReport.FirstAidDressingsNum = millionsMonthlyReport.FirstAidDressingsNum;
                newMillionsMonthlyReport.AttemptedEventNum = millionsMonthlyReport.AttemptedEventNum;
                newMillionsMonthlyReport.LossDayNum = millionsMonthlyReport.LossDayNum;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除百万工时安全统计月报
        /// </summary>
        /// <param name="millionsMonthlyReportId"></param>
        public static void DeleteMillionsMonthlyReportById(string millionsMonthlyReportId)
        {
            Model.SGGLDB db = Funs.DB;
            Model.InformationProject_MillionsMonthlyReport millionsMonthlyReport = db.InformationProject_MillionsMonthlyReport.FirstOrDefault(e => e.MillionsMonthlyReportId == millionsMonthlyReportId);
            if (millionsMonthlyReport != null)
            {
                CommonService.DeleteFlowOperateByID(millionsMonthlyReportId);//删除流程
                db.InformationProject_MillionsMonthlyReport.DeleteOnSubmit(millionsMonthlyReport);
                db.SubmitChanges();
            }
        }
    }
}