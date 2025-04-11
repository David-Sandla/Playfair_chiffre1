using System;
using System.Collections.Generic;
using System.Text;
using Playfair_Chiffre_Lib;

namespace Playfair_Chiffre_Lib
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

            key = key.ToUpper().Replace(" ", "").Replace("J", "I");
            bool[] used = new bool[26];
            used['J' - 'A'] = true;
            List<char> matrixList = new List<char>();

            foreach (char c in key)
            {
                if (c < 'A' || c > 'Z') continue;
                int index = c - 'A';
                if (!used[index])
                {
                    used[index] = true;
                    matrixList.Add(c);
                }
            }

            for (char c = 'A'; c <= 'Z'; c++)
            {
                if (c == 'J') continue;
                int index = c - 'A';
                if (!used[index])
                {
                    used[index] = true;
                    matrixList.Add(c);
                }
            }

            int pos = 0;
            for (int i = 0; i < 5; i++)
                for (int j = 0; j < 5; j++)
                {
                    matrix[i, j] = matrixList[pos];
                    positions[matrixList[pos]] = (i, j);
                    pos++;
                }
        }

        private string Preprocess(string text)
        {
            text = text.ToUpper().Replace(" ", "").Replace("J", "I");
            StringBuilder sb = new StringBuilder();
            foreach (char c in text)
                if (c >= 'A' && c <= 'Z')
                    sb.Append(c);
            return sb.ToString();
        }

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
                    second = text[i + 1];
                    if (first == second)
                    {
                        second = 'X';
                        i++;
                    }
                    else
                    {
                        i += 2;
                    }
                }
                else
                {
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
                    cipherText.Append(matrix[rowA, (colA + 1) % 5]);
                    cipherText.Append(matrix[rowB, (colB + 1) % 5]);
                }
                else if (colA == colB)
                {
                    cipherText.Append(matrix[(rowA + 1) % 5, colA]);
                    cipherText.Append(matrix[(rowB + 1) % 5, colB]);
                }
                else
                {
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
                    plainText.Append(matrix[rowA, (colA + 4) % 5]);
                    plainText.Append(matrix[rowB, (colB + 4) % 5]);
                }
                else if (colA == colB)
                {
                    plainText.Append(matrix[(rowA + 4) % 5, colA]);
                    plainText.Append(matrix[(rowB + 4) % 5, colB]);
                }
                else
                {
                    plainText.Append(matrix[rowA, colB]);
                    plainText.Append(matrix[rowB, colA]);
                }
            }

            return plainText.ToString();
        }
    }
}