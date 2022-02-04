using System;

namespace FileTracker.Common.Interfaces
{
    public interface ILogger
    {
        void Debug(string message);

        void Error(Exception exception, string message);

        void Error(string message);

        void Info(string message);
    }
}