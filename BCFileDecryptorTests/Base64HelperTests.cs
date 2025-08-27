using System;
using System.Text;
using BCFileDecryptorCore;
using Xunit;

namespace BCFileDecryptorTests
{
    public class Base64HelperTests
    {
        [Fact]
        public void Encode_WithValidData_ReturnsCorrectBase64()
        {
            // Arrange
            byte[] testData = Encoding.UTF8.GetBytes("TestString123");
            string expectedBase64 = "VGVzdFN0cmluZzEyMw==";

            // Act
            byte[] encoded = Base64Helper.encode(testData);
            string encodedString = Encoding.Default.GetString(encoded);

            // Assert
            Assert.Equal(expectedBase64, encodedString);
        }

        [Fact]
        public void Decode_WithValidString_ReturnsOriginalData()
        {
            // Arrange
            string base64String = "VGVzdFN0cmluZzEyMw==";
            string expected = "TestString123";

            // Act
            byte[] decoded = Base64Helper.decode(base64String);
            string decodedString = Encoding.UTF8.GetString(decoded);

            // Assert
            Assert.Equal(expected, decodedString);
        }

        [Fact]
        public void DecodeByteArray_WithValidData_ReturnsOriginalData()
        {
            // Arrange
            string base64String = "VGVzdFN0cmluZzEyMw==";
            byte[] base64Bytes = Encoding.Default.GetBytes(base64String);
            string expected = "TestString123";

            // Act
            byte[] decoded = Base64Helper.decode(base64Bytes);
            string decodedString = Encoding.UTF8.GetString(decoded);

            // Assert
            Assert.Equal(expected, decodedString);
        }

        [Fact]
        public void Encode_WithEmptyData_ThrowsException()
        {
            // Arrange
            byte[] emptyData = new byte[0];

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => Base64Helper.encode(emptyData));
            Assert.Equal("No data to encode", exception.Message);
        }

        [Fact]
        public void Decode_WithEmptyString_ThrowsException()
        {
            // Arrange
            string emptyString = "";

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => Base64Helper.decode(emptyString));
            Assert.Equal("No data to encode", exception.Message);
        }
    }
}
