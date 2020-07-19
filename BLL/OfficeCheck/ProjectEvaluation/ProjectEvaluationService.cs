using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 项目评价
    /// </summary>
    public static class ProjectEvaluationService
    {
        public static Model.SGGLDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取项目绩效评价
        /// </summary>
        /// <param name="perfomanceRecordId"></param>
        /// <returns></returns>
        public static Model.ProjectSupervision_ProjectEvaluation GetPerfomanceRecordById(string perfomanceRecordId)
        {
            return Funs.DB.ProjectSupervision_ProjectEvaluation.FirstOrDefault(e => e.PerfomanceRecordId == perfomanceRecordId);
        }

        /// <summary>
        /// 添加项目绩效评价
        /// </summary>
        /// <param name="perfomanceRecord"></param>
        public static void AddPerfomanceRecord(Model.ProjectSupervision_ProjectEvaluation perfomanceRecord)
        {
            Model.SGGLDB db = Funs.DB;
            Model.ProjectSupervision_ProjectEvaluation newPerfomanceRecord = new Model.ProjectSupervision_ProjectEvaluation
            {
                PerfomanceRecordId = perfomanceRecord.PerfomanceRecordId,
                ProjectId = perfomanceRecord.ProjectId,
                PerfomanceRecordCode = perfomanceRecord.PerfomanceRecordCode,
                EvaluationDate = perfomanceRecord.EvaluationDate,
                EvaluationDef = perfomanceRecord.EvaluationDef,
                RewardOrPunish = perfomanceRecord.RewardOrPunish,
                RPMoney = perfomanceRecord.RPMoney,
                AssessmentGroup = perfomanceRecord.AssessmentGroup,
                Behavior_1 = perfomanceRecord.Behavior_1,
                Behavior_2 = perfomanceRecord.Behavior_2,
                Behavior_3 = perfomanceRecord.Behavior_3,
                Behavior_4 = perfomanceRecord.Behavior_4,
                Behavior_5 = perfomanceRecord.Behavior_5,
                Behavior_6 = perfomanceRecord.Behavior_6,
                Behavior_7 = perfomanceRecord.Behavior_7,
                Behavior_8 = perfomanceRecord.Behavior_8,
                Behavior_9 = perfomanceRecord.Behavior_9,
                Behavior_10 = perfomanceRecord.Behavior_10,
                Behavior_11 = perfomanceRecord.Behavior_11,
                Behavior_12 = perfomanceRecord.Behavior_12,
                Behavior_13 = perfomanceRecord.Behavior_13,
                Behavior_14 = perfomanceRecord.Behavior_14,
                Behavior_15 = perfomanceRecord.Behavior_15,
                Behavior_16 = perfomanceRecord.Behavior_16,
                Behavior_17 = perfomanceRecord.Behavior_17,
                Behavior_18 = perfomanceRecord.Behavior_18,
                Behavior_19 = perfomanceRecord.Behavior_19,
                Behavior_20 = perfomanceRecord.Behavior_20,
                Score_1 = perfomanceRecord.Score_1,
                Score_2 = perfomanceRecord.Score_2,
                Score_3 = perfomanceRecord.Score_3,
                Score_4 = perfomanceRecord.Score_4,
                Score_5 = perfomanceRecord.Score_5,
                Score_6 = perfomanceRecord.Score_6,
                Score_7 = perfomanceRecord.Score_7,
                Score_8 = perfomanceRecord.Score_8,
                Score_9 = perfomanceRecord.Score_9,
                Score_10 = perfomanceRecord.Score_10,
                Score_11 = perfomanceRecord.Score_11,
                Score_12 = perfomanceRecord.Score_12,
                Score_13 = perfomanceRecord.Score_13,
                Score_14 = perfomanceRecord.Score_14,
                Score_15 = perfomanceRecord.Score_15,
                Score_16 = perfomanceRecord.Score_16,
                Score_17 = perfomanceRecord.Score_17,
                Score_18 = perfomanceRecord.Score_18,
                Score_19 = perfomanceRecord.Score_19,
                Score_20 = perfomanceRecord.Score_20,
                TotalJudging = perfomanceRecord.TotalJudging,
                TotalScore = perfomanceRecord.TotalScore,
                States = perfomanceRecord.States,
                CompileMan = perfomanceRecord.CompileMan,
                CompileDate = perfomanceRecord.CompileDate,
                AttachUrl = perfomanceRecord.AttachUrl
            };
            db.ProjectSupervision_ProjectEvaluation.InsertOnSubmit(newPerfomanceRecord);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改项目绩效评价
        /// </summary>
        /// <param name="perfomanceRecord"></param>
        public static void UpdatePerfomanceRecord(Model.ProjectSupervision_ProjectEvaluation perfomanceRecord)
        {
            Model.SGGLDB db = Funs.DB;
            Model.ProjectSupervision_ProjectEvaluation newPerfomanceRecord = db.ProjectSupervision_ProjectEvaluation.FirstOrDefault(e => e.PerfomanceRecordId == perfomanceRecord.PerfomanceRecordId);
            if (newPerfomanceRecord != null)
            {
                //newPerfomanceRecord.ProjectId = perfomanceRecord.ProjectId;
                newPerfomanceRecord.PerfomanceRecordCode = perfomanceRecord.PerfomanceRecordCode;
                newPerfomanceRecord.EvaluationDate = perfomanceRecord.EvaluationDate;
                newPerfomanceRecord.EvaluationDef = perfomanceRecord.EvaluationDef;
                newPerfomanceRecord.RewardOrPunish = perfomanceRecord.RewardOrPunish;
                newPerfomanceRecord.RPMoney = perfomanceRecord.RPMoney;
                newPerfomanceRecord.AssessmentGroup = perfomanceRecord.AssessmentGroup;
                newPerfomanceRecord.Behavior_1 = perfomanceRecord.Behavior_1;
                newPerfomanceRecord.Behavior_2 = perfomanceRecord.Behavior_2;
                newPerfomanceRecord.Behavior_3 = perfomanceRecord.Behavior_3;
                newPerfomanceRecord.Behavior_4 = perfomanceRecord.Behavior_4;
                newPerfomanceRecord.Behavior_5 = perfomanceRecord.Behavior_5;
                newPerfomanceRecord.Behavior_6 = perfomanceRecord.Behavior_6;
                newPerfomanceRecord.Behavior_7 = perfomanceRecord.Behavior_7;
                newPerfomanceRecord.Behavior_8 = perfomanceRecord.Behavior_8;
                newPerfomanceRecord.Behavior_9 = perfomanceRecord.Behavior_9;
                newPerfomanceRecord.Behavior_10 = perfomanceRecord.Behavior_10;
                newPerfomanceRecord.Behavior_11 = perfomanceRecord.Behavior_11;
                newPerfomanceRecord.Behavior_12 = perfomanceRecord.Behavior_12;
                newPerfomanceRecord.Behavior_13 = perfomanceRecord.Behavior_13;
                newPerfomanceRecord.Behavior_14 = perfomanceRecord.Behavior_14;
                newPerfomanceRecord.Behavior_15 = perfomanceRecord.Behavior_15;
                newPerfomanceRecord.Behavior_16 = perfomanceRecord.Behavior_16;
                newPerfomanceRecord.Behavior_17 = perfomanceRecord.Behavior_17;
                newPerfomanceRecord.Behavior_18 = perfomanceRecord.Behavior_18;
                newPerfomanceRecord.Behavior_19 = perfomanceRecord.Behavior_19;
                newPerfomanceRecord.Behavior_20 = perfomanceRecord.Behavior_20;
                newPerfomanceRecord.Score_1 = perfomanceRecord.Score_1;
                newPerfomanceRecord.Score_2 = perfomanceRecord.Score_2;
                newPerfomanceRecord.Score_3 = perfomanceRecord.Score_3;
                newPerfomanceRecord.Score_4 = perfomanceRecord.Score_4;
                newPerfomanceRecord.Score_5 = perfomanceRecord.Score_5;
                newPerfomanceRecord.Score_6 = perfomanceRecord.Score_6;
                newPerfomanceRecord.Score_7 = perfomanceRecord.Score_7;
                newPerfomanceRecord.Score_8 = perfomanceRecord.Score_8;
                newPerfomanceRecord.Score_9 = perfomanceRecord.Score_9;
                newPerfomanceRecord.Score_10 = perfomanceRecord.Score_10;
                newPerfomanceRecord.Score_11 = perfomanceRecord.Score_11;
                newPerfomanceRecord.Score_12 = perfomanceRecord.Score_12;
                newPerfomanceRecord.Score_13 = perfomanceRecord.Score_13;
                newPerfomanceRecord.Score_14 = perfomanceRecord.Score_14;
                newPerfomanceRecord.Score_15 = perfomanceRecord.Score_15;
                newPerfomanceRecord.Score_16 = perfomanceRecord.Score_16;
                newPerfomanceRecord.Score_17 = perfomanceRecord.Score_17;
                newPerfomanceRecord.Score_18 = perfomanceRecord.Score_18;
                newPerfomanceRecord.Score_19 = perfomanceRecord.Score_19;
                newPerfomanceRecord.Score_20 = perfomanceRecord.Score_20;
                newPerfomanceRecord.TotalJudging = perfomanceRecord.TotalJudging;
                newPerfomanceRecord.TotalScore = perfomanceRecord.TotalScore;
                newPerfomanceRecord.States = perfomanceRecord.States;
                newPerfomanceRecord.CompileMan = perfomanceRecord.CompileMan;
                newPerfomanceRecord.CompileDate = perfomanceRecord.CompileDate;
                newPerfomanceRecord.AttachUrl = perfomanceRecord.AttachUrl;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除项目绩效评价
        /// </summary>
        /// <param name="perfomanceRecordId"></param>
        public static void DeletePerfomanceRecordById(string perfomanceRecordId)
        {
            Model.SGGLDB db = Funs.DB;
            Model.ProjectSupervision_ProjectEvaluation perfomanceRecord = db.ProjectSupervision_ProjectEvaluation.FirstOrDefault(e => e.PerfomanceRecordId == perfomanceRecordId);
            if (perfomanceRecord != null)
            {
                UploadFileService.DeleteFile(Funs.RootPath, perfomanceRecord.AttachUrl);//删除附件
                db.ProjectSupervision_ProjectEvaluation.DeleteOnSubmit(perfomanceRecord);
                db.SubmitChanges();
            }
        }
    }
}