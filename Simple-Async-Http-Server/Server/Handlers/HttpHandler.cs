using Simple_Async_Http_Server.Server.Handlers.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple_Async_Http_Server.Server.Http.Contracts;
using Simple_Async_Http_Server.Server.Routing.Contracts;
using Simple_Async_Http_Server.Server.Common;
using System.Text.RegularExpressions;
using Simple_Async_Http_Server.Server.Http.Response;

namespace Simple_Async_Http_Server.Server.Handlers
{
    public class HttpHandler : IRequestHandler
    {
        private readonly IServerRouteConfig routeConfig;

        public HttpHandler(IServerRouteConfig routeConfig)
        {
            CommonValidator.ThrowIfNull(routeConfig, nameof(routeConfig));

            this.routeConfig = routeConfig;
        }

        public IHttpResponse Handle(IHttpContext httpContext)
        {
            CommonValidator.ThrowIfNull(httpContext, nameof(httpContext));

            var method = httpContext.Request.RequestMethod;
            foreach (var route in this.routeConfig.Routes[method])
            {
                var pattern = route.Key;
                var routeRegex = new Regex(pattern);
                var match = routeRegex.Match(httpContext.Request.Path);

                if (match.Success)
                {
                    foreach (var parameter in route.Value.Parameters)
                    {
                        var paramValue = match.Groups[parameter].Value;
                        httpContext.Request.AddUrlParameter(parameter, paramValue);
                    }

                    return route.Value.RequestHandler.Handle(httpContext);
                }
            }

            return new NotFoundResponse();
        }
    }
}
