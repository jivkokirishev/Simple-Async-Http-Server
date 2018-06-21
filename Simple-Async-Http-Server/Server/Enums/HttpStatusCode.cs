using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Async_Http_Server.Server.Enums
{
    public enum HttpStatusCode
    {
        OK = 200,
        MovedPermanently = 301,
        Found = 302,
        MovedTemporarily = 303,
        NotAuthorized = 401,
        NotFound = 404,
        InternalServerError = 500
    }
}
