using BLL;
using System;
using System.Linq;
using System.Web.Http;

namespace WebAPI.Controllers
{
    /// <summary>
    /// 专项检查
    /// </summary>
    public class CheckSpecialController : ApiController
    {
        #region 获取专项检查详细信息
        /// <summary>
        ///  根据主键ID获取专项检查
        /// </summary>
        /// <param name="CheckSpecialId"></param>
        /// <returns></returns>
        public Model.ResponeData getCheckSpecialById(string CheckSpecialId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                 responeData.data = APICheckSpecialService.getCheckSpecialById(CheckSpecialId);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion

        #region 获取专项检查列表信息
        /// <summary>
        /// 获取专项检查列表信息
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="states"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public Model.ResponeData getCheckSpecialList(string projectId, string states, int pageIndex)
        {
            var responeData = new Model.ResponeData();
            try
            {
                var getDataList = APICheckSpecialService.getCheckSpecialList(projectId, states);
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

        #region 保存专项检查 Check_CheckSpecial
        /// <summary>
        /// 保存专项检查 Check_CheckSpecial
        /// </summary>
        /// <param name="newItem">专项检查</param>
        /// <returns></returns>
        [HttpPost]
        public Model.ResponeData SaveCheckSpecial([FromBody] Model.CheckSpecialItem newItem)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.message = APICheckSpecialService.SaveCheckSpecial(newItem);               
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion

        #region  获取专项检查明细项详细
        /// <summary>
        ///  获取专项检查明细项详细
        /// </summary>
        /// <param name="checkSpecialDetailId"></param>
        /// <returns></returns>
        public Model.ResponeData getCheckSpecialDetailById(string checkSpecialDetailId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = APICheckSpecialService.getCheckSpecialDetailById(checkSpecialDetailId);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion

        #region 保存专项检查明细项 Check_CheckSpecialDetail
        /// <summary>
        /// 保存专项检查明细项 Check_CheckSpecialDetail
        /// </summary>
        /// <param name="newItem">专项检查</param>
        /// <returns></returns>
        [HttpPost]
        public Model.ResponeData SaveCheckSpecialDetail([FromBody] Model.CheckSpecialDetailItem newItem)
        {
            var responeData = new Model.ResponeData();
            try
            {
                APICheckSpecialService.SaveCheckSpecialDetail(newItem);
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
