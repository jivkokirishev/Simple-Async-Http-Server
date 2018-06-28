using Simple_Async_Http_Server.Server;
using Simple_Async_Http_Server.Server.Contracts;
using Simple_Async_Http_Server.Server.Routing;
using Simple_Async_Http_Server.Server.Routing.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Async_Http_Server
{
    public class Starter : IRunnable
    {
        private WebServer webServer;

        public static void Main()
        {
            new Starter().Run();
        }

        public void Run()
        {
            IAppRouteConfig routeConfig = new AppRouteConfig();
            this.webServer = new WebServer(1337, routeConfig);
            this.webServer.Run();
        }
    }
}
