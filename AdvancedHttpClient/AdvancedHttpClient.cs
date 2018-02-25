using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace AdvancedHttpClient
{
    public partial class HttpClientInstance
    {
        private AdvancedHttpClientSettings _settings;
        private HttpClient _client;

        public RequestHeaders RequestHeaders { get; set; }
        public Uri ResourceUri { get; set; }

        public HttpClientInstance(AdvancedHttpClientSettings settings = null)
        {
            _settings = settings ?? new AdvancedHttpClientSettings();
            RequestHeaders = new RequestHeaders();
        }

        private async Task<TResponse> SendAsync<TRequest, TResponse>(TRequest request, HttpMethod httpMethod)
        {
            ResolveHttpClient();
            var resourceUri = BuildResourceUri(request, httpMethod);

            using (var requestStream = new MemoryStream())
            {
                var httpRequest = new HttpRequestMessage(httpMethod, resourceUri)
                {
                    Content = await BuildStreamContent(request, httpMethod, requestStream).ConfigureAwait(false)
                };
                RequestHeaders.SetRequestHeaders(httpRequest);

                var httpResponse = await _client.SendAsync(httpRequest).ConfigureAwait(false);
                var responseStream = await httpResponse.Content.ReadAsStreamAsync().ConfigureAwait(false);

                return await _settings.ResponseFormatter.Deserialize<TResponse>(responseStream).ConfigureAwait(false);
            }
        }

        private async Task<StreamContent> BuildStreamContent<TRequest>(TRequest request, HttpMethod httpMethod, Stream stream)
        {
            if (httpMethod == HttpMethod.Get)
            {
                return null;
            }
            await _settings.RequestFormatter.Serialize(request, stream).ConfigureAwait(false);
            return new StreamContent(stream);
        }

        private Uri BuildResourceUri<TRequest>(TRequest request, HttpMethod httpMethod)
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
   
            if(!properties.Any())
            {
                return string.Empty;
            }

            return  $"?{string.Join("&", properties)}";
        }

        private HttpClient ResolveHttpClient()
        {
            if (_client == null)
            {
                var httpClientHandler = new HttpClientHandler
                {
                    AutomaticDecompression = _settings.DecompressionMethods                   
                };

                _client = new HttpClient(httpClientHandler);
            }

            return _client;
        }
    }
}
