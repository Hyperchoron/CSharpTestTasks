﻿namespace CSharpTestTasks
{
    internal class Program
    {
        private static string alphabet = "abcdefghijklmnopqrstuvwxyz";

        static void Main(string[] args)
        {
            string s = Console.ReadLine() ?? "";

            bool panic = false;
            foreach (char c in s)
            {
                if (alphabet.IndexOf(c) == -1)
                {
                    if (!panic)
                    {
                        Console.Write("Были введены неподходящие символы: ");

                        panic = true;
                    }

                    Console.Write(c);
                }
            }

            if (panic)
            {
                Console.WriteLine();

                return;
            }

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

            Console.WriteLine(newString);
        }
    }
}
