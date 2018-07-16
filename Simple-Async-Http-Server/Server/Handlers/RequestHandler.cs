using Simple_Async_Http_Server.Server.Handlers.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple_Async_Http_Server.Server.Http.Contracts;
using Simple_Async_Http_Server.Server.Common;
using Simple_Async_Http_Server.Server.Http;

namespace Simple_Async_Http_Server.Server.Handlers
{
    public abstract class RequestHandler : IRequestHandler
    {
        private readonly Func<IHttpRequest, IHttpResponse> handlerFunc;

        public RequestHandler(Func<IHttpRequest, IHttpResponse> handlerFunc)
        {
            CommonValidator.ThrowIfNull(handlerFunc, nameof(handlerFunc));

            this.handlerFunc = handlerFunc;
        }
        public IHttpResponse Handle(IHttpContext httpContext)
        {
            CommonValidator.ThrowIfNull(httpContext, nameof(httpContext));

            string sessId = null;

            if (!httpContext.Request.Cookies.ContainsKey("SID"))
            {
                sessId = Guid.NewGuid().ToString();
                 httpContext.Request.Session = SessionStore.GetOrAdd(sessId);
            }

            IHttpResponse response = this.handlerFunc.Invoke(httpContext.Request);

            if (!string.IsNullOrEmpty(sessId))
            {
                response.Headers.Add(new HttpHeader("Set-Cookie", $"SID={sessId}; HttpOnly; path=/"));
            }

            response.Headers.Add(new HttpHeader("Content-type", "text/html"));

            foreach (var cookie in response.Cookies)
            {
                response.Headers.Add(new HttpHeader("Set-Cookie", cookie.ToString()));
            }

            return response;
        }
    }
}
