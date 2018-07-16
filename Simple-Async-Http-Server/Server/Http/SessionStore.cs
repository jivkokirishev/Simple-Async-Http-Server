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
    public static class SessionStore
    {
        private static ConcurrentDictionary<string, IHttpSession> sessions = new ConcurrentDictionary<string, IHttpSession>();

        public static bool ConstainsKey(string key)
        {
            CommonValidator.ThrowIfNullOrEmpty(key, nameof(key));

            return sessions.ContainsKey(key);
        }

        public static IHttpSession GetOrAdd(string id)
        {
            CommonValidator.ThrowIfNullOrEmpty(id, nameof(id));

            return sessions.GetOrAdd(id, _ => new HttpSession(id));
        }
    }
}
