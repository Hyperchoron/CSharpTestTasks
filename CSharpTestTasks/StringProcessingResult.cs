namespace CSharpTestTasks
{
    public class StringProcessingResult
    {
        public string ProcessedString { get; set; } = "";
        public Dictionary<char, int> OccurencesCount { get; set; } = new Dictionary<char, int>();
        public string LongestSubstring { get; set; } = "";
        public string SortedString { get; set; } = "";
        public string CutString { get; set; } = "";
    }
}
