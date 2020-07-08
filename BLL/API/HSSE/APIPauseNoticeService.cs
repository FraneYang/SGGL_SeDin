using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    /// <summary>
    ///  工程暂停令
    /// </summary>
    public static class APIPauseNoticeService
    {
        #region 根据PauseNoticeId获取工程暂停令
        /// <summary>
        ///  根据 PauseNoticeId获取工程暂停令
        /// </summary>
        /// <param name="PauseNoticeId"></param>
        /// <returns></returns>
        public static Model.PauseNoticeItem getPauseNoticeById(string PauseNoticeId)
        {
            var getInfo = from x in new Model.SGGLDB(Funs.ConnString).Check_PauseNotice
                          where x.PauseNoticeId == PauseNoticeId
                          select new Model.PauseNoticeItem
                          {
                              PauseNoticeId = x.PauseNoticeId,
                              ProjectId = x.ProjectId,
                              PauseNoticeCode = x.PauseNoticeCode,
                              UnitId = x.UnitId,
                              UnitName = new Model.SGGLDB(Funs.ConnString).Base_Unit.First(u => u.UnitId == x.UnitId).UnitName,
                              ProjectPlace = x.ProjectPlace,
                              WrongContent = x.WrongContent,
                              PauseTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.PauseTime),
                              PauseContent = x.PauseContent,
                              OneContent = x.OneContent,
                              SecondContent = x.SecondContent,
                              ThirdContent = x.ThirdContent,                       
                              IsConfirm = x.IsConfirm,
                              IsConfirmName = (x.IsConfirm == true ? "已确认" : "待确认"),
                              CompileManId = x.CompileManId,
                              CompileManName = new Model.SGGLDB(Funs.ConnString).Sys_User.First(u => u.UserId == x.CompileManId).UserName,
                              CompileDate = string.Format("{0:yyyy-MM-dd HH:mm}", x.CompileDate),
                              SignManId = x.SignManId,
                              SignManName = new Model.SGGLDB(Funs.ConnString).Sys_User.First(u => u.UserId == x.SignManId).UserName,
                              SignDate = string.Format("{0:yyyy-MM-dd HH:mm}", x.SignDate),
                              ApproveManId = x.ApproveManId,
                              ApproveManName = new Model.SGGLDB(Funs.ConnString).Sys_User.First(u => u.UserId == x.ApproveManId).UserName,
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
                              SupervisorManId = x.SupervisorManId,
                              SupervisorManName = new Model.SGGLDB(Funs.ConnString).Sys_User.First(u => u.UserId == x.SupervisorManId).UserName,
                              SupervisorManTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.SupervisorManTime),
                              OwnerId = x.OwnerId,
                              OwnerName = new Model.SGGLDB(Funs.ConnString).Sys_User.First(u => u.UserId == x.OwnerId).UserName,
                              OwnerTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.OwnerTime),
                              States = x.States,
                              PauseStates = x.PauseStates,
                              PauseNoticeAttachUrl = APIUpLoadFileService.getFileUrl(x.PauseNoticeId, null),
                              FlowOperateItem = getFlowOperateItem(x.PauseNoticeId),
                          };
            return getInfo.FirstOrDefault();
        }
        #endregion        

        #region 根据ID 获取审核信息
        /// <summary>
        ///  根据ID 获取审核信息
        /// </summary>
        /// <returns></returns>
        public static List<Model.FlowOperateItem> getFlowOperateItem(string pauseNoticeId)
        {
            var getInfo = from x in new Model.SGGLDB(Funs.ConnString).Check_PauseNoticeFlowOperate
                          where x.PauseNoticeId == pauseNoticeId
                          orderby x.OperateTime descending
                          select new Model.FlowOperateItem
                          {
                              FlowOperateId = x.FlowOperateId,
                              DataId = x.PauseNoticeId,
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

        #region 获取工程暂停令列表信息
        /// <summary>
        /// 获取工程暂停令列表信息
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="unitId"></param>
        /// <param name="strParam"></param>
        /// <returns></returns>
        public static List<Model.PauseNoticeItem> getPauseNoticeList(string projectId, string unitId, string strParam, string states)
        {
            var getPauseNotice = from x in new Model.SGGLDB(Funs.ConnString).Check_PauseNotice
                                  where x.ProjectId == projectId && (x.UnitId == unitId || unitId == null) && x.PauseStates == states
                                  select new Model.PauseNoticeItem
                                  {
                                      PauseNoticeId = x.PauseNoticeId,
                                      ProjectId = x.ProjectId,
                                      PauseNoticeCode = x.PauseNoticeCode,
                                      UnitId = x.UnitId,
                                      UnitName = new Model.SGGLDB(Funs.ConnString).Base_Unit.First(u => u.UnitId == x.UnitId).UnitName,
                                      ProjectPlace = x.ProjectPlace,
                                      WrongContent = x.WrongContent,
                                      PauseTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.PauseTime),
                                      PauseContent = x.PauseContent,                                    
                                      IsConfirm = x.IsConfirm,
                                      IsConfirmName = (x.IsConfirm == true ? "已确认" : "待确认"),
                                      CompileManId = x.CompileManId,
                                      CompileManName = new Model.SGGLDB(Funs.ConnString).Sys_User.First(z=>z.UserId ==x.CompileManId).UserName,
                                      CompileDate = string.Format("{0:yyyy-MM-dd HH:mm}", x.CompileDate),
                                      States = x.States,
                                      PauseStates = x.PauseStates,
                                      PauseNoticeAttachUrl = APIUpLoadFileService.getFileUrl(x.PauseNoticeId, null),
                                  };
            if (!string.IsNullOrEmpty(strParam))
            {
                getPauseNotice = getPauseNotice.Where(x => x.PauseNoticeCode.Contains(strParam) || x.WrongContent.Contains(strParam) || x.PauseContent.Contains(strParam));
            }
            return getPauseNotice.OrderByDescending(x=> x.PauseNoticeCode).ToList();
        }
        #endregion        

        #region 保存Check_PauseNotice
        /// <summary>
        /// 保存Check_PauseNotice
        /// </summary>
        /// <param name="newItem">工程暂停令</param>
        /// <returns></returns>
        public static void SavePauseNotice(Model.PauseNoticeItem newItem)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                Model.Check_PauseNotice newPauseNotice = new Model.Check_PauseNotice
                {
                    PauseNoticeId = newItem.PauseNoticeId,
                    PauseNoticeCode = newItem.PauseNoticeCode,
                    ProjectId = newItem.ProjectId,
                    UnitId = newItem.UnitId,
                    ProjectPlace = newItem.ProjectPlace,
                    WrongContent = newItem.WrongContent,
                    PauseTime = Funs.GetNewDateTime(newItem.PauseTime),
                    PauseContent = newItem.PauseContent,
                    OneContent = newItem.OneContent,
                    SecondContent = newItem.SecondContent,
                    ThirdContent = newItem.ThirdContent,
                    States = Const.State_0,
                    PauseStates = newItem.PauseStates,
                };
                if (!string.IsNullOrEmpty(newItem.CompileManId))
                {
                    newPauseNotice.CompileManId = newItem.CompileManId;
                }
                if (newPauseNotice.PauseStates == Const.State_1)
                {
                    newPauseNotice.SignManId = newItem.SignManId;
                }
                var getUpdate = db.Check_PauseNotice.FirstOrDefault(x => x.PauseNoticeId == newItem.PauseNoticeId);
                if (getUpdate == null)
                {
                    newPauseNotice.CompileDate = DateTime.Now;
                    newPauseNotice.PauseNoticeId = SQLHelper.GetNewID();
                    newPauseNotice.PauseNoticeCode = CodeRecordsService.ReturnCodeByMenuIdProjectId(Const.ProjectPauseNoticeMenuId, newPauseNotice.ProjectId, newPauseNotice.UnitId);                    
                    db.Check_PauseNotice.InsertOnSubmit(newPauseNotice);                    
                    db.SubmitChanges();

                    CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(Const.ProjectPauseNoticeMenuId, newPauseNotice.ProjectId, newPauseNotice.UnitId, newPauseNotice.PauseNoticeId, newPauseNotice.CompileDate);
                    //// 回写巡检记录表
                    if (!string.IsNullOrEmpty(newItem.HazardRegisterId))
                    {
                        List<string> listIds = Funs.GetStrListByStr(newItem.HazardRegisterId, ',');
                        foreach (var item in listIds)
                        {
                            var getHazardRegister = new Model.SGGLDB(Funs.ConnString).HSSE_Hazard_HazardRegister.FirstOrDefault(x => x.HazardRegisterId == item);
                            if (getHazardRegister != null)
                            {
                                getHazardRegister.States = "3";
                                getHazardRegister.HandleIdea += "已下发工程暂停令：" + newPauseNotice.PauseNoticeCode;
                                getHazardRegister.ResultId = newPauseNotice.PauseNoticeId;
                                getHazardRegister.ResultType = "3";
                                Funs.SubmitChanges();
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
                                    dataType = "3";
                                    dataId = "3," + newPauseNotice.PauseNoticeId;
                                }
                                else
                                {
                                    dataType = getCheckSpecialDetail.DataType + ",3";
                                    dataId = getCheckSpecialDetail.DataId + "|3," + newPauseNotice.PauseNoticeId;
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
                    newPauseNotice.PauseNoticeId = getUpdate.PauseNoticeId;
                    getUpdate.PauseStates = newItem.PauseStates;
                    if (newPauseNotice.PauseStates == "0" || newPauseNotice.PauseStates == "1")  ////编制人 修改或提交
                    {
                        getUpdate.UnitId = newPauseNotice.UnitId;
                        getUpdate.ProjectPlace = newPauseNotice.ProjectPlace;
                        getUpdate.WrongContent = newPauseNotice.WrongContent;
                        getUpdate.PauseTime = newPauseNotice.PauseTime;
                        getUpdate.PauseContent = newPauseNotice.PauseContent;
                        getUpdate.OneContent = newPauseNotice.OneContent;
                        getUpdate.SecondContent = newPauseNotice.SecondContent;
                        getUpdate.ThirdContent = newPauseNotice.ThirdContent;
                        if (newPauseNotice.PauseStates == "1" && !string.IsNullOrEmpty(newItem.SignManId))
                        {
                            getUpdate.SignManId = newItem.SignManId;
                        }
                        else
                        {
                            newPauseNotice.PauseStates = getUpdate.PauseStates = "0";
                        }
                    }
                    else if (newPauseNotice.PauseStates == "2") ////【签发】总包安全经理
                    {
                        /// 不同意 打回 同意抄送专业工程师、施工经理、相关施工分包单位并提交【批准】总包项目经理
                        if (newItem.IsAgree == false)
                        {
                            newPauseNotice.PauseStates = getUpdate.PauseStates = "0";
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
                            if (!string.IsNullOrEmpty(newItem.SupervisorManId))
                            {
                                getUpdate.SupervisorManId = newItem.SupervisorManId;
                            }
                            if (!string.IsNullOrEmpty(newItem.OwnerId))
                            {
                                getUpdate.OwnerId = newItem.OwnerId;
                            }
                            if (!string.IsNullOrEmpty(newItem.ApproveManId))
                            {
                                getUpdate.ApproveManId = newItem.ApproveManId;
                                getUpdate.SignDate = DateTime.Now;
                            }
                            else
                            {
                                newPauseNotice.PauseStates = getUpdate.States = "1";
                            }
                        }
                    }
                    else if (newPauseNotice.PauseStates == "3") ////【批准】总包项目经理
                    {
                        /// 不同意 打回 同意下发【回执】施工分包单位
                        if (newItem.IsAgree == false || string.IsNullOrEmpty(newItem.DutyPersonId))
                        {
                            newPauseNotice.PauseStates = getUpdate.PauseStates = "1";
                        }
                        else
                        {
                            getUpdate.DutyPersonId = newItem.DutyPersonId;
                            getUpdate.ApproveDate = DateTime.Now;
                            getUpdate.IsConfirm = true;
                        }
                    }
                    else if (newPauseNotice.PauseStates == "4") ////【批准】总包项目经理
                    {
                        getUpdate.DutyPersonDate = DateTime.Now;
                        getUpdate.States = Const.State_2;                      
                    }

                    db.SubmitChanges();
                }


                //// 增加审核记录
                if (newItem.FlowOperateItem != null && newItem.FlowOperateItem.Count() > 0)
                {
                    var getOperate = newItem.FlowOperateItem.FirstOrDefault();
                    if (getOperate != null && !string.IsNullOrEmpty(getOperate.OperaterId))
                    {
                        Model.Check_PauseNoticeFlowOperate newOItem = new Model.Check_PauseNoticeFlowOperate
                        {
                            FlowOperateId = SQLHelper.GetNewID(),
                            PauseNoticeId = newPauseNotice.PauseNoticeId,
                            OperateName = getOperate.AuditFlowName,
                            OperateManId = getOperate.OperaterId,
                            OperateTime = DateTime.Now,
                            IsAgree = getOperate.IsAgree,
                            Opinion = getOperate.Opinion,
                        };
                        db.Check_PauseNoticeFlowOperate.InsertOnSubmit(newOItem);
                        db.SubmitChanges();
                    }
                }

                if (newItem.PauseStates == Const.State_0 || newItem.PauseStates == Const.State_1)
                {     //// 通知单附件
                    UploadFileService.SaveAttachUrl(UploadFileService.GetSourceByAttachUrl(newItem.PauseNoticeAttachUrl, 10, null), newItem.PauseNoticeAttachUrl, Const.ProjectPauseNoticeMenuId, newPauseNotice.PauseNoticeId);
                }
                if (getUpdate != null &&  getUpdate.States == Const.State_2)
                {
                    CommonService.btnSaveData(newPauseNotice.ProjectId, Const.ProjectPauseNoticeMenuId, newPauseNotice.PauseNoticeId, newPauseNotice.CompileManId, true, newPauseNotice.PauseContent, "../Check/PauseNoticeView.aspx?PauseNoticeId={0}");

                    var getcheck = from x in db.Check_CheckSpecialDetail where x.DataId.Contains(getUpdate.PauseNoticeId) select x;
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

        #region 暂停令通知单附件
        /// <summary>
        /// 暂停令通知单附件
        /// </summary>
        /// <param name="pauseNoticeId">主键</param>
        /// <param name="attachUrl">路径</param>
        public static void SavePauseNoticeUrl(string pauseNoticeId, string attachUrl)
        {
            var getPauseNotice = new Model.SGGLDB(Funs.ConnString).Check_PauseNotice.FirstOrDefault(x => x.PauseNoticeId == pauseNoticeId);
            if (getPauseNotice != null)
            {
                string menuId = Const.ProjectPauseNoticeMenuId;               
                ////保存附件
                if (!string.IsNullOrEmpty(attachUrl))
                {
                    UploadFileService.SaveAttachUrl(UploadFileService.GetSourceByAttachUrl(attachUrl, 10, null), attachUrl, menuId, getPauseNotice.PauseNoticeId);
                }
                else
                {
                    CommonService.DeleteAttachFileById(menuId, getPauseNotice.PauseNoticeId);
                }
            }
        }
        #endregion
    }
}
