using Simple_Async_Http_Server.Server.Routing.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple_Async_Http_Server.Server.Enums;
using Simple_Async_Http_Server.Server.Common;
using System.Text.RegularExpressions;

namespace Simple_Async_Http_Server.Server.Routing
{
    public class ServerRouteConfig : IServerRouteConfig
    {
        public ServerRouteConfig(IAppRouteConfig appRouteConfig)
        {
            CommonValidator.ThrowIfNull(appRouteConfig, nameof(appRouteConfig));

            this.Routes = new Dictionary<HttpRequestMethod, IDictionary<string, IRoutingContext>>();

            var methods = Enum.GetValues(typeof(HttpRequestMethod)).Cast<HttpRequestMethod>();
            foreach (var method in methods)
            {
                this.Routes[method] = new Dictionary<string, IRoutingContext>();
            }

            this.InitializeServerConfig(appRouteConfig);
        }

        public IDictionary<HttpRequestMethod, IDictionary<string, IRoutingContext>> Routes { get; private set; }

        private void InitializeServerConfig(IAppRouteConfig appRouteConfig)
        {
            foreach (var kvp in appRouteConfig.Routes)
            {
                foreach (var requestHandler in kvp.Value)
                {
                    var parameters = new List<string>();

                    string parsedRegex = this.ParseRoute(requestHandler.Key, ref parameters);

                    IRoutingContext routingContext = new RoutingContext(parameters, requestHandler.Value);
                    this.Routes[kvp.Key].Add(parsedRegex, routingContext);
                }
            }
        }

        private string ParseRoute(string requestHandlerKey, ref List<string> parameters)
        {
            var parsedRegex = new StringBuilder();
            parsedRegex.Append('^');

            if (requestHandlerKey == "/")
            {
                parsedRegex.Append("/$");
                return parsedRegex.ToString();
            }

            string[] tokens = requestHandlerKey.Split(new[] { '/' });
            this.ParseTokens(tokens, ref parameters,  ref parsedRegex);

            return parsedRegex.ToString();
        }

        private void ParseTokens(string[] tokens, ref List<string> parameters, ref StringBuilder parsedRegex)
        {
            for (int i = 0; i < tokens.Length; i++)
            {
                string end = (i == tokens.Length - 1 ? "$" : "/");

                if (!tokens[i].StartsWith("{") && !tokens[i].EndsWith("}"))
                {
                    parsedRegex.Append($"{tokens[i]}{end}");
                    continue;
                }

                var regex = new Regex("<\\w+>");
                var match = regex.Match(tokens[i]);

                if (!match.Success)
                {
                    //continue;
                    throw new InvalidProgramException($"The token '{tokens[i]}' is not valid!");
                }

                var paramName = match.Groups[0].Value.Substring(1, match.Groups[0].Length - 2);
                parameters.Add(paramName);
                parsedRegex.Append($"{tokens[i].Substring(1, tokens[i].Length - 2)}{end}");
            }
        }
    }
}
