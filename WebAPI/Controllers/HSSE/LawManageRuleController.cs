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
    public class LawManageRuleController : ApiController
    {
        #region 根据manageRuleId获取管理规定信息
        /// <summary>
        /// 根据manageRuleId获取管理规定信息
        /// </summary>
        /// <param name="manageRuleId"></param>
        /// <returns></returns>
        public Model.ResponeData getManageRuleListByManageRuleId(string manageRuleId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = from x in new Model.SGGLDB(Funs.ConnString).Law_ManageRule
                                   where x.ManageRuleId == manageRuleId && x.IsPass == true
                                   select new
                                   {
                                       x.ManageRuleId,
                                       x.ManageRuleCode,
                                       x.ManageRuleName,
                                       x.SeeFile,
                                       CompileDate = string.Format("{0:yyyy-MM-dd}", x.CompileDate),
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

        #region 根据lawsRegulationsTypeId获取管理规定
        /// <summary>
        /// 根据lawsRegulationsTypeId获取管理规定
        /// </summary>
        /// <param name="type"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public Model.ResponeData getManageRuleListByTypeId(string type, int pageIndex)
        {
            var responeData = new Model.ResponeData();
            try
            {
                IQueryable<Model.Law_ManageRule> q = from x in new Model.SGGLDB(Funs.ConnString).Law_ManageRule
                                                     where x.ManageRuleTypeId == type && x.IsPass == true
                                                     select x;
                int pageCount = q.Count();
                if (pageCount == 0)
                {
                    responeData.data = new { pageCount, q };
                }
                else
                {
                    var getDataList = from x in q.OrderBy(u => u.ManageRuleCode).Skip(Funs.PageSize * (pageIndex - 1)).Take(Funs.PageSize)
                                      select new
                                      {
                                          x.ManageRuleId,
                                          x.ManageRuleCode,
                                          x.ManageRuleName,
                                          x.SeeFile,
                                          CompileDate = string.Format("{0:yyyy-MM-dd}", x.CompileDate),
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

        #region 根据lawsRegulationsTypeId获取管理规定-查询
        /// <summary>
        /// 根据lawsRegulationsTypeId获取管理规定-查询
        /// </summary>
        /// <param name="type"></param>
        /// <param name="strParam"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public Model.ResponeData getManageRuleListByTypeIdQuery(string type, string strParam, int pageIndex)
        {
            var responeData = new Model.ResponeData();
            try
            {
                IQueryable<Model.Law_ManageRule> q = from x in new Model.SGGLDB(Funs.ConnString).Law_ManageRule
                                                     where x.ManageRuleTypeId == type && x.IsPass == true
                                                     select x;
                if (!string.IsNullOrEmpty(strParam))
                {
                    q = q.Where(x => x.ManageRuleName.Contains(strParam));
                }

                int pageCount = q.Count();
                if (pageCount == 0)
                {
                    responeData.data = new { pageCount, q };
                }
                else
                {
                    var getDataList = from x in q.OrderBy(u => u.ManageRuleCode).Skip(Funs.PageSize * (pageIndex - 1)).Take(Funs.PageSize)
                                      select new
                                      {
                                          x.ManageRuleId,
                                          x.ManageRuleCode,
                                          x.ManageRuleName,
                                          x.SeeFile,
                                          CompileDate = string.Format("{0:yyyy-MM-dd}", x.CompileDate),
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
