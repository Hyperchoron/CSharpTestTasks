using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
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

        static int _processingsCount = 0;
        int _maxProcessingsCount;

        public StringProcessingController(IConfiguration config) {
            _config = config;

            _blackList = _config.GetSection("Settings").GetSection("BlackList");

            try
            {
                _maxProcessingsCount = int.Parse(_config.GetSection("Settings").GetSection("ParallelLimit").Value);
            } catch(Exception)
            {
                _maxProcessingsCount = 0;
            }
        }

        private static string alphabet = "abcdefghijklmnopqrstuvwxyz";

        [HttpGet]
        public async Task<ActionResult<StringProcessingResult>> ProcessString(string s, SortMethod sortMethod)
        {
            // Check for parallel connections
            _processingsCount++;

            if (_processingsCount > _maxProcessingsCount)
                return StatusCode(503);

            StringProcessingResult results = new();

            // Check, if string is in black list

            if (_blackList.AsEnumerable().Any(kv => kv.Value == s))
            {
                return BadRequest("Это слово находится в чёрном списке");
            }

            bool panic = false;
            StringBuilder errorMessageBuilder = new();
            foreach (char c in s)
            {
                if (alphabet.IndexOf(c) == -1)
                {
                    if (!panic)
                    {
                        errorMessageBuilder.Append("Были введены неподходящие символы: ");

                        panic = true;
                    }

                    errorMessageBuilder.Append(c);
                }
            }

            if (panic)
            {
                return BadRequest(errorMessageBuilder.ToString());
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

            results.ProcessedString = new string(newString);

            // Count codepoints

            foreach (char c in newString)
            {
                if (results.OccurencesCount.ContainsKey(c)) results.OccurencesCount[c]++;
                else results.OccurencesCount[c] = 1;
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

            results.LongestSubstring = new string(newString).Substring(substringStart, substringEnd - substringStart + 1);

            // Sorting

            switch (sortMethod)
            {
            case SortMethod.Quicksort:
                new QuickSort().Sort(newString);
                break;
            case SortMethod.Treesort:
                new TreeSort().Sort(newString);
                break;
            }

            results.SortedString = new string(newString);

            // Deleting random symbol by using some REST service
            HttpClient client = new();

            int symbolIndex;

            try
            {
                using (HttpResponseMessage response = await client.GetAsync($"{_config["RandomApi"]}?min=0&max={newString.Length}"))
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

                symbolIndex = r.Next(0, newString.Length);
            }

            results.CutString = new string(newString).Remove(symbolIndex, 1);

            _processingsCount--;

            return results;
        }
    }
}
