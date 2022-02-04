namespace FileTracker.Common.Interfaces
{
    public interface IFile
    {
        bool DirectoryExists(string path);

        string ReadAllText(string path);
    }
}