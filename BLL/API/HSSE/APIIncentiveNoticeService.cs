using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmitMapper;

namespace BLL
{
    /// <summary>
    /// 奖励通知单
    /// </summary>
    public static class APIIncentiveNoticeService
    {
        #region 根据IncentiveNoticeId获取奖励通知单
        /// <summary>
        ///  根据 IncentiveNoticeId获取奖励通知单
        /// </summary>
        /// <param name="incentiveNoticeId"></param>
        /// <returns></returns>
        public static Model.IncentiveNoticeItem getIncentiveNoticeById(string incentiveNoticeId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var getInfo = from x in db.Check_IncentiveNotice
                              where x.IncentiveNoticeId == incentiveNoticeId
                              select new Model.IncentiveNoticeItem
                              {
                                  IncentiveNoticeId = x.IncentiveNoticeId,
                                  ProjectId = x.ProjectId,
                                  IncentiveNoticeCode = x.IncentiveNoticeCode,
                                  IncentiveDate = string.Format("{0:yyyy-MM-dd}", x.IncentiveDate),
                                  RewardTypeId = x.RewardType,
                                  RewardTypeName = db.Sys_Const.First(y => y.GroupId == ConstValue.Group_RewardType && y.ConstValue == x.RewardType).ConstText,
                                  UnitId = x.UnitId,
                                  UnitName = db.Base_Unit.First(u => u.UnitId == x.UnitId).UnitName,
                                  TeamGroupId = x.TeamGroupId,
                                  TeamGroupName = db.ProjectData_TeamGroup.First(u => u.TeamGroupId == x.TeamGroupId).TeamGroupName,
                                  PersonId = x.PersonId,
                                  PersonName = db.SitePerson_Person.First(u => u.PersonId == x.PersonId).PersonName,
                                  BasicItem = x.BasicItem,
                                  IncentiveMoney = x.IncentiveMoney ?? 0,
                                  Currency = x.Currency,
                                  TitleReward = x.TitleReward,
                                  MattleReward = x.MattleReward,
                                  FileContents = System.Web.HttpUtility.HtmlDecode(x.FileContents),
                                  CompileManId = x.CompileMan,
                                  CompileManName = db.Sys_User.First(u => u.UserId == x.CompileMan).UserName,
                                  CompileDate = string.Format("{0:yyyy-MM-dd}", x.CompileDate),
                                  SignManId = x.SignMan,
                                  SignManName = db.Sys_User.First(u => u.UserId == x.SignMan).UserName,
                                  ApproveManId = x.ApproveMan,
                                  ApproveManName = db.Sys_User.First(u => u.UserId == x.ApproveMan).UserName,
                                  States = x.States,
                                  AttachUrl = x.AttachUrl.Replace('\\', '/'),
                              };
                return getInfo.FirstOrDefault();
            }
        }
        #endregion        

        #region 获取奖励通知单列表信息
        /// <summary>
        /// 获取奖励通知单列表信息
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="unitId"></param>
        /// <param name="strParam"></param>
        /// <returns></returns>
        public static List<Model.IncentiveNoticeItem> getIncentiveNoticeList(string projectId, string unitId, string strParam, string states)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var getIncentiveNotice = from x in db.Check_IncentiveNotice
                                         where x.ProjectId == projectId && (x.UnitId == unitId || unitId == null)
                                        //&& (strParam == null || x.IncentiveNoticeName.Contains(strParam)) 
                                        && (states == null || (states == "0" && (x.States == "0" || x.States == null)) || (states == "1" && (x.States == "1" || x.States == "2")))
                                         orderby x.IncentiveNoticeCode descending
                                         select new Model.IncentiveNoticeItem
                                         {
                                             IncentiveNoticeId = x.IncentiveNoticeId,
                                             ProjectId = x.ProjectId,
                                             IncentiveNoticeCode = x.IncentiveNoticeCode,
                                             IncentiveDate = string.Format("{0:yyyy-MM-dd}", x.IncentiveDate),
                                             RewardTypeId = x.RewardType,
                                             RewardTypeName = db.Sys_Const.First(y => y.GroupId == ConstValue.Group_RewardType && y.ConstValue == x.RewardType).ConstText,
                                             UnitId = x.UnitId,
                                             UnitName = db.Base_Unit.First(u => u.UnitId == x.UnitId).UnitName,
                                             TeamGroupId = x.TeamGroupId,
                                             TeamGroupName = db.ProjectData_TeamGroup.First(u => u.TeamGroupId == x.TeamGroupId).TeamGroupName,
                                             PersonId = x.PersonId,
                                             PersonName = db.SitePerson_Person.First(u => u.PersonId == x.PersonId).PersonName,
                                             BasicItem = x.BasicItem,
                                             IncentiveMoney = x.IncentiveMoney,
                                             Currency = x.Currency,
                                             TitleReward = x.TitleReward,
                                             MattleReward = x.MattleReward,
                                             // FileContents = System.Web.HttpUtility.HtmlDecode(x.FileContents),
                                             CompileManId = x.CompileMan,
                                             CompileManName = db.Sys_User.First(u => u.UserId == x.CompileMan).UserName,
                                             CompileDate = string.Format("{0:yyyy-MM-dd}", x.CompileDate),
                                             //SignManId = x.SignMan,
                                             //SignManName = db.Sys_User.First(u => u.UserId == x.SignMan).UserName,
                                             // ApproveManId = x.ApproveMan,
                                             //ApproveManName = db.Sys_User.First(u => u.UserId == x.ApproveMan).UserName,
                                             States = x.States,
                                             //AttachUrl = x.AttachUrl.Replace('\\', '/'),
                                         };
                return getIncentiveNotice.ToList();
            }
        }
        #endregion        

        #region 保存Check_IncentiveNotice
        /// <summary>
        /// 保存Check_IncentiveNotice
        /// </summary>
        /// <param name="newItem">奖励通知单</param>
        /// <returns></returns>
        public static void SaveIncentiveNotice(Model.IncentiveNoticeItem newItem)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Check_IncentiveNotice newIncentiveNotice = new Model.Check_IncentiveNotice
            {
                IncentiveNoticeId = newItem.IncentiveNoticeId,
                IncentiveNoticeCode = newItem.IncentiveNoticeCode,
                ProjectId = newItem.ProjectId,
                IncentiveDate = Funs.GetNewDateTime(newItem.IncentiveDate),
                UnitId = newItem.UnitId,
                PersonId = newItem.PersonId,                
                RewardType = newItem.RewardTypeId,
                BasicItem = newItem.BasicItem,
                IncentiveMoney = newItem.IncentiveMoney,
                Currency = newItem.Currency,
                TitleReward = newItem.TitleReward,
                MattleReward = newItem.MattleReward,
                SignMan = newItem.SignManId,
                ApproveMan = newItem.ApproveManId,
                FileContents = System.Web.HttpUtility.HtmlEncode(newItem.FileContents),
                CompileMan = newItem.CompileManId,
                AttachUrl = newItem.AttachUrl,
                States = Const.State_2,
            };

            if (!string.IsNullOrEmpty(newItem.TeamGroupId))
            {
                newIncentiveNotice.TeamGroupId = newItem.TeamGroupId;
            }

            if (newItem.States != "1")
            {
                newIncentiveNotice.States = Const.State_0;
            }

            var updateIncentiveNotice = db.Check_IncentiveNotice.FirstOrDefault(x => x.IncentiveNoticeId == newItem.IncentiveNoticeId);
            if (updateIncentiveNotice == null)
            {
                newIncentiveNotice.CompileDate = DateTime.Now;
                newIncentiveNotice.IncentiveNoticeId = SQLHelper.GetNewID();
                newIncentiveNotice.IncentiveNoticeCode = CodeRecordsService.ReturnCodeByMenuIdProjectId(Const.ProjectIncentiveNoticeMenuId, newIncentiveNotice.ProjectId, newIncentiveNotice.UnitId);
                IncentiveNoticeService.AddIncentiveNotice(newIncentiveNotice);
            }
            else
            {
                IncentiveNoticeService.UpdateIncentiveNotice(newIncentiveNotice);
            }
            if (newIncentiveNotice.States == "1")
            {
                CommonService.btnSaveData(newIncentiveNotice.ProjectId, Const.ProjectIncentiveNoticeMenuId, newIncentiveNotice.IncentiveNoticeId, newIncentiveNotice.CompileMan, true, newIncentiveNotice.IncentiveNoticeCode, "../Check/IncentiveNoticeView.aspx?IncentiveNoticeId={0}");
            }            
        }
        #endregion
    }
}
