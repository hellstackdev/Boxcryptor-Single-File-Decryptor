using System;
using System.Text;
using BCFileDecryptorCore;
using Xunit;

namespace BCFileDecryptorTests
{
    public class HashHelperTests
    {
        [Fact]
        public void ComputeSHA256HMAC_WithValidData_ReturnsExpectedHash()
        {
            // Arrange
            byte[] data = Encoding.UTF8.GetBytes("Test data for HMAC");
            byte[] key = Encoding.UTF8.GetBytes("SecretKey12345");

            // Act
            byte[] hash1 = HashHelper.ComputeSHA256HMAC(data, key);
            byte[] hash2 = HashHelper.ComputeSHA256HMAC(data, key);

            // Assert
            Assert.NotNull(hash1);
            Assert.Equal(32, hash1.Length); // SHA256 produces a 32-byte hash
            Assert.Equal(hash1, hash2); // Same input should produce same hash
        }

        [Fact]
        public void ComputeSHA256HMAC_WithDifferentData_ReturnsDifferentHashes()
        {
            // Arrange
            byte[] data1 = Encoding.UTF8.GetBytes("Test data 1");
            byte[] data2 = Encoding.UTF8.GetBytes("Test data 2");
            byte[] key = Encoding.UTF8.GetBytes("SecretKey12345");

            // Act
            byte[] hash1 = HashHelper.ComputeSHA256HMAC(data1, key);
            byte[] hash2 = HashHelper.ComputeSHA256HMAC(data2, key);

            // Assert
            Assert.NotEqual(hash1, hash2); // Different input should produce different hash
        }

        [Fact]
        public void ComputeSHA256HMAC_WithDifferentKeys_ReturnsDifferentHashes()
        {
            // Arrange
            byte[] data = Encoding.UTF8.GetBytes("Test data");
            byte[] key1 = Encoding.UTF8.GetBytes("SecretKey1");
            byte[] key2 = Encoding.UTF8.GetBytes("SecretKey2");

            // Act
            byte[] hash1 = HashHelper.ComputeSHA256HMAC(data, key1);
            byte[] hash2 = HashHelper.ComputeSHA256HMAC(data, key2);

            // Assert
            Assert.NotEqual(hash1, hash2); // Different keys should produce different hash
        }

        [Fact]
        public void ComputeSHA256HMAC_WithEmptyData_ThrowsException()
        {
            // Arrange
            byte[] emptyData = new byte[0];
            byte[] key = Encoding.UTF8.GetBytes("SecretKey");

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => HashHelper.ComputeSHA256HMAC(emptyData, key));
            Assert.Equal("No data from which to calculate hmac", exception.Message);
        }

        [Fact]
        public void ComputeSHA256HMAC_WithEmptyKey_ThrowsException()
        {
            // Arrange
            byte[] data = Encoding.UTF8.GetBytes("Test data");
            byte[] emptyKey = new byte[0];

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => HashHelper.ComputeSHA256HMAC(data, emptyKey));
            Assert.Equal("No data from which to calculate hmac", exception.Message);
        }
    }
}
