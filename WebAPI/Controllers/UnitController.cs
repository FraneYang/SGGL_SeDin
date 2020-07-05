using BLL;
using System;
using System.Web.Http;

namespace WebAPI.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class UnitController : ApiController
    {
        #region 根据UnitId获取单位信息
        /// <summary>
        /// 根据UnitId获取单位信息
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>    
        public Model.ResponeData getUnitByUnitId(string unitId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = APIUnitService.getUnitByUnitId(unitId);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion

        #region 获取所有单位
        /// <summary>
        /// 获取所有单位
        /// </summary>
        /// <returns></returns>             
        public Model.ResponeData getUnitLists()
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = APIUnitService.getUnitLists();
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion

        #region 根据projectId、unitType获取单位信息（总包1;施工分包2;监理3;业主4;其他5）
        /// <summary>
        /// 根据projectId、unitType获取单位信息（总包1;施工分包2;监理3;业主4;其他5）
        /// </summary>
        /// <param name="projectId">项目ID</param>
        /// <param name="unitType">类型（null 所有单位）</param>
        /// <returns></returns>
        public Model.ResponeData getUnitByProjectIdUnitType(string projectId, string unitType)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = APIUnitService.getUnitByProjectIdUnitType(projectId, unitType);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }

        /// <summary>
        /// 根据projectId、unitType获取单位信息（总包1;施工分包2;监理3;业主4;其他5）
        /// </summary>
        /// <param name="projectId">项目ID</param>
        /// <param name="strParam">查询条件</param>
        /// <param name="unitType">类型（null 所有单位）</param>
        /// <returns></returns>
        public Model.ResponeData getUnitByProjectIdUnitTypeQuery(string projectId, string strParam, string unitType)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = APIUnitService.getUnitByProjectIdUnitTypeQuery(projectId, strParam,unitType);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion    

        #region 根据subUnitQualityId获取分包商资质信息
        /// <summary>
        /// 根据subUnitQualityId获取分包商资质信息
        /// </summary>
        /// <param name="subUnitQualityId"></param>
        /// <returns></returns>
        public Model.ResponeData getSubUnitQualityBySubUnitQualityId(string subUnitQualityId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = APIUnitService.getSubUnitQualityBySubUnitQualityId(subUnitQualityId);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion

        #region 根据projectId获取施工分包商资质各状态数
        /// <summary>
        /// 根据projectId获取施工分包商资质各状态数
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public Model.ResponeData getSubUnitQualityCount(string projectId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                //var getDataList = Funs.DB.View_QualityAudit_SubUnitQuality.Where(x => x.ProjectId == projectId);                
                ////// 总数
                //int tatalCount = getDataList.Count();
                ////// 过期
                //int count1 = getDataList.Where(x => x.BL_EnableDate < DateTime.Now || x.C_EnableDate < DateTime.Now || x.QL_EnableDate < DateTime.Now
                //|| x.H_EnableDate < DateTime.Now || x.SL_EnableDate < DateTime.Now).Count();
                //////即将过期
                //int count2 = getDataList.Where(x => (x.BL_EnableDate >= DateTime.Now && x.BL_EnableDate < DateTime.Now.AddMonths(1)) 
                //|| (x.C_EnableDate >= DateTime.Now && x.C_EnableDate < DateTime.Now.AddMonths(1))
                //|| (x.QL_EnableDate >= DateTime.Now && x.QL_EnableDate < DateTime.Now.AddMonths(1))
                //|| (x.H_EnableDate >= DateTime.Now && x.H_EnableDate < DateTime.Now.AddMonths(1))
                //|| (x.SL_EnableDate >= DateTime.Now && x.SL_EnableDate < DateTime.Now.AddMonths(1))).Count();

                //responeData.data = new { tatalCount, count1, count2 };
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion

        #region 根据projectId获取施工分包商资质信息
        /// <summary>
        /// 根据projectId获取施工分包商资质信息
        /// </summary>
        /// <param name="projectId">项目ID</param>
        /// <param name="type">数据类型0-已过期；1-即将过期</param>
        /// <param name="pageIndex">页码</param>
        /// <returns></returns>
        public Model.ResponeData getSubUnitQualityByProjectId(string projectId, string type, int pageIndex)
        {
            var responeData = new Model.ResponeData();
            try
            {
                //IQueryable<Model.View_QualityAudit_SubUnitQuality> q = from x in Funs.DB.View_QualityAudit_SubUnitQuality
                //                                                         where x.ProjectId == projectId
                //                                                         select x;               
                //if (type == "0")
                //{
                //    q = q.Where(x => x.BL_EnableDate < DateTime.Now || x.C_EnableDate < DateTime.Now || x.QL_EnableDate < DateTime.Now
                //                            || x.H_EnableDate < DateTime.Now || x.SL_EnableDate < DateTime.Now);
                //}
                //else if (type == "1")
                //{
                //    q = q.Where(x => (x.BL_EnableDate >= DateTime.Now && x.BL_EnableDate < DateTime.Now.AddMonths(1))
                //    || (x.C_EnableDate >= DateTime.Now && x.C_EnableDate < DateTime.Now.AddMonths(1))
                //    || (x.QL_EnableDate >= DateTime.Now && x.QL_EnableDate < DateTime.Now.AddMonths(1))
                //    || (x.H_EnableDate >= DateTime.Now && x.H_EnableDate < DateTime.Now.AddMonths(1))
                //    || (x.SL_EnableDate >= DateTime.Now && x.SL_EnableDate < DateTime.Now.AddMonths(1)));
                //}

                //int pageCount = q.Count();
                //if (pageCount == 0)
                //{
                //    responeData.data = new { pageCount, q };
                //}
                //else
                //{
                //    var getDataList = from x in q.OrderBy(u => u.UnitCode).Skip(Funs.PageSize * (pageIndex - 1)).Take(Funs.PageSize)
                //                      select new
                //                      {
                //                          x.UnitId,
                //                          x.UnitName,
                //                          x.ProjectId,
                //                          x.SubUnitQualityId,
                //                          x.SubUnitQualityName,
                //                          x.BusinessLicense,
                //                          BL_EnableDate = string.Format("{0:yyyy-MM-dd}", x.BL_EnableDate),
                //                          BL_ScanUrl = x.BL_ScanUrl.Replace("\\", "/"),
                //                          x.Certificate,
                //                          C_EnableDate = string.Format("{0:yyyy-MM-dd}", x.C_EnableDate),
                //                          C_ScanUrl = x.C_ScanUrl.Replace("\\", "/"),
                //                          x.QualityLicense,
                //                          QL_EnableDate = string.Format("{0:yyyy-MM-dd}", x.QL_EnableDate),
                //                          QL_ScanUrl = x.QL_ScanUrl.Replace("\\", "/"),
                //                          x.HSELicense,
                //                          H_EnableDate = string.Format("{0:yyyy-MM-dd}", x.H_EnableDate),
                //                          H_ScanUrl = x.H_ScanUrl.Replace("\\", "/"),
                //                          x.SecurityLicense,
                //                          SL_EnableDate = string.Format("{0:yyyy-MM-dd}", x.SL_EnableDate),
                //                          SL_ScanUrl = x.SL_ScanUrl.Replace("\\", "/")
                //                      };
                //    responeData.data = new { pageCount, getDataList };
                //}
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
