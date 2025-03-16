namespace CSharpTestTasks
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string s = Console.ReadLine() ?? "";

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
