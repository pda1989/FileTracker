using FileTracker.Common.Interfaces;
using System.IO;

namespace FileTracker.Common.Implementations
{
    public class FileIO : IFile
    {
        public bool DirectoryExists(string path)
        {
            return Directory.Exists(path);
        }

        public string ReadAllText(string path)
        {
            return File.ReadAllText(path);
        }
    }
}