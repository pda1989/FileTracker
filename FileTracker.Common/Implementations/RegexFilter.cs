using FileTracker.Common.Interfaces;
using System.Text.RegularExpressions;

namespace FileTracker.Common.Implementations
{
    public class RegexFilter : IFilter
    {
        private string _pattern;

        public RegexFilter(string pattern)
        {
            _pattern = pattern;
        }

        public bool IsMatch(string text)
        {
            return Regex.IsMatch(text, _pattern);
        }
    }
}