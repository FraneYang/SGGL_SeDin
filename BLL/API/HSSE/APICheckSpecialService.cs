using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    /// <summary>
    /// 专项检查
    /// </summary>
    public static class APICheckSpecialService
    {
        #region  获取专项检查详细
        /// <summary>
        ///  获取专项检查详细
        /// </summary>
        /// <param name="CheckSpecialId"></param>
        /// <returns></returns>
        public static Model.CheckSpecialItem getCheckSpecialById(string CheckSpecialId)
        {
            var getInfo = from x in new Model.SGGLDB(Funs.ConnString).Check_CheckSpecial
                          where x.CheckSpecialId == CheckSpecialId
                          select new Model.CheckSpecialItem
                          {
                              CheckSpecialId = x.CheckSpecialId,
                              ProjectId = x.ProjectId,
                              CheckSpecialCode = x.CheckSpecialCode,
                              CheckType =x.CheckType=="0"? "周检":(x.CheckType == "1" ? "月检" :"其他"),
                              CheckItemSetId = x.CheckItemSetId,
                              CheckItemSetName = new Model.SGGLDB(Funs.ConnString).Technique_CheckItemSet.First(y => y.CheckItemSetId == x.CheckItemSetId).CheckItemName,
                              CheckPersonId = x.CheckPerson,
                              CheckPersonName = new Model.SGGLDB(Funs.ConnString).Sys_User.First(u => u.UserId == x.CheckPerson).UserName,
                              CheckTime = string.Format("{0:yyyy-MM-dd}", x.CheckTime),
                              DaySummary=x.DaySummary,
                              PartInUnitIds=x.PartInUnits,
                              PartInUnitNames=UnitService.getUnitNamesUnitIds(x.PartInUnits),
                              PartInPersonIds=x.PartInPersonIds,
                              PartInPersonNames= UserService.getUserNamesUserIds(x.PartInPersonIds),
                              PartInPersonNames2=x.PartInPersonNames,
                              CompileManId = x.CompileMan,
                              CompileManName = new Model.SGGLDB(Funs.ConnString).Sys_User.First(u => u.UserId == x.CompileMan).UserName,
                              States = x.States,
                              AttachUrl1 =APIUpLoadFileService.getFileUrl(x.CheckSpecialId,null),
                              CheckSpecialDetailItems= getCheckSpecialDetailList(x.CheckSpecialId),
                          };
            return getInfo.FirstOrDefault();
        }
        #endregion        

        #region 获取专项检查列表信息
        /// <summary>
        /// 获取专项检查列表信息
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="states"></param>
        /// <returns></returns>
        public static List<Model.CheckSpecialItem> getCheckSpecialList(string projectId,  string states)
        {
            var getCheckSpecial = from x in new Model.SGGLDB(Funs.ConnString).Check_CheckSpecial
                                  where x.ProjectId == projectId  && (states == null || (states == "0" && (x.States == "0" || x.States == null)) || (states == "1" && (x.States == "1" || x.States == "2")))
                                  orderby x.CheckSpecialCode descending
                                  select new Model.CheckSpecialItem
                                  {
                                      CheckSpecialId = x.CheckSpecialId,
                                      ProjectId = x.ProjectId,
                                      CheckSpecialCode = new Model.SGGLDB(Funs.ConnString).Sys_CodeRecords.First(y=>y.DataId==x.CheckSpecialId).Code ?? x.CheckSpecialCode,
                                      CheckType = x.CheckType == "0" ? "周检" : (x.CheckType == "1" ? "月检" : "其他"),
                                      CheckItemSetId=x.CheckItemSetId,
                                      CheckItemSetName=new Model.SGGLDB(Funs.ConnString).Technique_CheckItemSet.First(y=>y.CheckItemSetId== x.CheckItemSetId).CheckItemName,
                                      CheckPersonId = x.CheckPerson,
                                      CheckPersonName = new Model.SGGLDB(Funs.ConnString).Sys_User.First(u => u.UserId == x.CheckPerson).UserName,
                                      CheckTime = string.Format("{0:yyyy-MM-dd}", x.CheckTime),
                                      DaySummary = x.DaySummary,
                                      PartInUnitIds = x.PartInUnits,
                                      PartInUnitNames = UnitService.getUnitNamesUnitIds(x.PartInUnits),
                                      PartInPersonIds = x.PartInPersonIds,
                                      PartInPersonNames = UserService.getUserNamesUserIds(x.PartInPersonIds),
                                      PartInPersonNames2 = x.PartInPersonNames,
                                      CompileManId = x.CompileMan,
                                      CompileManName = new Model.SGGLDB(Funs.ConnString).Sys_User.First(u => u.UserId == x.CompileMan).UserName,
                                      States = x.States,
                                      AttachUrl1 = APIUpLoadFileService.getFileUrl(x.CheckSpecialId, null),
                                  };
            return getCheckSpecial.ToList();
        }       
        #endregion        

        #region 保存Check_CheckSpecial
        /// <summary>
        /// 保存Check_CheckSpecial
        /// </summary>
        /// <param name="newItem">处罚通知单</param>
        /// <returns></returns>
        public static string SaveCheckSpecial(Model.CheckSpecialItem newItem)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                string message = string.Empty;
                Model.Check_CheckSpecial newCheckSpecial = new Model.Check_CheckSpecial
                {
                    CheckSpecialId = newItem.CheckSpecialId,
                    CheckSpecialCode = newItem.CheckSpecialCode,
                    CheckItemSetId=newItem.CheckItemSetId,
                    CheckType = newItem.CheckType,
                    ProjectId = newItem.ProjectId,
                    CheckPerson = newItem.CheckPersonId,
                    CheckTime = Funs.GetNewDateTime(newItem.CheckTime),
                    DaySummary = System.Web.HttpUtility.HtmlEncode(newItem.DaySummary),
                    PartInUnits = newItem.PartInUnitIds,
                    PartInPersonIds = newItem.PartInPersonIds,
                    PartInPersons = UserService.getUserNamesUserIds(newItem.PartInPersonIds),
                    PartInPersonNames = newItem.PartInPersonNames2,
                    CompileMan = newItem.CompileManId,
                    States = newItem.States,
                };
                if (newItem.States != Const.State_1)
                {
                    newCheckSpecial.States = Const.State_0;
                }

                var updateCheckSpecial = db.Check_CheckSpecial.FirstOrDefault(x => x.CheckSpecialId == newItem.CheckSpecialId);
                if (updateCheckSpecial == null)
                {
                    newCheckSpecial.CheckSpecialId = SQLHelper.GetNewID();                    
                    newCheckSpecial.CheckSpecialCode = CodeRecordsService.ReturnCodeByMenuIdProjectId(Const.ProjectCheckSpecialMenuId, newCheckSpecial.ProjectId, string.Empty);
                    db.Check_CheckSpecial.InsertOnSubmit(newCheckSpecial);
                    db.SubmitChanges();
                    ////增加一条编码记录
                    BLL.CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(BLL.Const.ProjectCheckSpecialMenuId, newCheckSpecial.ProjectId, null, newCheckSpecial.CheckSpecialId, newCheckSpecial.CheckTime);
                }
                else
                {
                    Check_CheckSpecialService.UpdateCheckSpecial(newCheckSpecial);
                    //// 删除专项检查明细项
                    Check_CheckSpecialDetailService.DeleteCheckSpecialDetails(newCheckSpecial.CheckSpecialId);
                }
                if (newCheckSpecial.States == "1")
                {
                    CommonService.btnSaveData(newCheckSpecial.ProjectId, Const.ProjectCheckSpecialMenuId, newCheckSpecial.CheckSpecialId, newCheckSpecial.CompileMan, true, newCheckSpecial.CheckSpecialCode, "../Check/CheckSpecialView.aspx?CheckSpecialId={0}");
                }
                ////保存附件
                if (!string.IsNullOrEmpty(newItem.AttachUrl1))
                {
                    UploadFileService.SaveAttachUrl(UploadFileService.GetSourceByAttachUrl(newItem.AttachUrl1, 10, null), newItem.AttachUrl1, Const.ProjectCheckSpecialMenuId, newCheckSpecial.CheckSpecialId);
                }
                else
                {
                    CommonService.DeleteAttachFileById(Const.ProjectCheckSpecialMenuId, newCheckSpecial.CheckSpecialId);
                }

                ///// 新增检查项
                if (newItem.CheckSpecialDetailItems != null && newItem.CheckSpecialDetailItems.Count() > 0)
                {
                      
                    foreach (var item in newItem.CheckSpecialDetailItems)
                    {
                        item.CheckSpecialId = newCheckSpecial.CheckSpecialId;
                        SaveCheckSpecialDetail(item);                      
                    }
                    //// 单据完成后 系统自动按照单位 整改要求生成隐患整改单
                    if (newCheckSpecial.States == Const.State_1)
                    {
                        var detailLists = db.Check_CheckSpecialDetail.Where(x => x.CheckSpecialId == newCheckSpecial.CheckSpecialId && x.CompleteStatus == false);
                        if (detailLists.Count() > 0)
                        {
                            message= Check_CheckSpecialService.IssueRectification(detailLists.ToList(), newCheckSpecial);
                        }
                    }
                }
                return message;
            }
        }
        #endregion

        #region  获取专项检查明细项列表
        /// <summary>
        ///  获取专项检查明细项
        /// </summary>
        /// <param name="checkSpecialId"></param>
        /// <returns></returns>
        public static List<Model.CheckSpecialDetailItem> getCheckSpecialDetailList(string checkSpecialId)
        {
            var getInfo = from x in new Model.SGGLDB(Funs.ConnString).Check_CheckSpecialDetail
                          where x.CheckSpecialId == checkSpecialId
                          select new Model.CheckSpecialDetailItem
                          {
                              CheckSpecialDetailId = x.CheckSpecialDetailId,
                              CheckSpecialId = x.CheckSpecialId,
                              CheckItemSetId = x.CheckItem,
                              CheckItemSetName = new Model.SGGLDB(Funs.ConnString).Technique_CheckItemSet.First(y => y.CheckItemSetId == x.CheckItem).CheckItemName,
                              CheckContent = x.CheckContent,
                              SortIndex = x.SortIndex,
                              Unqualified = x.Unqualified,
                              Suggestions = x.Suggestions,
                              WorkArea = x.WorkArea,
                              UnitId = x.UnitId,
                              UnitName = new Model.SGGLDB(Funs.ConnString).Base_Unit.First(y => y.UnitId == x.UnitId).UnitName,
                              HandleStep = x.HandleStep,
                              HandleStepName = getNames(x.HandleStep),
                              LimitedDate = string.Format("{0:yyyy-MM-dd}", x.LimitedDate),
                              CompleteStatus = x.CompleteStatus,
                              CompleteStatusName = x.CompleteStatus == true ? "已整改" : "待整改",
                              CompletedDate = string.Format("{0:yyyy-MM-dd}", x.CompletedDate),
                              AttachUrl1 = APIUpLoadFileService.getFileUrl(x.CheckSpecialDetailId, null),
                          };
            return getInfo.ToList();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static string getNames(string constValue)
        {
            return ConstValue.getConstTextsConstValues(constValue, ConstValue.Group_HandleStep);
        }
        #endregion      

        #region  获取专项检查明细项详细
        /// <summary>
        ///  获取专项检查明细项详细
        /// </summary>
        /// <param name="checkSpecialDetailId"></param>
        /// <returns></returns>
        public static Model.CheckSpecialDetailItem getCheckSpecialDetailById(string checkSpecialDetailId)
        {
            var getInfo = from x in new Model.SGGLDB(Funs.ConnString).Check_CheckSpecialDetail
                          where x.CheckSpecialDetailId == checkSpecialDetailId
                          select new Model.CheckSpecialDetailItem
                          {
                              CheckSpecialDetailId = x.CheckSpecialDetailId,
                              CheckSpecialId = x.CheckSpecialId,
                              CheckItemSetId = x.CheckItem,
                              CheckItemSetName = new Model.SGGLDB(Funs.ConnString).Technique_CheckItemSet.First(y => y.CheckItemSetId == x.CheckItem).CheckItemName,
                              CheckContent = x.CheckContent,
                              Unqualified = x.Unqualified,
                              Suggestions = x.Suggestions,
                              WorkArea = new Model.SGGLDB(Funs.ConnString).WBS_UnitWork.First(y => y.UnitWorkId == x.CheckArea).UnitWorkName,
                              WorkAreaId =x.CheckArea,
                              UnitId = x.UnitId,
                              UnitName = new Model.SGGLDB(Funs.ConnString).Base_Unit.First(y => y.UnitId == x.UnitId).UnitName,
                              HandleStep = x.HandleStep,
                              HandleStepName = new Model.SGGLDB(Funs.ConnString).Sys_Const.First(y => y.GroupId == ConstValue.Group_HandleStep && y.ConstValue == x.HandleStep).ConstText,
                              LimitedDate = string.Format("{0:yyyy-MM-dd}", x.LimitedDate),
                              CompleteStatus = x.CompleteStatus,
                              CompleteStatusName = x.CompleteStatus == true ? "已整改" : "待整改",
                              AttachUrl1 = APIUpLoadFileService.getFileUrl(x.CheckSpecialDetailId, null),
                          };
            return getInfo.First();
        }
        #endregion

        #region 保存专项检查明细项
        /// <summary>
        ///  保存专项检查明细项
        /// </summary>
        /// <param name="newDetail"></param>
        public static void SaveCheckSpecialDetail(Model.CheckSpecialDetailItem newDetail)
        {
            if (!string.IsNullOrEmpty(newDetail.CheckSpecialId))
            {
                Model.Check_CheckSpecialDetail newCheckSpecialDetail = new Model.Check_CheckSpecialDetail
                {
                    CheckSpecialId = newDetail.CheckSpecialId,
                    CheckItem = newDetail.CheckItemSetId,
                    SortIndex = newDetail.SortIndex,
                    CheckItemType = newDetail.CheckItemSetName,
                    Unqualified = newDetail.Unqualified,
                    UnitId = newDetail.UnitId,
                    HandleStep = newDetail.HandleStep,
                    CompleteStatus = newDetail.CompleteStatus ?? false,
                    RectifyNoticeId = newDetail.RectifyNoticeId,
                    LimitedDate = Funs.GetNewDateTime(newDetail.LimitedDate),
                    CompletedDate = Funs.GetNewDateTime(newDetail.CompletedDate),
                    Suggestions = newDetail.Suggestions,
                    WorkArea = newDetail.WorkArea,
                    CheckArea = newDetail.WorkAreaId,
                    CheckContent = newDetail.CheckContent,
                };
                var getUnit = UnitService.GetUnitByUnitId(newDetail.UnitId);
                if (getUnit != null)
                {
                    newCheckSpecialDetail.UnitId = newDetail.UnitId;
                }
                
                var updateDetail = new Model.SGGLDB(Funs.ConnString).Check_CheckSpecialDetail.FirstOrDefault(x => x.CheckSpecialDetailId == newDetail.CheckSpecialDetailId);
                if (updateDetail == null)
                {
                    newCheckSpecialDetail.CheckSpecialDetailId = SQLHelper.GetNewID();
                    new Model.SGGLDB(Funs.ConnString).Check_CheckSpecialDetail.InsertOnSubmit(newCheckSpecialDetail);
                    new Model.SGGLDB(Funs.ConnString).SubmitChanges();
                }
                else
                {
                    newCheckSpecialDetail.CheckSpecialDetailId = updateDetail.CheckSpecialDetailId;
                    updateDetail.CheckItem = newCheckSpecialDetail.CheckItem;
                    updateDetail.CheckItemType = newCheckSpecialDetail.CheckItemType;
                    updateDetail.SortIndex = newCheckSpecialDetail.SortIndex;
                    updateDetail.Unqualified = newCheckSpecialDetail.Unqualified;
                    updateDetail.UnitId = newCheckSpecialDetail.UnitId;
                    updateDetail.HandleStep = newCheckSpecialDetail.HandleStep;
                    updateDetail.CompleteStatus = newCheckSpecialDetail.CompleteStatus;
                    updateDetail.RectifyNoticeId = newCheckSpecialDetail.RectifyNoticeId;
                    updateDetail.LimitedDate = newCheckSpecialDetail.LimitedDate;
                    updateDetail.CompletedDate = newCheckSpecialDetail.CompletedDate;
                    updateDetail.Suggestions = newCheckSpecialDetail.Suggestions;
                    updateDetail.WorkArea = newCheckSpecialDetail.WorkArea;
                    updateDetail.CheckContent = newCheckSpecialDetail.CheckContent;
                    new Model.SGGLDB(Funs.ConnString).SubmitChanges();
                }
                ////保存附件
                if (!string.IsNullOrEmpty(newDetail.AttachUrl1))
                {
                    UploadFileService.SaveAttachUrl(UploadFileService.GetSourceByAttachUrl(newDetail.AttachUrl1, 10, null), newDetail.AttachUrl1, Const.ProjectCheckSpecialMenuId, newCheckSpecialDetail.CheckSpecialDetailId);
                }
                else
                {
                    CommonService.DeleteAttachFileById(Const.ProjectCheckSpecialMenuId, newCheckSpecialDetail.CheckSpecialDetailId);
                }
            }
        }
        #endregion        
    }
}
