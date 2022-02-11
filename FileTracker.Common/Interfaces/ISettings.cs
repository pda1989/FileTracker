namespace FileTracker.Common.Interfaces
{
    public interface ISettings
    {
        string FileMask { get; }

        string FilePath { get; }

        string RegexFilter { get; }
    }
}