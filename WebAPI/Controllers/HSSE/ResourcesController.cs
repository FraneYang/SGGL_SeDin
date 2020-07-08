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
    /// 公共资源
    /// </summary>
    public class ResourcesController : ApiController
    {
        #region 集团培训教材
        #region 根据父级类型ID获取培训教材类型
        /// <summary>
        /// 根据父级类型ID获取培训教材类型
        /// </summary>
        /// <param name="supTypeId"></param>
        /// <param name="pageIndex">分页</param>
        /// <returns></returns>
        public Model.ResponeData getTrainingListBySupTrainingId(string supTypeId,int pageIndex)
        {
            var responeData = new Model.ResponeData();
            try
            {
                var getDataList = APIResourcesService.getTrainingListBySupTrainingId(supTypeId);
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

        #region 根据培训教材类型id获取培训教材列表
        /// <summary>
        /// 根据培训教材类型id获取培训教材列表
        /// </summary>
        /// <param name="trainingId"></param>
        /// <param name="pageIndex">分页</param>
        /// <returns></returns>
        public Model.ResponeData getTrainingItemListByTrainingId(string trainingId, int pageIndex)
        {
            var responeData = new Model.ResponeData();
            try
            {
                var getDataList = APIResourcesService.getTrainingItemListByTrainingId(trainingId);
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

        #region 根据培训教材主键获取培训教材详细信息
        /// <summary>
        /// 根据培训教材主键获取培训教材详细信息
        /// </summary>
        /// <param name="trainingItemId"></param>
        /// <returns></returns>
        public Model.ResponeData getTrainingItemByTrainingItemId(string trainingItemId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = APIResourcesService.getTrainingItemByTrainingItemId(trainingItemId);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion
        #endregion

        #region 公司培训教材
        #region 根据父级类型ID获取公司培训教材类型
        /// <summary>
        /// 根据父级类型ID获取公司培训教材类型
        /// </summary>
        /// <param name="supTypeId"></param>
        /// <param name="pageIndex">分页</param>
        /// <returns></returns>
        public Model.ResponeData getCompanyTrainingListBySupTrainingId(string supTypeId, int pageIndex)
        {
            var responeData = new Model.ResponeData();
            try
            {
                var getDataList = APIResourcesService.getCompanyTrainingListBySupTrainingId(supTypeId);
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

        #region 根据培训教材类型id获取公司培训教材列表
        /// <summary>
        /// 根据培训教材类型id获取公司培训教材列表
        /// </summary>
        /// <param name="trainingId"></param>
        /// <param name="pageIndex">分页</param>
        /// <returns></returns>
        public Model.ResponeData getCompanyTrainingItemListByTrainingId(string trainingId, int pageIndex)
        {
            var responeData = new Model.ResponeData();
            try
            {
                var getDataList = APIResourcesService.getCompanyTrainingItemListByTrainingId(trainingId);
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

        #region 根据培训教材主键获取公司培训教材详细信息
        /// <summary>
        /// 根据培训教材主键获取公司培训教材详细信息
        /// </summary>
        /// <param name="trainingItemId"></param>
        /// <returns></returns>
        public Model.ResponeData getCompanyTrainingItemByTrainingItemId(string trainingItemId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = APIResourcesService.getCompanyTrainingItemByTrainingItemId(trainingItemId);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion
        #endregion

        #region 公司制度
        #region  获取公司制度列表
        /// <summary>
        /// 获取公司制度列表
        /// </summary>
        /// <param name="pageIndex">分页</param>
        /// <returns></returns>
        public Model.ResponeData getCompanySafetyInstitutionList( int pageIndex)
        {
            var responeData = new Model.ResponeData();
            try
            {
                var getDataList = APIResourcesService.getCompanySafetyInstitutionList();
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

        #region 获取公司制度详细信息
        /// <summary>
        /// 获取公司制度详细信息
        /// </summary>
        /// <param name="safetyInstitutionId"></param>
        /// <returns></returns>
        public Model.ResponeData getCompanySafetyInstitutionInfo(string safetyInstitutionId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = APIResourcesService.getCompanySafetyInstitutionInfo(safetyInstitutionId);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion
        #endregion

        #region 考试试题
        #region 根据父级类型ID获取考试试题类型
        /// <summary>
        /// 根据父级类型ID获取考试试题类型
        /// </summary>
        /// <param name="supTypeId"></param>
        /// <param name="pageIndex">分页</param>
        /// <returns></returns>
        public Model.ResponeData getTestTrainingListBySupTrainingId(string supTypeId, int pageIndex)
        {
            var responeData = new Model.ResponeData();
            try
            {
                var getDataList = APIResourcesService.getTestTrainingListBySupTrainingId(supTypeId);
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

        #region 根据培训教材类型id获取考试试题列表
        /// <summary>
        /// 根据培训教材类型id获取考试试题列表
        /// </summary>
        /// <param name="trainingId"></param>
        /// <param name="pageIndex">分页</param>
        /// <returns></returns>
        public Model.ResponeData getTestTrainingItemListByTrainingId(string trainingId, int pageIndex)
        {
            var responeData = new Model.ResponeData();
            try
            {
                var getDataList = APIResourcesService.getTestTrainingItemListByTrainingId(trainingId);
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

        #region 根据培训教材主键获取考试试题详细信息
        /// <summary>
        /// 根据培训教材主键获取考试试题详细信息
        /// </summary>
        /// <param name="trainingItemId"></param>
        /// <returns></returns>
        public Model.ResponeData getTestTrainingItemByTrainingItemId(string trainingItemId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = APIResourcesService.getTestTrainingItemByTrainingItemId(trainingItemId);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion
        #endregion

        #region 根据signManageId获取标识标牌信息
        /// <summary>
        /// 根据signManageId获取标识标牌信息
        /// </summary>
        /// <param name="signManageId"></param>
        /// <returns></returns>
        public Model.ResponeData getSignManageInfo(string signManageId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = from x in new Model.SGGLDB(Funs.ConnString).Resources_SignManage
                                   where x.SignManageId == signManageId
                                   select new
                                   {
                                       x.SignManageId,
                                       x.SignName,
                                       x.SignCode,
                                       x.SignLen,
                                       x.SignWide,
                                       x.SignHigh,
                                       x.SignThick,
                                       x.Material,
                                       x.SignArea,
                                       x.SignType,
                                       x.SignImage,
                                       AttachUrl = new Model.SGGLDB(Funs.ConnString).AttachFile.First(z=>z.ToKeyId ==x.SignManageId).AttachUrl.Replace('\\', '/')
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

        #region 根据SignTypeId获取标识标牌信息-查询
        /// <summary>
        /// 根据SignTypeId获取标识标牌信息-查询
        /// </summary>
        /// <param name="type"></param>
        /// <param name="strParam"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public Model.ResponeData getSignManageListQuery(string type, string strParam, int pageIndex)
        {
            var responeData = new Model.ResponeData();
            try
            {
                IQueryable<Model.Resources_SignManage> q = from x in new Model.SGGLDB(Funs.ConnString).Resources_SignManage
                                                            where x.SignType == type
                                                            select x;
                if (!string.IsNullOrEmpty(strParam))
                {
                    q = q.Where(x => x.SignName.Contains(strParam) || x.SignArea.Contains(strParam));
                }
                int pageCount = q.Count();
                if (pageCount == 0)
                {
                    responeData.data = new { pageCount, q };
                }
                else
                {
                    var getDataList = from x in q.OrderBy(u => u.SignCode).Skip(Funs.PageSize * (pageIndex - 1)).Take(Funs.PageSize)
                                      select new
                                      {
                                          x.SignManageId,
                                          x.SignName,
                                          x.SignCode,
                                          x.SignArea,
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

        #region 事故案例
        #region 根据父级类型ID获取事故案例类型
        /// <summary>
        /// 根据父级类型ID获取事故案例类型
        /// </summary>
        /// <param name="supTypeId"></param>
        /// <param name="pageIndex">分页</param>
        /// <returns></returns>
        public Model.ResponeData getAccidentCaseListBySupAccidentCaseId(string supTypeId, int pageIndex)
        {
            var responeData = new Model.ResponeData();
            try
            {
                var getDataList = APIResourcesService.getAccidentCaseListBySupAccidentCaseId(supTypeId);
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

        #region 根据事故案例类型id获取事故案例列表
        /// <summary>
        /// 根据培训教材类型id获取公司培训教材列表
        /// </summary>
        /// <param name="accidentCaseId"></param>
        /// <param name="pageIndex">分页</param>
        /// <returns></returns>
        public Model.ResponeData getAccidentCaseItemListById(string accidentCaseId, int pageIndex)
        {
            var responeData = new Model.ResponeData();
            try
            {
                var getDataList = APIResourcesService.getAccidentCaseItemListById(accidentCaseId);
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

        #region 根据事故案例主键获取事故案例详细信息
        /// <summary>
        /// 根据事故案例主键获取事故案例详细信息
        /// </summary>
        /// <param name="accidentCaseItemId"></param>
        /// <returns></returns>
        public Model.ResponeData getAccidentCaseItemById(string accidentCaseItemId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = APIResourcesService.getAccidentCaseItemById(accidentCaseItemId);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion
        #endregion

        #region 检查要点
        #region 根据父级类型ID获取检查要点类型
        /// <summary>
        /// 根据父级类型ID获取检查要点类型
        /// </summary>
        /// <param name="supTypeId"></param>
        /// <param name="checkType">1-checkType;2-专项检查;3-综合检查</param>
        /// <param name="pageIndex">分页</param>
        /// <returns></returns>
        public Model.ResponeData getCheckItemSetListBySupCheckItemId(string supTypeId, string checkType, int pageIndex)
        {
            var responeData = new Model.ResponeData();
            try
            {
                var getDataList = APIResourcesService.getCheckItemSetListBySupCheckItemId(supTypeId, checkType);
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

        #region 根据培训教材类型id获取检查要点列表
        /// <summary>
        /// 根据检查要点类型id获取检查要点列表
        /// </summary>
        /// <param name="checkItemSetId"></param>
        /// <param name="pageIndex">分页</param>
        /// <returns></returns>
        public Model.ResponeData getCheckItemSetItemListBycheckItemSetId(string checkItemSetId, int pageIndex)
        {
            var responeData = new Model.ResponeData();
            try
            {
                var getDataList = APIResourcesService.getCheckItemSetItemListBycheckItemSetId(checkItemSetId);
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

        #endregion
    }
}
