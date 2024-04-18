using NUnit.Framework;
using OTPApp.BusinessLogic;

namespace OTPApp.Tests
{
    [TestFixture]
    public class OTPHandlerTests
    {
        [Test]
        public void GenerateOTP_WhenCalled_ReturnsOTP()
        {
            // Arrange
            int timeBound = 30;

            // Act
            string otp = OTPHandler.GenerateOTP(timeBound);

            // Assert
            Assert.That(otp, Is.Not.Null);
        }

        [Test]
        public void GenerateOTP_WhenCalled_ReturnsOTPTemplate()
        {
            // Arrange
            int timeBound = 30;

            // Act
            string otp = OTPHandler.GenerateOTP(timeBound);

            // Assert
            Assert.That(otp, Does.Match(@"[!-~]{6}\d{14}[!-~]{6}30"));
        }

        [Test]
        public void GenerateOTP_DifferentTimeBoundLength_ReturnsOTPTemplate()
        {
            // Arrange

            // Act
            string otp1 = OTPHandler.GenerateOTP(5);
            string otp2 = OTPHandler.GenerateOTP(30);
            string otp3 = OTPHandler.GenerateOTP(100);
            string otp4 = OTPHandler.GenerateOTP(400);
            string otp5 = OTPHandler.GenerateOTP(5000);

            // Assert
            Assert.That(otp1, Does.Match(@"[!-~]{6}\d{14}[!-~]{6}5"));
            Assert.That(otp2, Does.Match(@"[!-~]{6}\d{14}[!-~]{6}30"));
            Assert.That(otp3, Does.Match(@"[!-~]{6}\d{14}[!-~]{6}100"));
            Assert.That(otp4, Does.Match(@"[!-~]{6}\d{14}[!-~]{6}400"));
            Assert.That(otp5, Does.Match(@"[!-~]{6}\d{14}[!-~]{6}5000"));
        }

        [Test]
        public void GenerateOTP_NegativeTimeBound_ThrowsException()
        {
            // Arrange
            int timeBound = -30;

            // Act

            // Assert
            Assert.That(() => OTPHandler.GenerateOTP(timeBound), Throws.ArgumentException);
        }

        [Test]
        public void GenerateOTP_ZeroTimeBound_ThrowsException()
        {
            // Arrange
            int timeBound = 0;

            // Act

            // Assert
            Assert.That(() => OTPHandler.GenerateOTP(timeBound), Throws.ArgumentException);
        }

        [Test]
        public void ValidateOTP_ValidOTP_ReturnsTrue()
        {
            // Arrange
            string otp = OTPHandler.GenerateOTP(5);
            
            // Act

            // Assert
            Assert.That(OTPHandler.ValidateOTP(otp), Is.True);
        }

        [Test]
        public void ValidateOTP_ExpiredOTP_ReturnsFalse()
        {
            // Arrange
            string otp = "6T9tba20230418063506Q1lw/F5";

            // Act

            // Assert
            Assert.That(OTPHandler.ValidateOTP(otp), Is.False);
        }

        [Test]
        public void ValidateOTP_ShortOTP_ReturnsFalse()
        {
            // Arrange
            string otp = "123456abcd";

            // Act

            // Assert
            Assert.That(OTPHandler.ValidateOTP(otp), Is.False);
        }

        [Test]
        public void ValidateOTP_InvalidOTPDateTime_ReturnsFalse()
        {
            // Arrange
            string otp = "6T9tba202gd418063506Q1lw";

            // Act

            // Assert
            Assert.That(OTPHandler.ValidateOTP(otp), Is.False);
        }
    }
}
