using AdvancedHttpClient.Formatters;
using AdvancedHttpClient.HeadersHandlers;
using AdvancedHttpClient.ResourceHandlers;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace AdvancedHttpClient
{
    public class AdvancedHttpClientSettings
    {
        public IFormatter RequestFormatter { get; set; }
        public IFormatter ResponseFormatter { get; set; }
        public IHeadersHandler HeadersHandler { get; set; }
        public IResourceHandler ResourceHandler { get; set; }
        public DecompressionMethods DecompressionMethods { get; set; }

        public AdvancedHttpClientSettings()
        {
            SetDefaultSettings();
        }

        private void SetDefaultSettings()
        {
            RequestFormatter = new ModelAsJsonFormatter();
            ResponseFormatter = new ModelAsJsonFormatter();
            HeadersHandler = new HeadersHandler();
            ResourceHandler = new ResourceHandler();

            DecompressionMethods = DecompressionMethods.Deflate | DecompressionMethods.GZip;
        }
    }
}
