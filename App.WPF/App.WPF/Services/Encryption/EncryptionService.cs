using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace App.UI.Services.Encryption
{
    public class EncryptionService : IEncryptionService
    {
        private readonly string _key;

        public EncryptionService(string key)
        {
            _key = key.PadRight(32);
        }

        public string Encrypt(string plainText)
        {
            using var aes = Aes.Create();
            aes.Key = Encoding.UTF8.GetBytes(_key);
            aes.GenerateIV();

            var iv = aes.IV;
            var encryptor = aes.CreateEncryptor();

            var plainBytes = Encoding.UTF8.GetBytes(plainText);
            var encryptedBytes = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);

            var result = Convert.ToBase64String(iv.Concat(encryptedBytes).ToArray());
            return result;
        }

        public string Decrypt(string encryptedText)
        {
            var fullBytes = Convert.FromBase64String(encryptedText);
            var iv = fullBytes.Take(16).ToArray();
            var cipherText = fullBytes.Skip(16).ToArray();

            using var aes = Aes.Create();
            aes.Key = Encoding.UTF8.GetBytes(_key);
            aes.IV = iv;

            var decryptor = aes.CreateDecryptor();
            var decryptedBytes = decryptor.TransformFinalBlock(cipherText, 0, cipherText.Length);

            return Encoding.UTF8.GetString(decryptedBytes);
        }
    }
}
