using CSharpTestTasks.Algorithms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace CSharpTestTasks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StringProcessingController : ControllerBase
    {
        private IConfiguration _config;
        private IConfigurationSection _blackList;

        public StringProcessingController(IConfiguration config)
        {
            _config = config;
            _blackList = _config.GetSection("Settings").GetRequiredSection("BlackList");
        }

        private static string alphabet = "abcdefghijklmnopqrstuvwxyz";

        [HttpGet]
        public async Task<ActionResult<StringProcessingResult>> ProcessString(string s, SortMethod sortMethod)
        {
            StringProcessingResult results = new();

            // Check, if string is in black list

            if (_blackList.AsEnumerable().Any(kv => kv.Value == s))
            {
                return BadRequest("Это слово находится в чёрном списке");
            }

            if (StringAlgorithms.HasNonAcceptedSymbols(s, out string symbols))
            {
                return BadRequest($"Были введены неподходящие символы: {symbols}");
            }

            results.ProcessedString = StringAlgorithms.ReverseString(s);

            // Count codepoints

            StringAlgorithms.SymbolCount(s, results.OccurencesCount);

            // Longest substring starting and ending with a vowel

            results.LongestSubstring = StringAlgorithms.LongestSubstring(s) ?? "";

            // Sorting

            char[] sArray = s.ToCharArray();

            switch (sortMethod)
            {
            case SortMethod.Quicksort:
                new QuickSort().Sort(sArray);
                break;
            case SortMethod.Treesort:
                new TreeSort().Sort(sArray);
                break;
            }

            results.SortedString = new string(sArray);

            // Deleting random symbol by using some REST service
            HttpClient client = new();

            int symbolIndex;

            try
            {
                using (HttpResponseMessage response = await client.GetAsync($"{_config["RandomApi"]}?min=0&max={s.Length}"))
                {
                    response.EnsureSuccessStatusCode();

                    var jsonResponse = await response.Content.ReadAsStringAsync();

                    var jsonDoc = JsonDocument.Parse(jsonResponse);

                    symbolIndex = jsonDoc.RootElement[0].GetInt32();
                }
            }
            catch (Exception)
            {
                Random r = new Random();

                symbolIndex = r.Next(0, s.Length);
            }

            results.CutString = s.Remove(symbolIndex, 1);

            return results;
        }
    }
}
