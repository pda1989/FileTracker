namespace FileTracker.Common.Interfaces
{
    public interface ISettings
    {
        string EndpointUrl { get; }

        string FileMask { get; }

        string FilePath { get; }

        string RegexFilter { get; }

        int RequestTimeoutMiliseconds { get; }
    }
}