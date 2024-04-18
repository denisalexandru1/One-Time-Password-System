using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using OTPApp.Controllers;
using OTPApp.Models;

namespace OTPApp.Tests
{
    [TestFixture]
    public class OTPControllerTests
    {
        [Test]
        public async Task GenerateOTP_ValidOTP_ReturnsOkResult()
        {
            // Arrange
            var controller = new OTPController();

            // Act
            var result = await controller.GenerateOTP(20);

            // Assert
            Assert.That(result, Is.InstanceOf<OkObjectResult>());

            var okResult = result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null);
            Assert.That(okResult.Value, Is.InstanceOf<OtpModel>());
            var otpModel = okResult.Value as OtpModel;
            Assert.That(otpModel, Is.Not.Null);
            Assert.That(otpModel.Otp, Is.Not.Null);
            Assert.That(otpModel.Otp, Is.Not.Empty);
        }

        [Test]
        public async Task GenerateOTP_TimeBoundMissing_ReturnsOkResult()
        {
            // Arrange
            var controller = new OTPController();

            // Act
            var result = await controller.GenerateOTP();

            // Assert
            Assert.That(result, Is.InstanceOf<OkObjectResult>());

            var okResult = result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null);
            Assert.That(okResult.Value, Is.InstanceOf<OtpModel>());
            var otpModel = okResult.Value as OtpModel;
            Assert.That(otpModel, Is.Not.Null);
            Assert.That(otpModel.Otp, Is.Not.Null);
            Assert.That(otpModel.Otp, Is.Not.Empty);
        }

        [Test]
        public async Task GenerateOTP_TimeBoundZero_ReturnsBadRequest()
        {
            // Arrange
            var controller = new OTPController();

            // Act
            var result = await controller.GenerateOTP(0);

            // Assert
            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
            var badRequestResult = result as BadRequestObjectResult;
            Assert.That(badRequestResult, Is.Not.Null);
            Assert.That("Time bound cannot be negative or zero", Is.EqualTo(badRequestResult.Value));
        }

        [Test]
        public async Task GenerateOTP_TimeBoundNegative_ReturnsBadRequest()
        {
            // Arrange
            var controller = new OTPController();

            // Act
            var result = await controller.GenerateOTP(-10);

            // Assert
            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
            var badRequestResult = result as BadRequestObjectResult;
            Assert.That(badRequestResult, Is.Not.Null);
            Assert.That("Time bound cannot be negative or zero", Is.EqualTo(badRequestResult.Value));
        }

        [Test]
        public async Task ValidateOTP_NullOTP_ReturnsBadRequest()
        {
            // Arrange
            var controller = new OTPController();

            // Act
            var result = await controller.ValidateOTP(null);

            // Assert
            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
            var badRequestResult = result as BadRequestObjectResult;
            Assert.That(badRequestResult, Is.Not.Null);
            Assert.That("OTP is required", Is.EqualTo(badRequestResult.Value));
        }

        [Test]
        public async Task ValidateOTP_EmptyOTP_ReturnsBadRequest()
        {
            // Arrange
            var controller = new OTPController();

            // Act
            var result = await controller.ValidateOTP(new OtpModel());

            // Assert
            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
            var badRequestResult = result as BadRequestObjectResult;
            Assert.That(badRequestResult, Is.Not.Null);
            Assert.That("OTP is required", Is.EqualTo(badRequestResult.Value));
        }
    }
}
