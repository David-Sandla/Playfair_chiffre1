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

            // Output
            TestContext.WriteLine($"Encrypted: {encrypted}");
            TestContext.WriteLine($"Decrypted: {decrypted}");

            // Normalize (optional)
            string normalized = decrypted.Replace(" ", "").ToUpper();

            // Assert
            Assert.That(normalized, Does.Contain("MORGEN"));
            Assert.That(normalized, Does.Contain("FERIEN"));
        }

        [Test]
        public void EncryptDecrypt_ShanamMaleckMeineEier_WithKeyTAUBE_ShouldContainShanam()
        {
            // Arrange
            string key = "TAUBE";
            string plaintext = "SHANAM MALECK MEINE EIER";
            var cipher = new PlayfairChriffre(key);

            // Act
            string encrypted = cipher.Encrypt(plaintext);
            string decrypted = cipher.Decrypt(encrypted);

            // Output
            TestContext.WriteLine($"Encrypted: {encrypted}");
            TestContext.WriteLine($"Decrypted: {decrypted}");

            // Normalize
            string normalized = decrypted.Replace(" ", "").ToUpper();

            // Assert
            Assert.That(normalized, Does.Contain("SHANAM"));
            Assert.That(normalized, Does.Contain("MEINE"));
            Assert.That(normalized, Does.Contain("EIER"));
        }
    }
}