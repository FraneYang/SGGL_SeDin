using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace BLL
{
    /// <summary>
    /// 项目级施工日志
    /// </summary>
    public static class ConstructionLogService
    {
        public static Model.SGGLDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取项目级施工日志
        /// </summary>
        /// <param name="ConstructionLogId"></param>
        /// <returns></returns>
        public static Model.ZHGL_ConstructionLog GetConstructionLogById(string ConstructionLogId)
        {
            return Funs.DB.ZHGL_ConstructionLog.FirstOrDefault(e => e.ConstructionLogId == ConstructionLogId);
        }

        /// <summary>
        /// 根据项目、用户及日期获取项目级施工日志
        /// </summary>
        /// <param name="ConstructionLogId"></param>
        /// <returns></returns>
        public static Model.ZHGL_ConstructionLog GetConstructionLogByProjectIdAndUserIDAndDate(string constructionLogId, string projectId, string userId, DateTime date)
        {
            return Funs.DB.ZHGL_ConstructionLog.FirstOrDefault(e => e.ConstructionLogId != constructionLogId && e.ProjectId == projectId && e.CompileMan == userId);
            //return Funs.DB.ZHGL_ConstructionLog.FirstOrDefault(e => e.ConstructionLogId != constructionLogId && e.ProjectId == projectId && e.CompileMan == userId && e.CompileDate == date);
        }

        /// <summary>
        /// 添加项目级施工日志
        /// </summary>
        /// <param name="ConstructionLog"></param>
        public static void AddConstructionLog(Model.ZHGL_ConstructionLog ConstructionLog)
        {
            Model.SGGLDB db = Funs.DB;
            Model.ZHGL_ConstructionLog newConstructionLog = new Model.ZHGL_ConstructionLog
            {
                ConstructionLogId = ConstructionLog.ConstructionLogId,
                ProjectId = ConstructionLog.ProjectId,
                Weather = ConstructionLog.Weather,
                TemperatureMax = ConstructionLog.TemperatureMax,
                TemperatureMin = ConstructionLog.TemperatureMin,
                MainWork = ConstructionLog.MainWork,
                MainProblems = ConstructionLog.MainProblems,
                Remark = ConstructionLog.Remark,
                CompileMan = ConstructionLog.CompileMan,
                CompileDate = ConstructionLog.CompileDate,
            };
            db.ZHGL_ConstructionLog.InsertOnSubmit(newConstructionLog);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改项目级施工日志
        /// </summary>
        /// <param name="ConstructionLog"></param>
        public static void UpdateConstructionLog(Model.ZHGL_ConstructionLog ConstructionLog)
        {
            Model.SGGLDB db = Funs.DB;
            Model.ZHGL_ConstructionLog newConstructionLog = db.ZHGL_ConstructionLog.FirstOrDefault(e => e.ConstructionLogId == ConstructionLog.ConstructionLogId);
            if (newConstructionLog != null)
            {
                newConstructionLog.Weather = ConstructionLog.Weather;
                newConstructionLog.TemperatureMax = ConstructionLog.TemperatureMax;
                newConstructionLog.TemperatureMin = ConstructionLog.TemperatureMin;
                newConstructionLog.MainWork = ConstructionLog.MainWork;
                newConstructionLog.MainProblems = ConstructionLog.MainProblems;
                newConstructionLog.Remark = ConstructionLog.Remark;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除项目级施工日志
        /// </summary>
        /// <param name="ConstructionLogId"></param>
        public static void DeleteConstructionLogById(string ConstructionLogId)
        {
            Model.SGGLDB db = Funs.DB;
            Model.ZHGL_ConstructionLog ConstructionLog = db.ZHGL_ConstructionLog.FirstOrDefault(e => e.ConstructionLogId == ConstructionLogId);
            if (ConstructionLog != null)
            {
                ////删除附件表
                BLL.CommonService.DeleteAttachFileById(ConstructionLog.ConstructionLogId);
                db.ZHGL_ConstructionLog.DeleteOnSubmit(ConstructionLog);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 获取天气状况
        /// </summary>
        /// <returns></returns>
        public static ListItem[] GetWeatherList()
        {
            ListItem[] lis = new ListItem[4];
            lis[0] = new ListItem("阴", "阴");
            lis[1] = new ListItem("晴", "晴");
            lis[2] = new ListItem("雨", "雨");
            lis[3] = new ListItem("雪", "雪");
            return lis;
        }
    }
}
