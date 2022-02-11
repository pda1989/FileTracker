using FileTracker.Common.Interfaces;

namespace FileTracker.Common.Implementations
{
    public class BaseStringFilter : IFilter
    {
        public bool IsMatch(string text)
        {
            return !string.IsNullOrEmpty(text);
        }
    }
}