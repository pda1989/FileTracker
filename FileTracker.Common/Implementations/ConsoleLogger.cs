using FileTracker.Common.Interfaces;
using System;

namespace FileTracker.Common.Implementations
{
    internal class ConsoleLogger : ILogger
    {
        private const string MessageMask = "{0} [{1}] {2}";

        public void Debug(string message)
        {
            WriteMessage("DBG", message);
        }

        public void Error(Exception exception, string message)
        {
            WriteMessage("ERR", message);
        }

        public void Error(string message)
        {
            WriteMessage("ERR", message);
        }

        public void Info(string message)
        {
            WriteMessage("INF", message);
        }

        private void WriteMessage(string logLevel, string message)
        {
            Console.WriteLine(MessageMask, DateTime.UtcNow, logLevel, message);
        }
    }
}