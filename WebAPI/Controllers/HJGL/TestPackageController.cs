using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BLL;
using Model;

namespace WebAPI.Controllers
{
    /// <summary>
    ///试压包
    /// </summary>
    public class TestPackageController : ApiController
    {
        #region 获取试压包号
        /// <summary>
        /// 获取试压包号
        /// </summary>
        /// <param name="unitWorkId">单位工程</param>
        /// <param name="isFinish">是否完成</param>
        /// <param name="testPackageNo">试压包号</param>
        /// <returns></returns>
        public Model.ResponeData getTestPackageNo(string unitWorkId, bool isFinish, string testPackageNo)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = APITestPackageService.getTestPackageNo(unitWorkId, isFinish, testPackageNo);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion

        #region 根据试压包ID获取明细
        /// <summary>
        /// 根据试压包ID获取明细
        /// </summary>
        /// <param name="ptp_Id"></param>
        /// <returns></returns>
        public Model.ResponeData GetTestPackageDetail(string ptp_Id)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = APITestPackageService.GetTestPackageDetail(ptp_Id);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion

        #region 获取具备试压条件的试压包提醒
        /// <summary>
        /// 获取具备试压条件的试压包提醒
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public Model.ResponeData GetCanTestPackageWarning(string projectId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = APITestPackageService.GetCanTestPackageWarning(projectId);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }

        #endregion

        #region 获取尾项检查试压包号
        /// <summary>
        /// 获取尾项检查试压包号
        /// </summary>
        /// <param name="unitWorkId">单位工程</param>
        /// <param name="isFinish">是否完成</param>
        /// <param name="testPackageNo">试压包号</param>
        /// <returns></returns>
        public Model.ResponeData getItemEndCheckTestPackageNo(string unitWorkId, string testPackageNo)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = APITestPackageService.getItemEndCheckTestPackageNo(unitWorkId, testPackageNo);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion

        #region
        //
        // 质量巡检 
        // GET: /Draw/
        [HttpGet]
        public ResponseData<List<View_PTP_ItemEndCheckList>> getItemEndCheckList(string itemEndCheckListId, int index, int page)
        {
            ResponseData<List<View_PTP_ItemEndCheckList>> res = new ResponseData<List<View_PTP_ItemEndCheckList>>();

            res.successful = true;
            res.resultValue = BLL.APITestPackageService.getItemEndCheckList(itemEndCheckListId, index, page);
            return res;
        }
        #endregion
    }
}