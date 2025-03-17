using Microsoft.AspNetCore.Http;
using System.Text;

namespace CSharpTestTasks.Algorithms
{
    public class StringAlgorithms
    {
        public static string ReverseString(string s)
        {
            char[] newString;
            if ((s.Length & 1) == 0)
            {
                newString = new char[s.Length];
                int substringLength = s.Length / 2;

                s.CopyTo(newString);

                newString.AsSpan(0, substringLength).Reverse();
                newString.AsSpan(substringLength, substringLength).Reverse();
            }
            else
            {
                newString = new char[s.Length * 2];

                s.CopyTo(newString.AsSpan(0));
                s.CopyTo(newString.AsSpan(s.Length));

                newString.AsSpan(0, s.Length).Reverse();
            }

            return new string(newString);
        }

        public static bool HasNonAcceptedSymbols(string s, out string nonAcceptedSymbols)
        {
            bool panic = false;
            StringBuilder symbols = new();

            foreach (char c in s)
            {
                if (!"abcdefghijklmnopqrstuvwxyz".Contains(c))
                {
                    symbols.Append(c);

                    panic = true;
                }
            }

            nonAcceptedSymbols = symbols.ToString();

            return panic;
        }

        public static void SymbolCount(string s, Dictionary<char, int> count)
        {
            foreach(char c in s)
            {
                if (count.ContainsKey(c)) count[c]++;
                else count[c] = 1;
            }
        }

        public static string? LongestSubstring(string s)
        {
            int substringStart;
            int substringEnd;

            // Scan string for vowel from the left
            for (substringStart = 0;
                substringStart < s.Length && !("aeiouy".Contains(s[substringStart]));
                substringStart++)
            { }

            // Scan string for vowel from the right
            for (substringEnd = s.Length - 1;
                substringEnd >= 0 && !("aeiouy".Contains(s[substringEnd]));
                substringEnd--)
            { }

            if (substringStart < substringEnd)
                return s.Substring(substringStart, substringEnd - substringStart + 1);

            return null;
        }
    }
}
