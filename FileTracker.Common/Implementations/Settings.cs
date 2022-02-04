using FileTracker.Common.Interfaces;

namespace FileTracker.Common.Implementations
{
    public class Settings : ISettings
    {
        public string FileMask { get; set; }

        public string FilePath { get; set; }
    }
}