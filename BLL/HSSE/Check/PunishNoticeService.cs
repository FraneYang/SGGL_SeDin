using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 处罚通知单
    /// </summary>
    public static class PunishNoticeService
    {
        public static Model.SGGLDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取处罚通知单
        /// </summary>
        /// <param name="punishNoticeId"></param>
        /// <returns></returns>
        public static Model.Check_PunishNotice GetPunishNoticeById(string punishNoticeId)
        {
            return Funs.DB.Check_PunishNotice.FirstOrDefault(e => e.PunishNoticeId == punishNoticeId);
        }

        /// <summary>
        /// 根据日期获取HSE奖励通知单集合
        /// </summary>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="projectId">项目号</param>
        /// <returns>HSE奖励通知单集合</returns>
        public static List<Model.Check_PunishNotice> GetPunishNoticeListsByDate(DateTime startTime, DateTime endTime, string projectId)
        {
            return (from x in Funs.DB.Check_PunishNotice where x.PunishNoticeDate >= startTime && x.PunishNoticeDate <= endTime && x.ProjectId == projectId orderby x.PunishNoticeDate select x).ToList();
        }

        /// <summary>
        /// 根据时间段获取HSE处罚通知单集合
        /// </summary>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="projectId">项目Id</param>
        /// <returns>时间段内的HSE处罚通知单集合</returns>
        public static int GetCountByDate(DateTime startTime, DateTime endTime, string projectId)
        {
            return (from x in Funs.DB.Check_PunishNotice where x.PunishNoticeDate >= startTime && x.PunishNoticeDate <= endTime && x.ProjectId == projectId select x).Count();
        }

        /// <summary>
        /// 根据时间段获取HSE处罚通知单处罚总金额
        /// </summary>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="projectId">项目Id</param>
        /// <returns>时间段内的HSE处罚通知单处罚总金额</returns>
        public static decimal GetSumMoneyByDate(DateTime startTime, DateTime endTime, string projectId)
        {
            var q = (from x in Funs.DB.Check_PunishNotice where x.PunishNoticeDate >= startTime && x.PunishNoticeDate <= endTime && x.ProjectId == projectId select x).ToList();
            if (q.Count > 0)
            {
                return (from x in q select x.PunishMoney ?? 0).Sum();
            }
            return 0;
        }

        /// <summary>
        /// 获取处罚金额总和
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public static decimal? GetSumMoney(string projectId)
        {
            Model.SGGLDB db = Funs.DB;
            decimal? sumRewardMoney = (from x in db.Check_PunishNotice
                                       where x.ProjectId == projectId && x.States == BLL.Const.State_2
                                       select x.PunishMoney).Sum();
            if (sumRewardMoney == null)
            {
                return 0;
            }
            return sumRewardMoney;
        }

        /// <summary>
        /// 根据日期获取处罚金额总和
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public static decimal? GetSumMoneyByTime(DateTime startTime, DateTime endTime, string projectId)
        {
            Model.SGGLDB db = Funs.DB;
            decimal? sumRewardMoney = (from x in db.Check_PunishNotice
                                       where x.PunishNoticeDate >= startTime && x.PunishNoticeDate < endTime && x.ProjectId == projectId && x.States == BLL.Const.State_2
                                       select x.PunishMoney).Sum();
            if (sumRewardMoney == null)
            {
                return 0;
            }
            return sumRewardMoney;
        }

        /// <summary>
        /// 添加处罚通知单
        /// </summary>
        /// <param name="punishNotice"></param>
        public static void AddPunishNotice(Model.Check_PunishNotice punishNotice)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Check_PunishNotice newPunishNotice = new Model.Check_PunishNotice
            {
                PunishNoticeId = punishNotice.PunishNoticeId,
                ProjectId = punishNotice.ProjectId,
                PunishNoticeCode = punishNotice.PunishNoticeCode,
                UnitId = punishNotice.UnitId,
                PunishNoticeDate = punishNotice.PunishNoticeDate,
                BasicItem = punishNotice.BasicItem,
                PunishMoney = punishNotice.PunishMoney,
                FileContents = punishNotice.FileContents,
                AttachUrl = punishNotice.AttachUrl,
                CompileMan = punishNotice.CompileMan,
                CompileDate = punishNotice.CompileDate,
                States = punishNotice.States,
                PunishStates = punishNotice.PunishStates,
                SignMan = punishNotice.SignMan,
                ApproveMan = punishNotice.ApproveMan,
                ContractNum = punishNotice.ContractNum,
                IncentiveReason = punishNotice.IncentiveReason,
                Currency = punishNotice.Currency,
                PunishName = punishNotice.PunishName,
            };
            db.Check_PunishNotice.InsertOnSubmit(newPunishNotice);
            db.SubmitChanges();
            CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(Const.ProjectPunishNoticeMenuId, punishNotice.ProjectId, null, punishNotice.PunishNoticeId, punishNotice.CompileDate);
        }

        /// <summary>
        /// 修改处罚通知单
        /// </summary>
        /// <param name="punishNotice"></param>
        public static void UpdatePunishNotice(Model.Check_PunishNotice punishNotice)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Check_PunishNotice newPunishNotice = db.Check_PunishNotice.FirstOrDefault(e => e.PunishNoticeId == punishNotice.PunishNoticeId);
            if (newPunishNotice != null)
            {
                //newPunishNotice.ProjectId = punishNotice.ProjectId;
                //newPunishNotice.PunishNoticeCode = punishNotice.PunishNoticeCode;
                newPunishNotice.UnitId = punishNotice.UnitId;
                newPunishNotice.PunishNoticeDate = punishNotice.PunishNoticeDate;
                newPunishNotice.BasicItem = punishNotice.BasicItem;
                newPunishNotice.PunishMoney = punishNotice.PunishMoney;
                newPunishNotice.FileContents = punishNotice.FileContents;
                newPunishNotice.AttachUrl = punishNotice.AttachUrl;
                newPunishNotice.CompileMan = punishNotice.CompileMan;
                newPunishNotice.CompileDate = punishNotice.CompileDate;
                newPunishNotice.States = punishNotice.States;
                newPunishNotice.PunishStates = punishNotice.PunishStates;
                newPunishNotice.SignMan = punishNotice.SignMan;
                newPunishNotice.ApproveMan = punishNotice.ApproveMan;
                newPunishNotice.ContractNum = punishNotice.ContractNum;
                newPunishNotice.IncentiveReason = punishNotice.IncentiveReason;
                newPunishNotice.Currency = punishNotice.Currency;
                newPunishNotice.PunishName = punishNotice.PunishName;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除处罚通知单
        /// </summary>
        /// <param name="punishNoticeId"></param>
        public static void DeletePunishNoticeById(string punishNoticeId)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Check_PunishNotice punishNotice = db.Check_PunishNotice.FirstOrDefault(e => e.PunishNoticeId == punishNoticeId);
            if (punishNotice != null)
            {
                CodeRecordsService.DeleteCodeRecordsByDataId(punishNoticeId);               
                UploadFileService.DeleteFile(Funs.RootPath, punishNotice.AttachUrl);
                ////删除审核流程表
                var getFlow = db.Check_PunishNoticeFlowOperate.Where(x => x.PunishNoticeId == punishNoticeId);
                if (getFlow.Count() > 0)
                {
                    db.Check_PunishNoticeFlowOperate.DeleteAllOnSubmit(getFlow);
                }
                db.Check_PunishNotice.DeleteOnSubmit(punishNotice);
                db.SubmitChanges();
            }
        }
    }
}