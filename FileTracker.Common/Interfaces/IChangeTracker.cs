namespace FileTracker.Common.Interfaces
{
    public interface IChangeTracker
    {
        void AddFile(string path);

        void AddFiles(string path, string mask);

        string GetDelta(string path);

        void RemoveFile(string path);
    }
}