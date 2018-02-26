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

        public Uri ResourceUri { get; set; }

        public HttpClientInstance(string resourceUrl, AdvancedHttpClientSettings settings = null)
            : this(new Uri(resourceUrl), settings)
        {
        }

        public HttpClientInstance(Uri resourceUri, AdvancedHttpClientSettings settings = null)
        {
            ResourceUri = resourceUri;
            _settings = settings ?? new AdvancedHttpClientSettings();
        }

        private async Task<TResponse> SendAsync<TRequest, TResponse>(TRequest request, HttpMethod httpMethod)
        {
            ResolveHttpClient();
            _settings.ResourceHandler.ResourceUri = ResourceUri;
            var requestResourceUri = _settings.ResourceHandler.BuildRequestResourceUri(request, httpMethod);

            using (var requestStream = new MemoryStream())
            {
                var httpRequest = new HttpRequestMessage(httpMethod, requestResourceUri)
                {
                    Content = await BuildStreamContent(request, httpMethod, requestStream).ConfigureAwait(false)
                };

                _settings.HeadersHandler.SetRequestHeaders(httpRequest);

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
