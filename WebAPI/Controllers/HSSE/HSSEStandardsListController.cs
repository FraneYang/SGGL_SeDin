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
    /// 标准规范信息
    /// </summary>
    public class HSSEStandardsListController : ApiController
    {
        #region 根据standardId获取标准规范信息
        /// <summary>
        /// 根据standardId获取标准规范信息
        /// </summary>
        /// <param name="standardId"></param>
        /// <returns></returns>
        public Model.ResponeData getHSSEStandardsListByStandardId(string standardId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = from x in new Model.SGGLDB(Funs.ConnString).Law_HSSEStandardsList
                                   where x.StandardId == standardId
                                   select new
                                   {
                                       x.StandardId,
                                       x.StandardGrade,
                                       x.StandardNo,
                                       x.StandardName,
                                       AttachUrl = APIUpLoadFileService.getFileUrl(x.StandardId, x.AttachUrl),
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

        #region 根据lawsRegulationsTypeId获取标准规范
        /// <summary>
        /// 根据lawsRegulationsTypeId获取标准规范
        /// </summary>
        /// <param name="type"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public Model.ResponeData getHSSEStandardsListByTypeId(string type, int pageIndex)
        {
            var responeData = new Model.ResponeData();
            try
            {
                IQueryable<Model.Law_HSSEStandardsList> q = from x in new Model.SGGLDB(Funs.ConnString).Law_HSSEStandardsList
                                                            where x.TypeId == type && x.IsPass == true
                                                            select x;
                int pageCount = q.Count();
                if (pageCount == 0)
                {
                    responeData.data = new { pageCount, q };
                }
                else
                {
                    var getDataList = from x in q.OrderBy(u => u.StandardNo).Skip(Funs.PageSize * (pageIndex - 1)).Take(Funs.PageSize)
                                      select new
                                      {
                                          x.StandardId,
                                          x.StandardGrade,
                                          x.StandardNo,
                                          x.StandardName,
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

        #region 根据lawsRegulationsTypeId获取标准规范-查询
        /// <summary>
        /// 根据lawsRegulationsTypeId获取标准规范
        /// </summary>
        /// <param name="type"></param>
        /// <param name="strParam"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public Model.ResponeData getHSSEStandardsListByTypeIdQuery(string type, string strParam, int pageIndex)
        {
            var responeData = new Model.ResponeData();
            try
            {
                IQueryable<Model.Law_HSSEStandardsList> q = from x in new Model.SGGLDB(Funs.ConnString).Law_HSSEStandardsList
                                                            where x.TypeId == type && x.IsPass == true
                                                            select x;
                if (!string.IsNullOrEmpty(strParam))
                {
                    q = q.Where(x => x.StandardName.Contains(strParam));
                }
                int pageCount = q.Count();
                if (pageCount == 0)
                {
                    responeData.data = new { pageCount, q };
                }
                else
                {
                    var getDataList = from x in q.OrderBy(u => u.StandardNo).Skip(Funs.PageSize * (pageIndex - 1)).Take(Funs.PageSize)
                                      select new
                                      {
                                          x.StandardId,
                                          x.StandardGrade,
                                          x.StandardNo,
                                          x.StandardName,
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
    }
}
