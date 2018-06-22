using Simple_Async_Http_Server.Server.Common;
using Simple_Async_Http_Server.Server.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Async_Http_Server.Server.Http.Response
{
    public class RedirectResponse: HttpResponse
    {
        public RedirectResponse(string redirectUrl)
            :base()
        {
            this.StatusCode = HttpStatusCode.Found;
            this.AddHeaders("Location", redirectUrl);
        }

        
        private  void AddHeaders(string key, string value)
        {
            CommonValidator.ThrowIfNullOrEmpty(key, nameof(key));
            CommonValidator.ThrowIfNullOrEmpty(value, nameof(value));

            this.Headers.Add(new HttpHeader(key, value));
        }
    }
}
