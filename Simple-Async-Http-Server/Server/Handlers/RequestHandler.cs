using Simple_Async_Http_Server.Server.Handlers.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple_Async_Http_Server.Server.Http.Contracts;

namespace Simple_Async_Http_Server.Server.Handlers
{
    public abstract class RequestHandler : IRequestHandler
    {
        private readonly Func<IHttpRequest, IHttpResponse> handlerFunc;

        public RequestHandler(Func<IHttpRequest, IHttpResponse> handlerFunc)
        {
            this.handlerFunc = handlerFunc;
        }
        public IHttpResponse Handle(IHttpContext httpContext)
        {
            IHttpResponse response = this.handlerFunc.Invoke(httpContext.Request);
            response.Headers.Add(new Http.HttpHeader("Content-type", "text/html"));

            return response;
        }
    }
}
