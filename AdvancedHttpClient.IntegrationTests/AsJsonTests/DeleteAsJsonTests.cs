using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using AdvancedHttpClient.IntegrationTests.Models;
using FluentAssertions;

namespace AdvancedHttpClient.IntegrationTests.AsJsonTests
{
    [TestClass]
    public class DeleteAsJsonTests
    {
        [TestMethod]
        public async Task DeleteAsJson_WithRequestAndResponse()
        {
            var client = new HttpClientInstance("https://postman-echo.com/delete");

            var request = BuildRequest();
            var response = await client.DeleteAsync<SimpleRequest, Response>(request).ConfigureAwait(false);

            response.Url.Should().Be("https://postman-echo.com/delete");
        }

        [TestMethod]
        public async Task DeleteAsJson_WithResponse()
        {
            var client = new HttpClientInstance("https://postman-echo.com/delete");

            var request = BuildRequest();
            var response = await client.DeleteAsync<Response>().ConfigureAwait(false);

            response.Url.Should().Be("https://postman-echo.com/delete");
        }

        [TestMethod]
        public async Task DeleteAsJson_WithRequest()
        {
            var client = new HttpClientInstance("https://postman-echo.com/delete");

            var request = BuildRequest();
            await client.DeleteAsync(request).ConfigureAwait(false);
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
