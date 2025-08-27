using System;
using System.Text;
using System.Security.Cryptography;
using BCFileDecryptorCore;
using Xunit;

namespace BCFileDecryptorTests
{
    public class AESHelperTests
    {
        [Fact]
        public void DecryptData_WithValidData_DecryptsCorrectly()
        {
            // Arrange
            byte[] originalData = Encoding.UTF8.GetBytes("This is a test message for AES encryption");
            byte[] key = new byte[32]; // 256-bit key
            new Random().NextBytes(key); // Generate random key for testing
            byte[] iv = new byte[16]; // 128-bit IV
            new Random().NextBytes(iv); // Generate random IV for testing

            // Encrypt with AES for testing
            byte[] encryptedData;
            using (Aes aes = Aes.Create())
            {
                aes.Mode = CipherMode.CBC;
                aes.Key = key;
                aes.IV = iv;
                aes.Padding = PaddingMode.PKCS7;

                using var encryptor = aes.CreateEncryptor();
                encryptedData = encryptor.TransformFinalBlock(originalData, 0, originalData.Length);
            }

            // Act
            byte[] decryptedData = AESHelper.DecryptData(encryptedData, key, iv, true);

            // Assert
            string decryptedString = Encoding.UTF8.GetString(decryptedData);
            Assert.Equal("This is a test message for AES encryption", decryptedString);
        }

        [Fact]
        public void DecryptData_WithInvalidKey_ThrowsException()
        {
            // Arrange
            byte[] originalData = Encoding.UTF8.GetBytes("This is a test message for AES encryption");
            byte[] correctKey = new byte[32];
            byte[] incorrectKey = new byte[32];
            byte[] iv = new byte[16];

            new Random().NextBytes(correctKey);
            new Random().NextBytes(incorrectKey);
            new Random().NextBytes(iv);

            // Encrypt with correct key
            byte[] encryptedData;
            using (Aes aes = Aes.Create())
            {
                aes.Mode = CipherMode.CBC;
                aes.Key = correctKey;
                aes.IV = iv;
                aes.Padding = PaddingMode.PKCS7;

                using var encryptor = aes.CreateEncryptor();
                encryptedData = encryptor.TransformFinalBlock(originalData, 0, originalData.Length);
            }

            // Act & Assert
            Assert.Throws<CryptographicException>(() => AESHelper.DecryptData(encryptedData, incorrectKey, iv, true));
        }

        [Fact]
        public void DecryptData_WithEmptyData_ThrowsException()
        {
            // Arrange
            byte[] emptyData = new byte[0];
            byte[] key = new byte[32];
            byte[] iv = new byte[16];

            new Random().NextBytes(key);
            new Random().NextBytes(iv);

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => AESHelper.DecryptData(emptyData, key, iv, false));
            Assert.Equal("Encrypted data, crypto key and initialization vector can't be empty", exception.Message);
        }

        [Fact]
        public void DecryptData_WithEmptyKey_ThrowsException()
        {
            // Arrange
            byte[] data = Encoding.UTF8.GetBytes("Test data");
            byte[] emptyKey = new byte[0];
            byte[] iv = new byte[16];

            new Random().NextBytes(iv);

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => AESHelper.DecryptData(data, emptyKey, iv, true));
            Assert.Equal("Encrypted data, crypto key and initialization vector can't be empty", exception.Message);
        }

        [Fact]
        public void DecryptData_WithEmptyIV_ThrowsException()
        {
            // Arrange
            byte[] data = Encoding.UTF8.GetBytes("Test data");
            byte[] key = new byte[32];
            byte[] emptyIV = new byte[0];

            new Random().NextBytes(key);

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => AESHelper.DecryptData(data, key, emptyIV, true));
            Assert.Equal("Encrypted data, crypto key and initialization vector can't be empty", exception.Message);
        }
    }
}
