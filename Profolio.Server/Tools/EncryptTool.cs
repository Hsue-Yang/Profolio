using System.Security.Cryptography;
using System.Text;

namespace Profolio.Server.Tools
{
	public class EncryptTool
	{
		private static string Encrypt_Key = "";

		public static void SetEncryptKey(string key)
		{
			Encrypt_Key = key;
		}

        public static string AESEncrypt(string strInput)
        {
            string result = string.Empty;
            try
            {
                Aes aes = Aes.Create();
                MD5 mD = MD5.Create();
                SHA256 sHA = SHA256.Create();
                byte[] key = sHA.ComputeHash(Encoding.UTF8.GetBytes(Encrypt_Key));
                byte[] iV = mD.ComputeHash(Encoding.UTF8.GetBytes(Encrypt_Key));
                aes.Key = key;
                aes.IV = iV;
                byte[] bytes = Encoding.UTF8.GetBytes(strInput);
                using MemoryStream memoryStream = new MemoryStream();
                using CryptoStream cryptoStream = new CryptoStream(memoryStream, aes.CreateEncryptor(), CryptoStreamMode.Write);
                cryptoStream.Write(bytes, 0, bytes.Length);
                cryptoStream.FlushFinalBlock();
                result = Convert.ToBase64String(memoryStream.ToArray());
                result = result.Replace("/", "_").Replace("+", "-"); // Replace / and + to avoid route issues
            }
            catch (Exception ex)
            {
                _ = ex.Message;
            }

            return result;
        }

        public static string AESDecrypt(string strInput)
        {
            string result = string.Empty;
            try
            {
                strInput = strInput.Replace("_", "/").Replace("-", "+"); // Revert the replacements
                Aes aes = Aes.Create();
                MD5 mD = MD5.Create();
                SHA256 sHA = SHA256.Create();
                byte[] key = sHA.ComputeHash(Encoding.UTF8.GetBytes(Encrypt_Key));
                byte[] iV = mD.ComputeHash(Encoding.UTF8.GetBytes(Encrypt_Key));
                aes.Key = key;
                aes.IV = iV;
                byte[] array = Convert.FromBase64String(strInput);
                using MemoryStream memoryStream = new MemoryStream();
                using CryptoStream cryptoStream = new CryptoStream(memoryStream, aes.CreateDecryptor(), CryptoStreamMode.Write);
                cryptoStream.Write(array, 0, array.Length);
                cryptoStream.FlushFinalBlock();
                result = Encoding.UTF8.GetString(memoryStream.ToArray());
            }
            catch (Exception ex)
            {
                _ = ex.Message;
            }

            return result;
        }

		public static string BCryptHash(string password)
		{
			string salt = BCrypt.Net.BCrypt.GenerateSalt();
			return BCrypt.Net.BCrypt.HashPassword(password, salt);
		}

		public static bool BCryptVerify(string originalPassword, string hashPassword)
		{
			return BCrypt.Net.BCrypt.Verify(originalPassword, hashPassword);
		}
	}
}