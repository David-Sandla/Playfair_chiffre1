using System;
using NUnit.Framework;
using Playfair_Chiffre_Lib;
using PlayfairChiffre_Lib;

namespace Test_Chiffre
{
    public class PlayfairTests
    {
        [Test]
        public void EncryptDecrypt_MorgenSindFerien()
        {
            string key = "FISCH";
            string text = "MORGEN SIND FERIEN";
            var cipher = new PlayfairChriffre(key);
            
            string encrypted = cipher.Encrypt(text);
            string decrypted = cipher.Decrypt(encrypted);
            
            TestContext.WriteLine($"Encrypted: {encrypted}");
            TestContext.WriteLine($"Decrypted: {decrypted}");
            
            string normal = decrypted.Replace(" ", "").ToUpper();
            
            Assert.That(normal, Does.Contain("MORGEN"));
            Assert.That(normal, Does.Contain("FERIEN"));
        }

        [Test]
        // Der Unit Test war Shanams Idee und ich soll ihn so lassen 
        public void EncryptDecrypt_ShanamMaleckMeineEier()
        {
            string key = "TAUBE";
            string text = "SHANAM MALECK MEINE EIER";
            var cipher = new PlayfairChriffre(key);
            
            string encrypted = cipher.Encrypt(text);
            string decrypted = cipher.Decrypt(encrypted);
            
            TestContext.WriteLine($"Encrypted: {encrypted}");
            TestContext.WriteLine($"Decrypted: {decrypted}");
            
            string normal = decrypted.Replace(" ", "").ToUpper();
            
            Assert.That(normal, Does.Contain("SHANAM"));
            Assert.That(normal, Does.Contain("MALECK"));
            Assert.That(normal, Does.Contain("MEINE"));
            Assert.That(normal, Does.Contain("EIER"));
        }
        
        [Test]
        public void EncryptDecrypt_BerndReiterWirdImmerBreiter()
        {
            string key = "BUTOLEN";
            string text = "BERND REITER WIRD IMMER BREITER";
            var cipher = new PlayfairChriffre(key);
            
            string encrypted = cipher.Encrypt(text);
            string decrypted = cipher.Decrypt(encrypted);
            
            TestContext.WriteLine($"Encrypted: {encrypted}");
            TestContext.WriteLine($"Decrypted: {decrypted}");
            
            string normal = decrypted.Replace(" ", "").ToUpper();
            
            Assert.That(normal, Does.Contain("BERND"));
            Assert.That(normal, Does.Contain("REITER"));
            Assert.That(normal, Does.Contain("WIRD"));
            Assert.That(normal, Does.Contain("IMXMER"));
            Assert.That(normal, Does.Contain("BREITER"));

        }
        
    }
}