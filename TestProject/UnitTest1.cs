using CSharpTestTasks.Algorithms;

namespace TestProject
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestStringReversal()
        {
            Assert.That(StringAlgorithms.ReverseString("a"), Is.EqualTo("aa"));
            Assert.That(StringAlgorithms.ReverseString("abcdef"), Is.EqualTo("cbafed"));
            Assert.That(StringAlgorithms.ReverseString("abcde"), Is.EqualTo("edcbaabcde"));
        }

        [Test]
        public void TestCharacters()
        {
            string symbols;

            Assert.That(StringAlgorithms.HasNonAcceptedSymbols("abc", out symbols), Is.EqualTo(false));
            Assert.That(symbols, Is.EqualTo(""));

            Assert.That(StringAlgorithms.HasNonAcceptedSymbols("HelloWorld", out symbols), Is.EqualTo(true));
            Assert.That(symbols, Is.EqualTo("HW"));

        }

        [Test]
        public void TestOccurences()
        {
            Dictionary<char, int> occurences = new();

            StringAlgorithms.SymbolCount("abca", occurences);

            Assert.That(occurences.Count, Is.EqualTo(3));
            Assert.That(occurences.ContainsKey('a'), Is.EqualTo(true));
            Assert.That(occurences['a'], Is.EqualTo(2));
        }

        [Test]
        public void TestSubstring()
        {
            Assert.That(StringAlgorithms.LongestSubstring("aa"), Is.EqualTo("aa"));
            Assert.That(StringAlgorithms.LongestSubstring("cbafed"), Is.EqualTo("afe"));
            Assert.That(StringAlgorithms.LongestSubstring("edcbaabcde"), Is.EqualTo("edcbaabcde"));
        }

        [Test]
        public void TestSorting()
        {
            var quicksort = new QuickSort();
            var treesort = new TreeSort();

            char[] str;

            // #1 A
            str = "abcdef".ToCharArray();

            quicksort.Sort(str);

            Assert.That(new string(str), Is.EqualTo("abcdef"));

            // #1 B
            str = "abcdef".ToCharArray();

            treesort.Sort(str);

            Assert.That(new string(str), Is.EqualTo("abcdef"));

            // #2 A
            str = "helloworld".ToCharArray();

            quicksort.Sort(str);

            Assert.That(new string(str), Is.EqualTo("dehllloorw"));

            // #2 B
            str = "helloworld".ToCharArray();

            treesort.Sort(str);

            Assert.That(new string(str), Is.EqualTo("dehllloorw"));
        }
    }
}