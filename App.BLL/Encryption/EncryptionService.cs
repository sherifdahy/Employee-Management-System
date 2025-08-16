using App.Entities.Helper;
using Microsoft.Extensions.Options;
using OpenQA.Selenium;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace App.BLL
{
    public class EncryptionService : IEncryptionService
    {
        private readonly IOptions<EncryptionSettings> _options;

        public EncryptionService(IOptions<EncryptionSettings> options)
        {
            _options = options;
        }
        private byte[] GenerateKey()
        {
            using var keyDerivationFunction = new Rfc2898DeriveBytes(_options.Value.Password, Encoding.UTF8.GetBytes(_options.Value.Salt), 100_000, HashAlgorithmName.SHA256);
            return keyDerivationFunction.GetBytes(32); // AES-256
        }
        public string Encrypt(string plainText)
        {
            using var aes = Aes.Create();
            aes.Key = GenerateKey();
            aes.GenerateIV();
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            using var encryptor = aes.CreateEncryptor();
            var plainBytes = Encoding.UTF8.GetBytes(plainText);
            var encryptedBytes = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);

            // Combine IV + cipher text
            var combinedBytes = aes.IV.Concat(encryptedBytes).ToArray();
            return Convert.ToBase64String(combinedBytes);
        }

        public string Decrypt(string encryptedText)
        {
            var fullBytes = Convert.FromBase64String(encryptedText);
            var iv = fullBytes.Take(16).ToArray();
            var cipherBytes = fullBytes.Skip(16).ToArray();

            using var aes = Aes.Create();
            aes.Key = GenerateKey();
            aes.IV = iv;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            using var decryptor = aes.CreateDecryptor();
            var decryptedBytes = decryptor.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);

            return Encoding.UTF8.GetString(decryptedBytes);
        }
    }

}
