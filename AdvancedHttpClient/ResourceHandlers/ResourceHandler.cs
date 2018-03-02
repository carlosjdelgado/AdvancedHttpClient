using System;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace AdvancedHttpClient.ResourceHandlers
{
    public abstract class ResourceHandler
    {
        internal Uri ResourceUri { set; private get; }

        public abstract Uri BuildResourceUri<TRequest>(TRequest request, HttpMethod httpMethod);

        protected Uri GetStandardResourceUri<TRequest>(TRequest request, HttpMethod httpMethod)
        {
            if (httpMethod == HttpMethod.Get)
            {
                return new Uri(ResourceUri, GetQueryString(request));
            }

            return ResourceUri;
        }

        protected string GetQueryString<TRequest>(TRequest request)
        {
            var properties = from p in request.GetType().GetProperties()
                             where p.GetValue(request) != null
                             select $"{p.Name}={WebUtility.UrlEncode(p.GetValue(request).ToString())}";

            if (!properties.Any())
            {
                return string.Empty;
            }

            return $"?{string.Join("&", properties)}";
        }
    }
}
