using Microsoft.VisualStudio.TestTools.UnitTesting;
using AdvancedHttpClient;
using System;
using System.Threading.Tasks;
using AdvancedHttpClient.IntegrationTests.Models;
using FluentAssertions;

namespace AdvancedHttpClient.IntegrationTests.AsJsonTests
{
    [TestClass]
    public class PostAsJsonTests
    {
        [TestMethod]
        public async Task PostAsJson_WithRequestAndResponse()
        {
            var client = new HttpClientInstance
            {
                ResourceUri = new Uri("https://postman-echo.com/post")
            };

            var request = BuildRequest();
            var response = await client.PostAsync<SimpleRequest, Response>(request).ConfigureAwait(false);

            response.Url.Should().Be("https://postman-echo.com/post");
        }

        [TestMethod]
        public async Task PostAsJson_WithResponse()
        {
            var client = new HttpClientInstance
            {
                ResourceUri = new Uri("https://postman-echo.com/post")
            };

            var request = BuildRequest();
            var response = await client.PostAsync<Response>().ConfigureAwait(false);

            response.Url.Should().Be("https://postman-echo.com/post");
        }

        private SimpleRequest BuildRequest()
        {
            return new SimpleRequest
            {
                Foo = "Foo",
                Bar = "Bar"
            };
        }
    }
}
