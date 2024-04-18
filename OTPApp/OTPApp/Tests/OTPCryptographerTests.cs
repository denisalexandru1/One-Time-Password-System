using NUnit.Framework;
using OTPApp.BusinessLogic;

namespace OTPApp.Tests
{
    [TestFixture]
    public class OTPCryptographerTests
    {
        [Test]
        public void Encrypt_WhenCalled_ReturnsEncryptedOTP()
        {
            // Arrange
            string otp = "6T9tba20240418063506Q1lw/F5";

            // Act
            string encryptedOTP = OTPCryptographer.Encrypt(otp);

            // Assert
            Assert.That(encryptedOTP, Is.Not.Null);
            Console.WriteLine(encryptedOTP);
        }

        [Test]
        public void Encrypt_WhenCalled_ReturnsEncryptedOTPString()
        {
            // Arrange
            string otp = "6T9tba20240418063506Q1lw/F5";

            // Act
            string encryptedOTP = OTPCryptographer.Encrypt(otp);

            // Assert
            Assert.That(encryptedOTP, Does.Match(@"[A-Za-z0-9+/]{4,}={0,2}"));
        }

        [Test]
        public void Decrypt_WhenCalled_ReturnsDecryptedOTP()
        {
            // Arrange
            string encrptedOTP = "V4GNiHGg+YZ8DowTJodAGG9U2mUzwab94zGTRHYVefg=";

            // Act
            string decryptedOTP = OTPCryptographer.Decrypt(encrptedOTP);

            // Assert
            Assert.That(decryptedOTP, Is.Not.Null);
        }

        [Test]
        public void Decrypt_WhenCalled_ReturnsDecryptedOTPString()
        {
            // Arrange
            string encrptedOTP = "V4GNiHGg+YZ8DowTJodAGG9U2mUzwab94zGTRHYVefg=";

            // Act
            string decryptedOTP = OTPCryptographer.Decrypt(encrptedOTP);

            // Assert
            Assert.That(decryptedOTP, Does.Match(@"[!-~]{6}\d{14}[!-~]{6}\d+"));
        }
    }
}
