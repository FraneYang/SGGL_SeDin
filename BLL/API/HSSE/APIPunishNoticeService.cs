using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmitMapper;

namespace BLL
{
    /// <summary>
    /// 处罚通知单
    /// </summary>
    public static class APIPunishNoticeService
    {
        #region 根据PunishNoticeId获取处罚通知单
        /// <summary>
        ///  根据 PunishNoticeId获取处罚通知单
        /// </summary>
        /// <param name="PunishNoticeId"></param>
        /// <returns></returns>
        public static Model.PunishNoticeItem getPunishNoticeById(string PunishNoticeId)
        {
            var getInfo = from x in new Model.SGGLDB(Funs.ConnString).Check_PunishNotice
                          where x.PunishNoticeId == PunishNoticeId
                          select new Model.PunishNoticeItem
                          {
                              PunishNoticeId = x.PunishNoticeId,
                              ProjectId = x.ProjectId,
                              PunishNoticeCode = x.PunishNoticeCode,
                              UnitId = x.UnitId,
                              UnitName = new Model.SGGLDB(Funs.ConnString).Base_Unit.First(u => u.UnitId == x.UnitId).UnitName,
                              PunishPersonId=x.PunishPersonId,
                              PunishPersonName = new Model.SGGLDB(Funs.ConnString).Sys_User.First(u => u.UserId == x.PunishPersonId).UserName,
                              ContractNum = x.ContractNum,
                              PunishNoticeDate = string.Format("{0:yyyy-MM-dd}", x.PunishNoticeDate),
                              BasicItem = x.BasicItem,
                              IncentiveReason = x.IncentiveReason,
                              PunishMoney = x.PunishMoney ?? 0,
                              Currency = x.Currency,
                              FileContents = System.Web.HttpUtility.HtmlDecode(x.FileContents),
                              CompileManId = x.CompileMan,
                              CompileManName = new Model.SGGLDB(Funs.ConnString).Sys_User.First(u => u.UserId == x.CompileMan).UserName,
                              CompileDate = string.Format("{0:yyyy-MM-dd HH:mm}", x.CompileDate),
                              SignManId = x.SignMan,
                              SignManName = new Model.SGGLDB(Funs.ConnString).Sys_User.First(u => u.UserId == x.SignMan).UserName,
                              SignDate = string.Format("{0:yyyy-MM-dd HH:mm}", x.SignDate),
                              ApproveManId = x.ApproveMan,
                              ApproveManName = new Model.SGGLDB(Funs.ConnString).Sys_User.First(u => u.UserId == x.ApproveMan).UserName,
                              ApproveDate = string.Format("{0:yyyy-MM-dd HH:mm}", x.ApproveDate),
                              DutyPersonId = x.DutyPersonId,
                              DutyPersonName = new Model.SGGLDB(Funs.ConnString).Sys_User.First(u => u.UserId == x.DutyPersonId).UserName,
                              DutyPersonDate = string.Format("{0:yyyy-MM-dd HH:mm}", x.DutyPersonDate),
                              ProfessionalEngineerId = x.ProfessionalEngineerId,
                              ProfessionalEngineerName = new Model.SGGLDB(Funs.ConnString).Sys_User.First(u => u.UserId == x.ProfessionalEngineerId).UserName,
                              ProfessionalEngineerTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.ProfessionalEngineerTime),
                              ConstructionManagerId = x.ConstructionManagerId,
                              ConstructionManagerName = new Model.SGGLDB(Funs.ConnString).Sys_User.First(u => u.UserId == x.ConstructionManagerId).UserName,
                              ConstructionManagerTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.ConstructionManagerTime),
                              UnitHeadManId = x.UnitHeadManId,
                              UnitHeadManName = new Model.SGGLDB(Funs.ConnString).Sys_User.First(u => u.UserId == x.UnitHeadManId).UserName,
                              UnitHeadManTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.UnitHeadManTime),
                              States = x.States,
                              PunishStates = x.PunishStates,
                              PunishUrl = APIUpLoadFileService.getFileUrl(Const.ProjectPunishNoticeStatisticsMenuId, x.PunishNoticeId, null),
                              ReceiptUrl = APIUpLoadFileService.getFileUrl(Const.ProjectPunishNoticeMenuId, x.PunishNoticeId, null),
                              FlowOperateItem = getFlowOperateItem(x.PunishNoticeId),
                              PunishNoticeItemItem = GetPunishNoticeItemList(x.PunishNoticeId),
                          };
            return getInfo.FirstOrDefault();
        }

        public static List<Model.PunishNoticeItemItem> GetPunishNoticeItemList(string punishNoticeId)
        {
            return (from x in new Model.SGGLDB(Funs.ConnString).Check_PunishNoticeItem
                    where x.PunishNoticeId == punishNoticeId
                    orderby x.SortIndex
                    select new Model.PunishNoticeItemItem
                    {
                        PunishNoticeItemId=x.PunishNoticeItemId,
                        PunishNoticeId=x.PunishNoticeId,
                        PunishContent=x.PunishContent,
                        PunishMoney=x.PunishMoney ?? 0,
                        SortIndex =x.SortIndex,
                    }).ToList();
        }
        #endregion     

        #region 根据ID 获取审核信息
        /// <summary>
        ///  根据ID 获取审核信息
        /// </summary>
        /// <returns></returns>
        public static List<Model.FlowOperateItem> getFlowOperateItem(string punishNoticeId)
        {
            var getInfo = from x in new Model.SGGLDB(Funs.ConnString).Check_PunishNoticeFlowOperate
                          where x.PunishNoticeId == punishNoticeId
                          orderby x.OperateTime descending
                          select new Model.FlowOperateItem
                          {
                              FlowOperateId = x.FlowOperateId,
                              DataId = x.PunishNoticeId,
                              AuditFlowName = x.OperateName,
                              OperaterId = x.OperateManId,
                              OperaterName = new Model.SGGLDB(Funs.ConnString).Sys_User.First(z => z.UserId == x.OperateManId).UserName,
                              OperaterTime = string.Format("{0:yyyy-MM-dd HH:mm:ss}", x.OperateTime),
                              IsAgree = x.IsAgree,
                              Opinion = x.Opinion,
                              SignatureUrl = APIUpLoadFileService.getFileUrl(string.Empty, x.SignatureUrl),
                          };
            return getInfo.ToList();
        }
        #endregion

        #region 获取处罚通知单列表信息
        /// <summary>
        /// 获取处罚通知单列表信息
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="unitId"></param>
        /// <param name="strParam"></param>
        /// <returns></returns>
        public static List<Model.PunishNoticeItem> getPunishNoticeList(string projectId, string unitId, string strParam, string states)
        {
            var getPunishNotice = from x in new Model.SGGLDB(Funs.ConnString).Check_PunishNotice
                                  where x.ProjectId == projectId && (x.UnitId == unitId || unitId == null) && x.PunishStates==states
                                  orderby x.PunishNoticeCode descending
                                  select new Model.PunishNoticeItem
                                  {
                                      PunishNoticeId = x.PunishNoticeId,
                                      ProjectId = x.ProjectId,
                                      PunishNoticeCode = x.PunishNoticeCode,
                                      UnitId = x.UnitId,
                                      UnitName = new Model.SGGLDB(Funs.ConnString).Base_Unit.First(u => u.UnitId == x.UnitId).UnitName,
                                      ContractNum = x.ContractNum,
                                      PunishNoticeDate = string.Format("{0:yyyy-MM-dd}", x.PunishNoticeDate),
                                      BasicItem = x.BasicItem,
                                      IncentiveReason = x.IncentiveReason,
                                      PunishMoney = x.PunishMoney ?? 0,
                                      Currency = x.Currency,
                                      //FileContents = System.Web.HttpUtility.HtmlDecode(x.FileContents),
                                      CompileManId = x.CompileMan,
                                      CompileManName = new Model.SGGLDB(Funs.ConnString).Sys_User.First(u => u.UserId == x.CompileMan).UserName,
                                      CompileDate = string.Format("{0:yyyy-MM-dd}", x.CompileDate),
                                      //SignManId = x.SignMan,
                                      //SignManName = new Model.SGGLDB(Funs.ConnString).Sys_User.First(u => u.UserId == x.SignMan).UserName,
                                      //ApproveManId = x.ApproveMan,
                                      //ApproveManName = new Model.SGGLDB(Funs.ConnString).Sys_User.First(u => u.UserId == x.ApproveMan).UserName,
                                      States = x.States,
                                      PunishStates=x.PunishStates,
                                      //PunishUrl = new Model.SGGLDB(Funs.ConnString).AttachFile.FirstOrDefault(z => z.MenuId == Const.ProjectPunishNoticeStatisticsMenuId && z.ToKeyId == x.PunishNoticeId).AttachUrl.Replace('\\', '/'),
                                      //ReceiptUrl = new Model.SGGLDB(Funs.ConnString).AttachFile.FirstOrDefault(z => z.MenuId == Const.ProjectPunishNoticeMenuId && z.ToKeyId == x.PunishNoticeId).AttachUrl.Replace('\\', '/'),
                                  };
            if (!string.IsNullOrEmpty(strParam))
            {
                getPunishNotice = getPunishNotice.Where(x => x.PunishNoticeCode.Contains(strParam) || x.IncentiveReason.Contains(strParam));
            }
            return getPunishNotice.ToList();
        }
        #endregion        

        #region 保存Check_PunishNotice
        /// <summary>
        /// 保存Check_PunishNotice
        /// </summary>
        /// <param name="newItem">处罚通知单</param>
        /// <returns></returns>
        public static void SavePunishNotice(Model.PunishNoticeItem newItem)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                bool insertPunishNoticeItemItem = false;
                Model.Check_PunishNotice newPunishNotice = new Model.Check_PunishNotice
                {
                    PunishNoticeId = newItem.PunishNoticeId,
                    PunishNoticeCode = newItem.PunishNoticeCode,
                    ProjectId = newItem.ProjectId,
                    PunishNoticeDate = Funs.GetNewDateTime(newItem.PunishNoticeDate),
                    UnitId = newItem.UnitId,
                    ContractNum = newItem.ContractNum,
                    IncentiveReason = newItem.IncentiveReason,
                    BasicItem = newItem.BasicItem,
                    PunishMoney = newItem.PunishMoney,
                    Currency = newItem.Currency,
                    FileContents = System.Web.HttpUtility.HtmlEncode(newItem.FileContents),               
                    States = Const.State_0,
                    PunishStates = newItem.PunishStates,
                };

                if (!string.IsNullOrEmpty(newItem.CompileManId))
                {
                    newPunishNotice.CompileMan = newItem.CompileManId;
                }
                if (!string.IsNullOrEmpty(newItem.PunishPersonId))
                {
                    newPunishNotice.PunishPersonId = newItem.PunishPersonId;
                }
                if (newPunishNotice.PunishStates == Const.State_1)
                {
                    newPunishNotice.SignMan = newItem.SignManId;
                }

                var getUpdate = db.Check_PunishNotice.FirstOrDefault(x => x.PunishNoticeId == newItem.PunishNoticeId);
                if (getUpdate == null)
                {
                    insertPunishNoticeItemItem = true;
                    newPunishNotice.CompileDate = DateTime.Now;
                    newPunishNotice.PunishNoticeId = SQLHelper.GetNewID();
                    newPunishNotice.PunishNoticeCode = CodeRecordsService.ReturnCodeByMenuIdProjectId(Const.ProjectPunishNoticeMenuId, newPunishNotice.ProjectId, newPunishNotice.UnitId);
                    db.Check_PunishNotice.InsertOnSubmit(newPunishNotice);
                    db.SubmitChanges();
                    CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(Const.ProjectPunishNoticeMenuId, newPunishNotice.ProjectId, newPunishNotice.UnitId, newPunishNotice.PunishNoticeId, newPunishNotice.CompileDate); 
               
                    //// 回写巡检记录表
                    if (!string.IsNullOrEmpty(newItem.HazardRegisterId))
                    {
                        List<string> listIds = Funs.GetStrListByStr(newItem.HazardRegisterId, ',');
                        foreach (var item in listIds)
                        {
                            var getHazardRegister = db.HSSE_Hazard_HazardRegister.FirstOrDefault(x => x.HazardRegisterId == item);
                            if (getHazardRegister != null)
                            {
                                getHazardRegister.States = "3";
                                getHazardRegister.HandleIdea += "已下发处罚通知单：" + newPunishNotice.PunishNoticeCode;
                                getHazardRegister.ResultId = newPunishNotice.PunishNoticeId;
                                getHazardRegister.ResultType = "2";
                                db.SubmitChanges();
                            }
                        }
                    }

                    //// 回写专项检查明细表
                    if (!string.IsNullOrEmpty(newItem.CheckSpecialDetailId))
                    {
                        List<string> listIds = Funs.GetStrListByStr(newItem.CheckSpecialDetailId, ',');
                        foreach (var item in listIds)
                        {
                            var getCheckSpecialDetail = db.Check_CheckSpecialDetail.FirstOrDefault(x => x.CheckSpecialDetailId == item);
                            if (getCheckSpecialDetail != null)
                            {
                                string dataType = string.Empty;
                                string dataId = string.Empty;
                                if (string.IsNullOrEmpty(getCheckSpecialDetail.DataType))
                                {
                                    dataType = "2";
                                    dataId = "2," + newPunishNotice.PunishNoticeId;
                                }
                                else
                                {
                                    dataType = getCheckSpecialDetail.DataType+",2";
                                    dataId = getCheckSpecialDetail.DataId + "|2," + newPunishNotice.PunishNoticeId;
                                }
                                getCheckSpecialDetail.DataType = dataType;
                                getCheckSpecialDetail.DataId = dataId;
                                db.SubmitChanges();
                            }
                        }
                    }
                }
                else
                {
                    newPunishNotice.PunishNoticeId = getUpdate.PunishNoticeId;
                    getUpdate.PunishStates = newItem.PunishStates;
                    if (newPunishNotice.PunishStates == "0" || newPunishNotice.PunishStates == "1")  ////编制人 修改或提交
                    {
                        var geDeleteItems = from x in db.Check_PunishNoticeItem
                                            where x.PunishNoticeId == getUpdate.PunishNoticeId
                                            select x;
                        if (geDeleteItems.Count() > 0)
                        {
                            db.Check_PunishNoticeItem.DeleteAllOnSubmit(geDeleteItems);
                            db.SubmitChanges();
                        }
                        insertPunishNoticeItemItem = true;
                        getUpdate.PunishNoticeDate = newPunishNotice.PunishNoticeDate;
                        getUpdate.UnitId = newPunishNotice.UnitId;
                        getUpdate.ContractNum = newPunishNotice.ContractNum;
                        getUpdate.IncentiveReason = newPunishNotice.IncentiveReason;
                        getUpdate.BasicItem = newPunishNotice.BasicItem;
                        getUpdate.PunishMoney = newPunishNotice.PunishMoney;
                        getUpdate.Currency = newPunishNotice.Currency;
                        getUpdate.FileContents = newPunishNotice.FileContents;
                        if (newPunishNotice.PunishStates == "1" && !string.IsNullOrEmpty(newItem.SignManId))
                        {
                            getUpdate.SignMan = newItem.SignManId;
                        }
                        else
                        {
                            newPunishNotice.PunishStates = getUpdate.PunishStates = "0";
                        }
                        db.SubmitChanges();                     
                    }
                    else if (newPunishNotice.PunishStates == "2") ////【签发】总包安全经理
                    {
                        /// 不同意 打回 同意抄送专业工程师、施工经理、相关施工分包单位并提交【批准】总包项目经理
                        if (newItem.IsAgree == false)
                        {
                            newPunishNotice.PunishStates = getUpdate.PunishStates = "0";
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
                                newPunishNotice.PunishStates = getUpdate.States = "1";
                            }
                        }
                        db.SubmitChanges();
                    }
                    else if (newPunishNotice.PunishStates == "3") ////【批准】总包项目经理
                    {
                        /// 不同意 打回 同意下发【回执】施工分包单位
                        if (newItem.IsAgree == false || string.IsNullOrEmpty(newItem.DutyPersonId))
                        {
                            newPunishNotice.PunishStates = getUpdate.PunishStates = "1";
                        }
                        else
                        {
                            getUpdate.DutyPersonId = newItem.DutyPersonId;
                            getUpdate.ApproveDate = DateTime.Now;
                        }
                        db.SubmitChanges();
                    }
                    else if (newPunishNotice.PunishStates == "4") ////【批准】总包项目经理
                    {
                        /// 不同意 打回 同意下发【回执】施工分包单位
                        if (string.IsNullOrEmpty(newItem.ReceiptUrl))
                        {
                            newPunishNotice.PunishStates = getUpdate.PunishStates = "3";
                        }
                        else
                        {
                            getUpdate.DutyPersonDate = DateTime.Now;
                            getUpdate.States = Const.State_2;
                        }
                        db.SubmitChanges();
                    }
                }

                if (insertPunishNoticeItemItem)
                {
                    //// 新增明细
                    if (newItem.PunishNoticeItemItem != null && newItem.PunishNoticeItemItem.Count() > 0)
                    {
                        foreach (var rItem in newItem.PunishNoticeItemItem)
                        {
                            Model.Check_PunishNoticeItem newPItem = new Model.Check_PunishNoticeItem
                            {
                                PunishNoticeItemId = SQLHelper.GetNewID(),
                                PunishNoticeId = newPunishNotice.PunishNoticeId,
                                PunishContent = rItem.PunishContent,
                                SortIndex = rItem.SortIndex,
                                PunishMoney=rItem.PunishMoney,
                            };
                            db.Check_PunishNoticeItem.InsertOnSubmit(newPItem);
                            db.SubmitChanges();
                        }
                    }
                }

                //// 增加审核记录
                if (newItem.FlowOperateItem != null && newItem.FlowOperateItem.Count() > 0)
                {
                    var getOperate = newItem.FlowOperateItem.FirstOrDefault();
                    if (getOperate != null && !string.IsNullOrEmpty(getOperate.OperaterId))
                    {
                        Model.Check_PunishNoticeFlowOperate newOItem = new Model.Check_PunishNoticeFlowOperate
                        {
                            FlowOperateId = SQLHelper.GetNewID(),
                            PunishNoticeId = newPunishNotice.PunishNoticeId,
                            OperateName = getOperate.AuditFlowName,
                            OperateManId = getOperate.OperaterId,
                            OperateTime = DateTime.Now,
                            IsAgree = getOperate.IsAgree,
                            Opinion = getOperate.Opinion,
                        };
                        db.Check_PunishNoticeFlowOperate.InsertOnSubmit(newOItem);
                        db.SubmitChanges();
                    }
                }

                if (newItem.PunishStates == Const.State_0 || newItem.PunishStates == Const.State_1)
                {     //// 通知单附件
                    APIUpLoadFileService.SaveAttachUrl(Const.ProjectPunishNoticeStatisticsMenuId, newPunishNotice.PunishNoticeId, newItem.PunishUrl, "0");
                }
                if (newItem.PunishStates == Const.State_4)
                {     //// 回执单附件
                    APIUpLoadFileService.SaveAttachUrl(Const.ProjectPunishNoticeMenuId, newPunishNotice.PunishNoticeId, newItem.ReceiptUrl, "0");
                }
                if (getUpdate != null && getUpdate.States == Const.State_2)
                {
                    CommonService.btnSaveData(newPunishNotice.ProjectId, Const.ProjectPunishNoticeMenuId, newPunishNotice.PunishNoticeId, newPunishNotice.CompileMan, true, newPunishNotice.PunishNoticeCode, "../Check/PunishNoticeView.aspx?PunishNoticeId={0}");
                    var getcheck = from x in db.Check_CheckSpecialDetail where x.DataId.Contains(getUpdate.PunishNoticeId) select x;
                    if (getcheck.Count() > 0)
                    {
                        foreach (var item in getcheck)
                        {
                            item.CompleteStatus = true;
                            item.CompletedDate = DateTime.Now;
                            db.SubmitChanges();
                        }
                    }
                }
            }
        }
        #endregion

        #region 保存处罚单-通知单回执单
        /// <summary>
        /// 保存处罚单-回执单
        /// </summary>
        /// <param name="punishNoticeId">主键</param>
        /// <param name="attachUrl">回执单路径</param>
        public static void SavePunishNoticeReceiptUrl(string punishNoticeId, string attachUrl, string type)
        {
            var getPunishNotice = new Model.SGGLDB(Funs.ConnString).Check_PunishNotice.FirstOrDefault(x => x.PunishNoticeId == punishNoticeId);
            if (getPunishNotice != null)
            {
                string menuId = Const.ProjectPunishNoticeMenuId;
                if (type == "0")
                {
                    menuId = Const.ProjectPunishNoticeStatisticsMenuId;
                }
                ////保存附件
                if (!string.IsNullOrEmpty(attachUrl))
                {
                    UploadFileService.SaveAttachUrl(UploadFileService.GetSourceByAttachUrl(attachUrl, 10, null), attachUrl, menuId, getPunishNotice.PunishNoticeId);
                }
                else
                {
                    CommonService.DeleteAttachFileById(menuId, getPunishNotice.PunishNoticeId);
                }
            }
        }
        #endregion
    }
}
