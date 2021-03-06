﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 人员信息
    /// </summary>
    public static class PersonService
    {
        /// <summary>
        /// 根据主键获取人员信息
        /// </summary>
        /// <param name="personId"></param>
        /// <returns></returns>
        public static Model.SitePerson_Person GetPersonById(string personId)
        {
            return Funs.DB.SitePerson_Person.FirstOrDefault(e => e.PersonId == personId);
        }

        /// <summary>
        /// 根据主键获取人员信息
        /// </summary>
        /// <param name="personId"></param>
        /// <returns></returns>
        public static string GetPersonNameById(string personId)
        {
            string name = string.Empty;
            var getp = Funs.DB.SitePerson_Person.FirstOrDefault(e => e.PersonId == personId);
            if (getp != null)
            {
                name = getp.PersonName;
            }
            return name;
        }

        /// <summary>
        /// 根据UserId主键获取人员信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static string GetPersonIdByUserId(string userId)
        {
            string personId = userId;
            var getPerson = GetPersonById(userId);
            if (getPerson == null)
            {
                var getUser = UserService.GetUserByUserId(userId);
                if (getUser != null)
                {
                    getPerson = Funs.DB.SitePerson_Person.FirstOrDefault(e => e.IdentityCard == getUser.IdentityCard);
                    if (getPerson != null)
                    {
                        personId = getPerson.PersonId;
                    }
                }
            }

            return personId;
        }

        /// <summary>
        /// 根据UserId主键获取人员信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static Model.SitePerson_Person GetPersonByUserId(string userId, string projectId)
        {
            var getPerson = GetPersonById(userId);
            if (getPerson == null)
            {
                var getUser = UserService.GetUserByUserId(userId);
                if (getUser != null)
                {
                    getPerson = Funs.DB.SitePerson_Person.FirstOrDefault(e => e.IdentityCard == getUser.IdentityCard && e.ProjectId == projectId);
                }
            }

            return getPerson;
        }

        /// <summary>
        /// 根据项目单位获取人员信息
        /// </summary>
        /// <param name="personId"></param>
        /// <returns></returns>
        public static List<Model.SitePerson_Person> GetPersonLitsByprojectIdUnitId(string projectId, string unitId)
        {
            if (!string.IsNullOrEmpty(unitId))
            {
                return (from x in Funs.DB.SitePerson_Person
                        where x.ProjectId == projectId && x.UnitId == unitId
                        orderby x.PersonName
                        select x).ToList();
            }
            else
            {
                return (from x in Funs.DB.SitePerson_Person
                        where x.ProjectId == projectId
                        orderby x.PersonName
                        select x).ToList();
            }

        }

        /// <summary>
        /// 根据项目单位获取人员信息
        /// </summary>
        /// <param name="personId"></param>
        /// <returns></returns>
        public static List<Model.SitePerson_Person> GetPersonLitsByprojectIdUnitIdTeamGroupId(string projectId, string unitId, string teamGroupId)
        {
            var getPersons = GetPersonLitsByprojectIdUnitId(projectId, unitId);
            if (!string.IsNullOrEmpty(teamGroupId))
            {
                getPersons = getPersons.Where(x => x.TeamGroupId == teamGroupId).OrderBy(x => x.PersonName).ToList();
            }

            return getPersons;
        }

        /// <summary>
        /// 获取最大的人员位置
        /// </summary>
        /// <returns>最大的人员位置</returns>
        public static int? GetMaxPersonIndex(string projectId)
        {
            return (from x in Funs.DB.SitePerson_Person where x.ProjectId == projectId select x.PersonIndex).Max();
        }

        /// <summary>
        /// 根据单位Id查询所有人员的数量
        /// </summary>
        /// <param name="unitId">单位Id</param>
        /// <returns>人员的数量</returns>
        public static int GetPersonCountByUnitId(string unitId, string projectId)
        {
            var q = (from x in Funs.DB.SitePerson_Person where x.UnitId == unitId && x.ProjectId == projectId && x.IsUsed == true select x).ToList();
            return q.Count();
        }

        /// <summary>
        /// 根据单位Id查询所有HSE人员的数量
        /// </summary>
        /// <param name="unitId">单位Id</param>
        /// <returns>HSE人员的数量</returns>
        public static int GetHSEPersonCountByUnitId(string unitId, string projectId)
        {
            var q = (from x in Funs.DB.SitePerson_Person where x.UnitId == unitId && x.ProjectId == projectId && (x.WorkPostId == BLL.Const.WorkPost_HSSEEngineer || x.WorkPostId == BLL.Const.WorkPost_SafetyManager) && x.IsUsed == true select x).ToList();
            return q.Count();
        }

        /// <summary>
        /// 获取所有人员位置集合
        /// </summary>
        /// <returns>所有人员位置集合</returns>
        public static List<int?> GetPersonIndexs(string projectId)
        {
            return (from x in Funs.DB.SitePerson_Person where x.ProjectId == projectId select x.PersonIndex).ToList();
        }

        /// <summary>
        /// 根据卡号查询人员信息
        /// </summary>
        /// <param name="cardNo">卡号</param>
        /// <returns>人员实体</returns>
        public static Model.SitePerson_Person GetPersonByCardNo(string projectId, string cardNo)
        {
            return Funs.DB.SitePerson_Person.FirstOrDefault(e => e.ProjectId == projectId && e.CardNo == cardNo);
        }

        /// <summary>
        /// 根据卡号查询所有人员的数量
        /// </summary>
        /// <param name="cardNo">卡号</param>
        /// <returns>人员的数量</returns>
        public static int GetPersonCountByCardNo(string projectId, string cardNo)
        {
            var q = (from x in Funs.DB.SitePerson_Person where x.ProjectId == projectId && x.CardNo == cardNo select x).ToList();
            return q.Count();
        }

        /// <summary>
        /// 根据人员姓名和所在单位判断人员是否存在
        /// </summary>
        /// <param name="unitId"></param>
        /// <param name="personName"></param>
        /// <returns></returns>
        public static bool IsExistPersonByUnit(string unitId, string personName, string projectId)
        {
            var q = from x in Funs.DB.SitePerson_Person where x.UnitId == unitId && x.PersonName == personName && x.ProjectId == projectId select x;
            if (q.Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 根据身份证号Id获取人员的数量
        /// </summary>
        /// <param name="identityCard">身份证号</param>
        /// <returns>人员的数量</returns>
        public static Model.SitePerson_Person GetPersonCountByIdentityCard(string identityCard, string projectId)
        {
            var q = Funs.DB.SitePerson_Person.FirstOrDefault(x => x.IdentityCard == identityCard && x.ProjectId == projectId);
            return q;
        }

        /// <summary>
        /// 获取人员信息列表
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public static List<Model.SitePerson_Person> GetPersonList(string projectId)
        {
            return (from x in Funs.DB.SitePerson_Person where x.ProjectId == projectId select x).ToList();
        }

        /// <summary>
        /// 增加人员信息
        /// </summary>
        /// <param name="person">人员实体</param>
        public static void AddPerson(Model.SitePerson_Person person)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                Model.SitePerson_Person newPerson = new Model.SitePerson_Person
                {
                    PersonId = person.PersonId,
                    CardNo = person.CardNo,
                    PersonName = person.PersonName,
                    Sex = person.Sex,
                    IdentityCard = person.IdentityCard,
                    Address = person.Address,
                    ProjectId = person.ProjectId,
                    UnitId = person.UnitId,
                    TeamGroupId = person.TeamGroupId,
                    WorkAreaId = person.WorkAreaId,
                    WorkPostId = person.WorkPostId,
                    OutTime = person.OutTime,
                    OutResult = person.OutResult,
                    Telephone = person.Telephone,
                    PositionId = person.PositionId,
                    PostTitleId = person.PostTitleId,
                    PhotoUrl = person.PhotoUrl,
                    HeadImage = person.HeadImage,
                    IsUsed = person.IsUsed,
                    IsCardUsed = person.IsCardUsed,
                    DepartId = person.DepartId,
                    FromPersonId = person.FromPersonId,
                    Password = GetPersonPassWord(person.IdentityCard),
                    AuditorId = person.AuditorId,
                    AuditorDate = person.AuditorDate,
                    IsForeign = person.IsForeign,
                    IsOutside = person.IsOutside,
                    EduLevel = person.EduLevel,
                    MaritalStatus = person.MaritalStatus,
                    Isprint = "0",
                    MainCNProfessionalId = person.MainCNProfessionalId,
                    ViceCNProfessionalId = person.ViceCNProfessionalId,
                    Birthday = person.Birthday,
                    IdcardType = person.IdcardType,
                    IdcardStartDate = person.IdcardStartDate,
                    IdcardEndDate = person.IdcardEndDate,
                    IdcardForever = person.IdcardForever,
                    PoliticsStatus = person.PoliticsStatus,
                    IdcardAddress = person.IdcardAddress,
                    Nation = person.Nation,
                    CountryCode = person.CountryCode,
                    ProvinceCode = person.ProvinceCode,
                };

                if (person.InTime.HasValue)
                {
                    newPerson.InTime = person.InTime;
                }
                else
                {
                    newPerson.InTime = Funs.GetNewDateTime(DateTime.Now.ToShortDateString());
                }

                db.SitePerson_Person.InsertOnSubmit(newPerson);
                db.SubmitChanges();

                ////增加一条编码记录
                BLL.CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(BLL.Const.PersonListMenuId, person.ProjectId, person.UnitId, person.PersonId, person.InTime);
            }
        }

        /// <summary>
        /// 修改人员信息
        /// </summary>
        /// <param name="person">人员实体</param>
        public static void UpdatePerson(Model.SitePerson_Person person)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                Model.SitePerson_Person newPerson = db.SitePerson_Person.FirstOrDefault(e => e.PersonId == person.PersonId);
                if (newPerson != null)
                {
                    newPerson.FromPersonId = person.FromPersonId;
                    newPerson.CardNo = person.CardNo;
                    newPerson.PersonName = person.PersonName;
                    newPerson.Sex = person.Sex;
                    newPerson.IdentityCard = person.IdentityCard;
                    newPerson.Address = person.Address;
                    newPerson.ProjectId = person.ProjectId;
                    newPerson.UnitId = person.UnitId;
                    newPerson.TeamGroupId = person.TeamGroupId;
                    newPerson.WorkAreaId = person.WorkAreaId;
                    newPerson.WorkPostId = person.WorkPostId;
                    newPerson.InTime = person.InTime;
                    newPerson.OutTime = person.OutTime;
                    newPerson.OutResult = person.OutResult;
                    newPerson.Telephone = person.Telephone;
                    newPerson.PositionId = person.PositionId;
                    newPerson.PostTitleId = person.PostTitleId;
                    newPerson.PhotoUrl = person.PhotoUrl;
                    newPerson.HeadImage = person.HeadImage;
                    newPerson.IsUsed = person.IsUsed;
                    newPerson.IsCardUsed = person.IsCardUsed;
                    newPerson.EduLevel = person.EduLevel;
                    newPerson.MaritalStatus = person.MaritalStatus;
                    newPerson.DepartId = person.DepartId;
                    newPerson.QRCodeAttachUrl = person.QRCodeAttachUrl;
                    newPerson.Password = GetPersonPassWord(person.IdentityCard);
                    if (!newPerson.OutTime.HasValue)
                    {
                        newPerson.OutTime = null;
                        newPerson.ExchangeTime = null;
                    }
                    newPerson.ExchangeTime2 = null;
                    newPerson.RealNameUpdateTime = null;
                    if (!string.IsNullOrEmpty(person.AuditorId))
                    {
                        newPerson.AuditorId = person.AuditorId;
                    }
                    if (person.AuditorDate.HasValue)
                    {
                        newPerson.AuditorDate = person.AuditorDate;
                    }

                    newPerson.IsForeign = person.IsForeign;
                    newPerson.IsOutside = person.IsOutside;
                    newPerson.Birthday = person.Birthday;
                    newPerson.MainCNProfessionalId = person.MainCNProfessionalId;
                    newPerson.ViceCNProfessionalId = person.ViceCNProfessionalId;
                    newPerson.IdcardType = person.IdcardType;
                    newPerson.IdcardStartDate = person.IdcardStartDate;
                    newPerson.IdcardEndDate = person.IdcardEndDate;
                    newPerson.IdcardForever = person.IdcardForever;
                    newPerson.PoliticsStatus = person.PoliticsStatus;
                    newPerson.IdcardAddress = person.IdcardAddress;
                    newPerson.Nation = person.Nation;
                    newPerson.CountryCode = person.CountryCode;
                    newPerson.ProvinceCode = person.ProvinceCode;
                    db.SubmitChanges();
                }
            }
        }

        /// <summary>
        /// 根据人员Id删除一个人员信息
        /// </summary>
        /// <param name="personId">人员Id</param>
        public static void DeletePerson(string personId)
        {
            Model.SitePerson_Person person = Funs.DB.SitePerson_Person.FirstOrDefault(e => e.PersonId == personId);
            if (person != null)
            {
                ///删除编码表记录
                BLL.CodeRecordsService.DeleteCodeRecordsByDataId(personId);

                //删除特岗人员资质
                var personQuality = PersonQualityService.GetPersonQualityByPersonId(personId);
                if (personQuality != null)
                {
                    CodeRecordsService.DeleteCodeRecordsByDataId(personQuality.PersonQualityId);//删除编号
                    CommonService.DeleteAttachFileById(personQuality.PersonQualityId);//删除附件
                    Funs.DB.QualityAudit_PersonQuality.DeleteOnSubmit(personQuality);
                    Funs.DB.SubmitChanges();
                }
                //删除安全人员资质
                Model.QualityAudit_SafePersonQuality safePersonQuality = Funs.DB.QualityAudit_SafePersonQuality.FirstOrDefault(e => e.PersonId == personId);
                if (safePersonQuality != null)
                {
                    CodeRecordsService.DeleteCodeRecordsByDataId(safePersonQuality.SafePersonQualityId);
                    CommonService.DeleteAttachFileById(safePersonQuality.SafePersonQualityId);
                    Funs.DB.QualityAudit_SafePersonQuality.DeleteOnSubmit(safePersonQuality);
                    Funs.DB.SubmitChanges();
                }
                ///违规人员
                //var getViolation = from x in Funs.DB.Check_ViolationPerson where x.PersonId == person.PersonId select x;
                //if (getViolation.Count() > 0)
                //{
                //    Funs.DB.Check_ViolationPerson.DeleteAllOnSubmit(getViolation);
                //    Funs.DB.SubmitChanges();
                //}
                ///删除考试记录
                var getTask = from x in Funs.DB.Training_Task where x.UserId == person.PersonId select x;
                if (getTask.Count() > 0)
                {
                    foreach (var item in getTask)
                    {
                        TrainingTaskService.DeleteTaskById(item.TaskId);
                    }
                }
                ///删除考试记录
                var getTestRecode = from x in Funs.DB.Training_TestRecord where x.TestManId == person.PersonId select x;
                if (getTestRecode.Count() > 0)
                {
                    foreach (var item in getTestRecode)
                    {
                        TestRecordService.DeleteTestRecordByTestRecordId(item.TestRecordId);
                    }
                }
                ///删除人员绩效
                //var getPerfomances = from x in Funs.DB.Perfomance_PersonPerfomance where x.PersonId == person.PersonId select x;
                //if (getPerfomances.Count() > 0)
                //{
                //    foreach (var item in getPerfomances)
                //    {
                //        PersonPerfomanceService.DeletePersonPerfomanceById(item.PersonPerfomanceId);
                //    }
                //}
                ///删除人员出入场记录
                BLL.PersonInOutService.DeletePersonInOutByPersonId(person.PersonId);

                ///删除编码表记录
                BLL.CodeRecordsService.DeleteCodeRecordsByDataId(personId);
                Funs.DB.SitePerson_Person.DeleteOnSubmit(person);
                Funs.DB.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据身份证号获取人员信息
        /// </summary>
        /// <param name="identityCard">身份证号</param>
        /// <returns>人员信息</returns>
        public static Model.SitePerson_Person GetPersonByIdentityCard(string projectId, string identityCard)
        {
            if (!string.IsNullOrEmpty(identityCard))
            {
                return Funs.DB.SitePerson_Person.FirstOrDefault(e => e.ProjectId == projectId && e.IdentityCard == identityCard);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 根据身份证号获取人员信息
        /// </summary>
        /// <param name="name">姓名</param>
        /// <returns>人员信息</returns>
        public static Model.SitePerson_Person GetPersonByName(string projectId, string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                return Funs.DB.SitePerson_Person.FirstOrDefault(e => e.ProjectId == projectId && e.PersonName == name);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 保存发卡信息
        /// </summary>
        /// <param name="personId"></param>
        /// <param name="cardNo"></param>
        public static void SaveSendCard(string personId, string cardNo, int personIndex)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                Model.SitePerson_Person card = db.SitePerson_Person.FirstOrDefault(e => e.CardNo == cardNo);
                if (card != null)
                {
                    card.CardNo = null;
                }
                else
                {
                    Model.SitePerson_Person person = db.SitePerson_Person.FirstOrDefault(e => e.PersonId == personId);
                    person.CardNo = cardNo;
                    person.PersonIndex = personIndex;
                    //person.CardNo = sendCardNo;
                }

                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据作业区域获取人员
        /// </summary>
        /// <param name="workAreaId"></param>
        /// <returns></returns>
        public static List<Model.SitePerson_Person> GetPersonListByWorkAreaId(string workAreaId)
        {
            return (from x in Funs.DB.SitePerson_Person where x.WorkAreaId == workAreaId select x).ToList();
        }

        #region 表下拉框
        /// <summary>
        ///  表下拉框
        /// </summary>
        /// <param name="dropName">下拉框名字</param>
        /// <param name="isShowPlease">是否显示请选择</param>
        public static void InitPersonByProjectUnitDropDownList(FineUIPro.DropDownList dropName, string projectId, string unitId, bool isShowPlease)
        {
            dropName.DataValueField = "PersonId";
            dropName.DataTextField = "PersonName";
            dropName.DataSource = GetPersonLitsByprojectIdUnitId(projectId, unitId);
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }

        /// <summary>
        ///  表下拉框
        /// </summary>
        /// <param name="dropName">下拉框名字</param>
        /// <param name="isShowPlease">是否显示请选择</param>
        public static void InitPersonByProjectUnitTeamGroupDropDownList(FineUIPro.DropDownList dropName, string projectId, string unitId, string teamGroupId, bool isShowPlease)
        {
            dropName.DataValueField = "PersonId";
            dropName.DataTextField = "PersonName";
            dropName.DataSource = GetPersonLitsByprojectIdUnitIdTeamGroupId(projectId, unitId, teamGroupId);
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }
        #endregion

        /// <summary>
        /// 获取人员密码
        /// </summary>
        /// <param name="idCard"></param>
        /// <returns></returns>
        public static string GetPersonPassWord(string idCard)
        {
            string passWord = Funs.EncryptionPassword(Const.Password);
            ////现场人员密码
            if (!string.IsNullOrEmpty(idCard))
            {
                if (idCard.Length > 3)
                {
                    passWord = Funs.EncryptionPassword(idCard.Substring(idCard.Length - 4));
                }
                else
                {
                    passWord = Funs.EncryptionPassword(idCard);
                }
            }
            return passWord;
        }
    }
}
