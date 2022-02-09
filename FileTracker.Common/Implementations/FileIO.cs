using FileTracker.Common.Interfaces;
using System.Collections.Generic;
using System.IO;

namespace FileTracker.Common.Implementations
{
    public class FileIO : IFile
    {
        public bool DirectoryExists(string path)
        {
            return Directory.Exists(path);
        }

        public bool FileExists(string path)
        {
            return File.Exists(path);
        }

        public IEnumerable<string> GetFiles(string path, string mask)
        {
            return Directory.GetFiles(path, mask);
        }

        public FileStream OpenRead(string path)
        {
            return File.OpenRead(path);
        }

        public string ReadAllText(string path)
        {
            return File.ReadAllText(path);
        }
    }
}