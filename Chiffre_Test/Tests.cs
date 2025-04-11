using System;
using NUnit.Framework;
using Playfair_Chiffre_Lib;

namespace Chiffre_Test{
    [TestFixture]
    public class Tests{
        [Test]
        public void EncryptDecrypt_MorgenSindFerien_WithKeyFISCH_ShouldMatch()
        {
            var key = "FISCH";
            var plaintext = "MORGEN SIND FERIEN";
            var cipher = new PlayfairChriffre(key);

            var encrypted = cipher.Encrypt(plaintext);
            var decrypted = cipher.Decrypt(encrypted);

            Assert.Contains("MORGEN", new[]{ decrypted });
            Assert.Contains("FERIEN", new[]{ decrypted });
        }
    }
}