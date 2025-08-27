using System;
using System.Text;
using System.Security.Cryptography;
using BCFileDecryptorCore;
using Xunit;

namespace BCFileDecryptorTests
{
    public class RSAHelperTests
    {
        [Fact]
        public void DecryptData_WithEmptyPrivateKey_ThrowsException()
        {
            // Arrange
            string encryptedData = "SomeEncryptedData==";
            byte[] emptyPrivateKey = new byte[0];

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => RSAHelper.DecryptData(encryptedData, emptyPrivateKey));
            Assert.Equal("The private key used for the RSA decryption can't be empty", exception.Message);
        }

        [Fact]
        public void DecryptData_WithInvalidBase64_ThrowsException()
        {
            // Arrange
            string invalidBase64 = "InvalidBase64String";
            byte[] privateKey = GenerateRsaPrivateKey();

            // Act & Assert
            Assert.Throws<FormatException>(() => RSAHelper.DecryptData(invalidBase64, privateKey));
        }

        [Fact]
        public void DecryptData_WithInvalidPrivateKey_ThrowsException()
        {
            // Arrange
            // Generate a random string and encode to base64
            byte[] randomData = new byte[16];
            new Random().NextBytes(randomData);
            string base64String = Convert.ToBase64String(randomData);

            // Invalid private key (random bytes, not a valid RSA key)
            byte[] invalidPrivateKey = new byte[64];
            new Random().NextBytes(invalidPrivateKey);

            // Act & Assert
            Assert.Throws<FormatException>(() => RSAHelper.DecryptData(base64String, invalidPrivateKey));
        }

        // Helper method to generate a valid RSA private key for testing
        private byte[] GenerateRsaPrivateKey()
        {
            using (RSA rsa = RSA.Create(2048))
            {
                return rsa.ExportRSAPrivateKey();
            }
        }
    }
}
