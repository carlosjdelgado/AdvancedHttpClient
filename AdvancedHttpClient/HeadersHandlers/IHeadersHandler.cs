using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedHttpClient.HeadersHandlers
{
    public interface IHeadersHandler
    {
        void SetRequestHeaders(HttpRequestMessage request);
    }
}
