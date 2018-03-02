using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;

namespace AdvancedHttpClient.HeadersHandlers
{
    public abstract class HeadersHandler
    {
        protected Dictionary<string, string> CustomHeaders { get; set; }
        protected List<string> Accept { get; set; }
        protected List<string> AcceptEncoding { get; set; }
        protected string ContentType { get; set; }

        internal void SetRequestHeaders(HttpRequestMessage request)
        {
            request.Headers.Clear();

            Accept.ForEach(x => request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(x)));
            AcceptEncoding.ForEach(x => request.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue(x)));
            CustomHeaders.ToList().ForEach(x => request.Headers.Add(x.Key, x.Value));
            if (request.Content != null)
            {
                request.Content.Headers.ContentType = new MediaTypeHeaderValue(ContentType);
            }
        }
    }
}
