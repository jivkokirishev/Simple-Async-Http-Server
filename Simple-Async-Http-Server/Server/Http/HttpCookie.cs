using Simple_Async_Http_Server.Server.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Async_Http_Server.Server.Http
{
    public class HttpCookie
    {
        public HttpCookie(string key, string value, int expires = 3)
        {
            CommonValidator.ThrowIfNull(key, nameof(key));
            CommonValidator.ThrowIfNull(value, nameof(value));

            this.Key = key;
            this.Value = value;

            this.Expires = DateTime.UtcNow.AddDays(expires);
        }

        public HttpCookie(string key, string value, bool isNew, int expires = 3)
            : this(key, value, expires)
        {
            this.IsNew = isNew;
        }

        public string Key { get; private set; }

        public string Value { get; private set; }

        public DateTime Expires { get; private set; }

        public bool IsNew { get; private set; } = true;

        public override string ToString()
        {
            var result = $"{this.Key}={this.Value}; Expires={this.Expires.ToLongTimeString()}";

            return result;
        }
    }
}
