using FileTracker.Common.Interfaces;
using System.Collections.Generic;
using System.IO;

namespace FileTracker.Common.Implementations
{
    public class ChangeTracker : IChangeTracker
    {
        private readonly IFile _fileIo;
        private readonly Dictionary<string, long> _files = new Dictionary<string, long>();
        private readonly ILogger _logger;

        public ChangeTracker(ILogger logger, IFile fileIo)
        {
            _logger = logger;
            _fileIo = fileIo;
        }

        public void AddFile(string path)
        {
            if (!_fileIo.FileExists(path))
            {
                _logger.Warning($"It's not possible to add file. The file '{path}' doesn't exist");
                return;
            }

            long fileLength = GetLength(path);
            if (_files.ContainsKey(path))
            {
                _files[path] = fileLength;
                _logger.Debug($"The file '{path}' has been updated. The current length: {fileLength} bytes");
            }
            else
            {
                _files.Add(path, fileLength);
                _logger.Debug($"The file '{path}' has been added. The current length: {fileLength} bytes");
            }
        }

        public void AddFiles(string path, string mask)
        {
            var files = _fileIo.GetFiles(path, mask);
            foreach (var fileName in files)
                AddFile(fileName);
        }

        public string GetDelta(string path)
        {
            if (!_fileIo.FileExists(path))
            {
                _logger.Warning($"It's not possible to get delta for the file. The file '{path}' doesn't exist");
                return string.Empty;
            }

            if (!_files.ContainsKey(path))
            {
                _logger.Warning($"The file '{path}' is not added for tracking changes");
                return string.Empty;
            }

            long currentLength = GetLength(path);

            _logger.Debug($"Calculating delta for the file '{path}'");
            _logger.Debug($"  The previos length: {_files[path]}");
            _logger.Debug($"  The current length: {currentLength}");

            string addedContent;
            using (var file = _fileIo.OpenRead(path))
            {
                file.Seek(_files[path], SeekOrigin.Begin);

                using (var reader = new StreamReader(file))
                    addedContent = reader.ReadToEnd();
            }

            _files[path] = currentLength;

            return addedContent;
        }

        public void RemoveFile(string path)
        {
            if (_files.ContainsKey(path))
            {
                _files.Remove(path);
                _logger.Debug($"The file '{path}' has been removed");
            }
        }

        private static long GetLength(string path)
        {
            return new FileInfo(path).Length;
        }
    }
}