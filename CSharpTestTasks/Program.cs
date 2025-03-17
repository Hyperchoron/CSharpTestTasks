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

            // Count codepoints

            Dictionary<char, int> codepointCount = new Dictionary<char, int>();

            foreach (char c in newString)
            {
                if (codepointCount.ContainsKey(c)) codepointCount[c]++;
                else codepointCount[c] = 1;
            }

            Console.WriteLine("Количество вхождений каждого символа в обработанной строке:");

            foreach ((char c, int count) in codepointCount)
            {
                Console.WriteLine($" - '{c}': {count}");
            }

            // Longest substring starting and ending with a vowel

            int substringStart;
            int substringEnd;

            // Scan string for vowel from the left
            for (substringStart = 0;
                substringStart < newString.Length && !("aeiouy".Contains(newString[substringStart]));
                substringStart++)
            { }

            // Scan string for vowel from the right
            for (substringEnd = newString.Length - 1;
                substringEnd >= 0 && !("aeiouy".Contains(newString[substringEnd]));
                substringEnd--)
            { }

            Console.Write("Подстрока с гласными в начале и конце: ");
            Console.WriteLine(newString, substringStart, substringEnd - substringStart + 1);

            // Sorting

            Console.WriteLine("Выберите метод сортировки:");
            Console.WriteLine(" [1] - Быстрая сортировка (Quicksort);");
            Console.WriteLine(" [2] - Сортировка деревом (Tree sort).");

            Console.Write("[1, 2]: ");

            string userInput = Console.ReadLine() ?? "";

            switch (userInput)
            {
            case "1":
                new QuickSort().Sort(newString);
                break;
            case "2":
                new TreeSort().Sort(newString);
                break;
            default:
                Console.WriteLine("Без сортировки");
                break;
            }

            Console.WriteLine(newString);
        }
    }
}
