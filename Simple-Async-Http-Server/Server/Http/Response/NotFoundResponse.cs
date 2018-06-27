using Simple_Async_Http_Server.Server.Enums;
using Simple_Async_Http_Server.Server.Http.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Async_Http_Server.Server.Http.Response
{
    public class NotFoundResponse : HttpResponse
    {
        public NotFoundResponse()
            : base()
        {
            this.StatusCode = HttpStatusCode.NotFound;
        }
    }
}
