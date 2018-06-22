using Simple_Async_Http_Server.Server.Contracts;
using Simple_Async_Http_Server.Server.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Async_Http_Server.Server.Http.Response
{
    public class ViewResponse: HttpResponse
    {
        private readonly IView view;

        public ViewResponse(HttpStatusCode statusCode, IView view)
            :base()
        {
            this.ValidateStatusCode(statusCode);
            this.StatusCode = statusCode;
            this.view = view;
        }

        private void ValidateStatusCode(HttpStatusCode statusCode)
        {
            var statCodeNum = (int)statusCode;
            if (statCodeNum <= 300 || statCodeNum > 400)
            {
                throw new InvalidOperationException($"View responses need different status code! Current status code: {statCodeNum}");
            }
        }

        public override string ToString()
        {
            return string.Format($"{base.ToString()} {this.view.View()}");
        }
    }
}
