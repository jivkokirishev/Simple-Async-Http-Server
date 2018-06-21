using Simple_Async_Http_Server.Server.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Async_Http_Server.Server.Http
{
    public class HttpHeader
    {
        public HttpHeader(string key, string value)
        {
            CommonValidator.ThrowIfNullOrEmpty(key, nameof(key));
            CommonValidator.ThrowIfNullOrEmpty(value, nameof(value));

            this.Key = key;
            this.Value = value;
        }

        public string Key { get; private set; }

        public string Value { get; private set; }

        public override string ToString()
        {
            return string.Format($"{this.Key}: {this.Value}");
        }
    }
}
