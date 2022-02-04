using System;

namespace FileTracker.Common.Interfaces
{
    public interface IFileWatcher : IDisposable
    {
        void StopWatching();

        void WatchFile(string path, string mask);
    }
}