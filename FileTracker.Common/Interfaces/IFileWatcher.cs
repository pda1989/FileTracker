using FileTracker.Common.Models;
using System;

namespace FileTracker.Common.Interfaces
{
    public interface IFileWatcher : IDisposable
    {
        event EventHandler<FileWatcherEventArgs> OnFileChanged;

        void StopWatching();

        void WatchFiles(string path, string mask);
    }
}