using System.Collections.Generic;

namespace AdvancedHttpClient.IntegrationTests.Models
{
    public class Response
    {
        public Dictionary<string, string> Args { get; set; }
        public Dictionary<string, string> Headers { get; set; }
        public string Url { get; set; }
    }
}
