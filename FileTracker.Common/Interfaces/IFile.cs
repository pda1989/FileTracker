using System.Collections.Generic;
using System.IO;

namespace FileTracker.Common.Interfaces
{
    public interface IFile
    {
        bool DirectoryExists(string path);

        bool FileExists(string path);

        IEnumerable<string> GetFiles(string path, string mask);

        FileStream OpenRead(string path);

        string ReadAllText(string path);
    }
}