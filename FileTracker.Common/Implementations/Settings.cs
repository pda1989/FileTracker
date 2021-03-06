using FileTracker.Common.Interfaces;

namespace FileTracker.Common.Implementations
{
    public class Settings : ISettings
    {
        public string EndpointUrl { get; set; }

        public string FileMask { get; set; }

        public string FilePath { get; set; }

        public string RegexFilter { get; set; }

        public int RequestTimeoutMiliseconds { get; set; }
    }
}