using Playfair_Chiffre_Lib;

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

            // Debug (optional)
            Console.WriteLine($"Encrypted: {encrypted}");
            Console.WriteLine($"Decrypted: {decrypted}");

            // Assert (ohne Leerzeichen, evtl. 'X' eingefügt – deshalb Teilvergleich)
            Assert.Contains("MORGEN", new[]{ decrypted });
            Assert.Contains("FERIEN", new[]{ decrypted });
        }
    }
}