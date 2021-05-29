using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebAPI.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class HTGLPersonController : ApiController
    {
        /// <summary>
        /// 根据人员信息添加用户
        /// </summary>
        /// <param name="PersonjsonData">人员信息（json字符串）</param>
        /// <returns></returns>
        [HttpPost]
         public Model.ResponeData AddHTGLPerson([FromBody]Model.Person PersonjsonData)
        {
            var responeData = new Model.ResponeData();
            try
            {
                var Data = APIHTGLPersonService.SavePerson(PersonjsonData);
                responeData.data = Data.data;
                if (Data.Message != "" && Data.Message != null)
                {
                    responeData.code = 0;
                    responeData.message = Data.Message;
                    ErrLogInfo.WriteLog(Newtonsoft.Json.JsonConvert.SerializeObject(responeData));
                }
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
                ErrLogInfo.WriteLog(Newtonsoft.Json.JsonConvert.SerializeObject(responeData));

            }

            return responeData;
        }

        /// <summary>
        /// 根据人员信息添加项目用户
        /// </summary>
        /// <param name="PersonjsonData"> 人员信息（json字符串）</param>
        /// <returns></returns>
        [HttpPost]
        public Model.ResponeData AddHTGLPerson_Pro([FromBody]Model.Pro_Person PersonjsonData)
        {
            var responeData = new Model.ResponeData();
            try
            {
                var Data = APIHTGLPersonService.SavePro_Person(PersonjsonData);
                responeData.data = Data.data;
                if (Data.Message !=""&&Data .Message!=null)
                {
                    responeData.code = 0;
                    responeData.message = Data.Message;
                    ErrLogInfo.WriteLog(Newtonsoft.Json.JsonConvert.SerializeObject(responeData));
                }
 
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
                ErrLogInfo.WriteLog(Newtonsoft.Json.JsonConvert.SerializeObject(responeData));

            }

            return responeData;
        }
    }
}
