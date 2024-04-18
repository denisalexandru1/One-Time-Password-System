using System.Security.Cryptography;

namespace OTPApp.BusinessLogic
{
    public class OTPCryptographer
    {
        static string secretKey = "1234567890123456"; // 16 characters secret key, can be any string of 16 characters
        public static string Encrypt(string otp)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = System.Text.Encoding.UTF8.GetBytes(secretKey);
                aes.IV = System.Text.Encoding.UTF8.GetBytes(secretKey);

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                byte[] encryptedOTP;
                using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        using (System.IO.StreamWriter sw = new System.IO.StreamWriter(cs))
                        {
                            sw.Write(otp);
                        }
                        encryptedOTP = ms.ToArray();
                    }
                }
                return System.Convert.ToBase64String(encryptedOTP);
            }
        }

        public static string Decrypt(string otp)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = System.Text.Encoding.UTF8.GetBytes(secretKey);
                aes.IV = System.Text.Encoding.UTF8.GetBytes(secretKey);

                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                byte[] encryptedOTP = System.Convert.FromBase64String(otp);
                string decryptedOTP;
                using (System.IO.MemoryStream ms = new System.IO.MemoryStream(encryptedOTP))
                {
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        using (System.IO.StreamReader sr = new System.IO.StreamReader(cs))
                        {
                            decryptedOTP = sr.ReadToEnd();
                        }
                    }
                }
                return decryptedOTP;
            }
        }
    }
}
