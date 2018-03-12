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
    public class Given_StreamFormatter
    {
        [TestMethod]
        public async Task Given_StreamFormatter_When_Serialize_IsCaller_WithValid_Stream_Then_Return_Stream()
        {
            var sut = new StreamFormatter();
            var testEntry = BuildMemoryStream(File.ReadAllBytes("Resources/test-image.png"));
            var testExpectation = BuildMemoryStream(File.ReadAllBytes("Resources/test-image.png"));

            var testResult = new MemoryStream();
            await sut.Serialize(testEntry, testResult).ConfigureAwait(false);

            testResult.ToArray().Should().BeEquivalentTo(testExpectation.ToArray());
        }

        [TestMethod]
        public async Task Given_StreamFormatter_When_Deserialize_IsCaller_WithValid_Stream_Then_Return_Stream()
        {
            var sut = new StreamFormatter();
            var testEntry = BuildMemoryStream(File.ReadAllBytes("Resources/test-image.png"));
            var testExpectation = BuildMemoryStream(File.ReadAllBytes("Resources/test-image.png"));

            var testResult = await sut.Deserialize<MemoryStream>(testEntry).ConfigureAwait(false);

            testResult.ToArray().Should().BeEquivalentTo(testExpectation.ToArray());
        }

        public MemoryStream BuildMemoryStream(byte[] data)
        {
            return new MemoryStream(data);
        }
    }
}
