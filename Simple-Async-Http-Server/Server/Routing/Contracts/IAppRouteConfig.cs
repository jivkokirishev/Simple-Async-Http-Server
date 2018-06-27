using Simple_Async_Http_Server.Server.Enums;
using Simple_Async_Http_Server.Server.Handlers;
using Simple_Async_Http_Server.Server.Handlers.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Async_Http_Server.Server.Routing.Contracts
{
    public interface IAppRouteConfig
    {
        IReadOnlyDictionary<HttpRequestMethod, IDictionary<string, IRequestHandler>> Routes { get; }

        void AddRoute(string route, RequestHandler httpHandler);
    }
}
