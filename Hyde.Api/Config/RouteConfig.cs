using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Routing;
using Hyde.Api.Constraint;
using System.Web.Routing;

namespace Hyde.Api.Config
{
    public class RouteConfig
    {
        /// <summary>
        /// 路由配置
        /// </summary>
        /// <param name="config"></param>
        public static void RouteRegister(HttpConfiguration config)
        {
            var routeTables = config.Routes;

            routeTables.MapHttpRoute("DefaultApi",
                 routeTemplate: "{controller}/{action}/{Key}",//api/{controller}/{action}/{Key}
                 defaults: new { Key = RouteParameter.Optional },
                 constraints: new { Key = new OptionalRegExConstraint(@"\d+") });

            
        }
    }
}