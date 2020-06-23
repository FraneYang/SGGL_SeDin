using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using WebAPI.Filter;

namespace WebAPI
{
    /// <summary>
    /// 配置
    /// </summary>
    public static class WebApiConfig
    {
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="config"></param>
        public static void Register(HttpConfiguration config)
        {
            // Web API 配置和服务
            GlobalConfiguration.Configuration.Formatters.XmlFormatter.SupportedMediaTypes.Clear();
            // Web API 路由
            config.MapHttpAttributeRoutes();
            // 
            config.Filters.Add(new TestPermissionAttribute());
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
