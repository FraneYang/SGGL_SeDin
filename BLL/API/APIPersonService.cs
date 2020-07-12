using EmitMapper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    public static class APIPersonService
    {
        #region 获取项目现场人员登录信息
        /// <summary>
        /// 获取项目现场人员登录信息
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public static Model.UserItem PersonLogOn(Model.UserItem userInfo)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var getUser = from x in db.SitePerson_Person
                              where (x.Telephone == userInfo.Account || x.PersonName == userInfo.Account)
                              && (x.Password == Funs.EncryptionPassword(userInfo.Password)
                                 || (x.IdentityCard != null && x.IdentityCard.Substring(x.IdentityCard.Length - 4) == userInfo.Password))
                              && x.InTime <= DateTime.Now && (!x.OutTime.HasValue || x.OutTime >= DateTime.Now) && x.IsUsed == true
                              select new Model.UserItem
                              {
                                  UserId = x.PersonId,
                                  UserCode = x.CardNo,
                                  Password = x.Password,
                                  UserName = x.PersonName,
                                  UnitId = x.UnitId,
                                  LoginProjectId = x.ProjectId,
                                  IdentityCard = x.IdentityCard,
                                  Account = x.Telephone,
                                  UnitName = db.Base_Unit.First(u => u.UnitId == x.UnitId).UnitName,
                                  LoginProjectName = db.Base_Project.First(u => u.ProjectId == x.ProjectId).ProjectName,
                                  Telephone = x.Telephone,
                                  WorkPostId = x.WorkPostId,
                                  WorkPostName = db.Base_WorkPost.First(w => w.WorkPostId == x.WorkPostId).WorkPostName,
                                  UserType = "3",
                              };

                return getUser.FirstOrDefault();
            }
        }
        #endregion

        #region 根据personId获取人员信息
        /// <summary>
        /// 根据personId获取人员信息
        /// </summary>
        /// <param name="personId"></param>
        /// <returns></returns>
        public static Model.PersonItem getPersonByPersonId(string personId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var getPerson = from x in db.View_SitePerson_Person
                                where x.PersonId == personId || x.IdentityCard == personId
                                select new Model.PersonItem
                                {
                                    PersonId = x.PersonId,
                                    CardNo = x.CardNo,
                                    PersonName = x.PersonName,
                                    Sex = x.Sex,
                                    SexName = x.SexName,
                                    IdentityCard = x.IdentityCard,
                                    Address = x.Address,
                                    ProjectId = x.ProjectId,
                                    ProjectCode = x.ProjectCode,
                                    ProjectName = x.ProjectName,
                                    UnitId = x.UnitId,
                                    UnitCode = x.UnitCode,
                                    UnitName = x.UnitName,
                                    TeamGroupId = x.TeamGroupId,
                                    TeamGroupName = x.TeamGroupName,
                                    WorkPostId = x.WorkPostId,
                                    WorkPostName = x.WorkPostName,
                                    InTime = string.Format("{0:yyyy-MM-dd}", x.InTime),
                                    OutTime = string.Format("{0:yyyy-MM-dd}", x.OutTime),
                                    OutResult = x.OutResult,
                                    Telephone = x.Telephone,
                                    PhotoUrl = x.PhotoUrl,
                                    DepartName = x.DepartName,
                                    IsUsed = x.IsUsed,
                                    IsUsedName = x.IsUsed == false ? "不启用" : "启用",
                                    AuditorId = x.AuditorId,
                                    AuditorName = db.Sys_User.First(z => z.UserId == x.AuditorId).UserName,
                                    IsForeign = x.IsForeign.HasValue ? x.IsForeign : false,
                                    IsOutside = x.IsOutside.HasValue ? x.IsOutside : false,
                                    AuditorDate = string.Format("{0:yyyy-MM-dd}", x.AuditorDate),
                                    AttachUrl1 = x.IDCardUrl == null ? APIUpLoadFileService.getFileUrl(personId + "#1", null) : x.IDCardUrl.Replace('\\', '/'),
                                    AttachUrl2 = APIUpLoadFileService.getFileUrl(personId + "#2", null),
                                    AttachUrl3 = APIUpLoadFileService.getFileUrl(personId + "#3", null),
                                    AttachUrl4 = getAttachUrl4(x.PersonId),
                                };
                return getPerson.FirstOrDefault();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string getAttachUrl4(string personId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                string returnUrl = APIUpLoadFileService.getFileUrl(personId + "#4", null);
                var getPersonQuality = db.QualityAudit_PersonQuality.FirstOrDefault(x => x.PersonId == personId);
                if (getPersonQuality != null)
                {
                    string url1 = APIUpLoadFileService.getFileUrl(getPersonQuality.PersonQualityId, null);
                    if (!string.IsNullOrEmpty(url1))
                    {
                        if (!string.IsNullOrEmpty(returnUrl))
                        {
                            returnUrl += "," + url1;
                        }
                        else
                        {
                            returnUrl = url1;
                        }
                    }
                }
                return returnUrl;
            }
        }
        #endregion

        #region 根据personid人员打回
        /// <summary>
        /// 根据personid人员打回
        /// </summary>
        /// <param name="personId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static void getReUserPersonByPersonId(string personId, string userId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var getPerson = db.SitePerson_Person.FirstOrDefault(x => x.PersonId == personId);
                if (getPerson != null)
                {
                    getPerson.IsUsed = false;
                    getPerson.AuditorId = userId;
                    getPerson.AuditorDate = DateTime.Now;
                    db.SubmitChanges();
                }
            }
        }
        #endregion

        #region 根据projectId、unitid获取人员信息
        /// <summary>
        /// 根据projectId、unitid获取人员信息
        /// </summary>
        /// <param name="personId"></param>
        /// <returns></returns>
        public static List<Model.PersonItem> getPersonByProjectIdUnitId(string projectId, string unitId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var persons = from x in db.View_SitePerson_Person
                              where x.ProjectId == projectId && (x.UnitId == unitId || unitId == null) && x.IsUsed == true
                              && x.InTime <= DateTime.Now && (!x.OutTime.HasValue || x.OutTime >= DateTime.Now)
                              orderby x.CardNo descending
                              select new Model.PersonItem
                              {
                                  PersonId = x.PersonId,
                                  CardNo = x.CardNo,
                                  PersonName = x.PersonName,
                                  SexName = x.SexName,
                                  IdentityCard = x.IdentityCard,
                                  Address = x.Address,
                                  ProjectId = x.ProjectId,
                                  ProjectCode = x.ProjectCode,
                                  ProjectName = x.ProjectName,
                                  UnitId = x.UnitId,
                                  UnitCode = x.UnitCode,
                                  UnitName = x.UnitName,
                                  TeamGroupId = x.TeamGroupId,
                                  TeamGroupName = x.TeamGroupName,
                                  WorkPostId = x.WorkPostId,
                                  WorkPostName = x.WorkPostName,
                                  InTime = string.Format("{0:yyyy-MM-dd}", x.InTime),
                                  OutTime = string.Format("{0:yyyy-MM-dd}", x.OutTime),
                                  OutResult = x.OutResult,
                                  Telephone = x.Telephone,
                                  PhotoUrl = x.PhotoUrl,
                                  DepartName = x.DepartName,
                                  WorkAreaId = x.WorkAreaId,
                                  WorkAreaName = x.WorkAreaName,
                                  PostType = x.PostType,
                                  IsForeign = x.IsForeign.HasValue ? x.IsForeign : false,
                                  IsOutside = x.IsOutside.HasValue ? x.IsOutside : false,
                                  PostTypeName = db.Sys_Const.First(z => z.GroupId == ConstValue.Group_PostType && z.ConstValue == x.PostType).ConstText,
                              };
                return persons.ToList();
            }
        }
        #endregion

        #region 获取在岗、离岗、待审人员列表
        /// <summary>
        /// 获取在岗、离岗、待审人员列表
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="unitId"></param>
        /// <param name="states">0待审1在岗2离岗</param>
        /// <param name="strUnitId">查询单位</param>
        /// <param name="strWorkPostId">查询岗位</param>
        ///  <param name="strParam">查询条件</param>
        /// <returns></returns>
        public static List<Model.PersonItem> getPersonListByProjectIdStates(string projectId, string unitId, string states, string strUnitId, string strWorkPostId, string strParam)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var getViews = from x in db.SitePerson_Person
                               where x.ProjectId == projectId && (strUnitId == null || x.UnitId == strUnitId)
                               && (strWorkPostId == null || x.WorkPostId == strWorkPostId)
                               select x;
                if (unitId != Const.UnitId_SEDIN || string.IsNullOrEmpty(unitId))
                {
                    getViews = getViews.Where(x => x.UnitId == unitId);
                }
                if (!string.IsNullOrEmpty(strParam))
                {
                    getViews = getViews.Where(x => x.PersonName.Contains(strParam) || x.IdentityCard.Contains(strParam));
                }
                if (states == "0")
                {
                    getViews = getViews.Where(x => x.IsUsed == false && !x.AuditorDate.HasValue);
                }
                else if (states == "1")
                {
                    getViews = getViews.Where(x => x.IsUsed == true && x.InTime <= DateTime.Now && (!x.OutTime.HasValue || x.OutTime >= DateTime.Now));
                }
                else if (states == "2")
                {
                    getViews = getViews.Where(x => x.IsUsed == true && x.OutTime <= DateTime.Now);
                }
                else if (states == "-1")
                {
                    getViews = getViews.Where(x => x.IsUsed == false && x.AuditorDate.HasValue);
                }


                var persons = from x in getViews
                              join y in db.Base_Unit on x.UnitId equals y.UnitId
                              orderby x.CardNo descending
                              select new Model.PersonItem
                              {
                                  PersonId = x.PersonId,
                                  CardNo = x.CardNo,
                                  PersonName = x.PersonName,
                                  SexName = (x.Sex == "2" ? "女" : "男"),
                                  IdentityCard = x.IdentityCard,
                                  Address = x.Address,
                                  ProjectId = x.ProjectId,
                                  UnitId = x.UnitId,
                                  UnitCode = y.UnitCode,
                                  UnitName = y.UnitName,
                                  TeamGroupId = x.TeamGroupId,
                                  TeamGroupName = db.ProjectData_TeamGroup.First(z => z.TeamGroupId == x.TeamGroupId).TeamGroupName,
                                  WorkPostId = x.WorkPostId,
                                  WorkPostName = db.Base_WorkPost.First(z => z.WorkPostId == x.WorkPostId).WorkPostName,
                                  InTime = string.Format("{0:yyyy-MM-dd}", x.InTime),
                                  OutTime = string.Format("{0:yyyy-MM-dd}", x.OutTime),
                                  OutResult = x.OutResult,
                                  Telephone = x.Telephone,
                                  PhotoUrl = x.PhotoUrl,
                                  IsUsed = x.IsUsed,
                                  IsUsedName = (x.IsUsed == true ? "启用" : "未启用"),
                                  WorkAreaId = x.WorkAreaId,
                                  WorkAreaName = db.WBS_UnitWork.First(z => z.UnitWorkId == x.WorkAreaId).UnitWorkName,
                                  PostType = ReturnQuality(x.PersonId, x.WorkPostId),
                                  //PostTypeName = db.Sys_Const.First(p => p.GroupId == ConstValue.Group_PostType && p.ConstValue == p.PostType).ConstText,
                                  IsForeign = x.IsForeign.HasValue ? x.IsForeign : false,
                                  IsOutside = x.IsOutside.HasValue ? x.IsOutside : false,
                              };
                return persons.ToList();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        private static string ReturnQuality(string personId, string workPostId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                string postType = "";
                var workPost = db.Base_WorkPost.FirstOrDefault(x => x.WorkPostId == workPostId);
                if (workPost != null)
                {
                    if (workPost.PostType == Const.PostType_2)
                    {
                        var getPerQ = db.QualityAudit_PersonQuality.FirstOrDefault(x => x.PersonId == personId && x.States == Const.State_2 && x.LimitDate >= DateTime.Now.AddMonths(1));
                        if (getPerQ == null)
                        {
                            postType = "1";
                        }
                    }
                    else if (workPost.PostType == Const.PostType_5)
                    {
                        var getPerQ = db.QualityAudit_EquipmentPersonQuality.FirstOrDefault(x => x.PersonId == personId && x.States == Const.State_2 && x.LimitDate >= DateTime.Now.AddMonths(1));
                        if (getPerQ == null)
                        {
                            postType = "3";
                        }
                    }
                    if (workPost.IsHsse == true)
                    {
                        var getPerQ = db.QualityAudit_SafePersonQuality.FirstOrDefault(x => x.PersonId == personId && x.States == Const.State_2 && x.LimitDate >= DateTime.Now.AddMonths(1));
                        if (getPerQ == null)
                        {
                            postType = "2";
                        }
                    }
                }
                return postType;
            }
        }
        #endregion

        #region 根据培训类型获取项目培训人员信息
        /// <summary>
        /// 根据培训类型获取项目培训人员信息
        /// </summary>
        /// <param name="projectId">项目ID</param>
        /// <param name="unitIds">培训单位ID</param>
        /// <param name="workPostIds">培训岗位ID</param>
        /// <param name="trainTypeId">培训类型ID</param>
        /// <returns></returns>
        public static List<Model.PersonItem> getTrainingPersonListByTrainTypeId(string projectId, string unitIds, string workPostIds, string trainTypeId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                List<string> unitIdList = Funs.GetStrListByStr(unitIds, ',');
                var getPersons = from x in db.View_SitePerson_Person
                                 where x.ProjectId == projectId && unitIdList.Contains(x.UnitId) && x.IsUsed == true
                                 && x.InTime <= DateTime.Now && (!x.OutTime.HasValue || x.OutTime >= DateTime.Now)
                                 select new Model.PersonItem
                                 {
                                     PersonId = x.PersonId,
                                     CardNo = x.CardNo,
                                     PersonName = x.PersonName,
                                     SexName = x.SexName,
                                     IdentityCard = x.IdentityCard,
                                     Address = x.Address,
                                     ProjectId = x.ProjectId,
                                     ProjectCode = x.ProjectCode,
                                     ProjectName = x.ProjectName,
                                     UnitId = x.UnitId,
                                     UnitCode = x.UnitCode,
                                     UnitName = x.UnitName,
                                     TeamGroupId = x.TeamGroupId,
                                     TeamGroupName = x.TeamGroupName,
                                     WorkPostId = x.WorkPostId,
                                     WorkPostName = x.WorkPostName,
                                     InTime = string.Format("{0:yyyy-MM-dd}", x.InTime),
                                     OutTime = string.Format("{0:yyyy-MM-dd}", x.OutTime),
                                     OutResult = x.OutResult,
                                     Telephone = x.Telephone,
                                     PhotoUrl = x.PhotoUrl,
                                     DepartName = x.DepartName,
                                 };
                if (!string.IsNullOrEmpty(workPostIds))
                {
                    List<string> workPostIdList = Funs.GetStrListByStr(workPostIds, ',');
                    getPersons = getPersons.Where(x => workPostIdList.Contains(x.WorkPostId));
                }

                List<Model.PersonItem> getTrainPersonList = new List<Model.PersonItem>();
                var getTrainType = TrainTypeService.GetTrainTypeById(trainTypeId);
                if (getTrainType != null && (!getTrainType.IsRepeat.HasValue || getTrainType.IsRepeat == false))
                {
                    foreach (var item in getPersons)
                    {
                        var getTrainPersonIdList1 = (from x in db.EduTrain_TrainRecordDetail
                                                     join y in db.EduTrain_TrainRecord on x.TrainingId equals y.TrainingId
                                                     where y.ProjectId == projectId && y.TrainTypeId == trainTypeId && x.CheckResult == true && x.PersonId == item.PersonId
                                                     select x).FirstOrDefault();
                        if (getTrainPersonIdList1 == null)
                        {
                            var getTrainPersonIdList2 = (from x in db.Training_Task
                                                         join y in db.Training_Plan on x.PlanId equals y.PlanId
                                                         where y.ProjectId == projectId && y.TrainTypeId == trainTypeId && y.States != "3" && x.UserId == item.PersonId
                                                         select x).FirstOrDefault();
                            if (getTrainPersonIdList2 == null)
                            {
                                getTrainPersonList.Add(item);
                            }
                        }
                    }
                    return getTrainPersonList;
                }
                else
                {
                    return getPersons.ToList();
                }
            }
        }
        #endregion

        #region 人员信息保存方法
        /// <summary>
        /// 人员信息保存方法
        /// </summary>
        /// <param name="person">人员信息</param>
        public static void SaveSitePerson(Model.PersonItem person)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                Model.SitePerson_Person newPerson = new Model.SitePerson_Person
                {
                    PersonId = person.PersonId,
                    ProjectId = person.ProjectId,
                    CardNo = person.CardNo,
                    PersonName = person.PersonName,
                    IdentityCard = person.IdentityCard,
                    Address = person.Address,
                    UnitId = person.UnitId,
                    OutResult = person.OutResult,
                    Telephone = person.Telephone,
                    PhotoUrl = person.PhotoUrl,
                    IDCardUrl = person.AttachUrl1,
                    InTime = Funs.GetNewDateTime(person.InTime),
                    OutTime = Funs.GetNewDateTime(person.OutTime),
                    AuditorId = person.AuditorId,
                    IsForeign = person.IsForeign,
                    IsOutside = person.IsOutside,
                     //AuditorDate = Funs.GetNewDateTime(person.AuditorDate),
                    Sex = person.Sex,
                };
                if (!string.IsNullOrEmpty(person.TeamGroupId))
                {
                    newPerson.TeamGroupId = person.TeamGroupId;
                }
                if (!string.IsNullOrEmpty(person.WorkPostId))
                {
                    newPerson.WorkPostId = person.WorkPostId;
                }
                if (!string.IsNullOrEmpty(person.WorkAreaId))
                {
                    newPerson.WorkAreaId = person.WorkAreaId;
                }
                if (person.IsUsed == true)
                {
                    newPerson.IsUsed = true;
                }
                else
                {
                    newPerson.IsUsed = false;
                }
                newPerson.Password = PersonService.GetPersonPassWord(person.IdentityCard);
                var getPerson = db.SitePerson_Person.FirstOrDefault(x => (x.IdentityCard == newPerson.IdentityCard && x.ProjectId==newPerson.ProjectId) || x.PersonId == newPerson.PersonId);
                if (getPerson == null)
                {
                    newPerson.Isprint = "0";
                    newPerson.PersonId = SQLHelper.GetNewID();
                    db.SitePerson_Person.InsertOnSubmit(newPerson);
                    db.SubmitChanges();
                    CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(Const.PersonListMenuId, person.ProjectId, person.UnitId, person.PersonId, newPerson.InTime);
                }
                else
                {
                    newPerson.PersonId = getPerson.PersonId;
                    getPerson.ProjectId = person.ProjectId;
                    getPerson.CardNo = person.CardNo;
                    getPerson.PersonName = person.PersonName;
                    getPerson.IdentityCard = person.IdentityCard;
                    getPerson.Address = person.Address;
                    getPerson.UnitId = person.UnitId;
                    getPerson.OutResult = person.OutResult;
                    getPerson.Telephone = person.Telephone;
                    getPerson.Password = newPerson.Password;
                    if (!string.IsNullOrEmpty(person.PhotoUrl))
                    {
                        getPerson.PhotoUrl = person.PhotoUrl;
                    }
                    if (!string.IsNullOrEmpty(person.AttachUrl1))
                    {
                        getPerson.IDCardUrl = person.AttachUrl1;
                    }
                    getPerson.InTime = Funs.GetNewDateTime(person.InTime);
                    getPerson.OutTime = Funs.GetNewDateTime(person.OutTime);
                    getPerson.Sex = person.Sex;

                   // getPerson.AuditorDate = Funs.GetNewDateTime(person.AuditorDate);
                    if (!string.IsNullOrEmpty(person.TeamGroupId))
                    {
                        getPerson.TeamGroupId = person.TeamGroupId;
                    }
                    if (!string.IsNullOrEmpty(person.WorkPostId))
                    {
                        getPerson.WorkPostId = person.WorkPostId;
                    }
                    if (!string.IsNullOrEmpty(person.WorkAreaId))
                    {
                        getPerson.WorkAreaId = person.WorkAreaId;
                    }
                    if (getPerson.AuditorDate.HasValue && getPerson.IsUsed == false)
                    {
                        getPerson.AuditorDate = null;
                    }
                    else
                    {
                        getPerson.IsUsed = person.IsUsed;
                        getPerson.AuditorDate = DateTime.Now;
                    }
                    getPerson.AuditorId = person.AuditorId;
                    if (!newPerson.OutTime.HasValue)
                    {
                        getPerson.OutTime = null;
                        getPerson.ExchangeTime = null;
                    }

                    getPerson.ExchangeTime2 = null;
                    getPerson.IsForeign = person.IsForeign;
                    getPerson.IsOutside = person.IsOutside;
                    db.SubmitChanges();
                }
                if (!string.IsNullOrEmpty(newPerson.PersonId))
                {
                    SaveMeetUrl(newPerson.PersonId, Const.ProjectPersonChangeMenuId, person.AttachUrl1, person.AttachUrl2, person.AttachUrl3, person.AttachUrl4);
                }
                // 更新同身份证号码用户的电话
                if(!string.IsNullOrEmpty(newPerson.Telephone))
                {
                    var getUser = db.Sys_User.FirstOrDefault(x => x.IdentityCard == newPerson.IdentityCard);
                    if (getUser != null)
                    {
                        getUser.Telephone = newPerson.Telephone;
                        db.SubmitChanges();
                    }
                }

                if (!newPerson.AuditorDate.HasValue && string.IsNullOrEmpty(newPerson.AuditorId))
                {
                    APICommonService.SendSubscribeMessage(newPerson.AuditorId, "人员信息" + newPerson.PersonName + "待您审核", person.ProjectCode, string.Format("{0:yyyy-MM-dd HH:mm:ss}", DateTime.Now));
                }
            }
        }

        #region 人员附件保存方法
        /// <summary>
        /// 人员附件保存方法
        /// </summary>
        public static void SaveMeetUrl(string personId, string menuId, string url1, string url2, string url3, string url4)
        {
            Model.ToDoItem toDoItem = new Model.ToDoItem
            {
                MenuId = menuId,
                DataId = personId + "#1",
                UrlStr = url1,
            };
            if (!string.IsNullOrEmpty(url1))
            {
                APIUpLoadFileService.SaveAttachUrl(toDoItem);
            }

            toDoItem.DataId = personId + "#2";
            toDoItem.UrlStr = url2;
            if (!string.IsNullOrEmpty(url2))
            {
                APIUpLoadFileService.SaveAttachUrl(toDoItem);
            }

            toDoItem.DataId = personId + "#3";
            toDoItem.UrlStr = url3;
            if (!string.IsNullOrEmpty(url3))
            {
                APIUpLoadFileService.SaveAttachUrl(toDoItem);
            }

            toDoItem.DataId = personId + "#4";
            toDoItem.UrlStr = url4;
            if (!string.IsNullOrEmpty(url4))
            { APIUpLoadFileService.SaveAttachUrl(toDoItem); }
        }
        #endregion
        #endregion

        #region 更新人员附件
        /// <summary>
        /// 更新人员附件
        /// </summary>
        /// <param name="person"></param>
        public static void SaveSitePersonAttachment(Model.PersonItem person)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var getPerson = db.SitePerson_Person.FirstOrDefault(x => x.IdentityCard == person.IdentityCard || x.PersonId == person.PersonId);
                if (getPerson != null)
                {
                    if (!string.IsNullOrEmpty(person.PhotoUrl))
                    {
                        getPerson.PhotoUrl = person.PhotoUrl;
                    }
                    if (!string.IsNullOrEmpty(person.AttachUrl1))
                    {
                        getPerson.IDCardUrl = person.AttachUrl1;
                    }
                    db.SubmitChanges();
                    SaveMeetUrl(getPerson.PersonId, Const.ProjectPersonChangeMenuId, person.AttachUrl1, person.AttachUrl2, person.AttachUrl3, person.AttachUrl4);
                }
            }
        }
        #endregion

        #region 人员离场
        /// <summary>
        /// 人员离场
        /// </summary>
        /// <param name="personId"></param>
        public static void getPersonOut(string personId)
        {
            if (!string.IsNullOrEmpty(personId))
            {
                using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
                {
                    List<string> getLists = Funs.GetStrListByStr(personId, ',');
                    foreach (var item in getLists)
                    {
                        var getPerson = db.SitePerson_Person.FirstOrDefault(x => x.PersonId == item);
                        if (getPerson != null)
                        {
                            getPerson.OutTime = DateTime.Now;
                            PersonService.UpdatePerson(getPerson);
                        }
                    }
                }
            }
        }
        #endregion

        #region 人员出入场
        /// <summary>
        /// 人员出入场
        /// </summary>
        /// <param name="projectId"></param>w
        /// <param name="idCard"></param>
        /// <param name="isIn"></param>
        /// <param name="changeTime"></param>
        public static void getPersonInOut(string projectId, string idCard, int isIn, DateTime changeTime)
        {
            if (!string.IsNullOrEmpty(idCard))
            {
                using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
                {
                    var getPerson = db.SitePerson_Person.FirstOrDefault(x => x.ProjectId == projectId && x.IdentityCard == idCard);
                    if (getPerson != null)
                    {
                        var getPersonInOut = db.SitePerson_PersonInOut.FirstOrDefault(x => x.PersonId == getPerson.PersonId && x.ProjectId == projectId && x.ChangeTime == changeTime);
                        if (getPersonInOut == null)
                        {
                            Model.SitePerson_PersonInOut newInOut = new Model.SitePerson_PersonInOut
                            {
                                PersonInOutId = SQLHelper.GetNewID(),
                                ProjectId = projectId,
                                UnitId = getPerson.UnitId,
                                PersonId = getPerson.PersonId,
                                IsIn = isIn == 1 ? true : false,
                                ChangeTime = changeTime,
                            };

                            db.SitePerson_PersonInOut.InsertOnSubmit(newInOut);
                            db.SubmitChanges();

                           // GetDataService.CorrectingPersonInOutNumber(projectId);
                        }
                    }
                }
            }
        }
        #endregion

        #region 更新人员数据交换时间
        /// <summary>
        /// 更新人员数据交换时间
        /// </summary>
        /// <param name="personId">人员ID</param>
        public static void getUpdatePersonExchangeTime(string personId, string type)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var getPerson = db.SitePerson_Person.FirstOrDefault(e => e.PersonId == personId);
                if (getPerson != null && !string.IsNullOrEmpty(type))
                {
                    if (type == "1")
                    {
                        getPerson.ExchangeTime2 = DateTime.Now;
                    }
                    else
                    {
                        getPerson.ExchangeTime = DateTime.Now;
                    }
                    db.SubmitChanges();
                }
            }
        }
        #endregion

        #region 获取人员信息出入场记录
        /// <summary>
        /// 获取人员信息出入场记录
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="unitId"></param>
        /// <param name="strParam"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public static List<Model.PersonInOutItem> getPersonInOutList(string projectId, string unitId, string startTime, string endTime)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                DateTime? startTimeD = Funs.GetNewDateTime(startTime);
                DateTime? endTimeD = Funs.GetNewDateTime(endTime);
                var personInOuts = from x in db.SitePerson_PersonInOut
                                   join y in db.SitePerson_Person on x.PersonId equals y.PersonId
                                   where x.ProjectId == projectId
                                   select new Model.PersonInOutItem
                                   {
                                       PersonId = x.PersonId,
                                       PersonName = y.PersonName,
                                       ProjectId = x.ProjectId,
                                       UnitId = y.UnitId,
                                       UnitName = db.Base_Unit.First(z => z.UnitId == y.UnitId).UnitName,
                                       WorkPostId = y.WorkPostId,
                                       WorkPostName = db.Base_WorkPost.First(z => z.WorkPostId == y.WorkPostId).WorkPostName,
                                       IsIn = x.IsIn,
                                       IsInName = x.IsIn == true ? "进场" : "出场",
                                       ChangeTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.ChangeTime),
                                       ChangeTimeD = x.ChangeTime,
                                   };
                if (!string.IsNullOrEmpty(unitId) && unitId != Const.UnitId_SEDIN)
                {
                    personInOuts = personInOuts.Where(x => x.UnitId == unitId);
                }

                if (startTimeD.HasValue)
                {
                    personInOuts = personInOuts.Where(x => x.ChangeTimeD >= startTimeD);
                }
                if (endTimeD.HasValue)
                {
                    personInOuts = personInOuts.Where(x => x.ChangeTimeD <= endTimeD);
                }

                return personInOuts.OrderByDescending(x => x.ChangeTimeD).ToList();
            }
        }
        #endregion

        #region 获取人员信息出入场记录
        /// <summary>
        /// 获取人员信息出入场记录
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="unitId"></param>
        /// <param name="strParam"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public static List<Model.PersonInOutItem> getPersonInOutList(string projectId, string userUnitId, string unitId, string workPostId, string strParam, string startTime, string endTime)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                DateTime? startTimeD = Funs.GetNewDateTime(startTime);
                DateTime? endTimeD = Funs.GetNewDateTime(endTime);
                var personInOuts = from x in db.SitePerson_PersonInOut
                                   join y in db.SitePerson_Person on x.PersonId equals y.PersonId
                                   where x.ProjectId == projectId
                                   select new Model.PersonInOutItem
                                   {
                                       PersonId = x.PersonId,
                                       PersonName = y.PersonName,
                                       ProjectId = x.ProjectId,
                                       UnitId = y.UnitId,
                                       UnitName = db.Base_Unit.First(z => z.UnitId == y.UnitId).UnitName,
                                       WorkPostId = y.WorkPostId,
                                       WorkPostName = db.Base_WorkPost.First(z => z.WorkPostId == y.WorkPostId).WorkPostName,
                                       IsIn = x.IsIn,
                                       IsInName = x.IsIn == true ? "进场" : "出场",
                                       ChangeTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.ChangeTime),
                                       ChangeTimeD = x.ChangeTime,
                                   };
                if (!string.IsNullOrEmpty(userUnitId) && unitId != Const.UnitId_SEDIN)
                {
                    personInOuts = personInOuts.Where(x => x.UnitId == userUnitId);
                }
                if (!string.IsNullOrEmpty(unitId))
                {
                    personInOuts = personInOuts.Where(x => x.UnitId == unitId);
                }
                if (!string.IsNullOrEmpty(workPostId))
                {
                    personInOuts = personInOuts.Where(x => x.WorkPostId == workPostId);
                }
                if (startTimeD.HasValue)
                {
                    personInOuts = personInOuts.Where(x => x.ChangeTimeD >= startTimeD);
                }
                if (endTimeD.HasValue)
                {
                    personInOuts = personInOuts.Where(x => x.ChangeTimeD <= endTimeD);
                }
                if (!string.IsNullOrEmpty(strParam))
                {
                    personInOuts = personInOuts.Where(x => x.PersonName.Contains(strParam));
                }
                return personInOuts.OrderByDescending(x => x.ChangeTimeD).ToList();
            }
        }
        #endregion

        #region 根据人员ID获取个人出入场记录
        /// <summary>
        /// 根据人员ID获取个人出入场记录
        /// </summary>
        /// <param name="personId"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public static List<Model.PersonInOutItem> getPersonInOutListByPersonId(string personId, string startTime, string endTime)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                DateTime? startTimeD = Funs.GetNewDateTime(startTime);
                DateTime? endTimeD = Funs.GetNewDateTime(endTime);
                var personInOuts = from x in db.SitePerson_PersonInOut
                                   join y in db.SitePerson_Person on x.PersonId equals y.PersonId
                                   where x.PersonId == personId
                                   select new Model.PersonInOutItem
                                   {
                                       PersonId = x.PersonId,
                                       PersonName = y.PersonName,
                                       ProjectId = x.ProjectId,
                                       UnitId = y.UnitId,
                                       UnitName = db.Base_Unit.First(z => z.UnitId == y.UnitId).UnitName,
                                       WorkPostId = y.WorkPostId,
                                       WorkPostName = db.Base_WorkPost.First(z => z.WorkPostId == y.WorkPostId).WorkPostName,
                                       IsIn = x.IsIn,
                                       IsInName = x.IsIn == true ? "进场" : "出场",
                                       ChangeTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.ChangeTime),
                                       ChangeTimeD = x.ChangeTime,
                                   };
                if (startTimeD.HasValue)
                {
                    personInOuts = personInOuts.Where(x => x.ChangeTimeD >= startTimeD);
                }
                if (endTimeD.HasValue)
                {
                    personInOuts = personInOuts.Where(x => x.ChangeTimeD <= endTimeD);
                }
                return personInOuts.OrderByDescending(x => x.ChangeTimeD).ToList();
            }
        }
        #endregion

        #region 根据identityCard获取人员资质信息
        /// <summary>
        /// 根据identityCard获取人员资质信息
        /// </summary>
        /// <param name="identityCard"></param>
        /// <returns></returns>
        public static Model.PersonQualityItem getPersonQualityByIdentityCard(string identityCard,string projectId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var getLists = from y in db.SitePerson_Person
                               join x in db.QualityAudit_PersonQuality on y.PersonId equals x.PersonId
                               where (y.IdentityCard == identityCard || x.PersonId == identityCard) && (projectId == null || y.ProjectId == projectId)
                               orderby y.CardNo
                               select new Model.PersonQualityItem
                               {
                                   PersonQualityId = x.PersonQualityId,
                                   PersonId = x.PersonId,
                                   PersonName = y.PersonName,
                                   CardNo = y.CardNo,
                                   IdentityCard = y.IdentityCard,
                                   ProjectId = y.ProjectId,
                                   UnitId = y.UnitId,
                                   UnitName = db.Base_Unit.First(z => z.UnitId == y.UnitId).UnitName,
                                   UnitCode = db.Base_Unit.First(z => z.UnitId == y.UnitId).UnitCode,
                                   WorkPostId = y.WorkPostId,
                                   WorkPostName = db.Base_WorkPost.First(z => z.WorkPostId == y.WorkPostId).WorkPostName,
                                   CertificateId = x.CertificateId,
                                   CertificateName = db.Base_Certificate.First(z => z.CertificateId == x.CertificateId).CertificateName,
                                   CertificateNo = x.CertificateNo,
                                   Grade = x.Grade,
                                   SendUnit = x.SendUnit,
                                   SendDate = string.Format("{0:yyyy-MM-dd}", x.SendDate),
                                   LimitDate = string.Format("{0:yyyy-MM-dd}", x.LimitDate),
                                   LateCheckDate = string.Format("{0:yyyy-MM-dd}", x.LateCheckDate),
                                   ApprovalPerson = db.Sys_User.First(z => z.UserId == x.AuditorId).UserName,
                                   Remark = x.Remark,
                                   CompileMan = x.CompileMan,
                                   CompileManName = db.Sys_User.First(z => z.UserId == x.CompileMan).UserName,
                                   CompileDate = string.Format("{0:yyyy-MM-dd}", x.CompileDate),
                                   AuditDate = string.Format("{0:yyyy-MM-dd}", x.AuditDate),
                                   AuditorName = db.Sys_User.First(z => z.UserId == x.AuditorId).UserName,
                                   AttachUrl = APIUpLoadFileService.getFileUrl(x.PersonQualityId, null),
                               };

                return getLists.FirstOrDefault();
            }
        }
        #endregion

        #region 根据identityCard获取人员资质信息
        /// <summary>
        /// 根据identityCard获取人员资质信息
        /// </summary>
        /// <param name="identityCard"></param>
        /// <returns></returns>
        public static List<Model.TestRecordItem> getPersonTestRecoedByIdentityCard(string identityCard, string projectId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var getLists = from x in db.EduTrain_TrainRecordDetail
                               join y in db.SitePerson_Person on x.PersonId equals y.PersonId
                               join z in db.EduTrain_TrainRecord on x.TrainingId equals z.TrainingId
                               where y.IdentityCard == identityCard && (projectId == null || z.ProjectId == projectId)
                               orderby z.TrainStartDate descending
                               select new Model.TestRecordItem
                               {
                                   TestRecordId = x.TrainDetailId,
                                   ProjectId = z.ProjectId,
                                   ProjectName = db.Base_Project.First(u => u.ProjectId == z.ProjectId).ProjectName,
                                   TestPlanName = z.TrainTitle,
                                   TestManId = x.PersonId,
                                   TestManName = y.PersonName,
                                   TestStartTime = string.Format("{0:yyyy-MM-dd HH:mm}", z.TrainStartDate),
                                   TestEndTime = string.Format("{0:yyyy-MM-dd HH:mm}", z.TrainEndDate),
                                   TestScores = x.CheckScore ?? 0,
                                   CheckResult = x.CheckResult,
                                   TestType = db.Base_TrainType.First(u => u.TrainTypeId == z.TrainTypeId).TrainTypeName,
                               };

                return getLists.ToList();
            }
        }
        #endregion

        #region 根据projectId、unitid获取特岗人员资质信息
        /// <summary>
        /// 根据projectId、unitid获取特岗人员资质信息
        /// </summary>
        /// <param name="projectId">项目ID</param>
        /// <param name="unitId">单位ID</param>
        /// <param name="type">数据类型0-已过期；1-即将过期；2-无证书</param>
        /// <param name="pageIndex">页码</param>
        /// <returns></returns>
        public static List<Model.PersonQualityItem> getPersonQualityByProjectIdUnitId(string projectId, string unitId, string type)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var getLists = (from x in db.SitePerson_Person
                                join y in db.QualityAudit_PersonQuality on x.PersonId equals y.PersonId
                                join z in db.Base_WorkPost on x.WorkPostId equals z.WorkPostId
                                where x.ProjectId == projectId && z.PostType == "2"
                                orderby y.LimitDate
                                select new Model.PersonQualityItem
                                {
                                    PersonQualityId = y.PersonQualityId,
                                    PersonId = y.PersonId,
                                    PersonName = x.PersonName,
                                    CardNo = x.CardNo,
                                    IdentityCard = x.IdentityCard,
                                    ProjectId = x.ProjectId,
                                    UnitId = x.UnitId,
                                    UnitName = db.Base_Unit.First(z => z.UnitId == x.UnitId).UnitName,
                                    CertificateId = y.CertificateId,
                                    CertificateName = db.Base_Certificate.First(z => z.CertificateId == y.CertificateId).CertificateName,
                                    WorkPostId = x.WorkPostId,
                                    WorkPostName = db.Base_WorkPost.First(z => z.WorkPostId == x.WorkPostId).WorkPostName,
                                    CertificateNo = y.CertificateNo,
                                    Grade = y.Grade,
                                    SendUnit = y.SendUnit,
                                    SendDate = string.Format("{0:yyyy-MM-dd}", y.SendDate),
                                    LimitDate = string.Format("{0:yyyy-MM-dd}", y.LimitDate),
                                    LimitDateD = y.LimitDate,
                                    LateCheckDate = string.Format("{0:yyyy-MM-dd}", y.LateCheckDate),
                                    ApprovalPerson = db.Sys_User.First(z => z.UserId == x.AuditorId).UserName,
                                    Remark = y.Remark,
                                    CompileMan = y.CompileMan,
                                    CompileManName = db.Sys_User.First(z => z.UserId == y.CompileMan).UserName,
                                    CompileDate = string.Format("{0:yyyy-MM-dd}", y.CompileDate),
                                    AuditDate = string.Format("{0:yyyy-MM-dd}", y.AuditDate),
                                    AuditorName = db.Sys_User.First(z => z.UserId == x.AuditorId).UserName,
                                    AttachUrl = APIUpLoadFileService.getFileUrl(y.PersonQualityId, null),
                                }).ToList();
                if (ProjectUnitService.GetProjectUnitTypeByProjectIdUnitId(projectId, unitId))
                {
                    getLists = getLists.Where(x => x.UnitId == unitId).ToList();
                }
                if (type == "0")
                {
                    getLists = getLists.Where(x => x.CertificateId != null && x.LimitDateD < DateTime.Now).ToList();
                }
                else if (type == "1")
                {
                    getLists = getLists.Where(x => x.CertificateId != null && x.LimitDateD >= DateTime.Now && x.LimitDateD < DateTime.Now.AddMonths(1)).ToList();
                }
                else if (type == "2")
                {
                    getLists = getLists.Where(x => x.CertificateId == null).ToList();
                }
                return getLists;
            }
        }
        #endregion

        #region 人员资质信息保存方法
        /// <summary>
        /// 人员资质信息保存方法
        /// </summary>
        /// <param name="personQuality">人员信息</param>
        public static void SavePersonQuality(Model.PersonQualityItem personQuality)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                Model.QualityAudit_PersonQuality newPersonQuality = new Model.QualityAudit_PersonQuality
                {
                    PersonQualityId=personQuality.PersonQualityId,
                    PersonId = personQuality.PersonId,                    
                    CertificateNo = personQuality.CertificateNo,                
                    CertificateName = personQuality.CertificateName,
                    Grade = personQuality.Grade,
                    SendUnit = personQuality.SendUnit,
                    SendDate = Funs.GetNewDateTime(personQuality.SendDate),
                    LimitDate = Funs.GetNewDateTime(personQuality.LimitDate),
                    LateCheckDate = Funs.GetNewDateTime(personQuality.LateCheckDate),
                    ApprovalPerson = personQuality.ApprovalPerson,
                    Remark = personQuality.Remark,                 
                    CompileDate = Funs.GetNewDateTime(personQuality.CompileDate),
                    AuditDate = Funs.GetNewDateTime(personQuality.AuditDate),
                };
                if (!string.IsNullOrEmpty(personQuality.CertificateId))
                {
                    newPersonQuality.CertificateId = personQuality.CertificateId;
                }
                if (!string.IsNullOrEmpty(personQuality.CompileMan))
                {
                    newPersonQuality.CompileMan = personQuality.CompileMan;
                }
                if (!string.IsNullOrEmpty(personQuality.AuditorId))
                {
                    newPersonQuality.AuditorId = personQuality.AuditorId;
                }
                var getPersonQuality = db.QualityAudit_PersonQuality.FirstOrDefault(x => x.PersonQualityId == newPersonQuality.PersonQualityId || x.PersonId == newPersonQuality.PersonId);
                if (getPersonQuality == null)
                {
                    newPersonQuality.PersonQualityId = SQLHelper.GetNewID();
                    newPersonQuality.AuditDate = null;
                    db.QualityAudit_PersonQuality.InsertOnSubmit(newPersonQuality);
                    db.SubmitChanges();
                }
                else
                {
                    newPersonQuality.PersonQualityId = getPersonQuality.PersonQualityId;
                    getPersonQuality.CertificateId = newPersonQuality.CertificateId;
                    getPersonQuality.CertificateNo = newPersonQuality.CertificateNo;
                    getPersonQuality.CertificateName = newPersonQuality.CertificateName;
                    getPersonQuality.Grade = newPersonQuality.Grade;
                    getPersonQuality.SendUnit = newPersonQuality.SendUnit;
                    getPersonQuality.SendDate = newPersonQuality.SendDate;
                    getPersonQuality.LimitDate = newPersonQuality.LimitDate;
                    getPersonQuality.LateCheckDate = newPersonQuality.LateCheckDate;
                    getPersonQuality.ApprovalPerson = newPersonQuality.ApprovalPerson;
                    getPersonQuality.Remark = newPersonQuality.Remark;
                    getPersonQuality.CompileMan = newPersonQuality.CompileMan;
                    getPersonQuality.CompileDate = newPersonQuality.CompileDate;
                    getPersonQuality.AuditDate = newPersonQuality.AuditDate;
                    getPersonQuality.AuditorId = newPersonQuality.AuditorId;
                    db.SubmitChanges();
                }
                if (!string.IsNullOrEmpty(newPersonQuality.PersonQualityId))
                {
                    APIUpLoadFileService.SaveAttachUrl(Const.PersonQualityMenuId, newPersonQuality.PersonQualityId, personQuality.AttachUrl, "0");                    
                }
            }
        }
        #endregion
    }
}
