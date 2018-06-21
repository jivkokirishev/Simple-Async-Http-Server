using Simple_Async_Http_Server.Server.Common;
using Simple_Async_Http_Server.Server.Http.Contracts;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Async_Http_Server.Server.Http
{
    public class HttpHeaderCollection : IHttpHeaderCollection
    {
        private readonly IDictionary<string, HttpHeader> headers;

        public HttpHeaderCollection()
        {
            this.headers = new Dictionary<string, HttpHeader>();
        }

        public void Add(HttpHeader header)
        {
            CommonValidator.ThrowIfNull(headers, nameof(header));

            this.headers[header.Key] = header;
        }

        public bool ContainsKey(string key)
        {
            CommonValidator.ThrowIfNullOrEmpty(key, nameof(key));

            if (this.headers.ContainsKey(key))
            {
                return true;
            }

            return false;
        }

        public HttpHeader GetHeader(string key)
        {
            CommonValidator.ThrowIfNullOrEmpty(key, nameof(key));

            if (this.headers.ContainsKey(key))
            {
                throw new ArgumentException($"{key} parameter was not found in the dictionary!", nameof(key));
            }

            var header = this.headers[key];

            return header;
        }

        public override string ToString()
        {
            return string.Join(Environment.NewLine, this.headers);
        }
    }
}
