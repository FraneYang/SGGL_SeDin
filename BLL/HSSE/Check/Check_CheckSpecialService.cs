using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace BLL
{
    /// <summary>
    /// 专项检查
    /// </summary>
    public static class Check_CheckSpecialService
    {
        /// <summary>
        /// 根据专项检查ID获取专项检查信息
        /// </summary>
        /// <param name="CheckSpecialName"></param>
        /// <returns></returns>
        public static Model.Check_CheckSpecial GetCheckSpecialByCheckSpecialId(string checkSpecialId)
        {
            return Funs.DB.Check_CheckSpecial.FirstOrDefault(e => e.CheckSpecialId == checkSpecialId);
        }

        /// <summary>
        /// 根据时间段获取专项检查信息集合
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public static int GetCountByCheckTime(DateTime startTime, DateTime endTime, string projectId)
        {
            return (from x in Funs.DB.Check_CheckSpecial where x.CheckTime >= startTime && x.CheckTime < endTime && x.ProjectId == projectId && x.States == BLL.Const.State_2 select x).Count();
        }

        /// <summary>
        /// 根据时间段获取专项检查集合
        /// </summary>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="projectId">项目号</param>
        /// <returns>专项检查集合</returns>
        public static List<Model.Check_CheckSpecial> GetListByCheckTime(DateTime startTime, DateTime endTime, string projectId)
        {
            return (from x in Funs.DB.Check_CheckSpecial where x.CheckTime >= startTime && x.CheckTime < endTime && x.ProjectId == projectId select x).ToList();
        }

        /// <summary>
        /// 根据时间段获取已完成的专项检查整改数量
        /// </summary>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="projectId">项目号</param>
        /// <returns>已完成的专项检查整改数量</returns>
        public static int GetIsOKViolationCountByCheckTime(DateTime startTime, DateTime endTime, string projectId)
        {
            return (from x in Funs.DB.Check_CheckSpecial
                    join y in Funs.DB.Check_CheckSpecialDetail on x.CheckSpecialId equals y.CheckSpecialId
                    where x.CheckTime >= startTime && x.CheckTime <= endTime && x.ProjectId == projectId && y.CompleteStatus != null && y.CompleteStatus == true
                    select y).Count();
        }

        /// <summary>
        /// 添加安全专项检查
        /// </summary>
        /// <param name="checkSpecial"></param>
        public static void AddCheckSpecial(Model.Check_CheckSpecial checkSpecial)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Check_CheckSpecial newCheckSpecial = new Model.Check_CheckSpecial
            {
                CheckSpecialId = checkSpecial.CheckSpecialId,
                CheckSpecialCode = checkSpecial.CheckSpecialCode,
                ProjectId = checkSpecial.ProjectId,
                CheckPerson = checkSpecial.CheckPerson,
                CheckTime = checkSpecial.CheckTime,
                ScanUrl = checkSpecial.ScanUrl,
                DaySummary = checkSpecial.DaySummary,
                PartInUnits = checkSpecial.PartInUnits,
                PartInPersons = checkSpecial.PartInPersons,
                PartInPersonIds = checkSpecial.PartInPersonIds,
                PartInPersonNames = checkSpecial.PartInPersonNames,
                CheckAreas = checkSpecial.CheckAreas,
                States = checkSpecial.States,
                CompileMan = checkSpecial.CompileMan,
                CheckType = checkSpecial.CheckType,
                CheckItemSetId = checkSpecial.CheckItemSetId,
            };
            db.Check_CheckSpecial.InsertOnSubmit(newCheckSpecial);
            db.SubmitChanges();
            ////增加一条编码记录
            BLL.CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(BLL.Const.ProjectCheckSpecialMenuId, checkSpecial.ProjectId, null, checkSpecial.CheckSpecialId, checkSpecial.CheckTime);

            if (!string.IsNullOrEmpty(newCheckSpecial.PartInPersonIds))
            { 
            }
        }

        /// <summary>
        /// 修改安全专项检查
        /// </summary>
        /// <param name="checkSpecial"></param>
        public static void UpdateCheckSpecial(Model.Check_CheckSpecial checkSpecial)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Check_CheckSpecial newCheckSpecial = db.Check_CheckSpecial.FirstOrDefault(e => e.CheckSpecialId == checkSpecial.CheckSpecialId);
            if (newCheckSpecial != null)
            {
                newCheckSpecial.CheckSpecialCode = checkSpecial.CheckSpecialCode;
                //newCheckSpecial.ProjectId = checkSpecial.ProjectId;
                newCheckSpecial.CheckPerson = checkSpecial.CheckPerson;
                newCheckSpecial.CheckTime = checkSpecial.CheckTime;
                newCheckSpecial.ScanUrl = checkSpecial.ScanUrl;
                newCheckSpecial.DaySummary = checkSpecial.DaySummary;
                newCheckSpecial.PartInUnits = checkSpecial.PartInUnits;
                newCheckSpecial.PartInPersons = checkSpecial.PartInPersons;
                newCheckSpecial.PartInPersonIds = checkSpecial.PartInPersonIds;
                newCheckSpecial.PartInPersonNames = checkSpecial.PartInPersonNames;
                newCheckSpecial.CheckAreas = checkSpecial.CheckAreas;
                newCheckSpecial.States = checkSpecial.States;
                newCheckSpecial.CheckType = checkSpecial.CheckType;
                newCheckSpecial.CheckItemSetId = checkSpecial.CheckItemSetId;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据专项检查ID删除对应专项检查记录信息
        /// </summary>
        /// <param name="superviseCheckReportId"></param>
        public static void DeleteCheckSpecial(string checkSpecialId)
        {
            Model.SGGLDB db = Funs.DB;
            var q = (from x in db.Check_CheckSpecial where x.CheckSpecialId == checkSpecialId select x).FirstOrDefault();
            if (q != null)
            {
                ///删除编码表记录
                BLL.CodeRecordsService.DeleteCodeRecordsByDataId(q.CheckSpecialId);
                ////删除附件表
                BLL.CommonService.DeleteAttachFileById(q.CheckSpecialId);              
                ////删除审核流程表
                BLL.CommonService.DeleteFlowOperateByID(q.CheckSpecialId);
                db.Check_CheckSpecial.DeleteOnSubmit(q);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="CheckSpecialDetailId"></param>
        /// <returns></returns>
        public static string ConvertHandleStep(object CheckSpecialDetailId)
        {
            string name = string.Empty;
            if (CheckSpecialDetailId != null)
            {
                var getDetail = BLL.Check_CheckSpecialDetailService.GetCheckSpecialDetailByCheckSpecialDetailId(CheckSpecialDetailId.ToString());
                if (getDetail != null)
                {
                    if (getDetail.DataType == "1")
                    {
                        name = "下发整改单：";
                        var getRe = RectifyNoticesService.GetRectifyNoticesById(getDetail.DataId);
                        if (getRe != null)
                        {
                            name += getRe.RectifyNoticesCode;
                        }
                    }
                    else if (getDetail.DataType == "2")
                    {
                        name = "下发处罚单：";
                        var getRe = PunishNoticeService.GetPunishNoticeById(getDetail.DataId);
                        if (getRe != null)
                        {
                            name += getRe.PunishNoticeCode;
                        }
                    }
                    else if (getDetail.DataType == "3")
                    {
                        name = "下发暂停令：";
                        var getRe = Check_PauseNoticeService.GetPauseNoticeByPauseNoticeId(getDetail.DataId);
                        if (getRe != null)
                        {
                            name += getRe.PauseNoticeCode;
                        }
                    }
                }
            }
            return name;
        }

        public static string IssueRectification(List<Model.Check_CheckSpecialDetail> detailLists, Model.Check_CheckSpecial checkSpecial)
        {
            string info = string.Empty;

            if (detailLists.Count() > 0 && checkSpecial != null)
            {
                ////隐患整改单
                var getDetail1 = detailLists.Where(x => x.HandleStep.Contains("1"));
                if (getDetail1.Count() > 0)
                {
                    var getUnitList = getDetail1.Select(x => x.UnitId).Distinct();
                    foreach (var unitItem in getUnitList)
                    {
                        Model.RectifyNoticesItem rectifyNotices = new Model.RectifyNoticesItem
                        {
                            ProjectId = checkSpecial.ProjectId,
                            UnitId = unitItem,
                            CompleteManId = checkSpecial.CompileMan,
                            CheckManNames = checkSpecial.PartInPersons,
                            CheckManIds = checkSpecial.PartInPersonIds,
                            CheckedDate = string.Format("{0:yyyy-MM-dd HH:mm:ss}", checkSpecial.CheckTime),
                            States = Const.State_0,
                        };
                        rectifyNotices.RectifyNoticesItemItem = new List<Model.RectifyNoticesItemItem>();
                        var getUnitDItem = getDetail1.Where(x => x.UnitId == unitItem);
                        foreach (var item in getUnitDItem)
                        {
                            Model.RectifyNoticesItemItem newRItem = new Model.RectifyNoticesItemItem();
                            if (!string.IsNullOrEmpty(item.WorkArea))
                            {
                                newRItem.WrongContent = item.WorkArea + item.Unqualified;
                            }
                            else
                            {
                                newRItem.WrongContent = item.Unqualified;
                            }
                            if (string.IsNullOrEmpty(rectifyNotices.CheckSpecialDetailId))
                            {
                                rectifyNotices.CheckSpecialDetailId = item.CheckSpecialDetailId;
                            }
                            else
                            {
                                rectifyNotices.CheckSpecialDetailId += "," + item.CheckSpecialDetailId;
                            }
                            var getAtt = Funs.DB.AttachFile.FirstOrDefault(x => x.ToKeyId == item.CheckSpecialDetailId);
                            if (getAtt != null && !string.IsNullOrEmpty(getAtt.AttachUrl))
                            {
                                newRItem.PhotoBeforeUrl = getAtt.AttachUrl;
                            }
                          
                            rectifyNotices.RectifyNoticesItemItem.Add(newRItem);
                        }

                        APIRectifyNoticesService.SaveRectifyNotices(rectifyNotices);
                    }
                    info += "整改单已下发。";
                }
                ///处罚单
                var getDetail2 = detailLists.Where(x => x.HandleStep.Contains("2"));
                if (getDetail2.Count() > 0)
                {
                    var getUnitList = getDetail2.Select(x => x.UnitId).Distinct();
                    foreach (var unitItem in getUnitList)
                    {
                        Model.PunishNoticeItem punishNotice = new Model.PunishNoticeItem
                        {
                            ProjectId = checkSpecial.ProjectId,
                            PunishNoticeDate = string.Format("{0:yyyy-MM-dd HH:mm:ss}", checkSpecial.CheckTime),
                            UnitId = unitItem,
                            CompileManId = checkSpecial.CompileMan,
                            PunishStates = Const.State_0,
                        };
                        punishNotice.PunishNoticeItemItem = new List<Model.PunishNoticeItemItem>();
                        var getUnitDItem = getDetail2.Where(x => x.UnitId == unitItem);
                        foreach (var item in getUnitDItem)
                        {
                            Model.PunishNoticeItemItem newPItem = new Model.PunishNoticeItemItem();
                            newPItem.PunishContent = item.Unqualified;
                            newPItem.SortIndex = item.SortIndex;
                            punishNotice.PunishNoticeItemItem.Add(newPItem);
                            if (string.IsNullOrEmpty(punishNotice.CheckSpecialDetailId))
                            {
                                punishNotice.CheckSpecialDetailId = item.CheckSpecialDetailId;
                            }
                            else
                            {
                                punishNotice.CheckSpecialDetailId += "," + item.CheckSpecialDetailId;
                            }
                        }

                        APIPunishNoticeService.SavePunishNotice(punishNotice);
                    }
                    info += "处罚单已下发。";
                }
                ///暂停令
                var getDetail3 = detailLists.Where(x => x.HandleStep.Contains("3"));
                if (getDetail3.Count() > 0)
                {
                    var getUnitList = getDetail3.Select(x => x.UnitId).Distinct();
                    foreach (var unitItem in getUnitList)
                    {
                        Model.PauseNoticeItem pauseNotice = new Model.PauseNoticeItem
                        {
                            ProjectId = checkSpecial.ProjectId,
                            UnitId = unitItem,
                            CompileManId= checkSpecial.CompileMan,
                            PauseTime = string.Format("{0:yyyy-MM-dd HH:mm:ss}", checkSpecial.CheckTime),
                            PauseStates = Const.State_0,
                        };

                        var getUnitDItem = getDetail3.Where(x => x.UnitId == unitItem);
                        foreach (var item in getUnitDItem)
                        {
                            Model.RectifyNoticesItemItem newRItem = new Model.RectifyNoticesItemItem();
                            pauseNotice.ThirdContent += item.Unqualified;
                            if (string.IsNullOrEmpty(pauseNotice.ProjectPlace))
                            {
                                pauseNotice.ProjectPlace = item.WorkArea;
                            }
                            else
                            {
                                if (!pauseNotice.ProjectPlace.Contains(item.WorkArea))
                                {
                                    pauseNotice.ProjectPlace += "," + item.WorkArea;
                                }
                            }
                            if (string.IsNullOrEmpty(pauseNotice.CheckSpecialDetailId))
                            {
                                pauseNotice.CheckSpecialDetailId = item.CheckSpecialDetailId;
                            }
                            else
                            {
                                pauseNotice.CheckSpecialDetailId += "," + item.CheckSpecialDetailId;
                            }
                            var getAtt = Funs.DB.AttachFile.FirstOrDefault(x => x.ToKeyId == item.CheckSpecialDetailId);
                            if (getAtt != null && !string.IsNullOrEmpty(getAtt.AttachUrl))
                            {
                                pauseNotice.PauseNoticeAttachUrl = getAtt.AttachUrl;
                            }
                        }

                        APIPauseNoticeService.SavePauseNotice(pauseNotice);
                    }
                    info += "整改单已下发。";
                }
            }

            if (!string.IsNullOrEmpty(info))
            {
                info += "请在相应办理页面提交！";
            }
            return info;
        }
    }
}
