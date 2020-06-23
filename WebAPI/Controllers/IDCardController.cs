using BLL;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Web.Http;

namespace WebAPI.Controllers
{
    /// <summary>
    ///  读取身份证信息
    /// </summary>
    public class IDCardController : ApiController
    {
        #region  读取身份证信息
        /// <summary>
        ///  读取身份证信息
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public Model.ResponeData getIDCardInfo(string url)
        {
            var responeData = new Model.ResponeData();
            try
            {               
                responeData.data = APIIDCardInfoService.ReadIDCardInfo(url);
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
