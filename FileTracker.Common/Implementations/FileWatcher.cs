using FileTracker.Common.Interfaces;
using FileTracker.Common.Models;
using System;
using System.IO;

namespace FileTracker.Common.Implementations
{
    public class FileWatcher : IFileWatcher
    {
        private readonly IChangeTracker _changeTracker;
        private readonly IFile _fileIo;
        private readonly ILogger _logger;
        private FileSystemWatcher _watcher;

        public FileWatcher(ILogger logger, IFile fileIo, IChangeTracker changeTracker)
        {
            _logger = logger;
            _fileIo = fileIo;
            _changeTracker = changeTracker;
        }

        public event EventHandler<FileWatcherEventArgs> OnFileChanged;

        public void Dispose()
        {
            Stop();
        }

        public void StopWatching()
        {
            Stop();
        }

        public void WatchFiles(string path, string mask)
        {
            if (_watcher != null)
            {
                _logger.Error("The file tracker is already running");
                return;
            }

            _logger.Info("Start the file tracker");
            _logger.Debug($"  File path: '{path}'");
            _logger.Debug($"  File mask: '{mask}'");

            InitFiles(path, mask);

            if (!_fileIo.DirectoryExists(path))
            {
                _logger.Error($"The directory doesn't exist");
                return;
            }

            _watcher = new FileSystemWatcher(path, mask)
            {
                NotifyFilter =
                    NotifyFilters.Size |
                    NotifyFilters.FileName |
                    NotifyFilters.DirectoryName |
                    NotifyFilters.CreationTime,
                EnableRaisingEvents = true
            };

            _watcher.Changed += OnChanged;
            _watcher.Created += OnCreated;
            _watcher.Deleted += OnDeleted;
            _watcher.Renamed += OnRenamed;
            _watcher.Error += OnError;
        }

        private void InitFiles(string path, string mask)
        {
            _changeTracker.AddFiles(path, mask);
        }

        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            if (e.ChangeType != WatcherChangeTypes.Changed)
                return;

            var delta = _changeTracker.GetDelta(e.FullPath);

            _logger.Info($"The file '{e.FullPath}' has been changed");
            _logger.Debug($"  Delta '{delta}'");

            OnFileChanged?.Invoke(this, new FileWatcherEventArgs { AddedContent = delta, FileName = e.FullPath });
        }

        private void OnCreated(object sender, FileSystemEventArgs e)
        {
            if (e.ChangeType != WatcherChangeTypes.Created)
                return;

            _changeTracker.AddFile(e.FullPath);
        }

        private void OnDeleted(object sender, FileSystemEventArgs e)
        {
            if (e.ChangeType != WatcherChangeTypes.Deleted)
                return;

            _changeTracker.RemoveFile(e.FullPath);
        }

        private void OnError(object sender, ErrorEventArgs e)
        {
            Exception exception = e?.GetException();
            if (exception != null)
                _logger.Error(exception, exception.Message);
        }

        private void OnRenamed(object sender, RenamedEventArgs e)
        {
            if (e.ChangeType != WatcherChangeTypes.Renamed)
                return;

            _changeTracker.RemoveFile(e.OldFullPath);
            _changeTracker.AddFile(e.FullPath);
        }

        private void Stop()
        {
            _watcher?.Dispose();
            _watcher = null;
            _logger.Info("The file tracker has been stoped");
        }
    }
}