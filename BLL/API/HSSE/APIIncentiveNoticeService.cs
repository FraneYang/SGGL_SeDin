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
                                        &&  x.States == states
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
            Model.SGGLDB db = Funs.DB;
            Model.Check_IncentiveNotice newIncentiveNotice = new Model.Check_IncentiveNotice
            {
                IncentiveNoticeId = newItem.IncentiveNoticeId,
                IncentiveNoticeCode = newItem.IncentiveNoticeCode,
                ProjectId = newItem.ProjectId,
                IncentiveDate = Funs.GetNewDateTime(newItem.IncentiveDate),
                UnitId = newItem.UnitId,               
                RewardType = newItem.RewardTypeId,
                BasicItem = newItem.BasicItem,
                IncentiveMoney = newItem.IncentiveMoney,
                Currency = newItem.Currency,
                TitleReward = newItem.TitleReward,
                MattleReward = newItem.MattleReward,
                FileContents = System.Web.HttpUtility.HtmlEncode(newItem.FileContents),
                CompileMan = newItem.CompileManId,
                AttachUrl = newItem.AttachUrl,
                States = newItem.States,
            };

            if (!string.IsNullOrEmpty(newItem.TeamGroupId))
            {
                newIncentiveNotice.TeamGroupId = newItem.TeamGroupId;
            }
            if (!string.IsNullOrEmpty(newItem.CompileManId))
            {
                newIncentiveNotice.CompileMan = newItem.CompileManId;
            }
            if (!string.IsNullOrEmpty(newItem.PersonId))
            {
                newIncentiveNotice.PersonId = newItem.PersonId;
            }
            if (newIncentiveNotice.States == Const.State_1)
            {
                newIncentiveNotice.SignMan = newItem.SignManId;
            }
            var getUpdate = db.Check_IncentiveNotice.FirstOrDefault(x => x.IncentiveNoticeId == newItem.IncentiveNoticeId);
            if (getUpdate == null)
            {
                newIncentiveNotice.States = Const.State_0;
                newIncentiveNotice.CompileDate = DateTime.Now;
                newIncentiveNotice.IncentiveNoticeId = SQLHelper.GetNewID();
                newIncentiveNotice.IncentiveNoticeCode = CodeRecordsService.ReturnCodeByMenuIdProjectId(Const.ProjectIncentiveNoticeMenuId, newIncentiveNotice.ProjectId, newIncentiveNotice.UnitId);
                IncentiveNoticeService.AddIncentiveNotice(newIncentiveNotice);
            }
               else
                {
                newIncentiveNotice.IncentiveNoticeId = getUpdate.IncentiveNoticeId;
                getUpdate.States = newItem.States;
                if (newIncentiveNotice.States == "0" || newIncentiveNotice.States == "1")  ////编制人 修改或提交
                {
                    getUpdate.IncentiveDate = newIncentiveNotice.IncentiveDate;
                    getUpdate.UnitId = newIncentiveNotice.UnitId;
                    getUpdate.TeamGroupId = newIncentiveNotice.TeamGroupId;
                    getUpdate.PersonId = newIncentiveNotice.PersonId;
                    getUpdate.RewardType = newIncentiveNotice.RewardType;
                    getUpdate.BasicItem = newIncentiveNotice.BasicItem;
                    getUpdate.IncentiveMoney = newIncentiveNotice.IncentiveMoney;
                    getUpdate.Currency = newIncentiveNotice.Currency;
                    getUpdate.TitleReward = newIncentiveNotice.TitleReward;
                    getUpdate.MattleReward = newIncentiveNotice.MattleReward;
                    getUpdate.FileContents = newIncentiveNotice.FileContents;

                    if (newIncentiveNotice.States == "1" && !string.IsNullOrEmpty(newItem.SignManId))
                    {
                        getUpdate.SignMan = newItem.SignManId;
                    }
                    else
                    {
                        newIncentiveNotice.States = getUpdate.States = "0";
                    }
                    db.SubmitChanges();
                }
                else if (newIncentiveNotice.States == "2") ////【签发】总包安全经理
                {
                    /// 不同意 打回 同意抄送专业工程师、施工经理、相关施工分包单位并提交【批准】总包项目经理
                    if (newItem.IsAgree == false)
                    {
                        newIncentiveNotice.States = getUpdate.States = "0";
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(newItem.ProfessionalEngineerId))
                        {
                            getUpdate.ProfessionalEngineerId = newItem.ProfessionalEngineerId;
                        }
                        if (!string.IsNullOrEmpty(newItem.ConstructionManagerId))
                        {
                            getUpdate.ConstructionManagerId = newItem.ConstructionManagerId;
                        }
                        if (!string.IsNullOrEmpty(newItem.UnitHeadManId))
                        {
                            getUpdate.UnitHeadManId = newItem.UnitHeadManId;
                        }
                        if (!string.IsNullOrEmpty(newItem.ApproveManId))
                        {
                            getUpdate.ApproveMan = newItem.ApproveManId;
                            getUpdate.SignDate = DateTime.Now;
                        }
                        else
                        {
                            newIncentiveNotice.States = getUpdate.States = "1";
                        }
                    }
                    db.SubmitChanges();
                }
                else if (newIncentiveNotice.States == "3") ////【批准】总包项目经理
                {
                    /// 不同意 打回 同意下发【回执】施工分包单位
                    if (newItem.IsAgree == false)
                    {
                        newIncentiveNotice.States = getUpdate.States = "1";
                    }
                    else
                    {
                        getUpdate.ApproveDate = DateTime.Now;
                    }
                    db.SubmitChanges();
                }
            }
            //// 增加审核记录
            if (newItem.FlowOperateItem != null && newItem.FlowOperateItem.Count() > 0)
            {
                var getOperate = newItem.FlowOperateItem.FirstOrDefault();
                if (getOperate != null && !string.IsNullOrEmpty(getOperate.OperaterId))
                {
                    Model.Check_IncentiveNoticeFlowOperate newOItem = new Model.Check_IncentiveNoticeFlowOperate
                    {
                        FlowOperateId = SQLHelper.GetNewID(),
                       IncentiveNoticeId = newIncentiveNotice.IncentiveNoticeId,
                        OperateName = getOperate.AuditFlowName,
                        OperateManId = getOperate.OperaterId,
                        OperateTime = DateTime.Now,
                        IsAgree = getOperate.IsAgree,
                        Opinion = getOperate.Opinion,
                    };
                    db.Check_IncentiveNoticeFlowOperate.InsertOnSubmit(newOItem);
                    db.SubmitChanges();
                }
            }
            if (newIncentiveNotice.States == "1" || newIncentiveNotice.States == "0")
            {
                CommonService.btnSaveData(newIncentiveNotice.ProjectId, Const.ProjectIncentiveNoticeMenuId, newIncentiveNotice.IncentiveNoticeId, newIncentiveNotice.CompileMan, true, newIncentiveNotice.IncentiveNoticeCode, "../Check/IncentiveNoticeView.aspx?IncentiveNoticeId={0}");
            }            
        }
        #endregion
    }
}
