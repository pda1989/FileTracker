namespace FileTracker.Common.Models
{
    public class FileWatcherEventArgs
    {
        public string AddedContent { get; set; }

        public string FileName { get; set; }
    }
}