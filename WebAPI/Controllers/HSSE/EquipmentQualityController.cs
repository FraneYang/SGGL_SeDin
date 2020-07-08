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
    ///  机具设备资质
    /// </summary>
    public class EquipmentQualityController : ApiController
    {
        #region 根据equipmentQualityId获取机具设备资质信息
        /// <summary>
        /// 根据equipmentQualityId获取机具设备资质信息
        /// </summary>
        /// <param name="equipmentQualityId"></param>
        /// <returns></returns>
        public Model.ResponeData getEquipmentQualityByEquipmentQualityId(string equipmentQualityId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = APIEquipmentQualityService.getEquipmentQualityByEquipmentQualityIdFactoryCode(equipmentQualityId);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion

        #region 根据factoryCode获取机具设备资质信息
        /// <summary>
        /// 根据factoryCode获取机具设备资质信息
        /// </summary>
        /// <param name="factoryCode"></param>
        /// <returns></returns>
        public Model.ResponeData getEquipmentQualityByFactoryCode(string factoryCode)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = APIEquipmentQualityService.getEquipmentQualityByEquipmentQualityIdFactoryCode(factoryCode);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion

        #region 根据projectId、unitid获取机具设备资质信息
        /// <summary>
        /// 根据projectId、unitid获取机具设备资质信息
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="unitId"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public Model.ResponeData getEquipmentQualityByProjectIdUnitId(string projectId, string unitId, int pageIndex)
        {
            var responeData = new Model.ResponeData();
            try
            {
                var getDataList = APIEquipmentQualityService.getEquipmentQualityList(projectId, unitId, null);
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

        #region 根据projectId、unitid获取机具设备资质信息-查询
        /// <summary>
        /// 根据projectId、unitid获取机具设备资质信息
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="unitId"></param>
        /// <param name="pageIndex"></param>
        ///  <param name="strParam">查询条件</param>
        /// <returns></returns>
        public Model.ResponeData getEquipmentQualityByProjectIdUnitIdQuery(string projectId, string unitId, string strParam, int pageIndex)
        {
            var responeData = new Model.ResponeData();
            try
            {
                var getDataList = APIEquipmentQualityService.getEquipmentQualityList(projectId, unitId, strParam);
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

        #region 根据projectId、unitid获取特岗机具设备资质资质各状态数
        /// <summary>
        /// 根据projectId、unitid获取特岗机具设备资质资质各状态数
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public Model.ResponeData getEquipmentQualityCount(string projectId, string unitId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                var getDataList = new Model.SGGLDB(Funs.ConnString).QualityAudit_EquipmentQuality.Where(x => x.ProjectId == projectId);
                if (ProjectUnitService.GetProjectUnitTypeByProjectIdUnitId(projectId, unitId))
                {
                    getDataList = getDataList.Where(x => x.UnitId == unitId);
                }
                ////总数
                int tatalCount = getDataList.Count();
                ////过期
                int count1 = getDataList.Where(x => x.LimitDate < DateTime.Now).Count();
                ////即将过期
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

        #region 根据projectId、unitid获取特岗机具设备资质资质信息
        /// <summary>
        /// 根据projectId、unitid获取特岗机具设备资质资质信息
        /// </summary>
        /// <param name="projectId">项目ID</param>
        /// <param name="unitId">单位ID</param>
        /// <param name="type">数据类型0-已过期；1-即将过期</param>
        /// <param name="pageIndex">分页</param>
        /// <returns></returns>
        public Model.ResponeData getEquipmentQualityByProjectIdUnitId(string projectId, string unitId, string type, int pageIndex)
        {
            var responeData = new Model.ResponeData();
            try
            {               
                IQueryable<Model.View_QualityAudit_EquipmentQuality> q = from x in new Model.SGGLDB(Funs.ConnString).View_QualityAudit_EquipmentQuality
                                                                         where x.ProjectId == projectId 
                                                                         select x;
                if (ProjectUnitService.GetProjectUnitTypeByProjectIdUnitId(projectId, unitId))
                {
                    q = q.Where(x => x.UnitId == unitId);
                }
                if (type == "0")
                {
                    q = q.Where(x => x.LimitDate < DateTime.Now);
                }
                else if (type == "1")
                {
                    q = q.Where(x => x.LimitDate >= DateTime.Now && x.LimitDate < DateTime.Now.AddMonths(1));
                }

                int pageCount = q.Count();
                if (pageCount == 0)
                {
                    responeData.data = new { pageCount, q};
                }
                else
                {
                    var getDataList = from x in q.OrderBy(u => u.LimitDate).Skip(Funs.PageSize * (pageIndex - 1)).Take(Funs.PageSize)
                                      select new 
                                      {
                                          x.EquipmentQualityId,
                                          x.ProjectId,
                                          x.EquipmentQualityCode,
                                          x.UnitId,
                                          x.UnitName,
                                          x.SpecialEquipmentId,
                                          x.SpecialEquipmentName,
                                          x.EquipmentQualityName,
                                          x.SizeModel,
                                          x.FactoryCode,
                                          x.CertificateCode,
                                          CheckDate = string.Format("{0:yyyy-MM-dd}", x.CheckDate),
                                          LimitDate = string.Format("{0:yyyy-MM-dd}", x.LimitDate),
                                          InDate = string.Format("{0:yyyy-MM-dd}", x.InDate),
                                          OutDate = string.Format("{0:yyyy-MM-dd}", x.OutDate),
                                          x.ApprovalPerson,
                                          x.CarNum,
                                          x.Remark,
                                          x.CompileMan,
                                          x.CompileManName,
                                          CompileDate = string.Format("{0:yyyy-MM-dd}", x.CompileDate),
                                          AttachUrl = x.AttachUrl.Replace('\\', '/')
                                      };
                    responeData.data = new { pageCount, getDataList };
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

        #region 保存QualityAudit_EquipmentQuality
        /// <summary>
        /// 保存QualityAudit_EquipmentQuality
        /// </summary>
        /// <param name="newitem">机具设备资质</param>
        /// <returns></returns>
        [HttpPost]
        public Model.ResponeData SaveEquipmentQuality([FromBody] Model.EquipmentQualityItem newitem)
        {
            var responeData = new Model.ResponeData();
            try
            {
                APIEquipmentQualityService.SaveEquipmentQuality(newitem);
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
