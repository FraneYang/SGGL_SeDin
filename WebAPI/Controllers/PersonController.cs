using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BLL;

namespace WebAPI.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class PersonController : ApiController
    {
        #region 根据personid获取人员信息
        /// <summary>
        /// 根据personid获取人员信息
        /// </summary>
        /// <param name="personId"></param>
        /// <returns></returns>
        public Model.ResponeData getPersonByPersonId(string personId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = APIPersonService.getPersonByPersonId(personId);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion

        #region 根据identityCard获取人员信息
        /// <summary>
        /// 根据identityCard获取人员信息
        /// </summary>
        /// <param name="identityCard"></param>
        /// <returns></returns>
        public Model.ResponeData getPersonByIdentityCard(string identityCard)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = APIPersonService.getPersonByPersonId(identityCard);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion

        #region 根据projectId、unitid获取人员信息
        /// <summary>
        /// 根据projectId、unitid获取人员信息
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="unitId"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public Model.ResponeData getPersonByProjectIdUnitId(string projectId, string unitId, int pageIndex)
        {
            var responeData = new Model.ResponeData();
            try
            {
                var getDataList = APIPersonService.getPersonByProjectIdUnitId(projectId, unitId);
                int pageCount = getDataList.Count;
                if (pageCount > 0 && pageIndex > 0)
                {
                    getDataList = getDataList.Skip(Funs.PageSize * (pageIndex - 1)).Take(Funs.PageSize).ToList();
                }
                responeData.data = new { pageCount, getDataList };
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }
            return responeData;
        }
        #endregion
        
        #region 获取在岗、离岗、待审人员数量
        /// <summary>
        /// 获取在岗、离岗、待审人员列表
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="unitId"></param>
        /// <param name="states"></param>
        /// <param name="strUnitId"></param>
        /// <param name="strWorkPostId"></param>
        /// <param name="strParam"></param>
        /// <returns></returns>
        public Model.ResponeData getPersonStatesCount(string projectId, string unitId, string states, string strUnitId, string strWorkPostId, string strParam)
        {
            var responeData = new Model.ResponeData();
            try
            {
                var getViews = from x in new Model.SGGLDB(Funs.ConnString).SitePerson_Person
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
                int tatalCount = getViews.Count();
                //在审
                int count0 = getViews.Where(x => x.IsUsed == false && !x.AuditorDate.HasValue).Count();
                //在岗
                int count1 = getViews.Where(x => x.IsUsed == true && x.InTime <= DateTime.Now && (!x.OutTime.HasValue || x.OutTime >= DateTime.Now)).Count();
                //离岗
                int count2 = getViews.Where(x => x.IsUsed == true && x.OutTime <= DateTime.Now).Count();
                //打回
                int count3 = getViews.Where(x => x.IsUsed == false && x.AuditorDate.HasValue).Count();
                responeData.data = new { tatalCount, count0, count1, count2, count3 };
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }
            return responeData;
        }
        #endregion

        #region 获取在岗、离岗、待审人员列表
        /// <summary>
        /// 获取在岗、离岗、待审人员列表
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="unitId">当前人单位ID</param>
        /// <param name="states">0待审1在岗2离岗</param>
        /// <param name="strUnitId">查询单位</param>
        /// <param name="strWorkPostId">查询岗位</param>
        ///  <param name="strParam">查询条件</param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public Model.ResponeData getPersonListByProjectIdStates(string projectId, string unitId, string states, string strUnitId, string strWorkPostId, string strParam, int pageIndex)
        {
            var responeData = new Model.ResponeData();
            try
            {
                var getDataList = APIPersonService.getPersonListByProjectIdStates(projectId, unitId, states, strUnitId, strWorkPostId, strParam);
                int pageCount = getDataList.Count;
                if (pageCount > 0 && pageIndex > 0)
                {
                    getDataList = getDataList.Skip(Funs.PageSize * (pageIndex - 1)).Take(Funs.PageSize).ToList();
                }
                responeData.data = new { pageCount, getDataList };
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }
            return responeData;
        }
        #endregion

        #region 根据identityCard获取人员培训考试信息
        /// <summary>
        /// 根据identityCard获取人员培训考试信息
        /// </summary>
        /// <param name="identityCard"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public Model.ResponeData getPersonTestRecoedByIdentityCard(string identityCard, string projectId = null)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = APIPersonService.getPersonTestRecoedByIdentityCard(identityCard, projectId);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion

        #region 根据identityCard获取人员资质信息
        /// <summary>
        /// 根据identityCard获取人员资质信息
        /// </summary>
        /// <param name="identityCard"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public Model.ResponeData getPersonQualityByIdentityCard(string identityCard, string projectId = null)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = APIPersonService.getPersonQualityByIdentityCard(identityCard, projectId);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion

        #region 根据projectId、unitid获取特岗人员资质各状态数
        /// <summary>
        /// 根据projectId、unitid获取特岗人员资质各状态数
        /// </summary>
        /// <param name="projectId">项目ID</param>
        /// <param name="unitId">单位ID</param>
        /// <returns>人员资质数量</returns>
        public Model.ResponeData getPersonQualityCount(string projectId, string unitId)
        {
            var responeData = new Model.ResponeData();
            try
            {                
                var getDataList = new Model.SGGLDB(Funs.ConnString).View_QualityAudit_PersonQuality.Where(x => x.ProjectId == projectId && x.CertificateId != null);
                if (ProjectUnitService.GetProjectUnitTypeByProjectIdUnitId(projectId, unitId))
                {
                    getDataList = getDataList.Where(x => x.UnitId == unitId);
                }
                //总数
                int tatalCount = getDataList.Count();
                //过期
                int count1 = getDataList.Where(x => x.LimitDate < DateTime.Now).Count();
                //即将过期
                int count2 = getDataList.Where(x => x.LimitDate >= DateTime.Now && x.LimitDate < DateTime.Now.AddMonths(1)).Count();

                responeData.data = new { tatalCount, count1, count2 };
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion

        #region 根据projectId、unitid获取特岗人员资质信息
        /// <summary>
        /// 根据projectId、unitid获取特岗人员资质信息
        /// </summary>
        /// <param name="projectId">项目ID</param>
        /// <param name="unitId">单位ID</param>
        /// <param name="type">数据类型0-已过期；1-即将过期;2-无证;3-待审核；4-已审核；-1打回</param>
        /// <param name="pageIndex">页码</param>
        /// <returns></returns>
        public Model.ResponeData getPersonQualityByProjectIdUnitId(string projectId, string unitId, string type, int pageIndex)
        {
            var responeData = new Model.ResponeData();
            try
            {
                var getDataList = APIPersonService.getPersonQualityByProjectIdUnitId(projectId, unitId, type);
                int pageCount = getDataList.Count();
                if (pageCount > 0 && pageIndex > 0)
                {
                    getDataList = getDataList.Skip(Funs.PageSize * (pageIndex - 1)).Take(Funs.PageSize).ToList();
                }
                responeData.data = new { pageCount, getDataList };
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }
            return responeData;
        }
        #endregion

        #region 保存 人员资质信息 QualityAudit_PersonQuality
        /// <summary>
        /// 保存Meeting
        /// </summary>
        /// <param name="personQuality">人员资质信息</param>
        /// <returns></returns>
        [HttpPost]
        public Model.ResponeData SavePersonQuality([FromBody] Model.PersonQualityItem personQuality)
        {
            var responeData = new Model.ResponeData();
            try
            {
                APIPersonService.SavePersonQuality(personQuality);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion

        #region 根据项目\单位\岗位\培训类型获取项目培训\考试人员信息
        /// <summary>
        /// 根据项目\单位\岗位\培训类型获取项目培训\考试人员信息
        /// </summary>
        /// <param name="projectId">项目ID</param>
        /// <param name="unitIds">培训单位ID</param>
        /// <param name="workPostIds">培训岗位ID(可为空)</param>
        /// <param name="trainTypeId">培训类型ID(可为空)</param>
        /// <param name="pageIndex">分页</param>
        /// <returns></returns>
        public Model.ResponeData getTrainingPersonListByTrainTypeId(string projectId, string unitIds, string workPostIds, string trainTypeId, int pageIndex)
        {
            var responeData = new Model.ResponeData();
            try
            {
                var getDataList = APIPersonService.getTrainingPersonListByTrainTypeId(projectId, unitIds, workPostIds, trainTypeId).OrderBy(x => x.UnitName).ThenBy(x => x.ProjectName).ToList();
                int pageCount = getDataList.Count;
                if (pageCount > 0 && pageIndex > 0)
                {
                    getDataList = getDataList.OrderBy(u => u.UnitName).ThenBy(u => u.PersonName).Skip(Funs.PageSize * (pageIndex - 1)).Take(Funs.PageSize).ToList(); ////200 ->Funs.PageSize
                }
                responeData.data = new { pageCount, getDataList };
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }
            return responeData;
        }
        #endregion

        #region 保存项目人员信息
        /// <summary>
        /// 保存项目人员信息
        /// </summary>
        /// <param name="person">人员信息</param>
        /// <returns></returns>
        [HttpPost]
        public Model.ResponeData SaveSitePerson([FromBody] Model.PersonItem person)
        {
            var responeData = new Model.ResponeData();
            try
            {
                if (person != null && !string.IsNullOrEmpty(person.IdentityCard))
                {
                    var getPerson = new Model.SGGLDB(Funs.ConnString).SitePerson_Person.FirstOrDefault(x => x.IdentityCard == person.IdentityCard && x.ProjectId == person.ProjectId);
                    if (getPerson != null && getPerson.PersonId != person.PersonId)
                    {
                        responeData.code = 2;
                        responeData.message = "人员身份证号码已存在！";
                    }
                    else
                    {
                        APIPersonService.SaveSitePerson(person);
                    }
                }
                else
                {
                    responeData.code = 2;
                    responeData.message = "人员信息有误！";
                }
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion

        #region 根据personid人员打回
        /// <summary>
        /// 根据personid人员打回
        /// </summary>
        /// <param name="personId">人员ID</param>
        /// <param name="userId">审核人id</param>
        /// <returns></returns>
        public Model.ResponeData getReUserPersonByPersonId(string personId, string userId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                APIPersonService.getReUserPersonByPersonId(personId, userId);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion

        #region 更新人员更新附件
        /// <summary>
        /// 更新人员更新附件
        /// </summary>
        /// <param name="person">人员信息</param>
        /// <returns></returns>
        [HttpPost]
        public Model.ResponeData SaveSitePersonAttachment([FromBody] Model.PersonItem person)
        {
            var responeData = new Model.ResponeData();
            try
            {
                if (person != null)
                {
                    APIPersonService.SaveSitePersonAttachment(person);
                }
                else
                {
                    responeData.code = 2;
                    responeData.message = "人员附件信息有误！";
                }
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion

        #region 人员离场
        /// <summary>
        /// 人员离场
        /// </summary>
        /// <param name="personId"></param>
        /// <returns></returns>
        public Model.ResponeData getPersonOut(string personId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                APIPersonService.getPersonOut(personId);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }
            return responeData;
        }
        #endregion

        #region 插入人员出入场记录
        /// <summary>
        /// 获取人员出入场记录
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="idCard"></param>
        /// <param name="isIn"></param>
        /// <param name="changeTime"></param>
        /// <returns></returns>
        public Model.ResponeData getPersonInOut(string projectId, string idCard, int isIn,DateTime changeTime)
        {
            var responeData = new Model.ResponeData();
            try
            {
                APIPersonService.getPersonInOut(projectId, idCard, isIn, changeTime);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }
            return responeData;
        }
        #endregion

        #region 获取发卡人员
        /// <summary>
        /// 获取发卡人员
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public Model.ResponeData getPersonDataExchange(string projectId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = from x in new Model.SGGLDB(Funs.ConnString).SitePerson_Person
                                   join y in new Model.SGGLDB(Funs.ConnString).Base_Unit on x.UnitId equals y.UnitId
                                   where x.ProjectId == projectId
                                   && !x.ExchangeTime.HasValue
                                   && (!x.OutTime.HasValue || x.OutTime > DateTime.Now) && x.InTime.HasValue && x.InTime < DateTime.Now
                                   && x.IsUsed == true && x.CardNo != null
                                   select new
                                   {
                                       x.PersonId,
                                       x.PersonName,
                                       x.CardNo,
                                       x.IdentityCard,
                                       x.UnitId,
                                       y.UnitCode,
                                       y.UnitName,
                                       x.Sex,
                                       new Model.SGGLDB(Funs.ConnString).Base_WorkPost.First(z => z.WorkPostId == x.WorkPostId).WorkPostName,
                                       x.Telephone,
                                       x.Address,
                                       x.ExchangeTime,
                                       x.ExchangeTime2,
                                       PhotoUrl = (x.PhotoUrl == null || x.PhotoUrl =="") ? x.IDCardUrl : x.PhotoUrl,
                                   };

            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }
            return responeData;
        }
        #endregion

        #region 获取离场人员
        /// <summary>
        /// 获取离场人员
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public Model.ResponeData getPersonOutDataExchange(string projectId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = from x in new Model.SGGLDB(Funs.ConnString).SitePerson_Person
                                   where x.ProjectId == projectId && x.InTime.HasValue && ((x.IsUsed == true && !x.OutTime.HasValue) || x.OutTime.HasValue)
                                    && x.InTime < DateTime.Now && x.CardNo != null && !x.ExchangeTime2.HasValue && x.ExchangeTime.HasValue
                                   select new
                                   {
                                       x.PersonId,
                                       x.PersonName,
                                       x.CardNo,
                                       x.IdentityCard,
                                       OutTime = x.OutTime == null ? DateTime.Now.AddYears(10) : x.OutTime,
                                   };
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }
            return responeData;
        }
        #endregion

        #region 更新人员数据交换时间
        /// <summary>
        /// 更新人员数据交换时间
        /// </summary>
        /// <param name="personId">人员ID</param>
        /// <param name="type">交换类型</param>
        /// <returns></returns>
        public Model.ResponeData getUpdatePersonExchangeTime(string personId, string type)
        {
            var responeData = new Model.ResponeData();
            try
            {
                APIPersonService.getUpdatePersonExchangeTime(personId, type);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }
            return responeData;
        }
        #endregion

        #region 获取人员信息出入场记录
        /// <summary>
        /// 获取人员信息出入场记录
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="unitId">当前人单位ID</param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="pageIndex">页码</param>
        /// <returns></returns>
        public Model.ResponeData getPersonInOutList(string projectId, string unitId, string startTime, string endTime, int pageIndex)
        {
            var responeData = new Model.ResponeData();
            try
            {
                var getDataList = APIPersonService.getPersonInOutList(projectId, unitId, startTime, endTime);
                int pageCount = getDataList.Count();
                if (pageCount > 0 && pageIndex > 0)
                {
                    getDataList = getDataList.Skip(Funs.PageSize * (pageIndex - 1)).Take(Funs.PageSize).ToList();
                }
                responeData.data = new { pageCount, getDataList };
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }
            return responeData;
        }
        #endregion

        #region 获取人员信息出入场记录-查询
        /// <summary>
        /// 获取人员信息出入场记录
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="unitId"></param>
        ///  <param name="userUnitId">当前人单位ID</param>
        /// <param name="workPostId">岗位</param>
        /// <param name="strParam">查询条件</param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="pageIndex">页码</param>
        /// <returns></returns>
        public Model.ResponeData getPersonInOutList(string projectId, string userUnitId, string workPostId, string strParam, string startTime, string endTime, int pageIndex, string unitId = null)
        {
            var responeData = new Model.ResponeData();
            try
            {
                var getDataList = APIPersonService.getPersonInOutList(projectId, userUnitId, unitId, workPostId, strParam, startTime, endTime);
                int pageCount = getDataList.Count();
                if (pageCount > 0 && pageIndex > 0)
                {
                    getDataList = getDataList.Skip(Funs.PageSize * (pageIndex - 1)).Take(Funs.PageSize).ToList();
                }
                responeData.data = new { pageCount, getDataList };
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }
            return responeData;
        }
        #endregion

        #region 根据人员ID获取个人出入场记录
        /// <summary>
        /// 根据人员ID获取个人出入场记录
        /// </summary> 
        /// <param name="personId"></param>   
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="pageIndex">页码</param>
        /// <returns></returns>
        public Model.ResponeData getPersonInOutListByPersonId(string personId, string startTime, string endTime, int pageIndex)
        {
            var responeData = new Model.ResponeData();
            try
            {
                var getDataList = APIPersonService.getPersonInOutListByPersonId(personId, startTime, endTime);
                int pageCount = getDataList.Count();
                if (pageCount > 0 && pageIndex > 0)
                {
                    getDataList = getDataList.Skip(Funs.PageSize * (pageIndex - 1)).Take(Funs.PageSize).ToList();
                }
                responeData.data = new { pageCount, getDataList };
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }
            return responeData;
        }
        #endregion
    }
}