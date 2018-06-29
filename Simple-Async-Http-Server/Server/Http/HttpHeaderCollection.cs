using Simple_Async_Http_Server.Server.Common;
using Simple_Async_Http_Server.Server.Http.Contracts;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Simple_Async_Http_Server.Server.Http
{
    public class HttpHeaderCollection : IHttpHeaderCollection
    {
        private readonly IDictionary<string, ICollection<HttpHeader>> headers;

        public HttpHeaderCollection()
        {
            this.headers = new Dictionary<string, ICollection<HttpHeader>>();
        }

        public void Add(HttpHeader header)
        {
            CommonValidator.ThrowIfNull(headers, nameof(header));

            if (!this.headers.ContainsKey(header.Key))
            {
                this.headers[header.Key] = new List<HttpHeader>();
            }

            this.headers[header.Key].Add(header);
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

        public IEnumerator<ICollection<HttpHeader>> GetEnumerator()
        {
            return this.headers.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.headers.Values.GetEnumerator();
        }

        public ICollection<HttpHeader> GetHeader(string key)
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
            var result = new StringBuilder();

            foreach (var header in this.headers)
            {
                var key = header.Key;

                foreach (var val in header.Value)
                {
                    result.AppendLine($"{key}: {val.Value}");
                }
            }

            return result.ToString();
        }
    }
}
