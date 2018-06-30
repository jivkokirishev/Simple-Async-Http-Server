using Simple_Async_Http_Server.Server.Common;
using Simple_Async_Http_Server.Server.Enums;
using Simple_Async_Http_Server.Server.Http.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Async_Http_Server.Server.Http.Response
{
    public abstract class HttpResponse : IHttpResponse
    {
        protected HttpResponse()
        {
            this.Headers = new HttpHeaderCollection();
            this.Cookies = new HttpCookieCollection();
        }

        public IHttpHeaderCollection Headers { get; }

        public IHttpCookieCollection Cookies { get; }

        public HttpStatusCode StatusCode { get; protected set; }

        private string StatusMessage
        {
            get
            {
                 return this.StatusCode.ToString();
            }
        }

        public override string ToString()
        {
            var response = new StringBuilder();
            response.AppendLine($"HTTP/1.1 {(int)this.StatusCode} {this.StatusMessage}");
            response.AppendLine(this.Headers.ToString());
            response.AppendLine();

            return response.ToString();
        }
    }
}
