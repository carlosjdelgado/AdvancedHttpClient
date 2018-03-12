using AdvancedHttpClient.Formatters;
using AdvancedHttpClient.UnitTests.Models;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedHttpClient.UnitTests.FormatterTests
{
    [TestClass]
    public class Given_ModelAsJsonFormatter
    {
        [TestMethod]
        public async Task Given_ModelAsJsonFormatter_When_Serialize_IsCaller_WithValid_Model_Then_Return_StreamAsJson()
        {
            var sut = new ModelAsJsonFormatter();
            var testModel = new SimpleModel
            {
                Name = "Foo",
                Surname = "Bar"
            };

            var testExpectation = BuildMemoryStream(testModel);
            var testResult = new MemoryStream();
            await sut.Serialize(testModel, testResult).ConfigureAwait(false);

            testResult.ToArray().Should().BeEquivalentTo(testExpectation.ToArray());
        }

        [TestMethod]
        public async Task Given_ModelAsJsonFormatter_When_Deserialize_IsCaller_WithValid_StreamAsJson_Then_Return_Model()
        {
            var sut = new ModelAsJsonFormatter();
            var testExpectation = new SimpleModel
            {
                Name = "Foo",
                Surname = "Bar"
            };

            var testEntry = BuildMemoryStream(testExpectation);
            var testResult = await sut.Deserialize<SimpleModel>(testEntry).ConfigureAwait(false);

            testResult.Should().BeEquivalentTo(testExpectation);
        }

        public MemoryStream BuildMemoryStream(SimpleModel model)
        {
            var json = "{" +
                $"\"Name\":\"{model.Name}\"," +
                $"\"Surname\":\"{model.Surname}\"" +
            "}";

            var jsonAsByteArray = Encoding.UTF8.GetBytes(json);
            var stream = new MemoryStream();
            stream.Write(jsonAsByteArray, 0, jsonAsByteArray.Length);
            stream.Position = 0;
            return stream;
        }
    }
}
