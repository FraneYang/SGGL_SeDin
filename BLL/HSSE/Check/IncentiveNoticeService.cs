using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    /// <summary>
    /// 奖励通知单
    /// </summary>
    public static class IncentiveNoticeService
    {
        public static Model.SGGLDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取奖励通知单
        /// </summary>
        /// <param name="incentiveNoticeId"></param>
        /// <returns></returns>
        public static Model.Check_IncentiveNotice GetIncentiveNoticeById(string incentiveNoticeId)
        {
            return Funs.DB.Check_IncentiveNotice.FirstOrDefault(e => e.IncentiveNoticeId == incentiveNoticeId);
        }

        /// <summary>
        /// 根据日期获取HSE奖励通知单集合
        /// </summary>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="projectId">项目号</param>
        /// <returns>HSE奖励通知单集合</returns>
        public static List<Model.Check_IncentiveNotice> GetIncentiveNoticeListsByDate(DateTime startTime, DateTime endTime, string projectId)
        {
            return (from x in Funs.DB.Check_IncentiveNotice where x.IncentiveDate >= startTime && x.IncentiveDate <= endTime && x.ProjectId == projectId orderby x.IncentiveDate select x).ToList();
        }

        /// <summary>
        /// 根据时间段获取HSE奖励通知单集合
        /// </summary>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="projectId">项目Id</param>
        /// <returns>时间段内的HSE奖励通知单集合</returns>
        public static int GetCountByDate(DateTime startTime, DateTime endTime, string projectId)
        {
            return (from x in Funs.DB.Check_IncentiveNotice where x.IncentiveDate >= startTime && x.IncentiveDate <= endTime && x.ProjectId == projectId select x).Count();
        }

        /// <summary>
        /// 根据时间段获取HSE奖励通知单奖励总金额
        /// </summary>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="projectId">项目Id</param>
        /// <returns>时间段内的HSE奖励通知单奖励总金额</returns>
        public static decimal GetSumMoneyByDate(DateTime startTime, DateTime endTime, string projectId)
        {
            var q = (from x in Funs.DB.Check_IncentiveNotice where x.IncentiveDate >= startTime && x.IncentiveDate <= endTime && x.ProjectId == projectId select x).ToList();
            if (q.Count > 0)
            {
                return (from x in q select x.IncentiveMoney ?? 0).Sum();
            }
            return 0;
        }

        /// <summary>
        /// 获取金额总和
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="rewardType"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public static decimal? GetSumMoney(string projectId)
        {
            Model.SGGLDB db = Funs.DB;
            decimal? sumRewardMoney = (from x in db.Check_IncentiveNotice
                                       where
                                       x.ProjectId == projectId && x.States == BLL.Const.State_2
                                       select x.IncentiveMoney).Sum();
            if (sumRewardMoney == null)
            {
                return 0;
            }
            return sumRewardMoney;
        }

        /// <summary>
        /// 根据日期获取金额总和
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="rewardType"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public static decimal? GetSumMoneyByTime(DateTime startTime, DateTime endTime, string projectId)
        {
            Model.SGGLDB db = Funs.DB;
            decimal? sumRewardMoney = (from x in db.Check_IncentiveNotice
                                       where
                                       x.IncentiveDate >= startTime && x.IncentiveDate < endTime && x.ProjectId == projectId && x.States == BLL.Const.State_2
                                       select x.IncentiveMoney).Sum();
            if (sumRewardMoney == null)
            {
                return 0;
            }
            return sumRewardMoney;
        }

        /// <summary>
        /// 根据日期和奖励方式获取金额总和
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="rewardType"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public static decimal? GetSumMoneyByTimeAndType(DateTime startTime, DateTime endTime, string rewardType, string projectId)
        {
            Model.SGGLDB db = Funs.DB;
            decimal? sumRewardMoney = (from x in db.Check_IncentiveNotice
                                       where
                                       x.RewardType == rewardType &&
                                       x.IncentiveDate >= startTime && x.IncentiveDate < endTime && x.ProjectId == projectId && x.States == BLL.Const.State_2
                                       select x.IncentiveMoney).Sum();
            if (sumRewardMoney == null)
            {
                return 0;
            }
            return sumRewardMoney;
        }

        /// <summary>
        /// 添加奖励通知单
        /// </summary>
        /// <param name="incentiveNotice"></param>
        public static void AddIncentiveNotice(Model.Check_IncentiveNotice incentiveNotice)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Check_IncentiveNotice newIncentiveNotice = new Model.Check_IncentiveNotice
            {
                IncentiveNoticeId = incentiveNotice.IncentiveNoticeId,
                ProjectId = incentiveNotice.ProjectId,
                IncentiveNoticeCode = incentiveNotice.IncentiveNoticeCode,
                UnitId = incentiveNotice.UnitId,
                TeamGroupId = incentiveNotice.TeamGroupId,
                IncentiveDate = incentiveNotice.IncentiveDate,
                PersonId = incentiveNotice.PersonId,
                BasicItem = incentiveNotice.BasicItem,
                IncentiveMoney = incentiveNotice.IncentiveMoney,
                TitleReward = incentiveNotice.TitleReward,
                MattleReward = incentiveNotice.MattleReward,
                FileContents = incentiveNotice.FileContents,
                AttachUrl = incentiveNotice.AttachUrl,
                CompileMan = incentiveNotice.CompileMan,
                CompileDate = incentiveNotice.CompileDate,
                States = incentiveNotice.States,
                SignMan = incentiveNotice.SignMan,
                ApproveMan = incentiveNotice.ApproveMan,
                RewardType = incentiveNotice.RewardType,
                Currency = incentiveNotice.Currency
            };
            db.Check_IncentiveNotice.InsertOnSubmit(newIncentiveNotice);
            db.SubmitChanges();
            CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(Const.ProjectIncentiveNoticeMenuId, incentiveNotice.ProjectId, null, incentiveNotice.IncentiveNoticeId, incentiveNotice.CompileDate);
        }

        /// <summary>
        /// 修改奖励通知单
        /// </summary>
        /// <param name="incentiveNotice"></param>
        public static void UpdateIncentiveNotice(Model.Check_IncentiveNotice incentiveNotice)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Check_IncentiveNotice newIncentiveNotice = db.Check_IncentiveNotice.FirstOrDefault(e => e.IncentiveNoticeId == incentiveNotice.IncentiveNoticeId);
            if (newIncentiveNotice != null)
            {
                //newIncentiveNotice.ProjectId = incentiveNotice.ProjectId;
                //newIncentiveNotice.IncentiveNoticeCode = incentiveNotice.IncentiveNoticeCode;
                newIncentiveNotice.UnitId = incentiveNotice.UnitId;
                newIncentiveNotice.IncentiveDate = incentiveNotice.IncentiveDate;
                newIncentiveNotice.TeamGroupId = incentiveNotice.TeamGroupId;
                newIncentiveNotice.PersonId = incentiveNotice.PersonId;
                newIncentiveNotice.BasicItem = incentiveNotice.BasicItem;
                newIncentiveNotice.IncentiveMoney = incentiveNotice.IncentiveMoney;
                newIncentiveNotice.TitleReward = incentiveNotice.TitleReward;
                newIncentiveNotice.MattleReward = incentiveNotice.MattleReward;
                newIncentiveNotice.FileContents = incentiveNotice.FileContents;
                newIncentiveNotice.AttachUrl = incentiveNotice.AttachUrl;
                newIncentiveNotice.CompileMan = incentiveNotice.CompileMan;
                newIncentiveNotice.CompileDate = incentiveNotice.CompileDate;
                newIncentiveNotice.States = incentiveNotice.States;
                newIncentiveNotice.SignMan = incentiveNotice.SignMan;
                newIncentiveNotice.ApproveMan = incentiveNotice.ApproveMan;
                newIncentiveNotice.RewardType = incentiveNotice.RewardType;
                newIncentiveNotice.Currency = incentiveNotice.Currency;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除奖励通知单
        /// </summary>
        /// <param name="incentiveNoticeId"></param>
        public static void DeleteIncentiveNoticeById(string incentiveNoticeId)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Check_IncentiveNotice incentiveNotice = db.Check_IncentiveNotice.FirstOrDefault(e => e.IncentiveNoticeId == incentiveNoticeId);
            if (incentiveNotice != null)
            {
                CodeRecordsService.DeleteCodeRecordsByDataId(incentiveNoticeId);//删除编号                 
                UploadFileService.DeleteFile(Funs.RootPath, incentiveNotice.AttachUrl);//删除附件
                ////删除流程表
                BLL.CommonService.DeleteFlowOperateByID(incentiveNotice.IncentiveNoticeId);
                var getFlow = db.Check_IncentiveNoticeFlowOperate.Where(x => x.IncentiveNoticeId == incentiveNotice.IncentiveNoticeId);
                if (getFlow.Count() > 0)
                {
                    db.Check_IncentiveNoticeFlowOperate.DeleteAllOnSubmit(getFlow);
                }
                db.Check_IncentiveNotice.DeleteOnSubmit(incentiveNotice);
                db.SubmitChanges();
            }
        }
    }
}
