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
    /// 法律法规信息
    /// </summary>
    public class LawRegulationListController : ApiController
    {
        #region 根据lawRegulationId获取法律法规信息
        /// <summary>
        /// 根据lawRegulationId获取法律法规信息
        /// </summary>
        /// <param name="lawRegulationId"></param>
        /// <returns></returns>
        public Model.ResponeData getLawRegulationListByLawRegulationId(string lawRegulationId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = from x in new Model.SGGLDB(Funs.ConnString).Law_LawRegulationList
                                   where x.LawRegulationId == lawRegulationId                                   
                                   select new
                                   {
                                       x.LawRegulationId,
                                       x.LawRegulationCode,
                                       x.LawRegulationName,
                                       ApprovalDate=string.Format("{0:yyyy-MM-dd}", x.ApprovalDate) ,
                                       EffectiveDate = string.Format("{0:yyyy-MM-dd}", x.EffectiveDate),
                                       x.Description,
                                       AttachUrl = APIUpLoadFileService.getFileUrl(x.LawRegulationId,x.AttachUrl),
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

        #region 根据lawsRegulationsTypeId获取法律法规
        /// <summary>
        /// 根据lawsRegulationsTypeId获取法律法规
        /// </summary>
        /// <param name="type"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public Model.ResponeData getLawRegulationListByTypeId(string type, int pageIndex)
        {
            var responeData = new Model.ResponeData();
            try
            {
                IQueryable<Model.Law_LawRegulationList> q = from x in new Model.SGGLDB(Funs.ConnString).Law_LawRegulationList
                                                            where x.LawsRegulationsTypeId == type && x.IsPass == true                                                            
                                                            select x;                
                int pageCount = q.Count();
                if (pageCount == 0)
                {
                    responeData.data = new { pageCount, q };
                }
                else
                {
                    var getDataList = from x in q.OrderBy(u => u.LawRegulationCode).Skip(Funs.PageSize * (pageIndex - 1)).Take(Funs.PageSize)
                                      select new
                                      {
                                          x.LawRegulationId,
                                          x.LawRegulationCode,
                                          x.LawRegulationName,
                                          ApprovalDate = string.Format("{0:yyyy-MM-dd}", x.ApprovalDate),
                                          EffectiveDate = string.Format("{0:yyyy-MM-dd}", x.EffectiveDate),
                                          x.Description,
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

        #region 根据lawsRegulationsTypeId获取法律法规-查询
        /// <summary>
        /// 根据lawsRegulationsTypeId获取法律法规-查询
        /// </summary>
        /// <param name="type"></param>
        /// <param name="strParam"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public Model.ResponeData getLawRegulationListByTypeIdQuery(string type, string strParam, int pageIndex)
        {
            var responeData = new Model.ResponeData();
            try
            {
                IQueryable<Model.Law_LawRegulationList> q = from x in new Model.SGGLDB(Funs.ConnString).Law_LawRegulationList
                                                            where x.LawsRegulationsTypeId == type && x.IsPass == true
                                                            select x;
                if (!string.IsNullOrEmpty(strParam))
                {
                    q = q.Where(x => x.LawRegulationName.Contains(strParam));
                }
                int pageCount = q.Count();
                if (pageCount == 0)
                {
                    responeData.data = new { pageCount, q };
                }
                else
                {
                    var getDataList = from x in q.OrderBy(u => u.LawRegulationCode).Skip(Funs.PageSize * (pageIndex - 1)).Take(Funs.PageSize)
                                      select new
                                      {
                                          x.LawRegulationId,
                                          x.LawRegulationCode,
                                          x.LawRegulationName,
                                          ApprovalDate = string.Format("{0:yyyy-MM-dd}", x.ApprovalDate),
                                          EffectiveDate = string.Format("{0:yyyy-MM-dd}", x.EffectiveDate),
                                          x.Description,
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
