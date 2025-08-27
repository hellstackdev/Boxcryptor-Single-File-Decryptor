using System;
using System.Text;
using BCFileDecryptorCore;
using Xunit;

namespace BCFileDecryptorTests
{
    public class PBKDF2HelperTests
    {
        [Fact]
        public void GetBytes_WithValidParameters_ReturnsExpectedLength()
        {
            // Arrange
            string password = "TestPassword123";
            byte[] salt = new byte[16];
            new Random().NextBytes(salt);
            int iterations = 1000;
            int byteCount = 64;

            PBKDF2Helper pbkdf2 = new PBKDF2Helper(password, salt, iterations);

            // Act
            byte[] result = pbkdf2.GetBytes(byteCount);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(byteCount, result.Length);
        }

        [Fact]
        public void GetBytes_WithNegativeByteCount_ThrowsException()
        {
            // Arrange
            string password = "TestPassword123";
            byte[] salt = new byte[16];
            new Random().NextBytes(salt);
            int iterations = 1000;
            int negativeByteCount = -1;

            PBKDF2Helper pbkdf2 = new PBKDF2Helper(password, salt, iterations);

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => pbkdf2.GetBytes(negativeByteCount));
            Assert.Equal("Parameter 'count' can't be zero or smaller", exception.Message);
        }

        [Fact]
        public void GetBytes_WithZeroByteCount_ThrowsException()
        {
            // Arrange
            string password = "TestPassword123";
            byte[] salt = new byte[16];
            new Random().NextBytes(salt);
            int iterations = 1000;
            int zeroByteCount = 0;

            PBKDF2Helper pbkdf2 = new PBKDF2Helper(password, salt, iterations);

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => pbkdf2.GetBytes(zeroByteCount));
            Assert.Equal("Could not derive bytes with PBKDF2", exception.Message);
        }

        [Fact]
        public void GetBytes_SameInputs_ReturnsSameResult()
        {
            // Arrange
            string password = "TestPassword123";
            byte[] salt = Encoding.UTF8.GetBytes("FixedSalt12345678"); // Using fixed salt for predictable results
            int iterations = 1000;
            int byteCount = 32;

            PBKDF2Helper pbkdf2_1 = new PBKDF2Helper(password, salt, iterations);
            PBKDF2Helper pbkdf2_2 = new PBKDF2Helper(password, salt, iterations);

            // Act
            byte[] result1 = pbkdf2_1.GetBytes(byteCount);
            byte[] result2 = pbkdf2_2.GetBytes(byteCount);

            // Assert
            Assert.Equal(result1, result2);
        }

        [Fact]
        public void GetBytes_DifferentPasswords_ReturnDifferentResults()
        {
            // Arrange
            string password1 = "TestPassword1";
            string password2 = "TestPassword2";
            byte[] salt = Encoding.UTF8.GetBytes("FixedSalt12345678");
            int iterations = 1000;
            int byteCount = 32;

            PBKDF2Helper pbkdf2_1 = new PBKDF2Helper(password1, salt, iterations);
            PBKDF2Helper pbkdf2_2 = new PBKDF2Helper(password2, salt, iterations);

            // Act
            byte[] result1 = pbkdf2_1.GetBytes(byteCount);
            byte[] result2 = pbkdf2_2.GetBytes(byteCount);

            // Assert
            Assert.NotEqual(result1, result2);
        }

        [Fact]
        public void GetBytes_DifferentSalts_ReturnDifferentResults()
        {
            // Arrange
            string password = "TestPassword";
            byte[] salt1 = Encoding.UTF8.GetBytes("FixedSalt12345678");
            byte[] salt2 = Encoding.UTF8.GetBytes("DiffrnSalt12345678");
            int iterations = 1000;
            int byteCount = 32;

            PBKDF2Helper pbkdf2_1 = new PBKDF2Helper(password, salt1, iterations);
            PBKDF2Helper pbkdf2_2 = new PBKDF2Helper(password, salt2, iterations);

            // Act
            byte[] result1 = pbkdf2_1.GetBytes(byteCount);
            byte[] result2 = pbkdf2_2.GetBytes(byteCount);

            // Assert
            Assert.NotEqual(result1, result2);
        }
    }
}
