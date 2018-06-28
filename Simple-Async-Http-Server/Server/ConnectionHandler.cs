using Simple_Async_Http_Server.Server.Common;
using Simple_Async_Http_Server.Server.CustomSocket;
using Simple_Async_Http_Server.Server.CustomSocket.Contracts;
using Simple_Async_Http_Server.Server.Handlers;
using Simple_Async_Http_Server.Server.Http;
using Simple_Async_Http_Server.Server.Http.Contracts;
using Simple_Async_Http_Server.Server.Routing.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Simple_Async_Http_Server.Server
{
    public class ConnectionHandler
    {
        private readonly ISocketWrapper client;
        private readonly IServerRouteConfig serverRouteConfig;

        public ConnectionHandler(Socket client, IServerRouteConfig serverRouteConfig)
        {
            CommonValidator.ThrowIfNull(client, nameof(client));
            CommonValidator.ThrowIfNull(serverRouteConfig, nameof(serverRouteConfig));

            this.client = new SocketWrapper(client);
            this.serverRouteConfig = serverRouteConfig;
        }

        public async Task ProcessRequestAsync()
        {
            var request = await this.client.ReceiveAsync(SocketFlags.None);

            if (request != string.Empty)
            {
                var reqContext = new HttpContext(request);
                var httpResponse = new HttpHandler(this.serverRouteConfig).Handle(reqContext);

                var response = httpResponse.ToString();
                var respBytes = Encoding.ASCII.GetBytes(response);
                var buffer = new ArraySegment<byte>(respBytes);

                await this.client.SendAsync(buffer, SocketFlags.None);



                Console.WriteLine("----------REQUEST----------");
                Console.WriteLine(request);
                Console.WriteLine("----------RESPONSE----------");
                Console.WriteLine(response);
            }

            this.client.ShutDown(SocketShutdown.Both);
        }
    }
}
