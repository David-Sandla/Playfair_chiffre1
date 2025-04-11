using System;
using System.Collections.Generic;
using System.Text;
using Playfair_Chiffre_Lib;

namespace PlayfairChiffre_Lib
{
    public class PlayfairChriffre : IChiffre
    {
        private char[,] matrix;
        private Dictionary<char, (int Row, int Col)> positions;
        private readonly string key;
 
        public PlayfairChriffre(string key)
        {
            this.key = key;
            BuildMatrix(key);
        }
 
        private void BuildMatrix(string key)
        {
            matrix = new char[5, 5];
            positions = new Dictionary<char, (int, int)>();
 
            // Vorbereitung: Großbuchstaben, Leerzeichen entfernen, "J" durch "I" ersetzen.
            key = key.ToUpper().Replace(" ", "").Replace("J", "I");
            bool[] used = new bool[26];
            used['J' - 'A'] = true; // Buchstabe J wird nicht verwendet.
            List<char> matrixList = new List<char>();
 
            foreach (char c in key)
            {
                if (c < 'A' || c > 'Z')
                    continue;
                int index = c - 'A';
                if (!used[index])
                {
                    used[index] = true;
                    matrixList.Add(c);
                }
            }
            for (char c = 'A'; c <= 'Z'; c++)
            {
                if (c == 'J')
                    continue;
                int index = c - 'A';
                if (!used[index])
                {
                    used[index] = true;
                    matrixList.Add(c);
                }
            }
 
            int pos = 0;
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    matrix[i, j] = matrixList[pos];
                    positions[matrixList[pos]] = (i, j);
                    pos++;
                }
            }
        }
 
        private string Preprocess(string text)
        {
            text = text.ToUpper().Replace(" ", "").Replace("J", "I");
            StringBuilder sb = new StringBuilder();
            foreach (char c in text)
            {
                if (c >= 'A' && c <= 'Z')
                    sb.Append(c);
            }
            return sb.ToString();
        }
 
        // Neuerstellung der Digraphen:
        // Wenn zwei gleiche Buchstaben hintereinander auftreten, wird ein 'X' eingefügt,
        // und nur der erste Buchstabe wird verarbeitet – der zweite Buchstabe bleibt für die nächste Runde.
        private List<string> CreateDigraphs(string text)
        {
            List<string> digraphs = new List<string>();
            int i = 0;
            while (i < text.Length)
            {
                char first = text[i];
                char second;
 
                if (i + 1 < text.Length)
                {
                    char nextChar = text[i + 1];
                    if (first == nextChar)
                    {
                        // Bei doppelten Buchstaben: füge X ein und erhöhe i nur um 1,
                        // sodass der doppelte Buchstabe in der nächsten Runde wieder als erster Buchstabe verarbeitet wird.
                        second = 'X';
                        i++; 
                    }
                    else
                    {
                        second = nextChar;
                        i += 2;
                    }
                }
                else
                {
                    // Einzelner Buchstabe am Ende => X anhängen
                    second = 'X';
                    i++;
                }
                digraphs.Add($"{first}{second}");
            }
            return digraphs;
        }
 
        public string Encrypt(string msg)
        {
            string preprocessed = Preprocess(msg);
            List<string> digraphs = CreateDigraphs(preprocessed);
            StringBuilder cipherText = new StringBuilder();
 
            foreach (string digraph in digraphs)
            {
                char a = digraph[0];
                char b = digraph[1];
                var (rowA, colA) = positions[a];
                var (rowB, colB) = positions[b];
 
                if (rowA == rowB)
                {
                    // Gleiche Zeile: jeweils Buchstabe rechts (mit Wrap-around)
                    int newColA = (colA + 1) % 5;
                    int newColB = (colB + 1) % 5;
                    cipherText.Append(matrix[rowA, newColA]);
                    cipherText.Append(matrix[rowB, newColB]);
                }
                else if (colA == colB)
                {
                    // Gleiche Spalte: jeweils Buchstabe darunter (mit Wrap-around)
                    int newRowA = (rowA + 1) % 5;
                    int newRowB = (rowB + 1) % 5;
                    cipherText.Append(matrix[newRowA, colA]);
                    cipherText.Append(matrix[newRowB, colB]);
                }
                else
                {
                    // Rechteckregel: Spalten tauschen.
                    cipherText.Append(matrix[rowA, colB]);
                    cipherText.Append(matrix[rowB, colA]);
                }
            }
            return cipherText.ToString();
        }
 
        public string Decrypt(string msg)
        {
            msg = msg.ToUpper().Replace(" ", "");
            StringBuilder plainText = new StringBuilder();
 
            for (int i = 0; i < msg.Length; i += 2)
            {
                char a = msg[i];
                char b = msg[i + 1];
                var (rowA, colA) = positions[a];
                var (rowB, colB) = positions[b];
 
                if (rowA == rowB)
                {
                    // Gleiche Zeile: Buchstabe links (mit Wrap-around)
                    int newColA = (colA + 4) % 5;
                    int newColB = (colB + 4) % 5;
                    plainText.Append(matrix[rowA, newColA]);
                    plainText.Append(matrix[rowB, newColB]);
                }
                else if (colA == colB)
                {
                    // Gleiche Spalte: Buchstabe oberhalb (mit Wrap-around)
                    int newRowA = (rowA + 4) % 5;
                    int newRowB = (rowB + 4) % 5;
                    plainText.Append(matrix[newRowA, colA]);
                    plainText.Append(matrix[newRowB, colB]);
                }
                else
                {
                    // Rechteckregel: Spalten tauschen.
                    plainText.Append(matrix[rowA, colB]);
                    plainText.Append(matrix[rowB, colA]);
                }
            }
            return plainText.ToString();
        }
    }
}