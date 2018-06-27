using Simple_Async_Http_Server.Server.Routing.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple_Async_Http_Server.Server.Handlers.Contracts;
using Simple_Async_Http_Server.Server.Common;

namespace Simple_Async_Http_Server.Server.Routing
{
    public class RoutingContext : IRoutingContext
    {
        public RoutingContext(IEnumerable<string> parameters, IRequestHandler requestHandler)
        {
            CommonValidator.ThrowIfNull(parameters, nameof(parameters));
            CommonValidator.ThrowIfNull(requestHandler, nameof(requestHandler));

            this.Parameters = parameters;
            this.RequestHandler = requestHandler;
        }

        public IEnumerable<string> Parameters { get; set; }

        public IRequestHandler RequestHandler { get; set; }
    }
}
