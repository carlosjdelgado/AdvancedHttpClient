using Microsoft.VisualStudio.TestTools.UnitTesting;
using AdvancedHttpClient;
using System;
using System.Threading.Tasks;
using AdvancedHttpClient.IntegrationTests.Models;
using FluentAssertions;

namespace AdvancedHttpClient.IntegrationTests.AsJsonTests
{
    [TestClass]
    public class PutAsJsonTests
    {
        [TestMethod]
        public async Task PutAsJson_WithRequestAndResponse()
        {
            var client = new HttpClientInstance("https://postman-echo.com/put");

            var request = BuildRequest();
            var response = await client.PutAsync<SimpleRequest, Response>(request).ConfigureAwait(false);

            response.Url.Should().Be("https://postman-echo.com/put");
        }

        [TestMethod]
        public async Task PutAsJson_WithResponse()
        {
            var client = new HttpClientInstance("https://postman-echo.com/put");
            
            var request = BuildRequest();
            var response = await client.PutAsync<Response>().ConfigureAwait(false);

            response.Url.Should().Be("https://postman-echo.com/put");
        }

        [TestMethod]
        public async Task PutAsJson_WithRequest()
        {
            var client = new HttpClientInstance("https://postman-echo.com/put");

            var request = BuildRequest();
            await client.PutAsync(request).ConfigureAwait(false);
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
