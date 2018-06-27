using Simple_Async_Http_Server.Server.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Async_Http_Server.Server.Routing.Contracts
{
    public interface IServerRouteConfig
    {
        IDictionary<HttpRequestMethod, IDictionary<string, IRoutingContext>> Routes { get; }
    }
}
