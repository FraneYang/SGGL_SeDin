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
    /// 规章制度信息
    /// </summary>
    public class LawRulesRegulationsController : ApiController
    {
        #region 根据rulesRegulationsId获取规章制度信息
        /// <summary>
        /// 根据rulesRegulationsId获取规章制度信息
        /// </summary>
        /// <param name="rulesRegulationsId"></param>
        /// <returns></returns>
        public Model.ResponeData getRulesRegulationsListByRulesRegulationsId(string rulesRegulationsId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = from x in new Model.SGGLDB(Funs.ConnString).Law_RulesRegulations
                                   where x.RulesRegulationsId == rulesRegulationsId
                                   select new
                                   {
                                       x.RulesRegulationsId,
                                       x.RulesRegulationsCode,
                                       x.RulesRegulationsName,
                                       x.ApplicableScope,
                                       CustomDate = string.Format("{0:yyyy-MM-dd}", x.CustomDate),
                                       x.Remark,
                                       AttachUrl = x.AttachUrl.Replace('\\', '/')
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

        #region 根据lawsRegulationsTypeId获取规章制度
        /// <summary>
        /// 根据lawsRegulationsTypeId获取规章制度
        /// </summary>
        /// <param name="type"></param>
        /// <param name="pageIndex">分页</param>
        /// <returns></returns>
        public Model.ResponeData getRulesRegulationsListByTypeId(string type, int pageIndex)
        {
            var responeData = new Model.ResponeData();
            try
            {
                IQueryable<Model.Law_RulesRegulations> q = from x in new Model.SGGLDB(Funs.ConnString).Law_RulesRegulations
                                                            where x.RulesRegulationsTypeId == type && x.IsPass == true
                                                            select x;
                int pageCount = q.Count();
                if (pageCount == 0)
                {
                    responeData.data = new { pageCount, q };
                }
                else
                {
                    var getDataList = from x in q.OrderBy(u => u.RulesRegulationsCode).Skip(Funs.PageSize * (pageIndex - 1)).Take(Funs.PageSize)
                                      select new
                                      {
                                          x.RulesRegulationsId,
                                          x.RulesRegulationsCode,
                                          x.RulesRegulationsName,
                                          x.ApplicableScope,
                                          CustomDate = string.Format("{0:yyyy-MM-dd}", x.CustomDate),                                        
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

        #region 根据lawsRegulationsTypeId获取规章制度-查询
        /// <summary>
        /// 根据lawsRegulationsTypeId获取规章制度-查询
        /// </summary>
        /// <param name="type"></param>
        /// <param name="strParam"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public Model.ResponeData getRulesRegulationsListByTypeIdQuery(string type, string strParam, int pageIndex)
        {
            var responeData = new Model.ResponeData();
            try
            {
                IQueryable<Model.Law_RulesRegulations> q = from x in new Model.SGGLDB(Funs.ConnString).Law_RulesRegulations
                                                           where x.RulesRegulationsTypeId == type && x.IsPass == true
                                                           select x;
                if (!string.IsNullOrEmpty(strParam))
                {
                    q = q.Where(x => x.RulesRegulationsName.Contains(strParam));
                }
                int pageCount = q.Count();
                if (pageCount == 0)
                {
                    responeData.data = new { pageCount, q };
                }
                else
                {
                    var getDataList = from x in q.OrderBy(u => u.RulesRegulationsCode).Skip(Funs.PageSize * (pageIndex - 1)).Take(Funs.PageSize)
                                      select new
                                      {
                                          x.RulesRegulationsId,
                                          x.RulesRegulationsCode,
                                          x.RulesRegulationsName,
                                          x.ApplicableScope,
                                          CustomDate = string.Format("{0:yyyy-MM-dd}", x.CustomDate),
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
