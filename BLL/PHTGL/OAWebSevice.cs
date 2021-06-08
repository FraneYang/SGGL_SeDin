using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.PHTGL
{
    class OAWebSevice
    {
        void sed(OAWebJson webJson)
        {
             OAWebService.OfsTodoDataWebServicePortTypeClient OAWeb = new OAWebService.OfsTodoDataWebServicePortTypeClient();
             string strjson = JsonConvert.SerializeObject(webJson);
             var returnjson=  OAWeb.receiveTodoRequestByJson(strjson);
         }


    }
    public class OAWebJson
    {
        /// <summary>
        /// 异构系统标识
        /// </summary>
        public string syscode { get; set; }
        /// <summary>
        /// 流程任务id
        /// </summary>
        public string flowid { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string requestname { get; set; }
        /// <summary>
        /// 流程类型名称
        /// </summary>
        public string workflowname { get; set; }
        /// <summary>
        /// 步骤名称（节点名称）
        /// </summary>
        public string nodename { get; set; }
        /// <summary>
        /// PC地址
        /// </summary>
        public string pcurl { get; set; }
        /// <summary>
        /// APP地址
        /// </summary>
        public string appurl { get; set; }
        /// <summary>
        /// 创建人（原值)
        /// </summary>
        public string creator { get; set; }
        /// <summary>
        /// 创建日期时间
        /// </summary>
        public string createdatetime { get; set; }
        /// <summary>
        /// 接收人（原值）
        /// </summary>
        public string receiver { get; set; }
        /// <summary>
        /// 接收日期时间
        /// </summary>
        public string receivedatetime { get; set; }
    }
}
