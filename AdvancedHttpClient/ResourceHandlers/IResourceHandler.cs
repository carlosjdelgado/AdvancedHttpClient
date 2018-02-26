using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedHttpClient.ResourceHandlers
{
    public interface IResourceHandler
    {
        Uri ResourceUri { set; }
        Uri BuildRequestResourceUri<TRequest>(TRequest request, HttpMethod httpMethod);
    }
}
