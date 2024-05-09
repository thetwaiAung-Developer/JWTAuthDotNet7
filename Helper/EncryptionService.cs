using System.Security.Cryptography;
using System.Text;

namespace JWTAuthDotNet7.Helper
{
    public class EncryptionService
    {
        public const string PassPhrase = "D17B0CA5";

        private byte[] DeriveKeyFromPassword(string password)
        {
            var emptySalt = Array.Empty<byte>();
            var iteration = 1000;
            var desiredKeyLength = 16;
            var hashMethod = HashAlgorithmName.SHA256;

            return Rfc2898DeriveBytes.Pbkdf2(Encoding.Unicode.GetBytes(password),
                                                emptySalt,
                                                iteration,
                                                hashMethod,
                                                desiredKeyLength);
        }

        private byte[] IV =
        {
                 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08,
                 0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16
        };

        public async Task<string> EncryptAsync(string clearText)
        {
            using Aes aes = Aes.Create();
            aes.Key = DeriveKeyFromPassword(PassPhrase);
            aes.IV = IV;

            using MemoryStream output = new();
            using CryptoStream cryptoStream = new(output, aes.CreateEncryptor(), CryptoStreamMode.Write);

            await cryptoStream.WriteAsync(Encoding.Unicode.GetBytes(clearText));
            await cryptoStream.FlushFinalBlockAsync();

            return Convert.ToBase64String(output.ToArray());
        }

        public async Task<string> DecryptAsync(string encrypted)
        {
            using Aes aes = Aes.Create();
            aes.Key = DeriveKeyFromPassword(PassPhrase);
            aes.IV = IV;

            byte[] encryptedValue = Convert.FromBase64String(encrypted);
            using MemoryStream input = new(encryptedValue);
            using CryptoStream cryptoStream = new(input, aes.CreateDecryptor(), CryptoStreamMode.Read);

            using MemoryStream outPut = new();
            await cryptoStream.CopyToAsync(outPut);

            return Encoding.Unicode.GetString(outPut.ToArray());
        }
    }
}
