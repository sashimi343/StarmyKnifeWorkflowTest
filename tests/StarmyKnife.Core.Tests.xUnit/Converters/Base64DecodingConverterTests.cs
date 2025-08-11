using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xunit;

using StarmyKnife.Core.Plugins;
using StarmyKnife.Core.Plugins.BuiltIn.Converters;

namespace StarmyKnife.Core.Tests.xUnit.Converters
{
    public class Base64DecodingConverterTests
    {
        [Fact]
        public void TestConvert_ValidBase64String_ReturnsDecodedString()
        {
            var input = "SGVsbG8gV29ybGQ="; // "Hello World" in Base64
            var expectedOutput = "Hello World";
            var result = TestHelper.TestConvert<Base64DecodingConverter>(input);
            Assert.True(result.Success);
            Assert.Equal(expectedOutput, result.Value);
        }

        [Fact]
        public void TestConvert_InvalidBase64String_ReturnsError()
        {
            var input = "InvalidBase64String";
            var result = TestHelper.TestConvert<Base64DecodingConverter>(input);
            Assert.False(result.Success);
            Assert.True(result.Errors.Any());
        }

        [Fact]
        public void TestConvert_EmptyString_ReturnsEmptyString()
        {
            var input = "";
            var expectedOutput = "";
            var result = TestHelper.TestConvert<Base64DecodingConverter>(input);
            Assert.True(result.Success);
            Assert.Equal(expectedOutput, result.Value);
        }

        [Fact]
        public void TestConvert_NullInput_ReturnsError()
        {
            string input = null;
            var result = TestHelper.TestConvert<Base64DecodingConverter>(input);
            Assert.False(result.Success);
            Assert.True(result.Errors.Any());
        }

        [Fact]
        public void TestConvert_WhitespaceInput_ReturnsEmptyString()
        {
            var input = "   "; // Whitespace input
            var expectedOutput = "";
            var result = TestHelper.TestConvert<Base64DecodingConverter>(input);
            Assert.True(result.Success);
            Assert.Equal(expectedOutput, result.Value);
        }

        [Fact]
        public void TestConvert_Base64WithDifferentEncoding_ReturnsCorrectlyDecodedString()
        {
            var input = "5pel5pys6Kqe44OG44K444Oz44Kw44O844OJ"; // "こんにちは" in UTF-8 Base64
            var expectedOutput = "こんにちは";
            var parameters = new KeyValuePair<string, object>[]
            {
                new KeyValuePair<string, object>("CharacterSet", Base64CharacterSet.Standard),
                new KeyValuePair<string, object>(Base64DecodingConverter.ParameterKeys.Encoding, PluginEncoding.UTF8)
            };
            var result = TestHelper.TestConvert<Base64DecodingConverter>(input, parameters);
            Assert.True(result.Success);
            Assert.Equal(expectedOutput, result.Value);
        }

        [Fact]
        public void TestConvert_Base64WithInvalidEncoding_ReturnsError()
        {
            var input = "SGVsbG8gV29ybGQ="; // "Hello World" in ASCII Base64
            var parameters = new KeyValuePair<string, object>[]
            {
                new KeyValuePair<string, object>(Base64DecodingConverter.ParameterKeys.CharacterSet, Base64CharacterSet.Standard),
                new KeyValuePair<string, object>(Base64DecodingConverter.ParameterKeys.Encoding, PluginEncoding.UTF16BE)
            };
            var result = TestHelper.TestConvert<Base64DecodingConverter>(input, parameters);
            Assert.False(result.Success);
            Assert.True(result.Errors.Any());
        }
    }
}
