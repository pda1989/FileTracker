using FileTracker.Common.Interfaces;
using System;
using System.Globalization;

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

        public void Warning(string message)
        {
            WriteMessage("WRN", message);
        }

        private void WriteMessage(string logLevel, string message)
        {
            string timestamp = DateTime.UtcNow.ToString("dd-MM-yyyy HH:mm:ss.fff", CultureInfo.InvariantCulture);

            Console.WriteLine(MessageMask, timestamp, logLevel, message);
        }
    }
}