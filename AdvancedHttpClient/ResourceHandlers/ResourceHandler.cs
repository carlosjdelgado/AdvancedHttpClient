using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedHttpClient.ResourceHandlers
{
    public class ResourceHandler : IResourceHandler
    {
        public Uri ResourceUri { set; private get; }

        public Uri BuildRequestResourceUri<TRequest>(TRequest request, HttpMethod httpMethod)
        {
            if (httpMethod == HttpMethod.Get)
            {
                return new Uri(ResourceUri, GetQueryString(request));
            }

            return ResourceUri;
        }

        private string GetQueryString<TRequest>(TRequest request)
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
