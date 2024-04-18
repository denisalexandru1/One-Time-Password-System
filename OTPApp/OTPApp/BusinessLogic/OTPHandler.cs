namespace OTPApp.BusinessLogic
{
    public class OTPHandler
    {
        public static string GenerateOTP(int timeBound)
        {
            if (timeBound <= 0)
            {
                throw new ArgumentException("Time bound cannot be negative or zero");
            }

            string dateTime = DateTime.UtcNow.ToString("yyyyMMddHHmmss");

            Random random = new Random();

            string randomChars = new string("");
            for (int i = 0; i < 12; i++)
            {
                randomChars += (char)random.Next(33, 126);
            }

            return randomChars.Substring(0, 6) + dateTime + randomChars.Substring(6, 6) + timeBound;
        }

        public static bool ValidateOTP(string otp)
        {
            if (otp.Length < 26)
            {
                return false;
            }

            double timeBound = double.Parse(otp.Substring(26));
            if (timeBound <= 0)
            {
                return false;
            }

            string otpDateTime = otp.Substring(6, 14);

            string currentDateTime = DateTime.UtcNow.ToString("yyyyMMddHHmmss");

            DateTime dateTime1;
;           DateTime dateTime2;

            try
            {
                dateTime1 = DateTime.ParseExact(otpDateTime, "yyyyMMddHHmmss", System.Globalization.CultureInfo.InvariantCulture);
                dateTime2 = DateTime.ParseExact(currentDateTime, "yyyyMMddHHmmss", System.Globalization.CultureInfo.InvariantCulture);
            }
            catch (FormatException)
            {
                return false;
            }

            double difference = (dateTime2 - dateTime1).TotalMinutes;
            return timeBound > difference;
        }
    }
}
