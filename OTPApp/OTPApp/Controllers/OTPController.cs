using Microsoft.AspNetCore.Mvc;
using OTPApp.BusinessLogic;
using OTPApp.Models;

namespace OTPApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OTPController : ControllerBase
    {
        [HttpGet("generate", Name = "Generate")]
        public async Task<IActionResult> GenerateOTP(int timeBound = 30)
        {
            if (timeBound <= 0)
            {
                return BadRequest("Time bound cannot be negative or zero");
            }
            string otp = OTPHandler.GenerateOTP(timeBound);
            OtpModel otpModel = new OtpModel { Otp = OTPCryptographer.Encrypt(otp) };
            return Ok(otpModel);
        }

        [HttpPost("validate", Name = "Validate")]
        public async Task<IActionResult> ValidateOTP([FromBody] OtpModel otpModel)
        {
            if (otpModel == null || string.IsNullOrEmpty(otpModel.Otp))
            {
                return BadRequest("OTP is required");
            }
            string decryptedOTP = OTPCryptographer.Decrypt(otpModel.Otp);
            return Ok(OTPHandler.ValidateOTP(decryptedOTP));
        }
    }
}
