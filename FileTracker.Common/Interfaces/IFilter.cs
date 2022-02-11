namespace FileTracker.Common.Interfaces
{
    public interface IFilter
    {
        bool IsMatch(string text);
    }
}