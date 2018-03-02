using System;
using System.Net.Http;

namespace AdvancedHttpClient.ResourceHandlers
{
    public class DefaultResourceHandler : ResourceHandler
    {
        public override Uri BuildResourceUri<TRequest>(TRequest request, HttpMethod httpMethod)
        {
            return GetStandardResourceUri(request, httpMethod);
        }
    }
}
