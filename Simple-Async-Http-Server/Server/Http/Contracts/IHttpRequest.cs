using Simple_Async_Http_Server.Server.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Async_Http_Server.Server.Http.Contracts
{
    public interface IHttpRequest
    {
        IDictionary<string, string> FormData { get; }

        IHttpHeaderCollection Headers { get; }

        IHttpCookieCollection Cookies { get; }

        string Path { get; }

        IDictionary<string, string> QueryParameters { get; }

        HttpRequestMethod RequestMethod { get; }

        string Url { get; }

        IDictionary<string, string> UrlParameters { get; }

        void AddUrlParameter(string key, string value);
    }
}
