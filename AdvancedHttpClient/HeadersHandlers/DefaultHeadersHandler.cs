using System.Collections.Generic;

namespace AdvancedHttpClient.HeadersHandlers
{
    public class DefaultHeadersHandler : HeadersHandler
    {
        public DefaultHeadersHandler()
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
    }
}
