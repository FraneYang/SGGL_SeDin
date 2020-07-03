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
    /// 硬度/热处理
    /// </summary>
    public class HotProcessHardController : ApiController
    {
        /// <summary>
        /// 根据单位工程获取热处理委托单号
        /// </summary>
        /// <param name="unitWrokId"></param>
        /// <returns></returns>
        public Model.ResponeData getHotProessTrustNo(string unitWrokId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = APIHotProcessHardService.getHotProessTrustNo(unitWrokId);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }

        /// <summary>
        /// 根据单位工程获取硬度委托单号
        /// </summary>
        /// <param name="unitWrokId"></param>
        /// <returns></returns>
        public Model.ResponeData getHardTrustNo(string unitWrokId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = APIHotProcessHardService.getHardTrustNo(unitWrokId);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }

        /// <summary>
        /// 根据热处理委托单获取委托单明细
        /// </summary>
        /// <param name="hotProessTrustId"></param>
        /// <returns></returns>
        public Model.ResponeData getHotProcessItem(string hotProessTrustId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data =APIHotProcessHardService.getHotProcessItem(hotProessTrustId);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }

        /// <summary>
        /// 根据硬度委托单获取委托单明细
        /// </summary>
        /// <param name="hardTrustId"></param>
        /// <returns></returns>
        public Model.ResponeData getHardTrustItem(string hardTrustId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = APIHotProcessHardService.getHardTrustItem(hardTrustId);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }

    }
}