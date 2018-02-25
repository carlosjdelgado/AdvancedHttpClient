using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedHttpClient
{
    public class RequestHeaders
    {
        public Dictionary<string, string> CustomHeaders { get; set; }
        public List<string> Accept { get; set; }
        public List<string> AcceptEncoding { get; set; }
        public string ContentType { get; set; }

        public RequestHeaders()
        {
            SetDefaultRequestHeaders();
        }

        private void SetDefaultRequestHeaders()
        {
            CustomHeaders = new Dictionary<string, string>();
            Accept = new List<string>() { "*/*" };
            AcceptEncoding = new List<string>() { "gzip", "deflate" };
            ContentType = "application/json";
        }

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
