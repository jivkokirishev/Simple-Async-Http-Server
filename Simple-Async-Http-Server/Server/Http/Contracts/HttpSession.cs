using Simple_Async_Http_Server.Server.Common;
using Simple_Async_Http_Server.Server.Http.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Async_Http_Server.Server.Http.Contracts
{
    public class HttpSession : IHttpSession
    {
        private string id;
        private IDictionary<string, object> parameters;

        public HttpSession(string id)
        {
            CommonValidator.ThrowIfNullOrEmpty(id, nameof(id));

            this.id = id;
            this.parameters = new Dictionary<string, object>();
        }

        public string Id => id;

        public void Add(string key, object parameter)
        {
            CommonValidator.ThrowIfNullOrEmpty(id, nameof(id));
            CommonValidator.ThrowIfNull(parameter, nameof(parameter));

            this.parameters[key] = parameter;
        }

        public void Clear()
        {
            this.parameters.Clear();
        }

        public T GetParameter<T>(string key)
        {
            CommonValidator.ThrowIfNullOrEmpty(key, nameof(key));

            if (!this.parameters.ContainsKey(key))
            {
                throw new InvalidOperationException($"The given key {key} is not present in the session!");
            }

            return (T)this.parameters[key];
        }
    }
}
