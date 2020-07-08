using System.Collections.Generic;
using System.Linq;
using System;

namespace BLL
{
    /// <summary>
    /// HSE日志
    /// </summary>
    public static class APIHSEDiaryService
    {
        #region 获取HSE日志信息
        /// <summary>
        /// 获取HSE日志信息
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="userId"></param>
        /// <param name="diaryDate"></param>
        /// <returns></returns>
        public static Model.HSEDiaryItem getHSEDiary(string projectId, string userId, string diaryDate)
        {
            DateTime? getDiaryDate = Funs.GetNewDateTime(diaryDate);
            Model.HSEDiaryItem getItem = new Model.HSEDiaryItem();
            if (getDiaryDate.HasValue && !string.IsNullOrEmpty(projectId) && !string.IsNullOrEmpty(userId))
            {
                var getFlowOperteList = ReturnFlowOperteList(projectId, userId, getDiaryDate.Value);
                getItem.ProjectId = projectId;
                getItem.UserId = userId;
                getItem.UserName = UserService.GetUserNameByUserId(userId);
                getItem.DiaryDate = diaryDate;
                getItem.HSEDiaryId = SQLHelper.GetNewID();
                getItem.Value1 = getValues1(getFlowOperteList,projectId, userId, getDiaryDate.Value);
                getItem.Value2 = getValues2(getFlowOperteList, projectId, userId, getDiaryDate.Value);
                getItem.Value3 = getValues3(getFlowOperteList, projectId, userId, getDiaryDate.Value);
                getItem.Value4 = getValues4(getFlowOperteList, projectId, userId, getDiaryDate.Value);
                getItem.Value5 = getValues5(getFlowOperteList, projectId, userId, getDiaryDate.Value);
                getItem.Value6 = getValues6(getFlowOperteList, projectId, userId, getDiaryDate.Value);
                getItem.Value7 = getValues7(getFlowOperteList, projectId, userId, getDiaryDate.Value);
                getItem.Value8 = getValues8(getFlowOperteList, projectId, userId, getDiaryDate.Value);
                getItem.Value9 = getValues9(getFlowOperteList, projectId, userId, getDiaryDate.Value);
                getItem.Value10 = getValues10(getFlowOperteList, projectId, userId, getDiaryDate.Value);
                var getInfo = new Model.SGGLDB(Funs.ConnString).Project_HSEDiary.FirstOrDefault(x => x.UserId == userId && x.DiaryDate == getDiaryDate);
                if (getInfo != null)
                {
                    getItem.HSEDiaryId = getInfo.HSEDiaryId;
                    getItem.DailySummary = getInfo.DailySummary;
                    getItem.TomorrowPlan = getInfo.TomorrowPlan;
                }
            }
            return getItem;
        }
        #endregion        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="getProjectId"></param>
        /// <param name="getUserId"></param>
        /// <param name="getDate"></param>
        /// <returns></returns>
        public static List<Model.Sys_FlowOperate> ReturnFlowOperteList(string getProjectId, string getUserId, DateTime getDate)
        {
          return  (from x in new Model.SGGLDB(Funs.ConnString).Sys_FlowOperate
                                 where x.ProjectId == getProjectId && x.OperaterId == getUserId && x.IsClosed == true
                                 && x.OperaterTime >= getDate && x.OperaterTime < getDate.AddDays(1)
                                 select x).ToList();
        }

        #region 获取HSE日志列表信息
        /// <summary>
        /// 获取HSE日志列表信息
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="userId"></param>
        /// <param name="diaryDate"></param>
        /// <returns></returns>
        public static List<Model.HSEDiaryItem> getHSEDiaryList(string projectId, string userId, string diaryDate)
        {
            DateTime? getDiaryDate = Funs.GetNewDateTime(diaryDate);
            var getDataList = from x in new Model.SGGLDB(Funs.ConnString).Project_HSEDiary
                              where x.ProjectId == projectId && (userId == null || x.UserId == userId)
                              && (diaryDate == null || x.DiaryDate == getDiaryDate)
                              orderby x.DiaryDate descending
                              select new Model.HSEDiaryItem
                              {
                                  HSEDiaryId = x.HSEDiaryId,
                                  ProjectId = x.ProjectId,
                                  DiaryDate = string.Format("{0:yyyy-MM-dd}", x.DiaryDate),
                                  UserId = x.UserId,
                                  UserName = new Model.SGGLDB(Funs.ConnString).Sys_User.First(u => u.UserId == x.UserId).UserName,
                                  DailySummary = x.DailySummary,
                                  TomorrowPlan = x.TomorrowPlan,
                              };
            return getDataList.ToList();
        }
        #endregion

        #region 保存HSE日志
        /// <summary>
        /// 保存HSE日志
        /// </summary>
        /// <param name="item"></param>
        public static void SaveHSEDiary(Model.HSEDiaryItem item)
        {
            DeleteHSEDiary(item.HSEDiaryId);
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                Model.Project_HSEDiary newHSEDiary = new Model.Project_HSEDiary
                {
                    HSEDiaryId = item.HSEDiaryId,
                    ProjectId = item.ProjectId,
                    DiaryDate = Funs.GetNewDateTime(item.DiaryDate),
                    UserId = item.UserId,
                    DailySummary = item.DailySummary,
                    TomorrowPlan = item.TomorrowPlan,
                    Value1 = item.Value1,
                    Value2 = item.Value2,
                    Value3 = item.Value3,
                    Value4 = item.Value4,
                    Value5 = item.Value5,
                    Value6 = item.Value6,
                    Value7 = item.Value7,
                    Value8 = item.Value8,
                    Value9 = item.Value9,
                    Value10 = item.Value10,
                };
                if (string.IsNullOrEmpty(newHSEDiary.HSEDiaryId))
                {
                    newHSEDiary.HSEDiaryId = SQLHelper.GetNewID();
                }
                db.Project_HSEDiary.InsertOnSubmit(newHSEDiary);
                db.SubmitChanges();
            }
        }
        #endregion

        /// <summary>
        /// 删除日志
        /// </summary>
        /// <param name="hseDiaryId"></param>
        public static void DeleteHSEDiary(string hseDiaryId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var getInfo = db.Project_HSEDiary.FirstOrDefault(x => x.HSEDiaryId == hseDiaryId);
                if (getInfo != null)
                {
                    db.Project_HSEDiary.DeleteOnSubmit(getInfo);
                    db.SubmitChanges();
                }
            }
        }

        #region 获取日志信息
        /// <summary>
        /// 1HSE检查情况及检查次数
        /// </summary>
        public static string getValues1(List<Model.Sys_FlowOperate> getFlowOperteList, string getProjectId, string getUserId, DateTime getDate)
        {
            string strValues = string.Empty;
            var getRegister = (from x in new Model.SGGLDB(Funs.ConnString).HSSE_Hazard_HazardRegister
                               where x.ProjectId == getProjectId && x.CheckManId == getUserId
                                 && getDate > x.CheckTime.Value.AddDays(-1) && getDate < x.CheckTime.Value.AddDays(1)
                               select x).Count();
            if (getRegister > 0)
            {
                strValues += "巡检：" + getRegister.ToString() + "；";
            }
            if (getFlowOperteList.Count() > 0)
            {
                var getDayCount = getFlowOperteList.Where(x => x.MenuId == Const.ProjectCheckDayMenuId).Count();
                if (getDayCount > 0)
                {
                    strValues += "日常：" + getDayCount.ToString();
                }
                var getSpecialCount = getFlowOperteList.Where(x => x.MenuId == Const.ProjectCheckSpecialMenuId).Count();
                if (getSpecialCount > 0)
                {
                    strValues += "专项：" + getSpecialCount.ToString();
                }
                var getColligationCount = getFlowOperteList.Where(x => x.MenuId == Const.ProjectCheckColligationMenuId).Count();
                if (getColligationCount > 0)
                {
                    strValues += "综合：" + getColligationCount.ToString();
                }
            }
            if (string.IsNullOrEmpty(strValues))
            {
                return "0";
            }
            else
            {
                return strValues;
            }
        }
        /// <summary>
        /// 2隐患整改情况及隐患整改数量
        /// </summary>
        public static string getValues2(List<Model.Sys_FlowOperate> getFlowOperteList, string getProjectId, string getUserId, DateTime getDate)
        {
            string strValues = string.Empty;
            var getCHeck = from x in new Model.SGGLDB(Funs.ConnString).Check_RectifyNotices
                           where x.ProjectId == getProjectId && x.CheckPerson == getUserId && getDate > x.CheckedDate.Value.AddDays(-1) && getDate < x.CheckedDate.Value.AddDays(1)
                           select x;
            if (getCHeck.Count() > 0)
            {
                strValues += "复查：" + getCHeck.Count().ToString() + "；";
            }
            var getSign = from x in new Model.SGGLDB(Funs.ConnString).Check_RectifyNotices
                          where x.ProjectId == getProjectId && x.SignPerson == getUserId && getDate > x.SignDate.Value.AddDays(-1) && getDate < x.SignDate.Value.AddDays(1)
                          select x;
            if (getSign.Count() > 0)
            {
                strValues += "签发：" + getSign.Count().ToString() + "；";
            }
            var getDuty = from x in new Model.SGGLDB(Funs.ConnString).Check_RectifyNotices
                          where x.ProjectId == getProjectId && x.DutyPersonId == getUserId && getDate > x.CompleteDate.Value.AddDays(-1) && getDate < x.CompleteDate.Value.AddDays(1)
                          select x;
            if (getDuty.Count() > 0)
            {
                strValues += "整改：" + getDuty.Count().ToString() + "；";
            }

            if (string.IsNullOrEmpty(strValues))
            {
                return "0";
            }
            else
            {
                return strValues;
            }
        }
        /// <summary>
        /// 3作业许可情况及作业票数量
        /// </summary>
        public static string getValues3(List<Model.Sys_FlowOperate> getFlowOperteList, string getProjectId, string getUserId, DateTime getDate)
        {
            string strValues = string.Empty;
            var getLicense = from x in new Model.SGGLDB(Funs.ConnString).License_FlowOperate
                             join y in new Model.SGGLDB(Funs.ConnString).Sys_Menu on x.MenuId equals y.MenuId
                             where x.ProjectId == getProjectId && x.OperaterId == getUserId
                                 && getDate > x.OperaterTime.Value.AddDays(-1) && getDate < x.OperaterTime.Value.AddDays(1)
                             select new { x.DataId, y.MenuName };

            if (getLicense.Count() > 0)
            {
                var getNames = getLicense.Select(x => x.MenuName).Distinct();
                foreach (var item in getNames)
                {

                    strValues += item.Replace("作业票", "") + "：" + getLicense.Where(x => x.MenuName == item).Select(x => x.DataId).Distinct().Count().ToString() + "；";
                }
            }

            if (string.IsNullOrEmpty(strValues))
            {
                return "0";
            }
            else
            {
                return strValues;
            }
        }

        /// <summary>
        /// 4施工机具、安全设施检查、验收情况及检查验收数量
        /// </summary>
        public static string getValues4(List<Model.Sys_FlowOperate> getFlowOperteList, string getProjectId, string getUserId, DateTime getDate)
        {
            string strValues = string.Empty;
            //var getCompileCount = (from x in new Model.SGGLDB(Funs.ConnString).License_EquipmentSafetyList
            //                    where x.ProjectId == getProjectId && x.CompileMan == getUserId 
            //                        && getDate > x.CompileDate.Value.AddDays(-1) && getDate < x.CompileDate.Value.AddDays(1)
            //                    select x).Count();
            //if (getCompileCount > 0)
            //{
            //    strValues += "申请:" + getCompileCount.ToString() + "；";
            //}
            var getAuditCount = getFlowOperteList.Where(x => x.MenuId == Const.ProjectEquipmentSafetyListMenuId).Count();
            if (getAuditCount > 0)
            {
                strValues = getAuditCount.ToString();
            }
            if (string.IsNullOrEmpty(strValues))
            {
                return "0";
            }
            else
            {
                return strValues;
            }
        }
        /// <summary>
        /// 5危险源辨识工作情况及次数
        /// </summary>
        public static string getValues5(List<Model.Sys_FlowOperate> getFlowOperteList, string getProjectId, string getUserId, DateTime getDate)
        {
            string strValues = string.Empty;
            //var getHCompileCount = (from x in new Model.SGGLDB(Funs.ConnString).Hazard_HazardList
            //                       where x.ProjectId == getProjectId && x.CompileMan == getUserId
            //                           && getDate > x.CompileDate.Value.AddDays(-1) && getDate < x.CompileDate.Value.AddDays(1)
            //                       select x).Count();
            //if (getHCompileCount > 0)
            //{
            //    strValues += "编制职业健康危险源:" + getHCompileCount.ToString() + "；";
            //}
            var getHAuditCount = getFlowOperteList.Where(x => x.MenuId == Const.ProjectHazardListMenuId).Count();
            if (getHAuditCount > 0)
            {
                strValues += "职业健康:" + getHAuditCount.ToString() + "；";
            }

            //var getECompileCount = (from x in new Model.SGGLDB(Funs.ConnString).Hazard_EnvironmentalRiskList
            //                        where x.ProjectId == getProjectId && x.CompileMan == getUserId
            //                            && getDate > x.CompileDate.Value.AddDays(-1) && getDate < x.CompileDate.Value.AddDays(1)
            //                        select x).Count();
            //if (getECompileCount > 0)
            //{
            //    strValues += "编制环境危险源:" + getECompileCount.ToString() + "；";
            //}
            var getEAuditCount = getFlowOperteList.Where(x => x.MenuId == Const.ProjectEnvironmentalRiskListMenuId).Count();
            if (getEAuditCount > 0)
            {
                strValues += "环境:" + getEAuditCount.ToString() + "；";
            }
            if (string.IsNullOrEmpty(strValues))
            {
                return "0";
            }
            else
            {
                return strValues;
            }
        }
        /// <summary>
        /// 6应急计划修编、演练及物资准备情况及次数
        /// </summary>
        public static string getValues6(List<Model.Sys_FlowOperate> getFlowOperteList, string getProjectId, string getUserId, DateTime getDate)
        {
            string strValues = string.Empty;
            var getCompileCount = (from x in new Model.SGGLDB(Funs.ConnString).Emergency_EmergencyList
                                   where x.ProjectId == getProjectId && (x.AuditMan == getUserId || x.ApproveMan == getUserId)
                                       && getDate > x.CompileDate.Value.AddDays(-1) && getDate < x.CompileDate.Value.AddDays(1)
                                   select x).Count();
            var getFlowCount = getFlowOperteList.Where(x => x.MenuId == Const.ProjectEmergencyListMenuId).Count();
            if (getCompileCount > 0)
            {
                strValues += "预案:" + (getCompileCount + getCompileCount).ToString() + "；";
            }

            var getDrillCount = getFlowOperteList.Where(x => x.MenuId == Const.ProjectDrillRecordListMenuId).Count();
            if (getDrillCount > 0)
            {
                strValues += "演练:" + getDrillCount.ToString() + "；";
            }
            var getSupplyCount = getFlowOperteList.Where(x => x.MenuId == Const.ProjectEmergencySupplyMenuId).Count();
            if (getSupplyCount > 0)
            {
                strValues += "物资:" + getSupplyCount.ToString() + "；";
            }
            if (string.IsNullOrEmpty(strValues))
            {
                return "0";
            }
            else
            {
                return strValues;
            }
        }
        /// <summary>
        /// 7教育培训情况及人次
        /// </summary>
        public static string getValues7(List<Model.Sys_FlowOperate> getFlowOperteList, string getProjectId, string getUserId, DateTime getDate)
        {
            string strValues = string.Empty;
            var getFlows = getFlowOperteList.Where(x => x.MenuId == Const.ProjectTrainRecordMenuId).ToList();
            if (getFlows.Count() > 0)
            {
                List<string> listIds = getFlows.Select(x => x.DataId).ToList();
                strValues += "次数:" + getFlows.Count().ToString() + "；";
                var getPersonCount = (from x in new Model.SGGLDB(Funs.ConnString).EduTrain_TrainRecord
                                      join y in new Model.SGGLDB(Funs.ConnString).EduTrain_TrainRecordDetail on x.TrainingId equals y.TrainingId
                                      where listIds.Contains(x.TrainingId)
                                      select y).Count();
                if (getPersonCount > 0)
                {
                    strValues += "人数:" + getPersonCount.ToString() + "。";
                }
            }

            if (string.IsNullOrEmpty(strValues))
            {
                return "0";
            }
            else
            {
                return strValues;
            }
        }
        /// <summary>
        ///  8 HSE会议情况及次数
        /// </summary>
        public static string getValues8(List<Model.Sys_FlowOperate> getFlowOperteList, string getProjectId, string getUserId, DateTime getDate)
        {
            string strValues = string.Empty;
            var getClassMeeting = getFlowOperteList.Where(x => x.MenuId == Const.ProjectClassMeetingMenuId).Count();
            if (getClassMeeting > 0)
            {
                strValues += "班前会：" + getClassMeeting.ToString() + "；";
            }
            var getWeekMeeting = new Model.SGGLDB(Funs.ConnString).Meeting_WeekMeeting.Where(x => (x.CompileMan == getUserId || x.MeetingHostManId == getUserId || x.AttentPersonIds.Contains(getUserId))
            && getDate > x.WeekMeetingDate.Value.AddDays(-1) && getDate < x.WeekMeetingDate.Value.AddDays(1)).Count();
            if (getWeekMeeting > 0)
            {
                strValues += "周例会：" + getWeekMeeting.ToString() + "；";
            }
            var getMonthMeeting = new Model.SGGLDB(Funs.ConnString).Meeting_MonthMeeting.Where(x => (x.CompileMan == getUserId || x.MeetingHostManId == getUserId || x.AttentPersonIds.Contains(getUserId))
            && getDate > x.MonthMeetingDate.Value.AddDays(-1) && getDate < x.MonthMeetingDate.Value.AddDays(1)).Count();
            if (getMonthMeeting > 0)
            {
                strValues += "月例会：" + getMonthMeeting.ToString() + "；";
            }
            var getSpecialMeeting = new Model.SGGLDB(Funs.ConnString).Meeting_SpecialMeeting.Where(x => (x.CompileMan == getUserId || x.CompileMan == getUserId || x.MeetingHostManId == getUserId || x.AttentPersonIds.Contains(getUserId))
            && getDate > x.SpecialMeetingDate.Value.AddDays(-1) && getDate < x.SpecialMeetingDate.Value.AddDays(1)).Count();
            if (getSpecialMeeting > 0)
            {
                strValues += "专题会：" + getSpecialMeeting.ToString() + "；";
            }
            var getAttendMeeting = new Model.SGGLDB(Funs.ConnString).Meeting_AttendMeeting.Where(x => (x.CompileMan == getUserId || x.MeetingHostManId == getUserId || x.AttentPersonIds.Contains(getUserId))
            && getDate > x.AttendMeetingDate.Value.AddDays(-1) && getDate < x.AttendMeetingDate.Value.AddDays(1)).Count();
            if (getAttendMeeting > 0)
            {
                strValues += "其他会议：" + getAttendMeeting.ToString() + "；";
            }

            if (string.IsNullOrEmpty(strValues))
            {
                return "0";
            }
            else
            {
                return strValues;
            }
        }
        /// <summary>
        ///  9 HSE宣传工作情况
        /// </summary>
        public static string getValues9(List<Model.Sys_FlowOperate> getFlowOperteList, string getProjectId, string getUserId, DateTime getDate)
        {
            string strValues = string.Empty;
            var getFlowCount = getFlowOperteList.Where(x => x.MenuId == Const.ProjectPromotionalActivitiesMenuId).Count();
            if (getFlowCount > 0)
            {
                strValues += getFlowCount.ToString();
            }
            if (string.IsNullOrEmpty(strValues))
            {
                return "0";
            }
            else
            {
                return strValues;
            }
        }
        /// <summary>
        ///  10 HSE奖惩工作情况、HSE奖励次数、HSE处罚次数
        /// </summary>
        public static string getValues10(List<Model.Sys_FlowOperate> getFlowOperteList, string getProjectId, string getUserId, DateTime getDate)
        {
            string strValues = string.Empty;
            var getFlowCount = getFlowOperteList.Where(x => x.MenuId == Const.ProjectIncentiveNoticeMenuId).Count();
            if (getFlowCount > 0)
            {
                strValues += "奖励单：" + getFlowCount.ToString();
            }

            var getPFlowCount = getFlowOperteList.Where(x => x.MenuId == Const.ProjectPunishNoticeMenuId).Count();
            if (getPFlowCount > 0)
            {
                strValues += "处罚单：" + getPFlowCount.ToString();
            }
            if (string.IsNullOrEmpty(strValues))
            {
                return "0";
            }
            else
            {
                return strValues;
            }
        }
        #endregion
    }
}
