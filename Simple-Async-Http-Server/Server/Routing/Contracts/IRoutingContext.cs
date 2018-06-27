using Simple_Async_Http_Server.Server.Handlers.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Async_Http_Server.Server.Routing.Contracts
{
    public interface IRoutingContext
    {
        IEnumerable<string> Parameters { get; }

        IRequestHandler RequestHandler { get; }
    }
}
