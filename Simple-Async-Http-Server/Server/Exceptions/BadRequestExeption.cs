using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Async_Http_Server.Server.Exceptions
{
    public class BadRequestExeption : Exception
    {
        public BadRequestExeption(string message) : base(message)
        {
        }
    }
}
