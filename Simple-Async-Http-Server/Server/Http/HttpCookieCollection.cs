using Simple_Async_Http_Server.Server.Http.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using Simple_Async_Http_Server.Server.Common;

namespace Simple_Async_Http_Server.Server.Http
{
    class HttpCookieCollection : IHttpCookieCollection
    {
        private readonly IDictionary<string, HttpCookie> cookies;

        public HttpCookieCollection()
        {
            this.cookies = new Dictionary<string, HttpCookie>();
        }

        public void Add(HttpCookie cookie)
        {
            CommonValidator.ThrowIfNull(cookies, nameof(cookie));

            this.cookies[cookie.Key] = cookie;
        }

        public bool ContainsKey(string key)
        {
            CommonValidator.ThrowIfNullOrEmpty(key, nameof(key));

            if (this.cookies.ContainsKey(key))
            {
                return true;
            }

            return false;
        }

        public IEnumerator<HttpCookie> GetEnumerator()
        {
            return this.cookies.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.cookies.Values.GetEnumerator();
        }

        public HttpCookie GetCookie(string key)
        {
            CommonValidator.ThrowIfNullOrEmpty(key, nameof(key));

            if (this.cookies.ContainsKey(key))
            {
                throw new ArgumentException($"GetCookie: {key} parameter was not found in the dictionary!", nameof(key));
            }

            var cookie = this.cookies[key];

            return cookie;
        }
    }
}
