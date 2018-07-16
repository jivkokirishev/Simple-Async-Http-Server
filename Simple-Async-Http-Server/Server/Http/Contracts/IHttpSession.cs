using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Async_Http_Server.Server.Http.Contracts
{
    public interface IHttpSession
    {
        string Id { get; }

        T GetParameter<T>(string key);

        void Add(string key, object parameter);

        void Clear();
    }
}
