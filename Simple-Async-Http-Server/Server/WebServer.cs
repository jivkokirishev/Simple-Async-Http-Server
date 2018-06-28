using Simple_Async_Http_Server.Server.Common;
using Simple_Async_Http_Server.Server.Contracts;
using Simple_Async_Http_Server.Server.Routing;
using Simple_Async_Http_Server.Server.Routing.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Async_Http_Server.Server
{
    public class WebServer : IRunnable
    {
        private readonly int port;
        private readonly IServerRouteConfig serverRouteConfig;
        private readonly TcpListener tcpListener;
        private bool isRunning = false;

        private const string LocalHostIp = "127.0.0.1";

        public WebServer(int port, IAppRouteConfig routeConfig)
        {
            CommonValidator.ThrowIfNull(port, nameof(port));
            CommonValidator.ThrowIfNull(routeConfig, nameof(routeConfig));

            this.port = port;
            this.serverRouteConfig = new ServerRouteConfig(routeConfig);
            this.tcpListener = new TcpListener(IPAddress.Parse(LocalHostIp), port);
        }

        public void Run()
        {
            this.tcpListener.Start();
            this.isRunning = true;

            Console.WriteLine($"Server started. Listening to TCP clients at {LocalHostIp}:{port}");

            Task t = Task.Run(() => this.ListenLoop());
            t.GetAwaiter().GetResult();
        }

        private async Task ListenLoop()
        {
            while (this.isRunning)
            {
                Socket client = await this.tcpListener.AcceptSocketAsync();
                ConnectionHandler connectionHandler = new ConnectionHandler(client, this.serverRouteConfig);
                Task connection = connectionHandler.ProcessRequestAsync();
                connection.GetAwaiter().GetResult();
            }
        }
    }
}
