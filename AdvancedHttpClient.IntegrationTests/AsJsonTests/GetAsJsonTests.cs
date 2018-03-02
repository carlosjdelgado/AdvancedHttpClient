using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using AdvancedHttpClient.IntegrationTests.Models;
using FluentAssertions;

namespace AdvancedHttpClient.IntegrationTests.AsJsonTests
{
    [TestClass]
    public class GetAsJsonTests
    {
        [TestMethod]
        public async Task GetAsJson_WithRequestAndResponse()
        {
            var client = new HttpClientInstance("https://postman-echo.com/get");

            var request = BuildRequest();
            var response = await client.GetAsync<SimpleRequest, Response>(request).ConfigureAwait(false);

            response.Url.Should().Be("https://postman-echo.com/get?Foo=Foo&Bar=Bar");
        }

        [TestMethod]
        public async Task GetAsJson_WithResponse()
        {
            var client = new HttpClientInstance("https://postman-echo.com/get");

            var request = BuildRequest();
            var response = await client.GetAsync<Response>().ConfigureAwait(false);

            response.Url.Should().Be("https://postman-echo.com/get");
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
