using Simple_Async_Http_Server.Server.Http.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple_Async_Http_Server.Server.Enums;
using Simple_Async_Http_Server.Server.Common;
using Simple_Async_Http_Server.Server.Exceptions;
using System.Net;

namespace Simple_Async_Http_Server.Server.Http
{
    public class HttpRequest : IHttpRequest
    {
        public HttpRequest(string requestString)
        {
            CommonValidator.ThrowIfNullOrEmpty(requestString, nameof(requestString));

            this.Headers = new HttpHeaderCollection();
            this.UrlParameters = new Dictionary<string, string>();
            this.QueryParameters = new Dictionary<string, string>();
            this.FormData = new Dictionary<string, string>();

            this.ParseRequest(requestString);
        }

        public IDictionary<string, string> FormData { get; private set; }

        public HttpHeaderCollection Headers { get; private set; }

        public string Path { get; private set; }

        public IDictionary<string, string> QueryParameters { get; private set; }

        public HttpRequestMethod RequestMethod { get; private set; }

        public string Url { get; private set; }

        public IDictionary<string, string> UrlParameters { get; private set; }

        public void AddUrlParameter(string key, string value)
        {
            CommonValidator.ThrowIfNullOrEmpty(key, nameof(key));
            CommonValidator.ThrowIfNullOrEmpty(value, nameof(value));

            this.UrlParameters[key] = value;
        }

        private void ParseRequest(string requestString)
        {
            CommonValidator.ThrowIfNullOrEmpty(requestString, nameof(requestString));

            string[] requestLines = requestString.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

            if (!requestLines.Any())
            {
                throw new BadRequestExeption("Invalid request lines!");
            }

            string[] requestLine = requestLines.First().Trim().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (requestLine.Length != 3 || requestLine.Last().ToLower() != "http/1.1")
            {
                throw new BadRequestExeption("Invalid request line!");
            }

            var method = requestLine.First().Trim();

            this.RequestMethod = this.ParseRequestMethod(method);
            this.Url = requestLine[1];
            this.Path = this.Url.Split(new[] { '?', '#' }, StringSplitOptions.RemoveEmptyEntries)[0];

            this.ParseHeaders(requestLines);
            this.ParseParameters();

            if (this.RequestMethod == HttpRequestMethod.POST)
            {
                this.ParseQuery(requestLines.Last(), this.FormData);
            }
        }

        private void ParseParameters()
        {
            if (!this.Url.Contains('?'))
            {
                return;
            }

            string query = this.Url.Split('?').Last();
            this.ParseQuery(query, this.QueryParameters);
        }

        private void ParseQuery(string query, IDictionary<string, string> queryParameters)
        {
            CommonValidator.ThrowIfNullOrEmpty(query, nameof(query));

            var paramets = query.Split(new[] { '&' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var param in paramets)
            {
                var kvp = param.Split(new[] { '=' }, StringSplitOptions.RemoveEmptyEntries);

                if (kvp.Length != 2)
                {
                    throw new BadRequestExeption($"Invalid parameter in the request: {WebUtility.UrlDecode(param)}");
                }

                queryParameters[WebUtility.UrlDecode(kvp.First())] = WebUtility.UrlDecode(kvp.Last());
            }
        }

        private void ParseHeaders(string[] requestLines)
        {
            int endIndex = Array.IndexOf(requestLines, string.Empty);

            for (int i = 1; i < endIndex; i++)
            {
                var kvp = requestLines[i].Split(new[] { ": " }, StringSplitOptions.None);

                if (kvp.Length != 2)
                {
                    throw new BadRequestExeption($"Invalid header in the request: {WebUtility.UrlDecode(requestLines[i])}");
                }
                var header = new HttpHeader(WebUtility.UrlDecode(kvp.First()), WebUtility.UrlDecode(kvp.Last()));
                this.Headers.Add(header);

                if (!this.Headers.ContainsKey("Host"))
                {
                    throw new BadRequestExeption($"Invalid header in the request: does not contain 'Host'");
                }
            }
        }

        private HttpRequestMethod ParseRequestMethod(string method)
        {
            CommonValidator.ThrowIfNullOrEmpty(method, nameof(method));

            HttpRequestMethod parsedMethod;

            if (Enum.TryParse(method, true, out parsedMethod))
            {
                throw new BadRequestExeption($"Request method '{method}' is not valid, or not implemented yet!");
            }

            return parsedMethod;
        }
    }
}
