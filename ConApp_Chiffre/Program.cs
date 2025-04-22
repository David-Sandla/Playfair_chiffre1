using System;
using Playfair_Chiffre_Lib;
using PlayfairChiffre_Lib;

namespace ConApp_Chiffre

{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Playfair Chriffre Test");

            Console.Write("Bitte das Schlüsselwort eingeben: ");
            string key = Console.ReadLine();

            // Instanziere die Verschlüsselungslogik mit dem eingegebenen Key
            IChiffre chiffre = new PlayfairChriffre(key);

            // Menü anzeigen
            Console.WriteLine("\nOptionen:");
            Console.WriteLine("1: Verschlüsseln");
            Console.WriteLine("2: Entschlüsseln");
            Console.Write("Bitte wählen Sie eine Option: ");
            string option = Console.ReadLine();

            if (option == "1")
            {
                Console.Write("\nGeben Sie den zu verschlüsselnden Text ein: ");
                string plaintext = Console.ReadLine();
                string encrypted = chiffre.Encrypt(plaintext);
                Console.WriteLine("Verschlüsselter Text: " + encrypted);
            }
            else if (option == "2")
            {
                Console.Write("\nGeben Sie den zu entschlüsselnden Text ein: ");
                string cipherText = Console.ReadLine();
                string decrypted = chiffre.Decrypt(cipherText);
                Console.WriteLine("Entschlüsselter Text: " + decrypted);
            }
            else
            {
                Console.WriteLine("\nUngültige Option!");
            }

            Console.WriteLine("\nDrücken Sie eine beliebige Taste zum Beenden...");
            Console.ReadKey();
        }
    }
}