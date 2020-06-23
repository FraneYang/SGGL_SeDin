using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace WebAPI.Filter
{
    /// <summary>
    /// 
    /// </summary>
    public class TestPermissionAttribute: ActionFilterAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="actionContext"></param>
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            bool isOk = false;
            actionContext.Request.Headers.TryGetValues("token", out IEnumerable<string> token);
            string strValues = actionContext.ActionDescriptor.ControllerDescriptor.ControllerName + "*" + ((ReflectedHttpActionDescriptor)actionContext.ActionDescriptor).ActionName;
            if (lists.FirstOrDefault(x => x == strValues) != null)
            {
                isOk = true;
            }

            if (!isOk && token != null)
            {
                var getUser = BLL.UserService.GetUserByUserId(token.FirstOrDefault());
                if (getUser != null)
                {
                    isOk = true;
                }
                else
                {
                    var getPerson = BLL.PersonService.GetPersonById(token.FirstOrDefault());
                    if (getPerson != null)
                    {
                        isOk = true;
                    }
                }
            }
           // base.OnActionExecuting(actionContext);
            if (isOk)
            {
                base.OnActionExecuting(actionContext);
            }
            else
            {
                actionContext.Response = actionContext.Request.CreateResponse(System.Net.HttpStatusCode.OK,
                       new { code = "0", message = "您没有权限！" }, actionContext.ControllerContext.Configuration.Formatters.JsonFormatter);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static List<string> lists = new List<string> { "User*postLoginOn" };
    }     
}