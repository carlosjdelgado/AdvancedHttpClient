using AdvancedHttpClient.Formatters;
using AdvancedHttpClient.HeadersHandlers;
using AdvancedHttpClient.ResourceHandlers;
using System.Net;

namespace AdvancedHttpClient
{
    public class Settings
    {
        public IFormatter RequestFormatter { get; set; }
        public IFormatter ResponseFormatter { get; set; }
        public HeadersHandler HeadersHandler { get; set; }
        public ResourceHandler ResourceHandler { get; set; }
        public DecompressionMethods DecompressionMethods { get; set; }

        public Settings()
        {
            SetDefaultSettings();
        }

        private void SetDefaultSettings()
        {
            RequestFormatter = new ModelAsJsonFormatter();
            ResponseFormatter = new ModelAsJsonFormatter();
            HeadersHandler = new DefaultHeadersHandler();
            ResourceHandler = new DefaultResourceHandler();

            DecompressionMethods = DecompressionMethods.Deflate | DecompressionMethods.GZip;
        }
    }
}
