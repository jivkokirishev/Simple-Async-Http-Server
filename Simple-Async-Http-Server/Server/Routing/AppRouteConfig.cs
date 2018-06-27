using Simple_Async_Http_Server.Server.Routing.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple_Async_Http_Server.Server.Enums;
using Simple_Async_Http_Server.Server.Handlers;
using Simple_Async_Http_Server.Server.Handlers.Contracts;
using System.Collections.ObjectModel;

namespace Simple_Async_Http_Server.Server.Routing
{
    public class AppRouteConfig : IAppRouteConfig
    {
        private readonly Dictionary<HttpRequestMethod, IDictionary<string, IRequestHandler>> routes;

        public AppRouteConfig()
        {
            this.routes = new Dictionary<HttpRequestMethod, IDictionary<string, IRequestHandler>>();

            var methods = Enum.GetValues(typeof(HttpRequestMethod)).Cast<HttpRequestMethod>();
            foreach (var method in methods)
            {
                this.routes[method] = new Dictionary<string, IRequestHandler>();
            }
        }

        public IReadOnlyDictionary<HttpRequestMethod, IDictionary<string, IRequestHandler>> Routes
        {
            get
            {
                return this.routes;
            }
        }

        public void AddRoute(string route, RequestHandler httpHandler)
        {
            var objectType = httpHandler.GetType().ToString().ToLower();

            if (objectType.Contains("get"))
            {
                this.routes[HttpRequestMethod.GET].Add(route, httpHandler);
            }
            else if (objectType.Contains("post"))
            {
                this.routes[HttpRequestMethod.POST].Add(route, httpHandler);
            }
            else
            {
                throw new InvalidOperationException($"Invalid http handler. The handler object is '{objectType}'.");
            }
        }
    }
}
