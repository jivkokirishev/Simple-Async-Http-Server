﻿using Simple_Async_Http_Server.Server.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Async_Http_Server.Server.Http.Contracts
{
    public interface IHttpResponse
    {
        HttpHeaderCollection Headers { get; set; }

        HttpStatusCode StatusCode { get; set; }
    }
}
