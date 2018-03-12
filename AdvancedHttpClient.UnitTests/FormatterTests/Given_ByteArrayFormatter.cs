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
    public class Given_ByteArrayFormatter
    {
        [TestMethod]
        public async Task Given_ByteArrayFormatter_When_Serialize_IsCaller_WithValid_ByteArray_Then_Return_Stream()
        {
            var sut = new ByteArrayFormatter();
            var testEntry = File.ReadAllBytes("Resources/test-image.png");

            var testExpectation = BuildMemoryStream(testEntry);
            var testResult = new MemoryStream();
            await sut.Serialize(testEntry, testResult).ConfigureAwait(false);

            testResult.ToArray().Should().BeEquivalentTo(testExpectation.ToArray());
        }

        [TestMethod]
        public async Task Given_ByteArrayFormatter_When_Deserialize_IsCaller_WithValid_Stream_Then_Return_ByteArray()
        {
            var sut = new ByteArrayFormatter();
            var testExpectation = File.ReadAllBytes("Resources/test-image.png");
            var testEntry = BuildMemoryStream(testExpectation);

            var testResult = await sut.Deserialize<byte[]>(testEntry).ConfigureAwait(false);

            testResult.Should().BeEquivalentTo(testExpectation);
        }

        public MemoryStream BuildMemoryStream(byte[] data)
        {
            return new MemoryStream(data);
        }
    }
}
