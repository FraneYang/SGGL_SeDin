using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    /// <summary>
    /// 应急信息
    /// </summary>
    public static class APIEmergencyService
    {
        #region 根据主键ID获取应急预案信息
        /// <summary>
        ///  根据主键ID获取应急预案信息
        /// </summary>
        /// <param name="emergencyListId"></param>
        /// <returns></returns>
        public static Model.FileInfoItem getEmergencyListByEmergencyListId(string emergencyListId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var getInfo = from x in db.Emergency_EmergencyList
                              where x.EmergencyListId == emergencyListId
                              select new Model.FileInfoItem
                              {
                                  FileId = x.EmergencyListId,
                                  ProjectId = x.ProjectId,
                                  FileCode = x.EmergencyCode,
                                  FileName = x.EmergencyName,
                                  FileType = db.Base_EmergencyType.First(y => y.EmergencyTypeId == x.EmergencyTypeId).EmergencyTypeName,
                                  FileTypeId = x.EmergencyTypeId,
                                  UnitId = x.UnitId,
                                  UnitName = db.Base_Unit.First(y => y.UnitId == x.UnitId).UnitName,
                                  FileContent = System.Web.HttpUtility.HtmlDecode(x.EmergencyContents),
                                  CompileManId = x.CompileMan,
                                  CompileManName = db.Sys_User.First(u => u.UserId == x.CompileMan).UserName,
                                  CompileDate = string.Format("{0:yyyy-MM-dd}", x.CompileDate),
                                  AuditManId = x.AuditMan,
                                  AuditManName = db.Sys_User.First(u => u.UserId == x.AuditMan).UserName,
                                  ApproveManId = x.ApproveMan,
                                  ApproveManName = db.Sys_User.First(u => u.UserId == x.ApproveMan).UserName,
                                  States = x.States,
                                  MenuType = "1",
                                  AttachUrl = APIUpLoadFileService.getFileUrl(x.EmergencyListId, x.AttachUrl),
                              };
                return getInfo.FirstOrDefault();
            }
        }
        #endregion        

        #region 获取应急预案列表信息
        /// <summary>
        /// 获取应急预案列表信息
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="unitId"></param>
        /// <param name="strParam"></param>
        /// <returns></returns>
        public static List<Model.FileInfoItem> getEmergencyList(string projectId, string unitId, string states, string strParam)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var getDataList = from x in db.Emergency_EmergencyList
                                  where x.ProjectId == projectId && (x.UnitId == unitId || unitId == null)
                                 && (strParam == null || x.EmergencyName.Contains(strParam) || x.EmergencyCode.Contains(strParam))
                                 && ((states == x.States) || (x.States == "2" && states == "1"))
                                  orderby x.EmergencyCode descending
                                  select new Model.FileInfoItem
                                  {
                                      FileId = x.EmergencyListId,
                                      ProjectId = x.ProjectId,
                                      FileCode = x.EmergencyCode,
                                      FileName = x.EmergencyName,
                                      FileType = db.Base_EmergencyType.First(y => y.EmergencyTypeId == x.EmergencyTypeId).EmergencyTypeName,
                                      FileTypeId = x.EmergencyTypeId,
                                      UnitId = x.UnitId,
                                      UnitName = db.Base_Unit.First(y => y.UnitId == x.UnitId).UnitName,
                                      FileContent = x.EmergencyContents,
                                      CompileManId = x.CompileMan,
                                      CompileManName = db.Sys_User.First(u => u.UserId == x.CompileMan).UserName,
                                      CompileDate = string.Format("{0:yyyy-MM-dd}", x.CompileDate),
                                      AuditManId = x.AuditMan,
                                      AuditManName = db.Sys_User.First(u => u.UserId == x.AuditMan).UserName,
                                      ApproveManId = x.ApproveMan,
                                      ApproveManName = db.Sys_User.First(u => u.UserId == x.ApproveMan).UserName,
                                      States = x.States,
                                      MenuType = "1",
                                      AttachUrl = APIUpLoadFileService.getFileUrl(x.EmergencyListId, x.AttachUrl),
                                  };
                return getDataList.ToList();
            }
        }
        #endregion        

        #region 根据主键ID获取应急物资信息
        /// <summary>
        ///  根据主键ID获取应急物资信息
        /// </summary>
        /// <param name="emergencySupplyId"></param>
        /// <returns></returns>
        public static Model.FileInfoItem getEmergencySupplyByEmergencySupplyId(string emergencySupplyId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var getInfo = from x in db.Emergency_EmergencySupply
                              where x.FileId == emergencySupplyId
                              select new Model.FileInfoItem
                              {
                                  FileId = x.FileId,
                                  ProjectId = x.ProjectId,
                                  FileCode = x.FileCode,
                                  FileName = x.FileName,
                                  UnitId = x.UnitId,
                                  UnitName = db.Base_Unit.First(y => y.UnitId == x.UnitId).UnitName,
                                  FileContent = System.Web.HttpUtility.HtmlDecode(x.FileContent),
                                  CompileManId = x.CompileMan,
                                  CompileManName = db.Sys_User.First(u => u.UserId == x.CompileMan).UserName,
                                  CompileDate = string.Format("{0:yyyy-MM-dd}", x.CompileDate),
                                  States = x.States,
                                  MenuType = "2",
                                  AttachUrl = APIUpLoadFileService.getFileUrl(x.FileId, x.AttachUrl),
                              };
                return getInfo.FirstOrDefault();
            }
        }
        #endregion        

        #region 获取应急物资列表信息
        /// <summary>
        /// 获取应急物资列表信息
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="unitId"></param>
        /// <param name="states"></param>
        /// <param name="strParam"></param>
        /// <returns></returns>
        public static List<Model.FileInfoItem> getEmergencySupplyList(string projectId, string unitId, string states, string strParam)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var getDataList = from x in db.Emergency_EmergencySupply
                                  where x.ProjectId == projectId && (x.UnitId == unitId || unitId == null)
                                 && (strParam == null || x.FileName.Contains(strParam) || x.FileCode.Contains(strParam))
                                  && ((states == x.States) || (x.States == "2" && states == "1"))
                                  orderby x.FileCode descending
                                  select new Model.FileInfoItem
                                  {
                                      FileId = x.FileId,
                                      ProjectId = x.ProjectId,
                                      FileCode = x.FileCode,
                                      FileName = x.FileName,
                                      UnitId = x.UnitId,
                                      UnitName = db.Base_Unit.First(y => y.UnitId == x.UnitId).UnitName,
                                      FileContent = x.FileContent,
                                      CompileManId = x.CompileMan,
                                      CompileManName = db.Sys_User.First(u => u.UserId == x.CompileMan).UserName,
                                      CompileDate = string.Format("{0:yyyy-MM-dd}", x.CompileDate),
                                      States = x.States,
                                      MenuType = "2",
                                      AttachUrl = APIUpLoadFileService.getFileUrl(x.FileId, x.AttachUrl),
                                  };
                return getDataList.ToList();
            }
        }
        #endregion        

        #region 根据主键ID获取应急队伍信息
        /// <summary>
        ///  根据主键ID获取应急队伍信息
        /// </summary>
        /// <param name="emergencyTeamAndTrainId"></param>
        /// <returns></returns>
        public static Model.FileInfoItem getEmergencyTeamAndTrainByEmergencyTeamAndTrainId(string emergencyTeamAndTrainId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var getInfo = from x in db.Emergency_EmergencyTeamAndTrain
                              where x.FileId == emergencyTeamAndTrainId
                              select new Model.FileInfoItem
                              {
                                  FileId = x.FileId,
                                  ProjectId = x.ProjectId,
                                  FileCode = x.FileCode,
                                  FileName = x.FileName,
                                  UnitId = x.UnitId,
                                  UnitName = db.Base_Unit.First(y => y.UnitId == x.UnitId).UnitName,
                                  FileContent = System.Web.HttpUtility.HtmlDecode(x.FileContent),
                                  CompileManId = x.CompileMan,
                                  CompileManName = db.Sys_User.First(u => u.UserId == x.CompileMan).UserName,
                                  CompileDate = string.Format("{0:yyyy-MM-dd}", x.CompileDate),
                                  States = x.States,
                                  MenuType = "3",
                                  AttachUrl = APIUpLoadFileService.getFileUrl(x.FileId, x.AttachUrl),
                                  EmergencyTeamItem = getEmergencyTeamItems(x.FileId),
                              };
                return getInfo.FirstOrDefault();
            }
        }

        /// <summary>
        /// 队伍明细
        /// </summary>
        /// <param name="fileId"></param>
        /// <returns></returns>
        public static List<Model.EmergencyTeamItem> getEmergencyTeamItems(string fileId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                return (from x in db.Emergency_EmergencyTeamItem
                        where x.FileId == fileId
                        select new Model.EmergencyTeamItem
                        {
                            EmergencyTeamItemId = x.EmergencyTeamItemId,
                            FileId = x.FileId,
                            PersonId = x.PersonId,
                            PersonName = db.SitePerson_Person.First(z => z.PersonId == x.PersonId).PersonName,
                            Job = x.Job,
                            Tel = x.Tel,
                        }).ToList();
            }
        }
        #endregion        

        #region 获取应急队伍列表信息
        /// <summary>
        /// 获取应急队伍列表信息
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="unitId"></param>
        /// <param name="states"></param>
        /// <param name="strParam"></param>
        /// <returns></returns>
        public static List<Model.FileInfoItem> getEmergencyTeamAndTrainList(string projectId, string unitId,string states, string strParam)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var getDataList = from x in db.Emergency_EmergencyTeamAndTrain
                                  where x.ProjectId == projectId && (x.UnitId == unitId || unitId == null)
                                 && (strParam == null || x.FileName.Contains(strParam) || x.FileCode.Contains(strParam))
                                 && ((states == x.States) || (x.States == "2" && states == "1"))
                                  orderby x.FileCode descending
                                  select new Model.FileInfoItem
                                  {
                                      FileId = x.FileId,
                                      ProjectId = x.ProjectId,
                                      FileCode = x.FileCode,
                                      FileName = x.FileName,
                                      UnitId = x.UnitId,
                                      UnitName = db.Base_Unit.First(y => y.UnitId == x.UnitId).UnitName,
                                      FileContent = x.FileContent,
                                      CompileManId = x.CompileMan,
                                      CompileManName = db.Sys_User.First(u => u.UserId == x.CompileMan).UserName,
                                      CompileDate = string.Format("{0:yyyy-MM-dd}", x.CompileDate),
                                      States = x.States,
                                      MenuType = "3",
                                      AttachUrl = APIUpLoadFileService.getFileUrl(x.FileId, x.AttachUrl),
                                  };
                return getDataList.ToList();
            }
        }
        #endregion        

        #region 保存emergencyInfo
        /// <summary>
        /// 保存emergencyInfo
        /// </summary>
        /// <param name="emergencyInfo">会议信息</param>
        /// <returns></returns>
        public static void SaveEmergency(Model.FileInfoItem emergencyInfo)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                string menuId = string.Empty;
                if (emergencyInfo.MenuType == "1")
                {
                    Model.Emergency_EmergencyList newEmergency = new Model.Emergency_EmergencyList
                    {
                        EmergencyListId = emergencyInfo.FileId,
                        ProjectId = emergencyInfo.ProjectId,
                        UnitId = emergencyInfo.UnitId == "" ? null : emergencyInfo.UnitId,
                        EmergencyTypeId = emergencyInfo.FileType == "" ? null : emergencyInfo.FileType,
                        EmergencyCode = emergencyInfo.FileCode,
                        EmergencyName = emergencyInfo.FileName,
                        EmergencyContents = emergencyInfo.FileContent,
                        CompileMan = emergencyInfo.CompileManId,
                        CompileDate = Funs.GetNewDateTime(emergencyInfo.CompileDate),
                        States = Const.State_2,
                    };
                    if (!string.IsNullOrEmpty(emergencyInfo.AuditManId))
                    {
                        newEmergency.AuditMan = emergencyInfo.AuditManId;
                    }
                    if (!string.IsNullOrEmpty(emergencyInfo.ApproveManId))
                    {
                        newEmergency.ApproveMan = emergencyInfo.ApproveManId;
                    }
                    if (emergencyInfo.States != Const.State_1)
                    {
                        newEmergency.States = Const.State_0;
                    }
                    var updateEmergency = db.Emergency_EmergencyList.FirstOrDefault(x => x.EmergencyListId == emergencyInfo.FileId);
                    if (updateEmergency == null)
                    {
                        newEmergency.CompileDate = DateTime.Now;
                        emergencyInfo.FileId = newEmergency.EmergencyListId = SQLHelper.GetNewID();
                        newEmergency.EmergencyCode = CodeRecordsService.ReturnCodeByMenuIdProjectId(Const.ProjectClassMeetingMenuId, newEmergency.ProjectId, null);
                        db.Emergency_EmergencyList.InsertOnSubmit(newEmergency);
                        db.SubmitChanges();
                        ////增加一条编码记录
                        CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(Const.ProjectEmergencyListMenuId, newEmergency.ProjectId, null, newEmergency.EmergencyListId, newEmergency.CompileDate);
                    }
                    else
                    {
                        updateEmergency.States = newEmergency.States;
                        updateEmergency.EmergencyName = newEmergency.EmergencyName;
                        updateEmergency.UnitId = newEmergency.UnitId;
                        updateEmergency.EmergencyTypeId = newEmergency.EmergencyTypeId;
                        updateEmergency.EmergencyContents = newEmergency.EmergencyContents;
                        updateEmergency.AuditMan = newEmergency.AuditMan;
                        updateEmergency.ApproveMan = newEmergency.ApproveMan;
                        db.SubmitChanges();
                    }
                    if (emergencyInfo.States == Const.State_1)
                    {
                        CommonService.btnSaveData(newEmergency.ProjectId, Const.ProjectEmergencyListMenuId, newEmergency.EmergencyListId, newEmergency.CompileMan, true, newEmergency.EmergencyName, "../Emergency/EmergencyListView.aspx?EmergencyListId={0}");
                    }

                    menuId = Const.ProjectEmergencyListMenuId;
                }
                else if (emergencyInfo.MenuType == "2")
                {
                    Model.Emergency_EmergencySupply newEmergency = new Model.Emergency_EmergencySupply
                    {
                        FileId = emergencyInfo.FileId,
                        ProjectId = emergencyInfo.ProjectId,
                        UnitId = emergencyInfo.UnitId == "" ? null : emergencyInfo.UnitId,
                        FileCode = emergencyInfo.FileCode,
                        FileName = emergencyInfo.FileName,
                        FileContent = emergencyInfo.FileContent,
                        CompileMan = emergencyInfo.CompileManId,
                        CompileDate = Funs.GetNewDateTime(emergencyInfo.CompileDate),
                        States = Const.State_2,
                    };

                    if (emergencyInfo.States != Const.State_1)
                    {
                        newEmergency.States = Const.State_0;
                    }
                    var updateEmergency = db.Emergency_EmergencySupply.FirstOrDefault(x => x.FileId == emergencyInfo.FileId);
                    if (updateEmergency == null)
                    {
                        newEmergency.CompileDate = DateTime.Now;
                        emergencyInfo.FileId = newEmergency.FileId = SQLHelper.GetNewID();
                        newEmergency.FileCode = CodeRecordsService.ReturnCodeByMenuIdProjectId(Const.ProjectEmergencySupplyMenuId, newEmergency.ProjectId, null);
                        db.Emergency_EmergencySupply.InsertOnSubmit(newEmergency);
                        db.SubmitChanges();
                    }
                    else
                    {
                        updateEmergency.States = newEmergency.States;
                        updateEmergency.UnitId = newEmergency.UnitId;
                        updateEmergency.FileCode = newEmergency.FileCode;
                        updateEmergency.FileName = newEmergency.FileName;
                        updateEmergency.FileContent = newEmergency.FileContent;
                        db.SubmitChanges();
                    }
                    if (emergencyInfo.States == Const.State_1)
                    {
                        CommonService.btnSaveData(newEmergency.ProjectId, Const.ProjectEmergencySupplyMenuId, newEmergency.FileId, newEmergency.CompileMan, true, newEmergency.FileName, "../Emergency/EmergencySupplyView.aspx?FileId={0}");
                    }
                    menuId = Const.ProjectEmergencySupplyMenuId;
                }
                else if (emergencyInfo.MenuType == "3")
                {
                    Model.Emergency_EmergencyTeamAndTrain newEmergency = new Model.Emergency_EmergencyTeamAndTrain
                    {
                        FileId = emergencyInfo.FileId,
                        ProjectId = emergencyInfo.ProjectId,
                        UnitId = emergencyInfo.UnitId == "" ? null : emergencyInfo.UnitId,
                        FileCode = emergencyInfo.FileCode,
                        FileName = emergencyInfo.FileName,
                        FileContent = emergencyInfo.FileContent,
                        CompileMan = emergencyInfo.CompileManId,
                        CompileDate = Funs.GetNewDateTime(emergencyInfo.CompileDate),
                        States = Const.State_2,
                    };

                    if (emergencyInfo.States != Const.State_1)
                    {
                        newEmergency.States = Const.State_0;
                    }

                    var updateEmergency = db.Emergency_EmergencyTeamAndTrain.FirstOrDefault(x => x.FileId == emergencyInfo.FileId);
                    if (updateEmergency == null)
                    {
                        newEmergency.CompileDate = DateTime.Now;
                        emergencyInfo.FileId = newEmergency.FileId = SQLHelper.GetNewID();
                        newEmergency.FileCode = CodeRecordsService.ReturnCodeByMenuIdProjectId(Const.ProjectEmergencyTeamAndTrainMenuId, newEmergency.ProjectId, null);
                        db.Emergency_EmergencyTeamAndTrain.InsertOnSubmit(newEmergency);
                        db.SubmitChanges();
                    }
                    else
                    {
                        emergencyInfo.FileId = updateEmergency.FileId;
                        updateEmergency.States = newEmergency.States;
                        updateEmergency.UnitId = newEmergency.UnitId;
                        updateEmergency.FileCode = newEmergency.FileCode;
                        updateEmergency.FileName = newEmergency.FileName;
                        updateEmergency.FileContent = newEmergency.FileContent;
                        db.SubmitChanges();
                        var delItem = from x in db.Emergency_EmergencyTeamItem where x.FileId == updateEmergency.FileId select x;
                        if (delItem.Count() > 0)
                        {
                            db.Emergency_EmergencyTeamItem.DeleteAllOnSubmit(delItem);
                            db.SubmitChanges();
                        }
                    }
                    if (emergencyInfo.EmergencyTeamItem != null && emergencyInfo.EmergencyTeamItem.Count() > 0)
                    {
                        var getItems = from x in emergencyInfo.EmergencyTeamItem
                                       select new Model.Emergency_EmergencyTeamItem
                                       {
                                           EmergencyTeamItemId =SQLHelper.GetNewID(),
                                           FileId = emergencyInfo.FileId,
                                           PersonId = x.PersonId,
                                           Job = x.Job,
                                           Tel = x.Tel,
                                       };
                        if (getItems.Count() > 0)
                        {
                            db.Emergency_EmergencyTeamItem.InsertAllOnSubmit(getItems);
                            db.SubmitChanges();
                        }
                    }

                    if (emergencyInfo.States == Const.State_1)
                    {
                        CommonService.btnSaveData(newEmergency.ProjectId, Const.ProjectEmergencyTeamAndTrainMenuId, newEmergency.FileId, newEmergency.CompileMan, true, newEmergency.FileName, "../Emergency/EmergencyTeamAndTrainView.aspx?FileId={0}");
                    }
                    menuId = Const.ProjectEmergencyTeamAndTrainMenuId;
                }
                else
                {
                }
                ///// 附件保存
                if (!string.IsNullOrEmpty(menuId) && !string.IsNullOrEmpty(emergencyInfo.FileId))
                {
                    APIUpLoadFileService.SaveAttachUrl(menuId, emergencyInfo.FileId, emergencyInfo.AttachUrl, "0");
                }
            }
        }
        #endregion

        #region 获取应急流程列表信息
        /// <summary>
        /// 获取应急队伍列表信息
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="processSteps"></param>
        /// <returns></returns>
        public static Model.EmergencyProcessItem getEmergencyProcessItem(string projectId, string processSteps)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var getDataList = (from x in db.Emergency_EmergencyProcess
                                   where x.ProjectId == projectId && x.ProcessSteps == processSteps
                                   select new Model.EmergencyProcessItem
                                   {
                                       EmergencyProcessId = x.EmergencyProcessId,
                                       ProjectId = x.ProjectId,
                                       ProcessSteps = x.ProcessSteps,
                                       ProcessName = x.ProcessName,
                                       StepOperator = x.StepOperator,
                                       Remark = x.Remark,
                                   }).FirstOrDefault();
                if (getDataList == null)
                {
                    getDataList = (from x in db.Emergency_EmergencyProcess
                                   where x.ProjectId == null && x.ProcessSteps == processSteps
                                   select new Model.EmergencyProcessItem
                                   {
                                       EmergencyProcessId = x.EmergencyProcessId,
                                       ProjectId = x.ProjectId,
                                       ProcessSteps = x.ProcessSteps,
                                       ProcessName = x.ProcessName,
                                       StepOperator = x.StepOperator,
                                       Remark = x.Remark,
                                   }).FirstOrDefault();
                }
                return getDataList;
            }
        }
        #endregion        

        #region 获取应急流程列表信息
        /// <summary>
        /// 获取应急队伍列表信息
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="strParam"></param>
        /// <returns></returns>
        public static List<Model.EmergencyProcessItem> getEmergencyProcessList(string projectId, string strParam)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var getDataList = from x in db.Emergency_EmergencyProcess
                                  where x.ProjectId == projectId
                                  && (strParam == null || x.ProcessName.Contains(strParam) || x.StepOperator.Contains(strParam))
                                  orderby x.ProcessSteps
                                  select new Model.EmergencyProcessItem
                                  {
                                      EmergencyProcessId = x.EmergencyProcessId,
                                      ProjectId = x.ProjectId,
                                      ProcessSteps = x.ProcessSteps,
                                      ProcessName = x.ProcessName,
                                      StepOperator = x.StepOperator,
                                      Remark = x.Remark,
                                  };
                return getDataList.ToList();
            }
        }
        #endregion        
    }
}
