namespace CSharpTestTasks
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

            if ((s.Length & 1) == 0) // Количество чётное.
            {
                int substringLength = s.Length >> 1;

                string firstSubString = new string(s.Substring(0, substringLength).Reverse().ToArray());
                string secondSubString = new string(s.Substring(substringLength, substringLength).Reverse().ToArray());

                Console.WriteLine(firstSubString + secondSubString);
            }
            else
            {
                string reversedString = new string(s.Reverse().ToArray());

                Console.WriteLine(reversedString + s);
            }
        }
    }
}
