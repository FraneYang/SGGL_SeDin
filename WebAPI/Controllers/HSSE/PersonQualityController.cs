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
    public class PersonQualityController : ApiController
    {
        #region 根据ID获取人员资质信息
        /// <summary>
        ///  根据ID获取人员资质信息
        /// </summary>
        /// <param name="type">人员资质类型（1-特种作业；2-安管人员；3-特种设备作业人员）</param>
        /// <param name="dataId">主键ID</param>
        /// <returns></returns>
        public Model.ResponeData getPersonQualityInfo(string type, string dataId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = APIPersonQualityService.getPersonQualityInfo(type, dataId);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion

        #region 获取人员资质各状态数
        /// <summary>
        /// 获取人员资质各状态数
        /// </summary>
        /// <param name="projectId">项目ID</param>
        /// <param name="unitId">单位ID</param>
        /// <param name="qualityType">人员资质类型（1-特种作业；2-安管人员；3-特种设备作业人员）</param>
        /// <param name="unitIdQ">查询单位ID</param>
        /// <returns>人员资质数量</returns>
        public Model.ResponeData getPersonQualityCount(string projectId, string unitId, string unitIdQ, string qualityType)
        {
            var responeData = new Model.ResponeData();
            try
            {
                Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
                int tatalCount = 0, count1 = 0, count2 = 0, count3 = 0, count4 = 0;
                bool isSub = !(unitId == Const.UnitId_SEDIN);
                if (qualityType == "1")
                {
                    var getPersons = from x in db.SitePerson_Person
                                     join y in db.Base_WorkPost on x.WorkPostId equals y.WorkPostId
                                     where x.ProjectId == projectId  && y.PostType == Const.PostType_2 && x.IsUsed == true && (!x.OutTime.HasValue || x.OutTime > DateTime.Now)
                                     select x;
                    if (isSub)
                    {
                        getPersons = getPersons.Where(x => x.UnitId == unitId);
                    }
                    if (!string.IsNullOrEmpty(unitIdQ))
                    {
                        getPersons = getPersons.Where(x => x.UnitId == unitIdQ);
                    }
                    /////总数
                    tatalCount = getPersons.Count();
                    var getPersonQuality = from x in db.QualityAudit_PersonQuality
                                           join y in getPersons on x.PersonId equals y.PersonId 
                                           select x;
                    ////无证
                    int noC = getPersons.Count() - getPersonQuality.Count();
                    ////待维护
                    count1 = noC +getPersonQuality.Where(x => x.States == null || x.States == Const.State_0 || x.LimitDate < DateTime.Now.AddMonths(1)).Count(); 
                    ////待审核
                    count2 = getPersonQuality.Where(x => x.States == Const.State_1).Count();
                    //// 已审核
                    count3 = getPersonQuality.Where(x => x.States ==Const.State_2 && x.LimitDate >=DateTime.Now.AddMonths(1)).Count();
                    //// 打回
                    count4 = getPersonQuality.Where(x => x.States == Const.State_R).Count();
                }
                else if (qualityType == "2")
                {
                    var getPersons = from x in db.SitePerson_Person
                                     join y in db.Base_WorkPost on x.WorkPostId equals y.WorkPostId
                                     where x.ProjectId == projectId && y.IsHsse ==true && x.IsUsed == true && (!x.OutTime.HasValue || x.OutTime > DateTime.Now)
                                     select x;
                    if (isSub)
                    {
                        getPersons = getPersons.Where(x => x.UnitId == unitId);
                    }
                    if (!string.IsNullOrEmpty(unitIdQ))
                    {
                        getPersons = getPersons.Where(x => x.UnitId == unitIdQ);
                    }
                    /////总数
                    tatalCount = getPersons.Count();
                    var getPersonQuality = from x in db.QualityAudit_SafePersonQuality
                                           join y in getPersons on x.PersonId equals y.PersonId
                                           select x;
                    ////无证
                    int noC = getPersons.Count() - getPersonQuality.Count();
                    ////待维护
                    count1 = noC + getPersonQuality.Where(x => x.States == null || x.States == Const.State_0 || x.LimitDate < DateTime.Now.AddMonths(1)).Count();
                    ////待审核
                    count2 = getPersonQuality.Where(x => x.States == Const.State_1).Count();
                    //// 已审核
                    count3 = getPersonQuality.Where(x => x.States == Const.State_2 && x.LimitDate >= DateTime.Now.AddMonths(1)).Count();
                    //// 打回
                    count4 = getPersonQuality.Where(x => x.States == Const.State_R).Count();
                }
                else if (qualityType == "3")
                {
                    var getPersons = from x in db.SitePerson_Person
                                     join y in db.Base_WorkPost on x.WorkPostId equals y.WorkPostId
                                     where x.ProjectId == projectId  && y.PostType == Const.PostType_5 && x.IsUsed == true && (!x.OutTime.HasValue || x.OutTime >DateTime.Now)
                                     select x;
                    if (isSub)
                    {
                        getPersons = getPersons.Where(x => x.UnitId == unitId);
                    }
                    if (!string.IsNullOrEmpty(unitIdQ))
                    {
                        getPersons = getPersons.Where(x => x.UnitId == unitIdQ);
                    }
                    /////总数
                    tatalCount = getPersons.Count();
                    var getPersonQuality = from x in db.QualityAudit_EquipmentPersonQuality
                                           join y in getPersons on x.PersonId equals y.PersonId
                                           select x;
                    ////无证
                    int noC = getPersons.Count() - getPersonQuality.Count();
                    ////待维护
                    count1 = noC + getPersonQuality.Where(x => x.States == null || x.States == Const.State_0 || x.LimitDate < DateTime.Now.AddMonths(1)).Count();
                    ////待审核
                    count2 = getPersonQuality.Where(x => x.States == Const.State_1).Count();
                    //// 已审核
                    count3 = getPersonQuality.Where(x => x.States == Const.State_2 && x.LimitDate >= DateTime.Now.AddMonths(1)).Count();
                    //// 打回
                    count4 = getPersonQuality.Where(x => x.States == Const.State_R).Count();
                }
                responeData.data = new { tatalCount, count1, count2, count3, count4 };
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
        /// <param name="qualityType">资质类型</param> 
        /// <param name="states">0-待提交；1-待审核；2-已审核；-1打回</param>
        /// <param name="unitIdQ">查询单位ID</param>
        /// <param name="pageIndex">页码</param>
        /// <returns></returns>
        public Model.ResponeData getPersonQualityList(string projectId, string unitId, string qualityType, string states,string unitIdQ, int pageIndex)
        {
            var responeData = new Model.ResponeData();
            try
            {
                var getDataList = APIPersonQualityService.getPersonQualityList(projectId, unitId, qualityType, states, unitIdQ);
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

        #region 保存 人员资质信息
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
                APIPersonQualityService.SavePersonQuality(personQuality);
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