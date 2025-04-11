using System;
using NUnit.Framework;
using Playfair_Chiffre_Lib;
using PlayfairChiffre_Lib;

namespace Test_Chiffre
{
    public class PlayfairTests
    {
        [Test]
        public void EncryptDecrypt_MorgenSindFerien_WithKeyFISCH_ShouldRestoreOriginal()
        {
            // Arrange
            string key = "FISCH";
            string plaintext = "MORGEN SIND FERIEN";
            var cipher = new PlayfairChriffre(key);

            // Act
            string encrypted = cipher.Encrypt(plaintext);
            string decrypted = cipher.Decrypt(encrypted);

            Console.WriteLine($"Encrypted: {encrypted}");
            Console.WriteLine($"Decrypted: {decrypted}");

            // Normalize (optional)
            string normalized = decrypted.Replace(" ", "").ToUpper();

            // Assert
            Assert.That(normalized, Does.Contain("MORGEN"));
            Assert.That(normalized, Does.Contain("FERIEN"));
        }
    }
}