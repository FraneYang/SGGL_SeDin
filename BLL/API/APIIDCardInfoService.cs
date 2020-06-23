using Newtonsoft.Json.Linq;
using System;
using System.Linq;

namespace BLL
{
    /// <summary>
    /// 获取身份证信息
    /// </summary>
    public static class APIIDCardInfoService
    {
        /// <summary>
        ///  获取身份证信息
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string ReadIDCardInfo(string url)
        {
            string access_token = string.Empty;// APICommonService.getaccess_token();
            return APIGetHttpService.Http("https://api.weixin.qq.com/cv/ocr/idcard?type=photo&img_url=" + url + "&access_token=" + access_token, "POST");
        }
    }
}
